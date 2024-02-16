using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.Popup
{
    public partial class Frm_tins_LabelRegister : Form
    {
        public Frm_tins_LabelRegister()
        {
            InitializeComponent();
        }

        private void Frm_tins_LabelRegister_Load(object sender, EventArgs e)
        {
            SetScreen();
            InitGrid();
        }
        private void SetScreen()
        {
            foreach (Control con in this.Controls)
            {
                con.Dock = DockStyle.Fill;
                foreach (Control ctl in con.Controls)
                {
                    ctl.Dock = DockStyle.Fill;
                    foreach (Control ct in ctl.Controls)
                    {
                        ct.Dock = DockStyle.Fill;
                        foreach (Control c in ct.Controls)
                        {
                            c.Dock = DockStyle.Fill;
                        }
                    }
                }
            }
        }

        private void InitGrid()
        {
            //grdCardList

            grdCardList.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdCardList.ColumnCount = 4;

            int i = 0;

            grdCardList.Columns[i].Name = "Idx";
            grdCardList.Columns[i].HeaderText = "";
            grdCardList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdCardList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdCardList.Columns[i].ReadOnly = true;
            grdCardList.Columns[i].Visible = true;

            grdCardList.Columns[i].Name = "CardID";
            grdCardList.Columns[i].HeaderText = "공정이동전표ID";
            grdCardList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdCardList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdCardList.Columns[i].ReadOnly = true;
            grdCardList.Columns[i].Visible = true;

            grdCardList.Columns[i].Name = "WorkQty";
            grdCardList.Columns[i].HeaderText = "생산수량\r\n(마지막공정)";
            grdCardList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdCardList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdCardList.Columns[i].ReadOnly = true;
            grdCardList.Columns[i].Visible = true;

            grdCardList.Columns[i].Name = "OrderID";
            grdCardList.Columns[i].HeaderText = "관리번호";
            grdCardList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdCardList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdCardList.Columns[i].ReadOnly = true;
            grdCardList.Columns[i].Visible = true;



            grdCardList.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            grdCardList.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10F);
            grdCardList.RowTemplate.Height = 30;
            grdCardList.ColumnHeadersHeight = 35;
            grdCardList.ScrollBars = ScrollBars.Both;
            grdCardList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdCardList.MultiSelect = false;
            grdCardList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdCardList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdCardList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdCardList.MultiSelect = true;

            foreach (DataGridViewColumn col in grdCardList.Columns)
            {
                col.DataPropertyName = col.Name;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
