using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICA_index : System.Web.UI.Page
{
    string tipoAcceso = "";
    string punto = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try { tipoAcceso = Request.QueryString["tipo"].ToString(); }
        catch { tipoAcceso = "All"; }
        try { punto = Request.QueryString["punto"].ToString(); }
        catch
        {
            SicaSegura.SICA_Agrupacion agrupacion = new SicaSegura.SICA_Agrupacion(Convert.ToInt16(Session["IDRaacs"].ToString()));
        Response.Redirect("fotos.aspx?punto=" + agrupacion.Get_PuntosDeContros()[0].ToString());
        }
        ///HAY QUE CONTROLAR EL ACCESO DE LOS USUARIOS QUE NO SON SICA
        SicaSegura.SICA_Interfaz.SICA_Sica InterfazSica  = new SicaSegura.SICA_Interfaz.SICA_Sica();

        String menu  = "-#-";
        HTML_Links_Sup_Izq.Text = InterfazSica.Get_Enlaces_Sup_Izq(menu.Split('#'));
        HTML_Subcabecera_Logos.Text = InterfazSica.Get_Cabecera();
        HTML_Menu_Navegacion.Text = InterfazSica.Get_Menu_Navegacion_Completo("cinco");
        CargaMenuLateral();
        HTML_Pie_Logo_Corporativo.Text = InterfazSica.Get_pie_logo_corporativo();
        HTML_Pie_pagina.Text = InterfazSica.Get_pie_pagina();
        NineRays.Reporting.Register.SetLicenseKey("4761628416024064132096");
        Cargar_Galeria();
    }

    private void CargaMenuLateral()
    {
        
        HTML_Links_puntoscontrol.Text="";
        SicaSegura.SICA_Agrupacion A = new SicaSegura.SICA_Agrupacion(Convert.ToInt16(Session["IDRaacs"].ToString()));
        string [] puntos = A.Get_PuntosDeContros();
        for (int i=0; i<puntos.Length;i++)
        {
            HTML_Links_puntoscontrol.Text+="<li><a href=\"fotos.aspx?punto=" + puntos[i].ToString() + "\">" + puntos[i].ToString() + "</a></li>";
        }
    }
    private void Cargar_Galeria()
    {
        HTML_Galeria.Text = "";
        string[] array1 = Directory.GetFiles(Session["ruta"].ToString() + @"\fotos\" + punto);
        for(int i=0; i<array1.Length; i++)
        {
            string[] rutaparcial = array1[i].ToString().Split(new string[] { "wwwroot" }, StringSplitOptions.None);
            HTML_Galeria.Text += "<DIV style=\"float:left; width: 200px;\"><img style=\"width:200px\" src=\"../../.."+rutaparcial[1].Replace('\\','/') +"\"></DIV>";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            // Get the HttpFileCollection
            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    hpf.SaveAs(Session["Ruta"].ToString() + "\\fotos\\" + punto + "\\" + Path.GetFileName(hpf.FileName));
                }
            }
        }
        catch (Exception ex)
        {
            // Handle your exception here
        }
        Cargar_Galeria();
    }
}
