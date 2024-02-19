namespace WizWork
{
    partial class Frm_tprc_Mold_Q
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMoldList = new System.Windows.Forms.DataGridView();
            this.btnUseMold = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
            this.tlpForm = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoldList)).BeginInit();
            this.tlpBottom.SuspendLayout();
            this.tlpForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMoldList
            // 
            this.dgvMoldList.AllowUserToAddRows = false;
            this.dgvMoldList.AllowUserToDeleteRows = false;
            this.dgvMoldList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMoldList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMoldList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMoldList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMoldList.Location = new System.Drawing.Point(3, 3);
            this.dgvMoldList.MultiSelect = false;
            this.dgvMoldList.Name = "dgvMoldList";
            this.dgvMoldList.ReadOnly = true;
            this.dgvMoldList.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dgvMoldList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMoldList.RowTemplate.Height = 23;
            this.dgvMoldList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMoldList.Size = new System.Drawing.Size(778, 299);
            this.dgvMoldList.TabIndex = 249;
            this.dgvMoldList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMoldList_CellClick);
            // 
            // btnUseMold
            // 
            this.btnUseMold.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnUseMold.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btnUseMold.Location = new System.Drawing.Point(4, 4);
            this.btnUseMold.Name = "btnUseMold";
            this.btnUseMold.Size = new System.Drawing.Size(381, 40);
            this.btnUseMold.TabIndex = 248;
            this.btnUseMold.Text = "확  인";
            this.btnUseMold.UseVisualStyleBackColor = true;
            this.btnUseMold.Click += new System.EventHandler(this.btnUseMold_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(392, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(382, 40);
            this.btnClose.TabIndex = 247;
            this.btnClose.Text = "취  소";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tlpBottom
            // 
            this.tlpBottom.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpBottom.ColumnCount = 2;
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpBottom.Controls.Add(this.btnUseMold, 0, 0);
            this.tlpBottom.Controls.Add(this.btnClose, 1, 0);
            this.tlpBottom.Location = new System.Drawing.Point(3, 308);
            this.tlpBottom.Name = "tlpBottom";
            this.tlpBottom.RowCount = 1;
            this.tlpBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpBottom.Size = new System.Drawing.Size(778, 48);
            this.tlpBottom.TabIndex = 250;
            // 
            // tlpForm
            // 
            this.tlpForm.ColumnCount = 1;
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForm.Controls.Add(this.tlpBottom, 0, 1);
            this.tlpForm.Controls.Add(this.dgvMoldList, 0, 0);
            this.tlpForm.Location = new System.Drawing.Point(1, 2);
            this.tlpForm.Name = "tlpForm";
            this.tlpForm.RowCount = 2;
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpForm.Size = new System.Drawing.Size(784, 359);
            this.tlpForm.TabIndex = 251;
            // 
            // Frm_tprc_Mold_Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.tlpForm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_tprc_Mold_Q";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "금형선택";
            this.Load += new System.EventHandler(this.Frm_tprc_Mold_Q_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoldList)).EndInit();
            this.tlpBottom.ResumeLayout(false);
            this.tlpForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox cboProcess;
        private System.Windows.Forms.DataGridView dgvMoldList;
        private System.Windows.Forms.Button btnUseMold;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tlpBottom;
        private System.Windows.Forms.TableLayoutPanel tlpForm;
    }
}

