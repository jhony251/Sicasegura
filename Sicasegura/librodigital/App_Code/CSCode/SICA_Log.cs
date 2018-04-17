using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace SicaSegura
{
    /// <summary>
    /// Clase cuya función es la de registrar los movimientos realizados en la Web.
    /// </summary>
    public class SICA_Log
    {
        private SqlConnection conn;
        public SICA_Log()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSICA"].ConnectionString.ToString());
        }
        public void acceso_pagina()
        {

        }
        public void acceso_Cauces_y_puntos()
        {

        }
        #region Mantenimientos

        public void acceso_Mantenimientos()
        {

        }

        #region Mantenimiento Cauces
        public void acceso_Mantenimientos_cauces()
        {

        }
        public void Alta_Mantenimientos_cauces()
        {

        }
        public void Modificacion_Mantenimientos_cauces()
        {

        }
        #endregion

        #region Mantenimiento Punto
        public void acceso_Mantenimientos_puntos()
        {

        }
        public void Alta_Mantenimientos_puntos()
        {

        }
        public void Modificacion_Mantenimientos_puntos()
        {

        }
        #endregion

        #region Mantenimiento Elemento de Medida
        public void acceso_Mantenimientos_ElementoMedida()
        {

        }
        public void Alta_Mantenimientos_ElementoMedida()
        {

        }
        public void Modificacion_Mantenimientos_ElementoMedida()
        {

        }
        public void Borrado_Mantenimientos_ElementoMedida()
        {

        }
        #endregion

        #region Mantenimiento Curvas
        public void Acceso_Mantenimientos_curvas()
        {

        }
        public void Alta_Mantenimientos_curvas()
        {

        }
        public void Modificacion_Mantenimientos_curvas()
        {

        }
        public void Borrado_Mantenimientos_curvas()
        {

        }

        #endregion

        #region Mantenimiento Boletines
        public void acceso_Mantenimientos_boletines()
        {

        }
        public void Alta_Mantenimientos_boletines()
        {

        }
        public void Modificacion_Mantenimientos_boletines()
        {

        }
        public void Borrado_Mantenimientos_boletines()
        {

        }
        #endregion

        #region Mantenimiento Seguimiento
        public void acceso_Mantenimientos_Seguimiento()
        {

        }
        public void Alta_Mantenimientos_Seguimiento()
        {

        }
        public void Modificacion_Mantenimientos_Seguimiento()
        {

        }
        public void Borrado_Mantenimientos_Seguimiento()
        {

        }
        #endregion

        #endregion

        #region SCV
        public void Acceso_SCV(string url)
        {
            string usuario = "";
            SicaSegura.SICA_DB Sica_DB = new SICA_DB();
            Page page = HttpContext.Current.Handler as Page;
            string IP = page.Request.ServerVariables["X_FORWARDED_FOR"];
            if (IP == null)
            {
                IP = page.Request.ServerVariables["REMOTE_ADDR"];
            }
            string Weburl = HttpContext.Current.Request.Url.AbsoluteUri;
            try { usuario = page.Session["loginUsuario"].ToString(); }
            catch { usuario = "Notauthorized"; }
            //id, usuario, fecha_evento, espagina, esinforme, concepto, idConcepto, url, terminalPC, IP, App
            String SQL = "INSERT INTO SICA_log_actividad" +
                "(usuario, fecha_evento, espagina, esinforme, concepto, idConcepto, url, terminalPC, IP, App)" +
                "VALUES('" + usuario + "','" + DateTime.Now.ToString() + "',1,0,'SCV','Acceso','" + Weburl + "','PC','" + IP + "','SICASEGURA')";
            Sica_DB.GetDataDBSICA(SQL);

        }
        public void Descarga_SCV_Alberca_informe()
        {
        }

        public void Acceso_SCV_RAACS()
        {
        }
        public void Descarga_SCV_RAACS_informe()
        {
        }

        public void Acceso_SCV_SICA()
        {
        }
        public void Descarga_SCV_SICA_informe(string concepto)
        {
            string usuario = "";
            SicaSegura.SICA_DB Sica_DB = new SICA_DB();
            Page page = HttpContext.Current.Handler as Page;
            string IP = page.Request.ServerVariables["X_FORWARDED_FOR"];
            if (IP == null)
            {
                IP = page.Request.ServerVariables["REMOTE_ADDR"];
            }
            string Weburl = HttpContext.Current.Request.Url.AbsoluteUri;
            try { usuario = page.Session["loginUsuario"].ToString(); }
            catch { usuario = "Notauthorized"; }
            //id, usuario, fecha_evento, espagina, esinforme, concepto, idConcepto, url, terminalPC, IP, App
            String SQL = "INSERT INTO SICA_log_actividad" +
                "(usuario, fecha_evento, espagina, esinforme, concepto, idConcepto, url, terminalPC, IP, App)" +
                "VALUES('" + usuario + "','" + DateTime.Now.ToString() + "',0,1,'SCV-SICA','" + concepto + "','" + Weburl + "','PC','" + IP + "','SICASEGURA')";
            Sica_DB.GetDataDBSICA(SQL.Replace(';', ' '));
        }
        #endregion
    }
    public class SICA_DB
    {
        protected void Alert(string msg)
        {
            Page page = HttpContext.Current.Handler as Page;
            string script = "alert(\"" + msg + "\");";
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true);
        }
        public DataTable GetDataSIGVECTOR(string SQL)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["PDASIGVECTOR"].ConnectionString;
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cadenaConexion);
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(SQL, connection);
            try
            {
                adapter.Fill(dt);

            }
            catch (Exception ee)
            {
                Alert(ee.Message + "--" + SQL);
            }
            return dt;
        }
        public DataTable GetDataDBSICA(string SQL)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["DBSICA"].ConnectionString;
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cadenaConexion);
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(SQL, connection);
            try
            {
                adapter.Fill(dt);

            }
            catch (Exception ee) { Alert(ee.Message + "--" + SQL); }
            return dt;
        }
        public DataTable GetDataRAACS(string SQL)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["RAACS"].ConnectionString;
            DataTable dt = new DataTable();
            try
            {

                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cadenaConexion);
                System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ee) { Alert(ee.Message + "--" + SQL); }
            return dt;
        }

        public DataSet GetDataSIGVECTOR_DS(string SQL)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["PDASIGVECTOR"].ConnectionString;
            DataSet dt = new DataSet();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cadenaConexion);
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(SQL, connection);
            try
            {
                adapter.Fill(dt);

            }
            catch (Exception ee)
            {
                Alert(ee.Message + "--" + SQL);
            }
            return dt;
        }
        public DataSet GetDataDBSICA_DS(string SQL)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["DBSICA"].ConnectionString;
            DataSet dt = new DataSet();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cadenaConexion);
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(SQL, connection);
            try
            {
                adapter.Fill(dt);

            }
            catch (Exception ee) { Alert(ee.Message + "--" + SQL); }
            return dt;
        }
        public DataSet GetDataRAACS_DS(string SQL)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["RAACS"].ConnectionString;
            DataSet dt = new DataSet();
            try
            {

                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cadenaConexion);
                System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ee) { Alert(ee.Message + "--" + SQL); }
            return dt;
        }
    }
    public class SICA_LibroControl
    {
        #region VARS
        private string login;
        private int _ID_usuario;
        private int _ID_Inscripcion;
        private bool _Acceso_Validado;
        private SICA_Agrupacion _Agrupacion;
        private string _InfoRaacs_splited;
        #endregion

        public int ID_Inscripcion { get { return _ID_Inscripcion; } set { _ID_Inscripcion = value; } }
        public int ID_usuario { get { return _ID_usuario; } set { _ID_usuario = value; } }
        public bool Acceso_validado { get { return _Acceso_Validado; } set { _Acceso_Validado = value; } }
        public SICA_Agrupacion Agrupacion { get { return _Agrupacion; } }
        /// <summary>
        /// Constructor que en base a los parámetros de entrada valida un usuario y crea el objeto LCA que 
        /// contendrá toda la información aquí listada:
        ///     ·Objeto Agrupación
        ///     ·Objeto InfoRaacs
        /// </summary>
        /// <param name="usuario">Usuario a validar</param>
        /// <param name="pass">Pass a validar</param>
        public SICA_LibroControl(string usuario, string pass)
        {
            login = usuario;

            if (validar_acceso(pass))
            {
                _ID_Inscripcion = Obtener_ID_Inscripcion();
                _Agrupacion = new SICA_Agrupacion(_ID_Inscripcion);
            }
        }
        /// <summary>
        /// Funcion para validar el usuario y contraseña que ha sido introducido en la 
        /// página de Log-in
        /// </summary>
        /// <param name="pass">Parametro que contiene la contraseña del usuario</param>
        /// <returns>Un valor Booleano</returns>
        private bool validar_acceso(string pass)
        {
            SICA_DB DB_LibroControl = new SICA_DB();
            DataTable dt = DB_LibroControl.GetDataDBSICA("SELECT id_usuario FROM sica_sist_usuario WHERE login='" + login + "' and password='" + pass + "'");
            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// Obtencion del ID de la inscripcion en el Raacs. 
        /// </summary>
        /// <returns>Devuelve un valor entero que corresponde con el Nº de inscripción</returns>
        private int Obtener_ID_Inscripcion()
        {
            SICA_DB DB_LibroControl = new SICA_DB();
            DataTable dt = DB_LibroControl.GetDataDBSICA("SELECT ID_Usuario FROM sica_sist_Usuario WHERE Login like '" + login + "'");
            ID_usuario = Convert.ToInt16(dt.Rows[0].ItemArray.GetValue(0).ToString());
            dt = DB_LibroControl.GetDataDBSICA("SELECT     ID_Sistema FROM         [SICA_SIST_Sistemas-Usuarios] WHERE     (ID_Usuario = " + ID_usuario + ")");
            if (dt.Rows.Count > 0)
            {
                int _ID_Sistema = Convert.ToInt16(dt.Rows[0].ItemArray.GetValue(0).ToString());
                dt = DB_LibroControl.GetDataDBSICA("SELECT NumInscripcion FROM sica_sist_sistemas WHERE ID=" + _ID_Sistema + "");
                return Convert.ToInt16(dt.Rows[0].ItemArray.GetValue(0).ToString());
            }
            else { return 0; }
        }

        public string RecuperarInfoRaacsInscripcion()
        {
            _InfoRaacs_splited = "";
            SICA_DB DB = new SICA_DB();
            DataTable dt = DB.GetDataRAACS("SELECT * FROM AprovechamientosInscritos WHERE NUMPAL=" + _ID_Inscripcion);
            for (int i = 0; i < dt.Rows[0].ItemArray.Length; i++)
            {
                _InfoRaacs_splited += dt.Rows[0].ItemArray.GetValue(i).ToString() + "#.";
            }
            return _InfoRaacs_splited;
        }
    }
    public class SICA_Agrupacion
    {
        #region VARS
        private int NumInscripcion, IdAgrupacion;
        private double Concesion_inscrita;
        private double Concesion_modificada;
        private double Modificacion_concesion;
        private double Consumido;
        private string[] PuntosControl;
        private string Descripcion;
        private bool Raacs;

        private System.Data.SqlClient.SqlConnection conexion = new System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings["dsnsegura_migracion"]);
        #endregion


        public int Get_NumeroInscripcion() { return NumInscripcion; }
        public int Get_IDAgrupacion() { return IdAgrupacion; }
        public double Get_ConcesionInscrita() { return Concesion_inscrita; }
        public double Get_ConcesionModificada() { return Concesion_modificada; }
        public double Get_ConcesionTemporal() { return Modificacion_concesion; }
        public double Get_Consumido() { return Consumido; }
        public string[] Get_PuntosDeContros() { return PuntosControl; }
        public string Get_Descripcion() { return Descripcion; }
        public bool EsDelRaacs() { return Raacs; }


        public SICA_Agrupacion(string descripcion)
        {
            //NumInscripcion = inscripcion;
            GetIdAgrupacion(descripcion);
            //Concesion_inscrita = ObtenerVolumenInscripcion(NumInscripcion);
            //Modificacion_concesion = ObtenerVolumenModificacionInscripcion(NumInscripcion);
            //Concesion_modificada = Concesion_inscrita + Modificacion_concesion;
            Consumido = CalculoConsumido();
            PuntosControl = ObtenerListadoPuntos();
        }
        public SICA_Agrupacion(int inscripcion)
        {
            NumInscripcion = inscripcion;
            GetIdAgrupacion(inscripcion);
            Concesion_inscrita = ObtenerVolumenInscripcion(NumInscripcion);
            Modificacion_concesion = ObtenerVolumenModificacionInscripcion(NumInscripcion);
            Concesion_modificada = Concesion_inscrita + Modificacion_concesion;
            Consumido = CalculoConsumido();
            PuntosControl = ObtenerListadoPuntos();
            Descripcion = ObtenerDescripcion(NumInscripcion);

        }
        private string ObtenerDescripcion(int NumInscripcion)
        {
            SicaSegura.SICA_DB DB = new SICA_DB();
            DataTable dt = DB.GetDataDBSICA("SELECT Descripcion FROM SICA_SIST_Sistemas WHERE [SICA_SIST_Sistemas].NumInscripcion=" + NumInscripcion + "");
            return dt.Rows[0].ItemArray.GetValue(0).ToString();
        }
        private void GetIdAgrupacion(string descripcion)
        {
            string sentencia = "";
            sentencia = "SELECT ID FROM SICA_SIST_Sistemas WHERE [SICA_SIST_Sistemas].Nombre like '" + descripcion + "'";
            SicaSegura.SICA_DB DB = new SICA_DB();
            DataTable dt = DB.GetDataDBSICA(sentencia);
            IdAgrupacion = Convert.ToInt16(dt.Rows[0][0]);

        }
        private void GetIdAgrupacion(int NumInscripcion)
        {
            string sentencia = "";
            sentencia = "SELECT ID FROM SICA_SIST_Sistemas WHERE [SICA_SIST_Sistemas].NumInscripcion=" + NumInscripcion + "";
            SicaSegura.SICA_DB DB = new SICA_DB();
            DataTable dt = DB.GetDataDBSICA(sentencia);
            IdAgrupacion = Convert.ToInt16(dt.Rows[0][0]);

        }
        private double ObtenerVolumenInscripcion(int NumInscripcion)
        {
            double VolumenTotal = 0;
            DataTable dtRegistro = new System.Data.DataTable();
            SicaSegura.SICA_DB DB = new SICA_DB();
            try
            {
                //Si la Agrupación es RAAC los datos, se cogen de la inscripción
                //'Es la suma total de los volúmenes de abastecimiento, riego...
                //'utiles.Comprobar_Conexion_BD(Page, conexionRegistro)
                //'daRegistro.SelectCommand.CommandText = "Select * from AprovechamientosInscritos WHERE NUMPAL=" & NumInscripcion
                //'daRegistro.Fill(dstRegistro, "TablaRegistro")
                dtRegistro = DB.GetDataRAACS("Select * from AprovechamientosInscritos WHERE NUMPAL=" + NumInscripcion);
                if (dtRegistro.Rows.Count > 0)
                {
                    VolumenTotal += Convert.ToDouble(dtRegistro.Rows[0]["VOLRE"]) + Convert.ToDouble(dtRegistro.Rows[0]["VOLAB"]) + Convert.ToDouble(dtRegistro.Rows[0]["VOLOT"]);
                }
                return VolumenTotal;
                //Return String.Format("{0:#,##0.##}", VolumenTotal)
            }

            catch (Exception ee)
            {
                return 0;
            }
        }
        private double ObtenerVolumenModificacionInscripcion(int NumInscripcion)
        {
            double Volumen = 0;
            DataTable dtRegistro = new System.Data.DataTable();
            SicaSegura.SICA_DB DB = new SICA_DB();
            try
            {
                dtRegistro = DB.GetDataDBSICA("Select TOP(1) volumen from SICA_SIST_ModificaCon WHERE NumInscripcion=" + NumInscripcion + " order by year");
                if (dtRegistro.Rows.Count > 0)
                {
                    Volumen += Convert.ToDouble(dtRegistro.Rows[0][0]);
                }
                return Volumen;
            }

            catch (Exception ee)
            {
                return 0;
            }
        }
        public double CalculoConsumido(int NumInscripcion, DateTime FI, DateTime FF)
        {
            string sentencia = "";
            sentencia = "SELECT     [SICA_SIST_Sistemas-Punto].ID_Sistema, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM, SICA_SIST_Sistemas.NumInscripcion " +
                 " FROM         SICA_SIST_PuntoSistema INNER JOIN [SICA_SIST_Sistemas-Punto] ON SICA_SIST_PuntoSistema.ID = [SICA_SIST_Sistemas-Punto].ID_PuntoSistema" +
                 " INNER JOIN     SICA_SIST_Sistemas ON    SICA_SIST_Sistemas.ID=[SICA_SIST_Sistemas-Punto].ID_Sistema" +
                 " WHERE [SICA_SIST_Sistemas-Punto].ID_Sistema='" + IdAgrupacion + "'";
            DataSet dstPuntos = new DataSet();
            SicaSegura.SICA_DB DB = new SICA_DB();
            dstPuntos = DB.GetDataDBSICA_DS(sentencia);
            Sicadll.AccesoADatos.AccesoADatos2 punto = new Sicadll.AccesoADatos.AccesoADatos2();
            double volumenGeneral = 0;
            double concesionAprovechamiento = 0;
            double PorcentajeTotal = 0;
            if (dstPuntos.Tables[0].Rows.Count > 0)
            {
                //'Se comprueba las fechas
                foreach (DataRow fila in dstPuntos.Tables[0].Rows)
                {
                    object[] resultado;
                    //Se obtienen los resultados de cada uno de los elementos de medida que
                    //componen el punto
                    if (fila["codigoPVYCR"].ToString().Trim() == "" || fila["EM"].ToString().Trim() == "")
                    {
                    }
                    else
                    {
                        resultado = punto.obtenerAcumulado(fila["codigoPVYCR"].ToString().Trim(), fila["EM"].ToString().Trim(), conexion, FI.ToShortDateString(), FF.ToShortDateString());
                        //'Se calculan los acumulados
                        volumenGeneral += Convert.ToDouble(resultado[1].ToString()); ;
                        if (fila["NumInscripcion"].ToString() == "")
                        {
                            concesionAprovechamiento += Convert.ToDouble(resultado[2].ToString());
                        }

                        PorcentajeTotal += Convert.ToDouble(resultado[3].ToString());
                    }
                }
            }
            else
            {
            }
            return volumenGeneral;
        }
        private double CalculoConsumido()
        {
            DateTime FI, FF;

            FF = DateTime.Now;
            if (FF.Month < 10) { FI = DateTime.Parse("01/10/" + FF.AddYears(-1).Year.ToString()); }
            else { FI = DateTime.Parse("01/10/" + FF.Year.ToString()); }

            string sentencia = "";
            sentencia = "SELECT     [SICA_SIST_Sistemas-Punto].ID_Sistema, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM, SICA_SIST_Sistemas.NumInscripcion " +
                 " FROM         SICA_SIST_PuntoSistema INNER JOIN [SICA_SIST_Sistemas-Punto] ON SICA_SIST_PuntoSistema.ID = [SICA_SIST_Sistemas-Punto].ID_PuntoSistema" +
                 " INNER JOIN     SICA_SIST_Sistemas ON    SICA_SIST_Sistemas.ID=[SICA_SIST_Sistemas-Punto].ID_Sistema" +
                 " WHERE [SICA_SIST_Sistemas-Punto].ID_Sistema='" + IdAgrupacion + "'";
            DataSet dstPuntos = new DataSet();
            SicaSegura.SICA_DB DB = new SICA_DB();
            dstPuntos = DB.GetDataDBSICA_DS(sentencia);
            Sicadll.AccesoADatos.AccesoADatos2 punto = new Sicadll.AccesoADatos.AccesoADatos2();
            double volumenGeneral = 0;
            double concesionAprovechamiento = 0;
            double PorcentajeTotal = 0;
            if (dstPuntos.Tables[0].Rows.Count > 0)
            {
                //'Se comprueba las fechas
                foreach (DataRow fila in dstPuntos.Tables[0].Rows)
                {
                    object[] resultado;
                    //Se obtienen los resultados de cada uno de los elementos de medida que
                    //componen el punto
                    if (fila["codigoPVYCR"].ToString().Trim() == "" || fila["EM"].ToString().Trim() == "")
                    {
                    }
                    else
                    {

                        resultado = punto.obtenerAcumulado(fila["codigoPVYCR"].ToString().Trim(), fila["EM"].ToString().Trim(), conexion, FI.ToShortDateString(), FF.ToShortDateString());
                        //'Se calculan los acumulados
                        volumenGeneral += Convert.ToDouble(resultado[1].ToString()); ;
                        if (fila["NumInscripcion"].ToString() == "")
                        {
                            concesionAprovechamiento += Convert.ToDouble(resultado[2].ToString());
                        }

                        PorcentajeTotal += Convert.ToDouble(resultado[3].ToString());
                    }
                }
            }
            else
            {
            }
            return volumenGeneral;
        }
        private string[] ObtenerListadoPuntos()
        {
            SicaSegura.SICA_DB DB = new SICA_DB();
            string sentencia = "SELECT     [SICA_SIST_Sistemas-Punto].ID_Sistema, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM, SICA_SIST_Sistemas.NumInscripcion " +
                 " FROM         SICA_SIST_PuntoSistema INNER JOIN [SICA_SIST_Sistemas-Punto] ON SICA_SIST_PuntoSistema.ID = [SICA_SIST_Sistemas-Punto].ID_PuntoSistema" +
                 " INNER JOIN     SICA_SIST_Sistemas ON    SICA_SIST_Sistemas.ID=[SICA_SIST_Sistemas-Punto].ID_Sistema" +
                 " WHERE [SICA_SIST_Sistemas-Punto].ID_Sistema='" + IdAgrupacion + "'";
            DataTable dt = DB.GetDataDBSICA(sentencia);
            string puntos = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                puntos += dt.Rows[i][1].ToString() + "-" + dt.Rows[i][2].ToString() + "#";
            }
            puntos = puntos.Substring(0, puntos.Length - 1);
            return puntos.Split('#');
        }
    }
    public class SICA_PuntoControl
    {

        #region VARS
        private string codigoPVYCR;
        private string[] ElementosDeMedida;
        class Lectura
        {
            private DateTime fecha_medida;
            private double valor;
            public Lectura(DateTime fecha, double lectura)
            {
                fecha_medida = fecha;
                valor = lectura;
            }
            public DateTime Get_Fecha_Medida()
            {
                return fecha_medida;
            }
            public double Get_valor()
            {
                return valor;
            }

        }
        #endregion
        public SICA_PuntoControl(string Pvycr)
        {
            codigoPVYCR = Pvycr;

        }
        private void ObtenerEM()
        {
            string SQL = "SELECT idelementoMedida from PVYCR_ElementosMedida where codigopvycr like '" + codigoPVYCR + "'";
            string EMs = "";
            SicaSegura.SICA_DB DB = new SICA_DB();
            DataTable dt = DB.GetDataSIGVECTOR(SQL);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EMs += dt.Rows[i][0].ToString() + "#";
            }
            EMs = EMs.Substring(0, EMs.Length - 1);
            ElementosDeMedida = EMs.Split('#');
        }
        private Lectura InterpolarLecturas(Lectura LecPrev, Lectura LecPost, DateTime fechaAInterpolar)
        {
            //=_y1+(_x-_x1)/(_x2-_x1)*(_y2-_y1)
            DateTime fecha; double valor;
            Lectura Lectura;
            valor = LecPrev.Get_valor() + (((Convert.ToDouble(fechaAInterpolar) - Convert.ToDouble(LecPrev.Get_Fecha_Medida())) / (Convert.ToDouble(LecPost.Get_Fecha_Medida()) - Convert.ToDouble(LecPrev.Get_Fecha_Medida())) * (LecPost.Get_valor() - LecPrev.Get_valor())));
            Lectura = new Lectura(fechaAInterpolar, valor);
            return Lectura;
        }
        private Lectura Obtener_Lectura_volumetrica_Anterior(DateTime Fecha)
        {
            Lectura Lectura;
            SICA_DB DB = new SICA_DB();
            string SQL = "SELECT TOP(1) Fecha_Medida, LecturaContador_M3 FROM PVYCR_DatosMotores where Fecha_medida < '" + Fecha + "'";
            DataTable dt = DB.GetDataSIGVECTOR(SQL);
            Lectura = new Lectura(DateTime.Parse(dt.Rows[0][0].ToString()), Convert.ToDouble(dt.Rows[0][1].ToString()));
            return Lectura;
        }
        private Lectura Obtener_Lectura_volumetrica_Posterior(DateTime Fecha)
        {
            Lectura Lectura;
            SICA_DB DB = new SICA_DB();
            string SQL = "SELECT TOP(1) Fecha_Medida, LecturaContador_M3 FROM PVYCR_DatosMotores where Fecha_medida > '" + Fecha + "'";
            DataTable dt = DB.GetDataSIGVECTOR(SQL);
            Lectura = new Lectura(DateTime.Parse(dt.Rows[0][0].ToString()), Convert.ToDouble(dt.Rows[0][1].ToString()));
            return Lectura;
        }

        public double ConsumoPorVolumetrico(DateTime FI, DateTime FF)
        {

            return 0;
        }
        public double ConsumoPorCaudalimetro(DateTime FI, DateTime FF)
        {
            return 0;
        }
        public double ConsumoPorElectrico(DateTime FI, DateTime FF)
        {
            return 0;
        }
        public double ConsumoPorHorometro(DateTime FI, DateTime FF)
        {
            return 0;
        }
    }
    public class SICA_SysemIO
    {
        #region "VARS"
        #endregion
        public SICA_SysemIO()
        { }
        public bool ExisteCarpetaEnServidor(string carpeta)
        {
            return System.IO.Directory.Exists(carpeta);
        }
    }
}