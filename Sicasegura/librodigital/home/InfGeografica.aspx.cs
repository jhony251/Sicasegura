using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class librodigital_home_InfGeografica : System.Web.UI.Page
{
    SicaSegura.SICA_LibroControl LCA;
    SicaSegura.SICA_Interfaz.SICA_LibroControl InterfazLibroControl;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            object Objeto_LC = Session["LCA"];
            object Titulo_Inscripcion = Session["USERNAME"];

            LCA = (SicaSegura.SICA_LibroControl)Objeto_LC;
            InterfazLibroControl = new SicaSegura.SICA_Interfaz.SICA_LibroControl();
            //
            //GENERACION DE LA INTERFAZ
            //
            string menu = "Cerrar sesion-borrarSesion.aspx#Información de usuario-index.aspx";
            HTML_Links_Sup_Izq.Text = InterfazLibroControl.Get_Enlaces_Sup_Izq(menu.Split('#'));
            HTML_Subcabecera_Logos.Text = InterfazLibroControl.Get_Cabecera();
            HTML_Menu_Navegacion.Text = InterfazLibroControl.Get_Menu_Navegacion("tres");
            HTML_Pie_Logo_Corporativo.Text = InterfazLibroControl.Get_pie_logo_corporativo();
            HTML_Pie_pagina.Text = InterfazLibroControl.Get_pie_pagina();
            string[] titular = (new SicaSegura.SICA_Titular_Aprovechamiento()).GetListadoTitularesRaacs(LCA.ID_Inscripcion)[0].Split(new string[] { "$%" }, StringSplitOptions.None);
            string otros = "";
            if ((new SicaSegura.SICA_Titular_Aprovechamiento()).GetListadoTitularesRaacs(LCA.ID_Inscripcion).Length > 1)
            {
                otros = " (y otros)";
            }
            HTML_Título_Aprovechamiento.Text = "Inscripción: " + LCA.ID_Inscripcion.ToString() + " -- " + LCA.Agrupacion.Get_Descripcion() + " <br>Titular: " + titular[1] + " " + titular[2] + otros;
            

            SicaSegura.SICA_PuntoControl Punto = new SicaSegura.SICA_PuntoControl(LCA.Agrupacion.Get_PuntosDeContros()[0].Split('-')[0].ToString().Trim());
            Int64 utmX = Convert.ToInt64(Punto.get_CoordenadasUTM()[0]);
            Int64 utmY = Convert.ToInt64(Punto.get_CoordenadasUTM()[1]);

            Int64 Xmax = utmX + 75;
            Int64 Xmin = utmX - 350;
            Int64 Ymin = utmY - 420;
            Int64 Ymax = utmY + 5;


            HTML_Iframe_GIS.Text = "<iframe src=\"http://www.chsegura.es/chsic/?escenario=sica&extent=" + Xmin + " " + Ymin + " " + Xmax + " " + Ymax + " 25830\" frameborder=\"0\" scrolling=\"no\" width=\"100%\" height\"500px\"></iframe>";

        }
        catch { Response.Redirect("../index.aspx"); }



        

    }
}
