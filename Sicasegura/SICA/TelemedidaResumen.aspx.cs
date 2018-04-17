using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICAH_Telemedidaresumen_Default : System.Web.UI.Page
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
        this.Cargalistado_TELEMEDIDAS();


    }

    private DataTable ObtenerListadoTELEMEDIDAS()
    {
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        DataTable dt = new DataTable();
        dt = DB.GetDataDBSICA("SELECT TELEMEDIDA.Alegacion AS Origen,  TELEMEDIDA.CargandoDatos as Cargando , TELEMEDIDA.EnviandoDatos as Enviando, Count(TELEMEDIDA.Punto_control) AS Cantidad, TELEMEDIDA.UTMx as Tipo FROM SICA_Puntos_control TELEMEDIDA Where TELEMEDIDA.UTMx = 1 OR TELEMEDIDA.UTMx = 2 OR TELEMEDIDA.UTMx = 3 GROUP BY TELEMEDIDA.Alegacion,  TELEMEDIDA.CargandoDatos, TELEMEDIDA.EnviandoDatos, TELEMEDIDA.UTMx;");
        return dt;
    }


    private void Cargalistado_TELEMEDIDAS()
    {
        int i = 0;
        int u = 0;
        DataTable listaISM = ObtenerListadoTELEMEDIDAS();
        
        HTML_Tabla_listado_ISM.Text += "<table>";
        HTML_Tabla_listado_ISM.Text += "<thead>";
        HTML_Tabla_listado_ISM.Text += "<tr>";
        for (u = 0; u < listaISM.Rows[0].Table.Columns.Count; u++)
        {
            HTML_Tabla_listado_ISM.Text += "<th>";
            HTML_Tabla_listado_ISM.Text += listaISM.Columns[u].ColumnName.ToString();
            HTML_Tabla_listado_ISM.Text += "</th>";
        }
        
        HTML_Tabla_listado_ISM.Text += "</tr>";
        HTML_Tabla_listado_ISM.Text += "</thead>";
        HTML_Tabla_listado_ISM.Text += "<tbody>";
        HTML_Tabla_listado_ISM.Text += "<tr>";
        for (i = 0; i < listaISM.Rows.Count; i++)
        {

            for (u = 0; u < listaISM.Rows[i].Table.Columns.Count; u++)
            {
                
                HTML_Tabla_listado_ISM.Text += "<td>";
                if (u == 4)
                { 
                    if(listaISM.Rows[i][u].ToString()=="1"){HTML_Tabla_listado_ISM.Text +="Telemedida";}
                    if(listaISM.Rows[i][u].ToString()=="2"){HTML_Tabla_listado_ISM.Text +="Nivel";}
                    if(listaISM.Rows[i][u].ToString()=="3"){HTML_Tabla_listado_ISM.Text +="Caudalímetro";}
                }
                else
                {
                    if (u == listaISM.Rows[i].Table.Columns.Count - 1)
                    {
                        HTML_Tabla_listado_ISM.Text += listaISM.Rows[i][u].ToString();
                    }
                    else
                    {
                        HTML_Tabla_listado_ISM.Text += listaISM.Rows[i][u].ToString();
                    }
                }
                HTML_Tabla_listado_ISM.Text += "</td>";

            }
            HTML_Tabla_listado_ISM.Text += "</tr>";
        }
        HTML_Tabla_listado_ISM.Text += "</table>";
        HTML_Tabla_listado_ISM.Text += "</table>";
    }
}
