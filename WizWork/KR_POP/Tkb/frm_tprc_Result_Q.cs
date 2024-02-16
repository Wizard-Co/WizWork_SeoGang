using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using WizWork.Properties;

namespace WizWork
{
    public partial class Frm_tprc_Result : Form
    {
        INI_GS gs = Frm_tprc_Main.gs;
        private DataSet ds = null;
        int z = 0; //수평 스크롤바 이동용 변수
        /// <summary>
        /// 생성
        /// </summary>
        public Frm_tprc_Result()
        {
            InitializeComponent();
        }


        private void btnLookup_Click(object sender, EventArgs e)
        {
            procQuery();
        }

        /// <summary>
        /// 메인화면 버튼 클릭 - 폼 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveMain_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetScreen()
        {
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Margin = new Padding(1, 1, 1, 1);
            foreach (Control control in tlpForm.Controls)//con = tlp 상위에서 2번째
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(1, 1, 1, 1);
                foreach (Control contro in control.Controls)//tlp 상위에서 3번째
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
                                    foreach (Control c in co.Controls)
                                    {
                                        c.Dock = DockStyle.Fill;
                                        c.Margin = new Padding(1, 1, 1, 1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)                            
        {
            int checkCount = 0;//체크된 카운트
            int deleteCount = 0; //작업일이 현재일자와 같지 않아서 삭제할 수 없는 행의 수
            int c = 0; //작업일이 현재일자와 같지 않아서 삭제할 수 없는 행의 수
            if (grdData.RowCount == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("조회 후 삭제 버튼을 눌러주십시오.", "[조회자료 없음]", 0, 1);
            }
            else
            {
                foreach (DataGridViewRow dgvr in grdData.Rows)
                {
                    if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                    {
                        checkCount++;
                    }
                }
                if (checkCount == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("삭제 대상을 선택 후 '삭제'버튼을 클릭해주세요.", "[삭제 대상 클릭]", 0, 0);
                }
                else
                {
                    List<string> list_Confirm = new List<string>();//프로시저 수행 성공여부 값 저장/success/failure
                    double i = 0;//삭제대상 JobID 임시 저장 변수
                    
                    if (WizCommon.Popup.MyMessageBox.ShowBox("선택항목에 대해서 삭제처리하시겠습니까?", "[삭제]", 0, 0) == DialogResult.OK)
                    {
                        foreach (DataGridViewRow dgvr in grdData.Rows)
                        {
                            if (dgvr.Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                            {
                                i = 0;
                                double.TryParse(dgvr.Cells["JobID"].Value.ToString(), out i);
                                //if (dgvr.Cells["WorkEndDate"].Value.ToString().Replace("-", "") == DateTime.Now.ToString("yyyyMMdd"))
                                //{
                                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                                    sqlParameter.Add(Work_sResultWithMachineComProcess.JOBID, i);// grdData.Rows[rowIndex].Cells["JobID"].Value.ToString());
                                    sqlParameter.Add(Work_sResultWithMachineComProcess.CREATEUSERID, Frm_tprc_Main.g_tBase.PersonID);
                                    sqlParameter.Add(Work_sResultWithMachineComProcess.SRTNMSG, "");
                                    string[] sConfirm = new string[2];
                                    sConfirm = DataStore.Instance.ExecuteProcedure("xp_prdWork_dWkResult", sqlParameter, true); //삭제
                                    list_Confirm.Add(sConfirm[0]);
                                    if (sConfirm[0].ToUpper() == "SUCCESS")
                                    { deleteCount++; }
                                //}
                                //else
                                //{
                                //    c++;
                                //}
                            }
                        }
                        if (list_Confirm.Count > 0)//삭제결과 리스트
                        {
                            procQuery();
                            if (c > 0)
                            {
                                WizCommon.Popup.MyMessageBox.ShowBox("현재 날짜와 동일한 작업일자" + deleteCount.ToString() + "건 삭제완료됬습니다." +
                                "\r\n" + c.ToString() + "개 작업건수는 현재 날짜와 동일하지 않아 삭제할 수 없습니다.", "[삭제 완료]", 0, 1);
                            }
                            else
                            {
                                WizCommon.Popup.MyMessageBox.ShowBox(deleteCount.ToString() + "건 삭제완료됬습니다.", "[삭제 완료]", 0, 1);
                            }
                        }
                        else//삭제 결과리스트가 없음 > 삭제를 안했음
                        {
                            if (c > 0)
                            {
                                WizCommon.Popup.MyMessageBox.ShowBox(c.ToString() + "개 작업건수는 현재 날짜와 동일하지 않아 삭제할 수 없습니다.", "[삭제 불가]", 0, 1);
                            }
                        }
                    }
                }
            }
        }
        public void procQuery()
        {
            int nRecordCount = 0;
            grdData.Rows.Clear();
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

            if (!chkResultDate.Checked)
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.CHKDATE, "0");
            }
            else
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.CHKDATE, "1");
            }

