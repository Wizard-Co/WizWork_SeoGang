using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KR_POP.Common.ControlEX
{
    [ToolboxItem(false)]
    public partial class FormStyleEditorControl : UserControl
    {
        public FormStyleEditorControl()
        {
            InitializeComponent();

            UpdateStyleList();
            FormStyleManager.StyleChanged += new EventHandler(FormStyleManager_StyleChanged);
        }

        private void toolNew_Click(object sender, EventArgs e)
        {
            NewStyle();
        }

        private void toolOpen_Click(object sender, EventArgs e)
        {
            OpenStyleLibrary();
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            SaveStyleLibrary();
        }

        private void FormStyleManager_StyleChanged(object sender, EventArgs e)
        {
            UpdateStyleList();
        }

        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            private set { _fileName = value; }
        }

        private CustomBorderForm _owningForm;

        public CustomBorderForm OwningForm
        {
            get { return _owningForm; }
            set
            {
                if (_owningForm != value)
                {
                    _owningForm = value;

                    //if (_owningForm != null)
                    //{
                    //    ActiveStyle = _owningForm.FormStyle;
                    //}
                }
            }
        }


        private FormStyle _activeStyle;

        public FormStyle ActiveStyle
        {
            get { return _activeStyle; }
            set 
            {
                if (_activeStyle != value)
                {
                    if (_activeStyle != null)
                        _activeStyle.ChildPropertyChanged -= new ChildPropertyChangedEventHandler(ActiveStyle_ChildPropertyChanged);

                    _activeStyle = value;

                    if (_activeStyle != null)
                        _activeStyle.ChildPropertyChanged += new ChildPropertyChangedEventHandler(ActiveStyle_ChildPropertyChanged);
                    
                    ClearDirtyFlag();
                    UptadeTreeView();
                    EnableTools();

                    //if (OwningForm != null && OwningForm.FormStyle != _activeStyle)
                    //{
                    //    OwningForm.FormStyle = _activeStyle;
                    //}
                }
            }
        }

        private bool _isDirty;
        public bool IsDirty
        {
            get { return _isDirty; }
        }

        public void ClearDirtyFlag()
        {
            _isDirty = false;
        }

        void ActiveStyle_ChildPropertyChanged(object sender, ChildPropertyChangedEventArgs args)
        {
            _isDirty = true;

            if (args.SubObject == ActiveStyle && args.PropertyName == FormStyleProperty.Name)
                UpdateStyleList();
        }

        private void NewStyle()
        {
            FormStyleManager.Reset();
        }

        private void OpenStyleLibrary()
        {
            openFileDialog.FileName = FileName;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FormStyleManager.Load(openFileDialog.FileName);
                    FileName = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


        private void SaveStyleLibrary()
        {
            saveFileDialog.FileName = FileName;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FormStyleManager.Save(saveFileDialog.FileName);
                    ClearDirtyFlag();
                    FileName = saveFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void UpdateStyleList()
        {
            object selected = toolStyleList.SelectedItem;
            toolStyleList.Items.Clear();

            string[] styles = FormStyleManager.GetStyleNames();
            toolStyleList.Items.AddRange(styles);

            if (toolStyleList.Items.Count > 0)
            {

                if (selected != null)
                    toolStyleList.SelectedItem = selected;

                if (toolStyleList.SelectedIndex < 0)
                    toolStyleList.SelectedIndex = 0;

            }
            EnableTools();
        }

        private void EnableTools()
        {
            bool styleListEmpty = toolStyleList.Items.Count == 0;

            toolSave.Enabled = !styleListEmpty;
            toolStyleList.Enabled = !styleListEmpty;
            msgEmptyLibrary.Visible = styleListEmpty;
            splitContainer.Visible = !styleListEmpty;

            bool activeStyleSet = ActiveStyle != null;

            toolDeleteStyle.Enabled = activeStyleSet;
        }

        private void UptadeTreeView()
        {
            treeView.Nodes.Clear();
            
            if (ActiveStyle != null)
                treeView.Nodes.Add(MapFormStyleToTreeNode(ActiveStyle, 
                    String.IsNullOrEmpty(ActiveStyle.Name)?"FormStyle":ActiveStyle.Name) );
            //if (OwningForm != null)
            //    treeView.Nodes.Add(MapFormToTreeNode(OwningForm, "Form"));
           
            treeView.ExpandAll();

            if (treeView.Nodes.Count > 0)
                treeView.SelectedNode = treeView.Nodes[0];
        }

        private TreeNode MapFormToTreeNode(CustomBorderForm form, string propertyName)
        {
            TreeNode node = new TreeNode(propertyName);
            node.Tag = form;
            return node;
        }

        TreeNode MapFormStyleToTreeNode(FormStyle formStyle, string propertyName)
        {
            TreeNode node = new TreeNode(propertyName);
            node.Tag = formStyle;
            node.Nodes.Add(MapSerializableImageToTreeNode(formStyle.NormalState, FormStyleProperty.NormalState));
            node.Nodes.Add(MapFormButtonStyleToTreeNode(formStyle.CloseButton, FormStyleProperty.CloseButton));
            node.Nodes.Add(MapFormButtonStyleToTreeNode(formStyle.MaximizeButton, FormStyleProperty.MaximizeButton));
            node.Nodes.Add(MapFormButtonStyleToTreeNode(formStyle.MinimizeButton, FormStyleProperty.MinimizeButton));
            node.Nodes.Add(MapFormButtonStyleToTreeNode(formStyle.RestoreButton, FormStyleProperty.RestoreButton));
            node.Nodes.Add(MapFormButtonStyleToTreeNode(formStyle.HelpButton, FormStyleProperty.HelpButton));
            return node;
        }

        private TreeNode MapSerializableImageToTreeNode(SerializableImage serializableImage, string propertyName)
        {
            TreeNode node = new TreeNode(propertyName);
            node.Tag = serializableImage;
            return node;
        }

        private TreeNode MapFormButtonStyleToTreeNode(FormButtonStyle formButtonStyle, string propertyName)
        {
            TreeNode node = new TreeNode(propertyName);
            node.Tag = formButtonStyle;
            node.Nodes.Add(MapSerializableImageToTreeNode(formButtonStyle.NormalState, FormButtonStyleProperty.NormalState));
            node.Nodes.Add(MapSerializableImageToTreeNode(formButtonStyle.HoverState, FormButtonStyleProperty.HoverState));
            node.Nodes.Add(MapSerializableImageToTreeNode(formButtonStyle.ActiveState, FormButtonStyleProperty.ActiveState));
            node.Nodes.Add(MapSerializableImageToTreeNode(formButtonStyle.DisabledState, FormButtonStyleProperty.DisabledState));
            return node;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
                propertyGrid.SelectedObject = e.Node.Tag;
            else
                propertyGrid.SelectedObject = null;
        }

        private void toolStyleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveStyle = FormStyleManager.GetStyle((string)toolStyleList.SelectedItem);
        }

        private void toolAddStyle_Click(object sender, EventArgs e)
        {
            FormStyle style = FormStyleManager.AddNewStyle();
            toolStyleList.SelectedItem = style.Name;
        }

        private void toolDeleteStyle_Click(object sender, EventArgs e)
        {
            if (ActiveStyle != null)
                FormStyleManager.DeleteStyle(ActiveStyle);
        }

        private void msgEmptyLibrary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ((string)e.Link.LinkData == "Add")
                toolAddStyle.PerformClick();
            else if ((string)e.Link.LinkData == "Open")
                toolOpen.PerformClick();
        }

        private void FormStyleEditorControl_Load(object sender, EventArgs e)
        {
            msgEmptyLibrary.Links.Add(new LinkLabel.Link(61, 13, "Add"));
            msgEmptyLibrary.Links.Add(new LinkLabel.Link(78, 27, "Open"));
            msgEmptyLibrary.Dock = DockStyle.Fill;
        }
    }
}
