namespace Common.Popup.DefectU
{
    partial class EditBoxAdd
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.lblCtlQtyChange = new System.Windows.Forms.Label();
            this.tlpFill = new System.Windows.Forms.TableLayoutPanel();
            this.cmdClearGetQty = new System.Windows.Forms.Button();
            this.cmdClearGetBoxID = new System.Windows.Forms.Button();
            this.txtGetBoxID = new System.Windows.Forms.TextBox();
            this.txtGetQty = new System.Windows.Forms.TextBox();
            this.btnBoxNo = new System.Windows.Forms.Button();
            this.btnQty = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpBottom.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.tlpFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.tlpMain);
            this.pnlMain.Location = new System.Drawing.Point(12, 1);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(641, 261);
            this.pnlMain.TabIndex = 1;
            // 
            // tlpMain
            // 
            this.tlpMain.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpBottom, 0, 2);
            this.tlpMain.Controls.Add(this.tlpTop, 0, 0);
            this.tlpMain.Controls.Add(this.tlpFill, 0, 1);
            this.tlpMain.Location = new System.Drawing.Point(0, 5);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.Size = new System.Drawing.Size(639, 254);
            this.tlpMain.TabIndex = 1;
            // 
            // tlpBottom
            // 
            this.tlpBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(149)))), ((int)(((byte)(168)))));
            this.tlpBottom.ColumnCount = 2;
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBottom.Controls.Add(this.btnOK, 0, 0);
            this.tlpBottom.Controls.Add(this.btnCancel, 1, 0);
            this.tlpBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpBottom.Location = new System.Drawing.Point(5, 211);
            this.tlpBottom.Name = "tlpBottom";
            this.tlpBottom.RowCount = 1;
            this.tlpBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBottom.Size = new System.Drawing.Size(629, 38);
            this.tlpBottom.TabIndex = 10;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(73)))));
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("맑은 고딕", 22F, System.Drawing.FontStyle.Bold);
            this.btnOK.Location = new System.Drawing.Point(0, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(314, 38);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 22F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(314, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(315, 38);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tlpTop
            // 
            this.tlpTop.BackColor = System.Drawing.Color.LightSkyBlue;
            this.tlpTop.ColumnCount = 1;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpTop.Controls.Add(this.lblCtlQtyChange, 0, 0);
            this.tlpTop.Location = new System.Drawing.Point(5, 5);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.Size = new System.Drawing.Size(555, 41);
            this.tlpTop.TabIndex = 1;
            // 
            // lblCtlQtyChange
            // 
            this.lblCtlQtyChange.AutoSize = true;
            this.lblCtlQtyChange.Font = new System.Drawing.Font("맑은 고딕", 24F);
            this.lblCtlQtyChange.Location = new System.Drawing.Point(3, 0);
            this.lblCtlQtyChange.Name = "lblCtlQtyChange";
            this.lblCtlQtyChange.Size = new System.Drawing.Size(426, 41);
            this.lblCtlQtyChange.TabIndex = 0;
            this.lblCtlQtyChange.Text = "박스수량 가져와서 추가하기";
            this.lblCtlQtyChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpFill
            // 
            this.tlpFill.ColumnCount = 3;
            this.tlpFill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpFill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpFill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFill.Controls.Add(this.cmdClearGetQty, 2, 1);
            this.tlpFill.Controls.Add(this.cmdClearGetBoxID, 2, 0);
            this.tlpFill.Controls.Add(this.txtGetBoxID, 1, 0);
            this.tlpFill.Controls.Add(this.txtGetQty, 1, 1);
            this.tlpFill.Controls.Add(this.btnBoxNo, 0, 0);
            this.tlpFill.Controls.Add(this.btnQty, 0, 1);
            this.tlpFill.Location = new System.Drawing.Point(5, 56);
            this.tlpFill.Name = "tlpFill";
            this.tlpFill.RowCount = 2;
            this.tlpFill.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFill.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFill.Size = new System.Drawing.Size(629, 141);
            this.tlpFill.TabIndex = 11;
            // 
            // cmdClearGetQty
            // 
            this.cmdClearGetQty.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.cmdClearGetQty.Image = global::Common.Properties.Resources.times;
            this.cmdClearGetQty.Location = new System.Drawing.Point(474, 73);
            this.cmdClearGetQty.Name = "cmdClearGetQty";
            this.cmdClearGetQty.Size = new System.Drawing.Size(152, 64);
            this.cmdClearGetQty.TabIndex = 5;
            this.cmdClearGetQty.UseVisualStyleBackColor = true;
            // 
            // cmdClearGetBoxID
            // 
            this.cmdClearGetBoxID.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.cmdClearGetBoxID.Image = global::Common.Properties.Resources.times;
            this.cmdClearGetBoxID.Location = new System.Drawing.Point(474, 3);
            this.cmdClearGetBoxID.Name = "cmdClearGetBoxID";
            this.cmdClearGetBoxID.Size = new System.Drawing.Size(152, 64);
            this.cmdClearGetBoxID.TabIndex = 4;
            this.cmdClearGetBoxID.UseVisualStyleBackColor = true;
            // 
            // txtGetBoxID
            // 
            this.txtGetBoxID.Font = new System.Drawing.Font("맑은 고딕", 34F, System.Drawing.FontStyle.Bold);
            this.txtGetBoxID.Location = new System.Drawing.Point(223, 3);
            this.txtGetBoxID.Name = "txtGetBoxID";
            this.txtGetBoxID.Size = new System.Drawing.Size(227, 68);
            this.txtGetBoxID.TabIndex = 0;
            // 
            // txtGetQty
            // 
            this.txtGetQty.Font = new System.Drawing.Font("맑은 고딕", 34F, System.Drawing.FontStyle.Bold);
            this.txtGetQty.Location = new System.Drawing.Point(223, 73);
            this.txtGetQty.Name = "txtGetQty";
            this.txtGetQty.Size = new System.Drawing.Size(227, 68);
            this.txtGetQty.TabIndex = 1;
            this.txtGetQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnBoxNo
            // 
            this.btnBoxNo.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.btnBoxNo.Location = new System.Drawing.Point(3, 3);
            this.btnBoxNo.Name = "btnBoxNo";
            this.btnBoxNo.Size = new System.Drawing.Size(197, 64);
            this.btnBoxNo.TabIndex = 2;
            this.btnBoxNo.Text = "가져오는박스번호";
            this.btnBoxNo.UseVisualStyleBackColor = true;
            // 
            // btnQty
            // 
            this.btnQty.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.btnQty.Location = new System.Drawing.Point(3, 73);
            this.btnQty.Name = "btnQty";
            this.btnQty.Size = new System.Drawing.Size(197, 55);
            this.btnQty.TabIndex = 3;
            this.btnQty.Text = "가져오는 수량";
            this.btnQty.UseVisualStyleBackColor = true;
            // 
            // EditBoxAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 285);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditBoxAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_tins_LabelRegister";
            this.Load += new System.EventHandler(this.EditBoxAdd_Load);
            this.pnlMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpBottom.ResumeLayout(false);
            this.tlpTop.ResumeLayout(false);
            this.tlpTop.PerformLayout();
            this.tlpFill.ResumeLayout(false);
            this.tlpFill.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpBottom;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.Label lblCtlQtyChange;
        private System.Windows.Forms.TableLayoutPanel tlpFill;
        private System.Windows.Forms.Button cmdClearGetQty;
        private System.Windows.Forms.Button cmdClearGetBoxID;
        private System.Windows.Forms.TextBox txtGetBoxID;
        private System.Windows.Forms.TextBox txtGetQty;
        private System.Windows.Forms.Button btnBoxNo;
        private System.Windows.Forms.Button btnQty;
    }
}