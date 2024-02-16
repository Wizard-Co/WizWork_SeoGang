using System;
using System.Windows.Forms;

namespace KR_POP.Common.ControlEX
{
	public class DataGridViewCalendarColumn : DataGridViewColumn
	{
		public DataGridViewCalendarColumn()
			: base(new DataGridViewCalendarCell())
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
				// Ensure that the cell used for the template is a CalendarCell. 
				if (value != null &&
					!value.GetType().IsAssignableFrom(typeof(DataGridViewCalendarCell)))
				{
					throw new InvalidCastException("Must be a CalendarCell");
				}
				base.CellTemplate = value;
			}
		}
	}
}
