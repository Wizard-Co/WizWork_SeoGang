using System;
using System.ComponentModel;
using System.Windows.Forms;


//*******************************************************************************
//프로그램명    DataGridViewTextBoxButtonColumn.cs
//메뉴ID        
//설명          DataGridViewTextBoxButtonColumn
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

    public class CellButtonClickEventArgs : EventArgs
    {
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
    }
    
    /// <summary>
/// Hosts a collection of DataGridViewTextBoxCell cells.
/// </summary>
public class DataGridViewTextBoxButtonColumn : DataGridViewColumn
{
    private bool showBrowseButton;

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Show a button in each cell for browsing for files.")]
    public bool ShowBrowseButton
    {
        get { return showBrowseButton; }
        set
        {

            showBrowseButton = value;
        }
    }

    private bool useButtonClick;
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("OpenFileDialog is dispalyed and on success the contents of the Cell is replaced with the new file path.")]
    public bool UseButtonClick
    {
        get { return useButtonClick; }
        set
        {
            useButtonClick = value;
        }
    }

    public event CellButtonClickEvent CellButtonClick;
    public delegate void CellButtonClickEvent(object sender, CellButtonClickEventArgs e);

    public void OnCellButtonClickEventHandle(CellButtonClickEventArgs e)
    {
        if (CellButtonClick != null) CellButtonClick(this, e);
    }

    public DataGridViewTextBoxButtonColumn()
        : base(new DataGridViewTextBoxButtonCell())
    {
    }
    public override DataGridViewCell CellTemplate
    {
        get
        {
            return base.CellTemplate;
        }
        set
        {
            if (null != value &&
                !value.GetType().IsAssignableFrom(typeof(DataGridViewTextBoxButtonCell)))
            {
                throw new InvalidCastException("must be a DataGridViewTextBoxButtonCell");
            }
            base.CellTemplate = value;
        }
    }
}
}
