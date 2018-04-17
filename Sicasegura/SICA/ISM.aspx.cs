using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICA_ISM : System.Web.UI.Page
{
    String ISM = "";
    String Raacs = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try { ISM = Request.QueryString["IDISM"]; }
        catch { ISM = "N/A"; }
        try { Raacs = Request.QueryString["IDRaacs"]; }
        catch { Raacs = "N/A"; }
        Session.Add("ISM", ISM);
        Session.Add("IDRaacs", Raacs);
        try {SicaSegura.SICA_Agrupacion A = new SicaSegura.SICA_Agrupacion(Convert.ToInt16(Session["IDRaacs"].ToString())); }
        catch { Response.Redirect("index.aspx");}
        
        if (ExisteEstructuraDeCarpetas()) 
        {

        }
        else { Response.Redirect("index.aspx"); }
        ///HAY QUE CONTROLAR EL ACCESO DE LOS USUARIOS QUE NO SON SICA
        SicaSegura.SICA_Interfaz.SICA_Sica InterfazSica = new SicaSegura.SICA_Interfaz.SICA_Sica();

        String menu = "-#-";
        HTML_Links_Sup_Izq.Text = InterfazSica.Get_Enlaces_Sup_Izq(menu.Split('#'));
        HTML_Subcabecera_Logos.Text = InterfazSica.Get_Cabecera();
        HTML_Menu_Navegacion.Text = InterfazSica.Get_Menu_Navegacion_Completo("dos");
        //Cargalistado_ISM();
        HTML_Pie_Logo_Corporativo.Text = InterfazSica.Get_pie_logo_corporativo();
        HTML_Pie_pagina.Text = InterfazSica.Get_pie_pagina();
        NineRays.Reporting.Register.SetLicenseKey("4761628416024064132096");
        
    }
    private bool ExisteEstructuraDeCarpetas()
    {
        string rutabase=@"C:\Inetpub\wwwroot\BaseDocumental\";
        Session.Add("Ruta", rutabase + Raacs + @"$$" + ISM.Replace('/', '-'));
        SicaSegura.SICA_SysemIO IO = new SicaSegura.SICA_SysemIO();
        if (IO.ExisteCarpetaEnServidor(rutabase + Raacs + @"$$" + ISM.Replace('/', '-')))
        {
            if (IO.ExisteCarpetaEnServidor(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Informes_Consumos"))
            { }
            else
            { IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Informes_Consumos"); }
            if (IO.ExisteCarpetaEnServidor(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Obras"))
            { }
            else
            { IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Obras"); }
            if (IO.ExisteCarpetaEnServidor(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Fotos"))
            { }
            else
            { IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Fotos"); }
            if (IO.ExisteCarpetaEnServidor(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Esquemas"))
            { }
            else
            { IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Esquemas"); }
            if (IO.ExisteCarpetaEnServidor(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Mantenimientos"))
            { }
            else
            { IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Mantenimientos"); }
            if (IO.ExisteCarpetaEnServidor(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Oficios"))
            { }
            else
            { IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Oficios"); }
        }
        else
        {
            IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-'));
            IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Informes_Consumos");
            IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Obras");
            IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Fotos");
            IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Esquemas");
            IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Mantenimientos");
            IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Oficios");
            SicaSegura.SICA_Agrupacion A = new SicaSegura.SICA_Agrupacion(Convert.ToInt16(Session["IDRaacs"].ToString()));
            string[] puntos = A.Get_PuntosDeContros();
            for (int i = 0; i < puntos.Length; i++)
            {
                IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Informes_Consumos\" + puntos[i].ToString());
                IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Obras\" + puntos[i].ToString());
                IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Fotos\" + puntos[i].ToString());
                IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Esquemas\" + puntos[i].ToString());
                IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Mantenimientos\" + puntos[i].ToString());
                IO.CrearCarpeta(rutabase + Raacs + @"$$" + ISM.Replace('/', '-') + @"\Oficios\" + puntos[i].ToString());
            }
        }
        
        

        return true;

    }
}
//http://forums.asp.net/t/1263634.aspx?Passing+parameters+from+webpage+to+Generic+Handler+page