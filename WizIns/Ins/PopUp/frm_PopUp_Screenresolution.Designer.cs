
namespace WizIns
{
    partial class frm_tprc_Screenresolution
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
            this.tlpForm = new System.Windows.Forms.TableLayoutPanel();
            this.btnScreen = new System.Windows.Forms.Button();
            this.btnNonScreen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tlpForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpForm
            // 
            this.tlpForm.ColumnCount = 3;
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpForm.Controls.Add(this.btnScreen, 0, 0);
            this.tlpForm.Controls.Add(this.btnNonScreen, 1, 0);
            this.tlpForm.Controls.Add(this.btnClose, 2, 0);
            this.tlpForm.Location = new System.Drawing.Point(4, 3);
            this.tlpForm.Name = "tlpForm";
            this.tlpForm.RowCount = 1;
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpForm.Size = new System.Drawing.Size(740, 196);
            this.tlpForm.TabIndex = 0;
            // 
            // btnScreen
            // 
            this.btnScreen.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnScreen.Location = new System.Drawing.Point(3, 3);
            this.btnScreen.Name = "btnScreen";
            this.btnScreen.Size = new System.Drawing.Size(240, 190);
            this.btnScreen.TabIndex = 0;
            this.btnScreen.Text = "화면 크기 조절";
            this.btnScreen.UseVisualStyleBackColor = true;
            this.btnScreen.Click += new System.EventHandler(this.btnScreen_Click);
            // 
            // btnNonScreen
            // 
            this.btnNonScreen.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnNonScreen.Location = new System.Drawing.Point(249, 3);
            this.btnNonScreen.Name = "btnNonScreen";
            this.btnNonScreen.Size = new System.Drawing.Size(240, 190);
            this.btnNonScreen.TabIndex = 1;
            this.btnNonScreen.Text = "기본 크기";
            this.btnNonScreen.UseVisualStyleBackColor = true;
            this.btnNonScreen.Click += new System.EventHandler(this.btnNonScreen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.Location = new System.Drawing.Point(495, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(242, 190);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "종료";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frm_tprc_Screenresolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 201);
            this.Controls.Add(this.tlpForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(760, 240);
            this.MinimizeBox = false;
            this.Name = "frm_tprc_Screenresolution";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_tprc_Screenresolution";
            this.Load += new System.EventHandler(this.frm_tprc_Screenresolution_Load);
            this.tlpForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpForm;
        private System.Windows.Forms.Button btnScreen;
        private System.Windows.Forms.Button btnNonScreen;
        private System.Windows.Forms.Button btnClose;
    }
}