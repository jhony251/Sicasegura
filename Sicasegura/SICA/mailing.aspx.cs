using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICA_mailing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ///HAY QUE CONTROLAR EL ACCESO DE LOS USUARIOS QUE NO SON SICA
        SicaSegura.SICA_Interfaz.SICA_Sica InterfazSica = new SicaSegura.SICA_Interfaz.SICA_Sica();

        String menu = "-#-";
        HTML_Links_Sup_Izq.Text = InterfazSica.Get_Enlaces_Sup_Izq(menu.Split('#'));
        HTML_Subcabecera_Logos.Text = InterfazSica.Get_Cabecera();
        HTML_Menu_Navegacion.Text = InterfazSica.Get_Menu_Navegacion_Completo("siete");
        //Cargalistado_ISM();
        HTML_Pie_Logo_Corporativo.Text = InterfazSica.Get_pie_logo_corporativo();
        HTML_Pie_pagina.Text = InterfazSica.Get_pie_pagina();
        NineRays.Reporting.Register.SetLicenseKey("4761628416024064132096");
        this.Cargarlistado_Mailing_ISM();

    }

    private void Cargarlistado_Mailing_ISM()
    {
        int i = 0;
        int u = 0;
        System.Data.DataTable dt = this.ObtenelListadoMailing();
        HTML_Tabla_listado_Mailing.Text += "<table>";
        HTML_Tabla_listado_Mailing.Text += "<thead>";
        HTML_Tabla_listado_Mailing.Text += "<th>Acciones</th>";
        
        for (u = 0; u < dt.Rows[0].Table.Columns.Count; u++)
        {

            HTML_Tabla_listado_Mailing.Text += "<th>";
            HTML_Tabla_listado_Mailing.Text += dt.Columns[u].ColumnName.ToString();
            HTML_Tabla_listado_Mailing.Text += "</th>";
        }

        HTML_Tabla_listado_Mailing.Text += "</tr>";
        HTML_Tabla_listado_Mailing.Text += "</thead>";
        HTML_Tabla_listado_Mailing.Text += "<tbody>";
        HTML_Tabla_listado_Mailing.Text += "<tr>";
        for (i = 0; i < dt.Rows.Count; i++)
        {
            HTML_Tabla_listado_Mailing.Text += "<td>";
            HTML_Tabla_listado_Mailing.Text += "<a href=\"\" target=\"_blank\"><img hspace=\"10\" alt=\"Editar\" src=\"./images/edit.png\" width=\"15\"></a>";
            HTML_Tabla_listado_Mailing.Text += "<a href=\"./mailing/eliminaSuscripciontitular.aspx?ID=" + dt.Rows[i][0].ToString() + "\" target=\"_blank\"><img hspace=\"10\" alt=\"borrar\" src=\"./images/borrar.png\" width=\"15\"></a>";
            HTML_Tabla_listado_Mailing.Text += "</td>";
            for (u = 0; u < dt.Rows[i].Table.Columns.Count; u++)
            {


                HTML_Tabla_listado_Mailing.Text += "<td>";
                HTML_Tabla_listado_Mailing.Text += dt.Rows[i][u].ToString();
                HTML_Tabla_listado_Mailing.Text += "</td>";

            }
            HTML_Tabla_listado_Mailing.Text += "</tr>";
        }
        HTML_Tabla_listado_Mailing.Text += "</table>";
        HTML_Tabla_listado_Mailing.Text += "</table>";
        
        

        //throw new NotImplementedException();
    }

    private System.Data.DataTable ObtenelListadoMailing()
    {
        string SQL = "SELECT * FROM SICA_Mailing_Direcciones";
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        return DB.GetDataDBSICA_test(SQL);
        //throw new NotImplementedException();
    }
    
}
