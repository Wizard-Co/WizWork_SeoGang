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
    public partial class Frm_PopUp_ImgArticleInfo : Form
    {

        string[] Message = new string[2];

        public string sPath = string.Empty;
        public string sFile = string.Empty;

        List<RadioButton> lstRbn = new List<RadioButton>();

        Frm_PopUp_ImgArticleInfo_CodeView ArticleImg = new Frm_PopUp_ImgArticleInfo_CodeView();

        PictureBox org = new PictureBox();

        // 확대 축소 기능을 사용 하는가!
        bool ZoomFuction = false;

        // Dock = Fill 로 하면 안되는 리스트
        //string notFill

        public Frm_PopUp_ImgArticleInfo()
        {
            InitializeComponent();
        }
        public Frm_PopUp_ImgArticleInfo(Frm_PopUp_ImgArticleInfo_CodeView ArticleImg)
        {
            InitializeComponent();

            this.ArticleImg = ArticleImg;
        }

        // 로드 이벤트.
        private void Frm_PopUp_ImgArticleInfo_Load(object sender, EventArgs e)
        {
            pnlForm.Dock = DockStyle.Fill;
            SetScreen(pnlForm.Controls);
            //SetScreen();

            CreateRadioButton("작업\r\n표준서", "2"); 
            CreateRadioButton("도면", "1");
            CreateRadioButton("외관검사\r\n기준서", "3");
            CreateRadioButton("검사약도", "4");
            CreateRadioButton("자주공정검사\r\n체크시트", "5");
            CreateRadioButton("작업\r\n지도서", "6");

            lstRbn[0].Checked = true;
            SetImage(lstRbn[0]);

            setZoomFunction();
        }

        // 확대 축소 기능 사용 여부 → 버튼들 Visible 세팅
        private void setZoomFunction()
        {
            btnExpand.Visible = ZoomFuction;
            btnReduce.Visible = ZoomFuction;
            btnUp.Visible = ZoomFuction;
            btnDown.Visible = ZoomFuction;
            btnRight.Visible = ZoomFuction;
            btnLeft.Visible = ZoomFuction;
        }

        #region 라디오 버튼 세팅하기 + 레이아웃에 등록

        private void CreateRadioButton(string Title, string Tag)
        {
            // 폰트 사이즈 구하기
            // 한줄의 6글자 : 13
            // 5 : 15
            // 4 : 17
            int fontSize = 17;
            if (Title.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries).Length > 0
                && Title.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)[0].Length > 4)
            {
                fontSize -= ((Title.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)[0].Length) - 4) * 2;
            }

            RadioButton rbn = new RadioButton();

            rbn.Text = Title;
            rbn.Tag = Tag;
            rbn.Dock = DockStyle.Fill;
            rbn.Font = new Font("맑은 고딕", fontSize, FontStyle.Bold);

            rbn.Checked = false;
            rbn.Appearance = System.Windows.Forms.Appearance.Button;
            rbn.BackColor = System.Drawing.Color.LightSkyBlue;
            rbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            rbn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            rbn.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            rbn.ForeColor = System.Drawing.Color.White;
            rbn.UseVisualStyleBackColor = false;
            rbn.TextAlign = ContentAlignment.MiddleCenter;
            rbn.Click += new System.EventHandler(this.rBtnTopMenu_CheckedChanged);

            lstRbn.Add(rbn);

            tlpTop.Controls.Add(rbn, tlpTop.Controls.Count, 0);
        }

        #endregion

        #region 라디오버튼 클릭 이벤트

        // 이미지 변경
        private void rBtnTopMenu_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbn = sender as RadioButton;
            SetImage(rbn);
        }

        private void SetImage(RadioButton rbn)
        {
            if (rbn.Checked
               && rbn.Tag != null
               && ArticleImg != null)
            {
                string ImageName = "";
                string ImagePath = "";

                if (rbn.Tag.Equals("1"))
                {
                    ImageName = ArticleImg.Sketch1File;
                    ImagePath = ArticleImg.Sketch1Path;
                }
                else if (rbn.Tag.Equals("2"))
                {
                    ImageName = ArticleImg.Sketch2File;
                    ImagePath = ArticleImg.Sketch2Path;
                }
                else if (rbn.Tag.Equals("3"))
                {
                    ImageName = ArticleImg.Sketch3File;
                    ImagePath = ArticleImg.Sketch3Path;
                }
                else if (rbn.Tag.Equals("4"))
                {
                    ImageName = ArticleImg.Sketch4File;
                    ImagePath = ArticleImg.Sketch4Path;
                }
                else if (rbn.Tag.Equals("5"))
                {
                    ImageName = ArticleImg.Sketch5File;
                    ImagePath = ArticleImg.Sketch5Path;
                }
                else if (rbn.Tag.Equals("6"))
                {
                    ImageName = ArticleImg.Sketch6File;
                    ImagePath = ArticleImg.Sketch6Path;
                }

                if (ImageName != string.Empty
                    || ImagePath != string.Empty)
                {
                    txtImgName.Text = ImageName;
                    FtpDownload(ImagePath, ImageName);
                }
                // 이미지가 등록되어 있지 않을 경우!!
                else
                {
                    txtImgName.Text = "";
                    picImg.Image = null;
                }
               
            }
        }

        #endregion

        private void ClearData()
        {
            txtImgName.Text = string.Empty;
            picImg.Image = null;
        }

        #region 패널 Dock = Fill 로 변경
       
        private void SetScreen(Control.ControlCollection controls)
        {
            foreach(Control con in controls)
            {
                if (ZoomFuction == true 
                    && con.Name.Equals("picImg")) { continue; }

                con.Dock = DockStyle.Fill;
                con.Margin = new Padding(0, 0, 0, 0);

                if (con.Controls.Count > 0)
                {
                    SetScreen(con.Controls);
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


        #region FTP 다운로드
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

                            // 확대 축소 기능 사용 여부에 따른 사진 세팅
                            if (ZoomFuction == true)
                            {
                                // 확대 축소 기능 사용
                                picImg.SizeMode = PictureBoxSizeMode.AutoSize;
                                picImg.Dock = DockStyle.None;
                            }
                            else
                            {
                                // 확대 축소 기능 사용 안함
                                picImg.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                         
                            org = new PictureBox();
                            org.Image = picImg.Image;

                            p = ImgPanel.AutoScrollPosition;
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
                    picImg.Dock = DockStyle.Fill;
                    org.Image = null;
                }
            }
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region 이미지 확대, 이동 버튼 클릭 이벤트

        Point p = new Point();

        private Image ZoomPicture(Image img, double width, double height)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * width)
                , Convert.ToInt32(img.Height * height));
            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }

        // 확대
        private void btnExpand_Click(object sender, EventArgs e)
        {
            if (org != null
                && org.Image != null)
            {
                picImg.Image = null;
                picImg.Image = ZoomPicture(org.Image, 1.1, 1.1);
                org.Image = picImg.Image;

                ImgPanel.AutoScrollPosition = p;
            }
        }

        // 축소
        private void btnReduce_Click(object sender, EventArgs e)
        {
            if (org != null
                && org.Image != null)
            {
                picImg.Image = null;
                picImg.Image = ZoomPicture(org.Image, 0.9, 0.9);
                org.Image = picImg.Image;

                ImgPanel.AutoScrollPosition = p;
            }
        }

        // 위
        private void btnUp_Click(object sender, EventArgs e)
        {
            //p = ImgPanel.AutoScrollPosition;

            p.Y -= 50;

            ImgPanel.AutoScrollPosition = p;
        }

        // 아래
        private void btnDown_Click(object sender, EventArgs e)
        {
            //p = ImgPanel.AutoScrollPosition;

            p.Y += 50;

            ImgPanel.AutoScrollPosition = p;
        }

        // 좌
        private void btnLeft_Click(object sender, EventArgs e)
        {
            //p = ImgPanel.AutoScrollPosition;

            p.X -= 50;

            ImgPanel.AutoScrollPosition = p;
        }

        // 우
        private void btnRight_Click(object sender, EventArgs e)
        {
            //p = ImgPanel.AutoScrollPosition;

            p.X += 50;

            ImgPanel.AutoScrollPosition = p;
        }

        #endregion
    }

    #region 코드뷰

    public class Frm_PopUp_ImgArticleInfo_CodeView
    {
        public string ArticleID { get; set; }
        public string Article { get; set; }
        public string BuyerArticleNo { get; set; }

        public string Sketch1File { get; set; }
        public string Sketch1Path { get; set; }

        public string Sketch2File { get; set; }
        public string Sketch2Path { get; set; }

        public string Sketch3File { get; set; }
        public string Sketch3Path { get; set; }

        public string Sketch4File { get; set; }
        public string Sketch4Path { get; set; }

        public string Sketch5File { get; set; }
        public string Sketch5Path { get; set; }

        public string Sketch6File { get; set; }
        public string Sketch6Path { get; set; }

    }

    #endregion
}