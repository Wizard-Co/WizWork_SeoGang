using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.Popup.DefectU
{
    public partial class FrmDefect : Form
    {
        List<CB_IDNAME> list_cbx = new List<CB_IDNAME>();
        WizWorkLib Lib = new WizWorkLib();
        public FrmDefect()
        {
            InitializeComponent();
        }

        public FrmDefect(List<CB_IDNAME> cbxInfo)
        {
            list_cbx = null;
            list_cbx = cbxInfo;
        }

        private void FrmDefect_Load(object sender, EventArgs e)
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

        //콤보박스에 Depart 부서 셋팅
        private void SetComboBox()
        {
            try
            {
                DataTable dt = Lib.GetCode(WizWorkLib.CodeTypeClss.CD_GRADE);

                //컬럼명 변경 ID와, NAME으로
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.ToUpper().Contains("ID"))
                    {
                        dc.ColumnName = "ID";
                    }
                    else //if (dc.ColumnName.ToUpper().Contains("NAME"))
                    {
                        dc.ColumnName = "NAME";
                    }
                }

                //패널크기에 따른 콤보박스 사이즈 및 폰트사이즈 변경
                Lib.SetComboBox(cboDefect, dt, list_cbx);
            }
            catch (Exception e)
            {
                Common.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }

        //주석 풀어서 수정할것
        //private void btnOK_Click(object sender, EventArgs e)
        //{
        //    if(Lib.OnlyNumber(txtGetQty.Text) > 0)
        //    {
        //        string Text = "이전처리 수량을 1 이상으로 입력하세요";
        //        Common.Popup.MyMessageBox.ShowBox(Text, "[확인]", 0, 0);
        //    }

        //    //xp_WizIns_sTransferBoxIDInfo
        //}
    }
}
