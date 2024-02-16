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
    public partial class frm_tins_InspectAutoResult_Q : Form
    {
        string[] Message = new string[2];
        INI_GS gs = Frm_tprc_Main.gs;
        WizWorkLib Lib = Frm_tprc_Main.Lib;
        DataTable dt = null;
        DataTable dt2 = null;
        int z = 0; //grddata 그리드 좌우 이동용 변수
        int x = 0; //grdlist 그리드 좌우 이동용 변수
        public frm_tins_InspectAutoResult_Q()
        {
            InitializeComponent();
        }

        //화면 닫기
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_tins_InspectAutoResult_Q_Load(object sender, EventArgs e)
        {
            SetScreen();
            InitGrid();
            InitgrdDataGrid();//  Grid Setting
            
            SerFormClearData(); // 화면 초기화
            setComboBox();
            chkProcess.Checked = true;

            txtPLotID.Text = gs.GetValue("Work", "SetLOTID", "");
            if (txtPLotID.Text != string.Empty)
            {
                chkPLotID.Checked = true;
                FillGridList();
            }
            else
            {
                chkPLotID.Checked = false;
                FillGridList();
            }
            
        }

        private void setComboBox()
        {
            try
            {
                string strProcessID = Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID");
                string[] gubunProcess = strProcessID.Split(new char[] { '|' });

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

                //공정 가져오기
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Work_sProcess.NCHKPROC, "1");//cboProcess.Text 
                sqlParameter.Add(Work_sProcess.PROCESSID, strProcessID);//cboProcess.Text
                DataSet ds = DataStore.Instance.ProcedureToDataSet("[xp_Work_sProcess]", sqlParameter, false);
                DataRow newRow = ds.Tables[0].NewRow();
                newRow[Work_sProcess.PROCESSID] = "*";
                newRow[Work_sProcess.PROCESS] = "전체";
                ds.Tables[0].Rows.InsertAt(newRow, 0);
                cboProcess.DataSource = ds.Tables[0];
                cboProcess.ValueMember = Work_sProcess.PROCESSID;
                cboProcess.DisplayMember = Work_sProcess.PROCESS;
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(ex.Message, "[공정 콤보박스 세팅 오류]", 0, 1);
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
            tlp_Search_Date.SetRowSpan(chkDate, 2);
        }

        #region Default Grid Setting

        private void InitGrid()
        {
            grdList.Columns.Clear();
            grdList.ColumnCount = 17;
            
            int n = 0;
            // Set the Colums Hearder Names



            grdList.Columns[n].Name = "No";
            grdList.Columns[n].HeaderText = "";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "InspectID";
            grdList.Columns[n].HeaderText = "InspectID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "InspectDate";
            grdList.Columns[n].HeaderText = "검사일";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;
            
            grdList.Columns[n].Name = "InspectTime";
            grdList.Columns[n].HeaderText = "검사\r\n시간";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "ArticleID";
            grdList.Columns[n].HeaderText = "ArticleID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "Article";
            grdList.Columns[n].HeaderText = "품명";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "BuyerArticleNo";
            grdList.Columns[n].HeaderText = "품번";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "FMLGubun";
            grdList.Columns[n].HeaderText = "초/중/종";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "InspectQty";
            grdList.Columns[n].HeaderText = "샘플량";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "DefectYN";
            grdList.Columns[n].HeaderText = "합/불";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "Process";
            grdList.Columns[n].HeaderText = "공정";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "Machine";
            grdList.Columns[n].HeaderText = "설비";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = true;
            
            grdList.Columns[n].Name = "Name";
            grdList.Columns[n].HeaderText = "검사자";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "BuyerModel";
            grdList.Columns[n].HeaderText = "차종";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "ECONo";
            grdList.Columns[n].HeaderText = "EO기준";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "InspectBasisID";
            grdList.Columns[n].HeaderText = "InspectBasisID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "LotID";
            grdList.Columns[n].HeaderText = "지시LOTID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            {
                chkCol.HeaderText = "";
                chkCol.Name = "Check";
                chkCol.Width = 110;
                //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
            grdList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdList.ScrollBars = ScrollBars.Both;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdList.MultiSelect = false;

            foreach (DataGridViewColumn col in grdList.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            return;
        }

        #endregion

        private void InitgrdDataGrid()
        {
            grdData.Columns.Clear();
            grdData.ColumnCount = 13;
            int a = 0;
            // Set the Colums Hearder Names
            grdData.Columns[a].Name = "No";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = true;

            grdData.Columns[++a].Name = WizWork.TableData.Ins_InspectAutoSub.LOTID;
            grdData.Columns[a].HeaderText = "지시LOTID";
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = false;
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            grdData.Columns[++a].Name = WizWork.TableData.Ins_InspectAutoSub.ARTICLE;
            grdData.Columns[a].HeaderText = "품명";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = false;

            grdData.Columns[++a].Name = "OrderArticleID";
            grdData.Columns[a].HeaderText = "품목코드";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = false;

            grdData.Columns[++a].Name = WizWork.TableData.Ins_InspectAutoSub.ITEMSPEC;
            grdData.Columns[a].HeaderText = "품명스펙";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = false;

            grdData.Columns[++a].Name = WizWork.TableData.Ins_InspectAutoSub.INSITEMNAME;
            grdData.Columns[a].HeaderText = "검사항목";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = true;

            grdData.Columns[++a].Name = WizWork.TableData.Ins_InspectAutoSub.CHKSPEC;
            grdData.Columns[a].HeaderText = "검사스펙";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = true;

            grdData.Columns[++a].Name = WizWork.TableData.Ins_InspectAutoSub.INSPECTVALUE;
            grdData.Columns[a].HeaderText = "측정값";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = true;

            grdData.Columns[++a].Name = WizWork.TableData.Ins_InspectAutoSub.INSPECTDATE;
            grdData.Columns[a].HeaderText = "검사일자";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = true;

            grdData.Columns[++a].Name = WizWork.TableData.Ins_InspectAutoSub.INSPECTMAN;
            grdData.Columns[a].HeaderText = "검사자";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = true;

            grdData.Columns[++a].Name = "FMLGubun";
            grdData.Columns[a].HeaderText = "초/중/종";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = true;

            grdData.Columns[++a].Name = WizWork.TableData.Ins_InspectAutoSub.INSPECTID;
            grdData.Columns[a].HeaderText = "InspectID";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = false;

            grdData.Columns[++a].Name = WizWork.TableData.Ins_InspectAutoSub.SEQ;
            grdData.Columns[a].HeaderText = "Seq";
            grdData.Columns[a].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //grdData.Columns[a].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[a].ReadOnly = true;
            grdData.Columns[a].Visible = false;

            grdData.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdData.ReadOnly = true;
            grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        private void SerFormClearData()
        {
            SetDateTime();
            mtb_From.Enabled = true;
            mtb_To.Enabled = true;

            grdData.Rows.Clear();

            //chkArticle.Checked = false;
            this.txtBuyerArticle.Text = "";
            this.txtBuyerArticle.Enabled = false;

            
        }
        private void SetDateTime()
        {
            ////ini 날짜 불러와서 기간 설정하기
            chkDate.Checked = true;
            int Days = 0;
            string[] sInstDate =Frm_tprc_Main.gs.GetValue("Work", "Screen", "Screen").Split('|');
            foreach (string str in sInstDate)
            {
                string[] Value = str.Split('/');
                //if (this.Name.ToUpper().Contains(Value[0].ToUpper()))
                if(Value[0].ToUpper().Contains("INSPECTRESULT"))
                {
                    int.TryParse(Value[1], out Days);
                    break;
                }
            }
            mtb_From.Text = DateTime.Today.AddDays(-Days).ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
            //
        }
        #region 조회조건 클릭 이벤트 
        private void chkResultDate_Click(object sender, EventArgs e)
        {

            if (this.chkDate.Checked == true)
            {
                this.mtb_From.Enabled = true;
                this.mtb_To.Enabled = true;
            }
            else
            {
                this.mtb_From.Enabled = false;
                this.mtb_To.Enabled = false;

            }

        }

        #endregion

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            FillGridList();
        }

        #region 프로시저호출
        private void FillGridList()
        {
            // 조회 버튼 클릭 시, 일자별 조회 탭을 활성화 시키기 위해서 → 이거 안먹힘 ㅓㄴ아ㅣ럼ㄴ아ㅣ러
            tabPage1.Select();

            //double douInsSampleQty = 0;
            //int InspectQty = 0;
            int z = 0;
            int intnChkDate = 0;
            int intnChkArticle = 0;
            string strSdate = "";
            string strEdate = "";
            string strArticle = "";
            string strInspectPoint = "9";  //입고 :1, 공정 : 5, 출고 : 9  자주 : 9
            int nFMLGubun = 0;
            string FMLGubun = "";
            int intnChkPLotID = 0;
            string strsPLotID = "";
            string sInspectDate = "";
            string sInspectTime = "";
            string DefectYN = "";
            if (chkDate.Checked)
            {
                if ((int.Parse(mtb_To.Text.Replace("-", "")) - int.Parse(mtb_From.Text.Replace("-", ""))) < 0)
                {
                    Message[0] = "[검색조건 오류]";
                    Message[1] = "조회 시작일이 종료일보다 늦을 수 없습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return;
                }
                intnChkDate = 1;
                strSdate = mtb_From.Text.Replace("-", "");
                strEdate = mtb_To.Text.Replace("-", "");
            }
            else
            {
                intnChkDate = 0;
                strSdate = "";
                strEdate = "";
            }

            if (chkArticle.Checked)
            {
                intnChkArticle = 1;
                strArticle = txtBuyerArticle.Tag.ToString();
            }
            else
            {
                intnChkArticle = 0;
                strArticle = "";
            }
            //if (chkPLotID.Checked)
            //{
            //    intnChkPLotID = 1;
            //    strsPLotID = txtPLotID.Text.Trim();
            //}
            //else
            //{
            //    intnChkPLotID = 0;
            //    strsPLotID = txtPLotID.Text.Trim();
            //}
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(WizWork.TableData.Ins_InspectAutoSub.NCHKDATE, intnChkDate);
                sqlParameter.Add(WizWork.TableData.Ins_InspectAutoSub.SDATE, strSdate);
                sqlParameter.Add(WizWork.TableData.Ins_InspectAutoSub.EDATE, strEdate);
                sqlParameter.Add(WizWork.TableData.Ins_InspectAutoSub.NCHKARTICLE, intnChkArticle);
                sqlParameter.Add(WizWork.TableData.Ins_InspectAutoSub.SARTICLEID, strArticle);
                sqlParameter.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTPOINT, strInspectPoint);
                sqlParameter.Add("nChkPLotID", intnChkPLotID);
                sqlParameter.Add("sPLotID", strsPLotID);
                sqlParameter.Add("nChkProcessID", chkProcess.Checked == true ? 1 : 0);
                sqlParameter.Add("ProcessID", cboProcess.SelectedValue != null ? cboProcess.SelectedValue.ToString() : "");
                dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sInspectAuto", sqlParameter, false);
                grdList.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    z++;
                    int.TryParse(dr["FMLGubun"].ToString(), out nFMLGubun);
                    sInspectDate = Lib.MakeDateTime("yyyymmdd", dr["InspectDate"].ToString());
                    sInspectTime = Lib.MakeDateTime("hhmm", dr["InspectTime"].ToString().Trim());
                    //double.TryParse(dr["InsSampleQty"].ToString().Trim(), out douInsSampleQty);
                    
                    if (dr["DefectYN"].ToString().ToUpper() == "N")
                    {
                        DefectYN = "합격";
                    }
                    else if (dr["DefectYN"].ToString().ToUpper() == "Y")
                    {
                        DefectYN = "불합격";
                    }
                    switch (nFMLGubun)
                    {
                        case 1: FMLGubun = "초"; break;
                        case 2: FMLGubun = "중"; break;
                        case 3: FMLGubun = "종"; break;
                    }
                    grdList.Rows.Add(
                                    false,
                                    z.ToString(),
                                    dr["InspectID"].ToString().Trim(),
                                    sInspectDate,
                                    sInspectTime,
                                    
                                    dr["ArticleID"].ToString().Trim(),
                                    dr["Article"].ToString().Trim(),
                                    dr["BuyerArticleNo"].ToString().Trim(),
                                    FMLGubun,
                                    "",//string.Format("{0:n0}", douInsSampleQty),
                                    DefectYN,
                                    
                                    dr["Process"].ToString().Trim(),
                                    dr["Machine"].ToString().Trim(),
                                    
                                    dr["Name"].ToString().Trim(),
                                    dr["BuyerModel"].ToString().Trim(),
                                    dr["ECONo"].ToString().Trim(),
                                    dr["InspectBasisID"].ToString().Trim(),
                                    dr["LotID"].ToString().Trim()
                                    );
                }
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdList.RowCount);
                Frm_tprc_Main.gv.SetStbInfo();
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        private void procQuery()
        {
            int z = 0;
            int nFMLGubun = 0;
            string strArticle = "";
            string strInspectPoint = "9";  //입고 :1, 공정 : 5, 출고 : 9  자주 : 9
            string FMLGubun = "";
            string strsPLotID = "";
            string strInspectID = "";
            double douInspectValue = 0;
            if (grdList.RowCount == 0 || grdList.SelectedRows.Count == 0)
            { return; }
            strInspectID = grdList.SelectedRows[0].Cells["InspectID"].Value.ToString();
            strArticle = grdList.SelectedRows[0].Cells["ArticleID"].Value.ToString();
            strsPLotID = grdList.SelectedRows[0].Cells["LotID"].Value.ToString();
            strInspectPoint = "9";
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("InspectID", strInspectID);
                sqlParameter.Add("ArticleID", strArticle);
                sqlParameter.Add("InspectPoint", strInspectPoint);
                sqlParameter.Add("PLotID", strsPLotID);
                dt2 = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sInspectAutoSub", sqlParameter, false);
                grdData.Rows.Clear();
                foreach (DataRow dr in dt2.Rows)
                {
                    z++;
                    int.TryParse(dr["FMLGubun"].ToString(), out nFMLGubun);
                    
                    switch (nFMLGubun)
                    {
                        case 1: FMLGubun = "초"; break;
                        case 2: FMLGubun = "중"; break;
                        case 3: FMLGubun = "종"; break;
                    }
                    if (dr[WizWork.TableData.Ins_InspectAutoSub.INSTYPE].ToString().Trim() == "1")
                    {
                        grdData.Rows.Add(z.ToString(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.LOTID].ToString().Trim(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.ARTICLE].ToString().Trim(),
                                            dr["OrderArticleID"].ToString(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.SPEC].ToString().Trim(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.INSITEMNAME].ToString().Trim(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.INSTPSPEC].ToString().Trim(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.INSPECTTEXT].ToString().Trim(),
                                            Lib.MakeDate(WizWorkLib.DateTimeClss.DF_MID, dr[WizWork.TableData.Ins_InspectAutoSub.INSPECTDATE].ToString().Trim()),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.NAME].ToString().Trim(),
                                            FMLGubun.ToString().Trim(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.INSPECTID].ToString().Trim(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.SEQ].ToString().Trim()
                                            );
                    }
                    else
                    {
                        double.TryParse(dr[WizWork.TableData.Ins_InspectAutoSub.INSPECTVALUE].ToString().Trim(), out douInspectValue);
                        grdData.Rows.Add(z.ToString(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.LOTID].ToString().Trim(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.ARTICLE].ToString().Trim(),
                                            dr["OrderArticleID"].ToString(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.SPEC].ToString().Trim(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.INSITEMNAME].ToString().Trim(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.INSRASPEC].ToString().Trim(),
                                            //dr[WizWork.TableData.Ins_InspectAutoSub.INSPECTVALUE].ToString().Trim(),
                                            string.Format("{0:n2}", douInspectValue),
                                            Lib.MakeDate(WizWorkLib.DateTimeClss.DF_MID, dr[WizWork.TableData.Ins_InspectAutoSub.INSPECTDATE].ToString().Trim()),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.NAME].ToString().Trim(),
                                            FMLGubun.ToString().Trim(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.INSPECTID].ToString().Trim(),
                                            dr[WizWork.TableData.Ins_InspectAutoSub.SEQ].ToString().Trim()
                                            );
                    }
                }
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdData.RowCount);
                Frm_tprc_Main.gv.SetStbInfo();
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }
        #endregion

        /// <summary>
        /// Grid 위로 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUp_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                Lib.btnRowUp(grdList, x);
            }
            else if (tabControl.SelectedIndex == 1)
            {
                Lib.btnRowUp(grdData, z);
            }
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                Lib.btnRowDown(grdList, x);
            }
            else if (tabControl.SelectedIndex == 1)
            {
                Lib.btnRowDown(grdData, z);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string strInspectID = "";
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
                                        strInspectID = "";
                                        strInspectID = dgvr.Cells["InspectID"].Value.ToString();
                                        if (dgvr.Cells["InspectDate"].Value.ToString().Replace("-", "") == DateTime.Now.ToString("yyyyMMdd"))
                                        {
                                            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                                            sqlParameter.Add("InspectID", strInspectID);
                                            string[] sConfirm = new string[2];
                                            sConfirm = DataStore.Instance.ExecuteProcedure("xp_WizWork_dInspectAuto", sqlParameter, true);
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
                                //if (list_Confirm.Count > 0)//삭제결과 리스트
                                if(deleteCount > 0)
                                {
                                    FillGridList();
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
                                    if(c > 0)
                                    {
                                        WizCommon.Popup.MyMessageBox.ShowBox(c.ToString() + "개 작업건수는 현재 날짜와 동일하지 않아 삭제할 수 없습니다.", "[삭제 완료]", 0, 1);
                                    }
                                }
                            }
                        }
                    }
                }
                //else if (tabControl.SelectedIndex == 1)
                //{
                //    if (grdData.RowCount == 0)
                //    {
                //        WizCommon.Popup.MyMessageBox.ShowBox("삭제대상 정보가 없습니다.", "[오류]", 0, 1);
                //        return;
                //    }
                //    if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
                //    {
                //        RemoveIdx = grdData.SelectedRows[0].Index;
                //        strInspectID = grdData.SelectedRows[0].Cells["InspectID"].Value.ToString();
                //        strSeq = grdData.SelectedRows[0].Cells["Seq"].Value.ToString();
                //        if (strInspectID == "" || strSeq == "")
                //        {
                //            WizCommon.Popup.MyMessageBox.ShowBox("삭제대상 정보가 없습니다.", "[오류]", 0, 1);
                //            return;
                //        }
                //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //        sqlParameter.Add("InspectID", strInspectID);
                //        sqlParameter.Add("Seq", strSeq);
                //        string[] Confirm = new string[2];
                //        Confirm = DataStore.Instance.ExecuteProcedure("[xp_WizIns_dInspectAutoSub]", sqlParameter, false);
                //        if (Confirm[0].ToLower() == "success")
                //        {
                //            grdData.Rows.RemoveAt(RemoveIdx);
                //            procQuery();
                //            if (grdData.RowCount == 0)
                //            {
                //                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                //                sqlParameter2.Add("InspectID", strInspectID);
                //                string[] Confirm2 = new string[2];
                //                Confirm2 = DataStore.Instance.ExecuteProcedure("xp_WizWork_dInspectAuto", sqlParameter2, false);
                //                if (Confirm2[0].ToLower() == "success")
                //                {
                //                    FillGridList();
                //                    tabControl.SelectedIndex = 0;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            throw new Exception(Confirm[1]);
                //        }
                //    }
                //}
                //20190124 수정 자주검사 세부내역 삭제 로직 주석처리. 
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        //private void btnArticle_Click(object sender, EventArgs e)
        //{
        //    Frm_PopUpSel_sArticle fps = new Frm_PopUpSel_sArticle();
        //    fps.WriteTextEvent += new Frm_PopUpSel_sArticle.TextEventHandler(GetData);
        //    fps.Owner = this;
        //    fps.Show();

        //    void GetData(string sArticleID, string sArticle)
        //    {
        //        txtBuyerArticle.Text = sArticle;
        //        txtBuyerArticle.Tag = sArticleID;
        //    }
        //}

        private void btnArticleClear_Click(object sender, EventArgs e)
        {
            txtBuyerArticle.Text = "";
            txtBuyerArticle.Tag = "";
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

        private void txtPLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtPLotID.Text.ToUpper().Contains("PL") && (txtPLotID.Text.Trim().Length == 15 || txtPLotID.Text.Trim().Length == 16))
                {
                    chkPLotID.Checked = true;
                    FillGridList();
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("작업지시목록 LotID가 아닙니다. 작업지시목록에 있는 바코드를 스캔해주세요!", "[바코드 오류]", 2, 1);
                    return;
                }
            }
        }

        private void chkPLotID_Click(object sender, EventArgs e)
        {
            if (chkPLotID.Checked)
            {
                txtPLotID.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("LOTID입력", "LOTID");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtPLotID.Text = keypad.tbInputText.Text;
                    chkArticle.Checked = false;
                    chkDate.Checked = false;
                    procQuery();
                }
            }
            else
            {
                txtPLotID.Text = "";
            }
        }
        

        private void chkArticle_Click(object sender, EventArgs e)
        {
            if (chkArticle.Checked)
            {
                txtBuyerArticle.Text = "";
                foreach (Form openForm in Application.OpenForms)//중복실행방지
                {
                    if (openForm.Name == "Frm_PopUpSel_sArticle_O")
                    {
                        openForm.BringToFront();
                        openForm.Activate();
                        return;
                    }
                }
                Frm_PopUpSel_sArticle_O fps = new Frm_PopUpSel_sArticle_O();
                fps.WriteTextEvent += new Frm_PopUpSel_sArticle_O.TextEventHandler(GetData);
                fps.Owner = this;
                fps.BringToFront();
                fps.Show();

                void GetData(string sArticleID, string sArticle, string sBuyerArticleNo)
                {
                    txtBuyerArticle.Text = sBuyerArticleNo;
                    txtBuyerArticle.Tag = sArticleID;
                    chkArticle.Checked = true;
                    FillGridList();
                }
            }
            else
            {
                txtBuyerArticle.Text = "";
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
            {
                cmdDelete.Visible = false;
                procQuery();
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdData.RowCount);
                Frm_tprc_Main.gv.SetStbInfo();
            }
            else if(tabControl.SelectedIndex == 0)
            {
                cmdDelete.Visible = true;
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdList.RowCount);
                Frm_tprc_Main.gv.SetStbInfo();
            }
        }

        private void btnColRight_Click(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    x = Frm_tprc_Main.Lib.btnColRight(grdList, x);
                    break;
                case 1:
                    z = Frm_tprc_Main.Lib.btnColRight(grdData, z);
                    break;
            }
        }

        private void btnColLeft_Click(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    x = Frm_tprc_Main.Lib.btnColLeft(grdList, x);
                    break;
                case 1:
                    z = Frm_tprc_Main.Lib.btnColLeft(grdData, z);
                    break;
            }
        }

        private void frm_tins_InspectAutoResult_Q_Activated(object sender, EventArgs e)
        {
            ((Frm_tprc_Main)(MdiParent)).LoadRegistry();
            txtPLotID.Text = gs.GetValue("Work", "SetLOTID", "");
            if (txtPLotID.Text != string.Empty)
            {
                chkPLotID.Checked = true;
                FillGridList();
            }
            else
            {
                chkPLotID.Checked = false;
                FillGridList();
            }
        }

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
