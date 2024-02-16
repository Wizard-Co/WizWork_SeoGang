using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


//*******************************************************************************
//프로그램명    StatusBox.cs
//메뉴ID        
//설명          StatusBox - 사용하지 않음
//작성일        2012.12.06
//개발자        남인호
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//
//
//*******************************************************************************

namespace KR_POP.Common.ControlEX.StatusBox
{
    public partial class StatusBox : Label
    {
        private int p_TitleWidth = 0;
        private string p_TitleString = string.Empty;
        private string p_ValueString = string.Empty;
        private string p_TotalString = string.Empty;
        private int p_Value = 0;
        private int p_Total = 0;
        private int p_PosTop = 0;
        private int p_PosBottom = 0;
        private Bitmap p_Bitmap = null;


        #region Properties

        public int PosDeviderBottom
        {
            get { return p_PosBottom; }
            set { p_PosBottom = value; }
        }

        public int PosDeviderTop
        {
            get { return p_PosTop; }
            set { p_PosTop = value; }
        }

        public int Total
        {
            get { return p_Total; }
            set { p_Total = value; }
        }

        public int Value
        {
            get { return p_Value; }
            set { p_Value = value; }
        }

        public string TotalString
        {
            get { return p_TotalString; }
            set { p_TotalString = value; }
        }

        public string ValueTitle
        {
            get { return p_ValueString; }
            set { p_ValueString = value; }
        }

        public string Title
        {
            get { return p_TitleString; }
            set { p_TitleString = value; }
        }

        public int TitleWidth
        {
            get { return p_TitleWidth; }
            set { p_TitleWidth = value; }
        }

        #endregion

        public StatusBox()
        {
            InitializeComponent();
        }

        public StatusBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            p_Bitmap = new Bitmap(Bounds.Width, Bounds.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(p_Bitmap))
            {
                g.Clear(Color.FromKnownColor(KnownColor.Control));

                int ValueWidth = p_PosTop - p_TitleWidth;
                Rectangle rectTitle = new Rectangle(0, 0, p_TitleWidth, this.Bounds.Height);
                Rectangle rectLabelValue = new Rectangle(p_TitleWidth, 0, ValueWidth, this.Bounds.Height);
                Rectangle rectLabelTotal = new Rectangle(p_PosBottom, 0, this.Bounds.Right, this.Bounds.Height);
                Rectangle rectValueString = new Rectangle(p_TitleWidth, 0, ValueWidth - ValueWidth / 2, this.Bounds.Height);
                Rectangle rectTotalString = new Rectangle(p_PosBottom + ValueWidth / 2, 0, this.Bounds.Right, this.Bounds.Height);
                Brush brushTitleBG = new SolidBrush(Color.FromKnownColor(KnownColor.ControlDarkDark));
                Brush brushTitleString = new SolidBrush(Color.White);
                Brush brushLabelString = new SolidBrush(Color.Gray);
                Brush brushValueString = new SolidBrush(Color.DarkOrange);
                Brush brushTotalString = new SolidBrush(Color.FromKnownColor(KnownColor.ControlDarkDark));
                Pen penTitleBg = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark));
                Pen penDivider = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark), 3);
                Font fontLabel = new Font(this.Font.FontFamily, 9, FontStyle.Regular, GraphicsUnit.Point, this.Font.GdiCharSet, false);

                StringFormat sfLabelValue = new StringFormat();
                sfLabelValue.Alignment = StringAlignment.Near;
                sfLabelValue.LineAlignment = StringAlignment.Near;

                StringFormat sfLabelTotal = new StringFormat();
                sfLabelTotal.Alignment = StringAlignment.Far;
                sfLabelTotal.LineAlignment = StringAlignment.Far;

                StringFormat sfValue = new StringFormat();
                sfValue.Alignment = StringAlignment.Far;
                sfValue.LineAlignment = StringAlignment.Center;

                StringFormat sfTotal = new StringFormat();
                sfTotal.Alignment = StringAlignment.Near;
                sfTotal.LineAlignment = StringAlignment.Center;



                g.FillRectangle(brushTitleBG, rectTitle);
                g.DrawRectangle(penTitleBg, rectTitle);
                g.DrawString(p_TitleString, Font, brushTitleString, rectTitle.X, rectTitle.Y);

                g.DrawLine(penDivider, p_PosTop, 0, p_PosBottom, Bounds.Bottom);

                g.DrawString(p_ValueString, fontLabel, brushLabelString, rectLabelValue, sfLabelValue);
                g.DrawString(p_TotalString, fontLabel, brushLabelString, rectLabelTotal, sfLabelTotal);

                g.DrawString(p_Value.ToString("N0"), Font, brushValueString, rectValueString, sfValue);
                g.DrawString(p_Total.ToString("N0"), Font, brushTotalString, rectTotalString, sfTotal);

                this.Image = p_Bitmap;
                this.Refresh();
            }

            base.OnPaint(e);
        }
    }
}
