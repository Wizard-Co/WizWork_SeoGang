using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizMon
{
    class LabelEx : Label
    {
        public string title = "모니터링 화면";

        // 
        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw the formatted text string to the DrawingContext of the control.
            //base.OnPaint(e);
            //Font font = new Font("Tahoma", 48f, FontStyle.Bold);
            //LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height + 5), Color.Gold, Color.Black, LinearGradientMode.Vertical);
            //e.Graphics.DrawString(Text, font, brush, 0, 0);

            RenderDropshadowText(e.Graphics, title, this.Font, Color.MidnightBlue, Color.DimGray, 150, new PointF(0, 0));
        }

        // 텍스트 변경 후 폰트를 다시 그리도록 Invalidate()
        public void setText(string str)
        {
            title = str;
            this.Invalidate();
        }

        #region Font DropShadow Method : RenderDropshadowText()

        protected void RenderDropshadowText(Graphics graphics, string text, Font font, Color foreground, Color shadow
                                                                  , int shadowAlpha, PointF location)
        {
            const int DISTANCE = 2;
            for (int offset = 1; 0 <= offset; offset--)
            {
                Color color = ((offset < 1) ?
                    foreground : Color.FromArgb(shadowAlpha, shadow));
                using (var brush = new SolidBrush(color))
                {
                    var point = new PointF()
                    {
                        X = location.X + (offset * DISTANCE),
                        Y = location.Y + (offset * DISTANCE)
                    };
                    graphics.DrawString(text, font, brush, point);
                }
            }
        }

        #endregion
    }
   


}

