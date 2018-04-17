using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using GuarderiaFluvial;

public partial class SICAH_Consumos : System.Web.UI.Page
{
    private Sicadll.AccesoADatos.AccesoADatos2 sicadll = new Sicadll.AccesoADatos.AccesoADatos2();
    private SicaSegura.SICA_DB sicadb = new SicaSegura.SICA_DB();
    private string inscripcion = "";
    private string anyo = "";
    private DateTime FI, FF;
    private string htmltxt ="";
    private string incremento="";
    private System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["PDASIGVECTOR"].ConnectionString.ToString());
    private DataTable dtpuntos;
    DateTime FUP = new DateTime();
    protected void Page_Load(object sender, EventArgs e)
    {
        string SQL = "";
        inscripcion = Request.QueryString["NUMPAL"].ToString();
        incremento = Request.QueryString["dias"].ToString();
        if (Request.QueryString["FI"] != null)
        {
            FI = DateTime.Parse(Request.QueryString["FI"].ToString());
        }
        
        RecuperaPuntosatratar();
        switch (incremento)
        {
            case "D":
                break;
            case "Q":
                break;
            case "M":
                InformeMensual();
                break;
            case "2M":
                break;
            case "3M":
                break;
            case "6M":
                break;
            case "Y":
                InformeAnual();
                break;
        }        
        LIT_Informe.Text = htmltxt;
    }
    private void RecuperaPuntosatratar()
    {
        string SQL = "SELECT P.ID, P.CodigoPVYCR, P.EM, P.CFD, P.TOC, P.FI, P.FF " +
                "FROM [SICA_SIST_Sistemas-Punto] AS SP INNER JOIN " +
                "SICA_SIST_PuntoSistema AS P ON P.ID = SP.ID_PuntoSistema INNER JOIN " +
                "SICA_SIST_Sistemas ON SP.ID_Sistema = SICA_SIST_Sistemas.ID " +
                "WHERE (SICA_SIST_Sistemas.NumInscripcion = " + inscripcion + ")";
        dtpuntos = sicadb.GetDataDBSICA(SQL);
    }
    private void InformeAnual()
    {
        int anyoinicio = 2003;
        FI = DateTime.Parse("01/10/" + anyoinicio.ToString());
        if (DateTime.Now.Month < 10)
        {
            anyo = Convert.ToString(DateTime.Now.Year - 1);
        }
        else
        {
            anyo = Convert.ToString(DateTime.Now.Year);
        }
        FF = DateTime.Parse("30/09/" + (anyoinicio+1).ToString());

        htmltxt += "<table style=\"width:200mm; border: solid 1px black;\">";
        htmltxt += "<tr style=\"font-weight:bold;\"><td> Inicio Periodo </td><td> Fin Periodo </td><td> Consumido Periodo (m3)</td><td> Acumulado (m3) </td></tr>";
        double totalizador = 0;
        double totalizador0 = 0;
        while (FI < DateTime.Parse("01/10/" + Convert.ToUInt16(Convert.ToUInt16(anyo) + 1).ToString()))
        {
            double parcial = 0;
            totalizador0 = 0;
            for (int i = 0; i < dtpuntos.Rows.Count; i++)
            {
                object[] a = sicadll.obtenerAcumulado(dtpuntos.Rows[i].ItemArray.GetValue(1).ToString(), dtpuntos.Rows[i].ItemArray.GetValue(2).ToString(), cnn, FI.Day.ToString() + "/" + FI.Month.ToString() + "/" + FI.Year.ToString(), FF.Day.ToString() + "/" + FF.Month.ToString() + "/" + FF.Year.ToString());
                parcial = Convert.ToDouble(a[1]);
                totalizador0 += parcial;
                totalizador += parcial;
            }
            htmltxt += "<tr><td>" + FI.ToShortDateString() + "</td><td>" + FF.ToShortDateString() + "</td><td>" + totalizador0 + "</td><td>" + totalizador + "</td></tr>";
            FI = FI.AddYears(1);
            FF = FF.AddYears(1);  
        }
        htmltxt += "</table>";
    }
    private void InformeMensual()
    {
        int anyoinicio;
        

        if (DateTime.Now.Month < 10){anyoinicio = DateTime.Now.Year - 1;}
        else{anyoinicio = DateTime.Now.Year;}

        if (FI.Year < 2000) { FI = DateTime.Parse("01/10/"+anyoinicio.ToString()); }

        htmltxt += "<table style=\"width:200mm; border: solid 1px black;\">";
        htmltxt += "<tr style=\"font-weight:bold;\"><td> Inicio Periodo </td><td> Fin Periodo </td><td> Consumido Periodo (m3)</td><td> Acumulado (m3) </td></tr>";
        
        double acumuladodelmes = 0;
        double acumuladodelmes2 = 0;
        double acumuladodelanyo = 0;
        double acumuladodelanyo2 = 0;
        object[] o = new object[5];
        for (int i =0; i<12 ;i++)
        {
            if (FI < DateTime.Now)
            {
                foreach (DataRow row in dtpuntos.Rows)
                {
                    acumuladodelmes += calculaconsumo1mes(row.ItemArray.GetValue(1).ToString(), row.ItemArray.GetValue(2).ToString(), FI.Month, FI.Year);
                    if (FI.Month == DateTime.Now.Month)
                    {
                        o = sicadll.obtenerAcumulado(row.ItemArray.GetValue(1).ToString(), row.ItemArray.GetValue(2).ToString(), cnn, FI.ToShortDateString(), DateTime.Now.ToShortDateString()); 
                    }
                    else { o = sicadll.obtenerAcumulado(row.ItemArray.GetValue(1).ToString(), row.ItemArray.GetValue(2).ToString(), cnn, FI.ToShortDateString(), FI.AddMonths(1).ToShortDateString()); }
                    acumuladodelmes2 += Convert.ToDouble(o[1]);
                }
                acumuladodelanyo += acumuladodelmes;
                acumuladodelanyo2 += acumuladodelmes2;
                htmltxt += "<tr><td>" + FI.ToShortDateString() + "</td><td>" + FI.AddMonths(1).ToShortDateString() + "</td><td>" + System.Math.Round(acumuladodelmes, 3) + "</td><td>" + System.Math.Round(acumuladodelmes2, 3) + "</td><td>" + System.Math.Round(acumuladodelanyo, 3) + "</td><td>" + System.Math.Round(acumuladodelanyo2, 3) + "</td></tr>";
                FI = FI.AddMonths(1);
            }
            else { i = 13; }
        }
        htmltxt += "</table>";
    }
    private double calculaconsumo1mes(string punto, string EM, int mes, int anyo)
    {
        string SQL = "";
        double consumo = 0;
        double consumoprimerdia = 0;
        double consumoultimodia = 0;
        
        DataTable dtdatos = new DataTable();
        DateTime fecha = DateTime.Parse("01/" + mes.ToString() + "/" + anyo.ToString());
        if (EM.Contains('V'))
        {
            SQL = "(SELECT top(1) D.* FROM PVYCR_DatosMotores D  where D.CodigoPVYCR like '" + punto + "' AND D.Fecha_Medida<'" + fecha + "' AND D.Fecha_Medida>'" + fecha.AddDays(-15) + "' order by fecha_medida ) " +
                "UNION All " +
                "(SELECT top(1) D2.* FROM PVYCR_DatosMotores D2  where D2.CodigoPVYCR like '" + punto + "' AND D2.Fecha_Medida>'" + fecha + "' AND D2.Fecha_Medida<'" + fecha.AddDays(15) + "' order by fecha_medida desc )";
        }
        if (EM.Contains('H'))
        {
            SQL = "SELECT top(1) D.* FROM PVYCR_Datoshorometros D  where D.CodigoPVYCR like '" + punto + "' AND D.Fecha_Medida<'" + fecha + "' AND D.Fecha_Medida>'" + fecha.AddDays(-15) + "' order by fecha_medida  " +
                    "UNION All " +
                    "SELECT top(1) D2.* FROM PVYCR_Datoshorometros D2  where D2.CodigoPVYCR like '" + punto + "' AND D2.Fecha_Medida>'" + fecha + "' AND D2.Fecha_Medida<'" + fecha.AddDays(15) + "' order by fecha_medida desc  ";
        }
        if (EM.Contains('E'))
        {
            SQL = "SELECT top(1) D.* FROM PVYCR_DatosAlimentacion D  where D.CodigoPVYCR like '" + punto + "' AND D.Fecha_Medida<'" + fecha + "' AND D.Fecha_Medida>'" + fecha.AddDays(-15) + "' order by fecha_medida  " +
                    "UNION All " +
                    "SELECT top(1) D2.* FROM PVYCR_DatosAlimentacion D2  where D2.CodigoPVYCR like '" + punto + "' AND D2.Fecha_Medida>'" + fecha + "' AND D2.Fecha_Medida<'" + fecha.AddDays(15) + "'  order by fecha_medida desc ";
        }
        dtdatos = sicadb.GetDataSIGVECTOR(SQL);
        if (dtdatos.Rows.Count == 1)
        {
            if (DateTime.Parse(dtdatos.Rows[0].ItemArray.GetValue(3).ToString()) > DateTime.Parse("01/" + mes.ToString() + "/" + anyo) &&
                DateTime.Parse(dtdatos.Rows[0].ItemArray.GetValue(3).ToString()) < DateTime.Parse("01/" + (mes + 1).ToString() + "/" + anyo))
            {
                consumoprimerdia = Convert.ToDouble(dtdatos.Rows[0].ItemArray.GetValue(4).ToString());
            }
        }
        else 
        {
            if (dtdatos.Rows.Count == 0)
            {
                consumoprimerdia = 0;
            }
            else
            {
                if (dtdatos.Rows.Count == 2)
                {
                    double[,] puntos = new double[3, 2];
                    puntos = Comprueba_incidencias(dtdatos);
                    consumoprimerdia = interpolar(puntos);
                }
            }
        }
        
        
        fecha = fecha.AddMonths(1);
        if (EM.Contains('V'))
        {
            SQL = "(SELECT top(1) D.* FROM PVYCR_DatosMotores D  where D.CodigoPVYCR like '" + punto + "' AND D.Fecha_Medida<'" + fecha + "' AND D.Fecha_Medida>'" + fecha.AddDays(-15) + "' order by fecha_medida  )" +
                "UNION All " +
                "(SELECT top(1) D2.* FROM PVYCR_DatosMotores D2  where D2.CodigoPVYCR like '" + punto + "' AND D2.Fecha_Medida>'" + fecha + "' AND D2.Fecha_Medida<'" + fecha.AddDays(15) + "' order by fecha_medida desc  )";
        }
        if (EM.Contains('H'))
        {
            SQL = "SELECT top(1) D.* FROM PVYCR_Datoshorometros D  where D.CodigoPVYCR like '" + punto + "' AND D.Fecha_Medida<'" + fecha + "' AND D.Fecha_Medida>'" + fecha.AddDays(-15) + "'  order by fecha_medida  " +
                    "UNION All " +
                    "SELECT top(1) D2.* FROM PVYCR_Datoshorometros D2  where D2.CodigoPVYCR like '" + punto + "' AND D2.Fecha_Medida>'" + fecha + "' AND D2.Fecha_Medida<'" + fecha.AddDays(15) + "' order by fecha_medida desc  ";
        }
        if (EM.Contains('E'))
        {
            SQL = "SELECT top(1) D.* FROM PVYCR_DatosAlimentacion D  where D.CodigoPVYCR like '" + punto + "' AND D.Fecha_Medida<'" + fecha + "' AND D.Fecha_Medida>'" + fecha.AddDays(-15) + "' order by fecha_medida  " +
                    "UNION All " +
                    "SELECT top(1) D2.* FROM PVYCR_DatosAlimentacion D2  where D2.CodigoPVYCR like '" + punto + "' AND D2.Fecha_Medida>'" + fecha + "' AND D2.Fecha_Medida<'" + fecha.AddDays(15) + "' order by fecha_medida desc  ";
        }
        dtdatos = sicadb.GetDataSIGVECTOR(SQL);

        if (dtdatos.Rows.Count == 1)
        {
            if (DateTime.Parse(dtdatos.Rows[0].ItemArray.GetValue(3).ToString()) > DateTime.Parse("01/" + mes.ToString() + "/" + anyo) &&
                DateTime.Parse(dtdatos.Rows[0].ItemArray.GetValue(3).ToString()) < DateTime.Parse("01/" + (mes + 1).ToString() + "/" + anyo))
            {
                consumoultimodia = Convert.ToDouble(dtdatos.Rows[0].ItemArray.GetValue(4).ToString());
            }
        }
        else
        {
            if (dtdatos.Rows.Count == 0)
            {
                consumoultimodia = consumoprimerdia;
            }
            else
            {
                if (dtdatos.Rows.Count == 2)
                {
                    double[,] puntos = new double[3, 2];
                    puntos = Comprueba_incidencias(dtdatos);
                    consumoultimodia = interpolar(puntos);
                }
            }
        }
        
        if (consumoultimodia - consumoprimerdia > 0) {consumo = consumoultimodia - consumoprimerdia; }
        return consumo;    
    }

    private double [,] Comprueba_incidencias(DataTable dtdatos)
    {
        TimeSpan span2 = DateTime.Parse(dtdatos.Rows[1].ItemArray.GetValue(3).ToString()) - DateTime.Parse(dtdatos.Rows[0].ItemArray.GetValue(3).ToString());
        TimeSpan span = DateTime.Parse(FI.ToShortDateString()) - DateTime.Parse(dtdatos.Rows[0].ItemArray.GetValue(3).ToString());
        //PVYCR     EM      CFD     FEC     LECTURA 
        //  0       1       2       3       4
        double[,] puntos = new double[3, 2];
        puntos[0, 1] = 0;
        puntos[1, 1] = span.Days;
        puntos[2, 1] = span2.Days;

        puntos[0, 0] = Convert.ToDouble(dtdatos.Rows[0].ItemArray.GetValue(4).ToString());
        puntos[1, 0] = 0;
        puntos[2, 0] = Convert.ToDouble(dtdatos.Rows[1].ItemArray.GetValue(4).ToString());

        int inc0 = 0, inc1 = 0;
        try { inc0 = Convert.ToInt16(dtdatos.Rows[0].ItemArray.GetValue(8).ToString()); }
        catch { }
        try { inc1 = Convert.ToInt16(dtdatos.Rows[1].ItemArray.GetValue(8).ToString()); }
        catch { }
        if ((inc0 == 6) || (inc1 == 7))
        {
            double adicional=0;
            try { Convert.ToDouble(dtdatos.Rows[1].ItemArray.GetValue(9).ToString()); }
            catch { }
            switch (inc1)
            {
                case 6:// Reseteo Contador
                    if (puntos[0, 0] > 100000)//Se resetea al 1.000.000
                    {
                        puntos[2, 0] = (1000000 + adicional );
                    }
                    else // se resetea al 100.000
                    {
                        puntos[2, 0] = (100000 + adicional);
                    }
                    break;
                case 7:// Cambio de contador
                    if (puntos[0, 0] > 100000)
                    {
                        puntos[2, 0] = puntos[0, 0] + adicional;
                    }
                    break;
            }
        }
        try { inc0 = Convert.ToInt16(dtdatos.Rows[0].ItemArray.GetValue(8).ToString()); }
        catch { }
        try { inc1 = Convert.ToInt16(dtdatos.Rows[1].ItemArray.GetValue(8).ToString()); }
        catch { }
        if ((inc0 == 6) || (inc0 == 7))
        {
            double adicional = 0;
            try { Convert.ToDouble(dtdatos.Rows[1].ItemArray.GetValue(9).ToString()); }
            catch { }
            switch (inc0)
            {
                case 6:// Reseteo Contador
                    puntos[0, 0] = (puntos[0, 0] + adicional);
                    break;
                case 7:// Cambio de contador
                    puntos[0, 0] = (puntos[0, 0] + adicional);
                    break;
            }
        }
        if (puntos[2, 0] - puntos[0, 0] < 0)
        {
            puntos[0, 0] = puntos[2, 0];
        }
        return puntos;
    }
    
    private double interpolar(double[,] matriz)
    {
        
        double X1, Y1, X2, Y2, X3, Y3, M;

        X1 = matriz[0, 1];
        Y1 = matriz[0, 0];

        X2 = matriz[1, 1];
        Y2 = matriz[1, 0];

        X3 = matriz[2, 1];
        Y3 = matriz[2, 0];

        M=(Y3-Y1)/(X3-X1);

        Y2=M*(X2-X1)+Y1;


        return Y2;
    }
}
