using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Properties;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using WizCommon;
namespace WizWork
{
    public partial class Frm_tprc_DailMoldCheck : Form
    {
        POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad();
        string g_ProcessID = string.Empty;
        string g_MachineList = string.Empty;
        INI_GS gs = new INI_GS();
        string ItemCode = string.Empty;
        string CheckList = string.Empty;
        string No = string.Empty;
        string InsContents = string.Empty;
        string McInsCheck = string.Empty;
        string Path = string.Empty;
        string File = string.Empty;
        bool blLoad = false;
        Frm_PopUp_ImgIns2 II2 = null;
        Frm_PopUp_ImgIns II = null;

        public Form mainForm = null;

        /// <summary>
        /// 생성
        /// </summary>
        public Frm_tprc_DailMoldCheck()
        {
            InitializeComponent();
        }
        private void cmdWorkDefect_Click(object sender, EventArgs e)
        {

            if (GridData1.Rows.Count > 0)//검색해서 행이 채워졌을때
            {
                for (int i = 0; i < GridData1.Rows.Count; i++)
                {
                    if (GridData1.Rows[i].Cells["InspectionLegendName"].Value.ToString() == string.Empty)
                    {
                        if (GridData1.Rows[i].Cells["McImagePath"].Value != null && GridData1.Rows[i].Cells["McImageFile"].Value != null)
                        {
                            No = GridData1.Rows[i].Cells["No"].Value.ToString().Trim();
                            CheckList = GridData1.Rows[i].Cells["CheckList"].Value.ToString().Trim();
                            InsContents = GridData1.Rows[i].Cells["InsContents"].Value.ToString().Trim();
                            Path = GridData1.Rows[i].Cells["McImagePath"].Value.ToString().Trim();
                            File = GridData1.Rows[i].Cells["McImageFile"].Value.ToString().Trim();
                        }
                        else
                        {
                            No = GridData1.Rows[i].Cells["No"].Value.ToString().Trim();
                            CheckList = GridData1.Rows[i].Cells["CheckList"].Value.ToString().Trim();
                            InsContents = GridData1.Rows[i].Cells["InsContents"].Value.ToString().Trim();
                            Path = string.Empty;
                            File = string.Empty;
                        }
                        //행의 전체 크기, 행의 입력시작 행위치를 보내서 행위치부터 마지막까지의 입력값만 받아온다.
                        GridData1.Rows[i].Selected = true;
                        int cnt = 0;
                        foreach (DataGridViewRow dgvr in GridData1.Rows)
                        {
                            if (dgvr.Visible)
                            { ++cnt; }
                        }
                        II = new Frm_PopUp_ImgIns(cnt, i, No, CheckList, InsContents, McInsCheck, Path, File);
                        II.blMod = false;
                        II.WriteTextEvent += new Frm_PopUp_ImgIns.TextEventHandler(GetData);
                        II.Show();
                        break;
                    }
                }
            }
        }
        void GetData(int a, string InspectionLegendName, string InspectionLegendID, Frm_PopUp_ImgIns Pop_II)
        {
            GridData1.Rows[a].Cells["InspectionLegendName"].Value = InspectionLegendName;
            GridData1.Rows[a].Cells["InspectionLegendID"].Value = InspectionLegendID;

            for (int i = a; GridData1.Rows.Count - 1 >= a + 1; a++)
            {
                if (GridData1.Rows.Count - 1 >= a + 1)
                {
                    if (GridData1.Rows[a + 1].Cells["InspectionLegendName"].Value.ToString() == "")
                    {
                        Pop_II.sNo = GridData1.Rows[a + 1].Cells["No"].Value.ToString().Trim();
                        Pop_II.sCheckList = GridData1.Rows[a + 1].Cells["CheckList"].Value.ToString().Trim();
                        Pop_II.sInsContents = GridData1.Rows[a + 1].Cells["InsContents"].Value.ToString().Trim();
                        Pop_II.sMcInsCheck = GridData1.Rows[a + 1].Cells["McInsCheck"].Value.ToString().Trim();
                        Pop_II.sPath = GridData1.Rows[a + 1].Cells["McImagePath"].Value.ToString().Trim();
                        Pop_II.sFile = GridData1.Rows[a + 1].Cells["McImageFile"].Value.ToString().Trim();
                        Pop_II.sCurrentRow = a;
                        GridData1.Rows[a + 1].Selected = true;
                        break;
                    }
                    else
                    {
                        Pop_II.sNo = "";
                        Pop_II.sCheckList = "";
                        Pop_II.sInsContents = "";
                        Pop_II.sMcInsCheck = "";
                        Pop_II.sPath = "";
                        Pop_II.sFile = "";
                    }
                }
            }

        }

