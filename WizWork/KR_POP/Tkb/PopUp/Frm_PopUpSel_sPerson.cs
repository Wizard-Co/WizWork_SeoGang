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
    public partial class Frm_PopUpSel_sPerson : Form
    {
        string sProcessID = string.Empty;
        public delegate void TextEventHandler();                // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;           // 대리자 타입의 이벤트 처리기를 설정합니다.
        //WizWorkLib Lib = new WizWorkLib();
        List<CB_IDNAME> list_cbx = new List<CB_IDNAME>();

        public Frm_PopUpSel_sPerson()
        {
            InitializeComponent();
        }

        public Frm_PopUpSel_sPerson(string strProcessID)
        {
            InitializeComponent();
            this.sProcessID = strProcessID.Substring(0, 2) + "01";
        }

        private void Frm_PopUpSel_sPerson_Load(object sender, EventArgs e)
        {
            this.Text = "사용자 선택";
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

            grdData.Columns[++i].Name = "PersonID";
            grdData.Columns[i].HeaderText = "코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Name";
            grdData.Columns[i].HeaderText = "작업자";
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
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("CHKPROCESS", "1");//1넣으면 공정값 넣어줘야함
                sqlParameter.Add("PROCESSID", sProcessID);
                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_Person_sPersonByProcess", sqlParameter, false) as DataSet;//공정별 사용자 선택 시 null 대신 sqlParameter 추가
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    grdData.Rows.Add(++i,
                                    dr["PersonID"].ToString(),
                                    dr["Name"].ToString());
                }

            }

            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection();
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
                if (iSelRow < grdData.Rows.Count - 1)
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
            Frm_tprc_Main.g_tBase.PersonID = grdData.SelectedRows[0].Cells["PersonID"].Value.ToString();
            Frm_tprc_Main.g_tBase.Person = grdData.SelectedRows[0].Cells["Name"].Value.ToString();
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
