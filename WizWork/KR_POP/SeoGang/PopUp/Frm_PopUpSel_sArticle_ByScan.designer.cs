namespace WizWork
{
    partial class Frm_PopUpSel_sArticle_ByScan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PopUpSel_sArticle_ByScan));
            this.grdData = new System.Windows.Forms.DataGridView();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tlpOC = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkArticle = new System.Windows.Forms.CheckBox();
            this.txtArticle = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlForm = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.tlpOC.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grdData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Location = new System.Drawing.Point(3, 49);
            this.grdData.MultiSelect = false;
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grdData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.grdData.RowTemplate.Height = 23;
            this.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new System.Drawing.Size(384, 194);
            this.grdData.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblTitle, 0, 0);
            this.tlpMain.Controls.Add(this.tlpOC, 0, 2);
            this.tlpMain.Controls.Add(this.grdData, 0, 1);
            this.tlpMain.Location = new System.Drawing.Point(6, 10);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(758, 309);
            this.tlpMain.TabIndex = 190;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(73)))));
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(655, 35);
            this.lblTitle.TabIndex = 242;
            this.lblTitle.Text = "해당 라벨 품명 리스트";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpOC
            // 
            this.tlpOC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(149)))), ((int)(((byte)(168)))));
            this.tlpOC.ColumnCount = 2;
            this.tlpOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOC.Controls.Add(this.btnOK, 0, 1);
            this.tlpOC.Controls.Add(this.btnDown, 1, 0);
            this.tlpOC.Controls.Add(this.btnCancel, 1, 1);
            this.tlpOC.Controls.Add(this.btnUp, 0, 0);
            this.tlpOC.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpOC.Location = new System.Drawing.Point(3, 249);
            this.tlpOC.Name = "tlpOC";
            this.tlpOC.RowCount = 2;
            this.tlpOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpOC.Size = new System.Drawing.Size(752, 57);
            this.tlpOC.TabIndex = 190;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(73)))));
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOK.Location = new System.Drawing.Point(0, 28);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(376, 29);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.SystemColors.Control;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(379, 4);
            this.btnDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(189, 20);
            this.btnDown.TabIndex = 186;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(376, 28);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(376, 29);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.SystemColors.Control;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(3, 4);
            this.btnUp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(189, 20);
            this.btnUp.TabIndex = 187;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(27, 338);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 40);
            this.panel1.TabIndex = 191;
            this.panel1.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtArticle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(738, 47);
            this.tableLayoutPanel1.TabIndex = 191;
            this.tableLayoutPanel1.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.chkArticle);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(141, 40);
            this.panel3.TabIndex = 193;
            // 
            // chkArticle
            // 
            this.chkArticle.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkArticle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkArticle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkArticle.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkArticle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkArticle.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkArticle.ForeColor = System.Drawing.Color.White;
            this.chkArticle.Location = new System.Drawing.Point(2, -1);
            this.chkArticle.Margin = new System.Windows.Forms.Padding(2);
            this.chkArticle.Name = "chkArticle";
            this.chkArticle.Size = new System.Drawing.Size(143, 43);
            this.chkArticle.TabIndex = 216;
            this.chkArticle.Text = "품명/품번";
            this.chkArticle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkArticle.UseVisualStyleBackColor = false;
            this.chkArticle.Click += new System.EventHandler(this.chkArticle_Click);
            // 
            // txtArticle
            // 
            this.txtArticle.Font = new System.Drawing.Font("맑은 고딕", 36F);
            this.txtArticle.Location = new System.Drawing.Point(150, 2);
            this.txtArticle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtArticle.Name = "txtArticle";
            this.txtArticle.Size = new System.Drawing.Size(175, 71);
            this.txtArticle.TabIndex = 218;
            this.txtArticle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtArticle_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Image = global::WizWork.Properties.Resources.icons8_search_48;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(592, 2);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(143, 43);
            this.btnSearch.TabIndex = 221;
            this.btnSearch.Text = "검색";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlForm
            // 
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForm.Controls.Add(this.tlpMain);
            this.pnlForm.Location = new System.Drawing.Point(5, 0);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(772, 332);
            this.pnlForm.TabIndex = 192;
            // 
            // Frm_PopUpSel_sArticle_ByScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 362);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Frm_PopUpSel_sArticle_ByScan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "품목코드 / 품번 선택";
            this.Load += new System.EventHandler(this.Frm_PopUpSel_sArticle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.tlpOC.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pnlForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpOC;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtArticle;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkArticle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblTitle;
    }
}