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
    public partial class frm_tprc_setProcess : Form
    {
        int intHorizontal = 0;
        int intVertical = 0;
        RadioButton[] newRadioButton = null;
        CheckBox[] newCheckBox = null;

        private string m_prodLotID = "";
        private string m_MachineID = "";
        private string m_ProcessID = "";
        private string m_InstID = "";

        private bool NoWork = false;

        //WorkU로 전달할 변수들
        public string sProcess = string.Empty;          //선택한 공정ID
        string sMachine = string.Empty;                 //선택한 머신
        string sMachineName = string.Empty;             //선택한 머신NAME
        string sProcessName = string.Empty;             //선택한 공정NAME
        string sPersonID = string.Empty;                //선택한 사용자ID
        string sPersonName = string.Empty;              //선택한 사용자NAME
        string sTeam = string.Empty;                    //선택한 TEAM
        string sUserID = string.Empty;                  //선택한 UserID


        private DataSet ds1 = null;
        private DataSet ds2 = null;


        RadioButton[] rBtnProcess = null;
        CheckBox[] rBtnTeams = null;
        RadioButton[] rBtnDayOrNight = null;


        string g_TeamID = string.Empty;                 //선택된 TEAM ID 변수
        string g_TeamName = string.Empty;               //선택된 TEAM 이름 변수

        string g_MachineByProcessID = string.Empty;     //선택한 공정ID
        string g_MachineByProcessName = string.Empty;   //선택한 공정명

        string g_ProcessID = string.Empty;              // Wizard.ini에서 가져온 ProcessID들 ex)0401|1101|5101

        Tools.INI_GS gs = new Tools.INI_GS();//getvalue,setvalue 가지고 있는 클래스

        private int rBtnProcessRowCnt = 0;
        private int rBtnTeamsRowCnt = 0;
        private int rBtnDayOrNightRowCnt = 0;

        public bool isTool = false;

        string[] Message = new string[2];
        WizWorkLib Lib = new WizWorkLib();

        // 작업조별 사원 리스트 검색
        string TeamID = "";

        public frm_tprc_setProcess()
        {
            InitializeComponent();
        }

        public frm_tprc_setProcess(bool blNoWork)
        {
            InitializeComponent();
            NoWork = blNoWork;
        }



        public frm_tprc_setProcess(string strprodLotID, string strpMachineID, string strProcessID, string strInstID)
        {
            InitializeComponent();

            m_prodLotID = strprodLotID;
            m_MachineID = strpMachineID;
            m_ProcessID = strProcessID;
            m_InstID = strInstID;
        }

        //2019.05.15 허윤구( AFT용 무작업 형태인데, 공정만 땡겨할 수 있도록 형태추가)
        public frm_tprc_setProcess(string strProcessID, string strpMachineID, bool blNoWork)
        {
            InitializeComponent();
            m_ProcessID = strProcessID;
            m_MachineID = strpMachineID;
            NoWork = blNoWork;
        }

        private void frm_tprc_setProcess_Load(object sender, EventArgs e)
        {
            bool chkrBtnProcess = false;
            bool chkrBtnTeams = false;
            bool chkrBtnDayOrNight = false;

            string strTemId = "";
            InitGrid();
            InitGrid2();
            SetScreen();
            strTemId = Frm_tprc_Main.g_tBase.TeamID;

            // 팀 구분 tlp 구분
            SetTeamRBtn();

            // 주/야 구분 tlp 구분
            SetDayOrNightRBtn();

            // 공정 구분 tlp 구분
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
                        break;
                    }
                }
                if (!chkrBtnProcess)
                {
                    rBtnProcess[0].Checked = true;
                }
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }

            /// 초기 주 / 야  설정
            try
            {

                // 야간 근무시각일 경우, 야간으로 기본세팅을 박아주어라.
                // 2020.04.13 > GLS 요청 건.
                bool Is_NightWorkTime = false;
                string NowTic = DateTime.Now.ToString("HHmmss");

                double D_NowTic = Lib.GetDouble(NowTic);
                if ((D_NowTic > 200000) || (D_NowTic < 70000))
                { Is_NightWorkTime = true; }

                for (int i = 0; i < rBtnDayOrNightRowCnt; i++)
                {
                    if (Is_NightWorkTime == true)
                    {
                        if (rBtnDayOrNight[i].Tag.ToString() == "02")
                        {
                            // 야간 으로 기본세팅 박기.
                            rBtnDayOrNight[i].Checked = true;
                            Frm_tprc_Main.g_tBase.DayOrNightID = rBtnDayOrNight[i].Tag.ToString();
                            Frm_tprc_Main.g_tBase.DayOrNight = rBtnDayOrNight[i].Text.ToString();
                            chkrBtnDayOrNight = true;
                            break;
                        }
                    }
                    else
                    if (true)
                    {
                        if (rBtnDayOrNight[i].Tag.ToString() == "01")
                        {
                            // 주간 으로 기본세팅 박기.
                            rBtnDayOrNight[i].Checked = true;
                            Frm_tprc_Main.g_tBase.DayOrNightID = rBtnDayOrNight[i].Tag.ToString();
                            Frm_tprc_Main.g_tBase.DayOrNight = rBtnDayOrNight[i].Text.ToString();
                            chkrBtnDayOrNight = true;
                            break;
                        }
                    }
                }
                if (!chkrBtnDayOrNight)
                {
                    rBtnDayOrNight[0].Checked = true;
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
                        TeamID = strTemId;
                        break;
                    }
                }
                if (!chkrBtnTeams)
                {
                    rBtnTeams[0].Checked = true;
                    TeamID = rBtnTeams[0].Tag.ToString();
                }
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }

            if (m_MachineID != "")
            {
                if (cboMachine.Items.Count > 0)
                {
                    bool FindMachine = false;
                    for (int j = 0; j < cboMachine.Items.Count; j++)
                    {
                        cboMachine.SelectedIndex = j;
                        if (m_MachineID == cboMachine.SelectedValue.ToString())
                        {
                            FindMachine = true;
                            break;
                        }
                    }
                    if (FindMachine == false)
                    {
                        cboMachine.SelectedIndex = 0;
                    }
                    else { FindMachine = false; }
                }
            }
            Tab_Person.SelectedIndex = 1;//전체사용자명단 조회
            Tab_Person.SelectedIndex = 0;//공정별사용자명단 조회 

            splPersonID.Text = Frm_tprc_Main.g_tBase.PersonID;
            splPersonName.Text = Frm_tprc_Main.g_tBase.Person;
        }


        private void setSelectedDayOrNight()
        { 

        }

        //Team조회 후 라디오버튼에 데이터(Team)바인딩
        private void SetTeamRBtn()
        {
            try
            {
                int Horizontal = 1;
                int Vertical = 1;
                ds2 = DataStore.Instance.ProcedureToDataSet("xp_Code_sTeam", null, false);
                Vertical = ds2.Tables[0].Rows.Count;
                SetLayout(Horizontal, Vertical, tlpTeam);  // ini 설정값으로 창 변경
                LoadDataTeam(Horizontal, Vertical);   // 데이터 불러오기

                //string[] Team = new string[ds2.Tables[0].Rows.Count];
                //string[] TeamID = new string[ds2.Tables[0].Rows.Count];
                //this.rBtnTeams = new RadioButton[ds2.Tables[0].Rows.Count];
                //for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                //{
                //    DataRo`w dr = ds2.Tables[0].Rows[i];
                //    Team[i] = dr[Code_sTeam.TEAM].ToString();
                //    TeamID[i] = dr[Code_sTeam.TEAMID].ToString();
                //    RadioButton rBtnTeams = new RadioButton();
                //    rBtnTeams.Text = Team[i];
                //    
                //    
                //    rBtnTeams.Checked = false;
                //    rBtnTeams.Appearance = System.Windows.Forms.Appearance.Button;
                //    rBtnTeams.BackColor = System.Drawing.Color.LightSkyBlue;
                //    rBtnTeams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                //    rBtnTeams.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
                //    rBtnTeams.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
                //    rBtnTeams.ForeColor = System.Drawing.Color.White;
                //    rBtnTeams.UseVisualStyleBackColor = false;

                //    rBtnTeams.Font = new System.Drawing.Font("맑은 고딕", 35F, System.Drawing.FontStyle.Bold);
                //    rBtnTeams.Size = new System.Drawing.Size(180, 78);
                //    rBtnTeams.TabStop = true;
                //    rBtnTeams.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                //    rBtnTeams.CheckedChanged += new EventHandler(rBtnTeams_CheckedChanged);
                //    rBtnTeams.Tag = TeamID[i];

                //    //pnlTeam.Controls.Add(rBtnTeams);
                //    this.rBtnTeams[i] = rBtnTeams;
                //    rBtnTeamsRowCnt = ds2.Tables[0].Rows.Count;
                //}
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        //주 / 야 구별자 조회 후 라디오버튼에 데이터(Team)바인딩
        private void SetDayOrNightRBtn()
        {
            try
            {
                int Horizontal = 1;
                int Vertical = 1;

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("CodeGbn", "DayOrNight");
                sqlParameter.Add("sRelation", "");

                ds2 = DataStore.Instance.ProcedureToDataSet("xp_Code_sCmCode", sqlParameter, false);
                Vertical = ds2.Tables[0].Rows.Count;

                SetLayout(Horizontal, Vertical, tlpDayOrNight);  // ini 설정값으로 창 변경
                LoadDataDayOrNight(Horizontal, Vertical);   // 데이터 불러오기                
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }


        //작업조가 변경될 때 작업조ID값을 변수에 담아둠
        private void rBtnTeams_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSender = sender as CheckBox;
            g_TeamID = chkSender.Tag.ToString();
            g_TeamName = chkSender.Text.ToString();
            TeamID = chkSender.Tag.ToString();

            SetDgvPerson();

            // 둘리
            // 지금 체크 된것을 제외한 나머지는 모두 체크 해제 시키기.
            if (chkSender.Checked == true)
            {
                foreach (CheckBox ck in newCheckBox)
                {
                    if (ck.Tag.ToString().Trim().Equals(chkSender.Tag.ToString().Trim()))
                    {
                        continue;
                    }

                    ck.Checked = false;
                }
            }
            // 2020.02.21 둘리 : 전체검색을 일단 빼고 작업하기 위해서, 체크해제가 안되도록!
            else
            {
                chkSender.Checked = true;
            }
        }
        //라디오버튼(공정) 데이터바인딩
        private void SetProcessRBtn()
        {
            procProcess_Q();

            string[] Process = new string[ds1.Tables[0].Rows.Count];
            string[] ProcessID = new string[ds1.Tables[0].Rows.Count];
            rBtnProcess = new RadioButton[ds1.Tables[0].Rows.Count];

            SetLayout(intHorizontal, intVertical, tlpProcess);  // ini 설정값으로 창 변경
            LoadDataProcess(intHorizontal, intVertical);   // 데이터 불러오기
        }

        //ini파일에 등록되있는 ProcessID로 Process 조회하여 ds1 셋팅
        private void procProcess_Q()
        {
            int ProcessCount = 0;
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
                ProcessCount = ds1.Tables[0].Rows.Count;

                if (ProcessCount < 6)
                {
                    intHorizontal = 5;
                    intVertical = 1;
                }
                else if (ProcessCount < 11)
                {
                    intHorizontal = 5;
                    intVertical = 2;
                }
                else if (ProcessCount < 16)
                {
                    intHorizontal = 5;
                    intVertical = 3;
                }
                else if (ProcessCount < 21)
                {
                    intHorizontal = 5;
                    intVertical = 4;
                }
                else if (ProcessCount < 26)
                {
                    intHorizontal = 5;
                    intVertical = 5;
                }
                else if (ProcessCount < 31)
                {
                    intHorizontal = 5;
                    intVertical = 6;
                }
                else if (ProcessCount < 36)
                {
                    intHorizontal = 5;
                    intVertical = 7;
                }
                else
                {
                    intHorizontal = 5;
                    intVertical = 8;
                }

            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }
        #region Default Grid Setting

        private void InitGrid()
        {
            grdPersonByProcess.Columns.Clear();
            grdPersonByProcess.ColumnCount = 6;

            int n = 0;
            // Set the Colums Hearder Names

            grdPersonByProcess.Columns[n].Name = "No";
            grdPersonByProcess.Columns[n].HeaderText = "";
            grdPersonByProcess.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdPersonByProcess.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdPersonByProcess.Columns[n++].Visible = true;

            grdPersonByProcess.Columns[n].Name = "Process";
            grdPersonByProcess.Columns[n].HeaderText = "공정명";
            grdPersonByProcess.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdPersonByProcess.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdPersonByProcess.Columns[n++].Visible = true;

            grdPersonByProcess.Columns[n].Name = "PersonID";
            grdPersonByProcess.Columns[n].HeaderText = "사원코드";
            grdPersonByProcess.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdPersonByProcess.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdPersonByProcess.Columns[n++].Visible = true;

            grdPersonByProcess.Columns[n].Name = "PersonName";
            grdPersonByProcess.Columns[n].HeaderText = "사용자명";
            grdPersonByProcess.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdPersonByProcess.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdPersonByProcess.Columns[n++].Visible = true;

            grdPersonByProcess.Columns[n].Name = "Team";
            grdPersonByProcess.Columns[n].HeaderText = "작업조";
            grdPersonByProcess.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdPersonByProcess.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdPersonByProcess.Columns[n++].Visible = true;

            grdPersonByProcess.Columns[n].Name = "TeamID";
            grdPersonByProcess.Columns[n].HeaderText = "TeamID";
            grdPersonByProcess.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdPersonByProcess.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdPersonByProcess.Columns[n++].Visible = false;
            

            grdPersonByProcess.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdPersonByProcess.RowTemplate.Height = 30;
            grdPersonByProcess.ColumnHeadersHeight = 45;
            grdPersonByProcess.ScrollBars = ScrollBars.Both;
            grdPersonByProcess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grdPersonByProcess.ReadOnly = true;
            grdPersonByProcess.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdPersonByProcess.ScrollBars = ScrollBars.Both;
            grdPersonByProcess.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdPersonByProcess.MultiSelect = false;

            foreach (DataGridViewColumn col in grdPersonByProcess.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        #endregion

        private void InitGrid2()
        {
            grdAllPerson.Columns.Clear();
            grdAllPerson.ColumnCount = 6;
            int a = 0;
            // Set the Colums Hearder Names
            grdAllPerson.Columns[a].Name = "No";
            grdAllPerson.Columns[a].HeaderText = "";
            grdAllPerson.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdAllPerson.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdAllPerson.Columns[a++].Visible = true;
                                 
            grdAllPerson.Columns[a].Name = "Process";
            grdAllPerson.Columns[a].HeaderText = "공정명";
            grdAllPerson.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdAllPerson.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdAllPerson.Columns[a++].Visible = true;

            grdAllPerson.Columns[a].Name = "PersonID";
            grdAllPerson.Columns[a].HeaderText = "사원코드";
            grdAllPerson.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdAllPerson.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdAllPerson.Columns[a++].Visible = true;

            grdAllPerson.Columns[a].Name = "PersonName";
            grdAllPerson.Columns[a].HeaderText = "사용자명";
            grdAllPerson.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdAllPerson.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdAllPerson.Columns[a++].Visible = true;

            grdAllPerson.Columns[a].Name = "Team";
            grdAllPerson.Columns[a].HeaderText = "작업조";
            grdAllPerson.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdAllPerson.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdAllPerson.Columns[a++].Visible = true;

            grdAllPerson.Columns[a].Name = "TeamID";
            grdAllPerson.Columns[a].HeaderText = "TeamID";
            grdAllPerson.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdAllPerson.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdAllPerson.Columns[a++].Visible = false;

            grdAllPerson.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdAllPerson.RowTemplate.Height = 30;
            grdAllPerson.ColumnHeadersHeight = 35;
            grdAllPerson.ScrollBars = ScrollBars.Both;
            grdAllPerson.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grdAllPerson.ReadOnly = true;
            grdAllPerson.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdAllPerson.ScrollBars = ScrollBars.Both;
            grdAllPerson.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdAllPerson.MultiSelect = false;

            foreach (DataGridViewColumn col in grdAllPerson.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #region TableLayoutPanel 하위 컨트롤들의 DockStyle.Fill 세팅
        private void SetScreen()
        {
            grdPersonByProcess.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdAllPerson.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Margin = new Padding(0, 0, 0, 0);
            foreach (Control control in tlpForm.Controls)//con = tlp 상위에서 2번째
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control contro in control.Controls)//tlp 상위에서 3번째
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
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        //공정을 선택할때마다 공정별 사원명단의 그리드뷰를 공정 기준으로 사용자 검색하는 기능
        void rBtnProcess_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radiobutton = sender as RadioButton;
            if (radiobutton.Checked)
            {
                g_MachineByProcessID = radiobutton.Tag.ToString();
                g_MachineByProcessName = radiobutton.Text.ToString();
                procMachine_Q();
                SetDgvPerson();
            }
        }

        // 주 / 야를 선택할때마다 주 / 야 변수명을 전역에 변경저장 할 것.
        void rBtnDayOrNight_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radiobutton = sender as RadioButton;
            if (radiobutton.Checked)
            {
                Frm_tprc_Main.g_tBase.DayOrNightID = radiobutton.Tag.ToString();
                Frm_tprc_Main.g_tBase.DayOrNight = radiobutton.Text.ToString();
            }
        }


        /// <summary>
        /// INI의 설정값으로 칸 나눔
        /// </summary>
        /// <param name="Horizontal"></param>
        /// <param name="Vertical"></param>
        void SetLayout(int Horizontal, int Vertical, TableLayoutPanel tlp)
        {
            while (tlp.ColumnCount != Horizontal)
            {
                if (tlp.ColumnCount > Horizontal)
                {
                    tlp.ColumnCount--;
                    tlp.ColumnStyles.RemoveAt(tlpProcess.ColumnCount);
                }
                else if (tlp.ColumnCount < Horizontal)
                {
                    tlp.ColumnCount++;
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
                }
            }
            foreach (ColumnStyle style in tlp.ColumnStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Width = 100.0f / tlp.ColumnCount;
            }

            while (tlp.RowCount != Vertical)
            {
                if (tlp.RowCount > Vertical)
                {
                    tlp.RowCount--;
                    tlp.RowStyles.RemoveAt(tlp.RowCount);
                }
                else if (tlp.RowCount < Vertical)
                {
                    tlp.RowCount++;
                    tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
                }
            }
            foreach (RowStyle style in tlp.RowStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Height = 100.0f / tlp.RowCount;
            }
        }

        /// <summary>
        /// DB의 부적합원인 리스트 불러오기
        /// </summary>
        /// <param name="Horizontal"></param>
        /// <param name="Vertical"></param>
        void LoadDataProcess(int Horizontal, int Vertical)
        {
            try
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.newRadioButton = new RadioButton[ds1.Tables[0].Rows.Count];
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        //if (i == Horizontal * Vertical - 1)
                        //{
                        //    break;
                        //}
                        RadioButton newRadioButton = new RadioButton();
                        tlpProcess.Controls.Add(newRadioButton, (i % Horizontal), (i / Horizontal));
                        DataRow dr = ds1.Tables[0].Rows[i];

                        newRadioButton.Text = dr["Process"].ToString().Trim();
                        newRadioButton.Tag = dr["ProcessID"].ToString().Trim();
                        newRadioButton.Dock = DockStyle.Fill;
                        newRadioButton.Font = new Font("맑은 고딕", 20, FontStyle.Bold);

                        newRadioButton.Checked = false;
                        newRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
                        newRadioButton.BackColor = System.Drawing.Color.LightSkyBlue;
                        newRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        newRadioButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
                        newRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
                        newRadioButton.ForeColor = System.Drawing.Color.White;
                        newRadioButton.UseVisualStyleBackColor = false;
                        newRadioButton.TextAlign = ContentAlignment.MiddleCenter;
                        newRadioButton.Click += new System.EventHandler(this.rBtnProcess_CheckedChanged);

                        this.newRadioButton[i] = newRadioButton;
                        rBtnProcess[i] = newRadioButton;
                        rBtnProcess[i].CheckedChanged += new System.EventHandler(this.rBtnProcess_CheckedChanged);
                    }
                    rBtnProcessRowCnt = ds1.Tables[0].Rows.Count;
                    foreach (RadioButton rb in newRadioButton)
                    {
                        if (rb.Checked)
                        {
                            Frm_tprc_Main.g_tBase.ProcessID = rb.Tag.ToString();
                            Frm_tprc_Main.g_tBase.Process = rb.Text.ToString();
                        }
                    }
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        void LoadDataTeam(int Horizontal, int Vertical)
        {
            try
            {
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    this.newCheckBox = new CheckBox[ds2.Tables[0].Rows.Count];
                    rBtnTeams = new CheckBox[ds2.Tables[0].Rows.Count];
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        CheckBox newCheckBox = new CheckBox();
                        tlpTeam.Controls.Add(newCheckBox, (i % Horizontal), (i / Horizontal));
                        DataRow dr = ds2.Tables[0].Rows[i];

                        newCheckBox.Text = dr[Code_sTeam.TEAM].ToString().Trim();
                        newCheckBox.Tag = dr[Code_sTeam.TEAMID].ToString().Trim();
                        newCheckBox.Dock = DockStyle.Fill;
                        newCheckBox.Font = new Font("맑은 고딕", 20, FontStyle.Bold);

                        newCheckBox.Checked = false;
                        newCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
                        newCheckBox.BackColor = System.Drawing.Color.LightSkyBlue;
                        newCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        newCheckBox.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
                        newCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
                        newCheckBox.ForeColor = System.Drawing.Color.White;
                        newCheckBox.UseVisualStyleBackColor = false;
                        newCheckBox.TextAlign = ContentAlignment.MiddleCenter;
                        newCheckBox.Click += new System.EventHandler(this.rBtnTeams_CheckedChanged);

                        this.newCheckBox[i] = newCheckBox;
                        this.rBtnTeams[i] = newCheckBox;
                        //rBtnTeams[i].CheckedChanged += new System.EventHandler(this.rBtnTeams_CheckedChanged);
                    }
                    rBtnTeamsRowCnt = ds2.Tables[0].Rows.Count;
                    foreach (CheckBox rb in newCheckBox)
                    {
                        if (rb.Checked)
                        {
                            Frm_tprc_Main.g_tBase.TeamID = rb.Tag.ToString();
                            Frm_tprc_Main.g_tBase.Team = rb.Text.ToString();
                        }
                    }
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }

        }


        void LoadDataDayOrNight(int Horizontal, int Vertical)
        {
            try
            {
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    this.newRadioButton = new RadioButton[ds2.Tables[0].Rows.Count];
                    rBtnDayOrNight = new RadioButton[ds2.Tables[0].Rows.Count];
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        RadioButton newRadioButton = new RadioButton();
                        tlpDayOrNight.Controls.Add(newRadioButton, (i % Horizontal), (i / Horizontal));
                        DataRow dr = ds2.Tables[0].Rows[i];

                        newRadioButton.Text = dr["Code_name"].ToString().Trim();
                        newRadioButton.Tag = dr["Code_ID"].ToString().Trim();
                        newRadioButton.Dock = DockStyle.Fill;
                        newRadioButton.Font = new Font("맑은 고딕", 20, FontStyle.Bold);

                        newRadioButton.Checked = false;
                        newRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
                        newRadioButton.BackColor = System.Drawing.Color.LightSkyBlue;
                        newRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        newRadioButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
                        newRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
                        newRadioButton.ForeColor = System.Drawing.Color.White;
                        newRadioButton.UseVisualStyleBackColor = false;
                        newRadioButton.TextAlign = ContentAlignment.MiddleCenter;
                        newRadioButton.Click += new System.EventHandler(this.rBtnDayOrNight_CheckedChanged);

                        this.newRadioButton[i] = newRadioButton;
                        this.rBtnDayOrNight[i] = newRadioButton;
                    }
                    rBtnDayOrNightRowCnt = ds2.Tables[0].Rows.Count;
                    foreach (RadioButton rb in newRadioButton)
                    {
                        if (rb.Checked)
                        {
                            Frm_tprc_Main.g_tBase.DayOrNightID = rb.Tag.ToString();
                            Frm_tprc_Main.g_tBase.DayOrNight = rb.Text.ToString();
                        }
                    }
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }

        }


        //공정별 설비 조회
        private void procMachine_Q()
        {
            try
            {
                string strProcess = g_MachineByProcessID;
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Work_sMachineByProcess.SPROCESSID, strProcess);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sMachine", sqlParameter, false);
                DataTable dt2 = dt.Clone();

                string[] sMachineID = null;
                sMachineID =Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine").Split('|');//배열에 설비아이디 넣기
                List<string> sMachine = new List<string>();
                foreach (string str in sMachineID)
                {
                    sMachine.Add(str);
                }
                sMachineID = null;
                bool chkOK = false;
                //ini값과 같으면 저장
                foreach (DataRow dr in dt.Rows)
                {
                    chkOK = false;
                    foreach (string Mac in sMachine)
                    {
                        if (Mac.Length > 4)
                        {
                            if (Mac.Substring(0, 4) == strProcess)
                            {
                                if (Mac.Substring(4, 2) == dr["MachineID"].ToString())
                                {
                                    chkOK = true;
                                    dt2.Rows.Add(dr.ItemArray);
                                    break;
                                }
                            }
                        }
                        
                    }
                    if (!chkOK)
                    {
                        sMachine.Remove(strProcess + dr["MachineID"].ToString());
                    }
                }

                cboMachine.ValueMember = Work_sMachineByProcess.MACHINEID;
                cboMachine.DisplayMember = Work_sMachineByProcess.MACHINE;
                cboMachine.DataSource = dt2;
                //if (dt2.Rows.Count > 1)
                //{
                //    cboMachine.SelectedIndex = 0;
                //}
                dt = null;
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }
        //사용자 셋팅
        private void SetDgvPerson()
        {
            // 작업조가 모두 체크되지 않았을 때 → 전체검색!
            // 나머진 팀검색
            // 2020.02.21 둘리 전체 검색은 일단 패스!!!

            int ChkTeam = 1;
            //foreach (CheckBox ck in newCheckBox)
            //{
            //    if (ck.Checked == true)
            //    {
            //        ChkTeam = 1;
            //        break;
            //    }
            //}

            DataGridViewRow row = null;
            //공정별 사용자 셋팅
            if (Tab_Person.SelectedIndex == 0)
            {
                grdPersonByProcess.Rows.Clear();//행 초기화
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Person_sPersonByProcess.CHKPROCESS, "1");
                sqlParameter.Add(Person_sPersonByProcess.PROCESSID, g_MachineByProcessID);
                sqlParameter.Add("ChkTeam", ChkTeam);
                sqlParameter.Add("TeamID", TeamID);
                ds2 = DataStore.Instance.ProcedureToDataSet("xp_Person_sPersonByProcessAndTeam", sqlParameter, false);

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds2.Tables[0].Rows[i];
                        
                        grdPersonByProcess.Rows.Add(
                            (i+1).ToString(),
                            dr["Process"],          //공정명
                            dr["PersonID"],         //사원코드
                            dr["PersonName"],       //사원명 
                            dr["Team"],             //작업조
                            dr["TeamID"]            //사원ID
                        );

                        row = grdPersonByProcess.Rows[i];
                        row.Height = 28;
                    }
                    grdPersonByProcess.ClearSelection();
                    grdPersonByProcess[0, 0].Selected = true;
                    if (grdPersonByProcess.Rows.Count > 0)
                    {
                        splPersonID.Text = grdPersonByProcess.Rows[0].Cells["PersonID"].Value.ToString();
                        splPersonName.Text = grdPersonByProcess.Rows[0].Cells["PersonName"].Value.ToString();
                    }
                }
            }
            //전체 사용자 셋팅
            else if (Tab_Person.SelectedIndex == 1)
            {
                grdAllPerson.Rows.Clear();//행 초기화
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Person_sPersonByProcess.CHKPROCESS, "0");
                sqlParameter.Add(Person_sPersonByProcess.PROCESSID, "");
                sqlParameter.Add("ChkTeam", 0);
                sqlParameter.Add("TeamID", TeamID);
                //sqlParameter.Add(Person_sPersonByProcess.CHKTEAM, "0");xp_Person_sPersonByProcess프로시저에서 teamid 체크추가시 사용
                //sqlParameter.Add(Person_sPersonByProcess.TEAMID, Team);xp_Person_sPersonByProcess프로시저에서 teamid 체크추가시 사용
                ds2 = DataStore.Instance.ProcedureToDataSet("xp_Person_sPersonByProcessAndTeam", sqlParameter, false);
                DataStore.Instance.CloseConnection();
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds2.Tables[0].Rows[i];

                        grdAllPerson.Rows.Add(
                            (i + 1).ToString(),
                            dr["Process"],          //공정명
                            dr["PersonID"],         //사원코드
                            dr["PersonName"],       //사원명 
                            dr["Team"],             //작업조
                            dr["TeamID"]            //사원ID
                        );
                    }
                    grdAllPerson.ClearSelection();
                    grdAllPerson[0, 0].Selected = true;
                    if (grdAllPerson.Rows.Count > 0)
                    {
                        splPersonID.Text = grdAllPerson.Rows[0].Cells["PersonID"].Value.ToString();
                        splPersonName.Text = grdAllPerson.Rows[0].Cells["PersonName"].Value.ToString();
                    }
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            splPersonID.Text = "";
            splPersonName.Text = "";
            Tab_Person.SelectedIndex = 1;
            grdAllPerson.ClearSelection();
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
            if (Tab_Person.SelectedIndex == 0)
            {

                if (grdPersonByProcess.Rows.Count > 0)
                {
                    Lib.btnRowUp(grdPersonByProcess);
                    int a = grdPersonByProcess.SelectedRows[0].Index;
                    if (a >= 0)
                    {
                        splPersonID.Text = grdPersonByProcess.Rows[a].Cells["PersonID"].Value.ToString();
                        splPersonName.Text = grdPersonByProcess.Rows[a].Cells["PersonName"].Value.ToString();
                    }
                }                

            }
            else if (Tab_Person.SelectedIndex == 1)
            {
                if (grdAllPerson.Rows.Count > 0)
                {
                    Lib.btnRowUp(grdAllPerson);
                    int a = grdPersonByProcess.SelectedRows[0].Index;
                    if (a >= 0)
                    {
                        splPersonID.Text = grdAllPerson.Rows[a].Cells["PersonID"].Value.ToString();
                        splPersonName.Text = grdAllPerson.Rows[a].Cells["PersonName"].Value.ToString();
                    }
                }                
            }
        }

        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            if (Tab_Person.SelectedIndex == 0)
            {
                if (grdPersonByProcess.Rows.Count > 0)
                {
                    Lib.btnRowDown(grdPersonByProcess);
                    int a = grdPersonByProcess.SelectedRows[0].Index;
                    if (a >= 0)
                    {
                        splPersonID.Text = grdPersonByProcess.Rows[a].Cells["PersonID"].Value.ToString();
                        splPersonName.Text = grdPersonByProcess.Rows[a].Cells["PersonName"].Value.ToString();
                    }
                }                
            }
            else if (Tab_Person.SelectedIndex == 1)
            {
                if (grdAllPerson.Rows.Count > 0)
                {
                    Lib.btnRowDown(grdAllPerson);
                    int a = grdAllPerson.SelectedRows[0].Index;
                    if (a >= 0)
                    {
                        splPersonID.Text = grdAllPerson.Rows[a].Cells["PersonID"].Value.ToString();
                        splPersonName.Text = grdAllPerson.Rows[a].Cells["PersonName"].Value.ToString();
                    }
                }                
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDgvPerson();//탭컨트롤 인덱스에 따라 공정/전체 사용자 조회
        }

        private void cmdPersonID_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_CMNumericKeypad numkeypad = new WizCommon.Popup.Frm_CMNumericKeypad(splPersonID.Text, "사원번호" );
            numkeypad.Owner = this;
            if (numkeypad.ShowDialog() == DialogResult.OK)
            {
                splPersonID.Text = numkeypad.tbInputText.Text;
                if (splPersonID.Text.Length == 8)
                {
                    Tab_Person.SelectedIndex = 1;//전체사원명단 탭으로 이동
                    //visibleTF(); //검색기능 | 검색해서 사원번호가 동일한 애 찾으면 동일한 애만 Visible=true 나머지는 Visible=false
                    int i = 0;
                    //검색 동일한 애를 찾기 있으면 Select 없으면 없는 사원번호라고 메시지박스
                    for (int n = 0; n < grdAllPerson.Rows.Count; n++)
                    {
                        if (splPersonID.Text == grdAllPerson.Rows[n].Cells["PersonID"].Value.ToString())//사원코드입력받은값 == 그리드뷰 값
                        {
                            grdAllPerson.Rows[n].Selected = true;
                            splPersonName.Text = grdAllPerson.CurrentRow.Cells["PersonID"].Value.ToString();
                            break;
                        }
                        else
                        {
                            i = i + 1;//사용자의입력값과 현재 행의 사원코드가 동일한 값이 아닐 때 변수에 1씩 추가해줌.
                        }
                    }
                    if (i == grdAllPerson.Rows.Count)//전체사용자의 수와 i 변수가 같을 때 동일한 값이 없는것
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

        private bool CheckData()
        {
            if (cboMachine.SelectedValue == null)
            {
                Message[0] = "[설비 선택]";
                Message[1] = "설비를 선택해주십시오.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return false;
            }
            if (Tab_Person.SelectedIndex == 0)
            {
                if (grdPersonByProcess.SelectedRows.Count == 0)
                {
                    Message[0] = "[사용자 선택]";
                    Message[1] = "사용자를 선택해주십시오.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return false;
                }
            }
            else
            {
                if (grdAllPerson.SelectedRows.Count == 0)
                {
                    Message[0] = "[사용자 선택]";
                    Message[1] = "사용자를 선택해주십시오.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return false;
                }
            }

            // 2020.04.13 둘리 -> 해당 호기가 작업중이어도 메시지 출력후에 화면이 꺼지지 않도록!!!
            if (Check_StartWorkingYN(m_ProcessID, cboMachine.SelectedValue != null ? cboMachine.SelectedValue.ToString() : "") == true)
            {
                Message[0] = "[작업내용 충돌]";
                Message[1] = "선택된 " + g_MachineByProcessName + "/" + sMachine + " 호기는" +
                    "\r\n" + " 이미 작업이 진행중입니다.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                return false;
            }

            return true;
        }


        // 선택. 클릭이벤트.
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!CheckData()) { return; }

            sProcess = g_MachineByProcessID;
            sProcessName = g_MachineByProcessName;//나중에 MDI폼 밑단에 표시할 공정이름, 추가적으로 머신이름도 필요할듯 머신이름 변수 필요함
            // sTeam = g_TeamID;  // 2020.02.21 둘리 : g_TeamID → 왜 라디오버튼을 통해서 세팅을 하는지 모르겠음!!! → 선택된 사원의 팀 아이디로 세팅하도록! 변경
            sMachine = cboMachine.SelectedValue.ToString();
            sMachineName = cboMachine.Text.ToString();
            Frm_tprc_Main.g_tBase.Machine = sMachineName;
            Frm_tprc_Main.g_tBase.MachineID = sMachine;
            Frm_tprc_Main.g_tBase.ProcessID = g_MachineByProcessID;
            Frm_tprc_Main.g_tBase.Process = g_MachineByProcessName;

            if (Tab_Person.SelectedIndex == 0 && grdPersonByProcess.SelectedRows.Count > 0)
            {
                sPersonID = grdPersonByProcess.SelectedRows[0].Cells["PersonID"].Value.ToString();
                sPersonName = grdPersonByProcess.SelectedRows[0].Cells["PersonName"].Value.ToString();
                sUserID = grdPersonByProcess.SelectedRows[0].Cells["PersonID"].Value.ToString();
                // 2020.02.21 둘리 : 여기서 세팅!
                sTeam = grdPersonByProcess.SelectedRows[0].Cells["TeamID"].Value.ToString();
                g_TeamName = grdPersonByProcess.SelectedRows[0].Cells["Team"].Value.ToString();
            }
            else if (Tab_Person.SelectedIndex == 1 && grdAllPerson.SelectedRows.Count > 0)
            {
                sPersonID = grdAllPerson.SelectedRows[0].Cells["PersonID"].Value.ToString();
                sPersonName = grdAllPerson.SelectedRows[0].Cells["PersonName"].Value.ToString();
                sUserID = grdAllPerson.SelectedRows[0].Cells["PersonID"].Value.ToString();
                // 2020.02.21 둘리 : 여기서 세팅!
                sTeam = grdAllPerson.SelectedRows[0].Cells["TeamID"].Value.ToString();
                g_TeamName = grdAllPerson.SelectedRows[0].Cells["Team"].Value.ToString();
            }
            else
            {
                return;
            }

            if (sProcess != string.Empty && sMachine != string.Empty && sPersonID != string.Empty && sUserID != string.Empty && sTeam != string.Empty)
            {
                Frm_tprc_Main.g_tBase.Person = sPersonName;
                Frm_tprc_Main.g_tBase.PersonID = sPersonID;
                Frm_tprc_Main.g_tBase.TeamID = sTeam;
                Frm_tprc_Main.g_tBase.Team = g_TeamName;
                Frm_tprc_Main.g_tBase.ProcessID = g_MachineByProcessID;
                Frm_tprc_Main.g_tBase.Process = g_MachineByProcessName;
                Frm_tprc_Main.g_tBase.MachineID = sMachine;
                // 상태바에 정보 담기

                if (!NoWork)//정상 공정작업일때
                {
                    ((WizWork.Frm_tprc_PlanInputMolded_Q)(this.Owner)).Set_stbInfo();

                    // 2020.01.05 허윤구.
                    // 작지에 호기가 필수선택이 아냐. (적어도 여기 삼주테크는..)
                    // 몇호기로 이 작지를 스타트 할지는 여기서 알 수 있지.
                    // 그러니까 여기서 현재 시작하고자 하는 작업이
                    // 이미 prescan 해서 작업시작을 끊어버린 공정과 호기는 아닌지 체크가 필요하다는 거지.
                    if (Check_StartWorkingYN(m_ProcessID, sMachine) == true)
                    {
                        Message[0] = "[작업내용 충돌]";
                        Message[1] = "선택된 " + g_MachineByProcessName + "/" + sMachine + " 호기는"  + 
                            "\r\n" + " 이미 작업이 진행중입니다."; 
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                        return;
                    }
                    else
                    {
                        ((WizWork.Frm_tprc_PlanInputMolded_Q)(this.Owner)).SetPreScanPopUpLoad(m_ProcessID, sMachine, "");
                    }                    
                }
                else if (isTool == true)
                {
                    DialogResult = DialogResult.OK;
                    return;
                }
                else
                {
                    if (Owner != null)
                    {
                        ((Frm_tprc_Main)(Owner)).Set_stsInfo();
                    }

                    // 하위품 목록 스캔이 아니라 바로 
                }
                DialogResult = DialogResult.OK;
            }
            else
            {
                if (sProcess == string.Empty)
                {
                    Message[0] = "[공정 선택 오류]";
                    Message[1] = "선택된 공정이 없습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                }
                if (sMachine == string.Empty)
                {
                    Message[0] = "[설비 선택 오류]";
                    Message[1] = "선택된 설비가 없습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                    lblChkMachine.Visible = true;
                }
                if (sPersonID == string.Empty && sUserID == string.Empty)
                {
                    Message[0] = "[작업자 선택 오류]";
                    Message[1] = "선택된 작업자가 없습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                }
                if (sTeam == string.Empty)
                {
                    Message[0] = "[작업조 선택 오류]";
                    Message[1] = "선택된 작업조가 없습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                }
            }
        }



        public bool LF_ChkMachineCheck()
        {
            // '***************************************************************
            // '0:공정작업입력 시 설비 점검(하루1회이상) 및 자주검사 수행(작업지시별 1회이상) check
            // '***************************************************************
            bool blResult = false;
            bool bFirst = false;

            string strMachine = "";
            string[] MachineTemp = null;

            string strMessage = "";
            string strMessageInspect = "";

            int intResult = 0;
            int intNoWorkTime = 0;
            int inAutoInspect = 0;

            Tools.INI_GS gs = new Tools.INI_GS();

            strMachine =Frm_tprc_Main.gs.GetValue("Work", "Machine", "");

            if (strMachine != "")
            {
                MachineTemp = strMachine.Split('|');//머신
                foreach (string str in MachineTemp)
                {
                    if (str == m_ProcessID + Frm_tprc_Main.g_tBase.MachineID)
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("ProcessID", Frm_tprc_Main.g_tBase.ProcessID);
                        sqlParameter.Add("MachineID", Frm_tprc_Main.g_tBase.MachineID);
                        sqlParameter.Add("PLotID", Frm_tprc_Main.g_tBase.sLotID);

                        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sToDayMcRegularInspectAutoYN", sqlParameter, false);
                        if (dt != null && dt.Rows.Count == 1)
                        {
                            DataRow dr = null;
                            dr = dt.Rows[0];

                            int.TryParse(dr["result"].ToString(), out intResult);

                            if (intResult < 0)
                            {
                                MessageBox.Show(string.Format("처리할 공정 및 호기를 설정하세요.", "공정및호기 설정오류"));
                                return blResult;
                            }

                            int.TryParse(dr["NoWorkTime"].ToString(), out intNoWorkTime);//'계획정지시간 이 없는 건만 Check

                            if (intNoWorkTime == 0)
                            {
                                if (intResult == 0)
                                {
                                    if (bFirst == true)
                                    {
                                        strMessage = dr["McName"].ToString().Trim();
                                    }
                                    else
                                    {
                                        if (strMessage == "")
                                        {
                                            strMessage = dr["McName"].ToString().Trim();
                                        }
                                        else
                                        {
                                            strMessage = strMessage + ",  " + dr["McName"].ToString().Trim();
                                        }
                                    }
                                    if (strMessage != "")
                                    {
                                        Message[0] = "[설비점검 오류]";
                                        Message[1] = strMessage + "의 설비점검을 하셔야합니다.";
                                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                                        //timer1.Stop();
                                        return false;
                                    }
                                }
                                int.TryParse(dr["AutoInspectByInstID"].ToString(), out inAutoInspect);
                                if (inAutoInspect == 0 && Frm_tprc_Main.g_tBase.ProcessID != "0405")
                                {
                                    if (bFirst == true)
                                    {
                                        strMessageInspect = dr["McName"].ToString().Trim();

                                    }
                                    else
                                    {
                                        if (strMessageInspect == "")
                                        {
                                            strMessageInspect = dr["McName"].ToString().Trim();
                                        }
                                        else
                                        {
                                            strMessageInspect = strMessageInspect + ",  " + dr["McName"].ToString().Trim();
                                        }
                                    }

                                    if (strMessageInspect != "")
                                    {
                                        Message[0] = "[자주검사 오류]";
                                        Message[1] = strMessageInspect + "의 자주검사를 하셔야합니다.";
                                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                                        //timer1.Stop();
                                        return false;
                                    }
                                }
                            }

                        }
                        else
                        {
                            Message[0] = "[공정및호기 설정오류]";
                            Message[1] = "처리할 공정 및 호기를 설정하세요.";
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool GetProcessAutoGatheringYN(string strProcessID)
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("nchkProc", 1);
                sqlParameter.Add("ProcessID", strProcessID);           //생산할 공정 =======setProcess 화면에서 변수값 받아와서 선택할것 수정요망

                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_Work_sProcess_AutoGatheringYN]", sqlParameter, false);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return false;
            }
        }



        private bool Check_StartWorkingYN(string ProcessID, string MachineID)
        {
            bool Flag = false;
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("ProcessID", ProcessID);
                sqlParameter.Add("MachineID", MachineID);           

                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdWork_sNowWorkingYN]", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Flag = true;
                }
                else
                {
                    
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                Flag = true;
            }
            return Flag;
        }



        private void grdPersonByProcess_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                splPersonID.Text = grdPersonByProcess.Rows[e.RowIndex].Cells["PersonID"].Value.ToString();
                splPersonName.Text = grdPersonByProcess.Rows[e.RowIndex].Cells["PersonName"].Value.ToString();
            }
        }

        private void grdAllPerson_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                splPersonID.Text = grdAllPerson.Rows[e.RowIndex].Cells["PersonID"].Value.ToString();
                splPersonName.Text = grdAllPerson.Rows[e.RowIndex].Cells["PersonName"].Value.ToString();
            }
        }
    }
}
