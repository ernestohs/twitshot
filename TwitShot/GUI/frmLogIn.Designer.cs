namespace TwitShot.GUI
{
    partial class frmLogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogIn));
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsr = new System.Windows.Forms.TextBox();
            this.txtPsw = new System.Windows.Forms.TextBox();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.chkGuardar = new System.Windows.Forms.CheckBox();
            this.lnkRecuperar = new System.Windows.Forms.LinkLabel();
            this.lnkTwitterSignUp = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(9, 15);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(81, 13);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario Twitter:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(26, 41);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(64, 13);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Contraseña:";
            // 
            // txtUsr
            // 
            this.txtUsr.Location = new System.Drawing.Point(96, 12);
            this.txtUsr.Name = "txtUsr";
            this.txtUsr.Size = new System.Drawing.Size(100, 20);
            this.txtUsr.TabIndex = 2;
            // 
            // txtPsw
            // 
            this.txtPsw.Location = new System.Drawing.Point(96, 38);
            this.txtPsw.Name = "txtPsw";
            this.txtPsw.Size = new System.Drawing.Size(100, 20);
            this.txtPsw.TabIndex = 3;
            this.txtPsw.UseSystemPasswordChar = true;
            this.txtPsw.TextChanged += new System.EventHandler(this.txtPsw_TextChanged);
            this.txtPsw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPsw_KeyUp);
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(202, 10);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 4;
            this.btnIniciar.Text = "&Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(202, 38);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "&Salir";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // chkGuardar
            // 
            this.chkGuardar.AutoSize = true;
            this.chkGuardar.Location = new System.Drawing.Point(185, 97);
            this.chkGuardar.Name = "chkGuardar";
            this.chkGuardar.Size = new System.Drawing.Size(87, 17);
            this.chkGuardar.TabIndex = 6;
            this.chkGuardar.Text = "&Recuerdame";
            this.chkGuardar.UseVisualStyleBackColor = true;
            // 
            // lnkRecuperar
            // 
            this.lnkRecuperar.AutoSize = true;
            this.lnkRecuperar.Location = new System.Drawing.Point(9, 84);
            this.lnkRecuperar.Name = "lnkRecuperar";
            this.lnkRecuperar.Size = new System.Drawing.Size(119, 13);
            this.lnkRecuperar.TabIndex = 7;
            this.lnkRecuperar.TabStop = true;
            this.lnkRecuperar.Tag = "";
            this.lnkRecuperar.Text = "¿Olvido su contraseña?";
            this.lnkRecuperar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRecuperar_LinkClicked);
            // 
            // lnkTwitterSignUp
            // 
            this.lnkTwitterSignUp.AutoSize = true;
            this.lnkTwitterSignUp.Location = new System.Drawing.Point(9, 104);
            this.lnkTwitterSignUp.Name = "lnkTwitterSignUp";
            this.lnkTwitterSignUp.Size = new System.Drawing.Size(137, 13);
            this.lnkTwitterSignUp.TabIndex = 8;
            this.lnkTwitterSignUp.TabStop = true;
            this.lnkTwitterSignUp.Text = "No tengo cuenta de Twitter";
            this.lnkTwitterSignUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTwitterSignUp_LinkClicked);
            // 
            // frmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 126);
            this.Controls.Add(this.lnkTwitterSignUp);
            this.Controls.Add(this.lnkRecuperar);
            this.Controls.Add(this.chkGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.txtPsw);
            this.Controls.Add(this.txtUsr);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsuario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TwitShot";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.frmLogIn_Activated);
            this.Deactivate += new System.EventHandler(this.frmLogIn_Deactivate);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.LinkLabel lnkRecuperar;
        public System.Windows.Forms.TextBox txtUsr;
        public System.Windows.Forms.TextBox txtPsw;
        public System.Windows.Forms.CheckBox chkGuardar;
        private System.Windows.Forms.LinkLabel lnkTwitterSignUp;
    }
}