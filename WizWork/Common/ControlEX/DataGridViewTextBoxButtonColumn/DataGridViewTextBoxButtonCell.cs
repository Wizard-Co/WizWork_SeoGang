using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using KR_POP.Common.Properties;


//*******************************************************************************
//프로그램명    DataGridViewTextBoxButtonCell.cs
//메뉴ID        
//설명          DataGridViewTextBoxButtonCell
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
/// <summary>
/// Displays editable text information in a DataGridView control. Uses
/// PathEllipsis formatting if the column is smaller than the width of a
/// displayed filesystem path.
/// </summary>
public class DataGridViewTextBoxButtonCell : DataGridViewTextBoxCell
{
    Button _btnBrowse;
    Dictionary<Color, SolidBrush> _dicBrushes = new Dictionary<Color, SolidBrush>();

    protected virtual SolidBrush GetCachedBrush(Color color)
    {
        if (this._dicBrushes.ContainsKey(color))
            return this._dicBrushes[color];
        SolidBrush brush = new SolidBrush(color);
        this._dicBrushes.Add(color, brush);
        return brush;
    }

    private bool _allowShowBrowseButton;

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Show a button in each cell for browsing for files.")]
    public bool ShowBrowseButton
    {
        get { return _allowShowBrowseButton; }
        set
        {

            _allowShowBrowseButton = value;
        }
    }

