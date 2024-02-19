using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WizCommon;

namespace WizWork.SamJoo
{
    public partial class frm_tprc_TransCracData : Form
    {
        WizWorkLib Lib = new WizWorkLib();

        string PortName = string.Empty;             //각종 컴포트 연결에 활용. 
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public delegate void AddDataDelegate(string mystring);
        public AddDataDelegate CracMachineDelegate;
        string strData = "";


        public frm_tprc_TransCracData()
        {            
            InitializeComponent();
            // dock style 지정.
            InitPanel();
        }

        private void frm_tprc_TransCracData_Load(object sender, EventArgs e)
        {
            //1. 통신연결 체크.
            try
            {
                PortName = SetPortNUM();  // 컴포트 연결.
                if (PortName != "")
                {
                    ConnectSerialPort(CracMachine, PortName);         // 중량기 연결하기
                }
                this.CracMachine.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CracMachine_DataReceived);
                this.CracMachineDelegate += new AddDataDelegate(AddCracMachineMethod);
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                btnCracInsStart.Enabled = false;
                btnCracInsStop.Enabled = false;
                lblInsepctStatus.Text = "현재 검사불가";
                lblInsepctStatus.BackColor = Color.FromArgb(232, 61, 61);
            }  
            //2. 데이터 그리드 컬럼 세팅
            // 데이터그리드 뷰 세팅.
            InitGrid();

            //3. 조회묶음.
            Fill_WorkLogGrid();

            //4. 버튼 기본세팅.
            if (lblInsepctStatus.Text == "현재 검사정지 중")
            {
                btnCracInsStop.Enabled = false;
            }
            else if (lblInsepctStatus.Text == "현재 검사 중")
            {
                btnCracInsStart.Enabled = false;
            }
        }


