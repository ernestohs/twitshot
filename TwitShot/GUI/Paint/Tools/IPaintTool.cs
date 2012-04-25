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

using System.Drawing;

namespace TwitShot.GUI.Paint.Tools
{
    public interface IPaintTool : System.IDisposable
    {
        #region Propiedades

        /// <summary>
        ///     Graphics donde se dibuja la forma
        /// </summary>
        Graphics Canvas { get; }
        /// <summary>
        ///     Brush de la herramienta
        /// </summary>
        Pen Pen { get; set; }
        /// <summary>
        ///     Color de dibujo
        /// </summary>
        Color ForeColor { get; set; }
        /// <summary>
        ///     Color Fondo
        /// </summary>
        Color BackgroundColor { get; set; }
        /// <summary>
        ///     Punto inicial de dibujo
        /// </summary>
        Point StartPoint { get; set; }
        /// <summary>
        ///     Punto final de dibujo
        /// </summary>
        Point EndPoint { get; set; }
        /// <summary>
        ///     Tamaño del objeto
        /// </summary>
        Size Size { get; }
        /// <summary>
        ///     Icono de la Herramienta
        /// </summary>
        Icon Icon { get; }
        #endregion

        #region Métodos
            /// <summary>
            ///     Método de Dibujo
            /// </summary>
            void Draw();

            /// <summary>
            ///     Convierte 2 puntos en un rectangulo
            /// </summary>
            /// <param name="originPoint"></param>
            /// <param name="endPoint"></param>
            /// <returns>Rectangle</returns>
            Rectangle MakeRectangle(Point originPoint, Point endPoint);
        #endregion
    }
}
