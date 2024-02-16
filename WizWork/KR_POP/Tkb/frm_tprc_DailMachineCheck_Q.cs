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
    public partial class Frm_tprc_DailMachineCheck_Q : Form
    {

        private DataSet ds = null;
        private DataSet ds1 = null;
        List<string> DeleteRowInspectID = new List<string>();
        DataGridView UpdateList = new DataGridView();

        string ItemCode = string.Empty;
        INI_GS gs = new INI_GS();

        /// <summary>
        /// 생성
        /// </summary>
        public Frm_tprc_DailMachineCheck_Q()
        {
            InitializeComponent();

        }
        private void Frm_tprc_DailMachineCheck_Q_Load(object sender, EventArgs e)
        {
            SetScreen();
            SetDateTime();

            InitGrid();
            InitGrid2();
            SetComboBox();
            ProcQuery();
        }
        private void SetDateTime()
        {
            ////ini 날짜 불러와서 기간 설정하기
            chkInsDate.Checked = true;
            int Days = 0;
            string[] sInstDate = gs.GetValue("Work", "Screen", "Screen").Split('|');
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
            //
        }

        #region Default Grid Setting

        private void InitGrid()
        {
            grdList.Columns.Clear();
            grdList.ColumnCount = 15;

            int n = 0;
            // Set the Colums Hearder Names

            grdList.Columns[n].Name = "RowSeq";
            grdList.Columns[n].HeaderText = "No";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "McRInspectID";
            grdList.Columns[n].HeaderText = "검사번호";
            grdList.Columns[n].Width = 130;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "McRInspectDate";
            grdList.Columns[n].HeaderText = "점검일자";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "McName";
            grdList.Columns[n].HeaderText = "설비명";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "McInsCycle";
            grdList.Columns[n].HeaderText = "검사\r\n구분";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "McInsBasisDate";
            grdList.Columns[n].HeaderText = "개정일자";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "Name";
            grdList.Columns[n].HeaderText = "검사자";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "McRInspectPersonID";
            grdList.Columns[n].HeaderText = "검사자ID";
            grdList.Columns[n].Width = 200;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "MCID";
            grdList.Columns[n].HeaderText = "설비ID";
            grdList.Columns[n].Width = 120;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "McInsCycleGbn";
            grdList.Columns[n].HeaderText = "검사주기구분";
            grdList.Columns[n].Width = 120;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "DefectContents";
            grdList.Columns[n].HeaderText = "문제내역";
            grdList.Columns[n].Width = 100;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "DefectReason";
            grdList.Columns[n].HeaderText = "문제원인";
            grdList.Columns[n].Width = 140;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "DefectRespectContents";
            grdList.Columns[n].HeaderText = "조치및대책";
            grdList.Columns[n].Width = 120;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "Comments";
            grdList.Columns[n].HeaderText = "비고";
            grdList.Columns[n].Width = 120;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "McInspectBasisID";
            grdList.Columns[n].HeaderText = "검사기준ID";
            grdList.Columns[n].Width = 120;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n++].Visible = false;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            {
                chkCol.HeaderText = "";
                chkCol.Name = "Check";
                chkCol.Width = 110;
                chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                chkCol.FlatStyle = FlatStyle.Standard;
                chkCol.ThreeState = true;
                chkCol.CellTemplate = new DataGridViewCheckBoxCell();
                chkCol.CellTemplate.Style.BackColor = Color.Beige;
                chkCol.Visible = true;
            }
            grdList.Columns.Insert(0, chkCol);

            grdList.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdList.RowTemplate.Height = 30;
            grdList.ColumnHeadersHeight = 45;
            grdList.ScrollBars = ScrollBars.Both;
            grdList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdList.ReadOnly = true;
            grdList.MultiSelect = false;

            foreach (DataGridViewColumn col in grdList.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        #endregion
        #region Default Grid2 Setting

        private void InitGrid2()
        {
            grdData.Columns.Clear();
            grdData.ColumnCount = 11;

            int n = 0;
            // Set the Colums Hearder Names

            grdData.Columns[n].Name = "RowSeq";
            grdData.Columns[n].HeaderText = "No";
            grdData.Columns[n].Width = 40;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "McRInspectID";
            grdData.Columns[n].HeaderText = "점검항목";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "McRInspectDate";
            grdData.Columns[n].HeaderText = "점검내용";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "McName";
            grdData.Columns[n].HeaderText = "확인";// + "\r\n" + "방법";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "McInsCycle";
            grdData.Columns[n].HeaderText = "주기";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "McInsBasisDate";
            grdData.Columns[n].HeaderText = "기록구분";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "Name";
            grdData.Columns[n].HeaderText = "점검결과";
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "McInspectBasisID";
            grdData.Columns[n].HeaderText = "McInspectBasisID";
            grdData.Columns[n].Width = 200;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "McInspectBasisSeq";
            grdData.Columns[n].HeaderText = "McInspectBasisSeq";
            grdData.Columns[n].Width = 120;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "McInsCycleGbn";
            grdData.Columns[n].HeaderText = "McInsCycleGbn";
            grdData.Columns[n].Width = 120;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "McInsRecordGbn";
            grdData.Columns[n].HeaderText = "McInsRecordGbn";
            grdData.Columns[n].Width = 100;
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = false;

            grdData.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdData.ReadOnly = true;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion
        //콤보박스 데이터 바인딩
        private void SetComboBox()
        {
            Tools.INI_GS gs = new Tools.INI_GS();
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Code_sMtMc", null, false);
            DataTable dt2 = dt.Clone();

            string[] sMachineID = null;
            sMachineID = gs.GetValue("Work", "Machine", "Machine").Split('|');//배열에 설비아이디 넣기
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
                        if (Mac.Substring(0, 4) == dr["ProcessID"].ToString())
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
                    sMachine.Remove(dr["ProcessID"].ToString() + dr["MachineID"].ToString());
                }
            }

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                DataRow newRow = dt2.NewRow();
                newRow[Code_sMtMc.MCID] = "*";
                newRow[Code_sMtMc.MCNAME] = "전체";
                dt2.Rows.InsertAt(newRow, 0);
                cboMcName.DataSource = dt2;
                cboMcName.ValueMember = Code_sMtMc.MCID;
                cboMcName.DisplayMember = Code_sMtMc.MCNAME;
                if (dt2.Rows.Count > 1)
                {
                    cboMcName.SelectedIndex = 0;
                }
            }
            dt = null;

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add(Code_sCmCode.CODEGBN, "MCCYCLEGBN");//cboProcess.Text 
            sqlParameter.Add(Code_sCmCode.SRELATION, "");//cboProcess.Text
            ds1 = DataStore.Instance.ProcedureToDataSet("xp_Code_sCmCode", sqlParameter, false);
            cboMcInsCycleGbn.DataSource = ds1.Tables[0];
            cboMcInsCycleGbn.ValueMember = Code_sCmCode.CODE_ID;
            cboMcInsCycleGbn.DisplayMember = Code_sCmCode.CODE_NAME;
        }
        /// <summary>
        /// 조회버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnLookup_Click(object sender, EventArgs e)
        {
            ProcQuery();
        }
        private void ProcQuery()
        {

            grdList.Rows.Clear();
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                DateTime dateTime = DateTime.Now;
                if (!chkInsDate.Checked)
                {
                    sqlParameter.Add(Work_sMcRegularInspect.CHKMCRINSPECTDATE, "0");    // 최종검사
                    sqlParameter.Add(Work_sMcRegularInspect.SFROMDATE, "");
                    sqlParameter.Add(Work_sMcRegularInspect.STODATE, "");
                }
                else
                {
                    sqlParameter.Add(Work_sMcRegularInspect.CHKMCRINSPECTDATE, "1");    // 최종검사
                    sqlParameter.Add(Work_sMcRegularInspect.SFROMDATE, mtb_From.Text.Replace("-", ""));
                    sqlParameter.Add(Work_sMcRegularInspect.STODATE, mtb_To.Text.Replace("-", ""));
                }

                if (chkMcName.Checked == false || (chkMcName.Checked && cboMcName.SelectedValue.ToString() == "*"))//|| chkMcInsCycleGbn.Checked == true && cboMcInsCycleGbn.SelectedIndex == 0) //공정체크 : N
                {
                    sqlParameter.Add(Work_sMcRegularInspect.CHKMCID, "0");
                    sqlParameter.Add(Work_sMcRegularInspect.MCID, "");
                }
                else if (chkMcName.Checked == true)//&& cboMcInsCycleGbn.SelectedIndex > 0) //공정체크 : Y
                {
                    sqlParameter.Add(Work_sMcRegularInspect.CHKMCID, "1");
                    sqlParameter.Add(Work_sMcRegularInspect.MCID, cboMcName.SelectedValue.ToString());
                }

                if (chkMcInsCycleGbn.Checked == false)//|| chkMcInsCycleGbn.Checked == true && cboMcInsCycleGbn.SelectedIndex == 0) //공정체크 : N
                {
                    sqlParameter.Add(Work_sMcRegularInspect.CHKINSCYCLEGBN, "0");
                    sqlParameter.Add(Work_sMcRegularInspect.INSCYCLEGBN, "");
                }
                else if (chkMcInsCycleGbn.Checked == true)//&& cboMcInsCycleGbn.SelectedIndex > 0) //공정체크 : Y
                {
                    sqlParameter.Add(Work_sMcRegularInspect.CHKINSCYCLEGBN, "1");
                    sqlParameter.Add(Work_sMcRegularInspect.INSCYCLEGBN, cboMcInsCycleGbn.SelectedValue.ToString());
                }
                ds = DataStore.Instance.ProcedureToDataSet("xp_McReqularInspect_sMcReqularInspect", sqlParameter, false);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        dateTime = DateTime.ParseExact(dr["McRInspectDate"].ToString(), "yyyyMMdd", null);
                        grdList.Rows.Add(
                            false,
                            i + 1,                                          //'0)Index
                            dr["McRInspectID"].ToString(),                  //'1)검사번호
                            dateTime.ToString("yyyy-MM-dd"),                //'2)검사일자
                            dr["McName"].ToString(),                        //'3)설비명
                            dr["McInsCycle"].ToString(),                    //'4)정기검사구분

                            dr["McInsBasisDate"].ToString(),                //'5)개정일자
                            dr["Name"].ToString(),                          //'6)검사자
                            dr["McRInspectPersonID"].ToString(),            //'7)ProcessID
                            dr["MCID"].ToString(),                          //'8)MCID
                            dr["McInsCycleGbn"].ToString(),                 //'9)정기검사구분

                            dr["DefectContents"].ToString(),                //'10)문제내역
                            dr["DefectReason"].ToString(),                  //'11)문제원인
                            dr["DefectRespectContents"].ToString(),         //'12)조치및대책
                            dr["Comments"].ToString(),                      //'13)비고
                            dr["McInspectBasisID"].ToString()               //'14)검사기준ID
                            );
                        grdList.Rows[i].Height = 30;
                    }
                    grdList.ClearSelection();
                    grdList[0, 0].Selected = true;
                    //grdList.AutoResizeColumns();
                    grdList.Columns["RowSeq"].Width = 60;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }
        private void FillGridData2()
        {
            try
            {
                grdData.Rows.Clear();
                if (grdList.SelectedRows.Count == 0)
                {
                    return;
                }
                Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();

                sqlParameter1.Add(Work_sMcRegularInspectSub.MCRINSPECTID, grdList.SelectedRows[0].Cells["McRInspectID"].Value.ToString());    // 최종검사
                sqlParameter1.Add(Work_sMcRegularInspectSub.MCINSPECTBASISID, grdList.SelectedRows[0].Cells["McInspectBasisID"].Value.ToString());
                sqlParameter1.Add(Work_sMcRegularInspectSub.MCINSCYCLEGBN, grdList.SelectedRows[0].Cells["McInsCycleGbn"].Value.ToString());

                ds1 = DataStore.Instance.ProcedureToDataSet("xp_McRegularInspect_sMcRegularInspectSub", sqlParameter1, false);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    double douValue = 0;
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds1.Tables[0].Rows[i];

                        if (dr[Work_sMcRegularInspectSub.MCINSRECORDGBN].ToString() == "1")
                        {
                            grdData.Rows.Add(
                            i + 1,            //NO
                            dr[Work_sMcRegularInspectSub.MCINSITEMNAME],        //점검항목
                            dr[Work_sMcRegularInspectSub.MCINSCONTENT],         //점검내용
                            dr[Work_sMcRegularInspectSub.MCINSCHECK],           //확인
                            dr[Work_sMcRegularInspectSub.MCINSCYCLE],           //주기
                            dr[Work_sMcRegularInspectSub.MCINSRECORD],          //기록

                            dr[Work_sMcRegularInspectSub.LEGEND]);              //점검결과
                        }
                        else if (dr[Work_sMcRegularInspectSub.MCINSRECORDGBN].ToString() == "2")
                        {
                            double.TryParse(dr[Work_sMcRegularInspectSub.MCRINSPECTVALUE].ToString(), out douValue);
                            grdData.Rows.Add(
                           i + 1,            //NO
                           dr[Work_sMcRegularInspectSub.MCINSITEMNAME],       //점검항목
                           dr[Work_sMcRegularInspectSub.MCINSCONTENT],               //점검내용
                           dr[Work_sMcRegularInspectSub.MCINSCHECK],           //확인
                           dr[Work_sMcRegularInspectSub.MCINSCYCLE],       //주기
                           dr[Work_sMcRegularInspectSub.MCINSRECORD],                 //기록
                           string.Format("{0:n1}", douValue));
                        }
                        grdData.Rows[i].Height = 30;
                    }
                    grdData.AutoResizeColumns();
                    Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdData.RowCount);
                    Frm_tprc_Main.gv.SetStbInfo();
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }
        public void procDelete()
        {
            if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
            {
                try
                {
                    int rowIndex = grdList.CurrentRow.Index;

                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add(Work_sMcRegularInspect.MCRINSPECTID, grdList.Rows[rowIndex].Cells["McRInspectID"].Value.ToString());

                    ds = DataStore.Instance.ProcedureToDataSet("xp_McRegularInspect_dMcRegularInspect", sqlParameter, true); //삭제

                    ProcQuery(); //재조회 
                }

                catch (Exception ex)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 메인화면 버튼 클릭 - 폼 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 제품명 검색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbItemName_ClickButton(object sender, EventArgs e)
        {
            //Frm_Qlt_SelectItem item = new Frm_Qlt_SelectItem();
            //item.ShowDialog();
        }

        /// <summary>
        /// 제품명 검색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbItemName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Frm_Qlt_SelectItem item = new Frm_Qlt_SelectItem();
            //item.ShowDialog();
        }

        private void cmdRowUp_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                Frm_tprc_Main.Lib.btnRowUp(grdList);
            }
            else
            {
                Frm_tprc_Main.Lib.btnRowUp(grdData);
            }
        }

        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                Frm_tprc_Main.Lib.btnRowDown(grdList);
            }
            else
            {
                Frm_tprc_Main.Lib.btnRowDown(grdData);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            int checkCount = 0;//체크된 카운트
            int c = 0; //작업일이 현재일자와 같지 않아서 삭제할 수 없는 행의 수
            int deleteCount = 0; //작업일이 현재일자와 같지 않아서 삭제할 수 없는 행의 수
            if (tabControl.SelectedIndex == 0)
            {
                if (grdList.RowCount == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("조회 후 삭제 버튼을 눌러주십시오.", "[조회 클릭]", 0, 0);
                }
                else
                {
                    foreach (DataGridViewRow dgvr in grdList.Rows)
                    {
                        if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                        {
                            checkCount++;
                        }
                    }
                    if (checkCount == 0)
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("삭제 대상을 선택 후 '삭제'버튼을 클릭해주세요.", "[삭제 대상 클릭]", 0, 0);
                    }
                    else
                    {
                        if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
                        {
                            List<string> list_Confirm = new List<string>();//프로시저 수행 성공여부 값 저장/success/failure
                            foreach (DataGridViewRow dgvr in grdList.Rows)
                            {
                                if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                                {
                                    if (dgvr.Cells["McRInspectDate"].Value.ToString().Replace("-", "") == DateTime.Now.ToString("yyyyMMdd"))
                                    {
                                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                                        sqlParameter.Add("McRInspectID", dgvr.Cells["McRInspectID"].Value.ToString());
                                        string[] sConfirm = new string[2];
                                        sConfirm = DataStore.Instance.ExecuteProcedure("xp_McRegularInspect_dMcRegularInspect", sqlParameter, true);
                                        list_Confirm.Add(sConfirm[0]);
                                        if (sConfirm[0].ToUpper() == "SUCCESS")
                                        { deleteCount++; }
                                    }
                                    else
                                    {
                                        c++;
                                    }
                                }
                            }
                            if (list_Confirm.Count > 0)//삭제결과 리스트
                            {
                                ProcQuery();
                                if (c > 0)
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("현재 날짜와 동일한 작업일자" + deleteCount.ToString() + "건 삭제완료됬습니다." +
                                    "\r\n" + c.ToString() + "개 작업건수는 현재 날짜와 동일하지 않아 삭제할 수 없습니다.", "[삭제 완료]", 0, 1);
                                }
                                else
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox(deleteCount.ToString() + "건 삭제완료됬습니다.", "[삭제 완료]", 0, 1);
                                }
                            }
                            else//삭제 결과리스트가 없음 > 삭제를 안했음
                            {
                                if (c > 0)
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox(c.ToString() + "개 작업건수는 현재 날짜와 동일하지 않아 삭제할 수 없습니다.", "[삭제 실패]", 0, 1);
                                }
                                else
                                {
                                    WizCommon.Popup.MyMessageBox.ShowBox("삭제실패! 관리자에게 문의하세요", "[삭제 실패]", 0, 1);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                if (grdList.Rows.Count > 0 && grdList.SelectedRows.Count > 0)
                {
                    btnDelete.Visible = true;
                    Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdList.RowCount);
                    Frm_tprc_Main.gv.SetStbInfo();
                }
            }
            else if (tabControl.SelectedIndex == 1)
            {
                btnDelete.Visible = false;
                FillGridData2();
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
        #region 달력 To값 입력 // 달력 창 띄우기
        private void mtb_To_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion

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
            tlpOperate.SetRowSpan(cmdClose, 2);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Frm_tprc_DailMachineCheck_Q_Activated(object sender, EventArgs e)
        {
            ((Frm_tprc_Main)(MdiParent)).LoadRegistry();
        }

        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (grdList.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    grdList.Rows[e.RowIndex].Cells["Check"].Value = true;
                }
                else if (grdList.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    grdList.Rows[e.RowIndex].Cells["Check"].Value = false;
                }
            }
        }
    }
}
