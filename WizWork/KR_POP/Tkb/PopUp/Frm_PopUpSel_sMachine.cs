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
    public partial class Frm_PopUpSel_sMachine : Form
    {
        string sProcessID = string.Empty;
        public delegate void TextEventHandler();                // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;           // 대리자 타입의 이벤트 처리기를 설정합니다.
        //WizWorkLib Lib = new WizWorkLib();
        List<CB_IDNAME> list_cbx = new List<CB_IDNAME>();

        public Frm_PopUpSel_sMachine()
        {
            InitializeComponent();
        }

        public Frm_PopUpSel_sMachine(string strProcessID)
        {
            InitializeComponent();
            this.sProcessID = strProcessID.Substring(0, 2) + "01";
        }

        private void Frm_PopUpSel_sMachine_Load(object sender, EventArgs e)
        {
            this.Text = "설비 선택";
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

            grdData.Columns[++i].Name = "MachineID";
            grdData.Columns[i].HeaderText = "코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Machine";
            grdData.Columns[i].HeaderText = "설비명";
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
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("sPROCESSID", sProcessID);             
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Process_sMachine", sqlParameter, false);
                DataTable dt2 = dt.Clone();
                string[] sMachineID = null;
                Tools.INI_GS gs = new Tools.INI_GS();
                sMachineID =Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine").Split('|');//배열에 설비아이디 넣기
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
                            if (Mac.Substring(0, 4) == sProcessID)
                            {
                                if (dr["MachineID"].ToString() == Mac.Substring(4, 2))
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
                        sMachine.Remove(sProcessID + dr["MachineID"].ToString());
                    }
                }

                int i = 0;
                foreach (DataRow dr in dt2.Rows)
                {
                    grdData.Rows.Add(++i,
                                    dr["MachineID"].ToString(),
                                    dr["Machine"].ToString());
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                if (iSelRow == grdData.Rows.Count - 1)
                {
                    grdData[0, iSelRow + 1].Selected = true;
                    break;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (grdData.Rows.Count == 0 || grdData.CurrentRow is null)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 사용자가 없습니다. 사용자를 선택해주세요.", "[오류]", 0, 1);
                return;
            }
            //전역변수에 사용자 저장
            Frm_tprc_Main.g_tBase.MachineID = grdData.SelectedRows[0].Cells["MachineID"].Value.ToString();
            Frm_tprc_Main.g_tBase.Machine = grdData.SelectedRows[0].Cells["Machine"].Value.ToString();
            WriteTextEvent();
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
