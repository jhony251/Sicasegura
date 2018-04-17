using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Conexiones_SQL
{
    public class ConexionSQL
    {
        public ConexionSQL()
        {
        }
        public SqlConnection Crear_Conexion(string IP_del_servidor, string Nombre_de_la_Base_de_Datos, string Usuario, string Contrasenya)
        {
            string cadena;
            cadena = "Server=" + IP_del_servidor.Trim() + "; database=" + Nombre_de_la_Base_de_Datos.Trim() + "; Password=" + Contrasenya.Trim() + ";Persist Security Info=True;User ID=" + Usuario.Trim() +";";
            SqlConnection cnn = new SqlConnection(cadena);
            return cnn;
        }
        public SqlConnection Crear_Conexion(string Cadena_de_conexion_completa)
        {
            SqlConnection cnn = new SqlConnection(Cadena_de_conexion_completa);
            return cnn;
        }
        public SqlDataAdapter Ejecutar_SQL(SqlConnection cnn, string SQL)
        {
            SqlDataAdapter da = new SqlDataAdapter(SQL, cnn);
            return da;
        }
        public DataSet Convertir_SQLDA_en_DS(SqlDataAdapter da)
        {
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

    }
}
