namespace WizWork
{
    partial class Frm_tprc_MoldDetail_Q
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_tprc_MoldDetail_Q));
            this.pnlForm = new System.Windows.Forms.Panel();
            this.tlpForm = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtBuyerModel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBuyerArticleNo = new System.Windows.Forms.Label();
            this.txtMoldNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtArticle = new System.Windows.Forms.Label();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.pnlForm.SuspendLayout();
            this.tlpForm.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlForm
            // 
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForm.Controls.Add(this.tlpForm);
            this.pnlForm.Location = new System.Drawing.Point(10, 12);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(974, 517);
            this.pnlForm.TabIndex = 167;
            // 
            // tlpForm
            // 
            this.tlpForm.ColumnCount = 1;
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForm.Controls.Add(this.btnClose, 0, 3);
            this.tlpForm.Controls.Add(this.pnlTop, 0, 0);
            this.tlpForm.Controls.Add(this.pnlFill, 0, 1);
            this.tlpForm.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tlpForm.Location = new System.Drawing.Point(91, -13);
            this.tlpForm.Name = "tlpForm";
            this.tlpForm.RowCount = 4;
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpForm.Size = new System.Drawing.Size(711, 541);
            this.tlpForm.TabIndex = 167;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(3, 489);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 46);
            this.btnClose.TabIndex = 156;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.tableLayoutPanel1);
            this.pnlTop.Location = new System.Drawing.Point(3, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(705, 75);
            this.pnlTop.TabIndex = 165;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.txtBuyerModel, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtBuyerArticleNo, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMoldNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtArticle, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(993, 79);
            this.tableLayoutPanel1.TabIndex = 164;
            // 
            // txtBuyerModel
            // 
            this.txtBuyerModel.BackColor = System.Drawing.Color.White;
            this.txtBuyerModel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBuyerModel.Font = new System.Drawing.Font("맑은 고딕", 13.75F, System.Drawing.FontStyle.Bold);
            this.txtBuyerModel.Location = new System.Drawing.Point(646, 39);
            this.txtBuyerModel.Name = "txtBuyerModel";
            this.txtBuyerModel.Size = new System.Drawing.Size(344, 36);
            this.txtBuyerModel.TabIndex = 18;
            this.txtBuyerModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(498, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 37);
            this.label5.TabIndex = 19;
            this.label5.Text = "차  종";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBuyerArticleNo
            // 
            this.txtBuyerArticleNo.BackColor = System.Drawing.Color.White;
            this.txtBuyerArticleNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBuyerArticleNo.Font = new System.Drawing.Font("맑은 고딕", 13.75F, System.Drawing.FontStyle.Bold);
            this.txtBuyerArticleNo.Location = new System.Drawing.Point(646, 0);
            this.txtBuyerArticleNo.Name = "txtBuyerArticleNo";
            this.txtBuyerArticleNo.Size = new System.Drawing.Size(344, 34);
            this.txtBuyerArticleNo.TabIndex = 15;
            this.txtBuyerArticleNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMoldNo
            // 
            this.txtMoldNo.BackColor = System.Drawing.Color.White;
            this.txtMoldNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtMoldNo.Font = new System.Drawing.Font("맑은 고딕", 13.75F, System.Drawing.FontStyle.Bold);
            this.txtMoldNo.Location = new System.Drawing.Point(151, 0);
            this.txtMoldNo.Name = "txtMoldNo";
            this.txtMoldNo.Size = new System.Drawing.Size(341, 34);
            this.txtMoldNo.TabIndex = 21;
            this.txtMoldNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 37);
            this.label2.TabIndex = 20;
            this.label2.Text = "금형LotNo";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(498, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 37);
            this.label3.TabIndex = 16;
            this.label3.Text = "품  번";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 37);
            this.label4.TabIndex = 17;
            this.label4.Text = "품  명";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtArticle
            // 
            this.txtArticle.BackColor = System.Drawing.Color.White;
            this.txtArticle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtArticle.Font = new System.Drawing.Font("맑은 고딕", 13.75F, System.Drawing.FontStyle.Bold);
            this.txtArticle.Location = new System.Drawing.Point(151, 39);
            this.txtArticle.Name = "txtArticle";
            this.txtArticle.Size = new System.Drawing.Size(341, 37);
            this.txtArticle.TabIndex = 14;
            this.txtArticle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFill
            // 
            this.pnlFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFill.Controls.Add(this.grdData);
            this.pnlFill.Location = new System.Drawing.Point(3, 84);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(690, 345);
            this.pnlFill.TabIndex = 157;
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdData.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdData.Location = new System.Drawing.Point(20, 12);
            this.grdData.MultiSelect = false;
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowHeadersVisible = false;
            this.grdData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.grdData.RowTemplate.Height = 23;
            this.grdData.Size = new System.Drawing.Size(705, 399);
            this.grdData.TabIndex = 163;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "일자";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "구분";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "입고량";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "출고량";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "재고량";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "제품생산량";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "공정";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "설비명";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnDown, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnUp, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 435);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(636, 48);
            this.tableLayoutPanel2.TabIndex = 166;
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.SystemColors.Control;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(321, 4);
            this.btnDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(189, 23);
            this.btnDown.TabIndex = 188;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
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
            this.btnUp.Size = new System.Drawing.Size(189, 23);
            this.btnUp.TabIndex = 189;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // Frm_tprc_MoldDetail_Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1008, 601);
            this.Controls.Add(this.pnlForm);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 639);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 639);
            this.Name = "Frm_tprc_MoldDetail_Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "금형상세이력보기";
            this.Activated += new System.EventHandler(this.Frm_tprc_MoldDetail_Q_Activated);
            this.Load += new System.EventHandler(this.Frm_tprc_MoldDetail_Q_Load);
            this.pnlForm.ResumeLayout(false);
            this.tlpForm.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox cboProcess;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.TableLayoutPanel tlpForm;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label txtBuyerModel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label txtBuyerArticleNo;
        private System.Windows.Forms.Label txtMoldNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label txtArticle;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
    }
}

