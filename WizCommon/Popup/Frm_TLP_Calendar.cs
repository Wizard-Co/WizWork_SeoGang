using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizCommon.Popup
{
    public partial class Frm_TLP_Calendar : Form
    {
        string paramDate = "";
        string paramName = "";
        WizWorkLib lib = new WizWorkLib();
        public delegate void TextEventHandler(string strDate, string btnName);// string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteDateTextEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다.

        public Frm_TLP_Calendar()
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
        }
        public Frm_TLP_Calendar(string strDate, string strName)
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정

            this.paramDate = strDate;
            this.paramName = strName;
        }




        #region 테이블 레이아웃 패널 사이즈 조정
        private void SetScreen()
        {
            tlpMain.Dock = DockStyle.Fill;
            foreach (Control control in tlpMain.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(1, 1, 1, 1);
                foreach (Control contro in control.Controls)
                {
                    contro.Dock = DockStyle.Fill;
                    contro.Margin = new Padding(1, 1, 1, 1);
                    foreach (Control contr in contro.Controls)
                    {
                        contr.Dock = DockStyle.Fill;
                        contr.Margin = new Padding(1, 1, 1, 1);
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(1, 1, 1, 1);
                            foreach (Control con in cont.Controls)
                            {
                                con.Dock = DockStyle.Fill;
                                con.Margin = new Padding(1, 1, 1, 1);
                                foreach (Control co in con.Controls)
                                {
                                    co.Dock = DockStyle.Fill;
                                    co.Margin = new Padding(1, 1, 1, 1);
                                }
                            }
                        }
                    }
                }
            }
        }





        #endregion


        // 로드 시.
        private void Frm_TLP_Calendar_Load(object sender, EventArgs e)
        {
            if (paramDate != "")
            {
                DateTime dt = DateTime.ParseExact(paramDate, "yyyyMMdd", null);

                string yyyy = dt.Year.ToString();
                string MM = dt.Month.ToString();
                string dd = dt.Day.ToString();

                if (MM.Length == 1) { MM = "0" + MM; }

                // 2. yyyy/mm을 상단 라벨에게 제공
                lblYYYYMM.Text = yyyy + "년" + " " + MM + "월";

                // 3. TLP_FILL 작업
                FillTlp(yyyy, MM);

                //4. 전달받은 그 값 색칠하기.
                foreach (Button bb in tlpFill.Controls)
                {
                    if (bb.Text == dd)
                    {
                        bb.BackColor = Color.DarkGray;
                    }
                }
            }
            else
            {
                DateTime dt = DateTime.Now;
                string yyyy = dt.Year.ToString();
                string MM = dt.Month.ToString();
                string dd = dt.Day.ToString();

                if (MM.Length == 1) { MM = "0" + MM; }

                // 2. yyyy/mm을 상단 라벨에게 제공
                lblYYYYMM.Text = yyyy + "년" + " " + MM + "월";

                // 3. TLP_FILL 작업
                FillTlp(yyyy, MM);

                //4. 전달받은 그 값 색칠하기.
                foreach (Button bb in tlpFill.Controls)
                {
                    if (bb.Text == dd)
                    {
                        bb.BackColor = Color.DarkGray;
                    }
                }
            }
        }


        #region 달력 메인숫자 채우기
        private void FillTlp(string YYYY, string MM)
        {
            // 1. YYYY-MM이 구해졌어. 구해진 YYYY-MM의 1일은 무슨요일이지?
            string FindDDDD = YYYY + " " + MM + " " + "01";
            DateTime FindDDD = Convert.ToDateTime(FindDDDD);    // 이번달 1일.
            string WeekDay = FindDDD.ToString("dddd");          //시작요일

            // 2. YYYY-MM은 몇일까지 있지?
            DateTime PlusOneMM = FindDDD.AddMonths(1);  // 이게 다음달 1일.
            DateTime dLastDayOfThisMonth = PlusOneMM.AddDays(-1); // 이게 지난 달 말일
            string LastNumber = dLastDayOfThisMonth.ToString("dd");

            // 3. 시작요일부터 순서대로 달력숫자 채우기.
            FillTlp_btnNumber(WeekDay, LastNumber);
        }

    
        // 시작요일부터 마지막 날짜까지 순서대로 달력숫자 채우기. 펑션
        private void FillTlp_btnNumber(string FirstDay, string LastNum)
        {
            tlpFill.Controls.Clear();
            HeadButtonSetting();
           
            int Number1 = 1;
            int LastNumber = Convert.ToInt32(LastNum);

            int column = 0;     // 0(일요일) ~ 6(토요일)
            int row = 1;        // 1(첫주) ~ 6(여섯주)


            switch (FirstDay)
            {
                case "일요일":
                    column = 0;
                    break;
                case "월요일":
                    column = 1;
                    break;
                case "화요일":
                    column = 2;
                    break;
                case "수요일":
                    column = 3;
                    break;
                case "목요일":
                    column = 4;
                    break;
                case "금요일":
                    column = 5;
                    break;
                case "토요일":
                    column = 6;
                    break;
            }
            for (int i = 0; i < LastNumber; i++)
            {
                Button BTN = new Button();
                BTN.Click += new System.EventHandler(this.Button_InsideTlp_Click);
                BTN.Dock = DockStyle.Fill;
                BTN.Font = new Font("굴림", 16, FontStyle.Bold);
                BTN.Text = Number1.ToString();

                // 위치에 따라 글자색깔 조정.
                if (column == 0)    //일요일
                {
                    BTN.ForeColor = Color.Red;
                }
                else if (column == 6)   // 토요일
                {
                    BTN.ForeColor = Color.Blue;
                }
                else
                {
                    BTN.ForeColor = Color.Black;
                }
                tlpFill.Controls.Add(BTN, column, row);


                //(1) 마지막 숫자까지 숫자 더하기 1
                if (Number1 < LastNumber)
                {
                    Number1 = Number1 + 1;
                }
                else { break; }
               
                //(2)다음 버튼 놓을 위치조정
                if (column == 6)
                {
                    column = 0;
                    row = row + 1;
                }
                else
                {
                    column = column + 1;
                }
            }
        }

        // 요일 헤더 생성.
        private void HeadButtonSetting()
        {
            Button btn_sun = new Button();
            btn_sun.Dock = DockStyle.Fill;
            btn_sun.Font = new Font("굴림", 12);
            btn_sun.ForeColor = Color.Red;
            btn_sun.Text = "일";
            tlpFill.Controls.Add(btn_sun, 0, 0);

            Button btn_mon = new Button();
            btn_mon.Dock = DockStyle.Fill;
            btn_mon.Font = new Font("굴림", 12);
            btn_mon.ForeColor = Color.Black;
            btn_mon.Text = "월";
            tlpFill.Controls.Add(btn_mon, 1, 0);

            Button btn_tue = new Button();
            btn_tue.Dock = DockStyle.Fill;
            btn_tue.Font = new Font("굴림", 12);
            btn_tue.ForeColor = Color.Black;
            btn_tue.Text = "화";
            tlpFill.Controls.Add(btn_tue, 2, 0);

            Button btn_wed = new Button();
            btn_wed.Dock = DockStyle.Fill;
            btn_wed.Font = new Font("굴림", 12);
            btn_wed.ForeColor = Color.Black;
            btn_wed.Text = "수";
            tlpFill.Controls.Add(btn_wed, 3, 0);

            Button btn_thu = new Button();
            btn_thu.Dock = DockStyle.Fill;
            btn_thu.Font = new Font("굴림", 12);
            btn_thu.ForeColor = Color.Black;
            btn_thu.Text = "목";
            tlpFill.Controls.Add(btn_thu, 4, 0);

            Button btn_fri = new Button();
            btn_fri.Dock = DockStyle.Fill;
            btn_fri.Font = new Font("굴림", 12);
            btn_fri.ForeColor = Color.Black;
            btn_fri.Text = "금";
            tlpFill.Controls.Add(btn_fri, 5, 0);

            Button btn_sat = new Button();
            btn_sat.Dock = DockStyle.Fill;
            btn_sat.Font = new Font("굴림", 12);
            btn_sat.ForeColor = Color.Blue;
            btn_sat.Text = "토";
            tlpFill.Controls.Add(btn_sat, 6, 0);
        }

        #endregion


        #region 년/월 이동 4버튼.

        // 이전 달 버튼클릭.
        private void btnPreviousMonth_Click(object sender, EventArgs e)
        {
            string yyyy = lblYYYYMM.Text.Substring(0, 4);
            string MM = lblYYYYMM.Text.Substring(6, 2);

            int iyyyy = Convert.ToInt32(yyyy);
            int imm = Convert.ToInt32(MM);
            if (imm == 1)
            {
                iyyyy = iyyyy - 1;
                imm = 12;
            }
            else
            {
                imm = imm - 1;
            }

            yyyy = iyyyy.ToString();
            MM = imm.ToString();
            if (MM.Length == 1) { MM = "0" + MM; }

            lblYYYYMM.Text = yyyy + "년" + " " + MM + "월";
            FillTlp(yyyy, MM);
        }

        // 전 년 버튼 클릭.
        private void btnPreviousYear_Click(object sender, EventArgs e)
        {
            string yyyy = lblYYYYMM.Text.Substring(0, 4);
            string MM = lblYYYYMM.Text.Substring(6, 2);

            int iyyyy = Convert.ToInt32(yyyy);
            int imm = Convert.ToInt32(MM);

            iyyyy = iyyyy - 1;

            yyyy = iyyyy.ToString();
            MM = imm.ToString();
            if (MM.Length == 1) { MM = "0" + MM; }

            lblYYYYMM.Text = yyyy + "년" + " " + MM + "월";
            FillTlp(yyyy, MM);
        }

        // 내 년 버튼 클릭.
        private void btnAfterYear_Click(object sender, EventArgs e)
        {
            string yyyy = lblYYYYMM.Text.Substring(0, 4);
            string MM = lblYYYYMM.Text.Substring(6, 2);

            int iyyyy = Convert.ToInt32(yyyy);
            int imm = Convert.ToInt32(MM);

            iyyyy = iyyyy + 1;

            yyyy = iyyyy.ToString();
            MM = imm.ToString();
            if (MM.Length == 1) { MM = "0" + MM; }

            lblYYYYMM.Text = yyyy + "년" + " " + MM + "월";
            FillTlp(yyyy, MM);
        }

        // 다음 달 버튼클릭.
        private void btnAfterMonth_Click(object sender, EventArgs e)
        {
            string yyyy = lblYYYYMM.Text.Substring(0, 4);
            string MM = lblYYYYMM.Text.Substring(6, 2);

            int iyyyy = Convert.ToInt32(yyyy);
            int imm = Convert.ToInt32(MM);
            if (imm == 12)
            {
                iyyyy = iyyyy + 1;
                imm = 1;
            }
            else
            {
                imm = imm + 1;
            }

            yyyy = iyyyy.ToString();
            MM = imm.ToString();
            if (MM.Length == 1) { MM = "0" + MM; }

            lblYYYYMM.Text = yyyy + "년" + " " + MM + "월";
            FillTlp(yyyy, MM);
        }

        #endregion


        #region 달력날짜 클릭
        // 달력공간 클릭 이벤트.
        private void Button_InsideTlp_Click(object sender, EventArgs e)
        {
            //1. sender가 꼭 버튼이어야 해.
            if (sender is Button)
            {
                string yyyy = lblYYYYMM.Text.Substring(0, 4);
                string MM = lblYYYYMM.Text.Substring(6, 2);
                //2. button text value가 숫자이어야 겠지??
                string val = ((Button)sender).Text;     // 이게 dd인 셈이지.
                if (val.Length == 1) { val = "0" + val; }

                string date = yyyy + MM + val; //+ DateTime.Now.ToString("HHmmss");
                WriteDateTextEvent(date, paramName);
                this.Close();
            }
            else
            {
                return;
            }           
        }

        #endregion


    }
}
