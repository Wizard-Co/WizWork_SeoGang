namespace WizWork
{
    partial class FrmSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOK = new System.Windows.Forms.Button();
            this.cmdRowDown = new System.Windows.Forms.Button();
            this.cmdRowUp = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tlpBottomRight = new System.Windows.Forms.TableLayoutPanel();
            this.btnTest = new System.Windows.Forms.Button();
            this.tlpForm = new System.Windows.Forms.TableLayoutPanel();
            this.grdInstDate = new System.Windows.Forms.DataGridView();
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboComport = new System.Windows.Forms.ComboBox();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.cmdSQL_0 = new System.Windows.Forms.Button();
            this.cmdSQL_1 = new System.Windows.Forms.Button();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
            this.tabProMacTerm = new System.Windows.Forms.TabControl();
            this.tabPro = new System.Windows.Forms.TabPage();
            this.grdProcess = new System.Windows.Forms.DataGridView();
            this.tabMac = new System.Windows.Forms.TabPage();
            this.grdMachine = new System.Windows.Forms.DataGridView();
            this.tlpBottomRight.SuspendLayout();
            this.tlpForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdInstDate)).BeginInit();
            this.tlpTop.SuspendLayout();
            this.tlpBottom.SuspendLayout();
            this.tabProMacTerm.SuspendLayout();
            this.tabPro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProcess)).BeginInit();
            this.tabMac.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMachine)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnOK.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btnOK.Image = global::WizWork.Properties.Resources.completed_tasks;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(3, 143);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(105, 63);
            this.btnOK.TabIndex = 162;
            this.btnOK.Text = "선택";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmdRowDown
            // 
            this.cmdRowDown.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdRowDown.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.cmdRowDown.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdRowDown.Image = global::WizWork.Properties.Resources.down_arrow;
            this.cmdRowDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRowDown.Location = new System.Drawing.Point(3, 73);
            this.cmdRowDown.Name = "cmdRowDown";
            this.cmdRowDown.Size = new System.Drawing.Size(105, 63);
            this.cmdRowDown.TabIndex = 160;
            this.cmdRowDown.Text = "아래";
            this.cmdRowDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRowDown.UseVisualStyleBackColor = true;
            this.cmdRowDown.Click += new System.EventHandler(this.cmdRowDown_Click);
            // 
            // cmdRowUp
            // 
            this.cmdRowUp.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdRowUp.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.cmdRowUp.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdRowUp.Image = global::WizWork.Properties.Resources.up_arrow__1_;
            this.cmdRowUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRowUp.Location = new System.Drawing.Point(3, 3);
            this.cmdRowUp.Name = "cmdRowUp";
            this.cmdRowUp.Size = new System.Drawing.Size(105, 63);
            this.cmdRowUp.TabIndex = 161;
            this.cmdRowUp.Text = "위  ";
            this.cmdRowUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRowUp.UseVisualStyleBackColor = true;
            this.cmdRowUp.Click += new System.EventHandler(this.cmdRowUp_Click_1);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSalmon;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btnClose.Image = global::WizWork.Properties.Resources.enter;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(3, 213);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 63);
            this.btnClose.TabIndex = 166;
            this.btnClose.Text = "닫기";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tlpBottomRight
            // 
            this.tlpBottomRight.ColumnCount = 1;
            this.tlpBottomRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottomRight.Controls.Add(this.btnTest, 0, 4);
            this.tlpBottomRight.Controls.Add(this.cmdRowUp, 0, 0);
            this.tlpBottomRight.Controls.Add(this.cmdRowDown, 0, 1);
            this.tlpBottomRight.Controls.Add(this.btnClose, 0, 3);
            this.tlpBottomRight.Controls.Add(this.btnOK, 0, 2);
            this.tlpBottomRight.Location = new System.Drawing.Point(498, 3);
            this.tlpBottomRight.Name = "tlpBottomRight";
            this.tlpBottomRight.RowCount = 5;
            this.tlpBottomRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpBottomRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpBottomRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpBottomRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpBottomRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpBottomRight.Size = new System.Drawing.Size(118, 350);
            this.tlpBottomRight.TabIndex = 171;
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.LightSalmon;
            this.btnTest.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnTest.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTest.Location = new System.Drawing.Point(3, 283);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(105, 63);
            this.btnTest.TabIndex = 167;
            this.btnTest.Text = "평량작업용버튼";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tlpForm
            // 
            this.tlpForm.ColumnCount = 1;
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForm.Controls.Add(this.grdInstDate, 0, 2);
            this.tlpForm.Controls.Add(this.tlpTop, 0, 0);
            this.tlpForm.Controls.Add(this.tlpBottom, 0, 1);
            this.tlpForm.Location = new System.Drawing.Point(5, -1);
            this.tlpForm.Name = "tlpForm";
            this.tlpForm.RowCount = 3;
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpForm.Size = new System.Drawing.Size(635, 519);
            this.tlpForm.TabIndex = 172;
            // 
            // grdInstDate
            // 
            this.grdInstDate.AllowUserToAddRows = false;
            this.grdInstDate.AllowUserToDeleteRows = false;
            this.grdInstDate.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 20F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdInstDate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdInstDate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 20F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdInstDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdInstDate.Location = new System.Drawing.Point(3, 443);
            this.grdInstDate.MultiSelect = false;
            this.grdInstDate.Name = "grdInstDate";
            this.grdInstDate.ReadOnly = true;
            this.grdInstDate.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grdInstDate.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.grdInstDate.RowTemplate.Height = 23;
            this.grdInstDate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdInstDate.Size = new System.Drawing.Size(556, 47);
            this.grdInstDate.TabIndex = 173;
            this.grdInstDate.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdInstDate_CellClick);
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 3;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTop.Controls.Add(this.label1, 2, 0);
            this.tlpTop.Controls.Add(this.cboComport, 2, 1);
            this.tlpTop.Controls.Add(this.txtDB, 1, 1);
            this.tlpTop.Controls.Add(this.cmdSQL_0, 0, 0);
            this.tlpTop.Controls.Add(this.cmdSQL_1, 0, 1);
            this.tlpTop.Controls.Add(this.txtServer, 1, 0);
            this.tlpTop.Location = new System.Drawing.Point(3, 3);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 2;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTop.Size = new System.Drawing.Size(588, 71);
            this.tlpTop.TabIndex = 171;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.label1.Location = new System.Drawing.Point(443, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 35);
            this.label1.TabIndex = 171;
            this.label1.Text = "Comport설정";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboComport
            // 
            this.cboComport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboComport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComport.Font = new System.Drawing.Font("맑은 고딕", 22F);
            this.cboComport.FormattingEnabled = true;
            this.cboComport.Location = new System.Drawing.Point(443, 38);
            this.cboComport.Name = "cboComport";
            this.cboComport.Size = new System.Drawing.Size(142, 48);
            this.cboComport.TabIndex = 169;
            // 
            // txtDB
            // 
            this.txtDB.Font = new System.Drawing.Font("맑은 고딕", 22F, System.Drawing.FontStyle.Bold);
            this.txtDB.Location = new System.Drawing.Point(120, 38);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(222, 47);
            this.txtDB.TabIndex = 169;
            // 
            // cmdSQL_0
            // 
            this.cmdSQL_0.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdSQL_0.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdSQL_0.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdSQL_0.Location = new System.Drawing.Point(3, 3);
            this.cmdSQL_0.Name = "cmdSQL_0";
            this.cmdSQL_0.Size = new System.Drawing.Size(109, 29);
            this.cmdSQL_0.TabIndex = 145;
            this.cmdSQL_0.Text = "Server";
            this.cmdSQL_0.UseVisualStyleBackColor = true;
            this.cmdSQL_0.Click += new System.EventHandler(this.cmdSQL_0_Click);
            // 
            // cmdSQL_1
            // 
            this.cmdSQL_1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdSQL_1.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdSQL_1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdSQL_1.Location = new System.Drawing.Point(3, 38);
            this.cmdSQL_1.Name = "cmdSQL_1";
            this.cmdSQL_1.Size = new System.Drawing.Size(109, 30);
            this.cmdSQL_1.TabIndex = 164;
            this.cmdSQL_1.Text = "Database";
            this.cmdSQL_1.UseVisualStyleBackColor = true;
            this.cmdSQL_1.Click += new System.EventHandler(this.cmdSQL_1_Click);
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("맑은 고딕", 22F, System.Drawing.FontStyle.Bold);
            this.txtServer.Location = new System.Drawing.Point(120, 3);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(222, 47);
            this.txtServer.TabIndex = 167;
            // 
            // tlpBottom
            // 
            this.tlpBottom.ColumnCount = 2;
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpBottom.Controls.Add(this.tlpBottomRight, 1, 0);
            this.tlpBottom.Controls.Add(this.tabProMacTerm, 0, 0);
            this.tlpBottom.Location = new System.Drawing.Point(3, 80);
            this.tlpBottom.Name = "tlpBottom";
            this.tlpBottom.RowCount = 1;
            this.tlpBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.Size = new System.Drawing.Size(619, 357);
            this.tlpBottom.TabIndex = 172;
            // 
            // tabProMacTerm
            // 
            this.tabProMacTerm.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabProMacTerm.Controls.Add(this.tabPro);
            this.tabProMacTerm.Controls.Add(this.tabMac);
            this.tabProMacTerm.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.tabProMacTerm.Location = new System.Drawing.Point(3, 3);
            this.tabProMacTerm.Name = "tabProMacTerm";
            this.tabProMacTerm.SelectedIndex = 0;
            this.tabProMacTerm.Size = new System.Drawing.Size(489, 350);
            this.tabProMacTerm.TabIndex = 231;
            this.tabProMacTerm.SelectedIndexChanged += new System.EventHandler(this.tabProMac_SelectedIndexChanged);
            // 
            // tabPro
            // 
            this.tabPro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPro.Controls.Add(this.grdProcess);
            this.tabPro.Font = new System.Drawing.Font("맑은 고딕", 20F);
            this.tabPro.Location = new System.Drawing.Point(4, 4);
            this.tabPro.Name = "tabPro";
            this.tabPro.Padding = new System.Windows.Forms.Padding(3);
            this.tabPro.Size = new System.Drawing.Size(481, 309);
            this.tabPro.TabIndex = 0;
            this.tabPro.Text = "    공정  설정    ";
            this.tabPro.UseVisualStyleBackColor = true;
            // 
            // grdProcess
            // 
            this.grdProcess.AllowUserToAddRows = false;
            this.grdProcess.AllowUserToDeleteRows = false;
            this.grdProcess.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 20F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdProcess.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdProcess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 20F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdProcess.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdProcess.Location = new System.Drawing.Point(-2, -5);
            this.grdProcess.MultiSelect = false;
            this.grdProcess.Name = "grdProcess";
            this.grdProcess.ReadOnly = true;
            this.grdProcess.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grdProcess.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.grdProcess.RowTemplate.Height = 23;
            this.grdProcess.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdProcess.Size = new System.Drawing.Size(530, 290);
            this.grdProcess.TabIndex = 157;
            this.grdProcess.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellClick);
            // 
            // tabMac
            // 
            this.tabMac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabMac.Controls.Add(this.grdMachine);
            this.tabMac.Font = new System.Drawing.Font("맑은 고딕", 20F);
            this.tabMac.Location = new System.Drawing.Point(4, 4);
            this.tabMac.Name = "tabMac";
            this.tabMac.Padding = new System.Windows.Forms.Padding(3);
            this.tabMac.Size = new System.Drawing.Size(481, 309);
            this.tabMac.TabIndex = 1;
            this.tabMac.Text = "    호기  설정    ";
            this.tabMac.UseVisualStyleBackColor = true;
            // 
            // grdMachine
            // 
            this.grdMachine.AllowUserToAddRows = false;
            this.grdMachine.AllowUserToDeleteRows = false;
            this.grdMachine.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("맑은 고딕", 20F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdMachine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grdMachine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("맑은 고딕", 20F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdMachine.DefaultCellStyle = dataGridViewCellStyle8;
            this.grdMachine.Location = new System.Drawing.Point(-2, -2);
            this.grdMachine.MultiSelect = false;
            this.grdMachine.Name = "grdMachine";
            this.grdMachine.ReadOnly = true;
            this.grdMachine.RowHeadersVisible = false;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grdMachine.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.grdMachine.RowTemplate.Height = 23;
            this.grdMachine.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdMachine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdMachine.Size = new System.Drawing.Size(533, 290);
            this.grdMachine.TabIndex = 231;
            this.grdMachine.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellClick);
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(647, 558);
            this.Controls.Add(this.tlpForm);
            this.Name = "FrmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "공정 및 호기 설정";
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            this.tlpBottomRight.ResumeLayout(false);
            this.tlpForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdInstDate)).EndInit();
            this.tlpTop.ResumeLayout(false);
            this.tlpTop.PerformLayout();
            this.tlpBottom.ResumeLayout(false);
            this.tabProMacTerm.ResumeLayout(false);
            this.tabPro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProcess)).EndInit();
            this.tabMac.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMachine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button cmdRowDown;
        private System.Windows.Forms.Button cmdRowUp;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tlpBottomRight;
        private System.Windows.Forms.TableLayoutPanel tlpForm;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.ComboBox cboComport;
        private System.Windows.Forms.Button cmdSQL_0;
        private System.Windows.Forms.Button cmdSQL_1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TableLayoutPanel tlpBottom;
        private System.Windows.Forms.TabControl tabProMacTerm;
        private System.Windows.Forms.TabPage tabPro;
        private System.Windows.Forms.DataGridView grdProcess;
        private System.Windows.Forms.TabPage tabMac;
        private System.Windows.Forms.DataGridView grdMachine;
        private System.Windows.Forms.DataGridView grdInstDate;
        private System.Windows.Forms.Button btnTest;
    }
}

