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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TwitShot.API;
using TwitShot.GUI.Paint.Tools;

namespace TwitShot.GUI.Paint
{
    public partial class frmImgEditor : Form
    {
        #region Propiedades Privadas
        private TwitShot.API.TwitPic TP;
        private Graphics g;
        private Color color;
        private App fn = new App(); 
        #endregion

        #region Colores
        Color[] colores = new Color[]{
                        Color.AliceBlue,
			            Color.AntiqueWhite,
			            Color.Aqua,
			            Color.Aquamarine,
			            Color.Azure,
			            Color.Beige,
			            Color.Bisque,
			            Color.Black,
			            Color.BlanchedAlmond,
			            Color.Blue,
			            Color.BlueViolet,
			            Color.Brown,
			            Color.BurlyWood,
			            Color.CadetBlue,
			            Color.Chartreuse,
			            Color.Chocolate,
			            Color.Coral,
			            Color.Cornsilk,
			            Color.Crimson,
			            Color.Cyan,
			            Color.DarkBlue,
			            Color.DarkCyan,
			            Color.DarkGoldenrod,
			            Color.DarkGray,
			            Color.DarkGreen,
			            Color.DarkKhaki,
			            Color.DarkMagenta,
			            Color.DarkOliveGreen,
			            Color.DarkOrange,
			            Color.DarkOrchid,
			            Color.DarkRed,
			            Color.DarkSalmon,
			            Color.DarkSeaGreen,
			            Color.DarkSlateBlue,
			            Color.DarkSlateGray,
			            Color.DarkTurquoise,
			            Color.DarkViolet,
			            Color.DeepPink,
			            Color.DeepSkyBlue,
			            Color.DimGray,
			            Color.DodgerBlue,
			            Color.Firebrick,
			            Color.FloralWhite,
			            Color.ForestGreen,
			            Color.Fuchsia,
			            Color.Gainsboro,
			            Color.GhostWhite,
			            Color.Gold,
			            Color.Goldenrod,
			            Color.Gray,
			            Color.Green,
			            Color.GreenYellow,
			            Color.Honeydew,
			            Color.HotPink,
			            Color.IndianRed,
			            Color.Indigo,
			            Color.Ivory,
			            Color.Khaki,
			            Color.Lavender,
			            Color.LavenderBlush,
			            Color.LawnGreen,
			            Color.LemonChiffon,
			            Color.LightBlue,
			            Color.LightCoral,
			            Color.LightCyan,
			            Color.LightGoldenrodYellow,
			            Color.LightGray,
			            Color.LightGreen,
			            Color.LightPink,
			            Color.LightSalmon,
			            Color.LightSeaGreen,
			            Color.LightSkyBlue,
			            Color.LightSlateGray,
			            Color.LightSteelBlue,
			            Color.LightYellow,
			            Color.Lime,
			            Color.LimeGreen,
			            Color.Linen,
			            Color.Magenta,
			            Color.Maroon,
			            Color.MediumAquamarine,
			            Color.MediumBlue,
			            Color.MediumOrchid,
			            Color.MediumPurple,
			            Color.MediumSeaGreen,
			            Color.MediumSlateBlue,
			            Color.MediumSpringGreen,
			            Color.MediumTurquoise,
			            Color.MediumVioletRed,
			            Color.MidnightBlue,
			            Color.MintCream,
			            Color.MistyRose,
			            Color.Moccasin,
			            Color.NavajoWhite,
			            Color.Navy,
			            Color.OldLace,
			            Color.Olive,
			            Color.OliveDrab,
			            Color.Orange,
			            Color.OrangeRed,
			            Color.Orchid,
			            Color.PaleGoldenrod,
			            Color.PaleGreen,
			            Color.PaleTurquoise,
			            Color.PaleVioletRed,
			            Color.PapayaWhip,
			            Color.PeachPuff,
			            Color.Peru,
			            Color.Pink,
			            Color.Plum,
			            Color.PowderBlue,
			            Color.Purple,
			            Color.Red,
			            Color.RosyBrown,
			            Color.RoyalBlue,
			            Color.SaddleBrown,
			            Color.Salmon,
			            Color.SandyBrown,
			            Color.SeaGreen,
			            Color.SeaShell,
			            Color.Sienna,
			            Color.Silver,
			            Color.SkyBlue,
			            Color.SlateBlue,
			            Color.SlateGray,
			            Color.Snow,
			            Color.SpringGreen,
			            Color.SteelBlue,
			            Color.Tan,
			            Color.Teal,
			            Color.Thistle,
			            Color.Tomato,
			            Color.Transparent,
			            Color.Turquoise,
			            Color.Violet,
			            Color.Wheat,
			            Color.White,
			            Color.WhiteSmoke,
			            Color.Yellow,
			            Color.YellowGreen
			            };
        #endregion

