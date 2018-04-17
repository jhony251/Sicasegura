using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Net.Mail;





namespace SicaSegura
{
    public class dbs_access_obras
    {
        private String connectionString;
        private String OleDBProvider = "Microsoft.JET.OLEDB.12.0"; 
        private String OleDBDataSource = "C:\\obras.accdb";
        private String OleDBPassword = "infosys";
        private String PersistSecurityInfo = "False";

        public dbs_access_obras()
        {

        }

        public dbs_access_obras(String connectionString)
        {
            this.connectionString = connectionString;
        }

        public String conectar()
        {
            connectionString = "Provider=" + OleDBProvider + ";Data Source=" + OleDBDataSource + ";JET OLEDB:Database Password=" + OleDBPassword + ";Persist Security Info=" + PersistSecurityInfo + "";
            return connectionString;
        }
    }


    /// <summary>
    /// Clase cuya función es la de registrar los movimientos realizados en la Web.
    /// </summary>
    public class SICA_Log
    {
        
        private SqlConnection conn;
        private string USER="", PROFILE="", PROFILECHS="", APP="", DISPOSITIVO = "", ESINFORME="", ESPAGINA="";
        public SICA_Log()
        {
            
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSICA"].ConnectionString.ToString());
        }
        public void acceso_pagina(string descripcion)
        {
            string usuario = ""; string IP = ""; string Weburl = ""; string SQL = ""; string observaciones = "";
            SicaSegura.SICA_DB Sica_DB = new SICA_DB();
            Weburl = get_URL_usuario();
            IP = get_IP_usuario();
            observaciones = USER + "  " + PROFILE + "  " + PROFILECHS;
            usuario = USER;
            //id, usuario, fecha_evento, espagina, esinforme, concepto, idConcepto, url, terminalPC, IP, App, observaciones
            SQL = "INSERT INTO SICA_log_actividad" +
                "(usuario, fecha_evento, espagina, esinforme, concepto, idConcepto, url, terminalPC, IP, App, observaciones)" +
                "VALUES('" + usuario + "','" + DateTime.Now.ToString() + "'," + ESPAGINA + "," + ESINFORME + ",'" + descripcion + "','Acceso','" + Weburl + "','" + DISPOSITIVO + "','" + IP + "','" + APP + "', '" + observaciones + "')";
            Sica_DB.GetDataDBSICA(SQL);

        }
        public void Set_APP(string value) { APP = value; }
        public void Set_USER(string value) { USER = value; }
        public void Set_PROFILE(string value) { PROFILE = value; }
        public void Set_DISPOSITIVO(string value) { DISPOSITIVO = value; }
        public void Set_ESPAGINA(string value) { ESPAGINA = value; }
        public void Set_ESINFORME(string value) { ESINFORME = value; }

        private string get_URL_usuario()
        {
            string URL;
            URL = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("'","");
            return URL;
        }
        private string get_IP_usuario()
        {
            Page page = HttpContext.Current.Handler as Page;
            string IP;
            IP = page.Request.ServerVariables["X_FORWARDED_FOR"];
            if (IP == null)
            {
                IP = page.Request.ServerVariables["REMOTE_ADDR"];
            }
            return IP;
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
            string usuario = ""; string IP = ""; string Weburl = ""; string SQL = ""; string observaciones = "";
            SicaSegura.SICA_DB Sica_DB = new SICA_DB();
            Page page = HttpContext.Current.Handler as Page;
            IP = get_IP_usuario();
            Weburl = get_URL_usuario();
            observaciones = USER + "  " + PROFILE + "  " + PROFILECHS;
            usuario = USER;
            //id, usuario, fecha_evento, espagina, esinforme, concepto, idConcepto, url, terminalPC, IP, App
            SQL = "INSERT INTO SICA_log_actividad" + 
                "(usuario, fecha_evento, espagina, esinforme, concepto, idConcepto, url, terminalPC, IP, App, observaciones)" +
                "VALUES('" + usuario + "','" + DateTime.Now.ToString() + "',1,0,'SCV','Acceso','" + Weburl + "','PC','" + IP +"','SICASEGURA','" + observaciones + "')";
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
            Sica_DB.GetDataDBSICA(SQL.Replace(';',' '));
        }
        #endregion
    }
    /// <summary>
    /// Esta Clase tiene la fianlidad de Gestionar las conexiones con BD
    /// </summary>
    public class SICA_DB
    {
        /// <summary>
        /// Alerts the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        protected void Alert(string msg)
        {
            Page page = HttpContext.Current.Handler as Page;
            string script = "alert(\"" + msg + "\");";
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script,true);
        }
        /// <summary>
        /// Gets the cadena conexion sigvector.
        /// </summary>
        /// <returns></returns>
        public string GetCadenaConexionSIGVECTOR()
        {
            return ConfigurationManager.ConnectionStrings["PDASIGVECTOR"].ConnectionString.ToString();
        }
        /// <summary>
        /// Gets the cadena conexion dbsica.
        /// </summary>
        /// <returns></returns>
        public string GetCadenaConexionDBSICA()
        {
            return ConfigurationManager.ConnectionStrings["DBSICA"].ConnectionString.ToString();
        }
        /// <summary>
        /// Gets the cadena conexion raacs.
        /// </summary>
        /// <returns></returns>
        public string GetCadenaConexionRaacs()
        {
            return ConfigurationManager.ConnectionStrings["RAACS"].ConnectionString.ToString();
        }
        /// <summary>
        /// Gets the data sigvector.
        /// </summary>
        /// <param name="SQL">The SQL.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the data dbsica.
        /// </summary>
        /// <param name="SQL">The SQL.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the data dbsica_test.
        /// </summary>
        /// <param name="SQL">The SQL.</param>
        /// <returns></returns>
        public DataTable GetDataDBSICA_test(string SQL)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["DBSICA_test"].ConnectionString;
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
        /// <summary>
        /// Gets the data raacs.
        /// </summary>
        /// <param name="SQL">The SQL.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the data sigvecto r_ ds.
        /// </summary>
        /// <param name="SQL">The SQL.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the data dbsic a_ ds.
        /// </summary>
        /// <param name="SQL">The SQL.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the data dbsica_test_ ds.
        /// </summary>
        /// <param name="SQL">The SQL.</param>
        /// <returns></returns>
        public DataSet GetDataDBSICA_test_DS(string SQL)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["DBSICA_test"].ConnectionString;
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

