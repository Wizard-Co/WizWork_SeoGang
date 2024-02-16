using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Microsoft.VisualBasic;
using System.Management;
using System.Printing;
using System.Data.SqlClient;
using WizCommon;
using System.Runtime.InteropServices;
using System.Collections;

namespace WizWork
{
    public partial class frm_tprc_Work_U : Form
    {
        private string m_LogID = "";


        private string updateJobID = "";        // 물고 들어간 Job ID
        private string m_ProcessID = "";
        private string m_LabelID = "";          // Start Save Label ID
        private List<string> lstStartLabel = new List<string>();
        private string ProdQty = "";

        // 생산박스당 수량
        private double ProdQtyPerBox = 0;

        private string m_WorkStartDate = "";
        private string m_WorkStartTime = "";

        /////////////////////////////
        ///
        private string m_MachineName = "";
        private string m_MachineID = "";
        private string m_LotID = "";
        private string m_MtrExceptYN = "";
        private string m_OutwareExceptYN = "";

        private string m_LastArticleYN = "";
        private string m_OrderID = "";
        private int m_OrderSeq = 0;
        private string m_OrderNO = "";
        private string m_UnitClss = "";
        private string m_UnitClssName = "";

        private string m_EffectDate = "";
        private string m_ProdAutoInspectYN = "";
        private string m_OrderArticleID = "";
        private string m_ArticleID = "";
        private string m_LabelGubun = "";

        private string m_Inspector = "";
        private double m_RemainQty = 0;   //      입고수량
        private double m_LocRemainQty = 0;  //    '자품목 현 재고량
        private double m_douReqQty = 0; //현재품목 소요량
        private double m_douProdCapa = 0;

        private string m_CycleTime = "";

        // 앞선 1번의 결과물을 알아야 뒤 라벨에 이 값을 그대로 박아 넣을 수 있으니까 가져와야 해.
        private string Wh_Ar_DayOrNightID = ""; // 주간이냐 / 야간이냐. 

        //// PL_InputDet_SEQ 가 1이더라도, 라벨프린트 발행여부 에 따라, 뽑을지 말지 결정해야 한다.
        //private string Wh_Ar_LabelPrintYN = "";


        INI_GS gs = new INI_GS();
        //public Sub_TMold[] Sub_TMold = null;
        public Sub_TWkResult Sub_TWkResult = new Sub_TWkResult();
        public Sub_TWkResultArticleChild Sub_TWkResultArticleChild = new Sub_TWkResultArticleChild();
        public Sub_TWkLabelPrint Sub_TWkLabelPrint = new Sub_TWkLabelPrint();
        public Sub_TtdChange Sub_Ttd = new Sub_TtdChange();
        public TTag Sub_m_tTag = new TTag();
        public TTagSub Sub_m_tItem = new TTagSub();
        public List<TTagSub> list_m_tItem = new List<TTagSub>();
        public List<Sub_TWkResultArticleChild> list_TWkResultArticleChild = new List<Sub_TWkResultArticleChild>();
        public List<Sub_TWkResult> list_TWkResult = new List<Sub_TWkResult>();
        public List<Sub_TWkLabelPrint> list_TWkLabelPrint = new List<Sub_TWkLabelPrint>();
        public List<Sub_TWkResult_SplitAdd> list_TWkResult_SplitAdd = new List<Sub_TWkResult_SplitAdd>();

        public List<Sub_TMold> list_TMold = null;

        List<string> lData = null;
        private string IsTagID = "";
        string[] m_sData = null;
        WizWorkLib Lib = new WizWorkLib();

        private string sTdGbn = "";
        
        private string ChildCheckYN = "N";
        private string LabelPrintYN = "N";      // 공정 간 이동전표를 뽑아햐 합니까? 그냥 저장만 하면 됩니까?
        string[] Message = new string[2];
        
        public bool blPRReuslt = true;  //해당 작업지시의 평량결과가 있는지 없는지
        public bool blpldClose = false; //해당 작업지시에서 현재공정보다 앞공정의 작업실적이 있는지 없는지
        public bool blSHExit = false;     //성형 작업종료 [uwkResult프로시저와 나머지 프로시저 사용]
        public string JobID0401 = "";
        public bool blClose = false;

        // 불량 리스트
        Dictionary<string, frm_tprc_Work_Defect_U_CodeView> dicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();

        // 라벨 인쇄 목록
        List<string> lstPrintLabel = new List<string>();

        public frm_tprc_Work_U()
        {
            InitializeComponent();
        }

        public frm_tprc_Work_U(string JobID, string strProcessID, List<string> lstStartLabel, string WorkStartDate, string WorkStartTime, string DayOrNightID, string CycleTime)
        {
            InitializeComponent();
            updateJobID = JobID;
            m_ProcessID = strProcessID;
            //m_LabelID = StartSaveLabelID;
            this.lstStartLabel = lstStartLabel;
            m_WorkStartDate = WorkStartDate;
            m_WorkStartTime = WorkStartTime;
            m_CycleTime = CycleTime;
            Wh_Ar_DayOrNightID = DayOrNightID;
        }
        
        // 둘리 : 대구산업용 바로 작업실적 화면으로 이동
        public frm_tprc_Work_U(string strProcessID, string CycleTime)
        {
            InitializeComponent();
            m_ProcessID = strProcessID;
            //m_LabelID = StartSaveLabelID;
            this.lstStartLabel = Frm_tprc_Main.lstStartLabel;
            m_WorkStartDate = DateTime.Today.ToString("yyyyMMdd");
            m_WorkStartTime = DateTime.Now.ToString("HHmmss");
            m_CycleTime = CycleTime;
        }

        private void FormLoading()
        {
            m_MachineName = Frm_tprc_Main.g_tBase.Machine;
            m_MachineID = Frm_tprc_Main.g_tBase.MachineID;
            txtCarModel.Text = m_MachineName;

            FormInit();

        }

        private void frm_tprc_Work_U_Load(object sender, EventArgs e)
        {
            FormLoading();
        }

        private void FormInit()
        {
            InitGridData1();
            InitGridData2();
            InitgrdBoxList();

            InitPanel();

            SetFormDataClear();

            Form_Activate();

            //mtb_From.Text = Frm_tprc_Main.g_tBase.tempDate;
            //mtb_To.Text = Frm_tprc_Main.g_tBase.tempDate;
            //dtStartTime.Value = DateTime.Parse("2020-06-14 08:00:00");
            //dtEndTime.Value = DateTime.Parse("2020-06-14 18:00:00");

            // 시간 기본 설정
            //setBaseTimeSetting();
            
        }

        private void setBaseTimeSetting()
        {
            DateTime Now = DateTime.Now;

            // 야간 일 때
            if (Wh_Ar_DayOrNightID != null
                && Wh_Ar_DayOrNightID.Equals("02"))
            {
                //토요일은 16 ~24
                if (Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    dtStartTime.Value = DateTime.Parse("2020-06-14 16:00:00");
                    dtEndTime.Value = DateTime.Parse("2020-06-14 00:00:00");
                }
                // 월~금 일때는 18 ~ 04
                else
                {
                    mtb_To.Text = Now.AddDays(1).ToString("yyyy-MM-dd");
                    dtStartTime.Value = DateTime.Parse("2020-06-14 18:00:00");
                    dtEndTime.Value = DateTime.Parse("2020-06-14 04:00:00");
                }
            }
            else // 주간일 때
            {
                // 토요일은 08 ~ 16
                if (Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    dtStartTime.Value = DateTime.Parse("2020-06-14 08:00:00");
                    dtEndTime.Value = DateTime.Parse("2020-06-14 16:00:00");
                }
                // 월~금 일때는 08 ~ 18
                else
                {
                    dtStartTime.Value = DateTime.Parse("2020-06-14 08:00:00");
                    dtEndTime.Value = DateTime.Parse("2020-06-14 18:00:00");
                }
            }
        }

        #region 패널 Dock → Fill

        private void InitPanel()
        {
            
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Margin = new Padding(1, 1, 1, 1);

            foreach (Control control in tlpForm.Controls)
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
                                        foreach (Control ctl in c.Controls)
                                        {
                                            ctl.Dock = DockStyle.Fill;
                                            ctl.Margin = new Padding(1, 1, 1, 1);
                                            foreach (Control ct in ctl.Controls)
                                            {
                                                ct.Dock = DockStyle.Fill;
                                                ct.Margin = new Padding(1, 1, 1, 1);
                                                foreach (Control cl in ct.Controls)
                                                {
                                                    cl.Dock = DockStyle.Fill;
                                                    cl.Margin = new Padding(1, 1, 1, 1);

                                                }
                                            }
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


        //private void InitgrdWDcar()
        //{
        //    int i = 0;
        //    //grdWDcar.Columns.Clear();
            
        //    foreach (DataGridViewColumn col in GridData1.Columns)
        //    {
        //        col.DataPropertyName = col.Name;
        //        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        col.SortMode = DataGridViewColumnSortMode.NotSortable;
        //    }
        //    return;

        //}

        //private void FillGridwdcard()
        //{
        //    ////xp_work_sWDcar
        //    //int i = 0;
        //    //DataSet ds = null;
        //    //ds = DataStore.Instance.ProcedureToDataSet("xp_work_sWDcar", null, false);
        //    //if (ds.Tables[0].Rows.Count > 0)
        //    //{
                
        //    //}

        //}

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.list_g_tsplit.Clear();
            this.Close();
        }

        private void InitGridData1()
        {
            int i = 0;
            GridData1.Columns.Clear();
            GridData1.ColumnCount = 2;
            // Set the Colums Hearder Names
            GridData1.Columns[i].Name = "RowSeq";
            GridData1.Columns[i].HeaderText = "No";
            //GridData1.Columns[i].Width = 30;
            GridData1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[i].ReadOnly = true;
            GridData1.Columns[i++].Visible = true;

            GridData1.Columns[i].Name = "LotID";
            GridData1.Columns[i].HeaderText = "LotID";
            //GridData1.Columns[i].Width = 140;
            GridData1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData1.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[i].ReadOnly = true;
            GridData1.Columns[i++].Visible = true;

            GridData1.Font = new Font("맑은 고딕", 15);//, FontStyle.Bold);
            GridData1.RowTemplate.Height = 30;
            GridData1.ColumnHeadersHeight = 37;
            GridData1.ScrollBars = ScrollBars.Both;
            GridData1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridData1.MultiSelect = false;
            GridData1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridData1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            GridData1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridData1.ReadOnly = true;

            foreach (DataGridViewColumn col in GridData1.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        private void InitGridData2()
        {
            int i = 0;
            GridData2.Columns.Clear();
            GridData2.ColumnCount = 21;

            // Set the Colums Hearder Names
            GridData2.Columns[i].Name = "RowSeq";
            GridData2.Columns[i].HeaderText = "No";
            //GridData2.Columns[i].Width = 30;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "InstID";
            GridData2.Columns[i].HeaderText = "InstID";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "DetSeq";
            GridData2.Columns[i].HeaderText = "DetSeq";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "ChildSeq";
            GridData2.Columns[i].HeaderText = "ChildSeq";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "ChildArticleID";
            GridData2.Columns[i].HeaderText = "ChildArticleID";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "Article";
            GridData2.Columns[i].HeaderText = "품명";
            //GridData2.Columns[i].Width = 120;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "BuyerArticle";
            GridData2.Columns[i].HeaderText = "품번";
            //GridData2.Columns[i].Width = 120;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "BarCode";
            GridData2.Columns[i].HeaderText = "바코드";
            //GridData2.Columns[i].Width = 120;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "ScanExceptYN";
            GridData2.Columns[i].HeaderText = "체크";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "LabelGubun";
            GridData2.Columns[i].HeaderText = "LabelGubun";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "Flag";
            GridData2.Columns[i].HeaderText = "Flag";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "ScanExceptYN1";
            GridData2.Columns[i].HeaderText = "예외";
            GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "RemainQty";
            GridData2.Columns[i].HeaderText = "Lot 전체창고잔량";
            GridData2.Columns[i].Width = 100;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "LocRemainQty";
            GridData2.Columns[i].HeaderText = "재고량";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "UnitClss";
            GridData2.Columns[i].HeaderText = "하위품단위";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "UnitClssName";
            GridData2.Columns[i].HeaderText = "재고단위";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "ReqQty";
            GridData2.Columns[i].HeaderText = "소모량";
            //GridData2.Columns[i].Width = 40;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "ProdCapa";
            GridData2.Columns[i].HeaderText = "생산가능량";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "ProdUnitClss";
            GridData2.Columns[i].HeaderText = "생산단위";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            GridData2.Columns[i].Name = "ProdUnitClssName";
            GridData2.Columns[i].HeaderText = "생산단위";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = true;

            GridData2.Columns[i].Name = "EffectDate";
            GridData2.Columns[i].HeaderText = "유효기간";
            //GridData2.Columns[i].Width = 80;
            GridData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[i].ReadOnly = true;
            GridData2.Columns[i++].Visible = false;

            //setComboboxCell(i);

            GridData2.Font = new Font("맑은 고딕", 15);//, FontStyle.Bold);
            GridData2.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 12);//, FontStyle.Bold);
            GridData2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridData2.RowTemplate.Height = 30;
            GridData2.ColumnHeadersHeight = 35;
            GridData2.ScrollBars = ScrollBars.Both;
            GridData2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridData2.MultiSelect = false;
            GridData2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridData2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            GridData2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridData2.ReadOnly = false;

            foreach (DataGridViewColumn col in GridData2.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;

        }

        private void setComboboxCell(int columnIndex)
        {
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.HeaderText = "combo";
            combo.Name = "combo";

            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("ID");
            DataColumn dc2 = new DataColumn("Name");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Rows.Add(1, "name1");
            dt.Rows.Add(2, "name2");
            dt.Rows.Add(2, "name3");
            
            combo.DataSource = dt;
            combo.DisplayMember = "Name";
            combo.ValueMember = "ID";

            //GridData2.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(mouseclick);
            GridData2.CellMouseClick += new DataGridViewCellMouseEventHandler(mouseclick);

            GridData2.Columns.Add(combo);
            
        }

        private void mouseclick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //ComboBox ocmb = sender as ComboBox;
            //if (ocmb != null)
            //{
            //    ocmb.DroppedDown = true;
            //}
            var row = this.GridData2.Rows[e.RowIndex];
            var cell = row.Cells[e.ColumnIndex];

            GridData2.CurrentCell = cell;
            GridData2.BeginEdit(true);
            DataGridViewComboBoxEditingControl comboboxEdit = (DataGridViewComboBoxEditingControl)this.GridData2.EditingControl;
            if (comboboxEdit != null)
            {
                comboboxEdit.DroppedDown = true;
            }
    }

        public class Book
        {
            public decimal price;
            public string title;
            public string author;
            public string currency;
        }

        private void dgvComboBoxColumnShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox ocmb = e.Control as ComboBox;
            if (ocmb != null)
            {
                ocmb.Enter -= new EventHandler(ctl_Enter);
                ocmb.Enter += new EventHandler(ctl_Enter);
            }
        }

        void ctl_Enter(object sender, EventArgs e)
        {
            (sender as ComboBox).DroppedDown = true;
        }

        /// <summary>
        /// Box GridList
        /// </summary>
        private void InitgrdBoxList()
        {
            //int i = 0;
            //   grdBoxList.Columns.Clear();           

        }


        // BOM - wk_resultArticleChild에 맞춰서, 
        #region CheckWorkQty 하위품 생산가능량보다 작업수량이 클 경우 잔량이동 팝업 활성화
        private bool CheckWorkQty()
        {
            bool flag = true;

           
            double WorkQty = ConvertDouble(txtWorkQty.Text);

            for (int i = 0; i < GridData2.Rows.Count; i++)
            {
                double CapaQty = ConvertDouble(GridData2.Rows[i].Cells["ProdCapa"].Value.ToString());
                string ChildArticle = GridData2.Rows[i].Cells["Article"].Value.ToString();
                string ChildLabelID = GridData2.Rows[i].Cells["BarCode"].Value.ToString();

                // 생산가능량 보다 작업 수량이 크다면!!
                if (WorkQty > CapaQty)
                {
                    Message[0] = "[하위품 생산가능량 부족]";
                    Message[1] = "하위품의 생산가능량이 부족합니다.\r\n(최대 생산 가능량 : " + CapaQty + " )\r\n잔량이동처리를 하시겠습니까?";
                    if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.OK)
                    {

                        List<ChildLabel> lstChildLabel = new List<ChildLabel>();

                        for (int k = 0; k < GridData2.Rows.Count; k++)
                        {
                            string LabelID = GridData2.Rows[k].Cells["BarCode"].Value.ToString();
                            string ArticleID = GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString();
                            string BuyerArticleNo = GridData2.Rows[k].Cells["Article"].Value.ToString();
                            string Article = GridData2.Rows[k].Cells["BuyerArticle"].Value.ToString();
                            string UnitClss = GridData2.Rows[k].Cells["UnitClss"].Value.ToString();
                            double ReaQty = ConvertDouble(GridData2.Rows[k].Cells["ReqQty"].Value.ToString());
                            double LocRemainQty = ConvertDouble(GridData2.Rows[k].Cells["LocRemainQty"].Value.ToString());

                            ChildLabel cl = new ChildLabel(LabelID, ArticleID, BuyerArticleNo, Article, (float)ReaQty, (float)LocRemainQty, (float)WorkQty);
                            cl.UnitClss = UnitClss;
                            lstChildLabel.Add(cl);
                        }

                        // 부족한 분 라벨 찍을 수 있는 스캔 팝업 활성화
                        frm_PopUp_PreScanWork3 FPPSW = new frm_PopUp_PreScanWork3(m_ProcessID, m_MachineID, "", lstChildLabel);
                        FPPSW.StartPosition = FormStartPosition.CenterScreen;
                        FPPSW.BringToFront();
                        FPPSW.TopMost = true;

                        if (FPPSW.ShowDialog() == DialogResult.OK)
                        {
                            // 하위품 정보 재조회
                            lstBarcodeCheck();
                        }

                        flag = false;
                        return flag;
                    }
                    else
                    {
                        flag = false;
                        return flag;
                    }
                }
                else
                {
                    continue;
                }
            }
            return flag;
        }
        #endregion

        #region 저장버튼 클릭 이벤트 2020.09 이전

        //private void cmdSave_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        // BOM - wk_resultArticleChild에 맞춰서, 
        //        // 하위품 생산가능량보다 작업수량이 클 경우 잔량이동 팝업 활성화
        //        if (CheckWorkQty() == false)
        //        {
        //            return;
        //        }


        //        Cursor = Cursors.WaitCursor;

        //        string sWDNO = "";
        //        string sWDID = "";
        //        float sWDQty = 0;
        //        int iCnt = 0;
        //        int nColorRow = 0;

        //        float StartTime = 0;
        //        float EndTime = 0;
        //        bool Success = false;
        //        float sLogID = 0;

        //        string FacilityCollectQty = "";
        //        string CycleTime = "";


        //        ProdQty = Lib.GetDouble(txtWorkQty.Text).ToString();
        //        StartTime = float.Parse(mtb_From.Text.Replace("-", "") + dtStartTime.Value.ToString("HHmmss"));
        //        EndTime = float.Parse(mtb_To.Text.Replace("-", "") + dtEndTime.Value.ToString("HHmmss"));

        //        FacilityCollectQty = Lib.GetDouble(txtFacilityCollectQty.Text).ToString();      // 설비수집 수량
        //        CycleTime = Lib.GetDouble(txtNowCT.Text).ToString();                        // CycleTime


        //        if (StartTime > EndTime)
        //        {
        //            throw new Exception("시작시간이 종료시간보다 더 큽니다.");
        //        }
        //        if (Frm_tprc_Main.g_tBase.ProcessID == "" || Frm_tprc_Main.g_tBase.MachineID == "")
        //        {
        //            throw new Exception("공정 또는 호기가 선택되지 않았습니다.");
        //        }
        //        if (Frm_tprc_Main.g_tBase.PersonID == "")//수정필요
        //        {
        //            throw new Exception("작업자가 선택되지 않았습니다.");
        //        }
        //        if (ProdQty == "0")
        //        {
        //            throw new Exception("작업수량이 입력되지 않았습니다.");
        //        }
        //        // 2020.02.06  CycleTime 역시 필수입력 항목으로 추가합니다..   허윤구
        //        //if (CycleTime == "0")
        //        //{
        //        //    throw new Exception("CycleTime이 입력되지 않았습니다.");
        //        //}


        //        //'-------------------------------------------------------------------------------
        //        //'생산 후 최소 숙성시간 경과 안된 건 사용 불가, 유효시간 경과한 고무제품 사용 불가
        //        //'-------------------------------------------------------------------------------
        //        //if (txtBoxID.Text != "" && txtlYbox.Text != "" && m_MtrExceptYN != "Y")
        //        if (m_MtrExceptYN != "Y")
        //        {
        //            //foreach (DataGridViewRow dgvr in GridData2.Rows)
        //            //{                        
        //            //    if (!CheckID(dgvr.Cells["BarCode"].Value.ToString()))
        //            //    {
        //            //        return;
        //            //    }                        
        //            //}
        //        }

        //        //'-----------------------------------------------------------------------------------------
        //        //'투입가능 하위품 선입선출 Check 원자재투입 예외처리 시 체크하지않는다.
        //        //'-----------------------------------------------------------------------------------------
        //        //GetMtrChileLotRemainQty(txtBoxID.Text, Frm_tprc_Main.g_tBase.ProcessID, ProdQty);
        //        //'-------------------------------------------------------------------------------
        //        //'지시량 대비 실적 많은지 check
        //        //'-------------------------------------------------------------------------------


        //        //'-------------------------------------------------------------------------------
        //        //'금형 Article 맞는지 한계수명 넘어가는지 체크
        //        //'-------------------------------------------------------------------------------

        //        //if (Frm_tprc_Main.list_tMold.Count > 0)
        //        //{
        //        //    for (int i = 0; i < Frm_tprc_Main.list_tMold.Count; i++)
        //        //    {
        //        //        if (i == 0)
        //        //        {
        //        //            MoldIDList = Frm_tprc_Main.list_tMold[i].sMoldID;
        //        //        }
        //        //        else
        //        //        {
        //        //            MoldIDList = MoldIDList + "," + Frm_tprc_Main.list_tMold[i].sMoldID;
        //        //        }
        //        //    }
        //        //}

        //        //GetWorkLotInfo(txtBoxID.Text, Frm_tprc_Main.g_tBase.ProcessID, Frm_tprc_Main.g_tBase.MachineID, MoldIDList);//공정이동전표의 정보 가져오기


        //        //'-------------------------------------------------------------------------------
        //        //'생산실적 잔량 초과 실적 등록 불가
        //        //'-------------------------------------------------------------------------------
        //        if (txtInUnitClss.Text == "EA") // 생산제품의 단위가 갯수(EA)로 세리고 있는 공정이라면
        //        {
        //            if ((int)Lib.GetDouble(ProdQty) > (int)Lib.GetDouble(txtInstRemainQty.Text))
        //            {
        //                Message[0] = "[확인]";
        //                Message[1] = "생산잔량: " + txtInstRemainQty.Text + "입니다. 초과된 생산 실적을 등록하시겠습니까?";
        //                // 등록 불가 합니다. \r\n 계속 진행 하시겠습니까?";
        //                if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.No)
        //                {
        //                    Cursor = Cursors.Default;
        //                    return;
        //                }
        //            }
        //        }
        //        else                // Gram이라 가정한다면.
        //        {
        //            double douProdQty = 0;
        //            double douInstRemainQty = 0;
        //            douProdQty = Lib.GetDouble(ProdQty) / 1000;//kg단위로 변환
        //            douInstRemainQty = Lib.GetDouble(txtInstRemainQty.Text);//kg단위
        //            if (douProdQty > douInstRemainQty)
        //            {
        //                Message[0] = "[확인]";
        //                Message[1] = "생산잔량: " + txtInstRemainQty.Text + "kg입니다. 초과된 생산 실적을 등록하시겠습니까?";
        //                if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.No)
        //                {
        //                    Cursor = Cursors.Default;
        //                    return;
        //                }
        //            }
        //        }
        //        //18.06.18 주석 
        //        //txtInstRemainQty.Text = string.Format("{0:#,###}", float.Parse(Lib.CheckNum(InstQty.ToString())) - float.Parse(Lib.CheckNum(WorkQty.ToString())) - float.Parse(Lib.CheckNum(ProdQty)));
        //        //18.06.18 주석 


        //        //'-----------------------------------------------------------------------------------------
        //        //'탭/다이스 교환
        //        //'-----------------------------------------------------------------------------------------

        //        Sub_Ttd.TdChangeYN = "N";
        //        sWDNO = "";
        //        sWDID = "";

        //        int TWkRCon = 1;

        //        // 2020.05.05 HYTech : 생산박스당 수량으로 나누어서 실적등록!!!!!!
        //        //List<double> lstWorkQty = new List<double>();
        //        //double TotalQty = 0;
        //        //double.TryParse(txtWorkQty.Text, out TotalQty);
        //        //if (ProdQtyPerBox > 1)
        //        //{
        //        //    // 갯수
        //        //    //TWkRCon = (int)(TotalQty / ProdQtyPerBox);
        //        //    TWkRCon = (int)Math.Ceiling(TotalQty / ProdQtyPerBox);
        //        //    for (int i = 0; i < TWkRCon; i++)
        //        //    {
        //        //        // 마지막 행
        //        //        if (i == (TWkRCon - 1))
        //        //        {
        //        //            double RemainQty = TotalQty % ProdQtyPerBox;
        //        //            if (RemainQty == 0) { RemainQty = ProdQtyPerBox; }
        //        //            lstWorkQty.Add(RemainQty);
        //        //        }
        //        //        else
        //        //        {
        //        //            lstWorkQty.Add(ProdQtyPerBox);
        //        //        }
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    lstWorkQty.Add(TotalQty);
        //        //}

        //        //'-------------------------------------------------------------------------------
        //        //'상위품 설정
        //        //'-------------------------------------------------------------------------------

        //        list_TWkResult = new List<Sub_TWkResult>();
        //        int InstDetSeq = 0;
        //        float WorkQty = 0;
        //        float nCycleTime = 0;
        //        for (int i = 0; i < TWkRCon; i++)
        //        {
        //            list_TWkResult.Add(new Sub_TWkResult());


        //            list_TWkResult[i].JobID = (float)ConvertDouble(updateJobID);
        //            list_TWkResult[i].InstID = txtInstID.Text;
        //            int.TryParse(Lib.GetDouble(txtInstDetSeq.Text).ToString(), out InstDetSeq);
        //            list_TWkResult[i].InstDetSeq = InstDetSeq;
        //            list_TWkResult[i].LabelID = lstStartLabel[0];
        //            list_TWkResult[i].LabelGubun = this.txtLabelGubun.Text;

        //            list_TWkResult[i].ProcessID = Frm_tprc_Main.g_tBase.ProcessID;
        //            list_TWkResult[i].MachineID = Frm_tprc_Main.g_tBase.MachineID;
        //            list_TWkResult[i].ArticleID = txtArticleID.Text;

        //            float.TryParse(txtWorkQty.Text, out WorkQty);
        //            list_TWkResult[i].WorkQty = WorkQty;
        //            float.TryParse(CycleTime, out nCycleTime);
        //            list_TWkResult[i].CycleTime = nCycleTime;
        //            if (txtDefectQty.Text != string.Empty)
        //            {
        //                float DefectQty = 0;
        //                float.TryParse(txtDefectQty.Text, out DefectQty);
        //                list_TWkResult[i].WorkQty = WorkQty - DefectQty;
        //            }

        //            list_TWkResult[i].sLastArticleYN = m_LastArticleYN;

        //            list_TWkResult[i].ProdAutoInspectYN = m_ProdAutoInspectYN;
        //            list_TWkResult[i].sOrderID = m_OrderID;
        //            list_TWkResult[i].nOrderSeq = m_OrderSeq;
        //            list_TWkResult[i].WorkStartDate = mtb_From.Text.Replace("-", "");
        //            list_TWkResult[i].WorkStartTime = dtStartTime.Value.ToString("HHmmss");

        //            list_TWkResult[i].WorkEndDate = mtb_To.Text.Replace("-", "");
        //            list_TWkResult[i].WorkEndTime = dtEndTime.Value.ToString("HHmmss");
        //            list_TWkResult[i].JobGbn = "1";


        //            //'------------------------------------------------------------------------------------------

        //            list_TWkResult[i].Comments = Frm_tprc_Main.g_tBase.ProcessID + "작업종료에 따른 데이터 저장(Upgrade)";
        //            list_TWkResult[i].ReworkOldYN = "";
        //            list_TWkResult[i].ReworkLinkProdID = "";
        //            list_TWkResult[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;
        //            list_TWkResult[i].WDNO = sWDNO;

        //            list_TWkResult[i].WDID = sWDID;
        //            list_TWkResult[i].WDQty = sWDQty;
        //            list_TWkResult[i].s4MID = "";
        //            float.TryParse(m_LogID, out sLogID);
        //            list_TWkResult[i].sLogID = sLogID;


        //            //if (m_ProcessID == "0405" || m_ProcessID == "1101" || m_ProcessID == "3101" || m_ProcessID == "4101")
        //            //{
        //            //    Frm_PopUpSel_AddSave fpas = new Frm_PopUpSel_AddSave(m_ProcessID, PartGBNID, i, txtArticleID.Text);
        //            //    fpas.Owner = this;
        //            //    fpas.WriteTextEvent += new Frm_PopUpSel_AddSave.TextEventHandler(GetData);
        //            //    if (fpas.ShowDialog() == DialogResult.OK)
        //            //    {

        //            //    }
        //            //    else
        //            //    {
        //            //        return;
        //            //    }
        //            //}
        //        }


        //        if (Frm_tprc_Main.list_tMold.Count > 0)
        //        {
        //            int nCount = list_TWkResult.Count;
        //            list_TMold = new List<Sub_TMold>();
        //            for (int i = 0; i < nCount; i++)
        //            {
        //                list_TMold.Add(new Sub_TMold());
        //            }

        //            //list_TMold = new Sub_TMold[nCount];
        //        }

        //        //if (Frm_tprc_Main.g_tMol.sMoldID != "")
        //        //{
        //        //    if (TWkRCon == 1)
        //        //    {
        //        //        Sub_TMold = new Sub_TMold[1];

        //        //        Sub_TMold[0].sMoldID = Frm_tprc_Main.g_tMol.sMoldID;
        //        //        Sub_TMold[0].sRealCavity = Frm_tprc_Main.g_tMol.sRealCavity;
        //        //        Sub_TMold[0].sHitCount = int.Parse(Lib.CheckNum(Sub_TWkResult[0].WorkQty.ToString()));
        //        //    }

        //        //}
        //        if (Frm_tprc_Main.list_tMold.Count > 0)
        //        {
        //            if (Frm_tprc_Main.list_tMold[0].sMoldID != "")
        //            {
        //                //if (TWkRCon > 1)
        //                //{
        //                //    //Sub_TMold = new Sub_TMold[/*list_TWkResult.Count*/TWkRCon];
        //                //}
        //                for (int i = 0; i < TWkRCon/*list_TWkResult.Count*/; i++)
        //                {
        //                    for (int j = 0; j < Frm_tprc_Main.list_tMold.Count; j++)
        //                    {
        //                        list_TMold[i].sMoldID = Frm_tprc_Main.list_tMold[j].sMoldID;
        //                        list_TMold[i].sRealCavity = Frm_tprc_Main.list_tMold[j].sRealCavity;

        //                    }
        //                }
        //            }
        //        }

        //        iCnt = 0;

        //        //'-------------------------------------------------------------------------------
        //        //'하위품 설정
        //        //'-------------------------------------------------------------------------------

        //        nColorRow = GridData2.Rows.Count;
        //        if (nColorRow > 0)
        //        {
        //            list_TWkResultArticleChild = new List<Sub_TWkResultArticleChild>();

        //            for (int i = 0; i < nColorRow; i++)
        //            {
        //                DataGridViewRow row = GridData2.Rows[i];

        //                list_TWkResultArticleChild.Add(new Sub_TWkResultArticleChild());

