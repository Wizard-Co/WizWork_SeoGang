using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
namespace WizWork
{
    public partial class frm_tins_InspectAuto_U_New : Form
    {
        string[] Message = new string[2];
        string m_ArticleID = "";

        WizWorkLib Lib = new WizWorkLib();

        public frm_tins_InspectAuto_U_New()
        {
            InitializeComponent();
        }

        private void frm_tins_InspectAuto_U_New_Load(object sender, EventArgs e)
        {
            Size = new System.Drawing.Size(1004, 620);
            SetScreen();
            setComboBox();
            setClear();
            EnabledFalse();
            EnabledTrue();
            txtLotNo.Select();
            txtLotNo.Focus();

            cboProcess.Enabled = false;
        }

        #region 패널 채우기 SetScreen

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

        #endregion

        private void cmdLotNo_Click(object sender, EventArgs e)
        {

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

            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sInspectAutoBasisByArticleID", sqlParameter, false);
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
            if (txtLotNo.Text != null)
            {
                try
                {

                }
                catch(Exception ex)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                }
            }

            return true;
        }

        private void SetFormValue(DataTable argsDt)
        {
           
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            

        }

        private void btnInit_Click(object sender, EventArgs e)
        {
           
        }

        private void EnabledFalse()
        {
            
        }

        private void EnabledTrue()
        {
           
        }

       

        private void cboEcoNO_KeyUp(object sender, KeyEventArgs e)
        {

        }


        private void SetcboEcoNO_Chg()
        {

            
        }
        private void FillGridInsItem()
        {
            try
            {
                

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
            
        }

        private bool SaveData()
        {
            return true;
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

            strInsType = dr.Cells["InsType"].Value.ToString().Trim();

            // 정량일때만, 최대값 과 최소값을 구해야 한다.  > 2020.02.12 허윤구 수정.
            if (strInsType == "2")
            {
                duMRAinVal = double.Parse(dr.Cells["InsRASpecMin"].Value.ToString().Trim());
                duRAMaxVal = double.Parse(dr.Cells["InsRASpecMax"].Value.ToString().Trim());
                strSpec = dr.Cells["InsSpec"].Value.ToString();
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
           
        }

        private void CellAccess()
        {
          

        }
        private void SetTabPage(DataGridView _dgvInspect, string _strtitle)
        {
        }

        private void FillGridData(string strGrdDatadRow)
        {
            


        }
      
        /// <summary>
        /// 검사입력값
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInputData_Click(object sender, EventArgs e)
        {
            
        }

        public void SetCheckValue(string strChkValue)
        {
         
        }

        public void SetCheckValueCancel(string strChkValue)
        {
            
        }
        

        private void InitTabPage()
        {
           
        }

        

        private void cmdLotNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void grdInsItem_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            
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
            
        }

        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            
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

        private void frm_tins_InspectAuto_U_New_Activated(object sender, EventArgs e)
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


            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sArticleJajuPicture", sqlParameter, false);
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow DR = dt.Rows[0];
                if ((DR["Article"].ToString().Trim() == txtArticle.Text.Trim()) && (DR["BuyerArticleNo"].ToString().Trim() == txtBuyerArticleNo.Text.Trim()))
                {
                    // 제대로 Article 정보를 가지고 왔다면,
                    string Path = DR["Sketch4Path"].ToString();
                    string File = DR["Sketch4File"].ToString();

                    if ((Lib.CheckNull(Path) == string.Empty) || (Lib.CheckNull(File) == string.Empty))
                    {
                        //Message[0] = "[그림참조 실패]";
                        //Message[1] = "그림을 첨부등록 하지 않았어!";
                        //WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
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
                        Message[0] = "FTP서버에 해당 파일인 " + File + " 파일이 존재하지 않습니다.";
                        Message[1] = "[파일 존재하지 않음]";
                        throw new Exception();
                    }

                    else if (_ftp.GetFileSize(picImg.Tag.ToString()) == 0)//파일사이즈가 0일때
                    {
                        Message[0] = "FTP서버에 해당 파일인 " + File + "의 파일사이즈가 0입니다. 사무실프로그램에서 파일을 다시 업로드 해주시기 바랍니다.";
                        Message[1] = "[파일 크기 오류]";
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
                }
                catch (Exception excpt)
                {
                    Console.Write(excpt.Message);
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[0], Message[1], 3, 1);
                }
            }
        }

        private void picImg_DoubleClick(object sender, EventArgs e)
        {
            if (picImg.Tag != null)
            {
                this.Visible = false;

                PopUp popup = new PopUp();
                popup.Owner = this;

                popup.Picture = picImg.Image; //OrderPopUp의 이미지를 PopUp의 PictureBox에 할당. 
                popup.ShowDialog();
            }            
        }
        public void ComeBack_BigPicture()
        {
            this.Visible = true;
        }
    }
}
