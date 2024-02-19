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
    public partial class Frm_PopUp_ImgWorkOrder : Form
    {

        string[] Message = new string[2];

        public string sPath = string.Empty;
        public string sFile = string.Empty;

        

        public Frm_PopUp_ImgWorkOrder()
        {
            InitializeComponent();
        }
        public Frm_PopUp_ImgWorkOrder(string Path, string File)
        {
            InitializeComponent();

            sPath = Path;
            sFile = File;
        }

        // 로드 이벤트.
        private void Frm_PopUp_ImgWorkOrder_Load(object sender, EventArgs e)
        {
            SetScreen();



            if (sPath != "" || sFile != "")
            {
                lblImgName.Text = sFile;
                FtpDownload(sPath, sFile);
            }
        }

        private void ClearData()
        {
            lblImgName.Text = string.Empty;
            picImg.Image = null;
        }


        #region 패널 Dock = Fill 로 변경
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

        #endregion

       

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
            this.Visible = false;

            PopUp popup = new PopUp();
            popup.Owner = this;

            popup.Picture = picImg.Image; //OrderPopUp의 이미지를 PopUp의 PictureBox에 할당. 
            popup.ShowDialog();            
        }

        public void ComeBack_BigPicture()
        {
            this.Visible = true;
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