        //                list_TWkResultArticleChild[i].JobID = 0;
        //                list_TWkResultArticleChild[i].ChildLabelID = Lib.CheckNull(row.Cells["BarCode"].Value.ToString());
        //                list_TWkResultArticleChild[i].ChildLabelGubun = Lib.CheckNull(row.Cells["LabelGubun"].Value.ToString());
        //                list_TWkResultArticleChild[i].ChildArticleID = Lib.CheckNull(row.Cells["ChildArticleID"].Value.ToString());
        //                list_TWkResultArticleChild[i].ReworkOldYN = "";
        //                list_TWkResultArticleChild[i].ReworkLinkChildProdID = "";
        //                list_TWkResultArticleChild[i].OutDate = DateTime.Now.ToString("yyyyMMdd");
        //                list_TWkResultArticleChild[i].OutTime = DateTime.Today.ToString("HHmmss");
        //                list_TWkResultArticleChild[i].Flag = Lib.CheckNull(row.Cells["Flag"].Value.ToString());
        //                list_TWkResultArticleChild[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

        //                //Sub_TWkResultArticleChild[i].JobID = 0;
        //                ////Sub_TWkResultArticleChild[i].JobSeq = int.Parse(Lib.CheckNull(row.Cells["Seq"].Value.ToString()));
        //                //Sub_TWkResultArticleChild[i].ChildLabelID = Lib.CheckNull(row.Cells["BarCode"].Value.ToString());
        //                //Sub_TWkResultArticleChild[i].ChildLabelGubun = Lib.CheckNull(row.Cells["LabelGubun"].Value.ToString());
        //                //Sub_TWkResultArticleChild[i].ChildArticleID = Lib.CheckNull(row.Cells["ChildArticleID"].Value.ToString());
        //                //Sub_TWkResultArticleChild[i].ReworkOldYN = "";
        //                //Sub_TWkResultArticleChild[i].ReworkLinkChildProdID = "";
        //                //Sub_TWkResultArticleChild[i].OutDate = Lib.MakeDate(4, DateTime.Today.ToString("yyyyMMdd"));
        //                //Sub_TWkResultArticleChild[i].OutTime = Lib.MakeDate(4, DateTime.Today.ToString("HHmmss"));
        //                //Sub_TWkResultArticleChild[i].Flag = Lib.CheckNull(row.Cells["Flag"].Value.ToString());
        //                //Sub_TWkResultArticleChild[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

        //                iCnt++;
        //            }
        //        }

        //        //'-------------------------------------------------------------------------------
        //        //'불량입력화면에서 가져온 불량 수량 만큼의 정보에 데이타 설정
        //        //'-------------------------------------------------------------------------------

        //        if (Frm_tprc_Main.g_tBase.DefectCnt > 0)
        //        {
        //            for (int i = 0; i < Frm_tprc_Main.g_tBase.DefectCnt; i++)
        //            {
        //                Frm_tprc_Main.list_g_tInsSub[i].BoxID = txtBoxID.Text;
        //                Frm_tprc_Main.list_g_tInsSub[i].OrderID = m_OrderID;
        //                Frm_tprc_Main.list_g_tInsSub[i].OrderSeq = m_OrderSeq;
        //            }
        //        }

        //        //'--------------------------------------------------------------------------------
        //        //'   현재 진행하는 건이 첫 공정 이라면 공동이동전표 발행 
        //        //'--------------------------------------------------------------------------------
        //        if (LabelPrintYN == "Y")
        //        {
        //            int mInstDetSeq = 0;
        //            long nQtyPerBox = 0;
        //            list_TWkLabelPrint = new List<Sub_TWkLabelPrint>();

        //            for (int i = 0; i < TWkRCon; i++)
        //            {
        //                list_TWkLabelPrint.Add(new Sub_TWkLabelPrint());

        //                list_TWkLabelPrint[i].sLabelID = "";
        //                list_TWkLabelPrint[i].sProcessID = Frm_tprc_Main.g_tBase.ProcessID;

        //                list_TWkLabelPrint[i].sArticleID = txtArticleID.Text;
        //                list_TWkLabelPrint[i].sLabelGubun = "7";

        //                //if (Frm_tprc_Main.g_tBase.ProcessID == "0405")//혼련이동전표
        //                //{
        //                //}
        //                //else if (Frm_tprc_Main.g_tBase.ProcessID == "1101")//재단이동전표
        //                //{
        //                //    list_TWkLabelPrint[i].sArticleID = txtArticleID.Text;
        //                //    list_TWkLabelPrint[i].sLabelGubun = "3";
        //                //}
        //                //else if (nProcessID > 2100)//성형공정(공정이동전표) 이후
        //                //{
        //                //    list_TWkLabelPrint[i].sArticleID = m_OrderArticleID;
        //                //    list_TWkLabelPrint[i].sLabelGubun = "5";
        //                //}

        //                list_TWkLabelPrint[i].sPrintDate = mtb_From.Text.Replace("-", "");

        //                list_TWkLabelPrint[i].sReprintDate = "";
        //                list_TWkLabelPrint[i].nReprintQty = 0;
        //                list_TWkLabelPrint[i].sInstID = txtInstID.Text;

        //                int.TryParse(Lib.GetDouble(txtInstDetSeq.Text).ToString(), out mInstDetSeq);
        //                list_TWkLabelPrint[i].nInstDetSeq = mInstDetSeq;
        //                list_TWkLabelPrint[i].sOrderID = m_OrderID;

        //                list_TWkLabelPrint[i].nPrintQty = 1;

        //                if (TWkRCon == 1)
        //                {
        //                    long.TryParse(Lib.GetDouble(ProdQty).ToString(), out nQtyPerBox);
        //                    list_TWkLabelPrint[i].nQtyPerBox = nQtyPerBox;
        //                }


        //                list_TWkLabelPrint[i].sCreateuserID = Frm_tprc_Main.g_tBase.PersonID;
        //                list_TWkLabelPrint[i].sLastUpdateUserID = Frm_tprc_Main.g_tBase.PersonID;
        //            }
        //        }
        //        //

        //        //'-------------------------------------------------------------------------------
        //        //'생산실적  저장
        //        //'-------------------------------------------------------------------------------

        //        Success = AddNewWorkResultByProdQtyPerBox_TKB(iCnt, Frm_tprc_Main.g_tBase.DefectCnt);
        //        if (Success)
        //        {
        //            if (LabelPrintYN == "Y"
        //                && !(ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq) == 1 && SaveSelectionLabelInfo.LabelID != null && !SaveSelectionLabelInfo.LabelID.Equals(""))) // TKB 추가
        //            {
        //                PrintWorkCard(TWkRCon);
        //                //WizCommon.Popup.MyMessageBox.ShowBox("[저장완료]", "대성공", 3, 1);
        //            }
        //            else
        //            {
        //                Message[0] = "[저장 완료]";
        //                Message[1] = "저장이 완료되었습니다.";
        //                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
        //            }

        //            SetFormDataClear();
        //            cmdExit_Click(null, null);  // 나가.
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }

        //        //'    '-----------------------------------------------------------------------------------------
        //        //     '저장된 결과 재 조회
        //        //'    '-----------------------------------------------------------------------------------------
        //        //FillGridData1();
        //        Cursor = Cursors.Default;
        //    }

        //    catch (Exception excpt)
        //    {
        //        WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<cmdSave>\r\n{0}", excpt.Message), "[오류]", 0, 1);
        //        Cursor = Cursors.Default;
        //    }
        //}

        #endregion

        #region 실제 주요 저장 구문. (Save_Function)
        // 저장 _ 실제 주요 로직 구문.
        private void Save_Function(string Split_GBN, double LabelPaper_Qty, 
                                   double LabelPaper_Count, double LabelPaper_OneMoreQty)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string sWDNO = "";
                string sWDID = "";
                float sWDQty = 0;
                int iCnt = 0;
                int nColorRow = 0;

                float StartTime = 0;
                float EndTime = 0;
                bool Success = false;
                float sLogID = 0;

                string FacilityCollectQty = "";
                string CycleTime = "";


                ProdQty = Lib.GetDouble(txtWorkQty.Text).ToString();
                StartTime = float.Parse(mtb_From.Text.Replace("-", "") + dtStartTime.Value.ToString("HHmmss"));
                EndTime = float.Parse(mtb_To.Text.Replace("-", "") + dtEndTime.Value.ToString("HHmmss"));

                FacilityCollectQty = Lib.GetDouble(txtFacilityCollectQty.Text).ToString();      // 설비수집 수량
                CycleTime = Lib.GetDouble(txtNowCT.Text).ToString();                        // CycleTime

                if (StartTime > EndTime)
                {
                    throw new Exception("시작시간이 종료시간보다 더 큽니다.");
                }
                if (Frm_tprc_Main.g_tBase.ProcessID == "" || Frm_tprc_Main.g_tBase.MachineID == "")
                {
                    throw new Exception("공정 또는 호기가 선택되지 않았습니다.");
                }
                if (Frm_tprc_Main.g_tBase.PersonID == "")//수정필요
                {
                    throw new Exception("작업자가 선택되지 않았습니다.");
                }
                if (ProdQty == "0")
                {
                    throw new Exception("작업수량이 입력되지 않았습니다.");
                }
                // 2020.02.06  CycleTime 역시 필수입력 항목으로 추가합니다..   허윤구
                if (CycleTime == "0")
                {
                    throw new Exception("CycleTime이 입력되지 않았습니다.");
                }


                //'-------------------------------------------------------------------------------
                //'생산 후 최소 숙성시간 경과 안된 건 사용 불가, 유효시간 경과한 고무제품 사용 불가
                //'-------------------------------------------------------------------------------
                //if (txtBoxID.Text != "" && txtlYbox.Text != "" && m_MtrExceptYN != "Y")
                if (m_MtrExceptYN != "Y")
                {
                    //foreach (DataGridViewRow dgvr in GridData2.Rows)
                    //{                        
                    //    if (!CheckID(dgvr.Cells["BarCode"].Value.ToString()))
                    //    {
                    //        return;
                    //    }                        
                    //}
                }

                //'-----------------------------------------------------------------------------------------
                //'투입가능 하위품 선입선출 Check 원자재투입 예외처리 시 체크하지않는다.
                //'-----------------------------------------------------------------------------------------
                //GetMtrChileLotRemainQty(txtBoxID.Text, Frm_tprc_Main.g_tBase.ProcessID, ProdQty);
                //'-------------------------------------------------------------------------------
                //'지시량 대비 실적 많은지 check
                //'-------------------------------------------------------------------------------


                //'-------------------------------------------------------------------------------
                //'금형 Article 맞는지 한계수명 넘어가는지 체크
                //'-------------------------------------------------------------------------------

                //if (Frm_tprc_Main.list_tMold.Count > 0)
                //{
                //    for (int i = 0; i < Frm_tprc_Main.list_tMold.Count; i++)
                //    {
                //        if (i == 0)
                //        {
                //            MoldIDList = Frm_tprc_Main.list_tMold[i].sMoldID;
                //        }
                //        else
                //        {
                //            MoldIDList = MoldIDList + "," + Frm_tprc_Main.list_tMold[i].sMoldID;
                //        }
                //    }
                //}

                //GetWorkLotInfo(txtBoxID.Text, Frm_tprc_Main.g_tBase.ProcessID, Frm_tprc_Main.g_tBase.MachineID, MoldIDList);//공정이동전표의 정보 가져오기


