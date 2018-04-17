using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICA_index : System.Web.UI.Page
{
    string tipoAcceso = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try { tipoAcceso = Request.QueryString["tipo"].ToString(); }
        catch { tipoAcceso = "All"; }
        ///HAY QUE CONTROLAR EL ACCESO DE LOS USUARIOS QUE NO SON SICA
        SicaSegura.SICA_Interfaz.SICA_Sica InterfazSica  = new SicaSegura.SICA_Interfaz.SICA_Sica();

        String menu  = "-#-";
        HTML_Links_Sup_Izq.Text = InterfazSica.Get_Enlaces_Sup_Izq(menu.Split('#'));
        HTML_Subcabecera_Logos.Text = InterfazSica.Get_Cabecera();
        HTML_Menu_Navegacion.Text = InterfazSica.Get_Menu_Navegacion();
        Cargalistado_ISM();
        HTML_Pie_Logo_Corporativo.Text = InterfazSica.Get_pie_logo_corporativo();
        HTML_Pie_pagina.Text = InterfazSica.Get_pie_pagina();
        NineRays.Reporting.Register.SetLicenseKey("4761628416024064132096");
        
    }
    private void Cargalistado_ISM()
    {
        int i = 0;
        int u = 0;
        DataTable listaISM = ObtenerListadoISM();
        HTML_Tabla_listado_ISM.Text += "<table>";
        HTML_Tabla_listado_ISM.Text += "<thead>";
        HTML_Tabla_listado_ISM.Text += "<tr>";
        for (u = 0; u < listaISM.Rows[0].Table.Columns.Count; u++)
        {
            HTML_Tabla_listado_ISM.Text += "<th>";
            HTML_Tabla_listado_ISM.Text += listaISM.Columns[u].ColumnName.ToString();
            HTML_Tabla_listado_ISM.Text += "</th>";
        }
        HTML_Tabla_listado_ISM.Text += "<th></th>";
        HTML_Tabla_listado_ISM.Text += "</tr>";
        HTML_Tabla_listado_ISM.Text += "</thead>";
        HTML_Tabla_listado_ISM.Text += "<tbody>";
        HTML_Tabla_listado_ISM.Text += "<tr>";
        for (i=0;i<listaISM.Rows.Count;i++)
        {
            
            for (u = 0; u < listaISM.Rows[i].Table.Columns.Count; u++)
            {
                HTML_Tabla_listado_ISM.Text += "<td>";
                if (u == listaISM.Rows[i].Table.Columns.Count - 1)
                {
                    if (listaISM.Rows[i][u].ToString().Length > 10)
                    {
                        HTML_Tabla_listado_ISM.Text += "<span title=\"" + listaISM.Rows[i][u].ToString() + "\">" + listaISM.Rows[i][u].ToString().Substring(0, 10) + "...</span>";
                    }
                    else { HTML_Tabla_listado_ISM.Text += listaISM.Rows[i][u].ToString(); }
                }
                else
                {
                    HTML_Tabla_listado_ISM.Text += listaISM.Rows[i][u];
                }
                HTML_Tabla_listado_ISM.Text += "</td>";
                
            }
            
                if (ExisteAgrupacionSICA(listaISM.Rows[i][1].ToString()))
                {
                    HTML_Tabla_listado_ISM.Text += "<td><a href=\"ISM.aspx?IDRaacs=" + listaISM.Rows[i][1].ToString() + "&IDISM=" + listaISM.Rows[i][0].ToString() + "\"><img border=\"0\" src=\"images/iconFlecha.png\" height=\"20px\"></a></td>";
                    HTML_Tabla_listado_ISM.Text += "</tr>";
                }
                else
                { 
                    HTML_Tabla_listado_ISM.Text += "<td></td></tr>"; 
                }
        }
        HTML_Tabla_listado_ISM.Text += "</table>";
        HTML_Tabla_listado_ISM.Text += "</table>";
    }
    private DataTable ObtenerListadoISM()
    {
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        DataTable dt = new DataTable();
        if (tipoAcceso == "OE")
        {
            dt = DB.GetDataDBSICA("SELECT EXP_ISM, INSCRIPCION, VOLUMEN, CATEGORIA,  SICA, TERMINO_MUNICIPAL, TIPO_APROVECHAMIENTO as [TIPO APROV], ESTADO_EXP_ISM, REGISTRO_DATOS_TELEMEDIDA as TELEMEDIDA, TITULAR FROM SICA_Seguimiento_Expedientes_ISM Where Observaciones like'%OOEE%'");
        }
        else 
        {
            dt = DB.GetDataDBSICA("SELECT EXP_ISM, INSCRIPCION, VOLUMEN, CATEGORIA,  SICA, TERMINO_MUNICIPAL, TIPO_APROVECHAMIENTO as [TIPO APROV], ESTADO_EXP_ISM, REGISTRO_DATOS_TELEMEDIDA as TELEMEDIDA, TITULAR FROM SICA_Seguimiento_Expedientes_ISM");
        }
        return dt;
    }
    private bool ExisteAgrupacionSICA(string id)
    {
        string sentencia = "";
        bool respuesta = false;
        if (id != "")
        {
            sentencia = "SELECT ID FROM SICA_SIST_Sistemas WHERE [SICA_SIST_Sistemas].NumInscripcion=" + id + "";
            SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
            DataTable dt = DB.GetDataDBSICA(sentencia);
            if (dt.Rows.Count > 0) { respuesta = true; }
            else { respuesta = false; }
        }
        else
        { respuesta = false; }
        return respuesta;
    }
}
