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

#if DEBUG
using System.Diagnostics; 
#endif

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace TwitShot.API
{
    /// <summary>
    /// Implementación del API de Twitpic <see cref="http://twitpic.com/api.do"/>
    /// 
    /// El código fue escrito implementando las siguiente guia la cual aparece en http://twitpic.com/api.do
    /// 
    /// ===================================================================================================
    /// 
    /// API DOCUMENTATION
    /// 
    /// METHOD: http://twitpic.com/api/uploadAndPost
    /// 
    /// Use this method to upload an image to TwitPic and to send it as a status update to Twitter.
    /// 
    /// Fields to post in
    /// (post data should be formatted as multipart/form-data):
    /// - media (required) - Binary image data
    /// - username (required) - Twitter username
    /// - password (required) - Twitter password
    /// - message (optional) - Message to post to twitter. The URL of the image is automatically added.
    /// 
    /// Sample response:
    /// <![CDATA[
    /// <?xml version="1.0" encoding="UTF-8"?>
    /// <rsp status="ok">
    /// <statusid>1111</statusid>
    /// <userid>11111</userid>
    ///  <mediaid>abc123</mediaid>
    ///  <mediaurl>http://twitpic.com/abc123</mediaurl>
    /// </rsp>
    /// ]]>
    /// METHOD: http://twitpic.com/api/upload
    ///
    /// Use this method if you only want to upload a photo to TwitPic.
    ///
    /// Fields to post in:
    /// (post data should be formatted as multipart/form-data)
    /// - media (required) - Binary image data
    /// - username (required) - Twitter username
    /// - password (required) - Twitter password
    ///
    /// Sample response:
    /// <![CDATA[
    /// <?xml version="1.0" encoding="UTF-8"?>
    /// <rsp stat="ok">
    ///  <mediaid>abc123</mediaid>
    ///  <mediaurl>http://twitpic.com/abc123</mediaurl>
    /// </rsp>
    /// ]]>
    ///
    /// Sample error response:
    /// <![CDATA[
    /// <?xml version="1.0" encoding="UTF-8"?>
    /// <rsp stat="fail">
    ///     <err code="1001" msg="Invalid twitter username or password" />
    /// </rsp>
    /// ]]>
    ///
    /// Error codes and their descriptions:
    /// 1001 - Invalid twitter username or passwor
    /// 1002 - Image not found
    /// 1003 - Invalid image type
    /// 1004 - Image larger than 4MB	
    ///
    ///
    /// Image thumbnails:
    /// Use the following URL structure to add Twitpic thumbnails into your app:
    ///
    /// http://twitpic.com/show/[size]/[image-id]
    ///
    /// Size: mini or thumb
    /// Image ID: this is the ID of the Twitpic photo
    ///
    /// Example: http://twitpic.com/show/thumb/1e10q
    ///
    /// *note: Twitpic Community Guidelines specify that if you use a Twitpic thumbnail in your app, the photo must link back to its original photo page or a link to the original photo page must be provided somewhere within context to the thumbnail
    ///
    /// **note: this URL may forward to an amazon s3 URL
    ///
    ///
    /// How do I get "from [my_application]" appended to updates sent from my API application?
    /// Please email noah@twitpic.com with the name, URL, and description of your application. Your application must have a web site to which we can link in order to this up. Please allow up to 48 hours for the link to your application to go live, as this is a manual process contingent on TwitPic's deployment schedule.
    /// 
    /// ===================================================================================================
    /// 
    /// </summary>

    public class TwitPic
    {
        #region Constantes  del API
        /// <summary>
        ///     URL de la dirección de solo subida, esto no genera twitt.
        /// </summary>
        private string UPLOAD_ONLY_URL = Properties.Settings.Default.TwitPicUploadOnly;
        /// <summary>
        ///     URL de la dirección de subida y twitt.
        /// </summary>
        private string UPLOAD_POST_URL = Properties.Settings.Default.TwitPicUploadPost;
        /// <summary>
        ///     URL de validación de usuario.
        ///     <remarks>
        ///         Esto no viene especificado en el API de twitpic, sin embargo es necesario para poder validar el usuario
        ///         y las peticiones enviadas a twitpic sean correctas. <see cref="http://apiwiki.twitter.com/Twitter-REST-API-Method:-account%C2%A0verify_credentials?SearchFor=verify+credentials&sp=1"/>
        ///     </remarks>
        /// </summary>
        private string TWITTER_VERIFY = Properties.Settings.Default.TwitterAPI_VerifyCredentials;
        /// <summary>
        ///     Valor del content-type de la petición de envio de petición.
        /// </summary>
        private const string CONTENT_TYPE = "application/x-www-form-urlencoded";
        /// <summary>
        ///     Tamaño del buffer de datos
        /// </summary>
        private const int BUFFER_SIZE = 8192;

        /// <summary>
        ///     Errores que pueden presentarse
        /// </summary>
        public enum Errors
        {
            /******No Especificado en el API******/
            NoResult = -1,  /// No hay resultado o respuesta
            NoComunication = -2,    /// No hay comunicación
            NoError = 1000, /// No hay error, todo salio bien. Nota: Este valor se deduce de la secuencia especificada en el API
            /*************************************/
            InvalidUsernameOrPassword = 1001,   /// El Nombre de usuario o contraseña no es correcto
            ImageNotFound = 1002,   /// No se encontro la imagen
            InvalidImageType = 1003,    /// Tipo de imagen no valido
            ImageLargerThanLimit = 1004 /// La imagen es más grande que el limite
        }

        /// <summary>
        ///     Tamaños del Thumb
        /// </summary>
        public enum ThumbSize { mini, thumb }

        #endregion

        #region Propiedades
        private Errors e;
        /// <summary>
        ///     Propiedad error
        /// </summary>
        public Errors Error
        {
            get
            {
                return e;
            }
            set
            {
                switch (value)
                {
                    case Errors.NoResult:
                        this.Message = "No hay resultado";
                        break;
                    case Errors.NoComunication:
                        this.Message = "No hay comunicacion";
                        break;
                    case Errors.NoError:
                        this.Message = "No hay Error";
                        break;
                    case Errors.InvalidUsernameOrPassword:
                        this.Message = "Contraseña y/o usuario incorrecto";
                        break;
                    case Errors.ImageNotFound:
                        this.Message = "No se encontro la imagen";
                        break;
                    case Errors.InvalidImageType:
                        this.Message = "Tipo de Imagen no valido";
                        break;
                    case Errors.ImageLargerThanLimit:
                        this.Message = "Imagen mayor al limite";
                        break;
                    default:
                        this.Message = "WTF!!!";
#if DEBUG
                        Console.WriteLine("--TwitPic.Error::set--");
                        Console.WriteLine("Error anormal de API: value = " + value);
#endif
                        break;
                }
                e = value;
            }
        }
        /// <summary>
        ///     Nombre del usuario
        /// </summary>
        public string Username;
        /// <summary>
        ///     Contraseña
        /// </summary>
        public StringPassword Password;
        /// <summary>
        ///     Nos dice si el usuario está registrado
        /// </summary>
        private bool Registrado;
        /// <summary>
        /// 
        /// </summary>
        private string ICT;
        /// <summary>
        ///     Link a la última imagen cargada a twitpic
        /// </summary>
        public Uri MediaUrl;
        /// <summary>
        ///     Identificador de la última imagen cargada a twitpic
        /// </summary>
        public string MediaId;
        /// <summary>
        ///     Mensaje de twitter.com
        /// </summary>
        public string Message;
        /// <summary>
        ///     Tipo del contenido de la imagen.
        /// </summary>
        public string ImageContentType { get { return ICT; } }

        #endregion

        #region .ctor

        /// <summary>
        ///     Constructor de clase.
        /// </summary>
        public TwitPic()
        {
            this.Registrado = false;
            ICT = "image/png";
            this.Error = Errors.NoError;
        }

        /// <summary>
        ///     Constructor de clase pide usuario y contraseña.
        /// </summary>
        /// <param name="usr">Nombre del usuario.</param>
        /// <param name="psw">Contraseña del usuario.</param>
        public TwitPic(string usr, SecureString psw)
        {
            this.Username = usr;
            this.Password = new StringPassword(psw);
            this.Registrado = VerifyCredentials() ? true : false;
            ICT = "image/png";
            this.Error = Errors.NoError;
        }

        #endregion

        #region Métodos publicos de la aplicación

        #region public bool UploadPhoto(+3 Sobrecargas)

        /// <summary>
        ///     Sube una imagen a twitpic.
        /// </summary>
        /// <param name="img">Objeto System.Drawing.Image de la imagen que vamos a subir</param>
        /// <param name="msg">Contenido del twitt, el URL de twitpic dos espacios y un guion son agregados.</param>
        /// <returns>bool Regresa true si la imagen fue subida con éxito.</returns>
        public bool UploadPhoto(Image img, string msg)
        {
            bool Resultado = false;
            string Archivo = GenFileName();

            string boundary = Guid.NewGuid().ToString();
            string Peticion = String.IsNullOrEmpty(msg) ? UPLOAD_ONLY_URL : UPLOAD_POST_URL;
            var wr = (HttpWebRequest)WebRequest.Create(Peticion);
            string encoding = "iso-8859-1"; // NOTA: se usa esta codificación para que la imagen sea aceptada, pero en realidad al enviarlo sale en UTF-8

            wr.PreAuthenticate = true;
            wr.AllowWriteStreamBuffering = true;
            wr.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);  // Especificado en el API: post data should be formatted as multipart/form-data
            wr.Method = "POST";
            wr.Timeout = 50000; //FIXME: Este Numerito mágico no me gusta Matarilelireron

            StringBuilder Cabecera = BuildHeader(img, msg, Archivo, boundary, encoding);


            byte[] bytes = ContentBytesBuilder(msg, boundary, encoding, Cabecera);

            wr.ContentLength = bytes.Length;
            try
            {
                ///NOTE: ¿Abusando de los using?
                using (Stream rs = wr.GetRequestStream())
                {
                    rs.Write(bytes, 0, bytes.Length);

                    using (var response = (HttpWebResponse)wr.GetResponse())
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            string result = reader.ReadToEnd();

                            XDocument doc = XDocument.Parse(result);
                            XElement rsp = doc.Element("rsp");
                            string status = rsp.Attribute(XName.Get("status")) != null ? rsp.Attribute(XName.Get("status")).Value : rsp.Attribute(XName.Get("stat")).Value;
                            if (status.ToUpperInvariant().Equals("OK"))
                            {
                                MediaUrl = new Uri(rsp.Element("mediaurl").Value);
                                MediaId = rsp.Element("mediaid").Value;
                                Error = Errors.NoError;
                                Resultado = true;
                            }
                            else
                            {
                                XElement err = rsp.Element("err");
                                Error = (Errors)Convert.ToInt32(err.Attribute(XName.Get("code")).Value);
                                Resultado = false;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine("Error: " + ex.Message);
#endif
                Error = Errors.NoComunication;
                Resultado = false;
            }

            return Resultado;
        }

        /// <summary>
        ///     Ensambla el contenido del mensaje.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="boundary"></param>
        /// <param name="encoding"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        private static byte[] ContentBytesBuilder(string message, string boundary, string encoding, StringBuilder header)
        {
            string pie = string.Format("--{0}--", boundary);
            var contenido = new StringBuilder();

            if (!String.IsNullOrEmpty(message))
            {
                contenido.AppendLine(string.Format("--{0}", boundary));
                contenido.AppendLine(String.Format("Content-Disposition: form-data; name=\"{0}\"; Content-Type: text/plain; charset=utf-8", "message"));
                contenido.AppendLine();
                contenido.AppendLine(message);
            }

            contenido.AppendLine(pie);

            byte[] bytes = AssemblyContent(encoding, header, contenido);
            return bytes;
        }

        /// <summary>
        ///     Ensambla el contenido, primero pone la cabecera la cual esta formateada en iso-8859-1 y después el
        ///     mensaje en UTF-8, el resultado es una matriz de bytes.
        /// </summary>
        /// <param name="encoding">Codificación de la cabecera (Normalmente iso-8859-1)</param>
        /// <param name="header">Cabecera de la petición</param>
        /// <param name="content">Contenido del mensaje</param>
        /// <returns>Petición completa en formato de Matriz de bytes</returns>
        private static byte[] AssemblyContent(string encoding, StringBuilder header, StringBuilder content)
        {
            byte[] bytesHeader = Encoding.GetEncoding(encoding).GetBytes(header.ToString());
            byte[] bytesMessage = Encoding.GetEncoding("UTF-8").GetBytes(content.ToString());
            var merge = new byte[bytesHeader.Length + bytesMessage.Length];
            Buffer.BlockCopy(bytesHeader, 0, merge, 0, bytesHeader.Length);
            Buffer.BlockCopy(bytesMessage, 0, merge, bytesHeader.Length, bytesMessage.Length);
            return merge;
        }

        /// <summary>
        ///     Crea el encabezado de la petición Web para enviar la imagen a Twitpic
        /// </summary>
        /// <param name="img">Imagen a enviar</param>
        /// <param name="msg"></param>
        /// <param name="Archivo"></param>
        /// <param name="boundary"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private StringBuilder BuildHeader(Image img, string msg, string Archivo, string boundary, string img_encoding)
        {
            string cabecera = string.Format("--{0}", boundary);

            var contenido = new StringBuilder();
            contenido.AppendLine(cabecera);

            string tipoContenido = ICT;
            string encabezadoArchivo = String.Format("Content-Disposition: file; name=\"{0}\"; filename=\"{1}\"", "media", Archivo);
            string datosArchivo = Encoding.GetEncoding(img_encoding).GetString(this.ToBytes(img));

            contenido.AppendLine(encabezadoArchivo);
            contenido.AppendLine(String.Format("Content-Type: {0}", tipoContenido));
            contenido.AppendLine();
            contenido.AppendLine(datosArchivo);

            contenido.AppendLine(cabecera);
            contenido.AppendLine(String.Format("Content-Disposition: form-data; name=\"{0}\"", "username"));
            contenido.AppendLine();
            contenido.AppendLine(Username);

            contenido.AppendLine(cabecera);
            contenido.AppendLine(String.Format("Content-Disposition: form-data; name=\"{0}\"", "password"));
            contenido.AppendLine();
            contenido.AppendLine(Password.ToInsecureString());

            return contenido;
        }

        /// <summary>
        ///     Sube una imagen a twitpic.
        /// </summary>
        /// <param name="filename">Nombre y ruta del archivo que se va a enviar a twitpic</param>
        /// <param name="msg">Contenido del twitt, el URL de twitpic dos espacios y un guion son agregados.</param>
        /// <returns>bool Regresa true si la imagen fue subida con éxito.</returns>
        public bool UploadPhoto(string filename, string msg)
        {
            ICT = GetImageContentType(filename);
            return UploadPhoto(Image.FromFile(filename), msg);
        }

        /// <summary>
        ///     Sube una imagen a twitpic.
        /// </summary>
        /// <param name="url">URL de la imagen</param>
        /// <param name="msg">Contenido del twitt, el URL de twitpic dos espacios y un guion son agregados.</param>
        /// <returns>bool Regresa true si la imagen fue subida con éxito.</returns>
        public bool UploadPhoto(Uri url, string msg)
        {
            return UploadPhoto(this.GetImageFromUrl(url.ToString()), msg);
        }

        #endregion

        /// <summary>
        ///     Verifica las credenciales del usuario.
        ///     <remarks>Almacena la respuesta de Twitter.com en la propiedad del objeto Mensaje</remarks>
        ///     <remarks>Esto fue implementado del API de Twitter y no desde el API de twitpic</remarks>
        /// </summary>
        /// <returns>bool Regresa true si el usuario y contraseña son validos</returns>
        public bool VerifyCredentials()
        {
            try
            {
                String nombreUsuario = "";
                var sb = new StringBuilder();
                var buffer = new byte[BUFFER_SIZE];
                string usuario = Convert.ToBase64String(Encoding.UTF8.GetBytes(Username + ":" + Password.ToInsecureString()));
                var wr = (HttpWebRequest)WebRequest.Create(TWITTER_VERIFY);
                wr.Method = "GET";
                wr.Headers.Add("Authorization", "Basic " + usuario);
                wr.ContentType = CONTENT_TYPE;
                var response = (HttpWebResponse)wr.GetResponse();
                Stream resStream = response.GetResponseStream();
                int bytesLeidos;
                do
                {
                    bytesLeidos = resStream.Read(buffer, 0, buffer.Length);
                    if (bytesLeidos != 0)
                    {
                        string tmp = Encoding.ASCII.GetString(buffer, 0, bytesLeidos);
                        sb.Append(tmp);
                    }
                } while (bytesLeidos > 0);
                var doc = new XmlDocument();
                doc.LoadXml(sb.ToString());
                XmlNodeList xnl = doc.SelectNodes("/user/name");
                foreach (XmlNode node in xnl)
                {
                    nombreUsuario = node.InnerText;
                }
                Message = "Bienvenido " + nombreUsuario;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unauthorized"))
                {
                    Message = "Nombre de usuario y/o contraseña incorrecta";
                }
                else if (ex.Message.Contains("Service Unavailable"))
                {
                    Message = "Servicio no disponible, intente después";
                }
                else
                {
                    Message = ex.Message;
                }

                return false;
            }

            return true;
        }

        #region public Image GetThumbnail(+2 Sobrecargas)
        /// <summary>
        ///     Obhtiene un Thumbnail de la última imagen cargada.
        /// </summary>
        /// <returns>Objeto System.Drawing.Image de la última imagen cargada.</returns>
        public Image GetThumbnail()
        {
            return this.GetThumbnail(ThumbSize.thumb);
        }

        /// <summary>
        ///     Obhtiene un Thumbnail de la última imagen cargada.
        /// </summary>
        /// <param name="ts">ThumbnailSize</param>
        /// <returns>Objeto System.Drawing.Image de la última imagen cargada.</returns>
        public Image GetThumbnail(ThumbSize ts)
        {
            return this.GetImageFromUrl(String.Format("http://twitpic.com/show/{0}/{1}", (ts == ThumbSize.thumb) ? "thumb" : "mini", this.MediaId));
        } 
        #endregion

        /// <summary>
        ///     Borra una imagen de nuestra lista
        /// </summary>
        /// <param name="MediaUrl">Url de la imagen en twitpic.com</param>
        /// <returns>Regresa true si fue posible borrar la imagen</returns>
        public bool EraseUploatedPicture(string MediaId)
        {
            /*
             <form method="post" action="http://twitpic.com/del.do">
             <input type="hidden" name="id" value="px2p6">
             </form>
             */
            var cookieJar = new CookieContainer();
            var rnd = new Random();
            var request = (HttpWebRequest)WebRequest.Create("http://twitpic.com/del.do");
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = String.Format("id={0}&x={1}&y={2}", MediaId, rnd.Next(14), rnd.Next(14));
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.PreAuthenticate = true;
            request.AllowWriteStreamBuffering = true;
            request.Timeout = 50000;
            request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/x-shockwave-flash, application/vnd.ms-xpsdocument, */*";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET CLR 3.0.30618; InfoPath.2; Media Center PC 5.0; SLCC1; Tablet PC 2.0; .NET CLR 4.0.20506; MS-RTC LM 8; OfficeLiveConnector.1.4; OfficeLivePatch.1.3)";
            request.SendChunked = true;
            request.TransferEncoding = "gzip, deflate";
            request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            request.CookieContainer = cookieJar;
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Set the Credentials for the page
            request.Credentials = new NetworkCredential(this.Username, this.Password.ToInsecureString());
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            string Status = ((HttpWebResponse)response).StatusDescription;
#if DEBUG
            // Display the status.
            Debug.WriteLine(Status);
#endif
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            var reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
#if DEBUG
            // Display the content.
            Debug.WriteLine("--Respuesta del Servidor--");
            Debug.WriteLine(responseFromServer);
#endif
            // Clean up the streams.
            reader.Close(); 
            dataStream.Close();
            response.Close();
            return Status.Equals("OK");
        }

        /// <summary>
        ///     Obtiene una lista de imagenes (No sincronizada con el sitio) mias
        /// </summary>
        /// <returns>Lista de Imagenes</returns>
        public List<Image> GetProfileImagesThumbs()
        {
            var ImagesThumbs = new List<Image>();
            bool next = false;
            int nextPage = 0;
            int imgs;
            do
            {
                nextPage++;
                String Contenido = GetWebPageContent(String.Format("http://twitpic.com/photos/{0}?page={1}", this.Username, nextPage));
                Regex rx = new Regex("<a href=\"/(.+)\"><img src=\"(.+)\" width=\"150\" height=\"150\"></a>", RegexOptions.Compiled);
                MatchCollection matches = rx.Matches(Contenido);// ALERTA: Aquí Contenido puede ser Null y vale M
                imgs = 0;
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    ImagesThumbs.Add(GetImageFromUrl(groups[2].Value));///MediaUrl
                    imgs++;
                }
                next = Contenido.IndexOf(String.Format("<a class=\"nav\" href='/photos/{0}?page={1}'>OLDER >></a>",this.Username, nextPage + 1)) != -1;// Hay siguiente página?
            } while (next);// Cambiar la condición

            return ImagesThumbs;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public List<string> GetProfileImagesMediaIds()
        {
            var ImagesMediaIds = new List<string>();
            bool next = false;
            int nextPage = 0;
            int imgs;
            do
            {
                nextPage++;
                String Contenido = GetWebPageContent(String.Format("http://twitpic.com/photos/{0}?page={1}", this.Username, nextPage));
                Regex rx = new Regex("<a href=\"/(.+)\"><img src=\"(.+)\" width=\"150\" height=\"150\"></a>", RegexOptions.Compiled);
                MatchCollection matches = rx.Matches(Contenido);// ALERTA: Aquí Contenido puede ser Null y vale M
                imgs = 0;
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    ImagesMediaIds.Add(groups[1].Value);///MediaUrl
                    imgs++;
                }
                next = Contenido.IndexOf(String.Format("<a class=\"nav\" href='/photos/{0}?page={1}'>OLDER >></a>", this.Username, nextPage + 1)) != -1;// Hay siguiente página?
            } while (next);// Cambiar la condición

            return ImagesMediaIds;
        }

        /// <summary>
        ///     Obtiene la Imagen según su MediaId
        /// </summary>
        /// <param name="MediaId">MediaId de la imagen</param>
        /// <returns>System.Drawing.Image</returns>
        public Image GetImage(string MediaId)
        {
            String Contenido = GetWebPageContent(String.Format("http://twitpic.com/{0}/full", MediaId));
            #region Que Marranada >_< hice
            int indice_Inicial = Contenido.IndexOf("<img class=\"photo-large\" src=\"") + "<img class=\"photo-large\" src=\"".Length;
            int indice_Final = Contenido.IndexOf("\" alt=\"", indice_Inicial);
            int TamUrl = indice_Final - indice_Inicial;
            #endregion
            return GetImageFromUrl(Contenido.Substring(indice_Inicial, TamUrl));
        }
        #endregion

        #region Métodos de Soporte

        /// <summary>
        ///     Genera un nombre de archivo según el tipo requerido.
        /// </summary>
        /// <returns>string con el nombre del archivo según el formato de la imagen</returns>
        private string GenFileName()
        {
            string archivo;

            switch (ICT)
            {
                case "image/jpeg": archivo = @"C:\twitshot.jpg"; break;
                case "image/gif": archivo = @"C:\twitshot.gif"; break;
                case "image/png": archivo = @"C:\twitshot.png"; break;
                default: archivo = @"C:\twitshot.png"; break;
            }
            return archivo;
        }

        /// <summary>
        ///     Regresa el Content-Type en base al nombre de un archivo
        /// </summary>
        /// <param name="filename">Nombre del archivo</param>
        /// <returns>Cadena de Content-Type</returns>
        private string GetImageContentType(string filename)
        {
            if (filename.EndsWith("jpg", true, CultureInfo.CurrentCulture))
            {
                return "image/jpeg";
            }
            else if (filename.EndsWith("jpeg", true, CultureInfo.CurrentCulture))
            {
                return "image/jpeg";
            }
            else if (filename.EndsWith("gif", true, CultureInfo.CurrentCulture))
            {
                return "image/gif";
            }
            else if (filename.EndsWith("png", true, CultureInfo.CurrentCulture))
            {
                return "image/png";
            }
            else
            {
                return "";  /// TODO: Tirar una excepción
            }
        }

        /// <summary>
        ///     Convierte un objeto System.Drawing.Image en una matriz de bytes.
        /// </summary>
        /// <param name="img">Objeto System.Drawing.Image</param>
        /// <returns>Matriz de bytes</returns>
        public byte[] ToBytes(Image img)
        {
            var memoryStream = new MemoryStream();
            img.Save(memoryStream, ImageFormat.Png);
            return memoryStream.ToArray();
        }

        #region public Image GetImageFromUrl(+2 Sobrecargas)
        /// <summary>
        ///     Obtiene una imagen desde un URL
        /// </summary>
        /// <param name="url">String que contiene la URL de la imagen que se convertira en System.Drawing.Image</param>
        /// <returns>System.Drawing.Image</returns>
        public Image GetImageFromUrl(string url)
        {
            return GetImageFromUrl(new Uri(url));
        }

        /// <summary>
        ///     Obtiene una imagen desde un URL
        /// </summary>
        /// <param name="url">URL de la imagen que se convertira en System.Drawing.Image</param>
        /// <returns>System.Drawing.Image</returns>
        public Image GetImageFromUrl(Uri url)
        {
            var wr = (HttpWebRequest)WebRequest.Create(url);
            var respuesta = (HttpWebResponse)wr.GetResponse();
            Image img = Image.FromStream(respuesta.GetResponseStream());
            respuesta.Close();
            return img;
        } 
        #endregion

        /// <summary>
        ///     Obtiene el contenido de una página
        /// </summary>
        /// <param name="url">Url de la página</param>
        /// <returns>Contenido de la página</returns>
        private String GetWebPageContent(string url)
        {
            try
            {
                var wReq = (HttpWebRequest)WebRequest.Create(url);
                wReq.KeepAlive = false;
                wReq.UserAgent = "";
                var wResp = (HttpWebResponse)wReq.GetResponse();
                Stream responseStream = wResp.GetResponseStream();
                int bufCount;
                var byteBuf = new byte[1024];
                var ContenidoPagina = new StringBuilder();
                do
                {
                    bufCount = responseStream.Read(byteBuf, 0, byteBuf.Length);
                    if (bufCount != 0)
                    {
                        ContenidoPagina.Append(Encoding.ASCII.GetString(byteBuf, 0, bufCount));
                    }
                }
                while (bufCount > 0);
                return ContenidoPagina.ToString();
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.Write(ex.Message);
#endif
                return null;
            }
        }
        
        #endregion
    }
}
