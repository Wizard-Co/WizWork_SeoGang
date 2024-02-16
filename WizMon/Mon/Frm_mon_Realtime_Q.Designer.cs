﻿using System.Drawing;

namespace WizMon
{
    partial class Frm_mon_Realtime_Q
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgdMain = new System.Windows.Forms.DataGridView();
            this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Machine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Article = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyerArticleNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CollectQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefectQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgdMain)).BeginInit();
            this.SuspendLayout();
            // 
            // dgdMain
            // 
            this.dgdMain.AllowUserToAddRows = false;
            this.dgdMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(100)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("휴먼둥근헤드라인", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgdMain.ColumnHeadersHeight = 100;
            this.dgdMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Num,
            this.Process,
            this.Machine,
            this.Article,
            this.BuyerArticleNo,
            this.CollectQty,
            this.WorkQty,
            this.DefectQty});
            this.dgdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgdMain.EnableHeadersVisualStyles = false;
            this.dgdMain.Location = new System.Drawing.Point(0, 0);
            this.dgdMain.Name = "dgdMain";
            this.dgdMain.ReadOnly = true;
            this.dgdMain.RowHeadersVisible = false;
            this.dgdMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgdMain.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.dgdMain.RowTemplate.Height = 23;
            this.dgdMain.Size = new System.Drawing.Size(1164, 770);
            this.dgdMain.TabIndex = 0;
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
            this.Num.DefaultCellStyle = dataGridViewCellStyle2;
            this.Num.HeaderText = "  ";
            this.Num.Name = "Num";
            this.Num.ReadOnly = true;
            this.Num.Width = 67;
            // 
            // Process
            // 
            this.Process.DataPropertyName = "Process";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Process.DefaultCellStyle = dataGridViewCellStyle3;
            this.Process.HeaderText = "공정";
            this.Process.MinimumWidth = 140;
            this.Process.Name = "Process";
            this.Process.ReadOnly = true;
            this.Process.Width = 140;
            // 
            // Machine
            // 
            this.Machine.DataPropertyName = "MachineNo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Machine.DefaultCellStyle = dataGridViewCellStyle4;
            this.Machine.HeaderText = "호기";
            this.Machine.MinimumWidth = 120;
            this.Machine.Name = "Machine";
            this.Machine.ReadOnly = true;
            this.Machine.Width = 120;
            // 
            // Article
            // 
            this.Article.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Article.DataPropertyName = "Article";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Article.DefaultCellStyle = dataGridViewCellStyle5;
            this.Article.HeaderText = "품명";
            this.Article.MinimumWidth = 120;
            this.Article.Name = "Article";
            this.Article.ReadOnly = true;
            // 
            // BuyerArticleNo
            // 
            this.BuyerArticleNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BuyerArticleNo.DataPropertyName = "BuyerArticleNo";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.BuyerArticleNo.DefaultCellStyle = dataGridViewCellStyle6;
            this.BuyerArticleNo.HeaderText = "품번";
            this.BuyerArticleNo.Name = "BuyerArticleNo";
            this.BuyerArticleNo.ReadOnly = true;
            // 
            // CollectQty
            // 
            this.CollectQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CollectQty.DataPropertyName = "CollectQty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.CollectQty.DefaultCellStyle = dataGridViewCellStyle7;
            this.CollectQty.HeaderText = "수집수량";
            this.CollectQty.Name = "CollectQty";
            this.CollectQty.ReadOnly = true;
            this.CollectQty.Width = 124;
            // 
            // WorkQty
            // 
            this.WorkQty.DataPropertyName = "WorkQty";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.WorkQty.DefaultCellStyle = dataGridViewCellStyle8;
            this.WorkQty.HeaderText = "작업량";
            this.WorkQty.MinimumWidth = 180;
            this.WorkQty.Name = "WorkQty";
            this.WorkQty.ReadOnly = true;
            this.WorkQty.Width = 180;
            // 
            // DefectQty
            // 
            this.DefectQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DefectQty.DataPropertyName = "DefectQty";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("휴먼둥근헤드라인", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.DefectQty.DefaultCellStyle = dataGridViewCellStyle9;
            this.DefectQty.HeaderText = "불량수량";
            this.DefectQty.Name = "DefectQty";
            this.DefectQty.ReadOnly = true;
            this.DefectQty.Width = 124;
            // 
            // Frm_mon_Realtime_Q
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 770);
            this.Controls.Add(this.dgdMain);
            this.Name = "Frm_mon_Realtime_Q";
            this.Text = "호기별 실시간 작업 현황";
            this.Load += new System.EventHandler(this.Frm_mon_Realtime_Q_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgdMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgdMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewTextBoxColumn Machine;
        private System.Windows.Forms.DataGridViewTextBoxColumn Article;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyerArticleNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CollectQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefectQty;
    }
}