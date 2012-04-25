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
using System.Runtime.InteropServices;

namespace TwitShot.API
{

    /// <summary>
    ///     Efectos visuales de la aplicación usando el WinAPI32
    ///     <remarks>Está sección puede ser eliminadas en el futuro para usar algún Wrapper o biblioteca preconstruida.</remarks>
    /// </summary>

    public static class WindowsFX
    {
        #region WinAPI32
            
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);  /// <see cref="http://msdn.microsoft.com/en-us/library/ms679346(VS.85).aspx"/>

            [StructLayout(LayoutKind.Sequential)]
            private struct FLASHWINFO   
            {
                /// <summary>
                /// The size of the structure in bytes.
                /// </summary>
                public uint cbSize;
                /// <summary>
                /// A Handle to the Window to be Flashed. The window can be either opened or minimized.
                /// </summary>
                public IntPtr hwnd;
                /// <summary>
                /// The Flash Status.
                /// </summary>
                public uint dwFlags;
                /// <summary>
                /// The number of times to Flash the window.
                /// </summary>
                public uint uCount;
                /// <summary>
                /// The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate.
                /// </summary>
                public uint dwTimeout;
            }   /// <see cref="http://msdn.microsoft.com/en-us/library/ms679348(VS.85).aspx"/>


            /// <summary>
            /// Stop flashing. The system restores the window to its original stae.
            /// </summary>
            public const uint FLASHW_STOP = 0;

            /// <summary>
            /// Flash the window caption.
            /// </summary>
            public const uint FLASHW_CAPTION = 1;

            /// <summary>
            /// Flash the taskbar button.
            /// </summary>
            public const uint FLASHW_TRAY = 2;

            /// <summary>
            /// Flash both the window caption and taskbar button.
            /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
            /// </summary>
            public const uint FLASHW_ALL = 3;
            /// <summary>
            /// Flash continuously, until the FLASHW_STOP flag is set.
            /// </summary>
            public const uint FLASHW_TIMER = 4;
            /// <summary>
            /// Flash continuously until the window comes to the foreground.
            /// </summary>
            public const uint FLASHW_TIMERNOFG = 12;
        
        #endregion

        private const uint Win2K = 5;

        #region Métodos
        /// <summary>
        ///     Parpadea el formulario hasta que se dispare el evento OnFocus()
        /// </summary>
        /// <param name="form">Formulario Intermitente</param>
        /// <returns>bool Refresa true si la llamada a la función de user32.dll fue exitosa.</returns>
        public static bool Flash(System.Windows.Forms.Form form)
        {
            if (IsCompatible)   //  ¿Esta función es compatible con la versión de Windows?
            {
                FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_ALL | FLASHW_TIMERNOFG, uint.MaxValue, 0);
                return FlashWindowEx(ref fi);
            }
            return false;
        }

        /// <summary>
        ///     Rellenamos los datos de la estructura FLASHWINFO
        /// </summary>
        /// <param name="handle">A Handle to the Window to be Flashed. The window can be either opened or minimized.</param>
        /// <param name="flags">The FLASHW Flags</param>
        /// <param name="count">The number of times to Flash the window.</param>
        /// <param name="timeout">The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate.</param>
        /// <returns>FLASHWINFO</returns>
        private static FLASHWINFO Create_FLASHWINFO(IntPtr handle, uint flags, uint count, uint timeout)
        {
            FLASHWINFO fi = new FLASHWINFO();
            fi.cbSize = Convert.ToUInt32(Marshal.SizeOf(fi));
            fi.hwnd = handle;
            fi.dwFlags = flags;
            fi.uCount = count;
            fi.dwTimeout = timeout;
            return fi;
        }

        /// <summary>
        ///     Hace intermitente a un formulario.
        /// </summary>
        /// <param name="form">Formulario intermitente</param>
        /// <param name="count">Número de veces que va a parpadear el formulario.</param>
        /// <returns>bool Refresa true si la llamada a la función de user32.dll fue exitosa.</returns>
        public static bool Flash(System.Windows.Forms.Form form, uint count)
        {
            if (IsCompatible)
            {
                FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_ALL, count, 0);
                return FlashWindowEx(ref fi);
            }
            return false;
        }

        /// <summary>
        ///     Inicia la intermitencia del formulario.
        /// </summary>
        /// <param name="form">Formulario</param>
        /// <returns>bool Refresa true si la llamada a la función de user32.dll fue exitosa.</returns>
        public static bool Start(System.Windows.Forms.Form form)
        {
            if (IsCompatible)
            {
                FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_ALL, uint.MaxValue, 0);
                return FlashWindowEx(ref fi);
            }
            return false;
        }

        /// <summary>
        ///     Detiene la intermitencia del formulario.
        /// </summary>
        /// <param name="form">Formulario intermitente.</param>
        /// <returns>bool Refresa true si la llamada a la función de user32.dll fue exitosa.</returns>
        public static bool Stop(System.Windows.Forms.Form form)
        {
            if (IsCompatible)
            {
                FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_STOP, uint.MaxValue, 0);
                return FlashWindowEx(ref fi);
            }
            return false;
        }

        /// <summary>
        ///     Valor booleano que nos indica si la versión del sistema operativo es compatible con las llamadas a WinAPI32 (user32.dll)
        /// </summary>
        private static bool IsCompatible
        {
            get { return System.Environment.OSVersion.Version.Major >= Win2K; }
        } 
        #endregion
    }
}
