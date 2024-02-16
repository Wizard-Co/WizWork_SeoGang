using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizCommon.Popup.DefectU
{
    public partial class EditBoxAdd : Form
    {
        WizWorkLib Lib = new WizWorkLib();
        public EditBoxAdd()
        {
            InitializeComponent();
        }

        private void EditBoxAdd_Load(object sender, EventArgs e)
        {
            SetScreen();
        }
        private void SetScreen()
        {
            foreach (Control con in this.Controls)
            {
                con.Dock = DockStyle.Fill;
                con.Margin = new Padding(0, 0, 0, 0);
                foreach (Control ctl in con.Controls)
                {
                    ctl.Dock = DockStyle.Fill;
                    ctl.Margin = new Padding(0, 0, 0, 0);
                    foreach (Control ct in ctl.Controls)
                    {
                        ct.Dock = DockStyle.Fill;
                        ct.Margin = new Padding(0, 0, 0, 0);
                        foreach (Control c in ct.Controls)
                        {
                            c.Dock = DockStyle.Fill;
                            c.Margin = new Padding(0, 0, 0, 0);
                        }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(Lib.OnlyNumber(txtGetQty.Text) > 0)
            {
                string Text = "이전처리 수량을 1 이상으로 입력하세요";
                WizCommon.Popup.MyMessageBox.ShowBox(Text, "[확인]", 0, 0);
            }

            //xp_WizIns_sTransferBoxIDInfo
        }
    }
}
