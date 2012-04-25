//---------------------------------------------------------------------------
//  This file is part of the TwitShot software
//  Copyright (c) 2009, Ernesto Herrera Salinas <ernestohs@gmail.com>
//  All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are 
// permitted provided that the following conditions are met:
//
// - Redistributions of source code must retain the above copyright notice, this list 
//   of conditions and the following disclaimer.
// - Redistributions in binary form must reproduce the above copyright notice, this list 
//   of conditions and the following disclaimer in the documentation and/or other 
//   materials provided with the distribution.
// - Neither the name of TwitShot nor the names of its contributors may be 
//   used to endorse or promote products derived from this software without specific 
//   prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
// IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.
//---------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TwitShot.GUI
{
    public partial class frmLogIn : Form
    {
        public TwitShot.API.TwitPic TP;
        private System.Security.SecureString Secreto;
        private bool Flash = false;

        public frmLogIn()
        {
            InitializeComponent();
            this.TP = new TwitShot.API.TwitPic();
            this.DialogResult = DialogResult.No;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cerrar TwitShot?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }

        private void lnkRecuperar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Properties.Settings.Default.Twitter_ReSendPassword);
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            Iniciar();
        }

        private void DesactivarDisparadores()
        {
            this.btnIniciar.Enabled = false;
            this.txtPsw.Enabled = false;
        }

        private void Iniciar()
        {
            LimpiarArroba();
            TP.Username = this.txtUsr.Text;
            TP.Password = new TwitShot.API.StringPassword(Secreto);
            if (TP.VerifyCredentials())
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                MessageBox.Show(TP.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtUsr.Focus();
                this.DialogResult = DialogResult.No;
            }
        }

        private void LimpiarArroba()    ///TODO: Esto no funciona muy bien que digamos
        {
            if (txtUsr.Text.Substring(0,1).Equals('@')) {
                txtUsr.Text.Remove(0);
            }
        }

        private void txtPsw_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                Iniciar();
            }
        }

        private void lnkTwitterSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Properties.Settings.Default.Twitter_SignUp);
        }

        /// <summary>
        ///     Evento: Se dispara cuando el contenido de txtPsw cambia.
        /// </summary>
        /// <param name="sender">Objeto que disparo el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void txtPsw_TextChanged(object sender, EventArgs e)
        {
            SecurePassword();
        }

        /// <summary>
        ///     Graba la contraseña en un objeto System.Security.SecureString
        ///     <remarks>Aqui debe implementarse un mecanismo que prevenga que la contraseña quede expuesta.</remarks>
        /// </summary>
        private void SecurePassword()
        {
            if (this.Secreto != null) { this.Secreto.Dispose(); }
            this.Secreto = null;
            Secreto = new System.Security.SecureString();
            foreach (char letra in txtPsw.Text)
            {
                Secreto.AppendChar(letra);
            }
        }

        private void frmLogIn_Activated(object sender, EventArgs e)
        {
            if ( Flash )
            {
                TwitShot.API.WindowsFX.Stop(this);
                Flash = false;
            }
        }

        private void frmLogIn_Deactivate(object sender, EventArgs e)
        {
            if (!Flash)
            {
                TwitShot.API.WindowsFX.Start(this);
                Flash = true;
            }
        }
    }
}
