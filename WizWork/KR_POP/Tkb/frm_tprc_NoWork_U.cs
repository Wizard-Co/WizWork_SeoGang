using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Tools;
using WizWork.Properties;
using WizCommon;
using System.Diagnostics;
using System.IO;

namespace WizWork
{
    public partial class Frm_tprc_NoWork_U : Form
    {
        WizWorkLib Lib = new WizWorkLib();

        private DataSet ds = null;
        string[] Message = new string[2];
        string sProcessID = "";
        string sMachineID = "";
        string sPersonID = "";
        string sTeam = "";
        string sUserID = "";
        string sDayOrNightID = "";

        public bool blClose = false;
        public Frm_tprc_NoWork_U()
        {
            InitializeComponent();
        }

        private void Frm_tprc_NoWork_U_Load(object sender, EventArgs e)
        {
            cmdsetProcess.Text = "공정\r\n작업자\r\n변경";
            SetInfo();                      // 작업자설정 팝업 로드
            SetScreen();
            SetDateTimePicker();
            //SetFormDataClear();
            if (!blClose)
            {
                SetComboBox();               // 콤보박스 설정
                SetFormDataClear();
                if (sUserID != "")
                {
                    sUserID = Frm_tprc_Main.g_tBase.PersonID;
                }
            }
            txtBarCodeScan.Select();
            txtBarCodeScan.Focus();
        }

        private void SetInfo()
        {
            sProcessID = Frm_tprc_Main.g_tBase.ProcessID;//setProcess폼에서 선택한 ProcessID
            sMachineID = Frm_tprc_Main.g_tBase.MachineID;//setProcess폼에서 선택한 MachineID
            sPersonID = Frm_tprc_Main.g_tBase.PersonID;//setProcess폼에서 선택한 PersonID
            sTeam = Frm_tprc_Main.g_tBase.TeamID;//setProcess폼에서 선택한 TeamID
            sUserID = Frm_tprc_Main.g_tBase.PersonID;//setProcess폼에서 선택한 UserID
            sDayOrNightID = Frm_tprc_Main.g_tBase.DayOrNightID;//setProcess폼에서 선택한 UserID
        }

        private void SetFormDataClear()
        {
            txtBarCodeScan.Text = "";   
            lblCustom.Text = "";
            lblCustom.Tag = "";
            lblArticle.Text = "";
            lblArticle.Tag = "";
            lblPLotID.Text = "";
            lblPLotID.Tag = "";
            lblBuyerArticleNo.Text = "";
            lblBuyerModel.Text = "";
            cboNoWorkCode.SelectedIndex = 0;           
            txtComments.Text = "";
            txtBarCodeScan.Focus();
        }

        private void SetComboBox()
        {
            try
            {
                ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sNoWorkCodeReason", null, false);
                cboNoWorkCode.DataSource = ds.Tables[0];
                DataRow newRow = ds.Tables[0].NewRow();
                newRow[WizWork_sNoWorkCodeReason.CODE_ID] = "";
                newRow[WizWork_sNoWorkCodeReason.CODE_NAME] = "";// Resources.CMB_VALUE_OPTION_ALL;
                ds.Tables[0].Rows.InsertAt(newRow, 0);

                cboNoWorkCode.ValueMember = WizWork_sNoWorkCodeReason.CODE_ID;
                cboNoWorkCode.DisplayMember = WizWork_sNoWorkCodeReason.CODE_NAME;
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }
            finally
            {
                DataStore.Instance.CloseConnection();
            }
        }
        private void cmdBarCodeScan_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_CMKeypad FK = new WizCommon.Popup.Frm_CMKeypad(txtBarCodeScan.Text.Trim(), "생산LOT ID");
            FK.Owner = this;
            if (FK.ShowDialog() == DialogResult.OK)
            {
                if ((txtBarCodeScan.Text.Length == 15 || txtBarCodeScan.Text.Length == 16) && txtBarCodeScan.Text.ToUpper().Contains("PL"))
                {
                    txtBarCodeScan.Text = FK.tbInputText.Text.Trim();
                }
                else
                {
                    txtBarCodeScan.Text = "";
                }
            }

            if (this.txtBarCodeScan.Text != "")
            {
                LF_GetBarcodeData();
            }
        }


        public void LF_GetBarcodeData()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("LotID", txtBarCodeScan.Text.Trim());

