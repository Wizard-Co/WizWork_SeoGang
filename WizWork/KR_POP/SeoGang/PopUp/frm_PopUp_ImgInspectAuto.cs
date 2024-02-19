using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizWork
{
    public partial class frm_PopUp_ImgInspectAuto : Form
    {
        Image _picture;

        string Path = "";
        string File = "";

        public frm_PopUp_ImgInspectAuto()
        {
            InitializeComponent();

            SetScreen();
        }

        public frm_PopUp_ImgInspectAuto(string Path, string File)
        {
            InitializeComponent();

            SetScreen();

            this.Path = Path;
            this.File = File;
        }

        #region Dock → 채우기

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

        public Image Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                _picture = value;
                pictureBox.Image = _picture;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 이미지 회전 - 버튼 클릭 이벤트
        // 2020.09.07 - TKB 추가
        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                Bitmap bit = new Bitmap(pictureBox.Image);

                bit.RotateFlip(RotateFlipType.Rotate90FlipXY);

                pictureBox.Image = bit;

                UploadImageToFtp();
            }
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                Bitmap bit = new Bitmap(pictureBox.Image);

                bit.RotateFlip(RotateFlipType.Rotate270FlipXY);

                pictureBox.Image = bit;

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

                byte[] img = ImageToByte(pictureBox.Image);

                UploadUsingByte(FTP_ADDRESS + Path, FTP_ID, FTP_PASS, img, File);
            }
            catch (Exception ex)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null) { this.Picture = pictureBox.Image; }            
            this.Close();
        }
    }
}
