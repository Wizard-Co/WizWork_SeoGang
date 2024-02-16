using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using System.IO;

namespace WizWork
{
    public partial class Frm_PopUp_ImgIns : Form
    {
        private DataSet ds = null;
        System.Windows.Forms.Button[] newButton = null;
        public string DefectYN = string.Empty;
        public string Value = string.Empty;
        public bool CheckedAll = false;

        POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad();
        POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();

        int sLegendRow = 0; //DailMachineCheck 폼에서 가져온 범례 그리드 로우 행의 갯수
        public int sCurrentRow = 0;//DailMachineCheck 폼의 범례 그리드의 현재 행의 위치

        public delegate void TextEventHandler(int a, string InspectionLegendName, string InspectionLegendID, Frm_PopUp_ImgIns form);    // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다. 
        string[] Message = new string[2];
        public string sNo = string.Empty;
        public string sCheckList = string.Empty;
        public string sInsContents = string.Empty;
        public string sMcInsCheck = string.Empty;
        public string sPath = string.Empty;
        public string sFile = string.Empty;
        public bool blMod = false;
        public Frm_PopUp_ImgIns()
        {
            InitializeComponent();
        }
        public Frm_PopUp_ImgIns(int LegendRow, int CurrentRow, string No, string CheckList, string InsContents, string McInsCheck, string Path, string File)
        {
            InitializeComponent();
            sLegendRow = LegendRow;
            sCurrentRow = CurrentRow;
            LoadData(5, 4);

            sNo = No;
            sCheckList = CheckList;
            sInsContents = InsContents;
            sMcInsCheck = McInsCheck;
            sPath = Path;
            sFile = File;
        }

        #region 그리드뷰 컬럼 셋팅
        private void InitGrid()
        {
            grdData1.Columns.Clear();
            grdData1.ColumnCount = 3;

            int i = 0;
            grdData1.Columns[i].Name = "No";
            grdData1.Columns[i].HeaderText = "No";
            grdData1.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData1.Columns[i].ReadOnly = true;
            grdData1.Columns[i].Visible = true;

            grdData1.Columns[++i].Name = "점검항목";
            grdData1.Columns[i].HeaderText = "점검항목";
            grdData1.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData1.Columns[i].ReadOnly = true;
            grdData1.Columns[i].Visible = true;

            grdData1.Columns[++i].Name = "확인방법";
            grdData1.Columns[i].HeaderText = "확인방법";
            grdData1.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData1.Columns[i].ReadOnly = true;
            grdData1.Columns[i].Visible = true;

            grdData1.Font = new Font("맑은 고딕", 15);
            grdData1.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 12);
            grdData1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdData1.RowTemplate.Height = 30;
            grdData1.ColumnHeadersHeight = 35;
            grdData1.ScrollBars = ScrollBars.Both;
            grdData1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData1.MultiSelect = false;
            grdData1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdData1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdData1.ReadOnly = true;

