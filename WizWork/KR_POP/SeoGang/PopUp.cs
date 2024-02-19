using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWork
{
    public partial class PopUp : Form
    {
        string[] Message = new string[2];
        Image _picture;

        public PopUp()
        {
            InitializeComponent();
        }
        
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
            try
            {
                ((WizWork.Frm_PopUp_ImgWorkOrder)(this.Owner)).ComeBack_BigPicture();
            }
            catch (Exception E)
            {
                Console.Write(E.Message);

                try
                {
                    ((WizWork.frm_tins_InspectAuto_U)(this.Owner)).ComeBack_BigPicture();
                }
                catch (Exception Ex)
                {
                    Message[0] = "[오류]";
                    Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", Ex.Message);
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }
            }
        }
    }
}
