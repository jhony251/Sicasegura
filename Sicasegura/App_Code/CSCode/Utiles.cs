using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SicaSegura
{
    /// <summary>
    /// Descripción breve de Utiles
    /// </summary>
    public class Utiles
    {
        public static object nullABlanco(object valor)
        {//'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
            if(valor == DBNull.Value)
            {
                return "";
            }
            else{ return valor;}
            
        }
        public static object nullACero(object valor)
        {//'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
            if(valor == DBNull.Value)
            {
                return 0;
            }
            else{ return valor;}
            
        }
        public static object nullAFalse(object valor)
        {//'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
            if(valor == DBNull.Value)
            {
                return false;
            }
            else{ return valor;}
            
        }
        public static object nullATrue(object valor)
        {//'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
            if(valor == DBNull.Value)
            {
                return true;
            }
            else{ return valor;}
            
        }
        public DateTime Fecha_inicio_anyo_hidrologico_actual() { if (DateTime.Now.Month < 10) { return DateTime.Parse("01/10/" + DateTime.Now.AddYears(-1).Year.ToString()); } else { return DateTime.Parse("01/10/" + DateTime.Now.Year.ToString()); } }
        public DateTime Fecha_fin_anyo_hidrologico_actual() { if (DateTime.Now.Month < 10) { return DateTime.Parse("01/10/" + DateTime.Now.Year.ToString()); } else { return DateTime.Parse("01/10/" + DateTime.Now.AddYears(1).Year.ToString()); } }
        public DateTime Fecha_inicio_anyo_hidrologico_anterior() { if (DateTime.Now.Month < 10) { return DateTime.Parse("01/10/" + DateTime.Now.AddYears(-2).Year.ToString()); } else { return DateTime.Parse("01/10/" + DateTime.Now.AddYears(-1).Year.ToString()); } }
        public DateTime Fecha_fin_anyo_hidrologico_anterior() { if (DateTime.Now.Month < 10) { return DateTime.Parse("01/10/" + DateTime.Now.AddYears(-1).Year.ToString()); } else { return DateTime.Parse("01/10/" + DateTime.Now.Year.ToString()); } }
        public DateTime Fecha_inicio_anyo_hidrologico(string anyo) { return DateTime.Parse("01/10/" + anyo); }
        public DateTime Fecha_fin_anyo_hidrologico(string anyo) { return DateTime.Parse("01/10/" + (Int16.Parse(anyo)+1).ToString()); }
    }
    public class UtilesIP
    {
        public string DetermineIP(HttpContext context)
        {
            if (context.Request.ServerVariables.AllKeys.Contains("HTTP_CLIENT_IP") && CheckIP(context.Request.ServerVariables["HTTP_CLIENT_IP"]))
                return context.Request.ServerVariables["HTTP_CLIENT_IP"];
            if (context.Request.ServerVariables.AllKeys.Contains("HTTP_X_FORWARDED_FOR"))
                foreach (string ip in context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(','))
                    if (CheckIP(ip.Trim()))
                        return ip.Trim();
            if (context.Request.ServerVariables.AllKeys.Contains("HTTP_X_FORWARDED") && CheckIP(context.Request.ServerVariables["HTTP_X_FORWARDED"]))
                return context.Request.ServerVariables["HTTP_X_FORWARDED"];
            if (context.Request.ServerVariables.AllKeys.Contains("HTTP_X_CLUSTER_CLIENT_IP") && CheckIP(context.Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"]))
                return context.Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"];
            if (context.Request.ServerVariables.AllKeys.Contains("HTTP_FORWARDED_FOR") && CheckIP(context.Request.ServerVariables["HTTP_FORWARDED_FOR"]))
                return context.Request.ServerVariables["HTTP_FORWARDED_FOR"];
            if (context.Request.ServerVariables.AllKeys.Contains("HTTP_FORWARDED") && CheckIP(context.Request.ServerVariables["HTTP_FORWARDED"]))
                return context.Request.ServerVariables["HTTP_FORWARDED"];
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        private bool CheckIP(string ip)
        {
            if (!String.IsNullOrEmpty(ip))
            {
                long ipToLong = -1;
                //Is it valid IP address
                if (TryConvertIPToLong(ip, out ipToLong))
                {
                    //Does it fall within a private network range
                    foreach (long[] privateIp in _privateIps)
                        if ((ipToLong >= privateIp[0]) && (ipToLong <= privateIp[1]))
                            return false;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        private bool TryConvertIPToLong(string ip, out long ipToLong)
        {
            try
            {
                ipToLong = ConvertIPToLong(ip);
                return true;
            }
            catch
            {
                ipToLong = -1;
                return false;
            }
        }
        static long ConvertIPToLong(string ip)
        {
            string[] ipSplit = ip.Split('.');
            return (16777216 * Convert.ToInt32(ipSplit[0]) + 65536 * Convert.ToInt32(ipSplit[1]) + 256 * Convert.ToInt32(ipSplit[2]) + Convert.ToInt32(ipSplit[3]));
        }
        private long[][] _privateIps = new long[][] {
              new long[] {ConvertIPToLong("0.0.0.0"), ConvertIPToLong("2.255.255.255")},
              new long[] {ConvertIPToLong("10.0.0.0"), ConvertIPToLong("10.255.255.255")},
              new long[] {ConvertIPToLong("127.0.0.0"), ConvertIPToLong("127.255.255.255")},
              new long[] {ConvertIPToLong("169.254.0.0"), ConvertIPToLong("169.254.255.255")},
              new long[] {ConvertIPToLong("172.16.0.0"), ConvertIPToLong("172.31.255.255")},
              new long[] {ConvertIPToLong("192.0.2.0"), ConvertIPToLong("192.0.2.255")},
              new long[] {ConvertIPToLong("192.168.0.0"), ConvertIPToLong("192.168.255.255")},
              new long[] {ConvertIPToLong("255.255.255.0"), ConvertIPToLong("255.255.255.255")}
            };

    }
}
