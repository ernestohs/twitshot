using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TwitShot.GUI.Paint.Tools
{
    class Frame : IDisposable, IPaintTool
    {
        #region .ctor
            /// <summary>
            ///     Constructor
            /// </summary>
            /// <param name="g">Graphics del Canvas donde se dibuja la elipse</param>
            public Frame(Graphics g)
            {
                this._Icon = null;//new Icon("NoImplementado.ico");
                this._Canvas = g;
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
            Canvas.DrawRectangle(this.Pen, MakeRectangle(StartPoint, EndPoint));
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

        #region Miembros de IDisposable

        public void Dispose()
        {
            // Limpieza a la casa
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
