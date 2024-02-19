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
    public partial class Frm_PopUp_sJobIDBy0405 : Form
    {
        WizWorkLib Lib = new WizWorkLib();
        public delegate void TextEventHandler(string JobID);                // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;           // 대리자 타입의 이벤트 처리기를 설정합니다.
        public Frm_PopUp_sJobIDBy0405()
        {
            InitializeComponent();
            SetScreen();
            InitGrid();
        }

        private void SetScreen()
        {
            pnlForm.Dock = DockStyle.Fill;
            foreach (Control control in pnlForm.Controls)
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
                        }
                    }
                }
            }
        }

        //그리드 컬럼 셋팅
        private void InitGrid()
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 9;

            int i = 0;

            grdData.Columns[i].Name = "RowSeq";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "JobID";
            grdData.Columns[i].HeaderText = "JobID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "WorkEndDate";
            grdData.Columns[i].HeaderText = "작업날짜";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "WorkEndTime";
            grdData.Columns[i].HeaderText = "작업시간";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "OrderArticleID";
            grdData.Columns[i].HeaderText = "품목코드";
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
            
            grdData.Columns[++i].Name = "WorkQty";
            grdData.Columns[i].HeaderText = "작업량(kg)";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Name";
            grdData.Columns[i].HeaderText = "작업자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "UseYN";
            grdData.Columns[i].HeaderText = "UseYN";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdData.ReadOnly = true;
            grdData.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public bool FillGrid()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("InstID", Frm_tprc_Main.g_tBase.sInstID);
                DateTime date = DateTime.Now;
                
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sWkResultBy0405", sqlParameter, false);
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    date = DateTime.ParseExact(dr["WorkEndDate"].ToString() + dr["WorkEndTime"].ToString(), "yyyyMMddHHmmss", null);
                    grdData.Rows.Add(++i,
                                    dr["JobID"].ToString(),
                                    date.ToString("yyyy-MM-dd"),
                                    date.ToString("HH:mm:ss"),
                                    //dr["WorkEndDate"].ToString(),
                                    //dr["WorkEndTime"].ToString(),
                                    dr["OrderArticleID"].ToString(),
                                    dr["Article"].ToString(),
                                    dr["WorkQty"].ToString(),
                                    dr["Name"].ToString(),
                                    dr["UseYN"].ToString()
                                    );
                    if (grdData.Rows[i - 1].Cells["UseYN"].Value.ToString() == "Y")
                    {
                        grdData.Rows[i - 1].DefaultCellStyle.BackColor = Color.FromArgb(238, 108, 128);
                    }
                }
                int count = 0;
                foreach (DataGridViewRow dgvr in grdData.Rows)
                {
                    if (dgvr.Cells["UseYN"].Value.ToString() == "Y")
                    {
                        count++;
                    }
                }
                
                if (grdData.Rows.Count == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("평량작업 실적이 없습니다. 해당 작업지시의 평량작업을 진행해주세요.", "[작업실적 없음]", 0, 1);
                    return false;
                }
                else if (grdData.Rows.Count == count)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("선택할 수 있는 평량작업 실적이 없습니다. 해당 작업지시의 평량작업을 진행해주세요.", "[선택가능 작업실적 없음]", 0, 1);
                    return false;
                }
                return true;
            }

            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                return false;
            }
        }

        private void Frm_PopUp_sJobIDBy0405_Load(object sender, EventArgs e)
        {
            
            SelectNotWorkRow();
            //if (!FillGrid())
            //{
            //    this.Close();
            //}

        }

        private void SelectNotWorkRow()
        {
            grdData.ClearSelection();
            foreach (DataGridViewRow dgvr in grdData.Rows)
            {
                if (dgvr.Cells["UseYN"].Value.ToString() == "N")
                {
                    dgvr.Selected = true;
                    break;
                }
            }
        }

        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grdData.Rows.Count > 0 && e.RowIndex >= 0)
                {
                    if (grdData.Rows[e.RowIndex].Cells["UseYN"].Value.ToString() == "Y")
                    {
                        if (grdData.Rows.Count - 1 >= e.RowIndex + 1)
                        {
                            grdData.Rows[e.RowIndex + 1].Selected = true;
                        }
                        else
                        {
                            grdData.ClearSelection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
           
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (grdData.Rows.Count == 0 || grdData.CurrentRow is null || grdData.SelectedRows[0] is null)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 평량작업이 없습니다. 평량작업을 선택해주세요.", "[오류]", 0, 1);
                return;
            }

            if (grdData.SelectedRows[0].Cells["UseYN"].Value.ToString() == "Y")
            {
                WizCommon.Popup.MyMessageBox.ShowBox("사용한 평량작업은 선택할 수 없습니다. 다른 평량작업을 선택해주세요.", "[오류]", 0, 1);
                return;
            }
            string JobID = grdData.SelectedRows[0].Cells["JobID"].Value.ToString();
            
            
            this.Close();
            WriteTextEvent(JobID);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                Lib.btnRowUp(grdData);
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            //Lib.btnRowUp(grdData);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                Lib.btnRowDown(grdData);
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
           
        }

        private void grdData_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdData.Rows.Count > 0)
                {
                    int i = 0;
                    if(grdData.SelectedRows != null)
                    {
                        if (grdData.SelectedRows.Count > 0)
                        {
                            i = grdData.SelectedRows[0].Index;
                            if (grdData.Rows[i].Cells["UseYN"].Value.ToString() == "Y")
                            {
                                if (grdData.Rows.Count - 1 >= i + 1)
                                {
                                    grdData.Rows[i + 1].Selected = true;
                                }
                                //if (grdData.Rows.Count > i && !(grdData.Rows.Count == i + 1))
                                //{
                                    
                                //}
                                else
                                {
                                    grdData.ClearSelection();
                                }
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            
            
        }
    }
}
