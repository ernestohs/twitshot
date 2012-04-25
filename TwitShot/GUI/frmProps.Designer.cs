namespace TwitShot.GUI
{
    partial class frmProps
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.spltPaneles = new System.Windows.Forms.SplitContainer();
            this.tvOpciones = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.spltPaneles.Panel1.SuspendLayout();
            this.spltPaneles.Panel2.SuspendLayout();
            this.spltPaneles.SuspendLayout();
            this.SuspendLayout();
            // 
            // spltPaneles
            // 
            this.spltPaneles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltPaneles.Location = new System.Drawing.Point(0, 0);
            this.spltPaneles.Name = "spltPaneles";
            // 
            // spltPaneles.Panel1
            // 
            this.spltPaneles.Panel1.Controls.Add(this.tvOpciones);
            // 
            // spltPaneles.Panel2
            // 
            this.spltPaneles.Panel2.Controls.Add(this.groupBox2);
            this.spltPaneles.Panel2.Controls.Add(this.groupBox3);
            this.spltPaneles.Panel2.Controls.Add(this.groupBox1);
            this.spltPaneles.Size = new System.Drawing.Size(624, 469);
            this.spltPaneles.SplitterDistance = 208;
            this.spltPaneles.TabIndex = 0;
            // 
            // tvOpciones
            // 
            this.tvOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvOpciones.Location = new System.Drawing.Point(0, 0);
            this.tvOpciones.Name = "tvOpciones";
            this.tvOpciones.Size = new System.Drawing.Size(208, 469);
            this.tvOpciones.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Captura";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(13, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(13, 224);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // frmProps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 469);
            this.Controls.Add(this.spltPaneles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmProps";
            this.Text = "Configuración";
            this.spltPaneles.Panel1.ResumeLayout(false);
            this.spltPaneles.Panel2.ResumeLayout(false);
            this.spltPaneles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spltPaneles;
        private System.Windows.Forms.TreeView tvOpciones;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}