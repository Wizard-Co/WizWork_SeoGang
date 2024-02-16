using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
namespace Common.Popup
{
    partial class Frm_CMNumericKeypad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CMNumericKeypad));
            this.btnKey1 = new System.Windows.Forms.Button();
            this.btnKey2 = new System.Windows.Forms.Button();
            this.btnKey3 = new System.Windows.Forms.Button();
            this.btnKey4 = new System.Windows.Forms.Button();
            this.btnKey5 = new System.Windows.Forms.Button();
            this.btnKey6 = new System.Windows.Forms.Button();
            this.btnKeyClear = new System.Windows.Forms.Button();
            this.btnKey7 = new System.Windows.Forms.Button();
            this.btnKey8 = new System.Windows.Forms.Button();
            this.btnKey9 = new System.Windows.Forms.Button();
            this.btnKeyClose = new System.Windows.Forms.Button();
            this.btnKey0 = new System.Windows.Forms.Button();
            this.btnKeyPoint = new System.Windows.Forms.Button();
            this.btnKeyInput = new System.Windows.Forms.Button();
            this.tbInputText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnKeyBackSpace = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnKey1
            // 
            this.btnKey1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKey1.FlatAppearance.BorderSize = 0;
            this.btnKey1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey1.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btnKey1.ForeColor = System.Drawing.Color.White;
            this.btnKey1.Location = new System.Drawing.Point(12, 156);
            this.btnKey1.Name = "btnKey1";
            this.btnKey1.Size = new System.Drawing.Size(62, 44);
            this.btnKey1.TabIndex = 16;
            this.btnKey1.Text = "1";
            this.btnKey1.UseVisualStyleBackColor = false;
            this.btnKey1.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKey2
            // 
            this.btnKey2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKey2.FlatAppearance.BorderSize = 0;
            this.btnKey2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey2.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btnKey2.ForeColor = System.Drawing.Color.White;
            this.btnKey2.Location = new System.Drawing.Point(80, 156);
            this.btnKey2.Name = "btnKey2";
            this.btnKey2.Size = new System.Drawing.Size(62, 44);
            this.btnKey2.TabIndex = 1;
            this.btnKey2.Text = "2";
            this.btnKey2.UseVisualStyleBackColor = false;
            this.btnKey2.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKey3
            // 
            this.btnKey3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKey3.FlatAppearance.BorderSize = 0;
            this.btnKey3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey3.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btnKey3.ForeColor = System.Drawing.Color.White;
            this.btnKey3.Location = new System.Drawing.Point(148, 156);
            this.btnKey3.Name = "btnKey3";
            this.btnKey3.Size = new System.Drawing.Size(62, 44);
            this.btnKey3.TabIndex = 2;
            this.btnKey3.Text = "3";
            this.btnKey3.UseVisualStyleBackColor = false;
            this.btnKey3.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKey4
            // 
            this.btnKey4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKey4.FlatAppearance.BorderSize = 0;
            this.btnKey4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey4.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btnKey4.ForeColor = System.Drawing.Color.White;
            this.btnKey4.Location = new System.Drawing.Point(12, 106);
            this.btnKey4.Name = "btnKey4";
            this.btnKey4.Size = new System.Drawing.Size(62, 44);
            this.btnKey4.TabIndex = 4;
            this.btnKey4.Text = "4";
            this.btnKey4.UseVisualStyleBackColor = false;
            this.btnKey4.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKey5
            // 
            this.btnKey5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKey5.FlatAppearance.BorderSize = 0;
            this.btnKey5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey5.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btnKey5.ForeColor = System.Drawing.Color.White;
            this.btnKey5.Location = new System.Drawing.Point(80, 106);
            this.btnKey5.Name = "btnKey5";
            this.btnKey5.Size = new System.Drawing.Size(62, 44);
            this.btnKey5.TabIndex = 5;
            this.btnKey5.Text = "5";
            this.btnKey5.UseVisualStyleBackColor = false;
            this.btnKey5.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKey6
            // 
            this.btnKey6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKey6.FlatAppearance.BorderSize = 0;
            this.btnKey6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey6.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btnKey6.ForeColor = System.Drawing.Color.White;
            this.btnKey6.Location = new System.Drawing.Point(148, 106);
            this.btnKey6.Name = "btnKey6";
            this.btnKey6.Size = new System.Drawing.Size(62, 44);
            this.btnKey6.TabIndex = 6;
            this.btnKey6.Text = "6";
            this.btnKey6.UseVisualStyleBackColor = false;
            this.btnKey6.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKeyClear
            // 
            this.btnKeyClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKeyClear.FlatAppearance.BorderSize = 0;
            this.btnKeyClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyClear.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.btnKeyClear.ForeColor = System.Drawing.Color.White;
            this.btnKeyClear.Location = new System.Drawing.Point(226, 106);
            this.btnKeyClear.Margin = new System.Windows.Forms.Padding(0);
            this.btnKeyClear.Name = "btnKeyClear";
            this.btnKeyClear.Size = new System.Drawing.Size(62, 44);
            this.btnKeyClear.TabIndex = 7;
            this.btnKeyClear.Text = "모두 지우기";
            this.btnKeyClear.UseVisualStyleBackColor = false;
            this.btnKeyClear.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKey7
            // 
            this.btnKey7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKey7.FlatAppearance.BorderSize = 0;
            this.btnKey7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey7.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btnKey7.ForeColor = System.Drawing.Color.White;
            this.btnKey7.Location = new System.Drawing.Point(12, 56);
            this.btnKey7.Name = "btnKey7";
            this.btnKey7.Size = new System.Drawing.Size(62, 44);
            this.btnKey7.TabIndex = 8;
            this.btnKey7.Text = "7";
            this.btnKey7.UseVisualStyleBackColor = false;
            this.btnKey7.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKey8
            // 
            this.btnKey8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKey8.FlatAppearance.BorderSize = 0;
            this.btnKey8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey8.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btnKey8.ForeColor = System.Drawing.Color.White;
            this.btnKey8.Location = new System.Drawing.Point(80, 56);
            this.btnKey8.Name = "btnKey8";
            this.btnKey8.Size = new System.Drawing.Size(62, 44);
            this.btnKey8.TabIndex = 9;
            this.btnKey8.Text = "8";
            this.btnKey8.UseVisualStyleBackColor = false;
            this.btnKey8.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKey9
            // 
            this.btnKey9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKey9.FlatAppearance.BorderSize = 0;
            this.btnKey9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey9.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btnKey9.ForeColor = System.Drawing.Color.White;
            this.btnKey9.Location = new System.Drawing.Point(148, 56);
            this.btnKey9.Name = "btnKey9";
            this.btnKey9.Size = new System.Drawing.Size(62, 44);
            this.btnKey9.TabIndex = 10;
            this.btnKey9.Text = "9";
            this.btnKey9.UseVisualStyleBackColor = false;
            this.btnKey9.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKeyClose
            // 
            this.btnKeyClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKeyClose.FlatAppearance.BorderSize = 0;
            this.btnKeyClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyClose.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnKeyClose.ForeColor = System.Drawing.Color.White;
            this.btnKeyClose.Location = new System.Drawing.Point(226, 156);
            this.btnKeyClose.Name = "btnKeyClose";
            this.btnKeyClose.Size = new System.Drawing.Size(62, 44);
            this.btnKeyClose.TabIndex = 11;
            this.btnKeyClose.Text = "취소";
            this.btnKeyClose.UseVisualStyleBackColor = false;
            this.btnKeyClose.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKey0
            // 
            this.btnKey0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKey0.FlatAppearance.BorderSize = 0;
            this.btnKey0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey0.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btnKey0.ForeColor = System.Drawing.Color.White;
            this.btnKey0.Location = new System.Drawing.Point(12, 206);
            this.btnKey0.Name = "btnKey0";
            this.btnKey0.Size = new System.Drawing.Size(130, 44);
            this.btnKey0.TabIndex = 13;
            this.btnKey0.Text = "0";
            this.btnKey0.UseVisualStyleBackColor = false;
            this.btnKey0.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKeyPoint
            // 
            this.btnKeyPoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKeyPoint.FlatAppearance.BorderSize = 0;
            this.btnKeyPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPoint.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btnKeyPoint.ForeColor = System.Drawing.Color.White;
            this.btnKeyPoint.Location = new System.Drawing.Point(148, 206);
            this.btnKeyPoint.Name = "btnKeyPoint";
            this.btnKeyPoint.Size = new System.Drawing.Size(62, 44);
            this.btnKeyPoint.TabIndex = 14;
            this.btnKeyPoint.Text = ".";
            this.btnKeyPoint.UseVisualStyleBackColor = false;
            this.btnKeyPoint.Click += new System.EventHandler(this.InputKey);
            // 
            // btnKeyInput
            // 
            this.btnKeyInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKeyInput.FlatAppearance.BorderSize = 0;
            this.btnKeyInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyInput.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnKeyInput.ForeColor = System.Drawing.Color.White;
            this.btnKeyInput.Location = new System.Drawing.Point(226, 206);
            this.btnKeyInput.Name = "btnKeyInput";
            this.btnKeyInput.Size = new System.Drawing.Size(62, 44);
            this.btnKeyInput.TabIndex = 15;
            this.btnKeyInput.Text = "확인";
            this.btnKeyInput.UseVisualStyleBackColor = false;
            this.btnKeyInput.Click += new System.EventHandler(this.InputKey);
            // 
            // tbInputText
            // 
            this.tbInputText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tbInputText.Font = new System.Drawing.Font("굴림", 18F);
            this.tbInputText.ForeColor = System.Drawing.Color.White;
            this.tbInputText.Location = new System.Drawing.Point(90, 12);
            this.tbInputText.Name = "tbInputText";
            this.tbInputText.Size = new System.Drawing.Size(198, 35);
            this.tbInputText.TabIndex = 0;
            this.tbInputText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInputText_KeyDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 35);
            this.button1.TabIndex = 17;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnKeyBackSpace
            // 
            this.btnKeyBackSpace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnKeyBackSpace.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnKeyBackSpace.BackgroundImage")));
            this.btnKeyBackSpace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnKeyBackSpace.FlatAppearance.BorderSize = 0;
            this.btnKeyBackSpace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyBackSpace.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnKeyBackSpace.ForeColor = System.Drawing.Color.White;
            this.btnKeyBackSpace.Location = new System.Drawing.Point(226, 56);
            this.btnKeyBackSpace.Name = "btnKeyBackSpace";
            this.btnKeyBackSpace.Size = new System.Drawing.Size(62, 44);
            this.btnKeyBackSpace.TabIndex = 3;
            this.btnKeyBackSpace.UseVisualStyleBackColor = false;
            this.btnKeyBackSpace.Click += new System.EventHandler(this.InputKey);
            // 
            // Frm_CMNumericKeypad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(299, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbInputText);
            this.Controls.Add(this.btnKeyInput);
            this.Controls.Add(this.btnKeyPoint);
            this.Controls.Add(this.btnKey0);
            this.Controls.Add(this.btnKeyClose);
            this.Controls.Add(this.btnKey9);
            this.Controls.Add(this.btnKey8);
            this.Controls.Add(this.btnKey7);
            this.Controls.Add(this.btnKeyClear);
            this.Controls.Add(this.btnKey6);
            this.Controls.Add(this.btnKey5);
            this.Controls.Add(this.btnKey4);
            this.Controls.Add(this.btnKeyBackSpace);
            this.Controls.Add(this.btnKey3);
            this.Controls.Add(this.btnKey2);
            this.Controls.Add(this.btnKey1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Frm_CMNumericKeypad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_CMNumericKeypad_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Qlt_NumericKeypad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKey1;
        private System.Windows.Forms.Button btnKey2;
        private System.Windows.Forms.Button btnKey3;
        private System.Windows.Forms.Button btnKeyBackSpace;
        private System.Windows.Forms.Button btnKey4;
        private System.Windows.Forms.Button btnKey5;
        private System.Windows.Forms.Button btnKey6;
        private System.Windows.Forms.Button btnKeyClear;
        private System.Windows.Forms.Button btnKey7;
        private System.Windows.Forms.Button btnKey8;
        private System.Windows.Forms.Button btnKey9;
        private System.Windows.Forms.Button btnKeyClose;
        private System.Windows.Forms.Button btnKey0;
        private System.Windows.Forms.Button btnKeyPoint;
        private System.Windows.Forms.Button btnKeyInput;
        public System.Windows.Forms.TextBox tbInputText;
        private System.Windows.Forms.Button button1;
    }
}