            sqlParameter.Add(Work_sResultWithMachineComProcess.SDATE, mtb_From.Text.Replace("-", ""));
            sqlParameter.Add(Work_sResultWithMachineComProcess.EDATE, mtb_To.Text.Replace("-", ""));

            if (cboProcess.SelectedIndex == 0)
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.CHKPROCESSID, "0");
                sqlParameter.Add(Work_sResultWithMachineComProcess.PROCESSID, ""); //this.cboProcess.SelectedValue.ToString()
                sqlParameter.Add(Work_sResultWithMachineComProcess.CHKMACHINEID, "0");
                sqlParameter.Add(Work_sResultWithMachineComProcess.MACHINEID, "0");
            }
            else
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.CHKPROCESSID, "1");
                sqlParameter.Add(Work_sResultWithMachineComProcess.PROCESSID, this.cboProcess.SelectedValue.ToString());
                if (cboMachine.SelectedIndex == 0)
                {
                    sqlParameter.Add(Work_sResultWithMachineComProcess.CHKMACHINEID, "0");
                    sqlParameter.Add(Work_sResultWithMachineComProcess.MACHINEID, "0");
                }
                else
                {
                    sqlParameter.Add(Work_sResultWithMachineComProcess.CHKMACHINEID, "1");
                    sqlParameter.Add(Work_sResultWithMachineComProcess.MACHINEID, cboMachine.SelectedValue.ToString());
                }
            }
            if (cboTeam.SelectedIndex == 0)
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.NCHKTEAMID, "0");
                sqlParameter.Add(Work_sResultWithMachineComProcess.STEAMID, cboTeam.SelectedValue.ToString());
            }
            else
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.NCHKTEAMID, "1");
                sqlParameter.Add(Work_sResultWithMachineComProcess.STEAMID, cboTeam.SelectedValue.ToString());
            }

            //if (chkPLotID.Checked) //
            //{
            //    sqlParameter.Add("nChkPLotID", "1");
            //    sqlParameter.Add("sPLotID", txtPLotID.Text.Trim().ToString());
            //}
             
            if (cboJobGbn.Text == "전체")//'':전체,1:정상,2:무작업,3:재작업
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.SJOBGBN, "");
            }
            else if (cboJobGbn.Text == "정상")
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.SJOBGBN, "1");
            }
            else if (cboJobGbn.Text == "무작업")
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.SJOBGBN, "2");
            }
            else if (cboJobGbn.Text == "재작업")
            {
                sqlParameter.Add(Work_sResultWithMachineComProcess.SJOBGBN, "3");
            }

            sqlParameter.Add("nBuyerArticleNo", chkBuyerArticleNo.Checked == true ? 1 : 0);
            sqlParameter.Add("BuyerArticleNo", chkBuyerArticleNo.Checked == true && txtBuyerArticleNo.Text.Trim().Length > 0 ? txtBuyerArticleNo.Text : "");

            ds = DataStore.Instance.ProcedureToDataSet("xp_prdWork_sWkResult", sqlParameter, false);
            IFormatProvider KR_Format = new System.Globalization.CultureInfo("ko-KR", true);
            if (ds.Tables[0].Rows.Count > 0)
            {
                double douWorkQty = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    string sDate = string.Empty;
                    string StartTime = string.Empty;
                    string EndTime = string.Empty;
                    double.TryParse(dr[Work_sResultWithMachineComProcess.WORKQTY].ToString(), out douWorkQty);
                    if (dr["WorkEndDate"].ToString().Length == 8)
                    {
                        sDate = dr["WorkEndDate"].ToString();
                        sDate = string.Format(sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6), "yyyy-MM-dd");
                    }
                    if (dr["WorkStartTime"].ToString().Length == 6)
                    {
                        StartTime = dr["WorkStartTime"].ToString();
                        StartTime = string.Format(StartTime.Substring(0, 2) + ":" + StartTime.Substring(2, 2), "HHmm");
                    }
                    if (dr["WorkEndTime"].ToString().Length == 6)
                    {
                        EndTime = dr["WorkEndTime"].ToString();
                        EndTime = string.Format(EndTime.Substring(0, 2) + ":" + EndTime.Substring(2, 2), "HHmm");
                    }

                    grdData.Rows.Add(
                        false,
                        i+1,
                        dr[Work_sResultWithMachineComProcess.JOBGBNNAME],           //작업구분
                        sDate,
                        StartTime,
                        EndTime,

                        dr[Work_sResultWithMachineComProcess.PROCESS],          //공정
                        dr[Work_sResultWithMachineComProcess.MACHINENO],        //호기
                        dr[Work_sResultWithMachineComProcess.BOXID],            //박스번호
                        dr[Work_sResultWithMachineComProcess.BUYERARTICLENO],   //품번
                        dr[Work_sResultWithMachineComProcess.ARTICLE],          //품명

                        dr["OrderArticleID"].ToString(),
                        string.Format("{0:n0}", douWorkQty),                    //작업량
                        dr[Work_sResultWithMachineComProcess.WORKMANNAME],      //작업자
                        dr[Work_sResultWithMachineComProcess.LOTID],            //지시번호
                        dr[Work_sResultWithMachineComProcess.KCUSTOM],          //거래처

                        dr["BuyerModel"].ToString(),                            //차종
                        dr[Work_sResultWithMachineComProcess.ORDERNO],          //오더번호
                       
                       
                        dr["UnitClss"].ToString(),                              //단위
                        dr[Work_sResultWithMachineComProcess.ORDERID],          //관리번호
                        dr[Work_sResultWithMachineComProcess.TEAM],             //작업조
                        


                        dr[Work_sResultWithMachineComProcess.NOREWORKCODENAME], //무작업사유 

                        dr[Work_sResultWithMachineComProcess.JOBID]);            //JobID 
                    if (grdData.Rows[i].Cells["JobGbnName"].Value.ToString() == "무작업")
                    {
                        grdData.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(238, 108, 128);
                    }
                }
                grdData.ClearSelection();

                grdData[0, 0].Selected = true; //0번째 행 선택
                //grdData.AutoResizeColumns();
                //grdData.Columns["RowSeq"].Width = 100;
                double Total = 0;
                for (int i = 0; i < grdData.Rows.Count; i++)
                {
                    Total += Convert.ToDouble(grdData.Rows[i].Cells["WorkQty"].Value);
                }
                nRecordCount = grdData.RowCount; //현재 행의 갯수
                Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", nRecordCount);
                Frm_tprc_Main.gv.SetStbInfo();
            }

            FillGrdSum();
 
        }

        public void procDelete(double i)
        {
            if(grdData.Rows.Count > 0 && grdData.SelectedRows.Count > 0)
            {
                    try
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add(Work_sResultWithMachineComProcess.JOBID, i);// grdData.Rows[rowIndex].Cells["JobID"].Value.ToString());
                        sqlParameter.Add(Work_sResultWithMachineComProcess.CREATEUSERID, Frm_tprc_Main.g_tBase.PersonID);
                        sqlParameter.Add(Work_sResultWithMachineComProcess.SRTNMSG, "");
                        DataStore.Instance.ExecuteProcedure("xp_wkResult_dWkResult", sqlParameter, true); //삭제
                    }
                    catch (Exception ex)
                    {
                    Console.Write(ex.Message);
                    //WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                }
            }
        }

        //콤보박스 데이터 바인딩
        private void SetComboBox()
        {
            cboJobGbn.SelectedIndex = 0;
            string strProcessID = "";

            strProcessID =Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID");
            string[] gubunProcess = strProcessID.Split(new char[] { '|' });

            strProcessID = string.Empty;

            for (int i = 0; i < gubunProcess.Length; i++)
            {
                if (strProcessID.Equals(string.Empty))
                {
                    strProcessID = gubunProcess[i];
                }
                else
                {
                    strProcessID = strProcessID + "|" + gubunProcess[i];
                }
            }

            //공정 가져오기
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add(Work_sProcess.NCHKPROC, "1");//cboProcess.Text 
            sqlParameter.Add(Work_sProcess.PROCESSID, strProcessID);//cboProcess.Text
            ds = DataStore.Instance.ProcedureToDataSet("[xp_Work_sProcess]", sqlParameter, false);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow[Work_sProcess.PROCESSID] = "*";
            newRow[Work_sProcess.PROCESS] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow, 0);
            cboProcess.DataSource = ds.Tables[0];
            cboProcess.ValueMember = Work_sProcess.PROCESSID;
            cboProcess.DisplayMember = Work_sProcess.PROCESS;

            //작업조 가져오기
            ds = DataStore.Instance.ProcedureToDataSet("xp_Code_sTeam", null, false);
            DataRow newRow1 = ds.Tables[0].NewRow();
            newRow1[Code_sTeam.TEAMID] = "*";
            newRow1[Code_sTeam.TEAM] = "전체";
            ds.Tables[0].Rows.InsertAt(newRow1, 0);
            cboTeam.DataSource = ds.Tables[0];
            cboTeam.ValueMember = Code_sTeam.TEAMID;
            cboTeam.DisplayMember = Code_sTeam.TEAM;
            //GetInsTypeDataSource(cboTeam);

            //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

           

            //sqlParameter.Add(Work_sProcess.PROCESSID, this.cboProcess.SelectedValue.ToString());
            //ds = DataStore.Instance.ProcedureToDataSet("xp_Work_sMachinebyProcess", sqlParameter, false);
            //DataStore.Instance.CloseConnection();
            
            //this.cboMachine.DataSource = null;

            //this.cboMachine.DataSource = ds.Tables[0];

            ////CHKPROCESSID
            //this.cboMachine.ValueMember = Work_sMachineByProcess.MACHINEID;
            //this.cboMachine.DisplayMember = Work_sMachineByProcess.MACHINE;
            

            
            //ds = DataStore.Instance.ExecuteDataSet("xp_Code_sTeam", null, false);
            //DataStore.Instance.CloseConnection();

            //this.cboTeam.DataSource = null;

            //this.cboTeam.DataSource = ds.Tables[0];
            //this.cboTeam.ValueMember = Code_sTeam.TEAMID;
            //this.cboTeam.DisplayMember = Code_sTeam.TEAM;

            

            //ds = DataStore.Instance.ExecuteDataSet("xp_Code_sTeam", null, false);
            //DataStore.Instance.CloseConnection();

            //this.cboJobGbn.DataSource = null;

            //this.cboJobGbn.DataSource = ds.Tables[0];
            //this.cboJobGbn.ValueMember = Code_sTeam.TEAMID;
            //this.cboJobGbn.DisplayMember = Code_sTeam.TEAM;


            ////콤보박스 Y, N 값 추가
            //DataTable dtUseYN = new DataTable();
            //dtUseYN.Columns.Add(Work_sProcess.PROCESSID);
            //dtUseYN.Columns.Add(Work_sProcess.PROCESS);
            //dtUseYN.Rows.Add("Y", "Y");
            //dtUseYN.Rows.Add("N", "N");

            //DataGridViewComboBoxColumn colUseYN = dgvMenuList.Columns[com_Menu.USEYN] as DataGridViewComboBoxColumn;
            //colUseYN.DataSource = dtUseYN;
            //colUseYN.DisplayMember = com_Code.CODENAME;
            //colUseYN.ValueMember = com_Code.SCODE;

            ////콤보박스 디자인
            //colUseYN.FlatStyle = FlatStyle.Flat;

        }

        //공정에 따른 기계이름 가져오기
        private void cboProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strProcess = cboProcess.SelectedValue.ToString();
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add(Work_sProcess.PROCESSID, strProcess);//cboProcess.Text 

            DataTable dt  = DataStore.Instance.ProcedureToDataTable("xp_Work_sMachinebyProcess", sqlParameter, false);
            DataTable dt2 = dt.Clone();

            string[] sMachineID = null;
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
                        if (Mac.Substring(0, 4) == strProcess)
                        {
                            if (Mac.Substring(4, 2) == dr["MachineID"].ToString())
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
                    sMachine.Remove(strProcess + dr["MachineID"].ToString());
                }
            }
            DataRow newRow = dt2.NewRow();
            newRow[Work_sMachineByProcess.MACHINEID] = "*";
            newRow[Work_sMachineByProcess.MACHINENO] = "전체";
            dt2.Rows.InsertAt(newRow, 0);
            cboMachine.DataSource = dt2;
            cboMachine.ValueMember = Work_sMachineByProcess.MACHINEID;
            cboMachine.DisplayMember = Work_sMachineByProcess.MACHINENO;
            if (dt2.Rows.Count > 1)
            {
                cboMachine.SelectedIndex = 0;
            }
            dt = null;
        }

       
        //위 버튼, 그리드뷰 선택된 셀에서 위로 이동
        private void cmdRowUp_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowUp(grdData, z);
        }
        //아래 버튼, 그리드뷰 선택된 셀에서 아래로 이동
        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowDown(grdData, z);      
        }

        private void SetDateTime()
        {
            ////ini 날짜 불러와서 기간 설정하기
            chkResultDate.Checked = true;
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
        }


        private void Frm_tprc_Result_Load(object sender, EventArgs e)
        {
            SetScreen();
            SetDateTime();
            SetComboBox();
            InitGrid();

            txtPLotID.Text = gs.GetValue("Work", "SetLOTID", "");
            if (txtPLotID.Text != string.Empty)
            {
                chkPLotID.Checked = true;
                procQuery();
            }
            else
            {
                chkPLotID.Checked = false;
                procQuery();
            }
        }
        #region Default Grid Setting

        private void InitGrid()
        {
            grdData.Columns.Clear();
            grdData.ColumnCount = 22;

            int n = 0;

            grdData.Columns[n].Name = "RowSeq";
            grdData.Columns[n].HeaderText = "No";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "JobGbnName";
            grdData.Columns[n].HeaderText = "구분";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "WorkEndDate";
            grdData.Columns[n].HeaderText = "작업" + "\r\n" +"완료일";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "WorkStartTime";
            grdData.Columns[n].HeaderText = "작업"+"\r\n" +"시작";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "WorkEndTime";
            grdData.Columns[n].HeaderText = "작업" + "\r\n" + "완료";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "Process";
            grdData.Columns[n].HeaderText = "공정";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "MachineNo";
            grdData.Columns[n].HeaderText = "호기";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "BoxID";
            grdData.Columns[n].HeaderText = "이동전표";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "BuyerArticleNo";
            grdData.Columns[n].HeaderText = "품번";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "Article";
            grdData.Columns[n].HeaderText = "품명";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "OrderArticleID";
            grdData.Columns[n].HeaderText = "품목코드";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "WorkQty";
            grdData.Columns[n].HeaderText = "작업량";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "WorkManName";
            grdData.Columns[n].HeaderText = "작업자";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "LotID";
            grdData.Columns[n].HeaderText = "지시LOTID";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "KCUSTOM";
            grdData.Columns[n].HeaderText = "거래처";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "BuyerModel";
            grdData.Columns[n].HeaderText = "차종";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "OrderNO";
            grdData.Columns[n].HeaderText = "OrderNo";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = false;            

            grdData.Columns[n].Name = "UnitClss";
            grdData.Columns[n].HeaderText = "단위";
            grdData.Columns[n++].Visible = false;
            
            grdData.Columns[n].Name = "OrderID";
            grdData.Columns[n].HeaderText = "접수번호";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "Team";
            grdData.Columns[n].HeaderText = "작업조";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = false;

            grdData.Columns[n].Name = "NoReworkCodeName";
            grdData.Columns[n].HeaderText = "무작업사유";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[n++].Visible = true;

            grdData.Columns[n].Name = "JobID";
            grdData.Columns[n].HeaderText = "JobID";
            grdData.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[n++].Visible = false;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            {
                chkCol.HeaderText = "";
                chkCol.Name = "Check";
                chkCol.Width = 110;
                //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                chkCol.FlatStyle = FlatStyle.Standard;
                chkCol.ThreeState = true;
                chkCol.CellTemplate = new DataGridViewCheckBoxCell();
                chkCol.CellTemplate.Style.BackColor = Color.Beige;
                chkCol.Visible = true;
            }
            grdData.Columns.Insert(0, chkCol);

            grdData.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdData.RowTemplate.Height = 37;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdData.ReadOnly = true;
            grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            return;
        }

        private void FillGrdSum()
        {
            double douMissingWorkSum = 0;
            int WorkCount = 0;
            WorkCount = grdData.Rows.Count;

            for (int i = 0; i < WorkCount; i++)
            {
                douMissingWorkSum = douMissingWorkSum + double.Parse(grdData.Rows[i].Cells["WorkQty"].Value.ToString());
            }
            
            DataTable dt = new DataTable();
            dt.Columns.Add("WorkSumText".ToString());
            dt.Columns.Add("WorkCount".ToString());
            dt.Columns.Add("WorkSum".ToString());
            //dt.Columns.Add("Space".ToString());
            DataRow dr = dt.NewRow();
            dr["WorkSumText"] = "작업 합계";
            dr["WorkCount"] = string.Format("{0:n0}", WorkCount) + " 건";
            dr["WorkSum"] = string.Format("{0:n0}", douMissingWorkSum);   // 소수점 없애달래.ㅇㅇ...         
            //dr["Space"] = "";
            dt.Rows.Add(dr);

            grdSum.DataSource = dt;
            grdSum.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);

            grdSum.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdSum.RowTemplate.Height = 40;
            grdSum.ColumnHeadersHeight = 35;
            grdSum.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdSum.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSum.ReadOnly = true;
            for (int i = 0; i < grdSum.SelectedCells.Count; i++)
            {
                grdSum.SelectedCells[i].Selected = false;
            }
        }

        #endregion

        private void cboProcess_Click(object sender, EventArgs e)
        {
            SetComboBox();
        }


        private void txtPLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtPLotID.Text.ToUpper().Contains("PL") && (txtPLotID.Text.Trim().Length == 15 || txtPLotID.Text.Trim().Length == 16))
                {
                    chkPLotID.Checked = true;
                    procQuery();
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("작업지시목록 LotID가 아닙니다. 작업지시목록에 있는 바코드를 스캔해주세요!", "[바코드 오류]", 2, 1);
                    return;
                }
            }
        }

        private void chkPLotID_Click(object sender, EventArgs e)
        {
            if (chkPLotID.Checked)
            {
                txtPLotID.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("LOTID입력", "LOTID");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtPLotID.Text = keypad.tbInputText.Text;
                }
            }
            else
            {
                txtPLotID.Text = "";
            }
        }
        #region 달력 From값 입력 // 달력 창 띄우기
        private void mtb_From_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
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
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnColRight_Click(object sender, EventArgs e)
        {
            z = Frm_tprc_Main.Lib.btnColRight(grdData, z);
        }

        private void btnColLeft_Click(object sender, EventArgs e)
        {
            z = Frm_tprc_Main.Lib.btnColLeft(grdData, z);
        }

        private void Frm_tprc_Result_Activated(object sender, EventArgs e)
        {
            ((Frm_tprc_Main)(MdiParent)).LoadRegistry();
            txtPLotID.Text = gs.GetValue("Work", "SetLOTID", "");
            if (txtPLotID.Text != string.Empty)
            {
                chkPLotID.Checked = true;
                procQuery();
            }
            else
            {
                chkPLotID.Checked = false;
                procQuery();
            }
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (grdData.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    grdData.Rows[e.RowIndex].Cells["Check"].Value = true;
                }
                else if (grdData.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    grdData.Rows[e.RowIndex].Cells["Check"].Value = false;
                }
            }
            
        }

        private void chkBuyerArticleNo_Click(object sender, EventArgs e)
        {
            if (chkBuyerArticleNo.Checked)
            {
                txtBuyerArticleNo.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("품번입력", "품번");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtBuyerArticleNo.Text = keypad.tbInputText.Text;
                    procQuery();
                }
            }
            else
            {
                txtBuyerArticleNo.Text = "";
            }
        }

        private void txtBuyerArticleNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                chkBuyerArticleNo.Checked = true;
                procQuery();
            }
        }
    }
}
