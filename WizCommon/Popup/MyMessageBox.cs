using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace WizCommon.Popup
{
    public partial class MyMessageBox : Form
    {

        static MyMessageBox newMessageBox;
        public Timer msgTimer;
        static string Button_id;
        int disposeFormTimer;
        int _intTimer;
        int _intSize;

        public MyMessageBox()
        {
            InitializeComponent();
        }

        //public static string ShowBox(string txtMessage)
        //{
        //    newMessageBox = new MyMessageBox();
        //    newMessageBox.lblMessage.Text = txtMessage;
        //    newMessageBox.ShowDialog();
        //    return Button_id;
        //}

        //public static string ShowBox(string txtMessage, string txtTitle)
        //{
        //    newMessageBox = new MyMessageBox();
        //    newMessageBox.lblTitle.Text = txtTitle;
        //    newMessageBox.lblMessage.Text = txtMessage;
        //    newMessageBox.ShowDialog();
        //    return Button_id;
        //}


        /// <summary>
        /// Type = 0 : OK, Cancel / Type = 1 : OK¸¸ / Type = 2 : ¶óº§ ¹ßÇà¿ëÀ¸·Î »ç¿ë
        /// </summary>
        /// <param name="txtMessage"></param>
        /// <param name="txtTitle"></param>
        /// <param name="intTimer"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public static DialogResult ShowBox(string txtMessage, string txtTitle, int intTimer, int intType, int intSize = 0)
        {
            newMessageBox = new MyMessageBox();
            newMessageBox.lblTitle.Text = txtTitle;
            newMessageBox.lblTitle.Tag = intTimer.ToString();
            newMessageBox.lblMessage.Text = txtMessage;
            newMessageBox.lblMessage.Tag = intType.ToString();
            newMessageBox._intSize = intSize;
            newMessageBox.ShowDialog();
            if (Button_id == "1")
            {
                return DialogResult.OK;
            }
            else
            {
                return DialogResult.No;
            }
        }

        private void SetScreen()
        {
            lblTimer.Text = "";
            //ÆÐ³Î ¹èÄ¡ ¹× Á¶Á¤          
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
                            }
                        }
                    }
                }
            }
        }

        private void MyMessageBox_Load(object sender, EventArgs e)
        {
            SetScreen();
            _intTimer = int.Parse(newMessageBox.lblTitle.Tag.ToString());
            int _intType = int.Parse(newMessageBox.lblMessage.Tag.ToString());
            
            //»çÀÌÁî
            if (newMessageBox._intSize == 0)
            {
                newMessageBox.Size = new Size(467, 167);
                lblTitle.Font = new Font("¸¼Àº °íµñ", 14F, FontStyle.Bold);
                lblTimer.Font = new Font("¸¼Àº °íµñ", 14F, FontStyle.Bold);
            }
            else if (newMessageBox._intSize == 1)
            {
                newMessageBox.Size = new Size(567, 267);
                lblTitle.Font = new Font("¸¼Àº °íµñ", 17F, FontStyle.Bold);
                lblTimer.Font = new Font("¸¼Àº °íµñ", 17F, FontStyle.Bold);
            }
            //OK¹öÆ°¸¸
            if (_intType == 1)
            {
                tlpOC.SetColumnSpan(btnOK, 2);
            }
            else if (_intType == 2)
            {
                tlpOC.Visible = false;
                //pnlMessage.Size = new Size(439, 118);
                //lblMessage.Size = pnlMessage.Size;
                lblMessage.Font = new Font("¸¼Àº °íµñ", 24F, FontStyle.Bold);
            }
            //Å¸ÀÌ¸Ó ¾µ °æ¿ì
            if(_intTimer > 0)
            { 
                disposeFormTimer = _intTimer;
                newMessageBox.lblTimer.Text = disposeFormTimer.ToString();
                msgTimer = new Timer();
                msgTimer.Interval = 1000;
                msgTimer.Enabled = true;
                msgTimer.Start();
                msgTimer.Tick += new System.EventHandler(this.timer_tick);
            }

        }

        private void MyMessageBox_Paint(object sender, PaintEventArgs e)
        {
            //Graphics mGraphics = e.Graphics;
            //Pen pen1 = new Pen(Color.FromArgb(96, 155, 173), 1);
            
            //Rectangle Area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            //LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(240, 240, 240), Color.FromArgb(0, 0, 0), LinearGradientMode.Vertical);
            ////LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(160, 160, 160), Color.FromArgb(0, 0, 0), LinearGradientMode.Vertical);
            ////LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(160, 160, 160), Color.FromArgb(245, 251, 251), LinearGradientMode.Vertical);
            ////LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(96, 155, 173), Color.FromArgb(245, 251, 251), LinearGradientMode.Vertical);
            ////LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(24, 131, 215), Color.FromArgb(245, 251, 251), LinearGradientMode.Vertical);
            ////LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(211, 216, 211), Color.FromArgb(245, 251, 251), LinearGradientMode.Vertical);
            //mGraphics.FillRectangle(LGB, Area1);
            //mGraphics.DrawRectangle(pen1, Area1);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_intTimer > 0)
            {
                newMessageBox.msgTimer.Stop();
                newMessageBox.msgTimer.Dispose();
            }
            Button_id = "1";
            //DialogResult = DialogResult.OK;
            newMessageBox.Dispose(); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_intTimer > 0)
            {
                newMessageBox.msgTimer.Stop();
                newMessageBox.msgTimer.Dispose();
            }
            Button_id = "2";
            //DialogResult = DialogResult.No;
            newMessageBox.Dispose();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            disposeFormTimer--;

            if (disposeFormTimer >= 0)
            {
                newMessageBox.lblTimer.Text = disposeFormTimer.ToString();
            }
            else
            {
                newMessageBox.msgTimer.Stop();
                newMessageBox.msgTimer.Dispose();
                newMessageBox.Dispose();
            }
        }
    }
}