using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWork
{
    public partial class Frm_tins_Calendar : Form
    {
        string paramDate = "";
        string paramName = "";
        IFormatProvider culture = new System.Globalization.CultureInfo("ko-KR", true);
        public delegate void TextEventHandler(string strDate, string btnName);// string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteDateTextEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다.
        public Frm_tins_Calendar()
        {
            InitializeComponent();
        }

        public Frm_tins_Calendar(string strDate, string strName)
        {
            InitializeComponent();
            this.paramDate = strDate;
            this.paramName = strName;
        }
        
        //-1달로 이동
        private void btnPrev_Click(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(excelCalendar.Value.ToString());
            excelCalendar.Value = dt.AddMonths(-1).ToString();
        }

        //+1달로 이동
        private void btnNext_Click(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(excelCalendar.Value.ToString());
            excelCalendar.Value = dt.AddMonths(1).ToString();
        }

        //클릭시 선택된 년월일 + 현재시간을 폼으로 보낸다.
        private void excelCalendar_ClickEvent(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(excelCalendar.Value.ToString(), culture);
            string date = dt.ToString("yyyyMMdd");// + DateTime.Now.ToString("HHmmss");
            WriteDateTextEvent(date, paramName);
            this.Dispose();
            this.Close();
        }

        //폼 로드 시 달력의 날짜값을 전달받은 값으로 셋팅한다.
        private void Frm_tins_Calendar_Load(object sender, EventArgs e)
        {
            if (paramDate != "" && paramDate.Length == 8)
            {
                DateTime dt = DateTime.ParseExact(paramDate, "yyyyMMdd", culture);
                excelCalendar.Value = dt;
            }
            else if(paramDate != "" && paramDate.Length == 6)
            {
                DateTime dt = DateTime.ParseExact(paramDate, "yyMMdd", culture);
                excelCalendar.Value = dt;
            }
        }
    }
}
