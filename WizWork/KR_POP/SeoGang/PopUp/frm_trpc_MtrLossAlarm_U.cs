using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using WizCommon;

namespace WizWork
{
    public partial class frm_trpc_MtrLossAlarm_U : Form
    {
        WizWorkLib Lib = new WizWorkLib();
        string[] Message = new string[2];//메시지박스용 배열
        string sProcessID = string.Empty;
        public delegate void TextEventHandler();                // string을 반환값으로 갖는 대리자를 선언합니다.
        //public event TextEventHandler WriteTextEvent;           // 대리자 타입의 이벤트 처리기를 설정합니다.
        List<WizCommon.CB_IDNAME> list_cbx = new List<WizCommon.CB_IDNAME>();
        WizCommon.Popup.Frm_CMKeypad FK = null;
        public frm_trpc_MtrLossAlarm_U()
        {
            InitializeComponent();
        }

        public frm_trpc_MtrLossAlarm_U(string strProcessID)
        {
            InitializeComponent();
            this.sProcessID = strProcessID.Substring(0, 2) + "01";
            this.lblProcess.Tag = strProcessID.Substring(0, 2) + "01";
        }

        private void frm_trpc_MtrLossAlarm_U_Load(object sender, EventArgs e)
        {
            DataClear();
            this.Text = "성형자재 부족요청";
            this.Size = new Size(963, 378);
            SetScreen();
            FillComboBox();
            GetGloVar();
            //GetPLotID();
            mtb_Date.Text = DateTime.Now.ToString("yyMMdd");
            txtInstID.Select();
            txtInstID.Focus();
        }

        private void GetGloVar() 
        {
            string[] sProcessID = new string[2];
            string Query = "select ProcessID from mt_Process where ProcessID = '"
                + Frm_tprc_Main.g_tBase.ProcessID + "'";
            sProcessID = DataStore.Instance.ExecuteQuery(Query, false);

            string[] sMachineID = new string[2];
            string Query2 = "select MachineID from mt_Machine where ProcessID = '"
                + Frm_tprc_Main.g_tBase.ProcessID + "'" + " and MachineID = '"
                + Frm_tprc_Main.g_tBase.MachineID + "'";
            sMachineID = DataStore.Instance.ExecuteQuery(Query2, false);

            string[] sPersonID = new string[2];
            string Query3 = "select PersonID from mt_PersonMachine where ProcessID = '"
                + Frm_tprc_Main.g_tBase.ProcessID + "'";
            sPersonID = DataStore.Instance.ExecuteQuery(Query3, false);

            if (sProcessID[0].ToLower() == "success")
            {
                if (sProcessID[1].ToString() == Frm_tprc_Main.g_tBase.ProcessID)
                {
                    lblProcess.Text = Frm_tprc_Main.g_tBase.Process;
                    lblProcess.Tag = Frm_tprc_Main.g_tBase.ProcessID;
                    if (sMachineID[0].ToLower() == "success" && sMachineID[1].ToString() == Frm_tprc_Main.g_tBase.MachineID)
                    {
                        lblMachine.Text = Frm_tprc_Main.g_tBase.Machine;
                        lblMachine.Tag = Frm_tprc_Main.g_tBase.MachineID;
                    }
                    else//MachineID가 해당 공정의 Machine이 아닐경우 라벨에 Machine을 넣지않는다.
                    {
                        lblMachine.Text = "";
                        lblMachine.Tag = "";
                    }
                    if (sPersonID[0].ToLower() == "success" && sPersonID[1].ToString() == Frm_tprc_Main.g_tBase.PersonID)
                    {//해당 공정의 작업자 일 경우 
                        lblPerson.Text = Frm_tprc_Main.g_tBase.Person;
                        lblPerson.Tag = Frm_tprc_Main.g_tBase.PersonID;
                    }
                    else
                    {
                        lblPerson.Text = "";
                        lblPerson.Tag = "";
                    }
                }
                else//전역변수 ProcessID가 존재하지 않을 경우 공정, 설비, 작업자 비운다.
                {
                    lblProcess.Text = "";
                    lblProcess.Tag = "";
                    lblMachine.Text = "";
                    lblMachine.Tag = "";
                    lblPerson.Text = "";
                    lblPerson.Tag = "";
                }
            }
            lblArticle.Text = "";
            lblArticle.Tag = "";
            lblOrderArticleID.Text = "";
        }

