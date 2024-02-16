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
    public partial class frm_tprc_SearchMenuCollection : Form
    {

        public frm_tprc_SearchMenuCollection()
        {
            InitializeComponent();
        }

        bool blOpen = false;


        private void frm_tprc_SearchMenuCollection_Load(object sender, EventArgs e)
        {
            SetScreen();
        }

        private void SetScreen()
        {
            //패널 배치 및 조정          
            tlpForm.Dock = DockStyle.Fill;
            foreach (Control control in tlpForm.Controls)
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
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(0, 0, 0, 0);                            
                        }
                    }
                }
            }
        }



        private void btnControl_Click(object sender, EventArgs e)
        {
            int i = 0;
            Form form = null;//폼 초기화

            try
            {
                Button btn = sender as Button;
                string x = btn.Name.Substring(btn.Name.Length - 1, 1);
                int.TryParse(x, out i);

                switch (i)
                {
                    case 1:     //생산실적 조회
                        Frm_tprc_Result child1 = new Frm_tprc_Result();
                        form = child1;
                        break;
                    case 2:     //자주검사 실적 조회
                        frm_tins_InspectAutoResult_Q child2 = new frm_tins_InspectAutoResult_Q();
                        form = child2;
                        break;
                    case 3:     //잔량 이동처리 조회
                        frm_mtr_RemainMove_Q child3 = new frm_mtr_RemainMove_Q();
                        form = child3;
                        break;
                    case 5:     //설비점검 조회
                        Frm_tprc_DailMachineCheck_Q child5 = new Frm_tprc_DailMachineCheck_Q();
                        form = child5;
                        break;
                    case 6:     //Tool 교환 조회(설비예비품 교체 조회)
                        frm_tprc_UseTool_Q child6 = new frm_tprc_UseTool_Q();
                        form = child6;
                        break;
                    case 7:     // 금형점검 조회
                        frm_tprc_DailMoldCheck_Q child7 = new frm_tprc_DailMoldCheck_Q();
                        form = child7;
                        break;

                }
                if (form != null)
                {
                    foreach (Form openForm in Application.OpenForms)//중복실행방지
                    {
                        if (openForm.Name == form.Name)
                        {
                            blOpen = true;
                            openForm.BringToFront();
                            openForm.Activate();
                            return;
                        }
                    }
                    form.MdiParent = this.ParentForm;   //<< 핵심.
                    form.TopLevel = false;
                    form.Dock = DockStyle.Fill;
                    form.Show();

                    if (!blOpen)
                    {
                        form.BringToFront();
                        form.Show();
                    }
                }

            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
