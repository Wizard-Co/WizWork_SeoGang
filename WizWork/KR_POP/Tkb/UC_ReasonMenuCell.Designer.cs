namespace WizWork
{
    partial class UC_ReasonMenuCell
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpChildTlp = new System.Windows.Forms.TableLayoutPanel();
            this.cbbCountY = new System.Windows.Forms.ComboBox();
            this.btnSetCellCount = new System.Windows.Forms.Button();
            this.cbbCountX = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbX = new System.Windows.Forms.Label();
            this.lbY = new System.Windows.Forms.Label();
            this.tlpChildTlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpChildTlp
            // 
            this.tlpChildTlp.ColumnCount = 2;
            this.tlpChildTlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChildTlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChildTlp.Controls.Add(this.cbbCountY, 1, 1);
            this.tlpChildTlp.Controls.Add(this.btnSetCellCount, 0, 2);
            this.tlpChildTlp.Controls.Add(this.cbbCountX, 0, 1);
            this.tlpChildTlp.Controls.Add(this.btnClose, 1, 2);
            this.tlpChildTlp.Controls.Add(this.lbX, 0, 0);
            this.tlpChildTlp.Controls.Add(this.lbY, 1, 0);
            this.tlpChildTlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpChildTlp.Location = new System.Drawing.Point(0, 0);
            this.tlpChildTlp.Name = "tlpChildTlp";
            this.tlpChildTlp.RowCount = 3;
            this.tlpChildTlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpChildTlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpChildTlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpChildTlp.Size = new System.Drawing.Size(200, 130);
            this.tlpChildTlp.TabIndex = 3;
            this.tlpChildTlp.SizeChanged += new System.EventHandler(this.tlpChildTlp_SizeChanged);
            // 
            // cbbCountY
            // 
            this.cbbCountY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbCountY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCountY.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbbCountY.FormattingEnabled = true;
            this.cbbCountY.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.cbbCountY.Location = new System.Drawing.Point(103, 29);
            this.cbbCountY.Name = "cbbCountY";
            this.cbbCountY.Size = new System.Drawing.Size(94, 40);
            this.cbbCountY.TabIndex = 5;
            // 
            // btnSetCellCount
            // 
            this.btnSetCellCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSetCellCount.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btnSetCellCount.Location = new System.Drawing.Point(3, 81);
            this.btnSetCellCount.Name = "btnSetCellCount";
            this.btnSetCellCount.Size = new System.Drawing.Size(94, 46);
            this.btnSetCellCount.TabIndex = 0;
            this.btnSetCellCount.Text = "적용";
            this.btnSetCellCount.UseVisualStyleBackColor = true;
            this.btnSetCellCount.Click += new System.EventHandler(this.btnSetCellCount_Click);
            // 
            // cbbCountX
            // 
            this.cbbCountX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbCountX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCountX.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbbCountX.FormattingEnabled = true;
            this.cbbCountX.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7"});
            this.cbbCountX.Location = new System.Drawing.Point(3, 29);
            this.cbbCountX.Name = "cbbCountX";
            this.cbbCountX.Size = new System.Drawing.Size(94, 40);
            this.cbbCountX.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(103, 81);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 46);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "나가기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbX
            // 
            this.lbX.AutoSize = true;
            this.lbX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbX.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            this.lbX.Location = new System.Drawing.Point(3, 0);
            this.lbX.Name = "lbX";
            this.lbX.Size = new System.Drawing.Size(94, 26);
            this.lbX.TabIndex = 3;
            this.lbX.Text = "X";
            this.lbX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbY
            // 
            this.lbY.AutoSize = true;
            this.lbY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbY.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            this.lbY.Location = new System.Drawing.Point(103, 0);
            this.lbY.Name = "lbY";
            this.lbY.Size = new System.Drawing.Size(94, 26);
            this.lbY.TabIndex = 4;
            this.lbY.Text = "Y";
            this.lbY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_ReasonMenuCell
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tlpChildTlp);
            this.Name = "UC_ReasonMenuCell";
            this.Size = new System.Drawing.Size(200, 130);
            this.tlpChildTlp.ResumeLayout(false);
            this.tlpChildTlp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel tlpChildTlp;
        private System.Windows.Forms.Button btnSetCellCount;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbX;
        private System.Windows.Forms.Label lbY;
        public System.Windows.Forms.ComboBox cbbCountY;
        public System.Windows.Forms.ComboBox cbbCountX;

    }
}
