using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class SICAH_Meteo_getInfostation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string estacion = "";
        string accion = "";
        estacion = Request.QueryString["Station"];
        accion = Request.QueryString["action"];
        SicaSegura.SICA_DB db;
        DataTable dt;

        string respuesta = "";

        DateTime hoy = DateTime.Now;
        string inic = hoy.Day + "/" + hoy.Month + "/" + hoy.Year;
        string fin = hoy.AddDays(1).Day + "/" + hoy.Month + "/" + hoy.Year;

        DataTable dt2;

        string inic2 = "01/" + hoy.Month + "/" + hoy.Year;
        string fin2 = "01/" + hoy.AddMonths(1).Month + "/" + hoy.AddMonths(1).Year;

        DataTable dt3;

		string campos = "";
        string[] aCampos = null;

        switch(accion){
            case "getdata":
                db = new SicaSegura.SICA_DB();
                dt = db.GetDataDBSICA("SELECT TOP(1)* from SICA_Meteo_DatosEstaciones where codigoEstacion like '" + estacion + "%' order by Fecha desc");

                dt2 = db.GetDataDBSICA("SELECT SUM(rain) FROM SICA_Meteo_DatosEstaciones where  codigoEstacion like '" + estacion + "%' AND  fecha between '" + inic + "' AND '" + fin + "'");

                dt3 = db.GetDataDBSICA("SELECT SUM(rain) FROM SICA_Meteo_DatosEstaciones where  codigoEstacion like '" + estacion + "%' AND  fecha between '" + inic2 + "' AND '" + fin2 + "'");


                for (int i = 0; i < dt.Rows[0].ItemArray.Length; i++)
                {
                    respuesta += dt.Rows[0].ItemArray.GetValue(i).ToString().Trim();
                    if (dt.Rows[0].ItemArray.Length - i > 1) { respuesta += "#"; }
                }
                if (dt2.Rows[0].ItemArray.GetValue(0) != null)
                {
                    if (dt2.Rows[0].ItemArray.GetValue(0).ToString() != "")
                    {
                        double rain = Convert.ToDouble(dt2.Rows[0].ItemArray.GetValue(0).ToString());
                        rain = rain * 10;
                        respuesta += "#" + rain.ToString();
                    }
                    else { respuesta += "#0"; }
                }
                else { respuesta += "#0"; }

                if (dt3.Rows[0].ItemArray.GetValue(0) != null)
                {
                    if (dt3.Rows[0].ItemArray.GetValue(0).ToString() != "")
                    {
                        double rain = Convert.ToDouble(dt3.Rows[0].ItemArray.GetValue(0).ToString());
                        rain = rain * 10;
                        respuesta += "#" + rain.ToString();
                    }
                    else { respuesta += "#0"; }
                }
                else { respuesta += "#0"; }

                Response.Write(respuesta);
                break;
            case "getdataFecha":
                string fecParam = Request.QueryString["fecha"];
                hoy = DateTime.Parse(fecParam);
                db = new SicaSegura.SICA_DB();
                dt = db.GetDataDBSICA("SELECT TOP(1)* from SICA_Meteo_DatosEstaciones where codigoEstacion like '" + estacion + "%' AND fecha like '"+ hoy.ToShortDateString() +"' order by Fecha desc");
                respuesta = "";

                
                inic = hoy.Day + "/" + hoy.Month + "/" + hoy.Year;
                fin = hoy.AddDays(1).Day + "/" + hoy.AddDays(1).Month + "/" + hoy.AddDays(1).Year;

                dt2 = db.GetDataDBSICA("SELECT SUM(rain) FROM SICA_Meteo_DatosEstaciones where  codigoEstacion like '" + estacion + "%' AND  fecha between '" + inic + "' AND '" + fin + "'");

                for (int i = 0; i < dt.Rows[0].ItemArray.Length; i++)
                {
                    respuesta += dt.Rows[0].ItemArray.GetValue(i).ToString().Trim();
                    if (dt.Rows[0].ItemArray.Length - i > 1) { respuesta += "#"; }
                }
                if (dt2.Rows[0].ItemArray.GetValue(0) != null)
                {
                    double rain = Convert.ToDouble(dt2.Rows[0].ItemArray.GetValue(0).ToString());
                    rain = rain * 10;
                    respuesta += "#" + rain.ToString();
                }
                else { respuesta += "#0"; }
                Response.Write(respuesta);
                break;
            case "getdatesrange":
                break;
            case "update":
                //Response.Redirect("https://sica.chsegura.es/sicasegura/SICAH/Meteo/SicaMeteo.apk");

                ViewState["PreviousPage"] = Request.UrlReferrer;
                string filepath = Server.MapPath("SicaMeteo.apk");
                FileInfo droidfile = new FileInfo(filepath);

                if (droidfile.Exists)
                {
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + droidfile.Name);
                    Response.AddHeader("Content-Length", droidfile.Length.ToString());
                    Response.ContentType = "application/vnd.android.package-archive";
                    Response.TransmitFile(droidfile.FullName);
                    Response.Flush();
                    Response.End();
                    Response.Redirect(ViewState["PreviousPage"].ToString());
                }


                break;
            case "getDirecciones":
                
                db = new SicaSegura.SICA_DB();
                dt = db.GetDataDBSICA("SELECT CodigoEstacion, Fecha, IP from SICA_Meteo_DireccionEstaciones order by Fecha desc");
                respuesta = "<html><body><table border='1px'>";

                respuesta += "<tr><td>Estación</td><td>Fecha</td><td>IP</td></tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    respuesta += "<tr><td>" + dt.Rows[i].ItemArray.GetValue(0).ToString().Trim() +
                                 "</td><td> "+ dt.Rows[i].ItemArray.GetValue(1).ToString().Trim() + 
                                 "</td><td>" + dt.Rows[i].ItemArray.GetValue(2).ToString().Trim() + 
                                 "</td></tr>";
                    
                }



                respuesta += "</table><body></html>";
                Response.Write(respuesta);
                break;

            case "getdataJSON":

                db = new SicaSegura.SICA_DB();
                campos = "Fecha,Hora,outTemp,outHumidity,windSpeed,windDir,barometer";
                aCampos = campos.Split(',');

                dt = db.GetDataDBSICA("SELECT TOP(1) convert(varchar,Fecha, 103),convert(varchar,Fecha, 108), convert(varchar,round(outTemp,2)), outHumidity,convert(varchar,round(windSpeed,2)),windDir,convert(varchar,round(barometer,2)) from SICA_Meteo_DatosEstaciones where codigoEstacion like '" + estacion + "%' order by Fecha desc");

                respuesta = "{";


                dt2 = db.GetDataDBSICA("SELECT SUM(rain) FROM SICA_Meteo_DatosEstaciones where  codigoEstacion like '" + estacion + "%' AND  fecha between '" + inic + "' AND '" + fin + "'");

                dt3 = db.GetDataDBSICA("SELECT SUM(rain) FROM SICA_Meteo_DatosEstaciones where  codigoEstacion like '" + estacion + "%' AND  fecha between '" + inic2 + "' AND '" + fin2 + "'");


                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows[0].ItemArray.Length; i++)
                    {
                        respuesta += "\"" + aCampos[i] + "\":\"" + dt.Rows[0].ItemArray.GetValue(i).ToString().Trim().Replace(',', '.') + "\"";
                        if (dt.Rows[0].ItemArray.Length - i > 1) { respuesta += ","; }
                    }

                    if (dt2.Rows[0].ItemArray.GetValue(0) != null)
                    {
                        if (dt2.Rows[0].ItemArray.GetValue(0).ToString() != "")
                        {
                            double rain = Convert.ToDouble(dt2.Rows[0].ItemArray.GetValue(0).ToString());
                            rain = rain * 10;
                            rain = Math.Round(rain, 2);
                            respuesta += ",\"rainDay\":\"" + rain.ToString().Replace(',', '.') + "\"";
                        }
                        else { respuesta += ",\"rainDay\":\"" + "0.0" + "\""; }
                    }
                    else { respuesta += ",\"rainDay\":\"" + "0.0" + "\""; }

                    if (dt3.Rows[0].ItemArray.GetValue(0) != null)
                    {
                        if (dt3.Rows[0].ItemArray.GetValue(0).ToString() != "")
                        {
                            double rain = Convert.ToDouble(dt3.Rows[0].ItemArray.GetValue(0).ToString());
                            rain = rain * 10;
							rain = Math.Round(rain, 2);
                            respuesta += ",\"rainMonth\":\"" + rain.ToString().Replace(',', '.') + "\"";
                        }
                        else { respuesta += ",\"rainMonth\":\"" + "0.0" + "\""; }
                    }
                    else { respuesta += ",\"rainMonth\":\"" + "0.0" + "\""; }
                }
				
                respuesta += "}";
                Response.Write(respuesta);
                break;

            case "getdata24JSON":

                db = new SicaSegura.SICA_DB();
                campos = "Fecha,Hora,outTemp,outHumidity,windSpeed,windDir,barometer,rain10";
                aCampos = campos.Split(',');

                dt = db.GetDataDBSICA("SELECT TOP(12*24) left(convert(varchar,Fecha, 103),5),left(convert(varchar,Fecha, 108),5), convert(varchar,round(outTemp,2)), outHumidity,convert(varchar,round(windSpeed,2)),windDir,convert(varchar,round(barometer,2)), convert(varchar,round(rain*10,2)) from SICA_Meteo_DatosEstaciones where codigoEstacion like '" + estacion + "%' order by Fecha desc");

                respuesta = "[";

                for (int j=0; j <dt.Rows.Count; j++)				
                {
					if (j==0) respuesta += "{";
					else respuesta += ",{";
					
                    for (int i = 0; i < dt.Rows[j].ItemArray.Length; i++)
                    {
                        respuesta += "\"" + aCampos[i] + "\":\"" + dt.Rows[j].ItemArray.GetValue(i).ToString().Trim().Replace(',', '.') + "\"";
                        if (dt.Rows[j].ItemArray.Length - i > 1) { respuesta += ","; }
                    }
                 
					respuesta += "}";
                }
				
                respuesta += "]";
                Response.Write(respuesta);
                break;

        }
        

    }
}
