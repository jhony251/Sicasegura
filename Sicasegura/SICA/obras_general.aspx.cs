using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICA_obras_general : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ///HAY QUE CONTROLAR EL ACCESO DE LOS USUARIOS QUE NO SON SICA
        SicaSegura.SICA_Interfaz.SICA_Sica InterfazSica = new SicaSegura.SICA_Interfaz.SICA_Sica();

        String menu = "-#-";
        HTML_Links_Sup_Izq.Text = InterfazSica.Get_Enlaces_Sup_Izq(menu.Split('#'));
        HTML_Subcabecera_Logos.Text = InterfazSica.Get_Cabecera();
        HTML_Menu_Navegacion.Text = InterfazSica.Get_Menu_Navegacion_Completo("cuatro");
        //Cargalistado_ISM();
        HTML_Pie_Logo_Corporativo.Text = InterfazSica.Get_pie_logo_corporativo();
        HTML_Pie_pagina.Text = InterfazSica.Get_pie_pagina();
        NineRays.Reporting.Register.SetLicenseKey("4761628416024064132096");
    }
}
