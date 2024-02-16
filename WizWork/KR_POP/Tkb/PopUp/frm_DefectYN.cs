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
    public partial class frm_DefectYN : Form
    {
        public string DefectYN = string.Empty;
        public string Value = string.Empty;
        public bool CheckedAll = false;
        int InspectType = 0;

        Frm_CMKeypad keypad = new Frm_CMKeypad();
        Frm_CMNumericKeypad numkeypad = new Frm_CMNumericKeypad();
        //Frm_Qlt_Keypad keypad = new Frm_Qlt_Keypad();
        //Frm_Qlt_NumericKeypad nkeypad = new Frm_Qlt_NumericKeypad();

        /// <summary>
        /// 생성
        /// </summary>
        /// <param name="InsType">검사타입(정성/정량)</param>
        public frm_DefectYN(int InsType)
        {
            InitializeComponent();
            InspectType = InsType;
        }

        /// <summary>
        /// 취소버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 적합버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDefectN_Click(object sender, EventArgs e)
        {
            DefectYN = "N";
            DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 부적합버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDefectY_Click(object sender, EventArgs e)
        {
            DefectYN = "Y";
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 입력텍스트 클릭 - 검사타입에 따라 해당 키패드 팝업
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInputValue_Click(object sender, EventArgs e)
        {
            if (InspectType == 1)
            {
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    ((TextBox)sender).Text = keypad.tbInputText.Text;
                }
            }
            else if (InspectType == 2)
            {
                if (numkeypad.ShowDialog() == DialogResult.OK)
                {
                    ((TextBox)sender).Text = numkeypad.tbInputText.Text;
                }
            }
        }
    }
}
