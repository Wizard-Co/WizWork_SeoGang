using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.InteropServices;   // DllImport

namespace Common.Popup
{
    public partial class Frm_CMKeypad : Form
    {
        public static string KeypadStr = string.Empty;
        public static string g_Name = string.Empty;

        public string InputTextValue { get { return this.tbInputText.Text; } }
        /// <summary>
        /// 생성
        /// </summary>
        public Frm_CMKeypad()
        {
            InitializeComponent();
            //cboSelectInsType.SelectedIndex = 0;
            //this.Text = g_Char + " 입력";
        }

        public Frm_CMKeypad(string Value, string Name)
        {
            InitializeComponent();
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
            KeypadStr = tbInputText.Text;

            if (((Button)sender) == btnKeyBackSpace) // 스페이스바
            {
                KeypadStr = (KeypadStr.Length > 0) ? KeypadStr.Substring(0, KeypadStr.Length - 1) : "";
            }
            else if (((Button)sender) == btnKeyClear) // 모두 지우기
            {
                KeypadStr = "";
            }
            else if (((Button)sender) == btnKeyClose) // 취소
            {
                this.Close();
            }
            else if (((Button)sender) == btnKeyInput) // 확인
            {
                KeypadStr = tbInputText.Text;

                DialogResult = DialogResult.OK;
                this.Close();
            }
            else // 문자버튼
            {
                KeypadStr += ((Button)sender).Text;
            }

            tbInputText.Text = KeypadStr; // 입력된 내용 보여주기
            tbInputText.Focus();
            tbInputText.SelectAll();
               
        }

        /// <summary>
        /// 폼 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_Qlt_Keypad_Load(object sender, EventArgs e)
        {
            //Text = g_Name + " 입력";
            //button1.Text = g_Name;
            button1.ForeColor = System.Drawing.Color.White;

            //tbInputText.Text = KeypadStr; // 키패드에 저장되어 있던 텍스트 보여주기
            tbInputText.Focus();
            tbInputText.SelectAll();
            //cboSelectInsType.SelectedValue = -1;
        }

//         private void cbSelectOption_SelectedIndexChanged(object sender, EventArgs e)
//         {
// //             if (cboSelectInsType.Text == "정성")
// //             {
// //                 ConvertKey(1);
// //             }
// //             else
// //             {
// //                 ConvertKey(2);
// //             }
//         }

        /// <summary>
        /// 미사용. 한글 키패드.
        /// </summary>
        /// <param name="InsType"></param>
        void ConvertKey(int InsType)
        {
            if (InsType == 1)
            {
                if (btnKeyShift.BackColor == Color.White)
                {
                    btnKeyQ.Text = "ㅃ"; btnKeyW.Text = "ㅉ"; btnKeyE.Text = "ㄸ"; btnKeyR.Text = "ㄲ"; btnKeyT.Text = "ㅆ";
                    btnKeyO.Text = "ㅒ"; btnKeyP.Text = "ㅖ";
                }
                else
                {
                    btnKeyQ.Text = "ㅂ"; btnKeyW.Text = "ㅈ"; btnKeyE.Text = "ㄷ"; btnKeyR.Text = "ㄱ"; btnKeyT.Text = "ㅅ";
                    btnKeyY.Text = "ㅛ"; btnKeyU.Text = "ㅕ"; btnKeyI.Text = "ㅑ"; btnKeyO.Text = "ㅐ"; btnKeyP.Text = "ㅔ";
                    btnKeyA.Text = "ㅁ"; btnKeyS.Text = "ㄴ"; btnKeyD.Text = "ㅇ"; btnKeyF.Text = "ㄹ"; btnKeyG.Text = "ㅎ";
                    btnKeyH.Text = "ㅗ"; btnKeyJ.Text = "ㅓ"; btnKeyK.Text = "ㅏ"; btnKeyL.Text = "ㅣ"; btnKeyZ.Text = "ㅋ";
                    btnKeyX.Text = "ㅌ"; btnKeyC.Text = "ㅊ"; btnKeyV.Text = "ㅍ"; btnKeyB.Text = "ㅠ"; btnKeyN.Text = "ㅜ";
                    btnKeyM.Text = "ㅡ";
                }
                btnKeyShift.Visible = true;
            }
            else if (InsType == 2)
            {
                btnKeyQ.Text = "Q"; btnKeyW.Text = "W"; btnKeyE.Text = "E"; btnKeyR.Text = "R"; btnKeyT.Text = "T";
                btnKeyY.Text = "Y"; btnKeyU.Text = "U"; btnKeyI.Text = "I"; btnKeyO.Text = "O"; btnKeyP.Text = "P";
                btnKeyA.Text = "A"; btnKeyS.Text = "S"; btnKeyD.Text = "D"; btnKeyF.Text = "F"; btnKeyG.Text = "G";
                btnKeyH.Text = "H"; btnKeyJ.Text = "J"; btnKeyK.Text = "K"; btnKeyL.Text = "L"; btnKeyZ.Text = "Z";
                btnKeyX.Text = "X"; btnKeyC.Text = "C"; btnKeyV.Text = "V"; btnKeyB.Text = "B"; btnKeyN.Text = "N";
                btnKeyM.Text = "M";
                btnKeyShift.Visible = false;
            }
        }

        /// <summary>
        /// 미사용. 한글 키패드 쌍자음.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKeyShift_Click(object sender, EventArgs e)
        {
            if (btnKeyShift.BackColor == Color.White)
            {
                btnKeyShift.BackColor = Color.FromArgb(51, 51, 51);
                btnKeyShift.ForeColor = Color.White;
            }
            else
            {
                btnKeyShift.BackColor = Color.White;
                btnKeyShift.ForeColor = Color.Black;
            }
            //cbSelectOption_SelectedIndexChanged(null, null);
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
    }
}

