using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using WizCommon;

namespace WizWork
{
    public partial class Frm_PopUpSel_sArticle_O : Form
    {
        WizWorkLib Lib = new WizWorkLib();
        public delegate void TextEventHandler(string sArticleID, string sArticle, string sOrderArticleID);                // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;           // 대리자 타입의 이벤트 처리기를 설정합니다.
        DataTable dt = null; //화면 로드 시 전체 dt 가져옴. 전역변수로 검색 시 마다 dt에서 조건 줘서 검색함.
        //DataTable tempdt = null; //조건 검색 row 담을 dt
        DataRow[] dr = null; //조건 검색용 datarow, 매검색시 마다 담아서 사용
        public Frm_PopUpSel_sArticle_O()
        {
            InitializeComponent();
        }

        public Frm_PopUpSel_sArticle_O(string Article)
        {
            InitializeComponent();
            txtArticle.Text = Article;
        }

        private void Frm_PopUpSel_sArticle_Load(object sender, EventArgs e)
        {
            Text = "품명/품번";
            Size = new Size(600, 499);
            InitGrid();
            SetScreen();
            FillGrid();
            if (txtArticle.Text != "")
            {
                FindFillGrid();
                txtArticle.Select();
                txtArticle.Focus();
            }
            txtArticle.Select();
            txtArticle.Focus();
        }
        //tableLayoutPanel 세팅
        private void SetScreen()
        {
            pnlForm.Dock = DockStyle.Fill;
            pnlForm.Margin = new Padding(0, 0, 0, 0);
            foreach (Control control in pnlForm.Controls)
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
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(0, 0, 0, 0);
                            foreach (Control con in cont.Controls)
                            {
                                con.Dock = DockStyle.Fill;
                                con.Margin = new Padding(0, 0, 0, 0);
                            }
                        }
                    }
                }
            }
        }
        //그리드 컬럼 셋팅
        private void InitGrid()
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 4;

            int i = 0;

            grdData.Columns[i].Name = "IDX";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].Width = 80;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ArticleID";
            grdData.Columns[i].HeaderText = "ArticleID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "품번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

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
            try
            {
                grdData.Rows.Clear();
                dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sArticleOnlyAFT", null, false);
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    grdData.Rows.Add(++i,
                                    dr["ArticleID"].ToString(),
                                    dr["Article"].ToString(),
                                    dr["BuyerArticleNo"].ToString()
                                    );
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }
        //닫기
        private void btnClose_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }
        private void btnUp_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowUp(grdData);
            txtArticle.Select();
            txtArticle.Focus();
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowDown(grdData);
            txtArticle.Select();
            txtArticle.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (grdData.Rows.Count == 0 || grdData.SelectedRows.Count == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 품명 또는 사번이 없습니다. 품명 또는 사번을 선택해주세요.", "[오류]", 0, 1);
                return;
            }
            string sArticleID = grdData.SelectedRows[0].Cells["ArticleID"].Value.ToString();
            string sArticle = grdData.SelectedRows[0].Cells["Article"].Value.ToString();
            string sSabun = grdData.SelectedRows[0].Cells["BuyerArticleNo"].Value.ToString();
            WriteTextEvent(sArticleID, sArticle, sSabun);
            Dispose();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FindFillGrid();
            txtArticle.Select();
            txtArticle.Focus();
        }

        private void txtArticle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                FindFillGrid();
            }
        }

        private void FindFillGrid()
        {
            try
            {         
                dr = dt.Select("BuyerArticleNo like '%" + txtArticle.Text + "%' or Article like '%" + txtArticle.Text + "%'" , "BuyerArticleNo DESC, ArticleID DESC");
                if (dr.Length > 0)
                {
                    grdData.Rows.Clear();
                    int i = 0;
                    foreach (DataRow drw in dr)
                    {
                        grdData.Rows.Add(++i,
                                        drw["ArticleID"].ToString(),
                                        drw["Article"].ToString(),
                                        drw["BuyerArticleNo"].ToString()
                                        );
                    }
                }
                
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }

        private void chkArticle_Click(object sender, EventArgs e)
        {
            //실행중인 프로세스가 없을때 
            if (!Frm_tprc_Main.Lib.ReturnProcessRunStop("osk"))
            {
                System.Diagnostics.Process ps = new System.Diagnostics.Process();
                ps.StartInfo.FileName = "osk.exe";
                ps.Start();
            }
            txtArticle.Select();
            txtArticle.Focus();
        }
    }
}

