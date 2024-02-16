using System.ComponentModel;
using System.Windows.Forms;


//*******************************************************************************
//프로그램명    CellPresenter.cs
//메뉴ID        
//설명          CellPresenter
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
    public class CellPresenter
    {
        #region Properties

        [Browsable(false)]
        public DataGridViewCell Cell { get; private set; }

        public bool ReadOnly
        {
            get { return Cell.ReadOnly; }
            set { Cell.ReadOnly = value; }
        }

        public string Value
        {
            get { return (string)Cell.Value; }
            set { Cell.Value = value; }
        }

        public int ColumnSpan
        {
            get
            {
                var cell = Cell as DataGridViewTextBoxCellEx;
                if (cell != null)
                    return cell.ColumnSpan;

                var cell1 = Cell as DataGridViewCheckBoxCellEx;
                if (cell1 != null)
                    return cell1.ColumnSpan;

                var cell2 = Cell as DataGridViewImageCellEx;
                if (cell2 != null)
                    return cell2.ColumnSpan;

                var cell3 = Cell as DataGridViewComboBoxCellEx;
                if (cell3 != null)
                    return cell3.ColumnSpan;

                return 1;
            }
            set
            {
                var cell = Cell as DataGridViewTextBoxCellEx;
                if (cell != null)
                    cell.ColumnSpan = value;

                var cell2 = Cell as DataGridViewImageCellEx;
                if (cell2 != null)
                    cell2.ColumnSpan = value;

            }
        }

        public int RowSpan
        {
            get
            {
                var cell = Cell as DataGridViewTextBoxCellEx;
                if (cell != null)
                    return cell.RowSpan;
                var cell2 = Cell as DataGridViewImageCellEx;
                if (cell2 != null)
                    return cell2.RowSpan;

                return 1;
            }
            set
            {
                var cell = Cell as DataGridViewTextBoxCellEx;
                if (cell != null)
                    cell.RowSpan = value;
                var cell2 = Cell as DataGridViewImageCellEx;
                if (cell2 != null)
                    cell2.RowSpan = value;
            }
        }

        public bool ColumnFrozen
        {
            get { return Cell.OwningColumn.Frozen; }
            set { Cell.OwningColumn.Frozen = value; }
        }

        public bool RowFrozen
        {
            get { return Cell.OwningRow.Frozen; }
            set { Cell.OwningRow.Frozen = value; }
        }

        public DataGridViewCellStyle CellStyle
        {
            get { return Cell.Style; }
            set { Cell.Style = value; }
        }

        public int ColumnDividerWidth
        {
            get { return Cell.OwningColumn.DividerWidth; }
            set { Cell.OwningColumn.DividerWidth = value; }
        }

        public int RowDividerHeight
        {
            get { return Cell.OwningRow.DividerHeight; }
            set { Cell.OwningRow.DividerHeight = value; }
        }

        #endregion

        #region ctor

        public CellPresenter(DataGridViewCell cell)
        {
            Cell = cell;
        }

        #endregion
    }
}
