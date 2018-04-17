using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICAH_Meteo_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        string data = Request.Form["data"];        
        string SQL = "";
        string SQL2 = "";
        string[] IP = Request.UserHostName.ToString().Split('.');
        if (Request.Form["data"] != null)
        {
            if (data.Contains('|'))
            {
                
                string[] datos = data.Split('|');
                DateTime fechaHora = DateTime.Parse(datos[1].Trim());
                string fecha = DateTime.Parse(datos[1].Trim()).Day + "/" + DateTime.Parse(datos[1].Trim()).Month + "/" + DateTime.Parse(datos[1].Trim()).Year + " " + DateTime.Parse(datos[1].Trim()).Hour + ":" + DateTime.Parse(datos[1].Trim()).Minute + ":" + DateTime.Parse(datos[1].Trim()).Second + ":" + DateTime.Parse(datos[1].Trim()).Millisecond;
                datos[1] = fecha;
                SQL = "INSERT INTO SICA_Meteo_DatosEstaciones"
                    + " (CodigoEstacion, Cod_fuente_dato, TipoObtenciondato, Fecha, "
                    + " usUnit, interval, barometer, pressure, altimeter, inTemp, "
                    + " outTemp, inHumidity, outHumidity, windSpeed, windDir, windGust, "
                    + " windGustDir, rainRate, rain, dewPoint, windChill, heatIndex, ET, "
                    + " radiation, UV, ExtraTemp1, ExtraTemp2, ExtraTemp3, soilTemp1, "
                    + " soilTemp2, soilTemp3, soilTemp4, leafTemp1, leafTemp2, extraHumid1, "
                    + " extraHumid2, soilMoist1,soilMoist2, soilMoist3, soilMoist4, "
                    + " leafWet1, leafWet2, RxCheckPercent, TxBatteryStatus, "
                    + " consBatteryVoltage, hail, hailrate, heatingTemp, heatingVoltage, "
                    + " SupplyVoltage, referenceVoltage, windBatteryStatus, rainBatteryStatus, "
                    + " outTempBatteryStatus, inTempBatteryStatus) "
                    + " VALUES    ('" + datos[0].Trim() + "', "
                                  + "'E', "
                                  + "'13', "
                                  + "'" + datos[1].Trim() + "', "    //Fecha
                                  + "" + datos[2] + ", "    //
                                  + "" + datos[3] + ", "    //
                                  + "" + datos[4] + ", "    //Barometer
                                  + "" + datos[5] + ", "    //Presure
                                  + "" + datos[6] + ", "    //Altimeter
                                  + "" + datos[7] + ", "    //InTemp
                                  + "" + datos[8] + ", "    //OutTemp
                                  + "" + datos[9] + ", "    //InHumidity
                                  + "" + datos[10] + ", "    //OutHumidity
                                  + "" + datos[11] + ", "    //WinSpeed
                                  + "" + datos[12] + ", "    //WinDir
                                  + "" + datos[13] + ", "    //
                                  + "" + datos[14] + ", "    //    
                                  + "" + datos[15] + ", "    //rainRate
                                  + "" + datos[16] + ", "    //rain
                                  + "" + datos[17] + ", "
                                  + "" + datos[18] + ", "
                                  + "" + datos[19] + ", "
                                  + "" + datos[20] + ", "
                                  + "" + datos[21] + ", "
                                  + "" + datos[22] + ", "
                                  + "" + datos[23] + ", "
                                  + "" + datos[24] + ", "
                                  + "" + datos[25] + ", "
                                  + "" + datos[26] + ", "
                                  + "" + datos[27] + ", "
                                  + "" + datos[28] + ", "
                                  + "" + datos[29] + ", "
                                  + "" + datos[30] + ", "
                                  + "" + datos[31] + ", "
                                  + "" + datos[32] + ", "
                                  + "" + datos[33] + ", "
                                  + "" + datos[34] + ", "
                                  + "" + datos[35] + ", "
                                  + "" + datos[36] + ", "
                                  + "" + datos[37] + ", "
                                  + "" + datos[38] + ", "
                                  + "" + datos[39] + ", "
                                  + "" + datos[40] + ", "
                                  + "" + datos[41] + ", "
                                  + "" + datos[42] + ", "
                                  + "" + datos[43] + ", "
                                  + "" + datos[44] + ", "
                                  + "" + datos[45] + ", "
                                  + "" + datos[46] + ", "
                                  + "" + datos[47] + ", "
                                  + "" + datos[48] + ", "
                                  + "" + IP[0] + ", "
                                  + "" + IP[1] + ", "
                                  + "" + IP[2] + ", "
                                  + "" + IP[3] + ")";

                SQL = SQL.Replace(", ,", ",0,");
                SQL = SQL.Replace(", ,", ",0,");
                SQL = SQL.Replace(", )", ",0)");

                SicaSegura.SICA_DB db = new SicaSegura.SICA_DB();
                try
                {
                    db.GetDataDBSICA(SQL);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString());
                }

                /* jmolina4; Cambio de tabla
                string strfecha = DateTime.Parse(datos[1].Trim().Replace(":0:", ":00:").Substring(0, datos[1].Trim().Replace(":0:", ":00:").Length - 2)).ToString();
                string pvycr = datos[0].Trim();
                Int64 ID = Convert.ToInt64(db.GetDataSIGVECTOR("SELECt MAX(AutoId) FROM PVYCR_DatosSAIHInstantaneos").Rows[0].ItemArray.GetValue(0)) + 1;
                SQL = "INSERT INTO dbo.PVYCR_DatosSAIHInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','P'," + datos[16].Trim() + ")"; //Lluvia
                db.GetDataSIGVECTOR(SQL);
                SQL = "INSERT INTO dbo.PVYCR_DatosSAIHInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','T'," + datos[8].Trim() + ")"; //Temperatura
                db.GetDataSIGVECTOR(SQL);
                SQL = "INSERT INTO dbo.PVYCR_DatosSAIHInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','H'," + datos[10].Trim() + ")"; //humedad
                db.GetDataSIGVECTOR(SQL);
                SQL = "INSERT INTO dbo.PVYCR_DatosSAIHInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','W'," + datos[11].Trim() + ")"; //viento
                db.GetDataSIGVECTOR(SQL);
                SQL = "INSERT INTO dbo.PVYCR_DatosSAIHInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','D'," + datos[12].Trim() + ")"; //Dirección Viento
                db.GetDataSIGVECTOR(SQL);
                SQL = "INSERT INTO dbo.PVYCR_DatosSAIHInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','B'," + datos[4].Trim() + ")"; //Lluvia
                db.GetDataSIGVECTOR(SQL);
                Response.Write(data);
                */

                String sMes = fechaHora.Month + "";
                String sDia = fechaHora.Day + "";
                String sHora = fechaHora.Hour + "";
                String sMinuto = fechaHora.Minute + "";
                String sSegundo = fechaHora.Second + "";

                sMes = sMes.PadLeft(2, '0');
                sDia = sDia.PadLeft(2, '0');
                sHora = sHora.PadLeft(2, '0');
                sMinuto = sMinuto.PadLeft(2, '0');
                sSegundo = sSegundo.PadLeft(2, '0');
                
                // >> jmolina4 (para normalizar formato de fecha en tabla...) 24/02/2014
                string strfecha = fechaHora.Year + "-" + sMes + "-" + sDia + " " + sHora + ":" + sMinuto + ":" + sSegundo;
                // antiguosica012014
                // string strfecha = DateTime.Parse(datos[1].Trim().Replace(":0:", ":00:").Substring(0, datos[1].Trim().Replace(":0:", ":00:").Length - 2)).ToString();
                // << jmolina4 (para normalizar formato de fecha en tabla...) 24/02/2014

                string pvycr = datos[0].Trim();
                //Int64 ID = Convert.ToInt64(db.GetDataSIGVECTOR("SELECt MAX(AutoId) FROM PVYCR_DatosSICAInstantaneos").Rows[0].ItemArray.GetValue(0)) + 1;
                SQL = "INSERT INTO dbo.PVYCR_DatosSICAInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','P','" + datos[16].Trim() + "')"; //Lluvia
                db.GetDataSIGVECTOR(SQL);
                SQL = "INSERT INTO dbo.PVYCR_DatosSICAInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','T','" + datos[8].Trim() + "')"; //Temperatura
                db.GetDataSIGVECTOR(SQL);
                SQL = "INSERT INTO dbo.PVYCR_DatosSICAInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','H','" + datos[10].Trim() + "')"; //humedad
                db.GetDataSIGVECTOR(SQL);
                SQL = "INSERT INTO dbo.PVYCR_DatosSICAInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','W','" + datos[11].Trim() + "')"; //viento
                db.GetDataSIGVECTOR(SQL);
                SQL = "INSERT INTO dbo.PVYCR_DatosSICAInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','D','" + datos[12].Trim() + "')"; //Dirección Viento
                db.GetDataSIGVECTOR(SQL);
                SQL = "INSERT INTO dbo.PVYCR_DatosSICAInstantaneos(Fecha, CodigoPVYCR, TipoVariable, Valor) VALUES ('" + strfecha + "','" + pvycr + "','B','" + datos[4].Trim() + "')"; //Lluvia
                db.GetDataSIGVECTOR(SQL);
                Response.Write(data);
            }
            else if (data.Length > 0)
            {
                string estacion = data.Trim();
                string direcc = Request.UserHostName.ToString();

                SQL2 = "DELETE FROM SICA_Meteo_DireccionEstaciones WHERE CodigoEstacion = '" + estacion + "'";
                SQL = "INSERT INTO SICA_Meteo_DireccionEstaciones (CodigoEstacion, Fecha, IP) values " +
                      "('" + estacion + "',getDate(),'" + direcc + "')";

                SicaSegura.SICA_DB db = new SicaSegura.SICA_DB();
                try
                {
                    db.GetDataDBSICA(SQL2);
                    db.GetDataDBSICA(SQL);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString());
                }


            }
        } 
    }
}
