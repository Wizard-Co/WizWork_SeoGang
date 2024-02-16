namespace WizWork
{
    partial class frm_PopUp_ChoiceLabelPrintType
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNC = new System.Windows.Forms.Button();
            this.btnYO = new System.Windows.Forms.Button();
            this.btnYC = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tlpBottomChoice = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tlpBottomChoice.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.panel1, 0, 0);
            this.tlpMain.Controls.Add(this.panel2, 0, 2);
            this.tlpMain.Controls.Add(this.panel6, 0, 4);
            this.tlpMain.Location = new System.Drawing.Point(12, 12);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpMain.Size = new System.Drawing.Size(993, 388);
            this.tlpMain.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tlpHeader);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(951, 47);
            this.panel1.TabIndex = 0;
            // 
            // tlpHeader
            // 
            this.tlpHeader.ColumnCount = 1;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpHeader.Controls.Add(this.lblHeader, 0, 0);
            this.tlpHeader.Location = new System.Drawing.Point(8, 6);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.Size = new System.Drawing.Size(896, 33);
            this.tlpHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.SystemColors.Highlight;
            this.lblHeader.Font = new System.Drawing.Font("맑은 고딕", 23F);
            this.lblHeader.ForeColor = System.Drawing.SystemColors.Control;
            this.lblHeader.Location = new System.Drawing.Point(3, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(847, 33);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "라벨 발행유형을 선택하세요!!";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Location = new System.Drawing.Point(3, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(964, 218);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.btnNC, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnYO, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnYC, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(922, 183);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnNC
            // 
            this.btnNC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(172)))), ((int)(((byte)(234)))));
            this.btnNC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNC.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.btnNC.Location = new System.Drawing.Point(3, 3);
            this.btnNC.Name = "btnNC";
            this.btnNC.Size = new System.Drawing.Size(273, 155);
            this.btnNC.TabIndex = 0;
            this.btnNC.Text = "1000EA로 인쇄된 \r\n라벨 1장이 나옵니다";
            this.btnNC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNC.UseVisualStyleBackColor = false;
            this.btnNC.Click += new System.EventHandler(this.btnNC_Click);
            // 
            // btnYO
            // 
            this.btnYO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(192)))), ((int)(((byte)(221)))));
            this.btnYO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnYO.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.btnYO.Location = new System.Drawing.Point(316, 3);
            this.btnYO.Name = "btnYO";
            this.btnYO.Size = new System.Drawing.Size(273, 155);
            this.btnYO.TabIndex = 0;
            this.btnYO.Text = "300EA로 인쇄된\r\n라벨 3장이 나옵니다";
            this.btnYO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnYO.UseVisualStyleBackColor = false;
            this.btnYO.Click += new System.EventHandler(this.btnYO_Click);
            // 
            // btnYC
            // 
            this.btnYC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnYC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnYC.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.btnYC.Location = new System.Drawing.Point(629, 3);
            this.btnYC.Name = "btnYC";
            this.btnYC.Size = new System.Drawing.Size(273, 155);
            this.btnYC.TabIndex = 0;
            this.btnYC.Text = "300EA, 100EA로 인쇄된\r\n라벨 4장이 나옵니다";
            this.btnYC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnYC.UseVisualStyleBackColor = false;
            this.btnYC.Click += new System.EventHandler(this.btnYC_Click);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.tlpBottomChoice);
            this.panel6.Location = new System.Drawing.Point(3, 331);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(964, 54);
            this.panel6.TabIndex = 3;
            // 
            // tlpBottomChoice
            // 
            this.tlpBottomChoice.ColumnCount = 3;
            this.tlpBottomChoice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpBottomChoice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlpBottomChoice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpBottomChoice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpBottomChoice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpBottomChoice.Controls.Add(this.btnCancel, 1, 0);
            this.tlpBottomChoice.Location = new System.Drawing.Point(3, 3);
            this.tlpBottomChoice.Name = "tlpBottomChoice";
            this.tlpBottomChoice.RowCount = 1;
            this.tlpBottomChoice.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottomChoice.Size = new System.Drawing.Size(910, 47);
            this.tlpBottomChoice.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(91, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(710, 44);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frm_PopUp_ChoiceLabelPrintType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 449);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_PopUp_ChoiceLabelPrintType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "전표라벨 스캔";
            this.Load += new System.EventHandler(this.frm_PopUp_ChoiceLabelPrintType_Load);
            this.tlpMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tlpHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tlpBottomChoice.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TableLayoutPanel tlpBottomChoice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNC;
        private System.Windows.Forms.Button btnYO;
        private System.Windows.Forms.Button btnYC;
    }
}