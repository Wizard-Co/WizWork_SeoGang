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
    public partial class Frm_CMNumericKeypad : Form
    {
        public static string KeypadStr = string.Empty;

        public Frm_CMNumericKeypad()
        {
            InitializeComponent();
        }
        public Frm_CMNumericKeypad(string Value, string Name)
        {
            InitializeComponent();
            this.Text = Name + " 입력";
            this.tbInputText.Text = Value;
            this.button1.Text = Name;
            this.tbInputText.SelectAll();
        }
        //intType == 0 이면 Time값 그러므로 Length는 최대 6자리
        public Frm_CMNumericKeypad(string Value, string Name, int intType)
        {
            InitializeComponent();
            if (intType == 0)
            {
                tbInputText.MaxLength = 4;//HHmm
            }
            this.Text = Name + " 입력";
            this.tbInputText.Text = Value;
            this.button1.Text = Name;
            this.tbInputText.SelectAll();
        }
        /// <summary>
        /// 키패드 키 입력 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InputKey(object sender, EventArgs e)
        {
            if (tbInputText.SelectedText.Length > 0)
            {
                tbInputText.Text = tbInputText.Text.Remove(tbInputText.SelectionStart, tbInputText.SelectionLength);
            }

            KeypadStr = tbInputText.Text;
            
            if (((Button)sender) == btnKeyClear) // 모두 지우기
            {
                KeypadStr = "";
            }
            else if (((Button)sender) == btnKeyBackSpace) // 지우기
            {
                KeypadStr = BackKeyProc(KeypadStr);
            }
            else if (((Button)sender) == btnKeyClose) // 취소
            {
                this.Close();
            }
            else if (((Button)sender) == btnKeyInput) // 확인
            {
                
                KeypadStr = tbInputText.Text;
                DialogResult = DialogResult.OK;


                //시간입력용 키패드일때
                if (tbInputText.MaxLength == 4)
                {
                    for (int i = KeypadStr.Length; i == 4; i++)
                    {
                        KeypadStr = KeypadStr + "0";
                    }
                    int HH = 0;
                    int mm = 0;
                    HH = int.Parse(KeypadStr.Substring(0, 2));
                    mm = int.Parse(KeypadStr.Substring(2, 2));

                    if (HH > 23 || mm > 59)
                    {
                        KeypadStr = "0000";
                    }
                }


                tbInputText.Text = KeypadStr;

                this.Close();
                
            }
            else // 문자버튼
            {
                KeypadStr += ((Button)sender).Text;
            }

            tbInputText.Text = KeypadStr; // 입력된 내용 보여주기
        }

        /// <summary>
        /// 엔터키 입력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInputText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// 폼 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_Qlt_NumericKeypad_Load(object sender, EventArgs e)
        {
            //Text = g_Name + " 입력";
            //button1.Text = g_Name;
 
            //if (g_Name == "시작시간" || g_Name == "종료시간")
            //{
            //    tbInputText.MaxLength = 6;
            //    //if (KeypadStr.Length < 6)
            //    //{
            //    //    for (int i = KeypadStr.Length; i == 6; i++)
            //    //    {
            //    //        KeypadStr = KeypadStr + "0";
            //    //    }
            //    //}
            //    //Console.WriteLine(KeypadStr+"///////////////////");
            //    tbInputText.Text = KeypadStr;
            //}
            
            //else
            //{
            //    tbInputText.Text = KeypadStr;
            //}
            
            this.Activate();
            tbInputText.Focus();
            tbInputText.SelectAll();
            
        }

        private string BackKeyProc(string text)
        {
 
        if (KeypadStr.Length <= 0){    return text;    }
            
        text = KeypadStr.Substring(0, KeypadStr.Length - 1); 

        return text;
        }

        private void Frm_CMNumericKeypad_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 버튼으로 종료시킨 경우가 아닐 때
            //if (!blnBtnClosing)
            //{
            //    if (WizCommonPopup.MyMessageBox.ShowBox("종료하시겠습니까?", "[종료]", 0, 0) == DialogResult.OK)
            //    {
            //        e.Cancel = true;
            //    }

            //    //if (System.Windows.Forms.MessageBox.Show(“종료하시겠습니까 ?”, “확인 취소”, MessageBoxButtons.YesNo) == DialogResult.No)
            //    //{
            //    //    e.Cancel = true;
            //    //}
            //}
        }
    }
}
