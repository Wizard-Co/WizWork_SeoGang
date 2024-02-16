using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using WizCommon;
namespace WizWork
{
    public partial class frm_tins_InspectAuto_U : Form
    {
        string[] Message = new string[2];
        public string m_InspectBasisID;
        public string m_ArticleID;
        public string m_ModelID;
        public string m_InspectText;
        WizWorkLib Lib = new WizWorkLib();

        private string m_InspectPoint;
        private string m_InspectID;

        DataGridView dgv = null;

        List<string> ProcedureInfo = null;
        List<List<string>> ListProcedureName = new List<List<string>>();
        List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();
        WizWork.Tools.INI_GS gs = new WizWork.Tools.INI_GS();

        frm_tins_InspectText m_Popform_1 = null;
        POPUP.Frm_CMNumericKeypad m_Popform_2 = null;

        // 이미지 경로, 파일 이름
        string Path = "";
        string File = "";

        public static string initLabel = "";

        public Form mainForm = null;

        public frm_tins_InspectAuto_U()
        {
            InitializeComponent();
        }

        private void frm_tins_InspectAuto_U_Load(object sender, EventArgs e)
        {
            Size = new System.Drawing.Size(1004, 620);
            SetScreen();
            setComboBox();
            setClear();
            EnabledFalse();
            EnabledTrue();
            m_InspectPoint = "9";
            m_InspectID = "";
            m_InspectBasisID = "";
            txtLotNo.Select();
            txtLotNo.Focus();

            cboProcess.Enabled = false;

            txtPerson.Text = Frm_tprc_Main.g_tBase.Person.ToString().Trim();
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

        //
        public void SetPlanInputLabel(string label)
        {
            txtLotNo.Text = label;

            if (ScanLotNo())
            {
                SetcboEcoNO_Chg();
                SetJaju_Picture();
            }
        }

        private void cmdLotNo_Click(object sender, EventArgs e)
        {

            WizWork.POPUP.Frm_CMKeypad dlg = new WizWork.POPUP.Frm_CMKeypad();


            dlg.StartPosition = FormStartPosition.CenterParent;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtLotNo.Text = dlg.InputTextValue;
                if ((txtLotNo.Text.Trim().Length == 15 || txtLotNo.Text.Trim().Length == 16) && txtLotNo.Text.ToUpper().Trim().Contains("PL"))
                {
                    if (ScanLotNo())
                    {
                        SetcboEcoNO_Chg();
                        SetJaju_Picture();
                    }
                }
                else
                {
                    string Message = "작업지시목록의 LotID를 스캔해주세요.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message, "[바코드 오류]", 0, 1);
                }
            }

        }

        #region 조회조건 콤보 셋팅

        private void SetFMLCombo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(com_Code.SCODE);
            dt.Columns.Add(com_Code.CODENAME);

            DataRow rowAll = dt.NewRow();
            rowAll[com_Code.SCODE] = "";
            rowAll[com_Code.CODENAME] = "";

            DataRow row0 = dt.NewRow();
            row0[com_Code.SCODE] = "1";
            row0[com_Code.CODENAME] = "초";

            DataRow row1 = dt.NewRow();
            row1[com_Code.SCODE] = "2";
            row1[com_Code.CODENAME] = "중";

            DataRow row2 = dt.NewRow();
            row2[com_Code.SCODE] = "3";
            row2[com_Code.CODENAME] = "종";

            dt.Rows.Add(rowAll);
            dt.Rows.Add(row0);
            dt.Rows.Add(row1);
            dt.Rows.Add(row2);

            cboFML.DisplayMember = com_Code.CODENAME;
            cboFML.ValueMember = com_Code.SCODE;
            cboFML.DataSource = dt;

            cboFML.SelectedIndex = 1;
        }

        /// <summary>
        /// EcoNo 콤보 설정
        /// </summary>
        /// <param name="strArticleID"></param>
        /// <param name="strsPoint"></param>
        private void GetEcoNOCombo(string strArticleID, string ProcessID, string strsPoint)
        {
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("ArticleID", strArticleID);
            sqlParameter.Add("ProcessID", ProcessID);
            sqlParameter.Add("InspectPoint", strsPoint);

            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sInspectAutoBasisByArticleID", sqlParameter, false);
            if (dt != null && dt.Rows.Count > 0)
            {
                cboEcoNO.ValueMember = "InspectBasisID";
                cboEcoNO.DisplayMember = "EcoNo";
                cboEcoNO.DataSource = dt;
            }

        }
        /// <summary>
        /// 공정콤보 설정
        /// </summary>
        private void GetProcessCombo()
        {
            string strProcessID =Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID");
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add(Work_sProcess.NCHKPROC, "1");
            sqlParameter.Add(Work_sProcess.PROCESSID, strProcessID);

            DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_Work_sProcess]", sqlParameter, false);

