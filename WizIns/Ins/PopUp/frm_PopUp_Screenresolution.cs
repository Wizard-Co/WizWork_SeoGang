using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizIns
{
    public partial class frm_tprc_Screenresolution : Form
    {
        public frm_tprc_Screenresolution()
        {
            InitializeComponent();
        }

        //load
        private void frm_tprc_Screenresolution_Load(object sender, EventArgs e)
        {
            InitPanel();
            this.BringToFront();
            this.Activate();
            //this.WindowState = FormWindowState.Maximized;
        }

        //크기 조절
        private void btnScreen_Click(object sender, EventArgs e)
        {
            //Application.Run(new POPUP.frm_tprc_Screenresolution());
            var openForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f is Frm_tins_Main);

            if (openForm != null)
            {
                openForm.BringToFront();
                openForm.Activate();
            }
            else
            {
                Frm_tins_Main secondForm = new Frm_tins_Main(true);
                secondForm.Show();

                // Close the main form
                this.Hide(); // Hide the main form to keep the application running
                secondForm.FormClosed += (s, args) => this.Close(); // Close the main form when the second form is closed
            }


        }

        //크기 조절 안함
        private void btnNonScreen_Click(object sender, EventArgs e)
        {
            //Application.Run(new POPUP.frm_tprc_Screenresolution());
            var openForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f is Frm_tins_Main);

            if (openForm != null)
            {
                openForm.BringToFront();
                openForm.Activate();
            }
            else
            {
                Frm_tins_Main secondForm = new Frm_tins_Main(false);
                secondForm.Show();
                secondForm.BringToFront();
                secondForm.Activate();

                // Close the main form
                this.Hide(); // Hide the main form to keep the application running
                secondForm.FormClosed += (s, args) => this.Close(); // Close the main form when the second form is closed
            }
        }

        //프로그램 종료
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitPanel()
        {
            tlpForm.Dock = DockStyle.Fill;
            foreach (Control control in tlpForm.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(1, 1, 1, 1);
                foreach (Control ctl in control.Controls)//tlp 상위에서 3번째
                {
                    ctl.Dock = DockStyle.Fill;
                    ctl.Margin = new Padding(1, 1, 1, 1);
                    foreach (Control con in ctl.Controls)
                    {
                        con.Dock = DockStyle.Fill;
                        con.Margin = new Padding(1, 1, 1, 1);
                        foreach (Control co in con.Controls)
                        {
                            co.Dock = DockStyle.Fill;
                            co.Margin = new Padding(1, 1, 1, 1);
                            foreach (Control c in co.Controls)
                            {
                                c.Dock = DockStyle.Fill;
                                c.Margin = new Padding(1, 1, 1, 1);
                                foreach (Control contro in c.Controls)
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
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