        /// <summary>
        /// Gets the data raac s_ ds.
        /// </summary>
        /// <param name="SQL">The SQL.</param>
        /// <returns></returns>
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
    /// <summary>
    /// Clase que define el objeto global usado en la aplicación Libro de control.
    /// </summary>
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
        public int ID_usuario{get { return _ID_usuario; }set { _ID_usuario = value; }}
        public bool Acceso_validado{get { return _Acceso_Validado; }set { _Acceso_Validado = value; }}
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
            DataTable dt = DB_LibroControl.GetDataDBSICA("SELECT id_usuario FROM sica_sist_usuario WHERE login='" + login + "' and password='" + pass + "'" );
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
        /// <summary>
        /// Obtención de la informacion registrada en el RAACS para la inscripción activa en el objeto activo
        /// </summary>
        /// <returns>Cadena de texto Spliteable a la cadena '#.'</returns>
        public string RecuperarInfoRaacsInscripcion() 
        {
            _InfoRaacs_splited="";
            SICA_DB DB = new SICA_DB();
            DataTable dt = DB.GetDataRAACS("SELECT * FROM AprovechamientosInscritos WHERE NUMPAL=" + _ID_Inscripcion);
            for (int i = 0; i < dt.Rows[0].ItemArray.Length; i++)
            {
                _InfoRaacs_splited += dt.Rows[0].ItemArray.GetValue(i).ToString() + "#.";
            }
            return _InfoRaacs_splited;
        }

    }
   
    /// <summary>
    /// 
    /// </summary>
    public class SICA_PuntoControl
    {       
        #region VARS
        /// <summary>
        /// The codigo pvycr
        /// </summary>
        private string codigoPVYCR;
        /// <summary>
        /// The elementos de medida
        /// </summary>
        private string[] ElementosDeMedida;
        /// <summary>
        /// The em activo
        /// </summary>
        private string EMActivo;
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="SICA_PuntoControl"/> class.
        /// </summary>
        /// <param name="Pvycr">The pvycr.</param>
        public SICA_PuntoControl(string Pvycr)
        {
            codigoPVYCR = Pvycr;
            ObtenerEM();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SICA_PuntoControl"/> class.
        /// </summary>
        /// <param name="Pvycr">The pvycr.</param>
        /// <param name="EM">The em.</param>
        public SICA_PuntoControl(string Pvycr, string EM)
        {
            codigoPVYCR = Pvycr;
            ObtenerEM();
            EMActivo = EM;
        }
        /// <summary>
        /// Obteners the em.
        /// </summary>
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
            try 
            {
            EMs = EMs.Substring(0, EMs.Length - 1);
            ElementosDeMedida = EMs.Split('#');
            }
            catch { }
            
        }
        /// <summary>
        /// Interpolars the lecturas.
        /// </summary>
        /// <param name="LecPrev">The lec previous.</param>
        /// <param name="LecPost">The lec post.</param>
        /// <param name="fechaAInterpolar">The fecha a interpolar.</param>
        /// <returns></returns>
        private string[] InterpolarLecturas(SICA_Lectura LecPrev, SICA_Lectura LecPost,DateTime fechaAInterpolar)
        {
            //=_y1+(_x-_x1)/(_x2-_x1)*(_y2-_y1)
            DateTime fecha; double valor;
            //SICA_Lectura Lectura;
            valor = LecPrev.Get_valor()+(((Convert.ToDouble(fechaAInterpolar)-Convert.ToDouble(LecPrev.Get_Fecha_Medida()))/(Convert.ToDouble(LecPost.Get_Fecha_Medida())-Convert.ToDouble(LecPrev.Get_Fecha_Medida()))*(LecPost.Get_valor()-LecPrev.Get_valor())));
            //Lectura = new SICA_Lectura(fechaAInterpolar, valor);

            return (fechaAInterpolar.ToString() + "%" + valor).Split('%');
        }
        /// <summary>
        /// Obtener_s the lectura_volumetrica_ anterior.
        /// </summary>
        /// <param name="Fecha">The fecha.</param>
        /// <returns></returns>
        private string[] Obtener_Lectura_volumetrica_Anterior(DateTime Fecha)
        {
            //SICA_Lectura Lectura;
            SICA_DB DB = new SICA_DB();
            string SQL = "SELECT TOP(1) Fecha_Medida, LecturaContador_M3 FROM PVYCR_DatosMotores where Fecha_medida < '" + Fecha + "'  and codigopvycr like '" + codigoPVYCR + "'";
            DataTable dt = DB.GetDataSIGVECTOR(SQL);
            //Lectura = new SICA_Lectura(DateTime.Parse(dt.Rows[0][0].ToString()), Convert.ToDouble(dt.Rows[0][1].ToString()));
            return (DateTime.Parse(dt.Rows[0][0].ToString()).ToString() + "%" + dt.Rows[0][1].ToString()).Split('%');
        }
        /// <summary>
        /// Obtener_s the lectura_volumetrica_ posterior.
        /// </summary>
        /// <param name="Fecha">The fecha.</param>
        /// <returns></returns>
        private string [] Obtener_Lectura_volumetrica_Posterior(DateTime Fecha)
        {
            SICA_Lectura Lectura;
            SICA_DB DB = new SICA_DB();
            string SQL = "SELECT TOP(1) Fecha_Medida, LecturaContador_M3 FROM PVYCR_DatosMotores where Fecha_medida > '" + Fecha + "' and codigopvycr like '" + codigoPVYCR + "'";
            DataTable dt = DB.GetDataSIGVECTOR(SQL);
            //Lectura = new SICA_Lectura(DateTime.Parse(dt.Rows[0][0].ToString()), Convert.ToDouble(dt.Rows[0][1].ToString()));
            try { return (DateTime.Parse(dt.Rows[0][0].ToString()).ToString() + "%" + dt.Rows[0][1].ToString()).Split('%'); }
            catch { return "no%disponible".Split('%'); }
        }
        /// <summary>
        /// Get_s the coordenadas utm.
        /// </summary>
        /// <returns></returns>
        public string[] get_CoordenadasUTM()
        {
            string[] coordenadas;
            SICA_DB DB = new SICA_DB();
            DataTable dt = DB.GetDataSIGVECTOR("SELECT X_UTM, Y_UTM FROM PVYCR_Puntos WHERE CodigoPVYCR like '" + codigoPVYCR + "'");
            string temp = dt.Rows[0].ItemArray.GetValue(0).ToString() + "#" + dt.Rows[0].ItemArray.GetValue(1).ToString();
            coordenadas = temp.Split('#');
            return coordenadas;
        }
        /// <summary>
        /// Consumoes the por volumetrico.
        /// </summary>
        /// <param name="FI">The fi.</param>
        /// <param name="FF">The ff.</param>
        /// <returns></returns>
        public double ConsumoPorVolumetrico(DateTime FI, DateTime FF)
        {
            System.Data.SqlClient.SqlConnection conexion = new System.Data.SqlClient.SqlConnection(new SicaSegura.SICA_DB().GetCadenaConexionSIGVECTOR());
            Sicadll.AccesoADatos.AccesoADatos2 punto = new Sicadll.AccesoADatos.AccesoADatos2();
            double acumulado = 0;
            for (int i = 0; i < ElementosDeMedida.Length; i++)
            {
                if (ElementosDeMedida[i].Contains("V")) { acumulado = acumulado + double.Parse( punto.obtenerAcumulado(codigoPVYCR.Trim(), ElementosDeMedida[i].Trim() , conexion, FI.ToShortDateString(), FF.ToShortDateString())[1].ToString()); }
            }
            return acumulado;
        }
        /// <summary>
        /// Consumoes the por caudalimetro.
        /// </summary>
        /// <param name="FI">The fi.</param>
        /// <param name="FF">The ff.</param>
        /// <returns></returns>
        public double ConsumoPorCaudalimetro(DateTime FI, DateTime FF)
        {
            return 0;
        }
        /// <summary>
        /// Consumoes the por electrico.
        /// </summary>
        /// <param name="FI">The fi.</param>
        /// <param name="FF">The ff.</param>
        /// <returns></returns>
        public double ConsumoPorElectrico(DateTime FI, DateTime FF)
        {
            return 0;
        }
        /// <summary>
        /// Consumoes the por horometro.
        /// </summary>
        /// <param name="FI">The fi.</param>
        /// <param name="FF">The ff.</param>
        /// <returns></returns>
        public double ConsumoPorHorometro(DateTime FI, DateTime FF)
        {
            return 0;
        }
        /// <summary>
        /// Insertars the lectura con foto en bd.
        /// </summary>
        public void InsertarLecturaConFotoEnBD()
        {

        }
        /// <summary>
        /// Insertars the lectura en bd.
        /// </summary>
        public void InsertarLecturaEnBD()
        {
        }
        public double get_lecturaContadorInicioAnyoHidrologico()
        {
            double lectura = 0;
            if (DateTime.Now.Month < 10)
            {
                   lectura = double.Parse(Obtener_Lectura_volumetrica_Posterior(DateTime.Parse("01/10/" + (DateTime.Now.Year-1).ToString()))[1]);
            }
            else
            {
                lectura = double.Parse(Obtener_Lectura_volumetrica_Posterior(DateTime.Parse("01/10/" + (DateTime.Now.Year).ToString()))[1]);
            }
            return lectura;
        }
        public double get_ultimalecturaContadorAnyoHidrologico()
        {
            double lectura = 0;
            lectura = double.Parse(Obtener_Lectura_volumetrica_Anterior(DateTime.Now)[1]);
            return lectura;
        }
        public string Get_codigoSICA() { return codigoPVYCR; }
    }

    public class SICA_ElementoMedida
    {
        private string Tipo_de_elemento = "";
        private int Numero_EM = 0;
        private string[] Tipo_Obtencion_caudal;
        private string Elemeto_de_medida = "";
        private string[] Cod_Fuente_datos;
        private SICA_PuntoControl punto;
        //private SICA_Cauce Cauce;
        private SICA_Agrupacion Agrupacion;
        
        public SICA_ElementoMedida(SICA_PuntoControl _punto, string EM, string [] CFDs)
        {
            
            Elemeto_de_medida = EM;
            Set_PuntoControlSICA(_punto);
            Set_CFDS(CFDs);
            
        }
        /// <summary>
        /// recibe por parametro el objeto Punto control al que pertenece el EM
        /// </summary>
        /// <param name="_punto">The _punto.</param>
        public void Set_PuntoControlSICA(SICA_PuntoControl _punto) { punto = _punto; }
        /// <summary>
        /// Estableces los Codigos Fuente de Dato que se tendran en cuenta con este EM
        /// </summary>
        /// <param name="CFDs">Array de valores con los CFD</param>
        public void Set_CFDS(string[] CFDs) { Cod_Fuente_datos = CFDs; }
        /// <summary>
        /// Establecer el EM de medida en cuestión en base a la tipología y al Numeral
        /// </summary>
        /// <param name="Tipo_EM">Define el Tipo de EM V//Q//H//E</param>
        /// <param name="Num_EM">Numero que define el ordinal que lo identifica</param>
        public void Set_EM(string Tipo_EM, int Num_EM) 
        {
            Set_Tipo_EM(Tipo_EM);
            Set_Num_EM(Num_EM);
            if (Numero_EM < 10) { Elemeto_de_medida = Tipo_EM + "0" + Numero_EM.ToString(); }
            else { Elemeto_de_medida = Tipo_EM + "" + Numero_EM.ToString(); }
        }
        /// <summary>
        /// Establecer el Tipo de EM.
        /// </summary>
        /// <param name="valor">Caracter que define el tipo</param>
        private void Set_Tipo_EM(string valor) { Tipo_de_elemento = valor; }
        /// <summary>
        /// Establecer el ordinal del EM.
        /// </summary>
        /// <param name="valor">The valor.</param>
        private void Set_Num_EM(int valor) 
        {
            Numero_EM = valor;
        }
        
        public double Get_consumo_hidrologico_en_curso()
        {
            SicaSegura.SICA_Calculos_Consumos Consumo = new SICA_Calculos_Consumos();
            Utiles utiles = new Utiles();
            SICA_DB DB = new SICA_DB();
            Consumo.CFDs = Cod_Fuente_datos;
            Consumo.Calculos_de_consumo_POR_CFDs = true;
            System.Data.SqlClient.SqlConnection conn = new SqlConnection(DB.GetCadenaConexionSIGVECTOR());
            return double.Parse(Consumo.obtenerAcumulado(punto.Get_codigoSICA(), Elemeto_de_medida, conn, utiles.Fecha_inicio_anyo_hidrologico_actual().ToShortDateString(), utiles.Fecha_fin_anyo_hidrologico_actual().ToShortDateString())[1].ToString());
            
        }
        public double Get_consumo_hidrologico_anterior() { return 0; }
        public double Get_consumo_hidrologico_anyo(string anyo) { return 0; }

        public DataRow Get_Ultm_registro_BD_anyo_hidrologico_en_curso() { return null; }
        public DataRow Get_Prim_registro_BD_anyo_hidrologico_en_curso() { return null; }
        public DataRow Get_Ultm_registro_BD_anyo_hidrologico_anterior() { return null; }
        public DataRow Get_Prim_registro_BD_anyo_hidrologico_anterior() { return null; }
        public DataRow Get_Ultm_registro_BD_hasta_fecha(DateTime FF) { return null; }
        public DataRow Get_Prim_registro_BD_desde_fecha(DateTime FI) { return null; }

        public void Convertir_en_EM_Favorito() { }


    }
    /// <summary>
    /// Clase para Trabajar con lecturas de producción.
    /// La lectura se debe proporcionar del siguiente modo:
    ///     Lectura tipo Q:
    ///     
    /// </summary>
    /// 
       
    public class SICA_Lectura
    {
        private DateTime fecha_medida;
        private double valor;
        private bool tienefoto = false;
        private string PVYCR, EM;
        private string Tipo_lectura, Lectura;
        /// <summary>
        /// Clase para Trabajar con lecturas de producción.
        /// 
        /// Los tipos de lectura con los que podremos trabajar ocn esta clase son:
        ///     
        ///     Lecturas de Caudal Tipo Q
        ///     Lecturas de Volumen Tipo V
        ///     Lecturas de Energía tipo E
        ///     Lecturas de tiempo de funcionamiento tipo H
        /// 
        /// La lectura se debe proporcionar del siguiente modo:
        /// 
        ///     Lectura tipo Q:PVYCR#@EM#@CFD#@Fecha#@Escala#@Calado#@Curva#@parada#@Caudal#@TOC#@Duda_calidad#@OBS#@Modificado
        ///     Lectura tipo V:PVYCR#@EM#@CFD#@Fecha#@Lectura#@Funciona#@OBS#@Justificacion#@IDincVol#@ConsVolAdic#@ReinLecVol#@Qmedido#@modificado
        ///     Lectura tipo E:PVYCR#@EM#@CFD#@Fecha#@LecturaI#@LecturaII#@LecturaIII#@TotKWH#@TotKVAR#@Funciona#@OBS#@Justificacion#@IDincElec#@ConsElecAdic#@ReinLecEle#@modificado
        ///     Lectura tipo H:PVYCR#@EM#@CFD#@Fecha#@Horas#@Funciona#@OBS#@Justificacion#@IDincVol#@ConsVolAdic#@ReinLecVol#@Qmedido#@modificado
        /// 
        /// Las unidades para Volumenes (m3)  Caudales (m3/h)  Tiempo (h)   Escala/Calado (m)
        /// 
        /// La clase permite la inserción de las mismas en BD
        /// 
        /// La ausencia de CFD supondrá "05"
        /// </summary>
        /// <param name="fecha">Momento en que se toma la lectura</param>
        /// <param name="lectura">V:Lectura  Q:Caudal  E:TotalKWh  H:horas</param>
        public SICA_Lectura(string tipo, string lectura)
        {
            Tipo_lectura = tipo;
            Lectura = lectura;
        }
        public DateTime Get_Fecha_Medida()
        {
            return fecha_medida;
        }
        public double Get_valor()
        {
            return valor;
        }
        public System.IO.MemoryStream recuperarFotoBD()
        {
            SqlConnection cn;
            SqlCommand cmd;
            DataSet ds;
            SqlDataAdapter da;
            DataRow dr;
            SqlDataReader sqldr;
            try
            {
                //da = new SqlDataAdapter("Select img from IMG where Descripcion = '" + descripcion + "'", cn);
                //ds = new DataSet();
                //da.Fill(ds, "IMG");
                //byte[] datos = new byte[0];
                //dr = ds.Tables["IMG"].Rows[0];
                //datos = (byte[])dr["img"];
                //System.IO.MemoryStream ms = new System.IO.MemoryStream(datos);
                //pbFoto.Image = System.Drawing.Bitmap.FromStream(ms);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("No se pudo consultar la Imagen: " + ex.ToString());
            }

            return null;
        }
        public bool GuardarfotoLecturaBD(string PVYCR, string EM, DateTime fechalectura, System.IO.MemoryStream ms)
        {
            SqlConnection cn;
            SqlCommand cmd;
            string mensaje = "Se inserto la imagen";
            try
            {
                SicaSegura.SICA_DB DB = new SICA_DB();
                cn = new SqlConnection(DB.GetCadenaConexionDBSICA());
                cn.Open();
                cmd = new SqlCommand("Insert into SICA_Lecturas_Imagenes((CodigoPVYCR, EM, Fecha, Imagen) values(@codigo, @EM, @fecha ,@Imagen)", cn);
                cmd.Parameters.Add("@codigo", SqlDbType.NChar);
                cmd.Parameters.Add("@EM", SqlDbType.NChar);
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime);
                cmd.Parameters.Add("@Imagen", SqlDbType.Image);

                cmd.Parameters["@codigo"].Value = PVYCR;
                cmd.Parameters["@EM"].Value = EM;
                cmd.Parameters["@fechalectura"].Value = fechalectura.ToLongDateString(); 
                cmd.Parameters["@Imagen"].Value = ms.GetBuffer();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                mensaje = "No se inserto la imagen: " + ex.ToString();
            }
            return true;
        }
        public string GuardarLecturaBaseDatos() { return ""; }
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
        public bool CrearCarpeta(String Carpeta)
        {
            System.IO.Directory.CreateDirectory(Carpeta);
            return true;
        }
        
    }

    public class SICA_Titular_Aprovechamiento 
    {
        public String[] GetListadoTitularesRaacs(int inscripcion) 
        {
            SICA_DB db = new SICA_DB();
            string SQL = "SELECT * FROM TitularesAprovechamientos where NNUMREG1="+ inscripcion.ToString();
            DataTable dt = db.GetDataRAACS(SQL);
            
            string registros = "";
            for(int i =0; i<dt.Rows.Count;i++)
            {
                for (int ii = 0; ii < dt.Columns.Count; ii++)
                {
                    registros += dt.Rows[i][ii].ToString();
                    registros += "$%";
                }
                registros += "%$";
            }
            String[] listado = registros.Split(new string[] { "$%%$" }, StringSplitOptions.None);
            return listado;

        }
        public string GetInformacionContactoLibroDigital(int inscripcion)
        {
            SICA_DB db = new SICA_DB();
            string SQL = "SELECT * FROM SICA_LibroControl_DatosContacto where NUMPAL=" + inscripcion.ToString();
            DataTable dt = db.GetDataDBSICA(SQL);
            string registros = "";
            if (dt.Rows.Count > 0)
            {
                for (int ii = 0; ii < dt.Columns.Count; ii++)
                {
                    registros += dt.Rows[0][ii].ToString();
                    registros += "$%";
                }
            }
            else { registros += "error$%noInfo"; }
            return registros;
        }
        public string EliminarInformacionContactoLibroDigital(int inscripcion)
        {
            string SQL="";
            SicaSegura.SICA_DB DB = new SICA_DB();
            SQL="delete FROM SICA_LibroControl_DatosContacto where NUMPAL=" + inscripcion.ToString();
            DB.GetDataDBSICA(SQL);
            return "OK";
        }
        public string InsertarInformacionContactoLibroDigital(int inscripcion, string datos)
        {
            SicaSegura.SICA_DB DB = new SICA_DB();
            string[] campos = datos.Split(new string[] { "$%" }, StringSplitOptions.None);
            string SQL = "INSERT INTO SICA_LibroControl_DatosContacto VALUES(";
            SQL += inscripcion;
            SQL += ",'" + campos[0].Trim().ToUpper() + "'";
            SQL += ",'" + campos[1].Trim().ToUpper() + "'";
            SQL += ",'" + campos[2].Trim().ToUpper() + "'";
            SQL += ",'" + campos[3].Trim().ToUpper() + "'";
            SQL += ",'" + campos[4].Trim().ToUpper() + "'";
            SQL += ")";
            DB.GetDataDBSICA(SQL);

            return "";
        }
        public Boolean Pendiente_de_validar_datos_de_contacto(int inscripcion)
        {
            SICA_DB DB = new SICA_DB();
            String SQL = "SELECT * FROM SICA_LibroControl_DatosContacto_validacion  WHERE inscripcion=" + inscripcion;
            DataTable dt = DB.GetDataDBSICA(SQL);
            Boolean resultado = true;
            if (dt.Rows.Count > 0)
            {
                if (((dt.Rows[0][1] != DBNull.Value) && (dt.Rows[0][3] != DBNull.Value)) && ((Convert.ToInt16(dt.Rows[0][1]) == 1) || (Convert.ToInt16(dt.Rows[0][3]) == 1)))
                {
                    resultado = false;
                }
            }
            return resultado;
        }
    }
    
    public class SICA_Interfaz
    {
        public class SICA_LibroControl
        {
            public SICA_LibroControl()
            { }
            public string Get_Enlaces_Sup_Izq_login(string[] items)
            { 
                string codigo="";
                codigo +="<div id=\"header\"><div id=\"banderas\">";
                codigo +="    <ul>";
                codigo += "        <li><a href=\"" + items[0].Split('-')[1].ToString() + "\">" + items[0].Split('-')[0].ToString() + "</a></li>";
                for (int i=1; i<items.Length; i++)
                {
                    codigo += "        <li><a href=\"" + items[i].Split('-')[1].ToString() + "\">" + items[i].Split('-')[0].ToString() + "</a></li>";
                }
                codigo +="    </ul>";
                codigo +="</div></div>";
                return codigo;
            
            }
            public string Get_Enlaces_Sup_Izq(string[] items)
            {
                string codigo = "";
                codigo += "<div id=\"header\"><div id=\"banderas\">";
                codigo += "    <ul>";
                codigo += "        <li><a href=\"" + items[0].Split('-')[1].ToString() + "\">" + items[0].Split('-')[0].ToString() + "</a></li>";
                for (int i = 1; i < items.Length; i++)
                {
                    string link = items[i].Split('-')[1].ToString();
                    link = link.Replace("index", "infousuario");
                    codigo += "        <li><a href=\"" + link + "\">" + items[i].Split('-')[0].ToString() + "</a></li>";
                }
                codigo += "    </ul>";
                codigo += "</div></div>";
                return codigo;

            }
            public void Get_Enlaces_Sup_Der()
            { }
            public string Get_Cabecera_login()
            { 
                string codigo="";
                codigo +="<div id=\"sub-headerHome\" title=\"El río Segura. Salmerón. Albacete (Autor: Jose Antonio Vera)\" style=\"Height:95px; background-image: url(http://www.chsegura.es/export/descargas/img/Otono/otono35.-El_rio_Segura_en_Salmeron._Albacete.jpg);\">";
				codigo +="  <h1>";
				codigo +="      <a class=\"marca\" href=\"http://www.chsegura.es/chs/\" style=\"margin:10px 0 0 10px;\" ><img src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/logo-chs.gif\" title=\"Logo de la Confederación Hidrográfica del Segura, ir a la página principal\" alt=\"Logo de la Confederación Hidrográfica del Segura, ir a la página principal\"></a>";
                codigo += "     <h1 style=\" text-size:40px;color:white; width:35%; float:left; margin:20px 0 0 35px; text-align:center; display:block; \">Libro de control del <br> Aprovechamiento</h1>";
                codigo += "     <a class=\"marca\" style=\"margin:10px 0 0 10px;float:right; \" ><img height=\"75px\"  src=\"./home/images/Logo_SICA.png\" title=\"Logo del Sistema Integrado de control de aprovechamientos\" alt=\"Logo del Sistema Integrado de control de aprovechamientos\" style=\"border:0px;\"></a>";
				codigo +="  </h1>";
                codigo +="  <script type=\"text/javascript\">";
                codigo +="      document.getElementById('sub-headerHome').style.backgroundImage = \"url(http://www.chsegura.es/export/descargas/img/Otono/otono35.-El_rio_Segura_en_Salmeron._Albacete.jpg)\";";
                codigo +="      document.getElementById('sub-headerHome').title = 'El río Segura. Salmerón. Albacete (Autor: Jose Antonio Vera)';";
				codigo +="  </script>";
			    codigo +="</div>";
                return codigo;
            
            }
            public string Get_Cabecera()
            {
                string codigo = "";
                codigo += "<div id=\"sub-headerHome\" title=\"El río Segura. Salmerón. Albacete (Autor: Jose Antonio Vera)\" style=\"Height:95px; background-image: url(http://www.chsegura.es/export/descargas/img/Otono/otono35.-El_rio_Segura_en_Salmeron._Albacete.jpg);\">";
                codigo += "  <h1>";
                codigo += "      <a class=\"marca\" href=\"http://www.chsegura.es/chs/\" style=\"margin:10px 0 0 10px;\" ><img src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/logo-chs.gif\" title=\"Logo de la Confederación Hidrográfica del Segura, ir a la página principal\" alt=\"Logo de la Confederación Hidrográfica del Segura, ir a la página principal\"></a>";
                codigo += "     <h1 style=\" text-size:40px;color:white; width:35%; float:left; margin:20px 0 0 35px; text-align:center; display:block; \">Libro de control del <br> Aprovechamiento</h1>";
                codigo += "     <a class=\"marca\" style=\"margin:10px 0 0 10px;float:right; \" ><img height=\"75px\"  src=\"./images/Logo_SICA.png\" title=\"Logo del Sistema Integrado de control de aprovechamientos\" alt=\"Logo del Sistema Integrado de control de aprovechamientos\" style=\"border:0px;\"></a>";
                codigo += "  </h1>";
                codigo += "  <script type=\"text/javascript\">";
                codigo += "      document.getElementById('sub-headerHome').style.backgroundImage = \"url(http://www.chsegura.es/export/descargas/img/Otono/otono35.-El_rio_Segura_en_Salmeron._Albacete.jpg)\";";
                codigo += "      document.getElementById('sub-headerHome').title = 'El río Segura. Salmerón. Albacete (Autor: Jose Antonio Vera)';";
                codigo += "  </script>";
                codigo += "</div>";
                return codigo;

            }
            public string Get_Menu_Navegacion(string pestanya)
            {
                string uno ="", dos="", tres="", cuatro="", cinco = "";
                string uno2 = "", dos2 = "", tres2 = "", cuatro2 = "", cinco2 = "";
                switch (pestanya)
                {
                    case "uno":
                        uno = "class=\"seleccionado\"";
                        uno2 = "class=\"glinkOn\"";
                        dos2 = tres2 = cuatro2 = cinco2 = "class=\"glink\"";
                        break;
                    case "dos":
                        dos = "class=\"seleccionado\"";
                        dos2 = "class=\"glinkOn\"";
                        uno2 = tres2 = cuatro2 = cinco2 = "class=\"glink\"";
                        break;
                    case "tres":
                        tres = "class=\"seleccionado\"";
                        tres2 = "class=\"glinkOn\"";
                        uno2 = dos2 = cuatro2 = cinco2 = "class=\"glink\"";
                        break;
                    case "cuatro":
                        cuatro = "class=\"seleccionado\"";
                        cuatro2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cinco2 = "class=\"glink\"";
                        break;
                    case "cinco":
                        cinco = "class=\"seleccionado\"";
                        cinco2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cuatro2 = "class=\"glink\"";
                        break;
                    default:
                        break;
                }
                string codigo="";
                codigo +="<div class=\"contenedorMenu\" style=\"z-index:1;\">";
			    codigo +="        <div id=\"globalNav\">";
				codigo +="            <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	            <li class=\"primernivel\"><div " + uno + "><a accesskey=\"0\" href=\"index.aspx\" " + uno2 + ">INICIO</a></div>";
                codigo += "	            </li>";
                codigo += "	            <li class=\"primernivel\"><div " + dos + "><a accesskey=\"1\" href=\"lecturas.aspx\" " + dos2 + ">LECTURAS SIST. MEDICIÓN</a></div>";
				codigo +="	            </li>";
                codigo += "	            <li class=\"primernivel\"><div " + tres + "><a accesskey=\"2\" href=\"infgeografica.aspx\" " + tres2 + ">UBICACIÓN DEL APROVECHAMIENTO</a></div>";
    			codigo +="	    	    </li>";
                codigo += "	            <li class=\"primernivel\"><div " + cuatro + "><a accesskey=\"3\" href=\"infadm.aspx\" " + cuatro2 + ">INF. ADMINISTRATIVA</a></div>"; 
				codigo +="	            </li>";
                codigo += "	            <li class=\"primernivel\"><div " + cinco + "><a accesskey=\"4\" href=\"correspondencia.aspx\" " + cinco2 + ">CORRESPONDENCIA</a></div>";
				codigo +="	            </li>";
				codigo +="            </ul>";
			    codigo +="        </div>";
			    codigo +="    </div>";
                return codigo;
            
            }
            public string Get_Menu_Navegacion_login()
            { 
                string codigo="";
                codigo +="<div class=\"contenedorMenu\">";
			    codigo +="    <div id=\"globalNav\">";
				codigo +="        <ul id=\"globalLink\" class=\"sinregistro\">";
				codigo +="	       <li class=\"primernivel\"><div class=\"seleccionado\"><a accesskey=\"0\" href=\"index.aspx\" class=\"glinkOn\">INICIO DE SESIÓN</a></div></li>";
				codigo +="        </ul>";
			    codigo +="    </div>";
			    codigo +="</div>";
                return codigo;
            }
            public string Get_pie_logo_corporativo()
            {
                string codigo = "";
                codigo +="<div class=\"promocionalHome\">";
				codigo +="    <ul>";
		        //codigo +="        <li class=\"rightFloat\"><a title=\"Logotipo del grupo tragsa\" href=\"http://www.tragsa.es/\"><img src=\"http://intranetgrupo/Style%20Library/Images/logo_tragsa.gif\" title=\"Logotipo del Grupo Tragsa\" alt=\"Logotipo del Grupo Tragsa\"></a></li>";
			    codigo +="    </ul>";
			    codigo +="</div>";
                return codigo;
            }
            public string Get_pie_pagina()
            {
                string codigo = "";
                codigo +="<div id=\"foot_izq\">";
		        codigo +="    <div id=\"foot_der\"></div>";
	            codigo +="</div>";
	            codigo +="<div id=\"footer\">";
		        codigo +="    <p><a href=\"http://www.magrama.gob.es\">Ministerio de Agricultura, Alimentación y Medio Ambiente</a>. Confederación Hidrográfica del Segura ©</p>";
		        codigo +="    <p>";
			    codigo +="        <a title=\"Explanation of Level Double-A Conformance\" href=\"http://www.w3.org/WAI/WCAG1AA-Conformance\"><img alt=\"Level Double-A conformance icon, W3C-WAI Web Content Accessibility Guidelines 1.0\" src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/logo_wcag1AA.gif_1718839745.gif\"></a>";
			    codigo +="        <a href=\"http://validator.w3.org/check?uri=http%3A%2F%2Fwww.chsegura.es\"><img alt=\"Valid XHTML 1.0 Transitional\" src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/xhtml_10.gif_1718839745.gif\"></a>";
			    codigo +="        <a href=\"http://jigsaw.w3.org/css-validator/validator?uri=http://www.chsegura.es\"><img alt=\"Valid CSS!\" src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/css.gif_1718839745.gif\"></a>";
			    codigo +="    </p>";
                codigo += "</div>";
                return codigo;
            }

        }

        public class SICA_Gestion_obras
        {
            public SICA_Gestion_obras() { }
           
            
            public string Get_pie_pagina()
            {
                string codigo = "";
                codigo += "<div id=\"foot_izq\">";
                codigo += "    <div id=\"foot_der\"></div>";
                codigo += "</div>";
                codigo += "<div id=\"footer\">";
                codigo += "    <p><a href=\"http://www.magrama.gob.es\">Ministerio de Agricultura, Alimentación y Medio Ambiente</a>. Confederación Hidrográfica del Segura ©</p>";
                codigo += "    <p>";
                codigo += "        <a title=\"Explanation of Level Double-A Conformance\" href=\"http://www.w3.org/WAI/WCAG1AA-Conformance\"><img alt=\"Level Double-A conformance icon, W3C-WAI Web Content Accessibility Guidelines 1.0\" src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/logo_wcag1AA.gif_1718839745.gif\"></a>";
                codigo += "        <a href=\"http://validator.w3.org/check?uri=http%3A%2F%2Fwww.chsegura.es\"><img alt=\"Valid XHTML 1.0 Transitional\" src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/xhtml_10.gif_1718839745.gif\"></a>";
                codigo += "        <a href=\"http://jigsaw.w3.org/css-validator/validator?uri=http://www.chsegura.es\"><img alt=\"Valid CSS!\" src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/css.gif_1718839745.gif\"></a>";
                codigo += "    </p>";
                codigo += "</div>";
                return codigo;
            }

            public string Get_Menu_Navegacion(string pestanya)
            {
                string uno = "", dos = "", tres = "", cuatro = "", cinco = "";
                string uno2 = "", dos2 = "", tres2 = "", cuatro2 = "", cinco2 = "";
                switch (pestanya)
                {
                    case "uno":
                        uno = "class=\"seleccionado\"";
                        uno2 = "class=\"glinkOn\"";
                        dos2 = tres2 = cuatro2 = cinco2 = "class=\"glink\"";
                        break;
                    case "dos":
                        dos = "class=\"seleccionado\"";
                        dos2 = "class=\"glinkOn\"";
                        uno2 = tres2 = cuatro2 = cinco2 = "class=\"glink\"";
                        break;
                    case "tres":
                        tres = "class=\"seleccionado\"";
                        tres2 = "class=\"glinkOn\"";
                        uno2 = dos2 = cuatro2 = cinco2 = "class=\"glink\"";
                        break;
                    case "cuatro":
                        cuatro = "class=\"seleccionado\"";
                        cuatro2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cinco2 = "class=\"glink\"";
                        break;
                    case "cinco":
                        cinco = "class=\"seleccionado\"";
                        cinco2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cuatro2 = "class=\"glink\"";
                        break;
                    default:
                        break;
                }
                string codigo = "";
                codigo += "<div class=\"contenedorMenu\" style=\"z-index:1;\">";
                codigo += "        <div id=\"globalNav\">";
                codigo += "            <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	            <li class=\"primernivel\"><div " + uno + "><a accesskey=\"0\" href=\"../index.aspx\" " + uno2 + ">INICIO</a></div>";
                codigo += "	            </li>";
                codigo += "	            <li class=\"primernivel\"><div " + dos + "><a accesskey=\"1\" href=\"gestionaobra.aspx\" " + dos2 + ">GESTIONAR OBRA EXISTENTE</a></div>";
				codigo +="	            </li>";
                codigo += "	            <li class=\"primernivel\"><div " + tres + "><a accesskey=\"2\" href=\"nuevaobra.aspx\" " + tres2 + ">NUEVA OBRA</a></div>";
    			codigo +="	    	    </li>";
                codigo += "	            <li class=\"primernivel\"><div " + cuatro + "><a accesskey=\"3\" href=\"eliminaobra.aspx\" " + cuatro2 + ">ELIMINAR OBRA EXISTENTE</a></div>"; 
				codigo +="	            </li>";
                codigo += "            </ul>";
                codigo += "        </div>";
                codigo += "    </div>";
                return codigo;

            }
            public string Get_Menu_Navegacion_login(string pestanya)
            {
                string uno = "", dos = "", tres = "", cuatro = "", cinco = "";
                string uno2 = "", dos2 = "", tres2 = "", cuatro2 = "", cinco2 = "";
                switch (pestanya)
                {
                    case "uno":
                        uno = "class=\"seleccionado\"";
                        uno2 = "class=\"glinkOn\"";
                        dos2 = tres2 = cuatro2 = cinco2 = "class=\"glink\"";
                        break;
                    case "dos":
                        dos = "class=\"seleccionado\"";
                        dos2 = "class=\"glinkOn\"";
                        uno2 = tres2 = cuatro2 = cinco2 = "class=\"glink\"";
                        break;
                    case "tres":
                        tres = "class=\"seleccionado\"";
                        tres2 = "class=\"glinkOn\"";
                        uno2 = dos2 = cuatro2 = cinco2 = "class=\"glink\"";
                        break;
                    case "cuatro":
                        cuatro = "class=\"seleccionado\"";
                        cuatro2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cinco2 = "class=\"glink\"";
                        break;
                    case "cinco":
                        cinco = "class=\"seleccionado\"";
                        cinco2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cuatro2 = "class=\"glink\"";
                        break;
                    default:
                        break;
                }
                string codigo = "";
                codigo += "<div class=\"contenedorMenu\" style=\"z-index:1;\">";
                codigo += "        <div id=\"globalNav\">";
                codigo += "            <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	            <li class=\"primernivel\"><div " + uno + "><a accesskey=\"0\" href=\"index.aspx\" " + uno2 + ">INICIO</a></div>";
                codigo += "	            </li>";
                codigo += "	            <li class=\"primernivel\"><div " + dos + "><a accesskey=\"1\" href=\"home/gestionaobra.aspx\" " + dos2 + ">GESTIONAR OBRA EXISTENTE</a></div>";
                codigo += "	            </li>";
                codigo += "	            <li class=\"primernivel\"><div " + tres + "><a accesskey=\"2\" href=\"home/nuevaobra.aspx\" " + tres2 + ">NUEVA OBRA</a></div>";
                codigo += "	    	    </li>";
                codigo += "	            <li class=\"primernivel\"><div " + cuatro + "><a accesskey=\"3\" href=\"home/eliminaobra.aspx\" " + cuatro2 + ">ELIMINAR OBRA EXISTENTE</a></div>";
                codigo += "	            </li>";
                codigo += "            </ul>";
                codigo += "        </div>";
                codigo += "    </div>";
                return codigo;

            }

            public string Get_Cabecera()
            {
                string codigo = "";
                codigo += "<div id=\"sub-headerHome\" title=\"El río Segura. Salmerón. Albacete (Autor: Jose Antonio Vera)\" style=\"Height:95px; background-image: url(http://www.chsegura.es/export/descargas/img/Otono/otono35.-El_rio_Segura_en_Salmeron._Albacete.jpg);\">";
                codigo += "  <h1>";
                codigo += "      <a class=\"marca\" href=\"http://www.chsegura.es/chs/\" style=\"margin:10px 0 0 10px;\" ><img src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/logo-chs.gif\" title=\"Logo de la Confederación Hidrográfica del Segura, ir a la página principal\" alt=\"Logo de la Confederación Hidrográfica del Segura, ir a la página principal\"></a>";
                codigo += "     <h1 style=\" text-size:40px;color:white; width:35%; float:left; margin:20px 0 0 35px; text-align:center; display:block; \">Gestion integral de <br> Obras Comisaría de Aguas CHS</h1>";
                codigo += "     <a class=\"marca\" style=\"margin:10px 0 0 10px;float:right; \" ><img height=\"75px\"  src=\"./images/Logo_SICA.png\" title=\"Logo del Sistema Integrado de control de aprovechamientos\" alt=\"Logo del Sistema Integrado de control de aprovechamientos\" style=\"border:0px;\"></a>";
                codigo += "  </h1>";
                codigo += "  <script type=\"text/javascript\">";
                codigo += "      document.getElementById('sub-headerHome').style.backgroundImage = \"url(http://www.chsegura.es/export/descargas/img/Otono/otono35.-El_rio_Segura_en_Salmeron._Albacete.jpg)\";";
                codigo += "      document.getElementById('sub-headerHome').title = 'El río Segura. Salmerón. Albacete (Autor: Jose Antonio Vera)';";
                codigo += "  </script>";
                codigo += "</div>";
                return codigo;

            }
            public string Get_Cabecera_login()
            {
                string codigo = "";
                codigo += "<div id=\"sub-headerHome\" title=\"El río Segura. Salmerón. Albacete (Autor: Jose Antonio Vera)\" style=\"Height:95px; background-image: url(http://www.chsegura.es/export/descargas/img/Otono/otono35.-El_rio_Segura_en_Salmeron._Albacete.jpg);\">";
                codigo += "  <h1>";
                codigo += "      <a class=\"marca\" href=\"http://www.chsegura.es/chs/\" style=\"margin:10px 0 0 10px;\" ><img src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/logo-chs.gif\" title=\"Logo de la Confederación Hidrográfica del Segura, ir a la página principal\" alt=\"Logo de la Confederación Hidrográfica del Segura, ir a la página principal\"></a>";
                codigo += "     <h1 style=\" text-size:40px;color:white; width:35%; float:left; margin:20px 0 0 35px; text-align:center; display:block; \">Gestion integral de <br> Obras Comisaría de Aguas CHS</h1>";
                codigo += "     <a class=\"marca\" style=\"margin:10px 0 0 10px;float:right; \" ><img height=\"75px\"  src=\"./home/images/Logo_SICA.png\" title=\"Logo del Sistema Integrado de control de aprovechamientos\" alt=\"Logo del Sistema Integrado de control de aprovechamientos\" style=\"border:0px;\"></a>";
                codigo += "  </h1>";
                codigo += "  <script type=\"text/javascript\">";
                codigo += "      document.getElementById('sub-headerHome').style.backgroundImage = \"url(http://www.chsegura.es/export/descargas/img/Otono/otono35.-El_rio_Segura_en_Salmeron._Albacete.jpg)\";";
                codigo += "      document.getElementById('sub-headerHome').title = 'El río Segura. Salmerón. Albacete (Autor: Jose Antonio Vera)';";
                codigo += "  </script>";
                codigo += "</div>";
                return codigo;

            }

            public string Get_Enlaces_Sup_Izq(string[] items)
            {
                string codigo = "";
                codigo += "<div id=\"header\"><div id=\"banderas\">";
                codigo += "    <ul>";
                codigo += "        <li><a href=\"" + items[0].Split('-')[1].ToString() + "\">" + items[0].Split('-')[0].ToString() + "</a></li>";
                for (int i = 1; i < items.Length; i++)
                {
                    string link = items[i].Split('-')[1].ToString();
                    link = link.Replace("index", "infousuario");
                    codigo += "        <li><a href=\"" + link + "\">" + items[i].Split('-')[0].ToString() + "</a></li>";
                }
                codigo += "    </ul>";
                codigo += "</div></div>";
                return codigo;

            }

            public string Get_pie_logo_corporativo()
            {
                string codigo = "";
                codigo += "<div class=\"promocionalHome\">";
                codigo += "    <ul>";
                //codigo +="        <li class=\"rightFloat\"><a title=\"Logotipo del grupo tragsa\" href=\"http://www.tragsa.es/\"><img src=\"http://intranetgrupo/Style%20Library/Images/logo_tragsa.gif\" title=\"Logotipo del Grupo Tragsa\" alt=\"Logotipo del Grupo Tragsa\"></a></li>";
                codigo += "    </ul>";
                codigo += "</div>";
                return codigo;
            }
        }

        public class SICA_Sica
        {
            public SICA_Sica() { }

            public string Get_Enlaces_Sup_Izq(string[] items)
            {
                string codigo = "";
                codigo += "<div id=\"header\"><div id=\"banderas\">";
                codigo += "    <ul>";
                codigo += "        <li><a href=\"" + items[0].Split('-')[1].ToString() + "\">" + items[0].Split('-')[0].ToString() + "</a></li>";
                for (int i = 1; i < items.Length; i++)
                {
                    codigo += "        <li><a href=\"" + items[i].Split('-')[1].ToString() + "\">" + items[i].Split('-')[0].ToString() + "</a></li>";
                }
                codigo += "    </ul>";
                codigo += "</div></div>";
                return codigo;

            }
            public void Get_Enlaces_Sup_Der()
            { }
            public string Get_Cabecera()
            {
                string codigo = "";
                codigo += "<div id=\"sub-headerHome\" title=\"El río Segura. Salmerón. Albacete (Autor: Jose Antonio Vera)\" style=\"Height:95px; background-image: url(http://www.chsegura.es/export/descargas/img/Otono/otono35.-El_rio_Segura_en_Salmeron._Albacete.jpg);\">";
                codigo += "  <h1>";
                codigo += "      <a class=\"marca\" href=\"http://www.chsegura.es/chs/\" style=\"margin:10px 0 0 10px;\" ><img src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/logo-chs.gif\" title=\"Logo de la Confederación Hidrográfica del Segura, ir a la página principal\" alt=\"Logo de la Confederación Hidrográfica del Segura, ir a la página principal\"></a>";
                codigo += "     <h1 style=\" text-size:40px;color:white; width:35%; float:left; margin:20px 0 0 35px; text-align:center; display:block; \">Sistema Integrado de <br> Control de Aprovechamientos</h1>";
                codigo += "     <a class=\"marca\" style=\"margin:10px 0 0 10px;float:right; \" ><img height=\"75px\"  src=\"http://sica.chsegura.es/sicasegura/sica/images/Logo_SICA.png\" title=\"Logo del Sistema Integrado de control de aprovechamientos\" alt=\"Logo del Sistema Integrado de control de aprovechamientos\" style=\"border:0px;\"></a>";
                codigo += "  </h1>";
                codigo += "  <script type=\"text/javascript\">";
                codigo += "      document.getElementById('sub-headerHome').style.backgroundImage = \"url(http://www.chsegura.es/export/descargas/img/Otono/otono35.-El_rio_Segura_en_Salmeron._Albacete.jpg)\";";
                codigo += "      document.getElementById('sub-headerHome').title = 'El río Segura. Salmerón. Albacete (Autor: Jose Antonio Vera)';";
                codigo += "  </script>";
                codigo += "</div>";
                return codigo;

            }
            public string Get_Menu_Navegacion()
            {
                string codigo = "";
                codigo += "<div class=\"contenedorMenu\">";
                codigo += "    <div id=\"globalNav\">";
                codigo += "        <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	       <li class=\"primernivel\"><div class=\"seleccionado\"><a accesskey=\"0\" href=\"index.aspx\" class=\"glinkOn\">INICIO</a></div></li>";
                codigo += "        </ul>";
                codigo += "    </div>";
                codigo += "</div>";
                return codigo;
            }
            public string Get_Menu_Navegacion_OE()
            {
                string codigo = "";
                codigo += "<div class=\"contenedorMenu\">";
                codigo += "    <div id=\"globalNav\">";
                codigo += "        <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	       <li class=\"primernivel\"><div class=\"seleccionado\"><a accesskey=\"0\" href=\"index.aspx\" class=\"glinkOn\">INICIO</a></div></li>";
                codigo += "        </ul>";
                codigo += "    </div>";
                codigo += "</div>";
                return codigo;
            }
            public string Get_Menu_Navegacion_Completo(string pestanya)
            {
                string uno = "", dos = "", tres = "", cuatro = "", cinco = "", seis = "", siete = "", ocho = "", nueve="";
                string uno2 = "", dos2 = "", tres2 = "", cuatro2 = "", cinco2 = "", seis2 = "", siete2 = "", ocho2 = "", nueve2="";
                switch (pestanya)
                {
                    case "uno":
                        uno = "class=\"seleccionado\"";
                        uno2 = "class=\"glinkOn\"";
                        dos2 = tres2 = cuatro2 = cinco2 = seis2 = siete2 = ocho2 = nueve2 = "class=\"glink\"";
                        break;
                    case "dos":
                        dos = "class=\"seleccionado\"";
                        dos2 = "class=\"glinkOn\"";
                        uno2 = tres2 = cuatro2 = cinco2 = seis2 = siete2 = ocho2 = nueve2 = "class=\"glink\"";
                        break;
                    case "tres":
                        tres = "class=\"seleccionado\"";
                        tres2 = "class=\"glinkOn\"";
                        uno2 = dos2 = cuatro2 = cinco2 = seis2 = siete2 = ocho2 = nueve2 = "class=\"glink\"";
                        break;
                    case "cuatro":
                        cuatro = "class=\"seleccionado\"";
                        cuatro2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cinco2 = seis2 = siete2 = ocho2 = nueve2 = "class=\"glink\"";
                        break;
                    case "cinco":
                        cinco = "class=\"seleccionado\"";
                        cinco2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cuatro2 = seis2 = siete2 = ocho2 = nueve2 = "class=\"glink\"";
                        break;
                    case "seis":
                        seis = "class=\"seleccionado\"";
                        seis2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cuatro2 = cinco2 = siete2 = ocho2 = nueve2 = "class=\"glink\"";
                        break;
                    case "siete":
                        siete = "class=\"seleccionado\"";
                        siete2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cuatro2 = cinco2 = seis2 = ocho2 = nueve2 = "class=\"glink\"";
                        break;
                    case "ocho":
                        ocho = "class=\"seleccionado\"";
                        ocho2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cuatro2 = cinco2 = siete2 = nueve2 = "class=\"glink\"";
                        break;
                    case "nueve":
                        nueve = "class=\"seleccionado\"";
                        nueve2 = "class=\"glinkOn\"";
                        uno2 = dos2 = tres2 = cuatro2 = cinco2 = siete2 = ocho2 = "class=\"glink\"";
                        break;
                    default:
                        break;
                }
                string codigo = "";
                codigo += "<div class=\"contenedorMenu\">";
                codigo += "    <div id=\"globalNav\">";
                codigo += "        <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	       <li class=\"primernivel\" style=\"width:130px!important;\"><div " + uno + "><a accesskey=\"0\" href=\"index.aspx\" " + uno2 + ">INICIO</a></div></li>";
                //codigo += "        </ul>";
                //codigo += "        <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	       <li class=\"primernivel\"  style=\"width:140px!important;\"><div " + dos + "><a accesskey=\"0\" href=\"ism.aspx\" " + dos2 + ">ISM/RAACS</a></div></li>";
                //codigo += "        </ul>";
                //codigo += "        <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	       <li class=\"primernivel\"  style=\"width:140px!important;\"><div " + tres + "><a accesskey=\"0\" href=\"consumos.aspx\" " + tres2 + ">CONSUMOS</a></div></li>";
                //codigo += "        </ul>";
                //codigo += "        <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	       <li class=\"primernivel\"  style=\"width:140px!important;\"><div " + cuatro + "><a accesskey=\"0\" href=\"telemedida.aspx\" " + cuatro2 + ">TELEMEDIDA</a></div></li>";
                //codigo += "        </ul>";
                //codigo += "        <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	       <li class=\"primernivel\"  style=\"width:140px!important;\"><div " + cinco + "><a accesskey=\"0\" href=\"fotos.aspx\" " + cinco2 + ">FOTOS</a></div></li>";
                //codigo += "        </ul>";
                //codigo += "        <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	       <li class=\"primernivel\"  style=\"width:140px!important;\"><div " + seis + "><a accesskey=\"0\" href=\"esquemas.aspx\" " + seis2 + ">ESQUEMAS</a></div></li>";
                //codigo += "        </ul>";
                //codigo += "        <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	       <li class=\"primernivel\"  style=\"width:140px!important;\"><div " + siete + "><a accesskey=\"0\" href=\"index.aspx\" " + siete2 + ">MANTENIMIENTOS</a></div></li>";
                //codigo += "        </ul>";
                //codigo += "        <ul id=\"globalLink\" class=\"sinregistro\">";
                codigo += "	       <li class=\"primernivel\"  style=\"width:140px!important;\"><div " + ocho + "><a accesskey=\"0\" href=\"index.aspx\" " + ocho2 + ">OFICIOS</a></div></li>";
                codigo += "        </ul>";
                codigo += "    </div>";
                codigo += "</div>";
                return codigo;
            }
            public string Get_pie_logo_corporativo()
            {
                string codigo = "";
                codigo += "<div class=\"promocionalHome\">";
                codigo += "    <ul>";
                //codigo +="        <li class=\"rightFloat\"><a title=\"Logotipo del grupo tragsa\" href=\"http://www.tragsa.es/\"><img src=\"http://intranetgrupo/Style%20Library/Images/logo_tragsa.gif\" title=\"Logotipo del Grupo Tragsa\" alt=\"Logotipo del Grupo Tragsa\"></a></li>";
                codigo += "    </ul>";
                codigo += "</div>";
                return codigo;
            }
            public string Get_pie_pagina()
            {
                string codigo = "";
                codigo += "<div id=\"foot_izq\">";
                codigo += "    <div id=\"foot_der\"></div>";
                codigo += "</div>";
                codigo += "<div id=\"footer\">";
                codigo += "    <p><a href=\"http://www.magrama.gob.es\">Ministerio de Agricultura, Alimentación y Medio Ambiente</a>. Confederación Hidrográfica del Segura ©</p>";
                codigo += "    <p>";
                codigo += "        <a title=\"Explanation of Level Double-A Conformance\" href=\"http://www.w3.org/WAI/WCAG1AA-Conformance\"><img alt=\"Level Double-A conformance icon, W3C-WAI Web Content Accessibility Guidelines 1.0\" src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/logo_wcag1AA.gif_1718839745.gif\"></a>";
                codigo += "        <a href=\"http://validator.w3.org/check?uri=http%3A%2F%2Fwww.chsegura.es\"><img alt=\"Valid XHTML 1.0 Transitional\" src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/xhtml_10.gif_1718839745.gif\"></a>";
                codigo += "        <a href=\"http://jigsaw.w3.org/css-validator/validator?uri=http://www.chsegura.es\"><img alt=\"Valid CSS!\" src=\"http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/css.gif_1718839745.gif\"></a>";
                codigo += "    </p>";
                codigo += "</div>";
                return codigo;
            }

        }

    }

    public class SICA_EquipoTelemedida
    {
        SICA_DB DB = new SICA_DB();
        private string MarcaEquipo = "Otro";
        private string ModeloEquipo = "";
        private string InfAdEquipo = "";
        private bool Enviadatos = false;
        private bool Cargadatos = false;
        private string Exp_ISM = "";
        private string CodigoSica = "";
        private string Titularidad = "";
        private string Descripcion = "";
        private string Instalador = "";
        private string Tecnico = "";
        private string TipoControl = "";
        private bool Instalado = false;
        public SICA_EquipoTelemedida(string codigoSICA)
        {
            CodigoSica = codigoSICA;
            string SQL = "SELECT * FROM SICA_Puntos_Control WHERE Punto_control like '" + CodigoSica + "%'";
            System.Data.DataTable dt = DB.GetDataDBSICA(SQL);
            ModeloEquipo = dt.Rows[0].ItemArray[5].ToString().Trim().Split(' ')[0].ToString().Trim();
            try { InfAdEquipo = dt.Rows[0].ItemArray[5].ToString().Trim().Split(' ')[1].ToString().Trim(); }
            catch { }
            if (ModeloEquipo == "MASERMIC") { MarcaEquipo = "MaserMic"; }
            if (ModeloEquipo.Contains("LS")) { MarcaEquipo = "Sofrel"; }
            if (ModeloEquipo.Contains("LT")) { MarcaEquipo = "Sofrel"; }
            if (ModeloEquipo.Contains("S500")) { MarcaEquipo = "Sofrel"; }
            if (dt.Rows[0].ItemArray[7].ToString().Trim() == "SI") { Cargadatos = true; }
            if (dt.Rows[0].ItemArray[8].ToString().Trim() == "SI") { Enviadatos = true; }
            Exp_ISM = dt.Rows[0].ItemArray[3].ToString().Trim();
            Titularidad = dt.Rows[0].ItemArray[13].ToString().Trim();
            Descripcion = dt.Rows[0].ItemArray[4].ToString().Trim();
            Instalador = dt.Rows[0].ItemArray[6].ToString().Trim();
            Tecnico = dt.Rows[0].ItemArray[15].ToString().Trim();
            TipoControl = dt.Rows[0].ItemArray[16].ToString().Trim();
            if (dt.Rows[0].ItemArray[17].ToString().Trim() == "1") { Instalado = true; }

        }
        public bool get_cargadatos() { return Cargadatos; }
        public bool get_enviadatos() { return Enviadatos; }
        public bool get_instalado() { return Instalado; }
        public void set_cargadatos(string valor)
        {
            string SQL = "UPDATE SICA_Puntos_Control SET cargandodatos = '" + valor.Trim() + "' where punto_control like '" + CodigoSica + "'";
            DB.GetDataDBSICA(SQL);
        }
        public void set_enviadatos(string valor)
        {
            string SQL = "UPDATE SICA_Puntos_Control SET enviandodatos = '" + valor.Trim() + "' where punto_control like '" + CodigoSica + "'";
            DB.GetDataDBSICA(SQL);
        }
        public void set_instalado(string valor)
        {
            string SQL = "UPDATE SICA_Puntos_Control SET utmy = '" + valor.Trim() + "' where punto_control like '" + CodigoSica + "'";
            DB.GetDataDBSICA(SQL);
        }
        public string get_modeloequipo() { return ModeloEquipo; }

    }

    public class SICA_EXP_ISM
    {
        private string numeroDeExpedienteISM = "";
        private SICA_Agrupacion _agrupacion;
        private System.Data.DataTable _titulares;

        private string _inscripcion = "";
        public SICA_EXP_ISM(string exp_ism)
        {
            numeroDeExpedienteISM = exp_ism;
            _agrupacion = new SICA_Agrupacion(int.Parse(Obtener_Inscripcion_Por_ISM()));
            SICA_Titular_Aprovechamiento tit = new SICA_Titular_Aprovechamiento();
            SICA_DB DB = new SICA_DB();
            _titulares = DB.GetDataRAACS( "SELECT NOMBRE, APELLIDO, CIF FROM TitularesAprovechamientos where NNUMREG1="+ Obtener_Inscripcion_Por_ISM());
            
        }
        public System.Data.DataTable get_Titulares(){return _titulares;}
        public string get_ExpISM() { return numeroDeExpedienteISM; }
        private string Obtener_Inscripcion_Por_ISM()
        {
            SICA_DB DB = new SICA_DB();
            return DB.GetDataDBSICA("SELECT INSCRIPCION FROM SICA_Seguimiento_Expedientes_ISM WHERE (EXP_ISM LIKE '" + numeroDeExpedienteISM + "')").Rows[0][0].ToString();
        }


    }


    public class SICA_Calculos_Consumos
    {

        public bool Calculos_de_consumo_POR_CFDs;
        public string[] CFDs;



        public object[] SacarLecturasPreYPro(string EM, string strWhere, string strOrden, SqlConnection conexion, bool anyadir_diferencial)
        {

            string strSQL = "";
            switch (EM.Substring(0,1)) {
                case "Q":
                    strSQL = "SELECT * FROM (" + "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, Escala_M, Calado_M, NULL as RegimenCurva, NULL as NumeroParada, Caudal_M3S, TipoObtencionCaudal, NULL as Duda_Calidad,'Pendiente' as Observaciones, 1 as Estadillo " + "FROM PVYCR_DatosAcequias_Estadillo " + strWhere + "UNION " + "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, Escala_M, Calado_M, RegimenCurva, NumeroParada, Caudal_M3S, TipoObtencionCaudal, Duda_Calidad, Observaciones, 0 as Estadillo " + "FROM PVYCR_DatosAcequias " + strWhere + ") AS DTABLE " + strOrden;
                    break;
                case "V":
                    strSQL = "SELECT * FROM (" + "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, LecturaContador_M3,CaudalMedido, Funciona as En_marcha, 'Pendiente' as Observaciones, idIncidenciaVolumetrica, ConsumoVolumetricoAdicional, ReinicioLecturaVolumetrica, Justificacion, 1 as Estadillo " + "FROM PVYCR_DatosMotores_Estadillo " + strWhere + "UNION " + "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, LecturaContador_M3,CaudalMedido, Funciona as En_marcha, 'Consolidado' as Observaciones, idIncidenciaVolumetrica, ConsumoVolumetricoAdicional, ReinicioLecturaVolumetrica, Justificacion, 0 as Estadillo " + "FROM PVYCR_DatosMotores " + strWhere + ") AS DTABLE " + strOrden;
                    break;
                case "E":
                    strSQL = "SELECT * FROM (" + "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, LecturaI, LecturaII, LecturaIII, Total_KWH, Total_Kvar, Funciona as En_marcha, cast(Observaciones as nvarchar(255)) as Observaciones, Justificacion, idIncidenciaElectrica, ConsumoElectricoAdicional, ReinicioLecturaElectrica, 1 as Estadillo " + "FROM PVYCR_DatosAlimentacion_Estadillo " + strWhere + "UNION " + "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, LecturaI, LecturaII, LecturaIII, Total_KWH, Total_Kvar, Funciona as En_marcha, cast(Observaciones as nvarchar(255)) as Observaciones, Justificacion, idIncidenciaElectrica, ConsumoElectricoAdicional, ReinicioLecturaElectrica, 0 as Estadillo " + "FROM PVYCR_DatosAlimentacion " + strWhere + ") AS DTABLE " + strOrden;
                    break;
                case "H":
                    strSQL = "SELECT * FROM (" + "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, HorasIntervalo, Funciona as En_marcha, cast(Observaciones as nvarchar(255)) as Observaciones, idIncidenciaVolumetrica, ConsumoVolumetricoAdicional, ReinicioLecturaVolumetrica, 1 as Estadillo " + "FROM PVYCR_DatosHorometros_Estadillo " + strWhere + "UNION " + "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, HorasIntervalo, Funciona as En_marcha, cast(Observaciones as nvarchar(255)) as Observaciones, idIncidenciaVolumetrica, ConsumoVolumetricoAdicional, ReinicioLecturaVolumetrica, 0 as Estadillo " + "FROM PVYCR_DatosHorometros " + strWhere + ") AS DTABLE " + strOrden;
                    break;
            }

            SqlDataAdapter da = new SqlDataAdapter("", conexion);
            DataSet dst = new DataSet();
            int i = 0;
            da.SelectCommand.CommandText = strSQL;
            da.Fill(dst, "Datos");
            if ((anyadir_diferencial == true)) {
                dst.Tables[0].Columns.Add("Volumen parcial");

                for (i = 0; i <= dst.Tables[0].Rows.Count - 2; i++) {
                    dst.Tables[0].Rows[i]["Volumen parcial"] = (double) dst.Tables[0].Rows[i]["LecturaContador_M3"] - (double) dst.Tables[0].Rows[i + 1]["LecturaContador_M3"];
                }
                dst.Tables[0].Rows[dst.Tables[0].Rows.Count - 1]["Volumen parcial"] = 0;
            }
            object[] vector = new object[3];
            vector[0] = dst;
            vector[1] = strSQL;
            return vector;
        }
        public object[] obtenerAcumulado(string codigoPVYCR, string EM, SqlConnection conexion, string fechaInicio, string fechaFin )
        {
            object[] resultado = new object[5];

            SqlDataAdapter da = new SqlDataAdapter("", conexion);
            DataSet dst = new DataSet();

            if (dst.Tables.Contains("Datos")) {
                dst.Tables.Remove("Datos");
            }

            CrearDatasetDiferencial(EM.Substring(0,1), codigoPVYCR, EM, fechaFin, fechaInicio, conexion, ref dst);
            dst.Tables[0].TableName = "Datos";

            FinalizarDatasetElementos(EM.Substring(0,1), dst);

            obtenerVolumenDiferencial(EM.Substring(0,1), dst.Tables["Datos"]);

            string codigoCauce = codigoPVYCR.Substring(0,codigoPVYCR.IndexOf('P')).ToString();

            switch (EM.Substring(0,1)) {
                case "Q":
                    resultado[0] = null;
                    resultado[1] = obtenerCaudalAcumulado(dst);
                    //(m3)
                    break; // TODO: might not be correct. Was : Exit Select

                    break;
                case "V":
                    resultado[0] = null;
                    resultado[1] = obtenerVolumenAcumulado(dst);
                    //(m3)
                    break; // TODO: might not be correct. Was : Exit Select

                    
                case "E":
                    resultado[0] = obtenerVolumenElectricoAcumulado(dst);
                    resultado[1] = obtenerVolumenElectricoAcumuladoM3(dst);
                    //(m3)
                    break; // TODO: might not be correct. Was : Exit Select

                   
                case "H":
                    resultado[0] = obtenerVolumenAcumuladoHoras(dst);
                    resultado[1] = obtenerVolumenHorometroAcumulado(dst);
                    //(m3)
                    break; // TODO: might not be correct. Was : Exit Select

                    
            }

            resultado[2] = this.ObtenerConcesion(codigoCauce, conexion);
            resultado[3] = this.ObtenerPorConcesion(Convert.ToInt32(resultado[2]), Convert.ToDouble(resultado[1]));
            if (dst.Tables["Datos"].Rows.Count > 0) {
                resultado[4] = dst.Tables["Datos"].Rows[0]["fecha_medida"];
            }

            return resultado;
        }
        private object[] CalculaAnyoHidrologico()
        {
            object[] vector = new object[3];
            DateTime AnyoHidroIni = default(DateTime);
            DateTime AnyoHidroFin = default(DateTime);

            if (DateTime.Now.Month >= 10) {
                AnyoHidroIni = Convert.ToDateTime("01/10/" + DateTime.Now.Year.ToString());
                AnyoHidroFin = Convert.ToDateTime("30/09/" + DateTime.Now.AddYears(1).Year.ToString());
            } else {
                AnyoHidroIni = Convert.ToDateTime("01/10/" + DateTime.Now.AddYears(-1).Year.ToString());
                AnyoHidroFin = Convert.ToDateTime("30/09/" + DateTime.Now.Year.ToString());
            }

            vector[0] = AnyoHidroIni;
            vector[1] = AnyoHidroFin;
            return vector;
        }
        private void CrearDatasetDiferencial(string tipoElemen, string codigoPVYCR, string idelemento, string FiltroFechaFin, string FiltroFechaIni, SqlConnection conexion, ref DataSet dst)
        {
            //No filtramos por las nulas, las sacamos todas (En sicasegura Mostrar Nulos = True)
            string sentenciaSel = "";
            string sFiltro = "";
            DateTime fechainicio = default(DateTime);
            DateTime fechaFin = default(DateTime);
            string sentenciaOrder = "";
            string vFiltroFechaIni = "";
            string vFiltroFechaFin = "";

            SqlDataAdapter da = new SqlDataAdapter("", conexion);
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
            SqlCommand comando = new SqlCommand("", conexion);
            comando.CommandText = "SELECT isnull(TipoSensor,'') FROM PVYCR_Puntos WHERE codigoPVYCR='" + codigoPVYCR + "'";
            string Telemedida =  comando.ExecuteScalar().ToString();

            if (string.IsNullOrEmpty(Telemedida)) {
                if (DateTime.Now.Month < 10) {
                    fechainicio = DateTime.Parse("01/10/" + DateTime.Now.AddYears(-2).Year.ToString());
                    fechaFin = DateTime.Today;
                } else {
                    fechainicio = DateTime.Parse("01/10/" + DateTime.Now.AddYears(-1).Year.ToString());
                    fechaFin = DateTime.Today;
                }

            } else {
                if (DateTime.Now.Month < 10) {
                    fechainicio = DateTime.Parse("01/10/" + DateTime.Now.AddYears(-1).Year.ToString());
                    fechaFin = DateTime.Today;
                } else {
                    fechainicio = DateTime.Parse("01/10/" + DateTime.Now.Year.ToString());
                    fechaFin = DateTime.Today;
                }
            }
            dst.Clear();

            if (!string.IsNullOrEmpty(FiltroFechaFin)) {
                vFiltroFechaFin = FiltroFechaFin + " 23:59:59";
            }
            if (!string.IsNullOrEmpty(FiltroFechaIni)) {
                vFiltroFechaIni = FiltroFechaIni + " 00:00:00";
            }
            if (!string.IsNullOrEmpty(FiltroFechaFin) & !string.IsNullOrEmpty(FiltroFechaIni)) {
                sFiltro = sFiltro + " and Fecha_medida between '" + vFiltroFechaIni + "' and '" + vFiltroFechaFin + "' ";
            } else if (string.IsNullOrEmpty(FiltroFechaFin) & string.IsNullOrEmpty(FiltroFechaIni)) {
                sFiltro = sFiltro + " and Fecha_Medida between '" + fechainicio + " 00:00:00' and '" + fechaFin + " 23:59:59' ";
            } else {
                sFiltro = "";
            }

            string Filtro_SQL_CFDs="(";
            for (int i = 0; i < CFDs.Length; i++)
            {
                if (i > 0) { Filtro_SQL_CFDs += " OR "; }
                Filtro_SQL_CFDs += " cod_fuente_dato like '" + CFDs[i] + "' ";
            }
            Filtro_SQL_CFDs += " ) ";


            if (tipoElemen == "Q") {
                if (Calculos_de_consumo_POR_CFDs) {
                    sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,Escala_M,d.Calado_M,d.Observaciones " + ",d.TipoObtencionCaudal,d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD " + ",d.modificado FROM PVYCR_DatosAcequias D " + "where codigoPVYCR = '" + codigoPVYCR + "' and " + Filtro_SQL_CFDs + "and " + "idElementoMedida = '" + idelemento + "' ";
                } else {
                    sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,Escala_M,d.Calado_M,d.Observaciones " + ",d.TipoObtencionCaudal,d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD, F.FUENTE_DATOS DescFuenteDato " + ",d.modificado FROM PVYCR_DatosAcequias D " + "INNER JOIN FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " + "where codigoPVYCR = '" + codigoPVYCR + "' and " + "idElementoMedida = '" + idelemento + "' ";
                }


                sentenciaOrder = " order by codigoPVYCR, Fecha_Medida desc, d.Cod_Fuente_dato, d.TipoObtencionCaudal ";

                if (!string.IsNullOrEmpty(sFiltro)) {
                    sentenciaSel = sentenciaSel + sFiltro + sentenciaOrder;
                } else {
                    sentenciaSel = sentenciaSel + " and Fecha_Medida between '" + fechainicio + " 00:00:00' and '" + fechaFin + " 23:59:59' " + sentenciaOrder;
                }

                da.SelectCommand.CommandText = sentenciaSel;
                da.Fill(dst, "TablaAcequias");

                DataTable Tabla = default(DataTable);
                Tabla = dst.Tables["TablaAcequias"].Clone();
                for (int i = dst.Tables["TablaAcequias"].Rows.Count - 1; i >= 0; i += -1) {
                    Tabla.Rows.Add(dst.Tables["TablaAcequias"].Rows[i].ItemArray);
                }
                dst.Tables.Remove(dst.Tables["TablaAcequias"]);
                dst.Tables.Add(Tabla);

            } else if (tipoElemen == "E") {
                sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " + "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " + "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " + "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato,isnull(C.E_FCorrectorContActiva,0) * isnull(C.E_RelacionM3_KWH,0) relacionM3_Kwh, C.IdContador " + ",D.modificado FROM PVYCR_DatosAlimentacion D " + "LEFT OUTER JOIN PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " + "INNER JOIN FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " + "LEFT OUTER JOIN PVYCR_Contadores_ElementosMedida E ON E.codigoPVYCR = d.codigoPVYCR and " + "E.idelementoMedida = D.idelementomedida and D.Fecha_Medida BETWEEN E.FechaInicio AND ISNULL(E.FECHAFINAL, GETDATE()) " + "LEFT OUTER JOIN PVYCR_Contadores C ON C.idContador = E.idContador and " + "C.FechaRevision = E.fechaRevision and c.tipocontador = 'E' " + "where D.idElementoMedida = '" + idelemento + "' and D.CodigoPVYCR = '" + codigoPVYCR + "' ";

                sentenciaOrder = " order by D.codigoPVYCR, D.Fecha_Medida desc";

                if (!string.IsNullOrEmpty(sFiltro)) {
                    sentenciaSel = sentenciaSel + sFiltro + sentenciaOrder;
                } else {
                    sentenciaSel = sentenciaSel + " and Fecha_Medida between '" + fechainicio + " 00:00:00' and '" + fechaFin + " 23:59:59' " + sentenciaOrder;
                }

                da.SelectCommand.CommandText = sentenciaSel;
                da.Fill(dst, "TablaAlimentacion");

                DataTable Tabla = default(DataTable);
                Tabla = dst.Tables["TablaAlimentacion"].Clone();
                for (int i = dst.Tables["TablaAlimentacion"].Rows.Count - 1; i >= 0; i += -1) {
                    Tabla.Rows.Add(dst.Tables["TablaAlimentacion"].Rows[i].ItemArray);
                }
                dst.Tables.Remove(dst.Tables["TablaAlimentacion"]);
                dst.Tables.Add(Tabla);

                QuitarRegistrosSegunFuenteDato("E", dst);

            } else if (tipoElemen == "V") {
                sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " + "D.LecturaContador_M3, D.Funciona, D.Justificacion, D.Observaciones, D.idIncidenciaVolumetrica, " + "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica,D.CaudalMedido, " + "IV.descripcion DescIncVol, F.FUENTE_DATOS DescFuenteDato " + ",d.modificado FROM PVYCR_DatosMotores D " + "LEFT OUTER JOIN PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " + "INNER JOIN FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " + "where D.idElementoMedida = '" + idelemento + "' and D.CodigoPVYCR = '" + codigoPVYCR + "' ";

                sentenciaOrder = " order by D.codigoPVYCR, D.Fecha_Medida desc";

                if (!string.IsNullOrEmpty(sFiltro)) {
                    sentenciaSel = sentenciaSel + sFiltro + sentenciaOrder;
                } else {
                    sentenciaSel = sentenciaSel + " and Fecha_Medida between '" + fechainicio + " 00:00:00' and '" + fechaFin + " 23:59:59' " + sentenciaOrder;
                }

                da.SelectCommand.CommandText = sentenciaSel;
                da.Fill(dst, "TablaMotores");

                DataTable Tabla = default(DataTable);
                Tabla = dst.Tables["TablaMotores"].Clone();
                for (int i = dst.Tables["TablaMotores"].Rows.Count - 1; i >= 0; i += -1) {
                    Tabla.Rows.Add(dst.Tables["TablaMotores"].Rows[i].ItemArray);
                }
                dst.Tables.Remove(dst.Tables["TablaMotores"]);
                dst.Tables.Add(Tabla);

                QuitarRegistrosSegunFuenteDato("V", dst);

            } else if (tipoElemen == "H") {
                sentenciaSel = "SELECT D2.CodigoPVYCR, D2.Cod_Fuente_Dato, D2.Fecha_Medida, D2.HorasIntervalo, D2.idElementoMedida, D2.Funciona, D2.Observaciones, " + " D2.idIncidenciaVolumetrica, D2.ConsumoVolumetricoAdicional, D2.ReinicioLecturaVolumetrica, D2.descIncVol, " + " D2.DescFuenteDato, D2.Caudal_LSeg, D2.codigoMotobomba, d2.modificado " + " FROM " + " (SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," + "D.HorasIntervalo, D.idElementoMedida, " + "D.Funciona, substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " + "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " + "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato, C.Caudal_LSeg,C.codigoMotobomba " + ",d.modificado FROM PVYCR_DatosHorometros D " + "LEFT OUTER JOIN PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " + "INNER JOIN FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " + "INNER JOIN PVYCR_ElementosMedida_MotoBombas E ON E.codigoPVYCR = d.codigoPVYCR and " + "E.idelementoMedida = D.idelementomedida " + "INNER JOIN PVYCR_Motobombas C ON C.codigoMotobomba = E.codigoMotobomba and " + "C.FechaRevision = E.fechaRevision " + "where D.idElementoMedida = '" + idelemento + "' and D.CodigoPVYCR = '" + codigoPVYCR + "' AND D.Fecha_Medida BETWEEN C.FechaInicial AND ISNULL(C.FECHAFIN, GETDATE()) " + " UNION " + "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," + "D.HorasIntervalo, D.idElementoMedida, " + "D.Funciona,substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " + "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " + "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato, NULL AS Caudal_LSeg,NULL AS codigoMotobomba " + ",d.modificado FROM PVYCR_DatosHorometros D " + "LEFT OUTER JOIN PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " + "INNER JOIN FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " + "where D.idElementoMedida = '" + idelemento + "' and D.CodigoPVYCR = '" + codigoPVYCR + "' " + " AND NOT EXISTS(SELECT * FROM dbo.PVYCR_ElementosMedida_MotoBombas AS E WHERE E.codigoPVYCR = D.CodigoPVYCR AND E.idElementoMedida = D.idElementoMedida )) as D2 " + " WHERE (1=1) " + " AND NOT EXISTS (SELECT D1.CODIGOPVYCR FROM PVYCR_DATOSHOROMETROS D1 where d2.codigopvycr=d1.codigopvycr and " + "d2.idelementomedida = d1.idelementomedida and d2.cod_fuente_dato = d1.cod_fuente_dato and d2.fecha_medida = d1.fecha_medida and (d1.cod_fuente_dato <> '05' and d1.idincidenciavolumetrica in (10,11)) ) ";

                sentenciaOrder = " order by D2.codigoPVYCR, D2.Fecha_medida desc";

                if (!string.IsNullOrEmpty(sFiltro)) {
                    sentenciaSel = sentenciaSel + sFiltro + sentenciaOrder;
                } else {
                    sentenciaSel = sentenciaSel + " and D2.Fecha_Medida between '" + fechainicio + " 00:00:00' and '" + fechaFin + " 23:59:59' " + sentenciaOrder;
                }

                da.SelectCommand.CommandText = sentenciaSel;
                da.Fill(dst, "TablaHorometros");

                DataTable Tabla = default(DataTable);
                Tabla = dst.Tables["TablaHorometros"].Clone();
                for (int i = dst.Tables["TablaHorometros"].Rows.Count - 1; i >= 0; i += -1) {
                    Tabla.Rows.Add(dst.Tables["TablaHorometros"].Rows[i].ItemArray);
                }
                dst.Tables.Remove(dst.Tables["TablaHorometros"]);
                dst.Tables.Add(Tabla);

            } else if (tipoElemen == "D") {
                sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,suministroMensualM3,d.Observaciones, " + " F.FUENTE_DATOS DescFuenteDato " + "FROM PVYCR_DatosSuministros D " + "INNER JOIN FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " + "where codigoPVYCR = '" + codigoPVYCR + "' and " + "idElementoMedida = '" + idelemento + "' ";

                sentenciaOrder = " order by codigoPVYCR, Fecha_Medida desc, d.Cod_Fuente_dato ";

                if (!string.IsNullOrEmpty(sFiltro)) {
                    sentenciaSel = sentenciaSel + sFiltro + sentenciaOrder;
                } else {
                    sentenciaSel = sentenciaSel + " and Fecha_Medida between '" + fechainicio + " 00:00:00' and '" + fechaFin + " 23:59:59' " + sentenciaOrder;
                }

                da.SelectCommand.CommandText = sentenciaSel;
                da.Fill(dst, "TablaDiferencial");

                DataTable Tabla = default(DataTable);
                Tabla = dst.Tables["TablaDiferencial"].Clone();
                for (int i = dst.Tables["TablaDiferencial"].Rows.Count - 1; i >= 0; i += -1) {
                    Tabla.Rows.Add(dst.Tables["TablaDiferencial"].Rows[i].ItemArray);
                }
                dst.Tables.Remove(dst.Tables["TablaDiferencial"]);
                dst.Tables.Add(Tabla);
            }

        }
        protected void FinalizarDatasetElementos(string tipo, DataSet dst)
        {
            if ((tipo == "Q")) {
                obtenerVolumenDiferencial("Q", dst.Tables["Datos"]);
                obtenerCaudalAcumulado(dst);
            } else if ((tipo == "E")) {
                obtenerVolumenDiferencial("E",  dst.Tables["Datos"]);
                obtenerVolumenElectricoAcumulado(dst);
            } else if ((tipo == "V")) {
                obtenerVolumenDiferencial("V", dst.Tables["Datos"]);
                obtenerVolumenAcumulado(dst);
            } else if ((tipo == "H")) {
                obtenerVolumenDiferencial("H",  dst.Tables["Datos"]);
                obtenerVolumenHorometroAcumulado(dst);
            } else if ((tipo == "D")) {
                obtenerVolumenDiferencial("D", dst.Tables["Datos"]);
            }
        }
        protected double obtenerCaudalAcumulado(DataSet dst)
        {
            double volumen = 0;
            //Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
            if (dst.Tables["Datos"].Rows.Count > 0) {
                if (double.Parse(dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_Acum"].ToString()) == 0) {
                    for (int j = dst.Tables["Datos"].Rows.Count - 1; j >= 0; j += -1) {
                        if (double.Parse(dst.Tables["Datos"].Rows[j]["Diferencial_Acum"].ToString()) != 0) {
                            volumen = double.Parse(dst.Tables["Datos"].Rows[j]["Diferencial_Acum"].ToString());
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                } else {
                    volumen = double.Parse(dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_Acum"].ToString());
                }
            }

            return volumen;
        }
        protected int ObtenerConcesion(string CodigoCauce, SqlConnection conexion)
        {
            int Concesion = 0;
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();

            System.Data.SqlClient.SqlDataAdapter daConcesion = new System.Data.SqlClient.SqlDataAdapter("", conexion);
            daConcesion.SelectCommand.CommandText = " SELECT volumenMaximoAnuallegal_m3 FROM PVYCR_Cauces WHERE codigocauce='" + CodigoCauce + "'";
            try { Concesion = Int32.Parse(SicaSegura.Utiles.nullACero(daConcesion.SelectCommand.ExecuteScalar().ToString()).ToString()); }
            catch (Exception aa) { }

            return Concesion;
        }
        protected double ObtenerPorConcesion(int Concesion, double Acumulado)
        {
            double Porcentaje = 0;
            if (Acumulado != 0 & Concesion != 0) {
                Porcentaje = (100 * Acumulado / Concesion);
            }
            return Porcentaje;
        }
        protected double obtenerVolumenAcumulado(DataSet dst)
        {
            double volumen = 0;
            //Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
            if (dst.Tables["Datos"].Rows.Count > 0) {
                if (double.Parse(dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_Acum"].ToString()) != 0) {
                    volumen = double.Parse(dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_Acum"].ToString());
                }
            }
            return volumen;
        }
        protected double obtenerVolumenAcumuladoHoras(DataSet dst)
        {
            double volumen = 0;
            //Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
            if (dst.Tables["Datos"].Rows.Count > 0) {
                if (double.Parse(dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_Acum_horas"].ToString()) != 0) {
                    volumen = double.Parse(dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_Acum_horas"].ToString());
                }
            }
            return volumen;
        }
        protected void obtenerVolumenDiferencial(string tipo, DataTable Tabla)
        {
            int filas = Tabla.Rows.Count;
            if (filas == 0)
                return;

            int i = 0;
            double v_lectura_ant_horas = 0;
            double v_diferencial_horas = 0;
            double v_vol_horas = 0;
            double v_vol_ant_horas = 0;
            double v_diferencial_acum_horas = 0;
            double v_vol = 0;
            double v_vol_ant = 0;
            double v_diferencial = 0;
            double v_diferencial_kwh = 0;
            double v_kwh = 0;
            double v_caudal = 0;
            double v_caudal_ant = 0;
            double v_caudal_medio = 0;
            double v_diferencial_acum = 0;
            double v_acum_kwh = 0;
            double v_lectura_ant = 0;
            double v_diferencial_m3 = 0;
            double v_diferencial_acumm3 = 0;
            System.DateTime v_tiempo = default(System.DateTime);
            System.DateTime v_tiempo_ant = default(System.DateTime);
            int v_tiempo_medio = 0;
            string primeroNulo = "N";

            if (!Tabla.Columns.Contains("Diferencial")) {
                Tabla.Columns.Add(new DataColumn("Diferencial", Type.GetType("System.Double")));
                Tabla.Columns.Add(new DataColumn("Diferencial_Acum", Type.GetType("System.Double")));
                Tabla.Columns.Add(new DataColumn("Diferencial_AcumM3", Type.GetType("System.Double")));
                Tabla.Columns.Add(new DataColumn("Diferencial_Acum_horas", Type.GetType("System.Double")));
            }

            if (tipo == "V") {
                for (i = 0; i <= filas - 1; i++) {
                    if (i == 0) {
                        Tabla.Rows[i]["Diferencial"] = 0;
                        Tabla.Rows[i]["Diferencial_Acum"] = 0;
                        v_lectura_ant = double.Parse(Tabla.Rows[i]["LecturaContador_M3"].ToString());
                        v_vol_ant = v_lectura_ant;

                        if (object.ReferenceEquals(Tabla.Rows[i]["LecturaContador_M3"], System.DBNull.Value)) {
                            primeroNulo = "S";
                        }
                    } else {
                        if ((object.ReferenceEquals(Tabla.Rows[i]["LecturaContador_M3"], System.DBNull.Value))) {
                            v_diferencial = 0;
                            Tabla.Rows[i]["Diferencial"] = 0;
                            Tabla.Rows[i]["Diferencial_Acum"] = v_diferencial_acum;
                        } else {
                            try {v_vol = double.Parse(Tabla.Rows[i]["LecturaContador_M3"].ToString());}catch{v_vol =0;}
                            if (primeroNulo == "S") {
                                primeroNulo = "N";
                                v_diferencial = 0;
                                try{v_lectura_ant = double.Parse(Tabla.Rows[i]["LecturaContador_M3"].ToString());}catch{v_lectura_ant=0;}
                            } else {
                                if ((double.Parse(Tabla.Rows[i]["idincidenciaVolumetrica"].ToString()) == 6 )||(double.Parse(Tabla.Rows[i]["idincidenciaVolumetrica"].ToString()) == 7) ){
                                    
                                    v_diferencial = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["LecturaContador_M3"]) - (double)SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ReiniciolecturaVolumetrica"]) + (double)SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoVolumetricoAdicional"]);
                                } else if ((double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["idincidenciaVolumetrica"]) == 5 || (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["idincidenciaVolumetrica"]) == 8) {
                                    if (object.ReferenceEquals(Tabla.Rows[i]["ConsumoVolumetricoAdicional"], System.DBNull.Value)) {
                                        v_diferencial = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["LecturaContador_M3"]) + (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoVolumetricoAdicional"]) - v_lectura_ant;
                                    } else {
                                        v_diferencial = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["LecturaContador_M3"]) + (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoVolumetricoAdicional"]) - v_lectura_ant;
                                    }
                                } else {
                                    v_diferencial = v_vol - v_vol_ant;
                                }
                            }

                            v_vol_ant = v_vol;
                            v_lectura_ant = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["LecturaContador_M3"]);
                            v_diferencial_acum = v_diferencial_acum + v_diferencial;

                            Tabla.Rows[i]["Diferencial"] = v_diferencial;
                            Tabla.Rows[i]["Diferencial_Acum"] = v_diferencial_acum;
                        }
                    }
                }
            } else if (tipo == "E") {
                if (Tabla.Rows.Count > 0) {
                    for (i = 0; i <= Tabla.Rows.Count - 1; i++) {
                        if (i == 0) {
                            Tabla.Rows[i]["Diferencial"] = 0;
                            Tabla.Rows[i]["Diferencial_acum"] = 0;
                            Tabla.Rows[i]["Diferencial_acumM3"] = 0;
                            v_vol_ant = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["Total_Kwh"]);
                            v_lectura_ant = v_vol_ant;

                            if (object.ReferenceEquals(Tabla.Rows[i]["Total_Kwh"], System.DBNull.Value)) {
                                primeroNulo = "S";
                            }
                        } else {
                            if (object.ReferenceEquals(Tabla.Rows[i]["Total_Kwh"], System.DBNull.Value)) {
                                v_diferencial = 0;
                                v_diferencial_acum = v_diferencial_acum;
                                v_diferencial_acumm3 = v_diferencial_acum;
                                v_lectura_ant = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["Total_Kwh"]);

                                Tabla.Rows[i]["Diferencial"] = 0;
                                Tabla.Rows[i]["Diferencial_acum"] = (double) SicaSegura.Utiles.nullACero(v_acum_kwh);
                                Tabla.Rows[i]["Diferencial_acumM3"] = (double) SicaSegura.Utiles.nullACero(v_diferencial_acumm3);

                            } else {
                                v_vol = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["Total_Kwh"]);
                                if ((primeroNulo == "S")) {
                                    primeroNulo = "N";
                                    v_diferencial = 0;
                                    v_diferencial_m3 = 0;
                                    v_kwh = 0;
                                } else {
                                    v_lectura_ant = v_vol;

                                    if ((double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["idIncidenciaElectrica"]) == 2 || (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["idIncidenciaElectrica"]) == 3) {
                                        if (Tabla.Rows[i]["COD_FUENTE_DATO"] == "05") {
                                            v_diferencial = ((double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["Total_Kwh"]) - (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ReiniciolecturaElectrica"]) + (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoElectricoAdicional"])) * (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["relacionM3_kwh"]);
                                            v_kwh = v_diferencial;

                                        }
                                    } else if ((double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["idIncidenciaElectrica"]) == 1 || (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["idIncidenciaElectrica"]) == 4) {
                                        if ((!object.ReferenceEquals(Tabla.Rows[i]["ConsumoElectricoAdicional"], System.DBNull.Value))) {
                                            v_diferencial = ((double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["Total_Kwh"]) + (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoElectricoAdicional"]) - v_lectura_ant) * (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["relacionM3_kwh"]);
                                            v_kwh = v_diferencial;
                                        } else {
                                            v_diferencial = ((double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["Total_Kwh"]) + (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoElectricoAdicional"]) - v_lectura_ant) * (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["relacionM3_kwh"]);
                                            v_kwh = v_diferencial;
                                        }
                                    } else {
                                        v_diferencial = (v_vol - v_vol_ant) * (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["relacionM3_kwh"]);
                                        v_kwh = (v_vol - v_vol_ant);
                                    }
                                }

                                v_vol_ant = v_vol;
                                v_acum_kwh = v_acum_kwh + v_kwh;
                                v_diferencial_acum = v_diferencial_acum + v_diferencial;
                                v_diferencial_m3 = v_diferencial;
                                v_diferencial_acumm3 = v_diferencial_acum;

                                Tabla.Rows[i]["Diferencial"] = v_diferencial_m3;
                                Tabla.Rows[i]["Diferencial_acum"] = v_acum_kwh;
                                Tabla.Rows[i]["Diferencial_acumm3"] = v_diferencial_acumm3;
                            }
                        }
                    }
                }
            } else if (tipo == "Q") {
                if (Tabla.Rows.Count > 0) {
                    for (i = 0; i <= Tabla.Rows.Count - 1; i++) {
                        if (i == 0) {
                            Tabla.Rows[i]["Diferencial"] = 0;
                            Tabla.Rows[i]["Diferencial_acum"] = 0;
                            if (object.ReferenceEquals(Tabla.Rows[i]["Caudal_M3S"], System.DBNull.Value)) {
                                primeroNulo = "S";
                            } 
                        } else {
                            v_caudal = double.Parse(SicaSegura.Utiles.nullACero(Tabla.Rows[i]["Caudal_M3S"]).ToString());
                            v_caudal_ant = double.Parse(SicaSegura.Utiles.nullACero(Tabla.Rows[i - 1]["Caudal_M3S"]).ToString());
                            v_caudal_medio = (v_caudal + v_caudal_ant) / 2;

                            v_tiempo = DateTime.Parse( Tabla.Rows[i]["Fecha_medida"].ToString());
                            v_tiempo_ant = DateTime.Parse( Tabla.Rows[i - 1]["Fecha_medida"].ToString());
                            v_tiempo_medio = Math.Abs(Int32.Parse(((v_tiempo-v_tiempo_ant).TotalMinutes*60).ToString()));
                            //v_tiempo_medio = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60));
                            v_diferencial = (v_caudal_medio * v_tiempo_medio);

                            //Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
                            if (object.ReferenceEquals(Tabla.Rows[i]["Caudal_M3S"], System.DBNull.Value)) {
                                v_diferencial_acum = v_diferencial_acum;
                                Tabla.Rows[i]["Diferencial"] = 0;
                                Tabla.Rows[i]["Diferencial_acum"] = 0;
                            } else {
                                if (primeroNulo == "S") {
                                    primeroNulo = "N";
                                } else {
                                    v_diferencial_acum = v_diferencial_acum + v_diferencial;
                                    Tabla.Rows[i]["Diferencial"] = v_diferencial;
                                    Tabla.Rows[i]["Diferencial_acum"] = v_diferencial_acum;
                                }
                            }
                        }
                    }
                }
            } else if (tipo == "H") {
                if (Tabla.Rows.Count > 0) {
                    for (i = 0; i <= Tabla.Rows.Count - 1; i++) {
                        if (i == 0) {
                            Tabla.Rows[i]["Diferencial"] = 0;
                            Tabla.Rows[i]["Diferencial_Acum"] = 0;
                            Tabla.Rows[i]["Diferencial_Acum_horas"] = 0;
                            v_vol_ant =  (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["HorasIntervalo"]) * 3600 * (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["Caudal_LSeg"]) / 1000;
                            v_vol_ant_horas = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["HorasIntervalo"]);
                            v_lectura_ant = v_vol_ant;
                            if (object.ReferenceEquals(Tabla.Rows[i]["HorasIntervalo"], System.DBNull.Value)) {
                                primeroNulo = "S";
                            }
                        } else {
                            if ((primeroNulo == "S")) {
                                if ((!object.ReferenceEquals(Tabla.Rows[i]["HorasIntervalo"], System.DBNull.Value))) {
                                    primeroNulo = "N";
                                    v_diferencial = 0;
                                    v_vol_ant = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["HorasIntervalo"]) * 3600 * (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["Caudal_LSeg"]) / 1000;
                                    v_vol_ant_horas = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["HorasIntervalo"]);
                                } else {
                                    v_vol_ant = 0;
                                    v_lectura_ant = 0;
                                }
                            } else {
                                if (object.ReferenceEquals(Tabla.Rows[i]["HorasIntervalo"].ToString(), System.DBNull.Value)) {
                                    v_diferencial = 0;
                                    v_diferencial_horas = 0;
                                    v_diferencial_acum = v_diferencial_acum;
                                    v_diferencial_acum_horas = v_diferencial_acum_horas;

                                    Tabla.Rows[i]["Diferencial"] = 0;
                                    Tabla.Rows[i]["Diferencial_Acum"] = v_diferencial_acum;
                                    Tabla.Rows[i]["Diferencial_Acum_horas"] = v_diferencial_acum_horas;
                                } else {
                                    v_vol = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["HorasIntervalo"]) * 3600 * (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["Caudal_LSeg"]) / 1000;
                                    v_vol_horas = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["HorasIntervalo"]);
                                    if ((double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["idincidenciaVolumetrica"]) == 10 | (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["idincidenciaVolumetrica"]) == 11) {
                                        v_diferencial = v_vol - (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ReiniciolecturaVolumetrica"]) + (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoVolumetricoAdicional"]);
                                        v_diferencial_horas = v_vol_horas - (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ReiniciolecturaVolumetrica"]) + (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoVolumetricoAdicional"]);
                                    } else if ((double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["idincidenciaVolumetrica"]) == 9 | (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["idincidenciaVolumetrica"]) == 12) {                                        
                                        if ((!object.ReferenceEquals(Tabla.Rows[i]["ConsumoVolumetricoAdicional"], System.DBNull.Value))) {
                                            v_diferencial = v_vol + (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoVolumetricoAdicional"]) - v_lectura_ant;
                                            v_diferencial_horas = v_vol_horas + (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoVolumetricoAdicional"].ToString()) - (0 + v_lectura_ant_horas);
                                        } else {
                                            v_diferencial = v_vol +(double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoVolumetricoAdicional"]) - v_lectura_ant;
                                            v_diferencial_horas = v_vol_horas + (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["ConsumoVolumetricoAdicional"]) - v_lectura_ant_horas;
                                        }
                                    } else {
                                        v_diferencial = v_vol - v_vol_ant;
                                        v_diferencial_horas = v_vol_horas - v_vol_ant_horas;
                                    }
                                }
                                v_vol_ant = v_vol;
                                v_vol_ant_horas = v_vol_horas;
                                v_lectura_ant = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["HorasIntervalo"]) * 3600 * (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["Caudal_LSeg"]) / 1000;
                                v_lectura_ant_horas = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["HorasIntervalo"]);
                                v_diferencial_acum = v_diferencial_acum + v_diferencial;
                                v_diferencial_acum_horas = v_diferencial_acum_horas + v_diferencial_horas;

                                Tabla.Rows[i]["Diferencial"] = v_diferencial;
                                Tabla.Rows[i]["Diferencial_Acum"] = v_diferencial_acum;
                                Tabla.Rows[i]["Diferencial_Acum_horas"] = v_diferencial_acum_horas;
                            }
                        }
                    }
                }
            } else if (tipo == "D") {
                if (Tabla.Rows.Count > 0) {
                    for (i = 0; i <= Tabla.Rows.Count - 1; i++) {
                        if (object.ReferenceEquals(Tabla.Rows[i]["SuministroMensualM3"], System.DBNull.Value)) {
                            v_diferencial = 0;
                            v_diferencial_acum = v_diferencial_acum;
                            Tabla.Rows[i]["Diferencial"] = 0;
                            Tabla.Rows[i]["Diferencial_Acum"] = v_diferencial_acum;
                        } else {
                            v_vol = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["SuministroMensualM3"]);
                            v_diferencial = v_vol + v_vol_ant;
                        }
                        v_vol_ant = v_diferencial;
                        v_lectura_ant = (double) SicaSegura.Utiles.nullACero(Tabla.Rows[i]["SuministroMensualM3"]);
                        v_diferencial_acum = v_diferencial_acum + v_diferencial;
                        Tabla.Rows[i]["Diferencial"] = v_diferencial;
                        Tabla.Rows[i]["Diferencial_Acum"] = v_diferencial_acum;
                    }
                }

            }
        }
        protected double obtenerVolumenElectricoAcumulado(DataSet dst)
        {
            double volumen = 0;
            if (dst.Tables["Datos"].Rows.Count > 0) {
                if ((double) SicaSegura.Utiles.nullACero(dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_Acum"]) != 0) {
                    volumen = (double) dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_Acum"];
                }
            }
            return volumen;
        }
        protected double obtenerVolumenElectricoAcumuladoM3(DataSet dst)
        {
            double volumen = 0;
            if (dst.Tables["Datos"].Rows.Count > 0) {
                if ((double) SicaSegura.Utiles.nullACero(dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_AcumM3"]) != 0) {
                    volumen = (double) dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_AcumM3"];
                }
            }
            return volumen;
        }
        protected double obtenerVolumenHorometroAcumulado(DataSet dst)
        {
            double volumen = 0;
            if (dst.Tables["Datos"].Rows.Count > 0) {
                if ((double) SicaSegura.Utiles.nullACero(dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_Acum"]) != 0) {
                    volumen = (double) dst.Tables["Datos"].Rows[dst.Tables["Datos"].Rows.Count - 1]["Diferencial_Acum"];
                }
            }
            return volumen;
        }
        public static void QuitarRegistrosSegunFuenteDato(string tipoElemento, DataSet dst)
        {
            
            Int16 i = 0;
            int contador = 0;
            switch (tipoElemento) {
                case "V":
                    if (!dst.Tables["TablaMotores"].Columns.Contains("BorrarFila")) {
                        dst.Tables["TablaMotores"].Columns.Add("BorrarFila");
                    }

                    contador = dst.Tables["TablaMotores"].Rows.Count - 1;
                    for (i = 0; i <= contador; i++) {
                        if (((string)SicaSegura.Utiles.nullABlanco(dst.Tables["TablaMotores"].Rows[i]["cod_fuente_dato"]) != "05") & ((double)SicaSegura.Utiles.nullACero(dst.Tables["TablaMotores"].Rows[i]["idIncidenciavolumetrica"]) == 6 | (double)SicaSegura.Utiles.nullACero(dst.Tables["TablaMotores"].Rows[i]["idIncidenciavolumetrica"]) == 7))
                        {
                            if (dst.Tables["TablaMotores"].Select("cod_fuente_ato = '05' and (idincidenciavolumetrica = 6 or idincidenciavolumetrica = 7)").Length != 0) {
                                dst.Tables["TablaMotores"].Rows[i]["BorrarFila"] = "S";
                            }
                        } else {
                            dst.Tables["TablaMotores"].Rows[i]["BorrarFila"] = "N";
                        }
                    }

                    DataTable dt = default(DataTable);
                    dt = dst.Tables["TablaMotores"].Copy();
                    int filas = dt.Rows.Count - 1;
                    for (i = 0; i <= filas; i++) {
                        if ((string)SicaSegura.Utiles.nullABlanco(dt.Rows[i]["BorrarFila"]) == "S")
                        {
                            dst.Tables["TablaMotores"].Rows[i].Delete();
                        }
                    }
                    
                    break;
                case "E":
                    if (!dst.Tables["TablaAlimentacion"].Columns.Contains("BorrarFila")) {
                        dst.Tables["TablaAlimentacion"].Columns.Add("BorrarFila");
                    }

                    contador = dst.Tables["TablaAlimentacion"].Rows.Count - 1;
                    for (i = 0; i <= contador; i++) {
                        if (((string)SicaSegura.Utiles.nullABlanco(dst.Tables["TablaAlimentacion"].Rows[i]["cod_fuente_dato"]) != "05" | (string)SicaSegura.Utiles.nullABlanco(dst.Tables["TablaAlimentacion"].Rows[i]["cod_fuente_dato"]) != "13" | (string)SicaSegura.Utiles.nullABlanco(dst.Tables["TablaAlimentacion"].Rows[i]["cod_fuente_dato"]) != "10") & ((double)SicaSegura.Utiles.nullACero(dst.Tables["TablaAlimentacion"].Rows[i]["idIncidenciaelectrica"]) == 2 | (double)SicaSegura.Utiles.nullACero(dst.Tables["TablaAlimentacion"].Rows[i]["idIncidenciaelectrica"]) == 3))
                        {
                            if (dst.Tables["TablaAlimentacion"].Select("cod_fuente_dato = '05' and (idIncidenciaelectrica = 2 or idIncidenciaelectrica = 3)").Length != 0) {
                                dst.Tables["TablaAlimentacion"].Rows[i]["BorrarFila"] = "S";
                            }
                        }
                    }

                    dt = default(DataTable);
                    dt = dst.Tables["TablaAlimentacion"].Copy();
                    filas = dt.Rows.Count - 1;
                    for (i = 0; i <= filas; i++) {
                        if ((string)SicaSegura.Utiles.nullABlanco(dt.Rows[i]["BorrarFila"]) == "S")
                        {
                            dst.Tables["TablaAlimentacion"].Rows[i].Delete();
                        }
                    }

                    break;
                case "H":
                    if (!dst.Tables["TablaHorometros"].Columns.Contains("BorrarFila")) {
                        dst.Tables["TablaHorometros"].Columns.Add("BorrarFila");
                    }

                    contador = dst.Tables["TablaHorometros"].Rows.Count - 1;
                    for (i = 0; i <= contador; i++) {
                        if (((string)SicaSegura.Utiles.nullABlanco(dst.Tables["TablaHorometros"].Rows[i]["cod_fuente_dato"]) != "05" | (string)SicaSegura.Utiles.nullABlanco(dst.Tables["TablaHorometros"].Rows[i]["cod_fuente_dato"]) != "13" | (string)SicaSegura.Utiles.nullABlanco(dst.Tables["TablaHorometros"].Rows[i]["cod_fuente_dato"]) != "10"))
                        {
                            dst.Tables["TablaHorometros"].Rows[i]["BorrarFila"] = "S";
                        }
                    }

                    dt = default(DataTable);
                    dt = dst.Tables["TablaAlimentacion"].Copy();
                    filas = dt.Rows.Count - 1;
                    for (i = 0; i <= filas; i++) {
                        if ((string)SicaSegura.Utiles.nullABlanco(dt.Rows[i]["BorrarFila"]) == "S")
                        {
                            dst.Tables["TablaAlimentacion"].Rows[i].Delete();
                        }
                    }

                    break;
            }
        }
    }



}
