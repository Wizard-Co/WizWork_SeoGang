using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

//*******************************************************************************
//프로그램명    frm_PopUp_BringSplitData.cs
//메뉴ID        
//설명          frm_PopUp_BringSplitData 메인소스입니다.
//작성일        2020.03.09
//개발자        허윤구
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************

//*******************************************************************************

namespace WizWork
{
    public partial class frm_PopUp_BringSplitData : Form
    {      

        string[] Message = new string[2];  // 메시지박스 처리용도.
        string Wh_Ar_ArticleID = string.Empty;      // ArticleID. 
                                                    // SPLIT 중 이 ARTICLE을 가진 녀석들만 조회되어야 한다.

        int z = 0; //수직 스크롤바 이동용 변수

        public delegate void TextEventHandler(string Sum_SplitUsingQty);     // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;                        // 대리자 타입의 이벤트 처리기를 설정합니다. 
        WizWorkLib Lib = new WizWorkLib();


        public frm_PopUp_BringSplitData()
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
        }
        public frm_PopUp_BringSplitData(string ArticleID)
        {
            InitializeComponent();
            Wh_Ar_ArticleID = ArticleID;

            SetScreen();  //TLP 사이즈 조정
        }

        #region 테이블 레이아웃 패널 사이즈 조정
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


        // 로드 이벤트.
        private void frm_PopUp_BringSplitData_Load(object sender, EventArgs e)
        {
            // 1. 일단 클리어.
            SetFormDataClear();

            // 2. InitGrid.
            InitGridData();

            // 3. ArticleID가 살아있다면, Form_Activate
            if (Wh_Ar_ArticleID != string.Empty)
            {
                Form_Activate();
            }

            // 4. 기존 Split 데이터와 일치하는 정보가 있다면 자동체크.
            Pre_Split_Reflect();

        }

        #region 전체 클리어
        private void SetFormDataClear()
        {
            txtCheckCount.TextAlign = HorizontalAlignment.Right;
            txtTotalQty.TextAlign = HorizontalAlignment.Right;

            this.txtArticle.Text = string.Empty;
            this.txtCheckCount.Text = "0";
            this.txtTotalQty.Text = "0";

            GridData.Rows.Clear();
        }

        #endregion

