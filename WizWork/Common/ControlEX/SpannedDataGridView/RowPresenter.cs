using System.Windows.Forms;


//*******************************************************************************
//프로그램명    RowPresenter.cs
//메뉴ID        
//설명          RowPresenter
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
    public class RowPresenter
    {
        #region Properties

        DataGridViewRow m_Row;

        public bool Visible
        {
            get { return m_Row.Visible; }
            set { m_Row.Visible = value; }
        }

        public bool Frozen
        {
            get { return m_Row.Frozen; }
            set { m_Row.Frozen = value; }
        }

        public int Index
        {
            get { return m_Row.Index; }
        }

        #endregion

        #region ctor

        public RowPresenter(DataGridViewRow row)
        {
            m_Row = row;
        }

        #endregion
    }
}
