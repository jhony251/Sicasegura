using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class SICAH_SeguimientoAdmin_SubidaFichero_post_file : System.Web.UI.Page
{
    private string parametro, parametro2;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["C"] == null)
        {
            if (Request.QueryString["R"] != null)
            {
                Response.Write(Request.QueryString["R"]);
            }
        }
        parametro = Request.QueryString["p"];
        parametro2 = parametro;
        parametro2 = parametro2.Replace("444", "");
        parametro2 = parametro2.Replace(@"/", "");
        if (Request.QueryString["C"] == "SI")
        {
            PNL_CajaSubida.Visible = false;
            Muestra_ficheros();
        }

    }
    private void Muestra_ficheros()
    {
        if(System.IO.Directory.Exists("C:\\uploads\\" + parametro2 + "\\"))
        {
            foreach (string file in System.IO.Directory.GetFiles("C:\\uploads\\" + parametro2 + "\\"))
            {
                string ficheroname = file.Split('\\')[file.Split('\\').Length - 1];
                string extension = ficheroname.Split('.')[1];
                ficheroname = ficheroname.Split('.')[0];
                string ficheronamesmall = ficheroname;
                if (ficheroname.Length > 7)
                {
                    ficheronamesmall = ficheronamesmall.Substring(0, 7);
                }
                LBL_listaFicheros.Text += "<div style=\"float:left; width:112px;margin-bottom:15px;\" ><center><a href=\"../download.aspx?Carpeta=" + parametro2 + "&file=" + ficheroname + "." + extension + "\" target=\"_blank\" title=\"" + ficheroname + "\"><font family='verdana' size='2' color='black'><b style='text-decoration:none;'><img src='../images/" + extension + ".png' border='0' height='96' alt='" + ficheroname + "'><br>" + ficheronamesmall + "... </b></font></a></center></div>";
                //LBL_listaFicheros.Text += "<br><a href=\"../download.aspx?Carpeta=" + parametro2 + "&file=" + file.Split('\\')[file.Split('\\').Length - 1] + "\" target=\"_blank\">" + file.Split('\\')[file.Split('\\').Length - 1] + "</a>";
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
            try
            {
                if (System.IO.Directory.Exists("C:\\uploads\\" + parametro2 + "\\")) { }
                else { System.IO.Directory.CreateDirectory("C:\\uploads\\" + parametro2 + "\\"); }
                FileUpload1.SaveAs("C:\\uploads\\" + parametro2 + "\\" + FileUpload1.FileName);
                Label1.Text = "File name: " + FileUpload1.PostedFile.FileName + "<br>" +
                FileUpload1.PostedFile.ContentLength + " kb<br>" + "Content type: " + FileUpload1.PostedFile.ContentType;
                Response.Redirect("indexie.aspx?P=" + parametro + "&R=" + Label1.Text.Replace("<br>"," ") );
        }
        catch (Exception ex)
        {
            Label1.Text = "ERROR: " + ex.Message.ToString();
        }
    else
    {
        Label1.Text = "You have not specified a file.";
    }
    }
}
















