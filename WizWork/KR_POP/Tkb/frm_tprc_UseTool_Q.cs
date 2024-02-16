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
    public partial class frm_tprc_UseTool_Q : Form
    {
        string[] Message = new string[2];
        INI_GS gs = Frm_tprc_Main.gs;
        WizWorkLib Lib = Frm_tprc_Main.Lib;
        DataTable dt = null;
        int x = 0; //grdlist 그리드 좌우 이동용 변수
        public frm_tprc_UseTool_Q()
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
            
            SerFormClearData(); // 화면 초기화
            SetMCComboBox();

            FillGridList();

            mtb_From.Text = DateTime.Today.ToString("yyyy-MM-dd");
            mtb_To.Text = DateTime.Today.ToString("yyyy-MM-dd");

            btnColLeft.Visible = false;
            btnColRight.Visible = false;
        }

        #region 설비 콤보박스 세팅

        private void SetMCComboBox()
        {
            string strProcessID = "";
            string strMachineID = "";

            //공정 가져오기
            try
            {
                string[] arr = Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine").Split('|');
                foreach (string str in arr)
                {
                    if (str.Trim().Length == 6)
                    {
                        if (strProcessID.Length == 0)
                        {
                            strProcessID += str.Substring(0, 4);
                            strMachineID += str.Substring(4, 2);
                        }
                        else
                        {
                            strProcessID += ("|" + str.Substring(0, 4));
                            strMachineID += ("|" + str.Substring(4, 2));
                        }
                    }
                }


                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Work_sProcess.NCHKPROC, 1);//cboProcess.Text 
                sqlParameter.Add(Work_sProcess.PROCESSID, strProcessID);//cboProcess.Text
                sqlParameter.Add("nchkMac", 1);//cboProcess.Text 
                sqlParameter.Add("MacineID", strMachineID);//cboProcess.Text

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_prdWork_sMCByProcessID_Q", sqlParameter, false);

                DataRow newRow = ds.Tables[0].NewRow();
                newRow["MCID"] = "";
                newRow["MCNAME"] = "전체";

                //DataRow newRow2 = ds.Tables[0].NewRow();
                //newRow2[Work_sProcess.PROCESSID] = strProcessID;
                //newRow2[Work_sProcess.PROCESS] = "부분전체";

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                    cboMC.DataSource = ds.Tables[0];
                }

                cboMC.ValueMember = "MCID";
                cboMC.DisplayMember = "MCNAME";
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        #endregion

        #region 레이아웃에 채우기

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

        #endregion

        #region Default Grid Setting

        private void InitGrid()
        {
            grdList.Columns.Clear();
            grdList.ColumnCount = 11;
            
            int n = 0;
            // Set the Colums Hearder Names

            grdList.Columns[n].Name = "No";
            grdList.Columns[n].HeaderText = "";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "ChangeCheckDate";
            grdList.Columns[n].HeaderText = "교체일";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "ChangeCheckTime";
            grdList.Columns[n].HeaderText = "교체시간";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "McID";
            grdList.Columns[n].HeaderText = "설비ID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "MCName";
            grdList.Columns[n].HeaderText = "설비";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "MCPartID";
            grdList.Columns[n].HeaderText = "교체\r툴명 ID";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "MCPartName";
            grdList.Columns[n].HeaderText = "교체\r툴명";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "CycleProdQty";
            grdList.Columns[n].HeaderText = "작업\r수량";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            grdList.Columns[n].Name = "SetProdQty";
            grdList.Columns[n].HeaderText = "설정\r수명";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "ChangeChasu";
            grdList.Columns[n].HeaderText = "교환차수";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = true;

            grdList.Columns[n].Name = "MCPartChangeID";
            grdList.Columns[n].HeaderText = "툴 교환 이력 ID(PK)";
            grdList.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns[n++].Visible = false;

            //DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            //{
            //    chkCol.HeaderText = "";
            //    chkCol.Name = "Check";
            //    chkCol.Width = 110;
            //    //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //    chkCol.FlatStyle = FlatStyle.Standard;
            //    chkCol.ThreeState = true;
            //    chkCol.CellTemplate = new DataGridViewCheckBoxCell();
            //    chkCol.CellTemplate.Style.BackColor = Color.Beige;
            //    chkCol.Visible = true;
            //}
            //grdList.Columns.Insert(0, chkCol);

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

        private void SerFormClearData()
        {
            SetDateTime();
            mtb_From.Enabled = true;
            mtb_To.Enabled = true;

            //grdData.Rows.Clear();

            //chkArticle.Checked = false;
            //this.txtMC.Text = "";
            //this.txtMC.Enabled = false;

            
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
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("chkDate", chkDate.Checked  == true ? 1 : 0);
                sqlParameter.Add("FromDate", mtb_From.Text.Replace("-", ""));
                sqlParameter.Add("ToDate", mtb_To.Text.Replace("-", ""));
                sqlParameter.Add("chkMcID", cboMC.SelectedValue == null || cboMC.SelectedValue.ToString().Trim().Equals("") ? 0 : 1);
                sqlParameter.Add("sMcID", cboMC.SelectedValue != null ? cboMC.SelectedValue.ToString() : "");

                dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_MCPartChange_s", sqlParameter, false);
                grdList.Rows.Clear();

                int index = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    index++;
                   
                    grdList.Rows.Add(
                                    index.ToString(),
                                    dr["ChangeCheckDate"].ToString().Trim(),
                                    dr["ChangeCheckTime"].ToString().Trim(),
                                    dr["McID"].ToString().Trim(),
                                    dr["MCName"].ToString().Trim(),
                                    dr["MCPartID"].ToString().Trim(),
                                    dr["MCPartName"].ToString().Trim(),
                                    dr["CycleProdQty"].ToString().Trim(),
                                    dr["SetProdQty"].ToString().Trim(),
                                    dr["ChangeChasu"].ToString().Trim(),
                                    dr["MCPartChangeID"].ToString().Trim()
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

       
        #endregion

        /// <summary>
        /// Grid 위로 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUp_Click(object sender, EventArgs e)
        {
            Lib.btnRowUp(grdList, x);
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            Lib.btnRowDown(grdList, x);
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {

            try
            {
                string MCPartChangeID = grdList.SelectedRows[0].Cells[10].FormattedValue.ToString();

                // MessageBox.Show("선택하신 항목을 삭제하시겠습니까?", "삭제 전 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes

                if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
                {
                    if (DeleteData(MCPartChangeID))
                    {
                        FillGridList();
                    }
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        private bool DeleteData(string strID)
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();

                sqlParameter.Add("MCPartChangeID", strID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_MCPartChange_d", sqlParameter, false);

                WizCommon.Popup.MyMessageBox.ShowBox("삭제가 완료되었습니다.", "[삭제]", 0, 1);

                if (dt is null || dt.Rows.Count == 0 || dt.Rows.Count > 1)
                {
                    return true;
                }

            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }

            return flag;
        }

        private void btnArticle_Click(object sender, EventArgs e)
        {
            
        }

        private void btnArticleClear_Click(object sender, EventArgs e)
        {
            
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

   

        private void chkArticle_Click(object sender, EventArgs e)
        {
            
        }

        private void btnColRight_Click(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    x = Frm_tprc_Main.Lib.btnColRight(grdList, x);
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
            }
        }

        private void frm_tins_InspectAutoResult_Q_Activated(object sender, EventArgs e)
        {
            //((Frm_tprc_Main)(MdiParent)).LoadRegistry();
            //txtPLotID.Text = gs.GetValue("Work", "SetLOTID", "");
            //if (txtPLotID.Text != string.Empty)
            //{
            //    chkPLotID.Checked = true;
            //    FillGridList();
            //}
            //else
            //{
            //    chkPLotID.Checked = false;
            //    FillGridList();
            //}
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

        // 설비가 선택이 변경되었을 때 자동 검색
        private void cboMC_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboMC.SelectedValue != null)
            {
                FillGridList();
            }
        }
    }
}
