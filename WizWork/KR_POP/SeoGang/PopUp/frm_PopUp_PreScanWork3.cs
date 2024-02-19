using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using WizWork.Popup;

//*******************************************************************************
//프로그램명    frm_PopUp_PreScanWork.cs
//메뉴ID        
//설명          frm_PopUp_PreScanWork 메인소스입니다.
//작성일        2019.07.29
//개발자        허윤구
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//  19_0729     허윤구  * 성형 하나에 재단 2개가 필요한 케이스에 따라 수정보완.
//                          (InsertX)
//  2019.08.01 > 허윤구    FMB가 어떤 이유로 이미 재단창고에 가 있을 케이스에 대한 로직추가.
//*******************************************************************************

namespace WizWork
{
    public partial class frm_PopUp_PreScanWork3 : Form
    {

        private string m_ProcessID = "";        //공정id
        private string m_MachineID = "";        //머신id
        private string m_ArticleID = "";        //품명id   

        private string m_LabelID = "";
        private string m_BuyerArticleNo = "";
        private string m_Article = "";

        private string m_LabelGubun = "";       //라벨구분  
        private string m_Inspector = "";        //검사자
        private string m_MoldID = "";           //몰드. 금형ID

        private string m_UnitClss = "";         // 단위
        private string m_EffectDate = "";       // 유효일? 효과일?    (스캔 후 초기화)     
        private string m_MtrExceptYN = "";      // 예외처리 체크용도

        private double m_RemainQty = 0;         //      입고수량  (스캔 후 초기화)
        private double m_LocRemainQty = 0;      //    '자품목 현 재고량  (스캔 후 초기화)
        //2019.06.14 허윤구 추가.
        private string WH_AR_m_LocID = "";      //     현재창고 위치 체크. > 에러구문 상세화 목적.

        //2020.01.07 허윤구 추가.
        private string Wh_Ar_ChildArticleID = "";  // 내가 스캔한 제품의 부모ArticleID >> 즉, 작지의 ArticleID

        //2020.01.07 허윤구 추가.
        private string Wh_Ar_InstID = "";       // PL_Input 작지의 해당 대상 InstID.
        private string Wh_Ar_InstID_Seq = "";   // PL_Input 작지의 해당 대상 InstID_Seq.



        string[] Message = new string[2];  // 메시지박스 처리용도.

        string WhAr_MoveArticle_I = string.Empty;    // I_FMB_재단 자동이동시의 Article.(Whole-Area)

        WizWorkLib Lib = new WizWorkLib();

        List<ChildLabel> lstChildLabel = new List <ChildLabel>();
        // 각 품명의 라벨 리스트
        Dictionary<string, List<ChildLabel>> dicSubList = new Dictionary<string, List<ChildLabel>>(); 

        public frm_PopUp_PreScanWork3()
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
        }
        public frm_PopUp_PreScanWork3(string strProcessID, string strMachineID, string strMoldID)
        {
            InitializeComponent();
            m_ProcessID = strProcessID;
            m_MachineID = strMachineID;
            m_MoldID = strMoldID;
            SetScreen();  //TLP 사이즈 조정
        }

        public frm_PopUp_PreScanWork3(string strProcessID, string strMachineID, string strMoldID, List<ChildLabel> lstChildLabel)
        {
            InitializeComponent();

            m_ProcessID = strProcessID;
            m_MachineID = strMachineID;
            m_MoldID = strMoldID;
            this.lstChildLabel = lstChildLabel;

            SetScreen();  //TLP 사이즈 조정
        }

