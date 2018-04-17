using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class SICAH_Meteo_index : System.Web.UI.Page
{

    private string rutaWeb = "C:\\Inetpub\\wwwroot\\SicaSegura\\SICAH\\Meteo\\";

    protected void Page_Load(object sender, EventArgs e)
    {

        string verImagenes = Request.QueryString["getImages"];


        if (verImagenes != null && verImagenes.Length > 0) verImagenesEstacion(verImagenes);
        else
        {

            string estacion = Request.Form["station"];
            string rutaImagenes = "upload\\" + estacion + "\\images";

            string nameFile = Request.Form["filename"];

            if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0) {
                Response.Write("NO_FILE_DETECTED");
            } else if (nameFile == null || nameFile.Length == 0) {
                Response.Write("NO_FILENAME_DETECTED");
            }
            else if (estacion == null || estacion.Length == 0)
            {
                Response.Write("NO_STATION_DETECTED");
            } else {
                
                /*
                DateTime dt = DateTime.Now;

                String mes  = (dt.Month + 100) + "";
                String dia =  (dt.Day + 100) + "";
                String hora = (dt.Hour + 100) + "";
                String minu = (dt.Minute + 100) + "";
                String segu = (dt.Second + 100) + "";

                nameFile = dt.Year + mes.Substring(1, 2) + dia.Substring(1, 2) + hora.Substring(1, 2) + minu.Substring(1, 2) + segu.Substring(1, 2) + ".jpg";                

                */

                Request.Files.Get(0).SaveAs(rutaWeb + rutaImagenes + "\\" + nameFile);
                Request.Files.Get(0).SaveAs(rutaWeb + rutaImagenes + "\\ultima.jpg");
                Response.Write("FILE_UPLOADED: " + nameFile);

            }
        }

    }



    private void verImagenesEstacion(String estacion)
    {
        string rutaImagenes = "upload\\" + estacion + "\\images";
        DirectoryInfo carpetaImages = new DirectoryInfo(rutaWeb + rutaImagenes);
        FileInfo[] ficherosImages = carpetaImages.GetFiles();

        string htmlcode = "<h1>Imagenes estacion " + estacion + "</h1>";


        htmlcode += "<table>";


        for (int i = 0; i < ficherosImages.Length; i++)
        {
            if (ficherosImages[i].Name.Contains(".jpg"))
            {

                htmlcode += "<tr><td>";
                htmlcode += "<a href=\"" + rutaImagenes.Replace("\\", "/") + "/" + ficherosImages[i].Name +
                            "\"/>" + ficherosImages[i].Name + "</a>";
                htmlcode += "</td></tr>";

            }
        }

        htmlcode += "</table>";
        Response.Write(htmlcode);
    }
	
	

}

