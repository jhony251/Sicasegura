using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SicaSegura
{
    /// <summary>
    /// Clase que define un conjuto de puntos de control del SICA
    /// </summary>
    public class SICA_Agrupacion
    {
        #region VARS
        private int NumInscripcion, IdAgrupacion, IDSistema;
        private double Concesion_inscrita;
        private double Concesion_modificada;
        private double Modificacion_concesion;
        private double Consumido;
        private string[] PuntosControl;
        private string Descripcion;
        private bool Raacs;
        private string EXP_ISM;
        private System.Data.SqlClient.SqlConnection conexion = new System.Data.SqlClient.SqlConnection((new SicaSegura.SICA_DB()).GetCadenaConexionSIGVECTOR());
        #endregion


        public int Get_NumeroInscripcion() { return NumInscripcion; }
        public int Get_IDSistema() { return IDSistema; }
        public int Get_IDAgrupacion() { return IdAgrupacion; }
        public double Get_ConcesionInscrita() { return Concesion_inscrita; }
        public double Get_ConcesionModificada() { return Concesion_modificada; }
        public double Get_ConcesionTemporal() { return Modificacion_concesion; }
        public double Get_Consumido() { return Consumido; }
        public string[] Get_PuntosDeContros() { return PuntosControl; }
        public string Get_Descripcion() { return Descripcion; }
        public string Get_ExpedienteISM() { return EXP_ISM; }
        public bool EsDelRaacs() { return Raacs; }
        public DateTime Get_FechaUltimaLecturaConsolidadAgrupacion() { return obtenerFechaUltimaLecturaConsolidadaAgrupacion(NumInscripcion); }

        /// <summary>
        /// Constructor de la agrupacion 
        /// </summary>
        /// <param name="descripcion">Cadena de texto con la descripción de la Agrupación</param>
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
        /// <summary>
        /// Funcion para obtener de BBDD el Expediente ISM
        /// </summary>
        /// <returns>Devuelve un String con el valor</returns>
        private string cargar_ExpedienteISM()
        {

            SICA_DB DB = new SICA_DB();
            return DB.GetDataDBSICA("SELECT EXP_ISM FROM SICA_Seguimiento_Expedientes_ISM WHERE (INSCRIPCION LIKE '" + NumInscripcion + "')").Rows[0][0].ToString();

        }
        /// <summary>
        /// Constructor de la agrupacion
        /// </summary>
        /// <param name="inscripcion">Entero que define el número de inscripción</param>
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
            IDSistema = ObtenerIDSistema();
            EXP_ISM = cargar_ExpedienteISM();
        }
        private int ObtenerIDSistema()
        {
            SICA_DB DB = new SICA_DB();
            string SQL = "SELECT DISTINCT ID, NumInscripcion FROM SICA_SIST_Sistemas WHERE (NumInscripcion = " + NumInscripcion.ToString() + ")";
            return int.Parse(DB.GetDataDBSICA(SQL).Rows[0][0].ToString());
        }
        /// <summary>
        /// Funcion para devolver la descripción de una Agrupación en base al número de inscripción
        /// </summary>
        /// <param name="NumInscripcion">Entero que define el número de inscripción</param>
        /// <returns>Cadena de texto correspondiente a la Descripción</returns>
        private string ObtenerDescripcion(int NumInscripcion)
        {
            SicaSegura.SICA_DB DB = new SICA_DB();
            DataTable dt = DB.GetDataDBSICA("SELECT Descripcion FROM SICA_SIST_Sistemas WHERE [SICA_SIST_Sistemas].NumInscripcion=" + NumInscripcion + "");
            return dt.Rows[0].ItemArray.GetValue(0).ToString();
        }
        /// <summary>
        /// Obtención del identificador a nivel de BD de una agrupación
        /// </summary>
        /// <param name="descripcion">Descripción de la agrupación buscada</param>
        private void GetIdAgrupacion(string descripcion)
        {
            string sentencia = "";
            sentencia = "SELECT ID FROM SICA_SIST_Sistemas WHERE [SICA_SIST_Sistemas].Nombre like '" + descripcion + "'";
            SicaSegura.SICA_DB DB = new SICA_DB();
            DataTable dt = DB.GetDataDBSICA(sentencia);
            IdAgrupacion = Convert.ToInt16(dt.Rows[0][0]);

        }
        /// <summary>
        /// Obtención del identificador a nivel de BD de una agrupación
        /// </summary>
        /// <param name="NumInscripcion">Número de Inscripción de la agrupación buscada</param>
        private void GetIdAgrupacion(int NumInscripcion)
        {
            string sentencia = "";
            sentencia = "SELECT ID FROM SICA_SIST_Sistemas WHERE [SICA_SIST_Sistemas].NumInscripcion=" + NumInscripcion + "";
            SicaSegura.SICA_DB DB = new SICA_DB();
            DataTable dt = DB.GetDataDBSICA(sentencia);
            IdAgrupacion = Convert.ToInt16(dt.Rows[0][0]);

        }
        /// <summary>
        /// Funcion que nos devolverá un objeto fecha con el valor de la última lectura que haya entrado al sistema SICA y haya sido consolidada
        /// </summary>
        /// <param name="NumInscripcion">Inscripción para la que buscamos la fecha de la última lectura consolidada</param>
        /// <returns>Objeto DateTime con el valor buscado</returns>
        private DateTime obtenerFechaUltimaLecturaConsolidadaAgrupacion(int NumInscripcion)
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
            DateTime ultimaFecha;
            ultimaFecha = DateTime.Parse("01/01/0001");
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
                        string codigo = fila["codigoPVYCR"].ToString().Trim();
                        string EM = fila["EM"].ToString().Trim();
                        //resultado = punto.obtenerAcumulado(fila["codigoPVYCR"].ToString().Trim(), fila["EM"].ToString().Trim(), conexion, FI.ToShortDateString(), FF.ToShortDateString());
                        //'Se calculan los acumulados
                        //volumenGeneral += Convert.ToDouble(resultado[1].ToString()); ;
                        if (fila["NumInscripcion"].ToString() != "")
                        {
                            string SQL = "";
                            string SQLFILTROCFD = " AND(cod_fuente_dato like'05' OR cod_fuente_dato like'10' OR cod_fuente_dato like'01' OR cod_fuente_dato like'13') ";
                            //concesionAprovechamiento += Convert.ToDouble(resultado[2].ToString());
                            switch (EM.Substring(0, 1).ToUpper())
                            {
                                case "V":
                                    SQL = "Select TOP(1) fecha_medida from PVYCR_Datosmotores where codigopvycr like '" + codigo + "' AND idelementomedida like '" + EM + "' " + SQLFILTROCFD + "  ORDER by fecha_medida desc";
                                    break;
                                case "E":
                                    SQL = "Select TOP(1) fecha_medida from PVYCR_Datosalimentacion where codigopvycr like '" + codigo + "' AND idelementomedida like '" + EM + "'  " + SQLFILTROCFD + "  ORDER by fecha_medida desc";
                                    break;
                                case "H":
                                    SQL = "Select TOP(1) fecha_medida from PVYCR_Datoshorometros where codigopvycr like '" + codigo + "' AND idelementomedida like '" + EM + "'  " + SQLFILTROCFD + "  ORDER by fecha_medida desc";
                                    break;
                                case "Q":
                                    SQL = "Select TOP(1) fecha_medida from PVYCR_DatosAcequias where codigopvycr like '" + codigo + "' AND idelementomedida like '" + EM + "'  " + SQLFILTROCFD + "  ORDER by fecha_medida desc";
                                    break;
                            }
                            DateTime temp = DateTime.Parse(DB.GetDataSIGVECTOR(SQL).Rows[0][0].ToString());
                            if (ultimaFecha > temp) { } else { ultimaFecha = temp; }
                        }

                        //PorcentajeTotal += Convert.ToDouble(resultado[3].ToString());
                    }
                }
            }
            else
            {
            }

            return ultimaFecha;
        }
        /// <summary>
        /// Función para obtener el Volumen que viene reflejado en la hoja de inscripción del aprovechamiento
        /// </summary>
        /// <param name="NumInscripcion">Entero que define el número de inscripcion para la que buscamos el volumen</param>
        /// <returns>Retorna un valor Double con el Volumen inscrito</returns>
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
                    VolumenTotal += Convert.ToDouble(dtRegistro.Rows[0]["VOLRE"]) + Convert.ToDouble(dtRegistro.Rows[0]["VOLAB"]) + Convert.ToDouble(dtRegistro.Rows[0]["VOLOT"]) + Convert.ToDouble(dtRegistro.Rows[0]["VOLIN"]) + Convert.ToDouble(dtRegistro.Rows[0]["VOLGA"]) + Convert.ToDouble(dtRegistro.Rows[0]["VOLHI"]);
                }
                return VolumenTotal;
                //Return String.Format("{0:#,##0.##}", VolumenTotal)
            }

            catch (Exception ee)
            {
                return 0;
            }
        }
        /// <summary>
        /// Función para obtener el Volumen modificador que viene reflejado en la hoja de inscripción del aprovechamiento
        /// </summary>
        /// <param name="NumInscripcion">Entero que define el número de inscripcion para la que buscamos el volumen</param>
        /// <returns>Retorna un valor Double con el Volumen inscrito</returns>
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
        /// <summary>
        /// Funcion para obtener en M3 el volumen computado para un aprovechamiento entre 2 fecha.
        /// </summary>
        /// <param name="NumInscripcion">Entero con el valor numerico que define la inscripcion a la que queremos calcular el volumen</param>
        /// <param name="FI">Fecha de inicio del periodo a calcular</param>
        /// <param name="FF">Fecha de fin del periodo a calcular</param>
        /// <returns>Devuelve un objeto Double con el volor numérico computado</returns>
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
        /// <summary>
        /// Funcion para obtener en M3 el volumen computado para un aprovechamiento desde el inicio de Año hidrológico
        /// </summary>
        /// <returns>Devuelve un objeto Double con el volor numérico computado</returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Obtener Trio de Codigo SICA, EM y SIGNO Respecto a la agrupación
        /// </summary>
        /// <returns>DataTable con el array de datos en este orden SICA//EM//SIGNO</returns>
        public DataTable Get_ListadoPuntosConSigno()
        {
            SicaSegura.SICA_DB DB = new SICA_DB();
            string sentencia = " SELECT        SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM, [SICA_SIST_Sistemas-Punto].Signo";
            sentencia += " FROM            [SICA_SIST_Sistemas-Punto] INNER JOIN";
            sentencia += " SICA_SIST_Sistemas ON [SICA_SIST_Sistemas-Punto].ID_Sistema = SICA_SIST_Sistemas.ID INNER JOIN";
            sentencia += " SICA_SIST_PuntoSistema ON [SICA_SIST_Sistemas-Punto].ID_PuntoSistema = SICA_SIST_PuntoSistema.ID";
            sentencia += " WHERE        ([SICA_SIST_Sistemas-Punto].ID_Sistema = " + IDSistema + ")";

            return DB.GetDataDBSICA(sentencia);
        }


    }
}