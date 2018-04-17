using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICAH_Telemedidalistado_Default : System.Web.UI.Page
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
        dt = DB.GetDataDBSICA("SELECT punto_control as [Código SICA],caracteristica1 as ISM,caracteristica3 as [Tipo Equipo],Fechaultimacarga as [Fec.Ultimo Dato], Alegacion as Titular, UTMX as [Tipo Mediciónn], UTMY as Istalado  FROM SICA_Puntos_control where UTMY=1 order by punto_control");
        return dt;
    }


    private void Cargalistado_TELEMEDIDAS()
    {
        int i = 0;
        int u = 0;
        DataTable listaISM = ObtenerListadoTELEMEDIDAS();
        HTML_Tabla_listado_ISM.Text += "<table>";
        HTML_Tabla_listado_ISM.Text += "<thead>";
        HTML_Tabla_listado_ISM.Text += "<tr><th>Estado</th>";
        for (u = 0; u < listaISM.Rows[0].Table.Columns.Count; u++)
        {
        
            HTML_Tabla_listado_ISM.Text += "<th>";
            HTML_Tabla_listado_ISM.Text += listaISM.Columns[u].ColumnName.ToString();
            HTML_Tabla_listado_ISM.Text += "</th>";
        }
        HTML_Tabla_listado_ISM.Text += "<th>";
        HTML_Tabla_listado_ISM.Text += "Cambiar Estado";
        HTML_Tabla_listado_ISM.Text += "</th>";
        
        HTML_Tabla_listado_ISM.Text += "</tr>";
        HTML_Tabla_listado_ISM.Text += "</thead>";
        HTML_Tabla_listado_ISM.Text += "<tbody>";
        HTML_Tabla_listado_ISM.Text += "<tr>";
        
        for (i = 0; i < listaISM.Rows.Count; i++)
        {

            string puntoColor = "";
            string dbColor = "<img hspace=\"10\" src=\"./Telemedida/images/dbrojo.png\" width=\"15\" >";
            string comunicaColor = "<img hspace=\"10\" src=\"./Telemedida/images/comunicarojo.png\" width=\"15\" >";
            string instaladoColor = "<img hspace=\"10\" src=\"./Telemedida/images/Instaladorojo.png\" width=\"15\" >";
            SicaSegura.SICA_EquipoTelemedida eq = new SicaSegura.SICA_EquipoTelemedida(listaISM.Rows[i][0].ToString());
            if (eq.get_cargadatos()) { dbColor = "<img hspace=\"10\" src=\"./Telemedida/images/dbverde.png\" width=\"15\" >"; }
            if (eq.get_enviadatos()) { comunicaColor = "<img hspace=\"10\" src=\"./Telemedida/images/comunicaverde.png\" width=\"15\" >"; }
            if (eq.get_instalado()) { instaladoColor = "<img hspace=\"10\" src=\"./Telemedida/images/Instaladoverde.png\" width=\"15\" >"; }
            if (eq.get_cargadatos() && eq.get_enviadatos())
            {
                puntoColor = "<img src=\"./Telemedida/images/circuloverde.png\" width=\"10\" >";
            }
            else if (!eq.get_cargadatos() && !eq.get_enviadatos())
            {
                puntoColor = "<img src=\"./Telemedida/images/circulorojo.png\" width=\"10\">";
            }
            else
            {
                puntoColor  = "<img src=\"./Telemedida/images/circuloamarillo.png\" width=\"10\">";
            }
            HTML_Tabla_listado_ISM.Text += "<td>" + puntoColor + dbColor + comunicaColor + instaladoColor + "</td>";
            for (u = 0; u < listaISM.Rows[i].Table.Columns.Count; u++)
            {
                
                
                HTML_Tabla_listado_ISM.Text += "<td>";
                HTML_Tabla_listado_ISM.Text += listaISM.Rows[i][u].ToString();
                HTML_Tabla_listado_ISM.Text += "</td>";

            }
            string PuntoSICA = listaISM.Rows[i][0].ToString();
            HTML_Tabla_listado_ISM.Text += "<td>";
            if (eq.get_enviadatos()) { HTML_Tabla_listado_ISM.Text += "<a  href='telemedida/quitardecargar.aspx?1=" + PuntoSICA + "'><img hspace=\"10\" src=\"./Telemedida/images/comunicarojo.png\" width=\"15\" ></a>"; }
            else { HTML_Tabla_listado_ISM.Text += "<img hspace=\"10\" src=\"./Telemedida/images/comunicaverde.png\" width=\"15\" >"; }

            if (eq.get_cargadatos()) { HTML_Tabla_listado_ISM.Text += "<a  href='telemedida/quitardecargar.aspx?1=" + PuntoSICA + "'><img hspace=\"10\" src=\"./Telemedida/images/dbrojo.png\" width=\"15\" ></a>"; }
            else { HTML_Tabla_listado_ISM.Text += "<a  href='telemedida/poneracargar.aspx?1=" + PuntoSICA + "'><img hspace=\"10\" src=\"./Telemedida/images/dbverde.png\" width=\"15\" ></a>"; }

            if (eq.get_instalado()) { HTML_Tabla_listado_ISM.Text += "<a  href='telemedida/EstablecerComoDesinstalado.aspx?1=" + PuntoSICA + "'><img hspace=\"10\" src=\"./Telemedida/images/instaladorojo.png\" width=\"15\" ></a>"; }
            else { HTML_Tabla_listado_ISM.Text += "<a  href='telemedida/EstablecerComoInsinstalado.aspx?1=" + PuntoSICA + "'><img hspace=\"10\" src=\"./Telemedida/images/instaladoverde.png\" width=\"15\" ></a>"; }
            HTML_Tabla_listado_ISM.Text += "</td>";
           

            HTML_Tabla_listado_ISM.Text += "</tr>";
        }
        HTML_Tabla_listado_ISM.Text += "</table>";
        HTML_Tabla_listado_ISM.Text += "</table>";
    }
}
