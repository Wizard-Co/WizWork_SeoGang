namespace WizWork
{
    partial class Frm_tprc_Result
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cboProcess = new System.Windows.Forms.ComboBox();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboJobGbn = new System.Windows.Forms.ComboBox();
            this.cboMachine = new System.Windows.Forms.ComboBox();
            this.cboTeam = new System.Windows.Forms.ComboBox();
            this.grdSum = new System.Windows.Forms.DataGridView();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.btnColRight = new System.Windows.Forms.Button();
            this.btnColLeft = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLookup = new System.Windows.Forms.Button();
            this.cmdRowUp = new System.Windows.Forms.Button();
            this.cmdRowDown = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tlpForm = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.tlp_Search_Date = new System.Windows.Forms.TableLayoutPanel();
            this.chkResultDate = new System.Windows.Forms.CheckBox();
            this.tlp_From = new System.Windows.Forms.TableLayoutPanel();
            this.btnCal_From = new System.Windows.Forms.Button();
            this.mtb_From = new System.Windows.Forms.MaskedTextBox();
            this.tlp_To = new System.Windows.Forms.TableLayoutPanel();
            this.btnCal_To = new System.Windows.Forms.Button();
            this.mtb_To = new System.Windows.Forms.MaskedTextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtBuyerArticleNo = new System.Windows.Forms.TextBox();
            this.chkBuyerArticleNo = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPLotID = new System.Windows.Forms.TextBox();
            this.chkPLotID = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdSum)).BeginInit();
            this.tlpRight.SuspendLayout();
            this.tlpForm.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.tlp_Search_Date.SuspendLayout();
            this.tlp_From.SuspendLayout();
            this.tlp_To.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            // 
            // cboProcess
            // 
            this.cboProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProcess.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.cboProcess.FormattingEnabled = true;
            this.cboProcess.IntegralHeight = false;
            this.cboProcess.Location = new System.Drawing.Point(185, 1);
            this.cboProcess.Margin = new System.Windows.Forms.Padding(1);
            this.cboProcess.MaxDropDownItems = 99;
            this.cboProcess.MinimumSize = new System.Drawing.Size(100, 0);
            this.cboProcess.Name = "cboProcess";
            this.cboProcess.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboProcess.Size = new System.Drawing.Size(182, 40);
            this.cboProcess.TabIndex = 165;
            this.cboProcess.SelectedIndexChanged += new System.EventHandler(this.cboProcess_SelectedIndexChanged);
            this.cboProcess.Click += new System.EventHandler(this.cboProcess_Click);
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "검사ID";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 2;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 2;
            // 
            // cboJobGbn
            // 
            this.cboJobGbn.AutoCompleteCustomSource.AddRange(new string[] {
            "전체"});
            this.cboJobGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJobGbn.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.cboJobGbn.FormattingEnabled = true;
            this.cboJobGbn.Items.AddRange(new object[] {
            "전체",
            "정상",
            "무작업",
            "재작업"});
            this.cboJobGbn.Location = new System.Drawing.Point(1, 1);
            this.cboJobGbn.Margin = new System.Windows.Forms.Padding(1);
            this.cboJobGbn.MaxDropDownItems = 99;
            this.cboJobGbn.MinimumSize = new System.Drawing.Size(100, 0);
            this.cboJobGbn.Name = "cboJobGbn";
            this.cboJobGbn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboJobGbn.Size = new System.Drawing.Size(182, 40);
            this.cboJobGbn.TabIndex = 166;
            // 
            // cboMachine
            // 
            this.cboMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMachine.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.cboMachine.FormattingEnabled = true;
            this.cboMachine.Location = new System.Drawing.Point(1, 1);
            this.cboMachine.Margin = new System.Windows.Forms.Padding(1);
            this.cboMachine.MaxDropDownItems = 99;
            this.cboMachine.MinimumSize = new System.Drawing.Size(100, 0);
            this.cboMachine.Name = "cboMachine";
            this.cboMachine.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboMachine.Size = new System.Drawing.Size(182, 40);
            this.cboMachine.TabIndex = 167;
            // 
            // cboTeam
            // 
            this.cboTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTeam.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.cboTeam.FormattingEnabled = true;
            this.cboTeam.Items.AddRange(new object[] {
            "전체"});
            this.cboTeam.Location = new System.Drawing.Point(185, 1);
            this.cboTeam.Margin = new System.Windows.Forms.Padding(1);
            this.cboTeam.MaxDropDownItems = 99;
            this.cboTeam.MinimumSize = new System.Drawing.Size(100, 0);
            this.cboTeam.Name = "cboTeam";
            this.cboTeam.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboTeam.Size = new System.Drawing.Size(182, 40);
            this.cboTeam.TabIndex = 172;
            // 
            // grdSum
            // 
            this.grdSum.AllowUserToAddRows = false;
            this.grdSum.AllowUserToDeleteRows = false;
            this.grdSum.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdSum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSum.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdSum.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdSum.Location = new System.Drawing.Point(3, 496);
            this.grdSum.Name = "grdSum";
            this.grdSum.ReadOnly = true;
            this.grdSum.RowHeadersVisible = false;
            this.grdSum.RowTemplate.Height = 30;
            this.grdSum.Size = new System.Drawing.Size(736, 30);
            this.grdSum.TabIndex = 228;
            // 
            // tlpRight
            // 
            this.tlpRight.ColumnCount = 1;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.Controls.Add(this.btnColRight, 0, 5);
            this.tlpRight.Controls.Add(this.btnColLeft, 0, 4);
            this.tlpRight.Controls.Add(this.btnClose, 0, 6);
            this.tlpRight.Controls.Add(this.btnLookup, 0, 0);
            this.tlpRight.Controls.Add(this.cmdRowUp, 0, 1);
            this.tlpRight.Controls.Add(this.cmdRowDown, 0, 2);
            this.tlpRight.Controls.Add(this.btnDelete, 0, 3);
            this.tlpRight.Location = new System.Drawing.Point(751, 3);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 7;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRight.Size = new System.Drawing.Size(113, 532);
            this.tlpRight.TabIndex = 229;
            // 
            // btnColRight
            // 
            this.btnColRight.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnColRight.Image = global::WizWork.Properties.Resources.right;
            this.btnColRight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnColRight.Location = new System.Drawing.Point(2, 377);
            this.btnColRight.Margin = new System.Windows.Forms.Padding(2);
            this.btnColRight.Name = "btnColRight";
            this.btnColRight.Size = new System.Drawing.Size(109, 71);
            this.btnColRight.TabIndex = 231;
            this.btnColRight.Text = "우";
            this.btnColRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnColRight.UseVisualStyleBackColor = true;
            this.btnColRight.Click += new System.EventHandler(this.btnColRight_Click);
            // 
            // btnColLeft
            // 
            this.btnColLeft.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnColLeft.Image = global::WizWork.Properties.Resources.left__3_;
            this.btnColLeft.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnColLeft.Location = new System.Drawing.Point(2, 302);
            this.btnColLeft.Margin = new System.Windows.Forms.Padding(2);
            this.btnColLeft.Name = "btnColLeft";
            this.btnColLeft.Size = new System.Drawing.Size(109, 71);
            this.btnColLeft.TabIndex = 231;
            this.btnColLeft.Text = "좌";
            this.btnColLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnColLeft.UseVisualStyleBackColor = true;
            this.btnColLeft.Click += new System.EventHandler(this.btnColLeft_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.Image = global::WizWork.Properties.Resources.enter;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(2, 452);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 78);
            this.btnClose.TabIndex = 197;
            this.btnClose.Text = "닫기";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLookup
            // 
            this.btnLookup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLookup.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLookup.Image = global::WizWork.Properties.Resources.icons8_search_48;
            this.btnLookup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLookup.Location = new System.Drawing.Point(2, 2);
            this.btnLookup.Margin = new System.Windows.Forms.Padding(2);
            this.btnLookup.Name = "btnLookup";
            this.btnLookup.Size = new System.Drawing.Size(109, 71);
            this.btnLookup.TabIndex = 139;
            this.btnLookup.Text = "조회";
            this.btnLookup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLookup.UseVisualStyleBackColor = true;
            this.btnLookup.Click += new System.EventHandler(this.btnLookup_Click);
            // 
            // cmdRowUp
            // 
            this.cmdRowUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdRowUp.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdRowUp.Image = global::WizWork.Properties.Resources.up_arrow__1_;
            this.cmdRowUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRowUp.Location = new System.Drawing.Point(2, 77);
            this.cmdRowUp.Margin = new System.Windows.Forms.Padding(2);
            this.cmdRowUp.Name = "cmdRowUp";
            this.cmdRowUp.Size = new System.Drawing.Size(109, 71);
            this.cmdRowUp.TabIndex = 191;
            this.cmdRowUp.Text = "위";
            this.cmdRowUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRowUp.UseVisualStyleBackColor = true;
            this.cmdRowUp.Click += new System.EventHandler(this.cmdRowUp_Click);
            // 
            // cmdRowDown
            // 
            this.cmdRowDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdRowDown.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdRowDown.Image = global::WizWork.Properties.Resources.down_arrow__1_;
            this.cmdRowDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRowDown.Location = new System.Drawing.Point(2, 152);
            this.cmdRowDown.Margin = new System.Windows.Forms.Padding(2);
            this.cmdRowDown.Name = "cmdRowDown";
            this.cmdRowDown.Size = new System.Drawing.Size(109, 71);
            this.cmdRowDown.TabIndex = 192;
            this.cmdRowDown.Text = "아래";
            this.cmdRowDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRowDown.UseVisualStyleBackColor = true;
            this.cmdRowDown.Click += new System.EventHandler(this.cmdRowDown_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelete.Image = global::WizWork.Properties.Resources.delete_button;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(3, 228);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(107, 69);
            this.btnDelete.TabIndex = 170;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
            // 
            // tlpForm
            // 
            this.tlpForm.ColumnCount = 2;
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpForm.Controls.Add(this.tlpRight, 1, 0);
            this.tlpForm.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpForm.Location = new System.Drawing.Point(5, -1);
            this.tlpForm.Name = "tlpForm";
            this.tlpForm.RowCount = 1;
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForm.Size = new System.Drawing.Size(881, 538);
            this.tlpForm.TabIndex = 230;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.grdData, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.grdSum, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(742, 532);
            this.tableLayoutPanel2.TabIndex = 230;
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.AllowUserToResizeRows = false;
            this.grdData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdData.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdData.Location = new System.Drawing.Point(3, 82);
            this.grdData.MultiSelect = false;
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grdData.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grdData.RowTemplate.Height = 23;
            this.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new System.Drawing.Size(736, 408);
            this.grdData.TabIndex = 232;
            this.grdData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdData_CellClick);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.pnlDate, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(740, 73);
            this.tableLayoutPanel3.TabIndex = 229;
            // 
            // pnlDate
            // 
            this.pnlDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDate.Controls.Add(this.tlp_Search_Date);
            this.pnlDate.Location = new System.Drawing.Point(3, 3);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(364, 29);
            this.pnlDate.TabIndex = 231;
            // 
            // tlp_Search_Date
            // 
            this.tlp_Search_Date.ColumnCount = 3;
            this.tlp_Search_Date.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlp_Search_Date.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tlp_Search_Date.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tlp_Search_Date.Controls.Add(this.chkResultDate, 0, 0);
            this.tlp_Search_Date.Controls.Add(this.tlp_From, 1, 0);
            this.tlp_Search_Date.Controls.Add(this.tlp_To, 2, 0);
            this.tlp_Search_Date.Location = new System.Drawing.Point(-19, 0);
            this.tlp_Search_Date.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_Search_Date.Name = "tlp_Search_Date";
            this.tlp_Search_Date.RowCount = 1;
            this.tlp_Search_Date.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Search_Date.Size = new System.Drawing.Size(441, 29);
            this.tlp_Search_Date.TabIndex = 225;
            // 
            // chkResultDate
            // 
            this.chkResultDate.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkResultDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkResultDate.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkResultDate.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkResultDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkResultDate.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkResultDate.ForeColor = System.Drawing.Color.White;
            this.chkResultDate.Location = new System.Drawing.Point(1, 1);
            this.chkResultDate.Margin = new System.Windows.Forms.Padding(1);
            this.chkResultDate.Name = "chkResultDate";
            this.chkResultDate.Size = new System.Drawing.Size(57, 25);
            this.chkResultDate.TabIndex = 201;
            this.chkResultDate.Text = "일자";
            this.chkResultDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkResultDate.UseVisualStyleBackColor = false;
            // 
            // tlp_From
            // 
            this.tlp_From.ColumnCount = 2;
            this.tlp_From.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlp_From.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_From.Controls.Add(this.btnCal_From, 1, 0);
            this.tlp_From.Controls.Add(this.mtb_From, 0, 0);
            this.tlp_From.Location = new System.Drawing.Point(67, 1);
            this.tlp_From.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_From.Name = "tlp_From";
            this.tlp_From.RowCount = 1;
            this.tlp_From.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_From.Size = new System.Drawing.Size(147, 19);
            this.tlp_From.TabIndex = 205;
            // 
            // btnCal_From
            // 
            this.btnCal_From.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCal_From.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnCal_From.Location = new System.Drawing.Point(118, 1);
            this.btnCal_From.Margin = new System.Windows.Forms.Padding(1);
            this.btnCal_From.Name = "btnCal_From";
            this.btnCal_From.Size = new System.Drawing.Size(28, 17);
            this.btnCal_From.TabIndex = 0;
            this.btnCal_From.UseVisualStyleBackColor = true;
            this.btnCal_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // mtb_From
            // 
            this.mtb_From.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtb_From.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.mtb_From.Location = new System.Drawing.Point(1, 1);
            this.mtb_From.Margin = new System.Windows.Forms.Padding(1);
            this.mtb_From.Mask = "0000-00-00";
            this.mtb_From.Name = "mtb_From";
            this.mtb_From.ReadOnly = true;
            this.mtb_From.Size = new System.Drawing.Size(115, 39);
            this.mtb_From.TabIndex = 205;
            this.mtb_From.TabStop = false;
            this.mtb_From.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_From.ValidatingType = typeof(System.DateTime);
            this.mtb_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // tlp_To
            // 
            this.tlp_To.ColumnCount = 2;
            this.tlp_To.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlp_To.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_To.Controls.Add(this.btnCal_To, 1, 0);
            this.tlp_To.Controls.Add(this.mtb_To, 0, 0);
            this.tlp_To.Location = new System.Drawing.Point(254, 1);
            this.tlp_To.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_To.Name = "tlp_To";
            this.tlp_To.RowCount = 1;
            this.tlp_To.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_To.Size = new System.Drawing.Size(147, 19);
            this.tlp_To.TabIndex = 207;
            // 
            // btnCal_To
            // 
            this.btnCal_To.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCal_To.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnCal_To.Location = new System.Drawing.Point(118, 1);
            this.btnCal_To.Margin = new System.Windows.Forms.Padding(1);
            this.btnCal_To.Name = "btnCal_To";
            this.btnCal_To.Size = new System.Drawing.Size(28, 17);
            this.btnCal_To.TabIndex = 0;
            this.btnCal_To.UseVisualStyleBackColor = true;
            this.btnCal_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // mtb_To
            // 
            this.mtb_To.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtb_To.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.mtb_To.Location = new System.Drawing.Point(1, 1);
            this.mtb_To.Margin = new System.Windows.Forms.Padding(1);
            this.mtb_To.Mask = "0000-00-00";
            this.mtb_To.Name = "mtb_To";
            this.mtb_To.ReadOnly = true;
            this.mtb_To.Size = new System.Drawing.Size(115, 39);
            this.mtb_To.TabIndex = 205;
            this.mtb_To.TabStop = false;
            this.mtb_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_To.ValidatingType = typeof(System.DateTime);
            this.mtb_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.cboJobGbn, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cboProcess, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1, 37);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(368, 31);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.cboMachine, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.cboTeam, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(371, 37);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(368, 31);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(373, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 30);
            this.panel1.TabIndex = 232;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.txtBuyerArticleNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkBuyerArticleNo, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(401, 30);
            this.tableLayoutPanel1.TabIndex = 234;
            // 
            // txtBuyerArticleNo
            // 
            this.txtBuyerArticleNo.BackColor = System.Drawing.Color.White;
            this.txtBuyerArticleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuyerArticleNo.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.txtBuyerArticleNo.Location = new System.Drawing.Point(134, 1);
            this.txtBuyerArticleNo.Margin = new System.Windows.Forms.Padding(1);
            this.txtBuyerArticleNo.Name = "txtBuyerArticleNo";
            this.txtBuyerArticleNo.Size = new System.Drawing.Size(140, 39);
            this.txtBuyerArticleNo.TabIndex = 231;
            this.txtBuyerArticleNo.WordWrap = false;
            this.txtBuyerArticleNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuyerArticleNo_KeyPress);
            // 
            // chkBuyerArticleNo
            // 
            this.chkBuyerArticleNo.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBuyerArticleNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkBuyerArticleNo.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkBuyerArticleNo.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkBuyerArticleNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBuyerArticleNo.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkBuyerArticleNo.ForeColor = System.Drawing.Color.White;
            this.chkBuyerArticleNo.Location = new System.Drawing.Point(1, 1);
            this.chkBuyerArticleNo.Margin = new System.Windows.Forms.Padding(1);
            this.chkBuyerArticleNo.Name = "chkBuyerArticleNo";
            this.chkBuyerArticleNo.Size = new System.Drawing.Size(68, 24);
            this.chkBuyerArticleNo.TabIndex = 203;
            this.chkBuyerArticleNo.Text = "품  번";
            this.chkBuyerArticleNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBuyerArticleNo.UseVisualStyleBackColor = false;
            this.chkBuyerArticleNo.Click += new System.EventHandler(this.chkBuyerArticleNo_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.txtPLotID, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.chkPLotID, 0, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(270, 582);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(401, 30);
            this.tableLayoutPanel6.TabIndex = 234;
            // 
            // txtPLotID
            // 
            this.txtPLotID.BackColor = System.Drawing.Color.White;
            this.txtPLotID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPLotID.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.txtPLotID.Location = new System.Drawing.Point(134, 1);
            this.txtPLotID.Margin = new System.Windows.Forms.Padding(1);
            this.txtPLotID.Name = "txtPLotID";
            this.txtPLotID.Size = new System.Drawing.Size(140, 39);
            this.txtPLotID.TabIndex = 231;
            this.txtPLotID.WordWrap = false;
            this.txtPLotID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPLotID_KeyPress);
            // 
            // chkPLotID
            // 
            this.chkPLotID.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPLotID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.chkPLotID.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkPLotID.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkPLotID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPLotID.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkPLotID.ForeColor = System.Drawing.Color.White;
            this.chkPLotID.Location = new System.Drawing.Point(1, 1);
            this.chkPLotID.Margin = new System.Windows.Forms.Padding(1);
            this.chkPLotID.Name = "chkPLotID";
            this.chkPLotID.Size = new System.Drawing.Size(68, 24);
            this.chkPLotID.TabIndex = 203;
            this.chkPLotID.Text = "지시LOTID";
            this.chkPLotID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPLotID.UseVisualStyleBackColor = false;
            this.chkPLotID.Click += new System.EventHandler(this.chkPLotID_Click);
            // 
            // Frm_tprc_Result
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(992, 686);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.tlpForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_tprc_Result";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "검사조회";
            this.Activated += new System.EventHandler(this.Frm_tprc_Result_Activated);
            this.Load += new System.EventHandler(this.Frm_tprc_Result_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdSum)).EndInit();
            this.tlpRight.ResumeLayout(false);
            this.tlpForm.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.pnlDate.ResumeLayout(false);
            this.tlp_Search_Date.ResumeLayout(false);
            this.tlp_From.ResumeLayout(false);
            this.tlp_From.PerformLayout();
            this.tlp_To.ResumeLayout(false);
            this.tlp_To.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnLookup;
        private System.Windows.Forms.ComboBox cboProcess;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ComboBox cboJobGbn;
        private System.Windows.Forms.ComboBox cboMachine;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cboTeam;
        private System.Windows.Forms.Button cmdRowDown;
        private System.Windows.Forms.Button cmdRowUp;
        private System.Windows.Forms.DataGridView grdSum;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.TableLayoutPanel tlpForm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel pnlDate;
        private System.Windows.Forms.TableLayoutPanel tlp_Search_Date;
        private System.Windows.Forms.CheckBox chkResultDate;
        private System.Windows.Forms.TableLayoutPanel tlp_From;
        private System.Windows.Forms.Button btnCal_From;
        private System.Windows.Forms.MaskedTextBox mtb_From;
        private System.Windows.Forms.TableLayoutPanel tlp_To;
        private System.Windows.Forms.Button btnCal_To;
        private System.Windows.Forms.MaskedTextBox mtb_To;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TextBox txtPLotID;
        private System.Windows.Forms.CheckBox chkPLotID;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.Button btnColRight;
        private System.Windows.Forms.Button btnColLeft;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtBuyerArticleNo;
        private System.Windows.Forms.CheckBox chkBuyerArticleNo;
    }
}