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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TwitShot.API;

namespace TwitShot.GUI
{
    public partial class frmMsg : Form
    {

        #region WinAPI32
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, System.Int32 dwRop);

        private const Int32 SRCCOPY = 0xCC0020; 
        #endregion

        #region Simbolos
        public string[] Simbolos = {
            "♥",            "✈",            "☺",            "♬",            "☑",            "♠",
            "☎",            "☻",            "♫",            "☒",            "♤",            "☤",
            "☹",            "♪",            "♀",            "✩",            "✉",            "☠",
            "✔",            "♂",            "★",            "✇",            "♺",            "✖",
            "♨",            "❦",            "☁",            "✌",            "♛",            "❁",
            "☪",            "☂",            "✏",            "♝",            "❀",            "☭",
            "☃",            "☛",            "♞",            "✿",            "☮",            "☼",
            "☚",            "♘",            "✾",            "☯",            "☾",            "☝",
            "♖",            "✽",            "✝",            "☄",            "☟",            "♟",
            "✺",            "☥",            "✂",            "✍",            "♕",            "✵",
            "☉",            "☇",            "☈",            "☡",            "✠",            "☊",
            "☋",            "☌",            "☍",            "♁",            "✇",            "☢",
            "☣",            "✣",            "✡",            "☞",            "☜",            "✜",
            "✛",            "❥",            "♈",            "♉",            "♊",            "♋",
            "♌",            "♍",            "♎",            "♏",            "♐",            "♑",
            "♒",            "♓",            "☬",            "☫",            "☨",            "☧",
            "☦",            "✁",            "✃",            "✄",            "✎",            "✐",
            "❂",            "❉",            "❆",            "♅",            "♇",            "♆",
            "♙",            "♟",            "♔",            "♕",            "♖",            "♗",
            "♘",            "♚",            "♛",            "♜",            "♝",            "♞",
            "©",            "®",            "™",            "…",            "∞",            "¥",
            "€",            "£",            "ƒ",            "$",            "≤",            "≥",
            "∑",            "«",            "»",            "ç",            "∫",            "µ",
            "◊",            "ı",            "∆",            "Ω",            "≈",            "*",
            "§",            "•",            "¶",            "¬",            "†",            "&",
            "¡",            "¿",            "ø",            "å",            "∂",            "œ",
            "Æ",            "æ",            "π",            "ß",            "÷",            "‰",
            "√",            "≠",            "%",            "˚",            "ˆ",            "˜",
            "˘",            "¯",            "∑",            "º",            "ª",            "‽"}; 
        #endregion

        #region Propiedades
        private TwitShot.API.TwitPic TP;///<summary>Objeto de API de Twitpic</summary>
        private TwitShot.API.App fnc;///<summary>Objeto de funciones de aplicación</summary>
        private bool Precos_Sending;///<summary>Variable de control de envio precos</summary>
        private WebCam WebCamObj = new WebCam();        
        #endregion

        #region .ctor
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="tp">Objeto TwitpicAPI</param>
        public frmMsg(TwitShot.API.TwitPic tp)
        {
            this.Icon = global::TwitShot.Properties.Resources.twitshot;
            fnc = new TwitShot.API.App();
            InitializeComponent();
            this.pbImagen.Dock = DockStyle.Fill;
            tsslLink.Text = Properties.Settings.Default.TwitPicPhotos + tp.Username;
            TP = tp;
            CapturaPantalla();
            this.ContadorCaracteres();
            this.DialogResult = DialogResult.No;
            this.Precos_Sending = true;
            foreach (String item in WebCamObj.CaptureDevices)
            {
                tsddbWebCam.DropDownItems.Add(item, tsddbWebCam.Image, ChangeWebCamSource);
            }
            if (tsddbWebCam.DropDownItems.Count == 0) {
                tsddbWebCam.Enabled = false;
            }
            foreach (string item in Simbolos)
        	{
                tssbSimbolos.DropDownItems.Add(item, null, ClickSimbolo);
        	}
            
        }

        private void CapturaPantalla()
        {
            fnc.ScreenShot();
            pbImagen.Image = fnc.Image;
        }

        public void ClickSimbolo(object sender, EventArgs e)
        {
            txtTwitt.Text = txtTwitt.Text.Insert(txtTwitt.SelectionStart, ((ToolStripItem)sender).Text);
        }

