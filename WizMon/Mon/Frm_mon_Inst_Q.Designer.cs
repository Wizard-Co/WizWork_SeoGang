using System.Drawing;

namespace WizMon
{
    partial class Frm_mon_Inst_Q
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgdMain = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chartInst = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyerArticleNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Article = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefectQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefectPPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgdMain)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartInst)).BeginInit();
            this.SuspendLayout();
            // 
            // dgdMain
            // 
            this.dgdMain.AllowUserToAddRows = false;
            this.dgdMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(100)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgdMain.ColumnHeadersHeight = 100;
            this.dgdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Num,
            this.Process,
            this.BuyerArticleNo,
            this.Article,
            this.InstQty,
            this.WorkQty,
            this.DefectQty,
            this.DefectPPM,
            this.ProcessRate});
            this.dgdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgdMain.EnableHeadersVisualStyles = false;
            this.dgdMain.Location = new System.Drawing.Point(3, 3);
            this.dgdMain.Name = "dgdMain";
            this.dgdMain.ReadOnly = true;
            this.dgdMain.RowHeadersVisible = false;
            this.dgdMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgdMain.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.dgdMain.RowTemplate.Height = 23;
            this.dgdMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgdMain.Size = new System.Drawing.Size(1158, 470);
            this.dgdMain.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.chartInst, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgdMain, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.93507F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.06494F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1164, 636);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // chartInst
            // 
            chartArea1.Name = "ChartArea1";
            this.chartInst.ChartAreas.Add(chartArea1);
            this.chartInst.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartInst.Legends.Add(legend1);
            this.chartInst.Location = new System.Drawing.Point(3, 479);
            this.chartInst.Name = "chartInst";
            series1.ChartArea = "ChartArea1";
            series1.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            series1.IsValueShownAsLabel = true;
            series1.LabelBackColor = System.Drawing.Color.White;
            series1.LabelBorderColor = System.Drawing.Color.LightGray;
            series1.Legend = "Legend1";
            series1.Name = "진척률";
            this.chartInst.Series.Add(series1);
            this.chartInst.Size = new System.Drawing.Size(1158, 154);
            this.chartInst.TabIndex = 1;
            this.chartInst.Text = "chart1";
            // 
            // Num
            // 
            this.Num.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Num.DataPropertyName = "Num";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(100)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            //dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            //dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.Num.DefaultCellStyle = dataGridViewCellStyle2;
            this.Num.HeaderText = "  ";
            this.Num.Name = "Num";
            this.Num.ReadOnly = true;
            this.Num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Num.Width = 54;
            // 
            // Process
            // 
            this.Process.DataPropertyName = "Process";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            //dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            //dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Process.DefaultCellStyle = dataGridViewCellStyle3;
            this.Process.HeaderText = "공정";
            this.Process.MinimumWidth = 120;
            this.Process.Name = "Process";
            this.Process.ReadOnly = true;
            this.Process.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Process.Width = 127;
            // 
            // BuyerArticleNo
            // 
            this.BuyerArticleNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BuyerArticleNo.DataPropertyName = "BuyerArticleNo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            //dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            //dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.BuyerArticleNo.DefaultCellStyle = dataGridViewCellStyle4;
            this.BuyerArticleNo.HeaderText = "품번";
            this.BuyerArticleNo.Name = "BuyerArticleNo";
            this.BuyerArticleNo.ReadOnly = true;
            this.BuyerArticleNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Article
            // 
            this.Article.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Article.DataPropertyName = "Article";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            //dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            //dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.Article.DefaultCellStyle = dataGridViewCellStyle5;
            this.Article.HeaderText = "품명";
            this.Article.MinimumWidth = 120;
            this.Article.Name = "Article";
            this.Article.ReadOnly = true;
            this.Article.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // InstQty
            // 
            this.InstQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.InstQty.DataPropertyName = "InstQty";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            //dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            //dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.InstQty.DefaultCellStyle = dataGridViewCellStyle6;
            this.InstQty.HeaderText = "지시수량";
            this.InstQty.Name = "InstQty";
            this.InstQty.ReadOnly = true;
            this.InstQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InstQty.Width = 105;
            // 
            // WorkQty
            // 
            this.WorkQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.WorkQty.DataPropertyName = "WorkQty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            //dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            //dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.WorkQty.DefaultCellStyle = dataGridViewCellStyle7;
            this.WorkQty.HeaderText = "작업수량";
            this.WorkQty.MinimumWidth = 180;
            this.WorkQty.Name = "WorkQty";
            this.WorkQty.ReadOnly = true;
            this.WorkQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WorkQty.Width = 180;
            // 
            // DefectQty
            // 
            this.DefectQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DefectQty.DataPropertyName = "DefectQty";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            //dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            //dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.DefectQty.DefaultCellStyle = dataGridViewCellStyle8;
            this.DefectQty.HeaderText = "불량수량";
            this.DefectQty.Name = "DefectQty";
            this.DefectQty.ReadOnly = true;
            this.DefectQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DefectQty.Width = 105;
            // 
            // DefectPPM
            // 
            this.DefectPPM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DefectPPM.DataPropertyName = "DefectPPM";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            //dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            //dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.DefectPPM.DefaultCellStyle = dataGridViewCellStyle9;
            this.DefectPPM.HeaderText = "불량률(ppm)";
            this.DefectPPM.MinimumWidth = 140;
            this.DefectPPM.Name = "DefectPPM";
            this.DefectPPM.ReadOnly = true;
            this.DefectPPM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DefectPPM.Width = 193;
            // 
            // ProcessRate
            // 
            this.ProcessRate.DataPropertyName = "ProcessRate";
            this.ProcessRate.HeaderText = "진행률";
            this.ProcessRate.Name = "ProcessRate";
            this.ProcessRate.ReadOnly = true;
            this.ProcessRate.Visible = false;
            // 
            // Frm_mon_Inst_Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 636);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Frm_mon_Inst_Q";
            this.Text = "작업지시 진행 현황";
            this.Load += new System.EventHandler(this.Frm_mon_Inst_Q_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgdMain)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartInst)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgdMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartInst;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyerArticleNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Article;
        private System.Windows.Forms.DataGridViewTextBoxColumn InstQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefectQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefectPPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessRate;
    }
}