        void GetData2(int a, string InspectionLegendFigure, Frm_PopUp_ImgIns2 Pop_II)
        {
            GridData2.Rows[a].Cells["InspectionFigure"].Value = InspectionLegendFigure;

            for (int i = a; GridData2.Rows.Count - 1 >= a + 1; a++)
            {
                if (GridData2.Rows.Count - 1 >= a + 1)
                {
                    if (GridData2.Rows[a + 1].Cells["InspectionFigure"].Value.ToString() == "")
                    {
                        Pop_II.sNo = GridData2.Rows[a + 1].Cells["No"].Value.ToString().Trim();
                        Pop_II.sCheckList = GridData2.Rows[a + 1].Cells["CheckList"].Value.ToString().Trim();
                        Pop_II.sInsContents = GridData2.Rows[a + 1].Cells["InsContents"].Value.ToString().Trim();
                        Pop_II.sMcInsCheck = GridData2.Rows[a + 1].Cells["McInsCheck"].Value.ToString().Trim();
                        Pop_II.sPath = GridData2.Rows[a + 1].Cells["McImagePath"].Value.ToString().Trim();
                        Pop_II.sFile = GridData2.Rows[a + 1].Cells["McImageFile"].Value.ToString().Trim();
                        Pop_II.sCurrentRow = a;
                        GridData2.Rows[a + 1].Selected = true;
                        break;
                    }
                    else
                    {
                        Pop_II.sNo = "";
                        Pop_II.sCheckList = "";
                        Pop_II.sInsContents = "";
                        Pop_II.sMcInsCheck = "";
                        Pop_II.sPath = "";
                        Pop_II.sFile = "";
                    }
                }
            }
        }

        //콤보박스 데이터 바인딩
        private void SetComboBox()
        {
            SetCycleGbn();                            //검사구분
        }

