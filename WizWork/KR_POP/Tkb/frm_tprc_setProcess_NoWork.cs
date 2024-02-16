using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Properties;
using WizCommon;

namespace WizWork
{
    public partial class frm_tprc_setProcess_NoWork : Form
    {
        private string m_prodLotID = "";
        private string m_MachineID = "";
        private string m_ProcessID = "";
        private string m_InstID = "";

        private string m_ReqType = "";

        //WorkU로 전달할 변수들
        public string sProcess = string.Empty;          //선택한 공정ID
        string sMachine = string.Empty;                 //선택한 머신
        string sMachineName = string.Empty;             //선택한 머신NAME
        string sProcessName = string.Empty;             //선택한 공정NAME
        string sPersonID = string.Empty;                //선택한 사용자ID
        string sPersonName = string.Empty;              //선택한 사용자NAME
        string sTeamID = string.Empty;                    //선택한 TEAM
        string sUserID = string.Empty;                  //선택한 UserID


        private DataSet ds1 = null;
        private DataSet ds2 = null;
        private DataSet ds3 = null;

        System.Windows.Forms.RadioButton[] rBtnProcess = null;
        System.Windows.Forms.RadioButton[] rBtnTeams = null;


        string g_TeamID = string.Empty;                 //선택된 TEAM ID 변수
        string g_TeamName = string.Empty;               //선택된 TEAM 이름 변수

        string g_MachineByProcessID = string.Empty;     //선택한 공정ID
        string g_MachineByProcessName = string.Empty;   //선택한 공정명

        string g_ProcessID = string.Empty;              // Wizard.ini에서 가져온 ProcessID들 ex)0401|1101|5101

        Tools.INI_GS gs = new Tools.INI_GS();//getvalue,setvalue 가지고 있는 클래스


        private int rBtnProcessRowCnt = 0;
        private int rBtnTeamsRowCnt = 0;

        public frm_tprc_setProcess_NoWork()
        {
            InitializeComponent();
        }

        public frm_tprc_setProcess_NoWork(string strReqType)
        {
            InitializeComponent();
            m_ReqType = strReqType;
        }



        public frm_tprc_setProcess_NoWork(string strprodLotID, string strpMachineID, string strProcessID, string strInstID)
        {
            InitializeComponent();

            m_prodLotID = strprodLotID;
            m_MachineID = strpMachineID;
            m_ProcessID = strProcessID;
            m_InstID = strInstID;
        }

        private void frm_tprc_setProcess_NoWork_Load(object sender, EventArgs e)
        {
            bool chkrBtnProcess = false;

            bool chkrBtnTeams = false;
            string strTemId = "";

            strTemId = Frm_tprc_Main.g_tBase.TeamID;
            SetTeamRBtn();

            SetProcessRBtn();


            /// 초기 공정 설정

            try
            {

                for (int i = 0; i < rBtnProcessRowCnt; i++)
                {
                    if (rBtnProcess[i].Tag.ToString() == m_ProcessID)
                    {
                        rBtnProcess[i].Checked = true;
                        chkrBtnProcess = true;

                    }
                }
                if (chkrBtnProcess == false)
                {
                    rBtnProcess[0].Checked = true;
                }
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }

            /// 작업 조 설정
            try
            {

                for (int i = 0; i < rBtnTeamsRowCnt; i++)
                {
                    if (rBtnTeams[i].Tag.ToString() == strTemId)
                    {
                        rBtnTeams[i].Checked = true;
                        chkrBtnTeams = true;

                    }
                }
                if (chkrBtnTeams == false)
                {
                    rBtnTeams[0].Checked = true;
                }
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }


            if (m_MachineID != "")
            {
                this.cboMachine.SelectedValue = m_MachineID;
            }

            Tab_Person.SelectedIndex = 1;//전체사용자명단 조회
            Tab_Person.SelectedIndex = 0;//공정별사용자명단 조회 

            splPersonID.Text = Frm_tprc_Main.g_tBase.PersonID;
            splPersonName.Text = Frm_tprc_Main.g_tBase.Person;

        }

