using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

//*******************************************************************************
//프로그램명    frm_PopUp_ChoiceLabelPrintType.cs
//메뉴ID        
//설명          frm_PopUp_ChoiceLabelPrintType 메인소스입니다.
//작성일        2020.03.05
//개발자        허윤구
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************

//*******************************************************************************

namespace WizWork
{
    public partial class frm_PopUp_ChoiceLabelPrintType : Form
    {

        string[] Message = new string[2];  // 메시지박스 처리용도.

        public delegate void TextEventHandler(string Message, double Qty, double PrintCount, double RemainOneMoreQty);    // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다.

        double D_AllQty = 0;
        double D_StandardQty = 0;
        double D_QuoQty = 0;
        double D_RemainQty = 0;

        bool HeaderMessage = false;

        WizWorkLib Lib = new WizWorkLib();

        public frm_PopUp_ChoiceLabelPrintType()
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
        }

        public frm_PopUp_ChoiceLabelPrintType(double AllQty , double StandardQty, bool HeaderAddMessage)
        {
            InitializeComponent();
            SetScreen();  //TLP 사이즈 조정
            D_AllQty = AllQty;
            D_StandardQty = StandardQty;
            HeaderMessage = HeaderAddMessage;
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

        // 로드 이벤트.
        private void frm_PopUp_ChoiceLabelPrintType_Load(object sender, EventArgs e)
        {

            if (HeaderMessage == true)
            {
                lblHeader.Text = lblHeader.Text + "(전표 미발행)";
            }


            // 각 세 버튼에 대한 TEXT 를 정해야 하기 때문에ㅡ.,
            // 가져온 AllQty랑, StandardQty로 몫이랑 나머지 값을 구하자.

            if (D_AllQty > D_StandardQty)
            {
                double C = D_AllQty / D_StandardQty;
                double QuoQty = System.Math.Truncate(C);        // 몫
                double RemainQty = D_AllQty % D_StandardQty;    // 나머지

                D_QuoQty = QuoQty;
                D_RemainQty = RemainQty;


                btnNC.Text = D_AllQty.ToString() + "EA로 인쇄된 \r\n" +
                    "라벨 1장이 나옵니다";
                btnYO.Text = D_StandardQty.ToString() + "EA로 인쇄된 \r\n" +
                    "라벨 " + QuoQty.ToString() + "장이 나옵니다";

                if (RemainQty == 0)
                {
                    btnYC.Text = D_StandardQty.ToString() + "EA로 인쇄된 \r\n" +
                    "라벨 " + QuoQty.ToString() + "장이 나옵니다";
                }
                else
                {
                    btnYC.Text = D_StandardQty.ToString() + "EA, " + RemainQty.ToString() + "EA로 인쇄된 \r\n" +
                    "라벨 " + (QuoQty + 1).ToString() + "장이 나옵니다";
                }                
            }
            else if (D_AllQty == D_StandardQty)
            {
                btnNC.Text = D_AllQty.ToString() + "EA로 인쇄된 \r\n" +
                    "라벨 1장이 나옵니다";
                btnYO.Text = D_AllQty.ToString() + "EA로 인쇄된 \r\n" +
                    "라벨 1장이 나옵니다";
                btnYC.Text = D_AllQty.ToString() + "EA로 인쇄된 \r\n" +
                    "라벨 1장이 나옵니다";
            }
            else if (D_AllQty < D_StandardQty)
            {
                btnNC.Text = D_AllQty.ToString() + "EA로 인쇄된 \r\n" +
                    "라벨 1장이 나옵니다";
                btnYO.Text = D_AllQty.ToString() + "EA로 실적등록, \r\n" +
                    "라벨은 발행되지 않습니다";
                btnYC.Text = D_AllQty.ToString() + "EA로 인쇄된 \r\n" +
                    "라벨 1장이 나옵니다";
            }
        }



        // 취소버튼 클릭.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            WriteTextEvent("Cancel", 0, 0, 0);
            this.Close();
        }



        // NC_버튼 클릭 이벤트.
        private void btnNC_Click(object sender, EventArgs e)
        {
            if (D_AllQty > D_StandardQty)
            {
                // D_AllQty 로 1장발행.
                WriteTextEvent("NC", D_AllQty, 1, D_RemainQty);
                this.Close();
            }
            else if (D_AllQty == D_StandardQty)
            {
                // D_AllQty 로 1장발행.
                WriteTextEvent("NC", D_AllQty, 1, D_RemainQty);
                this.Close();
            }
            else if (D_AllQty < D_StandardQty)
            {
                // D_AllQty 로 1장발행.
                WriteTextEvent("NC", D_AllQty, 1, D_RemainQty);
                this.Close();
            }
        }


        // YO_버튼 클릭 이벤트.
        private void btnYO_Click(object sender, EventArgs e)
        {
            if (D_AllQty > D_StandardQty)
            {
                // 기준수치 값으로 나누기 몫의 수량만큼 라벨발행.  +  D_RemainQty값 만큼 Split Table ADD. (0보다 크다면)
                WriteTextEvent("YO", D_StandardQty, D_QuoQty, D_RemainQty);
                this.Close();
            }
            else if (D_AllQty == D_StandardQty)
            {
                // D_AllQty 로 1장발행.
                WriteTextEvent("YO", D_AllQty, 1, D_RemainQty);
                this.Close();
            }
            else if (D_AllQty < D_StandardQty)
            {
                // D_AllQty 로 실적만 저장.  무 발행. + Split Table ADD
                WriteTextEvent("YO", D_AllQty, 0, D_RemainQty);
                this.Close();
            }
        }


        // YC_버튼 클릭 이벤트.
        private void btnYC_Click(object sender, EventArgs e)
        {
            if (D_AllQty > D_StandardQty)
            {                
                // 기준수치 값으로 몫 수만큼 라벨발행
                //  +) 나머지 값으로 1장 발행. (0보다 크다면)
                WriteTextEvent("YC", D_StandardQty, D_QuoQty, D_RemainQty);
                this.Close();                
            }
            else if (D_AllQty == D_StandardQty)
            {
                // D_AllQty 로 1장발행.
                WriteTextEvent("YC", D_AllQty, 1, D_RemainQty);
                this.Close();
            }
            else if (D_AllQty < D_StandardQty)
            {
                // D_AllQty 로 1장발행.
                WriteTextEvent("YC", D_AllQty, 1, D_RemainQty);
                this.Close();
            }
        }
    }
}