        #region 그리드 세팅 (InitGridData)
        private void InitGridData()
        {
            int i = 0;
            GridData.Columns.Clear();
            GridData.ColumnCount = 11;

            // Set the Colums Hearder Names
            GridData.Columns[i].Name = "RowSeq";
            GridData.Columns[i].HeaderText = "No";
            //GridData2.Columns[i].Width = 30;
            GridData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData.Columns[i].ReadOnly = true;
            GridData.Columns[i++].Visible = true;

            GridData.Columns[i].Name = "Qty";
            GridData.Columns[i].HeaderText = "수량";
            GridData.Columns[i].Width = 80;
            GridData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData.Columns[i].ReadOnly = true;
            GridData.Columns[i++].Visible = true;

            GridData.Columns[i].Name = "WorkPerson";
            GridData.Columns[i].HeaderText = "작업자";
            GridData.Columns[i].Width = 100;
            GridData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData.Columns[i].ReadOnly = true;
            GridData.Columns[i++].Visible = true;

            GridData.Columns[i].Name = "WorkEndDate";
            GridData.Columns[i].HeaderText = "작업일자";
            GridData.Columns[i].Width = 170;
            GridData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData.Columns[i].ReadOnly = true;
            GridData.Columns[i++].Visible = true;

            GridData.Columns[i].Name = "WorkEndTime";
            GridData.Columns[i].HeaderText = "작업시간";
            GridData.Columns[i].Width = 170;
            GridData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData.Columns[i].ReadOnly = true;
            GridData.Columns[i++].Visible = true;

            GridData.Columns[i].Name = "JobID";
            GridData.Columns[i].HeaderText = "JobID";
            //GridData.Columns[i].Width = 120;
            GridData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData.Columns[i].ReadOnly = true;
            GridData.Columns[i++].Visible = false;

            GridData.Columns[i].Name = "LabelID";
            GridData.Columns[i].HeaderText = "LabelID";
            //GridData.Columns[i].Width = 120;
            GridData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData.Columns[i].ReadOnly = true;
            GridData.Columns[i++].Visible = false;

            GridData.Columns[i].Name = "ArticleID";
            GridData.Columns[i].HeaderText = "ArticleID";
            //GridData.Columns[i].Width = 120;
            GridData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData.Columns[i].ReadOnly = true;
            GridData.Columns[i++].Visible = false;

            GridData.Columns[i].Name = "WorkPersonID";
            GridData.Columns[i].HeaderText = "WorkPersonID";
            //GridData.Columns[i].Width = 120;
            GridData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData.Columns[i].ReadOnly = true;
            GridData.Columns[i++].Visible = false;

            GridData.Columns[i].Name = "InstID";
            GridData.Columns[i].HeaderText = "InstID";
            //GridData.Columns[i].Width = 120;
            GridData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData.Columns[i].ReadOnly = true;
            GridData.Columns[i++].Visible = false;


            GridData.Font = new Font("맑은 고딕", 15);//, FontStyle.Bold);
            GridData.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 12);//, FontStyle.Bold);
            GridData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridData.RowTemplate.Height = 35;
            GridData.ColumnHeadersHeight = 35;
            GridData.ScrollBars = ScrollBars.Both;
            GridData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridData.MultiSelect = false;
            GridData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridData.ReadOnly = true;
            GridData.RowHeadersVisible = false;
            GridData.AllowUserToResizeRows = false;


            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            {
                chkCol.HeaderText = "";
                chkCol.Name = "Check";
                chkCol.Width = 40;
                //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                chkCol.FlatStyle = FlatStyle.Standard;
                chkCol.ThreeState = true;
                chkCol.CellTemplate = new DataGridViewCheckBoxCell();
                chkCol.CellTemplate.Style.BackColor = Color.Beige;
                chkCol.Visible = true;
            }
            GridData.Columns.Insert(1, chkCol);

            foreach (DataGridViewColumn col in GridData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;

        }

        #endregion

