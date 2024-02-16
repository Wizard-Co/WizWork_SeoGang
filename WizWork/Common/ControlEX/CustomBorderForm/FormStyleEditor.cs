using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace KR_POP.Common.ControlEX
{
    public partial class FormStyleEditor : Component
    {
        public FormStyleEditor()
        {
            InitializeComponent();
        }

        public FormStyleEditor(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        Form editorForm;

        public void ShowFormEditor(CustomBorderForm owningForm)
        {
            FormStyleEditorControl editor;
            if (editorForm == null || editorForm.IsDisposed)
            {
                editorForm = new Form();
                components.Add(editorForm);
                editorForm.Text = "Form Style Editor";
               // editorForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                editorForm.Size = new System.Drawing.Size(600, 400);
                editor = new FormStyleEditorControl();
                editor.Dock = DockStyle.Fill;
                editorForm.Controls.Add(editor);
                editorForm.FormClosing += new FormClosingEventHandler(editorForm_FormClosing);
                editorForm.FormClosed += new FormClosedEventHandler(editorForm_FormClosed);
            }
            else
            {
                editor = (FormStyleEditorControl)editorForm.Controls[0];
            }
            editor.OwningForm = owningForm;
            editorForm.Show();
        }

        void editorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            editorForm.FormClosing -= new FormClosingEventHandler(editorForm_FormClosing);
            editorForm.FormClosed -= new FormClosedEventHandler(editorForm_FormClosed);
            editorForm.Dispose();
            editorForm = null;
        }

        void editorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormStyleEditorControl editor = (FormStyleEditorControl)editorForm.Controls[0];
            if (editor.IsDirty)
            {
                DialogResult result = MessageBox.Show(editorForm,
                    "Current style contains unsaved changed. Do you want to close anyway?",
                    editorForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                e.Cancel = (result == DialogResult.No);
            }
        }
    }
}
