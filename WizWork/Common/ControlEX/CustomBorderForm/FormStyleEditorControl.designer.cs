namespace KR_POP.Common.ControlEX
{
    partial class FormStyleEditorControl
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
            if (disposing)
            {
                FormStyleManager.StyleChanged -= new System.EventHandler(FormStyleManager_StyleChanged);
                
                if (components != null)
                 components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStyleEditorControl));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.toolStyleLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStyleList = new System.Windows.Forms.ToolStripComboBox();
            this.toolAddStyle = new System.Windows.Forms.ToolStripButton();
            this.toolDeleteStyle = new System.Windows.Forms.ToolStripButton();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolNew = new System.Windows.Forms.ToolStripButton();
            this.toolOpen = new System.Windows.Forms.ToolStripButton();
            this.toolSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.msgEmptyLibrary = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 26);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.splitContainer.Size = new System.Drawing.Size(511, 319);
            this.splitContainer.SplitterDistance = 187;
            this.splitContainer.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.HideSelection = false;
            this.treeView.HotTracking = true;
            this.treeView.Location = new System.Drawing.Point(3, 3);
            this.treeView.Name = "treeView";
            this.treeView.ShowRootLines = false;
            this.treeView.Size = new System.Drawing.Size(184, 313);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 3);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(317, 313);
            this.propertyGrid.TabIndex = 0;
            // 
            // toolStyleLabel
            // 
            this.toolStyleLabel.Name = "toolStyleLabel";
            this.toolStyleLabel.Size = new System.Drawing.Size(68, 22);
            this.toolStyleLabel.Text = "Active Style:";
            // 
            // toolStyleList
            // 
            this.toolStyleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStyleList.Name = "toolStyleList";
            this.toolStyleList.Size = new System.Drawing.Size(100, 25);
            this.toolStyleList.SelectedIndexChanged += new System.EventHandler(this.toolStyleList_SelectedIndexChanged);
            // 
            // toolAddStyle
            // 
            this.toolAddStyle.Image = ((System.Drawing.Image)(resources.GetObject("toolAddStyle.Image")));
            this.toolAddStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAddStyle.Name = "toolAddStyle";
            this.toolAddStyle.Size = new System.Drawing.Size(73, 22);
            this.toolAddStyle.Text = "Add Style";
            this.toolAddStyle.Click += new System.EventHandler(this.toolAddStyle_Click);
            // 
            // toolDeleteStyle
            // 
            this.toolDeleteStyle.Image = ((System.Drawing.Image)(resources.GetObject("toolDeleteStyle.Image")));
            this.toolDeleteStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDeleteStyle.Name = "toolDeleteStyle";
            this.toolDeleteStyle.Size = new System.Drawing.Size(85, 22);
            this.toolDeleteStyle.Text = "Delete Style";
            this.toolDeleteStyle.Click += new System.EventHandler(this.toolDeleteStyle_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNew,
            this.toolOpen,
            this.toolSave,
            this.toolStripButton1,
            this.toolStyleLabel,
            this.toolStyleList,
            this.toolAddStyle,
            this.toolDeleteStyle});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip.Size = new System.Drawing.Size(511, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolNew
            // 
            this.toolNew.Image = ((System.Drawing.Image)(resources.GetObject("toolNew.Image")));
            this.toolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNew.Name = "toolNew";
            this.toolNew.Size = new System.Drawing.Size(48, 22);
            this.toolNew.Text = "&New";
            this.toolNew.Click += new System.EventHandler(this.toolNew_Click);
            // 
            // toolOpen
            // 
            this.toolOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolOpen.Image")));
            this.toolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOpen.Name = "toolOpen";
            this.toolOpen.Size = new System.Drawing.Size(53, 22);
            this.toolOpen.Text = "&Open";
            this.toolOpen.Click += new System.EventHandler(this.toolOpen_Click);
            // 
            // toolSave
            // 
            this.toolSave.Image = ((System.Drawing.Image)(resources.GetObject("toolSave.Image")));
            this.toolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSave.Name = "toolSave";
            this.toolSave.Size = new System.Drawing.Size(51, 22);
            this.toolSave.Text = "&Save";
            this.toolSave.Click += new System.EventHandler(this.toolSave_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 25);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "*.fsl";
            this.openFileDialog.Filter = "Form Style Library (*.fsl)|*.fsl|All files (*.*)|*.*";
            this.openFileDialog.Title = "Open Form Style Library";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "*.fsl";
            this.saveFileDialog.FileName = "FormStyle.fsl";
            this.saveFileDialog.Filter = "Form Style Library (*.fsl)|*.fsl|All files (*.*)|*.*";
            this.saveFileDialog.Title = "Save Form Style Library";
            // 
            // msgEmptyLibrary
            // 
            this.msgEmptyLibrary.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.msgEmptyLibrary.Location = new System.Drawing.Point(59, 115);
            this.msgEmptyLibrary.Name = "msgEmptyLibrary";
            this.msgEmptyLibrary.Size = new System.Drawing.Size(321, 69);
            this.msgEmptyLibrary.TabIndex = 1;
            this.msgEmptyLibrary.Text = "Currently there are no styles defined in FormStyleManager. \r\nAdd new style or ope" +
                "n existing style library.";
            this.msgEmptyLibrary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.msgEmptyLibrary.Visible = false;
            this.msgEmptyLibrary.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.msgEmptyLibrary_LinkClicked);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(511, 1);
            this.label1.TabIndex = 1;
            // 
            // FormStyleEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.msgEmptyLibrary);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip);
            this.Name = "FormStyleEditorControl";
            this.Size = new System.Drawing.Size(511, 345);
            this.Load += new System.EventHandler(this.FormStyleEditorControl_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolNew;
        private System.Windows.Forms.ToolStripButton toolOpen;
        private System.Windows.Forms.ToolStripButton toolSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripComboBox toolStyleList;
        private System.Windows.Forms.ToolStripButton toolAddStyle;
        private System.Windows.Forms.ToolStripButton toolDeleteStyle;
        private System.Windows.Forms.ToolStripLabel toolStyleLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.LinkLabel msgEmptyLibrary;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label label1;

    }
}
