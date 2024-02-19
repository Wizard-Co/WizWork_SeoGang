using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Properties;
using WizCommon;
using System.ComponentModel;
using WizWork.GLS.PopUp;

//*******************************************************************************
//프로그램명    Frm_tprc_PlanInputMolded_Q.cs
//메뉴ID        
//설명          Frm_tprc_PlanInputMolded_Q 메인소스입니다.
//작성일        2019.08.01
//개발자        허윤구
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************

//*******************************************************************************

namespace WizWork
{
    public partial class Frm_tprc_PlanInputMolded_Q : Form
    {
        DataGridViewRow dgvr = null;
        string[] Message = new string[2];
        //string sFileName = ConnectionInfo.filePath;             //Wizard.ini 파일위치
        INI_GS gs = new INI_GS();
        WizWorkLib Lib = new WizWorkLib();
        public static string strLotID = "";
        public static string strMachineID = "";
        public static string strProcessID = "";
        public static string strInstID = "";
        public static string strInstDetSeq = "";

        // 하단 작업버튼 모음

        // 1. 작업버튼 리스트
        List<Button> lstButtons = new List<Button>();
        // 2. 작업 버튼용 테이블 레이아웃
        TableLayoutPanel tlpWorkingButtons = new TableLayoutPanel();
        // 3. 버튼 갯수 지정
        int btnCount = 10;

        public Frm_tprc_PlanInputMolded_Q()
        {
            InitializeComponent();
        }

        private void Frm_tprc_PlanInput_Q_Load(object sender, EventArgs e)
        {
            txtPLotID.Text = "";
            txtBuyerArticle.Text = "";

            SetDateTime();

            InitPanel();
            InitGrid(); // Grid Setting  
            SetProcessComboBox();  // 공정 콤보 셋팅
            chkProcess.Checked = true;

            if (cboProcess.Items.Count == 3)
            {
                // 아템갯수 3 = 전체 + 부분전체 + 환경설정 상 선택한거 한개일경우,
                cboProcess.SelectedIndex = 2;   // 환경설정 항목으로 고정
             }
            else
            {
                cboProcess.SelectedIndex = 1;
            }
            btnThisMonth_Click(null, null);
            //chkComplete.Checked = true;

            procQuery();           

            chkInsDate.Checked = true;
            chkPLotID.Checked = false;
            txtPLotID.Select();
            txtPLotID.Focus();

            //panelBottom.Controls.Clear();

            // 이전 다음 버튼 안쓰는 버전
            setWorkingButtons();
            // 이전 다음 버튼 쓰는 버전
            //setWorkingButtons_NextAndBeforeButtons();
        }

        public void beSearch()
        {
            txtPLotID.Text = "";
            txtBuyerArticle.Text = "";

            SetDateTime();

            InitPanel();
            InitGrid(); // Grid Setting  
            SetProcessComboBox();  // 공정 콤보 셋팅
            chkProcess.Checked = true;

            if (cboProcess.Items.Count == 3)
            {
                // 아템갯수 3 = 전체 + 부분전체 + 환경설정 상 선택한거 한개일경우,
                cboProcess.SelectedIndex = 2;   // 환경설정 항목으로 고정
            }
            else
            {
                cboProcess.SelectedIndex = 1;
            }
            btnThisMonth_Click(null, null);
            //chkComplete.Checked = true;

            procQuery();

            chkInsDate.Checked = true;
            chkPLotID.Checked = false;
            txtPLotID.Select();
            txtPLotID.Focus();

            //panelBottom.Controls.Clear();

            // 이전 다음 버튼 안쓰는 버전
            setWorkingButtons();
            // 이전 다음 버튼 쓰는 버전
            //setWorkingButtons_NextAndBeforeButtons();
        }

        // 화면 활성화 이벤트
        private void Frm_tprc_PlanInput_Q_Activated(object sender, EventArgs e)
        {
            procQuery();
            refreshWorkingButtons();
        }

         // 금월버튼. (사번별 작지 월단위 일괄등록에 따라 필요해진 버튼.)
        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);