                ds = DataStore.Instance.ProcedureToDataSet("xp_wkNoWork_sLotID", sqlParameter, false);

                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    lblPLotID.Text = txtBarCodeScan.Text.Trim().ToUpper();
                    lblArticle.Tag = dr["ArticleID"].ToString().Trim();
                    lblArticle.Text = dr["Article"].ToString().Trim();
                    lblCustom.Text = dr["KCustom"].ToString().Trim();
                    lblPLotID.Tag = dr["InstID"].ToString().Trim();
                    lblCustom.Tag = dr["InstDetSeq"].ToString().Trim();
                    lblBuyerArticleNo.Text = dr["BuyerArticleNo"].ToString().Trim();
                    lblBuyerModel.Text = dr["BuyerModel"].ToString().Trim();
                    txtBarCodeScan.Text = "";
                }
                else
                {
                    Message[0] = "[지시LOTID 입력오류]";
                    Message[1] = "존재하지 않는 지시LotID입니다.\r\n확인하여주시기 바랍니다.";
                    txtBarCodeScan.Text = "";
                    throw new Exception();
                }
            }
            catch (Exception excpt)
            {
                Console.Write(excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
        }

        private void SetScreen()
        {
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Margin = new Padding(1, 1, 1, 1);
            foreach (Control control in tlpForm.Controls)//con = tlp 상위에서 2번째
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(1, 1, 1, 1);
                foreach (Control contro in control.Controls)//tlp 상위에서 3번째
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
                                    foreach (Control c in co.Controls)
                                    {
                                        c.Dock = DockStyle.Fill;
                                        c.Margin = new Padding(1, 1, 1, 1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool CheckData()
        {
            try
            {
                float StartTime = 0;
                float EndTime = 0;
                //if (lblPLotID.Text == "")
                //{
                //    Message[0] = "[지시LOTID 입력오류]";
                //    Message[1] = "지시LOTID를 입력해주십시오.";
                //    txtBarCodeScan.Focus();
                //    throw new Exception();
                //}
                if (cboNoWorkCode.SelectedValue.ToString().Trim() == "")
                {
                    Message[0] = "[무작업 사유]";
                    Message[1] = "무작업 사유가 선택되지 못하였습니다.";
                    cboNoWorkCode.Select();
                    cboNoWorkCode.Focus();
                    throw new Exception();
                }
                StartTime = float.Parse(mtb_From.Text.Replace("-", "") + dtStartTime.Value.ToString("HHmmss"));
                EndTime = float.Parse(mtb_To.Text.Replace("-", "") + dtEndTime.Value.ToString("HHmmss"));
                if (StartTime > EndTime)
                {
                    Message[0] = "[무작업시간 오류]";
                    Message[1] = "시작시간이 종료시간보다 더 큽니다.";
                    throw new Exception();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return false;
            }

        }
        //저장
        private void SaveData()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                string MoldID = string.Empty;
                string MCID = string.Empty;
                string managerid = string.Empty;
                string customid = string.Empty;
                string buycustomid = string.Empty;
                string personid = string.Empty;
                string personname = string.Empty;

                //1
                List<Procedure> Prolist = new List<Procedure>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                string strstrLabelGubun = "0";
                string strComments = txtComments.Text.Trim();//"무작업";
                string srReworkOldYN = "";
                string strReworkLinkProdID = "";
                string strJobGbn = "2";

                sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();
                sqlParameter.Add("JobID", 0);              //JobID 입력안함. 지금 프로시저 수행 후 만들어짐~~
                sqlParameter.Add("InstID", lblPLotID.Tag.ToString());
                sqlParameter.Add("InstDetSeq", lblCustom.Tag.ToString());
                sqlParameter.Add("LabelID", lblPLotID.Text.ToUpper());  //TWkLabelPrint(i).sLabelID)
                sqlParameter.Add("StartSaveLabelID", lblPLotID.Text.ToUpper());  //TWkLabelPrint(i).sLabelID)

                sqlParameter.Add("LabelGubun", strstrLabelGubun);       //WkLabelPrint(i).sLabelGubun
                sqlParameter.Add("ProcessID", sProcessID); //선택되있는 sProcessID(setProcess에서 선택한)
                sqlParameter.Add("MachineID", sMachineID); //선택되있는 sMachineID(setProcess에서 선택한)
                sqlParameter.Add("ScanDate", DateTime.Now.ToString("yyyyMMdd")); //년월일
                sqlParameter.Add("ScanTime", DateTime.Now.ToString("HHmmss")); //시분초

                sqlParameter.Add("ArticleID", lblArticle.Tag.ToString()); //품명코드=재종코드
                sqlParameter.Add("WorkQty", 0); //생산수량
                sqlParameter.Add("Comments", strComments);//지시커멘트
                sqlParameter.Add("ReworkOldYN", srReworkOldYN); //재작업여부 NO
                sqlParameter.Add("ReworkLinkProdID", strReworkLinkProdID);//????????????????????????

                sqlParameter.Add("WorkStartDate", mtb_From.Text.Replace("-", ""));      //작업시작날짜
                sqlParameter.Add("WorkStartTime", dtStartTime.Value.ToString("HHmmss"));//작업시작시간
                sqlParameter.Add("WorkEndDate",   mtb_To.Text.Replace("-", ""));        //작업종료날짜
                sqlParameter.Add("WorkEndTime", dtEndTime.Value.ToString("HHmmss"));    //작업종료시간

                sqlParameter.Add("JobGbn", strJobGbn);//작업구분 1:정상,2:무작업,3:재작업 NO_Work_U폼에서는 2번 무작업으로 처리
                sqlParameter.Add("NoReworkCode", cboNoWorkCode.SelectedValue.ToString());//무작업코드_
                sqlParameter.Add("DayOrNightID", sDayOrNightID);// 주간 / 야간

                sqlParameter.Add("WDNO", "");
                sqlParameter.Add("WDID", "");
                sqlParameter.Add("WDQty", 0);
                sqlParameter.Add("LogID", 0);
                sqlParameter.Add("s4MID", "");

                sqlParameter.Add("CreateUserID", sUserID);// 작업자

                Procedure pro1 = new Procedure();
                pro1.Name = "xp_wkResult_iWkResult";
                pro1.OutputUseYN = "Y";
                pro1.OutputName = "JobID";
                pro1.OutputLength = "19";

                Prolist.Add(pro1);
                ListParameter.Add(sqlParameter);
            

                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputGetCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")                
                {
                    Message[0] = "[저장 성공]";
                    Message[1] = "정상적으로 등록이 되었습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }
                else
                {
                    Message[0] = "[저장 실패]";
                    Message[1] = "오류! 관리자에게 문의";
                    throw new Exception();
                }
                SetFormDataClear();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                SaveData();
            }
        }

        private void cmdsetProcess_Click(object sender, EventArgs e)
        {
            frm_tprc_setProcess form = new frm_tprc_setProcess(true);
            if (form.ShowDialog() == DialogResult.OK)
            {
                SetInfo();
                ((Frm_tprc_Main)(MdiParent)).Set_stsInfo();
            };
        }

        //전체 초기화
        private void cmdClear_Click(object sender, EventArgs e)
        {
            SetFormDataClear();
        }

        public void Set_stbInfo(string g_TeamID, string g_TeamName, string sPersonID, string sPersonName, string sMachineID, string sMachineName,
                             string MoldID, string MoldName, string sProcessID, string sProcessName, string strInstID)
        {
            ((Frm_tprc_Main)(MdiParent)).Set_stsInfo();
        }

        private void txtBarCodeScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtBarCodeScan.Text.Length == 15 && txtBarCodeScan.Text.ToUpper().Contains("PL"))
                {
                    LF_GetBarcodeData();
                }
                else
                {
                    txtBarCodeScan.Text = "";
                    return;
                }
            }
        }

        #region 달력 From값 입력 // 달력 창 띄우기
        private void mtb_From_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion
        #region 달력 To값 입력 // 달력 창 띄우기
        private void mtb_To_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion
        #region Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
        private void GetDate(string strDate, string btnName)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
            if (btnName == mtb_From.Name)
            {
                mtb_From.Text = dateTime.ToString("yyyy-MM-dd");
            }
            else if (btnName == mtb_To.Name)
            {
                mtb_To.Text = dateTime.ToString("yyyy-MM-dd");
            }

        }
        #endregion
        private void TimeCheck(string strTime)
        {
            POPUP.Frm_CMNumericKeypad FK = new POPUP.Frm_CMNumericKeypad(strTime);
            FK.Owner = this;
            string sTime = "";
            DateTime dt = DateTime.Now;
            if (FK.ShowDialog() == DialogResult.OK)
            {

                sTime = FK.InputTextValue;
                if (sTime != "")
                {
                    dt = DateTime.ParseExact(sTime, "HHmmss", null);
                }
            }

            if (strTime == "시작시간") { dtStartTime.Value = dt; }
            else if (strTime == "종료시간") { dtEndTime.Value = dt; }
        }
        private void SetDateTimePicker()
        {
            mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
            dtStartTime.Format = DateTimePickerFormat.Custom;
            dtStartTime.CustomFormat = "HH:mm:ss";
            dtEndTime.Format = DateTimePickerFormat.Custom;
            dtEndTime.CustomFormat = "HH:mm:ss";
        }
        private void btnStartTime_Click(object sender, EventArgs e)
        {
            TimeCheck("시작시간");
        }

        private void btnEndTime_Click(object sender, EventArgs e)
        {
            TimeCheck("종료시간");
        }

        private void lblComments_Click(object sender, EventArgs e)
        {
            try
            {
                //실행중인 프로세스가 없을때 
                if (!Frm_tprc_Main.Lib.ReturnProcessRunStop("osk"))
                {
                    //System.Diagnostics.Process ps = new System.Diagnostics.Process();
                    //ps.StartInfo.FileName = "osk.exe";
                    //ps.Start();

                    string progFiles = @"C:\Program Files\Common Files\Microsoft Shared\ink";
                    string keyboardPath = Path.Combine(progFiles, "TabTip.exe");

                    Process.Start(keyboardPath);
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                MessageBox.Show("실패");
                Process.Start(@"C:\Windows\winsxs\x86_microsoft-windows-osk_31bf3856ad364e35_6.1.7601.18512_none_acc225fbb832b17f\osk.exe");
            }

            txtComments.Select();
            txtComments.Focus();
        }

        private void Frm_tprc_NoWork_U_Activated(object sender, EventArgs e)
        {
            ((Frm_tprc_Main)(MdiParent)).LoadRegistry();
        }
    }
}
