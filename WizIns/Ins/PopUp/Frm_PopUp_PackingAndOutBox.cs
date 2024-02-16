using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using WizWork;

//*******************************************************************************
//프로그램명    Frm_PopUp_PackingAndOutBox.cs
//메뉴ID        
//설명          Frm_PopUp_PackingAndOutBox 메인소스입니다.
//작성일        2019.07.29
//개발자        허윤구
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//  19_0729     허윤구  * 성형 하나에 재단 2개가 필요한 케이스에 따라 수정보완.
//                          (InsertX)
//  2019.08.01 > 허윤구    FMB가 어떤 이유로 이미 재단창고에 가 있을 케이스에 대한 로직추가.
//*******************************************************************************

namespace WizIns
{
    public partial class Frm_PopUp_PackingAndOutBox : Form
    {
        string[] Message = new string[2];  // 메시지박스 처리용도.

        WizWorkLib Lib = new WizWorkLib();

        // 메인 데이터 그리드 뷰 관리 객체
        List<Frm_PopUp_PackingAndOutBox_CodeView> lstMain = new List<Frm_PopUp_PackingAndOutBox_CodeView>();
        // 넘어온 총 생산량
        int TotalQty = 0;

        // 불량 리스트
        Dictionary<string, frm_tprc_Work_Defect_U_CodeView> dicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();

        public Frm_PopUp_PackingAndOutBox()
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
        }

        public Frm_PopUp_PackingAndOutBox(List<Frm_tins_Order_Q_CodeView> lstIns)
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정

            setInitial(lstIns);
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
        private void Frm_PopUp_PackingAndOutBox_Load(object sender, EventArgs e)
        {
            this.Size = new Size(960, 335);

            mtb_Date.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dtInspectTime.Value = DateTime.Now;
        }

        #region 첫 세팅 : setInitial()
        private void setInitial(List<Frm_tins_Order_Q_CodeView> lstIns)
        {
            lstMain.Clear();
            TotalQty = 0;
            for (int i = 0; i < lstIns.Count; i++)
            {
                lstMain.Add(new Frm_PopUp_PackingAndOutBox_CodeView(lstIns[i]));
                lstMain[i].Num = lstMain.Count;
                TotalQty += Lib.ConvertInt(lstIns[i].NoInspectQty);
            }

            BindingSource bs = new BindingSource();
            bs.DataSource = lstMain;
            dgdMain.DataSource = bs;

            txtInspectQty.Text = Lib.stringFormatN0(TotalQty);

            CalQty();
        }
        #endregion

        #region 검사일자, 검사시간 클릭 이벤트

