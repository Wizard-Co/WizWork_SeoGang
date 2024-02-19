namespace WizWork
{
    partial class frm_tprc_Work_Defect_U
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
            this.components = new System.ComponentModel.Container();
            this.cmdInit = new System.Windows.Forms.Button();
            this.cmdQuit = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdDefect = new System.Windows.Forms.Button();
            this.panelDefect = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.cmdClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNow = new System.Windows.Forms.Label();
            this.lblNowQty = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDefect = new System.Windows.Forms.Label();
            this.lblDefectQty = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3.SuspendLayout();
            this.tlpRight.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdInit
            // 
            this.cmdInit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmdInit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdInit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdInit.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdInit.Location = new System.Drawing.Point(3, 89);
            this.cmdInit.Name = "cmdInit";
            this.cmdInit.Size = new System.Drawing.Size(128, 80);
            this.cmdInit.TabIndex = 156;
            this.cmdInit.Text = "초기화";
            this.cmdInit.UseVisualStyleBackColor = false;
            this.cmdInit.Click += new System.EventHandler(this.cmdInit_Click);
            // 
            // cmdQuit
            // 
            this.cmdQuit.BackColor = System.Drawing.Color.LightSalmon;
            this.cmdQuit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdQuit.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdQuit.Location = new System.Drawing.Point(6, 597);
            this.cmdQuit.Name = "cmdQuit";
            this.cmdQuit.Size = new System.Drawing.Size(105, 63);
            this.cmdQuit.TabIndex = 161;
            this.cmdQuit.Text = "취  소";
            this.cmdQuit.UseVisualStyleBackColor = false;
            this.cmdQuit.Click += new System.EventHandler(this.cmdQuit_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOK.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdOK.Location = new System.Drawing.Point(3, 519);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(128, 80);
            this.cmdOK.TabIndex = 162;
            this.cmdOK.Text = "검사완료";
            this.cmdOK.UseVisualStyleBackColor = false;
            this.cmdOK.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 14F);
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(3, 673);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(421, 24);
            this.label2.TabIndex = 185;
            this.label2.Text = "☞ 생산에서 실적 등록시에만 불량 저장 됩니다.";
            // 
            // cmdDefect
            // 
            this.cmdDefect.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmdDefect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdDefect.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdDefect.Location = new System.Drawing.Point(3, 175);
            this.cmdDefect.Name = "cmdDefect";
            this.cmdDefect.Size = new System.Drawing.Size(128, 80);
            this.cmdDefect.TabIndex = 165;
            this.cmdDefect.Text = "불량정정";
            this.cmdDefect.UseVisualStyleBackColor = false;
            this.cmdDefect.Click += new System.EventHandler(this.cmdDefect_Click);
            // 
            // panelDefect
            // 
            this.panelDefect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDefect.Location = new System.Drawing.Point(3, 3);
            this.panelDefect.Name = "panelDefect";
            this.panelDefect.Size = new System.Drawing.Size(797, 694);
            this.panelDefect.TabIndex = 187;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.tlpRight);
            this.panel3.Controls.Add(this.cmdQuit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(806, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(136, 694);
            this.panel3.TabIndex = 188;
            // 
            // tlpRight
            // 
            this.tlpRight.ColumnCount = 1;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.Controls.Add(this.cmdClose, 0, 7);
            this.tlpRight.Controls.Add(this.tableLayoutPanel2, 0, 4);
            this.tlpRight.Controls.Add(this.tableLayoutPanel1, 0, 3);
            this.tlpRight.Controls.Add(this.cmdOK, 0, 6);
            this.tlpRight.Controls.Add(this.cmdDefect, 0, 2);
            this.tlpRight.Controls.Add(this.cmdInit, 0, 1);
            this.tlpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRight.Location = new System.Drawing.Point(0, 0);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 8;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRight.Size = new System.Drawing.Size(134, 692);
            this.tlpRight.TabIndex = 5;
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmdClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdClose.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdClose.Location = new System.Drawing.Point(3, 605);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(128, 84);
            this.cmdClose.TabIndex = 166;
            this.cmdClose.Text = "닫기";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblNow, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblNowQty, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 347);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(128, 80);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // lblNow
            // 
            this.lblNow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblNow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNow.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblNow.Location = new System.Drawing.Point(3, 0);
            this.lblNow.Name = "lblNow";
            this.lblNow.Size = new System.Drawing.Size(122, 40);
            this.lblNow.TabIndex = 169;
            this.lblNow.Text = "현재수량";
            this.lblNow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNowQty
            // 
            this.lblNowQty.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblNowQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNowQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNowQty.Font = new System.Drawing.Font("맑은 고딕", 19F, System.Drawing.FontStyle.Bold);
            this.lblNowQty.Location = new System.Drawing.Point(3, 40);
            this.lblNowQty.Name = "lblNowQty";
            this.lblNowQty.Size = new System.Drawing.Size(122, 40);
            this.lblNowQty.TabIndex = 168;
            this.lblNowQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblDefect, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDefectQty, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 261);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(128, 80);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // lblDefect
            // 
            this.lblDefect.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblDefect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDefect.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblDefect.Location = new System.Drawing.Point(3, 0);
            this.lblDefect.Name = "lblDefect";
            this.lblDefect.Size = new System.Drawing.Size(122, 40);
            this.lblDefect.TabIndex = 167;
            this.lblDefect.Text = "불량수량";
            this.lblDefect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDefect.Visible = false;
            // 
            // lblDefectQty
            // 
            this.lblDefectQty.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDefectQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDefectQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDefectQty.Font = new System.Drawing.Font("맑은 고딕", 19F, System.Drawing.FontStyle.Bold);
            this.lblDefectQty.Location = new System.Drawing.Point(3, 40);
            this.lblDefectQty.Name = "lblDefectQty";
            this.lblDefectQty.Size = new System.Drawing.Size(122, 40);
            this.lblDefectQty.TabIndex = 166;
            this.lblDefectQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDefectQty.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panelDefect, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(945, 700);
            this.tableLayoutPanel3.TabIndex = 189;
            // 
            // frm_tprc_Work_Defect_U
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(945, 700);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_tprc_Work_Defect_U";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "생산불량 등록";
            this.Load += new System.EventHandler(this.frm_tprc_Work_Defect_U_Load);
            this.panel3.ResumeLayout(false);
            this.tlpRight.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdInit;

        private System.Windows.Forms.Button cmdQuit;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdDefect;
        private System.Windows.Forms.Panel panelDefect;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblNow;
        private System.Windows.Forms.Label lblNowQty;
        private System.Windows.Forms.Label lblDefect;
        private System.Windows.Forms.Label lblDefectQty;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}

