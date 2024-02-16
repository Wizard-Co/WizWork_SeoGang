namespace WizCommon.Popup
{
    partial class Frm_TLP_Calendar
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.btnPreviousYear = new System.Windows.Forms.Button();
            this.btnPreviousMonth = new System.Windows.Forms.Button();
            this.btnAfterYear = new System.Windows.Forms.Button();
            this.btnAfterMonth = new System.Windows.Forms.Button();
            this.lblYYYYMM = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlpFill = new System.Windows.Forms.TableLayoutPanel();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlpFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.White;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlMain, 0, 0);
            this.tlpMain.Controls.Add(this.panel1, 0, 1);
            this.tlpMain.Location = new System.Drawing.Point(9, 9);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83F));
            this.tlpMain.Size = new System.Drawing.Size(951, 509);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.tlpHeader);
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(918, 80);
            this.pnlMain.TabIndex = 0;
            // 
            // tlpHeader
            // 
            this.tlpHeader.ColumnCount = 5;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tlpHeader.Controls.Add(this.btnPreviousYear, 1, 0);
            this.tlpHeader.Controls.Add(this.btnPreviousMonth, 0, 0);
            this.tlpHeader.Controls.Add(this.btnAfterYear, 3, 0);
            this.tlpHeader.Controls.Add(this.btnAfterMonth, 4, 0);
            this.tlpHeader.Controls.Add(this.lblYYYYMM, 2, 0);
            this.tlpHeader.Location = new System.Drawing.Point(3, 3);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.Size = new System.Drawing.Size(894, 71);
            this.tlpHeader.TabIndex = 0;
            // 
            // btnPreviousYear
            // 
            this.btnPreviousYear.Location = new System.Drawing.Point(119, 3);
            this.btnPreviousYear.Name = "btnPreviousYear";
            this.btnPreviousYear.Size = new System.Drawing.Size(110, 65);
            this.btnPreviousYear.TabIndex = 1;
            this.btnPreviousYear.Text = "▼\r\n\r\n전 년";
            this.btnPreviousYear.UseVisualStyleBackColor = true;
            this.btnPreviousYear.Click += new System.EventHandler(this.btnPreviousYear_Click);
            // 
            // btnPreviousMonth
            // 
            this.btnPreviousMonth.Location = new System.Drawing.Point(3, 3);
            this.btnPreviousMonth.Name = "btnPreviousMonth";
            this.btnPreviousMonth.Size = new System.Drawing.Size(110, 65);
            this.btnPreviousMonth.TabIndex = 0;
            this.btnPreviousMonth.Text = "◀\r\n\r\n이전달";
            this.btnPreviousMonth.UseVisualStyleBackColor = true;
            this.btnPreviousMonth.Click += new System.EventHandler(this.btnPreviousMonth_Click);
            // 
            // btnAfterYear
            // 
            this.btnAfterYear.Location = new System.Drawing.Point(664, 3);
            this.btnAfterYear.Name = "btnAfterYear";
            this.btnAfterYear.Size = new System.Drawing.Size(110, 65);
            this.btnAfterYear.TabIndex = 2;
            this.btnAfterYear.Text = "▲\r\n\r\n내 년";
            this.btnAfterYear.UseVisualStyleBackColor = true;
            this.btnAfterYear.Click += new System.EventHandler(this.btnAfterYear_Click);
            // 
            // btnAfterMonth
            // 
            this.btnAfterMonth.Location = new System.Drawing.Point(780, 3);
            this.btnAfterMonth.Name = "btnAfterMonth";
            this.btnAfterMonth.Size = new System.Drawing.Size(111, 65);
            this.btnAfterMonth.TabIndex = 3;
            this.btnAfterMonth.Text = "▶\r\n\r\n다음달";
            this.btnAfterMonth.UseVisualStyleBackColor = true;
            this.btnAfterMonth.Click += new System.EventHandler(this.btnAfterMonth_Click);
            // 
            // lblYYYYMM
            // 
            this.lblYYYYMM.AutoSize = true;
            this.lblYYYYMM.BackColor = System.Drawing.Color.White;
            this.lblYYYYMM.Font = new System.Drawing.Font("굴림", 31.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblYYYYMM.Location = new System.Drawing.Point(235, 0);
            this.lblYYYYMM.Name = "lblYYYYMM";
            this.lblYYYYMM.Size = new System.Drawing.Size(347, 53);
            this.lblYYYYMM.TabIndex = 4;
            this.lblYYYYMM.Text = "yyyy년 mm월";
            this.lblYYYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tlpFill);
            this.panel1.Location = new System.Drawing.Point(3, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(918, 407);
            this.panel1.TabIndex = 1;
            // 
            // tlpFill
            // 
            this.tlpFill.ColumnCount = 7;
            this.tlpFill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpFill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tlpFill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tlpFill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tlpFill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tlpFill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tlpFill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tlpFill.Controls.Add(this.button11, 6, 0);
            this.tlpFill.Controls.Add(this.button10, 5, 0);
            this.tlpFill.Controls.Add(this.button9, 4, 0);
            this.tlpFill.Controls.Add(this.button8, 3, 0);
            this.tlpFill.Controls.Add(this.button7, 2, 0);
            this.tlpFill.Controls.Add(this.button6, 1, 0);
            this.tlpFill.Controls.Add(this.button5, 0, 0);
            this.tlpFill.Location = new System.Drawing.Point(3, 3);
            this.tlpFill.Name = "tlpFill";
            this.tlpFill.RowCount = 7;
            this.tlpFill.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpFill.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpFill.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpFill.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpFill.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpFill.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpFill.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpFill.Size = new System.Drawing.Size(889, 382);
            this.tlpFill.TabIndex = 0;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.White;
            this.button11.Font = new System.Drawing.Font("굴림", 10.8F);
            this.button11.ForeColor = System.Drawing.Color.Blue;
            this.button11.Location = new System.Drawing.Point(764, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(116, 32);
            this.button11.TabIndex = 6;
            this.button11.Text = "토";
            this.button11.UseVisualStyleBackColor = false;
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.White;
            this.button10.Font = new System.Drawing.Font("굴림", 10.8F);
            this.button10.Location = new System.Drawing.Point(637, 3);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(116, 32);
            this.button10.TabIndex = 5;
            this.button10.Text = "금";
            this.button10.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.White;
            this.button9.Font = new System.Drawing.Font("굴림", 10.8F);
            this.button9.Location = new System.Drawing.Point(510, 3);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(116, 32);
            this.button9.TabIndex = 4;
            this.button9.Text = "목";
            this.button9.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.White;
            this.button8.Font = new System.Drawing.Font("굴림", 10.8F);
            this.button8.Location = new System.Drawing.Point(383, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(116, 32);
            this.button8.TabIndex = 3;
            this.button8.Text = "수";
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.White;
            this.button7.Font = new System.Drawing.Font("굴림", 10.8F);
            this.button7.Location = new System.Drawing.Point(256, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(116, 32);
            this.button7.TabIndex = 2;
            this.button7.Text = "화";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button6.Location = new System.Drawing.Point(129, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(116, 32);
            this.button6.TabIndex = 1;
            this.button6.Text = "월";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button5.ForeColor = System.Drawing.Color.Red;
            this.button5.Location = new System.Drawing.Point(3, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(116, 32);
            this.button5.TabIndex = 0;
            this.button5.Text = "일";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // Frm_TLP_Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 545);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_TLP_Calendar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_TLP_Calendar";
            this.Load += new System.EventHandler(this.Frm_TLP_Calendar_Load);
            this.tlpMain.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.tlpHeader.ResumeLayout(false);
            this.tlpHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tlpFill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private System.Windows.Forms.Button btnPreviousYear;
        private System.Windows.Forms.Button btnPreviousMonth;
        private System.Windows.Forms.Button btnAfterYear;
        private System.Windows.Forms.Button btnAfterMonth;
        private System.Windows.Forms.Label lblYYYYMM;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tlpFill;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
    }
}