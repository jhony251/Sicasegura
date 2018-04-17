using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SICAH_CotrolCauces_GraficoLecturas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        string CFD = "";
        string FI = "";
        string FF = "";
        string PC = "";
        string EM = "";

        try
        {   
            PC = Request.QueryString["PC"].ToString().Trim();
            EM = Request.QueryString["EM"].ToString().Trim();
            FI = Request.QueryString["FI"].ToString().Trim();
            FF = Request.QueryString["FF"].ToString().Trim();
            
        }
        catch (Exception ee) { Response.Write(""); }
        try { CFD = Request.QueryString["CFD"].ToString(); }
        catch { CFD = ""; }
        
        if (FI == "") { if (DateTime.Now.Month > 9 && DateTime.Now.Month < 12) { FI = "01/10/" + DateTime.Now.Year.ToString(); } else { FI = "01/10/" + DateTime.Now.AddYears(-1).Year.ToString(); } }

        if (FF == "") { if (DateTime.Now.Month > 9 && DateTime.Now.Month < 12) { FF = "01/10/" + DateTime.Now.AddYears(1).Year.ToString(); } else { FF = "01/10/" + DateTime.Now.Year.ToString(); } }

        //Response.Write(FI+FF+PC+EM);
        if (EM.Contains("V"))
        {
            #region
            string str = "";
            string sql = "";
            sql = "SELECT distinct Cod_fuente_Dato FROM PVYCR_DatosMotores where codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
            if (CFD != "") { sql += " AND Cod_fuente_dato like '" + CFD + "'"; }
            DataTable dt = DB.GetDataSIGVECTOR(sql);
            str = str + "series: [";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0) { str = str + ","; }
                str = str + "{type: 'spline', name: '" + dt.Rows[i][0].ToString() + "', marker: {enabled: false, states :{hover:{enabled: false}}}, data:[";
                sql = "select Fecha_medida, lecturacontador_m3 from PVYCR_datosmotores where cod_fuente_dato like '" + dt.Rows[i][0] + "' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
                DataTable dt2 = DB.GetDataSIGVECTOR(sql);
                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {
                   

                    //Date.UTC(" & FechaIniAux.Year & ", " & FechaIniAux.Month & ", " & FechaIniAux.Day & ", " & "00" & ", " & "01" & ", " & "01" & ")
                    
                    if (dt2.Rows[ii][1].ToString().Replace(",", ".") != "")
                    {
                        if (ii > 0) { str = str + ","; }
                        DateTime fecha = DateTime.Parse(dt2.Rows[ii][0].ToString());
                        str = str + "[Date.UTC(" + fecha.Year + "," + Convert.ToString(Convert.ToInt16(fecha.Month) - 1) + "," + fecha.Day + "," + fecha.Hour + "," + fecha.Minute + "," + fecha.Second + ")," + dt2.Rows[ii][1].ToString().Replace(",", ".") + "]";
                    }
                }
                str = str + "]}";
            }
            str += "]";
            string strdef = "";
            strdef = "<script>" +
            "jQuery(function()" +
                "{var chart1 = new Highcharts.Chart({" +
                    "chart: { renderTo: 'chart1' }," +
                    "navigator: { height: 60 }, " +
                    "title: { text: 'Lectura contador por Código Fuente de Dato', floating: false, align: 'center', x: -20, top: 20 }, " +
                    "yAxis: {title: {text:'Volumen (m3)'}}, " +
                    "xAxis: { type:'datetime',title: {text:'Fecha'}}," +
                    "tooltip: {" +
                        "formatter: function() " +
                        "{" +
                                "return '<b>'+ this.series.name +'</b><br/>'+ " +
                                "'Fecha:' + Highcharts.dateFormat('%e. %b', this.x) +' <br>Volumen '+ this.y +' (m3)'" +
                                       "}" +
                        "}," +
                    " " + str + " });});</script>";
            LIT_grafico.Text = strdef;



            //#######################
            //#######################
            //GRAFICO DE ESTADÍSTICOS
            //#######################
            //#######################

            //vemos de que fuente de dato hay más registros.
            str = "";
            int CFD13 = DB.GetDataSIGVECTOR("Select * from pvycr_datosmotores where cod_fuente_dato like '13' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count;
            int CFD05 = DB.GetDataSIGVECTOR("Select * from pvycr_datosmotores where cod_fuente_dato like '05' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count;
            int CFD10 = DB.GetDataSIGVECTOR("Select * from pvycr_datosmotores where cod_fuente_dato like '10' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count;

            if (CFD == "") 
                {
                    if (CFD05 > CFD13)
                    {
                        if (CFD05 > CFD10) { CFD = "05"; } else { if (CFD10 > CFD13) { CFD = "10"; } }

                    }
                    else
                    {
                        if (CFD13 > CFD10) { CFD = "13"; } else { if (CFD10 > CFD05) { CFD = "10"; } }
                    }
            }
            

            str = str + "series: [";
            for (int i = 0; i < 2; i++)
            {
                string nombre = "";//nombre de la serie
                if (i > 0) { str = str + ","; }
                if (i == 0) { nombre = "Consumos Parciales"; } else { nombre = "Acumulado"; }
                str = str + "{type: 'spline', name: '" + nombre + "', marker: {enabled: false, states :{hover:{enabled: false}}}, data:[";
                sql = "select Fecha_medida, lecturacontador_m3 from PVYCR_datosmotores where cod_fuente_dato like '" + CFD + "' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
                //Response.Write(sql);
                DataTable dt2 = DB.GetDataSIGVECTOR(sql);
                
                double anterior = 0;
                //if (dt2.Rows.Count > 0)
                //{
                //    try { anterior = Convert.ToDouble(dt2.Rows[0][1].ToString()); }
                //    catch { if  }
                //}
                double acumuladoparcial = 0;
                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {
                    try
                    {
                        Convert.ToDouble(dt2.Rows[ii][1].ToString()); 
                        if (ii == 0)
                        {
                            anterior = Convert.ToDouble(dt2.Rows[0][1].ToString()); 
                            
                        }
                        double valor = 0;

                        if (ii > 0) { str = str + ","; }
                        switch (i)
                        {
                            case 0://parcial
                                if (anterior < Convert.ToDouble(dt2.Rows[ii][1].ToString()))
                                { valor = Convert.ToDouble(dt2.Rows[ii][1].ToString()) - anterior; }
                                else
                                { valor = this.tratarIncidenciaContadorVolumetrico(anterior, Convert.ToDouble(dt2.Rows[ii][1].ToString())); }
                                anterior = Convert.ToDouble(dt2.Rows[ii][1].ToString());
                                DateTime fecha = DateTime.Parse(dt2.Rows[ii][0].ToString());
                                str = str + "[Date.UTC(" + fecha.Year + "," + Convert.ToString(Convert.ToInt16(fecha.Month) - 1) + "," + fecha.Day + "," + fecha.Hour + "," + fecha.Minute + "," + fecha.Second + ")," + valor.ToString().Replace(",", ".") + "]";
                                break;
                            case 1: //acumulado
                                if (anterior < Convert.ToDouble(dt2.Rows[ii][1].ToString()))
                                { valor = Convert.ToDouble(dt2.Rows[ii][1].ToString()) - anterior; }
                                else
                                { valor = this.tratarIncidenciaContadorVolumetrico(anterior, Convert.ToDouble(dt2.Rows[ii][1].ToString())); }
                                anterior = Convert.ToDouble(dt2.Rows[ii][1].ToString());
                                acumuladoparcial += valor;
                                DateTime fecha2 = DateTime.Parse(dt2.Rows[ii][0].ToString());
                                str = str + "[Date.UTC(" + fecha2.Year + "," + Convert.ToString(Convert.ToInt16(fecha2.Month) - 1) + "," + fecha2.Day + "," + fecha2.Hour + "," + fecha2.Minute + "," + fecha2.Second + ")," + acumuladoparcial.ToString().Replace(",", ".") + "]";
                                break;

                        }
                    }
                    catch(Exception eee){}
                
                }
                str = str + "]}";
                
            }
            str += "]";
            string strdef2 = "";
            strdef2 = "<script>" +
            "jQuery(function()" +
                "{var chart2 = new Highcharts.Chart({" +
                    "chart: { renderTo: 'chart2' }," +
                    "navigator: { height: 60 }, " +
                    "title: { text: 'Datos de consumos parciales y absolutos por 05/13', floating: false, align: 'center', x: -20, top: 20 }, " +
                    "yAxis: {title: {text:'Volumen (m3)'}}, " +
                    "xAxis: { type:'datetime',title: {text:'Fecha'}}," +
                    "tooltip: {" +
                        "formatter: function() " +
                        "{" +
                                "return '<b>'+ this.series.name +'</b><br/>'+ " +
                                "'Fecha:' + Highcharts.dateFormat('%e. %b', this.x) +'<br>Volumen '+ this.y +' (m3)'" +
                                       "}" +
                        "}," +
                    " " + str + " });});</script>";
            LIT_grafico2.Text = strdef2;

        }
        //#############################################################################################################################
        //#############################################################################################################################
        //#############################################################################################################################
        //#############################################################################################################################
        //#############################################################################################################################
        //#############################################################################################################################
            #endregion
        else if (EM.Contains("H"))
        {
            #region
            string str = "";
            string sql = "";
            sql = "SELECT distinct Cod_fuente_Dato FROM PVYCR_DatosHorometros where codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
            if (CFD != "") { sql += " AND Cod_fuente_dato like '" + CFD + "'"; }
            DataTable dt = DB.GetDataSIGVECTOR(sql);
            str = str + "series: [";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0) { str = str + ","; }
                str = str + "{type: 'spline', name: '" + dt.Rows[i][0].ToString() + "', marker: {enabled: false, states :{hover:{enabled: false}}}, data:[";
                sql = "select Fecha_medida, horasintervalo from PVYCR_datoshorometros where cod_fuente_dato like '" + dt.Rows[i][0] + "' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
                DataTable dt2 = DB.GetDataSIGVECTOR(sql);
                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {


                    //Date.UTC(" & FechaIniAux.Year & ", " & FechaIniAux.Month & ", " & FechaIniAux.Day & ", " & "00" & ", " & "01" & ", " & "01" & ")

                    if (dt2.Rows[ii][1].ToString().Replace(",", ".") != "")
                    {
                        if (ii > 0) { str = str + ","; }
                        DateTime fecha = DateTime.Parse(dt2.Rows[ii][0].ToString());
                        str = str + "[Date.UTC(" + fecha.Year + "," + Convert.ToString(Convert.ToInt16(fecha.Month)-1) + "," + fecha.Day + "," + fecha.Hour + "," + fecha.Minute + "," + fecha.Second + ")," + dt2.Rows[ii][1].ToString().Replace(",", ".") + "]";
                    }
                }
                str = str + "]}";
            }
            str += "]";
            string strdef = "";
            strdef = "<script>" +
            "jQuery(function()" +
                "{var chart1 = new Highcharts.Chart({" +
                    "chart: { renderTo: 'chart1' }," +
                    "navigator: { height: 60 }, " +
                    "title: { text: 'Lectura contador por Código Fuente de Dato', floating: false, align: 'center', x: -20, top: 20 }, " +
                    "yAxis: {title: {text:'Horas (H)'}}, " +
                    "xAxis: { type:'datetime',title: {text:'Fecha'}}," +
                    "tooltip: {" +
                        "formatter: function() " +
                        "{" +
                                "return '<b>'+ this.series.name +'</b><br/>'+ " +
                                "'Fecha:' + Highcharts.dateFormat('%e. %b', this.x) +' <br>Volumen '+ this.y +' (H)'" +
                                       "}" +
                        "}," +
                    " " + str + " });});</script>";
            LIT_grafico.Text = strdef;
                        
            //#######################
            //#######################
            //GRAFICO DE ESTADÍSTICOS
            //#######################
            //#######################
            //buscamos el caudal en L/s de la bomba 

            Double Qls = 0; // M3 a la hora
            string SQLQls = "SELECT     dbo.PVYCR_MotoBombas.CodigoMotoBomba, dbo.PVYCR_MotoBombas.Caudal_LSeg, " +
                        "dbo.PVYCR_ElementosMedida_MotoBombas.idElementoMedida, dbo.PVYCR_ElementosMedida_MotoBombas.codigoPVYCR " +
                        "FROM         dbo.PVYCR_MotoBombas INNER JOIN " +
                        "dbo.PVYCR_ElementosMedida_MotoBombas ON " +
                        "dbo.PVYCR_MotoBombas.CodigoMotoBomba = dbo.PVYCR_ElementosMedida_MotoBombas.CodigoMotobomba AND " +
                        "dbo.PVYCR_MotoBombas.FechaRevision = dbo.PVYCR_ElementosMedida_MotoBombas.FechaRevision " +
                        "WHERE     (dbo.PVYCR_MotoBombas.codigoPVYCR LIKE '" + PC.Trim() +"') AND (dbo.PVYCR_ElementosMedida_MotoBombas.idElementoMedida LIKE N'" + EM + "')";
            Qls = Convert.ToDouble(DB.GetDataSIGVECTOR(SQLQls).Rows[0].ItemArray.GetValue(1));
            Qls = System.Math.Round(Qls, 2);
            //vemos de que fuente de dato hay más registros.
            str = "";
            int CFD13 = DB.GetDataSIGVECTOR("Select * from pvycr_datoshorometros where cod_fuente_dato like '13' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count;
            int CFD05 = DB.GetDataSIGVECTOR("Select * from pvycr_datoshorometros where cod_fuente_dato like '05' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count;
            int CFD10 = DB.GetDataSIGVECTOR("Select * from pvycr_datoshorometros where cod_fuente_dato like '10' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count; ;

            if (CFD == "")
            {
                if (CFD05 > CFD13)
                {
                    if (CFD05 > CFD10) { CFD = "05"; } else { if (CFD10 > CFD13) { CFD = "10"; } }

                }
                else
                {
                    if (CFD13 > CFD10) { CFD = "13"; } else { if (CFD10 > CFD05) { CFD = "10"; } }
                }
            }

            str = str + "series: [";
            for (int i = 0; i < 2; i++)
            {
                string nombre = "";//nombre de la serie
                if (i > 0) { str = str + ","; }
                if (i == 0) { nombre = "Consumos Parciales"; } else { nombre = "Acumulado"; }
                str = str + "{type: 'spline', name: '" + nombre + "', marker: {enabled: false, states :{hover:{enabled: false}}}, data:[";
                sql = "select Fecha_medida, horasintervalo from PVYCR_datoshorometros where cod_fuente_dato like '" + CFD + "' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
                //Response.Write(sql);
                DataTable dt2 = DB.GetDataSIGVECTOR(sql);

                double anterior = 0;
                //if (dt2.Rows.Count > 0)
                //{
                //    try { anterior = Convert.ToDouble(dt2.Rows[0][1].ToString()); }
                //    catch { if  }
                //}
                double acumuladoparcial = 0;
                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {
                    try
                    {
                        Convert.ToDouble(dt2.Rows[ii][1].ToString());
                        if (ii == 0)
                        {
                            anterior = Convert.ToDouble(dt2.Rows[0][1].ToString());

                        }
                        double valor = 0;

                        if (ii > 0) { str = str + ","; }
                        switch (i)
                        {
                            case 0://parcial
                                if (anterior < Convert.ToDouble(dt2.Rows[ii][1].ToString()))
                                { valor = Convert.ToDouble(dt2.Rows[ii][1].ToString()) - anterior; }
                                else
                                { valor = this.tratarIncidenciaContadorVolumetrico(anterior, Convert.ToDouble(dt2.Rows[ii][1].ToString())); }
                                anterior = Convert.ToDouble(dt2.Rows[ii][1].ToString());
                                DateTime fecha = DateTime.Parse(dt2.Rows[ii][0].ToString());
                                str = str + "[Date.UTC(" + fecha.Year + "," + Convert.ToString(Convert.ToInt16(fecha.Month) - 1) + "," + fecha.Day + "," + fecha.Hour + "," + fecha.Minute + "," + fecha.Second + ")," + System.Math.Round((valor*Qls)/1000,2).ToString().Replace(",", ".") + "]";
                                break;
                            case 1: //acumulado
                                if (anterior < Convert.ToDouble(dt2.Rows[ii][1].ToString()))
                                { valor = Convert.ToDouble(dt2.Rows[ii][1].ToString()) - anterior; }
                                else
                                { valor = this.tratarIncidenciaContadorVolumetrico(anterior, Convert.ToDouble(dt2.Rows[ii][1].ToString())); }
                                anterior = Convert.ToDouble(dt2.Rows[ii][1].ToString());
                                acumuladoparcial += valor;
                                DateTime fecha2 = DateTime.Parse(dt2.Rows[ii][0].ToString());
                                str = str + "[Date.UTC(" + fecha2.Year + "," + Convert.ToString(Convert.ToInt16(fecha2.Month) - 1) + "," + fecha2.Day + "," + fecha2.Hour + "," + fecha2.Minute + "," + fecha2.Second + ")," + System.Math.Round((acumuladoparcial*Qls)/1000,2).ToString().Replace(",", ".") + "]";
                                break;

                        }
                    }
                    catch (Exception eee) { }

                }
                str = str + "]}";

            }
            str += "]";
            string strdef2 = "";
            strdef2 = "<script>" +
            "jQuery(function()" +
                "{var chart2 = new Highcharts.Chart({" +
                    "chart: { renderTo: 'chart2' }," +
                    "navigator: { height: 60 }, " +
                    "title: { text: 'Datos de consumos parciales y absolutos por 05/13', floating: false, align: 'center', x: -20, top: 20 }, " +
                    "yAxis: {title: {text:'Horas (M3)'}}, " +
                    "xAxis: { type:'datetime',title: {text:'Fecha'}}," +
                    "tooltip: {" +
                        "formatter: function() " +
                        "{" +
                                "return '<b>'+ this.series.name +'</b><br/>'+ " +
                                "'Fecha:' + Highcharts.dateFormat('%e. %b', this.x) +'<br>Volumen '+ this.y +' (M3)'" +
                                       "}" +
                        "}," +
                    " " + str + " });});</script>";
            LIT_grafico2.Text = strdef2;

            #endregion
        }
        else if (EM.Contains("E")) 
        {
            #region
            string str = "";
            string sql = "";
            sql = "SELECT distinct Cod_fuente_Dato FROM PVYCR_DatosAlimentacion where codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
            if (CFD != "") { sql += " AND Cod_fuente_dato like '" + CFD + "'"; }
            DataTable dt = DB.GetDataSIGVECTOR(sql);
            str = str + "series: [";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0) { str = str + ","; }
                str = str + "{type: 'spline', name: '" + dt.Rows[i][0].ToString() + "', marker: {enabled: false, states :{hover:{enabled: false}}}, data:[";
                sql = "select Fecha_medida, TOTAL_KWH from PVYCR_datosAlimentacion where cod_fuente_dato like '" + dt.Rows[i][0] + "' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
                DataTable dt2 = DB.GetDataSIGVECTOR(sql);
                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {


                    //Date.UTC(" & FechaIniAux.Year & ", " & FechaIniAux.Month & ", " & FechaIniAux.Day & ", " & "00" & ", " & "01" & ", " & "01" & ")

                    if (dt2.Rows[ii][1].ToString().Replace(",", ".") != "")
                    {
                        if (ii > 0) { str = str + ","; }
                        DateTime fecha = DateTime.Parse(dt2.Rows[ii][0].ToString());
                        str = str + "[Date.UTC(" + fecha.Year + "," + Convert.ToString(Convert.ToInt16(fecha.Month) - 1) + "," + fecha.Day + "," + fecha.Hour + "," + fecha.Minute + "," + fecha.Second + ")," + dt2.Rows[ii][1].ToString().Replace(",", ".") + "]";
                    }
                }
                str = str + "]}";
            }
            str += "]";
            string strdef = "";
            strdef = "<script>" +
            "jQuery(function()" +
                "{var chart1 = new Highcharts.Chart({" +
                    "chart: { renderTo: 'chart1' }," +
                    "navigator: { height: 60 }, " +
                    "title: { text: 'Lectura contador por Código Fuente de Dato', floating: false, align: 'center', x: -20, top: 20 }, " +
                    "yAxis: {title: {text:'Kilovatios-hora (KWH)'}}, " +
                    "xAxis: { type:'datetime',title: {text:'Fecha'}}," +
                    "tooltip: {" +
                        "formatter: function() " +
                        "{" +
                                "return '<b>'+ this.series.name +'</b><br/>'+ " +
                                "'Fecha:' + Highcharts.dateFormat('%e. %b', this.x) +' <br>Volumen '+ this.y +' (KWH)'" +
                                       "}" +
                        "}," +
                    " " + str + " });});</script>";
            LIT_grafico.Text = strdef;






            //#######################
            //#######################
            //GRAFICO DE ESTADÍSTICOS
            //#######################
            //#######################
            //buscamos el caudal en L/s de la bomba 

            Double M3Kwh = 0; // M3 a la hora
            string SQLQls = "SELECT     dbo.PVYCR_Contadores_ElementosMedida.CodigoPVYCR, dbo.PVYCR_Contadores.E_RelacionM3_KWH, " +
                            " dbo.PVYCR_Contadores_ElementosMedida.idElementoMedida " +
                            " FROM         dbo.PVYCR_Contadores INNER JOIN " +
                            " dbo.PVYCR_Contadores_ElementosMedida ON dbo.PVYCR_Contadores.idContador = dbo.PVYCR_Contadores_ElementosMedida.idContador AND " +
                            " dbo.PVYCR_Contadores.FechaRevision = dbo.PVYCR_Contadores_ElementosMedida.FechaRevision " +
                            " WHERE     (dbo.PVYCR_Contadores_ElementosMedida.CodigoPVYCR LIKE N'" + PC.Trim() + "') AND (dbo.PVYCR_Contadores_ElementosMedida.idElementoMedida LIKE N'" + EM + "') ";
            M3Kwh = Convert.ToDouble(DB.GetDataSIGVECTOR(SQLQls).Rows[0].ItemArray.GetValue(1));
            M3Kwh = System.Math.Round(M3Kwh, 2);
            //vemos de que fuente de dato hay más registros.
            str = "";

            int CFD13 = DB.GetDataSIGVECTOR("Select * from pvycr_datosAlimentacion where cod_fuente_dato like '13' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count;
            int CFD05 = DB.GetDataSIGVECTOR("Select * from pvycr_datosAlimentacion where cod_fuente_dato like '05' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count;
            int CFD10 = DB.GetDataSIGVECTOR("Select * from pvycr_datosAlimentacion where cod_fuente_dato like '10' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count;

            if (CFD == "")
            {
                if (CFD05 > CFD13)
                {
                    if (CFD05 > CFD10) { CFD = "05"; } else { if (CFD10 > CFD13) { CFD = "10"; } }

                }
                else
                {
                    if (CFD13 > CFD10) { CFD = "13"; } else { if (CFD10 > CFD05) { CFD = "10"; } }
                }
            }
            
                        

            str = str + "series: [";
            for (int i = 0; i < 2; i++)
            {
                string nombre = "";//nombre de la serie
                if (i > 0) { str = str + ","; }
                if (i == 0) { nombre = "Consumos Parciales"; } else { nombre = "Acumulado"; }
                str = str + "{type: 'spline', name: '" + nombre + "', marker: {enabled: false, states :{hover:{enabled: false}}}, data:[";
                sql = "select Fecha_medida, Total_KWH from PVYCR_datosAlimentacion where cod_fuente_dato like '" + CFD + "' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
                //Response.Write(sql);
                DataTable dt2 = DB.GetDataSIGVECTOR(sql);

                double anterior = 0;
                //if (dt2.Rows.Count > 0)
                //{
                //    try { anterior = Convert.ToDouble(dt2.Rows[0][1].ToString()); }
                //    catch { if  }
                //}
                double acumuladoparcial = 0;
                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {
                    try
                    {
                        Convert.ToDouble(dt2.Rows[ii][1].ToString());
                        if (ii == 0)
                        {
                            anterior = Convert.ToDouble(dt2.Rows[0][1].ToString());

                        }
                        double valor = 0;

                        if (ii > 0) { str = str + ","; }
                        switch (i)
                        {
                            case 0://parcial
                                if (anterior < Convert.ToDouble(dt2.Rows[ii][1].ToString()))
                                { valor = Convert.ToDouble(dt2.Rows[ii][1].ToString()) - anterior; }
                                else
                                { valor = this.tratarIncidenciaContadorVolumetrico(anterior, Convert.ToDouble(dt2.Rows[ii][1].ToString())); }
                                anterior = Convert.ToDouble(dt2.Rows[ii][1].ToString());
                                DateTime fecha = DateTime.Parse(dt2.Rows[ii][0].ToString());
                                str = str + "[Date.UTC(" + fecha.Year + "," + Convert.ToString(Convert.ToInt16(fecha.Month) - 1) + "," + fecha.Day + "," + fecha.Hour + "," + fecha.Minute + "," + fecha.Second + ")," + System.Math.Round((valor * M3Kwh) ,2).ToString().Replace(",", ".") + "]";
                                break;
                            case 1: //acumulado
                                if (anterior < Convert.ToDouble(dt2.Rows[ii][1].ToString()))
                                { valor = Convert.ToDouble(dt2.Rows[ii][1].ToString()) - anterior; }
                                else
                                { valor = this.tratarIncidenciaContadorVolumetrico(anterior, Convert.ToDouble(dt2.Rows[ii][1].ToString())); }
                                anterior = Convert.ToDouble(dt2.Rows[ii][1].ToString());
                                acumuladoparcial += valor;
                                DateTime fecha2 = DateTime.Parse(dt2.Rows[ii][0].ToString());
                                str = str + "[Date.UTC(" + fecha2.Year + "," + Convert.ToString(Convert.ToInt16(fecha2.Month) - 1) + "," + fecha2.Day + "," + fecha2.Hour + "," + fecha2.Minute + "," + fecha2.Second + ")," + System.Math.Round((acumuladoparcial * M3Kwh),2).ToString().Replace(",", ".") + "]";
                                break;

                        }
                    }
                    catch (Exception eee) { }

                }
                str = str + "]}";

            }
            str += "]";
            string strdef2 = "";
            strdef2 = "<script>" +
            "jQuery(function()" +
                "{var chart2 = new Highcharts.Chart({" +
                    "chart: { renderTo: 'chart2' }," +
                    "navigator: { height: 60 }, " +
                    "title: { text: 'Datos de consumos parciales y absolutos por 05/13', floating: false, align: 'center', x: -20, top: 20 }, " +
                    "yAxis: {title: {text:'Volumen (M3)'}}, " +
                    "xAxis: { type:'datetime',title: {text:'Fecha'}}," +
                    "tooltip: {" +
                        "formatter: function() " +
                        "{" +
                                "return '<b>'+ this.series.name +'</b><br/>'+ " +
                                "'Fecha:' + Highcharts.dateFormat('%e. %b', this.x) +'<br>Volumen '+ this.y +' (M3)'" +
                                       "}" +
                        "}," +
                    " " + str + " });});</script>";
            LIT_grafico2.Text = strdef2;
            #endregion
        }
        else if (EM.Contains("Q")) 
        {
            #region
            string str = "";
            string sql = "";
            sql = "SELECT distinct Cod_fuente_Dato FROM PVYCR_Datosacequias where codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
            if (CFD != "") { sql += " AND Cod_fuente_dato like '" + CFD + "'"; }
            DataTable dt = DB.GetDataSIGVECTOR(sql);
            str = str + "series: [";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0) { str = str + ","; }
                str = str + "{type: 'spline', name: '" + dt.Rows[i][0].ToString() + "', marker: {enabled: false, states :{hover:{enabled: false}}}, data:[";
                sql = "select Fecha_medida, Caudal_M3S from PVYCR_datosacequias where cod_fuente_dato like '" + dt.Rows[i][0] + "' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
                DataTable dt2 = DB.GetDataSIGVECTOR(sql);
                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {


                    //Date.UTC(" & FechaIniAux.Year & ", " & FechaIniAux.Month & ", " & FechaIniAux.Day & ", " & "00" & ", " & "01" & ", " & "01" & ")

                    if (dt2.Rows[ii][1].ToString().Replace(",", ".") != "")
                    {
                        if (ii > 0) { str = str + ","; }
                        DateTime fecha = DateTime.Parse(dt2.Rows[ii][0].ToString());
                        str = str + "[Date.UTC(" + fecha.Year + "," + Convert.ToString(Convert.ToInt16(fecha.Month) - 1) + "," + fecha.Day + "," + fecha.Hour + "," + fecha.Minute + "," + fecha.Second + ")," + dt2.Rows[ii][1].ToString().Replace(",", ".") + "]";
                    }
                }
                str = str + "]}";
            }
            str += "]";
            string strdef = "";
            strdef = "<script>" +
            "jQuery(function()" +
                "{var chart1 = new Highcharts.Chart({" +
                    "chart: { renderTo: 'chart1' }," +
                    "navigator: { height: 60 }, " +
                    "title: { text: 'Lectura contador por Código Fuente de Dato', floating: false, align: 'center', x: -20, top: 20 }, " +
                    "yAxis: {title: {text:'Caudal (M3S)'}}, " +
                    "xAxis: { type:'datetime',title: {text:'Fecha'}}," +
                    "tooltip: {" +
                        "formatter: function() " +
                        "{" +
                                "return '<b>'+ this.series.name +'</b><br/>'+ " +
                                "'Fecha:' + Highcharts.dateFormat('%e. %b', this.x) +' <br>Volumen '+ this.y +' (M3S)'" +
                                       "}" +
                        "}," +
                    " " + str + " });});</script>";
            LIT_grafico.Text = strdef;

            //#######################
            //#######################
            //GRAFICO DE ESTADÍSTICOS
            //#######################
            //#######################
            //buscamos el caudal en L/s de la bomba 

           
            //vemos de que fuente de dato hay más registros.
            str = "";



            int CFD13 = DB.GetDataSIGVECTOR("Select * from pvycr_datosAcequias where cod_fuente_dato like '13' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count;
            int CFD05 = DB.GetDataSIGVECTOR("Select * from pvycr_datosAcequias where cod_fuente_dato like '05' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count;
            int CFD10 = DB.GetDataSIGVECTOR("Select * from pvycr_datosAcequias where cod_fuente_dato like '10' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'").Rows.Count;

            if (CFD == "")
            {
                if (CFD05 > CFD13)
                {
                    if (CFD05 > CFD10) { CFD = "05"; } else { if (CFD10 > CFD13) { CFD = "10"; } }

                }
                else
                {
                    if (CFD13 > CFD10) { CFD = "13"; } else { if (CFD10 > CFD05) { CFD = "10"; } }
                }
            }
            
          

            str = str + "series: [";
            for (int i = 0; i < 2; i++)
            {
                string nombre = "";//nombre de la serie
                if (i > 0) { str = str + ","; }
                if (i == 0) { nombre = "Consumos Parciales"; } else { nombre = "Acumulado"; }
                str = str + "{type: 'spline', name: '" + nombre + "', marker: {enabled: false, states :{hover:{enabled: false}}}, data:[";
                sql = "select Fecha_medida, Caudal_M3S from PVYCR_datosAcequias where cod_fuente_dato like '" + CFD + "' AND codigoPVYCR like '" + PC + "' AND idElementomedida like '" + EM + "' AND Fecha_medida>'" + FI + "' AND Fecha_medida<'" + FF + "'";
                //Response.Write(sql);
                DataTable dt2 = DB.GetDataSIGVECTOR(sql);

                double anterior = 0;
                DateTime fecant = DateTime.Parse("01/01/2012") ;
                DateTime fecact;
                TimeSpan ts;
                
                double acumuladoparcial = 0;
                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {
                    try
                    {
                        Convert.ToDouble(dt2.Rows[ii][1].ToString());
                        if (ii == 0)
                        {
                            anterior = Convert.ToDouble(dt2.Rows[0][1].ToString());
                            fecant = DateTime.Parse(dt2.Rows[0][0].ToString());
                        }
                        double valor = 0;

                        if (ii > 0) { str = str + ","; }
                        switch (i)
                        {
                            case 0://parcial
                                fecact = DateTime.Parse(dt2.Rows[ii][0].ToString());
                                ts = fecact - fecant;
                                if (anterior < Convert.ToDouble(dt2.Rows[ii][1].ToString()))
                                { valor = (Convert.ToDouble(dt2.Rows[ii][1].ToString()) - anterior)*ts.TotalSeconds; }
                                else
                                { valor = (anterior - Convert.ToDouble(dt2.Rows[ii][1].ToString())) * ts.TotalSeconds; }
                                
                                anterior = Convert.ToDouble(dt2.Rows[ii][1].ToString());
                                fecant = DateTime.Parse(dt2.Rows[ii][0].ToString());
                                
                                DateTime fecha = DateTime.Parse(dt2.Rows[ii][0].ToString());
                                str = str + "[Date.UTC(" + fecha.Year + "," + Convert.ToString(Convert.ToInt16(fecha.Month) - 1) + "," + fecha.Day + "," + fecha.Hour + "," + fecha.Minute + "," + fecha.Second + ")," + System.Math.Round((valor), 2).ToString().Replace(",", ".") + "]";
                                break;
                            case 1: //acumulado
                                fecact = DateTime.Parse(dt2.Rows[ii][0].ToString());
                                ts = fecact - fecant;
                                if (anterior < Convert.ToDouble(dt2.Rows[ii][1].ToString()))
                                { valor = (Convert.ToDouble(dt2.Rows[ii][1].ToString()) - anterior) * ts.TotalSeconds; }
                                else
                                { valor = (anterior - Convert.ToDouble(dt2.Rows[ii][1].ToString())) * ts.TotalSeconds; }


                                anterior = Convert.ToDouble(dt2.Rows[ii][1].ToString());
                                fecant = DateTime.Parse(dt2.Rows[ii][0].ToString());


                                acumuladoparcial += valor;
                                DateTime fecha2 = DateTime.Parse(dt2.Rows[ii][0].ToString());
                                str = str + "[Date.UTC(" + fecha2.Year + "," + Convert.ToString(Convert.ToInt16(fecha2.Month) - 1) + "," + fecha2.Day + "," + fecha2.Hour + "," + fecha2.Minute + "," + fecha2.Second + ")," + System.Math.Round((acumuladoparcial), 2).ToString().Replace(",", ".") + "]";
                                break;

                        }
                    }
                    catch (Exception eee) { }

                }
                str = str + "]}";

            }
            str += "]";
            string strdef2 = "";
            strdef2 = "<script>" +
            "jQuery(function()" +
                "{var chart2 = new Highcharts.Chart({" +
                    "chart: { renderTo: 'chart2' }," +
                    "navigator: { height: 60 }, " +
                    "title: { text: 'Datos de consumos parciales y absolutos por 05/13', floating: false, align: 'center', x: -20, top: 20 }, " +
                    "yAxis: {title: {text:'Volumen (M3)'}}, " +
                    "xAxis: { type:'datetime',title: {text:'Fecha'}}," +
                    "tooltip: {" +
                        "formatter: function() " +
                        "{" +
                                "return '<b>'+ this.series.name +'</b><br/>'+ " +
                                "'Fecha:' + Highcharts.dateFormat('%e. %b', this.x) +'<br>Volumen '+ this.y +' (M3)'" +
                                       "}" +
                        "}," +
                    " " + str + " });});</script>";
            LIT_grafico2.Text = strdef2;


            #endregion
        }

    }
    public double tratarIncidenciaContadorVolumetrico(double anterior, double actual)
    {
        double valor;
        if (anterior < 10000000) { valor = 10000000 + actual; }
        if (anterior < 1000000)  { valor = 1000000 + actual; }
        if (anterior < 100000)   { valor = 100000 + actual; }
        if (anterior < 10000)    { valor = 10000 + actual; }
        if (anterior < 1000)     { valor = 1000 + actual; }
        return 0;
    }
}
