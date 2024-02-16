using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace WizWork.Popup
{
    public partial class Com_MessageBox : Form
    {

        static Com_MessageBox newMessageBox;
        public Timer msgTimer;
        static string Button_id;
        int disposeFormTimer;
        int _intTimer;

        public Com_MessageBox()
        {
            InitializeComponent();

            setClear();
        }

        private void setClear()
        {
            lblTitle.Text = "";
            lblTimer.Text = "";
            lblMessage.Text = "";
            txtInfo.Text = "";
        }

        //public static string ShowBox(string txtMessage)
        //{
        //    newMessageBox = new Com_MessageBox();
        //    newMessageBox.lblMessage.Text = txtMessage;
        //    newMessageBox.ShowDialog();
        //    return Button_id;
        //}

        //public static string ShowBox(string txtMessage, string txtTitle)
        //{
        //    newMessageBox = new Com_MessageBox();
        //    newMessageBox.lblTitle.Text = txtTitle;
        //    newMessageBox.lblMessage.Text = txtMessage;
        //    newMessageBox.ShowDialog();
        //    return Button_id;
        //}


        /// <summary>
        /// Type = 0 : OK, Cancel / Type = 1 : OK만 / Type = 2 : 라벨 발행용으로 사용
        /// </summary>
        /// <param name="txtMessage"></param>
        /// <param name="txtTitle"></param>
        /// <param name="intTimer"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public static DialogResult ShowBox(string txtMessage, string txtTitle = "", int intTimer = 0, int intType = 0, List<string> lstInfo = null)
        {
            newMessageBox = new Com_MessageBox();
            newMessageBox.lblTitle.Text = txtTitle;
            newMessageBox.lblTitle.Tag = intTimer.ToString();
            newMessageBox.lblMessage.Text = txtMessage;
            newMessageBox.lblMessage.Tag = intType.ToString();
            newMessageBox.setInfo(lstInfo);
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

        // 기본적으로 Info 는 안보이도록, Info 메시지 세팅
        private void setInfo(List<string> lstInfo)
        {
            if (lstInfo == null
                || lstInfo.Count == 0)
            {
                tlpBottom.ColumnStyles[0].Width = 0;
            }

            int height = newMessageBox.Height;

            newMessageBox.Height = newMessageBox.Height - ( newMessageBox.txtInfo.Height + 3);
            
            if (lstInfo != null
                && lstInfo.Count > 0)
            {
                string Info = "";
                for (int i = 0; i < lstInfo.Count; i++)
                {
                    if (i == 0) { Info += lstInfo[i]; }
                    else { Info += "\r\n" + lstInfo[i]; }
                }

                newMessageBox.txtInfo.Text = Info;
            }
        }

        private void Com_MessageBox_Load(object sender, EventArgs e)
        {
            //SetScreen();
            _intTimer = int.Parse(newMessageBox.lblTitle.Tag.ToString());
            int _intType = int.Parse(newMessageBox.lblMessage.Tag.ToString());

            //OK버튼만
            if (_intType == 1)
            {
                tlpOC.SetColumnSpan(btnOK, 2);
            }
            else if (_intType == 2)
            {
                tlpOC.Visible = false;
                //pnlMessage.Size = new Size(439, 118);
                //lblMessage.Size = pnlMessage.Size;
                lblMessage.Font = new Font("맑은 고딕", 24F, FontStyle.Bold);
            }

            //타이머 쓸 경우
            if (_intTimer > 0)
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

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (btnInfo.Text.Equals("접기"))
            {
                btnInfo.Image = global::WizWork.Properties.Resources.base_down_arrow;
                btnInfo.Text = "자세히";

                newMessageBox.Height = newMessageBox.Height - (txtInfo.Height + 3);
            }
            else
            {
                btnInfo.Image = global::WizWork.Properties.Resources.base_up_arrow;
                btnInfo.Text = "접기";

                newMessageBox.Height = newMessageBox.Height + (txtInfo.Height + 3);
            }
        }
    }
}