    private bool _allowUseButtonClick;

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("OpenFileDialog is dispalyed and on success the contents of the Cell is replaced with the new file path.")]
    public bool UseButtonClick
    {
        get { return _allowUseButtonClick; }
        set
        {
            _allowUseButtonClick = value;
        }
    }

    public event CellButtonClickEvent CellButtonClick;
    public delegate void CellButtonClickEvent(object sender, CellButtonClickEventArgs e);

    public void OnCellButtonClickEventHandle(CellButtonClickEventArgs e)
    {
        if (CellButtonClick != null) CellButtonClick(this, e);
    }

    protected override void Dispose(bool disposing)
    {
        RemoveButton();
        base.Dispose(disposing);
    }

    protected virtual Button GetBrowseButton(bool wireOpenFileDialog)
    {
        if (null == _btnBrowse)
        {
            _btnBrowse = new Button();
			_btnBrowse.Text = "…";
			_btnBrowse.Font = new Font("맑은 고딕", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			_btnBrowse.TextAlign = ContentAlignment.MiddleCenter;
			//_btnBrowse.BackgroundImage = Resources.zoom;
			//_btnBrowse.BackgroundImageLayout = ImageLayout.Zoom;
			//_btnBrowse.BackColor = Color.FromArgb(244, 238, 176);
            //_btnBrowse.ForeColor = Color.Black;
			_btnBrowse.UseVisualStyleBackColor = true;
			_btnBrowse.FlatStyle = FlatStyle.Standard;
            _btnBrowse.Click += new EventHandler(browseButton_Click); //yes, really two event handlers!
			//_btnBrowse.FlatAppearance.BorderColor = Color.FromArgb(98, 91, 12);

            //if (wireOpenFileDialog)
            //    browseButton.Click += new EventHandler(delegate(object sender, EventArgs e)
            //    {
            //        if (this.RowIndex >= 0)
            //        {
            //            using (OpenFileDialog ofd = new OpenFileDialog())
            //            {
            //                if (System.IO.File.Exists((string)this.Value))
            //                    ofd.InitialDirectory = System.IO.Path.GetDirectoryName((string)this.Value);
            //                if (ofd.ShowDialog() == DialogResult.OK)
            //                {
            //                    this.Value = ofd.FileName;
            //                }
            //            }
            //        }
            //    });
        }
        return _btnBrowse;
    }
 
    protected virtual bool RightToLeftInternal
    {
        get
        {
            return this.DataGridView.RightToLeft == RightToLeft.Yes;
        }
    }

    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
        if (cellStyle == null)
        {
            throw new ArgumentNullException("cellStyle");
        }
        this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
    }
 
    protected Rectangle PaintPrivate(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
        //System.Diagnostics.Debug.WriteLine(string.Format(“Painting Cell row {0} for rowindex {2} with rectangle {1}”, this.RowIndex, cellBounds, rowIndex));
        SolidBrush cachedBrush;
        Rectangle empty = Rectangle.Empty;
        if (PaintBorder(paintParts))
        {
            this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
        }
        Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
        Rectangle borderedCellRectangle = cellBounds;
        borderedCellRectangle.Offset(rectangle2.X, rectangle2.Y);
        borderedCellRectangle.Width -= rectangle2.Right;
        borderedCellRectangle.Height -= rectangle2.Bottom;
        Point currentCellAddress = base.DataGridView.CurrentCellAddress;
        bool isFirstCell = (currentCellAddress.X == base.ColumnIndex) && (currentCellAddress.Y == rowIndex);
        bool flagisFirstCellAndNotEditing = isFirstCell && (base.DataGridView.EditingControl != null);
        bool thisCellIsSelected = (cellState & DataGridViewElementStates.Selected) != DataGridViewElementStates.None;
        if ((PaintSelectionBackground(paintParts) && thisCellIsSelected) && !flagisFirstCellAndNotEditing)
        {
            cachedBrush = GetCachedBrush(cellStyle.SelectionBackColor);
        }
        else
        {
            cachedBrush = GetCachedBrush(cellStyle.BackColor);
        }
        if (((PaintBackground(paintParts)) && ((cachedBrush.Color.A == 0xff) && (borderedCellRectangle.Width > 0))) && (borderedCellRectangle.Height > 0))
        {
            graphics.FillRectangle(cachedBrush, borderedCellRectangle);
        }
        if (cellStyle.Padding != Padding.Empty)
        {
            if (RightToLeftInternal)
            {
                borderedCellRectangle.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
            }
            else
            {
                borderedCellRectangle.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
            }
            borderedCellRectangle.Width -= cellStyle.Padding.Horizontal;
            borderedCellRectangle.Height -= cellStyle.Padding.Vertical;
        }
        if (((isFirstCell) && (!flagisFirstCellAndNotEditing && PaintFocus(paintParts))) && ((ShowFocusCues && base.DataGridView.Focused) && ((borderedCellRectangle.Width > 0) && (borderedCellRectangle.Height > 0))))
        {
            ControlPaint.DrawFocusRectangle(graphics, borderedCellRectangle, Color.Empty, cachedBrush.Color);
        }
        Rectangle cellValueBounds = borderedCellRectangle;
        string text = formattedValue as string;
        if ((text != null) && (!flagisFirstCellAndNotEditing))
        {
            int y = (cellStyle.WrapMode == DataGridViewTriState.True) ? 1 : 2;
            borderedCellRectangle.Offset(0, y);
            borderedCellRectangle.Width = borderedCellRectangle.Width;
            borderedCellRectangle.Height -= y + 1;
            if ((borderedCellRectangle.Width > 0) && (borderedCellRectangle.Height > 0))
            {
                TextFormatFlags flags = //DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
                    TextFormatFlags.PathEllipsis;
 
                if (PaintContentForeground(paintParts))
                {
                    if ((flags & TextFormatFlags.SingleLine) != TextFormatFlags.GlyphOverhangPadding)
                    {
                        flags |= TextFormatFlags.EndEllipsis;
                    }

                    bool useButtonClick = false;
                    bool showBrowseButton = false;
                    
                    DataGridViewTextBoxButtonColumn textBoxButtonColumn = this.DataGridView.Columns[ColumnIndex] as DataGridViewTextBoxButtonColumn;

                    if (textBoxButtonColumn == null)
                    {
                        useButtonClick = this._allowUseButtonClick;
                        showBrowseButton = this._allowShowBrowseButton;
                    }
                    else
                    {
                        useButtonClick = textBoxButtonColumn.UseButtonClick;
                        showBrowseButton = textBoxButtonColumn.ShowBrowseButton;
                    }

                    Button browseButton = GetBrowseButton(useButtonClick);

                    if (true && showBrowseButton)
                    {
                        if (this.RowIndex >= 0)
                        {
                            bool changed = false;
                            //if ((browseButton.Width != Math.Max(10, borderedCellRectangle.Width / 4)) && (browseButton.Width != 20))
                            //{
                            //    System.Diagnostics.Trace.WriteLine(string.Format("browseButton Width was incorrect:{0} for given rectangle:{1}", browseButton.Width, borderedCellRectangle));
                            //    browseButton.Width = Math.Max(10, borderedCellRectangle.Width / 4);
                            //    browseButton.Width = Math.Min(browseButton.Width, 20);
                            //    changed = true;
                            //}
                            if (browseButton.Height != (borderedCellRectangle.Height))
                            {
                                System.Diagnostics.Trace.WriteLine(string.Format("browseButton Height was incorrect:{0} for given rectangle:{1}", browseButton.Height, borderedCellRectangle));
                                browseButton.Height = borderedCellRectangle.Height;
                                changed = true;
                            }

                            browseButton.Width = browseButton.Height;

                            Point loc = new Point();
                            loc.X = borderedCellRectangle.X + borderedCellRectangle.Width - browseButton.Width;
                            loc.Y = borderedCellRectangle.Y;
                            if (browseButton.Location != loc)
                            {
                                System.Diagnostics.Trace.WriteLine(string.Format("browseButton location was incorrect:{0} for given rectangle:{1} with loc: {2}", browseButton.Location, borderedCellRectangle, loc));
                                browseButton.Location = loc;
                                changed = true;
                            }
                            if (changed)
                                browseButton.Invalidate();
                            if (!this.DataGridView.Controls.Contains(browseButton))
                                this.DataGridView.Controls.Add(browseButton);
                            borderedCellRectangle.Width -= browseButton.Width;
                        }
                    }

                    TextRenderer.DrawText(graphics, text, cellStyle.Font, borderedCellRectangle, thisCellIsSelected ? cellStyle.SelectionForeColor : cellStyle.ForeColor, flags);
                }
 
            }
        }

        if ((base.DataGridView.ShowCellErrors) && PaintErrorIcon(paintParts))
        {
            PaintErrorIcon(graphics, cellStyle, rowIndex, cellBounds, cellValueBounds, errorText);
        }
        return empty;
    }
 
    void browseButton_Click(object sender, EventArgs e)
    {
        this.RaiseCellClick(new DataGridViewCellEventArgs(this.ColumnIndex, this.RowIndex));

        SendButtonClickEvent();
    }

    //protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
    //{
    //    //base.OnKeyDown(e, rowIndex);

    //    if (e.KeyCode == Keys.Enter)
    //    {
    //        e.Handled = true;
    //    }
    //}

    //protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
    //{
    //    //base.OnKeyUp(e, rowIndex);

    //    if (e.KeyCode == Keys.Enter)
    //    {
    //        SendButtonClickEvent();
    //    }
    //}

    protected override void OnMouseDoubleClick(DataGridViewCellMouseEventArgs e)
    {
        SendButtonClickEvent();
        base.OnMouseDoubleClick(e);
    }

    private void SendButtonClickEvent()
    {
        CellButtonClickEventArgs args = new CellButtonClickEventArgs();
        args.RowIndex = this.RowIndex;
        args.ColumnIndex = this.ColumnIndex;


		if (DataGridView == null)
		{
			RemoveButton();
			return;
		}

        DataGridViewTextBoxButtonColumn textBoxButtonColumn = this.DataGridView.Columns[ColumnIndex] as DataGridViewTextBoxButtonColumn;

        if (textBoxButtonColumn == null)
        {
            this.OnCellButtonClickEventHandle(args);
        }
        else
        {
            textBoxButtonColumn.OnCellButtonClickEventHandle(args);
        }
    }

    protected virtual Rectangle ComputeErrorIconBounds(Rectangle cellValueBounds)
    {
        if ((cellValueBounds.Width >= 20) && (cellValueBounds.Height >= 0x13))
        {
            return new Rectangle(RightToLeftInternal ? (cellValueBounds.Left + 4) : ((cellValueBounds.Right - 4) - 12), cellValueBounds.Y + ((cellValueBounds.Height - 11) / 2), 12, 11);
        }
        return Rectangle.Empty;
    }
 
    protected virtual void PaintErrorIcon(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Rectangle cellBounds, Rectangle cellValueBounds, string errorText)
    {
        if ((!string.IsNullOrEmpty(errorText) && (cellValueBounds.Width >= 20)) && (cellValueBounds.Height >= 0x13))
        {
            Rectangle iconBounds = this.GetErrorIconBounds(graphics, cellStyle, rowIndex);
            if ((iconBounds.Width >= 4) && (iconBounds.Height >= 11))
            {
                iconBounds.X += cellBounds.X;
                iconBounds.Y += cellBounds.Y;
                PaintErrorIcon(graphics, iconBounds);
            }
        }
    }
 
    protected static void PaintErrorIcon(Graphics graphics, Rectangle iconBounds)
    {
        Bitmap errorBitmap = new Bitmap(typeof(DataGridViewCell), "DataGridViewRow.error.bmp");
        errorBitmap.MakeTransparent();
        if (errorBitmap != null)
        {
            lock (errorBitmap)
            {
                graphics.DrawImage(errorBitmap, iconBounds, 0, 0, 12, 11, GraphicsUnit.Pixel);
            }
        }
    }
 
    protected static bool PaintErrorIcon(DataGridViewPaintParts paintParts)
    {
        return ((paintParts & DataGridViewPaintParts.ErrorIcon) != DataGridViewPaintParts.None);
    }
    
    protected static bool PaintFocus(DataGridViewPaintParts paintParts)
    {
        return ((paintParts & DataGridViewPaintParts.Focus) != DataGridViewPaintParts.None);
    }
    
    protected static bool PaintBackground(DataGridViewPaintParts paintParts)
    {
        return ((paintParts & DataGridViewPaintParts.Background) != DataGridViewPaintParts.None);
    }
    
    protected static bool PaintBorder(DataGridViewPaintParts paintParts)
    {
        return ((paintParts & DataGridViewPaintParts.Border) != DataGridViewPaintParts.None);
    }
    
    protected static bool PaintContentBackground(DataGridViewPaintParts paintParts)
    {
        return ((paintParts & DataGridViewPaintParts.ContentBackground) != DataGridViewPaintParts.None);
    }
    
    protected static bool PaintContentForeground(DataGridViewPaintParts paintParts)
    {
        return ((paintParts & DataGridViewPaintParts.ContentForeground) != DataGridViewPaintParts.None);
    }
    
    protected static bool PaintSelectionBackground(DataGridViewPaintParts paintParts)
    {
        return ((paintParts & DataGridViewPaintParts.SelectionBackground) != DataGridViewPaintParts.None);
    }
 
    public bool ShowFocusCues
    {
        get { return true; }
    }

    public void RemoveButton()
    {
        if (_btnBrowse != null)
        {
            _btnBrowse.Hide();

            if (this.DataGridView != null && this.DataGridView.Controls.Contains(_btnBrowse) == true)
            {
                this.DataGridView.Controls.Remove(_btnBrowse);
            }

            this._btnBrowse.Dispose();
        }
    }

    public override string  ToString()
    {
        return "DataGridViewTextBoxButtonCell";
    }
 
    protected bool ApplyVisualStylesToHeaders
    {
        get
        {
            if (Application.RenderWithVisualStyles)
            {
                return this.DataGridView.EnableHeadersVisualStyles;
            }
            return false;
        }
    }
 
    public DataGridViewTextBoxButtonCell(): base()
    {

    }

    ~DataGridViewTextBoxButtonCell()
    {
        if (this._btnBrowse != null)
        {
            _btnBrowse.Dispose();
        }
    }
}}