        #region TableLayoutPanel 하위 컨트롤들의 DockStyle.Fill 세팅
        private void SetScreen()
        {
            pnlForm.Dock = DockStyle.Fill;
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Margin = new Padding(0, 0, 0, 0);
            foreach (Control control in tlpForm.Controls)//con = tlp 상위에서 2번째
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control contro in control.Controls)//tlp 상위에서 3번째
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
                                        foreach (Control ctrl in c.Controls)
                                        {
                                            ctrl.Dock = DockStyle.Fill;
                                            ctrl.Margin = new Padding(0, 0, 0, 0);
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

        private void SetCycleGbn()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add(Code_sCmCode.CODEGBN, "MLDCYCLEGBN");
                sqlParameter.Add(Code_sCmCode.SRELATION, "");
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Code_sCmCode", sqlParameter, false);
                cboMcInsCycleGbn.DataSource = dt;
                cboMcInsCycleGbn.ValueMember = Code_sCmCode.CODE_ID;
                cboMcInsCycleGbn.DisplayMember = Code_sCmCode.CODE_NAME;
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }
        private void SetBasisDate()
        {
            try
            {
                if (lblMold.Text != string.Empty && lblMold.Tag != null)
                {
                    if (lblMold.Tag.ToString() != string.Empty)
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("@MoldID", lblMold.Tag.ToString());
                        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sMoldRegularInspectBasisSubByMoldID", sqlParameter, false);
                        cboMcInsBasisDate.ValueMember = "MoldInspectBasisID";
                        cboMcInsBasisDate.DisplayMember = "MoldInspectBasisDate";
                        cboMcInsBasisDate.DataSource = dt;
                    }
                }
                else
                {
                    cboMcInsBasisDate.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        /// <summary>
        /// 조회버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void ProcQuery()
        {
            GridData1.Rows.Clear();
            GridData2.Rows.Clear();
            try
            {
                if (cboMcInsBasisDate.DisplayMember != null && cboMcInsBasisDate.SelectedValue != null && cboMcInsCycleGbn.SelectedValue != null)
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("@MoldInspectBasisID", cboMcInsBasisDate.SelectedValue.ToString());
                    sqlParameter.Add("@MoldInspectCycleGbn", cboMcInsCycleGbn.SelectedValue.ToString());

                    DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sMoldRegularInspectBasisSub", sqlParameter, false);

                    if (dt is null)
                    {
                        return;
                    }

                    if (dt.Rows.Count > 0)
                    {
                        int j = 0; int k = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            if (dr[Work_sInspectBasisSub.MCINSRECORDGBN].ToString() == "01")
                            {
                                k = k + 1;
                                GridData1.Rows.Add(
                                    k,                                           //NO
                                    dr[Work_sInspectBasisSub.MCINSITEMNAME],     //점검항목
                                    dr[Work_sInspectBasisSub.MCINSCONTENT],      //점검내용
                                    dr[Work_sInspectBasisSub.MCINSCHECK],        //확인
                                    dr[Work_sInspectBasisSub.MCINSCYCLE],         //주기
                                    "",                                           //검사
                                    dr["MoldInspectBasisID"],                //mcseq
                                    dr["MoldSeq"],               //McSeq
                                    dr["McInsCycleGbn"],                                                                             //mcinsrecordgbn
                                    dr["McInsRecordGbn"],
                                    dr["McImagePath"],              //FTP이미지경로
                                    dr["McImageFile"]               //FTP이미지파일명
                                    );
                            }
                            else if (dr[Work_sInspectBasisSub.MCINSRECORDGBN].ToString() == "02")
                            {
                                j = j + 1;
                                GridData2.Rows.Add(
                                j,            //NO
                                dr[Work_sInspectBasisSub.MCINSITEMNAME],     //점검항목
                                dr[Work_sInspectBasisSub.MCINSCONTENT],      //점검내용
                                dr[Work_sInspectBasisSub.MCINSCHECK],        //확인
                                dr[Work_sInspectBasisSub.MCINSCYCLE],        //주기
                                "",
                                dr["MoldInspectBasisID"],                //mcseq
                                dr["MoldSeq"],               //McSeq
                                dr["McInsCycleGbn"],                                                                             //mcinsrecordgbn
                                dr["McInsRecordGbn"],
                                dr["McImagePath"],              //FTP이미지경로
                                dr["McImageFile"]               //FTP이미지파일명
                                );
                            }
                            foreach (DataGridViewRow dgvr in GridData1.Rows)
                            {
                                dgvr.Visible = false;
                            }
                            foreach (DataGridViewRow dgvr in GridData2.Rows)
                            {
                                dgvr.Visible = false;
                            }
                        }
                        GridData1.ClearSelection();
                        GridData2.ClearSelection();
                        FillGridByMcInsCycleGbn();
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        private void ProcSave()
        {
            //xp_McRegularInspect_iMcRegularInspect    
            //첫번째 프로시저 데이터값 셋팅
            //List<string> ProcedureInfo = new List<string>();
            //List<List<string>> ListProcedureName = new List<List<string>>();
            //List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();
            try
            {
                List<WizCommon.Procedure> Prolist = new List<WizCommon.Procedure>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                string strMcrInstpectID = "";
                string strMcrInstpectUserID = "";
                string strMcrInspectDate = "";
                string strMcInstpectBasisID = "";
                string strMcInstpectBasisDate = "";
                string strMcInsCycleGbn = "";

                string strDefectContents = "";
                string strDefectReason = "";
                string strDefectRespectContents = "";
                string strComments = "";
                string strCreateUserID = "";

                string strMcInspectBasisSeq = "";
                string strMcrInspectLegend = "";
                string strMcrInspectValue = "";

                strMcrInstpectID = "";
                strMcrInstpectUserID = Frm_tprc_Main.g_tBase.PersonID;

                strMcrInspectDate = mtb_Date.Text.Replace("-", "").Replace("/", "");
                strMcInstpectBasisID = cboMcInsBasisDate.SelectedValue.ToString();
                strMcInstpectBasisDate = cboMcInsBasisDate.Text.ToString();
                strMcInsCycleGbn = cboMcInsCycleGbn.SelectedValue.ToString();

                strDefectContents = "";
                strDefectReason = "";
                strDefectRespectContents = "";
                strComments = "";
                strCreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@MoldInspectID", "");              //JobID 입력안함. 지금 프로시저 수행 후 만들어짐~~

                sqlParameter.Add("@MoldInspectUserID", strMcrInstpectUserID);                                                      //검사자ID //메인에 저장되있는 전역변수 UserID //만들어야됨
                sqlParameter.Add("@MoldInspectDate", strMcrInspectDate);               //검사일자
                sqlParameter.Add("@MoldInspectBasisID", strMcInstpectBasisID);              //검사기준ID
                sqlParameter.Add("@MoldInspectBasisDate", strMcInstpectBasisDate);                         //개정일자
                sqlParameter.Add("@MoldICycleGbn", strMcInsCycleGbn);                  //검사주기

                sqlParameter.Add("@DefectContents", strDefectContents);//문제내역
                sqlParameter.Add("@DefectReason", strDefectReason);//문제원인
                sqlParameter.Add("@DefectRespectContents", strDefectRespectContents);//문제조치
                sqlParameter.Add("@Comments", strComments);//비고
                sqlParameter.Add("@CreateUserID", strCreateUserID);//생성자   //메인에 저장되있는 전역변수 UserID // 만들어야함

                WizCommon.Procedure pro1 = new WizCommon.Procedure();
                pro1.list_OutputName = new List<string>();
                pro1.list_OutputLength = new List<string>();

                pro1.Name = "xp_dvlMoldIns_iRegularInspect";
                pro1.OutputUseYN = "Y";
                pro1.list_OutputName.Add("@MoldInspectID");
                pro1.list_OutputLength.Add("20");

                Prolist.Add(pro1);
                ListParameter.Add(sqlParameter);

                if (GridData1.Rows.Count > 0)
                {
                    for (int i = 0; i < GridData1.Rows.Count; i++)
                    {
                        Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();

                        strMcInspectBasisSeq = GridData1.Rows[i].Cells["McSeq"].Value.ToString();
                        strMcrInspectLegend = GridData1.Rows[i].Cells["InspectionLegendID"].Value.ToString();
                        strMcrInspectValue = "0";

                        sqlParameter2.Add("@MoldInspectID", strMcrInstpectID);
                        sqlParameter2.Add("@MoldSeq", strMcInspectBasisSeq); //
                        sqlParameter2.Add("@MoldInsBasisID", strMcInstpectBasisID); //
                        sqlParameter2.Add("@MoldInsSeq", strMcInspectBasisSeq);//
                        sqlParameter2.Add("@MldValue", strMcrInspectValue);//
                        sqlParameter2.Add("@MldInspectLegend", strMcrInspectLegend);//

                        sqlParameter2.Add("@Comments", strComments);//비고
                        sqlParameter2.Add("@CreateUserID", strCreateUserID);//

                        WizCommon.Procedure pro2 = new WizCommon.Procedure();
                        pro2.list_OutputName = new List<string>();
                        pro2.list_OutputLength = new List<string>();

                        pro2.Name = "xp_dvlMoldIns_iRegularInspectSub";
                        pro2.OutputUseYN = "N";
                        pro2.list_OutputName.Add("@MoldInspectID");
                        pro2.list_OutputLength.Add("20");

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter2);

                    }
                }
                if (GridData2.Rows.Count > 0)
                {
                    for (int i = 0; i < GridData2.Rows.Count; i++)
                    {
                        Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();

                        strMcInspectBasisSeq = GridData2.Rows[i].Cells["McSeq"].Value.ToString();
                        strMcrInspectLegend = "";
                        strMcrInspectValue = GridData2.Rows[i].Cells["InspectionFigure"].Value.ToString();

                        sqlParameter3.Add("@MoldInspectID", strMcrInstpectID);
                        sqlParameter3.Add("@MoldSeq", strMcInspectBasisSeq); //
                        sqlParameter3.Add("@MoldInsBasisID", strMcInstpectBasisID); //
                        sqlParameter3.Add("@MoldInsSeq", strMcInspectBasisSeq);//
                        sqlParameter3.Add("@MldValue", strMcrInspectValue);//
                        sqlParameter3.Add("@MldInspectLegend", strMcrInspectLegend);//

                        sqlParameter3.Add("@Comments", strComments);//비고
                        sqlParameter3.Add("@CreateUserID", strCreateUserID);//

                        WizCommon.Procedure pro3 = new WizCommon.Procedure();
                        pro3.list_OutputName = new List<string>();
                        pro3.list_OutputLength = new List<string>();

                        pro3.Name = "xp_dvlMoldIns_iRegularInspectSub";
                        pro3.OutputUseYN = "N";
                        pro3.list_OutputName.Add("@MoldInspectID");
                        pro3.list_OutputLength.Add("20");

                        Prolist.Add(pro3);
                        ListParameter.Add(sqlParameter3);
                    }
                }
                List<KeyValue> list_Result = new List<KeyValue>();
                list_Result = DataStore.Instance.ExecuteAllProcedureOutputListGetCS(Prolist, ListParameter);

                if (list_Result[0].key.ToLower() == "success")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("금형 점검 등록을 완료하였습니다", "[금형점검등록완료]", 3, 1);
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("[저장실패]\r\n" + list_Result[0].value.ToString(), "[오류]", 0, 1);
                    return;
                }
                GridCellClear();
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
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


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (GridData1.RowCount + GridData2.RowCount == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("[금형]과 [개정일자]를 선택하시고, 입력버튼 클릭 후 값을 입력하여 주십시오.", "[확인]", 3, 1);
                return;
            }
            else
            {
                if (GridData1.Rows.Count > 0)
                {

                    for (int i = 0; i < GridData1.Rows.Count; i++)
                    {
                        if (GridData1.Rows[i].Cells["InspectionLegendID"].Value.ToString() == string.Empty)
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("검사자료-범례 '입력'을 클릭하시고, 값을 입력해주십시오.", "[입력 확인]", 3, 1);
                            return;
                        }
                    }
                }
                if (GridData2.Rows.Count > 0)
                {
                    for (int i = 0; i < GridData2.Rows.Count; i++)
                    {
                        if (GridData2.Rows[i].Cells["InspectionFigure"].Value.ToString() == string.Empty)
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("검사자료-수치 '입력'을 클릭하시고, 값을 입력해주십시오.", "[입력 확인]", 3, 1);
                            return;
                        }
                    }
                }
                ProcSave();
            }

        }

        private void cmd_Click(object sender, EventArgs e)
        {
            if (GridData2.Rows.Count > 0)
            {
                for (int i = 0; i < GridData2.Rows.Count; i++)
                {
                    GridData2.Rows[i].Selected = true;
                    if (GridData2.Rows[i].Cells["InspectionFigure"].Value.ToString() == string.Empty)
                    {
                        if (GridData2.Rows[i].Cells["McImagePath"].Value != null && GridData2.Rows[i].Cells["McImageFile"].Value != null)
                        {
                            No = GridData2.Rows[i].Cells["No"].Value.ToString().Trim();
                            CheckList = GridData2.Rows[i].Cells["CheckList"].Value.ToString().Trim();
                            InsContents = GridData2.Rows[i].Cells["InsContents"].Value.ToString().Trim();
                            Path = GridData2.Rows[i].Cells["McImagePath"].Value.ToString().Trim();
                            File = GridData2.Rows[i].Cells["McImageFile"].Value.ToString().Trim();
                        }
                        else
                        {
                            No = GridData2.Rows[i].Cells["No"].Value.ToString().Trim();
                            CheckList = GridData2.Rows[i].Cells["CheckList"].Value.ToString().Trim();
                            InsContents = GridData2.Rows[i].Cells["InsContents"].Value.ToString().Trim();
                            Path = string.Empty;
                            File = string.Empty;
                        }
                        //행의 전체 크기, 행의 입력시작 행위치를 보내서 행위치부터 마지막까지의 입력값만 받아온다.
                        GridData2.Rows[i].Selected = true;
                        int cnt = 0;
                        foreach (DataGridViewRow dgvr in GridData2.Rows)
                        {
                            if (dgvr.Visible)
                            { ++cnt; }
                        }
                        II2 = new Frm_PopUp_ImgIns2(cnt, i, No, CheckList, InsContents, McInsCheck, Path, File);
                        II2.WriteTextEvent += new Frm_PopUp_ImgIns2.TextEventHandler(GetData2);
                        II2.blMod = false;
                        II2.Show();
                        break;
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GridCellClear();
        }
        private void GridCellClear()
        {
            LegendClear();
            FigureClear();
        }
        private void LegendClear()
        {
            for (int i = 0; i < GridData1.Rows.Count; i++)
            {
                GridData1.Rows[i].Cells["InspectionLegendName"].Value = "";
                GridData1.Rows[i].Cells["InspectionLegendID"].Value = "";
            }
        }
        private void FigureClear()
        {
            for (int i = 0; i < GridData2.Rows.Count; i++)
            {
                GridData2.Rows[i].Cells["InspectionFigure"].Value = "";
            }
        }

        private void Frm_tprc_DailMachineCheck_Load(object sender, EventArgs e)
        {
            SetScreen();
            InitGrid();
            InitGrid2();
            g_ProcessID = gs.GetValue("Work", "ProcessID", "ProcessID");
            g_MachineList = gs.GetValue("Work", "Machine", "");
            cmdClear.Text = "전체\r\n초기화";
            ClearData();
            SetComboBox();
            blLoad = true;
            if (Frm_tprc_Main.g_tBase.sMoldID != null)
            {
                if (Frm_tprc_Main.g_tBase.sMoldID != string.Empty)
                {
                    SetBasisDate();                         //개정일자
                    ProcQuery();
                    GridCellClear();
                }
            }
        }
        #region Default Grid Setting

        private void InitGrid()
        {
            GridData1.Columns.Clear();
            GridData1.ColumnCount = 14;
            int n = 0;
            // Set the Colums Hearder Names
            GridData1.Columns[n].Name = "No";
            GridData1.Columns[n].HeaderText = "No";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "CheckList";
            GridData1.Columns[n].HeaderText = "점검항목";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "InsContents";
            GridData1.Columns[n].HeaderText = "점검내용";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "McInsCheck";//MoldNo
            GridData1.Columns[n].HeaderText = "확인";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "CycleGbnName";
            GridData1.Columns[n].HeaderText = "주기";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "InspectionLegendName";
            GridData1.Columns[n].HeaderText = "검사";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n++].Visible = true;

            GridData1.Columns[n].Name = "McInspectBasisID";
            GridData1.Columns[n].HeaderText = "McInspectBasisID";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "McSeq";
            GridData1.Columns[n].HeaderText = "McSeq";
            GridData1.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "McInsCycleGbn";
            GridData1.Columns[n].HeaderText = "McInsCycleGbn";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "McInsRecordGbn";
            GridData1.Columns[n].HeaderText = "McInsRecordGbn";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "McImagePath";
            GridData1.Columns[n].HeaderText = "McImagePath";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "McImageFile";
            GridData1.Columns[n].HeaderText = "McImageFile";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "McInspectBasisSeq";
            GridData1.Columns[n].HeaderText = "McInspectBasisSeq";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = false;

            GridData1.Columns[n].Name = "InspectionLegendID";
            GridData1.Columns[n].HeaderText = "InspectionLegendID";
            GridData1.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData1.Columns[n++].Visible = false;

            GridData1.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            GridData1.RowTemplate.Height = 30;
            GridData1.ColumnHeadersHeight = 45;
            GridData1.ScrollBars = ScrollBars.Both;
            GridData1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridData1.ReadOnly = true;

            foreach (DataGridViewColumn col in GridData1.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        #endregion
        #region Default Grid2 Setting

        private void InitGrid2()
        {
            GridData2.Columns.Clear();
            GridData2.ColumnCount = 12;

            int n = 0;
            // Set the Colums Hearder Names
            GridData2.Columns[n].Name = "No";
            GridData2.Columns[n].HeaderText = "No";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "CheckList";
            GridData2.Columns[n].HeaderText = "점검항목";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "InsContents";
            GridData2.Columns[n].HeaderText = "점검내용";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridData2.Columns[n].ReadOnly = true;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "McInsCheck";
            GridData2.Columns[n].HeaderText = "확인";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].ReadOnly = true;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "CycleGbnName";
            GridData2.Columns[n].HeaderText = "주기";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].ReadOnly = true;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "InspectionFigure";
            GridData2.Columns[n].HeaderText = "검사";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].ReadOnly = true;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[n++].Visible = true;

            GridData2.Columns[n].Name = "McInspectBasisID";
            GridData2.Columns[n].HeaderText = "McInspectBasisID";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].ReadOnly = true;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[n++].Visible = false;

            GridData2.Columns[n].Name = "McSeq";
            GridData2.Columns[n].HeaderText = "McSeq";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[n++].Visible = false;

            GridData2.Columns[n].Name = "McInsCycleGbn";
            GridData2.Columns[n].HeaderText = "McInsCycleGbn";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[n++].Visible = false;

            GridData2.Columns[n].Name = "McInsRecordGbn";
            GridData2.Columns[n].HeaderText = "McInsRecordGbn";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            GridData2.Columns[n++].Visible = false;

            GridData2.Columns[n].Name = "McImagePath";
            GridData2.Columns[n].HeaderText = "McImagePath";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[n++].Visible = false;

            GridData2.Columns[n].Name = "McImageFile";
            GridData2.Columns[n].HeaderText = "McImageFile";
            GridData2.Columns[n].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            GridData2.Columns[n].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            GridData2.Columns[n++].Visible = false;

            GridData2.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            GridData2.RowTemplate.Height = 30;
            GridData2.ColumnHeadersHeight = 45;
            GridData2.ScrollBars = ScrollBars.Both;
            GridData2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            GridData2.ReadOnly = true;
            foreach (DataGridViewColumn col in GridData2.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        #endregion
        private void ClearData()
        {
            GridData1.Rows.Clear();
            GridData2.Rows.Clear();
            GridCellClear();
            cboMcInsCycleGbn.DataSource = null;
            cboMcInsBasisDate.DataSource = null;
            mtb_Date.Text = DateTime.Today.ToString("yyyyMMdd");
        }

        private void EnabledTrue(bool boolEnable)
        {
            mtb_Date.Enabled = boolEnable;
            cboMcInsBasisDate.Enabled = boolEnable;
            cboMcInsCycleGbn.Enabled = boolEnable;

            cmdSave.Enabled = boolEnable;
            cmdClear.Enabled = boolEnable;
        }

        private void CheckData()
        {
            string strMcrInspectResult = "";
            string strMcInsCycleGbn = "";
            string strCboMcInsCycleGbn = "";
            if (GridData1.Rows.Count > 0)
            {
                for (int i = 0; i < GridData1.Rows.Count; i++)
                {
                    DataGridViewRow dgvr = GridData1.Rows[i];
                    strMcrInspectResult = dgvr.Cells["InspectionLegendName"].Value.ToString();
                    strMcInsCycleGbn = dgvr.Cells["McInsCycleGbn"].Value.ToString();
                    strCboMcInsCycleGbn = cboMcInsCycleGbn.SelectedValue.ToString();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboMcInsCycleGbn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blLoad)
            {
                ProcQuery();
                //FillGridByMcInsCycleGbn();
            }
        }
        private void FillGridByMcInsCycleGbn()
        {
            int i = 0;
            foreach (DataGridViewRow dgvr in GridData1.Rows)
            {
                dgvr.Cells["InspectionLegendName"].Value = "";
                if (dgvr.Cells["McInsCycleGbn"].Value.ToString() == cboMcInsCycleGbn.SelectedValue.ToString())
                {
                    dgvr.Visible = true;
                    dgvr.Cells["No"].Value = (++i).ToString();
                }
                else
                {
                    dgvr.Visible = false;
                }
            }
            i = 0;
            foreach (DataGridViewRow dgvr in GridData2.Rows)
            {
                dgvr.Cells["InspectionFigure"].Value = "";
                if (dgvr.Cells["McInsCycleGbn"].Value.ToString() == cboMcInsCycleGbn.SelectedValue.ToString())
                {
                    dgvr.Visible = true;
                    dgvr.Cells["No"].Value = (++i).ToString();
                }
                else
                {
                    dgvr.Visible = false;
                }
            }
        }

        private void cmdLegend_mod_Click(object sender, EventArgs e)
        {
            if (GridData1.Rows.Count > 0 && GridData1.SelectedRows.Count > 0)
            {
                int i = GridData1.SelectedRows[0].Index;
                No = GridData1.SelectedRows[0].Cells["No"].Value.ToString().Trim();
                CheckList = GridData1.SelectedRows[0].Cells["CheckList"].Value.ToString().Trim();
                InsContents = GridData1.SelectedRows[0].Cells["InsContents"].Value.ToString().Trim();
                McInsCheck = GridData1.SelectedRows[0].Cells["McInsCheck"].Value.ToString().Trim();
                Path = GridData1.SelectedRows[0].Cells["McImagePath"].Value.ToString().Trim();
                File = GridData1.SelectedRows[0].Cells["McImageFile"].Value.ToString().Trim();

                II = new Frm_PopUp_ImgIns(GridData1.Rows.Count, i, No, CheckList, InsContents, McInsCheck, Path, File);
                II.blMod = true;
                II.WriteTextEvent += new Frm_PopUp_ImgIns.TextEventHandler(GetData);
                II.Show();
            }
        }

        private void cmdFigure_mod_Click(object sender, EventArgs e)
        {
            if (GridData2.Rows.Count > 0 && GridData2.SelectedRows.Count > 0)
            {
                int i = GridData2.SelectedRows[0].Index;
                No = GridData2.SelectedRows[0].Cells["No"].Value.ToString().Trim();
                CheckList = GridData2.SelectedRows[0].Cells["CheckList"].Value.ToString().Trim();
                InsContents = GridData2.SelectedRows[0].Cells["InsContents"].Value.ToString().Trim();
                McInsCheck = GridData2.SelectedRows[0].Cells["McInsCheck"].Value.ToString().Trim();
                Path = GridData2.SelectedRows[0].Cells["McImagePath"].Value.ToString().Trim();
                File = GridData2.SelectedRows[0].Cells["McImageFile"].Value.ToString().Trim();

                II2 = new Frm_PopUp_ImgIns2(GridData2.Rows.Count, i, No, CheckList, InsContents, McInsCheck, Path, File);
                II2.WriteTextEvent += new Frm_PopUp_ImgIns2.TextEventHandler(GetData2);
                II2.blMod = true;
                II2.Show();
            }
        }

        private void cmdLegend_Clear_Click(object sender, EventArgs e)
        {
            LegendClear();
        }

        private void cmdFigure_Clear_Click(object sender, EventArgs e)
        {
            FigureClear();
        }

        private void cboMcInsBasisDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMcInsBasisDate.Items.Count > 0)
            {
                ProcQuery();
            }
        }
        private void GetMoldID()
        {
            bool blOpen = false;//폼이 열려있는지에 대한 여부 확인
            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name is "Frm_PopUpSel_sMoldByArticleID")
                {
                    openForm.Activate();
                    blOpen = true;
                }
            }
            if (!blOpen)
            {
                Frm_PopUpSel_sMoldByArticleID fpmbai = new Frm_PopUpSel_sMoldByArticleID();
                fpmbai.WriteTextEvent += new Frm_PopUpSel_sMoldByArticleID.TextEventHandler(GetData);
                fpmbai.Owner = this;
                fpmbai.Show();

                void GetData(string sArticleID, string sArticle, string sMold, string sMoldID)
                {
                    lblArticle.Text = sArticle;
                    lblArticle.Tag = sArticleID;
                    lblMold.Text = sMold;
                    lblMold.Tag = sMoldID;

                    SetBasisDate();
                    ProcQuery();
                }
            }
        }

