using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sicadll.AccesoADatos;
using System.Globalization;

public partial class librodigital_home_index : System.Web.UI.Page
{
    SicaSegura.SICA_LibroControl LCA;
    SicaSegura.SICA_Interfaz.SICA_LibroControl InterfazLibroControl;
    DateTime FF, FI;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            object Objeto_LC = Session["LCA"];
            object Titulo_Inscripcion = Session["USERNAME"];
            LCA = (SicaSegura.SICA_LibroControl)Objeto_LC;
            InterfazLibroControl = new SicaSegura.SICA_Interfaz.SICA_LibroControl();
            string[] campos = LCA.RecuperarInfoRaacsInscripcion().Split('#');
            string[] titular = (new SicaSegura.SICA_Titular_Aprovechamiento()).GetListadoTitularesRaacs(LCA.ID_Inscripcion)[0].Split(new string[] { "$%" }, StringSplitOptions.None);
            string otros = "";
            if ((new SicaSegura.SICA_Titular_Aprovechamiento()).GetListadoTitularesRaacs(LCA.ID_Inscripcion).Length > 1)
            {
                otros = " (y otros)";
            }
            HTML_Título_Aprovechamiento.Text = "Inscripción: " + LCA.ID_Inscripcion.ToString() + " -- " + LCA.Agrupacion.Get_Descripcion() +  " <br>Titular: "  +  titular[1] + " " + titular [2] + otros ;
        }
        catch { Response.Redirect("../index.aspx"); }



        //
        //GENERACION DE LA INTERFAZ
        //

        
        string menu = "Cerrar sesion-borrarSesion.aspx#Información de usuario-index.aspx";
        HTML_Links_Sup_Izq.Text = InterfazLibroControl.Get_Enlaces_Sup_Izq(menu.Split('#'));
        HTML_Subcabecera_Logos.Text = InterfazLibroControl.Get_Cabecera();
        HTML_Menu_Navegacion.Text = InterfazLibroControl.Get_Menu_Navegacion("uno");
        HTML_Pie_Logo_Corporativo.Text = InterfazLibroControl.Get_pie_logo_corporativo();
        HTML_Pie_pagina.Text = InterfazLibroControl.Get_pie_pagina();
        
        HTML_Link_descarga_derechos.Text = "<li><a href=\"download.aspx?area=Raacs&File=" + LCA.Agrupacion.Get_NumeroInscripcion() + ".pdf\">Inscripcion Registro de Aguas (pdf)</a></li>";
        HTML_Link_Lecturas.Text = "<li><a href=\"lecturas.aspx\">Lecturas del sistema de medición</a></li>";
        HTML_Link_Gis.Text = "<li><a href=\"infgeografica.aspx\">Ubicación del Aprovechamiento</a></li>";




        #region Calculo de fechas para los computos
        if (!IsPostBack)

        {
            Populata_DDL_anyoHidrologico();
            if ((Request.QueryString["FI"] == "") || (Request.QueryString["FI"] == null))
            {   
                DDL_AnyoHidrologico.SelectedIndex = 0;
                int anno = Convert.ToInt16(DDL_AnyoHidrologico.SelectedValue.Split('-')[0].ToString().Trim());
                FI = DateTime.Parse("01/10/" + anno);
                FF = DateTime.Parse("01/10/" + Convert.ToString(anno+1));

            }
            else { 
                FF = DateTime.Parse(Request.QueryString["FF"]);
                FI = DateTime.Parse(Request.QueryString["FI"]);
            }


            if ((Request.QueryString["FF"] == "") || (Request.QueryString["FF"] == null))
            { FF = DateTime.Today;  }
            else { FF = DateTime.Parse(Request.QueryString["FF"]);  }
            calculos(LCA, DateTime.Parse("01/10/" + DDL_AnyoHidrologico.SelectedValue.Split('-')[0].Trim().ToString()).ToShortDateString(), DateTime.Parse("01/10/" + Convert.ToString(Convert.ToUInt16(DDL_AnyoHidrologico.SelectedValue.Split('-')[0].Trim().ToString()) + 1)).ToShortDateString());
            HTML_Iframe_Grafico_Consumo.Text = "<iframe src=\"grafico.aspx?FF=" + FF.ToShortDateString() + "&FI=" + FI.ToShortDateString() + "&suministrado=" + TXT_Consumido_vol.Text + "&idagrupacion=" + LCA.Agrupacion.Get_NumeroInscripcion() + "\" frameborder=\"0\" scrolling=\"no\" width=\"100%\" height\"700px\"></iframe>";
        }
        else 
        {

            FI = DateTime.Parse("01/10/" + DDL_AnyoHidrologico.SelectedValue.Split('-')[0].Trim().ToString());
            FF = FI.AddYears(1);
            FF = FF.AddDays(-1);
            calculos(LCA, DateTime.Parse("01/10/" + DDL_AnyoHidrologico.SelectedValue.Split('-')[0].Trim().ToString()).ToShortDateString(), DateTime.Parse("01/10/" + Convert.ToString(Convert.ToUInt16(DDL_AnyoHidrologico.SelectedValue.Split('-')[0].Trim().ToString()) + 1)).ToShortDateString());
            HTML_Iframe_Grafico_Consumo.Text = "<iframe src=\"grafico.aspx?FF=" + FF.ToShortDateString() + "&FI=" + FI.ToShortDateString() + "&suministrado=" + TXT_Consumido_vol.Text + "&idagrupacion=" + LCA.Agrupacion.Get_NumeroInscripcion() + "\" frameborder=\"0\" scrolling=\"no\" width=\"100%\" height\"500px\"></iframe>";
        }
        #endregion
        
    }

    private void Populata_DDL_anyoHidrologico()
    {
        if (DDL_AnyoHidrologico.Items.Count > 0)
        {
            DDL_AnyoHidrologico.Items.Clear(); 
        }
        if (DateTime.Now.Month > 9)
        {
            for (int i = 0; i < 10; i++)
            {
                DDL_AnyoHidrologico.Items.Add((DateTime.Now.Year - i).ToString() + " - " + (DateTime.Now.Year - i + 1).ToString());
            }
        }
        else
        {
            for (int i = 1; i < 11; i++)
            {
                DDL_AnyoHidrologico.Items.Add((DateTime.Now.Year - i).ToString() + " - " + (DateTime.Now.Year - i + 1).ToString());
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx?FI=01/10/" + DDL_AnyoHidrologico.SelectedValue.ToString() + "&FF=01/10/" + Convert.ToString(Convert.ToInt16(DDL_AnyoHidrologico.SelectedValue)+1) + "&Inscripcion=" + TXT_Inscripcion.Text);
    }

   
    private void calculos(SicaSegura.SICA_LibroControl LCA, string FI, string FF)
    {
        SicaSegura.SICA_Agrupacion agrupacion;
        agrupacion = LCA.Agrupacion;
        TXT_Concesion.Text = agrupacion.Get_ConcesionInscrita().ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR"));
        TXT_Concesion_temporal.Text = agrupacion.Get_ConcesionTemporal().ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR"));
        TXT_Concesion_total.Text = agrupacion.Get_ConcesionModificada().ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR"));

        TXT_Consumido_vol.Text = agrupacion.CalculoConsumido(agrupacion.Get_NumeroInscripcion(), DateTime.Parse(FI), DateTime.Parse(FF)).ToString();
        TXT_Consumido_vol.Text = Convert.ToDouble(TXT_Consumido_vol.Text).ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR"));

        if (DDL_AnyoHidrologico.SelectedIndex > 0) { HTML_Fechaultimalecturasuministrado.Text = " Año Completo"; }
        else { HTML_Fechaultimalecturasuministrado.Text = agrupacion.Get_FechaUltimaLecturaConsolidadAgrupacion().ToShortDateString(); }

        TXT_Consumido_percent.Text = System.Math.Round((Convert.ToDouble(TXT_Consumido_vol.Text) * 100) / agrupacion.Get_ConcesionModificada(),3).ToString();
        if (Convert.ToDouble(TXT_Consumido_percent.Text) < 100)
        {
            //TXT_Consumido_percent.BackColor = System.Drawing.Color.Green;
            //TXT_Consumido_vol.BackColor = System.Drawing.Color.Green;
        }
        else
        {
            //TXT_Consumido_percent.BackColor = System.Drawing.Color.Red;
            //TXT_Consumido_vol.BackColor = System.Drawing.Color.Red;
        }
    }


}
