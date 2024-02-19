namespace WizWork
{
    partial class frm_tprc_setProcess
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.grdAllPerson = new System.Windows.Forms.DataGridView();
            this.btnDel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabAllPerson = new System.Windows.Forms.TabPage();
            this.tabPersonByProcess = new System.Windows.Forms.TabPage();
            this.grdPersonByProcess = new System.Windows.Forms.DataGridView();
            this.lblChkMachine = new System.Windows.Forms.Label();
            this.cmdMachine = new System.Windows.Forms.Button();
            this.cboMachine = new System.Windows.Forms.ComboBox();
            this.Tab_Person = new System.Windows.Forms.TabControl();
            this.splPersonName = new System.Windows.Forms.Label();
            this.splPersonID = new System.Windows.Forms.Label();
            this.cmdPersonID = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdRowUp = new System.Windows.Forms.Button();
            this.cmdRowDown = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tlpProcess = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpForm = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpTeamPerson = new System.Windows.Forms.TableLayoutPanel();
            this.tlpTeam = new System.Windows.Forms.TableLayoutPanel();
            this.tlpDayOrNight = new System.Windows.Forms.TableLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlRight = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdAllPerson)).BeginInit();
            this.tabAllPerson.SuspendLayout();
            this.tabPersonByProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPersonByProcess)).BeginInit();
            this.Tab_Person.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tlpForm.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tlpTeamPerson.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(150, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(582, 35);
            this.label1.TabIndex = 150;
            this.label1.Text = "작업자 선택";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdAllPerson
            // 
            this.grdAllPerson.AllowUserToAddRows = false;
            this.grdAllPerson.AllowUserToDeleteRows = false;
            this.grdAllPerson.AllowUserToResizeRows = false;
            this.grdAllPerson.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdAllPerson.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdAllPerson.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdAllPerson.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdAllPerson.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdAllPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAllPerson.Location = new System.Drawing.Point(2, 2);
            this.grdAllPerson.MultiSelect = false;
            this.grdAllPerson.Name = "grdAllPerson";
            this.grdAllPerson.ReadOnly = true;
            this.grdAllPerson.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.grdAllPerson.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.grdAllPerson.RowTemplate.Height = 23;
            this.grdAllPerson.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdAllPerson.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdAllPerson.Size = new System.Drawing.Size(496, 137);
            this.grdAllPerson.TabIndex = 152;
            this.grdAllPerson.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAllPerson_CellClick);
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.LightSalmon;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btnDel.ForeColor = System.Drawing.Color.Black;
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDel.Location = new System.Drawing.Point(797, 3);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(83, 40);
            this.btnDel.TabIndex = 148;
            this.btnDel.Text = "삭제";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 35);
            this.label3.TabIndex = 151;
            this.label3.Text = "작업조 선택";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabAllPerson
            // 
            this.tabAllPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabAllPerson.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabAllPerson.Controls.Add(this.grdAllPerson);
            this.tabAllPerson.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.tabAllPerson.Location = new System.Drawing.Point(4, 4);
            this.tabAllPerson.Margin = new System.Windows.Forms.Padding(2);
            this.tabAllPerson.Name = "tabAllPerson";
            this.tabAllPerson.Padding = new System.Windows.Forms.Padding(2);
            this.tabAllPerson.Size = new System.Drawing.Size(504, 145);
            this.tabAllPerson.TabIndex = 1;
            this.tabAllPerson.Text = "                  전체 사원명단                   ";
            this.tabAllPerson.UseVisualStyleBackColor = true;
            // 
            // tabPersonByProcess
            // 
            this.tabPersonByProcess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPersonByProcess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPersonByProcess.Controls.Add(this.grdPersonByProcess);
            this.tabPersonByProcess.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.tabPersonByProcess.Location = new System.Drawing.Point(4, 4);
            this.tabPersonByProcess.Margin = new System.Windows.Forms.Padding(2);
            this.tabPersonByProcess.Name = "tabPersonByProcess";
            this.tabPersonByProcess.Padding = new System.Windows.Forms.Padding(2);
            this.tabPersonByProcess.Size = new System.Drawing.Size(504, 145);
            this.tabPersonByProcess.TabIndex = 0;
            this.tabPersonByProcess.Text = "                   공정별 사원명단                 ";
            this.tabPersonByProcess.UseVisualStyleBackColor = true;
            // 
            // grdPersonByProcess
            // 
            this.grdPersonByProcess.AllowUserToAddRows = false;
            this.grdPersonByProcess.AllowUserToDeleteRows = false;
            this.grdPersonByProcess.AllowUserToResizeRows = false;
            this.grdPersonByProcess.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdPersonByProcess.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPersonByProcess.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdPersonByProcess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdPersonByProcess.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdPersonByProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPersonByProcess.Location = new System.Drawing.Point(2, 2);
            this.grdPersonByProcess.MultiSelect = false;
            this.grdPersonByProcess.Name = "grdPersonByProcess";
            this.grdPersonByProcess.ReadOnly = true;
            this.grdPersonByProcess.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grdPersonByProcess.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.grdPersonByProcess.RowTemplate.Height = 23;
            this.grdPersonByProcess.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdPersonByProcess.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPersonByProcess.Size = new System.Drawing.Size(496, 137);
            this.grdPersonByProcess.TabIndex = 145;
            this.grdPersonByProcess.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPersonByProcess_CellClick);
            // 
            // lblChkMachine
            // 
            this.lblChkMachine.BackColor = System.Drawing.Color.Yellow;
            this.lblChkMachine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblChkMachine.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.lblChkMachine.ForeColor = System.Drawing.Color.Black;
            this.lblChkMachine.Location = new System.Drawing.Point(3, 96);
            this.lblChkMachine.Name = "lblChkMachine";
            this.lblChkMachine.Size = new System.Drawing.Size(97, 50);
            this.lblChkMachine.TabIndex = 152;
            this.lblChkMachine.Text = "호기를 선택하십시오";
            this.lblChkMachine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblChkMachine.Visible = false;
            // 
            // cmdMachine
            // 
            this.cmdMachine.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmdMachine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMachine.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.cmdMachine.ForeColor = System.Drawing.Color.Black;
            this.cmdMachine.Location = new System.Drawing.Point(3, 3);
            this.cmdMachine.Name = "cmdMachine";
            this.cmdMachine.Size = new System.Drawing.Size(97, 42);
            this.cmdMachine.TabIndex = 9;
            this.cmdMachine.Text = "호기 선택";
            this.cmdMachine.UseVisualStyleBackColor = false;
            // 
            // cboMachine
            // 
            this.cboMachine.BackColor = System.Drawing.SystemColors.Window;
            this.cboMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMachine.Font = new System.Drawing.Font("맑은 고딕", 22F, System.Drawing.FontStyle.Bold);
            this.cboMachine.FormattingEnabled = true;
            this.cboMachine.Location = new System.Drawing.Point(3, 51);
            this.cboMachine.MaxDropDownItems = 5;
            this.cboMachine.Name = "cboMachine";
            this.cboMachine.Size = new System.Drawing.Size(97, 48);
            this.cboMachine.TabIndex = 18;
            this.cboMachine.SelectedIndexChanged += new System.EventHandler(this.cboMachine_SelectedIndexChanged);
            // 
            // Tab_Person
            // 
            this.Tab_Person.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.Tab_Person.Controls.Add(this.tabPersonByProcess);
            this.Tab_Person.Controls.Add(this.tabAllPerson);
            this.Tab_Person.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.Tab_Person.Location = new System.Drawing.Point(222, 2);
            this.Tab_Person.Margin = new System.Windows.Forms.Padding(2);
            this.Tab_Person.Multiline = true;
            this.Tab_Person.Name = "Tab_Person";
            this.Tab_Person.SelectedIndex = 0;
            this.Tab_Person.Size = new System.Drawing.Size(512, 205);
            this.Tab_Person.TabIndex = 150;
            this.Tab_Person.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // splPersonName
            // 
            this.splPersonName.BackColor = System.Drawing.Color.White;
            this.splPersonName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splPersonName.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.splPersonName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splPersonName.Location = new System.Drawing.Point(488, 0);
            this.splPersonName.Name = "splPersonName";
            this.splPersonName.Size = new System.Drawing.Size(260, 41);
            this.splPersonName.TabIndex = 151;
            this.splPersonName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splPersonID
            // 
            this.splPersonID.BackColor = System.Drawing.Color.White;
            this.splPersonID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splPersonID.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.splPersonID.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splPersonID.Location = new System.Drawing.Point(179, 0);
            this.splPersonID.Name = "splPersonID";
            this.splPersonID.Size = new System.Drawing.Size(303, 41);
            this.splPersonID.TabIndex = 21;
            this.splPersonID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdPersonID
            // 
            this.cmdPersonID.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdPersonID.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdPersonID.ForeColor = System.Drawing.Color.Black;
            this.cmdPersonID.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdPersonID.Location = new System.Drawing.Point(3, 3);
            this.cmdPersonID.Name = "cmdPersonID";
            this.cmdPersonID.Size = new System.Drawing.Size(170, 40);
            this.cmdPersonID.TabIndex = 145;
            this.cmdPersonID.Text = "사원번호 입력";
            this.cmdPersonID.UseVisualStyleBackColor = true;
            this.cmdPersonID.Click += new System.EventHandler(this.cmdPersonID_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.cmdRowUp, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdRowDown, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmdSave, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmdExit, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 14);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(87, 310);
            this.tableLayoutPanel1.TabIndex = 186;
            // 
            // cmdRowUp
            // 
            this.cmdRowUp.BackColor = System.Drawing.SystemColors.Control;
            this.cmdRowUp.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdRowUp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdRowUp.Image = global::WizWork.Properties.Resources.up_arrow__1_;
            this.cmdRowUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRowUp.Location = new System.Drawing.Point(3, 3);
            this.cmdRowUp.Name = "cmdRowUp";
            this.cmdRowUp.Size = new System.Drawing.Size(81, 62);
            this.cmdRowUp.TabIndex = 177;
            this.cmdRowUp.Text = "위  ";
            this.cmdRowUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRowUp.UseVisualStyleBackColor = true;
            this.cmdRowUp.Click += new System.EventHandler(this.cmdRowUp_Click);
            // 
            // cmdRowDown
            // 
            this.cmdRowDown.BackColor = System.Drawing.SystemColors.Control;
            this.cmdRowDown.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.cmdRowDown.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdRowDown.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdRowDown.Image = global::WizWork.Properties.Resources.down_arrow;
            this.cmdRowDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRowDown.Location = new System.Drawing.Point(3, 80);
            this.cmdRowDown.Name = "cmdRowDown";
            this.cmdRowDown.Size = new System.Drawing.Size(81, 56);
            this.cmdRowDown.TabIndex = 176;
            this.cmdRowDown.Text = "아래";
            this.cmdRowDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRowDown.UseVisualStyleBackColor = true;
            this.cmdRowDown.Click += new System.EventHandler(this.cmdRowDown_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.cmdSave.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdSave.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdSave.Image = global::WizWork.Properties.Resources.pen_3;
            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSave.Location = new System.Drawing.Point(3, 157);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(81, 60);
            this.cmdSave.TabIndex = 178;
            this.cmdSave.Text = "선택";
            this.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.SystemColors.Control;
            this.cmdExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.cmdExit.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdExit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExit.Image = global::WizWork.Properties.Resources.enter;
            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExit.Location = new System.Drawing.Point(3, 234);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(81, 59);
            this.cmdExit.TabIndex = 179;
            this.cmdExit.Text = "취소";
            this.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.tableLayoutPanel6);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(680, 198);
            this.panel3.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.panel5, 1, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 5);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(672, 173);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tlpProcess);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(528, 154);
            this.panel2.TabIndex = 0;
            // 
            // tlpProcess
            // 
            this.tlpProcess.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpProcess.ColumnCount = 2;
            this.tlpProcess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcess.Location = new System.Drawing.Point(9, 3);
            this.tlpProcess.Name = "tlpProcess";
            this.tlpProcess.RowCount = 2;
            this.tlpProcess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcess.Size = new System.Drawing.Size(224, 146);
            this.tlpProcess.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.tableLayoutPanel7);
            this.panel5.Location = new System.Drawing.Point(540, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(129, 154);
            this.panel5.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.lblChkMachine, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.cmdMachine, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cboMachine, 0, 1);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(7, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.4F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(103, 146);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // tlpForm
            // 
            this.tlpForm.ColumnCount = 1;
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForm.Controls.Add(this.panel3, 0, 0);
            this.tlpForm.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tlpForm.Location = new System.Drawing.Point(12, 6);
            this.tlpForm.Name = "tlpForm";
            this.tlpForm.RowCount = 2;
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpForm.Size = new System.Drawing.Size(977, 583);
            this.tlpForm.TabIndex = 185;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel5.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.pnlRight, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 207);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(948, 373);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.tableLayoutPanel4);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(770, 308);
            this.panel4.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.tlpTeamPerson, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.panel8, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel7, 0, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(750, 300);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tlpTeamPerson
            // 
            this.tlpTeamPerson.ColumnCount = 3;
            this.tlpTeamPerson.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpTeamPerson.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpTeamPerson.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpTeamPerson.Controls.Add(this.tlpTeam, 1, 0);
            this.tlpTeamPerson.Controls.Add(this.Tab_Person, 2, 0);
            this.tlpTeamPerson.Controls.Add(this.tlpDayOrNight, 0, 0);
            this.tlpTeamPerson.Location = new System.Drawing.Point(3, 63);
            this.tlpTeamPerson.Name = "tlpTeamPerson";
            this.tlpTeamPerson.RowCount = 1;
            this.tlpTeamPerson.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeamPerson.Size = new System.Drawing.Size(736, 223);
            this.tlpTeamPerson.TabIndex = 191;
            // 
            // tlpTeam
            // 
            this.tlpTeam.ColumnCount = 1;
            this.tlpTeam.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeam.Location = new System.Drawing.Point(113, 3);
            this.tlpTeam.Name = "tlpTeam";
            this.tlpTeam.RowCount = 1;
            this.tlpTeam.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeam.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tlpTeam.Size = new System.Drawing.Size(75, 102);
            this.tlpTeam.TabIndex = 190;
            // 
            // tlpDayOrNight
            // 
            this.tlpDayOrNight.ColumnCount = 1;
            this.tlpDayOrNight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDayOrNight.Location = new System.Drawing.Point(3, 3);
            this.tlpDayOrNight.Name = "tlpDayOrNight";
            this.tlpDayOrNight.RowCount = 1;
            this.tlpDayOrNight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDayOrNight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tlpDayOrNight.Size = new System.Drawing.Size(75, 102);
            this.tlpDayOrNight.TabIndex = 190;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.tableLayoutPanel2);
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(744, 24);
            this.panel8.TabIndex = 189;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.splPersonName, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmdPersonID, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDel, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.splPersonID, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(883, 56);
            this.tableLayoutPanel2.TabIndex = 186;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.tableLayoutPanel3);
            this.panel7.Location = new System.Drawing.Point(3, 33);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(744, 24);
            this.panel7.TabIndex = 188;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(735, 39);
            this.tableLayoutPanel3.TabIndex = 187;
            // 
            // pnlRight
            // 
            this.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRight.Controls.Add(this.tableLayoutPanel1);
            this.pnlRight.Location = new System.Drawing.Point(808, 3);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(91, 346);
            this.pnlRight.TabIndex = 2;
            // 
            // frm_tprc_setProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 596);
            this.Controls.Add(this.tlpForm);
            this.MaximumSize = new System.Drawing.Size(1028, 635);
            this.MinimumSize = new System.Drawing.Size(1028, 635);
            this.Name = "frm_tprc_setProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "공정 및 호기 설정";
            this.Load += new System.EventHandler(this.frm_tprc_setProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdAllPerson)).EndInit();
            this.tabAllPerson.ResumeLayout(false);
            this.tabPersonByProcess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPersonByProcess)).EndInit();
            this.Tab_Person.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tlpForm.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tlpTeamPerson.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grdAllPerson;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabAllPerson;
        private System.Windows.Forms.TabPage tabPersonByProcess;
        private System.Windows.Forms.Label lblChkMachine;
        private System.Windows.Forms.Button cmdMachine;
        private System.Windows.Forms.ComboBox cboMachine;
        private System.Windows.Forms.TabControl Tab_Person;
        private System.Windows.Forms.Button cmdPersonID;
        private System.Windows.Forms.Label splPersonID;
        private System.Windows.Forms.Label splPersonName;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button cmdRowUp;
        private System.Windows.Forms.Button cmdRowDown;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tlpForm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TableLayoutPanel tlpTeam;
        private System.Windows.Forms.TableLayoutPanel tlpTeamPerson;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tlpProcess;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.DataGridView grdPersonByProcess;
        private System.Windows.Forms.TableLayoutPanel tlpDayOrNight;
    }
}