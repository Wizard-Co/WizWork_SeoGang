using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using WizCommon;

namespace WizWork
{
    public partial class FrmSetting : Form
    {
        List<List<WizCommon.BoolValue>> list_AllProcessID = new List<List<WizCommon.BoolValue>>();
        //최상위리스트 : 전체프로세스ID담고있는 리스트, 중간리스트 : 각각프로세스ID들 머신들의 리스트, 딕셔너리 : 프로세스ID별 각각의 Machine 및 체크여부
        List<string> list_ProcessID = new List<string>();//공정ID 리스트
        /////////////////////////////////////////////////////////////////////////////////
        List<ProBoolValue> list_AllProTermProcessID = new List<ProBoolValue>();
        List<List<WizCommon.BoolValue>> list_AllLblTermProcessID = new List<List<WizCommon.BoolValue>>();
        //최상위리스트 : 전체프로세스ID담고있는 리스트, 중간리스트 : 각각프로세스ID들 머신들의 리스트, 딕셔너리 : 프로세스ID별 각각의 Machine 및 체크여부
        List<string> list_TermProcessID = new List<string>();//공정별 조건 리스트
        List<string> list_TermLabelID = new List<string>();//공정ID 리스트
        /////////////////////////////////////////////////////////////////////////////////
        private DataSet ds = null;
        string sProcess = "";                                    //폼안의 그리드뷰에 체크한 ProcessID를 저장할 변수
        string strProID = "";                                   //Process 그리드에서 선택한 ProcessID
        int intProIdx = 0;                                      //Process 그리드에서 선택한 ProcessID의 인덱스, 전체 List값의 인덱스와 동일 이 인덱스로 리스트의 프로세스 id를 알수있음.
        WizWorkLib Lib = new WizWorkLib();
        string FilePath = ConnectionInfo.filePath;             //Wizard.ini 파일위치
        INI_GS gs = new INI_GS();
        WizCommon.Popup.Frm_CMNumericKeypad FK = null;
        WizCommon.Popup.Frm_CMKeypad FCK = null;
        public FrmSetting()
        {
            InitializeComponent();
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        //섹션명[]. 키값,
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        //섹션명[], 키값, 기본값, 가져온문자열, 문자열버퍼크기, 파일이름

        #region Default Grid Setting

        private void InitGridPro()
        {
            grdProcess.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdProcess.ColumnCount = 3;

            int i = 0;

            grdProcess.Columns[i].Name = "RowSeq";
            grdProcess.Columns[i].HeaderText = "";
            grdProcess.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdProcess.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdProcess.Columns[i].ReadOnly = true;
            grdProcess.Columns[i].Visible = true;
            
            grdProcess.Columns[++i].Name = "ProcessID";
            grdProcess.Columns[i].HeaderText = "공정코드";
            grdProcess.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdProcess.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdProcess.Columns[i].ReadOnly = true;
            grdProcess.Columns[i].Visible = true;
            
            grdProcess.Columns[++i].Name = "Process";
            grdProcess.Columns[i].HeaderText = "공정명";
            grdProcess.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdProcess.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdProcess.Columns[i].ReadOnly = true;
            grdProcess.Columns[i].Visible = true;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            chkCol.HeaderText = "선택";
            chkCol.Name = "Check";
            chkCol.Width = 50;
            chkCol.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdProcess.Columns.Insert(1, chkCol);

            grdProcess.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdProcess.RowTemplate.Height = 30;
            grdProcess.ColumnHeadersHeight = 35;
            grdProcess.ScrollBars = ScrollBars.Both;
            grdProcess.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdProcess.MultiSelect = false;
            grdProcess.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdProcess.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            //grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdProcess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grdProcess.ReadOnly = true;

            foreach (DataGridViewColumn col in grdProcess.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void InitGridMac()
        {
            grdMachine.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdMachine.ColumnCount = 3;

            int i = 0;

            grdMachine.Columns[i].Name = "RowSeq";
            grdMachine.Columns[i].HeaderText = "";
            grdMachine.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdMachine.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdMachine.Columns[i].ReadOnly = true;
            grdMachine.Columns[i].Visible = true;
            
            grdMachine.Columns[++i].Name = "MachineID";
            grdMachine.Columns[i].HeaderText = "설비코드";
            grdMachine.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdMachine.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdMachine.Columns[i].ReadOnly = true;
            grdMachine.Columns[i].Visible = true;
            
            grdMachine.Columns[++i].Name = "Machine";
            grdMachine.Columns[i].HeaderText = "설비명";
            grdMachine.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdMachine.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdMachine.Columns[i].ReadOnly = true;
            grdMachine.Columns[i].Visible = true;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            chkCol.HeaderText = "선택";
            chkCol.Name = "Check";
            chkCol.Width = 50;
            chkCol.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdMachine.Columns.Insert(1, chkCol);

            grdMachine.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdMachine.RowTemplate.Height = 30;
            grdMachine.ColumnHeadersHeight = 35;
            grdMachine.ScrollBars = ScrollBars.Both;
            grdMachine.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdMachine.MultiSelect = false;
            grdMachine.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdMachine.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            //grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdMachine.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grdMachine.ReadOnly = true;

            foreach (DataGridViewColumn col in grdMachine.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void InitGridInstDate()
        {
            grdInstDate.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.

            InitCol("PlanInput", "공정작업");
            InitCol("CardRePrint", "이동전표");
            InitCol("Result", "생산실적");
            InitCol("InspectResult", "자주검사");
            InitCol("Move", "이동처리");
            InitCol("MissingWorkQty", "생산미달성");
            InitCol("DailMachineCheck", "설비점검");
            InitCol("MtrLotStock", "자재Lot재고");
            InitCol("MtrLossAlarm", "자재요청");

            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            {
                btnCol.HeaderText = "입력";
                btnCol.Name = "입력";
                btnCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                
                btnCol.Visible = true;
            }
            grdInstDate.Columns.Insert(grdInstDate.Columns.Count, btnCol);

            grdInstDate.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdInstDate.RowTemplate.Height = 30;
            grdInstDate.ColumnHeadersHeight = 25;
            grdInstDate.ScrollBars = ScrollBars.Both;
            grdInstDate.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdInstDate.MultiSelect = false;
            grdInstDate.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdInstDate.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdInstDate.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grdInstDate.ReadOnly = true;

            foreach (DataGridViewColumn col in grdInstDate.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            grdInstDate.Rows.Add("0", "0", "0", "0", "0", "0", "0", "0", "0","입력");
            //grdInstDate.Rows[0].Cells["입력"].Value = "입력";
        }

        //private void InitGridChkProcess()
        //{
        //    dgvChkProcess.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
        //    dgvChkProcess.ColumnCount = 3;

        //    int i = 0;

        //    dgvChkProcess.Columns[i].Name = "RowSeq";
        //    dgvChkProcess.Columns[i].HeaderText = "";
        //    dgvChkProcess.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkProcess.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    dgvChkProcess.Columns[i].ReadOnly = true;
        //    dgvChkProcess.Columns[i].Visible = true;
            
        //    dgvChkProcess.Columns[++i].Name = "ProcessID";
        //    dgvChkProcess.Columns[i].HeaderText = "ProcessID";
        //    dgvChkProcess.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkProcess.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    dgvChkProcess.Columns[i].ReadOnly = true;
        //    dgvChkProcess.Columns[i].Visible = false;
            
        //    dgvChkProcess.Columns[++i].Name = "Process";
        //    dgvChkProcess.Columns[i].HeaderText = "공정";
        //    dgvChkProcess.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        //    dgvChkProcess.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //    dgvChkProcess.Columns[i].ReadOnly = true;
        //    dgvChkProcess.Columns[i].Visible = true;

        //    DataGridViewCheckBoxColumn chkAgingYN = new DataGridViewCheckBoxColumn();
        //    chkAgingYN.HeaderText = "숙성";
        //    chkAgingYN.Name = "chkAgingYN";
        //    chkAgingYN.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    chkAgingYN.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkProcess.Columns.Insert(3, chkAgingYN);

        //    DataGridViewCheckBoxColumn chkDefectYN = new DataGridViewCheckBoxColumn();
        //    chkDefectYN.HeaderText = "배치";
        //    chkDefectYN.Name = "chkDefectYN";
        //    chkDefectYN.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    chkDefectYN.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkProcess.Columns.Insert(4, chkDefectYN);

        //    DataGridViewCheckBoxColumn chkEffectYN = new DataGridViewCheckBoxColumn();
        //    chkEffectYN.HeaderText = "유효";
        //    chkEffectYN.Name = "chkEffectYN";
        //    chkEffectYN.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    chkEffectYN.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkProcess.Columns.Insert(5, chkEffectYN);

        //    DataGridViewCheckBoxColumn chkFifoYN = new DataGridViewCheckBoxColumn();
        //    chkFifoYN.HeaderText = "선입";
        //    chkFifoYN.Name = "chkFifoYN";
        //    chkFifoYN.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    chkFifoYN.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkProcess.Columns.Insert(6, chkFifoYN);

        //    dgvChkProcess.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
        //    dgvChkProcess.RowTemplate.Height = 30;
        //    dgvChkProcess.ColumnHeadersHeight = 18;
        //    dgvChkProcess.ScrollBars = ScrollBars.Both;
        //    dgvChkProcess.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    dgvChkProcess.MultiSelect = false;
        //    dgvChkProcess.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkProcess.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
        //    //grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //    dgvChkProcess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //    dgvChkProcess.ReadOnly = true;

        //    foreach (DataGridViewColumn col in dgvChkProcess.Columns)
        //    {
        //        col.DataPropertyName = col.Name;
        //        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        col.SortMode = DataGridViewColumnSortMode.NotSortable;
        //    }
        //}

        //private void InitGridChkLabel()
        //{
        //    dgvChkLabel.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
        //    dgvChkLabel.ColumnCount = 4;

        //    int i = 0;

        //    dgvChkLabel.Columns[i].Name = "RowSeq";
        //    dgvChkLabel.Columns[i].HeaderText = "";
        //    dgvChkLabel.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkLabel.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    dgvChkLabel.Columns[i].ReadOnly = true;
        //    dgvChkLabel.Columns[i].Visible = true;

        //    dgvChkLabel.Columns[++i].Name = "TagID";
        //    dgvChkLabel.Columns[i].HeaderText = "TagID";
        //    dgvChkLabel.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkLabel.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    dgvChkLabel.Columns[i].ReadOnly = true;
        //    dgvChkLabel.Columns[i].Visible = false;

        //    dgvChkLabel.Columns[++i].Name = "Tag";
        //    dgvChkLabel.Columns[i].HeaderText = "라벨명";
        //    dgvChkLabel.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        //    dgvChkLabel.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //    dgvChkLabel.Columns[i].ReadOnly = true;
        //    dgvChkLabel.Columns[i].Visible = true;

        //    dgvChkLabel.Columns[++i].Name = "TagType";
        //    dgvChkLabel.Columns[i].HeaderText = "TagType";
        //    dgvChkLabel.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        //    dgvChkLabel.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    dgvChkLabel.Columns[i].ReadOnly = true;
        //    dgvChkLabel.Columns[i].Visible = false;

        //    DataGridViewCheckBoxColumn chkAgingYN = new DataGridViewCheckBoxColumn();
        //    chkAgingYN.HeaderText = "숙성";
        //    chkAgingYN.Name = "chkAgingYN";
        //    chkAgingYN.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    chkAgingYN.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkLabel.Columns.Insert(4, chkAgingYN);

        //    DataGridViewCheckBoxColumn chkDefectYN = new DataGridViewCheckBoxColumn();
        //    chkDefectYN.HeaderText = "배치";
        //    chkDefectYN.Name = "chkDefectYN";
        //    chkDefectYN.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    chkDefectYN.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkLabel.Columns.Insert(5, chkDefectYN);

        //    DataGridViewCheckBoxColumn chkEffectYN = new DataGridViewCheckBoxColumn();
        //    chkEffectYN.HeaderText = "유효";
        //    chkEffectYN.Name = "chkEffectYN";
        //    chkEffectYN.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    chkEffectYN.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkLabel.Columns.Insert(6, chkEffectYN);

        //    DataGridViewCheckBoxColumn chkFifoYN = new DataGridViewCheckBoxColumn();
        //    chkFifoYN.HeaderText = "선입";
        //    chkFifoYN.Name = "chkFifoYN";
        //    chkFifoYN.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //    chkFifoYN.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkLabel.Columns.Insert(7, chkFifoYN);

        //    dgvChkLabel.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
        //    dgvChkLabel.RowTemplate.Height = 30;
        //    dgvChkLabel.ColumnHeadersHeight = 18;
        //    dgvChkLabel.ScrollBars = ScrollBars.Both;
        //    dgvChkLabel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    dgvChkLabel.MultiSelect = false;
        //    dgvChkLabel.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    dgvChkLabel.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
        //    //grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //    dgvChkLabel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //    dgvChkLabel.ReadOnly = true;

        //    foreach (DataGridViewColumn col in dgvChkLabel.Columns)
        //    {
        //        col.DataPropertyName = col.Name;
        //        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        col.SortMode = DataGridViewColumnSortMode.NotSortable;
        //    }
        //}
        #endregion

        private void InitCol(string strName = "", string strHeader = "")
        {
            DataGridViewButtonColumn txtCol = new DataGridViewButtonColumn();
            {
                txtCol.HeaderText = strHeader;
                txtCol.Name = strName;
                if (strHeader.Length == 2)
                {
                    txtCol.Width = 50;
                }
                else if (strHeader.Length == 4)
                {
                    txtCol.Width = 100;
                }
                else
                {
                    txtCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
                txtCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                txtCol.Visible = true;
            }
            grdInstDate.Columns.Insert(grdInstDate.Columns.Count, txtCol);
        }

        private void SetScreen()
        {
            //패널 배치 및 조정          
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
                                    foreach (Control c in co.Controls)
                                    {
                                        c.Dock = DockStyle.Fill;
                                        c.Margin = new Padding(0, 0, 0, 0);
                                        foreach (Control ctl in c.Controls)
                                        {
                                            ctl.Dock = DockStyle.Fill;
                                            ctl.Margin = new Padding(0, 0, 0, 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadBaseSetting()
        {
            txtServer.Text =Frm_tprc_Main.gs.GetValue("SQLServer", "server", "WZServer");
            txtServer.Tag =Frm_tprc_Main.gs.GetValue("SQLServer", "server", "WZServer");
            txtDB.Text =Frm_tprc_Main.gs.GetValue("SQLServer", "Database", "WizMRP");
            txtDB.Tag =Frm_tprc_Main.gs.GetValue("SQLServer", "Database", "WizMRP");


            string com = "";
            int value = 0;

            cboComport.Items.Clear();
            //cboComport.Items.Add("");

            foreach (string comport in SerialPort.GetPortNames())
            {
                // 컴포트명이 4자리보다 클 시 5번째 자리가 숫자인지 쓰레기값인지 확인 후 저장
                if (comport.Length > 4)
                {
                    if (int.TryParse(comport.Substring(4, 1), out value))
                    {
                        com = comport;
                    }
                    else
                    {
                        com = comport.Substring(0, 4);
                    }
                }
                else
                {
                    com = comport;
                }
                cboComport.Items.Add(com);
            }

            StringBuilder WeightMeter = new StringBuilder();
            GetPrivateProfileString("COMPort", "WeightMeter", "", WeightMeter, 10, FilePath);
            if (WeightMeter.ToString() != "")
            {
                foreach (var comport in cboComport.Items)
                {
                    if (comport.ToString() == WeightMeter.ToString())
                    {
                        cboComport.SelectedItem = WeightMeter.ToString();
                    }
                }
            }
            
        }
        
        //DB에 있는 Process 값 조회해서 그리드뷰에 추가(USECLSS 체크되있는 애들만 가져올듯..??)
        private void FillGridPro()
         {
            ds = DataStore.Instance.ProcedureToDataSet("xp_Work_sProcess", null, false);
             
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    grdProcess.Rows.Add(
                        i + 1,                                  //No
                        false,                                  //체크여부
                        dr["PROCESSID"],            //공정코드
                        dr["PROCESS"]               //공정명
                        );
                    
                    list_ProcessID.Add(dr[Work_sProcess.PROCESSID].ToString());
                }
            }
        }

        //private void FillGridChkPro()
        //{
        //    int i = 0;
        //    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
        //    sqlParameter.Add("Seq", 0);
        //    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sSettingByTerm", sqlParameter, false);
            
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        dgvChkProcess.Rows.Add(++i,
        //                                dr["ProcessID"].ToString(),     //공정ID
        //                                dr["Process"].ToString(),       //공정명
        //                                false,                             //A : 숙성시간
        //                                false,                             //D : 배치검사
        //                                false,                             //E : 유효기간
        //                                false                              //F : 선입선출
        //                               );
        //        list_TermProcessID.Add(dr["ProcessID"].ToString());
        //    }
        //}
        
        //private void FillGridChkLbl()
        //{
        //    int i = 0;
        //    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
        //    sqlParameter.Add("Seq", 1);
        //    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sSettingByTerm", sqlParameter, false);

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        dgvChkLabel.Rows.Add(++i,
        //                                dr["TagID"].ToString(),                                     //라벨ID
        //                                dr["Tag"].ToString(),// + " / " + dr["TagType"].ToString(),    //라벨명+라벨약자
        //                                "",//dr["TagType"].ToString(),                                   //라벨약자
        //                                false,                                                      //A : 숙성시간
        //                                false,                                                      //D : 배치검사
        //                                false,                                                      //E : 유효기간
        //                                false                                                       //F : 선입선출
        //                               );
        //        list_TermLabelID.Add(dr["TagType"].ToString());
        //    }
        //}
        //그리드뷰의 셀클릭시 그리드뷰의체크박스 변경해주는 메소드
        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.RowIndex >= 0)
            {
                bool flag = (bool)dgv.Rows[e.RowIndex].Cells["Check"].Value;
                dgv.Rows[e.RowIndex].Cells["Check"].Value = !flag;
                if (dgv.Name == "grdMachine" && tabProMacTerm.SelectedIndex == 1)
                {
                    string MachineID = dgv.Rows[e.RowIndex].Cells["MachineID"].Value.ToString();
                    foreach (BoolValue bv in list_AllProcessID[intProIdx])
                    {
                        if (bv.value == MachineID)
                        {
                            bv.bl = !flag;
                            break;
                        }
                    }
                }
            }
        }

        private string checked_sProcess()
        {
            DataGridViewCell cell;
            DataRow dr;
            bool isChecked;
            for (int h = 0; h < grdProcess.Rows.Count; h++)
            {
                cell = grdProcess.Rows[h].Cells["Check"];
                isChecked = (bool)cell.EditedFormattedValue;
                if (isChecked)
                {
                    dr = ds.Tables[0].Rows[h];
                    if (sProcess == "")
                    {
                        sProcess = grdProcess.Rows[h].Cells["ProcessID"].Value.ToString();
                    }
                    else
                    {
                        sProcess = sProcess + "|" + grdProcess.Rows[h].Cells["ProcessID"].Value.ToString();
                    }
                }
            }
            return sProcess;
        }
        //Wizard.ini파일수정부분******** ProcessID 변경
        private void WriteProcess()
        {
           Frm_tprc_Main.gs.SetValue("Work", "ProcessID", checked_sProcess(), ConnectionInfo.filePath);
        }

        private void WriteMachine()
        {
           Frm_tprc_Main.gs.SetValue("Work", "Machine", GetMachineID(), ConnectionInfo.filePath);
        }

        private void WriteInstDate()
        {
           Frm_tprc_Main.gs.SetValue("Work", "Screen", GetInstDate(), ConnectionInfo.filePath);
        }

        //private void WriteProTerm()
        //{
        //   Frm_tprc_Main.gs.SetValue("Work", "ProTerm", GetChkProTerm(), ConnectionInfo.filePath);
        //}

        //private void WriteLblTerm()
        //{
        //   Frm_tprc_Main.gs.SetValue("Work", "LblTerm", GetChkLblTerm(), ConnectionInfo.filePath);
        //}

        private string GetInstDate()
        {
            int nDay = 0;
            string str = string.Empty;
            foreach (DataGridViewColumn dgvc in grdInstDate.Columns)
            {
                int.TryParse(grdInstDate.Rows[0].Cells[dgvc.Name].Value.ToString(), out nDay);
                if (nDay > 0)
                {
                    if (str == string.Empty)
                    {
                        str = dgvc.Name + "/" + nDay.ToString();
                    }
                    else
                    {
                        str = str + "|" + dgvc.Name + "/" + nDay.ToString();
                    }
                }
            }
            return str;
        }

        private void WriteComport()
        {
            if (cboComport.Items.Count > 0)
            {
                if (cboComport.SelectedIndex == -1)
                {
                    return;
                }
                else
                {
                   Frm_tprc_Main.gs.SetValue("Comport", "WeightMeter", cboComport.SelectedItem.ToString(), ConnectionInfo.filePath);
                }
            }
            
        }

        private string GetMachineID()
        {
            string sMachineID = "";
            int i = 0;
            foreach (List<WizCommon.BoolValue> list_bv in list_AllProcessID)
            {
                i = list_AllProcessID.IndexOf(list_bv);
                foreach (WizCommon.BoolValue bv in list_bv)
                {
                    if (bv.bl)
                    {
                        if (sMachineID == "")
                        {
                            sMachineID = grdProcess.Rows[i].Cells["ProcessID"].Value.ToString() + bv.value;
                        }
                        else
                        {
                            sMachineID = sMachineID + "|" + grdProcess.Rows[i].Cells["ProcessID"].Value.ToString() + bv.value;
                        }
                    }
                }
                
            }
            return sMachineID;
        }

        private void LoadGridInstDate()
        {
            string[] sInstDate =Frm_tprc_Main.gs.GetValue("Work", "Screen", "Screen").Split('|');
            string[] sValue = null;
            int intValue = 0;
            foreach (DataGridViewColumn dgvc in grdInstDate.Columns)
            {
                foreach (string Date in sInstDate)
                {
                    if (Date.ToUpper().Contains(dgvc.Name.ToUpper()))
                    {
                        sValue = Date.Split('/');
                        int.TryParse(sValue[1], out intValue);
                        grdInstDate.Rows[0].Cells[dgvc.Name].Value = intValue;
                    }
                }
                
            }
        }

        //그리드뷰 체크박스 셋팅(전체체크박스 초기화 후 ProcessID와 그리드뷰의 ProcessID 비교 후 같은 값 체크해준다)
        private void LoadGridPro()
        {
            string[] sProcessID = new string[ds.Tables[0].Rows.Count];
            sProcessID =Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID").Split('|');//배열에 프로세스아이디 넣기
            //그리드뷰 체크박스 초기화
            for (int k = 0; k < grdProcess.Rows.Count; k++)
            {
                grdProcess.Rows[k].Cells["Check"].Value = false;
            }
            for (int i = 0; i < sProcessID.Length; i++)
            {
                for (int j = 0; j < grdProcess.Rows.Count; j++)
                {
                    Console.WriteLine(grdProcess.Rows[j].Cells["ProcessID"].ToString());
                    if (sProcessID[i].ToString() == grdProcess.Rows[j].Cells["ProcessID"].Value.ToString())
                    {
                        grdProcess.Rows[j].Cells["Check"].Value = true;
                        break;
                    }
                }
            }
        }

        //그리드뷰 체크박스 셋팅(전체체크박스 초기화 후 ProcessID와 그리드뷰의 ProcessID 비교 후 같은 값 체크해준다)
        //private void LoadGridChkProLbl()
        //{
        //    string[] sProTerm =Frm_tprc_Main.gs.GetValue("Work", "ProTerm", "ProTerm").Split('|');//배열에 프로세스아이디 넣기
        //    string[] sLblTerm =Frm_tprc_Main.gs.GetValue("Work", "LblTerm", "LblTerm").Split('|');//배열에 프로세스아이디 넣기
        //    //그리드뷰 체크박스 초기화
        //    //공정별 조건
        //    foreach (DataGridViewRow dgvr in dgvChkProcess.Rows)
        //    {
        //        foreach (DataGridViewCell dgvc in dgvr.Cells)
        //        {
        //            if (dgvc.OwningColumn.CellType == typeof(DataGridViewCheckBoxCell))
        //            {
        //                dgvc.Value = false;
        //            }
        //        }
        //    }
        //    //라벨별 조건
        //    foreach (DataGridViewRow dgvr in dgvChkLabel.Rows)
        //    {
        //        foreach (DataGridViewCell dgvc in dgvr.Cells)
        //        {
        //            if (dgvc.OwningColumn.CellType == typeof(DataGridViewCheckBoxCell))
        //            {
        //                dgvc.Value = false;
        //            }
        //        }
        //    }

        //    //공정들
        //    foreach (string ProTerm in sProTerm)
        //    {
        //        foreach (DataGridViewRow dgvr in dgvChkProcess.Rows)
        //        {
        //            if (dgvr.Cells["ProcessID"].Value.ToString() == ProTerm.Substring(0, 4))// ProcessID가 같을때
        //            {
        //                if (ProTerm.Substring(4, 1).ToUpper() == "A")
        //                {
        //                    dgvr.Cells["chkAgingYN"].Value = true;
        //                    break;
        //                }
        //                else if (ProTerm.Substring(4, 1).ToUpper() == "D")
        //                {
        //                    dgvr.Cells["chkDefectYN"].Value = true;
        //                    break;
        //                }
        //                else if (ProTerm.Substring(4, 1).ToUpper() == "E")
        //                {
        //                    dgvr.Cells["chkEffectYN"].Value = true;
        //                    break;
        //                }
        //                else if (ProTerm.Substring(4, 1) == "F")
        //                {
        //                    dgvr.Cells["chkFifoYN"].Value = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    //라벨용
        //    foreach (string LblTerm in sLblTerm)
        //    {
        //        foreach (DataGridViewRow dgvr in dgvChkLabel.Rows)//공정들
        //        {
        //            if (dgvr.Cells["TagID"].Value.ToString() == LblTerm.Substring(1, 3) && // TagID가 같고
        //                dgvr.Cells["TagType"].Value.ToString() == LblTerm.Substring(0, 1)) // TagType이 같을때
        //            {
        //                if (LblTerm.Substring(4, 1) == "A")
        //                {
        //                    dgvr.Cells["chkAgingYN"].Value = true;
        //                    break;
        //                }
        //                else if (LblTerm.Substring(4, 1) == "D")
        //                {
        //                    dgvr.Cells["chkDefectYN"].Value = true;
        //                    break;
        //                }
        //                else if (LblTerm.Substring(4, 1) == "E")
        //                {
        //                    dgvr.Cells["chkEffectYN"].Value = true;
        //                    break;
        //                }
        //                else if (LblTerm.Substring(4, 1) == "F")
        //                {
        //                    dgvr.Cells["chkFifoYN"].Value = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        //private string GetChkProTerm()
        //{
        //    string sProTerm = "";       //check된 cell을 ini에 저장할 변수
        //    string Term = "";           //컬럼별 조건약자 저장할 변수
        //    foreach (string sProcessID in list_TermProcessID)
        //    {
        //        foreach (DataGridViewRow dgvr in dgvChkProcess.Rows)
        //        {
        //            if (sProcessID == dgvr.Cells["ProcessID"].Value.ToString())
        //            {
        //                foreach (DataGridViewCell dgvc in dgvr.Cells)
        //                {
        //                    if (dgvc.OwningColumn.CellType == typeof(DataGridViewCheckBoxCell))
        //                    {
        //                        if (dgvc.Value.ToString().ToUpper() == "TRUE")
        //                        {
        //                            Term = dgvc.OwningColumn.Name.Substring(3, 1).ToUpper();
        //                            if (sProTerm == "")
        //                            {
        //                                sProTerm = sProcessID + Term;
        //                            }
        //                            else
        //                            {
        //                                sProTerm = sProTerm + "|" + sProcessID + Term;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return sProTerm;
        //}

        //private string GetChkLblTerm()
        //{
        //    string sLblTerm = "";       //check된 cell을 ini에 저장할 변수
        //    string Term = "";           //컬럼별 조건약자 저장할 변수
        //    string TagID = "";
        //    foreach (string sLabelID in list_TermLabelID)
        //    {
        //        foreach (DataGridViewRow dgvr in dgvChkLabel.Rows)
        //        {
        //            if (sLabelID.ToUpper() == dgvr.Cells["TagType"].Value.ToString().ToUpper())
        //            {
        //                foreach (DataGridViewCell dgvc in dgvr.Cells)
        //                {
        //                    if (dgvc.OwningColumn.CellType == typeof(DataGridViewCheckBoxCell))
        //                    {
        //                        if (dgvc.Value.ToString().ToUpper() == "TRUE")
        //                        {
        //                            Term = dgvc.OwningColumn.Name.Substring(3, 1).ToUpper();
        //                            TagID = dgvr.Cells["TagID"].Value.ToString();
        //                            if (sLblTerm == "")
        //                            {
        //                                sLblTerm = sLabelID.ToUpper() + TagID + Term;
        //                            }
        //                            else
        //                            {
        //                                sLblTerm = sLblTerm + "|" + sLabelID.ToUpper() + TagID + Term;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return sLblTerm;
        //}

        //설비 그리드뷰 체크박스 셋팅
        private void LoadGridMac(string strProcessID)
        {
            foreach (WizCommon.BoolValue bv in list_AllProcessID[intProIdx])
            {
                if (bv.bl)
                {
                    foreach (DataGridViewRow dgvr in grdMachine.Rows)
                    {
                        if (dgvr.Cells["MachineID"].Value.ToString() == bv.value)
                        {
                            dgvr.Cells["Check"].Value = true;
                        }
                    }
                }
            }
        }

        
        //아래 버튼, 그리드뷰 선택된 셀에서 아래로 이동
        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            if (tabProMacTerm.SelectedIndex == 0)
            {
                Lib.btnRowDown(grdProcess);
            }
            else
            {
                Lib.btnRowDown(grdMachine);
            }
        }
        //위 버튼, 그리드뷰 선택된 셀에서 위로 이동
        private void cmdRowUp_Click_1(object sender, EventArgs e)
        {
            if (tabProMacTerm.SelectedIndex == 0)
            {
                Lib.btnRowUp(grdProcess);
            }
            else
            {
                Lib.btnRowUp(grdMachine);
            }
            
        }

        private void cmdSQL_0_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMKeypad FK = new POPUP.Frm_CMKeypad("Server", txtServer.Text);
            if (FK.ShowDialog() == DialogResult.OK)
            {
                txtServer.Text = FK.tbInputText.Text;
            }
        }

        private void cmdSQL_1_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMKeypad FK = new POPUP.Frm_CMKeypad("DB", txtDB.Text);
            if (FK.ShowDialog() == DialogResult.OK)
            {
                txtDB.Text = FK.tbInputText.Text;
            }
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            SetScreen();
            InitGridPro();
            InitGridMac();
            InitGridInstDate();
            //InitGridChkProcess();
            //InitGridChkLabel();
            cboComport.Text = "";
            FillGridPro();                                      //DB에 있는 Process 값 조회해서 그리드뷰에 추가
            LoadGridPro();                                      //그리드뷰 체크박스 셋팅
            LoadGridInstDate();
            LoadBaseSetting();                                  //Server,DB정보 불러오기
            SetListMachineByProcessID();
            //FillGridChkPro();
            //FillGridChkLbl();
            //LoadGridChkProLbl();
            SetListProTermByProcessID();
        }

        private void tabProMac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabProMacTerm.SelectedIndex == 1)
            {
                if (grdProcess.SelectedRows.Count > 0)
                {
                    strProID = grdProcess.SelectedRows[0].Cells["ProcessID"].Value.ToString();
                    intProIdx = grdProcess.SelectedRows[0].Index;
                    GetMachineCombo(strProID);
                    LoadGridMac(strProID);
                    return;
                }
            }
        }
        private void GetMachineCombo(string strProcess)
        {
            grdMachine.Rows.Clear();
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("ProcessID", strProcess);
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Work_sMachinebyProcess", sqlParameter, false);
            if (dt != null && dt.Rows.Count > 0)
            {
                int c = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    grdMachine.Rows.Add(
                                        ++c,
                                        false,
                                        dr["MachineID"].ToString(),
                                        dr["MachineNO"].ToString()
                                        );
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (txtServer.Text.ToString() != txtServer.Tag.ToString() || txtDB.Text.ToString() != txtDB.Tag.ToString())
            {
                if (MessageBox.Show("Server 혹은 DataBase 가 변경 되었습니다. 계속 진행하시겠습니까?", "확인 메세지", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                   Frm_tprc_Main.gs.SetValue("SQLServer", "server", txtServer.Text, ConnectionInfo.filePath);        //Wizard.ini server변경
                   Frm_tprc_Main.gs.SetValue("SQLServer", "Database", txtDB.Text, ConnectionInfo.filePath);          //Wizard.ini DB변경
                }
                else
                { return; }
            }
            WriteProcess();                                                                            //Wizard.ini파일수정부분******** ProcessID 변경                          
            WriteMachine();
            WriteComport();
            WriteInstDate();
            //WriteProTerm();
            //WriteLblTerm();

            //this.DialogResult = DialogResult.OK;

            Close();
        }
        private void SetListMachineByProcessID()
        {
            try
            {
                string[] sMachineID = null;
                sMachineID =Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine").Split('|');//배열에 설비아이디 넣기
                List<string> sMachine = new List<string>();
                foreach (string str in sMachineID)
                {
                    sMachine.Add(str);
                }
                //리스트에 머신 세팅 후 (db로드), 
                //ini에서 값 가져와서 체킹,

                //그리드뷰에서 체킹할때마다 리스트에 bool값 저장, 

                //최종저장시 리스트를 저장
                DataTable dt = null;
                //Dictionary<bool, string> blMachine = null;
                List<BoolValue> blMachine = new List<BoolValue>();
                Dictionary<string, object> sqlParameter = null;
                BoolValue bv = null;
                bool blChkOk = false;
                foreach (string strProcess in list_ProcessID)
                {
                    dt = null;
                    blMachine = null;
                    sqlParameter = null;
                    bv = null;

                    blMachine = new List<BoolValue>();
                    sqlParameter = new Dictionary<string, object>();
                    bv = new BoolValue();

                    sqlParameter.Add("ProcessID", strProcess);
                    dt = DataStore.Instance.ProcedureToDataTable("xp_Work_sMachinebyProcess", sqlParameter, false);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            bv = new BoolValue();
                            blChkOk = false;
                            
                            //ini값과 같으면 bool = true
                            foreach (string Mac in sMachine)
                            {
                                if (Mac.Length > 4)
                                {
                                    if (Mac.Substring(0, 4) == strProcess)
                                    {
                                        if (dr["MachineID"].ToString() == Mac.Substring(4, 2))
                                        {
                                            blChkOk = true;
                                            bv.bl = true;
                                            bv.value = dr["MachineID"].ToString();
                                            blMachine.Add(bv);
                                            break;
                                        }
                                    }
                                }
                                
                            }
                            if (!blChkOk)
                            {
                                bv.bl = false;
                                bv.value = dr["MachineID"].ToString();
                                blMachine.Add(bv);
                            }
                            else
                            {
                                sMachine.Remove(strProcess + dr["MachineID"].ToString());
                            }
                        }
                        list_AllProcessID.Add(blMachine);
                    }
                    else
                    {
                        bv = new BoolValue();
                        bv.bl = false;
                        bv.value = "";
                        blMachine.Add(bv);
                        list_AllProcessID.Add(blMachine);
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            
        }

        private void SetListProTermByProcessID()
        {
            try
            {
                string[] sProTerms = null;
                sProTerms =Frm_tprc_Main.gs.GetValue("Work", "ProTerm", "ProTerm").Split('|');//배열에 설비아이디 넣기
                List<string> sProTerm = new List<string>();
                List<string> sProTerm2 = new List<string>();//foreach문에서 sProTeam 바로 삭제 안되므로 sProTeam2로 foreach문

                List<string> AllTerm = new List<string>();
                AllTerm.Add("A");//AgingYN   숙성시간YN
                AllTerm.Add("D");//DefectYN  배치검사YN
                AllTerm.Add("E");//EffectYN  유효기간YN
                AllTerm.Add("F");//EffectYN  선입선출YN
                foreach (string str in sProTerms)
                {
                    sProTerm.Add(str);
                    sProTerm2.Add(str);
                }               
                //최종저장시 리스트를 저장
                ProBoolValue pbv = null;
                
                foreach (string sProcessID in list_TermProcessID)
                {
                    foreach (string Term in sProTerm2)
                    {
                        pbv = null;
                        pbv = new ProBoolValue();
                        if (Term.Length > 4)
                        {
                            if (Term.Substring(0, 4) == sProcessID)
                            {
                                foreach (string Tm in AllTerm)
                                {
                                    if (Term.Substring(4, 1) == Tm)
                                    {
                                        pbv.bl = true;
                                        pbv.value = Tm;
                                        pbv.pro = sProcessID;
                                        list_AllProTermProcessID.Add(pbv);
                                        sProTerm.Remove(sProcessID + Tm);
                                        break;
                                    }
                                }
                            }
                        }
                        
                    }
                }
                sProTerm2 = null;
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        private void SetListLblTermByTagID()
        {
            try
            {
                string[] sLblTerms = null;
                sLblTerms =Frm_tprc_Main.gs.GetValue("Work", "LblTerm", "LblTerm").Split('|');//배열에 설비아이디 넣기
                List<string> sLblTerm = new List<string>();
                List<string> sLblTerm2 = new List<string>();

                List<string> AllTerm = new List<string>();
                AllTerm.Add("A");//AgingYN   숙성시간YN
                AllTerm.Add("D");//DefectYN  배치검사YN
                AllTerm.Add("E");//EffectYN  유효기간YN
                AllTerm.Add("F");//EffectYN  선입선출YN
                foreach (string str in sLblTerms)
                {
                    sLblTerm.Add(str);
                    sLblTerm2.Add(str);
                }
                //최종저장시 리스트를 저장
                List<BoolValue> blTerm = new List<BoolValue>();
                Dictionary<string, object> sqlParameter = null;
                BoolValue bv = null;
                bool blChkOk = false;
                DataTable dt = null;

                sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("Seq", 1);
                dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sSettingByTerm", sqlParameter, false);

                foreach (DataRow dr in dt.Rows)
                {
                    dt = null;
                    blTerm = null;
                    sqlParameter = null;
                    bv = null;

                    blTerm = new List<BoolValue>();
                    bv = new BoolValue();
                    blChkOk = false;
                    foreach (string Term in sLblTerm2)
                    {
                        if (dr["TagType"].ToString() == Term.Substring(0, 1))//ex)I,M,T,C
                        {
                            if (dr["TagID"].ToString() == Term.Substring(1, 3))//ex)001,004,006,007
                            {
                                foreach (string Tm in AllTerm)
                                {
                                    if (Term.Substring(4, 1) == Tm)
                                    {
                                        blChkOk = true;
                                        bv.bl = true;
                                        bv.value = Tm;
                                        blTerm.Add(bv);
                                        break;
                                    }
                                    if (!blChkOk)
                                    {
                                        bv.bl = false;
                                        bv.value = Tm;
                                        blTerm.Add(bv);
                                    }
                                    else
                                    {
                                        sLblTerm.Remove(Term);
                                    }
                                }
                                list_AllLblTermProcessID.Add(blTerm);
                            }
                        }
                        else
                        {
                            bv.bl = false;
                            bv.value = "";
                            blTerm.Add(bv);
                            list_AllLblTermProcessID.Add(blTerm);
                        }
                    }
                }
                sLblTerm2 = null;
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        private void grdInstDate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ci = e.ColumnIndex;
            string colName = grdInstDate.Columns[ci].Name;
            string colHeader = grdInstDate.Columns[ci].HeaderText;
            string Value = "";
            if (colName == "입력")
            {
                if (grdInstDate.Rows.Count is 1 && e.RowIndex == 0)
                {
                    foreach (DataGridViewCell dgvc in grdInstDate.Rows[0].Cells)
                    {
                        if (dgvc.Value.ToString() == "입력")
                        {
                            return;
                        }

                        Value = dgvc.Value.ToString();
                        colHeader = grdInstDate.Columns[dgvc.ColumnIndex].HeaderText;
                        if (dgvc.OwningColumn.Name != "Comments")
                        {
                            FK = new WizCommon.Popup.Frm_CMNumericKeypad(Value, colHeader);
                            if (FK.ShowDialog() == DialogResult.OK)
                            {
                                double d = 0;
                                d = Lib.GetDouble(FK.tbInputText.Text.Trim());
                                dgvc.Value = string.Format("{0:n0}", d);
                            }
                            //btnKeyShift_Click();
                        }
                        else
                        {
                            if (dgvc.Value.ToString() == "입력")
                            {
                                return;
                            }
                            dgvc.Selected = true;
                            //btnKeyShift_Click();
                            FCK = new WizCommon.Popup.Frm_CMKeypad(Value, colHeader);
                            if (FCK.ShowDialog() == DialogResult.OK)
                            {
                                dgvc.Value = FCK.tbInputText.Text.Trim();
                            }
                        }

                    }
                }
            }
            else
            {
                if (grdInstDate.Rows.Count is 1 && e.RowIndex == 0)
                {
                    colHeader = grdInstDate.Columns[e.ColumnIndex].HeaderText;
                    Value = grdInstDate.Rows[e.RowIndex].Cells[colName].Value.ToString();
                    FK = new WizCommon.Popup.Frm_CMNumericKeypad(Value, colHeader);
                    if (FK.ShowDialog() == DialogResult.OK)
                    {
                        double d = 0;
                        d = Lib.GetDouble(FK.tbInputText.Text.Trim());
                        grdInstDate.Rows[e.RowIndex].Cells[colName].Value = string.Format("{0:n0}", d);
                    }
                }
            }
        }

        private void dgvChkTerm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                int r = e.RowIndex;
                int c = e.ColumnIndex;
                bool flag = false;
                if (dgv.Rows[r].Cells[c].OwningColumn.CellType == typeof(DataGridViewCheckBoxCell))
                {
                    flag = (bool)dgv.Rows[r].Cells[c].Value;
                    dgv.Rows[r].Cells[c].Value = !flag;
                }                
            }
        }

        //private void btnUpChkPro_Click(object sender, EventArgs e)
        //{
        //    Lib.btnRowUp(dgvChkProcess);
        //}

        //private void btnDownChkPro_Click(object sender, EventArgs e)
        //{
        //    Lib.btnRowDown(dgvChkProcess);
        //}

        //private void btnUpChkLbl_Click(object sender, EventArgs e)
        //{
        //    Lib.btnRowUp(dgvChkLabel);
        //}

        //private void btnDownChkLbl_Click(object sender, EventArgs e)
        //{
        //    Lib.btnRowDown(dgvChkLabel);
        //}

        private void btnTest_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == "frm_tprc_0401Working_U")
                {
                    foreach (Control con in openForm.Controls)
                    {
                        if (con.Name == "tlpForm")
                        {
                            foreach (Control ctl in con.Controls)
                            {
                                if (ctl.Name == "tlpRight")
                                {
                                    foreach (Control contr in ctl.Controls)
                                    {
                                        if (contr.Name == "btnTest")
                                        {
                                            contr.Visible = !contr.Visible;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
