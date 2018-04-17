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
    
    private string pvycr = "";
    private string Dirbase="C:\\Inetpub\\wwwroot\\BaseFotografica\\Images\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        pvycr = Request.QueryString["clavenodo"];
        LIT_Menu.Text = GuarderiaFluvial.genHTML.EnlacesMenuArbol(8, "../../", Page, 11, "P", pvycr, "N");
        //pvycr="va002p30";
        PreparaThumbailsDir();
        comprobarThumbails();
        configurarPagina();
        GenerarCroquis();
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
                    htmlcode +="<div class=\"single first\">";
                }
                else if ( i==4 || i==9|| i==14|| i==19|| i==24|| i==29|| i==34|| i==39 || i==44)
                {
                    htmlcode +="<div class=\"single last\">";
                }
                else
                {
                     htmlcode +="<div class=\"single\">";
                }
                htmlcode += "<a href=\"" + ficherosImages[i].Directory.ToString().Replace(@"C:\Inetpub\wwwroot\", "../../../../").Replace("\\", "/") + "/" + ficherosImages[i].Name +
            				"\" rel=\"lightbox_plus\" title=\"" + ficherosImages[i].Name + "\">";
                htmlcode += "<img src=\"" + ficherosImages[i].Directory.ToString().Replace(@"C:\Inetpub\wwwroot\", "../../../../").Replace("\\", "/") + "/TB/" + ficherosImages[i].Name + "\" alt=\"Plants: image 1 0f 4 thumb\" /></a>";
                htmlcode += "</div>";
            }
        }
        htmlcode +="</div>";
        htmlcode +="</div>";
        LIT_Galeria.Text = htmlcode;
    }
    private void GenerarCroquis()
    {
        string miniaturaError="";
        string strHtml="";
        miniaturaError = "../../../../BaseFotografica/Images/01-Croquis/TB/CroquisDefault.jpg";
        strHtml += "<td align='center' valign='bottom' colspan='5' style='text-align:center;'>";
        strHtml += "<center><a href='../../../../BaseFotografica/Images/01-Croquis/" + pvycr + ".jpg' rel='lightbox_plus' class='vertical' target='_blank'>";
        strHtml += "<img width='320' height='226' src='../../../../BaseFotografica/Images/01-Croquis/" + pvycr + ".jpg'  onerror=\"this.src='" + miniaturaError + "';\"> ";
        strHtml += "</a></center><br><hr><br>";
        strHtml += "</td>";
        strHtml += "</tr><tr>";
        LIT_Croquis.Text = strHtml;
    }


    public void BotonFileUpload_Click(Object sender,
                           EventArgs e)
    {
        String nombreFi="";

        //Button clickedButton = (Button)sender;        
        //clickedButton.Enabled = false;

        if (FileUpload.HasFile) {

           nombreFi = Dirbase + pvycr + "\\" + FileUpload.FileName;
           FileUpload.PostedFile.SaveAs(nombreFi);

           PreparaThumbailsDir();
           comprobarThumbails();
           configurarPagina();
           GenerarCroquis(); 
        }
    }

}
