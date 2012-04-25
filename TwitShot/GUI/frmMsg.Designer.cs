namespace TwitShot.GUI
{
    partial class frmMsg
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
            this.Estado = new System.Windows.Forms.StatusStrip();
            this.tsslRestan = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslEnviando = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspgEnviando = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslEspace = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLink = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsOpciones = new System.Windows.Forms.ToolStrip();
            this.tsbCapturaPantalla = new System.Windows.Forms.ToolStripButton();
            this.tsbArea = new System.Windows.Forms.ToolStripButton();
            this.tsbMapa = new System.Windows.Forms.ToolStripButton();
            this.tsddbWebCam = new System.Windows.Forms.ToolStripDropDownButton();
            this.tlpDIH = new System.Windows.Forms.TableLayoutPanel();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.txtTwitt = new System.Windows.Forms.TextBox();
            this.tlpDIV = new System.Windows.Forms.TableLayoutPanel();
            this.pnObjs = new System.Windows.Forms.Panel();
            this.WB = new System.Windows.Forms.WebBrowser();
            this.pbWebCamContainer = new System.Windows.Forms.PictureBox();
            this.pbImagen = new System.Windows.Forms.PictureBox();
            this.tsHerrmienta = new System.Windows.Forms.ToolStrip();
            this.tssbSimbolos = new System.Windows.Forms.ToolStripSplitButton();
            this.Estado.SuspendLayout();
            this.tsOpciones.SuspendLayout();
            this.tlpDIH.SuspendLayout();
            this.tlpDIV.SuspendLayout();
            this.pnObjs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWebCamContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
            this.tsHerrmienta.SuspendLayout();
            this.SuspendLayout();
            // 
            // Estado
            // 
            this.Estado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslRestan,
            this.tsslEnviando,
            this.tspgEnviando,
            this.tsslEspace,
            this.tsslLink});
            this.Estado.Location = new System.Drawing.Point(0, 547);
            this.Estado.Name = "Estado";
            this.Estado.Size = new System.Drawing.Size(453, 22);
            this.Estado.TabIndex = 0;
            // 
            // tsslRestan
            // 
            this.tsslRestan.Name = "tsslRestan";
            this.tsslRestan.Size = new System.Drawing.Size(85, 17);
            this.tsslRestan.Text = "Caracteres: 140";
            // 
            // tsslEnviando
            // 
            this.tsslEnviando.Name = "tsslEnviando";
            this.tsslEnviando.Size = new System.Drawing.Size(55, 17);
            this.tsslEnviando.Text = "Enviando:";
            this.tsslEnviando.Visible = false;
            // 
            // tspgEnviando
            // 
            this.tspgEnviando.Name = "tspgEnviando";
            this.tspgEnviando.Size = new System.Drawing.Size(100, 16);
            this.tspgEnviando.Visible = false;
            // 
            // tsslEspace
            // 
            this.tsslEspace.Name = "tsslEspace";
            this.tsslEspace.Size = new System.Drawing.Size(10, 17);
            this.tsslEspace.Text = " ";
            // 
            // tsslLink
            // 
            this.tsslLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslLink.IsLink = true;
            this.tsslLink.Name = "tsslLink";
            this.tsslLink.Size = new System.Drawing.Size(182, 17);
            this.tsslLink.Text = "http://twitpic.com/photos/ernestohs";
            this.tsslLink.Click += new System.EventHandler(this.tsslLink_Click);
            // 
            // tsOpciones
            // 
            this.tsOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCapturaPantalla,
            this.tsbArea,
            this.tsbMapa,
            this.tsddbWebCam});
            this.tsOpciones.Location = new System.Drawing.Point(0, 0);
            this.tsOpciones.Name = "tsOpciones";
            this.tsOpciones.Size = new System.Drawing.Size(453, 25);
            this.tsOpciones.TabIndex = 1;
            // 
            // tsbCapturaPantalla
            // 
            this.tsbCapturaPantalla.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCapturaPantalla.Image = global::TwitShot.Properties.Resources.Camara;
            this.tsbCapturaPantalla.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCapturaPantalla.Name = "tsbCapturaPantalla";
            this.tsbCapturaPantalla.Size = new System.Drawing.Size(23, 22);
            this.tsbCapturaPantalla.Text = "Captura de Pantalla";
            this.tsbCapturaPantalla.Click += new System.EventHandler(this.tsbCapturaPantalla_Click);
            // 
            // tsbArea
            // 
            this.tsbArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArea.Image = global::TwitShot.Properties.Resources.Cortar;
            this.tsbArea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArea.Name = "tsbArea";
            this.tsbArea.Size = new System.Drawing.Size(23, 22);
            this.tsbArea.Text = "Área";
            this.tsbArea.Click += new System.EventHandler(this.tsbArea_Click);
            // 
            // tsbMapa
            // 
            this.tsbMapa.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMapa.Image = global::TwitShot.Properties.Resources.Pin;
            this.tsbMapa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMapa.Name = "tsbMapa";
            this.tsbMapa.Size = new System.Drawing.Size(23, 22);
            this.tsbMapa.Text = "Mapa";
            this.tsbMapa.Click += new System.EventHandler(this.tsbMapa_Click);
            // 
            // tsddbWebCam
            // 
            this.tsddbWebCam.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsddbWebCam.Image = global::TwitShot.Properties.Resources.WebCam;
            this.tsddbWebCam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbWebCam.Name = "tsddbWebCam";
            this.tsddbWebCam.Size = new System.Drawing.Size(29, 22);
            this.tsddbWebCam.Text = "toolStripDropDownButton1";
            this.tsddbWebCam.Click += new System.EventHandler(this.tsddbWebCam_Click);
            // 
            // tlpDIH
            // 
            this.tlpDIH.ColumnCount = 2;
            this.tlpDIH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.06264F));
            this.tlpDIH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.93736F));
            this.tlpDIH.Controls.Add(this.btnEnviar, 1, 0);
            this.tlpDIH.Controls.Add(this.txtTwitt, 0, 0);
            this.tlpDIH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDIH.Location = new System.Drawing.Point(3, 394);
            this.tlpDIH.Name = "tlpDIH";
            this.tlpDIH.RowCount = 1;
            this.tlpDIH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDIH.Size = new System.Drawing.Size(447, 125);
            this.tlpDIH.TabIndex = 1;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEnviar.Image = global::TwitShot.Properties.Resources.Mensaje;
            this.btnEnviar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnviar.Location = new System.Drawing.Point(342, 3);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(102, 119);
            this.btnEnviar.TabIndex = 0;
            this.btnEnviar.Text = "    &Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // txtTwitt
            // 
            this.txtTwitt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTwitt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTwitt.Location = new System.Drawing.Point(3, 3);
            this.txtTwitt.Multiline = true;
            this.txtTwitt.Name = "txtTwitt";
            this.txtTwitt.Size = new System.Drawing.Size(333, 119);
            this.txtTwitt.TabIndex = 1;
            this.txtTwitt.Text = "#TwitShot";
            this.txtTwitt.TextChanged += new System.EventHandler(this.txtTwitt_TextChanged);
            // 
            // tlpDIV
            // 
            this.tlpDIV.ColumnCount = 1;
            this.tlpDIV.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDIV.Controls.Add(this.tlpDIH, 0, 2);
            this.tlpDIV.Controls.Add(this.pnObjs, 0, 0);
            this.tlpDIV.Controls.Add(this.tsHerrmienta, 0, 1);
            this.tlpDIV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDIV.Location = new System.Drawing.Point(0, 25);
            this.tlpDIV.Name = "tlpDIV";
            this.tlpDIV.RowCount = 3;
            this.tlpDIV.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpDIV.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlpDIV.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpDIV.Size = new System.Drawing.Size(453, 522);
            this.tlpDIV.TabIndex = 2;
            // 
            // pnObjs
            // 
            this.pnObjs.Controls.Add(this.WB);
            this.pnObjs.Controls.Add(this.pbWebCamContainer);
            this.pnObjs.Controls.Add(this.pbImagen);
            this.pnObjs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnObjs.Location = new System.Drawing.Point(3, 3);
            this.pnObjs.Name = "pnObjs";
            this.pnObjs.Size = new System.Drawing.Size(447, 359);
            this.pnObjs.TabIndex = 2;
            // 
            // WB
            // 
            this.WB.AllowWebBrowserDrop = false;
            this.WB.IsWebBrowserContextMenuEnabled = false;
            this.WB.Location = new System.Drawing.Point(342, 13);
            this.WB.MinimumSize = new System.Drawing.Size(20, 20);
            this.WB.Name = "WB";
            this.WB.ScriptErrorsSuppressed = true;
            this.WB.ScrollBarsEnabled = false;
            this.WB.Size = new System.Drawing.Size(96, 87);
            this.WB.TabIndex = 2;
            this.WB.Url = new System.Uri("", System.UriKind.Relative);
            this.WB.Visible = false;
            // 
            // pbWebCamContainer
            // 
            this.pbWebCamContainer.BackColor = System.Drawing.Color.Lime;
            this.pbWebCamContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbWebCamContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbWebCamContainer.Location = new System.Drawing.Point(201, 211);
            this.pbWebCamContainer.Name = "pbWebCamContainer";
            this.pbWebCamContainer.Size = new System.Drawing.Size(131, 114);
            this.pbWebCamContainer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbWebCamContainer.TabIndex = 1;
            this.pbWebCamContainer.TabStop = false;
            this.pbWebCamContainer.Visible = false;
            // 
            // pbImagen
            // 
            this.pbImagen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pbImagen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbImagen.Location = new System.Drawing.Point(9, 13);
            this.pbImagen.Name = "pbImagen";
            this.pbImagen.Size = new System.Drawing.Size(327, 316);
            this.pbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImagen.TabIndex = 0;
            this.pbImagen.TabStop = false;
            // 
            // tsHerrmienta
            // 
            this.tsHerrmienta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssbSimbolos});
            this.tsHerrmienta.Location = new System.Drawing.Point(0, 365);
            this.tsHerrmienta.Name = "tsHerrmienta";
            this.tsHerrmienta.Size = new System.Drawing.Size(453, 25);
            this.tsHerrmienta.TabIndex = 3;
            this.tsHerrmienta.Text = "toolStrip1";
            // 
            // tssbSimbolos
            // 
            this.tssbSimbolos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tssbSimbolos.Image = global::TwitShot.Properties.Resources.Simbolo;
            this.tssbSimbolos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbSimbolos.Name = "tssbSimbolos";
            this.tssbSimbolos.Size = new System.Drawing.Size(32, 22);
            this.tssbSimbolos.Text = "Símbolos";
            // 
            // frmMsg
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 569);
            this.Controls.Add(this.tlpDIV);
            this.Controls.Add(this.tsOpciones);
            this.Controls.Add(this.Estado);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TwitShot";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.frmMsg_Activated);
            this.Deactivate += new System.EventHandler(this.frmMsg_Deactivate);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmMsg_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmMsg_DragOver);
            this.Estado.ResumeLayout(false);
            this.Estado.PerformLayout();
            this.tsOpciones.ResumeLayout(false);
            this.tsOpciones.PerformLayout();
            this.tlpDIH.ResumeLayout(false);
            this.tlpDIH.PerformLayout();
            this.tlpDIV.ResumeLayout(false);
            this.tlpDIV.PerformLayout();
            this.pnObjs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbWebCamContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
            this.tsHerrmienta.ResumeLayout(false);
            this.tsHerrmienta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip Estado;
        private System.Windows.Forms.ToolStripStatusLabel tsslRestan;
        private System.Windows.Forms.ToolStripStatusLabel tsslEnviando;
        private System.Windows.Forms.ToolStripProgressBar tspgEnviando;
        private System.Windows.Forms.ToolStripStatusLabel tsslEspace;
        private System.Windows.Forms.ToolStripStatusLabel tsslLink;
        private System.Windows.Forms.ToolStrip tsOpciones;
        private System.Windows.Forms.ToolStripButton tsbCapturaPantalla;
        private System.Windows.Forms.ToolStripDropDownButton tsddbWebCam;
        private System.Windows.Forms.TableLayoutPanel tlpDIH;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.TextBox txtTwitt;
        private System.Windows.Forms.TableLayoutPanel tlpDIV;
        private System.Windows.Forms.Panel pnObjs;
        private System.Windows.Forms.WebBrowser WB;
        private System.Windows.Forms.PictureBox pbWebCamContainer;
        private System.Windows.Forms.PictureBox pbImagen;
        private System.Windows.Forms.ToolStripButton tsbMapa;
        private System.Windows.Forms.ToolStrip tsHerrmienta;
        private System.Windows.Forms.ToolStripSplitButton tssbSimbolos;
        private System.Windows.Forms.ToolStripButton tsbArea;
    }
}