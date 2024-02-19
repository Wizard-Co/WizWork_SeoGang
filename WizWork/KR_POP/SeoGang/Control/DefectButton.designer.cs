namespace WizWork
{
    partial class DefectButton
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpButton = new System.Windows.Forms.TableLayoutPanel();
            this.rbnDefect = new System.Windows.Forms.RadioButton();
            this.btnQty = new System.Windows.Forms.Button();
            this.tlpButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpButton
            // 
            this.tlpButton.ColumnCount = 1;
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButton.Controls.Add(this.rbnDefect, 0, 0);
            this.tlpButton.Controls.Add(this.btnQty, 0, 1);
            this.tlpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButton.Location = new System.Drawing.Point(0, 0);
            this.tlpButton.Name = "tlpButton";
            this.tlpButton.RowCount = 2;
            this.tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpButton.Size = new System.Drawing.Size(150, 150);
            this.tlpButton.TabIndex = 2;
            // 
            // rbnDefect
            // 
            this.rbnDefect.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbnDefect.AutoCheck = false;
            this.rbnDefect.BackColor = System.Drawing.Color.LightSkyBlue;
            this.rbnDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbnDefect.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rbnDefect.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rbnDefect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnDefect.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.rbnDefect.ForeColor = System.Drawing.Color.White;
            this.rbnDefect.Location = new System.Drawing.Point(3, 3);
            this.rbnDefect.Name = "rbnDefect";
            this.rbnDefect.Size = new System.Drawing.Size(144, 99);
            this.rbnDefect.TabIndex = 205;
            this.rbnDefect.TabStop = true;
            this.rbnDefect.Tag = "2";
            this.rbnDefect.Text = "버튼~~";
            this.rbnDefect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbnDefect.UseVisualStyleBackColor = false;
            this.rbnDefect.Click += new System.EventHandler(this.OnClick);
            // 
            // btnQty
            // 
            this.btnQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQty.Font = new System.Drawing.Font("맑은 고딕", 16F);
            this.btnQty.Location = new System.Drawing.Point(3, 108);
            this.btnQty.Name = "btnQty";
            this.btnQty.Size = new System.Drawing.Size(144, 39);
            this.btnQty.TabIndex = 0;
            this.btnQty.Text = "12,400,400";
            this.btnQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQty.UseVisualStyleBackColor = true;
            this.btnQty.Click += new System.EventHandler(this.OnClick);
            // 
            // DefectButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpButton);
            this.Name = "DefectButton";
            this.Load += new System.EventHandler(this.DefectButton_Load);
            this.tlpButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpButton;
        private System.Windows.Forms.Button btnQty;
        private System.Windows.Forms.RadioButton rbnDefect;
    }
}
