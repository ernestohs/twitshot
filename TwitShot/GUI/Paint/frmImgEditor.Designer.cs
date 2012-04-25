namespace TwitShot.GUI.Paint
{
    partial class frmImgEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImgEditor));
            this.ssEstado = new System.Windows.Forms.StatusStrip();
            this.tssSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssCords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tscContenedor = new System.Windows.Forms.ToolStripContainer();
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.tsHerramienta = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsColores = new System.Windows.Forms.ToolStrip();
            this.ssEstado.SuspendLayout();
            this.tscContenedor.ContentPanel.SuspendLayout();
            this.tscContenedor.TopToolStripPanel.SuspendLayout();
            this.tscContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.tsHerramienta.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssEstado
            // 
            this.ssEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssSize,
            this.toolStripStatusLabel2,
            this.tssCords,
            this.toolStripStatusLabel4});
            this.ssEstado.Location = new System.Drawing.Point(0, 543);
            this.ssEstado.Name = "ssEstado";
            this.ssEstado.Size = new System.Drawing.Size(605, 22);
            this.ssEstado.TabIndex = 0;
            // 
            // tssSize
            // 
            this.tssSize.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tssSize.Name = "tssSize";
            this.tssSize.Size = new System.Drawing.Size(60, 17);
            this.tssSize.Text = "1280x1024";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Image = global::TwitShot.Properties.Resources.Coordenadas;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(16, 17);
            // 
            // tssCords
            // 
            this.tssCords.Name = "tssCords";
            this.tssCords.Size = new System.Drawing.Size(54, 17);
            this.tssCords.Text = " X: 0, Y: 0";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Image = global::TwitShot.Properties.Resources.Direccion;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(16, 17);
            // 
            // tscContenedor
            // 
            // 
            // tscContenedor.ContentPanel
            // 
            this.tscContenedor.ContentPanel.Controls.Add(this.pbCanvas);
            this.tscContenedor.ContentPanel.Size = new System.Drawing.Size(605, 518);
            this.tscContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tscContenedor.Location = new System.Drawing.Point(0, 0);
            this.tscContenedor.Name = "tscContenedor";
            this.tscContenedor.Size = new System.Drawing.Size(605, 543);
            this.tscContenedor.TabIndex = 1;
            this.tscContenedor.Text = "toolStripContainer1";
            // 
            // tscContenedor.TopToolStripPanel
            // 
            this.tscContenedor.TopToolStripPanel.Controls.Add(this.tsHerramienta);
            this.tscContenedor.TopToolStripPanel.Controls.Add(this.tsColores);
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackColor = System.Drawing.Color.White;
            this.pbCanvas.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCanvas.Location = new System.Drawing.Point(0, 0);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(605, 518);
            this.pbCanvas.TabIndex = 0;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseMove);
            this.pbCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseClick);
            this.pbCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseDown);
            this.pbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseUp);
            // 
            // tsHerramienta
            // 
            this.tsHerramienta.Dock = System.Windows.Forms.DockStyle.None;
            this.tsHerramienta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.tsHerramienta.Location = new System.Drawing.Point(3, 0);
            this.tsHerramienta.Name = "tsHerramienta";
            this.tsHerramienta.Size = new System.Drawing.Size(35, 25);
            this.tsHerramienta.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::TwitShot.Properties.Resources.Lapiz;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Lapiz";
            // 
            // tsColores
            // 
            this.tsColores.Dock = System.Windows.Forms.DockStyle.None;
            this.tsColores.Location = new System.Drawing.Point(38, 0);
            this.tsColores.Name = "tsColores";
            this.tsColores.Size = new System.Drawing.Size(111, 25);
            this.tsColores.TabIndex = 1;
            // 
            // frmImgEditor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 565);
            this.Controls.Add(this.tscContenedor);
            this.Controls.Add(this.ssEstado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmImgEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmImgEditor_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmImgEditor_DragOver);
            this.ssEstado.ResumeLayout(false);
            this.ssEstado.PerformLayout();
            this.tscContenedor.ContentPanel.ResumeLayout(false);
            this.tscContenedor.TopToolStripPanel.ResumeLayout(false);
            this.tscContenedor.TopToolStripPanel.PerformLayout();
            this.tscContenedor.ResumeLayout(false);
            this.tscContenedor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.tsHerramienta.ResumeLayout(false);
            this.tsHerramienta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssEstado;
        private System.Windows.Forms.ToolStripContainer tscContenedor;
        private System.Windows.Forms.ToolStrip tsHerramienta;
        private System.Windows.Forms.ToolStrip tsColores;
        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripStatusLabel tssSize;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tssCords;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
    }
}