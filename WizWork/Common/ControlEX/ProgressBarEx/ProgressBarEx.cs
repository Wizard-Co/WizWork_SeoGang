using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace KR_POP.Common.ControlEX
{
	public enum ProgressBarDisplayText
	{
		Percentage,
		CustomText
	}

	public partial class ProgressBarEx : ProgressBar
	{
		//Property to set to decide whether to print a % or Text
		public ProgressBarDisplayText DisplayStyle
		{
			get
			{
				return _style;
			}
			set
			{
				_style = value;
				Refresh();
			}
		}
		private ProgressBarDisplayText _style = ProgressBarDisplayText.CustomText;



		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("모양")]
		[Description("컨트롤에 연결된 텍스트의 글꼴크기입니다.")]
		public new float FontSize { get; set; }


		//Property to hold the custom text
		public String CustomText
		{
			get
			{
				return _text;
			}
			set
			{
				// Set the Display text (Either a % amount or our custom text
				_text = DisplayStyle == ProgressBarDisplayText.Percentage ? Value.ToString() + '%' : value;
			}
		}

		public new int Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				// Set the Display text (Either a % amount or our custom text
				base.Value = value;
				_text = DisplayStyle == ProgressBarDisplayText.Percentage ? base.Value.ToString() + '%' : _text;
			}
		}

		private Font _f = null;
		private StringFormat _sf = new StringFormat();
		private string _text = "";

		public ProgressBarEx()
		{
			// Modify the ControlStyles flags
			//http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			FontSize = 10;
			_sf.Alignment = StringAlignment.Center;
			_sf.LineAlignment = StringAlignment.Center;
			NativeMethods.SendMessage(this.Handle, 
									  0x400 + 16, //WM_USER + PBM_SETSTATE
									  (IntPtr)KR_POP.Common.ControlEX.NativeMethods.SystemCommands.SC_PBSTPAUSE, //PBST_PAUSED
									  IntPtr.Zero);

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (_f == null)
			{
				_f = new Font("맑은고딕", FontSize, FontStyle.Bold);
			}

			Rectangle rect = ClientRectangle;
			Graphics g = e.Graphics;
			Bitmap bmp = new Bitmap(rect.Width, rect.Height);
			Graphics g_temp = Graphics.FromImage(bmp);

			ProgressBarRenderer.DrawHorizontalBar(g_temp, rect);
			rect.Inflate(-3, -3);
			if (Value > 0)
			{
				// As we doing this ourselves we need to draw the chunks on the progress bar
				Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
				ProgressBarRenderer.DrawHorizontalChunks(g_temp, clip);
			}



			SizeF len = g_temp.MeasureString(_text, _f);
			
			// Calculate the location of the text (the middle of progress bar)
			Point location = new Point(Convert.ToInt32((rect.Width / 2)), Convert.ToInt32((rect.Height / 2)));
			
			// Draw the custom text
			g_temp.DrawString(_text, _f, Brushes.Black, rect, _sf);

			g.DrawImage(bmp, 0, 0);
		}
	}
}
