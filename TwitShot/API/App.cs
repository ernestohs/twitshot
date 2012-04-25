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

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TwitShot.API
{
    class App
    {
        #region Propiedades
            
            /// <summary>
            ///     Imagen almacenada.
            /// </summary>
            public Image Image;
            /// <summary>
            ///     Lista de imagenes almacenadas
            /// </summary>
            public List<Image> Images;
            /// <summary>
            ///     Usar simulación de flash
            ///     <remarks>Esto hace que se resalten los colores en la imagen.</remarks>
            /// </summary>
            public bool Flash { get; set; }
            /// <summary>
            ///     Tiempo de espera en milisegundos antes de tomar la captura.
            /// </summary>
            public int Wait { get; set; }

            /// <summary>
            ///     Número de capturas almacenadas.
            ///     <remarks>Solo lectura.</remarks>
            /// </summary>
            public int Count
            {
                get
                {
                    return Images.Count;
                }
            }

        #endregion

        #region .ctor

            /// <summary>
            ///     Constructor de la clase.
            /// </summary>
            public App()
            {
                Images = new List<Image>();
            }
        
        #endregion

        #region Métodos

            /// <summary>
            ///     Toma una captura de pantalla y la almacena la misma en la propiedad Image.
            ///     <remarks>Agrega la imagen a la lista interna.</remarks>
            /// </summary>
            public void ScreenShot()
            {
#if IMPLEMENTACION1
                this.CropArea(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
#else
                var BMP = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                Graphics gr = Graphics.FromImage(BMP);
                gr.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                Image = BMP;
                Images.Add(Image);
#endif
            }

            #region public void CropArea(+3 Sobrecargas)

                /// <summary>
                ///     Captura un area de la pantalla.
                /// </summary>
                /// <param name="r">Area en formato System.Drawing.Rectangle</param>
                public void CropArea(Rectangle r)
                {
                    var BMP = new Bitmap(r.Width, r.Height, PixelFormat.Format32bppArgb);
                    Graphics gr = Graphics.FromImage(BMP);
                    gr.CopyFromScreen(new Point(r.X, r.Y), new Point(0, 0), r.Size, CopyPixelOperation.SourceCopy);
                    Image = BMP;
                    Images.Add(Image);
                }
                
                /// <summary>
                ///     Captura un area de la pantalla.
                /// </summary>
                /// <param name="p">Punto en la pantalla de donde se va a capturar.</param>
                /// <param name="s">Tamaño del area de captura.</param>
                public void CropArea(Point p, Size s)
                {
                    CropArea(new Rectangle(p, s));
                }

                /// <summary>
                ///     Captura un area de la pantalla.
                /// </summary>
                /// <param name="x">Coordenada x del punto de captura en pantalla.</param>
                /// <param name="y">Coordenada y del punto de captura en pantalla.</param>
                /// <param name="width">Ancho del area de captura.</param>
                /// <param name="height">Alto del area de captura.</param>
                public void CropArea(int x, int y, int width, int height)
                {
                    CropArea(new Rectangle(x, y, width, height));
                }
            
            #endregion

        #endregion
    }
}
