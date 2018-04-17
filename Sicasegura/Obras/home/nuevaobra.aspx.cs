using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Obras_home_nuevaobra : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SicaSegura.SICA_Interfaz.SICA_Gestion_obras Interfaz = new SicaSegura.SICA_Interfaz.SICA_Gestion_obras();


        string menu = "-";
        HTML_Links_Sup_Izq.Text = Interfaz.Get_Enlaces_Sup_Izq(menu.Split('#'));
        HTML_Subcabecera_Logos.Text = Interfaz.Get_Cabecera();
        HTML_Menu_Navegacion.Text = Interfaz.Get_Menu_Navegacion("tres");
        HTML_Pie_Logo_Corporativo.Text = Interfaz.Get_pie_logo_corporativo();
        HTML_Pie_pagina.Text = Interfaz.Get_pie_pagina();
        HTML_Link_InformacionGeneral.Text = "<li><a href=\"home/#\">Documentacion General</a></li>";
        HTML_Link_DocumentacionAdministrativa.Text = "<li><a href=\"home/#\">Documentacion Administrativa</a></li>";
        HTML_Link_DocumentacionTecnica.Text = "<li><a href=\"home/#\">Documentación Técnica</a></li>";
        HTML_Link_DocumentacionEconomica.Text = "<li><a href=\"home/#\">Documentación Económica</a></li>";
    }
}
