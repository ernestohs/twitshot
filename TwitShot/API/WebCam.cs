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
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;


namespace TwitShot.API
{
    class WebCam : IDisposable
    {

        #region WinAPI32
        private const short WM_CAP = 0x400;
        private const int WM_CAP_DRIVER_CONNECT = 0x40a;
        private const int WM_CAP_DRIVER_DISCONNECT = 0x40b;
        private const int WM_CAP_EDIT_COPY = 0x41e;
        private const int WM_CAP_SET_PREVIEW = 0x432;
        private const int WM_CAP_SET_OVERLAY = 0x433;
        private const int WM_CAP_SET_PREVIEWRATE = 0x434;
        private const int WM_CAP_SET_SCALE = 0x435;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const short SWP_NOMOVE = 0x2;
        private short SWP_NOZORDER = 0x4;
        private short HWND_BOTTOM = 1;

        [DllImport("avicap32.dll")]
        protected static extern bool capGetDriverDescriptionA(short wDriverIndex, [MarshalAs(UnmanagedType.VBByRefStr)]ref String lpszName, int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);

        [DllImport("avicap32.dll")]
        protected static extern int capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);

        [DllImport("user32")]
        protected static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("user32", EntryPoint = "SendMessageA")]
        protected static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);

        [DllImport("user32")]
        protected static extern bool DestroyWindow(int hwnd);

        #endregion

        #region .ctor
        public WebCam()
        {
            string Name = String.Empty.PadRight(100);
            string Version = String.Empty.PadRight(100);
            char[] clean = new char[] { '\0' };
            for (short index = 0; capGetDriverDescriptionA(index, ref Name, 100, ref Version, 100); index++)
            {
                CaptureDevices.Add(Name.Trim(clean));
            }
        } 
        #endregion

        #region Propiedades
        public int DeviceID = 0;
        public int hHwnd = 0;
        public ArrayList CaptureDevices = new ArrayList();
        public PictureBox Container { get; set; }
        public Image Image
        {
            get
            {
                IDataObject data;
                Image oImage;
                SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);
                data = Clipboard.GetDataObject();
                if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
                {
                    oImage = (Image)data.GetData(typeof(System.Drawing.Bitmap));
                    return oImage;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        ///     Inicia el flujo de video en el PictureBox contenedor
        /// </summary>
        public void Start()
        {
            string DeviceIndex = Convert.ToString(DeviceID);
            IntPtr oHandle = Container.Handle;
            if (oHandle == null)
            {
                throw new Exception("No se asigno un contenedor PictureBox a esta instancia");
            }

            //hHwnd = capCreateCaptureWindowA(ref DeviceIndex, WS_VISIBLE | WS_CHILD, 0, 0, 640, 480, oHandle.ToInt32(), 0);
            hHwnd = capCreateCaptureWindowA(ref DeviceIndex, WS_VISIBLE | WS_CHILD, 0, 0, Container.Width, Container.Height, oHandle.ToInt32(), 0);
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, DeviceID, 0) != 0)
            {
                SendMessage(hHwnd, WM_CAP_SET_SCALE, -1, 0);
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0);
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, -1, 0);
                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, Container.Height, Container.Width, SWP_NOMOVE | SWP_NOZORDER);
            }
            else
            {
                DestroyWindow(hHwnd);
            }
        }
        /// <summary>
        ///     Detiene el flujo de video en el PictureBox contenedor
        /// </summary>
        public void Stop()
        {
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, DeviceID, 0);
            DestroyWindow(hHwnd);
        }       
        #endregion

        #region Need Refactoring & ReEngenering
        public void SaveImage()
        {
            IDataObject data;
            Image oImage;
            SaveFileDialog sfdImage = new SaveFileDialog();
            sfdImage.Filter = "(*.bmp)|*.bmp";
            SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);
            data = Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
            {
                oImage = (Image)data.GetData(typeof(System.Drawing.Bitmap));
                Container.Image = oImage;
                Stop();
                if (sfdImage.ShowDialog() == DialogResult.OK)
                {
                    oImage.Save(sfdImage.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                }
            }
        } 
        #endregion

        #region Miembros de IDisposable

        public void Dispose()
        {
            Stop();
        }

        #endregion
    }
}