        // 검사 일자
        private void mtb_Date_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_Date.Text.Replace("-", ""), mtb_Date.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }

        //  Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
        private void GetDate(string strDate, string btnName)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
            mtb_Date.Text = dateTime.ToString("yyyy-MM-dd");
        }

        // 검사 시간
        private void btnInspectTime_Click(object sender, EventArgs e)
        {
            TimeCheck(dtInspectTime, "검사시간");
        }

        private void TimeCheck(DateTimePicker dtpSender, string Title)
        {
            try
            {
                WizWork.POPUP.Frm_CMNumericKeypad FK = new WizWork.POPUP.Frm_CMNumericKeypad(Title);
                FK.Owner = this;
                string sTime = "";
                DateTime dt = DateTime.Now;
                if (FK.ShowDialog() == DialogResult.OK)
                {
                    sTime = FK.InputTextValue;
                    if (sTime != "")
                    {
                        sTime = sTime.PadRight(6, '0').Substring(0, 6);

                        // 24시간 넘어가면, 0시로 변환
                        if (Lib.ConvertInt(sTime) >= 240000) { sTime = "000000"; }
                        
                        // 시간으로 변환
                        if (DateTime.TryParseExact(sTime.Substring(0, 6), "HHmmss", null, System.Globalization.DateTimeStyles.None, out dt) == false)
                        {
                            dt = DateTime.Now;
                        }
                    }
                    dtpSender.Value = dt;
                }
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(ex.Message, "[검사시간 등록 실패]", 0, 1);
                return;
            }
        }

        #endregion

        #region 확인, 취소 버튼 클릭 이벤트
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 저장 구문 SaveData(), CheckData() - 각각의 라벨을 그대로 검사, 포장 테이블에 넣기.

        private bool SaveData()
        {
            bool flag = false;

            List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
            List<List<string>> ListProcedureName = new List<List<string>>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            try
            {
                if (CheckData() == false) { return false; }

                for(int i = 0; i < dgdMain.Rows.Count; i++)
                {
                    // 라벨
                    //string LabelID = dgdMain.Rows[i].Cells["LabelID"].Value.ToString();

                    // 합격수량이 0개이면, 포장이 안되야됨

                    if (Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString()) > 0)
                    {
                        // wk_packing
                        sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("PackID", "");
                        sqlParameter.Add("nseq", 1);
                        sqlParameter.Add("ArticleID", dgdMain.Rows[0].Cells["ArticleID"].Value.ToString().Trim());
                        sqlParameter.Add("sPackDate", mtb_Date.Text.Replace("-", ""));
                        sqlParameter.Add("nPackqty", Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString()));
                        sqlParameter.Add("sPackPersonID", Frm_tins_Main.g_tBase.PersonID);
                        sqlParameter.Add("sInBoxID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString());

                        //sqlParameter.Add("sPackPersonID", Frm_tins_Main.g_tBase.PersonID);

                        WizCommon.Procedure pro1 = new WizCommon.Procedure();
                        pro1.Name = "[xp_prdIns_iWkPacking]";
                        pro1.OutputUseYN = "Y";
                        pro1.OutputName = "PackID";
                        pro1.OutputLength = "10";

                        Prolist.Add(pro1);
                        ListParameter.Add(sqlParameter);

                        // wk_packingCardList
                        sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("PackID", "");
                        sqlParameter.Add("nCardSeq", 1);
                        sqlParameter.Add("sCardID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString());
                        sqlParameter.Add("nProdQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString()));
                        sqlParameter.Add("sCreateUserID", Frm_tins_Main.g_tBase.PersonID);

                        WizCommon.Procedure pro2 = new WizCommon.Procedure();
                        pro2.Name = "[xp_prdIns_iWkPackingCardList]";
                        pro2.OutputUseYN = "N";
                        pro2.OutputName = "JobID";
                        pro2.OutputLength = "20";

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter);
                    }

                    // Inspect 넣기
                    int InspectQty = Lib.ConvertInt(dgdMain.Rows[i].Cells["InspectQty"].Value.ToString());

                    // 검사수량이 0 이상인 건들만 넣기
                    if (InspectQty > 0)
                    {
                        sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
                        sqlParameter.Add("RollSeq", 0);
                        sqlParameter.Add("OrderSeq", 1);
                        sqlParameter.Add("ExamNO", "00");
                        sqlParameter.Add("ExamDate", mtb_Date.Text.Replace("-", ""));

                        sqlParameter.Add("ExamTime", dtInspectTime.Value.ToString("HHmmss"));
                        sqlParameter.Add("TeamID", "");
                        sqlParameter.Add("PersonID", Frm_tins_Main.g_tBase.PersonID);
                        sqlParameter.Add("RealQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["InspectQty"].Value.ToString()));
                        sqlParameter.Add("CtrlQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["PassQty"].Value.ToString()));

                        sqlParameter.Add("UnitClss", "");
                        sqlParameter.Add("GradeID", "1");
                        sqlParameter.Add("LotNo", "");
                        sqlParameter.Add("BoxID", dgdMain.Rows[i].Cells["LabelID"].Value.ToString());
                        sqlParameter.Add("DefectQty", Lib.ConvertInt(dgdMain.Rows[i].Cells["DefectQty"].Value.ToString()));

                        sqlParameter.Add("DefectPoint", 0);
                        sqlParameter.Add("DefectID", "");
                        sqlParameter.Add("DefectClss", "");
                        sqlParameter.Add("InstID", "");
                        sqlParameter.Add("CardIDList", "");

                        sqlParameter.Add("CreateUserID", Frm_tins_Main.g_tBase.PersonID);
                        sqlParameter.Add("PackID", "");

                        WizCommon.Procedure pro3 = new WizCommon.Procedure();
                        pro3.Name = "[xp_prdIns_iInspectFinal]";
                        pro3.OutputUseYN = "Y";
                        pro3.OutputName = "RollSeq";
                        pro3.OutputLength = "20";

                        Prolist.Add(pro3);
                        ListParameter.Add(sqlParameter);

                        var lstDefect = dgdMain.Rows[i].Cells["lstDefect"].Value as Dictionary<string, frm_tprc_Work_Defect_U_CodeView>;
                        if (lstDefect != null)
                        {
                            int k = 0;
                            foreach (string Key in lstDefect.Keys)
                            {
                                var Defect = lstDefect[Key] as frm_tprc_Work_Defect_U_CodeView;
                                if (Defect != null
                                    && Lib.ConvertInt(Defect.DefectQty) > 0)
                                {
                                    sqlParameter = new Dictionary<string, object>();

                                    sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
                                    sqlParameter.Add("RollSeq", 0);
                                    sqlParameter.Add("DefectSeq", ++k);
                                    sqlParameter.Add("DefectID", Key);
                                    sqlParameter.Add("DefectQty", Lib.ConvertInt(Defect.DefectQty));

                                    sqlParameter.Add("PersonID", Frm_tins_Main.g_tBase.PersonID);
                                    sqlParameter.Add("PackID", "");

                                    WizCommon.Procedure pro4 = new WizCommon.Procedure();
                                    pro4.Name = "[xp_prdIns_iInspectSub]";
                                    pro4.OutputUseYN = "N";
                                    pro4.OutputName = "JobID";
                                    pro4.OutputLength = "20";

                                    Prolist.Add(pro4);
                                    ListParameter.Add(sqlParameter);
                                }
                            }
                        }

                        // 예외출고 구문
                        //sqlParameter = new Dictionary<string, object>();

                        //sqlParameter.Add("OrderID", dgdMain.Rows[i].Cells["OrderID"].Value.ToString());
                        //sqlParameter.Add("RollSeq", 0);

                        //WizCommon.Procedure pro5 = new WizCommon.Procedure();
                        //pro5.Name = "[xp_prdIns_iInspect_ExcptOut]";
                        //pro5.OutputUseYN = "N";
                        //pro5.OutputName = "JobID";
                        //pro5.OutputLength = "20";

                        //Prolist.Add(pro5);
                        //ListParameter.Add(sqlParameter);

                    }
                }
            
                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("검사 등록이 성공적으로 완료되었습니다.", "[저장 완료]", 0, 1);
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
                WizCommon.Popup.MyMessageBox.ShowBox(ex.Message, "[저장 실패 : SaveData부분]", 0, 1);
            }

            return flag;
        }

        // 저장 전 체크
        private bool CheckData()
        {
            if (dgdMain.Rows.Count == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("검사할 수 있는 라벨이 없습니다.", "저장 전", 0, 1);
                return false;
            }

            // 검사수량이 0 이면 막기.
            if (Lib.ConvertInt(txtInspectQty.Text) == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("검사 수량이 0 입니다.", "저장 전", 0, 1);
                return false;
            }

            // 합격수량이 0 미만이면 저장 못하게 막기!
            if (Lib.ConvertInt(txtPassQty.Text) < 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("합격 수량이 0 미만 일 수 없습니다.\r\n(검사수량, 불량수량을 확인해주세요.)", "저장 전", 0, 1);
                return false;
            }

            // 불량수량 텍스트박스가 "0"이 아닌 "" → 불량 검사를 안했다는 것이니, 메시지 띄우기
            if (txtDefectQty.Text.Equals(""))
            {
                WizCommon.Popup.MyMessageBox.ShowBox("불량 검사를 진행해주세요.", "저장 전", 0, 1);
                return false;
            }

            return true;
        }

        #endregion

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

        #region 수량 / 숫자 키패드 팝업 이벤트

        // 검사수량 등록 시 → 작업수량 합계를 초과한 경우에는 막아야됨
        private void chkInspectQty_Click(object sender, EventArgs e)
        {
            loadNumberKeyboard(txtInspectQty);
        }

        private void chkQtyPerBox_Click(object sender, EventArgs e)
        {
            loadNumberKeyboard(txtQtyPerBox);
        }

        // 작업수량 기입 팝업창 생성
        private void txtNumber_Click(object sender, EventArgs e)
        {
            TextBox txtSender = sender as TextBox;
            if (txtSender != null)
            {
                loadNumberKeyboard(txtSender);
            }
        }

        private void loadNumberKeyboard(TextBox txtSender)
        {
            string Title = txtSender.Tag == null ? "" : txtSender.Tag.ToString();

            //string bakup = txtSender.Text;

            //txtSender.Text = "";
            WizWork.POPUP.Frm_CMNumericKeypad keypad = new WizWork.POPUP.Frm_CMNumericKeypad(Title + "입력", Title);

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                int chgValue = Lib.ConvertInt(keypad.tbInputText.Text);

                // 검사 수량 변경시 체크
                if (CheckChangeInspectQty(txtSender, chgValue) == false) {  return; }

                txtSender.Text = Lib.stringFormatN0(chgValue);
            }

            CalQty();
        }

        // 수량 변경하는 건이 검사수량 일때.
        // 1. 투입 라벨의 총 작업수량을 넘길 수 없음.
        // 2. 불량수량보다 적을 수 없음.
        private bool CheckChangeInspectQty(TextBox txtSender, int value)
        {
            if (txtSender == null) { return false; }

            // 검사수량 변경 일시
            if (txtSender.Name.Equals("txtInspectQty") == true)
            {
                if (TotalQty < value)
                {
                    string MsgTotalQty = Lib.stringFormatN0(TotalQty);

                    WizCommon.Popup.MyMessageBox.ShowBox("검사 수량이 총 작업수량 : " + MsgTotalQty + "개를 넘을 수 없습니다.", "검사수량 변경 전", 0, 1);
                    return false;
                }

                int DefectQty = Lib.ConvertInt(txtDefectQty.Text);

                if (value < DefectQty)
                {
                    string MsgTotalQty = Lib.stringFormatN0(TotalQty);

                    WizCommon.Popup.MyMessageBox.ShowBox("검사 수량이 불량수량 : " + txtDefectQty.Text + "개 보다 작을 수 없습니다.", "검사수량 변경 전", 0, 1);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 수량 입력 및 변경시, 계산 메서드 : CalQty()

        private void CalQty()
        {
            //Dictionary<string, frm_tprc_Work_Defect_U_CodeView> cloneDicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>(dicDefect);
            Dictionary<string, frm_tprc_Work_Defect_U_CodeView> cloneDicDefect = dicDefect.ToDictionary(entry => entry.Key, entry => entry.Value.Copy());
            Dictionary<string, frm_tprc_Work_Defect_U_CodeView> InsDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();

            int InspectQty = Lib.ConvertInt(txtInspectQty.Text);
            int DefectQty = Lib.ConvertInt(txtDefectQty.Text);

            // 합격수량 = 검사수량 - 불량수량
            int PassQty = InspectQty - DefectQty;
            txtPassQty.Text = Lib.stringFormatN0(PassQty);

            // 잔량수량 = 총수량 - 검사수량
            int RemainQty = TotalQty - InspectQty;
            txtRemainQty.Text = Lib.stringFormatN0(RemainQty);

            // 
            int QtyPerBox = Lib.ConvertInt(txtQtyPerBox.Text);

            if (PassQty <= 0) // 합격 수량이 0 이면 라벨 발행할 필요가 없으므로 0처리
            {
                txtBoxQty.Text = "0";
            }
            else if (QtyPerBox == 0) // 박스당 수량이 없다면 1장으로 뽑기
            {
                txtBoxQty.Text = "1";
            }
            else // 나머지는 나누기 + 올림처리
            {
                txtBoxQty.Text = Lib.stringFormatN0(Math.Ceiling(1.0 * PassQty / QtyPerBox));
            }

            // 투입 라벨 리스트 수량 수정 처리
            for (int i = 0; i < dgdMain.Rows.Count; i++)
            {
                //int WorkQty = Lib.ConvertInt(dgdMain.Rows[i].Cells["WorkQty"].Value.ToString());

                var Main = dgdMain.Rows[i].DataBoundItem as Frm_PopUp_PackingAndOutBox_CodeView;
                if (Main != null)
                {
                    // 검사수량이 있어야됨!
                    if (InspectQty > 0)
                    {
                        // 검사수량 조정
                        if (Lib.ConvertInt(Main.WorkQty) >= InspectQty)
                        {
                            Main.InspectQty = Lib.stringFormatN0(InspectQty);
                            InspectQty = 0;
                        }
                        else
                        {
                            Main.InspectQty = Lib.stringFormatN0(Main.WorkQty);
                            InspectQty -= Lib.ConvertInt(Main.WorkQty);
                        }

                        // 불량 수량이 있으면.
                        if (DefectQty > 0)
                        {
                            // 불량 수량 조정
                            if (Lib.ConvertInt(Main.InspectQty) >= DefectQty)
                            {
                                Main.DefectQty = Lib.stringFormatN0(DefectQty);
                                DefectQty = 0;
                            }
                            else
                            {
                                Main.DefectQty = Lib.stringFormatN0(Main.InspectQty);
                                DefectQty -= Lib.ConvertInt(Main.InspectQty);
                            }
                        }

                        Main.PassQty = Lib.stringFormatN0(Lib.ConvertInt(Main.InspectQty) - Lib.ConvertInt(Main.DefectQty));
                        Main.RemainQty = Lib.stringFormatN0(Lib.ConvertInt(Main.WorkQty) - Lib.ConvertInt(Main.InspectQty));
                    }
                    else
                    {
                        Main.InspectQty = Lib.stringFormatN0(0);
                        Main.PassQty = Lib.stringFormatN0(0);
                        Main.DefectQty = Lib.stringFormatN0(0);
                        Main.RemainQty = Lib.stringFormatN0(Lib.ConvertInt(Main.WorkQty));
                    }

                    // 등록할 해당 라벨 불량 리스트 초기화
                    InsDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();

                    // 해당 라벨에 불량 수량이 존재한다면.
                    int InsDefectQty = Lib.ConvertInt(Main.DefectQty);
                    if (InsDefectQty > 0)
                    {
                        foreach(string Key in cloneDicDefect.Keys)
                        {
                            // 불량 갯수 모두 등록 했으면 종료
                            if (InsDefectQty <= 0) { break; }

                            // 불량 유형 하나 들고와서
                            var Defect = (cloneDicDefect[Key] as frm_tprc_Work_Defect_U_CodeView).Copy();
                            if (Defect != null
                                && Lib.ConvertInt(Defect.DefectQty) > 0)
                            {
                                // 해당 라벨 불량 수량 보다 작으면, 그대로 넣고 Key로 삭제.
                                if (Lib.ConvertInt(Defect.DefectQty) <= InsDefectQty)
                                {
                                    InsDefect.Add(Key, Defect);
                                    //cloneDicDefect.Remove(Key);
                                    cloneDicDefect[Key].DefectQty = "0";

                                    InsDefectQty -= Lib.ConvertInt(Defect.DefectQty);
                                }
                                else // 크면, 라벨 불량 갯수만큼만 넣고, 전체 불량 리스트 수정.
                                {
                                    cloneDicDefect[Key].DefectQty = Lib.stringFormatN0(Lib.ConvertInt(Defect.DefectQty) - InsDefectQty);

                                    Defect.DefectQty = Lib.stringFormatN0(InsDefectQty);
                                    InsDefect.Add(Key, Defect);

                                    InsDefectQty = 0;
                                }
                            }
                        }
                    }
                    
                    dgdMain.Rows[i].Cells["InspectQty"].Value = Main.InspectQty;
                    dgdMain.Rows[i].Cells["DefectQty"].Value = Main.DefectQty;
                    dgdMain.Rows[i].Cells["PassQty"].Value = Main.PassQty;
                    dgdMain.Rows[i].Cells["RemainQty"].Value = Main.RemainQty;
                    dgdMain.Rows[i].Cells["lstDefect"].Value = InsDefect;
                }
            }

            dgdMain.Refresh();
        }

        #endregion

        #region 불량 수량 클릭 이벤트

        private void chkDefectQty_Click(object sender, EventArgs e)
        {
            loadDefectList();
        }

        private void txtDefectQty_Click(object sender, EventArgs e)
        {
            loadDefectList();
        }

        private void loadDefectList()
        {
            double InsQty = Lib.ConvertDouble(txtInspectQty.Text);
            if (InsQty == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("검사 수량을 먼저 입력해주세요.\r\n(불량 수량은 검사수량 이하로 입력 가능합니다.)", "불량 등록 전", 0, 1);
                return;
            }

            frm_tprc_Work_Defect_U defect = new frm_tprc_Work_Defect_U("", dicDefect, Lib.ConvertDouble(txtInspectQty.Text));
            defect.Owner = this;
            defect.ShowDialog();
            if (defect.DialogResult == DialogResult.OK)
            {
                this.dicDefect = defect.dicDefect;
                this.txtDefectQty.Text = defect.returnTotalQty;
            }

            CalQty();
        }

        #endregion

        // 테스트
        private void txtInspectQty_TextChanged(object sender, EventArgs e)
        {
            //txtInspectQty.Font = new Font(txtInspectQty.Font, FontStyle.);
        }
    }

    #region 코드뷰 : Frm_PopUp_PackingAndOutBox_CodeView
    public class Frm_PopUp_PackingAndOutBox_CodeView
    {
        public int Num { get; set; }

        public string LabelID { get; set; }
        public string LabelGubun { get; set; }
        public string ArticleID { get; set; }
        public string Article { get; set; }
        public string BuyerArticleNo { get; set; }
        public string WorkQty { get; set; }
        public string InspectQty { get; set; }
        public string PassQty { get; set; }
        public string DefectQty { get; set; }
        public string RemainQty { get; set; }

        public string OrderID { get; set; }

        public Dictionary<string, frm_tprc_Work_Defect_U_CodeView> lstDefect { get; set; }

        public Frm_PopUp_PackingAndOutBox_CodeView(Frm_tins_Order_Q_CodeView Ins)
        {
            this.LabelID = Ins.LabelID;
            this.LabelGubun = Ins.LabelGubun;
            this.ArticleID = Ins.ArticleID;
            this.Article = Ins.Article;
            this.BuyerArticleNo = Ins.BuyerArticleNo;
            this.WorkQty = Ins.NoInspectQty;
            this.InspectQty = Ins.WorkQty;
            this.PassQty = Ins.WorkQty;
            this.DefectQty = "0";
            this.RemainQty = "0";

            this.OrderID = Ins.OrderID;

            this.lstDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();
        }
    }
    #endregion
}
