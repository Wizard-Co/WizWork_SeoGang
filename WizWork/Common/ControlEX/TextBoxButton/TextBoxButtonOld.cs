using System;
using System.Drawing;
using System.Windows.Forms;


//*******************************************************************************
//프로그램명    TextBoxButton.cs
//메뉴ID        
//설명          TextBoxButton
//작성일        2012.12.06
//개발자        남인호
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//
//
//*******************************************************************************

namespace KR_POP.Common.ControlEX
{
    public partial class TextBoxButtonOld : RichTextBox
    {
		public TextBoxButtonOld()
        {
            InitializeComponent();

            //p_Button.BackgroundImage = global::KR_POP.Common.Properties.Resources.zoom;
            //p_Button.BackgroundImageLayout = ImageLayout.Zoom;
            //p_Button.Font = new Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            p_Button.MouseEnter += new System.EventHandler(this.p_Button_MouseEnter);
            p_Button.MouseLeave += new System.EventHandler(this.p_Button_MouseLeave);
            //p_Button.FlatStyle = FlatStyle.Flat;
            //p_Button.Visible = true;
            //p_Button.BackColor = Color.White;
        }

        public event EventHandler ClickButton {
            add {
                this.p_Button.Click += value;
            }
            remove {
                this.p_Button.Click -= value;
            }
        }

        protected override void OnCreateControl()
        {
            Rectangle rect = this.Bounds;
            int buttonSize = rect.Height - 4;

            this.p_Button.Left = rect.Right - rect.Left - buttonSize - 4;
            this.p_Button.Top = 0;
            this.p_Button.Size = new Size(buttonSize, buttonSize);

			//if (!this.Controls.Contains(this.p_Button))
			//{
			//    this.Controls.Add(this.p_Button);
			//    //size of control - size control button +10
			//}

            base.OnCreateControl();
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                this.p_Button.Cursor = Cursors.Default;                
            }
            else {
                this.Cursor = Cursors.IBeam;
            }
            
        }

        private void p_Button_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void p_Button_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.IBeam;
        }

    }
}
