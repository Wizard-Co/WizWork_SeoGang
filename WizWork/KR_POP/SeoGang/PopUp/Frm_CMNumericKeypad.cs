using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWork.POPUP
{
    public partial class Frm_CMNumericKeypad : Form
    {
        public static string KeypadStr = string.Empty;
        public static string g_Name = string.Empty;
        bool blnBtnClosing = true;
        Lib.MES_WTS_Lib WTS_Lib = new Lib.MES_WTS_Lib();

        public string InputTextValue { get { return this.tbInputText.Text; } }

        public Frm_CMNumericKeypad()
        {
            InitializeComponent();

        }


        public Frm_CMNumericKeypad(string strTitle, string strValue)
        {
            InitializeComponent();

             this.Text =  strTitle;
             this.btnTitle.Text = strTitle;
             this.tbInputText.Text = strValue;
             KeypadStr = "";
        }

        public Frm_CMNumericKeypad(string strTitle)
        {
            InitializeComponent();

            this.Text = strTitle + " 입력";
            this.btnTitle.Text = strTitle;
            this.tbInputText.Text = "";
            KeypadStr = "";
        }

        /// <summary>
        /// 키패드 키 입력 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InputKey(object sender, EventArgs e)
        {
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
                if (SetChekValue("", "CANCEL") == true)
                {
                    blnBtnClosing = true;
                    DialogResult = DialogResult.No;
                    this.Close();
                }
                else
                {
                    blnBtnClosing = true;
                    DialogResult = DialogResult.No;
                    this.Close();
                }
            }
            else if (((Button)sender) == btnKeyInput) // 확인
            {
                KeypadStr = tbInputText.Text;

                if (SetChekValue(KeypadStr, "OK") == true)
                {
                    if (KeypadStr == "")
                    {
                        blnBtnClosing = false;
                    }
                    else
                    {
                        blnBtnClosing = true;
                    }
                    DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                else
                {

                    return;
                }
            }
            else // 문자버튼
            {
                // . 을 한번 입력한 상태라면 다시 입력 못하도록 막음 2020.10.22
                if (((Button)sender).Text.Equals(".") == true
                    && KeypadStr.IndexOf('.') >= 0)
                {
                    return;
                }

                KeypadStr += ((Button)sender).Text;
            }

            tbInputText.Text = KeypadStr; // 입력된 내용 보여주기
            blnBtnClosing = true;


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
                if (SetChekValue(tbInputText.Text, "OK") == true)
                {
                    blnBtnClosing = true;
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 폼 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_Qlt_NumericKeypad_Load(object sender, EventArgs e)
        {
            if (this.Text == "")
            {
                Text = g_Name + " 입력";
            }
            if (btnTitle.Text == "")
            {
                btnTitle.Text = g_Name ;
                this.btnTitle.ForeColor = System.Drawing.Color.White;
            }
            if (g_Name == "시작시간" || g_Name == "종료시간" || btnTitle.Text == "시작시간" || btnTitle.Text == "종료시간")
            {
                tbInputText.MaxLength = 6;
                tbInputText.Text = KeypadStr;
            }   
            else
            {
                tbInputText.Text = KeypadStr;
            }
            if (!(this.Owner is null))
            {
                if (this.Owner.Name == "frm_tins_InspectAuto_U")
                {
                    blnBtnClosing = true;
                }
            }
            
            
        }

        private string BackKeyProc(string text)
        {
 
        if (KeypadStr.Length <= 0){    return text;    }
            
        text = KeypadStr.Substring(0, KeypadStr.Length - 1); 

        return text;
        }

        //void PW()
        //{
        //    if (KeypadStr != "0000".ToString())
        //    {
        //        MessageBox.Show("암호를 정확히 입력하십시오.");
        //    }
        //}
        
        

        private void SetTime()
        {
            string strTime = tbInputText.Text;
            strTime = strTime.PadRight(6, '0');
            //for (int i = strTime.Length; i < 6; i++)//입력받은 시작시간의 길이가 6자리(HHmmss)가 아닐때 나머지 자리수는 0으로 채운다.
            //{
            //    strTime = strTime + "0";
            //}
            if (int.Parse(strTime.Substring(0, 2)) > 23)
            {
                strTime = "00" + strTime.Substring(2);
            }
            if (int.Parse(strTime.Substring(2, 2)) > 59)
            {
                strTime = strTime.Substring(0, 2) + "00" + strTime.Substring(4, 2);
            }
            if (int.Parse(strTime.Substring(4, 2)) > 59)
            {
                strTime = strTime.Substring(0, 4) + "00";
            }
            tbInputText.Text = strTime;
        }

        private bool SetChekValue(string strChkValue, string strKobType)
        {
            bool blResult = true;
            try
            {
                if (Owner != null
                    && Owner.Name != null)
                {
                    if (Owner.Name == "frm_tins_InspectAuto_U")
                    {
                        if (strKobType == "OK")
                        {
                            ((WizWork.frm_tins_InspectAuto_U)(this.Owner)).SetCheckValue(strChkValue);
                        }
                        else
                        {
                            ((WizWork.frm_tins_InspectAuto_U)(this.Owner)).SetCheckValueCancel(strChkValue);
                        }

                    }
                    if (this.Owner.Name.ToString() == "Frm_tprc_PlanInput_Q")
                    {
                        if (strKobType == "OK")
                        {

                            if (this.btnTitle.Text == "관리번호")
                            {
                                if (WTS_Lib.GetCheckstrOrderID(this.tbInputText.Text) == false)
                                {

                                    MessageBox.Show("잘못된 관리번호입니다", "입력번호", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.tbInputText.Text = "";

                                    blResult = false;


                                }
                            }
                        }
                    }
                }
                if (this.btnTitle.Text.Contains("시작시간") || this.btnTitle.Text.Contains("종료시간"))
                {
                    SetTime();
                }

            }
            catch(Exception e)
            {
                Console.Write(e.Message);
                blResult = true;
            }
            return blResult;
        }

        private void Frm_CMNumericKeypad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(this.Owner is null))
            {
                if (this.Owner.Name == "frm_tins_InspectAuto_U")
                {
                    if (!blnBtnClosing)
                    {
                        if (WizCommon.Popup.MyMessageBox.ShowBox("종료하시겠습니까? OK를 누르시면 측정값이 0으로 입력됩니다.", "[종료]", 0, 0) == DialogResult.Yes)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            ((WizWork.frm_tins_InspectAuto_U)(this.Owner)).SetCheckValue("0");
                        }
                    }
                }
            }
            //if (!blnBtnClosing)
            //{
            //    if (WizCommon.Popup.MyMessageBox.ShowBox("종료하시겠습니까? OK를 누르시면 측정값이 0으로 입력됩니다.", "[종료]", 0, 0) == DialogResult.Yes)
            //    {
            //        e.Cancel = true;
            //    }
            //    else
            //    {
            //        ((WizWork.frm_tins_InspectAuto_U)(this.Owner)).SetCheckValue("0");
            //    }
            //}
        }

        private void tbInputText_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = (int)e.KeyChar;  // 46 : Point  
            //숫자만 입력되도록 필터링
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)) && !(keyCode == 46))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
            else if (keyCode == 46)
            {
                if (string.IsNullOrEmpty(tbInputText.Text) || tbInputText.Text.Contains('.') == true)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
