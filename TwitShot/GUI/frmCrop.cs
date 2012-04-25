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
using System.ComponentModel;
using System.Windows.Forms;

namespace TwitShot.GUI
{
    class frmCrop : System.Windows.Forms.Form
    {
        #region Propiedades Extra Relacionadas con el Formulario
        /// <summary>
        ///     Contenedor de componentes
        /// </summary>
        private System.ComponentModel.IContainer Componentes;
        /// <summary>
        ///     Enumeracion de las areas del formulario
        /// </summary>
        private enum ResizeRegion
        {
            None, N, NE, E, SE, S, SW, W, NW
        } 
        #endregion

        #region Constantes para dibujo
            private const int BORDE_ANCHO_RESIZE = 10;
            private const int BORDE_ANCHO_INVISIBLE = 60;
            private const int INDICADOR_ALTURA = 15;
            private const int INDICADOR_ANCHO_SUPERIOR = 45;
            private const int INDICADOR_ANCHO_INFERIOR = 60;        
        #endregion

        #region Propiedades Internas
            /// <summary>
            ///     Coordenada X de la mitad
            /// </summary>
            private int _mitadX;
            /// <summary>
            ///     Coordenada Y de la mitad
            /// </summary>
            private int _mitadY;
            /// <summary>
            ///     Fuente de la pestañita que indica el alto y ancho en pixeles
            /// </summary>
            private Font _guiaFont = new Font("Verdana", 8f);
            /// <summary>
            ///     
            /// </summary>
            private Point _offset;
            /// <summary>
            ///     Punto donde se presiona el ratón.
            /// </summary>
            private Point _mouseDownPoint;
            /// <summary>
            ///     Rectangulo donde se presiona el ratón
            /// </summary>
            private Rectangle _mouseDownRect;
            /// <summary>
            ///     Puntos para dibujar las pestañitas
            /// </summary>
            private Point _punto1 = new Point(BORDE_ANCHO_INVISIBLE - INDICADOR_ALTURA, BORDE_ANCHO_INVISIBLE - INDICADOR_ALTURA);
            private Point _punto2 = new Point(BORDE_ANCHO_INVISIBLE + INDICADOR_ANCHO_SUPERIOR, BORDE_ANCHO_INVISIBLE - INDICADOR_ALTURA);
            private Point _punto3 = new Point(BORDE_ANCHO_INVISIBLE + INDICADOR_ANCHO_INFERIOR, BORDE_ANCHO_INVISIBLE);
            private Point _punto4 = new Point(BORDE_ANCHO_INVISIBLE, BORDE_ANCHO_INVISIBLE);
            private Point _punto5 = new Point(BORDE_ANCHO_INVISIBLE, BORDE_ANCHO_INVISIBLE + INDICADOR_ANCHO_INFERIOR);
            private Point _punto6 = new Point(BORDE_ANCHO_INVISIBLE - INDICADOR_ALTURA, BORDE_ANCHO_INVISIBLE + INDICADOR_ANCHO_SUPERIOR);
            /// <summary>
            ///     Colores de los elementos que vamos a dibujar
            /// </summary>
            private SolidBrush _indicadorBrush = new SolidBrush(Color.LightSteelBlue);
            private SolidBrush _areaBrush = new SolidBrush(Color.White);
            private SolidBrush _textoBrush = new SolidBrush(Color.Black);
            private ResizeRegion _resizeRegion = ResizeRegion.None;        
        #endregion

        /// <summary>
        ///     Imagen del área capturada.
        ///     <remarks>Esta propiedad queda publica para que el parent obtenga la imagen ya en formato System.Drawing.Image</remarks>
        /// </summary>
        public Image Captura;
        /// <summary>
        ///     Objeto de funciones de aplicación, en este caso se usa para capturar el área de la pantalla.
        /// </summary>
        public API.App fnc;

        #region .ctor
            /// <summary>
            ///     Constructor del formulario
            /// </summary>
            public frmCrop()
            {
                InitializeComponents();
                this.fnc = new TwitShot.API.App();
            }        
        #endregion

        #region Suplente del Diseñador
            /// <summary>
            ///     Ya que el diseñador de Windows Forms se me puso reina, 
            ///     tuve que implementar todo esto a pata Y_Y
            /// </summary>
            private void InitializeComponents()
            {
                this.Componentes = new System.ComponentModel.Container();

                System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMsg)); // Me da hueva hacer uno, así que copio el de otro formulario.

