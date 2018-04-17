using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICA_Telemedida_CambiarEstado : System.Web.UI.Page
{
    SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.DataTable dt = null;
        ///
        ///Para Cambiar el estado de un Telemedida hay que trabajar en estas 3 tablas.
        ///DBSICA.Sica_Puntos_control
        ///DBSICA.SICA_Equivalencia_nombreEquipo_PVYCR
        ///DBSICA.SICA_Equipos_ARM
        ///
        
        /// 
        /// Estado 0 = No recibe No carga
        /// Estado 1 = Si recibe No Carga
        /// Estado 2 = Si recibe Si Carga
        /// 
        string estado ="";
        string codigosica = "";
        string EMs = "";
        string html = "";
        
            try
            {
                codigosica = Request.QueryString[0].ToString();
                try
                {
                    EMs = Request.QueryString[1].ToString();
                    try
                    {
                        estado = Request.QueryString[2].ToString();
                        if ((codigosica != "") && (EMs != "") &&  (estado != ""))
                        {  
                            SicaSegura.SICA_EquipoTelemedida equipo = new SicaSegura.SICA_EquipoTelemedida(codigosica);
                            if (estado == "0")
                            {
                                equipo.set_cargadatos("NO");
                                equipo.set_enviadatos("NO");
                            }
                            if (estado == "1")
                            {
                                equipo.set_cargadatos("NO");
                                equipo.set_enviadatos("SI");
                            }
                            if (estado == "2")
                            {
                                equipo.set_cargadatos("SI");
                                equipo.set_enviadatos("SI");
                            }
                            if (existeCodigoSicayEMEnTabla_Equipos_ARM(codigosica, EMs))
                            {
                                
                            }
                            Response.Redirect("../telemedidalistado.aspx");
                        }
                        else
                        {
                            
                        }
                    }
                    catch
                    {
                        if (estado == "")
                        {
                            html += "Por favor Selecciona el motivo por<br>el que desea dejar de cargar este punto<br><br>";
                            html += "<a href='quitardecargar.aspx?0=" + codigosica + "&1=" + EMs + "&2=0'>El Elemento  " + EMs + " No llama</a><br>";
                            html += "<a href='quitardecargar.aspx?0=" + codigosica + "&1=" + EMs + "&2=1'>El Elemento  " + EMs + " Indice de contador erróneo</a><br>";
                        }   
                    }
                }
                catch
                {
                    html += "Por favor Selecciona el Elemeto de medida<br>que desea poner a cargar<br><br>";
                    dt = this.ObtenerEMparaCodigoSICA(codigosica);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        html += "<a href='quitardecargar.aspx?0=" + codigosica + "&1=" + dt.Rows[i][0].ToString() + "'>" + dt.Rows[i][0].ToString() + "</a><br>";
                    }
                }


            }
            catch
            {
                html += "Se ha producido un error";
            }
        

        LIT_HTML.Text = html;

        
        

        
        
        

    }
    #region Sica_Puntos_Control
    private bool existeCodigoSicaEnTabla_Puntos_control(string CodigoSica)
    {
        bool resultado = false;
        string SQL = "SELECT * FROM Sica_Puntos_Control WHERE codigoPVYCR like '" + CodigoSica.Trim() + "%'";
        System.Data.DataTable dt = DB.GetDataDBSICA(SQL);
        if (dt.Rows.Count > 0)
        { resultado = true; }
        return resultado;
    }
    private System.Data.DataTable ObtenerEMparaCodigoSICA(string CodigoSICA)
    {
        System.Data.DataTable resultado= null;
        string SQL = "SELECT idelementomedida FROM PVYCR_ElementosMedida WHERE codigoPVYCR like '" + CodigoSICA.Trim() + "%'";
        System.Data.DataTable dt = DB.GetDataSIGVECTOR(SQL);
        if (dt.Rows.Count > 0)
        { resultado = dt; }
        return resultado;

    }
    #endregion

    #region SICA_Equivalencia_nombreEquipo_PVYCR
    private bool existeCodigoSicaEnTabla_Equivalencias(string CodigoSica)
    {
        bool resultado = false;
        string SQL = "SELECT * FROM Sica_equivalencia_nombreEquipo_PVYCR WHERE codigoPVYCR like '" + CodigoSica.Trim() + "%'";
        System.Data.DataTable dt = DB.GetDataDBSICA(SQL);
        if (dt.Rows.Count>0)
        { resultado = true; }
        return resultado;
    }
    private void InsertaCodigoSicaEnTabla_Equivalencias(string CodigoSica)
    {
        string SQL = "INSERT INTO SICA_Equivalencia_nombreEquipo_PVYCR(Nombre_equipo, CodigoPvycr) VALUES ('" + CodigoSica.Trim() + "','" + CodigoSica.Trim() + "')";
        System.Data.DataTable dt = DB.GetDataDBSICA(SQL);
    }
    #endregion

    #region SICA_Equipos_ARM
    private bool existeCodigoSicayEMEnTabla_Equipos_ARM(string CodigoSica, string EM)
    {
        bool resultado = false;     
        string SQL = "SELECT * FROM SICA_Equipos_ARM WHERE EM like '" + EM.Trim() + "' AND codigoPVYCR like '" + CodigoSica.Trim() + "%'";
        System.Data.DataTable dt = DB.GetDataDBSICA(SQL);
        if (dt.Rows.Count > 0)
        { resultado = true; }
        return resultado;
    }
    private void insertaCodigoSicayEMEnTabla_Equipos_ARM(string CodigoSica, string EM, string tipoequipo)
    {

        string SQL = "INSERT INTO SICA_Equipos_ARM(CodigoPVYCR, EM, Tipo_equipo) VALUES ('" + CodigoSica.Trim() + "','" + EM.Trim() + "','" + tipoequipo + "')";
        System.Data.DataTable dt = DB.GetDataDBSICA(SQL);
    }
    #endregion


}
