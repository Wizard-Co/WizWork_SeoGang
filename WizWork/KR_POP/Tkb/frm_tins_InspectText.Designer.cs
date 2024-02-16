namespace WizWork
{
    partial class frm_tins_InspectText
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdSelect_2 = new System.Windows.Forms.Button();
            this.cmdSelect_1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 159);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cmdSelect_2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdSelect_1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(313, 159);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cmdSelect_2
            // 
            this.cmdSelect_2.BackColor = System.Drawing.Color.Red;
            this.cmdSelect_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdSelect_2.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdSelect_2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmdSelect_2.Location = new System.Drawing.Point(159, 4);
            this.cmdSelect_2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdSelect_2.Name = "cmdSelect_2";
            this.cmdSelect_2.Size = new System.Drawing.Size(151, 151);
            this.cmdSelect_2.TabIndex = 3;
            this.cmdSelect_2.Text = "불량";
            this.cmdSelect_2.UseVisualStyleBackColor = false;
            this.cmdSelect_2.Click += new System.EventHandler(this.cmdSelect_2_Click);
            // 
            // cmdSelect_1
            // 
            this.cmdSelect_1.BackColor = System.Drawing.Color.Blue;
            this.cmdSelect_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdSelect_1.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdSelect_1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmdSelect_1.Location = new System.Drawing.Point(3, 4);
            this.cmdSelect_1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdSelect_1.Name = "cmdSelect_1";
            this.cmdSelect_1.Size = new System.Drawing.Size(150, 151);
            this.cmdSelect_1.TabIndex = 2;
            this.cmdSelect_1.Text = "양호";
            this.cmdSelect_1.UseVisualStyleBackColor = false;
            this.cmdSelect_1.Click += new System.EventHandler(this.cmdSelect_1_Click);
            // 
            // frm_tins_InspectText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 159);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(329, 198);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(329, 198);
            this.Name = "frm_tins_InspectText";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "검사값 입력";
            this.Load += new System.EventHandler(this.frm_tins_InspectText_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button cmdSelect_2;
        private System.Windows.Forms.Button cmdSelect_1;
    }
}