        //Team조회 후 라디오버튼에 데이터(Team)바인딩
        private void SetTeamRBtn()
        {
            try
            {
                ds2 = DataStore.Instance.ProcedureToDataSet("xp_Code_sTeamID", null, false);

                string[] Team = new string[ds2.Tables[0].Rows.Count];
                string[] TeamID = new string[ds2.Tables[0].Rows.Count];
                this.rBtnTeams = new RadioButton[ds2.Tables[0].Rows.Count];
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds2.Tables[0].Rows[i];
                    Team[i] = dr[Code_sTeam.TEAM].ToString();
                    TeamID[i] = dr[Code_sTeam.TEAMID].ToString();
                    RadioButton rBtnTeams = new RadioButton();
                    rBtnTeams.Text = Team[i];
                    rBtnTeams.Appearance = System.Windows.Forms.Appearance.Button;
                    rBtnTeams.BackColor = System.Drawing.Color.LightSkyBlue;
                    rBtnTeams.Checked = false;
                    rBtnTeams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    rBtnTeams.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
                    rBtnTeams.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
                    rBtnTeams.ForeColor = System.Drawing.Color.White;
                    rBtnTeams.Font = new System.Drawing.Font("맑은 고딕", 35F, System.Drawing.FontStyle.Bold);
                    rBtnTeams.Size = new System.Drawing.Size(180, 78);
                    rBtnTeams.TabStop = true;
                    rBtnTeams.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    rBtnTeams.UseVisualStyleBackColor = false;
                    rBtnTeams.CheckedChanged += new EventHandler(rBtnTeams_CheckedChanged);
                    rBtnTeams.Tag = TeamID[i];
                    if (ds2.Tables[0].Rows.Count < 5) //PROCESS 개수가 5개이하일때의 버튼위치
                    {
                        rBtnTeams.Location = new System.Drawing.Point(0, i * 80 + 3);
                    }
                    pnlTeam.Controls.Add(rBtnTeams);
                    this.rBtnTeams[i] = rBtnTeams;
                    rBtnTeamsRowCnt = ds2.Tables[0].Rows.Count;
                }
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }
        }
        //작업조가 변경될 때 작업조ID값을 변수에 담아둠
        private void rBtnTeams_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radiobutton = sender as RadioButton;
            g_TeamID = radiobutton.Tag.ToString();
            g_TeamName = radiobutton.Text.ToString();
        }
        //라디오버튼(공정) 데이터바인딩
        private void SetProcessRBtn()
        {
            procProcess_Q();

            string[] Process = new string[ds1.Tables[0].Rows.Count];
            string[] ProcessID = new string[ds1.Tables[0].Rows.Count];
            this.rBtnProcess = new RadioButton[ds1.Tables[0].Rows.Count];
            try
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds1.Tables[0].Rows[i];
                    Process[i] = dr[Work_sProcess.PROCESS].ToString();
                    ProcessID[i] = dr[Work_sProcess.PROCESSID].ToString();
                    RadioButton rBtnProcess = new RadioButton();
                    rBtnProcess.Text = Process[i];
                    rBtnProcess.Appearance = System.Windows.Forms.Appearance.Button;
                    rBtnProcess.BackColor = System.Drawing.Color.LightSkyBlue;
                    rBtnProcess.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
                    rBtnProcess.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
                    rBtnProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    rBtnProcess.ForeColor = System.Drawing.Color.White;
                    if (rBtnProcess.Text.Length > 3)
                    {
                        rBtnProcess.Font = new System.Drawing.Font("맑은 고딕", 27F, System.Drawing.FontStyle.Bold);
                    }
                    else if (rBtnProcess.Text.Length == 3)
                    {
                        rBtnProcess.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold);
                    }
                    else
                    {
                        rBtnProcess.Font = new System.Drawing.Font("맑은 고딕", 35F, System.Drawing.FontStyle.Bold);
                    }
                    rBtnProcess.TabStop = true;
                    rBtnProcess.Tag = ProcessID[i];
                    if (rBtnProcess.Tag == ProcessID)
                    { rBtnProcess.Checked = true; }
                    else { rBtnProcess.Checked = false; }
                    rBtnProcess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    rBtnProcess.UseVisualStyleBackColor = false;
                    rBtnProcess.CheckedChanged += new EventHandler(rBtnProcess_CheckedChanged);
                    //rBtnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    //        | System.Windows.Forms.AnchorStyles.Left)
                    //        | System.Windows.Forms.AnchorStyles.Right)));

                    if (ds1.Tables[0].Rows.Count < 6) //PROCESS 개수가 5개이하일때의 버튼위치
                    {
                        rBtnProcess.Size = new System.Drawing.Size(129, 162);
                        rBtnProcess.Location = new System.Drawing.Point(135 * i, 0);
                    }
                    else if (ds1.Tables[0].Rows.Count < 11)//PROCESS 개수가 6개이상 10개 이하일때의 버튼위치
                    {
                        rBtnProcess.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
                        rBtnProcess.Size = new System.Drawing.Size(129, 78);
                        if (i < 5)
                        {
                            rBtnProcess.Location = new System.Drawing.Point(135 * i, 0);
                        }
                        else
                        {
                            rBtnProcess.Location = new System.Drawing.Point(135 * (i - 5), 84);
                        }
                    }
                    else if (ds1.Tables[0].Rows.Count < 16)//PROCESS 개수가 10개이상 15개 이하일때의 버튼위치
                    {
                        rBtnProcess.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
                        rBtnProcess.Size = new System.Drawing.Size(129, 52);
                        if (i < 5)
                        {
                            rBtnProcess.Location = new System.Drawing.Point(135 * i, 0);
                        }
                        else if (i < 10)
                        {
                            rBtnProcess.Location = new System.Drawing.Point(135 * (i - 5), 55);
                        }
                        else if (i < 15)
                        {
                            rBtnProcess.Location = new System.Drawing.Point(135 * (i - 10), 110);
                        }
                    }
                    pnlProcess.Controls.Add(rBtnProcess);
                    this.rBtnProcess[i] = rBtnProcess;
                    rBtnProcessRowCnt = ds1.Tables[0].Rows.Count;
                }
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }
        }

        //ini파일에 등록되있는 ProcessID로 Process 조회하여 ds1 셋팅
        private void procProcess_Q()
        {
            g_ProcessID =Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID");
            string[] gubunProcess = g_ProcessID.Split(new char[] { '|' });
            try
            {
                g_ProcessID = string.Empty;


                for (int i = 0; i < gubunProcess.Length; i++)
                {

                    if (g_ProcessID == string.Empty)
                    {
                        g_ProcessID = gubunProcess[i];
                    }
                    else
                    {
                        g_ProcessID = g_ProcessID + "|" + gubunProcess[i];
                    }
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Work_sProcess.NCHKPROC, "1");
                sqlParameter.Add(Work_sProcess.PROCESSID, g_ProcessID);
                ds1 = DataStore.Instance.ProcedureToDataSet("xp_Work_sProcess", sqlParameter, false);
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }
        }

        //공정을 선택할때마다 공정별 사원명단의 그리드뷰를 공정 기준으로 사용자 검색하는 기능
        void rBtnProcess_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radiobutton = sender as RadioButton;
            if (radiobutton.Checked == true)
            {
                g_MachineByProcessID = radiobutton.Tag.ToString();
                g_MachineByProcessName = radiobutton.Text.ToString();
                procMachine_Q();
                SetDgvPerson();
            }
        }
        //공정별 설비 조회
        private void procMachine_Q()
        {
            try
            {

                ds3 = null;

                try
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add(Work_sMachineByProcess.SPROCESSID, g_MachineByProcessID);

                    ds3 = DataStore.Instance.ProcedureToDataSet("xp_Process_sMachine", sqlParameter, false);
                    cboMachine.ResetText();

                    //DataRow newRow = ds3.Tables[0].NewRow();
                    //newRow[Work_sMachineByProcess.MACHINEID] = "";
                    //newRow[Work_sMachineByProcess.MACHINENO] = "전체";
                    //ds3.Tables[0].Rows.InsertAt(newRow, 0);

                    if (ds3 != null && ds3.Tables.Count > 0)
                    {
                        cboMachine.DataSource = ds3.Tables[0];
                    }
                    cboMachine.ValueMember = Work_sMachineByProcess.MACHINEID;
                    cboMachine.DisplayMember = Work_sMachineByProcess.MACHINENO;

                }
                catch (Exception excpt)
                {
                    MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
                }
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }
        }
        //사용자 셋팅
        private void SetDgvPerson()
        {
            DataGridViewRow row = null;
            //공정별 사용자 셋팅
            if (Tab_Person.SelectedIndex == 0)
            {
                dgvPersonByProcess.Rows.Clear();//행 초기화
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Person_sPersonByProcess.CHKPROCESS, "1");
                sqlParameter.Add(Person_sPersonByProcess.PROCESSID, g_MachineByProcessID);
                //sqlParameter.Add(Person_sPersonByProcess.CHKTEAM, "1");xp_Person_sPersonByProcess프로시저에서 teamid 체크추가시 사용
                //sqlParameter.Add(Person_sPersonByProcess.TEAMID, Team);xp_Person_sPersonByProcess프로시저에서 teamid 체크추가시 사용
                ds2 = DataStore.Instance.ProcedureToDataSet("xp_Person_sPersonByProcess", sqlParameter, false);
                DataStore.Instance.CloseConnection();
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds2.Tables[0].Rows[i];

                        dgvPersonByProcess.Rows.Add(
                            dr[Person_sPersonByProcess.PROCESS],            //공정명
                            dr[Person_sPersonByProcess.PERSONID],           //사원코드
                            dr[Person_sPersonByProcess.NAME],               //사원명 
                            dr[Person_sPersonByProcess.TEAM],                //작업조
                            dr[Person_sPersonByProcess.USERID]              //사원ID
                        );

                        row = dgvPersonByProcess.Rows[i];
                        row.Height = 28;
                    }
                    dgvPersonByProcess.ClearSelection();
                    dgvPersonByProcess[0, 0].Selected = true;
                }
            }
            //전체 사용자 셋팅
            else if (Tab_Person.SelectedIndex == 1)
            {
                dgvAllPerson.Rows.Clear();//행 초기화
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Person_sPersonByProcess.CHKPROCESS, "0");
                sqlParameter.Add(Person_sPersonByProcess.PROCESSID, "");
                //sqlParameter.Add(Person_sPersonByProcess.CHKTEAM, "0");xp_Person_sPersonByProcess프로시저에서 teamid 체크추가시 사용
                //sqlParameter.Add(Person_sPersonByProcess.TEAMID, Team);xp_Person_sPersonByProcess프로시저에서 teamid 체크추가시 사용
                ds2 = DataStore.Instance.ProcedureToDataSet("xp_Person_sPersonByProcess", sqlParameter, false);
                DataStore.Instance.CloseConnection();
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds2.Tables[0].Rows[i];

                        dgvAllPerson.Rows.Add(
                            dr[Person_sPersonByProcess.PROCESS],            //공정명
                            dr[Person_sPersonByProcess.PERSONID],           //사원코드
                            dr[Person_sPersonByProcess.NAME],               //사원명 
                            dr[Person_sPersonByProcess.USERID]              //사원ID
                                                                            //dr[Person_sPersonByProcess.TEAM]                //작업조
                        );
                    }
                    dgvAllPerson.ClearSelection();
                    dgvAllPerson[0, 0].Selected = true;
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            splPersonID.Text = "";
            splPersonName.Text = "";
            Tab_Person.SelectedIndex = 1;
            dgvAllPerson.ClearSelection();
        }

        private void cboMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMachine.SelectedIndex == 0)
            {
                lblChkMachine.Visible = true;
            }
            else
            {
                lblChkMachine.Visible = false;
            }
        }

        private void cmdRowUp_Click(object sender, EventArgs e)
        {
            int iSelRow = 0;
            if (Tab_Person.SelectedIndex == 0)
            {
                for (int i = 0; i < dgvPersonByProcess.SelectedCells.Count; i++)
                {
                    if (iSelRow > 0)
                    {
                        dgvPersonByProcess[0, iSelRow - 1].Selected = true;
                        break;
                    }
                }
            }
            else if (Tab_Person.SelectedIndex == 1)
            {
                if (dgvAllPerson.DisplayedRowCount(false) != 1)
                {
                    for (int i = 0; i < dgvAllPerson.SelectedCells.Count; i++)
                    {
                        iSelRow = dgvAllPerson.SelectedCells[i].RowIndex;
                        if (iSelRow > 0)
                        {
                            dgvAllPerson[0, iSelRow - 1].Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            int iSelRow = 0;
            if (Tab_Person.SelectedIndex == 0)
            {
                for (int i = 0; i < dgvPersonByProcess.SelectedCells.Count; i++)
                {
                    iSelRow = dgvPersonByProcess.SelectedCells[i].RowIndex;
                    if (iSelRow < dgvPersonByProcess.Rows.Count - 1)
                    {
                        dgvPersonByProcess[0, iSelRow + 1].Selected = true;
                        break;
                    }
                }
            }
            else if (Tab_Person.SelectedIndex == 1)
            {
                if (dgvAllPerson.DisplayedRowCount(false) != 1)
                {
                    for (int i = 0; i < dgvAllPerson.SelectedCells.Count; i++)
                    {
                        iSelRow = dgvAllPerson.SelectedCells[i].RowIndex;
                        if (iSelRow < dgvAllPerson.Rows.Count - 1)
                        {
                            dgvAllPerson[0, iSelRow + 1].Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            string strNowPersonID = "";
            string strNowProcessID = "";
            string strNowTeamID = "";

            sProcess = g_MachineByProcessID;
            sProcessName = g_MachineByProcessName;//나중에 MDI폼 밑단에 표시할 공정이름, 추가적으로 머신이름도 필요할듯 머신이름 변수 필요함
            sTeamID = g_TeamID;

            sMachineName = cboMachine.Text.ToString();

            try
            {
                strNowProcessID = Frm_tprc_Main.g_tBase.ProcessID;
                strNowPersonID = Frm_tprc_Main.g_tBase.PersonID;
                strNowTeamID = Frm_tprc_Main.g_tBase.TeamID;
                sMachine = Frm_tprc_Main.g_tBase.MachineID;

                if (strNowProcessID == "")
                {
                    MessageBox.Show("선택된 공정이 없습니다");
                    return;
                }

                if (strNowPersonID == "")
                {
                    MessageBox.Show("선택된 작업자가 없습니다");
                    return;
                }
                if (strNowTeamID == "")
                {
                    MessageBox.Show("선택된 작업조가 없습니다");
                    return;
                }
                //Frm_tprc_NoWork_U.pl_ProcessID = strNowProcessID;
                //Frm_tprc_NoWork_U.pl_InstID = m_InstID;
                //Frm_tprc_NoWork_U.set_sProcess = sProcess;
                //Frm_tprc_NoWork_U.set_sTeam = Frm_tprc_Main.g_tBase.TeamID;
                //Frm_tprc_NoWork_U.set_sMachine = sMachine;
                //Frm_tprc_NoWork_U.set_sPersonID = strNowPersonID;
                //Frm_tprc_NoWork_U.set_sUserID = strNowPersonID;

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("공정별 작업조 및 작업자를 선택하시기 바랍니다r\n{0}", ex.Message));
                return;
            }
            this.Close();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDgvPerson();//탭컨트롤 인덱스에 따라 공정/전체 사용자 조회
        }

        private void cmdPersonID_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad.g_Name = "사원번호";
            POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();
            numkeypad.Owner = this;

            POPUP.Frm_CMNumericKeypad.KeypadStr = splPersonID.Text;
            if (numkeypad.ShowDialog() == DialogResult.OK)
            {
                splPersonID.Text = numkeypad.tbInputText.Text;

                if (splPersonID.Text.Length == 8)
                {
                    Tab_Person.SelectedIndex = 1;//전체사원명단 탭으로 이동
                    //visibleTF(); //검색기능 | 검색해서 사원번호가 동일한 애 찾으면 동일한 애만 Visible=true 나머지는 Visible=false
                    int i = 0;
                    //검색 동일한 애를 찾기 있으면 Select 없으면 없는 사원번호라고 메시지박스
                    for (int n = 0; n < dgvAllPerson.Rows.Count; n++)
                    {
                        if (splPersonID.Text == dgvAllPerson.Rows[n].Cells[1].Value.ToString())//사원코드입력받은값 == 그리드뷰 값
                        {
                            dgvAllPerson.Rows[n].Selected = true;
                            this.splPersonName.Text = dgvAllPerson.CurrentRow.Cells[2].Value.ToString();
                            break;
                        }
                        else
                        {
                            i = i + 1;//사용자의입력값과 현재 행의 사원코드가 동일한 값이 아닐 때 변수에 1씩 추가해줌.
                        }
                    }
                    if (i == dgvAllPerson.Rows.Count)//전체사용자의 수와 i 변수가 같을 때 동일한 값이 없는것
                    {
                        AutoClosingMessageBox.Show("동일한 사원번호를 찾을수 없습니다. - '" + splPersonID.Text + "'", "검색된 사원번호가 없습니다", 1500);
                        splPersonID.Text = string.Empty;
                    }
                }
                else
                {
                    Console.WriteLine(splPersonID.Text.Length.ToString());
                    AutoClosingMessageBox.Show("잘못된 사원번호입니다(사원번호 8자리 오류) - '" + splPersonID.Text + "'", "사원번호 길이가 맞지 않습니다", 1500);
                    splPersonID.Text = string.Empty;
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            sProcess = g_MachineByProcessID;
            sProcessName = g_MachineByProcessName;//나중에 MDI폼 밑단에 표시할 공정이름, 추가적으로 머신이름도 필요할듯 머신이름 변수 필요함
            sTeamID = g_TeamID;
            sMachine = cboMachine.SelectedValue.ToString();
            sMachineName = cboMachine.Text.ToString();
            if (Tab_Person.SelectedIndex == 0 && dgvPersonByProcess.CurrentRow != null)
            //dgvPersonByProcess.CurrentRow.Cells[1].Value.ToString() != string.Empty && dgvPersonByProcess.CurrentRow.Cells[4].Value.ToString() != string.Empty)
            {
                sPersonID = dgvPersonByProcess.CurrentRow.Cells["PersonID_1"].Value.ToString();
                sPersonName = dgvPersonByProcess.CurrentRow.Cells["PersonName_1"].Value.ToString();
                sUserID = dgvPersonByProcess.CurrentRow.Cells["UserID_Pro"].Value.ToString();
            }
            else if (Tab_Person.SelectedIndex == 1 && dgvAllPerson.CurrentRow != null)
            {
                sPersonID = dgvAllPerson.CurrentRow.Cells["PersonID_2"].Value.ToString();
                sPersonName = dgvAllPerson.CurrentRow.Cells["PersonName_2"].Value.ToString();
                sUserID = dgvAllPerson.CurrentRow.Cells["UserID_All"].Value.ToString();
            }
            if (sProcess != string.Empty && sMachine != string.Empty && sPersonID != string.Empty && sUserID != string.Empty && sTeamID != string.Empty)
            {
                if (m_ReqType == "NoWork")
                {
                    ((WizWork.Frm_tprc_NoWork_U)(this.Owner)).Set_stbInfo(g_TeamID, g_TeamName, sPersonID, sPersonName, sMachine, sMachineName, "null", "null", g_MachineByProcessID, g_MachineByProcessName, m_InstID);


                    Frm_tprc_Main.g_tBase.sInstID = m_InstID;
                    Frm_tprc_Main.g_tBase.ProcessID = sProcess;
                    Frm_tprc_Main.g_tBase.Process = sProcessName;
                    Frm_tprc_Main.g_tBase.MachineID = sMachine;
                    Frm_tprc_Main.g_tBase.Machine = sMachineName;
                    Frm_tprc_Main.g_tBase.TeamID = m_ProcessID;
                    Frm_tprc_Main.g_tBase.Team = m_ProcessID;
                    Frm_tprc_Main.g_tBase.PersonID = sPersonID;

                    //Frm_tprc_NoWork_U.pl_ProcessID = m_ProcessID;
                    //Frm_tprc_NoWork_U.pl_InstID = m_InstID;
                    //Frm_tprc_NoWork_U.set_sProcess = sProcess;
                    //Frm_tprc_NoWork_U.set_sTeam = sTeamID;
                    //Frm_tprc_NoWork_U.set_sMachine = sMachine;
                    //Frm_tprc_NoWork_U.set_sPersonID = sPersonID;
                    //Frm_tprc_NoWork_U.set_sUserID = sUserID;

                    Close();
                }

            }
            else
            {
                if (sProcess == string.Empty)
                { AutoClosingMessageBox.Show("선택된 공정이 없습니다.", "Process Is Empty", 1500); }
                if (sMachine == string.Empty) { AutoClosingMessageBox.Show("선택된 설비가 없습니다.", "Machine Is Empty", 1500); lblChkMachine.Visible = true; }
                if (sPersonID == string.Empty && sUserID == string.Empty)
                { AutoClosingMessageBox.Show("선택된 작업자가 없습니다.", "User Is Empty", 1500); }
                if (sTeamID == string.Empty)
                { AutoClosingMessageBox.Show("선택된 작업조가 없습니다.", "Team Is Empty", 1500); }
            }
        }


        private string GetProcessAutoGatheringYN(string strProcessID)
        {
            string strResult = "N";
            DataSet ds = null;
            DataRow dr = null;
            try
            {
                ds = null;

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("nchkProc", 1);
                sqlParameter.Add("ProcessID", strProcessID);           //생산할 공정 =======setProcess 화면에서 변수값 받아와서 선택할것 수정요망

                ds = DataStore.Instance.ProcedureToDataSet("xp_Work_sProcess", sqlParameter, false);
                DataStore.Instance.CloseConnection();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = null;
                    dr = ds.Tables[0].Rows[0];
                    strResult = dr["AutoGatheringYN"].ToString();
                }
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
                strResult = "N";
            }
            return strResult;
        }

    }
}
