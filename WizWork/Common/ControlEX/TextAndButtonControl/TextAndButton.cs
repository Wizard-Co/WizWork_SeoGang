using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;


//*******************************************************************************
//프로그램명    TextAndButtonControl.cs
//메뉴ID        
//설명          TextAndButtonControl
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
    public partial class TextBoxButton : UserControl
    {
		public TextBoxButton()
        {
            InitializeComponent();
        }

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("모양")]
		[Description("컨트롤에 연결된 텍스트입니다.")]
		public new string Text
		{
			get { return textBox1.Text; }
			set { textBox1.Text = value; }
		}

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("동작")]
		[Description("사용하지않음")]
		public bool DetectUrls { get; set; }

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("동작")]
		[Description("편집 컨트롤의 텍스트를 두 줄 이상으로 확장할 수 있는지 여부를 제어합니다.")]
		public bool Multiline
		{
			get { return textBox1.Multiline; }
			set { textBox1.Multiline = value; }
		}

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("동작")]
		[Description("편집 컨트롤의 텍스트를 편집할 수 있는지 여부를 제어합니다.")]
		public bool ReadOnly
		{
			get { return textBox1.ReadOnly; }
			set
			{
				textBox1.ReadOnly = value;
				button1.Enabled = !value;
			}
		}

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("모양")]
		[Description("다중 행 편집 컨트롤에 표시할 스크롤 막대의 종류를 표시합니다.")]
		public ScrollBars ScrollBars
		{
			get { return textBox1.ScrollBars; }
			set
			{
				textBox1.ScrollBars = value;
			}
		}

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("동작")]
		[Description("다중 행 편집 컨트롤의 자동 줄바꿈 여부를 나타냅니다.")]
		public bool WordWrap
		{
			get { return textBox1.WordWrap; }
			set
			{
				textBox1.WordWrap = value;
			}
		}

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("동작")]
		[Description("사용하지않음")]
		public int RightMargin { get; set; }

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("작업")]
		[Description("구성요소의 버튼을 클릭할 때 발생합니다.")]
		public event EventHandler ClickButton
        {
            add
            {
                this.button1.Click += value;
            }
            remove
            {
                this.button1.Click -= value;
            }
        }

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("작업")]
		[Description("구성요소의 텍스트 박스를 두 번 클릭할 때 발생합니다.")]
		public new event MouseEventHandler MouseDoubleClick
		{
			add
			{
				this.textBox1.MouseDoubleClick += value;
			}
			remove
			{
				this.textBox1.MouseDoubleClick -= value;
			}
		}

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("속성 변경")]
		[Description("Text 속성값이 텍스트 박스에서 변경되면 이벤트가 발생합니다.")]
		public new event EventHandler TextChanged
		{
			add
			{
				this.textBox1.TextChanged += value;
			}
			remove
			{
				this.textBox1.TextChanged -= value;
			}
		}

		/// <summary>
		/// textbox와 button의 위치와 크기를 결정한다.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void TextAndButtonControl_Load(object sender, EventArgs e)
        {
			this.BorderStyle = BorderStyle.None;

			textBox1.Location = new Point(0, 0);
			textBox1.Size = new Size(this.Size.Width, this.Size.Height - 2);
			this.Size = textBox1.Size;	// 왜인지는 모르겠지만 this.Size.Height가 textBox1보다 1 작게 되는 문제가 있다.

			int btnSize = textBox1.Height - 4;
			int btnPosX = textBox1.Width - btnSize - 2;

			button1.Location = new Point(btnPosX, 2);
			button1.Size = new Size(btnSize, btnSize);
        }

		/// <summary>
		/// 커서의 모양을 바꿔준다.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBox1_MouseEnter(object sender, EventArgs e)
		{
			if (sender is Button)
			{
				this.button1.Cursor = Cursors.Default;
			}
			else
			{
				this.Cursor = Cursors.IBeam;
			}
		}

		/// <summary>
		/// 커서의 모양을 바꿔준다.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_MouseEnter(object sender, EventArgs e)
		{
			this.Cursor = Cursors.Default;
		}

		/// <summary>
		/// 커서의 모양을 바꿔준다.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_MouseLeave(object sender, EventArgs e)
		{
			this.Cursor = Cursors.IBeam;
		}
    }
}
