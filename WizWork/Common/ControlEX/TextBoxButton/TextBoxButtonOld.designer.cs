namespace KR_POP.Common.ControlEX
{
	partial class TextBoxButtonOld
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.p_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // p_Button
            // 
            this.p_Button.BackColor = System.Drawing.Color.White;
            this.p_Button.BackgroundImage = global::KR_POP.Common.Properties.Resources.zoom;
            this.p_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.p_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.p_Button.Location = new System.Drawing.Point(0, 0);
            this.p_Button.Name = "p_Button";
            this.p_Button.Size = new System.Drawing.Size(75, 23);
            this.p_Button.TabIndex = 0;
            this.p_Button.UseVisualStyleBackColor = false;
            // 
            // TextBoxButton
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DetectUrls = false;
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Multiline = false;
            this.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Size = new System.Drawing.Size(100, 22);
            this.WordWrap = false;
            this.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button p_Button;

    }
}
