using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Net;//Añadido para ftp

public partial class SICAH_Agrupaciones_download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ///Comprobamos que se ha pasado por la página indexcas.aspx y se ha hecho login
        if (true)
        {
            try
            {   //Recibimos el fichero que pretendemos descargar
                string file = Request.QueryString["file"];
                //string carpeta = Request.QueryString["Carpeta"].ToString();
                string carpeta = @"C:\uploads\" + Request.QueryString["Carpeta"] + @"\";
                
                //Lanzamos la descarga del fichero que se solicita y que recibirá el nombre aleatorio en base
                //al momento de la descarga.
                TransmitFile(Context, carpeta + file, DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString(), true);
            }
            //Capturamos cualquier tipo de excepción que hará que volvamos a la página principal
            catch (Exception ex) { Response.Redirect("./index.apsx"); }
        }
        else
        {
            //Si no estamos autenticados nos devuelve a la página de inicio
            //que a su vez al no estar logeados nos llevará al CAS.
            Response.Redirect("../inicio");
        }

    }
    /// <summary>
    /// Funcion desarrollada para realizar a modo de "blackBox" la descarga de los ficheros
    /// Cualquier documento para la descarga se encuentra en una  ruta no pública a la que accede
    /// la aplicación web para la descarga. El fichero se lee y se devuelve al usuario como un fichero 
    /// nuevo con el formato y contenido del que se encuentra en el servidor.
    /// </summary>
    /// <param name="context">HttpContext</param>
    /// <param name="filePath">Ruta Absoluta del fichero a descargar</param>
    /// <param name="downloadName">Nombre con el que se descargará el fichero</param>
    /// <param name="forceDownload">Permite que la descarga se incruste en el navegador o siempre se descargue</param>
    private void TransmitFile(HttpContext context, string filePath, string downloadName, bool forceDownload)
    {
        if (File.Exists(filePath))
        {
            string fileName = System.IO.Path.GetFileName(filePath);
            string extension = Path.GetExtension(filePath);
            FileInfo fileInfo = new FileInfo(filePath);

            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            if (forceDownload)
            {
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + downloadName.Replace(" ", "_") + extension);
                context.Response.BufferOutput = false;
            }

            string type = "";
            if (extension != null)
            {
                switch (extension.ToLower())
                {
                    case ".tif":
                    case ".tiff":
                        type = "image/tiff";
                        break;
                    case ".jpg":
                    case ".jpeg":
                        type = "image/jpeg";
                        break;
                    case ".gif":
                        type = "image/gif";
                        break;
                    case ".doc":
                    case ".rtf":
                        type = "Application/msword";
                        break;
                    case "pdf":
                        type = "Application/pdf";
                        break;
                    case "png":
                        type = "image/png";
                        break;
                    case "bmp":
                        type = "image/bmp";
                        break;
                    default:
                        type = "application/octet-stream";
                        break;
                }
            }

            context.Response.ContentType = type;
            context.Response.TransmitFile(filePath);
            context.Response.Flush();
        }
        else
        {
            Response.Write("<center><h1>El fichero al que intenta acceder no está<br>disponible temporalmente<br><br></h1><b>Disculpe las molestias</b></center>");
        }
    }
}


