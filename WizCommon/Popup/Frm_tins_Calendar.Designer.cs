namespace WizCommon.Popup
{
    partial class Frm_tins_Calendar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_tins_Calendar));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.excelCalendar = new AxMSACAL.AxCalendar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.excelCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnPrev);
            this.panel1.Controls.Add(this.excelCalendar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 562);
            this.panel1.TabIndex = 0;
            // 
            // btnNext
            // 
            this.btnNext.Image = global::WizCommon.Properties.Resources.forward_right_arrow_button;
            this.btnNext.Location = new System.Drawing.Point(675, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(112, 86);
            this.btnNext.TabIndex = 14;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(592, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 43);
            this.label1.TabIndex = 15;
            // 
            // btnPrev
            // 
            this.btnPrev.Image = global::WizCommon.Properties.Resources.left_arrow;
            this.btnPrev.Location = new System.Drawing.Point(15, 0);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(113, 86);
            this.btnPrev.TabIndex = 13;
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // excelCalendar
            // 
            this.excelCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.excelCalendar.Enabled = true;
            this.excelCalendar.Location = new System.Drawing.Point(0, 0);
            this.excelCalendar.Name = "excelCalendar";
            this.excelCalendar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("excelCalendar.OcxState")));
            this.excelCalendar.Size = new System.Drawing.Size(804, 560);
            this.excelCalendar.TabIndex = 12;
            this.excelCalendar.ClickEvent += new System.EventHandler(this.excelCalendar_ClickEvent);
            // 
            // Frm_tins_Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(806, 562);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_tins_Calendar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_tprc_Calendar";
            this.Load += new System.EventHandler(this.Frm_tins_Calendar_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.excelCalendar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrev;
        private AxMSACAL.AxCalendar excelCalendar;
    }
}