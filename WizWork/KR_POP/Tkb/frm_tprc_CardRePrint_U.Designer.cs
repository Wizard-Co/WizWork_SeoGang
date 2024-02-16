namespace WizWork
{
    partial class frm_tprc_CardRePrint_U
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
            this.tlpForm = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.tlpSearch_LL = new System.Windows.Forms.TableLayoutPanel();
            this.txtMtrLotNo = new System.Windows.Forms.TextBox();
            this.chkMtrLotNo = new System.Windows.Forms.CheckBox();
            this.rbnProcessLabel = new System.Windows.Forms.RadioButton();
            this.tlp_Search_Date = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_To = new System.Windows.Forms.TableLayoutPanel();
            this.btnCal_To = new System.Windows.Forms.Button();
            this.mtb_To = new System.Windows.Forms.MaskedTextBox();
            this.chkInstDate = new System.Windows.Forms.CheckBox();
            this.tlp_From = new System.Windows.Forms.TableLayoutPanel();
            this.btnCal_From = new System.Windows.Forms.Button();
            this.mtb_From = new System.Windows.Forms.MaskedTextBox();
            this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
            this.pnlData = new System.Windows.Forms.Panel();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdBarcode = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.cmdRowUp = new System.Windows.Forms.Button();
            this.cmdRowDown = new System.Windows.Forms.Button();
            this.tlpForm.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.tlpSearch_LL.SuspendLayout();
            this.tlp_Search_Date.SuspendLayout();
            this.tlp_To.SuspendLayout();
            this.tlp_From.SuspendLayout();
            this.tlpBottom.SuspendLayout();
            this.pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpForm
            // 
            this.tlpForm.ColumnCount = 2;
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpForm.Controls.Add(this.tlpMain, 0, 0);
            this.tlpForm.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tlpForm.Location = new System.Drawing.Point(12, 12);
            this.tlpForm.Name = "tlpForm";
            this.tlpForm.RowCount = 1;
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForm.Size = new System.Drawing.Size(965, 586);
            this.tlpForm.TabIndex = 236;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpTop, 0, 0);
            this.tlpMain.Controls.Add(this.tlpBottom, 0, 1);
            this.tlpMain.Location = new System.Drawing.Point(3, 3);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlpMain.Size = new System.Drawing.Size(814, 579);
            this.tlpMain.TabIndex = 197;
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 2;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTop.Controls.Add(this.tlpSearch_LL, 1, 0);
            this.tlpTop.Controls.Add(this.tlp_Search_Date, 0, 0);
            this.tlpTop.Location = new System.Drawing.Point(3, 3);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTop.Size = new System.Drawing.Size(776, 109);
            this.tlpTop.TabIndex = 0;
            // 
            // tlpSearch_LL
            // 
            this.tlpSearch_LL.ColumnCount = 3;
            this.tlpSearch_LL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tlpSearch_LL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpSearch_LL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpSearch_LL.Controls.Add(this.txtMtrLotNo, 1, 1);
            this.tlpSearch_LL.Controls.Add(this.chkMtrLotNo, 0, 1);
            this.tlpSearch_LL.Controls.Add(this.rbnProcessLabel, 0, 0);
            this.tlpSearch_LL.Location = new System.Drawing.Point(391, 3);
            this.tlpSearch_LL.Name = "tlpSearch_LL";
            this.tlpSearch_LL.RowCount = 2;
            this.tlpSearch_LL.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSearch_LL.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSearch_LL.Size = new System.Drawing.Size(355, 102);
            this.tlpSearch_LL.TabIndex = 201;
            // 
            // txtMtrLotNo
            // 
            this.txtMtrLotNo.BackColor = System.Drawing.Color.White;
            this.txtMtrLotNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMtrLotNo.Font = new System.Drawing.Font("맑은 고딕", 30F, System.Drawing.FontStyle.Bold);
            this.txtMtrLotNo.Location = new System.Drawing.Point(122, 53);
            this.txtMtrLotNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtMtrLotNo.Name = "txtMtrLotNo";
            this.txtMtrLotNo.Size = new System.Drawing.Size(113, 61);
            this.txtMtrLotNo.TabIndex = 207;
            this.txtMtrLotNo.WordWrap = false;
            this.txtMtrLotNo.Click += new System.EventHandler(this.txtMtrLotNo_Click);
            // 
            // chkMtrLotNo
            // 
            this.chkMtrLotNo.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkMtrLotNo.BackColor = System.Drawing.Color.LightSkyBlue;
            this.chkMtrLotNo.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkMtrLotNo.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkMtrLotNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMtrLotNo.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkMtrLotNo.ForeColor = System.Drawing.Color.White;
            this.chkMtrLotNo.Location = new System.Drawing.Point(2, 53);
            this.chkMtrLotNo.Margin = new System.Windows.Forms.Padding(2);
            this.chkMtrLotNo.Name = "chkMtrLotNo";
            this.chkMtrLotNo.Size = new System.Drawing.Size(88, 38);
            this.chkMtrLotNo.TabIndex = 203;
            this.chkMtrLotNo.Text = "원자재 LotNo";
            this.chkMtrLotNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMtrLotNo.UseVisualStyleBackColor = false;
            this.chkMtrLotNo.Click += new System.EventHandler(this.chkMtrLotNo_Click);
            // 
            // rbnProcessLabel
            // 
            this.rbnProcessLabel.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbnProcessLabel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.rbnProcessLabel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rbnProcessLabel.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rbnProcessLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnProcessLabel.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.rbnProcessLabel.ForeColor = System.Drawing.Color.White;
            this.rbnProcessLabel.Location = new System.Drawing.Point(3, 3);
            this.rbnProcessLabel.Name = "rbnProcessLabel";
            this.rbnProcessLabel.Size = new System.Drawing.Size(92, 37);
            this.rbnProcessLabel.TabIndex = 206;
            this.rbnProcessLabel.TabStop = true;
            this.rbnProcessLabel.Tag = "7";
            this.rbnProcessLabel.Text = "공정전표";
            this.rbnProcessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbnProcessLabel.UseVisualStyleBackColor = false;
            this.rbnProcessLabel.Click += new System.EventHandler(this.rbn_Click);
            // 
            // tlp_Search_Date
            // 
            this.tlp_Search_Date.ColumnCount = 2;
            this.tlp_Search_Date.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlp_Search_Date.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tlp_Search_Date.Controls.Add(this.tlp_To, 1, 1);
            this.tlp_Search_Date.Controls.Add(this.chkInstDate, 0, 0);
            this.tlp_Search_Date.Controls.Add(this.tlp_From, 1, 0);
            this.tlp_Search_Date.Location = new System.Drawing.Point(3, 3);
            this.tlp_Search_Date.Name = "tlp_Search_Date";
            this.tlp_Search_Date.RowCount = 2;
            this.tlp_Search_Date.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tlp_Search_Date.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tlp_Search_Date.Size = new System.Drawing.Size(370, 102);
            this.tlp_Search_Date.TabIndex = 205;
            // 
            // tlp_To
            // 
            this.tlp_To.ColumnCount = 2;
            this.tlp_To.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlp_To.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_To.Controls.Add(this.btnCal_To, 1, 0);
            this.tlp_To.Controls.Add(this.mtb_To, 0, 0);
            this.tlp_To.Location = new System.Drawing.Point(58, 54);
            this.tlp_To.Name = "tlp_To";
            this.tlp_To.RowCount = 1;
            this.tlp_To.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_To.Size = new System.Drawing.Size(170, 29);
            this.tlp_To.TabIndex = 206;
            // 
            // btnCal_To
            // 
            this.btnCal_To.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCal_To.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnCal_To.Location = new System.Drawing.Point(139, 3);
            this.btnCal_To.Name = "btnCal_To";
            this.btnCal_To.Size = new System.Drawing.Size(28, 23);
            this.btnCal_To.TabIndex = 0;
            this.btnCal_To.UseVisualStyleBackColor = true;
            this.btnCal_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // mtb_To
            // 
            this.mtb_To.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtb_To.Font = new System.Drawing.Font("맑은 고딕", 30F);
            this.mtb_To.Location = new System.Drawing.Point(3, 3);
            this.mtb_To.Mask = "0000-00-00";
            this.mtb_To.Name = "mtb_To";
            this.mtb_To.ReadOnly = true;
            this.mtb_To.Size = new System.Drawing.Size(130, 61);
            this.mtb_To.TabIndex = 205;
            this.mtb_To.TabStop = false;
            this.mtb_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_To.ValidatingType = typeof(System.DateTime);
            this.mtb_To.Click += new System.EventHandler(this.mtb_To_Click);
            // 
            // chkInstDate
            // 
            this.chkInstDate.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkInstDate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.chkInstDate.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkInstDate.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkInstDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkInstDate.Font = new System.Drawing.Font("맑은 고딕", 12.25F, System.Drawing.FontStyle.Bold);
            this.chkInstDate.ForeColor = System.Drawing.Color.White;
            this.chkInstDate.Location = new System.Drawing.Point(2, 2);
            this.chkInstDate.Margin = new System.Windows.Forms.Padding(2);
            this.chkInstDate.Name = "chkInstDate";
            this.chkInstDate.Size = new System.Drawing.Size(35, 31);
            this.chkInstDate.TabIndex = 201;
            this.chkInstDate.Text = "발행일";
            this.chkInstDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkInstDate.UseVisualStyleBackColor = false;
            // 
            // tlp_From
            // 
            this.tlp_From.ColumnCount = 2;
            this.tlp_From.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlp_From.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_From.Controls.Add(this.btnCal_From, 1, 0);
            this.tlp_From.Controls.Add(this.mtb_From, 0, 0);
            this.tlp_From.Location = new System.Drawing.Point(58, 3);
            this.tlp_From.Name = "tlp_From";
            this.tlp_From.RowCount = 1;
            this.tlp_From.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_From.Size = new System.Drawing.Size(170, 29);
            this.tlp_From.TabIndex = 205;
            // 
            // btnCal_From
            // 
            this.btnCal_From.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCal_From.Image = global::WizWork.Properties.Resources.calendar__2_;
            this.btnCal_From.Location = new System.Drawing.Point(139, 3);
            this.btnCal_From.Name = "btnCal_From";
            this.btnCal_From.Size = new System.Drawing.Size(28, 23);
            this.btnCal_From.TabIndex = 0;
            this.btnCal_From.UseVisualStyleBackColor = true;
            this.btnCal_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // mtb_From
            // 
            this.mtb_From.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtb_From.Font = new System.Drawing.Font("맑은 고딕", 30F);
            this.mtb_From.Location = new System.Drawing.Point(3, 3);
            this.mtb_From.Mask = "0000-00-00";
            this.mtb_From.Name = "mtb_From";
            this.mtb_From.ReadOnly = true;
            this.mtb_From.Size = new System.Drawing.Size(130, 61);
            this.mtb_From.TabIndex = 205;
            this.mtb_From.TabStop = false;
            this.mtb_From.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_From.ValidatingType = typeof(System.DateTime);
            this.mtb_From.Click += new System.EventHandler(this.mtb_From_Click);
            // 
            // tlpBottom
            // 
            this.tlpBottom.ColumnCount = 1;
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBottom.Controls.Add(this.pnlData, 0, 0);
            this.tlpBottom.Location = new System.Drawing.Point(3, 118);
            this.tlpBottom.Name = "tlpBottom";
            this.tlpBottom.RowCount = 1;
            this.tlpBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBottom.Size = new System.Drawing.Size(646, 451);
            this.tlpBottom.TabIndex = 1;
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.grdData);
            this.pnlData.Location = new System.Drawing.Point(3, 3);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(580, 336);
            this.pnlData.TabIndex = 206;
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdData.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdData.Location = new System.Drawing.Point(-2, 43);
            this.grdData.MultiSelect = false;
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grdData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.grdData.RowTemplate.Height = 23;
            this.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new System.Drawing.Size(498, 251);
            this.grdData.TabIndex = 160;
            this.grdData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdData_CellClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.cmdClose, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.cmdBarcode, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnNew, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmdRowUp, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmdRowDown, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(823, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(139, 477);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdClose.Image = global::WizWork.Properties.Resources.enter;
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.Location = new System.Drawing.Point(29, 399);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(2);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(108, 68);
            this.cmdClose.TabIndex = 225;
            this.cmdClose.Text = "닫기";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdBarcode
            // 
            this.cmdBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBarcode.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdBarcode.Image = global::WizWork.Properties.Resources.printer;
            this.cmdBarcode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdBarcode.Location = new System.Drawing.Point(35, 318);
            this.cmdBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.cmdBarcode.Name = "cmdBarcode";
            this.cmdBarcode.Size = new System.Drawing.Size(102, 68);
            this.cmdBarcode.TabIndex = 227;
            this.cmdBarcode.Text = "라벨\r\n재발행";
            this.cmdBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdBarcode.UseVisualStyleBackColor = true;
            this.cmdBarcode.Click += new System.EventHandler(this.cmdBarcode_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnNew.Image = global::WizWork.Properties.Resources.icons8_search_48;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(33, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(104, 68);
            this.btnNew.TabIndex = 222;
            this.btnNew.Text = "검색";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // cmdRowUp
            // 
            this.cmdRowUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRowUp.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdRowUp.Image = global::WizWork.Properties.Resources.up_arrow__1_;
            this.cmdRowUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRowUp.Location = new System.Drawing.Point(35, 81);
            this.cmdRowUp.Margin = new System.Windows.Forms.Padding(2);
            this.cmdRowUp.Name = "cmdRowUp";
            this.cmdRowUp.Size = new System.Drawing.Size(102, 68);
            this.cmdRowUp.TabIndex = 223;
            this.cmdRowUp.Text = "위로";
            this.cmdRowUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRowUp.UseVisualStyleBackColor = true;
            this.cmdRowUp.Click += new System.EventHandler(this.cmdRowUp_Click);
            // 
            // cmdRowDown
            // 
            this.cmdRowDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRowDown.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdRowDown.Image = global::WizWork.Properties.Resources.down_arrow__1_;
            this.cmdRowDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRowDown.Location = new System.Drawing.Point(35, 160);
            this.cmdRowDown.Margin = new System.Windows.Forms.Padding(2);
            this.cmdRowDown.Name = "cmdRowDown";
            this.cmdRowDown.Size = new System.Drawing.Size(102, 68);
            this.cmdRowDown.TabIndex = 224;
            this.cmdRowDown.Text = "아래";
            this.cmdRowDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRowDown.UseVisualStyleBackColor = true;
            this.cmdRowDown.Click += new System.EventHandler(this.cmdRowDown_Click);
            // 
            // frm_tprc_CardRePrint_U
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 620);
            this.Controls.Add(this.tlpForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_tprc_CardRePrint_U";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "이동전표 재발행";
            this.Load += new System.EventHandler(this.frm_tprc_CardRePrint_U_Load);
            this.tlpForm.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpTop.ResumeLayout(false);
            this.tlpSearch_LL.ResumeLayout(false);
            this.tlpSearch_LL.PerformLayout();
            this.tlp_Search_Date.ResumeLayout(false);
            this.tlp_To.ResumeLayout(false);
            this.tlp_To.PerformLayout();
            this.tlp_From.ResumeLayout(false);
            this.tlp_From.PerformLayout();
            this.tlpBottom.ResumeLayout(false);
            this.pnlData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdRowDown;
        private System.Windows.Forms.Button cmdRowUp;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button cmdBarcode;
        private System.Windows.Forms.TableLayoutPanel tlpForm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.TableLayoutPanel tlpSearch_LL;
        private System.Windows.Forms.CheckBox chkMtrLotNo;
        private System.Windows.Forms.TableLayoutPanel tlp_Search_Date;
        private System.Windows.Forms.TableLayoutPanel tlp_To;
        private System.Windows.Forms.Button btnCal_To;
        private System.Windows.Forms.MaskedTextBox mtb_To;
        private System.Windows.Forms.CheckBox chkInstDate;
        private System.Windows.Forms.TableLayoutPanel tlp_From;
        private System.Windows.Forms.Button btnCal_From;
        private System.Windows.Forms.MaskedTextBox mtb_From;
        private System.Windows.Forms.TableLayoutPanel tlpBottom;
        private System.Windows.Forms.Panel pnlData;
        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.TextBox txtMtrLotNo;
        private System.Windows.Forms.RadioButton rbnProcessLabel;
    }
}