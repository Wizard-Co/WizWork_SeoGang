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
    public partial class Frm_PopUpSel_sProcess : Form
    {
        WizWorkLib Lib = new WizWorkLib();
        string sProcessID = string.Empty;
        public delegate void TextEventHandler(string ProcessID, string Prcoess);                // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;           // 대리자 타입의 이벤트 처리기를 설정합니다.

        public Frm_PopUpSel_sProcess()
        {
            InitializeComponent();
        }

        private void Frm_PopUpSel_sProcess_Load(object sender, EventArgs e)
        {
            this.Text = "공정 선택";
            this.Size = new Size(457, 499);
            InitGrid();
            SetScreen();
            FillGrid();
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

            grdData.Columns[++i].Name = "ProcessID";
            grdData.Columns[i].HeaderText = "코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Process";
            grdData.Columns[i].HeaderText = "공정명";
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

        private void FillGrid()
        {
            try
            {
                Tools.INI_GS gs = new Tools.INI_GS();
                string strProcessID =Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID");
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Work_sProcess.NCHKPROC, "1");
                sqlParameter.Add(Work_sProcess.PROCESSID, strProcessID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_Work_sProcess]", sqlParameter, false);

                
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    grdData.Rows.Add(++i,
                                    dr["ProcessID"].ToString(),
                                    dr["Process"].ToString());
                }
            }

            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            Lib.btnRowUp(grdData);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            Lib.btnRowDown(grdData);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (grdData.Rows.Count == 0 || grdData.CurrentRow is null)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 공정이 없습니다. 공정을 선택해주세요.", "[오류]", 0, 1);
                return;
            }
            string sProcessID = grdData.SelectedRows[0].Cells["ProcessID"].Value.ToString(); ;
            string sProcess = grdData.SelectedRows[0].Cells["Process"].Value.ToString(); ;
            //전역변수에 공정저장
            //Frm_tprc_Main.g_tBase.ProcessID = sProcessID;
            //Frm_tprc_Main.g_tBase.Process = sProcess;
            WriteTextEvent(sProcessID, sProcess);
            this.Dispose();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

    }
}
