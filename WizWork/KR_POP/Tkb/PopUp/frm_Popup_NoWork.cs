using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizWork.HIT
{
    public partial class frm_Popup_NoWork : Form
    {
        public frm_Popup_NoWork()
        {
            InitializeComponent();
        }
        #region Default Grid Setting

        private void InitGrid()
        {
            grdData.Columns.Clear();
            grdData.ColumnCount = 8;
            int n = 0;

            // Set the Colums Hearder Names
            grdData.Columns[n].Name = "RowSeq";
            grdData.Columns[n].HeaderText = "No";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "InstID";
            grdData.Columns[n].HeaderText = "InstID";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "InstDetSeq";
            grdData.Columns[n].HeaderText = "InstDetSeq";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "ProcessID";
            grdData.Columns[n].HeaderText = "ProcessID";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "MachineID";
            grdData.Columns[n].HeaderText = "MachineID";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "JobGbn";
            grdData.Columns[n].HeaderText = "JobGbn";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "WorkStartDate";
            grdData.Columns[n].HeaderText = "시작일";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "WorkStartTime";
            grdData.Columns[n].HeaderText = "시작시간";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "WorkEndDate";
            grdData.Columns[n].HeaderText = "종료일";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "WorkEndTime";
            grdData.Columns[n].HeaderText = "종료시간";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "NoReworkCode";
            grdData.Columns[n].HeaderText = "NoReworkCode";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "NoReworkReason";
            grdData.Columns[n].HeaderText = "NoReworkReason";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[n].ReadOnly = true;
            grdData.Columns[n++].Visible = true;
            

            grdData.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }
        #endregion
        private void FillGrid()
        {
            //[xp_WizWork_sWkResultNoWorkByMachine]
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("@ProcessID", Frm_tprc_Main.g_tBase.ProcessID/*"PL2018081300014"*/);
            sqlParameter.Add("@MachineID", Frm_tprc_Main.g_tBase.MachineID/*"PL2018081300014"*/);

            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Work_sQPointbyLotID", sqlParameter, false);

            foreach (DataRow dr in dt.Rows)
            {
                
            }
        }
    }
}