        private void sMoldArticle_Click(object sender, EventArgs e)
        {
            GetMoldID();
        }

        private void mtb_Date_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }
        private void LoadCalendar()
        {
            Frm_tins_Calendar calendar = new Frm_tins_Calendar(mtb_Date.Text.Replace("-", ""), mtb_Date.Name);
            calendar.WriteDateTextEvent += new Frm_tins_Calendar.TextEventHandler(GetDate);
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

        private void Frm_tprc_DailMoldCheck_Activated(object sender, EventArgs e)
        {
            ((Frm_tprc_Main)(MdiParent)).LoadRegistry();
        }


        // 작업자 선택버튼 클릭 시 .. _ 신규생성 허윤구(2019.05.15)
        private void cmdPersonChoice_Click(object sender, EventArgs e)
        {            
            string Send_ProcessID = string.Empty;
            string Send_MachineID = string.Empty;

            Send_ProcessID = Frm_tprc_Main.g_tBase.ProcessID;
            Send_MachineID = Frm_tprc_Main.g_tBase.MachineID;

            frm_tprc_setProcess FTSP = new frm_tprc_setProcess(Send_ProcessID, Send_MachineID, true);
            FTSP.Owner = this.ParentForm;
            if (FTSP.ShowDialog() == DialogResult.OK)
            {
            };
        }
    }
}