        #region 통신연결 관련 모음
        /// <summary>
        /// 통신포트 연결
        /// </summary>    
        private string SetPortNUM()
        {
            StringBuilder port = new StringBuilder();
            GetPrivateProfileString("COMPort", "CracMachine", "(NONE)", port, 10, ConnectionInfo.filePath);

            return port.ToString();
        }
        //// <param name="SP">시리얼포트</param>
        //// <param name="SPName">포트명</param>
        void ConnectSerialPort(SerialPort SP, string SPName)
        {
            SP.PortName = SPName;
            if (SP.IsOpen == false)
            {
                SP.Open();
            }
        }
        // 크랙 검사기에서 데이터가 날라 들어 왔을때.
        private void CracMachine_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (CracMachine.IsOpen) 
            {
                if (lblInsepctStatus.Text == "현재 검사 중")
                {                    
                    strData = CracMachine.ReadLine();
                    while (true)
                    {
                        if (strData.IndexOf("\r") == -1) //  
                        {
                            break;
                        }
                        else
                        {
                            strData = strData.Replace("\r", ""); // 문자열 변환
                        }
                    }
                    txtQty.Invoke(this.CracMachineDelegate, strData);                                        
                }
                else
                {
                    CracMachine.DiscardOutBuffer();
                    CracMachine.DiscardInBuffer();
                }
            }
            else { return; }
            
            
        }

        // 크랙 검사기에서 받은 값이 Delegate 거친 후에 mystring을 통해 Append되어 활용됩니다.
        public void AddCracMachineMethod(string mystring)
        {

            txtInspectDate.Text = string.Empty;
            txtInspectTime.Text = string.Empty;
            txtDefectYN.Text = string.Empty;
            txtQty.Text = string.Empty;

            string date = DateTime.Now.ToString("yyyyMMdd");
            string time = DateTime.Now.ToString("HHmmSS");

            if (mystring == "G")
            {
                //검사일자
                txtInspectDate.Text = Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, date);
                //검사시각
                txtInspectTime.Text = Lib.MakeDateTime("HHMMSS", time);
                //판정결과
                txtDefectYN.Text = "합격";
                //검사수량
                txtQty.Text = "1";
            }
            else if (mystring == "R")
            {
                //검사일자
                txtInspectDate.Text = Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, date);
                //검사시각
                txtInspectTime.Text = Lib.MakeDateTime("HHMMSS", time);
                //판정결과
                txtDefectYN.Text = "불합격";
                //검사수량
                txtQty.Text = "1";
            }

            if (txtInspectDate.Text != string.Empty)
            {
                // WorkLog에 저장.
                SaveData_WorkLog();

            }
            
        }

        // WorkLog에 저장.
        private void SaveData_WorkLog()
        {
            try
            {
                List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
                List<List<string>> ListProcedureName = new List<List<string>>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                double zero = 0;
                double one = 1;

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();
                sqlParameter.Add("JobID", zero);
                sqlParameter.Add("DefectQty", txtDefectYN.Text == "불합격" ? one : zero);
                sqlParameter.Add("sCommetns", txtDefectYN.Text);

                WizCommon.Procedure pro_2 = new WizCommon.Procedure();
                pro_2.Name = "xp_WizWork_iCracWorkLog";
                pro_2.OutputUseYN = "N";
                pro_2.OutputName = "JobID";
                pro_2.OutputLength = "20";

                Prolist.Add(pro_2);
                ListParameter.Add(sqlParameter);

                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputToCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    list_Result.RemoveAt(0);
                    Fill_WorkLogGrid();     // 재 조회.
                }
                else
                {
                    foreach (KeyValue kv in list_Result)
                    {
                        if (kv.key.ToLower() == "failure")
                        {
                            throw new Exception(kv.value.ToString());
                        }
                    }
                }
            }            
            catch (Exception ex)
            {
                // 무언가 문제가 발생한다.
                btnCracInsStop_Click(null, null);     // 강제 중지.
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }



        #endregion

        #region InitPanel  TLP
        private void InitPanel()
        {
            tlpForm.Dock = DockStyle.Fill;
            foreach (Control control in tlpForm.Controls)
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
                                        foreach (Control c2 in c.Controls)
                                        {
                                            c2.Dock = DockStyle.Fill;
                                            c2.Margin = new Padding(1, 1, 1, 1);
                                            foreach (Control c3 in c2.Controls)
                                            {
                                                c3.Dock = DockStyle.Fill;
                                                c3.Margin = new Padding(1, 1, 1, 1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region (데이터그리드 뷰 ) InitGrid
        private void InitGrid()
        {
            int i = 0;
            grdData.Columns.Clear();
            grdData.ColumnCount = 4;
            // Set the Colums Hearder Names

            grdData.Columns[i].Name = "No";
            grdData.Columns[i].HeaderText = "N";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ChildArticle";
            grdData.Columns[i].HeaderText = "검사일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ChildArticleID";
            grdData.Columns[i].HeaderText = "검사시각";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ChildArticleID";
            grdData.Columns[i].HeaderText = "합/불 여부";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;


            grdData.Font = new Font("맑은 고딕", 12);
            //grdData.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 12);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.ForeColor = Color.IndianRed;
            style.BackColor = Color.Ivory;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.HeaderCell.Style = style;
            }

            return;

        }











        #endregion



        // 조회묶음        
        private void Fill_WorkLogGrid()
        {
            // 최근 50개의 통신기록(work_log 값)을 가지고 옵니다.
            ProcQuery();
            // 오늘자 크랙검사 통신이력 종합
            Today_CracData_History();
        }

        #region ( 50개 기록 가져오기 )  ProcQuery
        // 최근 50개의 통신기록(work_log 값)을 가지고 옵니다.
        private void ProcQuery()
        {
            grdData.Rows.Clear();

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();
                sqlParameter.Add("SelectType", "1");    // 마지막 50개 가져오기.
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sWorkLogLastCracData", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        i++;
                        grdData.Rows.Add(
                                         i.ToString(),
                                         dr["WorkDate"].ToString(),
                                         dr["WorkTime"].ToString(),
                                         dr["DefectYN"].ToString()
                                        );
                    }
                    Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdData.RowCount);
                    Frm_tprc_Main.gv.SetStbInfo();
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }

        }

        #endregion

        #region (오늘자 카운트 이력) Today_CracData_History
        private void Today_CracData_History()
        {
            lblTodayInspectQty.Text = "건";
            lblTodaySuccessQty.Text = "건";
            lblTodayDefectQty.Text = "건";

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Clear();
                sqlParameter.Add("SelectType", "2");        // 오늘자 검사이력 카운트 조회.
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sWorkLogLastCracData", sqlParameter, false);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    lblTodayInspectQty.Text = dr["Inspect"].ToString() + " 건";
                    lblTodaySuccessQty.Text = dr["GreenCount"].ToString() + " 건";
                    lblTodayDefectQty.Text = dr["RedCount"].ToString() + " 건";
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        #endregion



        // 화면닫기 버튼 클릭.
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        // 크랙검사 시작버튼
        private void btnCracInsStart_Click(object sender, EventArgs e)
        {
            if (lblInsepctStatus.Text == "현재 검사정지 중")
            {
                btnCracInsStart.Enabled = false;
                btnCracInsStop.Enabled = true;
                lblInsepctStatus.Text = "현재 검사 중";
                lblInsepctStatus.BackColor = Color.FromArgb(113, 194, 133);
            }

            Fill_WorkLogGrid();
        }

        // 크랙검사 정지버튼
        private void btnCracInsStop_Click(object sender, EventArgs e)
        {
            if (lblInsepctStatus.Text == "현재 검사 중")
            {
                btnCracInsStop.Enabled = false;
                btnCracInsStart.Enabled = true;
                lblInsepctStatus.Text = "현재 검사정지 중";
                lblInsepctStatus.BackColor = Color.FromArgb(255, 242, 157);
            }
            Fill_WorkLogGrid();
        }



    }
}