            if (dt != null && dt.Rows.Count > 0)
            {

                cboProcess.DataSource = dt;// ds.Tables[0];
                cboProcess.ValueMember = "ProcessID";
                cboProcess.DisplayMember = "Process";
                if (dt.Rows.Count > 1)
                {
                    cboProcess.SelectedIndex = 0;
                }
            }

        }

        /// <summary>
        /// 호기콤보설정
        /// </summary>
        private void GetMachineCombo(string strProcess)
        {
            cboMachine.DataSource = null;
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("ProcessID", strProcess);

            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Work_sMachinebyProcess", sqlParameter, false);
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
                    sMachine.Remove(strProcess + dr["MachineID"].ToString());
                }
            }

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                cboMachine.DataSource = dt2;
                cboMachine.ValueMember = "MachineID";
                cboMachine.DisplayMember = "MachineNo";
                //if (dt2.Rows.Count > 1)
                //{
                //    cboMachine.SelectedIndex = 0;
                //}
            }
            dt = null;
        }
        #endregion



        /// <summary>
        /// 화면 초기화
        /// </summary>
        private void setClear()
        {
            m_InspectBasisID = "";
            m_ArticleID = "";
            m_InspectText = "";

            txtArticle.Text = "";   //pnlArticle
            txtBuyerArticleNo.Text = "";
            txtLotNo.Text = "";
            mtb_Date.Text = DateTime.Today.ToString("yyyyMMdd");

            btnInputData.Enabled = false;

            cboEcoNO.DataSource = null;

            picImg.Tag = null;
            picImg.Image = null;

            InitgrdInsItemGrid();
            InitTabPage();
        }

        private void setComboBox()
        {
            GetProcessCombo();  // 공정 Process 추가
            SetFMLCombo();
        }


        /// <summary>
        /// 공정콤보 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMachineCombo(this.cboProcess.SelectedValue.ToString());
            return;

        }

        private bool ScanLotNo()
        {
            if (txtLotNo.Text == "")
            {
                return false;
            }

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("LotNo", txtLotNo.Text.ToUpper().Trim());
                sqlParameter.Add("InspectPoint", m_InspectPoint);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sLotNoByInspectPoint", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (Lib.CheckNull(dr["InspectBasisID"].ToString()) == "")
                    {
                        txtLotNo.Text = "";
                        txtLotNo.Select();
                        txtLotNo.Focus();
                        WizCommon.Popup.MyMessageBox.ShowBox("잘못된 LotNo이거나 \r\n " +
                            "검사기준등록화면에서 해당 품목의 검사기준데이터를 확인해 주세요.", "[확인]", 0, 1);
                        return false;
                    }
                    else
                    {
                        SetFormValue(dt);
                        //txtLotNo.Text = "";
                        return true;
                    }
                }
                else
                {
                    txtLotNo.Text = "";
                    txtLotNo.Select();
                    txtLotNo.Focus();
                    WizCommon.Popup.MyMessageBox.ShowBox("잘못된 LotNo이거나 검사기준등록을 확인하세요.", "[확인]", 0, 1);
                    return false;
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
                txtLotNo.Text = "";
                txtLotNo.Select();
                txtLotNo.Focus();
                return false;
            }

        }

        private void SetFormValue(DataTable argsDt)
        {
            string strProcess = "";
            string strEcoNo = "";
            m_ArticleID = argsDt.Rows[0]["ArticleID"].ToString();
            m_InspectBasisID = argsDt.Rows[0]["InspectBasisID"].ToString();
            txtArticle.Text = argsDt.Rows[0]["Article"].ToString();
            txtBuyerArticleNo.Text = argsDt.Rows[0]["BuyerArticleNo"].ToString();
            txtLotNo.Tag = txtLotNo.Text.ToString();
            txtLotNo.Text = "";
            strProcess = argsDt.Rows[0]["ProcessID"].ToString();
            m_ModelID = argsDt.Rows[0]["BuyerModelID"].ToString();
            strEcoNo = argsDt.Rows[0]["EcoNo"].ToString();

            if (strProcess != "")
            {
                foreach (var item in cboProcess.Items)
                {
                    DataRowView dr = (DataRowView)item;
                    if (dr["ProcessID"].ToString().Contains(strProcess))
                    {
                        cboProcess.SelectedIndex = cboProcess.FindStringExact(dr["Process"].ToString());
                        break;
                    }
                }
            }
            else
            {
                this.cboProcess.SelectedIndex = 0;
            }

            //선택된 품명이 있을 경우
            if (this.txtArticle.Text != "")
            {
                if (m_ArticleID != "")
                {
                    GetEcoNOCombo(m_ArticleID, strProcess, m_InspectPoint);
                    if (cboEcoNO.Items.Count > 0)
                    {
                        foreach (DataRowView drv in cboEcoNO.Items)
                        {
                            if (strEcoNo == drv["EcoNo"].ToString())
                            {
                                cboEcoNO.SelectedValue = drv["InspectBasisID"].ToString();
                                break;
                            }
                        }
                    }
                }
            }
            return;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            EnabledTrue();

            m_InspectID = "";
            m_InspectBasisID = "";
            this.txtLotNo.Focus();

        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            //setClear();
            //EnabledFalse();
            //FillGridData("0");

            //if (CheckAllInsertedValue() == false)
            //{
            //    MessageBox.Show("없어!!");
            //}
            //else
            //{
            //    MessageBox.Show("다 됬어!!");
            //}

            // 2020.11.16 GDU, 결과값만 초기화 되도록 수정
            clearAllInsertedValue();

            if (checkAllInsertedValue() == false)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }
        }

        private void EnabledFalse()
        {
            mtb_Date.Enabled = false;
            btnCal_Date.Enabled = false;
            cboFML.Enabled = false;
            cboProcess.Enabled = false;
            cboMachine.Enabled = false;
            txtLotNo.Enabled = false;
            btnInputData.Enabled = false;
            cboEcoNO.Enabled = false;

            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnInit.Enabled = false;
            btndgvInspectClear.Enabled = false;
            btnClose.Enabled = true;

            //cmdAddNew.Enabled = True
            //cmdSave.Enabled = False
            //cmdInit.Enabled = False
        }

        private void EnabledTrue()
        {
            btnCal_Date.Enabled = true;
            mtb_Date.Enabled = true;
            txtLotNo.ReadOnly = false;
            txtLotNo.Enabled = true;

            cboFML.Enabled = true;
            cboProcess.Enabled = true;
            cboMachine.Enabled = true;
            cboEcoNO.Enabled = true;

            btnSave.Enabled = true;
            btnInit.Enabled = true;
            btnClose.Enabled = true;
            btndgvInspectClear.Enabled = true;
        }

        private void InitgrdInsItemGrid()
        {
            grdInsItem.Columns.Clear();
            grdInsItem.ColumnCount = 13;

            // Set the Colums Hearder Names
            grdInsItem.Columns[0].Name = "No";
            grdInsItem.Columns[0].HeaderText = "";
            grdInsItem.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            grdInsItem.Columns[1].Name = "insItemName";
            grdInsItem.Columns[1].HeaderText = "항  목";
            //grdInsItem.Columns[1].Width = 250;
            grdInsItem.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdInsItem.Columns[1].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            grdInsItem.Columns[2].Name = "InsSpec";
            grdInsItem.Columns[2].HeaderText = "스펙";
            //grdInsItem.Columns[2].Width = 300;
            grdInsItem.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdInsItem.Columns[2].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            grdInsItem.Columns[3].Name = "InsTPSpecMax";
            grdInsItem.Columns[3].HeaderText = "정성적 Max";
            grdInsItem.Columns[3].Width = 0;
            grdInsItem.Columns[3].Visible = false;

            grdInsItem.Columns[4].Name = "InsTPSpecMin";
            grdInsItem.Columns[4].HeaderText = "정성적 Min";
            grdInsItem.Columns[4].Width = 0;
            grdInsItem.Columns[4].Visible = false;

            grdInsItem.Columns[5].Name = "InsRASpecMax";
            grdInsItem.Columns[5].HeaderText = "정량적 Max";
            grdInsItem.Columns[5].Width = 0;
            grdInsItem.Columns[5].Visible = false;

            grdInsItem.Columns[6].Name = "InsRASpecMin";
            grdInsItem.Columns[6].HeaderText = "정량적 Min";
            grdInsItem.Columns[6].Width = 0;
            grdInsItem.Columns[6].Visible = false;

            grdInsItem.Columns[7].Name = "InsSampleQty";
            grdInsItem.Columns[7].HeaderText = "샘플수량";
            grdInsItem.Columns[7].Width = 0;
            grdInsItem.Columns[7].Visible = false;


            grdInsItem.Columns[8].Name = "InsType";
            grdInsItem.Columns[8].HeaderText = "정량/정성 타입";
            grdInsItem.Columns[8].Width = 0;
            grdInsItem.Columns[8].Visible = false;

            grdInsItem.Columns[9].Name = "SubSeq";
            grdInsItem.Columns[9].HeaderText = "InspectBasisSub";
            grdInsItem.Columns[9].Width = 0;
            grdInsItem.Columns[9].Visible = false;

            grdInsItem.Columns[10].Name = "MinValue";
            grdInsItem.Columns[10].HeaderText = "최소값";
            grdInsItem.Columns[10].Width = 80;
            grdInsItem.Columns[10].Visible = false;

            grdInsItem.Columns[11].Name = "MaxValue";
            grdInsItem.Columns[11].HeaderText = "최대값";
            grdInsItem.Columns[11].Width = 80;
            grdInsItem.Columns[11].Visible = false;

            grdInsItem.Columns[12].Name = "DefectYN";
            grdInsItem.Columns[12].HeaderText = "합/불";
            //grdInsItem.Columns[12].Width = 80;
            grdInsItem.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdInsItem.Columns[12].Visible = true;

            grdInsItem.Rows.Clear();
            grdInsItem.RowTemplate.Height = 33;

            grdInsItem.ReadOnly = true;
            grdInsItem.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            //grdData.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10F);
            grdInsItem.RowTemplate.Height = 30;
            grdInsItem.ColumnHeadersHeight = 35;
            grdInsItem.ScrollBars = ScrollBars.Both;
            grdInsItem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdInsItem.MultiSelect = false;
            grdInsItem.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdInsItem.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdInsItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in grdInsItem.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        private void cboEcoNO_KeyUp(object sender, KeyEventArgs e)
        {

        }


        private void SetcboEcoNO_Chg()
        {

            string strCboEccno = "";
            try
            {
                strCboEccno = cboEcoNO.SelectedValue.ToString();
            }
            catch (Exception e2)
            {
                Console.Write(e2.Message);
                strCboEccno = "";
            }
            if (strCboEccno != "")
            {
                m_InspectBasisID = strCboEccno.Trim();
            }
            if (cboEcoNO.Items.Count > 0)
            {
                FillGridInsItem();
            }
            else
            {
                InitgrdInsItemGrid();
                InitTabPage();
                WizCommon.Popup.MyMessageBox.ShowBox("잘못된 LotNo이거나 검사기준등록을 확인하세요.", "[확인]", 0, 1);
                txtLotNo.Text = "";
                txtLotNo.Focus();
            }
            return;
        }
        private void FillGridInsItem()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("InspectBasisID", m_InspectBasisID);
                sqlParameter.Add("InspectPoint", m_InspectPoint);

                DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sInspectAutoBasisSub", sqlParameter, false);
                DataRow dr = null;

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    tabControl1.TabPages.Clear();
                    grdInsItem.Rows.Clear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = null;
                        dr = ds.Tables[0].Rows[i];
                        if (dr["insType"].ToString().Trim() == "1")
                        {
                            grdInsItem.Rows.Add(i + 1,
                                                dr["insItemName"],
                                                dr["InsTPSpec"],
                                                dr["InsTPSpecMax"],
                                                dr["InsTPSpecMin"],
                                                dr["InsRASpecMax"],
                                                dr["InsRASpecMin"],
                                                dr["InsSampleQty"],
                                                dr["insType"],
                                                dr["SubSeq"],
                                                dr["InsTPSpecMin"],
                                                dr["InsTPSpecMax"],
                                                ""
                                                );
                        }
                        else
                        {
                            grdInsItem.Rows.Add(i + 1,
                                                dr["insItemName"],
                                                dr["InsRASpec"],
                                                dr["InsTPSpecMax"],
                                                dr["InsTPSpecMin"],
                                                dr["InsRASpecMax"],
                                                dr["InsRASpecMin"],
                                                dr["InsSampleQty"],
                                                dr["insType"],
                                                dr["SubSeq"],
                                                dr["InsRASpecMin"],
                                                dr["InsRASpecMax"],
                                                ""
                                               );

                        }

                        if (i == 0)
                        {
                            this.btnInputData.Enabled = true;
                            this.btnSave.Enabled = true;
                        }
                        FillGridData(dr["InsSampleQty"].ToString());
                    }

                }
                else
                {
                    this.txtLotNo.Text = "";
                    this.txtLotNo.Focus();
                    InitgrdInsItemGrid();
                    InitTabPage();
                    WizCommon.Popup.MyMessageBox.ShowBox("EcoNo를 확인하세요.", "[확인]", 0, 1);
                    return;
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

        /// <summary>
        /// 저장버튼 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdInsItem.Rows.Count > 0)
                {
                    if (cboMachine.SelectedValue.ToString().Trim().Equals("") || cboProcess.SelectedValue.ToString().Trim().Equals("")
                        || cboFML.SelectedValue.ToString().Trim().Equals(""))
                    {
                        Message[0] = "[저장실패]";
                        Message[1] = "공정, 설비, 초중종 중에서 선택되지 않은 항목이 있습니다.\r\n 확인해주세요.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    }
                           
                    if (SaveData())
                    {
                        //EnabledTrue();
                        WizCommon.Popup.MyMessageBox.ShowBox("검사 등록을 완료하였습니다.", "[검사등록완료]", 1, 1);
                        txtLotNo.Focus();

                        EnabledFalse();
                        FillGridData("0");
                        setClear();
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }    
        }

        private bool CheckData()
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("PersonID", Frm_tprc_Main.g_tBase.PersonID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sCheckPersonID", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0
                    && dt.Columns.Count == 1)
                {
                    if (dt.Rows[0]["Msg"].ToString().Equals("PASS"))
                    {
                        flag = true;
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox(dt.Rows[0]["Msg"].ToString().Replace("|", "\r\n"), "[작업자 정보 오류 - SaveData]", 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }

            return flag;
        }

        private bool SaveData()
        {
            if (CheckData() == false)
            {
                return false;
            }

            bool blResult = false;
            int intRowChkCount = 0;
            string strCboEccno = "";
            string sBasisID = "";
            int iSeq = 0;
            string sEco = "";
            int intSampleQty = 0;
            string sInsType = "";
            int intInspectBasisSubSeq = 0;
            string strstrInspectDate = "";
            DataGridView dgvSave = null;

            ListProcedureName.Clear();
            ListParameter.Clear();

            for (int j = 0; j < grdInsItem.Rows.Count; j++)
            {
                DataGridViewRow dr2 = grdInsItem.Rows[j];
                dgvSave = tabControl1.TabPages[j].Controls[0] as DataGridView;
                strCboEccno = cboEcoNO.SelectedValue.ToString().Trim();
                sEco = cboEcoNO.Text;
                intInspectBasisSubSeq = int.Parse(dr2.Cells["SubSeq"].Value.ToString().Trim());
                intSampleQty = int.Parse(dr2.Cells["InsSampleQty"].Value.ToString().Trim());
                sInsType = dr2.Cells["InsType"].Value.ToString().Trim();
                if (strCboEccno != "")
                {
                    sBasisID = strCboEccno.Trim();
                }
                if (intSampleQty == 0)
                {
                    continue;
                }
                intRowChkCount = 0;
                for (int i = 0; i < dgvSave.Rows.Count; i++)
                {
                    if (Lib.CheckNull(dgvSave["InspectValueText", i].Value.ToString()) == "")
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("비어있는 측정값으로 인한 오류입니다. \r\n" + (j + 1).ToString() + "번째 탭의 측정값을 모두 입력해주십시오. ", "[측정값 오류]", 0, 1);
                        return false;
                    }
                    else
                    {
                        intRowChkCount = intRowChkCount + 1;
                    }
                }
                if (intRowChkCount < dgvSave.Rows.Count)
                {
                    Message[0] = "[WizWork - 종료]";
                    Message[1] = "측정값의 갯수가 등록해야하는 갯수보다 작습니다. 이대로 등록하시겠습니까 ?";

                    if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.No)//NO
                    {
                        return false;
                    }
                }
            }
            try
            {
                string strComments = "";
                string strInspectGubun = "2";
                string strInspectLevel = "1";
                string strSketchPath = "";
                string strSketchFile = "";
                string strAttachedPath = "";
                string strAttachedFile = "";
                string strInspectUserID = "";  // 추후 구현
                string strCreateUserID = "";  // 추후 구현
                string strsDefectYN = "";
                string strsProcessID = "";
                string strImportSecYN = "N";     // 보안 부품중요도
                string strImportlawYN = "N";     // 법규
                string strImportImpYN = "N";      // 중요
                string strImportNorYN = "N";      // 일반
                string strIRELevel = "3";       //   부품품질 위험도 -- 특별관리 고, 중요관리 중, 일반관리 저
                string strInpCustomID = "";
                string strInpDate = "";
                string strOutCustomID = "";
                string strOutDate = "";
                string strMachineID = "";
                string strFMLGubun = "";
                int intInspectBasisIDSeq = 1;
                if (strCboEccno != "")
                {
                    sBasisID = strCboEccno.Trim();
                }
                strstrInspectDate = mtb_Date.Text.Replace("-", ""); 
                strstrInspectDate = strstrInspectDate.Replace("/", "");
                if (Frm_tprc_Main.g_tBase.PersonID != "")
                {
                    strInspectUserID = Frm_tprc_Main.g_tBase.PersonID;
                    strCreateUserID = Frm_tprc_Main.g_tBase.PersonID;
                }
                else
                {
                    strInspectUserID = "";
                    strCreateUserID = Name.ToString();
                }
                bool blDefectYN = false;
                foreach (DataGridViewRow dgvr in grdInsItem.Rows)
                {
                    if (dgvr.Cells["DefectYN"].Value.ToString() == "불")
                    {
                        blDefectYN = true;
                        strsDefectYN = "Y";
                        break;
                    }
                }
                if (!blDefectYN)
                {
                    strsDefectYN = "N";
                }
                //strsDefectYN = LF_JudgeDefect();

                strsProcessID = cboProcess.SelectedValue.ToString();
                strMachineID = cboMachine.SelectedValue.ToString();
                strFMLGubun = cboFML.SelectedValue.ToString();
                ///////////////////    Ins_InspectAuto   DB SAve  ////////////////////
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTID, "");                 //pk
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.ARTICLEID, m_ArticleID);
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTGUBUN, strInspectGubun);    // 검사 구분  1:전수검사, 2:샘플검사, 3:일반검사 
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTDATE, strstrInspectDate);         // 입고일자
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.LOTID, txtLotNo.Tag.ToString().ToUpper()/*txtLotNo.Text.ToUpper()*/);                // lot ID

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTQTY, intRowChkCount);     // 검사수량
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.ECONO, sEco);               // ECO 번호
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.COMMENTS, strComments);         // COMMENTS
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTLEVEL, strInspectLevel);  // 검사수준

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.SKETCHPATH, strSketchPath);     // 약도파일 경로
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.SKETCHFILE, strSketchFile);     // 약도파일 명
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.ATTACHEDPATH, strAttachedPath);       // 첨부화일 경로
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.ATTACHEDFILE, strAttachedFile);       // 첨부화일 명
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTUSERID, strInspectUserID);      // 담당자
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.CREATEUSERID, strCreateUserID);       //

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.SINSPECTBASISID, sBasisID);
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTBASISIDSEQ ,intInspectBasisIDSeq); 
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.SDEFECTYN, strsDefectYN);           // 판정결과
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.SPROCESSID, strsProcessID);          // 공정
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTPOINT, m_InspectPoint);        // 1: 수입, 3:자주,5:출하

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.IMPORTSECYN, strImportSecYN);         // 보안 부품중요도  
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.IMPORTLAWYN, strImportlawYN);         // 법규
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.IMPORTIMPYN, strImportImpYN);         // 중요
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.IMPORTNORYN, strImportNorYN);         // 일반
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.IRELEVEL, strIRELevel);            // 부품품질 위험도 -- 특별관리 고, 중요관리 중, 일반관리 저 

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INPCUSTOMID, strInpCustomID);         // 입고거래처
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INPDATE, strInpDate);             // 입고일
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.OUTCUSTOMID, strOutCustomID);         // 출고거래처
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.OUTDATE, strOutDate);             // 출고일
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.MACHINEID, strMachineID);           // 설비 호기 

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.BUYERMODELID, m_ModelID);        //고객 모델 
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.FMLGUBUN, strFMLGubun);            // 초.중.종품 구분
                //sqlParameter.Add("DayOrNightID", Frm_tprc_Main.g_tBase.DayOrNightID.ToString().Trim());            // 초.중.종품 구분

                //ProcedureInfo List
                //0번 : 프로시저 이름, 1번 output yn, 2번 output 이름, 3번 output size(output y일때만)
                ProcedureInfo = new List<string>();
                ProcedureInfo.Add("xp_prdWork_iAutoInspect");
                ProcedureInfo.Add("Y");
                ProcedureInfo.Add("InspectID");
                ProcedureInfo.Add("10");

                ListProcedureName.Add(ProcedureInfo);
                ListParameter.Add(sqlParameter);
                ProcedureInfo = null;

                for (int j = 0; j < grdInsItem.Rows.Count; j++)
                {
                    DataGridViewRow dr2 = grdInsItem.Rows[j];

                    dgvSave = tabControl1.TabPages[j].Controls[0] as DataGridView;
                    dgv = tabControl1.TabPages[j].Controls[0] as DataGridView;
                    strCboEccno = cboEcoNO.SelectedValue.ToString().Trim();
                    sEco = cboEcoNO.Text;
                    intInspectBasisSubSeq = int.Parse(dr2.Cells["SubSeq"].Value.ToString().Trim());
                    intSampleQty = int.Parse(dr2.Cells["InsSampleQty"].Value.ToString().Trim());
                    sInsType = dr2.Cells["InsType"].Value.ToString().Trim();

                    if (intSampleQty == 0)
                    {
                        continue;
                    }

                    if (m_InspectID == "")
                    {
                        blResult = SetAddNewInspectAuto(m_ArticleID, strInspectGubun, strstrInspectDate, this.txtLotNo.Text.ToUpper(),
                                                        intRowChkCount, sEco, strComments, strInspectLevel,
                                                        strSketchPath, strSketchFile, strAttachedPath, strAttachedFile,
                                                        strInspectUserID, strCreateUserID, sBasisID, iSeq,
                                                        strsDefectYN, strsProcessID, m_InspectPoint, strImportSecYN,
                                                        strImportlawYN, strImportImpYN, strImportNorYN, strIRELevel,
                                                        strInpCustomID, strInpDate, strOutCustomID, strOutDate,
                                                        strMachineID, m_ModelID, strFMLGubun, intInspectBasisSubSeq,
                                                        sInsType);
                    }
                    else
                    {
                        blResult = SetUpdateInspectAuto(m_InspectID, m_ArticleID, strInspectGubun, strstrInspectDate, this.txtLotNo.Text.ToUpper(),
                                                        intRowChkCount, sEco, strComments, strInspectLevel,
                                                        strSketchPath, strSketchFile, strAttachedPath, strAttachedFile,
                                                        strInspectUserID, strCreateUserID, sBasisID, iSeq,
                                                        strsDefectYN, strsProcessID, m_InspectPoint, strImportSecYN,
                                                        strImportlawYN, strImportImpYN, strImportNorYN, strIRELevel,
                                                        strInpCustomID, strInpDate, strOutCustomID, strOutDate,
                                                        strMachineID, m_ModelID, strFMLGubun, intInspectBasisSubSeq,
                                                        sInsType);
                    }
                }
                string[] Confirm = new string[2];
                Confirm = DataStore.Instance.ExecuteAllProcedureOutput(ListProcedureName, ListParameter);
                if (Confirm[0].ToLower() == "success")
                {
                    blResult = true;
                }
                else
                {
                    Message[0] = "[저장실패]";
                    Message[1] = Confirm[1].ToString();
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return blResult;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            return blResult;
        }

        /// <summary>
        /// 신규저장
        /// </summary>
        /// <returns></returns>
        private bool SetAddNewInspectAuto(string strArticleID, string strInspectGubun, string strInspectDate, string strLotID,
                                           int intInspectQty, string strECONo, string strComments, string strInspectLevel,
                                           string strSketchPath, string strSketchFile, string strAttachedPath, string strAttachedFile,
                                           string strInspectUserID, string strCreateUserID, string strsInspectBasisID, int intInspectBasisIDSeq,
                                           string strsDefectYN, string strsProcessID, string strInspectPoint, string strImportSecYN,
                                           string strImportlawYN, string strImportImpYN, string strImportNorYN, string strIRELevel,
                                           string strInpCustomID, string strInpDate, string strOutCustomID, string strOutDate,
                                           string strMachineID, string strBuyerModelID, string strFMLGubun, int intInspectBasisSubSeq,
                                           string strsInsType)
        {
            bool blResult = false;
            string strInspectID = "";
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////// Ins_InspectAuto ,Ins_InspectAutoSub  DB Save ///////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                //tabControl1.SelectTab(intInspectBasisSubSeq - 1);
                //dgv = tabControl1.SelectedTab.Controls[0] as DataGridView;

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    intInspectBasisIDSeq = 1;
                    /////////////////    Ins_InspectAuto   DB SAve  ////////////////////
                    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                    sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTID, strInspectID);
                    sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.SEQ, "");
                    sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTBASISID, strsInspectBasisID);
                    sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTBASISSEQ, intInspectBasisIDSeq);
                    sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTBASISSUBSEQ, intInspectBasisSubSeq);

                    
                    if (strsInsType == "1")
                    {
                        sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTVALUE, 0);
                        sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTTEXT, dgv[1, i].Value.ToString());
                    }
                    else
                    {
                        sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTVALUE, dgv[1, i].Value.ToString());
                        sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTTEXT, "");
                    }
                    sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.CREATEUSERID, Frm_tprc_Main.g_tBase.PersonID);
                    //ProcedureInfo List
                    //0번 : 프로시저 이름, 1번 output yn, 2번 output 이름, 3번 output size(output y일때만)
                    ProcedureInfo = new List<string>();
                    ProcedureInfo.Add("xp_WizIns_iAutoInspectSub");
                    ProcedureInfo.Add("N");
                    ProcedureInfo.Add("InspectID");
                    ProcedureInfo.Add("10");

                    ListProcedureName.Add(ProcedureInfo);
                    ListParameter.Add(sqlParameter2);
                    ProcedureInfo = null;


                }

            }

            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }



            return blResult;
        }
        private bool SetUpdateInspectAuto(string strInspectID, string strArticleID, string strInspectGubun, string strInspectDate, string strLotID,
                                          int intInspectQty, string strECONo, string strComments, string strInspectLevel,
                                          string strSketchPath, string strSketchFile, string strAttachedPath, string strAttachedFile,
                                          string strInspectUserID, string strCreateUserID, string strsInspectBasisID, int intInspectBasisIDSeq,
                                          string strsDefectYN, string strsProcessID, string strInspectPoint, string strImportSecYN,
                                          string strImportlawYN, string strImportImpYN, string strImportNorYN, string strIRELevel,
                                          string strInpCustomID, string strInpDate, string strOutCustomID, string strOutDate,
                                          string strMachineID, string strBuyerModelID, string strFMLGubun, int intInspectBasisSubSeq,
                                          string strsInsType)
        {

            bool blResult = false;


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////// Ins_InspectAuto ,Ins_InspectAutoSub  DB Save ///////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                ///////////////////    Ins_InspectAuto   DB SAve  ////////////////////
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTID, strInspectID);                 //pk
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.ARTICLEID, strArticleID);
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTGUBUN, strInspectGubun);    // 검사 구분  1:전수검사, 2:샘플검사, 3:일반검사 
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTDATE, strInspectDate);         // 입고일자
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.LOTID, strLotID);                // lot ID

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTQTY, intInspectQty);     // 검사수량
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.ECONO, strECONo);               // ECO 번호
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.COMMENTS, strComments);         // COMMENTS
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTLEVEL, strInspectLevel);  // 검사수준

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.SKETCHPATH, strSketchPath);     // 약도파일 경로
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.SKETCHFILE, strSketchFile);     // 약도파일 명
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.ATTACHEDPATH, strAttachedPath);       // 첨부화일 경로
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.ATTACHEDFILE, strAttachedFile);       // 첨부화일 명
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTUSERID, strInspectUserID);      // 담당자
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.CREATEUSERID, strCreateUserID);       //

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.SINSPECTBASISID, strsInspectBasisID);
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTBASISIDSEQ, intInspectBasisIDSeq);
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.SDEFECTYN, strsDefectYN);           // 판정결과
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.SPROCESSID, strsProcessID);          // 공정
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INSPECTPOINT, strInspectPoint);        // 1: 수입, 3:자주,5:출하

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.IMPORTSECYN, strImportSecYN);         // 보안 부품중요도  
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.IMPORTLAWYN, strImportlawYN);         // 법규
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.IMPORTIMPYN, strImportImpYN);         // 중요
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.IMPORTNORYN, strImportNorYN);         // 일반
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.IRELEVEL, strIRELevel);            // 부품품질 위험도 -- 특별관리 고, 중요관리 중, 일반관리 저 

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INPCUSTOMID, strInpCustomID);         // 입고거래처
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.INPDATE, strInpDate);             // 입고일
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.OUTCUSTOMID, strOutCustomID);         // 출고거래처
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.OUTDATE, strOutDate);             // 출고일
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.MACHINEID, strMachineID);           // 설비 호기 

                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.BUYERMODELID, strBuyerModelID);        //고객 모델 
                sqlParameter.Add(WizWork.TableData.Ins_InspectAuto.FMLGUBUN, strFMLGubun);            // 초.중.종품 구분

                DataSet ds = DataStore.Instance.ProcedureToDataSet("[xp_WizIns_uAutoInspect]", sqlParameter, false);

                //ProcedureInfo List
                //0번 : 프로시저 이름, 1번 output yn, 2번 output 이름, 3번 output size(output y일때만)
                ProcedureInfo = new List<string>();
                ProcedureInfo.Add("xp_WizIns_uAutoInspect");
                ProcedureInfo.Add("Y");
                ProcedureInfo.Add("InspectID");
                ProcedureInfo.Add("10");

                ListProcedureName.Add(ProcedureInfo);
                ListParameter.Add(sqlParameter);
                ProcedureInfo = null;

                

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    /////////////////    Ins_InspectAuto   DB SAve  ////////////////////
                    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                    sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTID, strInspectID);
                    sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.SEQ, "");
                    sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTBASISID, m_InspectBasisID);
                    
                    sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTBASISSUBSEQ, intInspectBasisSubSeq);

                    if (strsInsType == "1")
                    {
                        sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTVALUE, 0);
                        sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTTEXT, grdData[1, i].Value.ToString());
                    }
                    else
                    {
                        sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTVALUE, grdData[1, i].Value.ToString());
                        sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.INSPECTTEXT, "");
                    }
                    sqlParameter2.Add(WizWork.TableData.Ins_InspectAutoSub.CREATEUSERID, Frm_tprc_Main.g_tBase.PersonID);

                    //Dictionary<string, int> outputParam2 = new Dictionary<string, int>();
                    //outputParam2.Add(WizWork.TableData.Ins_InspectAutoSub.SEQ, 10);

                    //Dictionary<string, string> dicResult2 = DataStore.Instance.ExecuteProcedureOutputNoTran("[xp_WizIns_iAutoInspectSub]", sqlParameter2, outputParam2, true);


                    //intSeq = int.Parse(dicResult2[WizWork.TableData.Ins_InspectAutoSub.SEQ]);
                    //grdData[0, i].Value = intSeq;
                    ProcedureInfo = new List<string>();
                    ProcedureInfo.Add("xp_WizIns_iAutoInspectSub");
                    ProcedureInfo.Add("N");
                    ProcedureInfo.Add("InspectID");
                    ProcedureInfo.Add("10");

                    ListProcedureName.Add(ProcedureInfo);
                    ListParameter.Add(sqlParameter2);
                    ProcedureInfo = null;

                }
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
            return blResult;
        }


        /// <summary>
        /// '정량 입력값을 기준으로 자동 불량 판정
        /// </summary>
        /// <returns></returns>
        private string LF_JudgeDefect()
        {

            string strResult = "";

            string strInsType = "";

            double duMRAinVal = 0;
            double duRAMaxVal = 0;
            string strSpec = "";

            Boolean bDefect = false;

            //DataGridViewRow dr = grdInsItem.SelectedRows[0];
            if (grdInsItem.SelectedRows is null)
            {
                return "";
            }
            DataGridViewRow dr = grdInsItem.SelectedRows[0];
            DataGridView dgvTab = tabControl1.SelectedTab.Controls[0] as DataGridView;

            strInsType = dr.Cells["InsType"].Value.ToString().Trim();

            // 정량일때만, 최대값 과 최소값을 구해야 한다.  > 2020.02.12 허윤구 수정.
            if (strInsType == "2")
            {
                duMRAinVal = double.Parse(dr.Cells["InsRASpecMin"].Value.ToString().Trim());
                duRAMaxVal = double.Parse(dr.Cells["InsRASpecMax"].Value.ToString().Trim());
                strSpec = dr.Cells["InsSpec"].Value.ToString();
            }
            
            for (int i = 0; i < dgvTab.Rows.Count; i++)
            {
                string strValue = dgvTab["InspectValueText", i].Value.ToString().Trim();
                if (strInsType == "1")
                {
                    if (dgvTab["InspectValueText", i].Value.ToString() != "")
                    {
                        if ((strValue != "양호") && (strValue != "정상"))
                        {
                            bDefect = true;
                            break;
                        }
                    }
                }
                else
                {
                    if ((double.Parse(strValue) < duMRAinVal) || (double.Parse(strValue) > duRAMaxVal))
                    {
                        bDefect = true;
                        break; ;
                    }
                }
            }

            if (bDefect == true)
            {
                strResult = "불";
            }
            else
            {
                strResult = "합";
            }
            return strResult;
        }

        private void grdInsItem_MouseClick(object sender, MouseEventArgs e)
        {
            if (grdInsItem.Rows.Count > 0)
            {
                string strInspectBasisSubSeq = "";
                string strInsSampleQty = "";
                DataSet ds = null;
                try
                {
                    if (grdInsItem.SelectedRows.Count > 0)
                    {
                        return;
                    }

                    DataGridViewRow dr = grdInsItem.SelectedRows[0];

                    btnInputData.Enabled = true;
                    btnSave.Enabled = true;

                    strInspectBasisSubSeq = dr.Cells["SubSeq"].Value.ToString();
                    strInsSampleQty = dr.Cells["InsSampleQty"].Value.ToString();

                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("LotNo", txtLotNo.Text.ToUpper().Trim());
                    sqlParameter.Add("InspectPoint", m_InspectPoint);
                    sqlParameter.Add("InspectBasisSubSeq", strInspectBasisSubSeq);
                    sqlParameter.Add("InspectID", m_InspectID);
                    sqlParameter.Add("InspectBasisID", m_InspectBasisID);

                    ds = DataStore.Instance.ProcedureToDataSet("xp_Inspect_sLotNoCheck", sqlParameter, false);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "OK")
                        {
                            FillGridData(strInsSampleQty);
                        }
                        else
                        {
                            Message[0] = "[오류]";
                            Message[1] = ds.Tables[0].Rows[0][0].ToString();
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                            btnInputData.Enabled = false;
                            btnSave.Enabled = false;
                            InitTabPage();
                        }
                    }
                    else
                    {
                        Message[0] = "[오류]";
                        Message[1] = ds.Tables[0].Rows[0][0].ToString();
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);

                        btnInputData.Enabled = false;
                        btnSave.Enabled = false;
                        InitTabPage();
                        return;
                    }
                }
                catch (Exception excpt)
                {
                    Message[0] = "[오류]";
                    Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message);
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }
            }

        }

        private void CellAccess()
        {
            int nCount = tabControl1.TabPages.Count;
            DataGridView dgv = null;
            if (nCount > 0)
            {
                for (int i = 0; i < nCount; i++)
                {
                    dgv = (DataGridView)tabControl1.TabPages[i].Controls[0];
                    //dgv.DataSource = (DataGridView)tabControl1.TabPages[i].Controls[0];
                    foreach (DataGridViewRow dr in dgv.Rows)
                    {
                        Console.WriteLine("@@");
                        Console.WriteLine(dr.Cells["InspectValueText"].Value.ToString());
                        Console.WriteLine("@@");
                    }
                }

            }

        }
        private void SetTabPage(DataGridView _dgvInspect, string _strtitle)
        {
            TabPage TabPage = new TabPage(_strtitle);
            TabPage.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            TabPage.Controls.Add(dgv);
            _dgvInspect.Location = new Point(2, 2);
            tabControl1.TabPages.Add(TabPage);
        }

        private void FillGridData(string strGrdDatadRow)
        {
            int intRowCnt = 0;
            if (strGrdDatadRow == "")
            {
                //this.grdData.Rows.Clear();
                InitTabPage();

                return;
            }
            try
            {
                //17.11.14 수정. 로직 꼬일시 다시...돌아갈것
                intRowCnt = Int32.Parse(strGrdDatadRow);
                string title = (tabControl1.TabCount + 1).ToString();
                dgv = new DataGridView();
                dgv.Dock = DockStyle.Fill;
                dgv.Margin = new Padding(1, 1, 1, 1);
                InitGrid(dgv);//, title);

                for (int i = 0; i < intRowCnt; i++)
                {
                    dgv.Rows.Add(i + 1, "");// "");
                }

                SetTabPage(dgv, title);//TabPage셋팅( 그리드뷰, 탭페이지 Name)


                //기존 VB 소스
                //intRowCnt = int.Parse(strGrdDatadRow);
                //this.grdData.Rows.Clear();

                //for(int i =0 ;i <intRowCnt; i++)
                //{
                //    grdData.Rows.Add(i + 1, "");
                //    DataGridViewRow row = grdData.Rows[i];
                //    row.Height = 33;
                //}
                //dgv.Rows.RemoveAt(intRowCnt-1);
            }
            catch (Exception e)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", e.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                dgv.Rows.Clear();
                return;
            }



        }
        void InitGrid(DataGridView _dgv)//, string name)
        {
            _dgv.AllowUserToAddRows = false;//이 속성 지정 안해놓으면 로우 추가되버림
            _dgv.Columns.Clear();
            _dgv.ColumnCount = 2;           //row 추가되는 지점 위에 AllowUserToAddRows 속성으로 인해서 안됨

            int n = 0;
            // Set the Colums Hearder Names

            _dgv.Columns[n].Name = "No";
            _dgv.Columns[n].HeaderText = "No";
            _dgv.Columns[n].Width = 65;
            _dgv.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            _dgv.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            _dgv.Columns[n++].Visible = true;

            _dgv.Columns[n].Name = "InspectValueText";
            _dgv.Columns[n].HeaderText = "측정값";
            _dgv.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _dgv.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            _dgv.Columns[n].Visible = true;

            _dgv.MultiSelect = false;
            _dgv.ReadOnly = true;
            _dgv.Size = new System.Drawing.Size(252, 351);
            _dgv.ColumnHeadersHeight = 35;
            _dgv.RowHeadersVisible = false;
            _dgv.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            _dgv.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
            _dgv.RowsDefaultCellStyle.Font = new Font("굴림", 14F);
            _dgv.RowTemplate.Height = 33;
        }
        /// <summary>
        /// 검사입력값
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInputData_Click(object sender, EventArgs e)
        {
            string strInsType = "";
            double duMinVal = 0;
            double duMaxVal = 0;
            string strSpec = "";
            if (cboMachine.DataSource is null || cboMachine.SelectedValue.ToString() == "")
            {
                string Message = "선택된 설비가 없습니다. \r\n 설비를 선택해주세요.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message, "[오류]", 0, 1);
                return;
            }
            else if (cboFML.DataSource is null || cboFML.SelectedValue.ToString() == "")
            {
                string Message = "선택된 초중종이 없습니다. \r\n 초품,중품,종품 중 하나를 선택해주세요.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message, "[오류]", 0, 1);
                return;
            }

            int nCount = tabControl1.TabPages.Count;

            if (nCount > 0)
            {
                for (int k = grdInsItem.SelectedRows[0].Index; k < grdInsItem.Rows.Count; k++)//TabPage 하나의 이벤트
                {
                    tabControl1.TabPages[k].Select();                               //TabPage선택
                    tabControl1.SelectedIndex = k;
                    grdInsItem.Rows[k].Selected = true;
                    double douValue = 0;
                    DataGridView dgvValue = tabControl1.TabPages[k].Controls[0] as DataGridView;

                    if (dgvValue.Rows.Count < 1)
                    {
                        continue;
                    }
                    EnabledFalse();

                    DataGridViewRow dgvr = grdInsItem.SelectedRows[0];

                    strInsType = dgvr.Cells["InsType"].Value.ToString().Trim();
                    double.TryParse(dgvr.Cells["InsRASpecMin"].Value.ToString(), out duMinVal);
                    double.TryParse(dgvr.Cells["InsRASpecMax"].Value.ToString(), out duMaxVal);

                    strSpec = dgvr.Cells["InsSpec"].Value.ToString();

                    for (int i = 0; i < dgvValue.Rows.Count; i++)
                    {
                        if (strInsType == "1")  // 정성적 검사값 입력
                        {
                            if (Lib.CheckNull(dgvValue["InspectValueText", i].Value.ToString()) == "")
                            {
                                dgvValue.Rows[i].Selected = true;
                                m_Popform_1 = new frm_tins_InspectText();
                                m_Popform_1.Owner = this;
                                m_InspectText = "";
                                m_Popform_1.ShowDialog();

                                if (m_InspectText == "")
                                {
                                    EnabledTrue();
                                    this.btnInputData.Enabled = true;
                                    this.btnNew.Enabled = false;
                                    return;
                                }
                                else
                                {
                                    dgvValue["InspectValueText", i].Value = m_InspectText;
                                    dgvValue.Columns["InspectValueText"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                                }

                                //if ((k + 1) < grdInsItem.Rows.Count)
                                //{
                                //    grdInsItem.Rows[k + 1].Selected = true;
                                //}
                                m_Popform_1 = null;

                                if ((i + 1) >= dgvValue.Rows.Count)
                                {
                                    string str = dgvr.Cells["insItemName"].Value.ToString();
                                    WizCommon.Popup.MyMessageBox.ShowBox(str + " 측정값을 다 입력하셨습니다", str, 1, 1);
                                    dgvr.Cells["DefectYN"].Value = LF_JudgeDefect();
                                    if (dgvr.Cells["DefectYN"].Value.ToString() == "불")
                                    {
                                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                                        style.ForeColor = Color.Red;
                                        dgvr.Cells["DefectYN"].Style = style;
                                    }
                                    this.btnNew.Enabled = false;
                                    this.btnSave.Enabled = true;
                                    this.btnInit.Enabled = true;
                                    this.btndgvInspectClear.Enabled = true;
                                }
                            }

                        }

                        else   // 정량적 입력값
                        {
                            if (Lib.CheckNull(dgvValue["InspectValueText", i].Value.ToString()) == "")
                            {
                                dgvValue.Rows[i].Selected = true;
                                m_Popform_2 = new POPUP.Frm_CMNumericKeypad("검사값 입력", "측정값");
                                m_Popform_2.Owner = this;
                                m_Popform_2.StartPosition = FormStartPosition.Manual;
                                m_Popform_2.Left = 600;
                                m_Popform_2.Top = 180;
                                m_Popform_2.ShowDialog();

                                // 키패드 팝업창을 x키로 종료하는 경우에 "양호"로 진행되는 경우가 있어서 추가 2020.10.22
                                if (m_Popform_2.DialogResult != DialogResult.OK)
                                {
                                    m_InspectText = "";
                                }

                                if (m_InspectText == "" )
                                {
                                    EnabledTrue();
                                    this.btnInputData.Enabled = true;
                                    this.btnNew.Enabled = false;
                                    return;
                                }
                                else
                                {
                                    try
                                    {
                                        douValue = 0;
                                        double.TryParse(m_InspectText, out douValue);
                                        //기준값 미만 또는 초과라서 불량일 경우 메세지 띄워줌
                                        if (duMinVal > douValue)
                                        {
                                            WizCommon.Popup.MyMessageBox.ShowBox("불량 " + "\r\n" + strSpec + "  " + "미만", "[불량]", 1, 1);
                                        }
                                        else if (duMaxVal < douValue)
                                        {
                                            WizCommon.Popup.MyMessageBox.ShowBox("불량 " + "\r\n" + strSpec + "  " + "초과", "[불량]", 1, 1);
                                        }
                                        dgvValue["InspectValueText", i].Value = m_InspectText;
                                        dgvValue.Columns["InspectValueText"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight;
                                    }
                                    catch (Exception e1)
                                    {
                                        Console.Write(e1.Message);
                                        // MessageBox.Show("측정값 입력 오류입니다.");
                                        i = i - 1;
                                        return;
                                    }
                                }
                                m_Popform_2 = null;

                                if ((k + 1) < grdInsItem.Rows.Count)
                                {
                                    //grdInsItem.Rows[k + 1].Selected = true;
                                    grdInsItem.Rows[k].Selected = true;
                                }

                                if ((i + 1) >= dgvValue.Rows.Count)
                                {
                                    string str = dgvr.Cells["insItemName"].Value.ToString();
                                    WizCommon.Popup.MyMessageBox.ShowBox(str + " 측정값을 다 입력하셨습니다", str, 1, 1);
                                    dgvr.Cells["DefectYN"].Value = LF_JudgeDefect();
                                    if (dgvr.Cells["DefectYN"].Value.ToString() == "불")
                                    {
                                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                                        style.ForeColor = Color.Red;
                                        dgvr.Cells["DefectYN"].Style = style;
                                    }
                                    btnNew.Enabled = false;
                                    btnSave.Enabled = true;
                                    btnInit.Enabled = true;
                                    btndgvInspectClear.Enabled = true;
                                }

                            }
                        }
                    }
                }
            }

            // 프로세스가
            // 1. 입력 버튼 눌렀을 때, 수정, 저장 초기화 버튼을 Enable = false 처리
            // 2. 입력 다 되었는지 체크 하고 버튼을 활성화 시키는것 같은데...

            // → (위에건 일단 무시하고) 마지막 구문에 입력이 다 되었을 시에는, 저장 버튼 활성화 되도록 추가 2020.11.16 GDU → 후에 입력 버튼 클릭 이벤트 전체 리팩토링 필요.
            if (checkAllInsertedValue()) // 모든 값이 입력 되었을 시,
            {
                btnSave.Enabled = true;
                btnInit.Enabled = true;
                btndgvInspectClear.Enabled = true;
            }
            else // 입력되지 않은 값이 있을 경우.
            {
                //btnSave.Enabled = true;
                btnInit.Enabled = true;
                btndgvInspectClear.Enabled = true;
            }
        }

        public void SetCheckValue(string strChkValue)
        {
            m_InspectText = strChkValue;
            return;
        }

        public void SetCheckValueCancel(string strChkValue)
        {
            m_InspectText = strChkValue;
            EnabledTrue();
            this.btnInputData.Enabled = true;
            this.btnNew.Enabled = false;
            return;
        }

        private void dgvInspectClear_Click(object sender, EventArgs e)
        {
            DataGridView dgv = null;
            int i = 0;
            dgv = (DataGridView)tabControl1.SelectedTab.Controls[0];
            i = tabControl1.SelectedIndex + 1;
            if (dgv.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in grdInsItem.Rows)
                {
                    if (dgvr.Cells["No"].Value.ToString() == i.ToString())
                    {
                        dgvr.Cells["DefectYN"].Value = "";
                    }
                }
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.Cells["InspectValueText"].Value = "";
                }
                btnInputData.Enabled = true;
            }

            if (checkAllInsertedValue() == false)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }
        }

        private void InitTabPage()
        {
            tabControl1.TabPages.Clear();

            tabControl1.DrawItem += tabControl1_DrawItem;

            tabControl1.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.SizeMode = TabSizeMode.Fixed;
            //tabControl1.Alignment = System.Windows.Forms.TabAlignment.MiddleCenter;
            dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.Margin = new Padding(1, 1, 1, 1);
            InitGrid(dgv);
            SetTabPage(dgv, "1");
        }

        //private void tabControl1_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    Brush _textBrush;

        //    // Get the item from the collection.
        //    TabPage _tabPage = tabControl1.TabPages[e.Index];

        //    // Get the real bounds for the tab rectangle.
        //    Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

        //    if (e.State == DrawItemState.Selected)
        //    {

        //        // Draw a different background color, and don't paint a focus rectangle.
        //        _textBrush = new SolidBrush(Color.Red);
        //        g.FillRectangle(Brushes.Gray, e.Bounds);
        //    }
        //    else
        //    {
        //        _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
        //        e.DrawBackground();
        //    }

        //    // Use our own font.
        //    Font _tabFont = new Font("Arial", (float)10.0, FontStyle.Bold, GraphicsUnit.Pixel);

        //    // Draw string. Center the text.
        //    StringFormat _stringFlags = new StringFormat();
        //    _stringFlags.Alignment = StringAlignment.Center;
        //    _stringFlags.LineAlignment = StringAlignment.Center;
        //    g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        //}

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics gr = e.Graphics;
            Font font = new Font("맑은 고딕", 14, FontStyle.Bold);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            //this.tabControl1.TabPages[0].Text = "Qwerty";

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                string str = "";
                str = tabControl1.TabPages[i].Text;
                gr.DrawString(str, font, Brushes.Black, tabControl1.GetTabRect(i), sf);
            }
        }

        private void cmdLotNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void grdInsItem_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (grdInsItem.SelectedRows.Count > 0)
            {
                int i = 0;
                int.TryParse(grdInsItem.SelectedRows[0].Cells["No"].Value.ToString(), out i);
                foreach (TabPage tp in tabControl1.TabPages)
                {
                    if (tp.Text == i.ToString())
                    {
                        tp.Select();
                        tabControl1.SelectedIndex = (i - 1);
                        bool isOK = false;
                        foreach (DataGridView dgv in tp.Controls)
                        {
                            foreach (DataGridViewRow dgvr in dgv.Rows)
                            {
                                if (dgvr.Cells["InspectValueText"].Value.ToString() == "")
                                {
                                    btnInputData.Enabled = true;
                                    isOK = true;
                                    break;
                                }
                            }
                            if (!isOK)
                            {
                                btnInputData.Enabled = false;
                            }
                            if (isOK)
                            {
                                break;
                            }
                        }
                    }

                }
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

            if (tabControl1.TabCount > 0)
            {
                int i = tabControl1.SelectedIndex;
                grdInsItem.Rows[i].Selected = true;
            }

        }

        private void mtb_Date_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }
        private void LoadCalendar()
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_Date.Text.Replace("-", ""), mtb_Date.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
            //Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
            void GetDate(string strDate, string btnName)
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
                mtb_Date.Text = dateTime.ToString("yyyy-MM-dd");
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdRowUp_Click(object sender, EventArgs e)
        {
            Lib.btnRowUp(grdInsItem);
            if (grdInsItem.SelectedRows.Count > 0)
            {
                int i = 0;
                i = grdInsItem.SelectedRows[0].Index;
                tabControl1.TabPages[i].Select();                               //TabPage선택
                tabControl1.SelectedIndex = i;
            }
        }

        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            Lib.btnRowDown(grdInsItem);
            if (grdInsItem.SelectedRows.Count > 0)
            {
                int i = 0;
                i = grdInsItem.SelectedRows[0].Index;
                tabControl1.TabPages[i].Select();                               //TabPage선택
                tabControl1.SelectedIndex = i;
            }
        }

        private void txtLotNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                if (ScanLotNo())
                {
                    SetcboEcoNO_Chg();
                    SetJaju_Picture();
                }
            }
        }

        private void frm_tins_InspectAuto_U_Activated(object sender, EventArgs e)
        {
            ((Frm_tprc_Main)(MdiParent)).LoadRegistry();
            txtLotNo.Select();
            txtLotNo.Focus();
        }

        private void grdData_Click(object sender, EventArgs e)
        {
            btnInputData.Enabled = true;
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboEcoNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEcoNO.DataSource != null)
            {
                if (cboEcoNO.Items.Count > 0)
                {
                    SetcboEcoNO_Chg();
                }
            }
        }

        // 작업자 선택버튼 클릭 시 .. _ 신규생성 허윤구(2019.05.15)
        private void cmdPersonChoice_Click(object sender, EventArgs e)
        {
            string Send_ProcessID = string.Empty;
            string Send_MachineID = string.Empty;

            Send_ProcessID = cboProcess.SelectedValue.ToString();
            if (cboMachine.DataSource != null || cboMachine.SelectedValue.ToString() != "")
            {
                Send_MachineID = cboMachine.SelectedValue.ToString();
            }

            frm_tprc_setProcess FTSP = new frm_tprc_setProcess(Send_ProcessID, Send_MachineID, true);
            FTSP.Owner = this.ParentForm;
            if (FTSP.ShowDialog() == DialogResult.OK)
            {
            };
        }

        // 자주검사 참조용 사진 도출. _ 신규생성 허윤구 (2020.03.26)
        private void SetJaju_Picture()
        {
            picImg.Tag = null;
            picImg.Image = null;

            string ArticleID = m_ArticleID;

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("ArticleID", ArticleID);


            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sArticleJajuPicture", sqlParameter, false);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow DR = dt.Rows[0];
                if ((DR["Article"].ToString().Trim() == txtArticle.Text.Trim()) && (DR["BuyerArticleNo"].ToString().Trim() == txtBuyerArticleNo.Text.Trim()))
                {
                    // 제대로 Article 정보를 가지고 왔다면,
                    Path = DR["InsImagePath"].ToString();
                    File = DR["InsImageFile"].ToString();

                    if ((Lib.CheckNull(Path) == string.Empty) || (Lib.CheckNull(File) == string.Empty))
                    {
                        //Message[0] = "[그림참조 실패]";
                        //Message[1] = "그림을 첨부등록 하지 않았어!";
                        //WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                        picImg.Image = global::WizWork.Properties.Resources.NoImage;
                        picImg.SizeMode = PictureBoxSizeMode.Zoom;
                        return;
                    }
                    else
                    {
                        FtpDownload(Path, File);
                    }
                }
                else
                {
                    Message[0] = "[그림참조 실패]";
                    Message[1] = "품명 정보가 일치하지 않습니다. 사무실 데이터를 확인해 주세요.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    return;
                }                
            }
        }

        private void FtpDownload(string Path, string File)
        {
            if (Path != "" || File != null)
            {
                FTP_EX _ftp = null;
                INI_GS gs = new INI_GS();

                string FTP_ADDRESS = "ftp://" + gs.GetValue("FTPINFO", "FileSvr", "wizis.iptime.org") + ":" + gs.GetValue("FTPINFO", "FTPPort", "21");
                string FTP_ID = "wizuser";
                string FTP_PASS = "wiz9999";

                _ftp = new FTP_EX(FTP_ADDRESS, FTP_ID, FTP_PASS);
                string LocalDirPath = Application.StartupPath + "\\" + "#Temp" + "\\" + Path + "\\"; //FTP서버내의 폴더명과 같은 폴더명을 LOCAL에서 사용하자;

                string FtpFolderPath = Path;//gs.GetValue("FTPINFO", "FTPIMAGEPATH", "/ImageData") + "/" + File; // ex)/ImageData/00065
                string[] fileListSimple;

                string Local_File = string.Empty;           //local 경로
                //picImg                                    //사진
                //lblImgName                                //text가 파일명 , tag 폴더명
                try
                {
                    fileListSimple = _ftp.directoryListSimple(FtpFolderPath, Encoding.Default);

                    if (fileListSimple.Length == 1
                       && fileListSimple[0].ToLower().Equals("error"))
                    {
                        picImg.Image = global::WizWork.Properties.Resources.NoImageByConnect;
                        picImg.SizeMode = PictureBoxSizeMode.Zoom;
                        return;
                    }

                    //로컬경로 생성
                    DirectoryInfo dir = new DirectoryInfo(LocalDirPath);//로컬
                    if (dir.Exists == false)//로컬 폴더 존재 유무 확인 후 없을 시 생성
                    { dir.Create(); }
                    //로컬경로 생성

                    bool ftpExistFile = false;
                    picImg.Tag = Path + "/" + File;//파일 경로 + 파일명
                    Local_File = LocalDirPath + "\\" + File;//로컬경로

                    //파일 존재 유무 확인 있을때 ftpExistFile변수 True 없을때 False
                    foreach (string filename in fileListSimple)
                    {
                        if (string.Compare(filename, File) == 0)
                        { ftpExistFile = true; break; }
                    }

                    if (ftpExistFile == false)
                    {
                        //Message[0] = "FTP서버에 해당 파일인 " + File + " 파일이 존재하지 않습니다.";
                        //Message[1] = "[파일 존재하지 않음]";
                        throw new Exception();
                    }

                    else if (_ftp.GetFileSize(picImg.Tag.ToString()) == 0)//파일사이즈가 0일때
                    {
                        //Message[0] = "FTP서버에 해당 파일인 " + File + "의 파일사이즈가 0입니다. 사무실프로그램에서 파일을 다시 업로드 해주시기 바랍니다.";
                        //Message[1] = "[파일 크기 오류]";
                        throw new Exception();
                    }

                    else//파일 사이즈가 0이 아닐때 기존폴더안의 파일들 삭제 후 다운로드
                    {
                        //FTP 다운로드 부분
                        FileInfo file = new FileInfo(Local_File);
                        if (file.Exists == true)//로컬 품명코드 폴더안의 파일 삭제
                        { file.Delete(); }
                        if (_ftp.download(picImg.Tag.ToString(), Local_File.ToString()))
                        {
                            FileStream fs = new FileStream(Local_File.ToString(), FileMode.Open, FileAccess.Read);
                            picImg.Image = System.Drawing.Image.FromStream(fs);
                            fs.Close();
                            //picImg.SizeMode = PictureBoxSizeMode.StretchImage;
                            picImg.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            Message[0] = "FTP파일 다운로드 실패. 통신상태를 확인해주세요.";
                            Message[1] = "[FTP파일 다운 오류]";
                            throw new Exception();
                        }
                    }

                    //ProcessStartInfo proc = new ProcessStartInfo(LocalDirPath + @"/" + File);
                    //proc.UseShellExecute = true;
                    //Process.Start(proc);
                }
                catch (Exception excpt)
                {
                    Console.Write(excpt.Message);
                    //WizCommon.Popup.MyMessageBox.ShowBox(Message[0], Message[1], 3, 1);
                    picImg.Image = global::WizWork.Properties.Resources.NoImage;
                    picImg.SizeMode = PictureBoxSizeMode.Zoom;

                }
            }
        }

        private void picImg_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (picImg.Image != null)
                {
                    this.Visible = false;

                    frm_PopUp_ImgInspectAuto popup = new frm_PopUp_ImgInspectAuto(Path, File);
                    popup.Owner = this;

                    popup.Picture = picImg.Image; //OrderPopUp의 이미지를 PopUp의 PictureBox에 할당. 
                    popup.ShowDialog();

                    picImg.Image = popup.Picture;
                }
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("관리자에게 문의해주세요.\r\n(오류 내역 : " + ex.Message + ")", "[이미지 더블 클릭 오류]", 3, 1);
            }
           
        }
        public void ComeBack_BigPicture()
        {
            this.Visible = true;
        }


        #region 이미지 회전 - 버튼 클릭 이벤트
        // 2020.09.07 - TKB 추가
        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            if (picImg.Image != null)
            {
                Bitmap bit = new Bitmap(picImg.Image);

                bit.RotateFlip(RotateFlipType.Rotate90FlipXY);

                picImg.Image = bit;

                UploadImageToFtp();
            }
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            if (picImg.Image != null)
            {
                Bitmap bit = new Bitmap(picImg.Image);

                bit.RotateFlip(RotateFlipType.Rotate270FlipXY);

                picImg.Image = bit;

                UploadImageToFtp();
            }
        }

        private void UploadImageToFtp()
        {
            try
            {
                INI_GS gs = new INI_GS();

                string FTP_ADDRESS = "ftp://" + gs.GetValue("FTPINFO", "FileSvr", "wizis.iptime.org") + ":" + gs.GetValue("FTPINFO", "FTPPort", "21");
                string FTP_ID = "wizuser";
                string FTP_PASS = "wiz9999";

                byte[] img = ImageToByte(picImg.Image);

                UploadUsingByte(FTP_ADDRESS + Path, FTP_ID, FTP_PASS, img, File);
            }
            catch(Exception ex)
            {
                MessageBox.Show("이미지 업로드 실패<UploadImageToFtp>\r\n : " + ex.Message, "[이미지 업로드 실패]");
            }

        }

        private byte[] ImageToByte(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        // 20191210 둘리 - 파일 업로드 테스트
        public bool UploadUsingByte(string host, string user, string pass, byte[] buffer, string FileName)
        {
            bool flag = false;

            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + FileName);
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

                Stream reqStream = ftpRequest.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return flag;
        }

        #endregion

        #region 작업자 변경 Version1.1 - 2020.10.16

        private void txtPerson_Click(object sender, EventArgs e)
        {
            Frm_PopUp_setPerson_Work setPerson = new Frm_PopUp_setPerson_Work();
            setPerson.Owner = this.mainForm;
            setPerson.ShowDialog();

            if (setPerson.DialogResult == DialogResult.OK)
            {
                txtPerson.Text = Frm_tprc_Main.g_tBase.Person;
            }
        }

        private void btnPersonChange_Click(object sender, EventArgs e)
        {
            Frm_PopUp_setPerson_Work setPerson = new Frm_PopUp_setPerson_Work();
            setPerson.Owner = this.mainForm;
            setPerson.ShowDialog();

            if (setPerson.DialogResult == DialogResult.OK)
            {
                txtPerson.Text = Frm_tprc_Main.g_tBase.Person;
            }
        }

        #endregion

        #region 모든 값이 다 입력 되었는지 체크하기 / 검사 결과 입력값 초기화 - 2020.11.16

        // 2020.11.16 GDU, 모든 값이 다 입력 되었는지 체크하기.
        private bool checkAllInsertedValue()
        {
            bool flag = true;

            for (int k = 0; k < tabControl1.TabCount; k++)
            {
                DataGridView dgv = tabControl1.TabPages[k].Controls[0] as DataGridView;

                if (dgv == null)
                {
                    return false;
                }

                if (dgv != null)
                {
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        DataGridViewRow row = dgv.Rows[i];
                        if (string.IsNullOrEmpty(row.Cells["InspectValueText"].Value.ToString()))
                        {
                            flag = false;
                        }
                    }
                }
            }

            return flag;
        }

        // 2020.11.16 GDU, 검사 결과 입력값만 초기화 시키기.
        private void clearAllInsertedValue()
        {
            for (int k = 0; k < tabControl1.TabCount; k++)
            {
                grdInsItem.Rows[k].Cells["DefectYN"].Value = string.Empty;

                // 측정값 초기화.
                DataGridView dgv = tabControl1.TabPages[k].Controls[0] as DataGridView;

                if (dgv == null)
                {
                    continue;
                }

                if (dgv != null)
                {
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        dgv.Rows[i].Cells["InspectValueText"].Value = string.Empty;
                    }
                }
            }

            // 초기화 후, 첫번째 행 선택하기
            if (grdInsItem.Rows.Count > 0)
            {
                grdInsItem.Rows[0].Selected = true;
            }
        }

        #endregion
    }
}
