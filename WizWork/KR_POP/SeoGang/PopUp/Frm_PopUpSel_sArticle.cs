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
    public partial class Frm_PopUpSel_sArticle : Form
    {
        public delegate void TextEventHandler(string sArticleID, string sArticle);                // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;           // 대리자 타입의 이벤트 처리기를 설정합니다.
        public Frm_PopUpSel_sArticle()
        {
            InitializeComponent();
        }

        private void Frm_PopUpSel_sArticle_Load(object sender, EventArgs e)
        {
            this.Text = "품명 또는 품번 선택";
            this.Size = new Size(457, 499);
            InitGrid();
            SetScreen();
            FillGrid();
        }
        //tableLayoutPanel 세팅
        private void SetScreen()
        {
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Margin = new Padding(0, 0, 0, 0);
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
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(0, 0, 0, 0);
                        }
                    }
                }
            }
        }
        //그리드 컬럼 셋팅
        private void InitGrid()
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 3;

            int i = 0;

            grdData.Columns[i].Name = "IDX";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ArticleID";
            grdData.Columns[i].HeaderText = "품번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
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
            try
            {
                grdData.Rows.Clear();
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@sArticle", txtArticle.Text.Trim());
                sqlParameter.Add("@iIncNotUse", "0");
                sqlParameter.Add("@sArticleGrpID", "3");//3 : 반제품, 5 : 완제품
                sqlParameter.Add("@sSupplyType", "2");//공급유형
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Article_sArticle", sqlParameter, false);
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    grdData.Rows.Add(++i,
                                    dr["ArticleID"].ToString(),
                                    dr["Article"].ToString());
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
            this.Dispose();
            this.Close();
        }
        //row 위로
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
        //row 아래로
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
            if (grdData.Rows.Count == 0 || grdData.SelectedRows.Count == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 품명이 없습니다. 품명을 선택해주세요.", "[오류]", 0, 1);
                return;
            }
            //전역변수에 사용자 저장
            string sArticleID = grdData.SelectedRows[0].Cells["ArticleID"].Value.ToString();
            string sArticle = grdData.SelectedRows[0].Cells["Article"].Value.ToString();
            //Frm_tprc_Main.g_tBase.sArticleID = grdData.SelectedRows[0].Cells["ArticleID"].Value.ToString();
            //Frm_tprc_Main.g_tBase.sArticle = grdData.SelectedRows[0].Cells["Article"].Value.ToString();
            WriteTextEvent(sArticleID, sArticle);
            this.Dispose();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnArticle_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_CMKeypad FK = new WizCommon.Popup.Frm_CMKeypad(txtArticle.Text.Trim(), "품번");

            if (FK.ShowDialog() == DialogResult.OK)
            {
                txtArticle.Text = FK.tbInputText.Text;
                FillGrid();
            }
        }

        private void chkArticle_Click(object sender, EventArgs e)
        {

        }
    }
}
