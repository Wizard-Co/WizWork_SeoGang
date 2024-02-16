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
    public partial class Frm_PopUpSel : Form
    {
        DataSet ds = null;

        public static int _Split { get; set; } //조회 구분자 Work_U
        public static string Set_sProcessID  { get; set; }   //공정명
        public static string Set_sMachine  { get; set; }     //설비명

        int Split = 0;
        string sProcessID = string.Empty;
        string sMachineName = string.Empty;
        string ArticleID = string.Empty;

        string sMachine = string.Empty;         //현재 폼에서 선택한 설비명
        string sMachineID = string.Empty;       //현재 폼에서 선택한 설비,호기ID
        string sPersonName = string.Empty;      //현재 폼에서 선택한 작업자Name
        string sPersonID = string.Empty;        //현재 폼에서 선택된 작업자ID
        string sInstID = string.Empty;
        string sArticleID = string.Empty;
        
        /// <summary>
        /// 폼간 데이터전달을 위한 소스
        /// </summary>
        /// <param name="text"></param> 
        ///  

        public delegate void TextEventHandler();// string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다.


        //WizINs용
        WizWorkLib Lib = new WizWorkLib();
        List<CB_IDNAME> list_cbx = new List<CB_IDNAME>();
        public Frm_PopUpSel()
        {
            InitializeComponent();
        }

        private void SetScreen()
        {
            tlpMain.Dock = DockStyle.Fill;
            foreach (Control control in tlpMain.Controls)
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
                        
                    }
                }
            }
            SetComboBox();
        }

        //그리드 컬럼 셋팅
        private void InitGrid()
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 3;

            int i = 0;

            grdData.Columns[i].Name = "RowSeq";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "PersonID";
            grdData.Columns[i].HeaderText = "코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Name";
            grdData.Columns[i].HeaderText = "검사자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;


            grdData.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdData.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdData.AlternatingRowsDefaultCellStyle.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        //그리드에 콤보박스 인덱스(부서)에 따른 사용자 세팅
        private void FillGrid()
        {
            //string DepartID = Lib.FindComboBoxID(cboDepart, list_cbx);
            //DB에서 사용자 불러와서 DT로 넣음
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            //sqlParameter.Add("@DepartID", DepartID);
            sqlParameter.Add("@ChkPersonID", "0");
            sqlParameter.Add("@PersonID", "");
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizTerm_sPerson", sqlParameter, false);

            grdData.DataSource = dt;
            int i = 1;
            foreach (DataGridViewRow dgvr in grdData.Rows)
            {
                dgvr.Cells["RowSeq"].Value = i.ToString();
                i++;
            }
        }



        //콤보박스에 Depart 부서 셋팅
        private void SetComboBox()
        {
            try
            {
                //DataTable dt = Lib.GetCode(WizWorkLib.CodeTypeClss.CD_DEPART);

                //컬럼명 변경 ID와, NAME으로
                //foreach (DataColumn dc in dt.Columns)
                //{
                //    if (dc.ColumnName.ToUpper().Contains("ID"))
                //    {
                //        dc.ColumnName = "ID";
                //    }
                //    else //if (dc.ColumnName.ToUpper().Contains("NAME"))
                //    {
                //        dc.ColumnName = "NAME";
                //    }
                //}

                //패널크기에 따른 콤보박스 사이즈 및 폰트사이즈 변경
                //Lib.SetComboBox(cboDepart, dt, list_cbx);
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
            finally
            {

                ////전역변수에 Depart가 없을 시 0번째 Index 선택
                //if (Frm_tprc_Main.g_tBase.DepartID == "")
                //{
                //    cboDepart.SelectedIndex = 0;
                //}
                ////전역변수에 Depart가 있을 시 해당 값을 가진 Index 선택
                //else
                //{
                //    //cboDepart.SelectedIndex = Convert.ToInt32(Lib.FindComboBoxIdx(cboDepart, list_cbx, Frm_tins_Main.g_tBase.DepartID));
                //}
            }

        }
        public Frm_PopUpSel(int intSplit, string strInstID)
        {
            InitializeComponent();
            this.Split = intSplit;
            if (this.Split == 5)
            {
                this.ArticleID = strInstID;
            }
            else
            {
                this.sInstID = strInstID;
            }
            
        }

        public Frm_PopUpSel(int intSplit, string strProcessID, string strMachineID)
        {
            InitializeComponent();
            this.Split = intSplit;
            this.sProcessID = strProcessID;
            this.sMachineID = strMachineID;
        }

        public Frm_PopUpSel(int intSplit, string strProcessID, string strMachineID, string strInstID)
        {
            InitializeComponent();
            this.Split = intSplit;
            this.sProcessID = strProcessID;
            this.sMachineID = strMachineID;
            this.sInstID = strInstID;
        }

        private void SetupDgv()
        {
            int j = 0;
            grdData.Columns.Clear();
            btnUp.Visible = true;
            btnDown.Visible = true;
            if (Split < 3 || Split == 6)
            {
                grdData.Location = new Point(0, 2);
                grdData.Size = new Size(384, 276);
            }
            else if (Split == 5)
            {
                grdData.Location = new Point(0, 2);
            }
            
            switch (Split)
            {
                
                case 0://설비 선택
                    this.Text = "설비 선택";        
                    grdData.ColumnCount = 1;
                    grdData.Columns[0].Name = "Machine";
                    grdData.Columns[0].HeaderText = "설비명";
                    ProcMachine_Q();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            grdData.Rows.Add(dr["Machine"].ToString());
                        }
                    }
                    
                    break;
                case 1://호기 선택
                    this.Text = "호기 선택";
                    grdData.ColumnCount = 2;
                    grdData.Columns[0].Name = "MachineNO";
                    grdData.Columns[1].Name = "MachineID";
                    grdData.Columns[0].HeaderText = "호기";
                    grdData.Columns[1].HeaderText = "MachineID";
                    grdData.Columns[1].Visible = false;
                    ProcMachineByName();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            grdData.Rows.Add(dr["MachineNo"].ToString(),
                                              dr["MachineID"].ToString());
                        }
                    }
                    break;
                case 2://작업자 선택
                    this.Text = "작업자 선택";
                    grdData.ColumnCount = 2;
                    grdData.Columns[0].Name = "Name";
                    grdData.Columns[1].Name = "PersonID";
                    grdData.Columns[0].HeaderText = "이름";
                    grdData.Columns[1].HeaderText = "PersonID";
                    grdData.Columns[1].Visible = false;
                    ProcPerson();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            grdData.Rows.Add(dr["NAME"].ToString(),
                                              dr["PERSONID"].ToString());
                        }
                    }
                    break;
                case 3://날짜별 지시번호or오더번호 조회
                    this.Text = "지시번호 선택";
                    //pnlDate.Visible = true;
                    //chkResultDate.Checked = true;
                    grdData.ColumnCount = 3;

                    
                    // Set the Colums Hearder Names

                    grdData.Columns[j].Name = "InstID";
                    grdData.Columns[j].HeaderText = "지시번호";
                    grdData.Columns[j].Width = 60;
                    grdData.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                    grdData.Columns[j].ReadOnly = true;
                    grdData.Columns[j].Visible = true;

                    grdData.Columns[++j].Name = "Article";
                    grdData.Columns[j].HeaderText = "재종";
                    grdData.Columns[j].Width = 60;
                    grdData.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                    grdData.Columns[j].ReadOnly = true;
                    grdData.Columns[j].Visible = true;

                    grdData.Columns[++j].Name = "ArticleID";
                    grdData.Columns[j].HeaderText = "ArticleID";
                    grdData.Columns[j].Width = 60;
                    grdData.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                    grdData.Columns[j].ReadOnly = true;
                    grdData.Columns[j].Visible = false;
                    foreach (DataGridViewColumn col in grdData.Columns)
                    {
                        col.DataPropertyName = col.Name;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    //return;
                    ProcInstID();
                    break;
                case 4:
                    this.Text = "압출노즐Size 선택";
                    //pnlDate.Visible = true;
                    //chkResultDate.Checked = true;

                    grdData.Columns[j].Name = "AcNozzleSize";
                    grdData.Columns[j].HeaderText = "압출노즐Size";
                    grdData.Columns[j].Width = 60;
                    grdData.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                    grdData.Columns[j].ReadOnly = true;
                    grdData.Columns[j].Visible = true;

                    grdData.Columns[++j].Name = "ChildInputQty";
                    grdData.Columns[j].HeaderText = "생산수량";
                    grdData.Columns[j].Width = 60;
                    grdData.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                    grdData.Columns[j].ReadOnly = true;
                    grdData.Columns[j].Visible = false;
                    foreach (DataGridViewColumn col in grdData.Columns)
                    {
                        col.DataPropertyName = col.Name;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    //return;
                    //procACNozzle();
                    break;
                case 5://GP Lot No 선택
                    this.Text = "GP Lot No 선택";
                    grdData.ColumnCount = 1;
                    grdData.Columns[j].Name = "InstID";
                    grdData.Columns[j].HeaderText = "GPLotNo";
                    
                    foreach (DataGridViewColumn col in grdData.Columns)
                    {
                        col.DataPropertyName = col.Name;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    ProcGPLot_Q();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            //dgvPopup.Rows.Add(dr["InstID"].ToString());
                            grdData.Rows.Add(dr["LotID"].ToString());
                        }
                    }
                    break;
                case 6://Machine 검색
                    this.Text = "설비 선택";
                    grdData.ColumnCount = 2;

                    grdData.Columns[j].Name = "Machine";
                    grdData.Columns[j].HeaderText = "설비 선택";
                    grdData.Columns[j].Width = 60;
                    grdData.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                    grdData.Columns[j].ReadOnly = true;
                    grdData.Columns[j].Visible = true;

                    grdData.Columns[++j].Name = "MachineID";
                    grdData.Columns[j].HeaderText = "MachineID";
                    grdData.Columns[j].Width = 60;
                    grdData.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                    grdData.Columns[j].ReadOnly = true;
                    grdData.Columns[j].Visible = false;
                    foreach (DataGridViewColumn col in grdData.Columns)
                    {
                        col.DataPropertyName = col.Name;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    ProcMachine_Q2();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            grdData.Rows.Add(dr["MachineNameNo"].ToString(), dr["MachineID"].ToString());
                        }
                    }
                    break;
            }
            if (grdData.Rows.Count > 0)
            { grdData.Rows[0].Selected = true; }
            
        }
        private void ProcMachine_Q()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("ProcessID", sProcessID);
                ds = DataStore.Instance.ProcedureToDataSet("xp_Process_sMachineMachine", sqlParameter, false) as DataSet;
            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }
        }

        private void ProcMachine_Q2()//
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PROCESSID", sProcessID);
                ds = DataStore.Instance.ProcedureToDataSet("[xp_Process_sMachine]", sqlParameter, false) as DataSet;
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

        //생산된 GP Lot를 조회, 잔량이 0이상인 놈들만 조회
        private void ProcGPLot_Q()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //sqlParameter.Add("ArticleID", ArticleID);
                //ds = DataStore.Instance.ProcedureToDataSet("[xp_wkResult_sGPLotQtyCheck]", sqlParameter, false) as DataSet;
                
                sqlParameter.Add("@sDate", DateTime.Now.AddDays(1).ToString("yyyyMMdd"));
                sqlParameter.Add("@sArticleID", ArticleID);

                ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_sGPLot]", sqlParameter, false) as DataSet;
                
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

        private void ProcMachineByName()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PROCESSID", sProcessID);
                sqlParameter.Add("MACHINE", sMachineID);
                ds = DataStore.Instance.ProcedureToDataSet("xp_Process_sMachineByName", sqlParameter, false) as DataSet;
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
         
        private void ProcPerson()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("CHKPROCESS", "1");//1넣으면 공정값 넣어줘야함
                sqlParameter.Add("PROCESSID", sProcessID);
                //sqlParameter.Add(Person_sPersonByProcess.CHKTEAM, "0");xp_Person_sPersonByProcess프로시저에서 teamid 체크추가시 사용
                //sqlParameter.Add(Person_sPersonByProcess.TEAMID, Team);xp_Person_sPersonByProcess프로시저에서 teamid 체크추가시 사용
                ds = DataStore.Instance.ProcedureToDataSet("xp_Person_sPersonByProcess", sqlParameter, false) as DataSet;//공정별 사용자 선택 시 null 대신 sqlParameter 추가
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
        //날짜별 오더 or 지시 조회
        public void ProcInstID()
        {
            grdData.Rows.Clear();

            //string strErr = "";

            //DataSet ds = null;
            //DataGridViewRow row = null;

           

            //int intnchkInstDate = 0;
            //string strStartDate = "";
            //string strEndDate = "";
            //string strOrderID = "";
            //string strInstID = "";

            //int intnChkProcessID = 0;
            //string strProcessID = "";
            //int intnChkMachineID = 0;
            //string strMachineID = "";

            //int intnChkLotID = 0;
            //string strLotID = "";

            //if (this.Owner is Frm_tprc_Work_U_KR || this.Owner is Frm_tprc_Work_U_AC)//지시일, 공정ID, 머신ID로 검색
            //{
            //    if (this.chkResultDate.Checked == false) //지시일 체크 : N
            //    {
            //        intnchkInstDate = 0;
            //        strStartDate = "";
            //        strEndDate = "";
            //    }
            //    else //지시일 체크 : Y
            //    {
            //        if ((dtpOrderDateTo.Value - dtpOrderDateFrom.Value).TotalDays < 0)
            //        {
            //            return;
            //        }
            //        intnchkInstDate = 1;
            //        strStartDate = dtpOrderDateFrom.Value.ToString("yyyyMMdd");
            //        strEndDate = dtpOrderDateTo.Value.ToString("yyyyMMdd");
            //    }
            //    strOrderID = "";
            //    strInstID = ""; //지시번호
            //    if(sProcessID != string.Empty)
            //    {
            //        intnChkProcessID = 1;
            //        strProcessID = sProcessID;
            //    }
            //    //if(sMachineID != string.Empty)
            //    //{
            //    //    intnChkMachineID = 1;
            //    //    strMachineID = sMachineID;
            //    //}   
            //}
            
            //try
            //{
                

            //    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            //    sqlParameter.Add("nchkInstDate", intnchkInstDate);
            //    sqlParameter.Add("FromDate", strStartDate);
            //    sqlParameter.Add("ToDate", strEndDate);
            //    sqlParameter.Add("OrderID", strOrderID);
            //    sqlParameter.Add("InstID", strInstID);
            //    sqlParameter.Add("nChkProcessID", intnChkProcessID);
            //    sqlParameter.Add("ProcessID", strProcessID);
            //    sqlParameter.Add("nChkMachineID", intnChkMachineID);
            //    sqlParameter.Add("MachineID", strMachineID);
            //    sqlParameter.Add("nChkLotID", intnChkLotID);
            //    sqlParameter.Add("LotID", strLotID);

            //    ds = DataStore.Instance.ProcedureToDataSet("xp_plInputDet_splInputDet", sqlParameter, false) as DataSet;


            //    if (ds != null && ds.Tables.Count > 0)
            //    {
            //        DataRow dr = null;

            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //            dr = ds.Tables[0].Rows[i];
            //            dgvPopup.Rows.Add(dr["InstID"],
            //                              dr["Article"],
            //                              dr["ArticleID"]
            //            );
            //            row = dgvPopup.Rows[i];
            //            row.Height = 30;


            //        }
            //        dgvPopup.ClearSelection();
            //        dgvPopup.AutoResizeColumns();
            //        dgvPopup.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //        if (dgvPopup.Rows.Count > 0) { dgvPopup[0, 0].Selected = true; } //0번째 행 선택 

            //        //((WizWork.Work.Frm_tprc_Main)(this.MdiParent)).SetstbLookUp(ds.Tables[0].Rows.Count.ToString() + "개의 자료가 검색되었습니다.");
            //    }
            //    else
            //    {
            //        //((WizWork.Work.Frm_tprc_Main)(this.MdiParent)).SetstbLookUp("0개의 자료가 검색되었습니다.");
            //        dgvPopup.Rows.Clear();
            //    }

            //}
            //catch (Exception excpt)
            //{
            //    MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            //}
            //finally
            //{
            //    DataStore.Instance.CloseConnection();


            //}

        }
        //압출노즐Size별 생산량 조회용
        //public void procACNozzle()
        //{
        //    dgvPopup.Rows.Clear();

        //    //string strErr = "";

        //    DataSet ds = null;
        //    DataGridViewRow row = null;

        //    int intnchkInstDate = 0;
        //    string strStartDate = "";
        //    string strEndDate = "";
        //    string strOrderID = "";
        //    string strInstID = "";

        //    int intnChkProcessID = 0;
        //    string strProcessID = "";
        //    int intnChkMachineID = 0;
        //    string strMachineID = "";

        //    int intnChkLotID = 0;
        //    string strLotID = "";

        //    if (this.Owner is Frm_tprc_Work_U_Sinter)//지시일, 공정ID, 머신ID로 검색
        //    {
        //        strInstID = ""; //지시번호

        //        if (this.chkResultDate.Checked == false) //지시일 체크 : N
        //        {
        //            intnchkInstDate = 0;
        //            strStartDate = "";
        //            strEndDate = "";
        //        }
        //        else //지시일 체크 : Y
        //        {
        //            if ((dtpOrderDateTo.Value - dtpOrderDateFrom.Value).TotalDays < 0)
        //            {
        //                return;
        //            }
        //            intnchkInstDate = 1;
        //            strStartDate = dtpOrderDateFrom.Value.ToString("yyyyMMdd");
        //            strEndDate = dtpOrderDateTo.Value.ToString("yyyyMMdd");
        //        }
        //        strOrderID = "";

        //        if (sProcessID != string.Empty)
        //        {
        //            intnChkProcessID = 1;
        //            strProcessID = sProcessID;
        //        }

        //        if (Split == 4 && sInstID != "")
        //        {
        //            strInstID = sInstID;
        //        }
                
        //    }

        //    try
        //    {
        //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

        //        sqlParameter.Add("nchkInstDate", intnchkInstDate);
        //        sqlParameter.Add("FromDate", strStartDate);
        //        sqlParameter.Add("ToDate", strEndDate);
        //        sqlParameter.Add("OrderID", strOrderID);
        //        sqlParameter.Add("InstID", strInstID);
        //        sqlParameter.Add("nChkProcessID", intnChkProcessID);
        //        sqlParameter.Add("ProcessID", strProcessID);
        //        sqlParameter.Add("nChkMachineID", intnChkMachineID);
        //        sqlParameter.Add("MachineID", strMachineID);
        //        sqlParameter.Add("nChkLotID", intnChkLotID);
        //        sqlParameter.Add("LotID", strLotID);

        //        ds = DataStore.Instance.ProcedureToDataSet("xp_plInputDet_splInputDet", sqlParameter, false);

        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            DataRow dr = null;

        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                dr = ds.Tables[0].Rows[i];
        //                dgvPopup.Rows.Add(dr["AcNozzleSize"],
        //                                  dr["ChildInputQty"]
        //                );
        //                row = dgvPopup.Rows[i];
        //                row.Height = 30;
        //            }
        //            dgvPopup.ClearSelection();
        //            dgvPopup.AutoResizeColumns();
        //            dgvPopup.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        //            if (dgvPopup.Rows.Count > 0) { dgvPopup[0, 0].Selected = true; } //0번째 행 선택 
        //            //((WizWork.Work.Frm_tprc_Main)(this.MdiParent)).SetstbLookUp(ds.Tables[0].Rows.Count.ToString() + "개의 자료가 검색되었습니다.");
        //        }
        //        else
        //        {
        //            //((WizWork.Work.Frm_tprc_Main)(this.MdiParent)).SetstbLookUp("0개의 자료가 검색되었습니다.");
        //            dgvPopup.Rows.Clear();
        //        }

        //    }
        //    catch (Exception excpt)
        //    {
        //        MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
        //    }
        //    finally
        //    {
        //        DataStore.Instance.CloseConnection();


        //    }

        ////}
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void btnSelect_Click(object sender, EventArgs e)
        //{
        //    string sInstID = "";
        //    string sArticleID = "";
        //    if (grdData.Rows.Count > 0)
        //    {
        //        switch (Split)
        //        {
        //            case 0:
        //                sMachine = grdData.CurrentRow.Cells["Machine"].Value.ToString();
        //                WriteTextEvent(Split, sMachine, string.Empty);
        //                break;
        //            case 1:
        //                sMachine = grdData.CurrentRow.Cells["MachineNO"].Value.ToString();
        //                sMachineID = grdData.CurrentRow.Cells["MachineID"].Value.ToString();
        //                WriteTextEvent(Split, sMachine, sMachineID);
        //                break;
        //            case 2:
        //                sPersonName = grdData.CurrentRow.Cells["Name"].Value.ToString();
        //                sPersonID = grdData.CurrentRow.Cells["PersonID"].Value.ToString();
        //                WriteTextEvent(Split, sPersonName, sPersonID);
        //                break;
        //            case 3:
        //                sInstID = grdData.CurrentRow.Cells["InstID"].Value.ToString();
        //                sArticleID = grdData.CurrentRow.Cells["ArticleID"].Value.ToString();
        //                WriteTextEvent(Split, sInstID, sArticleID);
        //                break;
        //            case 5:
        //                sInstID = grdData.CurrentRow.Cells["InstID"].Value.ToString();
        //                WriteTextEvent(Split, sInstID, string.Empty);
        //                break;
        //            case 6:
        //                sMachine = grdData.CurrentRow.Cells["Machine"].Value.ToString();
        //                sMachineID = grdData.CurrentRow.Cells["MachineID"].Value.ToString();
        //                WriteTextEvent(Split, sMachine, sMachineID);
        //                break;
        //        }
        //        this.Close();
        //    }
        //}

        private void Frm_PopUpSel_Load(object sender, EventArgs e)
        {
            this.Text = "사용자 선택";
            this.Size = new Size(457, 499);
            InitGrid();
            SetScreen();
            
            
            //SetupDgv();
        }

        private void dgvPopup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //btnSelect_Click(sender, e);
                //btnSelect_Click(dgvPopup.CurrentRow, e);
                //dgvPopup.CurrentRow.Selected = true;
            }
        }

        private void dtpOrderDateTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnInst_Click(object sender, EventArgs e)
        {
            ProcInstID();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int iSelRow = 0;
            for (int i = 0; i < grdData.SelectedCells.Count; i++)
            {
                iSelRow = grdData.SelectedCells[i].RowIndex;
                if (iSelRow > 0)
                {
                    grdData[0, iSelRow - 1].Selected = true;
                    break;
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int iSelRow = 0;
            for (int i = 0; i < grdData.SelectedCells.Count; i++)
            {
                iSelRow = grdData.SelectedCells[i].RowIndex;
                if (iSelRow < grdData.Rows.Count - 1)
                {
                    grdData[0, iSelRow + 1].Selected = true;
                    break;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //콤보박스의 부서를 선택안했을때
            //if (Lib.FindComboBoxID(cboDepart, list_cbx) == "")
            //{
            //    WizCommon.Popup.MyMessageBox.ShowBox("부서를 선택해주세요.", "[오류]", 0, 1);
            //    return;
            //}
            if (grdData.Rows.Count == 0 || grdData.CurrentRow is null)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 사용자가 없습니다. 사용자를 선택해주세요.", "[오류]", 0, 1);
                return;
            }
            //전역변수에 사용자 저장
            //Frm_tins_Main.g_tBase.PersonID = grdData.SelectedRows[0].Cells["PersonID"].Value.ToString();
            //Frm_tins_Main.g_tBase.Person = grdData.SelectedRows[0].Cells["Name"].Value.ToString();
            WriteTextEvent();
            this.Dispose();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void cboDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}