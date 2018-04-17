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
        ///Para Cambiar el estado de un Telemedida hay que trabajar en estas 3 tablas.
        ///DBSICA.Sica_Puntos_control
        ///DBSICA.SICA_Equivalencia_nombreEquipo_PVYCR
        ///DBSICA.SICA_Equipos_ARM
        
        /// Estado 0 = No recibe No carga
        /// Estado 1 = Si recibe No Carga
        /// Estado 2 = Si recibe Si Carga
        string estado = Request.QueryString[0].ToString();
        string codigosica = Request.QueryString[1].ToString();
        string EMs = Request.QueryString[2].ToString();
        if (existeCodigoSicaEnTabla_Equivalencias(codigosica) == false)
        {
            InsertaCodigoSicaEnTabla_Equivalencias(codigosica);
        }
        if (existeCodigoSicayEMEnTabla_Equipos_ARM(codigosica, EMs) == false)
        {
            SicaSegura.SICA_EquipoTelemedida equipo = new SicaSegura.SICA_EquipoTelemedida(codigosica);
            string codigo_modelo_equipo ="";
            if (equipo.get_modeloequipo().Trim() =="MASERMIC")
            { codigo_modelo_equipo = "9997"; }
            else { codigo_modelo_equipo = "9998"; }
            insertaCodigoSicayEMEnTabla_Equipos_ARM(codigosica, EMs,codigo_modelo_equipo );
        }
        if (existeCodigoSicaEnTabla_Equivalencias(codigosica) && existeCodigoSicayEMEnTabla_Equipos_ARM(codigosica, EMs))
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
        }
        
        

    }
    #region Sica_Puntos_Control
    private bool existeCodigoSicaEnTabla_Puntos_control(string CodigoSica)
    {
        bool resultado = false;
        string SQL = "SELECT * FROM Sica_Puntos_Control WHERE codigoPVYR like '" + CodigoSica.Trim() + "%'";
        System.Data.DataTable dt = DB.GetDataDBSICA(SQL);
        if (dt.Rows.Count > 0)
        { resultado = true; }
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
