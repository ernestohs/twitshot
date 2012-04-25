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
using System.Security;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace TwitShot.API
{
    class AppConfig
    {
        private const string RegisryProductName = "TwitShot";

        private static string ToInsecureString(SecureString Secreto)
        {
            string returnValue = string.Empty;
            IntPtr ptr = Marshal.SecureStringToBSTR(Secreto);
            try
            {
                returnValue = Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

        public static bool SavedCredentials {
            get {
                RegistryKey baseRegistryKey = Registry.CurrentUser;
                string subKey = "SOFTWARE\\" + RegisryProductName + "\\DWP";
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);

                if (sk1 == null)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        byte[] datos = (byte[])sk1.GetValue("SSP");
                        return (datos.Length != 0 && datos != null) ? true : false;
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        Console.WriteLine("--AppConfig.SavedCredentials::get--");
                        Console.WriteLine("Error: " + e.Message);
#endif
                        return false;
                    }
                }
            }

            set {
                if (value == false)
                { 
                    // Limpiar el contenido
                    Username = "";
                    Password = new SecureString();
                }
            } 
        }

        public static string Username {
            get {
                RegistryKey baseRegistryKey = Registry.CurrentUser;
                string subKey = "SOFTWARE\\" + RegisryProductName + "\\DWP";
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                if (sk1 == null)
                {
                    return null;
                }
                else
                {
                    try
                    {
                        return (string)sk1.GetValue("RSU");
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        Console.WriteLine("--AppConfig.Username::set--");
                        Console.WriteLine("Error" + e.Message);
#endif
                        return null;
                    }
                }   
            }

            set {
                try
                {
                    RegistryKey rk = Registry.CurrentUser;
                    string subKey = "SOFTWARE\\" + RegisryProductName + "\\DWP";
                    RegistryKey sk1 = rk.CreateSubKey(subKey);
                    sk1.SetValue("RSU", value);
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.WriteLine("--AppConfig.Username::set--");
                    Console.WriteLine("Error" + e.Message); 
#endif
                }
            }
        }

        public static SecureString Password {
            get {
                RegistryKey baseRegistryKey = Registry.CurrentUser;
                string subKey = "SOFTWARE\\" + RegisryProductName + "\\DWP";
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                if (sk1 == null)
                {
                    return null;
                }
                else
                {
                    try
                    {
                        System.Security.SecureString pass = new System.Security.SecureString();
                        byte[] code = (byte[])sk1.GetValue("SSP");
                        foreach (byte c in code)
                        {
                            pass.AppendChar((char)c); // Algún algoritmo de encripcion es necesario para decodificar
                        }
                        return pass;
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        Console.WriteLine("--AppConfig.Password::get--");
                        Console.WriteLine("Error: "+e.Message);
#endif
                        return null;
                    }
                }
            }

            set {
                try
                {
                    RegistryKey rk = Registry.CurrentUser;
                    string subKey = "SOFTWARE\\" + RegisryProductName + "\\DWP";
                    RegistryKey sk1 = rk.CreateSubKey(subKey);
                    sk1.SetValue("SSP", (new System.Text.ASCIIEncoding()).GetBytes(ToInsecureString(value)), RegistryValueKind.Binary); // Algoritmo para encriptar
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.WriteLine("--AppConfig.Password::set--");
                    Console.WriteLine("Error:"+e.Message);
#endif
                }
            }
        }

    }
}
