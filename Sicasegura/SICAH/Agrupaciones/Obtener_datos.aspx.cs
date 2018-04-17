using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Net;//Añadido para ftp
using GuarderiaFluvial;



public partial class Obtener_datos : System.Web.UI.Page 
{
    DirectoryInfo DIR;
    string documentoJson;
    string inicio, paginacion, ordenadopor, orden;
    protected void Page_Load(object sender, EventArgs e)
    {
        string tipo = Request.QueryString["tipo"];
        string solucion="";
        DataSet ds = new DataSet();
        string ficheroRespuesta = @"C:\Temp\" + tipo + "-" + DateTime.Now.Ticks.ToString() + ".dat";
        // Generamos la salida en el fichero establecido
        string sql = "";
        string json = "";
        //HttpContext.Current.Session.Add("Filtro", "");
        if (Request["filtros"] != null)
        {
            if (Request["filtros"].Contains("BORRAFILTROS"))
            {
                HttpContext.Current.Session.Add("Filtro", "");
            }
            else
            {
                HttpContext.Current.Session.Add("Filtro", Request["filtros"]);
            }
        }
        else
        {
            if (HttpContext.Current.Session["Filtro"] == null)
            {
                HttpContext.Current.Session.Add("Filtro", "");
            }
        }
        using (StreamWriter salida = new StreamWriter(ficheroRespuesta, false, System.Text.Encoding.UTF8))
        {
            if (tipo != null)
            {
                switch (tipo)
                {
                    case "combo_tipo":
                        if (Request["valor"] == null)
                        {
                            sql = "SELECT DISTINCT " + Request["campo"] + " FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos] Where revision like 'VIGENTE' order by " + Request["campo"];
                        }
                        else sql = "SELECT DISTINCT " + Request["campo"] + " FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos] Where revision like 'VIGENTE' AND " + Request["campo"] + " like '%" + Request["valor"] + "%' order by " + Request["campo"];
                        ds = cargarDatosBD(sql);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            json="";
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                json += ",{id:'" + ds.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + "',valor:'" + ds.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + "'}";       
                            }
                        }
                        if (json == "") json = ",";
                        salida.WriteLine("["+json.Substring(1)+"]");
                        break;
                }
            }
            else
            {
                string a = Convert.ToString(Request.Form.ToString());
                inicio = Request["start"];
                paginacion = Request["limit"];
                ordenadopor = Request["sort"];
                orden = Request["dir"];
                if (HttpContext.Current.Session["Filtro"] != "")
                {
                    sql = "SELECT * FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos] Where revision like 'VIGENTE' AND " + HttpContext.Current.Session["Filtro"].ToString().Replace("#", "'") + " order by  " + ordenadopor + " " + orden;
                    if (Request["filtros"]!= null && Request["filtros"].Contains("BORRA"))
                    {
                        if (ordenadopor == "Expr1")
                        { 
                            sql = "SELECT * FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos] Where revision like 'VIGENTE' order by ID desc, " + ordenadopor + " " + orden; 
                        }
                        else 
                        { 
                            sql = "SELECT * FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos] Where revision like 'VIGENTE' order by  " + ordenadopor + " " + orden; 
                        }  
                    }
                }
                else 
                {
                    if (ordenadopor == "Expr1")
                    { sql = "SELECT * FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos] Where revision like 'VIGENTE' order by ID desc, " + ordenadopor + " " + orden; }
                    else { sql = "SELECT * FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos] Where revision like 'VIGENTE' order by  " + ordenadopor + " " + orden; }                  
                }
                //string sql = "SELECT TOP (100) PERCENT CONVERT(INT, SICA_SIST_AprovechamientosInscritos.NUMPAL) AS Expr1, PVYCR_AprovechamientosExcedidos.Fecha_deteccion, SICA_SIST_AprovechamientosInscritos.Procedencia,  SICA_SIST_AprovechamientosInscritos.Rel_NUMPAL, SICA_SIST_AprovechamientosInscritos.RevisionFROM   SICA_SIST_AprovechamientosInscritos LEFT JOIN PVYCR_AprovechamientosExcedidos ON SICA_SIST_AprovechamientosInscritos.NUMPAL = PVYCR_AprovechamientosExcedidos.Id_aprovechamientoORDER BY Expr1";
                //sql = "SELECT * FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos]order by " + ordenadopor + " " + orden;
                salida.WriteLine(Get_array_aprovechamientos_inscritos(GetDataDBSICA(sql)));
                Context.Response.ContentType = "text/json";
                //salida.WriteLine(Get_array_aprovechamientos_inscritos(GetDa taDBSICA("SELECT * FROM SICA_VW_SIST_Aprovechamientos_Inscritos")));

            }
        }
        // Devolvemos lo generado en el fichero de respuesta
        Context.Response.WriteFile(ficheroRespuesta);
        Context.Response.Flush();
        //File.Delete(ficheroRespuesta);
    }

    private string Get_array_aprovechamientos_inscritos(DataTable dt)
    {
        string configurado_en_SICA = "";
        string descripcion="";
        string Volumenlegal="0";
        string Consumido="0";
        string Concesion = "0";
        string Porcent="0";
        string html = "";

        int hasta = Convert.ToInt16(inicio) + Convert.ToInt16( paginacion);
        if (dt.Rows.Count < 20) { hasta = Convert.ToInt16(dt.Rows.Count); }
        html += "{success: true, results: " + obtenerTotalRegistros() + ", rows: [";
        for (int i = Convert.ToInt16(inicio); i < hasta ; i++)
        {
            if (dt.Rows[i].ItemArray.GetValue(19).ToString().Trim() != "")
            {
                try
                {
                    configurado_en_SICA = dt.Rows[i].ItemArray.GetValue(19).ToString().Trim();
                    Consumido = this.CalculoTotales(Convert.ToInt32(dt.Rows[i].ItemArray.GetValue(0).ToString().Trim()));
                    Consumido = Convert.ToString(System.Math.Round(Convert.ToDouble(Consumido),0));
                    Porcent = Convert.ToString(System.Math.Round((Convert.ToDouble(Consumido) * 100) / Convert.ToInt32(dt.Rows[i].ItemArray.GetValue(14).ToString().Trim()),0)).Replace(",",".");
                    Consumido = Consumido.Replace(",", ".");
                }
                catch (Exception ee) { }
            }
            else
            {
                configurado_en_SICA = "NO";
            }

            Concesion = dt.Rows[i].ItemArray.GetValue(14).ToString().Trim().Replace(',', '.');
            if (Concesion == "") { Concesion = "0"; }
            //Controlamos que la fecha no sea nula
            if (dt.Rows[i].ItemArray.GetValue(16).ToString().Trim() == "") { html += "{Fecha_Inscripcion:'01/01/1999',"; }
            else { html += "{Fecha_Inscripcion:'" + dt.Rows[i].ItemArray.GetValue(16).ToString().Trim().Substring(0, 10) + "',"; }           
            html += "Procedencia:'" + dt.Rows[i].ItemArray.GetValue(4).ToString().Trim() + "',";
            html += "SEC:'" + dt.Rows[i].ItemArray.GetValue(1).ToString().Trim() + "',";
            html += "TOMO:'" + dt.Rows[i].ItemArray.GetValue(2).ToString().Trim() + "',";
            html += "HOJA:'" + dt.Rows[i].ItemArray.GetValue(3).ToString().Trim() + "',";
            html += "ACUIF:'" + dt.Rows[i].ItemArray.GetValue(5).ToString().Trim() + ".',";
            html += "LUGAR:'" + dt.Rows[i].ItemArray.GetValue(6).ToString().Trim().Replace(',', ' ').Replace('"', '`').Replace('/', '-').Replace('(', '-').Replace(')', '-') + ".',";
            html += "TERMI:'" + dt.Rows[i].ItemArray.GetValue(7).ToString().Trim() + ".',";
            html += "PROVI:'" + dt.Rows[i].ItemArray.GetValue(8).ToString().Trim() + ".',";
            html += "Expr1:" + dt.Rows[i].ItemArray.GetValue(0).ToString().Trim() + ",";
            html += "Concesion:" + Concesion + ",";
            html += "Consumido:" + Consumido + ",";
            html += "SICA:'" + configurado_en_SICA + "',";
            html += "Exceso:" + Porcent + "},";
            Consumido = "0";
            Porcent = "0";
        }
        html = html.Substring(0, html.Length - 1);
        html += "]}";
        return html;
    }

    private string obtenerTotalRegistros()
    {
        string valor = "";
        string SQL = "";
        try
        {
            if (Request["filtros"] == null)
            {
                if (HttpContext.Current.Session["Filtro"] == "")
                { SQL = "SELECT COUNT(*) FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos] Where revision like 'VIGENTE'"; 
                }
                else
                { SQL = "SELECT COUNT(*) FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos] Where revision like 'VIGENTE' AND " + HttpContext.Current.Session["Filtro"].ToString().Replace("#", "'") ;  }
            }
            else
            {
                //if (Request["filtros"].Contains("BORRA"))
                if (true)
                { SQL = "SELECT COUNT(*) FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos] Where revision like 'VIGENTE'"; }
            }
        }
        catch { SQL = "SELECT COUNT(*) FROM [DBSICA].[dbo].[SICA_VW_SIST_Aprovechamientos_Inscritos] Where revision like 'VIGENTE'"; }


        DataTable dt = new DataTable();
        dt = GetDataDBSICA(SQL);
        valor = dt.Rows[0].ItemArray.GetValue(0).ToString().Trim();
        return valor;
        
 
    }
    private string ObtenerDatoRaacs(int ID, string Campo)
    {
        DataTable dt = GetDataRAACS("SELECT " + Campo + " FROM AprovechamientosInscritos WHERE NUMPAL = " + ID );
        return dt.Rows[0].ItemArray.GetValue(0).ToString().Trim();
    }
    private string ObtenerVolumenInscripcion (int NumInscripcion)
    {    
        double VolumenTotal=0;
        DataTable dtRegistro  = new System.Data.DataTable();

        try
        {
                //Si la Agrupación es RAAC los datos, se cogen de la inscripción
                //'Es la suma total de los volúmenes de abastecimiento, riego...
                //'utiles.Comprobar_Conexion_BD(Page, conexionRegistro)
                //'daRegistro.SelectCommand.CommandText = "Select * from AprovechamientosInscritos WHERE NUMPAL=" & NumInscripcion
                //'daRegistro.Fill(dstRegistro, "TablaRegistro")
                dtRegistro = GetDataRAACS("Select * from AprovechamientosInscritos WHERE NUMPAL=" + NumInscripcion);
                if (dtRegistro.Rows.Count > 0)
                {
                    VolumenTotal += Convert.ToDouble(dtRegistro.Rows[0]["VOLRE"]) + Convert.ToDouble(dtRegistro.Rows[0]["VOLAB"]) + Convert.ToDouble(dtRegistro.Rows[0]["VOLOT"]);
                }
                
                return VolumenTotal.ToString();
                //Return String.Format("{0:#,##0.##}", VolumenTotal)
        }

        catch (Exception ee)
        {
                return "0";
        }
    }
    private string CalculoTotales(int idagrupacion)
    {
        System.Data.SqlClient.SqlConnection conexion = new System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings["dsnsegura_migracion"]);

        System.Data.SqlClient.SqlConnection conexionDBSica = new System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings["dsn_dbsica"]);
        System.Data.SqlClient.SqlDataAdapter daAgrupaciones = new System.Data.SqlClient.SqlDataAdapter("", conexionDBSica);
        string sentencia = "";
        sentencia = "SELECT     [SICA_SIST_Sistemas-Punto].ID_Sistema, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM, SICA_SIST_Sistemas.NumInscripcion " +
                 " FROM         SICA_SIST_PuntoSistema INNER JOIN [SICA_SIST_Sistemas-Punto] ON SICA_SIST_PuntoSistema.ID = [SICA_SIST_Sistemas-Punto].ID_PuntoSistema" +
                 " INNER JOIN     SICA_SIST_Sistemas ON    SICA_SIST_Sistemas.ID=[SICA_SIST_Sistemas-Punto].ID_Sistema" +
                 " WHERE [SICA_SIST_Sistemas].NumInscripcion='" + idagrupacion + "'";


        DataSet dstPuntos = new DataSet();
        utiles.Comprobar_Conexion_BD(Page, conexionDBSica);
        daAgrupaciones.SelectCommand.CommandText = sentencia;
        daAgrupaciones.Fill(dstPuntos, "TablaPuntos");

        string cadena = "";

        Sicadll.AccesoADatos.AccesoADatos2 utiliSica = new Sicadll.AccesoADatos.AccesoADatos2();

        double volumenGeneral = 0;
        int concesionAprovechamiento = 0;
        double PorcentajeTotal = 0;

        if (dstPuntos.Tables["TablaPuntos"].Rows.Count > 0)
        {
            //'Se comprueba las fechas
            foreach (DataRow fila in dstPuntos.Tables["TablaPuntos"].Rows)
            {
                object[] resultado;
                //'Se obtienen los resultados de cada uno de los elementos de medida que
                //'componen el punto
                string FI;
                if (DateTime.Now.Month < 10)
                {
                    FI = "01/10/" + Convert.ToString(DateTime.Now.Year - 1);
                }
                else
                {
                    FI = "01/10/" + DateTime.Now.Year.ToString();
                }
                try
                {
                    resultado = utiliSica.obtenerAcumulado(fila["codigoPVYCR"].ToString(), fila["EM"].ToString(), conexion, FI, DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                    //'Se calculan los acumulados
                    volumenGeneral += Convert.ToDouble(resultado[1].ToString());

                    if (fila["NumInscripcion"].ToString() == "")
                    {
                        concesionAprovechamiento += Convert.ToInt16(resultado[2]);
                    }
                    PorcentajeTotal += Convert.ToDouble(resultado[3]);
                }
                catch (Exception ee)
                {
                }
                

            }
            
        }
        return volumenGeneral.ToString();
    }
    private string ObtenerDescripcionAgrupacion(int ID)
    {
        //'obtenemos la descripción de la Agrupación

        string sentencia = "select Descripcion from SICA_SIST_Sistemas where NumInscripcion = " + ID;

        DataTable dt = GetDataDBSICA(sentencia);
        //'utiles.Comprobar_Conexion_BD(Page, conexionDBSica)
        //'daAgrupaciones.SelectCommand.CommandText = sentencia
        //'Dim Descripcion As String = daAgrupaciones.SelectCommand.ExecuteScalar
        //'utiles.CerrarConexion(conexionDBSica)
        string Descripcion = dt.Rows[0].ItemArray.GetValue(0).ToString();
        
        if (Descripcion == "") return "";
        else return Descripcion;
    }
    private DataTable GetDataDBSICA(string SQL)
    {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["DBSICA"].ConnectionString;
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cadenaConexion);
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(SQL, connection);
            try
            {
                adapter.Fill(dt);
                
            }
            catch (Exception ee) { }
            return dt;
    }
    private DataTable GetDataRAACS(string SQL)
    {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["RAACS"].ConnectionString;
            DataTable dt = new DataTable();
            try
            {

                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cadenaConexion);
                System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ee) { }
            return dt;
    }
    static void WalkDirectoryTree(System.IO.DirectoryInfo root, System.Collections.ArrayList jsonDocumentos)
    {
        System.IO.DirectoryInfo[] subDirs = null;
        // First, process all the files directly under this folder
        // Now find all the subdirectories under this directory.
        subDirs = root.GetDirectories();
        if (subDirs.Length == 0)
        {
            int valorstring = jsonDocumentos[0].ToString().LastIndexOf("leaf");
            jsonDocumentos[0] = jsonDocumentos[0].ToString().Substring(0, valorstring) + "leaf:true},";
        }
        else
        {
            foreach (System.IO.DirectoryInfo dirInfo in subDirs)
            {
                jsonDocumentos[0] += "{text:'" + dirInfo.Name + "',id:'" + dirInfo.Name + "',ruta:'" + dirInfo.FullName.Replace('\\', '/') + "',iconCls:'x-tree-node-collapsed',leaf:false,children:[";
                WalkDirectoryTree(dirInfo, jsonDocumentos);
                jsonDocumentos[0] += "]},";
            }
        }
    }
    public static DataSet cargarDatosBD(string sql)
    {
        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["DBSICA"].ConnectionString);
        DataSet ds = new DataSet();
        System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(sql, connection);
        try
        {
            adapter.Fill(ds);
        }
        catch (Exception ee) { }
        return ds;
    }
}
