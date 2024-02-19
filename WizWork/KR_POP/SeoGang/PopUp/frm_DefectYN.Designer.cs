namespace WizWork.POPUP
{
    partial class frm_DefectYN
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
            this.btnDefectN = new System.Windows.Forms.Button();
            this.btnDefectY = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbInputValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDefectN
            // 
            this.btnDefectN.BackColor = System.Drawing.Color.LimeGreen;
            this.btnDefectN.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDefectN.Location = new System.Drawing.Point(14, 68);
            this.btnDefectN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDefectN.Name = "btnDefectN";
            this.btnDefectN.Size = new System.Drawing.Size(134, 76);
            this.btnDefectN.TabIndex = 0;
            this.btnDefectN.Text = "적합";
            this.btnDefectN.UseVisualStyleBackColor = false;
            this.btnDefectN.Click += new System.EventHandler(this.btnDefectN_Click);
            // 
            // btnDefectY
            // 
            this.btnDefectY.BackColor = System.Drawing.Color.Red;
            this.btnDefectY.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDefectY.Location = new System.Drawing.Point(162, 68);
            this.btnDefectY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDefectY.Name = "btnDefectY";
            this.btnDefectY.Size = new System.Drawing.Size(134, 76);
            this.btnDefectY.TabIndex = 1;
            this.btnDefectY.Text = "부적합";
            this.btnDefectY.UseVisualStyleBackColor = false;
            this.btnDefectY.Click += new System.EventHandler(this.btnDefectY_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.tbInputValue);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnDefectN);
            this.panel1.Controls.Add(this.btnDefectY);
            this.panel1.Location = new System.Drawing.Point(7, 6);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 157);
            this.panel1.TabIndex = 2;
            // 
            // tbInputValue
            // 
            this.tbInputValue.Font = new System.Drawing.Font("맑은 고딕", 24F);
            this.tbInputValue.Location = new System.Drawing.Point(88, 11);
            this.tbInputValue.Name = "tbInputValue";
            this.tbInputValue.Size = new System.Drawing.Size(209, 50);
            this.tbInputValue.TabIndex = 45;
            this.tbInputValue.Click += new System.EventHandler(this.tbInputValue_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(13, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 50);
            this.label6.TabIndex = 44;
            this.label6.Text = "입력";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Frm_Qlt_DefectYN
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(323, 169);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_Qlt_DefectYN";
            this.Text = "판정";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDefectN;
        private System.Windows.Forms.Button btnDefectY;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox tbInputValue;
    }
}