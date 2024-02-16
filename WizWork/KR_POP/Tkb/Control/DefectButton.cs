using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWork
{
    public partial class DefectButton : UserControl
    {
        [Description("클릭이벤트"), Category("속성")]
        public event EventHandler onClick;

        public DefectButton()
        {
            InitializeComponent();
        }

        private void DefectButton_Load(object sender, EventArgs e)
        {
            LayoutSetting();
        }

        // Dock 설정 → Fill
        private void LayoutSetting()
        {
            foreach (Control control in tlpButton.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control contro in control.Controls)
                {
                    contro.Dock = DockStyle.Fill;
                    contro.Margin = new Padding(0, 0, 0, 0);
                    foreach (Control contr in contro.Controls)
                    {
                        contr.Dock = DockStyle.Fill;
                        contr.Margin = new Padding(0, 0, 0, 0);
                    }
                }
            }
        }

        public void OnClick(object sender, EventArgs args)
        {
            if (onClick != null)
            {
                Invoke(onClick);
            }
        }

        // 버튼 초기 세팅
        public void initDefectButton(string DefectName)
        {
            this.rbnDefect.Text = DefectName;
            this.rbnDefect.Checked = false;

            this.btnQty.Text = "";
        }

        public void setEnabled(bool flag)
        {
            this.rbnDefect.Enabled = flag;
            this.btnQty.Enabled = flag;

            if (flag == false)
            {
                this.rbnDefect.BackColor = Color.Empty;
                this.btnQty.BackColor = Color.Empty;
            }
        }

        // 버튼 불량 갯수 수정
        public void setDefectQty(double DefectQty)
        {
            btnQty.Text = stringFormatN0(DefectQty);

            if (DefectQty > 0)
            {
                this.rbnDefect.Checked = true;
            }
            else
            {
                btnQty.Text = "";
                this.rbnDefect.Checked = false;
            }
        }

        // 불량 정보 가져오기
        public DefectButton_CodeView getDefectInfo()
        {
            var Defect = new DefectButton_CodeView()
            {
                DefectName = rbnDefect.Text.Trim(),
                DefectQty = btnQty.Text.Trim(),
            };

            return Defect;
        }

        // 불량 갯수 가져오기
        public double getDefectQty()
        {
            return ConvertDouble(btnQty.Text.Trim());
        }

        #region 기타 메서드

        // 천단위 콤마로 변환
        private string stringFormatN0(object obj)
        {
            if (obj == null) { return ""; }

            return string.Format("{0:N0}", obj);
        }

        // 소수로 변환
        private double ConvertDouble(string str)
        {
            if (str == null) { return 0; }

            double result = 0;
            double chkDouble = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Replace(",", "");

                if (Double.TryParse(str, out chkDouble) == true)
                {
                    result = Double.Parse(str);
                }
            }

            return result;
        }

        #endregion
    }

    public class DefectButton_CodeView
    {
        public string DefectName { get; set; }
        public string DefectQty { get; set; }
    }
}
