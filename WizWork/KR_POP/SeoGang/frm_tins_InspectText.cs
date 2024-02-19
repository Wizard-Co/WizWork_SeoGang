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
    public partial class frm_tins_InspectText : Form
    {
        public frm_tins_InspectText()
        {
            InitializeComponent();
        }

        private void frm_tins_InspectText_Load(object sender, EventArgs e)
        {

        }

        private void cmdSelect_1_Click(object sender, EventArgs e)
        {
            SetChekValue(this.cmdSelect_1.Text);
        }

        private void cmdSelect_2_Click(object sender, EventArgs e)
        {
              SetChekValue(this.cmdSelect_2.Text);
        }


        private void SetChekValue(string strChkValue)
        {
            if (Owner.Name == "frm_tins_InspectAuto_U"  )
            {
                ((WizWork.frm_tins_InspectAuto_U)(this.Owner)).SetCheckValue(strChkValue);
                
            }
            
            Close();
            return;
        }

    }
}
