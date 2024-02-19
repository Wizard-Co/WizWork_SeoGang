namespace WizWork
{
    partial class Frm_tprc_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_tprc_Main));
            this.stsInfo = new System.Windows.Forms.StatusStrip();
            this.stsInfo_Msg = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsInfo_ProMac = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsInfo_Team = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsInfo_Person = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsInfo_Mold = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsInfo_Time = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer_Clock = new System.Windows.Forms.Timer(this.components);
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnDailyCheck = new System.Windows.Forms.Button();
            this.btnNoWork = new System.Windows.Forms.Button();
            this.btnWork = new System.Windows.Forms.Button();
            this.btnMove = new System.Windows.Forms.Button();
            this.btnWorkQ = new System.Windows.Forms.Button();
            this.btnChoiceWorker = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRePrint = new System.Windows.Forms.Button();
            this.btnInsInspectAuto = new System.Windows.Forms.Button();
            this.btnMold = new System.Windows.Forms.Button();
            this.stsInfo.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // stsInfo
            // 
            this.stsInfo.AutoSize = false;
            this.stsInfo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.stsInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsInfo_Msg,
            this.stsInfo_ProMac,
            this.stsInfo_Team,
            this.stsInfo_Person,
            this.stsInfo_Mold,
            this.stsInfo_Time});
            this.stsInfo.Location = new System.Drawing.Point(0, 663);
            this.stsInfo.Name = "stsInfo";
            this.stsInfo.Size = new System.Drawing.Size(1005, 22);
            this.stsInfo.SizingGrip = false;
            this.stsInfo.TabIndex = 6;
            this.stsInfo.Text = "statusStrip1";
            // 
            // stsInfo_Msg
            // 
            this.stsInfo_Msg.AutoSize = false;
            this.stsInfo_Msg.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_Msg.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_Msg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_Msg.Name = "stsInfo_Msg";
            this.stsInfo_Msg.Size = new System.Drawing.Size(385, 17);
            this.stsInfo_Msg.Text = "Message";
            // 
            // stsInfo_ProMac
            // 
            this.stsInfo_ProMac.AutoSize = false;
            this.stsInfo_ProMac.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_ProMac.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_ProMac.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_ProMac.Name = "stsInfo_ProMac";
            this.stsInfo_ProMac.Size = new System.Drawing.Size(200, 17);
            this.stsInfo_ProMac.Text = "자동사상 성형기 17호기";
            // 
            // stsInfo_Team
            // 
            this.stsInfo_Team.AutoSize = false;
            this.stsInfo_Team.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_Team.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_Team.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_Team.Name = "stsInfo_Team";
            this.stsInfo_Team.Size = new System.Drawing.Size(70, 17);
            this.stsInfo_Team.Text = "주간";
            // 
            // stsInfo_Person
            // 
            this.stsInfo_Person.AutoSize = false;
            this.stsInfo_Person.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_Person.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_Person.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_Person.Name = "stsInfo_Person";
            this.stsInfo_Person.Size = new System.Drawing.Size(121, 17);
            this.stsInfo_Person.Text = "작업자 : 관리자";
            // 
            // stsInfo_Mold
            // 
            this.stsInfo_Mold.AutoSize = false;
            this.stsInfo_Mold.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_Mold.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_Mold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_Mold.Name = "stsInfo_Mold";
            this.stsInfo_Mold.Size = new System.Drawing.Size(121, 17);
            this.stsInfo_Mold.Text = "금형 : M502279";
            // 
            // stsInfo_Time
            // 
            this.stsInfo_Time.AutoSize = false;
            this.stsInfo_Time.BackColor = System.Drawing.SystemColors.Control;
            this.stsInfo_Time.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stsInfo_Time.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stsInfo_Time.Name = "stsInfo_Time";
            this.stsInfo_Time.Size = new System.Drawing.Size(121, 17);
            this.stsInfo_Time.Text = "오전 12:00";
            // 
            // timer_Clock
            // 
            this.timer_Clock.Tick += new System.EventHandler(this.timer_Clock_Tick);
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 11;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tlpTop.Controls.Add(this.btnInfo, 0, 0);
            this.tlpTop.Controls.Add(this.btnDailyCheck, 1, 0);
            this.tlpTop.Controls.Add(this.btnExit, 10, 0);
            this.tlpTop.Controls.Add(this.btnSetting, 9, 0);
            this.tlpTop.Controls.Add(this.btnChoiceWorker, 8, 0);
            this.tlpTop.Controls.Add(this.btnWorkQ, 7, 0);
            this.tlpTop.Controls.Add(this.btnRePrint, 6, 0);
            this.tlpTop.Controls.Add(this.btnMove, 5, 0);
            this.tlpTop.Controls.Add(this.btnNoWork, 4, 0);
            this.tlpTop.Controls.Add(this.btnWork, 3, 0);
            this.tlpTop.Controls.Add(this.btnMold, 2, 0);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTop.Location = new System.Drawing.Point(0, 0);
            this.tlpTop.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.Size = new System.Drawing.Size(1005, 84);
            this.tlpTop.TabIndex = 14;
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(192)))), ((int)(((byte)(92)))));
            this.btnInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInfo.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInfo.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInfo.Image")));
            this.btnInfo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInfo.Location = new System.Drawing.Point(0, 0);
            this.btnInfo.Margin = new System.Windows.Forms.Padding(0);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(91, 84);
            this.btnInfo.TabIndex = 0;
            this.btnInfo.Text = "공지사항";
            this.btnInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnInfo.UseVisualStyleBackColor = false;
            // 
            // btnDailyCheck
            // 
            this.btnDailyCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(108)))), ((int)(((byte)(128)))));
            this.btnDailyCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDailyCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDailyCheck.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnDailyCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDailyCheck.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDailyCheck.Image = ((System.Drawing.Image)(resources.GetObject("btnDailyCheck.Image")));
            this.btnDailyCheck.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDailyCheck.Location = new System.Drawing.Point(91, 0);
            this.btnDailyCheck.Margin = new System.Windows.Forms.Padding(0);
            this.btnDailyCheck.Name = "btnDailyCheck";
            this.btnDailyCheck.Size = new System.Drawing.Size(91, 84);
            this.btnDailyCheck.TabIndex = 11;
            this.btnDailyCheck.Text = "설비점검";
            this.btnDailyCheck.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDailyCheck.UseVisualStyleBackColor = false;
            // 
            // btnNoWork
            // 
            this.btnNoWork.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(108)))), ((int)(((byte)(128)))));
            this.btnNoWork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNoWork.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnNoWork.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNoWork.Font = new System.Drawing.Font("맑은 고딕", 8F, System.Drawing.FontStyle.Bold);
            this.btnNoWork.Image = ((System.Drawing.Image)(resources.GetObject("btnNoWork.Image")));
            this.btnNoWork.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNoWork.Location = new System.Drawing.Point(364, 0);
            this.btnNoWork.Margin = new System.Windows.Forms.Padding(0);
            this.btnNoWork.Name = "btnNoWork";
            this.btnNoWork.Size = new System.Drawing.Size(91, 84);
            this.btnNoWork.TabIndex = 8;
            this.btnNoWork.Text = "무작업\r\n";
            this.btnNoWork.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNoWork.UseVisualStyleBackColor = false;
            // 
            // btnWork
            // 
            this.btnWork.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(108)))), ((int)(((byte)(128)))));
            this.btnWork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnWork.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnWork.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWork.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnWork.Image = ((System.Drawing.Image)(resources.GetObject("btnWork.Image")));
            this.btnWork.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnWork.Location = new System.Drawing.Point(273, 0);
            this.btnWork.Margin = new System.Windows.Forms.Padding(0);
            this.btnWork.Name = "btnWork";
            this.btnWork.Size = new System.Drawing.Size(91, 84);
            this.btnWork.TabIndex = 9;
            this.btnWork.Text = "공정작업";
            this.btnWork.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnWork.UseVisualStyleBackColor = false;
            // 
            // btnMove
            // 
            this.btnMove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(108)))), ((int)(((byte)(128)))));
            this.btnMove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMove.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMove.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMove.Image = ((System.Drawing.Image)(resources.GetObject("btnMove.Image")));
            this.btnMove.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMove.Location = new System.Drawing.Point(455, 0);
            this.btnMove.Margin = new System.Windows.Forms.Padding(0);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(91, 84);
            this.btnMove.TabIndex = 10;
            this.btnMove.Text = "잔량이동";
            this.btnMove.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMove.UseVisualStyleBackColor = false;
            // 
            // btnWorkQ
            // 
            this.btnWorkQ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(166)))), ((int)(((byte)(244)))));
            this.btnWorkQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnWorkQ.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnWorkQ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWorkQ.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnWorkQ.Image = ((System.Drawing.Image)(resources.GetObject("btnWorkQ.Image")));
            this.btnWorkQ.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnWorkQ.Location = new System.Drawing.Point(637, 0);
            this.btnWorkQ.Margin = new System.Windows.Forms.Padding(0);
            this.btnWorkQ.Name = "btnWorkQ";
            this.btnWorkQ.Size = new System.Drawing.Size(91, 84);
            this.btnWorkQ.TabIndex = 7;
            this.btnWorkQ.Text = "\n조회화면";
            this.btnWorkQ.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnWorkQ.UseVisualStyleBackColor = false;
            // 
            // btnChoiceWorker
            // 
            this.btnChoiceWorker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(166)))), ((int)(((byte)(244)))));
            this.btnChoiceWorker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnChoiceWorker.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChoiceWorker.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnChoiceWorker.Image = global::WizWork.Properties.Resources.worker;
            this.btnChoiceWorker.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnChoiceWorker.Location = new System.Drawing.Point(728, 0);
            this.btnChoiceWorker.Margin = new System.Windows.Forms.Padding(0);
            this.btnChoiceWorker.Name = "btnChoiceWorker";
            this.btnChoiceWorker.Size = new System.Drawing.Size(91, 84);
            this.btnChoiceWorker.TabIndex = 21;
            this.btnChoiceWorker.Text = "작업자선택";
            this.btnChoiceWorker.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChoiceWorker.UseVisualStyleBackColor = false;
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(194)))), ((int)(((byte)(133)))));
            this.btnSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSetting.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSetting.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSetting.Image = ((System.Drawing.Image)(resources.GetObject("btnSetting.Image")));
            this.btnSetting.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSetting.Location = new System.Drawing.Point(819, 0);
            this.btnSetting.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(91, 84);
            this.btnSetting.TabIndex = 10;
            this.btnSetting.Text = "환경설정";
            this.btnSetting.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSetting.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(162)))), ((int)(((byte)(143)))));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(910, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 84);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "작업종료";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRePrint
            // 
            this.btnRePrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(166)))), ((int)(((byte)(244)))));
            this.btnRePrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRePrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRePrint.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRePrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRePrint.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRePrint.Image = global::WizWork.Properties.Resources.printer;
            this.btnRePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRePrint.Location = new System.Drawing.Point(546, 0);
            this.btnRePrint.Margin = new System.Windows.Forms.Padding(0);
            this.btnRePrint.Name = "btnRePrint";
            this.btnRePrint.Size = new System.Drawing.Size(91, 84);
            this.btnRePrint.TabIndex = 7;
            this.btnRePrint.Text = "공정라벨\r\n재발행";
            this.btnRePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRePrint.UseVisualStyleBackColor = false;
            // 
            // btnInsInspectAuto
            // 
            this.btnInsInspectAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(108)))), ((int)(((byte)(128)))));
            this.btnInsInspectAuto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnInsInspectAuto.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnInsInspectAuto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInsInspectAuto.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInsInspectAuto.Image = ((System.Drawing.Image)(resources.GetObject("btnInsInspectAuto.Image")));
            this.btnInsInspectAuto.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInsInspectAuto.Location = new System.Drawing.Point(910, 84);
            this.btnInsInspectAuto.Margin = new System.Windows.Forms.Padding(0);
            this.btnInsInspectAuto.Name = "btnInsInspectAuto";
            this.btnInsInspectAuto.Size = new System.Drawing.Size(91, 84);
            this.btnInsInspectAuto.TabIndex = 12;
            this.btnInsInspectAuto.Text = "자주검사";
            this.btnInsInspectAuto.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnInsInspectAuto.UseVisualStyleBackColor = false;
            this.btnInsInspectAuto.Visible = false;
            // 
            // btnMold
            // 
            this.btnMold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(108)))), ((int)(((byte)(128)))));
            this.btnMold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMold.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnMold.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMold.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMold.Image = ((System.Drawing.Image)(resources.GetObject("btnMold.Image")));
            this.btnMold.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMold.Location = new System.Drawing.Point(182, 0);
            this.btnMold.Margin = new System.Windows.Forms.Padding(0);
            this.btnMold.Name = "btnMold";
            this.btnMold.Size = new System.Drawing.Size(91, 84);
            this.btnMold.TabIndex = 22;
            this.btnMold.Text = "금형점검";
            this.btnMold.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMold.UseVisualStyleBackColor = false;
            // 
            // Frm_tprc_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 685);
            this.Controls.Add(this.tlpTop);
            this.Controls.Add(this.stsInfo);
            this.Controls.Add(this.btnInsInspectAuto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MaximumSize = new System.Drawing.Size(1021, 724);
            this.MinimumSize = new System.Drawing.Size(1021, 724);
            this.Name = "Frm_tprc_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "생산정보시스템";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Frm_tprc_Main_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_tprc_Main_FormClosing);
            this.Load += new System.EventHandler(this.MDIParent1_Load);
            this.stsInfo.ResumeLayout(false);
            this.stsInfo.PerformLayout();
            this.tlpTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        public System.Windows.Forms.StatusStrip stsInfo;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_ProMac;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_Team;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_Person;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_Mold;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_Time;
        public System.Windows.Forms.ToolStripStatusLabel stsInfo_Msg;
        private System.Windows.Forms.Timer timer_Clock;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnDailyCheck;
        private System.Windows.Forms.Button btnInsInspectAuto;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnWorkQ;
        private System.Windows.Forms.Button btnNoWork;
        private System.Windows.Forms.Button btnWork;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnChoiceWorker;
        private System.Windows.Forms.Button btnRePrint;
        private System.Windows.Forms.Button btnMold;
    }
}



