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
    public partial class frm_trpc_MtrLossAlarm_Q : Form
    {
        INI_GS gs = new INI_GS();
        string[] sMachineID = null;
        WizWorkLib Lib = new WizWorkLib();
        string[] Message = new string[2];//메시지박스용 배열
        string sProcessID = string.Empty;
        public delegate void TextEventHandler();                // string을 반환값으로 갖는 대리자를 선언합니다.
        //public event TextEventHandler WriteTextEvent;           // 대리자 타입의 이벤트 처리기를 설정합니다.
        //WizCommon.Popup.Frm_CMKeypad FK = null;
        public frm_trpc_MtrLossAlarm_Q()
        {
            InitializeComponent();
        }
        
        private void frm_trpc_MtrLossAlarm_Q_Load(object sender, EventArgs e)
        {
            this.Text = "성형자재부족 요청조회";
            SetScreen();
            SetDateTime();
            InitGrid();
            FillGrid();
            btnReq_Click(null, null);
        }
        #region 달력 From값 입력 // 달력 창 띄우기
        private void mtb_From_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_tins_Calendar calendar = new WizCommon.Popup.Frm_tins_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_tins_Calendar.TextEventHandler(GetDate);
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
            WizCommon.Popup.Frm_tins_Calendar calendar = new WizCommon.Popup.Frm_tins_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_tins_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion
        #region Default Grid Setting
        private void InitGrid()
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 13;

            int i = 0;
            //DataGridViewCheckBoxColumn curCol = new DataGridViewCheckBoxColumn();
            //curCol.HeaderText = "선택";
            //curCol.Name = "Check";
            //curCol.Width = 50;
            //curCol.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grdData.Columns.Insert(0, curCol);

            grdData.Columns[i].Name = "RowSeq";
            grdData.Columns[i].HeaderText = "No";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;


            grdData.Columns[i].Name = "ReqStus";
            grdData.Columns[i].HeaderText = "상태";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            grdData.Columns[i].Name = "WorkAlarmID";
            grdData.Columns[i].HeaderText = "WorkAlarmID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = false;

            grdData.Columns[i].Name = "Sabun";
            grdData.Columns[i].HeaderText = "사번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            grdData.Columns[i].Name = "ArticleID";
            grdData.Columns[i].HeaderText = "품명코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = false;

            grdData.Columns[i].Name = "Article";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = false;

            grdData.Columns[i].Name = "ProcessID";
            grdData.Columns[i].HeaderText = "ProcessID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = false;

            grdData.Columns[i].Name = "Process";
            grdData.Columns[i].HeaderText = "공정명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = false;

            grdData.Columns[i].Name = "MachineID";
            grdData.Columns[i].HeaderText = "MachineID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = false;

            grdData.Columns[i].Name = "Machine";
            grdData.Columns[i].HeaderText = "설비명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            grdData.Columns[i].Name = "AlarmStartDateTime";
            grdData.Columns[i].HeaderText = "요청시간";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            //grdData.Columns[++i].Name = "AlarmStartTime";
            //grdData.Columns[i].HeaderText = "요청시간";
            //grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdData.Columns[i].ReadOnly = true;
            //grdData.Columns[i].Visible = true;
            
            grdData.Columns[i].Name = "AlarmEndDateTime";
            grdData.Columns[i].HeaderText = "완료시간";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            //grdData.Columns[++i].Name = "AlarmEndTime";
            //grdData.Columns[i].HeaderText = "처리시간";
            //grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdData.Columns[i].ReadOnly = true;
            //grdData.Columns[i].Visible = false;

            grdData.Columns[i].Name = "Comments";
            grdData.Columns[i].HeaderText = "비고";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;


            grdData.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            //grdData.DefaultCellStyle.ForeColor = Color.White;
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
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
        }

        #endregion

       

        private void SetScreen()
        {
            tlpForm.Dock = DockStyle.Fill;
            foreach (Control control in tlpForm.Controls)
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
                                foreach (Control co in con.Controls)
                                {
                                    co.Dock = DockStyle.Fill;
                                    co.Margin = new Padding(0, 0, 0, 0);
                                    foreach (Control ctl in co.Controls)
                                    {
                                        ctl.Dock = DockStyle.Fill;
                                        ctl.Margin = new Padding(0, 0, 0, 0);
                                        foreach (Control ct in ctl.Controls)
                                        {
                                            ct.Dock = DockStyle.Fill;
                                            ct.Margin = new Padding(0, 0, 0, 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
       
        private void btnDate_Click(object sender, EventArgs e)
        {

        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            Lib.btnRowUp(grdData);
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            Lib.btnRowDown(grdData);
        }
        private void FillGrid()
        {
            //ini에서 날짜기간 가져와서 파라미터로 던져주면 (현재날짜 - ini날짜기간) ~ (현재날짜)로 검색
            //ini값이 없을 경우 기본적으로 하루전 - 금일 로 검색한다.
            //[xp_WizWork_swkWorkAlarmMtr]
            //int SetDateTime()
            //{
            //    ////ini 날짜 불러와서 기간 설정하기
            //    int Days = 0;
            //    string[] sInstDate = Frm_tprc_Main.gs.GetValue("Work", "Screen", "Screen").Split('|');
            //    foreach (string str in sInstDate)
            //    {
            //        string[] Value = str.Split('/');
            //        if (this.Name.ToUpper().Contains(Value[0].ToUpper()))
            //        {
            //            int.TryParse(Value[1], out Days);
            //            if (Days == 0)
            //            {
            //                Days = 1;
            //            }
            //            break;
            //        }
            //    }
            //    return Days;
            //}
            try
            {
                grdData.Rows.Clear();
                //int nFromDate = 1;
                int nChkDate = 0;
                string chkReqStus = "";
                if (chkDate.Checked)
                {
                    nChkDate = 1;
                }
                foreach (Control chkCon in tlpChk.Controls)
                {
                    if (chkCon is CheckBox)
                    {
                        CheckBox cb = chkCon as CheckBox;
                        if (cb.Checked)
                        {
                            if (chkReqStus == "")
                            {
                                chkReqStus = chkCon.Tag.ToString();
                            }
                            else
                            {
                                chkReqStus = chkReqStus + "|" + chkCon.Tag.ToString();
                            }
                        }
                    }
                }
                
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //sqlParameter.Add("FromDate", SetDateTime());
                sqlParameter.Add("@nChkDate", nChkDate);
                sqlParameter.Add("@FromDate", mtb_From.Text.Replace("-", ""));
                sqlParameter.Add("@ToDate", mtb_To.Text.Replace("-", ""));
                sqlParameter.Add("@ReqStus", chkReqStus);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_swkWorkAlarmMtr", sqlParameter, false);
                string ReqStus = string.Empty;
                string StartDateTime = string.Empty;
                string EndDateTime = string.Empty;
                int i = 0;
                int nReq = 0;
                int nCnc = 0;
                int nFin = 0;
                int nTot = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ReqStus"].ToString() == "R")
                    {
                        ReqStus = "요청";
                        ++nReq;
                    }
                    else if (dr["ReqStus"].ToString() == "C")
                    {
                        ReqStus = "취소";
                        ++nCnc;
                    }
                    else if(dr["ReqStus"].ToString() == "F")
                    {
                        ReqStus = "완료";
                        ++nFin;
                    }
                    
                    DateTime std_dt = DateTime.ParseExact(dr["AlarmStartDateTime"].ToString(), "yyyyMMddHHmmss", null);
                    StartDateTime = std_dt.ToString("yyyy-MM-dd HH:mm:ss");
                    EndDateTime = null;

                    if (dr["AlarmEndDateTime"].ToString().Trim() != "")
                    {
                        DateTime end_dt = DateTime.ParseExact(dr["AlarmEndDateTime"].ToString(), "yyyyMMddHHmmss", null);
                        EndDateTime = end_dt.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    

                    grdData.Rows.Add(++i,
                                    ReqStus,
                                    dr["WorkAlarmID"].ToString(),
                                    dr["Sabun"].ToString(),
                                    dr["ArticleID"].ToString(),
                                    dr["Article"].ToString(),
                                    dr["ProcessID"].ToString(),
                                    dr["Process"].ToString(),
                                    dr["MachineID"].ToString(),
                                    dr["Machine"].ToString(),
                                    StartDateTime,
                                    EndDateTime,
                                    dr["Comments"].ToString()
                                    
                                    );
                    if (ReqStus == "요청")
                    {
                        grdData.Rows[i - 1].Cells["ReqStus"].Style.BackColor = Color.FromArgb(255, 242, 157);
                    }
                    else if (ReqStus == "취소")
                    {
                        grdData.Rows[i - 1].Cells["ReqStus"].Style.BackColor = Color.FromArgb(240, 147, 114);//255, 87, 87);
                    }
                    else if (ReqStus == "완료")
                    {
                        grdData.Rows[i - 1].Cells["ReqStus"].Style.BackColor = Color.FromArgb(113, 194, 133);// 0, 162, 73);
                    }
                }
                lblReqCnt.Text = nReq.ToString() + "건";
                lblCncCnt.Text = nCnc.ToString() + "건";
                lblFinCnt.Text = nFin.ToString() + "건";
                nTot = nReq + nCnc + nFin;
                lblTotalQty.Text = nTot.ToString() + "건";
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
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
                if (this.Name.ToUpper().Contains(Value[0].ToUpper()))
                {
                    int.TryParse(Value[1], out Days);
                    break;
                }
            }
            mtb_From.Text = DateTime.Today.AddDays(-Days).ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
            //
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnCnc_Click(object sender, EventArgs e)
        {
            //ini로 공정/설비 체크 후 ini상에 등록되어있는 설비만 취소할수있음.
            //ReqStus 요청 : R ,취소 : C, 완료 : F
            //등록되어있는 설비의 경우 취소처리 // 그렇지 않을 경우 환경설정에서 설비를 추가해주세요라는 메세지 띄울것.
            if (grdData.SelectedRows.Count > 0)
            {
                bool blchk = false;
                if (grdData.SelectedRows[0].Cells["ReqStus"].Value.ToString() == "요청")
                {
                    
                    DataGridViewRow dgvr = grdData.SelectedRows[0];
                    Tools.INI_GS gs = new Tools.INI_GS();
                    sMachineID =Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine").Split('|');//배열에 설비아이디 넣기
                    List<string> sMachine = new List<string>();
                    foreach (string str in sMachineID)
                    {
                        sMachine.Add(str);
                    }
                    sMachineID = null;
                    //ini값과 같으면 요청취소처리
                    foreach (string Mac in sMachine)
                    {
                        if (Mac.Length > 4)
                        {
                            if (Mac.Substring(0, 4) == dgvr.Cells["ProcessID"].Value.ToString())
                            {
                                if (dgvr.Cells["MachineID"].Value.ToString() == Mac.Substring(4, 2))
                                {
                                    blchk = true;
                                    break;
                                }
                            }
                        }
                        
                    }
                    if (blchk)
                    {
                        try
                        {
                            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                            sqlParameter.Add("ReqStus", "C");
                            sqlParameter.Add("WorkAlarmID", dgvr.Cells["WorkAlarmID"].Value.ToString());
                            sqlParameter.Add("PersonID", Frm_tprc_Main.g_tBase.PersonID);
                            DataStore.Instance.ProcedureToDataTable("xp_WizWork_uwkWorkAlarmMtr", sqlParameter, false);
                            WizCommon.Popup.MyMessageBox.ShowBox(dgvr.Cells["Sabun"].Value.ToString() + "의 요청건이 취소되었습니다.", "[요청취소 성공]", 2, 1);
                        }
                        catch (Exception exm)
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", exm.Message), "[오류]", 0, 1);
                        }
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("환경설정에서 취소하려는 설비를 등록해주세요.", "[설비 등록 안되있음]", 2, 1);
                    }
                    dgvr = null;
                }
                else 
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("이미 취소상태이거나 완료된 상태의 요청은 취소할수없습니다.", "[오류]", 0, 1);
                }
                
            }
            else
            {
                WizCommon.Popup.MyMessageBox.ShowBox("취소 할 요청이 선택되지 않았습니다. 선택해주세요.", "[오류]", 0, 1);
            }
            FillGrid();
        }

        private void btnReq_Click(object sender, EventArgs e)
        {
            frm_trpc_MtrLossAlarm_U ftma = new frm_trpc_MtrLossAlarm_U();
            if (ftma.ShowDialog() == DialogResult.OK)
            {
                FillGrid();
            }
        }

        private void btnFin_Click(object sender, EventArgs e)
        {
            

            //ini로 공정/설비 체크 후 ini상에 등록되어있는 설비만 취소할수있음.
            //ReqStus 요청 : R ,취소 : C, 완료 : F
            //등록되어있는 설비의 경우 취소처리 // 그렇지 않을 경우 환경설정에서 설비를 추가해주세요라는 메세지 띄울것.
            if (grdData.SelectedRows.Count > 0)
            {
                bool blchk = false;
                if (grdData.SelectedRows[0].Cells["ReqStus"].Value.ToString() == "요청")
                {

                    DataGridViewRow dgvr = grdData.SelectedRows[0];
                    Tools.INI_GS gs = new Tools.INI_GS();
                    sMachineID =Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine").Split('|');//배열에 설비아이디 넣기
                    List<string> sMachine = new List<string>();
                    foreach (string str in sMachineID)
                    {
                        sMachine.Add(str);
                    }
                    sMachineID = null;
                    //ini값과 같으면 요청 완료처리화면 팝업
                    foreach (string Mac in sMachine)
                    {
                        if (Mac.Length > 4)
                        {
                            if (Mac.Substring(0, 4) == dgvr.Cells["ProcessID"].Value.ToString())
                            {
                                if (dgvr.Cells["MachineID"].Value.ToString() == Mac.Substring(4, 2))
                                {
                                    blchk = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (blchk)
                    {
                        try
                        {
                            string WorkAlarmID = grdData.SelectedRows[0].Cells["WorkAlarmID"].Value.ToString();
                            frm_mtr_InputMove_U2 fmim = new frm_mtr_InputMove_U2(WorkAlarmID);
                            if (fmim.ShowDialog() == DialogResult.OK)
                            {
                                FillGrid();
                            }                            
                        }
                        catch (Exception exm)
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", exm.Message), "[오류]", 0, 1);
                        }
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("환경설정에서 완료처리하려는 설비를 등록해주세요.", "[설비 등록 안되있음]", 2, 1);
                    }
                    dgvr = null;
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("요청이외의 상태의 자재요청건은 완료처리를 할 수 없습니다.", "[오류]", 0, 1, 1);
                }

            }
            else
            {
                WizCommon.Popup.MyMessageBox.ShowBox("취소 할 요청이 선택되지 않았습니다. 선택해주세요.", "[오류]", 0, 1);
            }
            FillGrid();
        }

        private void chkReq_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}