                #region Propiedades de Formulario
                    this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
                    this.BackColor = System.Drawing.Color.Magenta;
                    this.ClientSize = new System.Drawing.Size(300, 300);
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                    this.Name = "frmCrop";
                    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    this.Text = "Capturar Area";
                    this.TransparencyKey = System.Drawing.Color.Magenta;
                    this.Opacity = 0.4;
                    this.TopMost = true; 
                    //this.DoubleBuffered = true;
                #endregion

                #region SetStyle
                    SetStyle(ControlStyles.UserPaint, true);
                    SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                    SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                    SetStyle(ControlStyles.UserMouse, true); 
                #endregion
            }        
        #endregion
        
        #region Controles de Visibilidad & Área

            /// <summary>
            ///     Debido a que las propiedades del formulario Width, Height, Top, Left y Size 
            ///     no son las mismas que la del área visible se usan estas propiedades.
            /// </summary>
            #region Propiedades de visibilidad

            private int VisibleWidth
            {
                get
                {
                    return this.Width - (BORDE_ANCHO_INVISIBLE * 2);
                }
                set
                {
                    this.Width = value + (BORDE_ANCHO_INVISIBLE * 2);
                }
            }
            
            private int VisibleHeight
            {
                get
                {
                    return this.Height - (BORDE_ANCHO_INVISIBLE * 2);
                }
                set
                {
                    this.Height = value + (BORDE_ANCHO_INVISIBLE * 2);
                }
            }

            private int VisibleLeft
            {
                get
                {
                    return this.Left + (BORDE_ANCHO_INVISIBLE);
                }
                set
                {
                    this.Left = value - (BORDE_ANCHO_INVISIBLE);
                }

            }

            private int VisibleTop
            {
                get
                {
                    return this.Top + (BORDE_ANCHO_INVISIBLE);
                }
                set
                {
                    this.Top = value - (BORDE_ANCHO_INVISIBLE);
                }
            }

            private Size VisibleClientSize
            {
                set
                {
                    Width = value.Width + (BORDE_ANCHO_INVISIBLE * 2);
                    Height = value.Height + (BORDE_ANCHO_INVISIBLE * 2);
                }
            }

            private Rectangle VisibleClientRectangle
            {
                get
                {
                    Rectangle visibleClient = new Rectangle(BORDE_ANCHO_INVISIBLE,
                        BORDE_ANCHO_INVISIBLE,
                        VisibleWidth - 1,
                        VisibleHeight - 1);
                    return visibleClient;
                }
            }

            #endregion

            /// <summary>
            ///     Determina si se encuentra en el área para cambiar de tamaño el formulario.
            /// </summary>
            /// <returns>bool regresa true si esta dentro del área indicada</returns>
            private bool IsInResizeArea()
            {
                Point clientCursorPos = PointToClient(MousePosition);

                Rectangle clientVisibleRect = ClientRectangle;
                clientVisibleRect.Inflate(-BORDE_ANCHO_INVISIBLE, -BORDE_ANCHO_INVISIBLE);

                Rectangle resizeInnerRect = clientVisibleRect;
                resizeInnerRect.Inflate(-BORDE_ANCHO_RESIZE, -BORDE_ANCHO_RESIZE);

                return (clientVisibleRect.Contains(clientCursorPos) && !resizeInnerRect.Contains(clientCursorPos));
            }

            /// <summary>
            ///     Determina si el puntero del ratón se encuentra en cierta área.
            /// </summary>
            /// <param name="rectangle">área rectangular donde se quiere saber si esta el puntero del ratón.</param>
            /// <returns>bool regresa true si esta dentro del área indicada</returns>
            private bool IsMouseInRectangle(Rectangle rectangle)
            {
                Point clientCursorPos = PointToClient(MousePosition);
                return (rectangle.Contains(clientCursorPos));
            }

        #endregion

        #region Métodos
            
            private ResizeRegion GetResizeRegion()
            {
                Point clientCursorPos = PointToClient(MousePosition);
                if ((clientCursorPos.X >= (Width - (BORDE_ANCHO_INVISIBLE + BORDE_ANCHO_RESIZE))) & (clientCursorPos.Y >= (Height - (BORDE_ANCHO_INVISIBLE + BORDE_ANCHO_RESIZE))))
                    return ResizeRegion.SE;
                else if (clientCursorPos.X >= (Width - (BORDE_ANCHO_INVISIBLE + BORDE_ANCHO_RESIZE)))
                    return ResizeRegion.E;
                else if (clientCursorPos.Y >= (Height - (BORDE_ANCHO_INVISIBLE + BORDE_ANCHO_RESIZE)))
                    return ResizeRegion.S;
                else
                    return ResizeRegion.None;
            }

            private void CenterSize(int interval)
            {
                if (interval > 100)
                    throw new ArgumentOutOfRangeException("interval", interval, "El intervalo no debe ser mayor de 100");

                if (VisibleWidth > interval & VisibleHeight > interval)
                {
                    int interval2 = interval * 2;
                    Width = Width - interval2;
                    Height = Height - interval2;
                    Left = Left + interval;
                    Top = Top + interval;
                }
            }

            private void HandleResize()
            {
                int diffX = MousePosition.X - _mouseDownPoint.X;
                int diffY = MousePosition.Y - _mouseDownPoint.Y;
                switch (_resizeRegion)
                {
                    case ResizeRegion.E:
                        Width = _mouseDownRect.Width + diffX;
                        break;
                    case ResizeRegion.S:
                        Height = _mouseDownRect.Height + diffY;
                        break;
                    case ResizeRegion.SE:
                        Width = _mouseDownRect.Width + diffX;
                        Height = _mouseDownRect.Height + diffY;
                        break;
                }
            }

            private void SetResizeCursor(ResizeRegion region)
            {
                switch (region)
                {
                    case ResizeRegion.N:
                    case ResizeRegion.S:
                        Cursor = Cursors.SizeNS;
                        break;

                    case ResizeRegion.E:
                        Cursor = Cursors.SizeWE;
                        break;
                    case ResizeRegion.W:

                    case ResizeRegion.NW:
                    case ResizeRegion.SE:
                        Cursor = Cursors.SizeNWSE;
                        break;

                    default:
                        Cursor = Cursors.Default;
                        break;
                }
            }

            private void AjustarPosicion(int interval, Keys keys)
            {
                switch (keys)
                {
                    case Keys.Left:
                        Left = Left - interval;
                        break;
                    case Keys.Right:
                        Left = Left + interval;
                        break;
                    case Keys.Up:
                        Top = Top - interval;
                        break;
                    case Keys.Down:
                        Top = Top + interval;
                        break;
                }
            }

            private void AjustarTamaño(int interval, Keys keys)
            {
                switch (keys)
                {
                    case Keys.Left:
                        Width = Width - interval;
                        break;
                    case Keys.Right:
                        Width = Width + interval;
                        break;
                    case Keys.Up:
                        Height = Height - interval;
                        break;
                    case Keys.Down:
                        Height = Height + interval;
                        break;
                }
            }

            private void CapturarPantalla()
            {
                this.Hide();
                fnc.CropArea(new Point(VisibleLeft, VisibleTop), new Size(VisibleWidth, VisibleHeight));
                this.Captura = fnc.Image;
            }

        #endregion

        #region Eventos

            /// <summary>
            ///     Evento OnResize
            /// </summary>
            /// <param name="e">EventArgs</param>
            protected override void OnResize(EventArgs e)
            {
                _mitadX = (VisibleWidth / 2) + BORDE_ANCHO_INVISIBLE;
                _mitadY = (VisibleHeight / 2) + BORDE_ANCHO_INVISIBLE;

                if (VisibleWidth <= 1)
                    VisibleWidth = 1;
                if (VisibleHeight <= 1)
                    VisibleHeight = 1;
                this.Refresh(); // Hack
            }

            /// <summary>
            ///     Evento OnMouseUp
            /// </summary>
            /// <param name="e">MouseEventArgs</param>
            protected override void OnMouseUp(MouseEventArgs e)
            {
                HandleMouseUp(e);
            }

            /// <summary>
            ///     Evento OnMouseDown
            /// </summary>
            /// <param name="e">MouseEventArgs</param>
            protected override void OnMouseDown(MouseEventArgs e)
            {
                HandleMouseDown(e);
            }

            /// <summary>
            ///     Evento OnMouseMove
            /// </summary>
            /// <param name="e">MouseEventArgs</param>
            protected override void OnMouseMove(MouseEventArgs e)
            {
                HandleMouseMove(e);
            }

            /// <summary>
            ///     Evento OnPaint
            /// </summary>
            /// <param name="e">PaintEventArgs</param>
            protected override void OnPaint(PaintEventArgs e)
            {
                PaintUI(e.Graphics, e);
            }

            /// <summary>
            ///     Evento OnKeyDown
            /// </summary>
            /// <param name="e">KeyEventArgs</param>
            protected override void OnKeyDown(KeyEventArgs e)
            {
                HandleKeyDown(e);
            }
            
            /// <summary>
            ///     Evento OnDoubleClick
            ///     <remarks>Dispara la captura de pantalla</remarks>
            /// </summary>
            /// <param name="e">EventArgs</param>
            protected override void OnDoubleClick(EventArgs e)
            {
                CapturarPantalla();
            }

            /// <summary>
            ///     Evento OnClosing
            /// </summary>
            /// <param name="e">CancelEventArgs</param>
            protected override void OnClosing(CancelEventArgs e)
            {
                base.OnClosing(e);
            }

            /// <summary>
            ///     Manejador de KeyDown
            ///     <remarks>Disparadores de funciones.</remarks>
            /// </summary>
            /// <param name="e">KeyEventArgs</param>
            private void HandleKeyDown(KeyEventArgs e)
            {

                int interval;
                if (e.Control)
                    interval = 10;
                else
                    interval = 1;

                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        CapturarPantalla();
                        this.Close();
                        e.Handled = true;
                        break;
                    case Keys.Escape:
                        this.Close();
                        e.Handled = true;
                        break;
                    // Más funciones se puden poner aquí
                }

                if (e.Alt)  // ¿Está precionada la tecla Alt junto con la tecla?
                    AjustarTamaño(interval, e.KeyCode);
                else
                    AjustarPosicion(interval, e.KeyCode);
            }
            
            /// <summary>
            ///     Manejador de MouseUp
            /// </summary>
            /// <param name="e">MouseEventArgs</param>
            private void HandleMouseUp(MouseEventArgs e)
            {
                _resizeRegion = ResizeRegion.None;
            }

            /// <summary>
            ///     Manejador de MouseDown
            /// </summary>
            /// <param name="e">MouseEventArgs</param>
            private void HandleMouseDown(MouseEventArgs e)
            {
                _offset = new Point(MousePosition.X - Location.X, MousePosition.Y - Location.Y);
                _mouseDownRect = ClientRectangle;
                _mouseDownPoint = MousePosition;

                if (IsInResizeArea())
                {
                    _resizeRegion = GetResizeRegion();
                    SetResizeCursor(_resizeRegion);
                }
            }

            /// <summary>
            ///     Manejador de MouseMove
            /// </summary>
            /// <param name="e">MouseEventArgs</param>
            private void HandleMouseMove(MouseEventArgs e)
            {
                if (_resizeRegion != ResizeRegion.None)
                {
                    HandleResize();
                }
                else
                {
                    if (e.Button == MouseButtons.Left)
                        Location = new Point(MousePosition.X - _offset.X, MousePosition.Y - _offset.Y);

                    if (IsInResizeArea() && e.Button != MouseButtons.Left)
                        SetResizeCursor(GetResizeRegion());
                    else if (_resizeRegion == ResizeRegion.None)
                        Cursor = Cursors.Default;
                }
            }

            /// <summary>
            ///     Pinta la interfase grafica como a mi me gusta :P
            /// </summary>
            /// <param name="graphics">Objeto Graphics del Formulario</param>
            /// <param name="e">Argumentos del evento Paint</param>
            private void PaintUI(Graphics graphics, PaintEventArgs e)
            {
                Point[] points = { _punto1, _punto2, _punto3, _punto4, _punto5, _punto6 };
                graphics.FillPolygon(this._indicadorBrush, points); // Dibujamos la pestañita.

                Rectangle frm = e.ClipRectangle;
                Rectangle area = VisibleClientRectangle;

                Region formRegion = new Region(frm);
                Region areaRegion = new Region(area);

                formRegion.Complement(areaRegion);

                graphics.FillRegion(this._areaBrush, areaRegion); // área principal
                formRegion.Dispose();
                areaRegion.Dispose();

                Pen linePen = new Pen(this._textoBrush);
                graphics.DrawRectangle(linePen, area);

                if (VisibleWidth > 30 & VisibleHeight > 30) // dibujamos la cruz del centro
                {
                    graphics.DrawLine(linePen, _mitadX, (_mitadY) + 10, _mitadX, (_mitadY) - 10);
                    graphics.DrawLine( linePen, (_mitadX) + 10, _mitadY, (_mitadX) - 10, _mitadY);
                }
                linePen.Dispose();

                // Ancho del area visible de recorte
                graphics.DrawString(VisibleWidth + " px", _guiaFont, _textoBrush, BORDE_ANCHO_INVISIBLE, BORDE_ANCHO_INVISIBLE - 15);

                // Altura del area visible de recorte
                graphics.RotateTransform(90);   // Rotación para que se vea cacheton
                graphics.DrawString(VisibleHeight + " px", _guiaFont, _textoBrush, BORDE_ANCHO_INVISIBLE, -BORDE_ANCHO_INVISIBLE);
            }

        #endregion
    }
}
