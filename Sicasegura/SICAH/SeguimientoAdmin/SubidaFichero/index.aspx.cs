using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class SICAH_SeguimientoAdmin_SubidaFichero_post_file : System.Web.UI.Page
{
    private string parametro;
    protected void Page_Load(object sender, EventArgs e)
    {
        string ruta = "";
        parametro = Request.QueryString["P"];
        LBL_IdCarpeta.Text = "<font face='verdana'size='2'><b><br> " + parametro.Replace("444", "  > ") + " </b></font><br><br><br>";
        //Response.Write(parametro);
        ruta = parametro.Replace("444", "").Replace("/","");
        if (System.IO.Directory.Exists(@"C:\uploads\" + ruta))
        {
            LBL_NumFicheros.Text= "Hay <b>" + System.IO.Directory.GetFiles(@"C:\uploads\" + ruta).Length + "</b> Ficheros";
        }

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        if (browser.Browser == "IE")
        {
            Response.Redirect("indexie.aspx?P=" + parametro);
        }
        else
        {
            Response.Redirect("indexie.aspx?P=" + parametro);
            //Response.Redirect("index.html");
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        if (browser.Browser == "IE")
        {
            Response.Redirect("indexie.aspx?P=" + parametro +"&C=SI");
        }
        else
        {
            Response.Redirect("indexie.aspx?P=" + parametro + "&C=SI");
            //Response.Redirect("index.html");
        }
    }
}
