            //mtb_From.Text = startOfMonth.ToString("yyyyMMdd");
            mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
        }

        private void SetDateTime()
        {
            ////ini 날짜 불러와서 기간 설정하기
            chkInsDate.Checked = true;
            int Days = 0;
            string[] sInstDate =Frm_tprc_Main.gs.GetValue("Work", "Screen", "Screen").Split('|');
            foreach (string str in sInstDate)
            {
                string[] Value = str.Split('/');
                if (this.Name.ToUpper().Contains(Value[0].ToUpper()))
                {
                    int.TryParse(Value[1], out Days);
                    break;
                }
            }
            mtb_From.Text = DateTime.Today.AddDays(-Days).ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
        }

        #region InitPanel

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

        #region Default Grid Setting

        private void InitGrid()
        {
            grdData.Columns.Clear();
            grdData.ColumnCount = 28;
            // Set the Colums Hearder Names
            int i = 0;

            grdData.Columns[i].Name = "RowSeq";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "StartDate";
            grdData.Columns[i].HeaderText = "지시일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "CustomID";
            grdData.Columns[i].HeaderText = "거래처코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "KCustom";
            grdData.Columns[i].HeaderText = "거래처";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "BuyerModelID";
            grdData.Columns[i].HeaderText = "차종코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "Model";
            grdData.Columns[i].HeaderText = "차종명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "ArticleID";
            grdData.Columns[i].HeaderText = "품목코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;        
            
            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "품번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "InstID";
            grdData.Columns[i].HeaderText = "지시번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OrderID";
            grdData.Columns[i].HeaderText = "관리번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "InstQty";
            grdData.Columns[i].HeaderText = "지시수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Process";
            grdData.Columns[i].HeaderText = "공정";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "MachineID";
            grdData.Columns[i].HeaderText = "호기";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "ToTalWorkQty";
            grdData.Columns[i].HeaderText = "작업완료수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "WorkQty";
            grdData.Columns[i].HeaderText = "합격수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "DefectQty";
            grdData.Columns[i].HeaderText = "불량수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;


            grdData.Columns[++i].Name = "LotID";
            grdData.Columns[i].HeaderText = "생산LOTID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //grdData.Columns[++i].Name = "InstQty";
            //grdData.Columns[i].HeaderText = "지시수량";
            //grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            //grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdData.Columns[i].ReadOnly = true;
            //grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "LabelPrintQty";
            grdData.Columns[i].HeaderText = "발행수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OrderQty";
            grdData.Columns[i].HeaderText = "오더수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "QtyPerBox";
            grdData.Columns[i].HeaderText = "박스당수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "InstDetSeq";
            grdData.Columns[i].HeaderText = "InstDetSeq";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "LabelPrintYN";
            grdData.Columns[i].HeaderText = "LabelPrintYN";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "PATTERNID";
            grdData.Columns[i].HeaderText = "PATTERNID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "ProcessID";
            grdData.Columns[i].HeaderText = "ProcessID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "MachineID";
            grdData.Columns[i].HeaderText = "MachineID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OverYN";
            grdData.Columns[i].HeaderText = "OverYN";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;         

            grdData.Columns[++i].Name = "PrevProcessCompletYN";
            grdData.Columns[i].HeaderText = "이전공정";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;


            grdData.Font = new Font("맑은 고딕", 13);
            grdData.RowTemplate.DefaultCellStyle.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
            grdData.RowTemplate.Height = 32;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            //grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdData.ReadOnly = true;

            grdData.EnableHeadersVisualStyles = false;  // 헤더 셀 스타일 적용 용도.

            foreach (DataGridViewColumn col in grdData.Columns)
            {              
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;


        }

        #endregion

        //콤보박스 데이터 바인딩
        private void SetProcessComboBox()
        {
            int intnChkProc = 1;
            string strProcessID = "";

            DataSet ds = null;

            strProcessID =Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID");
            if (strProcessID.Equals("ProcessID")) { strProcessID = ""; }
            string[] gubunProcess = strProcessID.Split(new char[] { '|' });

            //공정 가져오기
            try
            {
                strProcessID = string.Empty;

                for (int i = 0; i < gubunProcess.Length; i++)
                {
                    if (strProcessID.Equals(string.Empty))
                    {
                        strProcessID = gubunProcess[i];
                    }
                    else
                    {
                        strProcessID = strProcessID + "|" + gubunProcess[i];
                    }
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Work_sProcess.NCHKPROC, intnChkProc);//cboProcess.Text 
                sqlParameter.Add(Work_sProcess.PROCESSID, strProcessID);//cboProcess.Text

                ds = DataStore.Instance.ProcedureToDataSet("[xp_Work_sProcess]", sqlParameter, false);

                DataRow newRow = ds.Tables[0].NewRow();
                newRow[Work_sProcess.PROCESSID] = "*";
                newRow[Work_sProcess.PROCESS] = "전체";

                DataRow newRow2 = ds.Tables[0].NewRow();
                newRow2[Work_sProcess.PROCESSID] = strProcessID;
                newRow2[Work_sProcess.PROCESS] = "부분전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    ds.Tables[0].Rows.InsertAt(newRow2, 1);
                    cboProcess.DataSource = ds.Tables[0];
                }

                cboProcess.ValueMember = Work_sProcess.PROCESSID;
                cboProcess.DisplayMember = Work_sProcess.PROCESS;
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
            return;
        }



        public void procQuery()
        {
            string strErr = "";

            grdData.Rows.Clear();

            int intnchkInstDate = 0;

            string strStartDate = "";
            string strEndDate = "";
            int intnChkProcessID = 0;
            string strProcessID = "";

            int intnChkBuyerArticle = 0;
            string strBuyerArticle = "";
            int intnChkLotID = 0;
            string strLotID = "";


            double InstQty = 0;
            int intnChkCompleteYN = 0;

            // 검색일자 체크
            if (chkInsDate.Checked)
            {
                intnchkInstDate = 1;
                strStartDate = mtb_From.Text.Replace("-", "");
                strEndDate = mtb_To.Text.Replace("-", "");
                if ((int.Parse(strStartDate) - int.Parse(strEndDate)) > 0)
                {
                    string Message = "[지시일] 시작일이 종료일보다 늦을 수 없습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message, "[검색조건]", 0, 1);
                    return;
                }
            }

            // 완료일자 포함여부 체크.
            if (chkComplete.Checked)
            {
                intnChkCompleteYN = 1;
            }

            // 작지번호 기입여부 체크
            if (chkPLotID.Checked)
            {
                if (this.txtPLotID.Text == "" || txtPLotID.Text == "전체")
                {
                    Message[0] = "[검색조건]";
                    Message[1] = "LOTID 를 입력하시기 바랍니다.!!";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    this.txtPLotID.Focus();

                    return;
                }
                //strInstID = txtPSabun.Text.Trim().Substring(2, 12);//지시번호
                intnChkLotID = 1;
                strLotID = txtPLotID.Text.Trim();
            }

            // 품번 기입여부 체크
            if (chkBuyerArticle.Checked)
            {
                if (this. txtBuyerArticle.Text == "" || txtBuyerArticle.Text == string.Empty)
                {
                    Message[0] = "[검색조건]";
                    Message[1] = "품번 을 입력하시기 바랍니다.!!";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    this.txtBuyerArticle.Focus();

                    return;
                }

                intnChkBuyerArticle = 1;
                strBuyerArticle = txtBuyerArticle.Text.Trim();
            }



            // 공정 값체크
            if (chkProcess.Checked == false || (chkProcess.Checked == true && cboProcess.SelectedIndex == 0)) //공정체크 : N
            {
                intnChkProcessID = 0;
                strProcessID = "";
            }
            else if (chkProcess.Checked == true && cboProcess.SelectedIndex > 0) // 공정체크 : O
            {
                intnChkProcessID = 1;
                try
                {
                    strProcessID = cboProcess.SelectedValue.ToString();

                    if (strProcessID == null
                        || strProcessID.Equals(""))
                    {
                        cboProcess.SelectedIndex = 0;
                        intnChkProcessID = 0;
                        strProcessID = "";
                    }
                }
                catch (Exception e1)
                {
                    strErr = e1.Message.ToString();

                    strProcessID = "";
                }
            }

            if (chkInsDate.Checked == false && chkProcess.Checked == false && chkBuyerArticle.Checked == false && intnchkInstDate == 0 && intnChkProcessID == 0 &&  intnChkLotID == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("최소한 하나의 검색조건을 선택하세요.\n ※검색조건 선택은 버튼을 눌러 오목하게 들어간 모양으로 만들어주세요!!", "[검색조건]", 0, 1);
                return;
            }
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("nchkInstDate", intnchkInstDate);
                sqlParameter.Add("FromDate", strStartDate);
                sqlParameter.Add("ToDate", strEndDate);
                sqlParameter.Add("nChkProcessID", intnChkProcessID);
                sqlParameter.Add("ProcessID", strProcessID);

                sqlParameter.Add("nChkBuyerArticle", intnChkBuyerArticle);
                sqlParameter.Add("BuyerArticle", strBuyerArticle);
                sqlParameter.Add("nChkLotID", intnChkLotID);
                sqlParameter.Add("LotID", strLotID);
                sqlParameter.Add("nChkCompleteYN", intnChkCompleteYN);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_splInputDet", sqlParameter, false);


                if (dt != null && dt.Rows.Count > 0)
                {
                    int i = 0;
                    int OrderQty = 0;
                    int ProdQtyPerBox = 0;
                    int LabelPrintQty = 0;

                    double TotalWorkQty = 0;
                    double WorkQty = 0;
                    double DefectQty = 0;

                    DataGridViewRow dgvr = null;

                    foreach (DataRow dr in dt.Rows)
                    {
                        string PrevProcessCompleYN = "X";                                                
                        
                        double.TryParse(dr["InstQty"].ToString(), out InstQty);
                        int.TryParse(dr["OrderQty"].ToString(), out OrderQty);                       
                        int.TryParse(dr["ProdQtyPerBox"].ToString(), out ProdQtyPerBox);
                        int.TryParse(dr["LabelPrintQty"].ToString(), out LabelPrintQty);

                        double.TryParse(dr["Workqty"].ToString(), out WorkQty);
                        double.TryParse(dr["DefectQty"].ToString(), out DefectQty);
                        double.TryParse(dr["ToTalWorkQty"].ToString(), out TotalWorkQty);

                        grdData.Rows.Add(++i,
                                                Lib.MakeDate(WizWorkLib.DateTimeClss.DF_MID, dr["StartDate"].ToString()),                                                
                                                 Lib.CheckNull(dr["CustomID"].ToString()),
                                                 Lib.CheckNull(dr["KCustom"].ToString()),
                                                 Lib.CheckNull(dr["BuyerModelID"].ToString()),
                                                 Lib.CheckNull(dr["Model"].ToString()),
                                                 Lib.CheckNull(dr["ArticleID"].ToString()),                                              
                                                 Lib.CheckNull(dr["BuyerArticleNo"].ToString()),
                                                 Lib.CheckNull(dr["Article"].ToString()),
                                                 Lib.CheckNull(dr["InstID"].ToString()),
                                                 Lib.CheckNull(dr["OrderID"].ToString()),
                                                 string.Format("{0:n0}", (int)InstQty),          
                                                 Lib.CheckNull(dr["Process"].ToString()),
                                                 Lib.CheckNull(dr["MachineID"].ToString()),

                                                 string.Format("{0:n0}", (int)TotalWorkQty),    // 작업완료수량
                                                 string.Format("{0:n0}", (int)WorkQty),         // 합격수량
                                                 string.Format("{0:n0}", (int)DefectQty),       // 불량수량
                                                 Lib.CheckNull(dr["LotID"].ToString()),                                                 
                                                 string.Format("{0:n0}", LabelPrintQty),
                                                 string.Format("{0:n0}", OrderQty),
                                                 string.Format("{0:n0}", ProdQtyPerBox),
                                                 Lib.CheckNull(dr["InstDetSeq"].ToString()),
                                                 Lib.CheckNull(dr["LabelPrintYN"].ToString()),
                                                 Lib.CheckNull(dr["PATTERNID"].ToString()),
                                                 Lib.CheckNull(dr["ProcessID"].ToString()),
                                                 Lib.CheckNull(dr["MachineID"].ToString()),
                                                 Lib.CheckNull(dr["OverYN"].ToString()),                                                 
                                                 PrevProcessCompleYN //
                        );                        
                        dgvr = grdData.Rows[i - 1];
                        dgvr.Cells["DefectQty"].Style.ForeColor = Color.Red;
                        //작업을 했네.                

                    }
                }
                else
                {
                    //((WizWork.Frm_tprc_Main)(this.MdiParent)).SetstbLookUp("0개의 자료가 검색되었습니다.");
                    grdData.Rows.Clear();
                }
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdData.RowCount);
                Frm_tprc_Main.gv.SetStbInfo();
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }


        /// <summary>
        /// 조회버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLookup_Click(object sender, EventArgs e)
        {
            procQuery();
            refreshWorkingButtons();
        }

      

        private void cmdRowUp_Click(object sender, EventArgs e)
        {
            Lib.btnRowUp(grdData);
        }

        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            Lib.btnRowDown(grdData);
        }

        public void SetsetProcessFormLoad()
        {
            frm_tprc_setProcess Set_ps = new frm_tprc_setProcess(strLotID, strMachineID, strProcessID, strInstID);
            Set_ps.Owner = this;
            if (Set_ps.ShowDialog() == DialogResult.OK)
            {

            };
        }
        public void Set_stbInfo()
        {
            if (MdiParent == null)
            {
                MdiParent = new Frm_tprc_Main();
            }
            ((WizWork.Frm_tprc_Main)(MdiParent)).Set_stsInfo();
        }

        private void cboProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string strProcess = cboProcess.SelectedValue.ToString();
            //    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            //    sqlParameter.Add(Work_sProcess.PROCESSID, strProcess);//cboProcess.Text 

            //    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Work_sMachinebyProcess", sqlParameter, false);
            //    //DataRow newRow = dt.NewRow();
            //    //newRow[Work_sMachineByProcess.MACHINEID] = "*";
            //    //newRow[Work_sMachineByProcess.MACHINENO] = "전체";
            //    //dt.Rows.InsertAt(newRow, 0);

            //    DataTable dt2 = dt.Clone();

            //    string[] sMachineID = null;
            //    sMachineID =Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine").Split('|');//배열에 설비아이디 넣기
            //    List<string> sMachine = new List<string>();
            //    foreach (string str in sMachineID)
            //    {
            //        sMachine.Add(str);
            //    }
            //    sMachineID = null;
            //    bool chkOK = false;
            //    //ini값과 같으면 저장
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        chkOK = false;
            //        foreach (string Mac in sMachine)
            //        {
            //            if (Mac.Length > 4)
            //            {
            //                if (Mac.Substring(0, 4) == strProcess)
            //                {
            //                    if (Mac.Substring(4, 2) == dr["MachineID"].ToString())
            //                    {
            //                        chkOK = true;
            //                        dt2.Rows.Add(dr.ItemArray);
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //        if (!chkOK)
            //        {
            //            sMachine.Remove(strProcess + dr["MachineID"].ToString());
            //        }
            //    }
            //    DataRow newRow = dt2.NewRow();

            //    if (strProcess == "2101") // 성형작업시. 머신이 너무 길어. 
            //    {
            //        newRow[Work_sMachineByProcess.MACHINEID] = "*";
            //        newRow[Work_sMachineByProcess.MACHINENO] = "전체";
            //    }
            //    else
            //    {
            //        newRow[Work_sMachineByProcess.MACHINEID] = "*";
            //        newRow[Work_sMachineByProcess.MACHINE] = "전체";
            //    }
            //    //newRow[Work_sMachineByProcess.MACHINEID] = "*";
            //    //newRow[Work_sMachineByProcess.MACHINE] = "전체";
            //    dt2.Rows.InsertAt(newRow, 0);

            //}
            //catch (Exception excpt)
            //{
            //    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            //}
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            if (WizCommon.Popup.MyMessageBox.ShowBox("공정작업 화면을 종료하시겠습니까?", "[공정작업 종료]", 0, 0) == DialogResult.OK)
            {
                
                Dispose();
                Close();
            }
            return;
        }
        public bool SetJobIDBy0405()
        {
            return true;
        }

        

        public void SetWork_UFormLoad()
        {
            //Frm_tprc_Work_U WkU_NO = new Frm_tprc_Work_U();
            //WkU_NO.Owner = this;
            //WkU_NO.ShowDialog();

        }


        private void dgvLookupResult_CurrentCellChanged(object sender, EventArgs e)
        {
            //e.RowIndex

        }

        private void cboProcess_Click(object sender, EventArgs e)
        {
            SetProcessComboBox();
        }



        private void chkBuyerArticle_Click(object sender, EventArgs e)
        {
            if (this.chkBuyerArticle.Checked)
            {
                txtBuyerArticle.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("품번입력", "품번");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtBuyerArticle.Text = keypad.tbInputText.Text;
                    procQuery();
                }
            }
            else
            {                
                this.txtBuyerArticle.Text = "";               
            }
        }
        private void txtBuyerArticle_Click(object sender, EventArgs e)
        {
            if (this.chkBuyerArticle.Checked)
            {
                txtBuyerArticle.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("품번입력", "품번");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtBuyerArticle.Text = keypad.tbInputText.Text;
                    procQuery();
                }
            }
            else
            {
                chkBuyerArticle.Checked = true;
                txtBuyerArticle.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("품번입력", "품번");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtBuyerArticle.Text = keypad.tbInputText.Text;
                    procQuery();
                }
            }
        }


        private void txtPLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtPLotID.Text.Trim().Length == 15)
                {
                    chkPLotID.Checked = true;
                    procQuery();
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("올바른 작업지시 번호가 아닙니다. \r\n 작업지시번호를 스캔해주세요!", "[바코드 오류]", 2, 1);
                    return;
                }
            }
        }

        private void chkPLotID_Click(object sender, EventArgs e)
        {
            if (this.chkPLotID.Checked)
            {
                txtPLotID.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("작지번호 입력", "번호");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtPLotID.Text = keypad.tbInputText.Text;
                    procQuery();
                }
            }
            else
            {
                this.txtPLotID.Text = "";
            }
        }

        private void txtPLotID_Click(object sender, EventArgs e)
        {
            if (this.chkPLotID.Checked)
            {
                txtPLotID.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("작지번호 입력", "번호");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtPLotID.Text = keypad.tbInputText.Text;
                    procQuery();
                }
            }
            else
            {
                chkPLotID.Checked = true;
                txtPLotID.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("작지번호 입력", "번호");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtPLotID.Text = keypad.tbInputText.Text;
                    procQuery();
                }
            }
        }

        private void chkInsDate_Click(object sender, EventArgs e)
        {
            if (chkInsDate.Checked)
            {
                mtb_From.Enabled = true;
                mtb_To.Enabled = true;
                btnCal_From.Enabled = true;
                btnCal_To.Enabled = true;
            }
            else
            {
                mtb_From.Enabled = false;
                mtb_To.Enabled = false;
                btnCal_From.Enabled = false;
                btnCal_To.Enabled = false;
            }
        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((DataGridView)(sender)).SelectedRows.Count > 0)
            {
                int i = ((DataGridView)(sender)).SelectedCells[0].RowIndex;
                if (((DataGridView)(sender)).Rows[i].Cells["OrderID"].Value.ToString().Trim().Length > 0)// != "")
                {
                    Frm_tprc_Main.g_tBase.OrderID = ((DataGridView)(sender)).Rows[i].Cells["OrderID"].Value.ToString();
                }
                Frm_tprc_Main.g_tBase.sLotID = ((DataGridView)(sender)).Rows[i].Cells["LotID"].Value.ToString();
                Frm_tprc_Main.g_tBase.sInstID = ((DataGridView)(sender)).Rows[i].Cells["InstID"].Value.ToString();
            }
        }

        

        // 설비점검 및 자주검사 체크로직. >> set process 화면 생략에 따른 조치.
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

            strMachine = Frm_tprc_Main.gs.GetValue("Work", "Machine", "");

            if (strMachine != "")
            {
                MachineTemp = strMachine.Split('|');//머신
                foreach (string str in MachineTemp)
                {
                    if (str == strProcessID + Frm_tprc_Main.g_tBase.MachineID)
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
                                    if (bFirst)
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
                                if (Frm_tprc_Main.g_tBase.ProcessID == "0405")
                                {
                                    if (intResult < 4)
                                    {
                                        Message[0] = "[설비점검 오류]";
                                        Message[1] = "CMB/혼련 공정에서는 총 4기기의 설비점검을 모두 하셔야합니다.";
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
                                        //Message[0] = "[자주검사 오류]";
                                        //Message[1] = strMessageInspect + "의 자주검사를 하셔야합니다.";
                                        //WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                                        //timer1.Stop();
                                        // 2019.0305 허윤구 >> 2019.0320 허윤구, 자주검사 체크 안한다 했음.
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


        // Q_Point
        private void btnQ_Point_Click(object sender, EventArgs e)
        {
            Message[0] = "[공사중]";
            Message[1] = string.Format("준비중입니다.\r\n기능은 관리자에게 문의하세요.");
            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
        }


        #region(통신페이지로 넘어가도 좋은지 체크) btnPLC_Transfer_AccessCheck_YN
        private bool btnPLC_Transfer_AccessCheck_YN()
        {
            // 1. 통신을 볼려면.. 성형이어야 겠지?
            if (grdData.SelectedRows.Count > 0 && grdData.SelectedRows.Count == 1)
            {
                string ChkProcess = grdData.SelectedRows[0].Cells["ProcessID"].Value.ToString();
                if (ChkProcess == "1101")
                {
                    // 재단은 필요없자나..
                    WizCommon.Popup.MyMessageBox.ShowBox("선택한 작지는 재단 작업지시입니다. \r\n 금형변경 버튼을 선택할 수 없습니다.", "[공정오류]", 0, 1);
                    return false;
                }
                return true;
            }
            else
            { return false; }
        }

        #endregion


        // 그리드 헤더 컬럼 셀렉션 ASC, DESC 이벤트 먹이기. 2019.05.13 허윤구
        private void grdData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int point = e.ColumnIndex;
            if (grdData.SortOrder.ToString() == "Ascending") // Check if sorting is Ascending
            {
                grdData.Sort(grdData.Columns[point], ListSortDirection.Descending);
            }
            else
            {
                grdData.Sort(grdData.Columns[point], ListSortDirection.Ascending);
            }
        }

        #region 하단의 작업 버튼 세팅 메서드 모음

        private void setWorkingButtons()
        {
            btnNextWork.Tag = null;

            // 테이블 레아이웃 칸 나누기 및 세팅
            createWorkTabelLayoutPanel();

            // 작업 버튼 생성
            createWorkButtons();

            // 생성한 버튼을 레이아웃에 등록
            setWorkButtonsInTableLayoutPanel();

            // 작업 중인 버튼 → 노란색 활성화
            // 작업중 아닌 버튼 → 비활성화
            setWorkButtons_isWorking();
        }

        private void setWorkingButtons_NextAndBeforeButtons()
        {
            // 모든 컴포넌트 Dock → Fill 로 바꾸면서 Margin 도 기본 1로 두기 때문에 따로 조정
            btnNextWork.Margin = new Padding(2, 7, 2, 7);
            btnBeforeWork.Margin = new Padding(2, 7, 2, 7);
            btnNextWork.Tag = "1";

            // 테이블 레아이웃 칸 나누기 및 세팅
            createWorkTabelLayoutPanel();

            // 작업 버튼 생성
            createWorkButtons();

            // 생성한 버튼을 레이아웃에 등록
            setWorkButtonsInTableLayoutPanel();

            // 작업 중인 버튼 → 노란색 활성화
            // 작업중 아닌 버튼 → 비활성화
            setWorkButtons_isWorking();
        }

        // 작업 버튼 Refresh (작업버튼 생성을 제외환)
        private void refreshWorkingButtons()
        {
            // 작업 버튼 생성
            createWorkButtons();

            // 생성한 버튼을 레이아웃에 등록
            setWorkButtonsInTableLayoutPanel();

            // 작업 중인 버튼 → 노란색 활성화
            // 작업중 아닌 버튼 → 비활성화
            setWorkButtons_isWorking();
        }

        // 하위 작업버튼 테이블 레이아웃 패널 생성
        private void createWorkTabelLayoutPanel()
        {
            // 하위 작업 버튼을 쪼개서 넣을 패널 생성 → 공용 변수로 등록
            //TableLayoutPanel tlpWorkingButtons = new TableLayoutPanel();
            tlpWorkingButtons.Dock = DockStyle.Fill;

            //int btnCount = 9; // 버튼 갯수
            float widthPercent = 100.0F / btnCount; // 폭 퍼센트 설정 = 100 / 버튼갯수

            // 작업 패널 생성
            for (int i = 0; i < btnCount; i++)
            {
                // 이상하게 마지막 버튼만 크다??? → 해서 조정
                //if (i == btnCount - 1) { widthPercent -= 1; }

                tlpWorkingButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, widthPercent));
            }

            // [생성한 버튼을 넣은 패널]을 
            // [하위 패널]에 등록
            tlpBottom.Controls.Add(tlpWorkingButtons, 1, 0);
        }

        // 작업버튼 생성
        private void createWorkButtons()
        {
            // 1. 환경설정에 설정된 공정 + 호기 데이터 가져오기
            // 2. 버튼 생성 → lstButtons 에 추가
            // 3. btnCount = 9 → 9의 배수로 생성 (ex : 2번이 15개 → 4개는 작업7~9 버튼 생성해서 추가)
            try
            {
                lstButtons.Clear();

                // 전체 / 부분전체 / 공정1 ... ← 공정이 2개 이상이면 부분전체 / 공정이 하나면 그 하나의 공정만 세팅
                string sProcessID = Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "Non");
                string sMachineID = Frm_tprc_Main.gs.GetValue("Work", "Machine", "Non");

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("sProcessID", sProcessID);  // 공정 세팅
                sqlParameter.Add("sMachineID", sMachineID);  // 호기 세팅

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sWorkButtonsBySetting", sqlParameter, false);

                // 환경설정 - 호기와 매칭 되는 것들만 모아 놓기
                DataTable dtButtons = new DataTable();

                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("환경설정에서 공정과 호기가 설정되지 않았습니다.", "작업 버튼 설정 오류(setWorkButtons_BySetting)", 0, 1);
                        fillEmptyWorkButtons();
                        return;
                    }
                    else
                    {
                        int index = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            index++;

                            var wButtons = new WorkButtonsBySetting_CodeView()
                            {
                                ProcessID = dr["ProcessID"].ToString(),
                                Process = dr["Process"].ToString(),
                                MachineID = dr["MachineID"].ToString(),
                                Machine = dr["Machine"].ToString(),
                                MachineNo = dr["MachineNo"].ToString()
                            };

                            Button btnWork = new Button();
                            btnWork.Name = "btnWork" + index;
                            btnWork.Text = wButtons.MachineNo.Trim() + "\r\n" + wButtons.Process;
                            btnWork.Tag = wButtons.ProcessID + wButtons.MachineID;

                            btnWork.Click += new System.EventHandler(this.btnWorkButton_Click);

                            btnWork.Enabled = false;
                            btnWork.Dock = DockStyle.Fill;
                            btnWork.BackColor = SystemColors.Control;
                            btnWork.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
                            btnWork.TextAlign = ContentAlignment.MiddleCenter;
                            btnWork.UseVisualStyleBackColor = true;

                            // 리스트에 저장
                            lstButtons.Add(btnWork);
                        }
                    }
                }

                // 빈 버튼으로 나머지 채우기
                //fillEmptyWorkButtons();

                // 버튼 생성 후 버튼 갯수가 9개 미만이면 다음 작업 버튼은 안보이도록
                //setBeforeAndNextWorkButton();
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해 주세요.\r\n" + ex.Message, "작업 버튼 생성 오류(createWorkButtons)", 0, 1);
            }
            finally
            {
                // 빈 버튼으로 나머지 채우기
                fillEmptyWorkButtons();

                // 버튼 생성 후 버튼 갯수가 9개 미만이면 다음 작업 버튼은 안보이도록
                setBeforeAndNextWorkButton();
            }
        }

        // 이전 다음 버튼 세팅
        // 버튼 생성 후 버튼 갯수가 9개 미만이면 다음 작업 버튼은 안보이도록
        private void setBeforeAndNextWorkButton()
        {
            if (btnNextWork.Tag == null)
            {
                return;
            }

            if (lstButtons.Count <= btnCount) // 9개 보다 작다면 → 이전 다음 버튼 숨기기
            {
                btnBeforeWork.Visible = false;
                btnNextWork.Visible = false;

                if (tlpBottom.ColumnStyles.Count > 2)
                {
                    tlpBottom.ColumnStyles[0].Width = 1F;
                    tlpBottom.ColumnStyles[1].Width = 98F;
                    tlpBottom.ColumnStyles[2].Width = 1F;
                }
            }
            else // 9개 보다 크다면 → 버튼 Seq 에 따라 서 이전 다음 버튼 숨기기
            {
                int index = 1;
                int max = lstButtons.Count / btnCount;

                if (btnNextWork.Tag != null)
                {
                    Int32.TryParse(btnNextWork.Tag.ToString(), out index);
                }

                // 첫번째 Seq 면 이전 버튼 숨기기
                if (index == 1)
                {
                    btnBeforeWork.Visible = false;
                    btnNextWork.Visible = true;

                    if (tlpBottom.ColumnStyles.Count > 2)
                    {
                        tlpBottom.ColumnStyles[0].Width = 1F;
                        tlpBottom.ColumnStyles[1].Width = 94F;
                        tlpBottom.ColumnStyles[2].Width = 5F;
                    }
                }
                // 마지막 Seq 면 다음 버튼 숨기기
                else if (index == max)
                {
                    btnBeforeWork.Visible = true;
                    btnNextWork.Visible = false;

                    if (tlpBottom.ColumnStyles.Count > 2)
                    {
                        tlpBottom.ColumnStyles[0].Width = 5F;
                        tlpBottom.ColumnStyles[1].Width = 94F;
                        tlpBottom.ColumnStyles[2].Width = 1F;
                    }
                }
                // 중간이면 둘다 활성화
                else if (index > 1 && index < max)
                {
                    btnBeforeWork.Visible = true;
                    btnNextWork.Visible = true;

                    if (tlpBottom.ColumnStyles.Count > 2)
                    {
                        tlpBottom.ColumnStyles[0].Width = 5F;
                        tlpBottom.ColumnStyles[1].Width = 90F;
                        tlpBottom.ColumnStyles[2].Width = 5F;
                    }
                }
                // 이도 저도 아니면 없애기
                else
                {
                    btnBeforeWork.Visible = false;
                    btnNextWork.Visible = false;

                    if (tlpBottom.ColumnStyles.Count > 2)
                    {
                        tlpBottom.ColumnStyles[0].Width = 1F;
                        tlpBottom.ColumnStyles[1].Width = 98F;
                        tlpBottom.ColumnStyles[2].Width = 1F;
                    }
                }
            }
        }

        // 작업버튼 갯수가 9(버튼 갯수 설정 값)의 배수가 아니면 빈 버튼 채우기
        private void fillEmptyWorkButtons()
        {
            int val = lstButtons.Count % btnCount == 0 ? btnCount : lstButtons.Count % btnCount;
            if (lstButtons.Count == 0) { val = 0; }
            for (int i = val; i < btnCount; i++)
            {
                Button btnWork = new Button();
                btnWork.Name = "btnWork" + (i + 1);
                btnWork.Text = "작업" + (i + 1);
                btnWork.Tag = "";

                btnWork.Click += new System.EventHandler(this.btnWorkButton_Click);

                btnWork.Enabled = false;
                btnWork.Dock = DockStyle.Fill;
                btnWork.BackColor = SystemColors.Control;
                btnWork.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
                btnWork.TextAlign = ContentAlignment.MiddleCenter;
                btnWork.UseVisualStyleBackColor = true;

                // 리스트에 저장
                lstButtons.Add(btnWork);
            }
        }

        // 만든 작업 버튼 세팅
        private void setWorkButtonsInTableLayoutPanel()
        {
            try
            {
                tlpWorkingButtons.Controls.Clear();

                int index = 1;
                if (btnNextWork.Tag != null)
                {
                    Int32.TryParse(btnNextWork.Tag.ToString(), out index);
                }

                for (int i = btnCount * (index - 1); i <= btnCount * index - 1; i++)
                {
                    tlpWorkingButtons.Controls.Add(lstButtons[i], i - ((index - 1) * btnCount), 0);
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해 주세요.\r\n" + ex.Message, "작업 버튼 세팅(setWorkButtonsInTableLayoutPanel)", 0, 1);
            }
        }

        // 작업 중인 호기는 노란색 + 버튼 활성화 / 나머지는 비활성화
        private void setWorkButtons_isWorking()
        {
            try
            {
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sNowWorking", null, false);

                if (dt.Rows != null
                    && dt.Rows.Count > 0)
                {
                    // WorkButtonsBySetting_CodeView
                    foreach(DataRow dr in dt.Rows)
                    {
                        var WorkB = new WorkButtonsBySetting_CodeView()
                        {
                            LabelID = dr["LabelID"].ToString(),
                            ProcessID = dr["ProcessID"].ToString(),
                            Process = dr["Process"].ToString().Trim(),
                            MachineID = dr["MachineID"].ToString(),
                            MachineNo = dr["MachineNo"].ToString().Trim(),

                            WorkStartDate = dr["WorkStartDate"].ToString(),
                            WorkStartTime = dr["WorkStartTime"].ToString(),
                            WorkPersonID = dr["WorkPersonID"].ToString(),
                            Name = dr["Name"].ToString()
                        };

                        for(int i = 0; i < lstButtons.Count; i++)
                        {
                            // 공정과 호기가 같으면!
                            if (lstButtons[i].Tag.ToString() != ""
                                && lstButtons[i].Tag.ToString().Equals(WorkB.ProcessID + WorkB.MachineID))
                            {
                                //  btnMoldW1.BackColor = Color.Yellow;
                                lstButtons[i].BackColor = Color.Yellow;
                                lstButtons[i].Text = WorkB.Process + "\r\n" + WorkB.MachineNo + "\r\n" + WorkB.Name + "\r\n" + "작업중";
                                lstButtons[i].Tag = WorkB.LabelID + "|" + WorkB.ProcessID + "|" + WorkB.MachineID;
                                lstButtons[i].Enabled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해 주세요.\r\n" + ex.Message, "작업 버튼 매칭 오류(setWorkButtons_isWorking)", 0, 1);
            }
        }

        #endregion // 하단의 작업 버튼 세팅 메서드 모음

        #region 하단 작업 버튼 클릭 이벤트 모음

        // 버튼 클릭 이벤트
        private void btnWorkButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnSender = sender as Button;
                if (btnSender.Tag != null
                    && btnSender.Tag.ToString().Split('|').Length > 2)
                {
                    string[] arr = btnSender.Tag.ToString().Split('|');

                    string LabelID = arr[0];
                    string ProcessID = arr[1];
                    string MachineID = arr[2];

                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("LabelID", LabelID); 
                    sqlParameter.Add("ProcessID", ProcessID);
                    sqlParameter.Add("MachineID", MachineID);

                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sWorkingEndTarget", sqlParameter, false);

                    if (dt.Rows.Count == 0)
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("작업 진행중인 공정 LotID가 아닙니다. {" + LabelID + "} 관리자에게 문의해 주세요!", "[Start 데이터 서치오류]", 2, 1);
                        return;
                    }
                    else
                    {
                        // 값 받기
                        string InstID = dt.Rows[0]["InstID"].ToString();
                        string InstDetSeq = dt.Rows[0]["InstDetSeq"].ToString();
                        string wrLabelID = dt.Rows[0]["LabelID"].ToString();
                        string wrProcessID = dt.Rows[0]["ProcessID"].ToString();
                        string Process = dt.Rows[0]["Process"].ToString();

                        string wrMachineID = dt.Rows[0]["MachineID"].ToString();
                        string MachineNo = dt.Rows[0]["MachineNo"].ToString();
                        string PersonID = dt.Rows[0]["WorkPersonID"].ToString();
                        string Name = dt.Rows[0]["Name"].ToString();
                        string CT = dt.Rows[0]["CT"].ToString();

                        string DayOrNightID = dt.Rows[0]["DayOrNightID"].ToString();
                        string WorkStartDate = dt.Rows[0]["WorkStartDate"].ToString();
                        string WorkStartTime = dt.Rows[0]["WorkStartTime"].ToString();
                        string ArticleID = dt.Rows[0]["ArticleID"].ToString();
                        string JobID = dt.Rows[0]["JobID"].ToString();

                        // 세팅
                        Frm_tprc_Main.g_tBase.sInstID = InstID;
                        Frm_tprc_Main.g_tBase.sInstDetSeq = InstDetSeq;
                        Frm_tprc_Main.g_tBase.sLotID = LabelID;
                        Frm_tprc_Main.g_tBase.MachineID = wrMachineID;
                        Frm_tprc_Main.g_tBase.Machine = MachineNo;

                        Frm_tprc_Main.g_tBase.ProcessID = wrProcessID;
                        Frm_tprc_Main.g_tBase.Process = Process;
                        Frm_tprc_Main.g_tBase.PersonID = PersonID;
                        Frm_tprc_Main.g_tBase.Person = Name;
                        Frm_tprc_Main.g_tBase.DayOrNightID = DayOrNightID;
                        Frm_tprc_Main.g_tBase.sArticleID = ArticleID;

                        Set_stbInfo();

                        // 하위 라벨 리스트 
                        List<string> lstStartLabel = new List<string>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lstStartLabel.Add(dt.Rows[i]["ChildLabelID"].ToString());
                        }

                        frm_tprc_Work_U workForm = new frm_tprc_Work_U(JobID, wrProcessID, lstStartLabel, WorkStartDate, WorkStartTime, DayOrNightID, CT);
                        Form form = workForm;

                        if (form != null)
                        {
                            foreach (Form openForm in Application.OpenForms)//중복실행방지
                            {
                                if (openForm.Name == form.Name)
                                {
                                    openForm.BringToFront();
                                    openForm.Activate();
                                    return;
                                }
                            }
                            form.MdiParent = this.ParentForm;
                            form.TopLevel = false;
                            form.Dock = DockStyle.Fill;

                            form.BringToFront();
                            form.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해 주세요.\r\n" + ex.Message, "작업 버튼 클릭 오류(btnWorkButton_Click)", 0, 1);
            }
        }

        // 이전 버튼 클릭 이벤트
        private void btnBeforeWork_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 1;

                if (btnNextWork.Tag != null)
                {
                    Int32.TryParse(btnNextWork.Tag.ToString(), out index);
                }

                if (index < 1) { return; }

                index--;

                btnNextWork.Tag = index;

                // 리프레쉬
                setWorkButtonsInTableLayoutPanel();
                setWorkButtons_isWorking();
                setBeforeAndNextWorkButton();
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해 주세요.\r\n" + ex.Message, "이전 작업버튼 클릭 오류(btnBeforeWork_Click)", 0, 1);
            }
        }

        // 다음 버튼 클릭 이벤트
        private void btnNextWork_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 1;

                if (btnNextWork.Tag != null
                    && Int32.TryParse(btnNextWork.Tag.ToString(), out index) == true)
                {
                    index++;
                }

                int max = lstButtons.Count;
                if (max == (index - 1) * btnCount) { return; }

                btnNextWork.Tag = index;

                // 리프레쉬
                setWorkButtonsInTableLayoutPanel();
                setWorkButtons_isWorking();
                setBeforeAndNextWorkButton();
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해 주세요.\r\n" + ex.Message, "다음 작업버튼 클릭 오류(btnNextWork_Click)", 0, 1);
            }
        }

        #endregion

        #region 이거 필요가 없네.. 허헣
        // [호기]정보(공정 + 호기)를 공정 / 호기로 분리하기
        private List<string> getSplitProcessIDAndMachineID(string str)
        {
            List<string> lstResult = new List<string>();

            string[] strArray = str.Split('|');
            string ProcessIDS = "";
            string MachineIDS = "";
            for(int i = 0; i < strArray.Length; i++)
            {
                string val = strArray[i];
                if (val.Length == 6)
                {
                    if (i == 0)
                    {
                        ProcessIDS = val.Substring(0, 4); // 공정
                        MachineIDS = val.Substring(4, 2); // 호기
                    }
                    else
                    {
                        ProcessIDS += "|" + val.Substring(0, 4); // 공정
                        MachineIDS += "|" + val.Substring(4, 2); // 호기
                    }
                }
            }

            lstResult.Add(ProcessIDS);
            lstResult.Add(MachineIDS);

            return lstResult;
        }
        #endregion

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

        private void grdData_SelectionChanged(object sender, EventArgs e)
        {
            //if (grdData.SelectedRows.Count > 0 && grdData.SelectedRows.Count == 1)
            //{
            //    dgvr = grdData.SelectedRows[0];
            //    if (dgvr.Cells["ProcessID"].Value.ToString() == "2101" && dgvr.Cells["SHWorkingYN"].Value.ToString() == "Y")
            //    {
            //        lblSHWorkingYN.Visible = true;
            //    }
            //    else
            //    {
            //        lblSHWorkingYN.Visible = false;
            //    }
            //}
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (grdData.SelectedRows.Count > 0 && grdData.SelectedRows.Count == 1)
            {
                dgvr = null;
                dgvr = grdData.SelectedRows[0];                

                strLotID = "";
                strMachineID = "";
                strProcessID = "";
                strInstID = "";
                strInstDetSeq = "";

                strLotID = grdData.SelectedRows[0].Cells["LotID"].Value.ToString();
                strMachineID = grdData.SelectedRows[0].Cells["MachineID"].Value.ToString().Trim();
                strProcessID = grdData.SelectedRows[0].Cells["ProcessID"].Value.ToString().Trim();
                strInstID = grdData.SelectedRows[0].Cells["InstID"].Value.ToString();
                strInstDetSeq = grdData.SelectedRows[0].Cells["InstDetSeq"].Value.ToString();
                string strArticleID = grdData.SelectedRows[0].Cells["ArticleID"].Value.ToString();

                Frm_tprc_Main.g_tBase.sInstID = strInstID;
                Frm_tprc_Main.g_tBase.sLotID = strLotID;
                Frm_tprc_Main.g_tBase.MachineID = strMachineID;
                Frm_tprc_Main.g_tBase.ProcessID = strProcessID;
                Frm_tprc_Main.g_tBase.sInstDetSeq = strInstDetSeq;
                Frm_tprc_Main.g_tBase.sInstDetSeq = strInstDetSeq;
                Frm_tprc_Main.g_tBase.sArticleID = strArticleID;

                Frm_tprc_Main.g_tBase.tempDate = mtb_From.Text;

                Frm_tprc_Main.list_tMold = new List<TMold>();

                SetsetProcessFormLoad();

                return;
            }
            else
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 작업지시가 없습니다.", "[오류]", 0, 1);
            }
        }


        public void SetPreScanPopUpLoad(string processid, string machindid, string moldid)
        {
            //if (ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq) == 1)
            //{
            //    frm_PopUp_PreScanWork4 FPPSW = new frm_PopUp_PreScanWork4(processid, machindid, moldid);
            //    FPPSW.StartPosition = FormStartPosition.CenterScreen;
            //    FPPSW.BringToFront();
            //    FPPSW.TopMost = true;

            //    if (FPPSW.ShowDialog() == DialogResult.OK)
            //    {
            //        // ok라는건, 새로운 시작처리가 하나 있다는 것.
            //        // re_search.
            //        procQuery();
            //        refreshWorkingButtons();
            //    }
            //}
            //else
            //{
            //금형선택
            //일단 무조건 금형선택 띄우기
            Frm_tprc_Mold_Q FTMQ = new Frm_tprc_Mold_Q(Frm_tprc_Main.g_tBase.sLotID, Frm_tprc_Main.g_tBase.Process, machindid);
            FTMQ.StartPosition = FormStartPosition.CenterScreen;
            FTMQ.BringToFront();
            FTMQ.TopMost = false;

            if (FTMQ.ShowDialog() == DialogResult.OK)
            {
                frm_PopUp_PreScanWork FPPSW = new frm_PopUp_PreScanWork(processid, machindid, moldid);
                FPPSW.StartPosition = FormStartPosition.CenterScreen;
                FPPSW.BringToFront();
                FPPSW.TopMost = true;

                // 라벨 리스트 초기화
                Frm_tprc_Main.lstStartLabel = new List<string>();
                if (FPPSW.ShowDialog() == DialogResult.OK)
                {
                    procQuery();
                    refreshWorkingButtons();

                    // 둘리 : 대구산업 → 바로 실적 화면으로 이동
                    //WorkingMold(machindid);
                }
            }
            //}


        }

        #region 바로 실적등록으로 이동하기 위한 메서드 : 둘리 : 대구산업용

        // 노란색으로 색칠된 작업 진행중인 아이를 클릭했어요.
        // 중간과정을 모두 생략하고(처음 작업 시작할때 했으니까)
        // 바로 (work_U) 로 보내버려요.
        private void WorkingMold(string MachineID)
        {

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("LotID", Frm_tprc_Main.g_tBase.sLotID);  // 유사 로트번호
            sqlParameter.Add("MachineID", MachineID);  // 2020.02.24 둘리
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sForInsertWorkData", sqlParameter, false);

            if (dt.Rows.Count == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("작업지시 라벨이 잘못되었습니다. {" + Frm_tprc_Main.g_tBase.sLotID + "} 관리자에게 문의해 주세요!", "[Start 데이터 서치오류]", 2, 1);
                return;
            }
            else
            {
                string strInstID = dt.Rows[0]["InstID"].ToString();
                string prodlotid = dt.Rows[0]["LabelID"].ToString();
                string strMachineID = dt.Rows[0]["MachineID"].ToString();
                string strMachine = dt.Rows[0]["MachineNo"].ToString();
                string strprocessid = dt.Rows[0]["ProcessID"].ToString();
                string strProcess = dt.Rows[0]["Process"].ToString();
                string strInstDetSeq = dt.Rows[0]["InstDetSeq"].ToString();
                string strCT = dt.Rows[0]["CT"].ToString(); // 2020.03.03 둘리 추가

                // 불나방도 최소한의 대비는 하고 가야지. _ g_tBase 값 Update.
                Frm_tprc_Main.g_tBase.sInstID = strInstID;
                Frm_tprc_Main.g_tBase.sLotID = prodlotid;
                Frm_tprc_Main.g_tBase.MachineID = strMachineID;
                Frm_tprc_Main.g_tBase.Machine = strMachine;
                Frm_tprc_Main.g_tBase.ProcessID = strprocessid;
                Frm_tprc_Main.g_tBase.Process = strProcess;
                Frm_tprc_Main.g_tBase.sInstDetSeq = strInstDetSeq;

                Set_stbInfo();

                // 2020.02.24 둘리 : 해당 공정 선택했을시 하단의 텍스트 수정 되도록!!!!! 일단 공정과 설비만

                Form form = null;
                frm_tprc_Work_U child8 = new frm_tprc_Work_U(strprocessid, strCT);
                form = child8;

                if (form != null)
                {
                    foreach (Form openForm in Application.OpenForms)//중복실행방지
                    {
                        if (openForm.Name == form.Name)
                        {
                            openForm.BringToFront();
                            openForm.Activate();
                            return;
                        }
                    }
                    form.MdiParent = this.ParentForm;
                    form.TopLevel = false;
                    form.Dock = DockStyle.Fill;

                    form.BringToFront();
                    form.Show();
                }
            }
        }

        #endregion

        #endregion


        // 툴 교환 등록 버튼 클릭 이벤트 → 툴 교환 팝업 띄우기
        private void btnToolChange_Click(object sender, EventArgs e)
        {
            frm_tprc_setProcess Set_ps = new frm_tprc_setProcess(true);//NoWork == true라는 bool값
            Frm_tprc_Main main = new Frm_tprc_Main();
            Set_ps.Owner = main;
            if (Set_ps.ShowDialog() == DialogResult.OK)
            {
                // Tool 교환 등록 띄우기
                SetToolPopUpLoad();
            };
        }

        // 작업지도서 확인 버튼 클릭 이벤트 > 작업지도서 확인이 가능한 그림파일 띄어주기.
        private void btnWorkOrderJPG_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdData.SelectedRows.Count > 0 && grdData.SelectedRows.Count == 1)
                { 

                    string ArticleID = grdData.SelectedRows[0].Cells["ArticleID"].Value.ToString();

                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("ArticleID", ArticleID);//cboProcess.Text 

                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sWorkOrderRoute", sqlParameter, false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        Frm_PopUp_ImgArticleInfo_CodeView ArticleImg = new Frm_PopUp_ImgArticleInfo_CodeView()
                        {
                            Sketch1File = dr["Sketch1File"].ToString(),
                            Sketch1Path = dr["Sketch1Path"].ToString(),
                            Sketch2File = dr["Sketch2File"].ToString(),
                            Sketch2Path = dr["Sketch2Path"].ToString(),
                            Sketch3File = dr["Sketch3File"].ToString(),
                            Sketch3Path = dr["Sketch3Path"].ToString(),
                            Sketch4File = dr["Sketch4File"].ToString(),
                            Sketch4Path = dr["Sketch4Path"].ToString(),
                            Sketch5File = dr["Sketch5File"].ToString(),
                            Sketch5Path = dr["Sketch5Path"].ToString(),
                            Sketch6File = dr["Sketch6File"].ToString(),
                            Sketch6Path = dr["Sketch6Path"].ToString(),
                        };

                        // Frm_PopUp_ImgArticleInfo
                        Frm_PopUp_ImgArticleInfo IWO = new Frm_PopUp_ImgArticleInfo(ArticleImg);
                        IWO.StartPosition = FormStartPosition.CenterParent;
                        IWO.Show();
                    }
                }
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해주세요.\r\n" + ex.Message, "[품명 이미지 조회 오류]", 2, 1);
                return;
            }
        }

        private void btnFunctionPrepare(object sender, EventArgs e)
        {
            WizCommon.Popup.MyMessageBox.ShowBox("기능이 준비중에 있습니다.", "[준비중]", 2, 1);
            return;
        }



        private void SetToolPopUpLoad()
        {
            frm_tprc_UseTool_U Tool = new frm_tprc_UseTool_U();
            Tool.StartPosition = FormStartPosition.CenterScreen;
            Tool.BringToFront();
            //Tool.TopMost = true;

            if (Tool.ShowDialog() == DialogResult.OK)
            {

            }
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

        // 시간 형식 6글자라면! 11:11:11
        private string DateTimeFormat(string str)
        {
            str = str.Replace(":", "").Trim();

            if (str.Length == 6)
            {
                string Hour = str.Substring(0, 2);
                string Min = str.Substring(2, 2);
                string Sec = str.Substring(4, 2);

                str = Hour + ":" + Min + ":" + Sec;
            }

            return str;
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

        #region 공정작업에서 자주검사 이동 모음 - btnInspectAuto_Click(), CheckIsInspectAuto()

        // 한민테크 7월 요청사항 → 자주검사를 작업지시서 라벨 스캔없이 할 수 있도록
        // → 공정작업에서 해당 작업지시를 선택하여 자주검사를 시행하면?
        private void btnInspectAuto_Click(object sender, EventArgs e)
        {
            try
            {
                string LotID = grdData.SelectedRows[0].Cells["LOTID"].Value.ToString();

                if (CheckIsInspectAuto(LotID))
                {
                    Frm_tprc_Main.OpenInspectAuto(LotID);
                }

            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        // 자주검사 등록이 되어 있는지 체크
        private bool CheckIsInspectAuto(string LotID)
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("LotID", LotID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sCheckInspectAuto", sqlParameter, false);

                if (dt != null 
                    && dt.Rows.Count > 0
                    && dt.Columns.Count == 1)
                {
                    if (dt.Rows[0]["Msg"].ToString().Equals("PASS"))
                    {
                        flag = true;
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox(dt.Rows[0]["Msg"].ToString(), "[자주검사 이동 오류 - btnInspectAuto_Click]", 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }

            return flag;
        }

        #endregion

        private void pnlForm_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    #region CodeView

    // 작업 버튼을 위한 클래스
    class WorkButtonsBySetting_CodeView
    {
        public string MachineID { get; set; }
        public string Machine { get; set; }
        public string MachineNo { get; set; }
        public string SetHitCount { get; set; }
        public string ProductLocID { get; set; }
        public string ProcessID { get; set; }
        public string Process { get; set; }
        public string CommStationNo { get; set; }
        public string CommIP { get; set; }

        public string LabelID { get; set; }
        public string WorkStartDate { get; set; }
        public string WorkStartTime { get; set; }
        public string WorkPersonID { get; set; }
        public string Name { get; set; }
    }

    #endregion
}
