namespace InformaciónColumna
{
    partial class UserControl1
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
            this.NombreTabla = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NombreTabla
            // 
            this.NombreTabla.AutoSize = true;
            this.NombreTabla.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NombreTabla.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.NombreTabla.Location = new System.Drawing.Point(9, 7);
            this.NombreTabla.Name = "NombreTabla";
            this.NombreTabla.Size = new System.Drawing.Size(41, 13);
            this.NombreTabla.TabIndex = 0;
            this.NombreTabla.Text = "label1";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.Controls.Add(this.NombreTabla);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(167, 250);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NombreTabla;
    }
}