        public void ChangeWebCamSource(object sender, EventArgs e)
        {
            // Cambiar el dispositivo
            ToolStripItem o = (ToolStripItem)sender;
            WebCamObj.DeviceID = WebCamObj.CaptureDevices.IndexOf(o.Text);
            pbImagen.Dock = WB.Dock = DockStyle.None;
            pbImagen.Visible = WB.Visible = false;
            WebCamObj.Container = pbWebCamContainer;
            pbWebCamContainer.Dock = DockStyle.Fill;
            pbWebCamContainer.Visible = true;
            WebCamObj.Start();
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="tp">Objeto TwitpicAPI</param>
        /// <param name="captura">Imagen que se va a enviar</param>
        public frmMsg(TwitShot.API.TwitPic tp, Image captura)
        {
            this.Icon = global::TwitShot.Properties.Resources.twitshot;
            fnc = new TwitShot.API.App();
            InitializeComponent();
            this.pbImagen.Dock = DockStyle.Fill;
            tsslLink.Text = Properties.Settings.Default.TwitPicPhotos + tp.Username;
            TP = tp;
            this.Precos_Sending = true;
            pbImagen.Image = captura;
        } 
        #endregion  

        #region Métodos de Soporte
        /// <summary>
        ///     Cuenta los caracteres escritos y evita que se escriba de más en el mensaje.
        /// </summary>
        private void ContadorCaracteres()
        {
            int CarRes = Restan();
            if (CarRes <= 0) { this.tsslRestan.ForeColor = Color.Red; } else if (CarRes >= 0) { this.tsslRestan.ForeColor = this.txtTwitt.ForeColor; }
            this.tsslRestan.Text = "Restan: " + String.Format("{0}", CarRes);
        }

        private int Restan()
        {
            return 113 - this.txtTwitt.TextLength;
        }

        /// <summary>
        ///     Realiza el proceso de envio de la imagen a twitpic.com
        /// </summary>
        private void EnviarImagen()
        {
            if (Restan() >= 0)
            {
                if (pbImagen.Visible)
                {
                    this.WindowState = FormWindowState.Minimized;
                    this.Hide();
                    // TODO: Agregar barra de progreso de envio
                    if (TP.UploadPhoto(this.pbImagen.Image, this.txtTwitt.Text))
                    {
                        this.DialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        MessageBox.Show("La imagen no se pudo enviar el mensaje es: " + TP.Message, "Fallo al enviar imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    this.Close();
                }
                else if (pbWebCamContainer.Visible)
                {
                    Image img = WebCamObj.Image;
                    WebCamObj.Stop();
                    this.WindowState = FormWindowState.Minimized;
                    this.Hide();
                    // TODO: Agregar barra de progreso de envio
                    if (TP.UploadPhoto(img, this.txtTwitt.Text))
                    {
                        this.DialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        MessageBox.Show("La imagen no se pudo enviar el mensaje es: " + TP.Message, "Fallo al enviar imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    this.Close();
                }
                else if (WB.Visible)
                {
                    Bitmap memImage;
                    Graphics graphic = this.WB.CreateGraphics();
                    Size s = this.WB.Size;
                    memImage = new Bitmap(s.Width, s.Height, graphic);
                    Graphics memGraphic = Graphics.FromImage(memImage);
                    IntPtr dc1 = graphic.GetHdc();
                    IntPtr dc2 = memGraphic.GetHdc();
                    BitBlt(dc2, 0, 0, this.WB.ClientRectangle.Width,
                    this.WB.ClientRectangle.Height, dc1, 0, 0, SRCCOPY);
                    graphic.ReleaseHdc(dc1);
                    memGraphic.ReleaseHdc(dc2);

                    this.WindowState = FormWindowState.Minimized;
                    this.Hide();
                    // TODO: Agregar barra de progreso de envio
                    if (TP.UploadPhoto((Image)memImage, this.txtTwitt.Text))
                    {
                        this.DialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        MessageBox.Show("La imagen no se pudo enviar el mensaje es: " + TP.Message, "Fallo al enviar imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("El mensaje es muy largo", "Aún no", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.txtTwitt.Focus();
            }
        } 
        #endregion

        private void tsslLink_Click(object sender, EventArgs e)
        {
            Process.Start(tsslLink.Text);
        }

        private void txtTwitt_TextChanged(object sender, EventArgs e)
        {
            ContadorCaracteres();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            EnviarImagen();
        }

        private void tsbMapa_Click(object sender, EventArgs e)
        {
            pbImagen.Dock = DockStyle.None;
            pbImagen.Visible = false;
            WB.Dock = DockStyle.Fill;
            WB.Visible = true;
            WB.Navigate("about:blank");
            WB.DocumentText = "<html><head></head><body><iframe width=\"100%\" height=\"100%\" style=\"top:0px; left:0px; overflow:visible; width:100%; height:100%;\" scrolling=\"no\" marginheight=\"0\" marginwidth=\"0\" src=\"http://maps.google.com/?ie=UTF8&amp;hq=&amp;hnear=Morelia,+Michoac%C3%A1n+de+Ocampo,+M%C3%A9xico&amp;output=embed\"></iframe></body></html>";
        }

        private void frmMsg_DragOver(object sender, DragEventArgs e)
        {   
            if (e.Data.GetDataPresent(DataFormats.FileDrop) ||
                e.Data.GetDataPresent(DataFormats.Text) || 
                e.Data.GetDataPresent(DataFormats.Bitmap) ||
                e.Data.GetDataPresent(DataFormats.Html) ||
                e.Data.GetDataPresent(DataFormats.MetafilePict) ||
                e.Data.GetDataPresent(DataFormats.EnhancedMetafile))
            {
                if ((e.AllowedEffect & DragDropEffects.All) != 0)
                    e.Effect = DragDropEffects.All;
            }
        }

        private void frmMsg_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] filepath = (string[])e.Data.GetData(DataFormats.FileDrop);
                    Image image = Image.FromFile(filepath[0]);
                    this.pbImagen.Image = image;
                }
                else if (e.Data.GetDataPresent(DataFormats.Text))
                {
                    try
                    {
                        string texto = (string)e.Data.GetData(DataFormats.Text);
                        if (texto.IndexOf("://") != -1)
                        {
                            this.pbImagen.Image = TP.GetImageFromUrl(texto);
                        }
                        else if (texto.IndexOf(":\\") != -1)
                        {
                            this.pbImagen.Image = Image.FromFile(texto);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }

                }
                else if (e.Data.GetDataPresent(DataFormats.Html))
                {
                    List<Image> ImagesThumbs = new List<Image>();
                    String Contenido = (String)e.Data.GetData(DataFormats.Html);
                    Regex rx = new Regex("<img src=\"(.+)\"", RegexOptions.Compiled);
                    MatchCollection matches = rx.Matches(Contenido);// ALERTA: Aquí Contenido puede ser Null y vale M
                    foreach (Match match in matches)
                    {
                        GroupCollection groups = match.Groups;
                        ImagesThumbs.Add(TP.GetImageFromUrl(groups[1].Value));///MediaUrl
                    }
                    this.pbImagen.Image = ImagesThumbs[0];
                }
                else if (e.Data.GetDataPresent(DataFormats.Bitmap))
                {
                    this.pbImagen.Image = (Image)e.Data.GetData(DataFormats.Bitmap);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void frmMsg_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void frmMsg_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = 0.3;
        }

        private void tsddbWebCam_Click(object sender, EventArgs e)
        {
            /*pbImagen.Dock = DockStyle.None;
            pbImagen.Visible = false;
            WebCamObj.Container = pbWebCamContainer;
            pbWebCamContainer.Dock = DockStyle.Fill;
            pbWebCamContainer.Visible = true;
            WebCamObj.Start();*/
        }

        private void tsbCapturaPantalla_Click(object sender, EventArgs e)
        {
            pbWebCamContainer.Dock = WB.Dock = DockStyle.None;
            WB.Visible = pbWebCamContainer.Visible = false;
            pbImagen.Dock = DockStyle.Fill;
            pbImagen.Visible = true;
            this.Hide();
            this.CapturaPantalla();
            this.Show();
        }

        private void tsbArea_Click(object sender, EventArgs e)
        {
            pbWebCamContainer.Dock = WB.Dock = DockStyle.None;
            WB.Visible = pbWebCamContainer.Visible = false;
            pbImagen.Dock = DockStyle.Fill;
            pbImagen.Visible = true;
            this.Hide();
            frmCrop fc = new frmCrop();
            fc.ShowDialog();
            pbImagen.Image = fc.Captura;
            this.Show();
        }

    }
}
