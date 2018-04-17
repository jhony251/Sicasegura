using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class SICAH_SeguimientoAdmin_SubidaFichero_post_file : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        HttpFileCollection UploadFicheroColeccion = HttpContext.Current.Request.Files;
        for (int i = 0; i < UploadFicheroColeccion.Count;i++ )
        {
            HttpPostedFile UploadFichero = UploadFicheroColeccion[0];
            UploadFichero.SaveAs("C:\\uploads\\" + Path.GetFileName(UploadFichero.FileName));
        }
        UploadFicheroColeccion = null;


        //Dim UploadFicheroColeccion As HttpFileCollection = HttpContext.Current.Request.Files
        //Dim UploadFichero As HttpPostedFile = UploadFicheroColeccion(0)
        //UploadFichero.SaveAs(HttpContext.Current.Server.MapPath(“files\”) & Path.GetFileName(UploadFichero.FileName))
        //UploadFicheroColeccion = Nothing
        //Response.Write(“{“”status”":”"File was uploaded successfuly!”"}”)

    }
}
