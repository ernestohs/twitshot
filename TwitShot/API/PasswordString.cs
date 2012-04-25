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
using System.Security;
using System.Runtime.InteropServices;

namespace TwitShot.API
{
    /// <summary>
    ///     Esta clase protege la contraseña y permite realizar operaciones necesarias con la misma.
    /// </summary>
    public sealed class StringPassword
    {
        #region Propiedades
            /// <summary>
            ///     Contraseña.
            /// </summary>
            private SecureString Secreto; 
        #endregion

        #region .ctor
        /// <summary>
        ///     Constructor de clase
        /// </summary>
        /// <param name="password">Constaseña en formato SecureString</param>
        public StringPassword(SecureString password)
        {
            Secreto = password;
        } 
        #endregion

        #region Métodos
        /// <summary>
        ///     "Convierte" el tipo TwitShot.API.StringPassword a System.Security.SecureString
        /// </summary>
        /// <returns>Contraseña en formato SecureString</returns>
        public SecureString ToSecureString()
        {
            return Secreto;
        }

        /// <summary>
        ///     Convierte la contraseña almacenada en una cadena común.
        /// </summary>
        /// <returns>Contraseña desnuda.</returns>
        public string ToInsecureString()
        {
            string value;
            IntPtr ptr = Marshal.SecureStringToBSTR(Secreto);   //Hack
            try
            {
                value = Marshal.PtrToStringBSTR(ptr);     //Hack
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
            return value;
        } 
        #endregion
    }
}
