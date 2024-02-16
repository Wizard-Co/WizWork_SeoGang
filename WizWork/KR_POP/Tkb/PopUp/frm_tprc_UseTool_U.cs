using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizWork.GLS.PopUp
{
    public partial class frm_tprc_UseTool_U : Form
    {

        public frm_tprc_UseTool_U()
        {
            InitializeComponent();

            if (Frm_tprc_Main.g_tBase.PersonID != null
                && !Frm_tprc_Main.g_tBase.PersonID.Trim().Equals(""))
            {
                txtPerson.Tag = Frm_tprc_Main.g_tBase.PersonID.Trim();
                txtPerson.Text = Frm_tprc_Main.g_tBase.Person.Trim();
            }

            InitPanel();
            SetMCComboBox(Frm_tprc_Main.g_tBase.ProcessID, Frm_tprc_Main.g_tBase.MachineID); // 설비 콤보박스 세팅

            mtb_ChangeDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dtChangeTime.Text = DateTime.Now.ToString();

            //dtChangeTime.Enabled = false;
        }

        #region 설비 콤보박스 세팅

        private void SetMCComboBox(string ProcessID, string MachineID)
        {
            int intnChkProc = 1;

            //strProcessID = Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID");
            //string[] gubunProcess = strProcessID.Split(new char[] { '|' });

            //공정 가져오기
            try
            {
                //strProcessID = string.Empty;

                //for (int i = 0; i < gubunProcess.Length; i++)
                //{
                //    if (strProcessID.Equals(string.Empty))
                //    {
                //        strProcessID = gubunProcess[i];
                //    }
                //    else
                //    {
                //        strProcessID = strProcessID + "|" + gubunProcess[i];
                //    }
                //}

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("nchkProc", intnChkProc);//cboProcess.Text 
                sqlParameter.Add("ProcessID", ProcessID);//cboProcess.Text
                sqlParameter.Add("MachineID", MachineID);//cboProcess.Text

                DataSet ds = DataStore.Instance.ProcedureToDataSet("[xp_prdWork_sMCByProcessID]", sqlParameter, false);

                //DataRow newRow = ds.Tables[0].NewRow();
                //newRow[Work_sProcess.PROCESSID] = "";
                //newRow[Work_sProcess.PROCESS] = "전체";

                //DataRow newRow2 = ds.Tables[0].NewRow();
                //newRow2[Work_sProcess.PROCESSID] = strProcessID;
                //newRow2[Work_sProcess.PROCESS] = "부분전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //ds.Tables[0].Rows.InsertAt(newRow, 0);
                    cboMC.DataSource = ds.Tables[0];
                }

                cboMC.ValueMember = "MCID";
                cboMC.DisplayMember = "MCNAME";
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        #endregion

        #region 설비 선택시 → 해당하는 Tool 조회하기

        // 설비 선택시 → 해당하는 Tool 조회하기
        private void cboMachine_SelectedValueChanged(object sender, EventArgs e)
        {
            cboMCPart.DataSource = null;

            if (cboMC.SelectedValue != null)
            {
                try
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Clear();

                    sqlParameter.Add("MCID", cboMC.SelectedValue.ToString());

                    DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_ToolChange_sMCPartByMCID", sqlParameter, false);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        cboMCPart.DataSource = ds.Tables[0];
                    }

                    cboMCPart.ValueMember = "MCPartID";
                    cboMCPart.DisplayMember = "MCPartName";
                }
                catch (Exception excpt)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                }
            }
        }

        #endregion

        #region MCPart - Tool 이 선택 되면 자동으로 작업수량, 툴 설정수명을 가져옴

        private void cboMCPart_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboMC.SelectedValue != null
                && cboMCPart.SelectedValue != null)
            {
                FillGrid_MCPartInfo(cboMC.SelectedValue.ToString(), cboMCPart.SelectedValue.ToString());
            }
        }

        // 설정된 설비와 툴 아이디로 작업수량, 설정수명
        // 조회 메서드
        private void FillGrid_MCPartInfo(string MCID, string MCPartID)
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("@nchkMCID", 1);
                sqlParameter.Add("@sMCID", MCID);
                sqlParameter.Add("@nchkMCPartID", 1);
                sqlParameter.Add("@sMCPartID", MCPartID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sMCPartInfo", sqlParameter, false);


                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    txtWorkQty.Text = stringFormatN0(dr["WorkQty"]).Equals("") ? "0" : stringFormatN0(dr["WorkQty"]);
                    txtSetProdQty.Text = stringFormatN0(dr["setProdQty"]).Equals("") ? "0" : stringFormatN0(dr["setProdQty"]);
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        #endregion

        #region tblForm : 테이블 레이아웃 의 하위 템플릿을 꽉차게 설정!!!!!
        private void InitPanel()
        {
            tlpForm.Dock = DockStyle.Fill;
            foreach (Control control in tlpForm.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(1, 1, 1, 1);
                foreach (Control ctl in control.Controls)//tlp 상위에서 3번째
                {
                    ctl.Dock = DockStyle.Fill;
                    ctl.Margin = new Padding(1, 1, 1, 1);
                    foreach (Control con in ctl.Controls)
                    {
                        con.Dock = DockStyle.Fill;
                        con.Margin = new Padding(1, 1, 1, 1);
                        foreach (Control co in con.Controls)
                        {
                            co.Dock = DockStyle.Fill;
                            co.Margin = new Padding(1, 1, 1, 1);
                            foreach (Control c in co.Controls)
                            {
                                c.Dock = DockStyle.Fill;
                                c.Margin = new Padding(1, 1, 1, 1);
                                foreach (Control contro in c.Controls)
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
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
        #endregion

        #region Content -  교체일시, 작업자변경, 교체수명 클릭 → 키패드 호출

        // 교체일시 날짜입력 → 캘린더
        // 어차피 교체한 날짜에 입력 하는데, 날짜 변경이 필요한가?
        // 날짜 수정은 사무실화면에서 하면 되는거 아닌가 싶어서 일단 막음
        private void mtb_ChangeDate_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }
        private void LoadCalendar()
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_ChangeDate.Text.Replace("-", ""), mtb_ChangeDate.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
            //Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
            void GetDate(string strDate, string btnName)
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
                mtb_ChangeDate.Text = dateTime.ToString("yyyy-MM-dd");
            }
        }

        // 작업자 변경시!!!!!!
        private void txtPerson_Click(object sender, EventArgs e)
        {
            frm_tprc_setProcess Person = new frm_tprc_setProcess(true);//NoWork == true라는 bool값
            //Person.Owner = this;
            if (Person.ShowDialog() == DialogResult.OK)
            {
                txtPerson.Tag = Frm_tprc_Main.g_tBase.PersonID.Trim();
                txtPerson.Text = Frm_tprc_Main.g_tBase.Person.Trim();
            };
        }

        // 교체수명 클릭 → 입력하기 위한 키패드 호출
        private void txtChasu_Click(object sender, EventArgs e)
        {
            txtChasu.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("교체수명", "교체수명");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtChasu.Text = keypad.tbInputText.Text;
            }
        }

        #endregion

        // 저장버튼 클릭 이벤트
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                if (cboMC.SelectedValue != null
                              && cboMCPart.SelectedValue != null)
                {
                    if (SaveData())
                    {
                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        #region 저장

        private bool SaveData()
        {
            bool flag = false;

            try
            {
                
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("McID", cboMC.SelectedValue.ToString());
                sqlParameter.Add("McPartID", cboMCPart.SelectedValue.ToString());
                sqlParameter.Add("Chasu", ConvertInt(txtChasu.Text));
                sqlParameter.Add("CycleProdQty", ConvertInt(txtWorkQty.Text));
                sqlParameter.Add("WorkPersonID", txtPerson.Tag != null ? txtPerson.Tag.ToString() : "");

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_MCPartChange_i", sqlParameter, false);

                if (dt is null || dt.Rows.Count == 0 || dt.Rows.Count > 1)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("툴 교체 정보가 저장되었습니다.", "[저장완료]", 0, 1);
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }

            return flag;
        }

        #endregion

        #region 유효성 검사
        
        private bool CheckData()
        {
            bool flag = true;

            // 설비를 선택하지 않았을 때
            if (cboMC.SelectedValue == null)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("설비를 선택해주세요.", "[Tool 교체 등록 전 검사]", 0, 1);
                flag = false;
                return flag;
            }

            // Tool 을 선택하지 않았을 때
            if (cboMCPart.SelectedValue == null)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("해당 설비의 Tool을 입력해주세요.", "[Tool 교체 등록 전 검사]", 0, 1);
                flag = false;
                return flag;
            }

            // 교체수명 입력 안했을 시
            if (txtChasu.Text.Trim().Equals(""))
            {
                WizCommon.Popup.MyMessageBox.ShowBox("교체수명을 입력해주세요.", "[Tool 교체 등록 전 검사]", 0, 1);
                flag = false;
                return flag;
            }

            // 작업자 입력 안했을 시
            if (txtPerson.Tag == null)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("작업자를 입력해주세요.", "[Tool 교체 등록 전 검사]", 0, 1);
                flag = false;
                return flag;
            }

            return flag;
        }

        #endregion

        // 화면닫기 버튼 클릭 이벤트
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

       
    }
}
