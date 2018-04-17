using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class SICA_Mailing_EnviaCorreoTitular : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string periodicidad="";
        string SQL = "";
        try 
        { 
            periodicidad = Request.QueryString["P"].ToString();
            SQL = "SELECT * FROM SICA_Mailing_DIRECCIONES Where periodicidad like '" + periodicidad + "'";
        }
        catch { }
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        System.Data.DataTable dt = DB.GetDataDBSICA(SQL);
        for (int i = 0; i<dt.Rows.Count; i++)
        {
            SicaSegura.SICA_Mailing email = new SicaSegura.SICA_Mailing();
            email.Set_asunto("Informe de consumos para la Inscripción " + dt.Rows[i][2].ToString());
            email.Set_cuerpo(generar_cuerpo_correo(dt.Rows[i][1].ToString()));
            email.Set_destinatarios(dt.Rows[i][4].ToString());
            email.Set_destinatarios_cco("jcarril1@tragsa.es");
            email.Set_formatoHTML();
            email.Enviar_mail();
        }

    }
    private string generar_cuerpo_correo(string Inscripcion)
    {
        string Num_insc, Exp_ism, Titular, Concesion;
        
        SicaSegura.SICA_Agrupacion ag = new SicaSegura.SICA_Agrupacion(int.Parse(Inscripcion));
        SicaSegura.SICA_EXP_ISM ISM = new SicaSegura.SICA_EXP_ISM(ag.Get_ExpedienteISM());
        Num_insc = Inscripcion;
        Exp_ism = ISM.get_ExpISM();
        System.Data.DataTable titulares = ISM.get_Titulares();
        Titular = "";
        Concesion = new SicaSegura.SICA_DB().GetDataDBSICA("SELECT Observaciones FROM SICA_Mailing_DIRECCIONES Where IDRaacs like '" + Inscripcion + "'").Rows[0][0].ToString().Trim();
        for (int i = 0; i < titulares.Rows.Count ; i++)
        {
            Titular += titulares.Rows[i][0].ToString() +" "+ titulares.Rows[i][1].ToString() + " - CIF/NIF: " + titulares.Rows[i][2].ToString();
            Titular += "<br>";
        }
        string Cabecera = "<table cellspacing=\"0\" cellpadding=\"0\" border=\"0px\" width=\"100%\"><tr><td border=\"0px\" width=\"100%\"> <img border=\"0\" src=\"http://sica.chsegura.es/SicaSegura/images/cabecera_sica_correo.png\"></td></tr></table><br><br>";
        string Introduccion ="<p align=\"center\"><strong><h1>SICA - Informe diario de consumos ISM </h1></strong></p> <p>&nbsp;</p> <p>Este informe muestra, para el aprovechamiento de referencia, un resumen del volumen consumido en el presente a&ntilde;o hidrol&oacute;gico, considerando las lecturas recibidas por el sistema hasta las 08.00 AM del d&iacute;a en curso o en su defecto la &uacute;ltima m&aacute;s pr&oacute;xima.</p>";
        string TablaDatosInsc = "<p align=\"center\"><table border=\"1\" width=\"80%\"><tbody><tr><td style=\"background-color:#B1B84D;\" colspan=\"2\" width=\"593\"><p align=\"center\"><strong>Datos del aprovechamiento</strong></p></td></tr><tr><td width=\"168\"><p><strong>N&ordm; Inscripci&oacute;n</strong></p></td><td width=\"425\"><p align=\"center\">" + Num_insc + "</p></td></tr><tr><td width=\"168\"><p><strong>Expediente ISM</strong></p></td><td width=\"425\"><p align=\"center\">" + Exp_ism + "</p></td></tr><tr><td width=\"168\"><p><strong>Titular</strong></p></td><td width=\"425\"><p align=\"center\">" + Titular + "</p></td></tr><tr><td width=\"168\"><p><strong>Concesi&oacute;n (m" + ((char)179).ToString() + ")/año</strong></p></td><td width=\"425\"><p align=\"center\">" + double.Parse(Concesion.ToString()).ToString("N0")  + "</p></td></tr></tbody></table></p><br><br><br>";


        string Piedepagina = "<p>*Los datos mostrados en este informe son meramente informativos y serán susceptibles de cambio y/o validación por el Servicio de Aforos y Control de Aprovechamientos.</p><p>Este correo ha sido enviado por un servicio automático en pruebas, está sujeto a posibles fallos o incidencias. Gracias por su comprensión</p>";
        int mes = DateTime.Now.Month;
        int anyo = DateTime.Now.Year;
        DateTime fi_mescurso = DateTime.Parse("01/" + mes.ToString() + "/" + anyo.ToString());
        DateTime ff_mescurso = fi_mescurso.AddMonths(1);
        DateTime fi_acumulado, ff_acumulado;
        if (mes < 10) { fi_acumulado = DateTime.Parse("01/10/" + (anyo - 1).ToString());  ff_acumulado = DateTime.Now; }
        else  { fi_acumulado = DateTime.Parse("01/10/" + anyo.ToString());  ff_acumulado = DateTime.Now; }
        double Consumo_total_Inscripcion_Q = 0;
        double Consumo_total_Inscripcion_V = 0;
        double Consumo_total_Inscripcion = 0;
        System.Data.DataTable puntos = ag.Get_ListadoPuntosConSigno();
        string TablaConsumos = "";
        string TablaConsumosContador = "<p align=\"center\"><table border=\"1\" width=\"80%\"><tbody><tr><td style=\"background-color:#B1B84D;width:25%;\"><p align=\"center\"><strong>Captación</strong></p></td><td style=\"background-color:#B1B84D;width:25%;\"><p align=\"center\"><strong>Lectura Inicial Año Hidrológico(m" + ((char)179).ToString() + ")</strong></p></td>";
        string TablaConsumosCaudalimetro = "<p align=\"center\"><table border=\"1\" width=\"80%\"><tbody><tr><td style=\"background-color:#B1B84D;width:25%;\"><p align=\"center\"><strong>Captación</strong></p></td><td style=\"background-color:#B1B84D;width:25%;\"><p align=\"center\"><strong></strong></p></td><td></td><td></td></tr>";
        TablaConsumosContador += "<td style=\"background-color:#B1B84D;width:25%;\"><p align=\"center\"><strong>Última lectura Año Hidrológico(m" + ((char)179).ToString() + ")</strong></p></td><td style=\"background-color:#B1B84D;width:25%;\"><p align=\"center\"><strong>Consumo acumulado A&ntilde;o Hidrol&oacute;gico (m" + ((char)179).ToString() + ")</strong></p></td></tr>";

        for (int i = 0; i < puntos.Rows.Count; i++)
        {
            
            string PVYCR = puntos.Rows[i][0].ToString().Trim();
            string EM = puntos.Rows[i][1].ToString().Trim();
            SicaSegura.SICA_PuntoControl puntoSICA = new SicaSegura.SICA_PuntoControl(PVYCR);
            
            double Lec_in = 0;
            double Lec_fin = 0;
            double consumo_mes_curso = 0;
            double consumo_anyo_hid = 0;
            
            if (EM.Contains("V")) 
            {
                SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
                string SQL = "";
                SQL = "SELECT TOP(1) LecturaContador_M3 FROM PVYCR_Datosmotores where codigoPVYCR like'" + PVYCR.Trim() + "' AND idelementomedida like '" + EM.Trim() + "'  and Fecha_medida >'" + fi_acumulado + "' order by fecha_medida asc";
                try { Lec_in = double.Parse(DB.GetDataSIGVECTOR(SQL).Rows[0][0].ToString()); }
                catch { Lec_in = 0; }
                SQL = "SELECT TOP(1) LecturaContador_M3 FROM PVYCR_Datosmotores where codigoPVYCR like'" + PVYCR.Trim() + "' AND idelementomedida like '" + EM.Trim() + "'  and Fecha_medida <'" + ff_acumulado + "' order by fecha_medida desc";
                try { Lec_fin = double.Parse(DB.GetDataSIGVECTOR(SQL).Rows[0][0].ToString()); }
                catch { Lec_in = 0; }
                
                SicaSegura.SICA_Calculos_Consumos Datos = new SicaSegura.SICA_Calculos_Consumos();
                SicaSegura.SICA_ElementoMedida ElemMed = new SicaSegura.SICA_ElementoMedida(puntoSICA, EM, "10#13".Split('#'));  
                consumo_anyo_hid = puntoSICA.ConsumoPorVolumetrico(fi_acumulado,ff_acumulado);
                SicaSegura.AccesoADatos3 AD = new SicaSegura.AccesoADatos3();
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(DB.GetCadenaConexionSIGVECTOR().ToString());
                
                consumo_anyo_hid = double.Parse(AD.obtenerAcumulado(PVYCR.ToString(), EM.ToString(),cnn, fi_acumulado.ToShortDateString(), ff_acumulado.ToShortDateString())[1].ToString());
                Consumo_total_Inscripcion_V += consumo_anyo_hid;
                
                //consumo_anyo_hid = ElemMed.Get_consumo_hidrologico_en_curso();
                if (puntos.Rows[i][2].ToString().Trim() == "+") { Consumo_total_Inscripcion += consumo_anyo_hid; }
                else if (puntos.Rows[i][2].ToString().Trim() == "+") { Consumo_total_Inscripcion -= consumo_anyo_hid; }
                else  { Consumo_total_Inscripcion += consumo_anyo_hid;

                }
                TablaConsumosContador += "<tr><td><strong>" + puntos.Rows[i][2].ToString().Trim() + puntos.Rows[i][0].ToString().Trim() + "</strong></td><td><p align=\"right\">" + Lec_in.ToString("N0") + "</p></td><td><p align=\"right\">" + Lec_fin.ToString("N0") + "</p></td><td><p align=\"right\">" + consumo_anyo_hid.ToString("N0") + "</p></td></tr>";
            }
            if (EM.Contains("Q"))   
            {
                SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
                SicaSegura.SICA_Calculos_Consumos Datos = new SicaSegura.SICA_Calculos_Consumos();
                SicaSegura.SICA_ElementoMedida ElemMed = new SicaSegura.SICA_ElementoMedida(puntoSICA, EM, "10#13".Split('#'));  
                //Datos.Calculos_de_consumo_POR_CFDs = true;
                //Datos.CFDs = "10#13".Split('#');
                //consumo_anyo_hid = double.Parse (Datos.obtenerAcumulado(PVYCR,EM, new System.Data.SqlClient.SqlConnection(DB.GetCadenaConexionSIGVECTOR()),fi_acumulado.ToShortDateString(),ff_acumulado.ToShortDateString())[1].ToString());
                //consumo_anyo_hid = puntoSICA.ConsumoPorVolumetrico(fi_acumulado,ff_acumulado);
                consumo_anyo_hid = ElemMed.Get_consumo_hidrologico_en_curso();
                if (puntos.Rows[i][2].ToString().Trim() == "+") { Consumo_total_Inscripcion += consumo_anyo_hid; }
                else if (puntos.Rows[i][2].ToString().Trim() == "-") { Consumo_total_Inscripcion -= consumo_anyo_hid; }
                else { Consumo_total_Inscripcion += consumo_anyo_hid; }
                TablaConsumosCaudalimetro += "<tr><td><strong>" + puntos.Rows[i][2].ToString().Trim() + puntos.Rows[i][0].ToString().Trim() + "</strong></td><td><p align=\"right\">" + Lec_in.ToString("N0") + "</p></td><td><p align=\"right\">" + Lec_fin.ToString("N0") + "</p></td><td><p align=\"right\">" + consumo_anyo_hid.ToString("N0") + "</p></td></tr>";
            }
            if (EM.Contains("E"))   { }
            if (EM.Contains("H"))   { }
            
        }


        TablaConsumosContador += "<tr><td><strong>TOTAL CIRCULANTE</strong></td><td></td><td></td><td><p align=\"right\"><strong>" + Consumo_total_Inscripcion_V.ToString("N0") + "</strong></p></td></tr>";
        TablaConsumosCaudalimetro += "<tr><td><strong>TOTAL CIRCULANTE</strong></td><td></td><td></td><td><p align=\"right\"><strong>" + Consumo_total_Inscripcion_Q.ToString("N0") + "</strong></p></td></tr>";
        TablaConsumosContador += "</table><br><br>";
        TablaConsumosCaudalimetro += "</table><br><br>";

        //string mes1 = ag.CalculoConsumido(int.Parse(Inscripcion), DateTime.Parse("01/10/2016"), DateTime.Parse("01/11/2016")).ToString();
        //string mes2 = ag.CalculoConsumido(int.Parse(Inscripcion), DateTime.Parse("01/11/2016"), DateTime.Parse("01/12/2016")).ToString();
        //string mes3 = ag.CalculoConsumido(int.Parse(Inscripcion), DateTime.Parse("01/12/2016"), DateTime.Parse("01/01/2011")).ToString();
        //int acum1 = 0 + int.Parse(mes1);
        //int acum2 = acum1 + int.Parse(mes2);
        //int acum3 = acum2 + int.Parse(mes3);
        //TablaConsumosContador+="<tr><td width=\"140\"><p>Octubre 2016</p></td><td width=\"227\"><p>" + mes1 + "</p></td><td width=\"227\"><p>" + acum1 + "</p></td></tr><tr><td width=\"140\"><p>Noviembre 2016</p></td><td width=\"227\">";
        //TablaConsumosContador+="<p>" + mes2 + "</p></td><td width=\"227\"><p>" + acum2 + "</p></td></tr><tr><td width=\"140\"><p>Diciembre 2016</p></td><td width=\"227\"><p>" + mes3 + "</p>";
        //TablaConsumosContador+="</td><td width=\"227\"><p>" + acum3 + "</p></td></tr></tbody></table></p>";
        
        string cuerpo = "<center><table style=\"  border: 2px solid black;\" width=\"500\"><tr><td>" + Cabecera + Introduccion + TablaDatosInsc + TablaConsumosContador + Piedepagina + "</td></tr></table></center>";
        return cuerpo;
    }

}