                //'-------------------------------------------------------------------------------
                //'생산실적 잔량 초과 실적 등록 불가
                //'-------------------------------------------------------------------------------
                if (txtInUnitClss.Text == "EA") // 생산제품의 단위가 갯수(EA)로 세리고 있는 공정이라면
                {
                    if ((int)Lib.GetDouble(ProdQty) > (int)Lib.GetDouble(txtInstRemainQty.Text))
                    {
                        Message[0] = "[확인]";
                        Message[1] = "생산잔량: " + txtInstRemainQty.Text + "입니다. 초과된 생산 실적을 등록하시겠습니까?";
                        // 등록 불가 합니다. \r\n 계속 진행 하시겠습니까?";
                        if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.No)
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                    }
                }
                else                // Gram이라 가정한다면.
                {
                    double douProdQty = 0;
                    double douInstRemainQty = 0;
                    douProdQty = Lib.GetDouble(ProdQty) / 1000;//kg단위로 변환
                    douInstRemainQty = Lib.GetDouble(txtInstRemainQty.Text);//kg단위
                    if (douProdQty > douInstRemainQty)
                    {
                        Message[0] = "[확인]";
                        Message[1] = "생산잔량: " + txtInstRemainQty.Text + "kg입니다. 초과된 생산 실적을 등록하시겠습니까?";
                        if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.No)
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                    }
                }
                //18.06.18 주석 
                //txtInstRemainQty.Text = string.Format("{0:#,###}", float.Parse(Lib.CheckNum(InstQty.ToString())) - float.Parse(Lib.CheckNum(WorkQty.ToString())) - float.Parse(Lib.CheckNum(ProdQty)));
                //18.06.18 주석 


                //'-----------------------------------------------------------------------------------------
                //'탭/다이스 교환
                //'-----------------------------------------------------------------------------------------

                Sub_Ttd.TdChangeYN = "N";
                sWDNO = "";
                sWDID = "";

                int TWkRCon = 1;

               


                //'-------------------------------------------------------------------------------
                //'상위품 설정
                //'-------------------------------------------------------------------------------

                list_TWkResult = new List<Sub_TWkResult>();
                int InstDetSeq = 0;
                float WorkQty = 0;
                float nCycleTime = 0;
                for (int i = 0; i < TWkRCon; i++)
                {
                    list_TWkResult.Add(new Sub_TWkResult());


                    list_TWkResult[i].JobID = float.Parse(updateJobID);
                    list_TWkResult[i].InstID = txtInstID.Text;
                    int.TryParse(Lib.GetDouble(txtInstDetSeq.Text).ToString(), out InstDetSeq);
                    list_TWkResult[i].InstDetSeq = InstDetSeq;
                    list_TWkResult[i].LabelID = txtProcess.Text;
                    list_TWkResult[i].LabelGubun = this.txtLabelGubun.Text;

                    list_TWkResult[i].ProcessID = Frm_tprc_Main.g_tBase.ProcessID;
                    list_TWkResult[i].MachineID = Frm_tprc_Main.g_tBase.MachineID;
                    list_TWkResult[i].ArticleID = txtArticleID.Text;

                    float.TryParse(ProdQty, out WorkQty);
                    list_TWkResult[i].WorkQty = WorkQty;
                    float.TryParse(CycleTime, out nCycleTime);
                    list_TWkResult[i].CycleTime = nCycleTime;
                    if (txtDefectQty.Text != string.Empty)
                    {
                        float DefectQty = 0;
                        float.TryParse(txtDefectQty.Text, out DefectQty);
                        list_TWkResult[i].WorkQty = WorkQty - DefectQty;
                    }

                    list_TWkResult[i].sLastArticleYN = m_LastArticleYN;

                    list_TWkResult[i].ProdAutoInspectYN = m_ProdAutoInspectYN;
                    list_TWkResult[i].sOrderID = m_OrderID;
                    list_TWkResult[i].nOrderSeq = m_OrderSeq;
                    list_TWkResult[i].WorkStartDate = mtb_From.Text.Replace("-", "");
                    list_TWkResult[i].WorkStartTime = dtStartTime.Value.ToString("HHmmss");

                    list_TWkResult[i].WorkEndDate = mtb_To.Text.Replace("-", "");
                    list_TWkResult[i].WorkEndTime = dtEndTime.Value.ToString("HHmmss");
                    list_TWkResult[i].JobGbn = "1";


                    //'------------------------------------------------------------------------------------------

                    list_TWkResult[i].Comments = Frm_tprc_Main.g_tBase.ProcessID + "작업종료에 따른 데이터 저장(Upgrade)";
                    list_TWkResult[i].ReworkOldYN = "";
                    list_TWkResult[i].ReworkLinkProdID = "";
                    list_TWkResult[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;
                    list_TWkResult[i].WDNO = sWDNO;

                    list_TWkResult[i].WDID = sWDID;
                    list_TWkResult[i].WDQty = sWDQty;
                    list_TWkResult[i].s4MID = "";
                    float.TryParse(m_LogID, out sLogID);
                    list_TWkResult[i].sLogID = sLogID;


                    //if (m_ProcessID == "0405" || m_ProcessID == "1101" || m_ProcessID == "3101" || m_ProcessID == "4101")
                    //{
                    //    Frm_PopUpSel_AddSave fpas = new Frm_PopUpSel_AddSave(m_ProcessID, PartGBNID, i, txtArticleID.Text);
                    //    fpas.Owner = this;
                    //    fpas.WriteTextEvent += new Frm_PopUpSel_AddSave.TextEventHandler(GetData);
                    //    if (fpas.ShowDialog() == DialogResult.OK)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        return;
                    //    }
                    //}
                }


                if (Frm_tprc_Main.list_tMold.Count > 0)
                {
                    int nCount = list_TWkResult.Count;
                    list_TMold = new List<Sub_TMold>();
                    for (int i = 0; i < nCount; i++)
                    {
                        list_TMold.Add(new Sub_TMold());
                    }

                    //list_TMold = new Sub_TMold[nCount];
                }

                //if (Frm_tprc_Main.g_tMol.sMoldID != "")
                //{
                //    if (TWkRCon == 1)
                //    {
                //        Sub_TMold = new Sub_TMold[1];

                //        Sub_TMold[0].sMoldID = Frm_tprc_Main.g_tMol.sMoldID;
                //        Sub_TMold[0].sRealCavity = Frm_tprc_Main.g_tMol.sRealCavity;
                //        Sub_TMold[0].sHitCount = int.Parse(Lib.CheckNum(Sub_TWkResult[0].WorkQty.ToString()));
                //    }

                //}
                if (Frm_tprc_Main.list_tMold.Count > 0)
                {
                    if (Frm_tprc_Main.list_tMold[0].sMoldID != "")
                    {
                        //if (TWkRCon > 1)
                        //{
                        //    //Sub_TMold = new Sub_TMold[/*list_TWkResult.Count*/TWkRCon];
                        //}
                        for (int i = 0; i < TWkRCon/*list_TWkResult.Count*/; i++)
                        {
                            for (int j = 0; j < Frm_tprc_Main.list_tMold.Count; j++)
                            {
                                list_TMold[i].sMoldID = Frm_tprc_Main.list_tMold[j].sMoldID;
                                list_TMold[i].sRealCavity = Frm_tprc_Main.list_tMold[j].sRealCavity;

                            }
                        }
                    }
                }

                iCnt = 0;

                //'-------------------------------------------------------------------------------
                //'하위품 설정
                //'-------------------------------------------------------------------------------

                nColorRow = GridData2.Rows.Count;
                if (nColorRow > 0)
                {
                    list_TWkResultArticleChild = new List<Sub_TWkResultArticleChild>();

                    for (int i = 0; i < nColorRow; i++)
                    {
                        DataGridViewRow row = GridData2.Rows[i];

                        list_TWkResultArticleChild.Add(new Sub_TWkResultArticleChild());

                        list_TWkResultArticleChild[i].JobID = 0;
                        list_TWkResultArticleChild[i].ChildLabelID = Lib.CheckNull(row.Cells["BarCode"].Value.ToString());
                        list_TWkResultArticleChild[i].ChildLabelGubun = Lib.CheckNull(row.Cells["LabelGubun"].Value.ToString());
                        list_TWkResultArticleChild[i].ChildArticleID = Lib.CheckNull(row.Cells["ChildArticleID"].Value.ToString());
                        list_TWkResultArticleChild[i].ReworkOldYN = "";
                        list_TWkResultArticleChild[i].ReworkLinkChildProdID = "";
                        list_TWkResultArticleChild[i].OutDate = DateTime.Now.ToString("yyyyMMdd");
                        list_TWkResultArticleChild[i].OutTime = DateTime.Today.ToString("HHmmss");
                        list_TWkResultArticleChild[i].Flag = Lib.CheckNull(row.Cells["Flag"].Value.ToString());
                        list_TWkResultArticleChild[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                        //Sub_TWkResultArticleChild[i].JobID = 0;
                        ////Sub_TWkResultArticleChild[i].JobSeq = int.Parse(Lib.CheckNull(row.Cells["Seq"].Value.ToString()));
                        //Sub_TWkResultArticleChild[i].ChildLabelID = Lib.CheckNull(row.Cells["BarCode"].Value.ToString());
                        //Sub_TWkResultArticleChild[i].ChildLabelGubun = Lib.CheckNull(row.Cells["LabelGubun"].Value.ToString());
                        //Sub_TWkResultArticleChild[i].ChildArticleID = Lib.CheckNull(row.Cells["ChildArticleID"].Value.ToString());
                        //Sub_TWkResultArticleChild[i].ReworkOldYN = "";
                        //Sub_TWkResultArticleChild[i].ReworkLinkChildProdID = "";
                        //Sub_TWkResultArticleChild[i].OutDate = Lib.MakeDate(4, DateTime.Today.ToString("yyyyMMdd"));
                        //Sub_TWkResultArticleChild[i].OutTime = Lib.MakeDate(4, DateTime.Today.ToString("HHmmss"));
                        //Sub_TWkResultArticleChild[i].Flag = Lib.CheckNull(row.Cells["Flag"].Value.ToString());
                        //Sub_TWkResultArticleChild[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                        iCnt++;
                    }
                }

                //'-------------------------------------------------------------------------------
                //'불량입력화면에서 가져온 불량 수량 만큼의 정보에 데이타 설정
                //'-------------------------------------------------------------------------------

                if (Frm_tprc_Main.g_tBase.DefectCnt > 0)
                {
                    for (int i = 0; i < Frm_tprc_Main.g_tBase.DefectCnt; i++)
                    {
                        Frm_tprc_Main.list_g_tInsSub[i].BoxID = txtBoxID.Text;
                        Frm_tprc_Main.list_g_tInsSub[i].OrderID = m_OrderID;
                        Frm_tprc_Main.list_g_tInsSub[i].OrderSeq = m_OrderSeq;
                    }
                }

                //'--------------------------------------------------------------------------------
                //'   현재 진행하는 건이 첫 공정 이라면 공동이동전표 발행 
                //'--------------------------------------------------------------------------------
                if (LabelPrintYN == "Y")
                {
                    int mInstDetSeq = 0;
                    long nQtyPerBox = 0;
                    list_TWkLabelPrint = new List<Sub_TWkLabelPrint>();

                    for (int i = 0; i < TWkRCon; i++)
                    {
                        list_TWkLabelPrint.Add(new Sub_TWkLabelPrint());

                        list_TWkLabelPrint[i].sLabelID = "";
                        list_TWkLabelPrint[i].sProcessID = Frm_tprc_Main.g_tBase.ProcessID;

                        list_TWkLabelPrint[i].sArticleID = txtArticleID.Text;
                        list_TWkLabelPrint[i].sLabelGubun = "7";

                        //if (Frm_tprc_Main.g_tBase.ProcessID == "0405")//혼련이동전표
                        //{
                        //}
                        //else if (Frm_tprc_Main.g_tBase.ProcessID == "1101")//재단이동전표
                        //{
                        //    list_TWkLabelPrint[i].sArticleID = txtArticleID.Text;
                        //    list_TWkLabelPrint[i].sLabelGubun = "3";
                        //}
                        //else if (nProcessID > 2100)//성형공정(공정이동전표) 이후
                        //{
                        //    list_TWkLabelPrint[i].sArticleID = m_OrderArticleID;
                        //    list_TWkLabelPrint[i].sLabelGubun = "5";
                        //}

                        list_TWkLabelPrint[i].sPrintDate = mtb_From.Text.Replace("-", "");

                        list_TWkLabelPrint[i].sReprintDate = "";
                        list_TWkLabelPrint[i].nReprintQty = 0;
                        list_TWkLabelPrint[i].sInstID = txtInstID.Text;

                        int.TryParse(Lib.GetDouble(txtInstDetSeq.Text).ToString(), out mInstDetSeq);
                        list_TWkLabelPrint[i].nInstDetSeq = mInstDetSeq;
                        list_TWkLabelPrint[i].sOrderID = m_OrderID;

                        list_TWkLabelPrint[i].nPrintQty = 1;

                        if (TWkRCon == 1)
                        {
                            long.TryParse(Lib.GetDouble(ProdQty).ToString(), out nQtyPerBox);
                            list_TWkLabelPrint[i].nQtyPerBox = nQtyPerBox;
                        }


                        list_TWkLabelPrint[i].sCreateuserID = Frm_tprc_Main.g_tBase.PersonID;
                        list_TWkLabelPrint[i].sLastUpdateUserID = Frm_tprc_Main.g_tBase.PersonID;
                    }
                }
                //

                //'-------------------------------------------------------------------------------
                //'생산실적  저장
                //'-------------------------------------------------------------------------------

                Success = AddNewWorkResult(iCnt, Frm_tprc_Main.g_tBase.DefectCnt);
                if (Success)
                {
                    if (LabelPrintYN == "Y")
                    {
                        PrintWorkCard(TWkRCon);
                    }
                    else
                    {
                        Message[0] = "[저장 완료]";
                        Message[1] = "저장이 완료되었습니다.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                    }

                    SetFormDataClear();
                    cmdExit_Click(null, null);  // 나가.
                }
                else
                {
                    throw new Exception();
                }
                //'    '-----------------------------------------------------------------------------------------
                //     '저장된 결과 재 조회
                //'    '-----------------------------------------------------------------------------------------
                //FillGridData1();
                Cursor = Cursors.Default;
            }

            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<cmdSave>\r\n{0}", excpt.Message), "[오류]", 0, 1);
                Cursor = Cursors.Default;
            }
        }

        #endregion


        private void Fpas_WriteTextEvent(List<KeyValue> AddSaveResult)
        {
            foreach (KeyValue kv in AddSaveResult)
            {

            }
        }

        #region 생산 등록, 생산 하위품 등록, 생산 불량 등록한다
        private bool AddNewWorkResult(int nCnt, int nDefectCnt)
        {
            List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
            List<List<string>> ListProcedureName = new List<List<string>>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

            try
            {
                for (int i = 0; i < list_TWkResult.Count; i++)
                {
                    //'*****************************************************************************************************
                    //'                  공정이동전표 등록
                    //'*****************************************************************************************************
                    if (list_TWkLabelPrint.Count > 0)
                    {
                        if (list_TWkLabelPrint[i].sProcessID != "")
                        {
                            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                            sqlParameter.Add("LabelID", list_TWkLabelPrint[i].sLabelID);
                            sqlParameter.Add("LabelGubun", list_TWkLabelPrint[i].sLabelGubun);
                            sqlParameter.Add("ProcessID", list_TWkLabelPrint[i].sProcessID);
                            sqlParameter.Add("ArticleID", list_TWkLabelPrint[i].sArticleID);
                            sqlParameter.Add("PrintDate", list_TWkLabelPrint[i].sPrintDate);

                            sqlParameter.Add("ReprintDate", list_TWkLabelPrint[i].sReprintDate);
                            sqlParameter.Add("ReprintQty", list_TWkLabelPrint[i].nReprintQty);
                            sqlParameter.Add("InstID", list_TWkLabelPrint[i].sInstID);
                            sqlParameter.Add("InstDetSeq", list_TWkLabelPrint[i].nInstDetSeq);
                            sqlParameter.Add("OrderID", list_TWkLabelPrint[i].sOrderID);

                            sqlParameter.Add("PrintQty", list_TWkLabelPrint[i].nPrintQty);
                            sqlParameter.Add("LabelPrintQty", 1);
                            sqlParameter.Add("nQtyPerBox", list_TWkLabelPrint[i].nQtyPerBox);
                            sqlParameter.Add("CreateUserID", list_TWkLabelPrint[i].sCreateuserID);

                            WizCommon.Procedure pro1 = new WizCommon.Procedure();
                            pro1.Name = "[xp_WizWork_iwkLabelPrint_C]";
                            pro1.OutputUseYN = "Y";
                            pro1.OutputName = "LabelID";
                            pro1.OutputLength = "20";

                            Prolist.Add(pro1);
                            ListParameter.Add(sqlParameter);
                        }
                    }
                    //'************************************************************************************************
                    //'                               상위품 생산 //xp_wkResult_iWkResult
                    //'************************************************************************************************
                    Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                    WizCommon.Procedure pro2 = new WizCommon.Procedure();

                    sqlParameter1.Add("JobID", list_TWkResult[i].JobID);

                    if (list_TWkLabelPrint.Count > 0)
                    {
                        sqlParameter1.Add("LabelID", list_TWkLabelPrint[i].sLabelID);
                    }
                    else
                    {
                        sqlParameter1.Add("LabelID", list_TWkResult[i].LabelID);
                    }
                    sqlParameter1.Add("LabelGubun", list_TWkResult[i].LabelGubun);
                    sqlParameter1.Add("WorkEndDate", list_TWkResult[i].WorkEndDate);
                    sqlParameter1.Add("WorkEndTime", list_TWkResult[i].WorkEndTime);

                    sqlParameter1.Add("WorkQty", list_TWkResult[i].WorkQty);
                    sqlParameter1.Add("CycleTime", list_TWkResult[i].CycleTime);
                    sqlParameter1.Add("ProcessID", list_TWkResult[i].ProcessID);
                    sqlParameter1.Add("MachineID", list_TWkResult[i].MachineID);
                    sqlParameter1.Add("Comments", list_TWkResult[i].ProcessID + "작업종료에 따른 저장구문");

                    sqlParameter1.Add("UpdateUserID", list_TWkResult[i].CreateUserID);

                    //pro2.Name = "xp_wkResult_uWkResultOne";
                    pro2.Name = "xp_prdWork_uWkResultOne";//하이테크만 이렇게 사용 // 2020.02.21 둘리 : CycleTime 추가되도록 수정
                    pro2.OutputUseYN = "N";
                    pro2.OutputName = "JobID";
                    pro2.OutputLength = "20";

                    Prolist.Add(pro2);
                    ListParameter.Add(sqlParameter1);
                    //'****************************************************************************************************
                    //'정상작업의 경우    Sub_TWkResult.JobGbn = "1"
                    //'****************************************************************************************************
                    if (list_TWkResult[0].JobGbn == "1")
                    {

                        //'************************************************************************************************
                        //'                               하위품 스켄이력 등록
                        //'************************************************************************************************

                        #region 기존 하위품 등록 → wk_resultArticleChild
                        if (nCnt > 0)
                        {
                            for (int k = 0; k < nCnt; k++)
                            {
                                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                                sqlParameter2.Add("JobID", list_TWkResult[i].JobID);
                                sqlParameter2.Add("ChildLabelID", list_TWkResultArticleChild[k].ChildLabelID);//
                                sqlParameter2.Add("ChildLabelGubun", list_TWkResultArticleChild[k].ChildLabelGubun);
                                sqlParameter2.Add("ChildArticleID", list_TWkResultArticleChild[k].ChildArticleID);
                                sqlParameter2.Add("ReworkOldYN", list_TWkResultArticleChild[k].ReworkOldYN);
                                sqlParameter2.Add("ReworkLinkChildProdID", list_TWkResultArticleChild[k].ReworkLinkChildProdID);

                                sqlParameter2.Add("Seq", (k + 1));
                                sqlParameter2.Add("CreateUserID", list_TWkResultArticleChild[k].CreateUserID);

                                WizCommon.Procedure pro3 = new WizCommon.Procedure();
                                pro3.Name = "xp_prdWork_iWkResultArticleChild";
                                pro3.OutputUseYN = "N";
                                pro3.OutputName = "JobID";
                                pro3.OutputLength = "20";

                                Prolist.Add(pro3);
                                ListParameter.Add(sqlParameter2);

                            }
                        }
                        #endregion

                        //'************************************************************************************************
                        //'                            하위품   LabelMaster 추가 및 업데이트   xp_LabelMaster_UScanSub
                        //'************************************************************************************************

                        //for (int k = 0; k < nCnt; k++)
                        //{
                        //ds = null;
                        //sqlParameter = null;

                        //    sqlParameter.Add("LabelID",	        Sub_TWkResultArticleChild[k].ChildLabelID);
                        //    sqlParameter.Add("LabelGubun",	    Sub_TWkResultArticleChild[k].ChildLabelGubun);
                        //    sqlParameter.Add("ArticleID",	    Sub_TWkResultArticleChild[k].ChildArticleID);
                        //    sqlParameter.Add("OutDate",	        Sub_TWkResultArticleChild[k].OutDate);
                        //    sqlParameter.Add("OutTime",	        Sub_TWkResultArticleChild[k].OutTime);
                        //    sqlParameter.Add("UserID",          Sub_TWkResultArticleChild[k].CreateUserID);




                        ////'생산과 동시 검사 완료 항목이서 최종 공정의 최종일 경우 자동 검사실적 생성  xp_WizIns_iInspectFinal
                        //if (list_TWkResult[i].ProdAutoInspectYN == "Y" && list_TWkResult[i].sLastArticleYN == "Y")
                        //{
                        //    ds = null;
                        //    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                        //    sqlParameter.Add("OrderID", list_TWkResult[i].sOrderID);
                        //    sqlParameter.Add("RollSeq", 0);
                        //    sqlParameter.Add("OrderSeq", list_TWkResult[i].nOrderSeq);
                        //    sqlParameter.Add("ExamNO", "00");
                        //    sqlParameter.Add("ExamDate", list_TWkResult[i].ScanDate);

                        //    sqlParameter.Add("ExamTime", list_TWkResult[i].ScanTime.Substring(0, 4));
                        //    sqlParameter.Add("TeamID", "");
                        //    sqlParameter.Add("PersonID", list_TWkResult[i].CreateUserID);
                        //    sqlParameter.Add("RealQty", list_TWkResult[i].WorkQty);
                        //    sqlParameter.Add("CtrlQty", list_TWkResult[i].WorkQty);

                        //    sqlParameter.Add("SampleQty", 0);
                        //    sqlParameter.Add("LossQty", 0);
                        //    sqlParameter.Add("CutQty", 0);
                        //    sqlParameter.Add("UnitClss", list_TWkResult[i].sUnitClss);
                        //    sqlParameter.Add("Density  ", 0);

                        //    sqlParameter.Add("GradeID", "1");                                           //A등급
                        //    sqlParameter.Add("LotNo", "1");
                        //    sqlParameter.Add("BoxID", list_TWkResult[i].LabelID);                      //BoxID

                        //    sqlParameter.Add("DefectQty", 0);
                        //    sqlParameter.Add("DefectPoint", 0);
                        //    sqlParameter.Add("DefectID", "");
                        //    sqlParameter.Add("CutDefectID", "");
                        //    sqlParameter.Add("DefectClss", "");
                        //    sqlParameter.Add("CutDefectClss", "");


                        //    sqlParameter.Add("InstID", list_TWkResult[i].InstID);
                        //    sqlParameter.Add("CardID", "");
                        //    sqlParameter.Add("SplitID", "");
                        //    //sqlParameter.Add("CardIDList",          "");
                        //    sqlParameter.Add("CreateUserID", list_TWkResult[i].CreateUserID);

                        //if (i == 0)//xp_work_iTdchange 성광콜드포징만 적용됨
                        //{
                        //    if (Sub_Ttd.TdChangeYN == "Y")// '탭다이스 교환 여부
                        //    {
                        //        Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();

                        //        sqlParameter3.Add("ProcessID", Sub_Ttd.ProcessID);
                        //        sqlParameter3.Add("MachineID", Sub_Ttd.MachineID);
                        //        sqlParameter3.Add("TdGbn", Sub_Ttd.Tdgbn);
                        //        sqlParameter3.Add("tdDate", Sub_Ttd.TdDate);
                        //        sqlParameter3.Add("tdTime", Sub_Ttd.tdtime);
                        //        sqlParameter3.Add("PersonID", Sub_Ttd.PersonID);
                        //        sqlParameter3.Add("CreateUserID", Sub_Ttd.CreateUserID);

                        //        WizCommon.Procedure pro4 = new WizCommon.Procedure();
                        //        pro4.Name = "xp_work_iTdchange";
                        //        pro4.OutputUseYN = "N";
                        //        pro4.OutputName = "JobID";
                        //        pro4.OutputLength = "20";

                        //        Prolist.Add(pro4);

                        //        ListParameter.Add(sqlParameter3);
                        //    }
                        //}
                        // '************************************************************************************************
                        //'                              불량 등록 시   //xp_wkResult_iInspect
                        //'************************************************************************************************

                        if (nDefectCnt > 0)
                        {
                            for (int k = 0; k < nDefectCnt; k++)
                            {
                                Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();

                                sqlParameter4.Add("WkDefectID", "");
                                sqlParameter4.Add("OrderID", Frm_tprc_Main.list_g_tInsSub[k].OrderID);
                                sqlParameter4.Add("OrderSeq", Frm_tprc_Main.list_g_tInsSub[k].OrderSeq);
                                sqlParameter4.Add("ProcessID", Frm_tprc_Main.list_g_tInsSub[k].ProcessID);
                                sqlParameter4.Add("MachineID", Frm_tprc_Main.list_g_tInsSub[k].MachineID);

                                sqlParameter4.Add("DefectQty", Frm_tprc_Main.list_g_tInsSub[k].nDefectQty);
                                sqlParameter4.Add("BoxID", Frm_tprc_Main.list_g_tInsSub[k].BoxID);
                                sqlParameter4.Add("DefectID", Frm_tprc_Main.list_g_tInsSub[k].DefectID);
                                sqlParameter4.Add("XPos", Frm_tprc_Main.list_g_tInsSub[k].XPos);
                                sqlParameter4.Add("YPos", Frm_tprc_Main.list_g_tInsSub[k].YPos);

                                sqlParameter4.Add("InspectDate", Frm_tprc_Main.list_g_tInsSub[k].InspectDate);
                                sqlParameter4.Add("InspectTime", Frm_tprc_Main.list_g_tInsSub[k].InspectTime);
                                sqlParameter4.Add("PersonID", list_TWkResult[0].CreateUserID);
                                sqlParameter4.Add("JobID", list_TWkResult[0].JobID);
                                sqlParameter4.Add("CreateUserID", list_TWkResult[0].CreateUserID);

                                WizCommon.Procedure pro5 = new WizCommon.Procedure();
                                pro5.Name = "xp_wkResult_iInspect";
                                pro5.OutputUseYN = "N";
                                pro5.OutputName = "JobID";
                                pro5.OutputLength = "20";

                                Prolist.Add(pro5);
                                ListParameter.Add(sqlParameter4);
                                //ProcedureInfo = null;
                            }
                        }


                        //'************************************************************************************************
                        //'                            생산제품 재고 생성 및 하품 자재 출고 처리  //xp_wkResult_iWkResultStuffInOut
                        //'************************************************************************************************
                        //if (m_ProcessID != "2101" || (m_ProcessID == "2101" && blSHExit))
                        ////성형공정이 아니거나 또는 , 성형공정이면서 작업종료 시점일때만 입력
                        //{

                        Dictionary<string, object> sqlParameter5 = new Dictionary<string, object>();

                        sqlParameter5.Add("JobID", list_TWkResult[i].JobID);
                        sqlParameter5.Add("CreateUserID", list_TWkResult[i].CreateUserID);
                        sqlParameter5.Add("sRtnMsg", "");

                        WizCommon.Procedure pro6 = new WizCommon.Procedure();
                        pro6.Name = "xp_wkResult_iWkResultStuffInOut";
                        pro6.OutputUseYN = "N";
                        pro6.OutputName = "JobID";
                        pro6.OutputLength = "20";

                        Prolist.Add(pro6);
                        ListParameter.Add(sqlParameter5);


                        //}
                    }
                }
                //'************************************************************************************************
                //'                           사용 금형 등록
                //'************************************************************************************************ 
                if (list_TMold != null)
                {
                    if (m_ProcessID != "2101" || (m_ProcessID == "2101" && blSHExit))
                    //성형공정이 아니거나 또는 , 성형공정이면서 작업종료 시점일때만 입력
                    {
                        //if (list_TMold.Count > 0)
                        //{
                        //    for (int j = 0; j < Frm_tprc_Main.list_tMold.Count; j++)
                        //    {
                        //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                        //        sqlParameter.Add("JobID", list_TWkResult[j].JobID);
                        //        sqlParameter.Add("MoldID", list_TMold[j].sMoldID/*Frm_tprc_Main.list_tMold[j].sMoldID*/);
                        //        sqlParameter.Add("RealCavity", list_TMold[j].sRealCavity/*Frm_tprc_Main.list_tMold[j].sRealCavity*/);
                        //        sqlParameter.Add("HitCount", list_TMold[j].sHitCount/*list_TWkResult[i].WorkQty*/);
                        //        sqlParameter.Add("CreateUserID", list_TWkResult[j].CreateUserID);

                        //        WizCommon.Procedure pro_2 = new WizCommon.Procedure();
                        //        pro_2.Name = "xp_wkResult_iwkResultMold";
                        //        pro_2.OutputUseYN = "N";
                        //        pro_2.OutputName = "JobID";
                        //        pro_2.OutputLength = "20";

                        //        Prolist.Add(pro_2);
                        //        ListParameter.Add(sqlParameter);
                        //    }
                        //}
                    }

                }
                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    list_Result.RemoveAt(0);

                    //int rsCount = list_Result.Count / 2;//2 = Output갯수(JobID, LabelID)
                    int a = 0;
                    for (int i = 0; i < list_Result.Count; i++)
                    {
                        KeyValue kv = list_Result[i];
                        if (kv.key == "LabelID")
                        {
                            list_TWkLabelPrint[a++].sLabelID = kv.value;
                            //list_TWkLabelPrint[i].sLabelID = kv.value;
                        }
                        //else if (kv.key == "JobID")
                        //{
                        //    //list_TWkResult[i / 2].JobID = float.Parse(kv.value);
                        //    list_TWkResult[i].JobID = float.Parse(kv.value);
                        //}
                    }
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

        #region [장입량으로 나누어] 생산 등록, 하위품, 불량등록

        private bool AddNewWorkResultByProdQtyPerBox(int nCnt, int nDefectCnt)
        {
            List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
            List<List<string>> ListProcedureName = new List<List<string>>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

            try
            {
                for (int i = 0; i < list_TWkResult.Count; i++)
                {
                    //'*****************************************************************************************************
                    //'                  공정이동전표 등록
                    //'*****************************************************************************************************
                    if (list_TWkLabelPrint.Count > 0)
                    {
                        if (list_TWkLabelPrint[i].sProcessID != "")
                        {
                            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                            sqlParameter.Add("LabelID", "");
                            sqlParameter.Add("LabelGubun", list_TWkLabelPrint[i].sLabelGubun);
                            sqlParameter.Add("ProcessID", list_TWkLabelPrint[i].sProcessID);
                            sqlParameter.Add("ArticleID", list_TWkLabelPrint[i].sArticleID);
                            sqlParameter.Add("PrintDate", list_TWkLabelPrint[i].sPrintDate);

                            sqlParameter.Add("ReprintDate", list_TWkLabelPrint[i].sReprintDate);
                            sqlParameter.Add("ReprintQty", list_TWkLabelPrint[i].nReprintQty);
                            sqlParameter.Add("InstID", list_TWkLabelPrint[i].sInstID);
                            sqlParameter.Add("InstDetSeq", list_TWkLabelPrint[i].nInstDetSeq);
                            sqlParameter.Add("OrderID", list_TWkLabelPrint[i].sOrderID);

                            sqlParameter.Add("PrintQty", list_TWkLabelPrint[i].nPrintQty);
                            sqlParameter.Add("LabelPrintQty", 1);
                            sqlParameter.Add("nQtyPerBox", list_TWkLabelPrint[i].nQtyPerBox);
                            sqlParameter.Add("CreateUserID", list_TWkLabelPrint[i].sCreateuserID);

                            WizCommon.Procedure pro1 = new WizCommon.Procedure();
                            pro1.Name = "[xp_WizWork_iwkLabelPrint_C]";
                            pro1.OutputUseYN = "Y";
                            pro1.OutputName = "LabelID";
                            pro1.OutputLength = "20";

                            Prolist.Add(pro1);
                            ListParameter.Add(sqlParameter);
                        }
                    }
                    //'************************************************************************************************
                    //'                               상위품 생산 //xp_wkResult_iWkResult
                    //'************************************************************************************************
                    Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                    WizCommon.Procedure pro2 = new WizCommon.Procedure();

                    if (i == 0)
                    {
                        sqlParameter1.Add("JobID", list_TWkResult[i].JobID);

                        if (list_TWkLabelPrint.Count > 0)
                        {
                            sqlParameter1.Add("LabelID", list_TWkLabelPrint[i].sLabelID);
                        }
                        else
                        {
                            sqlParameter1.Add("LabelID", list_TWkResult[i].LabelID);
                        }
                        sqlParameter1.Add("LabelGubun", list_TWkResult[i].LabelGubun);
                        sqlParameter1.Add("WorkStartDate", list_TWkResult[i].WorkStartDate);
                        sqlParameter1.Add("WorkStartTime", list_TWkResult[i].WorkStartTime);
                        sqlParameter1.Add("WorkEndDate", list_TWkResult[i].WorkEndDate);
                        sqlParameter1.Add("WorkEndTime", list_TWkResult[i].WorkEndTime);
                        sqlParameter1.Add("WorkQty", list_TWkResult[i].WorkQty);
                        sqlParameter1.Add("CycleTime", list_TWkResult[i].CycleTime);
                        sqlParameter1.Add("ProcessID", list_TWkResult[i].ProcessID);
                        sqlParameter1.Add("MachineID", list_TWkResult[i].MachineID);
                        sqlParameter1.Add("Comments", list_TWkResult[i].ProcessID + "작업종료에 따른 저장구문");

                        sqlParameter1.Add("UpdateUserID", list_TWkResult[i].CreateUserID);

                        //pro2.Name = "xp_wkResult_uWkResultOne";
                        pro2.Name = "xp_prdWork_uWkResultOne";//하이테크만 이렇게 사용 // 2020.02.21 둘리 : CycleTime 추가되도록 수정
                        pro2.OutputUseYN = "N";
                        pro2.OutputName = "JobID";
                        pro2.OutputLength = "20";
                    }
                    else
                    {
                        sqlParameter1.Add("JobID", 0);
                        sqlParameter1.Add("InstID", list_TWkResult[i].InstID);
                        sqlParameter1.Add("InstDetSeq", list_TWkResult[i].InstDetSeq);
                        if (list_TWkLabelPrint.Count > i)
                        {
                            sqlParameter1.Add("LabelID", list_TWkLabelPrint[i].sLabelID);
                        }
                        else
                        {
                            sqlParameter1.Add("LabelID", list_TWkResult[i].LabelID);
                        }
                        sqlParameter1.Add("StartSaveLabelID", "");

                        sqlParameter1.Add("LabelGubun", list_TWkResult[i].LabelGubun);
                        sqlParameter1.Add("ProcessID", list_TWkResult[i].ProcessID);
                        sqlParameter1.Add("MachineID", list_TWkResult[i].MachineID);
                        sqlParameter1.Add("ScanDate", list_TWkResult[i].WorkStartDate); // 둘리 : 2020.05.27 : StartDate, StartTime 이랑 맞춤
                        sqlParameter1.Add("ScanTime", list_TWkResult[i].WorkStartTime);

                        sqlParameter1.Add("ArticleID", list_TWkResult[i].ArticleID);
                        sqlParameter1.Add("WorkQty", list_TWkResult[i].WorkQty);
                        sqlParameter1.Add("Comments", list_TWkResult[i].Comments);
                        sqlParameter1.Add("ReworkOldYN", list_TWkResult[i].ReworkOldYN);
                        sqlParameter1.Add("ReworkLinkProdID", list_TWkResult[i].ReworkLinkProdID);

                        sqlParameter1.Add("WorkStartDate", list_TWkResult[i].WorkStartDate);
                        sqlParameter1.Add("WorkStartTime", list_TWkResult[i].WorkStartTime);
                        sqlParameter1.Add("WorkEndDate", list_TWkResult[i].WorkEndDate);
                        sqlParameter1.Add("WorkEndTime", list_TWkResult[i].WorkEndTime);
                        sqlParameter1.Add("JobGbn", list_TWkResult[i].JobGbn);

                        sqlParameter1.Add("NoReworkCode", list_TWkResult[i].sNowReworkCode);
                        sqlParameter1.Add("WDNO", list_TWkResult[i].WDNO);
                        sqlParameter1.Add("WDID", list_TWkResult[i].WDID);
                        sqlParameter1.Add("WDQty", list_TWkResult[i].WDQty);
                        sqlParameter1.Add("LogID", list_TWkResult[i].sLogID);

                        sqlParameter1.Add("s4MID", list_TWkResult[i].s4MID);
                        sqlParameter1.Add("DayOrNightID", Frm_tprc_Main.g_tBase.DayOrNightID);
                        sqlParameter1.Add("CycleTime", list_TWkResult[i].CycleTime);
                        sqlParameter1.Add("FirstJobID", list_TWkResult[0].JobID);
                        sqlParameter1.Add("CreateUserID", list_TWkResult[i].CreateUserID);

                        pro2.Name = "xp_prdWork_iWkResultSecond";
                        pro2.OutputUseYN = "Y";
                        pro2.OutputName = "JobID";
                        pro2.OutputLength = "20";
                    }


                    Prolist.Add(pro2);
                    ListParameter.Add(sqlParameter1);

                    //'****************************************************************************************************
                    //'정상작업의 경우    Sub_TWkResult.JobGbn = "1"
                    //'****************************************************************************************************
                    if (list_TWkResult[0].JobGbn == "1")
                    {
                        //'************************************************************************************************
                        //'                               하위품 스켄이력 등록
                        //'************************************************************************************************

                        #region 기존 하위품 등록 → wk_resultArticleChild
                        if (nCnt > 0)
                        {
                            for (int k = 0; k < nCnt; k++)
                            {
                                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                                if (i == 0)
                                {
                                    sqlParameter2.Add("JobID", list_TWkResult[i].JobID);
                                }
                                else
                                {
                                    sqlParameter2.Add("JobID", 0);
                                }

                                //sqlParameter2.Add("JobID", list_TWkResult[i].JobID);
                                sqlParameter2.Add("ChildLabelID", list_TWkResultArticleChild[k].ChildLabelID);//
                                sqlParameter2.Add("ChildLabelGubun", list_TWkResultArticleChild[k].ChildLabelGubun);
                                sqlParameter2.Add("ChildArticleID", list_TWkResultArticleChild[k].ChildArticleID);
                                sqlParameter2.Add("ReworkOldYN", list_TWkResultArticleChild[k].ReworkOldYN);
                                sqlParameter2.Add("ReworkLinkChildProdID", list_TWkResultArticleChild[k].ReworkLinkChildProdID);

                                sqlParameter2.Add("Seq", (k + 1));
                                sqlParameter2.Add("CreateUserID", list_TWkResultArticleChild[k].CreateUserID);

                                WizCommon.Procedure pro3 = new WizCommon.Procedure();
                                pro3.Name = "xp_prdWork_iWkResultArticleChild";
                                pro3.OutputUseYN = "N";
                                pro3.OutputName = "JobID";
                                pro3.OutputLength = "20";

                                Prolist.Add(pro3);
                                ListParameter.Add(sqlParameter2);

                            }
                        }
                        #endregion

                        
                        // '************************************************************************************************
                        //'                              불량 등록 시   //xp_wkResult_iInspect
                        //'************************************************************************************************

                        if (i == 0)
                        {
                            foreach(string Key in dicDefect.Keys)
                            {
                                var Defect = dicDefect[Key] as frm_tprc_Work_Defect_U_CodeView;
                                if (Defect != null)
                                {
                                    Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();

                                    sqlParameter4.Add("DefectID", Key.Trim());
                                    sqlParameter4.Add("DefectQty", ConvertDouble(Defect.DefectQty));
                                    sqlParameter4.Add("XPos", ConvertInt(Defect.XPos));
                                    sqlParameter4.Add("YPos", ConvertInt(Defect.YPos));
                                    sqlParameter4.Add("JobID", list_TWkResult[0].JobID);
                                    sqlParameter4.Add("CreateUserID", list_TWkResult[0].CreateUserID);

                                    WizCommon.Procedure pro4 = new WizCommon.Procedure();
                                    pro4.Name = "xp_prdWork_iWorkDefect";
                                    pro4.OutputUseYN = "N";
                                    pro4.OutputName = "JobID";
                                    pro4.OutputLength = "20";

                                    Prolist.Add(pro4);
                                    ListParameter.Add(sqlParameter4);
                                }
                            }
                        }

                        //'************************************************************************************************
                        //'                            생산제품 재고 생성 및 하품 자재 출고 처리  //xp_wkResult_iWkResultStuffInOut
                        //'************************************************************************************************
                        //if (m_ProcessID != "2101" || (m_ProcessID == "2101" && blSHExit))
                        ////성형공정이 아니거나 또는 , 성형공정이면서 작업종료 시점일때만 입력
                        //{

                        Dictionary<string, object> sqlParameter5 = new Dictionary<string, object>();

                        sqlParameter5.Add("JobID", list_TWkResult[i].JobID);
                        sqlParameter5.Add("CreateUserID", list_TWkResult[i].CreateUserID);
                        sqlParameter5.Add("sRtnMsg", "");

                        WizCommon.Procedure pro6 = new WizCommon.Procedure();
                        pro6.Name = "xp_prdWork_iWkResultStuffInOut";
                        pro6.OutputUseYN = "N";
                        pro6.OutputName = "JobID";
                        pro6.OutputLength = "20";

                        Prolist.Add(pro6);
                        ListParameter.Add(sqlParameter5);


                        //}
                    }
                }
          
                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    list_Result.RemoveAt(0);

                    //int rsCount = list_Result.Count / 2;//2 = Output갯수(JobID, LabelID)
                    int a = 0;
                    for (int i = 0; i < list_Result.Count; i++)
                    {
                        KeyValue kv = list_Result[i];
                        if (kv.key == "LabelID")
                        {
                            list_TWkLabelPrint[a++].sLabelID = kv.value;
                            //list_TWkLabelPrint[i].sLabelID = kv.value;
                        }
                        //else if (kv.key == "JobID")
                        //{
                        //    //list_TWkResult[i / 2].JobID = float.Parse(kv.value);
                        //    list_TWkResult[i].JobID = float.Parse(kv.value);
                        //}
                    }
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

        private bool Main_TSplit_ConnectionEvent()
        {
            try
            {
                // 저장하려 하는데, Main_TSplit 카운트가 있다.
                // 즉, 앞선 작업자의 데이터를 더해 쓰려고 한다.
                // 그때, 먼저 이루어져야 하는 작업 리스트 모음입니다.
                // 2020.03.10  허윤구.. (난 이게 한계야...미안해.ㅠㅠ)

                //1. 가져와서 쓰려 하는 리스트의 남은 잔량만큼 같은 JobID로 한번 더 Insert.
                //  Split의 JobID로 Groupby 하면 생산박스 수량만큼 두명의 작업자가 뜨게끔.

                list_TWkResult_SplitAdd = new List<Sub_TWkResult_SplitAdd>();

                for (int i = 0; i < Frm_tprc_Main.list_g_tsplit.Count; i++)
                {
                    list_TWkResult_SplitAdd.Add(new Sub_TWkResult_SplitAdd());
                    list_TWkResult_SplitAdd[i].JobID = Frm_tprc_Main.list_g_tsplit[i].JobID;
                    list_TWkResult_SplitAdd[i].SplitSeq = 2;
                    list_TWkResult_SplitAdd[i].WorkPersonID = Frm_tprc_Main.g_tBase.PersonID;
                    list_TWkResult_SplitAdd[i].WorkQty = Lib.GetDouble(txtSetCT.Text) - Frm_tprc_Main.list_g_tsplit[i].Qty;
                    list_TWkResult_SplitAdd[i].ScanDate = "";

                    list_TWkResult_SplitAdd[i].ScanTime = "";
                    list_TWkResult_SplitAdd[i].WorkStartDate = mtb_From.Text.Replace("-", "");
                    list_TWkResult_SplitAdd[i].WorkEndDate = mtb_To.Text.Replace("-", "");
                    list_TWkResult_SplitAdd[i].WorkStartTime = dtStartTime.Value.ToString("HHmmss");
                    list_TWkResult_SplitAdd[i].WorkEndTime = dtEndTime.Value.ToString("HHmmss");

                    list_TWkResult_SplitAdd[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                    //////////////////////////////////////////////////////////////////////////////////////
                    ///

                }

                List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
                List<List<string>> ListProcedureName = new List<List<string>>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                for (int i = 0; i < list_TWkResult_SplitAdd.Count; i++)
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("JobID", list_TWkResult_SplitAdd[i].JobID);
                    sqlParameter.Add("SplitSeq", list_TWkResult_SplitAdd[i].SplitSeq);
                    sqlParameter.Add("WorkPersonID", list_TWkResult_SplitAdd[i].WorkPersonID);
                    sqlParameter.Add("WorkQty", list_TWkResult_SplitAdd[i].WorkQty);
                    sqlParameter.Add("ScanDate", list_TWkResult_SplitAdd[i].ScanDate);

                    sqlParameter.Add("ScanTime", list_TWkResult_SplitAdd[i].ScanTime);
                    sqlParameter.Add("WorkStartDate", list_TWkResult_SplitAdd[i].WorkStartDate);
                    sqlParameter.Add("WorkEndDate", list_TWkResult_SplitAdd[i].WorkEndDate);
                    sqlParameter.Add("WorkStartTime", list_TWkResult_SplitAdd[i].WorkStartTime);
                    sqlParameter.Add("WorkEndTime", list_TWkResult_SplitAdd[i].WorkEndTime);

                    sqlParameter.Add("CreateUserID", list_TWkResult_SplitAdd[i].CreateUserID);

                    WizCommon.Procedure pro = new WizCommon.Procedure();
                    pro.Name = "xp_wkResult_iWkResult_Split";
                    pro.OutputUseYN = "N";
                    pro.OutputName = "JobID";
                    pro.OutputLength = "20";

                    Prolist.Add(pro);
                    ListParameter.Add(sqlParameter);


                    // 2. TSPLIT 리스트의 JobID를 기준으로,
                    //  기존에 넣어 둔 부족불량의 WK_RESULT와 LABEL의 QTY를 Update 쳐야 합니다. (+) split의 useclss = *
                    //  생산박스 수량과 동일해야 하는데, 이론상으로는 지금 넣는 splitqty + 기 투입량을 하면, prod_qtyperbox.
                    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                    sqlParameter2.Add("JobID", list_TWkResult_SplitAdd[i].JobID);
                    sqlParameter2.Add("WorkQty", list_TWkResult_SplitAdd[i].WorkQty);

                    WizCommon.Procedure pro2 = new WizCommon.Procedure();
                    pro2.Name = "xp_WizWork_uSplit_ConnectionQty";
                    pro2.OutputUseYN = "N";
                    pro2.OutputName = "JobID";
                    pro2.OutputLength = "20";

                    Prolist.Add(pro2);
                    ListParameter.Add(sqlParameter2);



                    // 3. 기존에 적은분량으로 들어간 JobID의 StuffinOut 기록을 제거하고,
                    Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();
                    sqlParameter3.Add("JobID", list_TWkResult_SplitAdd[i].JobID);

                    WizCommon.Procedure pro3 = new WizCommon.Procedure();
                    pro3.Name = "xp_WizWork_dStuffinOut_SplitBasicData";
                    pro3.OutputUseYN = "N";
                    pro3.OutputName = "JobID";
                    pro3.OutputLength = "20";

                    Prolist.Add(pro3);
                    ListParameter.Add(sqlParameter3);



                    //  4. 다시 stuffinout 타기.
                    // prod_qtyperbox의 수가 나오도록 업데이트 쳤으니까, 그 수에 맞는 stuffinout으로 다시타기.
                    Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();

                    sqlParameter4.Add("JobID", list_TWkResult_SplitAdd[i].JobID);
                    sqlParameter4.Add("CreateUserID", list_TWkResult_SplitAdd[i].CreateUserID);
                    sqlParameter4.Add("sRtnMsg", "");

                    WizCommon.Procedure pro4 = new WizCommon.Procedure();
                    pro4.Name = "xp_wkResult_iWkResultStuffInOut";
                    pro4.OutputUseYN = "N";
                    pro4.OutputName = "JobID";
                    pro4.OutputLength = "20";

                    Prolist.Add(pro4);
                    ListParameter.Add(sqlParameter4);


                }

                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    list_Result.RemoveAt(0);
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






        private void GetMtrChileLotRemainQty(string strBoxID, string strProcessID, string ProdQty)
        {
            DataSet ds = null;
            DataRow dr = null;
            int iChildQty = 0;
            string[] ChildArticleID = null;
            string[] ChildLotID = null;
            string[] StuffinRemainQty = null;
            string[] ChildRnk = null;
            string[] UnitClss = null;
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("sParentLotID", strBoxID);//상위품ID
                sqlParameter.Add("ProcessID", strProcessID);
                if (m_ProcessID == "0405")//마지막 스캔한 바코드 기준의 UnitClss
                {
                    double douProdQty = 0;
                    double.TryParse(ProdQty, out douProdQty);
                    douProdQty = douProdQty / 1000;
                    sqlParameter.Add("nWorkQty", douProdQty);
                    sqlParameter.Add("UnitClss", 1);
                }
                else
                {
                    sqlParameter.Add("nWorkQty", ProdQty);
                    sqlParameter.Add("UnitClss", 2);
                }

                ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_ChkChildLotQty]", sqlParameter, false);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    int Count = ds.Tables[0].Rows.Count;

                    ChildArticleID = new string[Count];
                    ChildLotID = new string[Count];
                    StuffinRemainQty = new string[Count];
                    ChildRnk = new string[Count];
                    UnitClss = new string[Count];
                    double douStuffinRemainQty = 0;
                    double douReqQty = 0;
                    double douProdQty = 0;

                    for (int i = 0; i < Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];

                        iChildQty++;
                        ChildArticleID[i] = dr["ChildArticleID"].ToString();
                        ChildLotID[i] = dr["ChildLotID"].ToString();
                        StuffinRemainQty[i] = dr["StuffinRemainQty"].ToString();
                        ChildRnk[i] = dr["Rnk"].ToString();
                        UnitClss[i] = dr["UnitClss"].ToString();
                    }

                    for (int i = 0; i < GridData2.Rows.Count; i++)
                    {
                        if (GridData2.Rows[i].Cells["ScanExceptYN1"].Value.ToString() == "N")
                        {
                            for (int j = 0; j < iChildQty - 1; j++)
                            {
                                if (GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString() == ChildArticleID[j])
                                {
                                    if (ChildRnk[j] == "1" && GridData2.Rows[i].Cells["BarCode"].Value.ToString().Trim().ToUpper() == ChildLotID[j].Trim().ToUpper())
                                    {
                                        double.TryParse(StuffinRemainQty[j], out douStuffinRemainQty);
                                        double.TryParse(GridData2.Rows[i].Cells["ReqQty"].Value.ToString(), out douReqQty);
                                        double.TryParse(ProdQty, out douProdQty);

                                        if (GridData2.Rows[i].Cells["UnitClss"].Value.ToString() == UnitClss[j])//단위가 같을때
                                        {
                                            if (douStuffinRemainQty < douProdQty * douReqQty)
                                            {
                                                throw new Exception("생산수량이 현투입 자재량을 초과합니다.");
                                            }
                                        }
                                        else//단위가 다를때
                                        {
                                            if (UnitClss[j] == "1")//재고 단위 g
                                            {
                                                if (GridData2.Rows[i].Cells["UnitClss"].Value.ToString() == "2")//하위품 단위 kg
                                                {
                                                    //재고량에 나누기 1000을 해서 kg으로 고쳐서 계산한다.
                                                    if (douStuffinRemainQty / 1000 < douProdQty * douReqQty)
                                                    {
                                                        throw new Exception("생산수량이 현투입 자재량을 초과합니다.");
                                                    }
                                                }
                                                else//하위품의 단위가 kg, g이 아닐때
                                                {
                                                    throw new Exception("선입선출에 위배 되었습니다.");
                                                }
                                            }
                                            else if (UnitClss[j] == "2")//재고 단위 kg
                                            {
                                                if (GridData2.Rows[i].Cells["UnitClss"].Value.ToString() == "1")//하위품 단위 kg
                                                {
                                                    //하위품에 나누기 1000을 해서 kg으로 고쳐서 계산한다.
                                                    if (douStuffinRemainQty < douProdQty * douReqQty / 1000)
                                                    {
                                                        throw new Exception("생산수량이 현투입 자재량을 초과합니다.");
                                                    }
                                                }
                                                else//하위품의 단위가 kg, g이 아닐때
                                                {
                                                    throw new Exception("선입선출에 위배 되었습니다.");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (GridData2.Rows[i].Cells["BarCode"].Value.ToString().Trim().ToUpper() != ChildLotID[j].Trim().ToUpper())
                                        {
                                            throw new Exception("선입선출에 위배 되었습니다. \r\n 해당부품의 출고대상은 LOT ID는 " + ChildLotID[j] + "입니다.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<GetMtrChileLotRemainQty>\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return;
            }
        }
        /// <summary>
        /// '공정이동전표의 정보 가져오기
        /// </summary>
        /// <param name="strLotID"></param>
        /// <param name="strProcessID"></param>
        /// <param name="strMachineID"></param>
        /// <param name="strMoldIDList"></param>
        private void GetWorkLotInfo(string strLotID, string strProcessID, string strMachineID, string strMoldIDList)
        {

            DataSet ds = null;
            DataRow dr = null;
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("PLotID", strLotID);
                sqlParameter.Add("ProcessID", strProcessID);
                sqlParameter.Add("MachineID", strMachineID);
                sqlParameter.Add("MoldIDList", strMoldIDList);

                ds = DataStore.Instance.ProcedureToDataSet("xp_wkresult_sWorkLotID", sqlParameter, false);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    double InstQty = 0;
                    double WorkQty = 0;
                    double InstRemainQty = 0;
                    double.TryParse(Lib.GetDouble(dr["InstQty"].ToString()).ToString(), out InstQty);
                    double.TryParse(Lib.GetDouble(dr["WorkQty"].ToString()).ToString(), out WorkQty);
                    InstRemainQty = InstQty - WorkQty;
                    sTdGbn = Lib.CheckNull(dr["TdGbn"].ToString());
                    txtlInstQty.Text = string.Format("{0:n3}", InstQty);
                    txtInstRemainQty.Text = string.Format("{0:n3}", InstRemainQty);

                    if (Frm_tprc_Main.list_tMold.Count > 0)
                    {
                        string strMoldIDCheck = "";
                        strMoldIDCheck = dr["MoldIDCheck"].ToString();
                        if (!(int.Parse(Lib.CheckNum(strMoldIDCheck)) == Frm_tprc_Main.list_tMold.Count))
                        {
                            Message[0] = "[금형 오류]";
                            Message[1] = "선택된 금형은 이 품목의 금형이 아닙니다.";
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                            return;
                        }
                        // 'AFT 는 금형 Cavity 가 1 이므로 쓰고 다른 업체일 경우 변경 필요 >> ?? 이해안감..
                        double douHitCount = 0;
                        double douRealCavity = 0;
                        double douSafeHitCount = 0;
                        for (int i = 0; i < Frm_tprc_Main.list_tMold.Count; i++)
                        {
                            double.TryParse(Lib.GetDouble(Frm_tprc_Main.list_tMold[i].sHitCount.ToString()).ToString(), out douHitCount);
                            double.TryParse(Lib.GetDouble(Frm_tprc_Main.list_tMold[i].sRealCavity.ToString()).ToString(), out douRealCavity);                            
                            double.TryParse(Lib.GetDouble(Frm_tprc_Main.list_tMold[i].sSafeHitCount.ToString()).ToString(), out douSafeHitCount);

                            if (douHitCount /*+ douWorkQty / douRealCavity */> douSafeHitCount)
                            {
                                Message[0] = "[금형 오류]";
                                Message[1] = "선택된 금형 중 생산 진행 시 타발수가 한계수명을 넘어가는 금형이 있습니다. (" + Frm_tprc_Main.list_tMold[i].sLotNo + ")";
                                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<GetWorkLotInfo>\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        private bool CheckID(string strBoxID)
        {
            try
            {
                if (!BarCodeCheck(strBoxID))
                {
                    throw new Exception();
                }
                if (GridData2.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in GridData2.Rows)
                    {
                        if (dgvr.Cells["BarCode"].Value.ToString().Trim() == "")
                        {
                            Message[0] = "[하위품 체크 오류]";
                            Message[1] = dgvr.Cells["Article"].Value.ToString() + "\r\n" + "하위품이 선택되지 않았습니다. 하위품을 스캔해주십시오.";
                            throw new Exception();
                        }
                    }
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

        private string GetItemText(int inti)
        {
            string sText = "";
            int nLen = 0;
            string sChar = "";
            int iIdx = 0;
            string sTempText = "";
            string sTempChar = "";
            if (list_m_tItem.Count > 0)
            {
                //list_m_tItem[inti]
                sText = m_sData[list_m_tItem[inti].nRelation].Trim();
                nLen = Strings.Len(Strings.StrConv(sText, VbStrConv.Narrow, 0));

                if (list_m_tItem[inti].nSpace == 0)
                {
                    sChar = " ";
                }
                else if (list_m_tItem[inti].nSpace == 1)
                {
                    sChar = "0";
                }
                if (list_m_tItem[inti].nAlign == 0)
                {
                    if (nLen > list_m_tItem[inti].nLength)
                    {
                        iIdx = 1;
                        sTempText = "";
                        for (int i = 1; i < Math.Abs(list_m_tItem[inti].nLength); i++)
                        {
                            sTempChar = Strings.Mid(sText, iIdx, 1);
                            iIdx++;
                            sTempText = sTempText + sTempChar;
                            if (!IsHangul(sTempChar))
                            {
                                break;
                            }
                        }
                        sText = sTempText;
                    }
                    else if (nLen < list_m_tItem[inti].nLength)
                    {
                        for (int i = nLen; i < list_m_tItem[inti].nLength - 1; i++)

                        //for (int i = nLen; i < list_m_tItem[inti].nLength; i++)
                        {
                            sText = sText + sChar;
                        }
                    }
                }
                else
                {
                    if (nLen > list_m_tItem[inti].nLength)
                    {
                        iIdx = Strings.Len(sText);
                        sTempText = "";
                        for (int i = 1; i < Math.Abs(list_m_tItem[inti].nLength); i++)
                        {
                            sTempChar = Strings.Mid(sText, iIdx, 1);
                            iIdx--;
                            sTempText = sTempChar + sTempText;
                            if (!IsHangul(sTempChar))
                            {
                                break;
                            }
                        }
                        sText = sTempText;
                    }
                    else if (nLen < list_m_tItem[inti].nLength)
                    {
                        for (int i = nLen; i < list_m_tItem[inti].nLength - 1; i++)
                        {
                            sText = sChar + sText;
                        }
                    }
                }
            }
            return sText;

        }

        private bool IsHangul(string sText)
        {
            return Strings.Len(sText) == Strings.Len(Strings.StrConv(sText, VbStrConv.Narrow, 0)) ? true : false;
        }

        private string GetBarCodeItemText(int inti)
        {
            string _GetBarCodeItemText = "";
            _GetBarCodeItemText = GetItemText(inti);
            for (int i = 0; i < list_m_tItem.Count; i++)
            {
                if (list_m_tItem[i].nPrevItem - 1 == inti)
                {
                    _GetBarCodeItemText = _GetBarCodeItemText + GetItemText(i);
                    break;
                }
            }
            return _GetBarCodeItemText;
        }


        public bool SendWindowDllCommand(List<string> vData, string sTagID, int nPrintCount, int nDefectCnt)
        {
            try
            {
                #region 2020.05.29 이전

                //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //sqlParameter.Add("TagID", "008");
                //DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sMtTag]", sqlParameter, false);

                //if (dt != null W&& dt.Rows.Count > 0)
                //{
                //    DataRow dr = dt.Rows[0];
                //    Sub_m_tTag.sTagID = Lib.CheckNull(dr["TagID"].ToString());
                //    Sub_m_tTag.sTag = Lib.CheckNull(dr["Tag"].ToString());
                //    Sub_m_tTag.nWidth = int.Parse(dr["Width"].ToString());
                //    Sub_m_tTag.nHeight = int.Parse(dr["Height"].ToString());
                //    //Sub_m_tTag.sUse_YN = dr["clss"].ToString();

                //    Sub_m_tTag.nDefHeight = int.Parse(dr["DefHeight"].ToString());
                //    Sub_m_tTag.nDefBaseY = int.Parse(dr["DefBaseY"].ToString());
                //    Sub_m_tTag.nDefBaseX1 = int.Parse(dr["DefBaseX1"].ToString());
                //    Sub_m_tTag.nDefBaseX2 = int.Parse(dr["DefBaseX2"].ToString());
                //    Sub_m_tTag.nDefBaseX3 = int.Parse(dr["DefBaseX3"].ToString());

                //    Sub_m_tTag.nDefGapY = int.Parse(dr["DefGapY"].ToString());
                //    Sub_m_tTag.nDefGapX1 = int.Parse(dr["DefGapX1"].ToString());
                //    Sub_m_tTag.nDefGapX2 = int.Parse(dr["DefGapX2"].ToString());
                //    Sub_m_tTag.nDefLength = int.Parse(dr["DefLength"].ToString());
                //    Sub_m_tTag.nDefHCount = int.Parse(dr["DefHCount"].ToString());

                //    Sub_m_tTag.nDefBarClss = int.Parse(dr["DefBarClss"].ToString());
                //    Sub_m_tTag.nGap = int.Parse(dr["Gap"].ToString());
                //    Sub_m_tTag.sDirect = dr["Direct"].ToString();
                //}

                //dt = null;
                //Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                //sqlParameter2.Add("TagID", "009");
                //dt = DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sMtTagSub]", sqlParameter, false);

                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        DataRow dr = dt.Rows[i];

                //        list_m_tItem.Add(new TTagSub());

                //        //list_m_tItem[i]' .sTag_ID = int.Parse(dr["TagID"].ToString());
                //        //list_m_tItem[i]' .sTag_Seq = 	int.Parse(dr["TagSeq"].ToString());
                //        list_m_tItem[i].sName = dr["Name"].ToString();
                //        list_m_tItem[i].nType = int.Parse(dr["Type"].ToString());
                //        list_m_tItem[i].nAlign = int.Parse(dr["Align"].ToString());
                //        list_m_tItem[i].x = int.Parse(dr["x"].ToString());
                //        list_m_tItem[i].y = int.Parse(dr["y"].ToString());
                //        list_m_tItem[i].nFont = int.Parse(dr["Font"].ToString());
                //        list_m_tItem[i].nLength = int.Parse(dr["Length"].ToString());
                //        list_m_tItem[i].nHMulti = int.Parse(dr["HMulti"].ToString());
                //        list_m_tItem[i].nVMulti = int.Parse(dr["VMulti"].ToString());
                //        list_m_tItem[i].nRelation = int.Parse(dr["Relation"].ToString());
                //        list_m_tItem[i].nRotation = int.Parse(dr["Rotation"].ToString());
                //        list_m_tItem[i].nSpace = int.Parse(dr["Space"].ToString());

                //        list_m_tItem[i].nPrevItem = int.Parse(dr["PrevItem"].ToString());
                //        list_m_tItem[i].nBarType = int.Parse(dr["BarType"].ToString());
                //        list_m_tItem[i].nBarHeight = int.Parse(dr["BarHeight"].ToString());
                //        list_m_tItem[i].nFigureWidth = int.Parse(dr["FigureWidth"].ToString());
                //        list_m_tItem[i].nFigureHeight = int.Parse(dr["FigureHeight"].ToString());
                //        list_m_tItem[i].nThickness = int.Parse(dr["Thickness"].ToString());
                //        list_m_tItem[i].sImageFile = dr["ImageFile"].ToString();
                //        list_m_tItem[i].nWidth = int.Parse(dr["Width"].ToString());
                //        list_m_tItem[i].nHeight = int.Parse(dr["Height"].ToString());
                //        list_m_tItem[i].nVisible = int.Parse(dr["Visible"].ToString());

                //        list_m_tItem[i].sFontName = dr["FontName"].ToString();
                //        list_m_tItem[i].sFontStyle = dr["FontStyle"].ToString();
                //        list_m_tItem[i].sFontUnderLine = dr["FontUnderLine"].ToString();


                //        //int a = 0;
                //        //foreach (string str in lData)
                //        //{
                //        //    Console.WriteLine(a++.ToString() + "/////" + str + "///////");
                //        //}

                //        //20171011 김종영 수정 type 변경
                //        //if (list_m_tItem[i].nType == 1 && list_m_tItem[i].sName.Substring(0, 1).ToUpper() == "D")
                //        //if (list_m_tItem[i].nType < 2 && list_m_tItem[i].sName.Substring(0, 1).ToUpper() == "D")
                //        //{
                //        //    if (list_m_tItem[i].nRelation == 0 && list_m_tItem[i].nType == 1)//바코드
                //        //    {
                //        //        list_m_tItem[i].sText = vData[0];
                //        //    }

                //        //    else if (list_m_tItem[i].nRelation > 0 && list_m_tItem[i].nType == 0)
                //        //    {
                //        //        if (vData.Count > list_m_tItem[i].nRelation)
                //        //        {
                //        //            list_m_tItem[i].sText = vData[list_m_tItem[i].nRelation];
                //        //        }
                //        //        else
                //        //        {
                //        //            list_m_tItem[i].sText = "";
                //        //        }
                //        //    }
                //        //}
                //        //else
                //        //{
                //        //    list_m_tItem[i].sText = Lib.CheckNull(dr["Text"].ToString());
                //        //}

                //        //20171011 김종영 수정 type 변경
                //        //if (list_m_tItem[i].nType == 1 && list_m_tItem[i].sName.Substring(0, 1).ToUpper() == "D")
                //        if ((list_m_tItem[i].nType < 2 || list_m_tItem[i].nType == 8) && list_m_tItem[i].sName.Substring(0, 1).ToUpper() == "D")
                //        {
                //            if (list_m_tItem[i].nRelation == 0 && list_m_tItem[i].nType == 8)//바코드
                //            {
                //                list_m_tItem[i].sText = vData[0];
                //            }

                //            else if (list_m_tItem[i].nRelation > 0 && list_m_tItem[i].nType == 0)
                //            {
                //                if (vData.Count > list_m_tItem[i].nRelation)
                //                {
                //                    list_m_tItem[i].sText = vData[list_m_tItem[i].nRelation];
                //                }
                //                else
                //                {
                //                    list_m_tItem[i].sText = "";
                //                }
                //            }
                //        }
                //        else
                //        {
                //            list_m_tItem[i].sText = dr["Text"].ToString();
                //        }
                //    }
                //}

                //string strWidth = "";
                //string strHeight = "";
                //try
                //{
                //    if (Lib.CheckNum(Sub_m_tTag.nWidth.ToString()) != "0")
                //    {
                //        strWidth = (Sub_m_tTag.nWidth / 10F).ToString();
                //    }
                //    if (Lib.CheckNum(Sub_m_tTag.nHeight.ToString()) != "0")
                //    {
                //        strHeight = (Sub_m_tTag.nHeight / 10F).ToString();
                //    }
                //}
                //catch
                //{
                //    strWidth = "0";
                //    strHeight = "0";
                //}

                //TSCLIB_DLL.setup(strWidth, strHeight, "8", "15", "0", "3", "0");//기존소스
                ////TSCLIB_DLL.setup(strWidth, strHeight, "8", "15", "0", "0", "0");//감열지 테스트용
                //TSCLIB_DLL.sendcommand("DIRECTION " + Sub_m_tTag.sDirect);

                //TSCLIB_DLL.clearbuffer();
                //string sText = "";
                //string[] sBarType = new string[2];

                //for (int i = 0; i < list_m_tItem.Count; i++)
                //{
                //    if (list_m_tItem[i].nVisible > 0)//출력여부
                //    {
                //        //'QR CODE
                //        if (list_m_tItem[i].nType == EnumItem.IO_QRcode)
                //        {
                //            //QRCODE x, y, ECC Level,cell width, mode, rotation,[model, mask,]"content"

                //            string qr_command = "QRCODE " + list_m_tItem[i].x.ToString() + "," +
                //                        list_m_tItem[i].y.ToString() + "," +
                //                        "L" + "," +     // ECC Level (L,M,Q,H)
                //                        list_m_tItem[i].nFigureWidth.ToString() + "," +
                //                        "M" + "," +         // MODE (A,M)
                //                        "0" + "," +
                //                        "M2" + "," +
                //                        "S1" + "," +
                //                        "\"A" + list_m_tItem[0].sText + "\"";


                //            //string qr_command = "QRCODE 100,80,L,7,M,0,M2,S1," + "\"A" + list_m_tItem[0].sText + "\"";

                //            TSCLIB_DLL.sendcommand(qr_command);

                //            //TSCLIB_DLL.windowsfont(lstTagSub[i].x, lstTagSub[i].y, fontheight, rotation, fontstyle, fontunderline, szFaceName, content);
                //        }
                //        //'바코드
                //        else if (list_m_tItem[i].nType == EnumItem.IO_BARCODE)
                //        {
                //            if (list_m_tItem[i].nPrevItem == 0)
                //            {
                //                if (list_m_tItem[i].nBarType == 0)// 1:1 Code
                //                {
                //                    sBarType[0] = "1";
                //                    sBarType[1] = "1";
                //                }
                //                else                            // 2:5 Code
                //                {
                //                    sBarType[0] = "2";
                //                    sBarType[1] = "5";
                //                }
                //                TSCLIB_DLL.barcode(list_m_tItem[i].x.ToString(), // x
                //                                   list_m_tItem[i].y.ToString(), // x
                //                                   "39", // type
                //                                   list_m_tItem[i].nBarHeight.ToString(), // 높이
                //                                   "1", // ReadAble
                //                                   list_m_tItem[i].nRotation.ToString(), // Rotation
                //                                   sBarType[0], // Narrow
                //                                   sBarType[1], // Wide
                //                                   list_m_tItem[0].sText
                //                                   );
                //            }
                //        }
                //        //데이터 OR 문자
                //        else if (list_m_tItem[i].nType == EnumItem.IO_DATA || list_m_tItem[i].nType == EnumItem.IO_TEXT)
                //        {
                //            sText = Lib.CheckNull(list_m_tItem[i].sText);
                //            int intx = list_m_tItem[i].x;
                //            int inty = list_m_tItem[i].y;
                //            int fontheight = int.Parse((list_m_tItem[i].nFont).ToString());
                //            int rotation = list_m_tItem[i].nRotation;
                //            int fontstyle = int.Parse(Lib.CheckNum(list_m_tItem[i].sFontStyle));
                //            int fontunderline = int.Parse(Lib.CheckNum(list_m_tItem[i].sFontUnderLine));
                //            string szFaceName = list_m_tItem[i].sFontName;
                //            string content = sText.Trim();

                //            TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, szFaceName, content);
                //        }
                //        //'선(Line)-5이하
                //        else if (list_m_tItem[i].nType == EnumItem.IO_LINE)// && (list_m_tItem[i].nFigureHeight <= 5 || list_m_tItem[i].nFigureWidth <= 5))
                //        {
                //            int x1 = 0;
                //            int x2 = 0;
                //            int y1 = 0;
                //            int y2 = 0;
                //            int.TryParse(list_m_tItem[i].x.ToString(), out x1);
                //            int.TryParse(list_m_tItem[i].y.ToString(), out y1);
                //            int.TryParse(list_m_tItem[i].nFigureWidth.ToString(), out x2);
                //            int.TryParse(list_m_tItem[i].nFigureHeight.ToString(), out y2);

                //            string IsDllStr = "BAR " + x1.ToString() + ", " + y1.ToString() + ", " + x2.ToString() + ", " + y2.ToString();

                //            TSCLIB_DLL.sendcommand(IsDllStr);
                //        }
                //        else if (list_m_tItem[i].nType == EnumItem.IO_BOX)
                //        {
                //            int x1 = 0;
                //            int x2 = 0;
                //            int y1 = 0;
                //            int y2 = 0;
                //            int nTh = 0;
                //            int.TryParse(list_m_tItem[i].x.ToString(), out x1);
                //            int.TryParse(list_m_tItem[i].y.ToString(), out y1);
                //            int.TryParse(list_m_tItem[i].nFigureWidth.ToString(), out x2);
                //            int.TryParse(list_m_tItem[i].nFigureHeight.ToString(), out y2);
                //            int.TryParse(list_m_tItem[i].nThickness.ToString(), out nTh);

                //            string IsDllStr = "BOX " + x1.ToString() + ", " + y1.ToString() + ", " + x2.ToString() + ", " + y2.ToString() + ", " + nTh.ToString();

                //            TSCLIB_DLL.sendcommand(IsDllStr);
                //        }

                //    }
                //}
                ////if (m_ProcessID == "0405")
                ////{
                ////    nPrintCount = 2;
                ////}

                //TSCLIB_DLL.printlabel("1", nPrintCount.ToString());

                //list_m_tItem = new List<TTagSub>();
                //vData = new List<string>();
                //return true;

                #endregion

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("TagID", sTagID);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sMtTag]", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    Sub_m_tTag.sTagID = Lib.CheckNull(dr["TagID"].ToString());
                    Sub_m_tTag.sTag = Lib.CheckNull(dr["Tag"].ToString());
                    Sub_m_tTag.nWidth = int.Parse(dr["Width"].ToString());
                    Sub_m_tTag.nHeight = int.Parse(dr["Height"].ToString());
                    //Sub_m_tTag.sUse_YN = dr["clss"].ToString();

                    Sub_m_tTag.nDefHeight = int.Parse(dr["DefHeight"].ToString());
                    Sub_m_tTag.nDefBaseY = int.Parse(dr["DefBaseY"].ToString());
                    Sub_m_tTag.nDefBaseX1 = int.Parse(dr["DefBaseX1"].ToString());
                    Sub_m_tTag.nDefBaseX2 = int.Parse(dr["DefBaseX2"].ToString());
                    Sub_m_tTag.nDefBaseX3 = int.Parse(dr["DefBaseX3"].ToString());

                    Sub_m_tTag.nDefGapY = int.Parse(dr["DefGapY"].ToString());
                    Sub_m_tTag.nDefGapX1 = int.Parse(dr["DefGapX1"].ToString());
                    Sub_m_tTag.nDefGapX2 = int.Parse(dr["DefGapX2"].ToString());
                    Sub_m_tTag.nDefLength = int.Parse(dr["DefLength"].ToString());
                    Sub_m_tTag.nDefHCount = int.Parse(dr["DefHCount"].ToString());

                    Sub_m_tTag.nDefBarClss = int.Parse(dr["DefBarClss"].ToString());
                    Sub_m_tTag.nGap = int.Parse(dr["Gap"].ToString());
                    Sub_m_tTag.sDirect = dr["Direct"].ToString();
                }

                dt = null;
                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                sqlParameter2.Add("TagID", sTagID);
                dt = DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sMtTagSub]", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        list_m_tItem.Add(new TTagSub());

                        //list_m_tItem[i]' .sTag_ID = int.Parse(dr["TagID"].ToString());
                        //list_m_tItem[i]' .sTag_Seq = 	int.Parse(dr["TagSeq"].ToString());
                        list_m_tItem[i].sName = dr["Name"].ToString();
                        list_m_tItem[i].nType = int.Parse(dr["Type"].ToString());
                        list_m_tItem[i].nAlign = int.Parse(dr["Align"].ToString());
                        list_m_tItem[i].x = int.Parse(dr["x"].ToString());
                        list_m_tItem[i].y = int.Parse(dr["y"].ToString());
                        list_m_tItem[i].nFont = int.Parse(dr["Font"].ToString());
                        list_m_tItem[i].nLength = int.Parse(dr["Length"].ToString());
                        list_m_tItem[i].nHMulti = int.Parse(dr["HMulti"].ToString());
                        list_m_tItem[i].nVMulti = int.Parse(dr["VMulti"].ToString());
                        list_m_tItem[i].nRelation = int.Parse(dr["Relation"].ToString());
                        list_m_tItem[i].nRotation = int.Parse(dr["Rotation"].ToString());
                        list_m_tItem[i].nSpace = int.Parse(dr["Space"].ToString());

                        list_m_tItem[i].nPrevItem = int.Parse(dr["PrevItem"].ToString());
                        list_m_tItem[i].nBarType = int.Parse(dr["BarType"].ToString());
                        list_m_tItem[i].nBarHeight = int.Parse(dr["BarHeight"].ToString());
                        list_m_tItem[i].nFigureWidth = int.Parse(dr["FigureWidth"].ToString());
                        list_m_tItem[i].nFigureHeight = int.Parse(dr["FigureHeight"].ToString());
                        list_m_tItem[i].nThickness = int.Parse(dr["Thickness"].ToString());
                        list_m_tItem[i].sImageFile = dr["ImageFile"].ToString();
                        list_m_tItem[i].nWidth = int.Parse(dr["Width"].ToString());
                        list_m_tItem[i].nHeight = int.Parse(dr["Height"].ToString());
                        list_m_tItem[i].nVisible = int.Parse(dr["Visible"].ToString());

                        list_m_tItem[i].sFontName = dr["FontName"].ToString();
                        list_m_tItem[i].sFontStyle = dr["FontStyle"].ToString();
                        list_m_tItem[i].sFontUnderLine = dr["FontUnderLine"].ToString();


                        //int a = 0;
                        //foreach (string str in lData)
                        //{
                        //    Console.WriteLine(a++.ToString() + "/////" + str + "///////");
                        //}

                        //20171011 김종영 수정 type 변경
                        //if (list_m_tItem[i].nType == 1 && list_m_tItem[i].sName.Substring(0, 1).ToUpper() == "D")
                        if ((list_m_tItem[i].nType < 2 || list_m_tItem[i].nType == 8) && list_m_tItem[i].sName.Substring(0, 1).ToUpper() == "D")
                        {
                            if (list_m_tItem[i].nRelation == 0 && list_m_tItem[i].nType == 1)//바코드
                            {
                                list_m_tItem[i].sText = vData[0];
                            }
                            else if (list_m_tItem[i].nRelation == 0 && list_m_tItem[i].nType == 8)// QR
                            {
                                list_m_tItem[i].sText = vData[0];
                            }
                            else if (list_m_tItem[i].nRelation > 0 && list_m_tItem[i].nType == 0)
                            {
                                if (vData.Count > list_m_tItem[i].nRelation)
                                {
                                    list_m_tItem[i].sText = vData[list_m_tItem[i].nRelation];
                                }
                                else
                                {
                                    list_m_tItem[i].sText = "";
                                }
                            }
                        }
                        else
                        {
                            list_m_tItem[i].sText = Lib.CheckNull(dr["Text"].ToString());
                        }
                    }
                }

                string strWidth = "";
                string strHeight = "";
                try
                {
                    if (Lib.CheckNum(Sub_m_tTag.nWidth.ToString()) != "0")
                    {
                        strWidth = (Sub_m_tTag.nWidth / 10F).ToString();
                    }
                    if (Lib.CheckNum(Sub_m_tTag.nHeight.ToString()) != "0")
                    {
                        strHeight = (Sub_m_tTag.nHeight / 10F).ToString();
                    }
                }
                catch
                {
                    strWidth = "0";
                    strHeight = "0";
                }

                TSCLIB_DLL.setup(strWidth, strHeight, "8", "15", "0", "3", "0");//기존소스
                //TSCLIB_DLL.setup(strWidth, strHeight, "8", "15", "0", "0", "0");//감열지 테스트용
                TSCLIB_DLL.sendcommand("DIRECTION " + Sub_m_tTag.sDirect);

                TSCLIB_DLL.clearbuffer();
                string sText = "";
                string[] sBarType = new string[2];

                for (int i = 0; i < list_m_tItem.Count; i++)
                {
                    if (list_m_tItem[i].nVisible > 0)//출력여부
                    {

                        //'QR CODE
                        if (list_m_tItem[i].nType == EnumItem.IO_QRcode)
                        {
                            //QRCODE x, y, ECC Level,cell width, mode, rotation,[model, mask,]"content"

                            string qr_command = "QRCODE " + list_m_tItem[i].x.ToString() + "," +
                                        list_m_tItem[i].y.ToString() + "," +
                                        "L" + "," +     // ECC Level (L,M,Q,H)
                                        list_m_tItem[i].nFigureWidth.ToString() + "," +
                                        "M" + "," +         // MODE (A,M)
                                        "0" + "," +
                                        "M2" + "," +
                                        "S1" + "," +
                                        "\"A" + list_m_tItem[0].sText + "\"";


                            //string qr_command = "QRCODE 100,80,L,7,M,0,M2,S1," + "\"A" + list_m_tItem[0].sText + "\"";

                            TSCLIB_DLL.sendcommand(qr_command);

                            //TSCLIB_DLL.windowsfont(lstTagSub[i].x, lstTagSub[i].y, fontheight, rotation, fontstyle, fontunderline, szFaceName, content);

                            // 위 아래 버전
                            //sText = Lib.CheckNull(list_m_tItem[i].sText);
                            //int intx = list_m_tItem[i].x - 14;
                            //int inty = list_m_tItem[i].y + 128;
                            //int fontheight = 35;
                            //int rotation = 0;
                            //int fontstyle = 0;
                            //int fontunderline = 0;
                            //string szFaceName = "맑은 고딕";
                            //string content = list_m_tItem[0].sText.Trim();

                            sText = Lib.CheckNull(list_m_tItem[i].sText);
                            int intx = list_m_tItem[i].x + 175;
                            int inty = list_m_tItem[i].y + 78; // 26이 중간
                            int fontheight = 60;
                            int rotation = 0;
                            int fontstyle = 0;
                            int fontunderline = 0;
                            string szFaceName = "맑은 고딕";
                            string content = list_m_tItem[0].sText.Trim();

                            TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, szFaceName, content);
                        }
                        //'바코드
                        else if (list_m_tItem[i].nType == EnumItem.IO_BARCODE)
                        {
                            if (list_m_tItem[i].nPrevItem == 0)
                            {
                                if (list_m_tItem[i].nBarType == 0)// 1:1 Code
                                {
                                    sBarType[0] = "1";
                                    sBarType[1] = "1";
                                }
                                else                            // 2:5 Code
                                {
                                    sBarType[0] = "2";
                                    sBarType[1] = "5";
                                }
                                TSCLIB_DLL.barcode(list_m_tItem[i].x.ToString(),
                                                   list_m_tItem[i].y.ToString(),
                                                   "39",
                                                   list_m_tItem[i].nBarHeight.ToString(),
                                                   "1", // Readable
                                                   list_m_tItem[i].nRotation.ToString(),
                                                   sBarType[0], // g: string; sets up narrow bar ratio, refer to TSPL user's manual
                                                   sBarType[1], // h: string; sets up wide bar ratio, refer to TSPL user's manual
                                                   list_m_tItem[0].sText
                                                   );
                            }
                        }
                        //데이터 OR 문자
                        else if (list_m_tItem[i].nType == EnumItem.IO_DATA || list_m_tItem[i].nType == EnumItem.IO_TEXT)
                        {
                            sText = Lib.CheckNull(list_m_tItem[i].sText);
                            int intx = list_m_tItem[i].x;
                            int inty = list_m_tItem[i].y;
                            int fontheight = int.Parse((list_m_tItem[i].nFont).ToString());
                            int rotation = list_m_tItem[i].nRotation;
                            int fontstyle = int.Parse(Lib.CheckNum(list_m_tItem[i].sFontStyle));
                            int fontunderline = int.Parse(Lib.CheckNum(list_m_tItem[i].sFontUnderLine));
                            string szFaceName = list_m_tItem[i].sFontName;
                            string content = sText.Trim();

                            TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, szFaceName, content);
                        }
                        //'선(Line)-5이하
                        else if (list_m_tItem[i].nType == EnumItem.IO_LINE)// && (list_m_tItem[i].nFigureHeight <= 5 || list_m_tItem[i].nFigureWidth <= 5))
                        {
                            int x1 = 0;
                            int x2 = 0;
                            int y1 = 0;
                            int y2 = 0;
                            int.TryParse(list_m_tItem[i].x.ToString(), out x1);
                            int.TryParse(list_m_tItem[i].y.ToString(), out y1);
                            int.TryParse(list_m_tItem[i].nFigureWidth.ToString(), out x2);
                            int.TryParse(list_m_tItem[i].nFigureHeight.ToString(), out y2);

                            string IsDllStr = "BAR " + x1.ToString() + ", " + y1.ToString() + ", " + x2.ToString() + ", " + y2.ToString();

                            TSCLIB_DLL.sendcommand(IsDllStr);
                        }
                        else if (list_m_tItem[i].nType == EnumItem.IO_BOX)
                        {
                            int x1 = 0;
                            int x2 = 0;
                            int y1 = 0;
                            int y2 = 0;
                            int nTh = 0;
                            int.TryParse(list_m_tItem[i].x.ToString(), out x1);
                            int.TryParse(list_m_tItem[i].y.ToString(), out y1);
                            int.TryParse(list_m_tItem[i].nFigureWidth.ToString(), out x2);
                            int.TryParse(list_m_tItem[i].nFigureHeight.ToString(), out y2);
                            int.TryParse(list_m_tItem[i].nThickness.ToString(), out nTh);

                            string IsDllStr = "BOX " + x1.ToString() + ", " + y1.ToString() + ", " + x2.ToString() + ", " + y2.ToString() + ", " + nTh.ToString();

                            TSCLIB_DLL.sendcommand(IsDllStr);
                        }

                    }
                }
                if (m_ProcessID == "0405")
                {
                    nPrintCount = 2;
                }

                TSCLIB_DLL.printlabel("1", nPrintCount.ToString());

                list_m_tItem = new List<TTagSub>();
                vData = new List<string>();
                return true;
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<SendWindowDllCommand>\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return false;
            }
        }


        private void PrintWorkCard(int intPrintCount)
        {
            #region 2020.05.29 이전 구문 
            //string g_sPrinterName = Lib.GetDefaultPrinter();
            //try
            //{
            //    int R = 0;      // Rotation R.
            //    IsTagID = Frm_tprc_Main.g_tBase.TagID;
            //    List<string> list_Data = null;
            //    //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            //    //sqlParameter.Add("InstID", list_TWkLabelPrint[0].sInstID);
            //    //DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_WorkCard_sWorkCard]", sqlParameter, false);

            //    for (int i = 0; i < intPrintCount; i++)
            //    {                    
            //        list_Data = new List<string>();
            //        Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

            //        if (Frm_tprc_Main.list_g_tsplit.Count > i)
            //        {
            //            sqlParameter2.Add("InstID", Frm_tprc_Main.list_g_tsplit[i].InstID);
            //            sqlParameter2.Add("CardID", Frm_tprc_Main.list_g_tsplit[i].LabelID);
            //            R++;
            //        }
            //        else
            //        {
            //            sqlParameter2.Add("InstID", list_TWkLabelPrint[i - R].sInstID);
            //            if (LabelPrintYN == "Y")
            //            {
            //                sqlParameter2.Add("CardID", list_TWkLabelPrint[i - R].sLabelID);
            //            }
            //            else
            //            {
            //                sqlParameter2.Add("CardID", list_TWkResult[i - R].LabelID);
            //            }
            //        }

            //        DataTable dt2 = DataStore.Instance.ProcedureToDataTable("[xp_WorkCard_sWorkCardPrint]", sqlParameter2, false);
            //        lData = new List<string>();
            //        string strProcessID = "";
            //        int a = 0;
            //        int count = 0;
            //        double douworkqty = 0;
            //        double doudefectqty = 0;
            //        string effectdate = "";
            //        foreach (DataRow dr in dt2.Rows)
            //        {
            //            strProcessID = dr["ProcessID"].ToString();
            //            if (dr["ProcSeq"].ToString() == "1" && strProcessID == m_ProcessID) // 첫 공정일 때,
            //            {
            //                double.TryParse(dr["WorkQty"].ToString(), out douworkqty);
            //                double.TryParse(dr["wk_defectQty"].ToString(), out doudefectqty);

            //                list_Data.Add(Lib.CheckNull(dr["wk_CardID"].ToString())); //라벨번호(공정전표)

            //                list_Data.Add(Lib.CheckNull(dr["Model"].ToString()));// 차종
            //                list_Data.Add(Lib.CheckNull(dr["BuyerArticleNo"].ToString()));// 품번
            //                list_Data.Add((string.Format("{0:n0}", (int)douworkqty)));// _수량
            //                list_Data.Add(Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, Lib.CheckNull(dr["wk_ResultDate"].ToString())));//D_생산일자
            //                list_Data.Add(Lib.CheckNull(dr["wk_Name"].ToString()));// 작업자
            //                list_Data.Add(Lib.CheckNull(dr["wk_CardID"].ToString())); //라벨번호(공정전표)

            //            }
            //        }

            //        g_sPrinterName = Lib.GetDefaultPrinter();
            //        TSCLIB_DLL.openport(g_sPrinterName);
            //        if (SendWindowDllCommand(list_Data, IsTagID, 1, 0))
            //        {
            //            Message[0] = "[라벨발행중]";
            //            Message[1] = "라벨 발행중입니다. 잠시만 기다려주세요.";
            //            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
            //        }
            //        else
            //        {
            //            Message[0] = "[라벨발행 실패]";
            //            Message[1] = "라벨 발행에 실패했습니다. 관리자에게 문의하여주세요.\r\n<SendWindowDllCommand>";
            //            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
            //        }
            //        TSCLIB_DLL.clearbuffer();
            //        TSCLIB_DLL.closeport();
            //    }
            //}
            //catch (Exception excpt)
            //{
            //    Message[0] = "[오류]";
            //    Message[1] = string.Format("오류!관리자에게 문의\r\n{0}", excpt.Message);
            //    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            //}
            #endregion

            string g_sPrinterName = Lib.GetDefaultPrinter();
            try
            {
                IsTagID = Frm_tprc_Main.g_tBase.TagID;
                List<string> list_Data = null;
                //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //sqlParameter.Add("InstID", list_TWkLabelPrint[0].sInstID);
                //DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_WorkCard_sWorkCard]", sqlParameter, false);

                for (int i = 0; i < intPrintCount; i++)
                {
                    list_Data = new List<string>();
                    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                    if (list_TWkResult.Count > 0)
                    {
                        sqlParameter2.Add("InstID", list_TWkLabelPrint[i].sInstID);
                        sqlParameter2.Add("CardID", list_TWkLabelPrint[i].sLabelID);
                    }

                    DataTable dt2 = DataStore.Instance.ProcedureToDataTable("[xp_WorkCard_sWorkCardPrint]", sqlParameter2, false);
                    lData = new List<string>();
                    string strProcessID = "";

                    double douworkqty = 0;
                    double doudefectqty = 0;
                    foreach (DataRow dr in dt2.Rows)
                    {
                        strProcessID = dr["ProcessID"].ToString();
                        if (strProcessID == m_ProcessID) 
                        {
                            double.TryParse(dr["WorkQty"].ToString(), out douworkqty);
                            double.TryParse(dr["wk_defectQty"].ToString(), out doudefectqty);

                            list_Data.Add(Lib.CheckNull(dr["wk_CardID"].ToString())); //라벨번호(공정전표)
                            

                            //list_Data.Add(Lib.CheckNull(dr["Article"].ToString())); // 품명
                            //list_Data.Add(Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, Lib.CheckNull(dr["wk_ResultDate"].ToString())));//D_생산일자
                            //list_Data.Add(Lib.CheckNull(dr["BuyerArticleNo"].ToString()));// 품번
                            //list_Data.Add(Lib.CheckNull(dr["Model"].ToString()));// 차종
                            //list_Data.Add(Lib.CheckNull(dr["Process"].ToString()));// 공정명
                            //list_Data.Add((string.Format("{0:n0}", (int)douworkqty)));// _수량
                            //list_Data.Add((string.Format("{0:n0}", (int)doudefectqty)));// _불량수량
                            //list_Data.Add(Lib.CheckNull(dr["wk_Name"].ToString()));// 작업자

                            list_Data.Add(Lib.CheckNull(dr["Article"].ToString())); // 품명
                            list_Data.Add(Lib.CheckNull(dr["BuyerArticleNo"].ToString()));// 품번
                            list_Data.Add(Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, Lib.CheckNull(dr["wk_ResultDate"].ToString())));//D_생산일자
                            //list_Data.Add(Lib.CheckNull(dr["Model"].ToString()));// 차종
                            list_Data.Add(Lib.CheckNull(dr["Process"].ToString()));// 공정명
                            list_Data.Add((string.Format("{0:n0}", (int)douworkqty)));// _수량
                            list_Data.Add((string.Format("{0:n0}", (int)doudefectqty)));// _불량수량
                            list_Data.Add(Lib.CheckNull(dr["wk_Name"].ToString()));// 작업자
                            
                        }

                        if (strProcessID != m_ProcessID && list_Data.Count > 7)
                        {
                            list_Data.Add(Lib.CheckNull(dr["Process"].ToString())); // 다음(순차) 공정의  품명
                        }
                    }

                   

                    g_sPrinterName = Lib.GetDefaultPrinter();
                    TSCLIB_DLL.openport(g_sPrinterName);
                    if (SendWindowDllCommand(list_Data, IsTagID, 1, 0))
                    {
                        Message[0] = "[라벨발행중]";
                        Message[1] = "라벨 발행중입니다. 잠시만 기다려주세요.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                    }
                    else
                    {
                        Message[0] = "[라벨발행 실패]";
                        Message[1] = "라벨 발행에 실패했습니다. 관리자에게 문의하여주세요.\r\n<SendWindowDllCommand>";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                    }
                    TSCLIB_DLL.clearbuffer();
                    TSCLIB_DLL.closeport();
                }
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류!관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
        }


        private void cmdBoxList_Click(object sender, EventArgs e)
        {

        }

        private void cmdWorkDefect_Click(object sender, EventArgs e)
        {
            double WorkQty = Lib.ConvertDouble(txtWorkQty.Text);
            if (WorkQty == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("작업 수량을 먼저 입력해주세요.\r\n(불량 수량은 작업수량 이하로 입력 가능합니다.)", "불량 등록 전", 0, 1);
                return;
            }

            frm_tprc_Work_Defect_U defect = new frm_tprc_Work_Defect_U(updateJobID, dicDefect, WorkQty); 
            defect.Owner = this;
            defect.ShowDialog();
            if (defect.DialogResult == DialogResult.OK)
            {
                this.dicDefect = defect.dicDefect;
                this.txtDefectQty.Text = defect.returnTotalQty;
            }
        }

        private void My4mPop_WriteTextEvent(string SumDefectQty)
        {
            txtDefectQty.Text = string.Format("{0:n0}", SumDefectQty);
        }       


        private void SetGrid1RowClear()
        {
            while (GridData1.Rows.Count > 0)
            {
                GridData1.Rows.RemoveAt(0);
            }
        }
        private void SetGrid12owClear()
        {
            while (GridData2.Rows.Count > 0)
            {
                GridData2.Rows.RemoveAt(0);
            }
        }


        private void SetFormDataClear()
        {
            

            this.txtArticle.Text = "";
            txtBuyerArticleNo.Text = "";            

            //this.txtSpec.Text = "";

            SetGrid1RowClear();
            SetGrid12owClear();

            this.txtRemark.Text = "";
            this.txtDailyInstWorkQty.Text = "";
            this.txtDefectQty.Text = "";
            this.txtOrderQty.Text = "";
            this.txtOrderWorkQty.Text = "";
            this.txtOrderRemainQty.Text = "";
            this.txtlInstQty.Text = "";
            this.txtInstRemainQty.Text = "";
            txtInRmUnitClss.Text = "";
            txtInUnitClss.Text = "";

            txtProdQty.Text = "";
            txtInstWorkQty.Text = "";
            

            SetDateTimePicker();

            txtBoxID.Text = "";            
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

        private void Form_Activate()
        {
            try
            {
                //앞공정의 실적을 체크한다. 없을 시 close한다. ex)2차가류에 데이터가 없는데, 
                //Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
                //sqlParameters.Add("@InstID", Frm_tprc_Main.g_tBase.sInstID);
                //sqlParameters.Add("@InstSeq", Frm_tprc_Main.g_tBase.sInstDetSeq);
                //DataTable dtb = DataStore.Instance.ProcedureToDataTable("[xp_prdWork_sWorkQtyByInstIDProcessID]", sqlParameters, false);
                //foreach (DataRow dr in dtb.Rows)
                //{
                //    Frm_tprc_Main.g_tBase.OrderID = dr["OrderID"].ToString();
                //    Frm_tprc_Main.g_tBase.OrderUnit = dr["UnitClss"].ToString();
                //    if (dr["WorkQty"].ToString() == "0")
                //    {
                //        string pro = dr["Process"].ToString();
                //        string nowpro = Frm_tprc_Main.g_tBase.Process;
                //        Message[0] = "[실적오류]";
                //        Message[1] = pro + " 공정에 실적이 입력되지 않았기때문에.\r\n" + nowpro + " 공정의 작업실적을 입력해주세요.";
                //        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                //        blpldClose = true;
                //        return;
                //    }
                //}

                //하위품 있다 : Y , 없다 : N
                string[] CheckYN = new string[2];
                string Query = "select ChildCheckYN from mt_Process where ProcessID = '" + Frm_tprc_Main.g_tBase.ProcessID + "'";
                CheckYN = DataStore.Instance.ExecuteQuery(Query, false);
                ChildCheckYN = CheckYN[1];


                CheckLabelID(Frm_tprc_Main.g_tBase.sLotID);//pl_inputdet의 LotID를 
                
                FillGridData1();//당일 해당공정의 생산실적 조회           
                Frm_tprc_Main.g_tBase.OrderID = m_OrderID;//생산으로 넘어올 시 글로벌 오더ID 변경

                /////
                //txtProcess.Text = m_LabelID;
                //BarcodeEnter();  // 여기서 이제 선 기입한 바코드 값 자동기입.
                /////
                lstBarcodeCheck();

                // 시작일자와 시작시간을 scandate로 맞출 것.
                DateTime dateTime = new DateTime();
                dateTime = DateTime.ParseExact(m_WorkStartDate, "yyyyMMdd", null);
                mtb_From.Text = dateTime.ToString("yyyy-MM-dd");

                DateTime dt = DateTime.Now;
                dt = DateTime.ParseExact(m_WorkStartTime, "HHmmss", null);
                dtStartTime.Value = dt;

                // 현재 작업자 표시.
                txtNowWorker.Text = Frm_tprc_Main.g_tBase.Person;

                // WorkLog에 데이터 수집을 하는 공정이라면, 
                // 수집데이터를 가져와서 자동으로 뿌려주는 작업을 진행해야 겠지. ㅇㅇ.
                Find_Collect_WorkLogData();

                //if (Frm_tprc_Main.g_tBase.sInstDetSeq == "1")
                //{
                //    cmdSave.Text = "전표발행";
                //    LabelPrintYN = "Y";
                //    Frm_tprc_Main.g_tBase.TagID = "009"; //HYTech 공정이동전표

                //    //btnBringSplitData.Text = "전표발행\r(계속진행)";
                //    //btnBringSplitData.Visible = true;
                //}
                //else
                //{

            //        cmdSave.Text = "저 장";
            //        LabelPrintYN = "N";
                //    
                //}

                if (CheckLabelPrintYN())
                {
                    cmdSave.Text = "전표발행";
                    LabelPrintYN = "Y";
                    Frm_tprc_Main.g_tBase.TagID = "013"; //HYTech 공정이동전표
                }
                else
                {
                    cmdSave.Text = "저 장";
                    LabelPrintYN = "N";
                }

                // 2020.09.03 TKB 추가 - 첫번째 공정일 때 라벨 선택 저장 버튼 활성화
                if (ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq) == 1)
                {
                    btnSaveSelection.Visible = true;
                }

                // 수량 / CycleTime 값 0 세팅.
                if (txtFacilityCollectQty.Text == string.Empty)
                {
                    txtFacilityCollectQty.Text = "0";       // 설비수집 수량
                }
                
                //txtWorkQty.Text = "0";                  // (내가 한) 작업수량 → 설비 자동수량으로 가져옴

                txtSetCT.Text = stringFormatN1(ConvertDouble(m_CycleTime));               // 설정 CT 값

                // 현재 CT 값 구하기 = 1시간에 몇개를 생산하였는가?
                //txtNowCT.Text = "0";         // 총 수량( 라벨발행의 기준)

                // 생산박스 당 수량 값 가져오기.
                if (this.txtArticleID.Text != string.Empty)
                {
                    // ArticleID가 어쨌건 무언가 있다는 거니까.
                    BringProdLotQty(this.txtArticleID.Text);
                }
            }

            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        #region 라벨 →

        private bool CheckLabelPrintYN()
        {
            bool flag = false;

            try
            {
                //앞공정의 실적을 체크한다. 없을 시 close한다. ex)2차가류에 데이터가 없는데, 
                Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("@InstID", Frm_tprc_Main.g_tBase.sInstID);
                sqlParameters.Add("@InstSeq", ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq));
                DataTable dtb = DataStore.Instance.ProcedureToDataTable("[xp_prdWork_sWorkLabelPrintYN]", sqlParameters, false);

                foreach (DataRow dr in dtb.Rows)
                {
                    if (dr["Result"].ToString().Equals("Y"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
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

        #region 생산박스 당 수량 값 가져오기(BringProdLotQty)
        private void BringProdLotQty(string ArticleID)
        {
            string[] ProdLotQty = new string[2];
            string Query = "select ProdQtyPerBox from mt_Article where ArticleID = '" + ArticleID + "'";
            ProdLotQty = DataStore.Instance.ExecuteQuery(Query, false);
            txtProdQtyPerBox.Text = Lib.CheckNull(ProdLotQty[1]);

            double.TryParse(Lib.CheckNull(ProdLotQty[1]), out this.ProdQtyPerBox);
        }

        #endregion

        private void ShowYbox(bool blViewYn)
        {
            if (blViewYn == true)
            {                

                pnlFrame2.Visible = false;
            }
            else
            {

                pnlFrame2.Visible = true;
            }
        }
        private void ShowMoveStatement(bool blViewYn)
        {
            if (blViewYn == true)
            {
                pnlMoveStatement.Visible = true;
                //this.pnlMoveStatement.Location = new System.Drawing.Point(214, 258);
                this.pnlMoveStatement.Location = new System.Drawing.Point(2, 112);

            }
            else
            {
                pnlMoveStatement.Visible = false;
                this.pnlMoveStatement.Location = new System.Drawing.Point(2, 788);

            }
        }

        private void ShowBoxList(bool blViewYn)
        {            
        }

        //pl_inputdet의 LotID를 
        private string CheckLabelID(string strBarCode)
        {
            string strInstID = "";
            try
            {
                string sMoldID = "";
                if (Frm_tprc_Main.list_tMold.Count > 0)
                {
                    sMoldID = Frm_tprc_Main.list_tMold[0].sMoldID;
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PLotID", strBarCode);
                sqlParameter.Add("ProcessID", m_ProcessID); //SearchProcessID());                
                sqlParameter.Add("MoldID", sMoldID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_Chkworklotid", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    m_MtrExceptYN = Lib.CheckNull(dr["MtrExceptYN"].ToString());//PLotID가 라벨일때 pl_input의 MtrExceptYN
                    m_OutwareExceptYN = Lib.CheckNull(dr["OutwareExceptYN"].ToString());//PLotID가 라벨일때 pl_input의 OutwareExceptYN
                    strInstID = Lib.CheckNull(dr["InstID"].ToString());//PLotID가 라벨일때 pl_input의 InstID
                    this.txtInstID.Text = Lib.CheckNull(dr["InstID"].ToString());//PLotID가 라벨일때 pl_input의 InstID
                    this.txtInstDetSeq.Text = Lib.CheckNull(dr["InstDetSeq"].ToString());//PLotID가 라벨일때 pl_inputdet의 Instdetseq
                    this.txtLabelGubun.Text = Lib.CheckNull(dr["LabelGubun"].ToString());//PLotID가 라벨일때 LabelGubun = 0                    
                    this.txtArticleID.Text = Lib.CheckNull(dr["ArticleID"].ToString());//PLotID가 라벨일때 pl_inputdet의 ArticleID
                    this.txtArticle.Text = Lib.CheckNull(dr["pldArticle"].ToString());
                    this.txtBuyerArticleNo.Text = Lib.CheckNull(dr["BuyerArticleNo"].ToString());
                    this.txtProcess.Text = Lib.CheckNull(dr["Process"].ToString());

                    // 라벨발행여부 YN (전역변수 기입) → 2020.04 GLS 라벨을 안뽑아도 되도록  해달라고 요청
                    //Wh_Ar_LabelPrintYN = Lib.CheckNull(dr["LabelPrintYN"].ToString());


                    // 차종을 조회하는 대신, 그 자리에 진행중인 호기정보를 표시해 주세요.
                    // 2020.03.25 여영애 과장님
                    //this.txtCarModel.Text = Lib.CheckNull(dr["Model"].ToString());   // 차종



                    double InstQty = 0;
                    double ProdQtyPerBox = 0;
                    double InstWorkQty = 0;
                    double InstRemainQty = 0;
                    double.TryParse(dr["InstQty"].ToString(), out InstQty);
                    //pIdProdQtyPerBox 임시 18.06.18 계속확인할것
                    double.TryParse(dr["ProdQtyPerBox"].ToString(), out ProdQtyPerBox);
                    //if (m_ProcessID == "0405" || m_ProcessID == "1101" || m_ProcessID == "2101")
                    //{
                    //    double.TryParse(dr["pIdProdQtyPerBox"].ToString(), out ProdQtyPerBox);
                    //}
                    //else
                    //{

                    //}
                    //pIdProdQtyPerBox 임시 18.06.18 계속확인할것
                    double.TryParse(dr["InstWorkQty"].ToString(), out InstWorkQty);
                    InstRemainQty = InstQty - InstWorkQty;

                    txtlInstQty.Text = string.Format("{0:n0}", (int)InstQty);//지시량
                    txtInstRemainQty.Text = string.Format("{0:n0}", (int)InstRemainQty);//남은지시수량 = 지시수량 - 지시누계량

                    txtInUnitClss.Text = Lib.CheckNull(dr["UnitClssName"].ToString());
                    txtInRmUnitClss.Text = Lib.CheckNull(dr["UnitClssName"].ToString());

                    // 
                    if (!strBarCode.ToUpper().Contains("PL"))
                    {
                        txtProdQty.Text = string.Format("{0:n0}", (int)ProdQtyPerBox);//mt_article의 qtyperbox박스당수량
                    }
                    txtInstWorkQty.Text = string.Format("{0:n0}", (int)InstWorkQty);//지시누계량 = wk_result 생산수량의 합 
                                                                                    //

                    txtErrMsg.Text = Lib.CheckNull(dr["Msg"].ToString());//에러메세지

                    if (Lib.CheckNull(dr["OrderArticleID"].ToString()) ==
                        Lib.CheckNull(dr["ArticleID"].ToString())) //Y : 완제품, N : 완제품X
                    {
                        m_LastArticleYN = "Y";//마지막Article이니? Y 완제품이니?
                    }
                    else
                    {
                        m_LastArticleYN = "N";//마지막Article이니? N
                    }
                    txtRemark.Text = Lib.CheckNull(dr["Remark"].ToString());//pl_inputdet Remark
                    Frm_tprc_Main.g_tBase.Article = Lib.CheckNull(dr["Article"].ToString());//mt_article
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
                    //////////
                    m_OrderID = Lib.CheckNull(dr["OrderID"].ToString());//pl_input의 OrderID
                    m_ProdAutoInspectYN = Lib.CheckNull(dr["ProductAutoInspectYN"].ToString());//Order의 ProductAutoInspectYN
                    m_OrderSeq = OrderSeq;
                    /////////////
                    Frm_tprc_Main.g_tBase.Basis = "";
                    Frm_tprc_Main.g_tBase.BasisID = 0;

                    m_OrderNO = Lib.CheckNull(dr["OrderNO"].ToString());//수주번호
                    m_UnitClss = Lib.CheckNull(dr["UnitClss"].ToString());//pl_inputdet articleid의 UnitClss

                                        
                    
                    /////////////////////////
                    double OrderQty = 0;
                    double OrderWorkQty = 0;
                    double OrderRemainQty = 0;
                    double DefectQty = 0;
                    double DailyInstWorkQty = 0;
                    double.TryParse(dr["OrderQty"].ToString(), out OrderQty);
                    double.TryParse(dr["OrderWorkQty"].ToString(), out OrderWorkQty);
                    double.TryParse(dr["DefectQty"].ToString(), out DefectQty);
                    double.TryParse(dr["DailyInstWorkQty"].ToString(), out DailyInstWorkQty);
                    OrderRemainQty = OrderQty - OrderWorkQty;
                    txtOrderQty.Text = string.Format("{0:n0}", OrderQty);//오더전체수주량//전체오더량
                    txtOrderWorkQty.Text = string.Format("{0:n0}", OrderWorkQty);//오더생산량
                    txtOrderRemainQty.Text = string.Format("{0:n0}", OrderRemainQty);//오더잔량 = 전체오더량 - 오더에 따른 생산량(wk_result)
                    //txtDefectQty.Text = string.Format("{0:n0}", DefectQty); // 오더번호 기준 기존 불량수량
                    txtDailyInstWorkQty.Text = string.Format("{0:n0}", DailyInstWorkQty);//당일 지시 누계량
                    
                    //2018.06.17 추가

                    m_OrderArticleID = dr["OrderArticleID"].ToString().Trim();//오더ArticleID

                    ////수정여지가 있음..원인분석필요 .. 프로시저 수정?? 
                    //if (Frm_tprc_Main.g_tBase.ProcessID == "1101" || Frm_tprc_Main.g_tBase.ProcessID == "1105")//준비공정에서만 ....
                    //{
                    //    m_OrderArticleID = Lib.CheckNull(dr["ArticleID"].ToString());
                    //}
                    //2018.06.18 주석
                    //사출공정일때 박스당 수량 텍스트박스에 박스당수량 자동으로 입력해준다.
                    //if (Frm_tprc_Main.g_tBase.ProcessID == "2101")// || Frm_tprc_Main.g_tBase.ProcessID == "2101") // '0401:재단, 2101:성형 제외시킴
                    //{
                    //    txtQtyPerBox.Text = Lib.CheckNull(dr["ProdQtyPerBox"].ToString());
                    //    UpdatepnlBoxQty("W");
                    //}
                    //2018.06.18 주석
                    
                    UpdatepnlBoxQty("W");

                    // 2020.04.28 작업지시 번호가 아닌 JobID 로 하위품목 세팅
                    if (strBarCode.Contains("PL"))
                    {
                        FillGridData2(this.updateJobID, Frm_tprc_Main.g_tBase.ProcessID);
                    }
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return "";
            }

            return strInstID;

        }

        private void SetGridData2RowClear()
        {
            while (GridData2.Rows.Count > 0)
            {
                GridData2.Rows.RemoveAt(0);
            }
        }


        #region 단일 하위품 바코드 스캔 체크 BarCodeCheck()

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
                //Detail ProcessYN 체크 / 세부공정인지 확인
                string DetailProcessYN = "";
                string[] DetailProcYN = new string[2];
                string sql = "select DetailProcessYN from mt_Process where ProcessID = '" + Frm_tprc_Main.g_tBase.ProcessID + "'";
                DetailProcYN = DataStore.Instance.ExecuteQuery(sql, false);
                DetailProcessYN = DetailProcYN[1];

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("LotID", strBarCode);
                if (DetailProcessYN == "Y")
                {
                    string strTopProcess = Frm_tprc_Main.g_tBase.ProcessID.Substring(0, 2) + "01";
                    sqlParameter.Add("ProcessID", strTopProcess);
                }
                else
                {
                    sqlParameter.Add("ProcessID", m_ProcessID);
                }
                sqlParameter.Add("MachineID", m_MachineID);
                sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                sqlParameter.Add("InstDetSeq", Frm_tprc_Main.g_tBase.sInstDetSeq);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sLotInfoByLotID", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];                   

                    m_ArticleID = dr["ArticleID"].ToString().Trim();
                    m_LabelGubun = dr["LabelGubun"].ToString().Trim();                    //라벨구분 1은 원자재 
                    this.txtLabelGubun.Text = m_LabelGubun;
                    m_Inspector = dr["Inspector"].ToString().Trim();                      //검사자
                    double.TryParse(dr["RemainQty"].ToString(), out m_RemainQty);         //전체재고량
                    double.TryParse(dr["LocRemainQty"].ToString(), out m_LocRemainQty);   //해당창고재고량
                    m_UnitClss = dr["UnitClss"].ToString();
                    m_UnitClssName = dr["UnitClssName"].ToString();                         //투입되는 원자재의 재고단위
                    m_EffectDate = Lib.MakeDateTime("yyyyMMdd", dr["EffectDate"].ToString());

                 

                    if (m_MtrExceptYN == "N")//예외처리YN : 예외처리 아닐때
                    {
                        string[] sProTerm =Frm_tprc_Main.gs.GetValue("Work", "ProTerm", "ProTerm").Split('|');//배열에 공정별 조건 넣기
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
                    return true;
                }
                else
                {
                    //2020.04.03 허윤구
                    // DT가 NULL이 될 수 있는 케이스.
                    // 지속적으로 찾아서 개별 케이스별로 메시지 UPDATE 해야 합니다.

                    // 0. 재고가 소진된 경우
                    string[] OutwareYN = new string[2];
                    string sql_1 = "select cnt = OutwareYN from StuffinSub where LotID = '" + strBarCode + "'";
                    OutwareYN = DataStore.Instance.ExecuteQuery(sql_1, false);
                    if (OutwareYN[1] == "Y")
                    {
                        Message[0] = "[해당 라벨 재고 소진]";
                        Message[1] = "해당 하위품( " + strBarCode + " )은 재고가 소진되었습니다. 작업 취소 후 다시 시작해주세요.";
                        throw new Exception();
                    }

                    //1. 하위품이 사라진경우. > 즉, 알수없는 (여러) 이유로 삭제된 케이스.                    

                    string[] DeleteBarcodeYN = new string[2];
                    string sql_2 = "select cnt = COUNT(*) from wk_result where labelid = '" + strBarCode + "'";
                    DeleteBarcodeYN = DataStore.Instance.ExecuteQuery(sql_2, false);
                    if (DeleteBarcodeYN[1] == "0")
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
            catch (Exception excpt)
            {
                m_ArticleID = "";
                m_LabelGubun = "";
                m_Inspector = "";
                m_RemainQty = 0;
                m_LocRemainQty = 0;
                m_UnitClss = "";
                m_UnitClssName = "";
                m_EffectDate = "";

                Console.Write(excpt.Message);
                return false;
            }
        }

        #endregion


        private void FillGridData2(string strJobID, string strProcessID)
        {
            double dulReqQty = 0;
            SetGridData2RowClear();

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("JobID", ConvertDouble(strJobID));
                sqlParameter.Add("ProcessID", strProcessID);

                DataTable dt = null;
                dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sGetworkchild", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int a = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        double.TryParse(dr["ReqQty"].ToString(), out dulReqQty);
                        GridData2.Rows.Add(++a
                                           , dr["InstID"].ToString().Trim()
                                           , dr["DetSeq"].ToString().Trim()
                                           , dr["ChildSeq"].ToString().Trim()
                                           , dr["ChildArticleID"].ToString().Trim()
                                           , dr["Article"].ToString().Trim()
                                           , dr["BuyerArticleNo"].ToString().Trim()
                                           , ""
                                           , dr["ScanExceptYN"].ToString().Trim()
                                           , ""
                                           , dr["Flag"].ToString().Trim()
                                           , dr["ScanExceptYN"].ToString().Trim()
                                           , ""
                                           , ""
                                           , dr["UnitClss"].ToString().Trim()
                                           , ""
                                           , dulReqQty.ToString()
                                           , ""
                                           , m_UnitClss//""
                                           , "" 
                                           , ""
                                          );
                        if (dr["ScanExceptYN"].ToString() == "Y")
                        {
                            GridData2.Rows[a - 1].DefaultCellStyle.BackColor = Color.FromArgb(238, 108, 128);
                        }                        
                    }
                }
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류!관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
        }

        /// <summary>
        /// BoxQty Update (생산수량 , 박스당 수량 변동으로 인한)
        /// </summary>
        /// <param name="strGbn"></param>
        private void UpdatepnlBoxQty(string strGbn)
        {
            

        }

        /// <summary>
        /// 스켄 ID  기본 정상여부 확인
        /// </summary>
        /// <returns></returns>
        private bool LF_Check_ScanData(string strBarcode)
        {
            bool blResult = true;
            if (strBarcode != "")
            {
                if (strBarcode.ToUpper().Contains("PL"))
                {
                    //'지시 LotID 15, 16자리
                    if (!(strBarcode.Trim().Length == 15 || strBarcode.Trim().Length == 16))
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("코드가 잘못되었습니다.", "[바코드 길이오류]", 0, 1);
                        blResult = false;
                    }
                    return blResult;
                }
                //공정이동전표 , 길이 변경 2017.02.09 ,   13 --> 9 자리로 ,  외에는 13자리
                else if (strBarcode.ToUpper().Contains("C") //성형이동전표
                    || strBarcode.ToUpper().Contains("I")   //원자재이동전표
                    || strBarcode.ToUpper().Contains("M")   //혼련이동전표
                    || strBarcode.ToUpper().Contains("T")   //재단이동전표
                    || strBarcode.ToUpper().Contains("B"))  //박스이동전표?
                {
                    //'지시 LotID 15자리
                    if ((strBarcode.Trim().Length != 10))
                    {
                        Message[0] = "[길이 오류]";
                        Message[1] = "코드가 잘못되었습니다.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);

                        blResult = false;

                    }
                    return blResult;
                }
            }
            return blResult;
        }

        private bool SetProcessID(string strBarcode)
        {
            bool blResult = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("ProcessID", strBarcode);

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_Code_sProcess", sqlParameter, false);
                DataTable dt = null;

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    Frm_tprc_Main.g_tBase.ProcessID = dt.Rows[0]["ProcessID"].ToString();
                    Frm_tprc_Main.g_tBase.Process = dt.Rows[0]["Process"].ToString();
                    //m_ProcessID = dt.Rows[0]["ProcessID"].ToString();
                    //m_Process = dt.Rows[0]["Process"].ToString();
                    blResult = true;
                }
                else
                {
                    blResult = false;
                    Frm_tprc_Main.g_tBase.ProcessID = "";
                    Frm_tprc_Main.g_tBase.Process = "";
                }

            }
            catch (Exception excpt)
            {
                blResult = false;
                Frm_tprc_Main.g_tBase.ProcessID = "";
                Frm_tprc_Main.g_tBase.Process = "";
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }
            return blResult;

        }

        private bool SetPersonID(string strBarcode)
        {
            bool blResult = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("UserID", strBarcode);

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_PlanInput_sPersonID", sqlParameter, false);
                DataTable dt = null;

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];

                    //m_PersonID =    dt.Rows[0]["PersonID"].ToString();
                    //m_Person =      dt.Rows[0]["Name"].ToString();
                    //m_TeamID =      dt.Rows[0]["TeamID"].ToString();
                    //m_Team =        dt.Rows[0]["Team"].ToString();

                    Frm_tprc_Main.g_tBase.PersonID = dt.Rows[0]["PersonID"].ToString();
                    Frm_tprc_Main.g_tBase.Person = dt.Rows[0]["Name"].ToString();
                    Frm_tprc_Main.g_tBase.TeamID = dt.Rows[0]["TeamID"].ToString();
                    Frm_tprc_Main.g_tBase.Team = dt.Rows[0]["Team"].ToString();


                    blResult = true;
                }
                else
                {
                    blResult = false;
                    Frm_tprc_Main.g_tBase.PersonID = "";
                    Frm_tprc_Main.g_tBase.Person = "";
                    Frm_tprc_Main.g_tBase.TeamID = "";
                    Frm_tprc_Main.g_tBase.Team = "";
                }

            }
            catch (Exception excpt)
            {
                blResult = false;
                Frm_tprc_Main.g_tBase.PersonID = "";
                Frm_tprc_Main.g_tBase.Person = "";
                Frm_tprc_Main.g_tBase.TeamID = "";
                Frm_tprc_Main.g_tBase.Team = "";
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }


            return blResult;
        }

        private void SetGridData1RowClear()
        {
            while (GridData1.Rows.Count > 0)
            {
                GridData1.Rows.RemoveAt(0);
            }
        }


        //당일 해당공정의 생산실적 조회
        private void FillGridData1()
        {
            DataRow dr = null;
            DataGridViewRow row = null;

            SetGridData1RowClear();
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("sDate", DateTime.Now.ToString("yyyyMMdd"));
                sqlParameter.Add("ProcessID", Frm_tprc_Main.g_tBase.ProcessID);
                sqlParameter.Add("MachineID", Frm_tprc_Main.g_tBase.MachineID);
                DataSet ds = null;
                ds = DataStore.Instance.ProcedureToDataSet("xp_Work_sResultListbyProcessID", sqlParameter, false);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        GridData1.Rows.Add(i + 1
                                           , dr["LabelID"].ToString().Trim()
                                          );
                        row = GridData1.Rows[i];
                        row.Height = 30;
                    }
                    GridData1.ClearSelection();
                    txtGrd1Count.Text = ds.Tables[0].Rows.Count.ToString() + "건";
                }
                else
                {

                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<FillGridData1>\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            SetGridData2RowClear();
            SetFormDataClear();
        }

        /// <summary>
        ///  박스 입력 창 확인
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdBoxListClose_Click(object sender, EventArgs e)
        {
            UpdatepnlWorkQty();
            ShowBoxList(false);
            return;
        }
        /// <summary>
        ///  박스수량 입력창 확인 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdBoxQtyInput_Click(object sender, EventArgs e)
        {
            //string strBoxQty = "";
            

        }

        private void UpdatepnlWorkQty()
        {
            //int SumQty = 0;
            
        }
        
        private void cmdUpBurnTemper1_Click(object sender, EventArgs e)
        {


            POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();            
            POPUP.Frm_CMNumericKeypad.g_Name = "생산수량";
            return;

        }
        private void cmdDownBurnTemper1_Click(object sender, EventArgs e)
        {

            POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();            

        }
        private void cmdFormaTime_Click(object sender, EventArgs e)
        {

            POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();            
        }
        private void cmdUpBurnTemper2_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();            
        }

        private void cmdDownBurnTemper2_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();            
        }
        private void cmdSetUpBurnTemper_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();            
        }
        private void cmdSetDownBurnTemper_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();            
        }


        private void cmdSetFormaTime_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();            
        }






        private void txtBarCodeScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }




        #region 단일 하위품 바코드 체크 BarcodeEnter()
        /// <summary>
        /// ////////////////////
        /// </summary>
        // 바코드 (자동) 스캔.
        private void BarcodeEnter()
        {

            string sGridArticleID = "";
            string Barcode = "";//txtProcess.Text.Trim();

            try
            {               
                // '바코드에 해당하는 Article, LabelGubun을 전역변수에 저장
                if (!BarCodeCheck(Barcode))
                {
                    throw new Exception();
                }
                        
                if (GridData2.RowCount > 0)
                {
                    for (int i = 0; i < GridData2.RowCount; i++)
                    {
                        sGridArticleID = GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString();
                        if (m_ArticleID == sGridArticleID)
                        {
                            //Grid의 단위와 불러온 입고품의 단위를 Grid의 단위에 맞게 맞추기
                            if (GridData2.Rows[i].Cells["UnitClss"].Value.ToString() != m_UnitClss)
                            {
                                string GridUnitClss = GridData2.Rows[i].Cells["UnitClss"].Value.ToString();
                                if (GridUnitClss == "1" && m_UnitClss == "2")//g
                                {
                                    m_LocRemainQty = m_LocRemainQty * 1000;
                                }
                                else if (GridUnitClss == "2" && m_UnitClss == "1")//kg
                                {
                                    m_LocRemainQty = m_LocRemainQty / 1000;
                                }
                            }
                            if (GridData2.Rows[i].Cells["BarCode"].Value.ToString() == txtProcess.Text.Trim())
                            {
                                GridData2.Rows[i].Cells["ScanExceptYN"].Value = "N";
                                GridData2.Rows[i].Cells["BarCode"].Value = "";
                                GridData2.Rows[i].Cells["LabelGubun"].Value = "";
                                GridData2["Article", i].Selected = true;
                                GridData2.Rows[i].Cells["RemainQty"].Value = 0;
                                GridData2.Rows[i].Cells["LocRemainQty"].Value = 0;
                                GridData2.Rows[i].Cells["ProdCapa"].Value = 0;
                                GridData2.Rows[i].Cells["EffectDate"].Value = "";
                            }
                            else
                            {
                                GridData2.Rows[i].Cells["ScanExceptYN"].Value = "Y";
                                GridData2.Rows[i].Cells["BarCode"].Value = this.txtProcess.Text.Trim();
                                GridData2.Rows[i].Cells["LabelGubun"].Value = m_LabelGubun;
                                GridData2["BuyerArticle", i].Selected = true;        
                                GridData2.Rows[i].Cells["RemainQty"].Value = string.Format("{0:n0}", m_RemainQty);//전체잔량
                                GridData2.Rows[i].Cells["LocRemainQty"].Value = string.Format("{0:n0}", m_LocRemainQty);//창고잔량
                                double.TryParse(GridData2.Rows[i].Cells["ReqQty"].Value.ToString(), out m_douReqQty);
                                GridData2.Rows[i].Cells["EffectDate"].Value = m_EffectDate;
                                GridData2.Rows[i].Cells["UnitClssName"].Value = m_UnitClssName;         // 하위품의 재고단위
                                GridData2.Rows[i].Cells["ProdUnitClssName"].Value = txtInUnitClss.Text; // 생산되는 생산품의 재고단위

                                m_douProdCapa = m_LocRemainQty / m_douReqQty;
                                if (m_douProdCapa.ToString().Contains("."))
                                {
                                    string[] sProdCapa = m_douProdCapa.ToString().Split('.');
                                    double.TryParse(sProdCapa[0].ToString(), out m_douProdCapa);//소수점 버림
                                }
                                GridData2.Rows[i].Cells["ProdCapa"].Value = string.Format("{0:n0}", m_douProdCapa);

                                
                                
                            }
                            m_ArticleID = "";
                            m_LabelGubun = "";
                            m_LocRemainQty = 0;
                            m_RemainQty = 0;
                            m_EffectDate = "";
                            m_UnitClssName = "";
                        }
                    }                        
                }                    
            }
            catch (Exception)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return;
            }

        }

        #endregion

        #region 복수 하위품 바코드 체크 lstBarcodeCheck() : 둘리 : 대구산업용

        /// <summary>
        /// ////////////////////
        /// </summary>
        // 바코드 (자동) 스캔.
        private void lstBarcodeCheck()
        {
            GridData2.Rows.Clear();

            DataRow dr = null;

            // 라벨 없이 시작하는 경우
            if (lstStartLabel.Count == 0)
            {
                try
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Clear();

                    sqlParameter.Add("LotID", "");
                    sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                    sqlParameter.Add("InstDetSeq", ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq));
                    sqlParameter.Add("MachineID", m_MachineID);
                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sGetSubItemInfo", sqlParameter, false);


                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return;
                }
            }
            

            for (int k = 0; k < lstStartLabel.Count; k++)
            {
                try
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Clear();

                    sqlParameter.Add("LotID", lstStartLabel[k]);
                    sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                    sqlParameter.Add("InstDetSeq", ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq));
                    sqlParameter.Add("MachineID", m_MachineID);
                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sGetSubItemInfo", sqlParameter, false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        int index = k;

                        // 둘리 : 대구산업 2020.05.02
                        // 이곳에서 하위품 데이터그리드 넣기
                        GridData2.Rows.Add(++index
                                           , dr["InstID"].ToString().Trim()
                                           , dr["InstDetSeq"].ToString().Trim()
                                           , index.ToString()
                                           , dr["ArticleID"].ToString().Trim()
                                           , dr["Article"].ToString().Trim()
                                           , dr["BuyerArticleNo"].ToString().Trim()
                                           , lstStartLabel[k].ToString().Trim() // 바코드
                                           , "" // 체크
                                           , dr["LabelGubun"].ToString().Trim() // 라벨구분
                                           , "A" // 플래그
                                           , "Y" // ScanExceptYN1 예외
                                           , stringFormatN0(dr["RemainQty"]) // 품명 재고량
                                           , stringFormatN0(dr["LocRemainQty"]) // 해당라벨 재고량
                                           , dr["UnitClss"].ToString().Trim() // 하위품 단위
                                           , dr["UnitClssName"].ToString() // 단위 이름
                                           , stringFormatNDigit(dr["ReaQty"], 3) // 소모량
                                           , "" // 생산가능량
                                           , m_UnitClss//"" // 생산단위
                                           , m_UnitClssName // 생산단위 이름
                                           , "" // 유효기간??
                                          );

                        m_LabelGubun = dr["LabelGubun"].ToString().Trim();
                        m_RemainQty = ConvertDouble(dr["RemainQty"].ToString());
                        m_LocRemainQty = ConvertDouble(dr["LocRemainQty"].ToString());
                        m_douReqQty = ConvertDouble(dr["ReaQty"].ToString());

                        // 둘리 : 대구산업 : 2020.05.28
                        // 근데 만약에 상위품과 하위품이 같다면!
                        // → 소요량은 1임
                        string PArticleID = Frm_tprc_Main.g_tBase.sArticleID;
                        if (PArticleID != null
                            && PArticleID.Equals(dr["ArticleID"].ToString().Trim()))
                        {
                            m_douReqQty = 1;
                        }

                        //Grid의 단위와 불러온 입고품의 단위를 Grid의 단위에 맞게 맞추기
                        if (GridData2.Rows[k].Cells["UnitClss"].Value.ToString() != m_UnitClss)
                        {
                            string GridUnitClss = GridData2.Rows[k].Cells["UnitClss"].Value.ToString();
                            if (GridUnitClss == "1" && m_UnitClss == "2")//g
                            {
                                m_LocRemainQty = m_LocRemainQty * 1000;
                            }
                            else if (GridUnitClss == "2" && m_UnitClss == "1")//kg
                            {
                                m_LocRemainQty = m_LocRemainQty / 1000;
                            }
                        }

                        GridData2["Article", k].Selected = true;
                        GridData2.Rows[k].Cells["LocRemainQty"].Value = string.Format("{0:n0}", m_LocRemainQty);//창고잔량
                        GridData2.Rows[k].Cells["ProdUnitClssName"].Value = txtInUnitClss.Text; // 생산되는 생산품의 재고단위

                        m_douProdCapa = devideNum(m_LocRemainQty, m_douReqQty);
                        GridData2.Rows[k].Cells["ProdCapa"].Value = stringFormatN0(m_douProdCapa);
                    }
                    else
                    {
                        //2020.04.03 허윤구
                        // DT가 NULL이 될 수 있는 케이스.
                        // 지속적으로 찾아서 개별 케이스별로 메시지 UPDATE 해야 합니다.

                        // 0. 재고가 소진된 경우
                        string[] OutwareYN = new string[2];
                        string sql_1 = "select cnt = OutwareYN from StuffinSub where LotID = '" + lstStartLabel[k] + "'";
                        OutwareYN = DataStore.Instance.ExecuteQuery(sql_1, false);
                        if (OutwareYN[1] == "Y")
                        {
                            Message[0] = "[해당 라벨 재고 소진]";
                            Message[1] = "해당 하위품( " + lstStartLabel[k] + " )은 재고가 모두 소진되었습니다.\r\n작업 취소 후 다시 시작해주세요.";
                            cmdSave.Enabled = false;
                            throw new Exception();
                        }

                        //1. 하위품이 사라진경우. > 즉, 알수없는 (여러) 이유로 삭제된 케이스.                    
                        string[] DeleteBarcodeYN = new string[2];
                        string sql_2 = "select cnt = COUNT(*) from wk_result where labelid = '" + lstStartLabel[k] + "'";
                        DeleteBarcodeYN = DataStore.Instance.ExecuteQuery(sql_2, false);
                        if (DeleteBarcodeYN[1] == "0")
                        {
                            Message[0] = "[하위품 소실]";
                            Message[1] = "해당 하위품( " + lstStartLabel[k] + " )은 승인되지 않은 품목이거나 삭제처리된 Lot입니다.\r\n작업 취소 후 다시 시작해주세요.";
                            throw new Exception();
                        }
                        else
                        {
                            Message[0] = "[입고승인]";
                            Message[1] = "해당 품목은 승인되지 않은 품목이거나 입고내역이 없는 품목이므로 사용할 수 없습니다.\r\n작업 취소 후 다시 시작해주세요.";
                            throw new Exception();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 하위품 조회시 오류 발생 → 따로 저장할수 없도록
                    cmdSave.Enabled = false;
                    btnSaveSelection.Enabled = false;
                    cmdWorkDefect.Enabled = false;

                    Console.Write(ex.Message);
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    continue;
                }
            }
        }

        #endregion

        // workLog 가져올거 있으면 가져오고 뿌리고 해야 함.
        private void Find_Collect_WorkLogData()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("MachineID", Frm_tprc_Main.g_tBase.MachineID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sCollectWorkLogData", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        // 공정과 호기정보가 일치한다면,
                        if (m_ProcessID == dr["ProcessID"].ToString() &&
                            Frm_tprc_Main.g_tBase.MachineID == dr["MachineID"].ToString())
                        {
                            txtFacilityCollectQty.Text = Lib.CheckNull(dr["WorkQty"].ToString().Trim());
                            break;
                        }
                    }

                    if (txtFacilityCollectQty.Text != string.Empty)
                    {
                        //worklog에서 값을 가져왔다면,
                        //지금 가져온 값이 오늘하루의 총 작업수량이 될 테니까,
                        txtWorkQty.Text = txtFacilityCollectQty.Text;
                        Get_TotalQty();
                        Get_NowCT();
                    }
                }


            }
            catch (Exception)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return;
            }

        }








        private void btnStartTime_Click(object sender, EventArgs e)
        {
            TimeCheck("시작시간");

            Get_NowCT();
        }

        private void btnEndTime_Click(object sender, EventArgs e)
        {
            TimeCheck("종료시간");

            Get_NowCT();
        }

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

        private void txtWorkQty_TextChanged(object sender, EventArgs e)
        {            

            int intInstQty = 0;
            int intWorkQty = 0;
            int intInstRemainQty = 0;
            int.TryParse(Lib.GetDouble(txtlInstQty.Text).ToString(), out intInstQty);            

            intInstRemainQty = intInstQty - intWorkQty;
            //this.txtInstRemainQty.Text = string.Format("{0:n0}", intInstRemainQty);
        }

        private void txtProdQty_TextChanged(object sender, EventArgs e)
        {
            ProdQty = Lib.CheckNum(txtProdQty.Text).Replace(",", "");
        }


        private void txtWorkQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            WizWorkLib.TypingOnlyNumber(sender, e, true, false);
        }

        private void txtQtyPerBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            WizWorkLib.TypingOnlyNumber(sender, e, true, false);
        }

        private void txtBoxQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            WizWorkLib.TypingOnlyNumber(sender, e, true, false);
        }
        //boolean 값을 받기위해 private -> public 으로 수정 18.01.15
        public bool LF_ChkMachineCheck()
        {
            // '***************************************************************
            // '0:공정작업입력 시 설비 점검(하루1회이상) 및 자주검사 수행(작업지시별 1회이상) check
            // '***************************************************************
            bool blResult = false;
            bool bFirst = false;

            string strMachine = "";
            string[] MachineTemp = null;

            DataSet ds = null;
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

                        sqlParameter.Add("ProcessID", m_ProcessID);
                        sqlParameter.Add("MachineID", m_MachineID);
                        sqlParameter.Add("PLotID", m_LotID);

                        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sToDayMcRegularInspectAutoYN", sqlParameter, false);
                        if (dt != null && dt.Rows.Count == 1)
                        {

                            DataRow dr = null;
                            dr = ds.Tables[0].Rows[0];

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

                                }
                                int.TryParse(dr["AutoInspect"].ToString(), out inAutoInspect);
                                if (inAutoInspect == 0)
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
                                    if (strMessage != "")
                                    {
                                        Message[0] = "[설비점검 오류]";
                                        Message[1] = strMessage + "의 설비점검을 하셔야합니다.";
                                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                                        //timer1.Stop();
                                        return false;
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

        private void save2101()
        {
            list_TWkResult = new List<Sub_TWkResult>();
            int InstDetSeq = 0;
            int i = 0;
            float sLogID = 0;
            float WorkQty = 0;
            //성형 금형정보 

            list_TWkResult.Add(new Sub_TWkResult());
            list_TWkResult[i].JobID = 0;
            list_TWkResult[i].InstID = txtInstID.Text;
            int.TryParse(Lib.GetDouble(txtInstDetSeq.Text).ToString(), out InstDetSeq);
            list_TWkResult[i].InstDetSeq = InstDetSeq;
            list_TWkResult[i].LabelID = txtBoxID.Text;
            list_TWkResult[i].LabelGubun = txtLabelGubun.Text;
            list_TWkResult[i].ProcessID = Frm_tprc_Main.g_tBase.ProcessID;
            list_TWkResult[i].MachineID = Frm_tprc_Main.g_tBase.MachineID;
            list_TWkResult[i].ArticleID = txtArticleID.Text;
            list_TWkResult[i].WorkQty = WorkQty;
            list_TWkResult[i].sLastArticleYN = m_LastArticleYN;
            list_TWkResult[i].ProdAutoInspectYN = m_ProdAutoInspectYN;
            list_TWkResult[i].sOrderID = m_OrderID;
            list_TWkResult[i].nOrderSeq = m_OrderSeq;

            list_TWkResult[i].WorkStartDate = mtb_From.Text.Replace("-", "");
            list_TWkResult[i].WorkStartTime = dtStartTime.Value.ToString("HHmmss");
            list_TWkResult[i].WorkEndDate = mtb_To.Text.Replace("-", "");
            list_TWkResult[i].WorkEndTime = dtEndTime.Value.ToString("HHmmss");
            list_TWkResult[i].ScanDate = list_TWkResult[i].WorkEndDate;
            list_TWkResult[i].ScanTime = list_TWkResult[i].WorkEndTime;
            list_TWkResult[i].JobGbn = "1";

            //'------------------------------------------------------------------------------------------



            //'------------------------------------------------------------------------------------------

            list_TWkResult[i].Comments = "";
            list_TWkResult[i].ReworkOldYN = "";
            list_TWkResult[i].ReworkLinkProdID = "";
            list_TWkResult[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;
            list_TWkResult[i].WDNO = "";
            list_TWkResult[i].WDID = "";
            list_TWkResult[i].WDQty = 0;
            list_TWkResult[i].s4MID = "";
            float.TryParse(m_LogID, out sLogID);
            list_TWkResult[i].sLogID = sLogID;


        }

        #region 달력 From값 입력 // 달력 창 띄우기
        private void mtb_From_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();

            Get_NowCT();
        }
        #endregion
        #region 달력 To값 입력 // 달력 창 띄우기
        private void mtb_To_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();

            Get_NowCT();
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

        private void pnlBarcode_Paint(object sender, PaintEventArgs e)
        {

        }


        #region 수량 / CycleTime 기입용 숫자 키패드 팝업창 모음.

        // 작업수량 기입 팝업창 생성
        private void chkWorkQty_Click(object sender, EventArgs e)
        {
            double DOU_WorkQty = 0;

            txtWorkQty.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtWorkQty.Text = keypad.tbInputText.Text;
                if (txtWorkQty.Text == "" || Convert.ToInt32(txtWorkQty.Text) == 0)
                {
                    txtWorkQty.Text = "0";
                }
                Double.TryParse(txtWorkQty.Text, out DOU_WorkQty);
                txtWorkQty.Text = string.Format("{0:n0}", (int)DOU_WorkQty);
            }
            Get_TotalQty();
        }
        // 작업수량 기입 팝업창 생성
        private void txtWorkQty_Click(object sender, EventArgs e)
        {
            double DOU_WorkQty = 0;

            txtWorkQty.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("수량입력", "수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtWorkQty.Text = keypad.tbInputText.Text;
                if (txtWorkQty.Text == "" || Convert.ToInt32(txtWorkQty.Text) == 0)
                {
                    txtWorkQty.Text = "0";
                }
                Double.TryParse(txtWorkQty.Text, out DOU_WorkQty);
                txtWorkQty.Text = string.Format("{0:n0}", (int)DOU_WorkQty);
            }
            Get_TotalQty();
            Get_NowCT();

            // 현재 CT 값 구하기
        }

        #region 현재 CT 값 구하기 → 작업수량 변경시, 시간 변경시 계산 되어야 함.

        private void Get_NowCT()
        {
            //DateTime Start = DateTime.Now;
            //DateTime End = DateTime.Now;
            //DateTime.TryParse(mtb_From.Text + " " + dtStartTime.Text, out Start);
            //DateTime.TryParse(mtb_To.Text + " " + dtEndTime.Text, out End);

            //TimeSpan timeDiff = End - Start;

            //// 시간차 = 시간 + (분 / 60)
            //double Hour = timeDiff.Hours + (timeDiff.Minutes / 60.0);

            //if (Hour == 0)
            //{
            //    txtNowCT.Text = "0";
            //}
            //else
            //{
            //    // CT값 계산
            //    txtNowCT.Text = stringFormatN1(ConvertDouble(txtWorkQty.Text) / Hour);
            //}
            FillNowCT();
        }

        #region CT 값 자동 계산 프로시저 : xp_prdWork_getNowCT

        private void FillNowCT()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("StartDate", mtb_From.Text.Replace("-", ""));
                sqlParameter.Add("EndDate", mtb_To.Text.Replace("-", ""));
                sqlParameter.Add("StartTime", dtStartTime.Value.ToString("HHmm"));
                sqlParameter.Add("EndTime", dtEndTime.Value.ToString("HHmm"));
                sqlParameter.Add("WorkQty", ConvertDouble(txtWorkQty.Text));

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_prdWork_getNowCT", sqlParameter, false);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //{
                    //    txtNowCT.Text = stringFormatN1(dr["NowCT"]);
                    //}

                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        txtNowCT.Text = stringFormatN1(dr["NowCT"]);
                    }
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<FillGridData1>\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }
        #endregion

        #endregion

        // 생산박스 당 수량 기입 팝업창 생성
        private void chkLotProdQty_Click(object sender, EventArgs e)
        {
            double DOU_LotProdQty = 0;

            txtSetCT.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("생산수량", "생산수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtSetCT.Text = keypad.tbInputText.Text;
                if (txtSetCT.Text == "" || Convert.ToInt32(txtSetCT.Text) == 0)
                {
                    txtSetCT.Text = "0";
                }
                Double.TryParse(txtSetCT.Text, out DOU_LotProdQty);
                txtSetCT.Text = string.Format("{0:n0}", (int)DOU_LotProdQty);
            }
        }
        // 생산박스 당 수량 기입 팝업창 생성
        private void txtLotProdQty_Click(object sender, EventArgs e)
        {
            double DOU_LotProdQty = 0;

            txtSetCT.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("생산수량", "생산수량");

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtSetCT.Text = keypad.tbInputText.Text;
                if (txtSetCT.Text == "" || Convert.ToInt32(txtSetCT.Text) == 0)
                {
                    txtSetCT.Text = "0";
                }
                Double.TryParse(txtSetCT.Text, out DOU_LotProdQty);
                txtSetCT.Text = string.Format("{0:n0}", (int)DOU_LotProdQty);
            }
        }


        // CycleTime 기입 팝업창 생성
        private void chkCycleTime_Click(object sender, EventArgs e)
        {
            //double DOU_CycleTime = 0;

            //txtCycleTime.Text = "";
            POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("C-T", "C-T");

            //keypad.Owner = this;
            //if (keypad.ShowDialog() == DialogResult.OK)
            //{
            //    txtCycleTime.Text = keypad.tbInputText.Text;
            //    if (txtCycleTime.Text == "" || Convert.ToInt32(txtCycleTime.Text) == 0)
            //    {
            //        txtCycleTime.Text = "0";
            //    }
            //    Double.TryParse(txtCycleTime.Text, out DOU_CycleTime);
            //    txtCycleTime.Text = string.Format("{0:n0}", (int)DOU_CycleTime);
            //}
        }
        // CycleTime 기입 팝업창 생성
        private void txtCycleTime_Click(object sender, EventArgs e)
        {
            //double DOU_CycleTime = 0;

            //txtCycleTime.Text = "";
            //POPUP.Frm_CMNumericKeypad keypad = new POPUP.Frm_CMNumericKeypad("C-T", "C-T");

            //keypad.Owner = this;
            //if (keypad.ShowDialog() == DialogResult.OK)
            //{
            //    txtCycleTime.Text = keypad.tbInputText.Text;
            //    if (txtCycleTime.Text == "" || Convert.ToInt32(txtCycleTime.Text) == 0)
            //    {
            //        txtCycleTime.Text = "0";
            //    }
            //    Double.TryParse(txtCycleTime.Text, out DOU_CycleTime);
            //    txtCycleTime.Text = string.Format("{0:n0}", (int)DOU_CycleTime);
            //}
        }

        #endregion



        // 합계수량 구해내기.
        private void Get_TotalQty()
        {
            double My_WorkQty = 0;
            double Your_RemainQty = 0;
            double TotalQty = 0;

            double.TryParse(txtWorkQty.Text, out My_WorkQty);
            double.TryParse(txtSetCT.Text, out Your_RemainQty);

            TotalQty = My_WorkQty + Your_RemainQty;
            //txtNowCT.Text = TotalQty.ToString();
        }



        // 오른쪽 작업(취소)(삭제)(파기)??? 버튼 신규생성.
        private void btnWorkingDestory_Click(object sender, EventArgs e)
        {
            Message[0] = "[작업삭제]";
            Message[1] = "생산작업을 진행중이였습니다. 작업을 삭제하시겠습니까?";
            if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.OK)
            {
                //1. 현재아이 정보.
                float NowJobID = float.Parse(updateJobID);
                string YLabelID = m_LabelID;

                try
                {
                    //2. 삭제 프로시저.
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("JobID", NowJobID);
                    sqlParameter.Add("YLabelID", YLabelID);
                    DataStore.Instance.ProcedureToDataSet("[xp_WizWork_dWkResult_YellowIng]", sqlParameter, true);

                    //3. 다시 목록으로 복귀.
                    cmdExit_Click(null, null);
                }
                catch (Exception EX)
                {
                    Message[0] = "[작업삭제]";
                    Message[1] = "작업삭제에 실패하였습니다. JobID: " + NowJobID + ",라벨: " + YLabelID + "\r\n" +
                        EX.Message.ToString();
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return;
                }
            }
        }


        // 오른쪽 잔량 불러오기 버튼 클릭.
        private void btnBringSplitData_Click(object sender, EventArgs e)
        {
            //string BringArticleID = this.txtArticleID.Text;

            //frm_PopUp_BringSplitData BringSplitData = new frm_PopUp_BringSplitData(BringArticleID);
            //BringSplitData.Owner = this;
            //BringSplitData.WriteTextEvent += BringSplitData_WriteTextEvent;

            //void BringSplitData_WriteTextEvent(string Sum_SplitUsingQty)
            //{
            //    double D_Sum_SplitUsingQty = 0;

            //    double.TryParse(Sum_SplitUsingQty, out D_Sum_SplitUsingQty);

            //    txtSetCT.Text = D_Sum_SplitUsingQty.ToString();
            //    Get_TotalQty();
            //}

            //BringSplitData.ShowDialog();
            //return;

            #region 저장

            try
            {
                // BOM - wk_resultArticleChild에 맞춰서, 
                // 하위품 생산가능량보다 작업수량이 클 경우 잔량이동 팝업 활성화
                if (CheckWorkQty() == false)
                {
                    return;
                }


                Cursor = Cursors.WaitCursor;

                string sWDNO = "";
                string sWDID = "";
                float sWDQty = 0;
                int iCnt = 0;
                int nColorRow = 0;

                float StartTime = 0;
                float EndTime = 0;
                bool Success = false;
                float sLogID = 0;

                string FacilityCollectQty = "";
                string CycleTime = "";


                ProdQty = Lib.GetDouble(txtWorkQty.Text).ToString();
                StartTime = float.Parse(mtb_From.Text.Replace("-", "") + dtStartTime.Value.ToString("HHmmss"));
                EndTime = float.Parse(mtb_To.Text.Replace("-", "") + dtEndTime.Value.ToString("HHmmss"));

                FacilityCollectQty = Lib.GetDouble(txtFacilityCollectQty.Text).ToString();      // 설비수집 수량
                CycleTime = Lib.GetDouble(txtNowCT.Text).ToString();                        // CycleTime


                if (StartTime > EndTime)
                {
                    throw new Exception("시작시간이 종료시간보다 더 큽니다.");
                }
                if (Frm_tprc_Main.g_tBase.ProcessID == "" || Frm_tprc_Main.g_tBase.MachineID == "")
                {
                    throw new Exception("공정 또는 호기가 선택되지 않았습니다.");
                }
                if (Frm_tprc_Main.g_tBase.PersonID == "")//수정필요
                {
                    throw new Exception("작업자가 선택되지 않았습니다.");
                }
                if (ProdQty == "0")
                {
                    throw new Exception("작업수량이 입력되지 않았습니다.");
                }
                // 2020.02.06  CycleTime 역시 필수입력 항목으로 추가합니다..   허윤구
                //if (CycleTime == "0")
                //{
                //    throw new Exception("CycleTime이 입력되지 않았습니다.");
                //}


                //'-------------------------------------------------------------------------------
                //'생산 후 최소 숙성시간 경과 안된 건 사용 불가, 유효시간 경과한 고무제품 사용 불가
                //'-------------------------------------------------------------------------------
                //if (txtBoxID.Text != "" && txtlYbox.Text != "" && m_MtrExceptYN != "Y")
                if (m_MtrExceptYN != "Y")
                {
                    //foreach (DataGridViewRow dgvr in GridData2.Rows)
                    //{                        
                    //    if (!CheckID(dgvr.Cells["BarCode"].Value.ToString()))
                    //    {
                    //        return;
                    //    }                        
                    //}
                }

                //'-----------------------------------------------------------------------------------------
                //'투입가능 하위품 선입선출 Check 원자재투입 예외처리 시 체크하지않는다.
                //'-----------------------------------------------------------------------------------------
                //GetMtrChileLotRemainQty(txtBoxID.Text, Frm_tprc_Main.g_tBase.ProcessID, ProdQty);
                //'-------------------------------------------------------------------------------
                //'지시량 대비 실적 많은지 check
                //'-------------------------------------------------------------------------------


                //'-------------------------------------------------------------------------------
                //'금형 Article 맞는지 한계수명 넘어가는지 체크
                //'-------------------------------------------------------------------------------

                //if (Frm_tprc_Main.list_tMold.Count > 0)
                //{
                //    for (int i = 0; i < Frm_tprc_Main.list_tMold.Count; i++)
                //    {
                //        if (i == 0)
                //        {
                //            MoldIDList = Frm_tprc_Main.list_tMold[i].sMoldID;
                //        }
                //        else
                //        {
                //            MoldIDList = MoldIDList + "," + Frm_tprc_Main.list_tMold[i].sMoldID;
                //        }
                //    }
                //}

                //GetWorkLotInfo(txtBoxID.Text, Frm_tprc_Main.g_tBase.ProcessID, Frm_tprc_Main.g_tBase.MachineID, MoldIDList);//공정이동전표의 정보 가져오기


                //'-------------------------------------------------------------------------------
                //'생산실적 잔량 초과 실적 등록 불가
                //'-------------------------------------------------------------------------------
                if (txtInUnitClss.Text == "EA") // 생산제품의 단위가 갯수(EA)로 세리고 있는 공정이라면
                {
                    if ((int)Lib.GetDouble(ProdQty) > (int)Lib.GetDouble(txtInstRemainQty.Text))
                    {
                        Message[0] = "[확인]";
                        Message[1] = "생산잔량: " + txtInstRemainQty.Text + "입니다. 초과된 생산 실적을 등록하시겠습니까?";
                        // 등록 불가 합니다. \r\n 계속 진행 하시겠습니까?";
                        if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.No)
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                    }
                }
                else                // Gram이라 가정한다면.
                {
                    double douProdQty = 0;
                    double douInstRemainQty = 0;
                    douProdQty = Lib.GetDouble(ProdQty) / 1000;//kg단위로 변환
                    douInstRemainQty = Lib.GetDouble(txtInstRemainQty.Text);//kg단위
                    if (douProdQty > douInstRemainQty)
                    {
                        Message[0] = "[확인]";
                        Message[1] = "생산잔량: " + txtInstRemainQty.Text + "kg입니다. 초과된 생산 실적을 등록하시겠습니까?";
                        if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.No)
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                    }
                }
                //18.06.18 주석 
                //txtInstRemainQty.Text = string.Format("{0:#,###}", float.Parse(Lib.CheckNum(InstQty.ToString())) - float.Parse(Lib.CheckNum(WorkQty.ToString())) - float.Parse(Lib.CheckNum(ProdQty)));
                //18.06.18 주석 


                //'-----------------------------------------------------------------------------------------
                //'탭/다이스 교환
                //'-----------------------------------------------------------------------------------------

                Sub_Ttd.TdChangeYN = "N";
                sWDNO = "";
                sWDID = "";

                int TWkRCon = 1;

                // 2020.05.05 HYTech : 생산박스당 수량으로 나누어서 실적등록!!!!!!
                List<double> lstWorkQty = new List<double>();
                double TotalQty = 0;
                double.TryParse(txtWorkQty.Text, out TotalQty);
                if (ProdQtyPerBox > 1)
                {
                    // 갯수
                    //TWkRCon = (int)(TotalQty / ProdQtyPerBox);
                    TWkRCon = (int)Math.Ceiling(TotalQty / ProdQtyPerBox);
                    for (int i = 0; i < TWkRCon; i++)
                    {
                        // 마지막 행
                        if (i == (TWkRCon - 1))
                        {
                            double RemainQty = TotalQty % ProdQtyPerBox;
                            if (RemainQty == 0) { RemainQty = ProdQtyPerBox; }
                            lstWorkQty.Add(RemainQty);
                        }
                        else
                        {
                            lstWorkQty.Add(ProdQtyPerBox);
                        }
                    }
                }
                else
                {
                    lstWorkQty.Add(TotalQty);
                }

                //'-------------------------------------------------------------------------------
                //'상위품 설정
                //'-------------------------------------------------------------------------------

                list_TWkResult = new List<Sub_TWkResult>();
                int InstDetSeq = 0;
                float WorkQty = 0;
                float nCycleTime = 0;
                for (int i = 0; i < TWkRCon; i++)
                {
                    list_TWkResult.Add(new Sub_TWkResult());


                    list_TWkResult[i].JobID = float.Parse(updateJobID);
                    list_TWkResult[i].InstID = txtInstID.Text;
                    int.TryParse(Lib.GetDouble(txtInstDetSeq.Text).ToString(), out InstDetSeq);
                    list_TWkResult[i].InstDetSeq = InstDetSeq;
                    list_TWkResult[i].LabelID = lstStartLabel[0];
                    list_TWkResult[i].LabelGubun = this.txtLabelGubun.Text;

                    list_TWkResult[i].ProcessID = Frm_tprc_Main.g_tBase.ProcessID;
                    list_TWkResult[i].MachineID = Frm_tprc_Main.g_tBase.MachineID;
                    list_TWkResult[i].ArticleID = txtArticleID.Text;

                    float.TryParse(lstWorkQty[i].ToString(), out WorkQty);
                    list_TWkResult[i].WorkQty = WorkQty;
                    float.TryParse(CycleTime, out nCycleTime);
                    list_TWkResult[i].CycleTime = nCycleTime;
                    if (txtDefectQty.Text != string.Empty)
                    {
                        float DefectQty = 0;
                        float.TryParse(txtDefectQty.Text, out DefectQty);
                        //list_TWkResult[i].WorkQty = WorkQty - DefectQty;
                    }

                    list_TWkResult[i].sLastArticleYN = m_LastArticleYN;

                    list_TWkResult[i].ProdAutoInspectYN = m_ProdAutoInspectYN;
                    list_TWkResult[i].sOrderID = m_OrderID;
                    list_TWkResult[i].nOrderSeq = m_OrderSeq;
                    list_TWkResult[i].WorkStartDate = mtb_From.Text.Replace("-", "");
                    list_TWkResult[i].WorkStartTime = dtStartTime.Value.ToString("HHmmss");

                    list_TWkResult[i].WorkEndDate = mtb_To.Text.Replace("-", "");
                    list_TWkResult[i].WorkEndTime = dtEndTime.Value.ToString("HHmmss");
                    list_TWkResult[i].JobGbn = "1";


                    //'------------------------------------------------------------------------------------------

                    list_TWkResult[i].Comments = Frm_tprc_Main.g_tBase.ProcessID + "작업종료에 따른 데이터 저장(Upgrade)";
                    list_TWkResult[i].ReworkOldYN = "";
                    list_TWkResult[i].ReworkLinkProdID = "";
                    list_TWkResult[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;
                    list_TWkResult[i].WDNO = sWDNO;

                    list_TWkResult[i].WDID = sWDID;
                    list_TWkResult[i].WDQty = sWDQty;
                    list_TWkResult[i].s4MID = "";
                    float.TryParse(m_LogID, out sLogID);
                    list_TWkResult[i].sLogID = sLogID;


                    //if (m_ProcessID == "0405" || m_ProcessID == "1101" || m_ProcessID == "3101" || m_ProcessID == "4101")
                    //{
                    //    Frm_PopUpSel_AddSave fpas = new Frm_PopUpSel_AddSave(m_ProcessID, PartGBNID, i, txtArticleID.Text);
                    //    fpas.Owner = this;
                    //    fpas.WriteTextEvent += new Frm_PopUpSel_AddSave.TextEventHandler(GetData);
                    //    if (fpas.ShowDialog() == DialogResult.OK)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        return;
                    //    }
                    //}
                }


                if (Frm_tprc_Main.list_tMold.Count > 0)
                {
                    int nCount = list_TWkResult.Count;
                    list_TMold = new List<Sub_TMold>();
                    for (int i = 0; i < nCount; i++)
                    {
                        list_TMold.Add(new Sub_TMold());
                    }

                    //list_TMold = new Sub_TMold[nCount];
                }

                //if (Frm_tprc_Main.g_tMol.sMoldID != "")
                //{
                //    if (TWkRCon == 1)
                //    {
                //        Sub_TMold = new Sub_TMold[1];

                //        Sub_TMold[0].sMoldID = Frm_tprc_Main.g_tMol.sMoldID;
                //        Sub_TMold[0].sRealCavity = Frm_tprc_Main.g_tMol.sRealCavity;
                //        Sub_TMold[0].sHitCount = int.Parse(Lib.CheckNum(Sub_TWkResult[0].WorkQty.ToString()));
                //    }

                //}
                if (Frm_tprc_Main.list_tMold.Count > 0)
                {
                    if (Frm_tprc_Main.list_tMold[0].sMoldID != "")
                    {
                        //if (TWkRCon > 1)
                        //{
                        //    //Sub_TMold = new Sub_TMold[/*list_TWkResult.Count*/TWkRCon];
                        //}
                        for (int i = 0; i < TWkRCon/*list_TWkResult.Count*/; i++)
                        {
                            for (int j = 0; j < Frm_tprc_Main.list_tMold.Count; j++)
                            {
                                list_TMold[i].sMoldID = Frm_tprc_Main.list_tMold[j].sMoldID;
                                list_TMold[i].sRealCavity = Frm_tprc_Main.list_tMold[j].sRealCavity;

                            }
                        }
                    }
                }

                iCnt = 0;

                //'-------------------------------------------------------------------------------
                //'하위품 설정
                //'-------------------------------------------------------------------------------

                nColorRow = GridData2.Rows.Count;
                if (nColorRow > 0)
                {
                    list_TWkResultArticleChild = new List<Sub_TWkResultArticleChild>();

                    for (int i = 0; i < nColorRow; i++)
                    {
                        DataGridViewRow row = GridData2.Rows[i];

                        list_TWkResultArticleChild.Add(new Sub_TWkResultArticleChild());

                        list_TWkResultArticleChild[i].JobID = 0;
                        list_TWkResultArticleChild[i].ChildLabelID = Lib.CheckNull(row.Cells["BarCode"].Value.ToString());
                        list_TWkResultArticleChild[i].ChildLabelGubun = Lib.CheckNull(row.Cells["LabelGubun"].Value.ToString());
                        list_TWkResultArticleChild[i].ChildArticleID = Lib.CheckNull(row.Cells["ChildArticleID"].Value.ToString());
                        list_TWkResultArticleChild[i].ReworkOldYN = "";
                        list_TWkResultArticleChild[i].ReworkLinkChildProdID = "";
                        list_TWkResultArticleChild[i].OutDate = DateTime.Now.ToString("yyyyMMdd");
                        list_TWkResultArticleChild[i].OutTime = DateTime.Today.ToString("HHmmss");
                        list_TWkResultArticleChild[i].Flag = Lib.CheckNull(row.Cells["Flag"].Value.ToString());
                        list_TWkResultArticleChild[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                        //Sub_TWkResultArticleChild[i].JobID = 0;
                        ////Sub_TWkResultArticleChild[i].JobSeq = int.Parse(Lib.CheckNull(row.Cells["Seq"].Value.ToString()));
                        //Sub_TWkResultArticleChild[i].ChildLabelID = Lib.CheckNull(row.Cells["BarCode"].Value.ToString());
                        //Sub_TWkResultArticleChild[i].ChildLabelGubun = Lib.CheckNull(row.Cells["LabelGubun"].Value.ToString());
                        //Sub_TWkResultArticleChild[i].ChildArticleID = Lib.CheckNull(row.Cells["ChildArticleID"].Value.ToString());
                        //Sub_TWkResultArticleChild[i].ReworkOldYN = "";
                        //Sub_TWkResultArticleChild[i].ReworkLinkChildProdID = "";
                        //Sub_TWkResultArticleChild[i].OutDate = Lib.MakeDate(4, DateTime.Today.ToString("yyyyMMdd"));
                        //Sub_TWkResultArticleChild[i].OutTime = Lib.MakeDate(4, DateTime.Today.ToString("HHmmss"));
                        //Sub_TWkResultArticleChild[i].Flag = Lib.CheckNull(row.Cells["Flag"].Value.ToString());
                        //Sub_TWkResultArticleChild[i].CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                        iCnt++;
                    }
                }

                //'-------------------------------------------------------------------------------
                //'불량입력화면에서 가져온 불량 수량 만큼의 정보에 데이타 설정
                //'-------------------------------------------------------------------------------

                if (Frm_tprc_Main.g_tBase.DefectCnt > 0)
                {
                    for (int i = 0; i < Frm_tprc_Main.g_tBase.DefectCnt; i++)
                    {
                        Frm_tprc_Main.list_g_tInsSub[i].BoxID = txtBoxID.Text;
                        Frm_tprc_Main.list_g_tInsSub[i].OrderID = m_OrderID;
                        Frm_tprc_Main.list_g_tInsSub[i].OrderSeq = m_OrderSeq;
                    }
                }

                //'--------------------------------------------------------------------------------
                //'   현재 진행하는 건이 첫 공정 이라면 공동이동전표 발행 
                //'--------------------------------------------------------------------------------
                if (LabelPrintYN == "Y")
                {
                    int mInstDetSeq = 0;
                    long nQtyPerBox = 0;
                    list_TWkLabelPrint = new List<Sub_TWkLabelPrint>();

                    for (int i = 0; i < TWkRCon; i++)
                    {
                        list_TWkLabelPrint.Add(new Sub_TWkLabelPrint());

                        list_TWkLabelPrint[i].sLabelID = "";
                        list_TWkLabelPrint[i].sProcessID = Frm_tprc_Main.g_tBase.ProcessID;

                        list_TWkLabelPrint[i].sArticleID = txtArticleID.Text;
                        list_TWkLabelPrint[i].sLabelGubun = "7";

                        //if (Frm_tprc_Main.g_tBase.ProcessID == "0405")//혼련이동전표
                        //{
                        //}
                        //else if (Frm_tprc_Main.g_tBase.ProcessID == "1101")//재단이동전표
                        //{
                        //    list_TWkLabelPrint[i].sArticleID = txtArticleID.Text;
                        //    list_TWkLabelPrint[i].sLabelGubun = "3";
                        //}
                        //else if (nProcessID > 2100)//성형공정(공정이동전표) 이후
                        //{
                        //    list_TWkLabelPrint[i].sArticleID = m_OrderArticleID;
                        //    list_TWkLabelPrint[i].sLabelGubun = "5";
                        //}

                        list_TWkLabelPrint[i].sPrintDate = mtb_From.Text.Replace("-", "");

                        list_TWkLabelPrint[i].sReprintDate = "";
                        list_TWkLabelPrint[i].nReprintQty = 0;
                        list_TWkLabelPrint[i].sInstID = txtInstID.Text;

                        int.TryParse(Lib.GetDouble(txtInstDetSeq.Text).ToString(), out mInstDetSeq);
                        list_TWkLabelPrint[i].nInstDetSeq = mInstDetSeq;
                        list_TWkLabelPrint[i].sOrderID = m_OrderID;

                        list_TWkLabelPrint[i].nPrintQty = 1;

                        if (TWkRCon == 1)
                        {
                            long.TryParse(Lib.GetDouble(ProdQty).ToString(), out nQtyPerBox);
                            list_TWkLabelPrint[i].nQtyPerBox = nQtyPerBox;
                        }


                        list_TWkLabelPrint[i].sCreateuserID = Frm_tprc_Main.g_tBase.PersonID;
                        list_TWkLabelPrint[i].sLastUpdateUserID = Frm_tprc_Main.g_tBase.PersonID;
                    }
                }
                //

                //'-------------------------------------------------------------------------------
                //'생산실적  저장
                //'-------------------------------------------------------------------------------

                Success = AddNewWorkResultByProdQtyPerBox(iCnt, Frm_tprc_Main.g_tBase.DefectCnt);
                if (Success)
                {
                    if (LabelPrintYN == "Y")
                    {
                        PrintWorkCard(TWkRCon);
                        //WizCommon.Popup.MyMessageBox.ShowBox("[저장완료]", "대성공", 3, 1);
                    }
                    else
                    {
                        Message[0] = "[저장 완료]";
                        Message[1] = "저장이 완료되었습니다.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                    }

                    StartHandleWorking_Click();
                    SetFormDataClear();
                    cmdExit_Click(null, null);  // 나가.
                }
                else
                {
                    throw new Exception();
                }
                //'    '-----------------------------------------------------------------------------------------
                //     '저장된 결과 재 조회
                //'    '-----------------------------------------------------------------------------------------
                //FillGridData1();
                Cursor = Cursors.Default;
            }

            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<cmdSave>\r\n{0}", excpt.Message), "[오류]", 0, 1);
                Cursor = Cursors.Default;
            }
            #endregion
        }

        #region 실적등록 후 작업 계속! → DB 임시인서트 펑션

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
                sqlParameter1.Add("StartSaveLabelID", "");

                sqlParameter1.Add("LabelGubun", "1");
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

                sqlParameter1.Add("ArticleID", txtArticleID.Text);
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
                for (int i = 0; i < GridData2.Rows.Count; i++)
                {

                    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                    sqlParameter2.Add("JobID", "");
                    sqlParameter2.Add("ChildLabelID", GridData2.Rows[i].Cells["Barcode"].Value.ToString());//
                    sqlParameter2.Add("ChildLabelGubun", ""); // 더미 데이터니까, 일단 이건 뺌
                    sqlParameter2.Add("ChildArticleID", GridData2.Rows[i].Cells["ChildArticleID"].Value.ToString());
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

        #region 기타 메서드 모음

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN0(object obj)
        {
            return string.Format("{0:N0}", obj);
        }

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN1(object obj)
        {
            return string.Format("{0:N1}", obj);
        }

        // 천마리 콤마, 소수점 두자리
        private string stringFormatN2(object obj)
        {
            return string.Format("{0:N2}", obj);
        }

        // 천마리 콤마, 소수점 두자리
        private string stringFormatNDigit(object obj, int num)
        {
            return string.Format("{0:N" + num + "}", obj);
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

        // 나누기 함수
        private double devideNum(double a, double b)
        {
            double result = 0;

            if (b == 0)
            {
                return result;
            }
            else
            {
                result = a / b;
            }

            return result;
        }

        #endregion


        // TKB 라벨 선택 저장 - 2020.09.03 추가
        // 요청사항 : 첫공정인데 하루, 이틀 걸려서 한박스를 완성하는 것도 있음
        //                하지만 작업실적은 각각 넣고 싶음.
        // → 첫번째 공정일 때만 활성화 할것!!!
        // → 라벨을 선택(스캔)하여 그 라벨에 저장할 수 있도록!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //     = 라벨 스캔 시 만들어지는 그 품명과 똑같은 라벨인지 확인을 하고? 기타등등 할것
        private frm_PopUp_SaveSelection_CodeView SaveSelectionLabelInfo = new frm_PopUp_SaveSelection_CodeView();

        private void btnSaveSelection_Click(object sender, EventArgs e)
        {
            // 저장전 체크
            if (CheckData() == false)
            {
                return;
            }

            // BOM - wk_resultArticleChild에 맞춰서, 
            // 하위품 생산가능량보다 작업수량이 클 경우 잔량이동 팝업 활성화
            if (CheckWorkQty() == false)
            {
                return;
            }

            frm_PopUp_SaveSelection SaveSel = new frm_PopUp_SaveSelection(txtArticle.Text, txtBuyerArticleNo.Text, txtProdQtyPerBox.Text);
            SaveSel.StartPosition = FormStartPosition.CenterScreen;
            SaveSel.BringToFront();
            SaveSel.TopMost = true;

            if (SaveSel.ShowDialog() == DialogResult.OK)
            {
                // 라벨값을 가지고!
                this.SaveSelectionLabelInfo.LabelID = SaveSel.returnLabelInfo.LabelID;
                if (SaveSel.returnLabelInfo.SaveQtyPerBoxYN == true)
                {
                    SaveSelectionLabelInfo.SaveQtyPerBoxYN = true;
                    SaveSelectionLabelInfo.ProdQtyPerBox = Lib.CheckNull(SaveSel.returnLabelInfo.ProdQtyPerBox);
                    SaveSelectionLabelInfo.NowLoc = Lib.CheckNull(SaveSel.returnLabelInfo.NowLoc);
                }
                cmdSave_Click(null, null);
            }
        }

        #region [장입량으로 나누어] 생산 등록, 하위품, 불량등록 : TKB용 + 라벨 선택 저장 추가한 버전(첫번째 공정일때)

        private bool AddNewWorkResultByProdQtyPerBox_TKB(int nCnt, int nDefectCnt)
        {
            List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
            List<List<string>> ListProcedureName = new List<List<string>>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

            try
            {
                for (int i = 0; i < list_TWkResult.Count; i++)
                {

                    //'*****************************************************************************************************
                    //'                  공정이동전표 등록
                    //'*****************************************************************************************************
                    if (list_TWkLabelPrint.Count > 0)
                    {
                        if (list_TWkLabelPrint[i].sProcessID != ""
                            && !(ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq) == 1 && SaveSelectionLabelInfo.LabelID != null && !SaveSelectionLabelInfo.LabelID.Equals("")))  // TKB 추가 2020.09.03
                        {
                            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                            sqlParameter.Add("LabelID", "");
                            sqlParameter.Add("LabelGubun", list_TWkLabelPrint[i].sLabelGubun);
                            sqlParameter.Add("ProcessID", list_TWkLabelPrint[i].sProcessID);
                            sqlParameter.Add("ArticleID", list_TWkLabelPrint[i].sArticleID);
                            sqlParameter.Add("PrintDate", list_TWkLabelPrint[i].sPrintDate);

                            sqlParameter.Add("ReprintDate", list_TWkLabelPrint[i].sReprintDate);
                            sqlParameter.Add("ReprintQty", list_TWkLabelPrint[i].nReprintQty);
                            sqlParameter.Add("InstID", list_TWkLabelPrint[i].sInstID);
                            sqlParameter.Add("InstDetSeq", list_TWkLabelPrint[i].nInstDetSeq);
                            sqlParameter.Add("OrderID", list_TWkLabelPrint[i].sOrderID);

                            sqlParameter.Add("PrintQty", list_TWkLabelPrint[i].nPrintQty);
                            sqlParameter.Add("LabelPrintQty", 1);
                            sqlParameter.Add("nQtyPerBox", list_TWkLabelPrint[i].nQtyPerBox);
                            sqlParameter.Add("CreateUserID", list_TWkLabelPrint[i].sCreateuserID);

                            WizCommon.Procedure pro1 = new WizCommon.Procedure();
                            pro1.Name = "[xp_WizWork_iwkLabelPrint_C]";
                            pro1.OutputUseYN = "Y";
                            pro1.OutputName = "LabelID";
                            pro1.OutputLength = "20";

                            Prolist.Add(pro1);
                            ListParameter.Add(sqlParameter);
                        }
                    }
                    //'************************************************************************************************
                    //'                               상위품 생산 //xp_wkResult_iWkResult
                    //'************************************************************************************************
                    Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                    WizCommon.Procedure pro2 = new WizCommon.Procedure();

                    if (i == 0)
                    {
                        sqlParameter1.Add("JobID", list_TWkResult[i].JobID);

                        if (list_TWkLabelPrint.Count > 0)
                        {
                            // TKB 추가 2020.09.03
                            if (ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq) == 1 && SaveSelectionLabelInfo.LabelID != null && !SaveSelectionLabelInfo.LabelID.Equals(""))
                            {
                                sqlParameter1.Add("LabelID", SaveSelectionLabelInfo.LabelID);
                            }
                            else
                            {
                                sqlParameter1.Add("LabelID", list_TWkLabelPrint[i].sLabelID);

                            }
                        }
                        else
                        {
                            sqlParameter1.Add("LabelID", list_TWkResult[i].LabelID);
                        }
                        sqlParameter1.Add("LabelGubun", list_TWkResult[i].LabelGubun);
                        sqlParameter1.Add("WorkStartDate", list_TWkResult[i].WorkStartDate);
                        sqlParameter1.Add("WorkStartTime", list_TWkResult[i].WorkStartTime);
                        sqlParameter1.Add("WorkEndDate", list_TWkResult[i].WorkEndDate);
                        sqlParameter1.Add("WorkEndTime", list_TWkResult[i].WorkEndTime);
                        sqlParameter1.Add("WorkQty", list_TWkResult[i].WorkQty);
                        sqlParameter1.Add("CycleTime", list_TWkResult[i].CycleTime);
                        sqlParameter1.Add("ProcessID", list_TWkResult[i].ProcessID);
                        sqlParameter1.Add("MachineID", list_TWkResult[i].MachineID);
                        sqlParameter1.Add("Comments", list_TWkResult[i].ProcessID + "작업종료에 따른 저장구문");

                        sqlParameter1.Add("UpdateUserID", list_TWkResult[i].CreateUserID);

                        //pro2.Name = "xp_wkResult_uWkResultOne";
                        pro2.Name = "xp_prdWork_uWkResultOne";//하이테크만 이렇게 사용 // 2020.02.21 둘리 : CycleTime 추가되도록 수정
                        pro2.OutputUseYN = "N";
                        pro2.OutputName = "JobID";
                        pro2.OutputLength = "20";
                    }
                    else
                    {
                        sqlParameter1.Add("JobID", 0);
                        sqlParameter1.Add("InstID", list_TWkResult[i].InstID);
                        sqlParameter1.Add("InstDetSeq", list_TWkResult[i].InstDetSeq);
                        if (list_TWkLabelPrint.Count > i)
                        {
                            // TKB 추가 2020.09.03
                            if (ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq) == 1 && SaveSelectionLabelInfo.LabelID != null && !SaveSelectionLabelInfo.LabelID.Equals(""))
                            {
                                sqlParameter1.Add("LabelID", SaveSelectionLabelInfo.LabelID);
                            }
                            else
                            {
                                sqlParameter1.Add("LabelID", list_TWkLabelPrint[i].sLabelID);

                            }
                        }
                        else
                        {
                            sqlParameter1.Add("LabelID", list_TWkResult[i].LabelID);
                        }
                        sqlParameter1.Add("StartSaveLabelID", "");

                        sqlParameter1.Add("LabelGubun", list_TWkResult[i].LabelGubun);
                        sqlParameter1.Add("ProcessID", list_TWkResult[i].ProcessID);
                        sqlParameter1.Add("MachineID", list_TWkResult[i].MachineID);
                        sqlParameter1.Add("ScanDate", list_TWkResult[i].WorkStartDate); // 둘리 : 2020.05.27 : StartDate, StartTime 이랑 맞춤
                        sqlParameter1.Add("ScanTime", list_TWkResult[i].WorkStartTime);

                        sqlParameter1.Add("ArticleID", list_TWkResult[i].ArticleID);
                        sqlParameter1.Add("WorkQty", list_TWkResult[i].WorkQty);
                        sqlParameter1.Add("Comments", list_TWkResult[i].Comments);
                        sqlParameter1.Add("ReworkOldYN", list_TWkResult[i].ReworkOldYN);
                        sqlParameter1.Add("ReworkLinkProdID", list_TWkResult[i].ReworkLinkProdID);

                        sqlParameter1.Add("WorkStartDate", list_TWkResult[i].WorkStartDate);
                        sqlParameter1.Add("WorkStartTime", list_TWkResult[i].WorkStartTime);
                        sqlParameter1.Add("WorkEndDate", list_TWkResult[i].WorkEndDate);
                        sqlParameter1.Add("WorkEndTime", list_TWkResult[i].WorkEndTime);
                        sqlParameter1.Add("JobGbn", list_TWkResult[i].JobGbn);

                        sqlParameter1.Add("NoReworkCode", list_TWkResult[i].sNowReworkCode);
                        sqlParameter1.Add("WDNO", list_TWkResult[i].WDNO);
                        sqlParameter1.Add("WDID", list_TWkResult[i].WDID);
                        sqlParameter1.Add("WDQty", list_TWkResult[i].WDQty);
                        sqlParameter1.Add("LogID", list_TWkResult[i].sLogID);

                        sqlParameter1.Add("s4MID", list_TWkResult[i].s4MID);
                        sqlParameter1.Add("DayOrNightID", Frm_tprc_Main.g_tBase.DayOrNightID);
                        sqlParameter1.Add("CycleTime", list_TWkResult[i].CycleTime);
                        sqlParameter1.Add("FirstJobID", list_TWkResult[0].JobID);
                        sqlParameter1.Add("CreateUserID", list_TWkResult[i].CreateUserID);

                        pro2.Name = "xp_prdWork_iWkResultSecond";
                        pro2.OutputUseYN = "Y";
                        pro2.OutputName = "JobID";
                        pro2.OutputLength = "20";
                    }


                    Prolist.Add(pro2);
                    ListParameter.Add(sqlParameter1);

                    //'****************************************************************************************************
                    //'정상작업의 경우    Sub_TWkResult.JobGbn = "1"
                    //'****************************************************************************************************
                    if (list_TWkResult[0].JobGbn == "1")
                    {
                        //'************************************************************************************************
                        //'                               하위품 스켄이력 등록
                        //'************************************************************************************************

                        #region 기존 하위품 등록 → wk_resultArticleChild
                        if (nCnt > 0)
                        {
                            for (int k = 0; k < nCnt; k++)
                            {
                                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                                if (i == 0)
                                {
                                    sqlParameter2.Add("JobID", list_TWkResult[i].JobID);
                                }
                                else
                                {
                                    sqlParameter2.Add("JobID", 0);
                                }

                                //sqlParameter2.Add("JobID", list_TWkResult[i].JobID);
                                sqlParameter2.Add("ChildLabelID", list_TWkResultArticleChild[k].ChildLabelID);//
                                sqlParameter2.Add("ChildLabelGubun", list_TWkResultArticleChild[k].ChildLabelGubun);
                                sqlParameter2.Add("ChildArticleID", list_TWkResultArticleChild[k].ChildArticleID);
                                sqlParameter2.Add("ReworkOldYN", list_TWkResultArticleChild[k].ReworkOldYN);
                                sqlParameter2.Add("ReworkLinkChildProdID", list_TWkResultArticleChild[k].ReworkLinkChildProdID);

                                sqlParameter2.Add("Seq", (k + 1));
                                sqlParameter2.Add("CreateUserID", list_TWkResultArticleChild[k].CreateUserID);

                                WizCommon.Procedure pro3 = new WizCommon.Procedure();
                                pro3.Name = "xp_prdWork_iWkResultArticleChild";
                                pro3.OutputUseYN = "N";
                                pro3.OutputName = "JobID";
                                pro3.OutputLength = "20";

                                Prolist.Add(pro3);
                                ListParameter.Add(sqlParameter2);

                            }
                        }
                        #endregion


                        // '************************************************************************************************
                        //'                              불량 등록 시   //xp_wkResult_iInspect
                        //'************************************************************************************************

                        if (i == 0)
                        {
                            foreach (string Key in dicDefect.Keys)
                            {
                                var Defect = dicDefect[Key] as frm_tprc_Work_Defect_U_CodeView;
                                if (Defect != null)
                                {
                                    Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();

                                    sqlParameter4.Add("DefectID", Key.Trim());
                                    sqlParameter4.Add("DefectQty", ConvertDouble(Defect.DefectQty));
                                    sqlParameter4.Add("XPos", ConvertInt(Defect.XPos));
                                    sqlParameter4.Add("YPos", ConvertInt(Defect.YPos));
                                    sqlParameter4.Add("JobID", list_TWkResult[0].JobID);
                                    sqlParameter4.Add("CreateUserID", list_TWkResult[0].CreateUserID);

                                    WizCommon.Procedure pro4 = new WizCommon.Procedure();
                                    pro4.Name = "xp_prdWork_iWorkDefect";
                                    pro4.OutputUseYN = "N";
                                    pro4.OutputName = "JobID";
                                    pro4.OutputLength = "20";

                                    Prolist.Add(pro4);
                                    ListParameter.Add(sqlParameter4);
                                }
                            }
                        }

                        //'************************************************************************************************
                        //'                            생산제품 재고 생성 및 하품 자재 출고 처리  //xp_wkResult_iWkResultStuffInOut
                        //'************************************************************************************************
                        //if (m_ProcessID != "2101" || (m_ProcessID == "2101" && blSHExit))
                        ////성형공정이 아니거나 또는 , 성형공정이면서 작업종료 시점일때만 입력
                        //{

                        Dictionary<string, object> sqlParameter5 = new Dictionary<string, object>();

                        sqlParameter5.Add("JobID", list_TWkResult[i].JobID);
                        sqlParameter5.Add("CreateUserID", list_TWkResult[i].CreateUserID);
                        sqlParameter5.Add("sRtnMsg", "");

                        WizCommon.Procedure pro6 = new WizCommon.Procedure();
                        pro6.Name = "xp_prdWork_iWkResultStuffInOut";
                        pro6.OutputUseYN = "N";
                        pro6.OutputName = "JobID";
                        pro6.OutputLength = "20";

                        Prolist.Add(pro6);
                        ListParameter.Add(sqlParameter5);


                        //}
                    }
                }

                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    list_Result.RemoveAt(0);

                    //int rsCount = list_Result.Count / 2;//2 = Output갯수(JobID, LabelID)
                    int a = 0;
                    for (int i = 0; i < list_Result.Count; i++)
                    {
                        KeyValue kv = list_Result[i];
                        if (kv.key == "LabelID")
                        {
                            list_TWkLabelPrint[a++].sLabelID = kv.value;
                            //list_TWkLabelPrint[i].sLabelID = kv.value;
                        }
                        //else if (kv.key == "JobID")
                        //{
                        //    //list_TWkResult[i / 2].JobID = float.Parse(kv.value);
                        //    list_TWkResult[i].JobID = float.Parse(kv.value);
                        //}
                    }
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

        #region 2020.09 저장 전 체크 이벤트 - CheckData

        private bool CheckData()
        {
            bool flag = true;

            // 작업시간 오류
            float StartTime = float.Parse(mtb_From.Text.Replace("-", "") + dtStartTime.Value.ToString("HHmmss"));
            float EndTime = float.Parse(mtb_To.Text.Replace("-", "") + dtEndTime.Value.ToString("HHmmss"));
            if (StartTime > EndTime)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("시작시간이 종료시간보다 더 큽니다.", "[저장 전 오류]", 0, 1);
                return false;
            }

            // 공정, 호기 오류
            if (Frm_tprc_Main.g_tBase.ProcessID == "" || Frm_tprc_Main.g_tBase.MachineID == "")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("공정 또는 호기가 선택되지 않았습니다.", "[저장 전 오류]", 0, 1);
                return false;
            }

            // 작업자 오류
            if (Frm_tprc_Main.g_tBase.PersonID == "")//수정필요
            {
                WizCommon.Popup.MyMessageBox.ShowBox("작업자가 선택되지 않았습니다.", "[저장 전 오류]", 0, 1);
                return false;
            }

            // 작업 수량 미입력
            if (Lib.GetDouble(txtWorkQty.Text) == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("작업수량이 입력되지 않았습니다.", "[저장 전 오류]", 0, 1);
                return false;
            }

            // 불량수량 > 작업수량
            if (ConvertDouble(txtWorkQty.Text) < ConvertDouble(txtDefectQty.Text))
            {
                WizCommon.Popup.MyMessageBox.ShowBox("불량수량이 작업수량보다 클 수 없습니다.", "[저장 전 오류]", 0, 1);
                return false;
            }

            // BOM - wk_resultArticleChild에 맞춰서, 
            // 하위품 생산가능량보다 작업수량이 클 경우 잔량이동 팝업 활성화
            if (CheckWorkQty() == false)
            {
                //WizCommon.Popup.MyMessageBox.ShowBox("하위품의 생산 가능량이 부족합니다.", "[저장 전 오류]", 0, 1);
                return false;
            }

            // 작업지시 잔량 초과 등록 시

            return flag;  
        }

        #endregion

        #region 2020.09 저장 구문 - SaveData

        private bool SaveData()
        {
            List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
            List<List<string>> ListProcedureName = new List<List<string>>();
            List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            try
            {
                // 저장할 객체 세팅
                List<frm_tprc_Work_U_CodeView> lstWork = setSaveWorkList("QtyPerBox");

                for (int i = 0; i < lstWork.Count; i++)
                {
                    #region 1. 라벨 생성 - LabelPrint
                    if (lstWork[i].LabelID.Equals("")
                        && lstWork[i].LabelPrintYN == true)
                    {
                        sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("LabelID", "");
                        sqlParameter.Add("LabelGubun", "7");
                        sqlParameter.Add("ProcessID", Frm_tprc_Main.g_tBase.ProcessID);
                        sqlParameter.Add("ArticleID", txtArticleID.Text);
                        sqlParameter.Add("PrintDate", mtb_To.Text.Replace("-", ""));

                        sqlParameter.Add("ReprintDate", "");
                        sqlParameter.Add("ReprintQty", 0);
                        sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                        sqlParameter.Add("InstDetSeq", ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq));
                        sqlParameter.Add("OrderID", m_OrderID);

                        sqlParameter.Add("PrintQty", 1);
                        sqlParameter.Add("LabelPrintQty", 1);
                        sqlParameter.Add("nQtyPerBox", ConvertDouble(txtWorkQty.Text));
                        sqlParameter.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

                        WizCommon.Procedure pro1 = new WizCommon.Procedure();
                        pro1.Name = "[xp_WizWork_iwkLabelPrint_C]";
                        pro1.OutputUseYN = "Y";
                        pro1.OutputName = "LabelID";
                        pro1.OutputLength = "20";

                        Prolist.Add(pro1);
                        ListParameter.Add(sqlParameter);
                    }
                    #endregion

                    #region 2. wk_Result

                    WizCommon.Procedure pro2 = new WizCommon.Procedure();

                    if (!lstWork[i].JobID.Equals(""))
                    {
                        sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("JobID", ConvertDouble(lstWork[i].JobID));
                        sqlParameter.Add("LabelID", lstWork[i].LabelID);
                        sqlParameter.Add("LabelGubun", "7");
                        sqlParameter.Add("WorkStartDate", mtb_From.Text.Replace("-", ""));
                        sqlParameter.Add("WorkStartTime", dtStartTime.Value.ToString("HHmmss"));
                        sqlParameter.Add("WorkEndDate", mtb_To.Text.Replace("-", ""));
                        sqlParameter.Add("WorkEndTime", dtEndTime.Value.ToString("HHmmss"));
                        sqlParameter.Add("WorkQty", lstWork[i].WorkQty);
                        sqlParameter.Add("CycleTime", ConvertDouble(txtNowCT.Text));
                        sqlParameter.Add("ProcessID", Frm_tprc_Main.g_tBase.ProcessID);
                        sqlParameter.Add("MachineID", Frm_tprc_Main.g_tBase.MachineID);
                        sqlParameter.Add("Comments", Frm_tprc_Main.g_tBase.ProcessID + "작업종료에 따른 데이터 저장(Upgrade)");

                        sqlParameter.Add("UpdateUserID", Frm_tprc_Main.g_tBase.PersonID);

                        //pro2.Name = "xp_wkResult_uWkResultOne";
                        pro2.Name = "xp_prdWork_uWkResultOne";//하이테크만 이렇게 사용 // 2020.02.21 둘리 : CycleTime 추가되도록 수정
                        pro2.OutputUseYN = "N";
                        pro2.OutputName = "JobID";
                        pro2.OutputLength = "20";

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter);
                    }
                    else
                    {
                        sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("JobID", 0);
                        sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                        sqlParameter.Add("InstDetSeq", ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq));
                        sqlParameter.Add("LabelID", "");
                        sqlParameter.Add("StartSaveLabelID", "");

                        sqlParameter.Add("LabelGubun", "7");
                        sqlParameter.Add("ProcessID", Frm_tprc_Main.g_tBase.ProcessID);
                        sqlParameter.Add("MachineID", Frm_tprc_Main.g_tBase.MachineID);
                        sqlParameter.Add("ScanDate", mtb_From.Text.Replace("-", "")); // 둘리 : 2020.05.27 : StartDate, StartTime 이랑 맞춤
                        sqlParameter.Add("ScanTime", dtStartTime.Value.ToString("HHmmss"));

                        sqlParameter.Add("ArticleID", txtArticleID.Text);
                        sqlParameter.Add("WorkQty", lstWork[i].WorkQty);
                        sqlParameter.Add("Comments", Frm_tprc_Main.g_tBase.ProcessID + "작업종료에 따른 데이터 저장(Insert)");
                        sqlParameter.Add("ReworkOldYN", "");
                        sqlParameter.Add("ReworkLinkProdID", "");

                        sqlParameter.Add("WorkStartDate", mtb_From.Text.Replace("-", ""));
                        sqlParameter.Add("WorkStartTime", dtStartTime.Value.ToString("HHmmss"));
                        sqlParameter.Add("WorkEndDate", mtb_To.Text.Replace("-", ""));
                        sqlParameter.Add("WorkEndTime", dtEndTime.Value.ToString("HHmmss"));
                        sqlParameter.Add("JobGbn", "1");

                        sqlParameter.Add("NoReworkCode", "");
                        sqlParameter.Add("WDNO", "");
                        sqlParameter.Add("WDID", "");
                        sqlParameter.Add("WDQty", 0);
                        sqlParameter.Add("LogID", 0);

                        sqlParameter.Add("s4MID", "");
                        sqlParameter.Add("DayOrNightID", Frm_tprc_Main.g_tBase.DayOrNightID);
                        sqlParameter.Add("CycleTime", ConvertDouble(txtNowCT.Text));
                        sqlParameter.Add("FirstJobID", ConvertDouble(updateJobID));
                        sqlParameter.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

                        pro2.Name = "xp_prdWork_iWkResultSecond";
                        pro2.OutputUseYN = "Y";
                        pro2.OutputName = "JobID";
                        pro2.OutputLength = "20";

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter);
                    }
                    #endregion

                    #region 3. wk_Result_ArticleChild

                    int nRow = GridData2.Rows.Count;
                    for (int k = 0; k < nRow; k++)
                    {
                        sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("JobID", ConvertDouble(lstWork[i].JobID));

                        sqlParameter.Add("ChildLabelID", Lib.CheckNull(GridData2.Rows[k].Cells["BarCode"].Value.ToString()));//
                        sqlParameter.Add("ChildLabelGubun", Lib.CheckNull(GridData2.Rows[k].Cells["LabelGubun"].Value.ToString()));
                        sqlParameter.Add("ChildArticleID", Lib.CheckNull(GridData2.Rows[k].Cells["ChildArticleID"].Value.ToString()));
                        sqlParameter.Add("ReworkOldYN", "");
                        sqlParameter.Add("ReworkLinkChildProdID", "");

                        sqlParameter.Add("Seq", (k + 1));
                        sqlParameter.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

                        WizCommon.Procedure pro3 = new WizCommon.Procedure();
                        pro3.Name = "xp_prdWork_iWkResultArticleChild";
                        pro3.OutputUseYN = "N";
                        pro3.OutputName = "JobID";
                        pro3.OutputLength = "20";

                        Prolist.Add(pro3);
                        ListParameter.Add(sqlParameter);
                    }
                    #endregion

                    #region 4. wk_Result_Defect → 처음거에 넣기.

                    if (i == 0)
                    {
                        foreach (string Key in dicDefect.Keys)
                        {
                            var Defect = dicDefect[Key] as frm_tprc_Work_Defect_U_CodeView;
                            if (Defect != null)
                            {
                                Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();

                                sqlParameter4.Add("DefectID", Key.Trim());
                                sqlParameter4.Add("DefectQty", ConvertDouble(Defect.DefectQty));
                                sqlParameter4.Add("XPos", ConvertInt(Defect.XPos));
                                sqlParameter4.Add("YPos", ConvertInt(Defect.YPos));
                                sqlParameter4.Add("JobID", ConvertDouble(lstWork[i].JobID));
                                sqlParameter4.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);

                                WizCommon.Procedure pro4 = new WizCommon.Procedure();
                                pro4.Name = "xp_prdWork_iWorkDefect";
                                pro4.OutputUseYN = "N";
                                pro4.OutputName = "JobID";
                                pro4.OutputLength = "20";

                                Prolist.Add(pro4);
                                ListParameter.Add(sqlParameter4);
                            }
                        }
                    }
                    #endregion

                    // 5. 입출고

                    sqlParameter = new Dictionary<string, object>();

                    sqlParameter.Add("JobID", ConvertDouble(lstWork[i].JobID));
                    sqlParameter.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);
                    sqlParameter.Add("sRtnMsg", "");

                    WizCommon.Procedure pro6 = new WizCommon.Procedure();
                    pro6.Name = "xp_prdWork_iWkResultStuffInOut";
                    pro6.OutputUseYN = "N";
                    pro6.OutputName = "JobID";
                    pro6.OutputLength = "20";

                    Prolist.Add(pro6);
                    ListParameter.Add(sqlParameter);
                }

                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    lstPrintLabel.Clear();
                    for (int i = 0; i < list_Result.Count; i++)
                    {
                        KeyValue kv = list_Result[i];
                        if (kv.key == "LabelID")
                        {
                            lstPrintLabel.Add(kv.value);
                        }
                    }
                    return true;
                }
                else
                {
                    foreach (KeyValue kv in list_Result)
                    {                    list_Result.RemoveAt(0);

                        if (kv.key.ToLower() == "failure")
                        {
                            throw new Exception(kv.value.ToString());
                        }
                    }
                    return false;
                }
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<SaveData>\r\n{0}", ex.Message), "[오류]", 0, 1);
                return false;
            }
        }

        #endregion

        #region 2020.09 저장 객체 세팅 - setSaveWorkList

        /// <summary>
        ///  1. QtyPerBox : 생산박스수량 세팅 / 2. SaveSelection : 선택 세팅 / 3. 나머지 : 일반 세팅
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private List<frm_tprc_Work_U_CodeView> setSaveWorkList(string flag)
        {
            List<frm_tprc_Work_U_CodeView> lstWork = new List<frm_tprc_Work_U_CodeView>();

            #region 1. 박스당 수량 등록
            if (flag.Equals("QtyPerBox"))
            {
                double WorkQty = ConvertDouble(txtWorkQty.Text) - ConvertDouble(txtDefectQty.Text);
                double QtyPerBox = ConvertDouble(txtProdQtyPerBox.Text) == 0 ? WorkQty : ConvertDouble(txtProdQtyPerBox.Text);

                int Cnt = (int)Math.Ceiling(WorkQty / QtyPerBox);

                for (int i = 0; i < Cnt; i++)
                {
                    frm_tprc_Work_U_CodeView Work = new frm_tprc_Work_U_CodeView()
                    {
                        JobID = i == 0 ? updateJobID : "",
                        // W-Qty = 4500 / QtyPerBox 1500 -> 마지막 나머지 값이 0 이므로, 이 부분 제외 2020.11.16
                        WorkQty = i == Cnt - 1 && (WorkQty % QtyPerBox) != 0 ? (WorkQty % QtyPerBox) : QtyPerBox,
                        LabelID = "",
                        LabelPrintYN = this.LabelPrintYN.Equals("Y") ? true : false
                    };

                    lstWork.Add(Work);
                }
            }
            #endregion
            #region 2. 선택 저장인 경우 - 첫번째 공정만.
            else if ( ConvertInt(Frm_tprc_Main.g_tBase.sInstDetSeq) == 1
                && SaveSelectionLabelInfo.LabelID != null
                && !SaveSelectionLabelInfo.LabelID.Equals(""))
            {
                // 선택 라벨 저장
                // 1. 박스당 수량만 채워넣고 나머지는 박스당 수량으로 나눠서 라벨 신규 발행.
                // 2. 작업수량을 그대로 선택한 라벨에 저장
                if (SaveSelectionLabelInfo.SaveQtyPerBoxYN == true
                    && SaveSelectionLabelInfo.ProdQtyPerBox != null
                    && ConvertDouble(SaveSelectionLabelInfo.ProdQtyPerBox) != 0)
                {
                    double WorkQty = ConvertDouble(txtWorkQty.Text) - ConvertDouble(txtDefectQty.Text);
                    double QtyPerBox = ConvertDouble(SaveSelectionLabelInfo.ProdQtyPerBox);

                    // 선택 라벨 재고
                    double NowLoc = ConvertDouble(SaveSelectionLabelInfo.NowLoc);

                    double Remainder = QtyPerBox - NowLoc;
                    if (Remainder > 0)
                    {
                        if (Remainder < WorkQty)
                        {
                            // 1. 남은 수량만큼 채워 넣고 
                            frm_tprc_Work_U_CodeView Work = new frm_tprc_Work_U_CodeView()
                            {
                                JobID = updateJobID,
                                WorkQty = Remainder,
                                LabelID = SaveSelectionLabelInfo.LabelID,
                                LabelPrintYN = false
                            };

                            lstWork.Add(Work);

                            // 2. 채워넣은 나머지로 박스당 수량만큼 등록
                            WorkQty = WorkQty - Remainder;
                            int Cnt = (int)Math.Ceiling(WorkQty / QtyPerBox);

                            for (int i = 0; i < Cnt; i++)
                            {
                                frm_tprc_Work_U_CodeView WorkR = new frm_tprc_Work_U_CodeView()
                                {
                                    JobID = "",
                                    WorkQty = i == Cnt - 1 ? (WorkQty % QtyPerBox) : QtyPerBox,
                                    LabelID = "",
                                    LabelPrintYN = this.LabelPrintYN.Equals("Y") ? true : false
                                };

                                lstWork.Add(WorkR);
                            }
                        }
                        else // WorkQty < Remainder 라면, 그냥 선택 라벨에 저장
                        {
                            frm_tprc_Work_U_CodeView Work = new frm_tprc_Work_U_CodeView()
                            {
                                JobID = updateJobID,
                                WorkQty = WorkQty,
                                LabelID = SaveSelectionLabelInfo.LabelID,
                                LabelPrintYN = false
                            };

                            lstWork.Add(Work);
                        }
                    }
                    else // Remainder가 - 라면, 해당 라벨(선택한라벨)이 박스당 수량을 초과했음 → 새로운 라벨을 발행 해야 됨(박스당 수량만큼) 
                    {
                        int Cnt = (int)Math.Ceiling(WorkQty / QtyPerBox);

                        for (int i = 0; i < Cnt; i++)
                        {
                            frm_tprc_Work_U_CodeView Work = new frm_tprc_Work_U_CodeView()
                            {
                                JobID = i == 0 ? updateJobID : "",
                                WorkQty = i == Cnt - 1 ? (WorkQty % QtyPerBox) : QtyPerBox,
                                LabelID = "",
                                LabelPrintYN = this.LabelPrintYN.Equals("Y") ? true : false
                            };

                            lstWork.Add(Work);
                        }
                    }
                }
                else // 2. 작업 수량 그대로 선택 라벨에 저장.
                {
                    frm_tprc_Work_U_CodeView Work = new frm_tprc_Work_U_CodeView()
                    {
                        JobID = updateJobID,
                        WorkQty = ConvertDouble(txtWorkQty.Text) - ConvertDouble(txtDefectQty.Text),
                        LabelID = SaveSelectionLabelInfo.LabelID,
                        LabelPrintYN = false
                    };

                    lstWork.Add(Work);
                }
            }
            #endregion
            else // 일반적인 저장.
            {
                frm_tprc_Work_U_CodeView Work = new frm_tprc_Work_U_CodeView()
                {
                    JobID = updateJobID,
                    WorkQty = ConvertDouble(txtWorkQty.Text) - ConvertDouble(txtDefectQty.Text),
                    LabelID = this.LabelPrintYN.Equals("Y") ? "" : lstStartLabel[0].ToString(),
                    LabelPrintYN = this.LabelPrintYN.Equals("Y") ? true : false
                };

                lstWork.Add(Work);
            }

            return lstWork;
        }

        #endregion

        #region 2020.09 인쇄 - PrintWork

        private void PrintWork(List<string> lstLabelID)
        {
            string g_sPrinterName = Lib.GetDefaultPrinter();
            try
            {
                IsTagID = Frm_tprc_Main.g_tBase.TagID;
                List<string> list_Data = null;
                //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //sqlParameter.Add("InstID", list_TWkLabelPrint[0].sInstID);
                //DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_WorkCard_sWorkCard]", sqlParameter, false);

                for (int i = 0; i < lstLabelID.Count; i++)
                {
                    list_Data = new List<string>();
                    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                    sqlParameter2.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                    sqlParameter2.Add("CardID", lstLabelID[i]);

                    DataTable dt2 = DataStore.Instance.ProcedureToDataTable("[xp_WorkCard_sWorkCardPrint]", sqlParameter2, false);
                    lData = new List<string>();
                    string strProcessID = "";

                    // 데이터 셋팅
                    double douworkqty = 0;
                    double doudefectqty = 0;

                    string DayNightName = string.Empty;
                    string ArticleSub = string.Empty;

                    foreach (DataRow dr in dt2.Rows)
                    {
                        strProcessID = dr["ProcessID"].ToString();
                        if (strProcessID == m_ProcessID)
                        {
                            if (dr["DayOrNightID"].ToString().Trim().Equals("01"))
                            {
                                DayNightName = "주간";
                            }
                            else if (dr["DayOrNightID"].ToString().Trim().Equals("02"))
                            {
                                DayNightName = "야간";
                            }
                            double.TryParse(dr["WorkQty"].ToString(), out douworkqty);
                            double.TryParse(dr["wk_defectQty"].ToString(), out doudefectqty);

                            list_Data.Add(Lib.CheckNull(dr["wk_CardID"].ToString())); //라벨번호(공정전표)

                            list_Data.Add(Lib.CheckNull(dr["Model"].ToString()));// 차종
                            list_Data.Add(Lib.CheckNull(dr["Process"].ToString()));// 공정명
                            list_Data.Add(Lib.CheckNull(dr["BuyerArticleNo"].ToString()));// 품번
                            list_Data.Add(Lib.CheckNull(dr["Article"].ToString())); // 품명
                                                                                    //list_Data.Add(Lib.CheckNull(dr["DayOrNightID"].ToString())); // 주/야간
                            list_Data.Add(Lib.CheckNull(DayNightName));
                            list_Data.Add(Lib.CheckNull(dr["wk_Name"].ToString()));// 작업자
                            list_Data.Add((string.Format("{0:n0}", (int)douworkqty)));// _수량
                            list_Data.Add(Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, Lib.CheckNull(dr["wk_ResultDate"].ToString())));//D_생산일자
                                                                                                                                      //list_Data.Add((string.Format("{0:n0}", (int)doudefectqty)));// _불량수량

                            #region 서강정밀 Zebra Printer 사용
                            //#region 라벨에 주/야간 출력(서강정밀)

                            //if (dr["Article"].ToString().Length > 12)     // 품명이 12글자 이상일 경우 12글자까지만 출력
                            //{
                            //    ArticleSub = dr["Article"].ToString().Substring(0, 12);
                            //}
                            //#endregion

                            //#region 제브라 ZT230 프린터 라벨 소스 (2020.11.03.서강정밀)
                            ////Zebra Printer 인쇄 라벨 디자인 2020.10.23.KGH
                            //string zZPL = " ^XA\n";
                            //zZPL += "^SEE:UHANGUL.DAT^FS\n";               // 요 두줄을 날려줘야 한글이 출력
                            //zZPL += "^CW1,E:KFONT3.FNT^CI26^FS\n";

                            //zZPL += "^LH3,3\n";
                            //zZPL += "^FO10,20^GB712,390,3^FS\n";           // 가장 바깥 테두리
                            //zZPL += "^FO10,20^^FS\n";
                            //zZPL += "^FO20,30^A1N35,35^FD생 산 전 표^FS\n";// T 좌측상단 텍스트
                            //zZPL += "^FO600,30^A1N35,35^FD출하동^FS\n";    // T 우측상단 텍스트
                            ////zZPL += "^FO125,335^BY2,3,60^B3 ,,,, ^FD"+Lib.CheckNull(dr["wk_CardID"].ToString())+"^FS\n";
                            //zZPL += "^FO125,315^BY3^BCN,60,Y,N,N^FD" + Lib.CheckNull(dr["wk_CardID"].ToString()) + "^FS\n";   // D 바코드
                            //zZPL += "^PRD\n";                              // 프린트 속도 152.4mm/Sec
                            //zZPL += "^FO20,65^GB693,60,3^FS\n";            // 세로 1번째 칸
                            //zZPL += "^FO20,65^^FS\n";
                            //zZPL += "^FO20,65^GB693,120,3^FS\n";           // 세로 2번째 칸
                            //zZPL += "^FO20,65^^FS\n";
                            //zZPL += "^FO20,65^GB693,180,3^FS\n";           // 세로 3번째 칸
                            //zZPL += "^FO20,65^^FS\n";
                            //zZPL += "^FO20,65^GB693,240,3^FS\n";           // 세로 4번째 칸
                            //zZPL += "^FO20,65^^FS\n";
                            //zZPL += "^FO25,80^A1N35,35^FD차 종^FS\n";      // T 세로 1번째 텍스트
                            //zZPL += "^FO25,140^A1N35,35^FD품 번^FS\n";     // T 세로 2번째 텍스트
                            //zZPL += "^FO25,200^A1N35,35^FD품 명^FS\n";     // T 세로 3번째 텍스트
                            //zZPL += "^FO25,260^A1N35,35^FD수 량^FS\n";     // T 세로 4번째 텍스트
                            //zZPL += "^FO130,80^A1N35,35^FD" + Lib.CheckNull(dr["Model"].ToString()) + "^FS\n"; // D 텍스트 1 내용 텍스트 (차종)
                            //zZPL += "^FO130,140^A1N35,35^FD" + Lib.CheckNull(dr["BuyerArticleNo"].ToString()) + "^FS\n"; // D 텍스트 2 내용 텍스트 (품번)
                            //zZPL += "^FO130,200^A1N35,35^FD" + ArticleSub + "..." + "^FS\n"; // D 텍스트 3 내용 텍스트(품명)
                            //zZPL += "^FO130,260^A1N35,35^FD" + string.Format("{0:n0}", (int)douworkqty) + "^FS\n"; // D 텍스트 4 내용 텍스트 (수량)
                            //zZPL += "^FO20,65^GB100,240,3^FS\n";           // 차종, 품번, 품명, 수량 칸
                            //zZPL += "^FO20,65^^FS\n";
                            //zZPL += "^FO310,65^GB149,60,3^FS\n";           // 작업장(공정) 칸
                            //zZPL += "^FO310,65^^FS\n";
                            //zZPL += "^FO335,80^A1N35,35^FD공 정^FS\n";    // T 작업장(공정) 칸 텍스트
                            //zZPL += "^FO470,80^A1N35,35^FD" + Lib.CheckNull(dr["Process"].ToString()) + "^FS\n";    // D 작업장 내용 텍스트(공정명)
                            //zZPL += "^FO548,123^GB165,122,2^FS\n";         // 주/야간 칸, 합격자 성명 칸
                            //zZPL += "^FO585,123^^FS\n";
                            //zZPL += "^FO560,140^A1N35,35^FD" + DayNightName + "^FS\n";   // D 주/야간 텍스트  
                            //zZPL += "^FO455,183^GB95,62,2^FS\n";          // 합격 칸
                            //zZPL += "^FO455,183^^FS\n";
                            //zZPL += "^FO465,200^A1N35,35^FD합격^FS\n";    // T 합격 칸 텍스트 
                            //zZPL += "^FO560,200^A1N35,35^FD" + Lib.CheckNull(dr["wk_Name"].ToString()) + "^FS\n";  // D 합격 칸 내용 텍스트
                            //zZPL += "^FO310,242^GB157,63,2^FS\n";          // 생산일자 칸
                            //zZPL += "^FO310,242^^FS\n";
                            //zZPL += "^FO320,260^A1N35,35^FD생산일자^FS\n"; // T 생산일자 텍스트
                            ////zZPL += "^FO500,277^AE30,30^FD"+Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, Lib.CheckNull(dr["wk_ResultDate"].ToString().Trim()))+"^FS\n"; // D 날짜 텍스트
                            //zZPL += "^FO480,260^AE35,35^FD" + ConvertDateTime(Lib.CheckNull(dr["wk_ResultDate"].ToString().Trim())) + "^FS\n"; // D 날짜 텍스트
                            //zZPL += "^FO20,65^GB693,240,3^FS\n";           // 안쪽 바깥 테두리
                            //zZPL += "^XZ";

                            //// Zebra Printer 연결
                            //RawPrinterHelper.SendStringToPrinter("Zebra ZM400 (203 dpi) - ZPL", zZPL); // 프린터 이름으로 연결(ZDesigner ZT230-200dpi ZPL), Zebra ZM400 (203 dpi) - ZPL, Zebra ZT230 (203 dpi)
                            //#endregion
                            #endregion

                        }

                        if (strProcessID != m_ProcessID && list_Data.Count > 7)
                        {
                            list_Data.Add(Lib.CheckNull(dr["Process"].ToString())); // 다음(순차) 공정의  품명
                        }
                    }

                    g_sPrinterName = Lib.GetDefaultPrinter();
                    TSCLIB_DLL.openport(g_sPrinterName);
                    if (SendWindowDllCommand(list_Data, IsTagID, 1, 0))
                    {
                        Message[0] = "[라벨발행중]";
                        Message[1] = "라벨 발행중입니다. 잠시만 기다려주세요.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                    }
                    else
                    {
                        Message[0] = "[라벨발행 실패]";
                        Message[1] = "라벨 발행에 실패했습니다. 관리자에게 문의하여주세요.\r\n<SendWindowDllCommand>";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                    }
                    TSCLIB_DLL.clearbuffer();
                    TSCLIB_DLL.closeport();
                }
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류!관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
        }

        #endregion

        private DateTime ConvertDateTime(string str)
        {
            DateTime result = new DateTime();
            str = str.Replace("/", "").Replace(".", "").Replace("-", "").Trim();

            if (str.Length == 8)
            {
                str = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
            }

            if (DateTime.TryParse(str, out result) == false)
            {
                result = DateTime.Today;
                //result = Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, Lib.CheckNull(dr["wk_ResultDate"].ToString().Trim()));
            }
            return result;
        }

        #region 2020.09 저장버튼 클릭 - cmdSave_Click

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 저장전 체크
                if (CheckData() == false)
                {
                    return;
                }

                // BOM - wk_resultArticleChild에 맞춰서, 
                // 하위품 생산가능량보다 작업수량이 클 경우 잔량이동 팝업 활성화
                if (CheckWorkQty() == false)
                {
                    return;
                }

                Cursor = Cursors.WaitCursor;

                //'-------------------------------------------------------------------------------
                //'생산실적  저장
                //'-------------------------------------------------------------------------------

                if (SaveData())
                {
                    if (lstPrintLabel.Count > 0)
                    {
                        PrintWork(lstPrintLabel);
                    }
                    else
                    {
                        Message[0] = "[저장 완료]";
                        Message[1] = "저장이 완료되었습니다.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 3, 1);
                    }

                    SetFormDataClear();
                    cmdExit_Click(null, null);  // 나가.
                }
                else
                {
                    throw new Exception();
                }

                //'    '-----------------------------------------------------------------------------------------
                //     '저장된 결과 재 조회
                //'    '-----------------------------------------------------------------------------------------
                //FillGridData1();
                Cursor = Cursors.Default;
            }

            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<cmdSave>\r\n{0}", excpt.Message), "[오류]", 0, 1);
                Cursor = Cursors.Default;
            }
        }

        #endregion

        // 코드뷰
        class frm_tprc_Work_U_CodeView
        {
            public string JobID { get; set; }
            public double WorkQty { get; set; }
            public string LabelID { get; set; } // 라벨이 있으면 그거 쓰고, 없으면 생성!
            public bool LabelPrintYN { get; set; }
        }

    }
}