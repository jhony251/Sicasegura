using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Obras_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SicaSegura.SICA_Interfaz.SICA_Gestion_obras Interfaz = new SicaSegura.SICA_Interfaz.SICA_Gestion_obras();


        string menu = "-";
        HTML_Links_Sup_Izq.Text = Interfaz.Get_Enlaces_Sup_Izq(menu.Split('#'));
        HTML_Subcabecera_Logos.Text = Interfaz.Get_Cabecera_login();
        HTML_Menu_Navegacion.Text = Interfaz.Get_Menu_Navegacion_login("uno"); 
        HTML_Pie_Logo_Corporativo.Text = Interfaz.Get_pie_logo_corporativo();
        HTML_Pie_pagina.Text = Interfaz.Get_pie_pagina();
        HTML_Link_NuevaObra.Text = "<li><a href=\"home/nuevaobra.aspx\">Nueva Obra</a></li>";
        HTML_Link_GestionarObrasExistentes.Text = "<li><a href=\"home/Gestionaobra.aspx\">Gestionar Obra Existente</a></li>";
        HTML_Link_EliminarObra.Text = "<li><a href=\"home/Eliminarobra.aspx\">Eliminar Obra Existente</a></li>";
    }
}