        private void SetScreen()
        {
            tlpForm.Dock = DockStyle.Fill;
            tlpFill.SetColumnSpan(tlpComment, 3);
            foreach (Control control in tlpForm.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control contro in control.Controls)
                {
                    contro.Dock = DockStyle.Fill;
                    contro.Margin = new Padding(0, 0, 0, 0);
                    foreach (Control contr in contro.Controls)
                    {
                        contr.Dock = DockStyle.Fill;
                        contr.Margin = new Padding(0, 0, 0, 0);
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(0, 0, 0, 0);
                            foreach (Control con in cont.Controls)
                            {
                                con.Dock = DockStyle.Fill;
                                con.Margin = new Padding(0, 0, 0, 0);
                                foreach (Control co in con.Controls)
                                {
                                    co.Dock = DockStyle.Fill;
                                    co.Margin = new Padding(0, 0, 0, 0);
                                    foreach (Control ctl in co.Controls)
                                    {
                                        ctl.Dock = DockStyle.Fill;
                                        ctl.Margin = new Padding(0, 0, 0, 0);
                                        foreach (Control ct in ctl.Controls)
                                        {
                                            ct.Dock = DockStyle.Fill;
                                            ct.Margin = new Padding(0, 0, 0, 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool CheckData()
        {
            try
            {
                if (lblProcess.Tag.ToString() == "")
                {
                    throw new Exception("공정이");
                }
                if (lblMachine.Tag.ToString() == "")
                {
                    throw new Exception("설비가");
                }
                if (lblPerson.Tag.ToString() == "")
                {
                    throw new Exception("작업자가");
                }
                if (lblArticle.Tag.ToString() == "")
                {
                    throw new Exception("품목코드");
                }

                try
                {
                    //[xp_WizWork_sProMacPer]
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("ProcessID",   lblProcess.Tag.ToString());
                    sqlParameter.Add("MachineID",   lblMachine.Tag.ToString());
                    sqlParameter.Add("PersonID",    lblPerson.Tag.ToString());
                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sProMacPer", sqlParameter, false);
                    if (dt.Rows.Count != 1)
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("공정, 설비, 작업자 중 데이터가 올바르지 않습니다. 다시 입력해주세요.", "[오류]", 0, 1);
                        lblProcess.Text = "";
                        lblProcess.Tag = "";
                        lblMachine.Text = "";
                        lblMachine.Tag = "";
                        lblPerson.Text = "";
                        lblPerson.Tag = "";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("{0} 입력되지 않았습니다. {0}를 확인해주세요.", ex.Message), "[오류]", 0, 1);
                return false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckData())
                {
                    return;
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //sqlParameter.Add("WorkAlarmID", "");
                sqlParameter.Add("ProcessID", lblProcess.Tag.ToString());
                sqlParameter.Add("MachineID", lblMachine.Tag.ToString());
                sqlParameter.Add("ArticleID", lblArticle.Tag.ToString());
                sqlParameter.Add("PLotID", txtInstID.Text.Trim());
                sqlParameter.Add("ReqStus", "R");//요청 : R ,취소 : C, 완료 : F
                //if (rbnReq.Checked)
                //{
                //    sqlParameter.Add("ReqStus", "R");//요청 : R ,취소 : C, 완료 : F
                //}
                //else if(rbnCnc.Checked)
                //{
                //    sqlParameter.Add("ReqStus", "C");//요청 : R ,취소 : C, 완료 : F
                //}
                //sqlParameter.Add("AlarmStartDate", strSeq);
                //sqlParameter.Add("AlarmStartTime", strSeq);
                sqlParameter.Add("Comments", txtComment.Text.Trim());
                sqlParameter.Add("CreateUserID", lblPerson.Tag.ToString());

                string[] Confirm = new string[2];
                Confirm = DataStore.Instance.ExecuteProcedure("[xp_WizWork_iwkWorkAlarmMtr]", sqlParameter, false);

                if (Confirm[0].ToLower() == "success")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("정상적으로 재단품을 요청하였습니다.", "[자재요청성공]", 2, 1);
                    DialogResult = DialogResult.OK;
                    //if (rbnReq.Checked)
                    //{
                    //    WizCommon.Popup.MyMessageBox.ShowBox("정상적으로 재단품을 요청하였습니다.", "[자재요청성공]", 2, 1);
                    //}
                    //else if (rbnCnc.Checked)
                    //{
                    //    WizCommon.Popup.MyMessageBox.ShowBox("정상적으로 재단품 요청을 취소하였습니다.", "[자재요청 취소성공]", 2, 1);
                    //}
                    this.Dispose();
                    this.Close();
                }
                else
                {
                    throw new Exception(Confirm[1]);
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        private void FillComboBox()
        {
            DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sWorkAlarmMtrComment]", null, false);
            DataRow newRow = dt.NewRow();
            newRow["Code_ID"] = "*";
            newRow["Code_Name"] = "";
            dt.Rows.InsertAt(newRow, 0);
            cboComment.DataSource = dt;
            cboComment.ValueMember = "Code_ID";
            cboComment.DisplayMember = "Code_Name";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnS_ArticleID_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel_sArticle_O fps = new Frm_PopUpSel_sArticle_O();
            fps.WriteTextEvent += new Frm_PopUpSel_sArticle_O.TextEventHandler(GetData);
            fps.Owner = this;
            fps.Show();

            void GetData(string sArticleID, string sArticle, string sOrderArticleID)
            {
                lblArticle.Text = sArticle;
                lblArticle.Tag = sArticleID;
                lblOrderArticleID.Text = sOrderArticleID;
            }
        }

        private void btnS_LotLabel_Click(object sender, EventArgs e)
        {
            //프로시저에 PLOTID / LabelID
            FK = new WizCommon.Popup.Frm_CMKeypad("", btnS_LotLabel.Text.Trim());
            if (FK.ShowDialog() == DialogResult.OK)
            {
                FillBox(FK.tbInputText.Text.Trim());
            }
        }

        private void GetPLotID()
        {
            string[] sPLotID = new string[2];
            string Query = "select LotID from pl_InputDet where ProcessID = '" + Frm_tprc_Main.g_tBase.ProcessID + "'" + "and InstID = '" + Frm_tprc_Main.g_tBase.sInstID + "'" ;
            sPLotID = DataStore.Instance.ExecuteQuery(Query, false);
            if (sPLotID[0].ToLower() == "success")
            {
                FillBox(sPLotID[1]);
            }
        }

        private void FillBox(string _strLotID)
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                if ((_strLotID.ToUpper().Contains("PL") && (_strLotID.Length == 15 || _strLotID.Length == 16)))
                {
                    sqlParameter.Add("@LotID", _strLotID);
                }
                else if ((_strLotID.ToUpper().Contains("T") || _strLotID.ToUpper().Contains("C")) && (_strLotID.Length == 10))
                {
                    sqlParameter.Add("@LabelID", _strLotID);
                }
                else
                {
                    Message[0] = "[올바르지 않은 LotID / 이동전표번호]";
                    Message[1] = "잘못된 LotID / 이동전표번호입니다.\r\n다시 확인 후 입력해주세요.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    txtInstID.Text = "";
                    txtInstID.Select();
                    txtInstID.Focus();
                    return;
                }
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sArticleIDByLotLabelID]", sqlParameter, false);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    lblArticle.Text = Lib.CheckNull(dr["Article"].ToString());
                    lblArticle.Tag = Lib.CheckNull(dr["ArticleID"].ToString());
                    lblOrderArticleID.Text = Lib.CheckNull(dr["OrderArticleID"].ToString());
                    //lblInstID.Text = Lib.CheckNull(dr["PLotID"].ToString());
                    txtInstID.Tag = _strLotID;
                    txtInstID.Text = "";
                    txtInstID.Select();
                    txtInstID.Focus();
                }
                else
                {
                    Message[0] = "[검색결과]";
                    Message[1] = "검색결과가 없습니다. 올바르지 않은 LotID / 이동전표번호입니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    txtInstID.Text = "";
                    txtInstID.Select();
                    txtInstID.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                txtInstID.Text = "";
                txtInstID.Select();
                txtInstID.Focus();
            }
            
        }
        private void LoadCalendar()
        {
            WizCommon.Popup.Frm_tins_Calendar calendar = new WizCommon.Popup.Frm_tins_Calendar(mtb_Date.Text.Replace("-", ""), mtb_Date.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_tins_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
            //Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
            void GetDate(string strDate, string btnName)
            {
                if (strDate.Length == 8)
                {
                    strDate = strDate.Substring(2, 6);
                }
                DateTime dateTime = new DateTime();
                dateTime = DateTime.ParseExact(strDate, "yyMMdd", null);
                mtb_Date.Text = dateTime.ToString("yyMMdd");
            }
        }

        private void btnCal_Date_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        private void mtb_Date_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        private void lblPerson_Click(object sender, EventArgs e)
        {
            GetPerson();
        }
        private void GetPerson()
        {
            Frm_PopUpSel_sPerson PopSel1 = new Frm_PopUpSel_sPerson(lblProcess.Tag.ToString());
            PopSel1.WriteTextEvent += new Frm_PopUpSel_sPerson.TextEventHandler(GetData);
            PopSel1.Owner = this;
            
            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == "Frm_PopUpSel_sPerson")
                {
                    openForm.Activate();
                    return;
                }
                else
                {
                    PopSel1.Show();
                    break;
                }
            }
            void GetData()
            {
                lblPerson.Text = Frm_tprc_Main.g_tBase.Person;
                lblPerson.Tag = Frm_tprc_Main.g_tBase.PersonID;
            }
        }

        private void lblPerson__Click(object sender, EventArgs e)
        {
            GetPerson();
        }

        private void GetMachine()
        {
            Frm_PopUpSel_sMachine PopSel1 = new Frm_PopUpSel_sMachine(lblProcess.Tag.ToString());
            PopSel1.WriteTextEvent += new Frm_PopUpSel_sMachine.TextEventHandler(GetData);
            PopSel1.Owner = this;
            
            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == "Frm_PopUpSel_sMachine")
                {
                    openForm.Activate();
                    return;
                }
                else
                {
                    PopSel1.Show();
                    break;
                }
            }
            void GetData()
            {
                lblMachine.Text = Frm_tprc_Main.g_tBase.Machine;
                lblMachine.Tag = Frm_tprc_Main.g_tBase.MachineID;
            }
        }

        private void lblMachine__Click(object sender, EventArgs e)
        {
            GetMachine();
        }

        private void lblMachine_Click(object sender, EventArgs e)
        {
            GetMachine();
        }

        private void lblProcess__Click(object sender, EventArgs e)
        {
            GetProcess();
        }

        private void lblProcess_Click(object sender, EventArgs e)
        {
            GetProcess();
        }
        private void GetProcess()
        {

            Frm_PopUpSel_sProcess PopSel1 = new Frm_PopUpSel_sProcess();
            PopSel1.WriteTextEvent += new Frm_PopUpSel_sProcess.TextEventHandler(GetData);
            PopSel1.Owner = this;
            
            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == "Frm_PopUpSel_sProcess")
                {
                    openForm.Activate();
                    return;
                }
                else
                {
                    PopSel1.Show();
                    break;
                }
            }
            void GetData(string sProcessID, string sProcess)
            {
                lblProcess.Text = sProcess; 
                lblProcess.Tag = sProcessID;
            }
        }

        private void txtComment_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process ps = new System.Diagnostics.Process();
            ps.StartInfo.FileName = "osk.exe";
            txtComment.Select();
            txtComment.Focus();
            ps.Start();
        }

        private void DataClear()
        {
            rbnReq.Checked = true;
            txtInstID.Text = string.Empty;
            lblProcess.Text = string.Empty;
            lblMachine.Text = string.Empty;
            lblPerson.Text = string.Empty;
            lblArticle.Text = string.Empty;
            lblOrderArticleID.Text = string.Empty;
            txtComment.Text = string.Empty;
            mtb_Date.Text = DateTime.Now.ToString("yyMMdd");
        }

        private void cboComment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboComment.Items.Count > 0)
            {
                if (cboComment.SelectedIndex > 0)
                {
                    txtComment.Text = cboComment.GetItemText(cboComment.SelectedItem);
                    cboComment.SelectedIndex = 0;
                }
            }
        }

        private void txtInstID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                FillBox(txtInstID.Text.Trim());
            }
        }
    }
}

