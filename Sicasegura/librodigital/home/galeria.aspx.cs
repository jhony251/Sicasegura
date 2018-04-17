using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Configuration;

public partial class SICAH_Galeria_galeria : System.Web.UI.Page
{
    SicaSegura.SICA_LibroControl LCA;
    SicaSegura.SICA_Interfaz.SICA_LibroControl InterfazLibroControl;
    private string pvycr = "";
    private string Dirbase="C:\\Inetpub\\wwwroot\\BaseFotografica\\Images\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            object Objeto_LC = Session["LCA"];
            object Titulo_Inscripcion = Session["USERNAME"];

            LCA = (SicaSegura.SICA_LibroControl)Objeto_LC;
            InterfazLibroControl = new SicaSegura.SICA_Interfaz.SICA_LibroControl();
        }
        catch { Response.Redirect("../index.aspx"); }



        //
        //GENERACION DE LA INTERFAZ
        //
        string menu = "Cerrar sesion-borrarSesion.aspx#Información de usuario-index.aspx";
        HTML_Links_Sup_Izq.Text = InterfazLibroControl.Get_Enlaces_Sup_Izq(menu.Split('#'));
        HTML_Subcabecera_Logos.Text = InterfazLibroControl.Get_Cabecera();
        HTML_Menu_Navegacion.Text = InterfazLibroControl.Get_Menu_Navegacion("cuatro");
        HTML_Pie_Logo_Corporativo.Text = InterfazLibroControl.Get_pie_logo_corporativo();
        HTML_Pie_pagina.Text = InterfazLibroControl.Get_pie_pagina();

        HTML_Link_Esqumas.Text = "<li><a href=\"esquemas.aspx\">Esquema Instalaciones</a></li>";
        HTML_Link_Fotos.Text = "<li class=\"selected\"><a  href=\"galeria.aspx\">Fotos Instalaciones</a>" + get_listadoPuntosMenuLateralFotos();
        HTML_Link_Infadm_pdf.Text = "<li><a href=\"download.aspx?area=Raacs&File=" + LCA.Agrupacion.Get_NumeroInscripcion() + ".pdf\">Derechos (pdf)</a></li>";
        HTML_Link_Infadm.Text = "<li><a href=\"infadm.aspx\">Información administrativa</a></li>";
        //HTML_Link_InfContadores.Text = "<li><a  href=\"contadores.aspx\">Información contadores</a></li>";


        if (Request.QueryString["codigoPVYCR"] == null || Request.QueryString["codigoPVYCR"] == "")
        {
            pvycr = ((string[])LCA.Agrupacion.Get_PuntosDeContros())[0].ToUpper().Split('-')[0].ToString();
        }
        else { pvycr = Request.QueryString["codigoPVYCR"]; }
        //pvycr = Session["PuntoActivo"].ToString().Split('-')[0];
        
        //pvycr="va002p30";
        PreparaThumbailsDir();
        comprobarThumbails();
        configurarPagina();
        GenerarCroquis();
    }
    private string get_listadoPuntosMenuLateralFotos()
    {
        string HtmlCode = "<ul>";
        string [] puntos = LCA.Agrupacion.Get_PuntosDeContros();
        for (int i = 0; i < puntos.Length; i++)
        {
            HtmlCode += "<li><a href=\"galeria.aspx?codigoPVYCR=" + puntos[i].ToUpper().Split('-')[0] + "&EM=" + puntos[i].ToUpper().Split('-')[1].Trim() + "\">" + puntos[i].ToUpper() + "</a>";
        }
        HtmlCode += "</ul></li>";
        return HtmlCode;
    }

    private void comprobarThumbails()
    {
        DirectoryInfo carpetaImages = new DirectoryInfo(Dirbase + pvycr + "");
        DirectoryInfo carpetaThumbs = new DirectoryInfo(Dirbase + pvycr + "\\TB");
        FileInfo[] ficherosImages = carpetaImages.GetFiles();
        FileInfo[] ficherosThumbs = carpetaThumbs.GetFiles();
        for (int i = 0; i < ficherosImages.Length; i++)
        {
            bool crearThumb = true;
            string imagen = ficherosImages[i].Name.ToString();
            if (imagen.Contains("db")) { crearThumb = false; }
            else
            {
                for (int ii = 0; ii < ficherosThumbs.Length; ii++)
                {
                    string thumb = ficherosThumbs[ii].Name.ToString();
                    if (imagen == thumb) { crearThumb = false; }
                }
            }
            if (crearThumb == true) { GenerateThumbNail(ficherosImages[i].DirectoryName + "\\" + imagen, ficherosImages[i].DirectoryName + "\\TB\\" + imagen, 150); }
        }

    }
    private void PreparaThumbailsDir()
    {
        DirectoryInfo carpeta = new DirectoryInfo(Dirbase + pvycr + "\\TB");
        
        try
        {
            //si no existe la carpeta temporal la creamos
            if (!(carpeta.Exists))
            {
                Directory.CreateDirectory("c:\\inetpub\\wwwroot\\basefotografica\\images\\" + pvycr + "\\TB");                
            }
            else{ }
        }
        catch (Exception errorC) { }
    }
    private void GenerateThumbNail(string sourcefile, string destinationfile, int width)
    {
        Bitmap bmp = null;
        System.Drawing.Image image = null;
        int srcWidth = 0;
        int srcHeight = 0;

        //string script = "alert(\"" + sourcefile + "\");";
        //this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, true);

        try
        {
            image = System.Drawing.Image.FromFile(sourcefile);

            //FileStream fs = new FileStream(sourcefile, FileMode.Open, FileAccess.Read);
            //System.Drawing.Image image = System.Drawing.Image.FromStream(fs);

            srcWidth = image.Width;
            srcHeight = image.Height;

            int thumbWidth = width;
            int thumbHeight;



            //thumbHeight = thumbWidth;
            //thumbWidth = (srcWidth / srcHeight) * thumbHeight;
            //bmp = new Bitmap(width, width);
            if (srcHeight > srcWidth)
            {
                thumbHeight = (srcHeight / srcWidth) * thumbWidth;
                bmp = new Bitmap(thumbWidth, thumbHeight);
            }
            else
            {
                thumbHeight = thumbWidth;
                thumbWidth = (srcWidth / srcHeight) * thumbHeight;
                bmp = new Bitmap(thumbWidth, thumbHeight);
            }

            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            System.Drawing.Rectangle rectDestination =
                   new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
            gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
            bmp.Save(destinationfile);
            bmp.Dispose();
        }
        catch (Exception ex)
        {
            
            // Indicamos que no se ha podido generar (jmolina4;26/03/2014)
            image = System.Drawing.Image.FromFile("c:\\inetpub\\wwwroot\\basefotografica\\PreviaNoDisponible.jpg");
            srcWidth = image.Width;
            srcHeight = image.Height;
            image.Save(destinationfile);
            
        }
        


        image.Dispose();


    }

    private void configurarPagina()
    {
        DirectoryInfo carpetaImages = new DirectoryInfo(Dirbase + pvycr + "");
        FileInfo[] ficherosImages = carpetaImages.GetFiles();
        string htmlcode="";


        htmlcode +="<div class=\"imageRow\">";
  	    htmlcode +="<div class=\"set\">";
        
  		for(int i=0; i< ficherosImages.Length; i++)
        {
            if (ficherosImages[i].Name.Contains("db")){}
            else
            {
                if(i==0 || i==5 || i==10|| i==15|| i==20|| i==25|| i==30|| i==35|| i==40)
                {
                    htmlcode +="<div class=\"single first\" style=\"height:250px;\">";
                }
                else if ( i==4 || i==9|| i==14|| i==19|| i==24|| i==29|| i==34|| i==39 || i==44)
                {
                    htmlcode += "<div class=\"single last\" style=\"height:250px;\">";
                }
                else
                {
                    htmlcode += "<div class=\"single\" style=\"height:250px;\">";
                }
                htmlcode += "<a href=\"" + ficherosImages[i].Directory.ToString().Replace(@"C:\Inetpub\wwwroot\", "../../../").Replace("\\", "/") + "/" + ficherosImages[i].Name +
            				"\" rel=\"lightbox_plus\" title=\"" + ficherosImages[i].Name + "\">";
                htmlcode += "<img src=\"" + ficherosImages[i].Directory.ToString().Replace(@"C:\Inetpub\wwwroot\", "../../../").Replace("\\", "/") + "/TB/" + ficherosImages[i].Name + "\"  /></a>";
                htmlcode += "</div>";
            }
        }
        htmlcode +="</div>";
        htmlcode +="</div>";
        if (ficherosImages.Length == 0)
        {
            htmlcode += "<span style=\"font-size:20px\">Estamos trabajando en disponer de imágenes de todos los aprovechamientos</span><br><br><br>";
            htmlcode += "<span style=\"font-size:15px\">Puede aportar las imágenes que desee por email a sica@chsegura.es</span><br>";
            htmlcode += "<span style=\"font-size:15px\">Gracias</span><br>";
        }
        LIT_Galeria.Text = htmlcode;
    }
    private void GenerarCroquis()
    {
        string miniaturaError="";
        string strHtml="";
        miniaturaError = "../../../BaseFotografica/Images/01-Croquis/TB/CroquisDefault.jpg";
        strHtml += "<td align='center' valign='bottom' colspan='5' style='text-align:center;'>";
        strHtml += "<center><a href='../../../BaseFotografica/Images/01-Croquis/" + pvycr + ".jpg' rel='lightbox_plus' class='vertical' target='_blank'>";
        strHtml += "<img width='320' height='226' src='../../../BaseFotografica/Images/01-Croquis/" + pvycr + ".jpg'  onerror=\"this.src='" + miniaturaError + "';\"> ";
        strHtml += "</a></center><br><hr><br>";
        strHtml += "</td>";
        strHtml += "</tr><tr>";
        //LIT_Croquis.Text = strHtml;
    }


}