            foreach (DataGridViewColumn col in grdData1.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void InitGrid2()
        {
            grdData2.Columns.Clear();
            grdData2.ColumnCount = 1;

            int i = 0;
            grdData2.Columns[i].Name = "점검내용";
            grdData2.Columns[i].HeaderText = "점검내용";
            grdData2.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData2.Columns[i].ReadOnly = true;
            grdData2.Columns[i].Visible = true;

            grdData2.Font = new Font("맑은 고딕", 15);
            grdData2.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 12);
            grdData2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdData2.ColumnHeadersHeight = 35;
            grdData2.RowTemplate.Height = 30;
            grdData2.ScrollBars = ScrollBars.Both;
            grdData2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData2.MultiSelect = false;
            grdData2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdData2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdData2.ReadOnly = true;

            foreach (DataGridViewColumn col in grdData2.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        #endregion

        private void FillGrid(string No, string CheckList, string InsContents, string McInsCheck)
        {
            ClearData();
            grdData1.Rows.Add(No, CheckList, McInsCheck);
            grdData2.Rows.Add(InsContents);
        }
        private void ClearData()
        {
            lblImgName.Text = string.Empty;
            picImg.Image = null;
            grdData1.Rows.Clear();
            grdData2.Rows.Clear();
        }

        void LoadData(int Horizontal, int Vertical)
        {
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add(Code_sCmCode.CODEGBN, "MCLEGEND");
            sqlParameter.Add(Code_sCmCode.SRELATION, "");

            ds = DataStore.Instance.ProcedureToDataSet("xp_Code_sCmCode", sqlParameter, false);

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.newButton = new Button[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == Horizontal * Vertical - 1)
                    {
                        break;
                    }

                    Button newButton = new Button();
                    tlpReason.Controls.Add(newButton, (i % Horizontal), (i / Horizontal));
                    DataRow dr = ds.Tables[0].Rows[i];

                    newButton.Text = dr[Code_sCmCode.CODE_NAME].ToString();
                    newButton.Tag = dr[Code_sCmCode.CODE_ID].ToString();//ReasonCode;
                    newButton.Dock = DockStyle.Fill;
                    newButton.Font = new Font("맑은 고딕", 80, FontStyle.Bold);
                    newButton.ForeColor = Color.Black;
                    newButton.Click += new System.EventHandler(this.SelectReasonBtn);
                    this.newButton[i] = newButton;
                }
            }
        }
        private void SelectReasonBtn(object sender, EventArgs e)
        {
            if (sLegendRow >= sCurrentRow)
            {
                WriteTextEvent(sCurrentRow, ((Button)sender).Text.ToString(), ((Button)sender).Tag.ToString(), this);
                if (blMod)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
                sCurrentRow = sCurrentRow + 1;
                if (sLegendRow == sCurrentRow)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                if (sNo == "" && sCheckList == "" && sInsContents == "" && sMcInsCheck == "")
                {
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
                else
                {
                    FillGrid(sNo, sCheckList, sInsContents, sMcInsCheck);
                }

                if (sPath != "" && sFile != "")
                {
                    lblImgName.Text = sFile;
                    FtpDownload(sPath, sFile);
                }

                
            }
        }
        private void SetScreen()
        {
            //패널 배치 및 조정          
            pnlForm.Dock = DockStyle.Fill;
            foreach (Control control in pnlForm.Controls)
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
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Frm_PopUp_ImgIns_Load(object sender, EventArgs e)
        {
            InitGrid();
            InitGrid2();
            SetScreen();

            FillGrid(sNo, sCheckList, sInsContents, sMcInsCheck);

            if (sPath != "" && sFile != "")
            {
                lblImgName.Text = sFile;
                FtpDownload(sPath, sFile);
            }
        }

        private void Frm_PopUp_ImgIns_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Control pic in Controls)
            {
                if (pic is PictureBox)
                {
                    if (((PictureBox)(pic)).Image != null)
                    {
                        ((PictureBox)(pic)).Image = null;
                        ((PictureBox)(pic)).Image.Dispose();
                    }
                }
            }
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            PopUp popup = new PopUp();
            popup.Picture = picImg.Image; //OrderPopUp의 이미지를 PopUp의 PictureBox에 할당. 
            popup.Show();
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

                //// FTP 접속이 가능한지 체크하기
                //if (_ftp.checkConnect() == false)
                //{
                //    picImg.Image = global::WizWork.Properties.Resources.NoImageByConnect;
                //    picImg.SizeMode = PictureBoxSizeMode.Zoom;
                //    return;
                //}

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
                        if (string.Compare(filename.ToUpper(), File.ToUpper()) == 0)
                        { ftpExistFile = true; break; }
                    }

                    if (ftpExistFile == false)
                    {
                        //Message[0] = "해당 " + File + " 이미지가 존재하지 않습니다.";
                        //Message[1] = "[파일 존재하지 않음]";
                        throw new Exception();
                    }

                    else if (_ftp.GetFileSize(picImg.Tag.ToString()) == 0)//파일사이즈가 0일때
                    {
                        //Message[0] = "해당 " + File + "의 이미지의 파일사이즈가 0입니다. 사무실프로그램에서 파일을 다시 업로드 해주시기 바랍니다.";
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}