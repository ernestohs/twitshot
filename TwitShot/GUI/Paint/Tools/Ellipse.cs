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
using System.Drawing;

namespace TwitShot.GUI.Paint.Tools
{
    public class Ellipse: IDisposable, IPaintTool
    {
        #region .ctor
            /// <summary>
            ///     Constructor
            /// </summary>
            /// <param name="g">Graphics del Canvas donde se dibuja la elipse</param>
            public Ellipse(Graphics g)
            {
                this._Icon = null;//new Icon("NoImplementado.ico");
                this._Canvas = g;
            } 
        #endregion

        #region Métodos
            #region Miembros de IDisposable
                /// <summary>
                /// 
                /// </summary>
                public void Dispose()
                {
                    // Limpieza a la casa
                    GC.SuppressFinalize(this);
                }
            #endregion

            #region Miembros de IPaintTool

            #region Valores
                private Graphics _Canvas;
                private Pen _Pen;
                private Color _ForeColor;
                private Color _BackgroundColor;
                private Point _StartPoint;
                private Point _EndPoint;
                private Icon _Icon;
            #endregion

            public Graphics Canvas
            {
                get { return this._Canvas; }
            }

            public Pen Pen
            {
                get
                {
                    return this._Pen;
                }
                set
                {
                    this._Pen = value;
                }
            }

            public Color ForeColor
            {
                get
                {
                    return this._ForeColor;
                }
                set
                {
                    this._ForeColor = value;
                }
            }

            public Color BackgroundColor
            {
                get
                {
                    return this._BackgroundColor;
                }
                set
                {
                    this._BackgroundColor = value;
                }
            }

            public Point StartPoint
            {
                get
                {
                    return this._StartPoint;
                }
                set
                {
                    this._StartPoint = value;
                }
            }

            public Point EndPoint
            {
                get
                {
                    return this._EndPoint;
                }
                set
                {
                    this._EndPoint = value;
                }
            }

            public Size Size
            {
                get
                {
                    return MakeRectangle(StartPoint, EndPoint).Size;
                }
            }

            public Icon Icon
            {
                get { return this._Icon; }
            }

            public void Draw()
            {
                Canvas.DrawEllipse(this.Pen, MakeRectangle(StartPoint, EndPoint));
            }

            public Rectangle MakeRectangle(Point originPoint, Point endPoint)
            {
                Rectangle Rectangulo = new Rectangle(0, 0, 0, 0);
                if (originPoint.X > endPoint.X)
                    Rectangulo.X = endPoint.X;
                else
                    Rectangulo.X = originPoint.X;

                if (originPoint.Y > endPoint.Y)
                    Rectangulo.Y = endPoint.Y;
                else
                    Rectangulo.Y = originPoint.Y;

                Rectangulo.Width = (originPoint.X > endPoint.X) ? originPoint.X - endPoint.X : endPoint.X - originPoint.X;
                Rectangulo.Height = (originPoint.Y > endPoint.Y) ? originPoint.Y - endPoint.Y : endPoint.Y - originPoint.Y;

                return Rectangulo;
            }

            #endregion 
        #endregion   
    }
}
