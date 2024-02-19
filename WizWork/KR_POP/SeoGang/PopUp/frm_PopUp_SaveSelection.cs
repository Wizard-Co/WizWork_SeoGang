using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

//*******************************************************************************
//프로그램명    frm_PopUp_SaveSelection.cs
//메뉴ID        
//설명          frm_PopUp_SaveSelection 메인소스입니다.
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
    public partial class frm_PopUp_SaveSelection : Form
    {

        private string m_ProcessID = "";        //공정id

        private string m_MtrExceptYN = "";      // 예외처리 체크용도

        //2020.01.07 허윤구 추가.
        private string Wh_Ar_InstID = "";       // PL_Input 작지의 해당 대상 InstID.
        private string Wh_Ar_InstID_Seq = "";   // PL_Input 작지의 해당 대상 InstID_Seq.


        string[] Message = new string[2];  // 메시지박스 처리용도.

        string WhAr_MoveArticle_I = string.Empty;    // I_FMB_재단 자동이동시의 Article.(Whole-Area)

        WizWorkLib Lib = new WizWorkLib();

        public frm_PopUp_SaveSelection_CodeView returnLabelInfo = new frm_PopUp_SaveSelection_CodeView();

        public frm_PopUp_SaveSelection()
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
        }

        public frm_PopUp_SaveSelection(string Article, string BuyerArticleNo, string QtyPerBox)
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정

            this.txtArticle.Text = Article;
            this.txtBuyerArticleNo.Text = BuyerArticleNo;
            this.txtQtyPerBox.Text = QtyPerBox;

            this.Size = new Size(819, 560);
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
        private void frm_PopUp_SaveSelection_Load(object sender, EventArgs e)
        {
            // 전체 클리어.
            SetFormDataClear();

            // 시작처리 버튼숨기기.
            //tlpBottomChoice.ColumnStyles[1].Width = 0;
            //tlpBottomChoice.ColumnStyles[2].Width = 0;

            // 시작작업 처리
            //Form_Activate();

            int DetSeq = ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq);
            
        }

        // After Load
        private void frm_PopUp_SaveSelection_Shown(object sender, EventArgs e)
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


        #region 스캔

        // 스캔 텍스트 체크.
        private void txtBarCodePreScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    txtBarCodePreScan.Text = txtBarCodePreScan.Text.Trim().ToUpper();

                    FillGrid(txtBarCodePreScan.Text);
                    txtBarCodePreScan.Text = "";
                    txtBarCodePreScan.Focus();
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(ex.Message.ToString(), "[오류]", 0, 1);
            }            
        }

        private void FillGrid(string LabelID)
        {
            try
            {
                if (dgdMain.Rows.Count > 0)
                {
                    dgdMain.Rows.Clear();
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("LotID", LabelID);
                sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                sqlParameter.Add("InstDetSeq", ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq));
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sLabelInfo_ForSaveSelection", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    // 품명이 안맞다던지 등등 에러 
                    if (dt.Columns.Count == 1)
                    {
                        Message[0] = "스캔 오류";
                        Message[1] = dr["Msg"].ToString().Replace("|", "\r\n");
                        throw new Exception();
                    }

                    int index = 0;
                    // 둘리 : 대구산업 2020.05.02
                    // 이곳에서 하위품 데이터그리드 넣기
                    dgdMain.Rows.Add(++index
                                       , dr["ArticleID"].ToString().Trim() // 품명ID
                                       , dr["BuyerArticleNo"].ToString().Trim() // 품번
                                       , dr["Article"].ToString().Trim()  // 품명
                                       , stringFormatN0(dr["NowLoc"]) // 현재고량
                                       , dr["UnitClssName"].ToString().Trim() // 단위
                                       , dr["LabelID"].ToString().Trim() // 라벨
                                       , DatePickerFormat(dr["WorkEndDate"].ToString().Trim()) // 작업일
                                      );
                }
                else
                {
                    //2020.04.03 허윤구
                    // DT가 NULL이 될 수 있는 케이스.
                    // 지속적으로 찾아서 개별 케이스별로 메시지 UPDATE 해야 합니다.

                    //1. 하위품이 사라진경우. > 즉, 알수없는 (여러) 이유로 삭제된 케이스.                    
                    string[] DeleteBarcodeYN = new string[2];
                    string sql_2 = "select cnt = COUNT(*) from wk_result where labelid = '" + LabelID + "'";
                    DeleteBarcodeYN = DataStore.Instance.ExecuteQuery(sql_2, false);
                    if (DeleteBarcodeYN[1] == "0")
                    {
                        Message[0] = "[하위품 소실]";
                        Message[1] = "해당 하위품( " + LabelID + " )은 승인되지 않은 품목이거나 삭제처리된 Lot입니다.";
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
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
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
                //if (BarcodeEnter())
                //{
                //    if (btnOK.Tag != null
                //        && btnOK.Tag.ToString().Equals("OK"))
                //    {
                //        btnOK_Click(null, null);
                //    }
                //}
                //txtBarCodePreScan.Focus();

                FillGrid(txtBarCodePreScan.Text);
                txtBarCodePreScan.Text = "";
                txtBarCodePreScan.Focus();
            }
            else
            {
                txtBarCodePreScan.Text = string.Empty;
            }
        }

        #endregion

        #region 시작, 취소 버튼선택.

        // 시작처리 버튼 선택시.
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgdMain.Rows.Count <= 0)
                {
                    Message[0] = "[오류]";
                    Message[1] = string.Format("선택 저장할 라벨을 스캔해주세요.");
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return;
                }

                this.returnLabelInfo = new frm_PopUp_SaveSelection_CodeView()
                {
                    LabelID = dgdMain.Rows[0].Cells["Label"].Value.ToString()
                };
                DialogResult = DialogResult.OK;
                btnCancel_Click(null, null);
            }
            catch(Exception ex)
            {
                Message[0] = "[저장 오류]";
                Message[1] = string.Format("관리자에게 문의해주세요. btnOK_Click 부분\r\n[에러 내용 : " + ex.Message + "]");
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
        }

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

            for( int i = 0; i < dgdMain.Rows.Count; i++)
            {
                string IsIN = dgdMain.Rows[i].Cells["IsIN"].Value.ToString().ToUpper().Trim();

                if (IsIN.Equals("X"))
                {
                    ArticleS += "\r\n(" + dgdMain.Rows[i].Cells["ArticleID"].Value.ToString().Trim() + ")" +  dgdMain.Rows[i].Cells["Article"].Value.ToString().Trim();

                    flag = false;
                }
            }

            if (flag == false)
            {
                Message[0] = "[시작 오류]";
                Message[1] = string.Format("아래의 하위품이 투입되지 않았습니다." + ArticleS);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }

            return flag;
        }

        #endregion

        #endregion


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

        // 천마리 콤마, 소수점 두자리
        private string stringFormatN3(object obj)
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
            if (str == null) { return 0; }

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

        // 둘리 2020.05.27
        // 라벨 없이 시작하기 테스트
        private void btnGo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            btnCancel_Click(null, null);
        }


        // 잔량은 새 라벨을 발행 하게 하려면?
        private void btnRemainToNewLabel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgdMain.Rows.Count <= 0)
                {
                    Message[0] = "[오류]";
                    Message[1] = string.Format("선택 저장할 라벨을 스캔해주세요.");
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return;
                }

                // 박스당 수량을 입력하지 않았을 때는, 진행 못하도록
                if (ConvertDouble(txtQtyPerBox.Text) == 0)
                {
                    Message[0] = "[오류]";
                    Message[1] = string.Format("박스당 수량을 입력해주세요.");
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return;
                }

                this.returnLabelInfo = new frm_PopUp_SaveSelection_CodeView()
                {
                    LabelID = dgdMain.Rows[0].Cells["Label"].Value.ToString(),
                    NowLoc = dgdMain.Rows[0].Cells["NowLoc"].Value.ToString(),
                    ProdQtyPerBox = txtQtyPerBox.Text,
                    SaveQtyPerBoxYN = true
                };
                DialogResult = DialogResult.OK;
                btnCancel_Click(null, null);
            }
            catch (Exception ex)
            {
                Message[0] = "[저장 오류]";
                Message[1] = string.Format("관리자에게 문의해주세요. btnOK_Click 부분\r\n[에러 내용 : " + ex.Message + "]");
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
        }

        #region 박스당 수량 체크박스, 텍스트박스 클릭 이벤트

        private void chkQtyPerBox_Click(object sender, EventArgs e)
        {
            double DOU_WorkQty = 0;

            txtQtyPerBox.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtQtyPerBox.Text = keypad.tbInputText.Text;
                if (txtQtyPerBox.Text == "" || Convert.ToInt32(txtQtyPerBox.Text) == 0)
                {
                    txtQtyPerBox.Text = "0";
                }
                Double.TryParse(txtQtyPerBox.Text, out DOU_WorkQty);
                txtQtyPerBox.Text = string.Format("{0:n0}", (int)DOU_WorkQty);
            }
        }

        private void txtQtyPerBox_Click(object sender, EventArgs e)
        {
            double DOU_WorkQty = 0;

            txtQtyPerBox.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtQtyPerBox.Text = keypad.tbInputText.Text;
                if (txtQtyPerBox.Text == "" || Convert.ToInt32(txtQtyPerBox.Text) == 0)
                {
                    txtQtyPerBox.Text = "0";
                }
                Double.TryParse(txtQtyPerBox.Text, out DOU_WorkQty);
                txtQtyPerBox.Text = string.Format("{0:n0}", (int)DOU_WorkQty);
            }
        }

        // (실제는 체크, 모양은 버튼) 체크형 버튼들 > 체크하더라도 본 색깔 유형 그대로 유지하도록.
        private void checkBox_CheckedPrevent(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Checked = false;
            }
            else
            {
                ((CheckBox)sender).Checked = false;
            }
        }

        #endregion
    }

    public class frm_PopUp_SaveSelection_CodeView
    {
        public string LabelID { get; set; }
        public string NowLoc { get; set; }
        public string ProdQtyPerBox { get; set; }
        public bool SaveQtyPerBoxYN { get; set; }
    }
}
