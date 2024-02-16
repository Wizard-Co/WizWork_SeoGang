using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using KR_POP.Common.ControlEX;
using KR_POP.Properties;


//*******************************************************************************
//프로그램명    Frm_Base.cs
//메뉴ID        
//설명          Base Form
//작성일        2012.12.06
//개발자        남인호
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//
//
//*******************************************************************************



namespace KR_POP
{
#if SUB_UI
	public partial class Frm_Base : LonghornForm, IProcedure
#else
	public partial class Frm_Base : Form, IProcedure
#endif
	{
        #region Default Color And Font

        protected static Color ColorReadOnly = Color.FromArgb(255, 255, 191);
        protected static Color ColorKeyValue = Color.FromArgb(170, 200, 200);
        //protected static Color ColorSum = Color.FromArgb(246, 184, 97);
        protected static Color ColorSum = Color.FromArgb(252, 195, 92);
        protected static Color ColorModified = Color.FromArgb(10, 96, 177);

        protected static Font FontModified;
		protected static Font FontNormal;
		//protected static FontFamily ff;
		//protected static PrivateFontCollection pfc = new PrivateFontCollection();

        #endregion



        #region Properties

        protected string _strFormName = string.Empty;
        public string FormName
        {
            get { return _strFormName; }
        }

        protected string _strFormID = string.Empty;
        public string FormID
        {
            get { return _strFormID; }
        }


        #endregion



        #region Ctor
		static Frm_Base()
		{
			//CargoPrivateFontCollection(Resources.malgun);
			//CargoPrivateFontCollection(Resources.malgunbd);

			FontModified = new Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			FontNormal = new Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
		}

        public Frm_Base()
        {

			if (this.Tag != null) return;

            this.Tag = "Y";
            InitializeComponent();
            SetDataGridViewCellStyle();
        }

        #endregion



        #region Control Style and Colors

        private void Frm_Base_Load(object sender, EventArgs e)
        {
			SetControlColors();
        }


		//[DllImport("gdi32.dll")]
		//private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);


		//private static void CargoPrivateFontCollection(byte[] fontArray)
		//{
		//    // Create the byte array and get its length
		//    int dataLength = fontArray.Length;


		//    // ASSIGN MEMORY AND COPY  BYTE[] ON THAT MEMORY ADDRESS
		//    IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength);
		//    Marshal.Copy(fontArray, 0, ptrData, dataLength);


		//    uint cFonts = 0;
		//    AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

		//    //PASS THE FONT TO THE  PRIVATEFONTCOLLECTION OBJECT
		//    pfc.AddMemoryFont(ptrData, dataLength);

		//    //FREE THE  "UNSAFE" MEMORY
		//    Marshal.FreeCoTaskMem(ptrData);

		//    ff = pfc.Families[0];
		//}

        protected void SetControlColors()
        {
            this.BackColor = Color.White;
            SetControlColors(this.Controls);
        }

        private void SetControlColors(Control.ControlCollection collection)
        {
            foreach (Control control in collection)
            {
                string strClassname = control.ToString().Split(',')[0].Split('.').Last();

                if (strClassname.Equals("Label") == true)
                {
                    Label label = control as Label;
					//label.BackColor = Color.FromArgb(244, 238, 176);
					//label.BorderStyle = BorderStyle.Fixed3D;
					label.BackColor = Color.FromArgb(250, 247, 220);
					label.BorderStyle = BorderStyle.None;
                    label.ForeColor = Color.Black;
                }
                else if (strClassname.Equals("CheckBox") == true)
                {
                    CheckBox box = control as CheckBox;
                    box.BackColor = Color.FromArgb(244, 238, 176);
                    box.ForeColor = Color.Black;
                }
				else if (strClassname.Equals("ComboBox") == true)
				{
					ComboBox combo = control as ComboBox;
					combo.DropDownStyle = ComboBoxStyle.DropDownList;
				}
				else if (strClassname.Equals("Panel") == true)
				{
					Panel panel = control as Panel;
					panel.BackColor = Color.FromArgb(250, 247, 220);
					panel.BorderStyle = BorderStyle.Fixed3D;
				}
				else if (strClassname.Equals("DataGridView") == true || strClassname.Equals("DataGridViewEx") == true || strClassname.Equals("TreeGridView") == true)
				{
					foreach (DataGridViewColumn col in (control as DataGridView).Columns)
					{
						col.HeaderCell.Style.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(129)));
						col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
						col.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
					}

					DataGridView grid = control as DataGridView;
					//grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
					grid.EnableHeadersVisualStyles = false;
					grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(244, 238, 176);
					grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(134, 125, 24);
					grid.AllowUserToResizeRows = false;
					grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					grid.EditMode = DataGridViewEditMode.EditOnEnter;
				}
				else if (strClassname.Equals("TableLayoutPanel") == true)
				{
					TableLayoutPanel table = control as TableLayoutPanel;
				}
				else if (strClassname.Equals("Button") == true)
				{
					Button button = control as Button;
					button.FlatStyle = FlatStyle.Standard;
					button.BackColor = Color.FromArgb(244, 238, 176);
					button.ForeColor = Color.Black;
					button.UseVisualStyleBackColor = true;
				}
				else if (strClassname.Equals("TabControl") == true)
				{
					TabControl tab = control as TabControl;
					foreach (TabPage page in tab.TabPages)
					{
						page.BackColor = Color.FromArgb(250, 247, 220);
					}
				}


                if (control.Controls.Count > 0)
                {
                    SetControlColors(control.Controls);
                }
            }
        }
        #endregion



        #region DataGridView Cell Style, and Colors

        protected static DataGridViewCellStyle _styleReadOnlyLeft = new DataGridViewCellStyle();
        protected static DataGridViewCellStyle _styleReadOnlyCenter = new DataGridViewCellStyle();
        protected static DataGridViewCellStyle _styleReadOnlyRight = new DataGridViewCellStyle();
        protected static DataGridViewCellStyle _styleNormalCenter = new DataGridViewCellStyle();
        protected static DataGridViewCellStyle _styleNormalRight = new DataGridViewCellStyle();
        protected static DataGridViewCellStyle _styleKeyValueLeft = new DataGridViewCellStyle();
        protected static DataGridViewCellStyle _styleKeyValueCenter = new DataGridViewCellStyle();
        protected static DataGridViewCellStyle _styleKeyValueRight = new DataGridViewCellStyle();

        private void SetDataGridViewCellStyle()
        {
            _styleReadOnlyLeft.BackColor = ColorReadOnly;

            _styleReadOnlyCenter.BackColor = ColorReadOnly;
            _styleReadOnlyCenter.Alignment = DataGridViewContentAlignment.MiddleCenter;

            _styleReadOnlyRight.BackColor = ColorReadOnly;
            _styleReadOnlyRight.Alignment = DataGridViewContentAlignment.MiddleRight;

            _styleNormalCenter.BackColor = Color.White;
            _styleNormalCenter.Alignment = DataGridViewContentAlignment.MiddleCenter;

            _styleNormalRight.Alignment = DataGridViewContentAlignment.MiddleRight;

            _styleKeyValueLeft.BackColor = ColorKeyValue;

            _styleKeyValueCenter.BackColor = ColorKeyValue;
            _styleKeyValueCenter.Alignment = DataGridViewContentAlignment.MiddleCenter;

            _styleKeyValueRight.BackColor = ColorKeyValue;
            _styleKeyValueRight.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        #endregion



        #region IProcedure 멤버

        public virtual void SetMenuInfo(MenuInfo menu) { }

        public virtual void procClear() { }

        public virtual void procInsert() { }

        public virtual void procUpdate() { }

        public virtual void procDelete() { }

        public virtual void procSave() { }

        public virtual void procQuery() { }

        public virtual void procPreview() { }

        public virtual void procPrint() { }

        public virtual void procExcel() { }

		protected bool _skipEvent = false;
		protected bool _hasChanges = false;

        public void procClose() 
        {
			if (_hasChanges == true)
			{
				if (MessageBox.Show(Resources.MSG_CLOSE_CONFIRM2, Resources.MSG_CAPTION_CONFIRM, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				{
					_skipEvent = true;
					this.Close();
				}
			}
			else
			{
				if (MessageBox.Show(Resources.MSG_CLOSE_CONFIRM, Resources.MSG_CAPTION_CONFIRM, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				{
					_skipEvent = true;
					this.Close();
				}
			}
        }

        #endregion
    }
}