        #region Banda Elastica
        private Point ptOriginal = new Point();
        private Point ptLast = new Point();
        private bool isDragging = false; 
        #endregion

        public frmImgEditor(TwitShot.API.TwitPic tp, Image img)
        {
            InitializeComponent();
            this.pbCanvas.Image = img;
            InitializePaintValues(tp);
        }

        public frmImgEditor(TwitShot.API.TwitPic tp)
        {
            InitializeComponent();  
            fn.ScreenShot();
            this.pbCanvas.Image = fn.Image;
            InitializePaintValues(tp);
        }

        private void InitializePaintValues(TwitShot.API.TwitPic tp)
        {
            this.TP = tp;
            tssSize.Text = String.Format("{0}x{1}", this.pbCanvas.Image.Width, this.pbCanvas.Image.Height);
            color = Color.Black;
            g = this.CreateGraphics();
            foreach (Color col in colores)
            {
                tsColores.Items.Add(col.ToString(), GenImgColor(col), new System.EventHandler(Color_MouseClick));
            }
        }

        private Image GenImgColor(Color c)
        {
            Bitmap bmp = new Bitmap(2, 2);
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    bmp.SetPixel(x, y, c);
                }
            }
            return (Image)bmp;
        }

        private void Color_MouseClick(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)((ToolStripButton)sender).Image;
            color = bmp.GetPixel(0, 0);
        }

        private void frmImgEditor_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] filepath = (string[])e.Data.GetData(DataFormats.FileDrop);
                    Image image = Image.FromFile(filepath[0]);
                    this.pbCanvas.Image = image;
                }
                else if (e.Data.GetDataPresent(DataFormats.Text))
                {
                    try
                    {
                        string texto = (string)e.Data.GetData(DataFormats.Text);
                        if (texto.IndexOf("://") != -1)
                        {
                            this.pbCanvas.Image = TP.GetImageFromUrl(texto);
                        }
                        else if (texto.IndexOf(":\\") != -1)
                        {
                            this.pbCanvas.Image = Image.FromFile(texto);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void frmImgEditor_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) ||
                    e.Data.GetDataPresent(DataFormats.Text))
            {
                if ((e.AllowedEffect & DragDropEffects.Move) != 0)
                    e.Effect = DragDropEffects.Move;
            }
        }

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                #region Banda Elastica
                isDragging = true;
                ptOriginal.X = e.X; ptOriginal.Y = e.Y;
                ptLast.X = -1; ptLast.Y = -1; 
                #endregion
            }
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            #region Banda Elastica
            Point ptCurrent = new Point(e.X, e.Y);
            if (isDragging)
            {
                if (ptLast.X != -1)
                {
                    DrawReversibleRectangle(ptOriginal, ptLast);
                }
                ptLast = ptCurrent;
                DrawReversibleRectangle(ptOriginal, ptCurrent);
            } 
            #endregion
            tssCords.Text = String.Format("X:{0}, Y:{1}", e.X, e.Y);
        }

        private void pbCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            // Nada de Nada
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            #region Banda Elastica
            isDragging = false;
            if (ptLast.X != -1)
            {
                Point ptCurrent = new Point(e.X, e.Y);
                DrawReversibleRectangle(ptOriginal, ptLast);
            }
            MetodoDibujo();
            ptLast.X = -1; ptLast.Y = -1;
            ptOriginal.X = -1; ptOriginal.Y = -1; 
            #endregion
            // Dibujar 
        }

        private void MetodoDibujo()
        {
            Frame eli = new Frame(pbCanvas.CreateGraphics());
            eli.Pen = new Pen(color, 1);
            eli.StartPoint = ptOriginal;
            eli.EndPoint = ptLast;
            eli.Draw();
        }

        private void DrawReversibleRectangle(Point StartPoint, Point EndPoint)
        {
            Rectangle Rect = new Rectangle();
            StartPoint = pbCanvas.PointToScreen(StartPoint); EndPoint = pbCanvas.PointToScreen(EndPoint);
            if (StartPoint.X < EndPoint.X)
            {
                Rect.X = StartPoint.X;
                Rect.Width = EndPoint.X - StartPoint.X;
            }
            else
            {
                Rect.X = EndPoint.X;
                Rect.Width = StartPoint.X - EndPoint.X;
            }
            if (StartPoint.Y < EndPoint.Y)
            {
                Rect.Y = StartPoint.Y;
                Rect.Height = EndPoint.Y - StartPoint.Y;
            }
            else
            {
                Rect.Y = EndPoint.Y;
                Rect.Height = StartPoint.Y - EndPoint.Y;
            }
            ControlPaint.DrawReversibleFrame(Rect, Color.Red, FrameStyle.Dashed);
        }

    }
}
