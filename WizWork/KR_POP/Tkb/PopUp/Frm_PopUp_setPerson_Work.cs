using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizWork
{
    public partial class Frm_PopUp_setPerson_Work : Form
    {
        // 주간 / 야간 테이블 레이아웃
        TableLayoutPanel tlpDayOrNight = new TableLayoutPanel();
        List<RadioButton> lstRbnDayOrNight = new List<RadioButton>();

        TableLayoutPanel tlpTeam = new TableLayoutPanel();
        List<RadioButton> lstRbnTeam = new List<RadioButton>();

        List<Frm_PopUp_setPerson_CodeView> lstPerson = new List<Frm_PopUp_setPerson_CodeView>();

        public Frm_PopUp_setPerson_Work()
        {
            InitializeComponent();
        }

        private void Frm_PopUp_setPerson_Load(object sender, EventArgs e)
        {
            // 테이블 레이아웃 Dock = Fill
            SetScreen();

            this.Size = new Size(815, 490);

            this.lblPersonID.Text = Frm_tprc_Main.g_tBase.PersonID != null ? Frm_tprc_Main.g_tBase.PersonID : "";
            this.lblName.Text = Frm_tprc_Main.g_tBase.Person != null ? Frm_tprc_Main.g_tBase.Person : "";

            // 주간 야간 버튼 세팅
            SetDayOrNightRBtn();

            // 팀 버튼 세팅
            SetTeamRBtn();

            FillGrid(dgdByProcess);
        }

        #region 테이블 레이아웃 패널 사이즈 조정 : SetScreen()
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

        #region 주간 야간 라디오버튼 생성 : SetDayOrNightRBtn()

        private void SetDayOrNightRBtn()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("CodeGbn", "DayOrNight");
                sqlParameter.Add("sRelation", "");

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_Code_sCmCode", sqlParameter, false);
                
                if (ds != null
                    && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    // 주간 야간 버튼 모음 초기화
                    lstRbnDayOrNight.Clear();

                    //int btnCount = 9; // 버튼 갯수
                    float heightPercent = 100.0F / dt.Rows.Count; // 폭 퍼센트 설정 = 100 / 버튼갯수

                    // 작업 패널 생성
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tlpDayOrNight.RowStyles.Add(new RowStyle(SizeType.Percent, heightPercent));
                    }

                    tlpTeamPerson.Controls.Add(tlpDayOrNight, 0, 0);
                    tlpDayOrNight.Dock = DockStyle.Fill;

                    int index = 0;
                    foreach(DataRow dr in dt.Rows)
                    {
                        // 라디오 버튼 생성
                        RadioButton newRadioButton = new RadioButton();
                        tlpDayOrNight.Controls.Add(newRadioButton, 0, index);

                        newRadioButton.Text = dr["Code_name"].ToString().Trim();
                        newRadioButton.Tag = dr["Code_ID"].ToString().Trim();
                        newRadioButton.Dock = DockStyle.Fill;
                        newRadioButton.Width = 20; // ???
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

                        lstRbnDayOrNight.Add(newRadioButton);

                        index++;
                    }

                    if (lstRbnDayOrNight.Count > 0)
                    {
                        lstRbnDayOrNight[0].Checked = true;
                        Frm_tprc_Main.g_tBase.DayOrNightID = lstRbnDayOrNight[0].Tag.ToString();
                        Frm_tprc_Main.g_tBase.DayOrNight = lstRbnDayOrNight[0].Text.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        #endregion

        #region 팀 라디오버튼 생성 : SetDayOrNightRBtn()

        private void SetTeamRBtn()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_Code_sTeam", sqlParameter, false);

                if (ds != null
                    && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    // 주간 야간 버튼 모음 초기화
                    lstRbnTeam.Clear();

                    //int btnCount = 9; // 버튼 갯수
                    float heightPercent = 100.0F / dt.Rows.Count; // 폭 퍼센트 설정 = 100 / 버튼갯수

                    // 작업 패널 생성
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tlpTeam.RowStyles.Add(new RowStyle(SizeType.Percent, heightPercent));
                    }

                    tlpTeamPerson.Controls.Add(tlpTeam, 1, 0);
                    tlpTeam.Dock = DockStyle.Fill;

                    int index = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        // 라디오 버튼 생성
                        RadioButton newRadioButton = new RadioButton();
                        tlpTeam.Controls.Add(newRadioButton, 0, index);

                        newRadioButton.Text = dr["Team"].ToString().Trim();
                        newRadioButton.Tag = dr["TeamID"].ToString().Trim();
                        newRadioButton.Dock = DockStyle.Fill;
                        newRadioButton.Width = 20; // ???
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
                        newRadioButton.Click += new System.EventHandler(this.rBtnTeam_CheckedChanged);

                        lstRbnTeam.Add(newRadioButton);

                        index++;
                    }

                    if (lstRbnTeam.Count > 0)
                    {
                        lstRbnTeam[0].Checked = true;
                        Frm_tprc_Main.g_tBase.TeamID = lstRbnTeam[0].Tag.ToString();
                        Frm_tprc_Main.g_tBase.Team = lstRbnTeam[0].Text.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }
        #endregion

        #region 주간/야간, 팀 라디오버튼 클릭 이벤트

        // 주 / 야를 선택할때마다 주 / 야 변수명을 전역에 변경저장 할 것.
        private void rBtnDayOrNight_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radiobutton = sender as RadioButton;
            if (radiobutton.Checked)
            {
                Frm_tprc_Main.g_tBase.DayOrNightID = radiobutton.Tag.ToString();
                Frm_tprc_Main.g_tBase.DayOrNight = radiobutton.Text.ToString();
            }
        }

        // 팀 선택할때마다 팀 변수명을 전역에 변경저장 할 것.
        private void rBtnTeam_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radiobutton = sender as RadioButton;
            if (radiobutton.Checked)
            {
                Frm_tprc_Main.g_tBase.TeamID = radiobutton.Tag.ToString();
                Frm_tprc_Main.g_tBase.Team = radiobutton.Text.ToString();

                if (Tab_Person.SelectedIndex == 0)
                {
                    FillGrid(dgdByProcess);
                }
                else
                {
                    FillGrid(dgdAll);
                }
            }

        }

        #endregion

        #region 사원 조회 :  FillGrid()

        private void FillGrid(DataGridView dgv)
        {
            try
            {
                lstPerson.Clear();

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("ChkAll", dgv.Name.Equals("dgdAll") ? 1 : 0);
                sqlParameter.Add("ChkTeam", Frm_tprc_Main.g_tBase.TeamID != null ? 1 : 0);
                sqlParameter.Add("TeamID", Frm_tprc_Main.g_tBase.TeamID != null ? Frm_tprc_Main.g_tBase.TeamID : "");
                DataSet ds = DataStore.Instance.ProcedureToDataSet("[xp_prdWork_sPersonByTeam]", sqlParameter, false);

                DataTable dt = ds.Tables[0];
                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        var Person = new Frm_PopUp_setPerson_CodeView()
                        {
                            Process = dr["Process"].ToString(),
                            Name = dr["Name"].ToString(),
                            Team = dr["Team"].ToString(),
                            TeamID = dr["TeamID"].ToString(),
                            PersonID = dr["PersonID"].ToString()
                        };

                        lstPerson.Add(Person);
                    }
                }

                BindingSource bs = new BindingSource();
                bs.DataSource = lstPerson;
                dgv.DataSource = bs;
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }
        #endregion

        // 탭 클릭 이벤트
        private void Tab_Person_TabIndexChanged(object sender, EventArgs e)
        {
            if (Tab_Person.SelectedIndex == 0)
            {
                FillGrid(dgdByProcess);
            }
            else
            {
                FillGrid(dgdAll);
            }
        }
        
        // 선택 버튼 클릭 이벤트
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DataGridView dgv = new DataGridView();
            if (Tab_Person.SelectedIndex == 0)
            {
                dgv = dgdByProcess;
            }
            else
            {
                dgv = dgdAll;
            }

            var Person = dgv.SelectedRows[0].DataBoundItem as Frm_PopUp_setPerson_CodeView;
            if (Person != null)
            {
                Frm_tprc_Main.g_tBase.PersonID = Person.PersonID;
                Frm_tprc_Main.g_tBase.Person = Person.Name;
                Frm_tprc_Main.g_tBase.TeamID = Person.TeamID;
                Frm_tprc_Main.g_tBase.Team = Person.Team;

                if (Owner != null)
                {
                    ((Frm_tprc_Main)(Owner)).Set_stsInfo();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                WizCommon.Popup.MyMessageBox.ShowBox("작업하는 사원을 선택해주세요.", "[저장 전]", 0, 1);
            }
        }
        // 닫기 버튼 클릭 이벤트
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 위 아래 버튼 클릭 이벤트 모음

        private void btnUp_Click(object sender, EventArgs e)
        {
            DataGridSelRow_UpDown(-1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            DataGridSelRow_UpDown(1);
        }

        private void DataGridSelRow_UpDown(int upDown)
        {
            DataGridView dgv = new DataGridView();
            if (Tab_Person.SelectedIndex == 0)
            {
                dgv = dgdByProcess;
            }
            else
            {
                dgv = dgdAll;
            }

            int moveIndex = dgv.SelectedRows[0].Index + upDown;
            int maxIndex = dgv.Rows.Count;

            if (moveIndex >= 0
                && moveIndex < maxIndex)
            {
                dgv[0, moveIndex].Selected = true;
            }
        }

        #endregion

        // 삭제 버튼 클릭 → 왜 필요 한건지??
        private void btnDel_Click(object sender, EventArgs e)
        {

        }
    }

    public class Frm_PopUp_setPerson_CodeView
    {
        public string Process { get; set; }
        public string PersonID { get; set; }
        public string Name { get; set; }
        public string DepartID { get; set; }
        public string Depart { get; set; }
        public string TeamID { get; set; }
        public string Team { get; set; }
    }
}