        #region Form_Activate (Form_Activate)
        // Form_Activate()
        private void Form_Activate()
        {
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("ArticleID", Wh_Ar_ArticleID);

            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sBringSplitData", sqlParameter, false);

            if (dt != null && dt.Rows.Count > 0)
            {
                int i = 1;
                double WorkQty = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    double.TryParse(dr["WorkQty"].ToString(), out WorkQty);

                    GridData.Rows.Add(
                                        i++,
                                        false,
                                        string.Format("{0:n0}", (int)WorkQty),
                                        Lib.CheckNull(dr["Name"].ToString()),
                                        Lib.MakeDate(WizWorkLib.DateTimeClss.DF_MID, dr["WorkEndDate"].ToString()),

                                        Lib.MakeDateTime("HHmmSS", dr["WorkEndTime"].ToString().Trim()),
                                        Lib.CheckNull(dr["JobID"].ToString()),
                                        Lib.CheckNull(dr["LabelID"].ToString()),
                                        Lib.CheckNull(dr["ArticleID"].ToString()),
                                        Lib.CheckNull(dr["WorkPersonID"].ToString()),

                                        Lib.CheckNull(dr["InstID"].ToString())

                        );

                }
                txtArticle.Text = dt.Rows[0]["Article"].ToString();
            }
        }

        #endregion

        #region 기존 데이터 확인. 자동체크해주기 (Pre_Split_Reflect)
        private void Pre_Split_Reflect()
        {
            if (Frm_tprc_Main.list_g_tsplit.Count > 0)
            {
                // 기존에 반영되어있던 불량내역을 여기서 간접적으로 되살리고,

                string pre_JobID = string.Empty;
                // 
                foreach (TSplit TS in Frm_tprc_Main.list_g_tsplit)
                {
                    pre_JobID = string.Empty;

                    pre_JobID = TS.JobID.ToString();

                    for (int k = 0; k < GridData.Rows.Count; k++)
                    {
                        if (GridData.Rows[k].Cells["JobID"].Value.ToString() == pre_JobID)
                        {
                            GridData.Rows[k].Cells["Check"].Value = true;
                        }
                    }
                }
                Calculator_CountNCheck();
            }
        }

        #endregion





        // 위 버튼
        private void btnUp_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowUp(GridData, z);
        }
        // 아래 버튼
        private void btnDown_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowDown(GridData, z);
        }


        #region 체크이벤트

        private void GridData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (GridData.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    GridData.Rows[e.RowIndex].Cells["Check"].Value = true;
                }
                else if (GridData.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    GridData.Rows[e.RowIndex].Cells["Check"].Value = false;
                }

                Calculator_CountNCheck();
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            DataGridView dgv = GridData;
            
            if (dgv.Rows.Count > 0)
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    int iSelRow = 0;
                    for (int i = 0; i < dgv.SelectedCells.Count; i++)
                    {
                        iSelRow = dgv.SelectedCells[i].RowIndex;

                        if (dgv.Rows[iSelRow].Visible)
                        {
                            if (Convert.ToBoolean(dgv.Rows[iSelRow].Cells["Check"].Value) == true)
                            {
                                dgv.Rows[iSelRow].Cells["Check"].Value = false;
                            }
                            else
                            { dgv.Rows[iSelRow].Cells["Check"].Value = true; }

                            Calculator_CountNCheck();
                            break;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {
                        if (dgvr.Visible)
                        {
                            break;
                        }
                    }
                }
            }            
        }


        #endregion

        // 체크 수, 총 수량 계산하기.
        private void Calculator_CountNCheck()
        {
            int CountCheck = 0;
            double TotalQty = 0;
            foreach (DataGridViewRow dgvr in GridData.Rows)
            {
                if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    CountCheck = CountCheck + 1;
                    TotalQty = TotalQty + Lib.GetDouble(dgvr.Cells["Qty"].Value.ToString());
                }
            }

            txtCheckCount.Text = CountCheck.ToString();
            txtTotalQty.Text = TotalQty.ToString();
        }



        // 적용 버튼 클릭.
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Frm_tprc_Main.list_g_tsplit.Clear();

                int i = 0;
                float JobID = 0;
                double Qty = 0;
                foreach (DataGridViewRow dgvr in GridData.Rows)
                {
                    float.TryParse(dgvr.Cells["JobID"].Value.ToString(), out JobID);
                    double.TryParse(dgvr.Cells["Qty"].Value.ToString(), out Qty);

                    if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                    {
                        // 리스트 추가.
                        // 여기 담기는 리스트는 나중에 Update 데이터에 활용할 예정입니다.
                        // 남은잔량이 이번에 합쳐지는 거니까.  ( wk_labelprint, wk_result) (wk_result_split insert)
                        Frm_tprc_Main.list_g_tsplit.Add(new TSplit());
                        Frm_tprc_Main.list_g_tsplit[i].JobID = JobID;
                        Frm_tprc_Main.list_g_tsplit[i].LabelID = Lib.CheckNull(dgvr.Cells["LabelID"].Value.ToString());
                        Frm_tprc_Main.list_g_tsplit[i].ArticleID = Lib.CheckNull(dgvr.Cells["ArticleID"].Value.ToString());
                        Frm_tprc_Main.list_g_tsplit[i].PersonID = Lib.CheckNull(dgvr.Cells["WorkPersonID"].Value.ToString());
                        Frm_tprc_Main.list_g_tsplit[i].Qty = Qty;

                        Frm_tprc_Main.list_g_tsplit[i].InstID = Lib.CheckNull(dgvr.Cells["InstID"].Value.ToString());
                        i++;
                    }
                }
                WriteTextEvent(txtTotalQty.Text);
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            this.Close();
        }



        // 취소 버튼 선택시.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