        #region 테이블 레이아웃 패널 사이즈 조정
        private void SetScreen()
        {
            tlpMain.Dock = DockStyle.Fill;
            foreach (Control control in tlpMain.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(1, 1, 1, 1);
                foreach (Control contro in control.Controls)
                {
                    contro.Dock = DockStyle.Fill;
                    contro.Margin = new Padding(1, 1, 1, 1);
                    foreach (Control contr in contro.Controls)
                    {
                        contr.Dock = DockStyle.Fill;
                        contr.Margin = new Padding(1, 1, 1, 1);
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(1, 1, 1, 1);
                            foreach (Control con in cont.Controls)
                            {
                                con.Dock = DockStyle.Fill;
                                con.Margin = new Padding(1, 1, 1, 1);
                                foreach (Control co in con.Controls)
                                {
                                    co.Dock = DockStyle.Fill;
                                    co.Margin = new Padding(1, 1, 1, 1);                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        //로드.
        private void frm_PopUp_PreScanWork_Load(object sender, EventArgs e)
        {
            // 전체 클리어.
            SetFormDataClear();

            // 시작처리 버튼숨기기.
            //tlpBottomChoice.ColumnStyles[1].Width = 0;
            //tlpBottomChoice.ColumnStyles[2].Width = 0;

            // 시작작업 처리
            Form_Activate();

            int DetSeq = ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq);

            setDataGrid__ChildArticle();
        }

        #region 실적에서 들고온 정보로 하위 그리드 세팅 : dgdMain, dgdSub

        private void setDataGrid__ChildArticle()
        {
            dgdMain.Rows.Clear();
            dgdSub.Rows.Clear();

            try
            {
                // 메인 그리드 세팅 + 각 품명의 라벨 리스트 dicSubList 세팅
                for (int i = 0; i < lstChildLabel.Count; i++)
                {
                    int index = i + 1;
                    dgdMain.Rows.Add(index.ToString()
                                                , lstChildLabel[i].ArticleID
                                                , lstChildLabel[i].BuyerArticleNo
                                                , lstChildLabel[i].Article
                                                , stringFormatN0(lstChildLabel[i].ReaQty)
                                                , stringFormatN0(lstChildLabel[i].NeedQty)
                                                , stringFormatN0(lstChildLabel[i].DeficiencyQty)
                                                );

                    // 각 품명의 라벨 리스트 dicSubList 세팅
                    List<ChildLabel> lstCL = new List<ChildLabel>();
                    lstCL.Add(lstChildLabel[i]);
                    dicSubList.Add(lstChildLabel[i].ArticleID, lstCL);
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        #endregion

        #region 하위 그리드 취소 버튼 클릭 > 해당 하위품 취소
        private void dgdSub_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgdSub.Columns[e.ColumnIndex].Name == "Cancel2"
                && dgdSub.SelectedRows[0].Cells["Cancel2"].Value.ToString().Trim().Equals("-") == false)
                {
                    if (MessageBox.Show("해당 하위품을 투입 취소하시겠습니까?", "취소 전 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string sArticleID = dgdSub.SelectedRows[0].Cells["ArticleID2"].Value.ToString();
                        string sLabelID = dgdSub.SelectedRows[0].Cells["LabelID2"].Value.ToString();
                        double CancelQty = ConvertDouble(dgdSub.SelectedRows[0].Cells["LocRemainQty2"].Value.ToString());

                        dgdSub.Rows.Remove(dgdSub.SelectedRows[0]);

                        // 삭제하면서 dgdMain 에 부족량 업데이트 하기
                        // 1. 서브 그리드 다시 수량 합산
                        double ProdQty = 0;
                        for (int i = 0; i < dgdSub.Rows.Count; i++)
                        {
                            double LocRemainQty = ConvertDouble(dgdSub.Rows[i].Cells["LocRemainQty2"].Value.ToString());
                            ProdQty += LocRemainQty;
                        }

                        // 2 dicSubList 부족량 변경 + 
                        // 2.5 메인그리드 해당 품명 찾아서 부족량 변경
                        for (int i = 0; i < dgdMain.Rows.Count; i++)
                        {
                            string mArticleID = dgdMain.Rows[i].Cells["ArticleID"].Value.ToString();
                            double WorkQty = 0;
                            if (mArticleID.Trim().Equals(sArticleID.Trim()))
                            {
                                foreach(string Key in dicSubList.Keys)
                                {
                                    if (Key.Trim().Equals(mArticleID.Trim()))
                                    {
                                        if (dicSubList[Key].Count > 0)
                                        {
                                            

                                            List<ChildLabel> lstCL = new List<ChildLabel>();
                                            lstCL = dicSubList[Key];

                                            foreach (ChildLabel cl in lstCL)
                                            {
                                                if (cl.LabelID.Trim().Equals(sLabelID))
                                                {
                                                    lstCL.Remove(cl);
                                                    break;
                                                }
                                            }
                                        }

                                        WorkQty = (double)dicSubList[Key][0].WorkQty;
                                    }
                                }

                                double DeficiencyQty = WorkQty - ProdQty;
                                if (DeficiencyQty < 0)
                                {
                                    DeficiencyQty = 0;
                                }

                                dgdMain.Rows[i].Cells["DeficiencyQty"].Value = stringFormatN0(DeficiencyQty);
                            }
                        }

                        
                    }
                }
                else if (dgdSub.SelectedRows[0].Cells["Cancel2"].Value.ToString().Trim().Equals("-"))
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("선 스캔한 라벨은 투입취소가 불가능합니다.", "[선스캔라벨 취소 불가]", 0, 1);
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }

            
        }

        #endregion

        // After Load
        private void frm_PopUp_PreScanWork_Shown(object sender, EventArgs e)
        {
            // 포커스
            txtBarCodePreScan.Focus();
            txtBarCodePreScan.Select(0, 0);
            txtBarCodePreScan.Focus();
        }


        #region 전체 클리어
        private void SetFormDataClear()
        {
            this.txtBarCodePreScan.Text = string.Empty;
            //this.txtArticle.Text = string.Empty;
            //this.txtBuyerArticleNo.Text = string.Empty;

            // 스캔체크에 통과할때까지 '시작처리' 버튼은 사용불가. 
            //btnOK.Enabled = false;
        }

        #endregion


        #region Form_Activate 묶음

        #region 시작작업
        private void Form_Activate()
        {
            try
            {
                Wh_Ar_InstID = CheckLabelID(Frm_tprc_Main.g_tBase.sLotID);
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            
        }

        #endregion

        #region  시작용 체크 + Textbox 채우기.
        private string CheckLabelID(string strBarCode)
        {
            string strInstID = "";
            string strInstDetSeq = "";

            try
            {
                string sMoldID = "";

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PLotID", strBarCode);
                sqlParameter.Add("ProcessID", m_ProcessID); //SearchProcessID());                
                sqlParameter.Add("MoldID", sMoldID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_Chkworklotid", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    m_MtrExceptYN = Lib.CheckNull(dr["MtrExceptYN"].ToString());//PLotID가 라벨일때 pl_input의 MtrExceptYN                    
                    strInstID = Lib.CheckNull(dr["InstID"].ToString());//PLotID가 라벨일때 pl_input의 InstID                                        
                    strInstDetSeq = Lib.CheckNull(dr["InstDetSeq"].ToString());
                    Wh_Ar_InstID_Seq = strInstDetSeq;

                    double InstQty = 0;
                    double InstWorkQty = 0;
                    double InstRemainQty = 0;

                    double.TryParse(dr["InstQty"].ToString(), out InstQty);
                    double.TryParse(dr["InstWorkQty"].ToString(), out InstWorkQty);
                    InstRemainQty = InstQty - InstWorkQty;                    

                    Frm_tprc_Main.g_tBase.sArticleID = Lib.CheckNull(dr["ArticleID"].ToString());//mt_article
                    Frm_tprc_Main.g_tBase.Article = Lib.CheckNull(dr["pldArticle"].ToString());//mt_article
                    Frm_tprc_Main.g_tBase.OrderID = Lib.CheckNull(dr["OrderID"].ToString());//pl_input
                    Frm_tprc_Main.g_tBase.OrderNO = Lib.CheckNull(dr["OrderNO"].ToString());//order

                    ///////////////
                    int WorkQty = 0;
                    int OrderSeq = 0;
                    int.TryParse(dr["ProdQtyPerBox"].ToString(), out WorkQty);
                    int.TryParse(dr["OrderSeq"].ToString(), out OrderSeq);
                    Frm_tprc_Main.g_tBase.WorkQty = WorkQty;//wk_labelprint의 수량
                    //전역변수 WorkQty(생산량)에 박스당 수량을 집어넣는다? 왜?? 수정이 필요해보임
                    Frm_tprc_Main.g_tBase.OrderUnit = Lib.CheckNull(dr["UnitClss"].ToString());//order
                    Frm_tprc_Main.g_tBase.OrderSeq = OrderSeq;
                    Frm_tprc_Main.g_tBase.Basis = "";
                    Frm_tprc_Main.g_tBase.BasisID = 0;

                    //m_UnitClss = Lib.CheckNull(dr["UnitClss"].ToString());//pl_inputdet articleid의 UnitClss
                    //m_UnitClssName = Lib.CheckNull(dr["UnitClssName"].ToString());

                // 2020.04.22 데이터 넣는곳
                    //txtArticle.Text = Lib.CheckNull(dr["pldArticle"].ToString());

                    //txtBuyerArticleNo.Text = Lib.CheckNull(dr["BuyerArticleNo"].ToString());                   

                }

            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return "";
            }

            return strInstID;
        }

        #endregion

        #endregion


        #region 스캔 묶음

        #region 스캔

        // 스캔 텍스트 체크.
        private void txtBarCodePreScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    txtBarCodePreScan.Text = txtBarCodePreScan.Text.Trim().ToUpper();
                    if (BarcodeEnter())
                    {
                        //if (btnOK.Tag != null
                        //    && btnOK.Tag.ToString().Equals("OK"))
                        //{
                        //    btnOK_Click(null, null);
                        //}

                        //부족재고량이 이제 없는지 확인하기
                        //if (CheckAllDeficiencyQty())
                        //{
                        //    btnOK_Click(null, null);
                        //}
                    }

                    txtBarCodePreScan.Text = string.Empty;
                    txtBarCodePreScan.Focus();
                    //txtBarCodePreScan.Focus();
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(ex.Message.ToString(), "[오류]", 0, 1);
            }            
        }

        private bool CheckAllDeficiencyQty()
        {
            bool flag = true;

            for(int i = 0; i < dgdMain.Rows.Count; i++)
            {
                double DeficiencyQty = ConvertDouble(dgdMain.Rows[i].Cells["DeficiencyQty"].Value.ToString());

                if (DeficiencyQty > 0)
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        // 스캔버튼 클릭.
        private void cmdBarCodePreScan_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMKeypad.g_Name = "바코드 스캔";
            POPUP.Frm_CMKeypad FK = new POPUP.Frm_CMKeypad();
            POPUP.Frm_CMKeypad.KeypadStr = txtBarCodePreScan.Text.Trim();
            if (FK.ShowDialog() == DialogResult.OK)
            {
                txtBarCodePreScan.Text = FK.tbInputText.Text;
                if (BarcodeEnter())
                {
                    if (btnOK.Tag != null
                            && btnOK.Tag.ToString().Equals("OK"))
                    {
                        btnOK_Click(null, null);
                    }

                }

                txtBarCodePreScan.Text = string.Empty;
                txtBarCodePreScan.Focus();
            }
            else
            {
                txtBarCodePreScan.Text = string.Empty;
            }
        }

        #endregion

        #region 스캔 바코드 기반 데이터 가져오기

        // 스캔 바코드 기반 조건체크 및 데이터 가져오기.
        private bool BarcodeEnter()
        {
            Wh_Ar_ChildArticleID = string.Empty;

            string Barcode = txtBarCodePreScan.Text.Trim();

            try
            { 
                // 1. 스켄 ID  기본 정상여부 확인
                if (!(LF_Check_ScanData(Barcode)))
                {
                    Message[0] = "[바코드오류]";
                    Message[1] = "바코드 형식이 정상적이지 않습니다.";
                    throw new Exception();
                }

                //2. 바코드 체크.
                if (!(BarCodeCheck(Barcode)))
                {
                    Message[0] = "[바코드체크]";
                    Message[1] = "바코드에 기반한 데이터를 찾지 못했습니다. \r\n" +
                        "바코드 정보와 공정코드를 확인해 주세요.";
                    throw new Exception();
                }

                // 
                //if (Wh_Ar_ChildArticleID != m_ArticleID)
                //{
                //    Message[0] = "[스캔오류]";
                //    Message[1] = "하위품이 아니거나 잘못된 바코드입니다. \r\n" +
                //        Wh_Ar_ChildArticleID + "미일치" + m_ArticleID;
                //    throw new Exception();
                //}

                if ((m_RemainQty == 0) || (m_RemainQty < 0))
                {
                    Message[0] = "[라벨오류]";
                    Message[1] = "입력된 롯트는 현재 재고량이 0이하입니다.\r\n" +
                                   "재고를 모두 소진하였습니다.";
                    throw new Exception();
                }

                // 하위품 맞는지 체크하기
                if (CheckChildArticleID() == false)
                {
                    throw new Exception();
                }

                return true;               
            }

            catch (Exception)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                WH_AR_m_LocID = "";
                return false;
            }
        }

        #endregion

        #region 데이터그리드 FOR 돌면서 해당 품명이 맞는지 체크 + 이미 투입한 품목이라면!!! 투입완료되었다고 메시지 출력

        // 해당 품명 맞는지 보고 맞으면 → 라벨 추가 → 부족재고량 수정
        // 만약에 부족재고량이 0인 애를 추가를 한다면? 추가를 막아야 됨
        // 품명이 안맞아도 막아야 되고
        private bool CheckChildArticleID()
        {
            bool flag = true;

            int OkCnt = 0; // 투입 완료한 갯수

            bool IsHere = false; // 하위품 그리드에 같은 품명이 있는지만 체크하는 플래그

            tabControl.SelectedIndex = 0; // 하위품목 탭으로 그냥 이동시킴 → 추후 해당하는 하위품목 라벨 리스트를 보여줘야 됨

            for (int i = 0; i < dgdMain.Rows.Count; i++)
            {
                string ArticleID = dgdMain.Rows[i].Cells["ArticleID"].Value.ToString().Trim();
                double DeficiencyQty = ConvertDouble(dgdMain.Rows[i].Cells["DeficiencyQty"].Value.ToString().Trim());

                // 스캔한 바코드 품명아이디
                if (this.m_ArticleID.Trim().Equals(ArticleID))
                {
                    IsHere = true;

                    if (DeficiencyQty <= 0)
                    {
                        Message[0] = "[부족재고량 없음]";
                        Message[1] = "해당 품목은 부족재고량이 없으므로 투입이 불가능합니다.";
                        flag = false;
                    }
                    else
                    {
                        // dgdMain.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                        // 행 글자 색상변환

                       

                        // dicSubList 의 부족재고량도 수정
                        // dicSubList 에 라벨 추가
                        foreach(string Key in dicSubList.Keys)
                        {
                            if (Key.Trim().Equals(m_ArticleID))
                            {
                                if (dicSubList[Key].Count > 0)
                                {
                                    // 라벨 중복 체크하기
                                    List<ChildLabel> lstCL = dicSubList[Key];
                                    foreach(ChildLabel clabel in lstCL)
                                    {
                                        if (clabel.LabelID.Trim().Equals(m_LabelID.Trim()))
                                        {
                                            Message[0] = "[라벨 오류]";
                                            Message[1] = "해당 라벨은 이미 투입되었습니다.";
                                            return false;
                                        }
                                    }

                                    ChildLabel cl = new ChildLabel(m_LabelID, m_ArticleID, m_Article, m_BuyerArticleNo, dicSubList[Key][0].ReaQty, (float)m_LocRemainQty, 0);
                                    dicSubList[Key][0].DeficiencyQty = (float)DeficiencyQty;
                                    dicSubList[Key].Add(cl);
                                }
                            }
                        }

                        // dgdMain 에 부족재고량 수정
                        DeficiencyQty -= m_LocRemainQty;
                        if (DeficiencyQty <= 0) { DeficiencyQty = 0; }
                        dgdMain.Rows[i].Cells["DeficiencyQty"].Value = stringFormatN0(DeficiencyQty);
                    }
                }
            }

            // 하위품에 있는게 없으면! 하위품이 아니라는 메시지 출력
            if (IsHere == false)
            {
                Message[0] = "[라벨 오류]";
                Message[1] = "해당 품목은 하위품이 아닙니다.";
                flag = false;
            }

            // 하위품 정보 데이터그리드 행 갯수 = 투입 완료한 갯수 같으면 → 테그에 오케이 넣기.
            if (dgdMain.Rows.Count == OkCnt)
            {
                btnOK.Tag = "OK";
            }

            return flag;
        }

        #endregion

        #region LF_Check_ScanData 펑션
        /// <summary>
        /// 스켄 ID  기본 정상여부 확인
        /// </summary>
        /// <returns></returns>
        private bool LF_Check_ScanData(string strBarcode)
        {
            bool blResult = true;
            if (strBarcode != "")
            {
                // 입고 원자재라면, I로 시작하겠지.
                if (strBarcode.ToUpper().Contains("I"))
                {
                    //'입고 Stuffinsub LotID 11자리
                    if (!(strBarcode.Trim().Length == 11))
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("코드가 잘못되었습니다.", "[바코드 길이오류]", 0, 1);
                        txtBarCodePreScan.Text = "";
                        blResult = false;
                    }
                    return blResult;
                }
                // 첫번째 공정에서 나온 라벨은 뭘로 시작해야 하나요??
                else if (strBarcode.ToUpper().Contains("C"))
                {
                    //'공정 wk_Label LotID 11자리
                    if (!(strBarcode.Trim().Length == 11))
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("코드가 잘못되었습니다.", "[바코드 길이오류]", 0, 1);
                        txtBarCodePreScan.Text = "";
                        blResult = false;
                    }
                    return blResult;
                }
                else
                { blResult = false; }
            }
            blResult = false;
            return blResult;
        }

        #endregion
        
        #region BarCodeCheck 펑션

        /// <summary>
        /// LotID에 해당하는 ArticleID 가져오기
        /// 하위품 스캔 체크
        /// </summary>
        /// <param name="strBarCode"></param>
        private bool BarCodeCheck(string strBarCode)
        {
            DataRow dr = null;
            try
            {

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("LotID", strBarCode);
                sqlParameter.Add("ProcessID", m_ProcessID);
                sqlParameter.Add("MachineID", m_MachineID);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sLotInfoByLotID", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];  

                    m_ArticleID = dr["ArticleID"].ToString().Trim();
                    m_LabelGubun = dr["LabelGubun"].ToString().Trim();                    //라벨구분 1은 원자재 
                    m_Inspector = dr["Inspector"].ToString().Trim();                      //검사자
                    double.TryParse(dr["RemainQty"].ToString(), out m_RemainQty);         //전체재고량
                    double.TryParse(dr["LocRemainQty"].ToString(), out m_LocRemainQty);   //해당창고재고량
                    WH_AR_m_LocID = dr["LocID"].ToString().Trim();                        //현재 위치하고 있는 창고.
                    m_UnitClss = dr["UnitClss"].ToString();
                    m_BuyerArticleNo = dr["BuyerArticleNo"].ToString();
                    m_Article = dr["Article"].ToString();
                    m_LabelID = dr["LotID"].ToString();
                    m_EffectDate = Lib.MakeDateTime("yyyyMMdd", dr["EffectDate"].ToString());

                    if (m_MtrExceptYN == "N")//예외처리YN : 예외처리 아닐때
                    {                   
                        string[] sProTerm = Frm_tprc_Main.gs.GetValue("Work", "ProTerm", "ProTerm").Split('|');//배열에 공정별 조건 넣기
                        foreach (string ProTerm in sProTerm)
                        { 
                            if (ProTerm.Length > 4)
                            {
                                if (m_ProcessID == ProTerm.Substring(0, 4))
                                {
                                    if (ProTerm.Substring(4, 1) == "A")//숙성시간
                                    {
                                        //숙성시간YN AgingYN = Y면 숙성시간 24시간 지났으므로 사용가능
                                        if (dr["AgingYN"].ToString() != "Y")
                                        {
                                            Message[0] = "[숙성시간 24시간미만 재료 사용불가]";
                                            Message[1] = "재단 입고 이후 24시간 경과 되지 않은 " + Lib.CheckNull(dr["AgingTime"].ToString()) + "시간 경과된 )  재료 사용 불가능 합니다." +
                                                "숙성시작시간은 " + Lib.CheckNull(dr["AgingStartTime"].ToString()) + " 입니다.";
                                            throw new Exception();
                                        }
                                    }
                                    else if (ProTerm.Substring(4, 1) == "D")//배치검사
                                    {
                                        if (strBarCode.Substring(0, 1) != "I")
                                        {
                                            //배치검사YN DefectYN == N일때 배치검사 통과
                                            if (dr["DefectYN"].ToString() == "")//.Trim() == "")//값이 없으므로, 수행되지 않았음.
                                            {
                                                Message[0] = "[배치검사 오류]";
                                                Message[1] = "배치검사가 수행되지 않았습니다. \r\n  실험실에서 배치검사를 진행하여주십시오.";
                                                throw new Exception();
                                            }
                                            else if (dr["DefectYN"].ToString().ToUpper() == "Y")
                                            {
                                                Message[0] = "[배치검사 오류]";
                                                Message[1] = "배치검사를 통과하지 못했습니다. \r\n  해당 품목은 사용할 수 없습니다.";
                                                throw new Exception();
                                            }
                                        }                                        
                                    }
                                    else if (ProTerm.Substring(4, 1) == "E")//유효기간
                                    {
                                        if (dr["ChkEffect"].ToString() == "Y")//유효기간 체크여부YN
                                        {
                                            if (dr["EffectYN"].ToString() != "Y")//유효기간 EffectYN = Y일때 사용가능
                                            {
                                                if (dr["EffectDate"].ToString().Trim() == "")
                                                {
                                                    Message[0] = "[유효기간 없음]";
                                                    Message[1] = "유효기간이 입력되어있지 않습니다. \r\n 해당 품목은 사용할 수 없습니다. \r\n " +
                                                        "자재입고 담당자에게 유효기간 입력을 요청하세요.";
                                                }
                                                else
                                                {
                                                    Message[0] = "[유효기간 오류]";
                                                    Message[1] = "유효기간이 지났습니다. 해당 품목은 사용할 수 없습니다.";
                                                }

                                                throw new Exception();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (Find_BOM_Child())
                    {
                        return true;
                    }
                    else
                    {
                        // BOM_Child에 실패할 수 있는 케이스.

                        // 1. 작지번호상 Seq가 있는데, ArticleChild에 동일 DetSeq가 없는 경우.
                        string[] DetCountYN = new string[2];
                        string Query = "select  cnt = COUNT(*)  from pl_InputDetArticleChild  where InstID =  '" + Wh_Ar_InstID + "'  and DetSeq = '" + Wh_Ar_InstID_Seq + "'  ";
                        DetCountYN = DataStore.Instance.ExecuteQuery(Query, false);
                        if (Lib.CheckNull(DetCountYN[1]).ToString() == "0")
                        {
                            Message[0] = "[작업지시 등록오류]";
                            Message[1] = "작업지시 순서와 작업하위품 순서등록이 일치하지 않습니다. \r\n" +
                                "위저드정보시스템으로 문의해 주세요.(pl_InputDetArticleChild) \r\n" +
                                "InstID= " + Wh_Ar_InstID + ", DetSeq= " + Wh_Ar_InstID_Seq;
                            throw new Exception();
                        }
                        else
                        {
                            Message[0] = "[bom 실패]";
                            Message[1] = "해당 품목의 상위 BOM으로 작업지시를 내리지 않았습니다. \r\n" +
                                "BOM 변경 후, 새로운 작업지시를 내려주세요.";
                            throw new Exception();
                        }                        
                    }
                    
                }
                else
                {
                    // dt.rows count가 0 이야.
                    // 근데, 여기 들어오는 케이스가 현재 발견된게 지금 3건.
                    //  1. 입고승인/ 2. stuffinsub 의 outwareyn = y 인 케이스.
                    //  3. 하위품 사라진 경우(DB삭제)
                    //  이것들을 가려내야 한다.   (20_0330_허윤구)

                    //stuffinsub outware y or n
                    string[] CheckYN = new string[2];
                    string Query = "select outwareyn from stuffinsub where lotid = '" + strBarCode + "'";
                    CheckYN = DataStore.Instance.ExecuteQuery(Query, false);
                    string OutCheckYN = CheckYN[1];

                    string[] DeleteBarcodeYN = new string[2];
                    string sql_2 = "select cnt = COUNT(*) from wk_result where labelid = '" + strBarCode + "'";
                    DeleteBarcodeYN = DataStore.Instance.ExecuteQuery(sql_2, false);


                    if (OutCheckYN == "Y")
                    {
                        Message[0] = "[재고소진]";
                        Message[1] = "입력된 자재는 현재 재고량이 0이하입니다.\r\n" +
                                       "재고를 모두 소진하였습니다.";
                        throw new Exception();
                    }
                    else if (DeleteBarcodeYN[1] == "0")
                    {
                        Message[0] = "[하위품 소실]";
                        Message[1] = "해당 하위품( " + strBarCode + " )은 승인되지 않은 품목이거나 삭제처리된 Lot입니다.";
                        throw new Exception();
                    }
                    else
                    {
                        Message[0] = "[입고승인]";
                        Message[1] = "해당 품목은 승인되지 않은 품목이거나 입고내역이 없는 품목이므로 사용할 수 없습니다.";
                        throw new Exception();
                    }
                }          
            }
            catch (Exception)
            {
                m_ArticleID = "";
                m_LabelGubun = "";
                m_Inspector = "";
                m_RemainQty = 0;
                m_LocRemainQty = 0;
                WH_AR_m_LocID = "";
                m_UnitClss = "";
                m_EffectDate = "";
                txtBarCodePreScan.Text = "";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return false;
            }
        }

        #endregion

        private bool Find_BOM_Child()
        {
            try
            {
                // BOM 풀지말고 작지의 CHILD_ARTICLE_ID 를 가져와서 비교하는 방식으로 해.
                // PL_INPUTDET_CHILDARTICLE
                // 2020.01.07 허윤구
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();
                sqlParameter.Add("sInstID", Wh_Ar_InstID);
                sqlParameter.Add("sInstSeq", Wh_Ar_InstID_Seq);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_PlanInput_sPlanInputDetArticle", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Wh_Ar_ChildArticleID = dt.Rows[0]["CHildArticleID"].ToString();
                }

                if (Wh_Ar_ChildArticleID == string.Empty)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                return false;
            }                       
        }
        #endregion


        #region 시작, 취소 버튼선택.

        // 시작처리 버튼 선택시.
        private void btnOK_Click(object sender, EventArgs e)
        {

            if (CheckData())
            {
                if (SaveData())
                {
                    DialogResult = DialogResult.OK;
                    btnCancel_Click(null, null);
                }
            }

            

            //// DB 임시 인서트.
            //if (StartHandleWorking_Click() == true)
            //{
            //    DialogResult = DialogResult.OK;
            //    btnCancel_Click(null, null);
            //}
            //else
            //{
            //    Message[0] = "[오류]";
            //    Message[1] = string.Format("오류! 관리자에게 문의\r\n");
            //    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            //}

        }

        #region 잔량이동처리 메서드

        private bool SaveData()
        {
            try
            {
                int successCount = 0;
                List<string> lstSuccessMsg = new List<string>();

                foreach(string Key in dicSubList.Keys)
                {
                    List<ChildLabel> lstCL = dicSubList[Key];

                    if (lstCL.Count > 1)
                    {
                        // 창고 아이디 하드코딩
                        string LocID = "A0001";
                        ChildLabel ALot = lstCL[0];

                        double NeedQty = ALot.NeedQty;
                        double ProdCapaA = (double)ALot.LocRemainQty;

                        NeedQty -= ProdCapaA;

                        for (int i = 1; i < lstCL.Count; i++)
                        {
                            ChildLabel BLot = lstCL[i];
                            double ProdCapaB = (double)BLot.LocRemainQty;

                            double MoveQty = 0;
                            if (NeedQty >= ProdCapaB)
                            {
                                MoveQty = ProdCapaB;
                            }
                            else
                            {
                                MoveQty = NeedQty;
                            }
                            NeedQty -= ProdCapaB;

                            #region 저장구문

                            List<TSTUFFIN> list_TStuffin = new List<TSTUFFIN>();
                            List<TSTUFFINSUB> list_TStuffinSub = new List<TSTUFFINSUB>();
                            List<TOUTWARE> list_TOutware = new List<TOUTWARE>();
                            List<TOUTWARESUB> list_TOutwareSub = new List<TOUTWARESUB>();
                            List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
                            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();
                            List<WizCommon.Procedure> Prolist2 = new List<WizCommon.Procedure>();
                            List<Dictionary<string, object>> ListParameter2 = new List<Dictionary<string, object>>();

                            #region B Lot 

                            TOUTWARE tOUTWARE = new TOUTWARE();
                            tOUTWARE.OrderID = Frm_tprc_Main.g_tBase.OrderID;
                            tOUTWARE.CompanyID = "0001";

                            tOUTWARE.OutClss = "06";                                    //'이동구분 06 : 잔량이동처리
                            tOUTWARE.CustomID = "0001";                                 //'이동의 경우에는 거래처가 없으므로 해당 시스템이 설치된 업체의 코드를 가져옴(해당시스템 업체의 거래처명, 매출)
                            tOUTWARE.BuyerDirectYN = "N";
                            tOUTWARE.WorkID = "0001";                                   //'가공구분
                            tOUTWARE.ExchRate = "0";

                            tOUTWARE.InsStuffINYN = "Y";                               //'동시입고 Y, 이동이므로 출고와 동시입고 처리함

                            tOUTWARE.OutCustomID = "0001";                            //'이동의 경우에는 거래처가 없으므로 해당 시스템이 설치된 업체의 코드를 가져옴(해당시스템 업체의 거래처명, 매출)
                            tOUTWARE.OutCustom = "대구산업 잔량이동";
                            tOUTWARE.LossRate = "0";
                            tOUTWARE.LossQty = "0";
                            tOUTWARE.OutRoll = "1";// Lib.GetDouble(lblOutRoll.Text).ToString();                   //'건수

                            tOUTWARE.OutQty = MoveQty.ToString();//Lib.GetDouble(lblOutQty.Text).ToString();                     //'출고량-정산에서 사용
                            tOUTWARE.OutRealQty = MoveQty.ToString();//Lib.GetDouble(lblOutQty.Text).ToString();                 //'소요량-수불에서 사용

                            tOUTWARE.OutDate = DateTime.Now.ToString("yyyyMMdd");   // '출고일자
                            tOUTWARE.ResultDate = tOUTWARE.OutDate;

                            tOUTWARE.BoOutClss = "";
                            tOUTWARE.OutTime = DateTime.Now.ToString("HHmm");
                            tOUTWARE.LoadTime = DateTime.Now.ToString("HHmm");

                            tOUTWARE.OutType = "3";                                    //'스켄출고
                            tOUTWARE.snVatAmount = "0";                                   //'이동의 경우 금액 0 -- lblVatINDYN.Caption
                            tOUTWARE.VatINDYN = "0";                                   //'이동의 경우 금액 0 -- lblVatINDYN.Caption
                            tOUTWARE.sUnitClss = ALot.UnitClss == null || ALot.UnitClss.Equals("") ? "0" : ALot.UnitClss;

                            tOUTWARE.sFromLocID = LocID;
                            tOUTWARE.sToLocID = "";
                            tOUTWARE.sArticleID = BLot.ArticleID;            //          '품명코드

                            TOUTWARESUB tOUTWARESUB = new TOUTWARESUB();
                            tOUTWARESUB.sOutwareID = "";
                            tOUTWARESUB.OrderID = "";
                            tOUTWARESUB.OutSubSeq = "1";
                            tOUTWARESUB.OrderSeq = "0";
                            tOUTWARESUB.ArticleID = BLot.ArticleID;
                            tOUTWARESUB.RollSeq = "0";
                            tOUTWARESUB.LotNo = "0";
                            tOUTWARESUB.OutQty = MoveQty.ToString();
                            tOUTWARESUB.OutRoll = "1";
                            tOUTWARESUB.LabelID = BLot.LabelID;
                            tOUTWARESUB.LabelGubun = BLot.LabelID.Length > 0 ? (BLot.LabelID.Substring(0, 1).Equals("I") ? "1" : "7") : "";
                            tOUTWARESUB.Unitprice = "0";

                            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                            sqlParameter.Add("@OrderID", tOUTWARE.OrderID);
                            sqlParameter.Add("@CompanyID", tOUTWARE.CompanyID);
                            sqlParameter.Add("@OutSeq", tOUTWARE.OutSeq);
                            sqlParameter.Add("@OutwareID", tOUTWARE.sOutwareID);         //'출고번호		
                            sqlParameter.Add("@OutClss", tOUTWARE.OutClss);
                            sqlParameter.Add("@CustomID", tOUTWARE.CustomID);
                            sqlParameter.Add("@BuyerDirectYN", tOUTWARE.BuyerDirectYN);
                            sqlParameter.Add("@WorkID", tOUTWARE.WorkID);
                            sqlParameter.Add("@ExchRate", tOUTWARE.ExchRate);
                            sqlParameter.Add("@UnitPriceclss", tOUTWARE.Unitprice);
                            sqlParameter.Add("@InsStuffInYN", tOUTWARE.InsStuffINYN);
                            sqlParameter.Add("@OutcustomID", tOUTWARE.OutCustomID);
                            sqlParameter.Add("@Outcustom", tOUTWARE.OutCustom);
                            sqlParameter.Add("@LossRate", tOUTWARE.LossRate);
                            sqlParameter.Add("@LossQty", tOUTWARE.LossQty);
                            sqlParameter.Add("@OutRoll", tOUTWARE.OutRoll);
                            sqlParameter.Add("@OutQty", tOUTWARE.OutQty);
                            sqlParameter.Add("@OutRealQty", tOUTWARE.OutRealQty);
                            sqlParameter.Add("@OutDate", tOUTWARE.OutDate);
                            sqlParameter.Add("@ResultDate", tOUTWARE.ResultDate);
                            sqlParameter.Add("@BoOutClss", tOUTWARE.BoOutClss);
                            sqlParameter.Add("@Remark", tOUTWARE.Remark);
                            sqlParameter.Add("@OutType", tOUTWARE.OutType);
                            sqlParameter.Add("@Amount", "0");                                    // '금액   		
                            sqlParameter.Add("@VatAmount", tOUTWARE.snVatAmount);            // '부가세 	
                            sqlParameter.Add("@VatINDYN", tOUTWARE.VatINDYN);         // '부가세별도여부 		
                            sqlParameter.Add("@FromLocID", tOUTWARE.sFromLocID);       // '이전창고		
                            sqlParameter.Add("@ToLocID", tOUTWARE.sToLocID);            // '이후창고			
                            sqlParameter.Add("@UnitClss", tOUTWARE.sUnitClss);               //  '단위	
                            sqlParameter.Add("@ArticleID", tOUTWARE.sArticleID);
                            sqlParameter.Add("@UserID", Frm_tprc_Main.g_tBase.PersonID);
                            //output 2개 ow.OutSeq  //ow.sOutwareID
                            WizCommon.Procedure pro1 = new WizCommon.Procedure();
                            pro1.list_OutputName = new List<string>();
                            pro1.list_OutputLength = new List<string>();

                            //pro1.Name = "xp_Outware_iOutware";
                            pro1.Name = "xp_WizWork_iOutware";
                            pro1.OutputUseYN = "Y";
                            pro1.list_OutputName.Add("@OutSeq");
                            pro1.list_OutputName.Add("@OutwareID");
                            pro1.list_OutputLength.Add("10");
                            pro1.list_OutputLength.Add("12");

                            Prolist.Add(pro1);
                            ListParameter.Add(sqlParameter);

                            Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                            sqlParameter2.Add("@OutwareID", tOUTWARESUB.sOutwareID);
                            sqlParameter2.Add("@OrderID", tOUTWARESUB.OrderID);
                            sqlParameter2.Add("@OutSeq", tOUTWARE.OutSeq);
                            sqlParameter2.Add("@OutSubSeq", tOUTWARESUB.OutSubSeq);
                            sqlParameter2.Add("@OrderSeq", tOUTWARESUB.OrderSeq);
                            sqlParameter2.Add("@LineSeq", "0");
                            sqlParameter2.Add("@LineSubSeq", "0");
                            sqlParameter2.Add("@RollSeq", tOUTWARESUB.RollSeq);
                            sqlParameter2.Add("@LabelID", tOUTWARESUB.LabelID);
                            sqlParameter2.Add("@LabelGubun", tOUTWARESUB.LabelGubun);
                            sqlParameter2.Add("@LotNo", tOUTWARESUB.LotNo);
                            sqlParameter2.Add("@Gubun", "2");
                            sqlParameter2.Add("@StuffQty", "0");
                            sqlParameter2.Add("@OutQty", tOUTWARESUB.OutQty);
                            sqlParameter2.Add("@OutRoll", tOUTWARESUB.OutRoll);
                            sqlParameter2.Add("@UnitPrice", tOUTWARESUB.Unitprice);  //'단가
                            sqlParameter2.Add("@UserID", Frm_tprc_Main.g_tBase.PersonID);
                            sqlParameter2.Add("@CustomBoxID", tOUTWARESUB.CustomBoxID); //'고객 BoxID
                            sqlParameter2.Add("@ArticleID", tOUTWARESUB.ArticleID); //'고객 BoxID

                            WizCommon.Procedure pro2 = new WizCommon.Procedure();
                            pro2.list_OutputName = new List<string>();
                            pro2.list_OutputLength = new List<string>();

                            pro2.Name = "xp_WizWork_iOutwareSub";
                            //pro2.Name = "xp_Outware_iOutwareSub";
                            pro2.OutputUseYN = "N";
                            pro2.list_OutputName.Add("sRtnMsg");
                            pro2.list_OutputLength.Add("30");

                            Prolist.Add(pro2);
                            ListParameter.Add(sqlParameter2);

                            #endregion

                            #region A Lot

                            ProdCapaA += MoveQty;
                            TSTUFFIN tSTUFFIN = new TSTUFFIN();
                            tSTUFFIN.StuffinID = "";
                            tSTUFFIN.StuffDate = DateTime.Now.ToString("yyyyMMdd");     // '입고일자
                            tSTUFFIN.StuffClss = "06";                                  //'이동구분 06 : 잔량이동처리
                            tSTUFFIN.CustomID = "0001";                            //'이동의 경우에는 거래처가 없으므로 해당 시스템이 설치된 업체의 코드를 가져옴(해당시스템 업체의 거래처명, 매출)
                                                                                   //tSTUFFIN.Custom = mc.KCustom
                            tSTUFFIN.UnitClss = ALot.UnitClss == null || ALot.UnitClss.Equals("") ? "0" : ALot.UnitClss; ;
                            tSTUFFIN.TotRoll = "1";
                            tSTUFFIN.TotQty = MoveQty.ToString();
                            tSTUFFIN.TotQtyY = MoveQty.ToString();
                            tSTUFFIN.UnitPrice = "0";
                            tSTUFFIN.PriceClss = "";
                            tSTUFFIN.ExchRate = "0";
                            tSTUFFIN.VatIndYN = "0";
                            tSTUFFIN.Remark = "잔량이동처리시점 LOTID " + ALot.LabelID + "의 현재 총 잔량은 |" + ProdCapaA.ToString() + "| 이다.";
                            tSTUFFIN.OrderId = "";
                            tSTUFFIN.ArticleID = ALot.ArticleID;             //ArticleID
                            tSTUFFIN.WorkID = "0001";                                   //'가공구분
                            tSTUFFIN.OrderNO = "";
                            tSTUFFIN.InsStuffINYN = "Y";
                            tSTUFFIN.OutwareID = "";
                            tSTUFFIN.CompanyID = "0001";
                            tSTUFFIN.FromLocID = "";
                            tSTUFFIN.TOLocID = LocID;
                            tSTUFFIN.CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                            sqlParameter = new Dictionary<string, object>();
                            sqlParameter.Add("@StuffinID", "");
                            sqlParameter.Add("@StuffDate", tSTUFFIN.StuffDate);
                            sqlParameter.Add("@StuffClss", tSTUFFIN.StuffClss);
                            sqlParameter.Add("@CustomID", tSTUFFIN.CustomID);
                            sqlParameter.Add("@UnitClss", tSTUFFIN.UnitClss);
                            sqlParameter.Add("@TotRoll", tSTUFFIN.TotRoll);
                            sqlParameter.Add("@TotQty", tSTUFFIN.TotQty);
                            sqlParameter.Add("@TotQtyY", tSTUFFIN.TotQtyY);
                            sqlParameter.Add("@UnitPrice", tSTUFFIN.UnitPrice);
                            sqlParameter.Add("@PriceClss", tSTUFFIN.PriceClss);
                            sqlParameter.Add("@ExchRate", tSTUFFIN.ExchRate);
                            sqlParameter.Add("@VatIndYN", tSTUFFIN.VatIndYN);
                            sqlParameter.Add("@Remark", tSTUFFIN.Remark);
                            sqlParameter.Add("@OrderId", tSTUFFIN.OrderId);
                            sqlParameter.Add("@ArticleID", tSTUFFIN.ArticleID);
                            sqlParameter.Add("@WorkID", tSTUFFIN.WorkID);
                            sqlParameter.Add("@OrderNO", tSTUFFIN.OrderNO);
                            sqlParameter.Add("@InsStuffINYN", tSTUFFIN.InsStuffINYN);
                            sqlParameter.Add("@OutwareID", tSTUFFIN.OutwareID);
                            sqlParameter.Add("@CompanyID", tSTUFFIN.CompanyID);
                            sqlParameter.Add("@BrandClss", tSTUFFIN.BrandClss);
                            sqlParameter.Add("@OrderForm", tSTUFFIN.OrderForm);
                            sqlParameter.Add("@FromLocID", tSTUFFIN.FromLocID);
                            sqlParameter.Add("@TOLocID", tSTUFFIN.TOLocID);
                            sqlParameter.Add("@CreateUserID", tSTUFFIN.CreateUserID);

                            pro1 = new WizCommon.Procedure();
                            pro1.list_OutputName = new List<string>();
                            pro1.list_OutputLength = new List<string>();

                            pro1.Name = "xp_WizWork_iStuffIN";
                            pro1.OutputUseYN = "Y";
                            pro1.list_OutputName.Add("@StuffinID");
                            pro1.list_OutputLength.Add("20");

                            Prolist.Add(pro1);
                            ListParameter.Add(sqlParameter);

                            TSTUFFINSUB tSTUFFINSUB = new TSTUFFINSUB();
                            tSTUFFINSUB.StuffInID = "";
                            tSTUFFINSUB.StuffInSubSeq = "1";
                            tSTUFFINSUB.RollNo = "1";
                            tSTUFFINSUB.StuffClss = "06";                                  //'이동구분 06 : 잔량이동처리
                            tSTUFFINSUB.Qty = MoveQty.ToString();
                            tSTUFFINSUB.LotID = ALot.LabelID;
                            tSTUFFINSUB.SetDate = "";
                            tSTUFFINSUB.InspectApprovalYN = "Y";
                            tSTUFFINSUB.CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                            sqlParameter2 = new Dictionary<string, object>();
                            sqlParameter2.Add("@StuffInID", tSTUFFINSUB.StuffInID);
                            sqlParameter2.Add("@StuffInSubSeq", tSTUFFINSUB.StuffInSubSeq);
                            sqlParameter2.Add("@RollNo", tSTUFFINSUB.RollNo);
                            sqlParameter2.Add("@StuffClss", tSTUFFINSUB.StuffClss);
                            sqlParameter2.Add("@Qty", tSTUFFINSUB.Qty);
                            sqlParameter2.Add("@LotID", tSTUFFINSUB.LotID);
                            sqlParameter2.Add("@SetDate", tSTUFFINSUB.SetDate);
                            sqlParameter2.Add("@InspectApprovalYN", tSTUFFINSUB.InspectApprovalYN);
                            sqlParameter2.Add("@CreateUserID", tSTUFFINSUB.CreateUserID);

                            pro2 = new WizCommon.Procedure();
                            pro2.list_OutputName = new List<string>();
                            pro2.list_OutputLength = new List<string>();

                            pro2.Name = "xp_WizWork_iStuffINSub";
                            pro2.OutputUseYN = "N";

                            Prolist.Add(pro2);
                            ListParameter.Add(sqlParameter2);

                            list_TStuffin.Add(tSTUFFIN);
                            list_TStuffinSub.Add(tSTUFFINSUB);
                            tSTUFFIN = null;
                            tSTUFFINSUB = null;

                            #endregion

                            List<KeyValue> list_Result = new List<KeyValue>();
                            list_Result = DataStore.Instance.ExecuteProcedureNoCommitGetOutputList(Prolist, ListParameter);

                            if (list_Result[0].key.ToLower() == "success")
                            {
                                List<string> OUTWAREID = new List<string>();
                                string STUFFINID = "";
                                foreach (KeyValue kv in list_Result)
                                {
                                    if (kv.key.ToUpper() == "@OUTWAREID")
                                    {
                                        OUTWAREID.Add(kv.value.Trim().ToUpper());
                                    }
                                    else if (kv.key.ToUpper() == "@STUFFINID")
                                    {
                                        STUFFINID = kv.value.ToUpper().Trim();
                                    }
                                }
                                if (STUFFINID != "")
                                {
                                    int k = 0;
                                    foreach (string owid in OUTWAREID)
                                    {
                                        Dictionary<string, object> sqlParameter0 = new Dictionary<string, object>();
                                        List<string> Pro = new List<string>();
                                        k++;
                                        sqlParameter0.Add("MoveID", "");
                                        sqlParameter0.Add("STUFFINID", STUFFINID);
                                        sqlParameter0.Add("OUTWAREID", owid);
                                        sqlParameter0.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);
                                        if (k == 1)
                                        {
                                            pro1 = new WizCommon.Procedure();
                                            pro1.list_OutputName = new List<string>();
                                            pro1.list_OutputLength = new List<string>();

                                            pro1.Name = "xp_WizWork_iRemainMove";
                                            pro1.OutputUseYN = "Y";
                                            pro1.list_OutputName.Add("MoveID");
                                            pro1.list_OutputLength.Add("16");

                                            Prolist2.Add(pro1);
                                            ListParameter2.Add(sqlParameter0);
                                        }
                                        else
                                        {
                                            pro2 = new WizCommon.Procedure();
                                            pro2.list_OutputName = new List<string>();
                                            pro2.list_OutputLength = new List<string>();

                                            pro2.Name = "xp_WizWork_iRemainMoveNoOut";
                                            pro2.OutputUseYN = "N";

                                            Prolist2.Add(pro2);
                                            ListParameter2.Add(sqlParameter0);
                                        }
                                    }
                                    bool list_Remain = DataStore.Instance.ExecuteProcedureAllNoBeginOKCommit(Prolist2, ListParameter2);
                                    if (list_Remain)
                                    {
                                        //WizCommon.Popup.MyMessageBox.ShowBox("잔량이동처리가 완료되었습니다.", "[잔량이동처리완료]", 3, 1);
                                        successCount++;
                                        lstSuccessMsg.Add(successCount + ". " + BLot.LabelID + " → " + ALot.LabelID + " ( 수량 : " + MoveQty.ToString() + " )");
                                    }
                                }
                            }
                            else
                            {
                                if (successCount > 0)
                                {
                                    //WizCommon.Popup.MyMessageBox.ShowBox(successCount + " 건의 잔량이동처리가 완료되었습니다.", "[성공]", 0, 1, 1);
                                    Com_MessageBox.ShowBox(successCount + " 건의 잔량이동처리가 완료되었습니다.", "[성공]", 0, 0, lstSuccessMsg);
                                }

                                WizCommon.Popup.MyMessageBox.ShowBox("[잔량이동처리실패 : " + BLot.LabelID + " → " + ALot.LabelID + " ( 수량 : " + MoveQty.ToString() + " )" + "]\r\n" + list_Result[0].value.ToString(), "[오류]", 0, 1, 1);
                                return false;
                            }

                            #endregion
                        }
                    }
                }

                if (successCount > 0)
                {
                    //WizCommon.Popup.MyMessageBox.ShowBox(successCount + " 건의 잔량이동처리가 완료되었습니다.", "[성공]", 0, 1, 1);
                    Com_MessageBox.ShowBox(successCount + " 건의 잔량이동처리가 완료되었습니다.", "[성공]", 0, 0, lstSuccessMsg);
                }

                return true;
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
                return false;
            }
        }

        #endregion

        // 취소 버튼 선택시.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region

        // 유효성 검사
        private bool CheckData()
        {
            bool flag = true;

            string ArticleS = "";

            for (int i = 0; i < dgdMain.Rows.Count; i++)
            {
                double DeficiencyQty = ConvertDouble(dgdMain.Rows[i].Cells["DeficiencyQty"].Value.ToString());

                if (DeficiencyQty > 0)
                {
                    ArticleS += "\r\n(" + dgdMain.Rows[i].Cells["ArticleID"].Value.ToString().Trim() + ")" + dgdMain.Rows[i].Cells["Article"].Value.ToString().Trim();

                    flag = false;
                }
            }

            if (flag == false)
            {
                Message[0] = "[시작 오류]";
                Message[1] = string.Format("아래의 하위품 재고가 부족합니다." + ArticleS + "\r\n부족재고량을 확인해주세요.");
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }

            return flag;
        }

        #endregion


        #region DB 임시인서트 펑션

        // DB 임시데이터 인서트 작업.
        private bool StartHandleWorking_Click()
        {
            List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

            try
            {
                Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                double WorkQty = 0;

                sqlParameter1.Add("JobID", 0);
                sqlParameter1.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                sqlParameter1.Add("InstDetSeq", Frm_tprc_Main.g_tBase.sInstDetSeq);
                sqlParameter1.Add("LabelID", Frm_tprc_Main.g_tBase.sLotID);
                sqlParameter1.Add("StartSaveLabelID", txtBarCodePreScan.Text.Trim());

                sqlParameter1.Add("LabelGubun", m_LabelGubun);
                sqlParameter1.Add("ProcessID", m_ProcessID);
                sqlParameter1.Add("MachineID", m_MachineID);

                // 새벽 작업자 scan date 일자 -1 작업(새벽반의 물량도 전날물량으로 쳐야 하니까.)
                // 2019.09.05 허윤구.
                string Midnight_Worker = DateTime.Now.ToString("HHmmss");
                if ((Convert.ToInt32(Midnight_Worker) >= 000000) && (Convert.ToInt32(Midnight_Worker) <= 073000))
                {
                    sqlParameter1.Add("ScanDate", DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
                }
                else
                {
                    sqlParameter1.Add("ScanDate", DateTime.Now.ToString("yyyyMMdd"));
                }
                sqlParameter1.Add("ScanTime", DateTime.Now.ToString("HHmmss"));

                sqlParameter1.Add("ArticleID", Frm_tprc_Main.g_tBase.sArticleID);
                sqlParameter1.Add("WorkQty", WorkQty);
                sqlParameter1.Add("Comments", m_ProcessID + " " + "공정 시작처리에 의한 기록작업");
                sqlParameter1.Add("ReworkOldYN", "");
                sqlParameter1.Add("ReworkLinkProdID", "");

                sqlParameter1.Add("WorkStartDate", DateTime.Now.ToString("yyyyMMdd"));
                sqlParameter1.Add("WorkStartTime", DateTime.Now.ToString("HHmmss"));
                sqlParameter1.Add("WorkEndDate", "");
                sqlParameter1.Add("WorkEndTime", "");
                sqlParameter1.Add("JobGbn", "1");

                sqlParameter1.Add("NoReworkCode", "");
                sqlParameter1.Add("WDNO", "");
                sqlParameter1.Add("WDID", "");
                sqlParameter1.Add("WDQty", 0);
                sqlParameter1.Add("LogID", 0);

                sqlParameter1.Add("s4MID", "");
                sqlParameter1.Add("DayOrNightID", Frm_tprc_Main.g_tBase.DayOrNightID);
                sqlParameter1.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);
                

                WizCommon.Procedure pro1 = new WizCommon.Procedure();
                pro1.Name = "xp_wkResult_iWkResult";
                pro1.OutputUseYN = "Y";
                pro1.OutputName = "JobID";
                pro1.OutputLength = "20";

                Prolist.Add(pro1);
                ListParameter.Add(sqlParameter1);


                #region [안씀] 하위품이 하나일 때
                //if (InsertX > 0)
                //{
                //    for (int k = 0; k < InsertX; k++)
                //    {
                //        Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                //        sqlParameter2.Add("JobID", "");
                //        sqlParameter2.Add("ChildLabelID", "");//
                //        sqlParameter2.Add("ChildLabelGubun", "");
                //        sqlParameter2.Add("ChildArticleID", "");
                //        sqlParameter2.Add("ReworkOldYN", "");
                //        sqlParameter2.Add("ReworkLinkChildProdID", "");
                //        sqlParameter2.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);
                //        sqlParameter2.Add("ProdQtyOver100YN", "");

                //        WizCommon.Procedure pro2 = new WizCommon.Procedure();
                //        pro2.Name = "xp_wkResult_iWkResultArticleChild";
                //        pro2.OutputUseYN = "N";
                //        pro2.OutputName = "JobID";
                //        pro2.OutputLength = "20";

                //        Prolist.Add(pro2);
                //        ListParameter.Add(sqlParameter2);
                //    }
                //}
                #endregion
                #region 하위품이 여러개일때 → 하위품 그리드가 있을 경우
                for (int i = 0; i < dgdMain.Rows.Count; i++)
                {

                    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                    sqlParameter2.Add("JobID", "");
                    sqlParameter2.Add("ChildLabelID", dgdMain.Rows[i].Cells["Label"].Value.ToString());//
                    sqlParameter2.Add("ChildLabelGubun", ""); // 더미 데이터니까, 일단 이건 뺌
                    sqlParameter2.Add("ChildArticleID", dgdMain.Rows[i].Cells["ArticleID"].Value.ToString());
                    sqlParameter2.Add("ReworkOldYN", "");
                    sqlParameter2.Add("ReworkLinkChildProdID", "");
                    sqlParameter2.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

                    WizCommon.Procedure pro2 = new WizCommon.Procedure();
                    pro2.Name = "xp_wkResult_iWkResultArticleChild";
                    pro2.OutputUseYN = "N";
                    pro2.OutputName = "JobID";
                    pro2.OutputLength = "20";

                    Prolist.Add(pro2);
                    ListParameter.Add(sqlParameter2);
                }
                #endregion

                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    list_Result.RemoveAt(0);
                    m_ArticleID = "";
                    m_LabelGubun = "";
                    return true;
                }
                else
                {
                    foreach (KeyValue kv in list_Result)
                    {
                        if (kv.key.ToLower() == "failure")
                        {
                            throw new Exception(kv.value.ToString());
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                return false;
            }
                    
        }



        #endregion

        #endregion

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }


        #region 기타 메서드 모음

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN0(object obj)
        {
            return string.Format("{0:N0}", obj);
        }

        // 천마리 콤마, 소수점 두자리
        private string stringFormatN2(object obj)
        {
            return string.Format("{0:N2}", obj);
        }

        // 데이터피커 포맷으로 변경
        private string DatePickerFormat(string str)
        {
            string result = "";

            if (str.Length == 8)
            {
                if (!str.Trim().Equals(""))
                {
                    result = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
                }
            }

            return result;
        }

        // Int로 변환
        private int ConvertInt(string str)
        {
            int result = 0;
            int chkInt = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Replace(",", "");

                if (Int32.TryParse(str, out chkInt) == true)
                {
                    result = Int32.Parse(str);
                }
            }

            return result;
        }

        // 소수로 변환 가능한지 체크 이벤트
        private bool CheckConvertDouble(string str)
        {
            bool flag = false;
            double chkDouble = 0;

            if (!str.Trim().Equals(""))
            {
                if (Double.TryParse(str, out chkDouble) == true)
                {
                    flag = true;
                }
            }

            return flag;
        }

        // 숫자로 변환 가능한지 체크 이벤트
        private bool CheckConvertInt(string str)
        {
            bool flag = false;
            int chkInt = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Trim().Replace(",", "");

                if (Int32.TryParse(str, out chkInt) == true)
                {
                    flag = true;
                }
            }

            return flag;
        }

        // 소수로 변환
        private double ConvertDouble(string str)
        {
            double result = 0;
            double chkDouble = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Replace(",", "");

                if (Double.TryParse(str, out chkDouble) == true)
                {
                    result = Double.Parse(str);
                }
            }

            return result;
        }




        #endregion

        // 탭 체인지
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0) // 하위품목
            {

            }
            else if (tabControl.SelectedIndex == 1) // 라벨 목록
            {
                string ArticleID = dgdMain.SelectedRows[0].Cells["ArticleID"].Value.ToString().Trim();

                // 메인그리드의 품명을 기준으로 라벨 리스트 조회
                dgdSub.Rows.Clear();

                // dicSubList
                foreach(string Key in dicSubList.Keys)
                {
                    if (Key.Trim().Equals(ArticleID))
                    {
                        List<ChildLabel> lstCL = dicSubList[Key];
                        for (int i = 0; i < lstCL.Count; i++)
                        {
                            int index = i + 1;
                            dgdSub.Rows.Add(index.ToString()
                                                        , lstCL[i].LabelID
                                                        , lstCL[i].ArticleID
                                                        , lstCL[i].BuyerArticleNo
                                                        , lstCL[i].Article
                                                        , stringFormatN0(lstCL[i].ReaQty)
                                                        , stringFormatN0(lstCL[i].LocRemainQty)
                                                        , (i == 0 ? "-" : "X")
                                                        , (i == 0 ? "선스캔 라벨" : "")
                                                        );
                        }
                        break;
                    }
                }
            }
        }
    }
}
