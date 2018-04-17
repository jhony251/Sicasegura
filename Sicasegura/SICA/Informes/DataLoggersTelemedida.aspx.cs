using System;
using System.Data;

public partial class SICA_informes_DataLoggersTelemedida : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Recargar_lista_de_direcciones();
        //Creamos la conexión a la base de datos.
        //conexionSICA = conexionSQL.Crear_Conexion("Data Source=10.31.224.74;Initial Catalog=DBSICA;Persist Security Info=True;User ID=Aplisica;Password=Parafina19");
        //DataSet ds = new DataSet();
        //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionSICA, "SELECT * FROM SICA_configuraciones WHERE ((Servidor LIKE 'galileo')AND((nombre LIKE 'connection_string')))"));
        //ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();
        //conexionCHS = conexionSQL.Crear_Conexion(ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim());
        this.genera_informe();
        //conexionCHS = conexionSQL.Crear_Conexion("Data Source=10.31.224.74;Initial Catalog=SIGVECTOR;Persist Security Info=True;User ID=PVYCR_avanzado;Password=Estambul45");

        //this.enviaratodos();
    }
    #region Parte de la generación del informe
    private string genera_cuadro_resumen_informe()
    {
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        string HTML = "";
        string sql = @"SELECT  Titular as [Titular de equipos], Validado [ESTADO EQUIPOS], COUNT(Tipo) AS [TOTAL EQUIPOS]
                           FROM (SELECT TOP (100) PERCENT Punto_control AS [Codigo SICA], caracteristica2, Caracteristica1 AS [EXP ISM], Alegacion AS Titular, UTMx AS Tipo, 
                                        UTMy AS Configurado, EnviandoDatos AS Operativo, CargandoDatos AS Validado, caracteristica3, caracteristica4
                                 FROM   SICA_Puntos_control
                                 WHERE  utmx=1 AND ((Alegacion LIKE 'CHS') OR (Alegacion LIKE 'titular'))AND utmy=1 and enviandodatos like 'SI'
                                 ORDER BY [EXP ISM]) AS derivedtbl_1
                           GROUP BY Titular, Tipo, Configurado, Operativo, Validado";

        //DataSet ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionSICA, sql));
        DataSet ds = DB.GetDataDBSICA_DS(sql);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        HTML += "<center><h1>Resumen estado de telemedidas en la D.H. del Segura</h1><br><br><table width=\"80%\"><tr>";
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            HTML += "<th style=\"border:1px solid black;\">" + dt.Columns[i].ColumnName.ToString().Trim() + "</th>";
        }
        HTML += "</tr>";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            HTML += "<tr>";
            for (int u = 0; u < dt.Columns.Count; u++)
            {
                string valor = dt.Rows[i][u].ToString().Trim();
                valor = valor.Replace("NO", "NO VALIDADOS");
                valor = valor.Replace("SI", "VALIDADOS");
                valor = valor.Replace("TITULAR", "PRIVADO");
                HTML += "<td style=\"border:1px solid black;\">" + valor + "</td>";
            }
            HTML += "</tr>";
        }
        int total = 0;
        sql = @"SELECT  Count(Validado) 
                           FROM (SELECT TOP (100) PERCENT Punto_control AS [Codigo SICA], caracteristica2, Caracteristica1 AS [EXP ISM], Alegacion AS Titular, UTMx AS Tipo, 
                                        UTMy AS Configurado, EnviandoDatos AS Operativo, CargandoDatos AS Validado, caracteristica3, caracteristica4
                                 FROM   SICA_Puntos_control
                                 WHERE  utmx=1 AND ((Alegacion LIKE 'CHS') OR (Alegacion LIKE 'titular'))AND utmy=1 and enviandodatos like 'SI'
                                 ORDER BY [EXP ISM]) AS derivedtbl_1
                    WHERE Validado like 'SI'";
        //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionSICA, sql));
        ds = DB.GetDataDBSICA_DS(sql);
        dt = new DataTable();
        dt = ds.Tables[0];
        HTML += "<tr><td><b>TOTAL EQUIPOS VALIDADOS</b></td><td></td><td><b>" + dt.Rows[0][0].ToString().Trim() + "</b></td></tr>";
        total += int.Parse(dt.Rows[0][0].ToString().Trim());
        sql = @"SELECT  Count(Validado) 
                           FROM (SELECT TOP (100) PERCENT Punto_control AS [Codigo SICA], caracteristica2, Caracteristica1 AS [EXP ISM], Alegacion AS Titular, UTMx AS Tipo, 
                                        UTMy AS Configurado, EnviandoDatos AS Operativo, CargandoDatos AS Validado, caracteristica3, caracteristica4
                                 FROM   SICA_Puntos_control
                                 WHERE  utmx=1 AND ((Alegacion LIKE 'CHS') OR (Alegacion LIKE 'titular'))AND utmy=1 and enviandodatos like 'SI'
                                 ORDER BY [EXP ISM]) AS derivedtbl_1
                    WHERE Validado like 'NO'";
        //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionSICA, sql));
        ds = DB.GetDataDBSICA_DS(sql);
        dt = new DataTable();
        dt = ds.Tables[0];
        HTML += "<tr><td><b>TOTAL EQUIPOS SIN VALIDACION</b></td><td></td><td><b>" + dt.Rows[0][0].ToString().Trim() + "</b></td></tr>";
        total += int.Parse(dt.Rows[0][0].ToString().Trim());
        sql = @"SELECT  Count(Validado) 
                           FROM (SELECT TOP (100) PERCENT Punto_control AS [Codigo SICA], caracteristica2, Caracteristica1 AS [EXP ISM], Alegacion AS Titular, UTMx AS Tipo, 
                                        UTMy AS Configurado, EnviandoDatos AS Operativo, CargandoDatos AS Validado, caracteristica3, caracteristica4
                                 FROM   SICA_Puntos_control
                                 WHERE  utmx=1 AND ((Alegacion LIKE 'CHS') OR (Alegacion LIKE 'titular'))AND utmy=1 and enviandodatos like 'SI'
                                 ORDER BY [EXP ISM]) AS derivedtbl_1
                    WHERE Validado like 'I1'";
        //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionSICA, sql));
        ds = DB.GetDataDBSICA_DS(sql);
        dt = new DataTable();
        dt = ds.Tables[0];
        HTML += "<tr><td><b>TOTAL I1</b></td><td></td><td><b>" + dt.Rows[0][0].ToString().Trim() + "</b></td></tr>";
        total += int.Parse(dt.Rows[0][0].ToString().Trim());
        sql = @"SELECT  Count(Validado) 
                           FROM (SELECT TOP (100) PERCENT Punto_control AS [Codigo SICA], caracteristica2, Caracteristica1 AS [EXP ISM], Alegacion AS Titular, UTMx AS Tipo, 
                                        UTMy AS Configurado, EnviandoDatos AS Operativo, CargandoDatos AS Validado, caracteristica3, caracteristica4
                                 FROM   SICA_Puntos_control
                                 WHERE  utmx=1 AND ((Alegacion LIKE 'CHS') OR (Alegacion LIKE 'titular'))AND utmy=1 and enviandodatos like 'SI'
                                 ORDER BY [EXP ISM]) AS derivedtbl_1
                    WHERE Validado like 'I2'";
        //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionSICA, sql));
        ds = DB.GetDataDBSICA_DS(sql);
        dt = new DataTable();
        dt = ds.Tables[0];
        HTML += "<tr><td><b>TOTAL I2</b></td><td></td><td><b>" + dt.Rows[0][0].ToString().Trim() + "</b></td></tr>";
        total += int.Parse(dt.Rows[0][0].ToString().Trim());
        HTML += "<tr><td><b>TOTAL EQUIPOS INSTALADOS EN D.H. SEGURA</b></td><td></td><td><b>" + total.ToString().Trim() + "</b></td></tr>";
        HTML += "</table></center><br><br>";
        HTML += "<p align=\"Left\">";
        HTML += "<u>Leyenda</u><br><br>";
        HTML += "VALIDADOS - Equipo emitiendo correctamente<br>";
        HTML += "NO VALIDADOS - Equipo emitiendo datos, dato no consolidado<br><br>";
        HTML += "I1 - Registrador no comunica correctamente<br>";
        HTML += "I2 - Indice de contador desincronizado con registrador<br>";
        HTML += "I9 - Registrador retirado";
        HTML += "</p><br><br><br>";
        return HTML;
    }

    private void genera_informe()
    {
        bool incidencia = false;
        bool incidenciaanterior = false;
        bool incidenciasiguiente = false;
        bool incidenciafecha = false;
        bool incidenciapulsos = false;
        string EM = "";
        string PVYCR = "";
        string[] DatoPVYCR;
        string[] anterior;
        string[] Siguiente;
        string[] UltimoSica;
        string HTMLBoby = "";
        string HTMLTablaInci = "";
        string HTMLTablaNormal = "";
        string HTMLTablaInciPulsos = "";
        string Error = "&nbsp;";
        string stylefilaroja = ""; string stylefilaroja1 = ""; string stylefilaroja2 = ""; string stylefilaroja3 = "";


        string fechaUltMantenimiento;
        string tipoUltMantenimiento;
        string fabricante;
        string unidadIng;
        double margenError;
        int maxDiasSinComunicacion = 15;
        int equiposConInci = 0;
        int equiposConInci2 = 0;
        int equiposSinInci = 0;

        HTMLBoby += "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//ES\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\" ><head><title>Utilidades SICA</title><meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\"><style> body {font-family: Verdana;	font-style: normal;	font-variant: normal;font-weight: normal; font-size: 9pt; line-height: 100%; word-spacing: normal; letter-spacing: normal; text-decoration: none; text-transform: none;	text-align: left; text-indent: 0ex;}  #tabledatos { border-collapse: collapse; border: 1px solid black; }</style></head><body bgcolor=\"#FFFFDD\"><table width=\"100%\"><tr><td style=\"background-image:url('http://sicasegura.tragsatec.es/images/cabeceraSicaSegura.jpg')\"><br><br><br><br><br></td></tr></table><hr>"; ;
        HTMLBoby += "   ";

        HTMLBoby += "   <center>";
        HTMLBoby += "   <img src=\"http://sica.chsegura.es/sicasegura/images/cabecera_sica_correo.png\">";
        HTMLBoby += "   <br><br><br>";
        HTMLBoby += genera_cuadro_resumen_informe();
        HTMLBoby += "   <br><br><br>";

        HTMLTablaInci += "<h1>I1 - Registrador no comunica correctamente</h1>";

        HTMLTablaInci += "       <table width=95% cellspacing=0 cellpadding=5 style='border:1px solid Black'>";
        HTMLTablaInci += "           <tr bgcolor='gray'>";
        HTMLTablaInci += "               <td style='border:1px solid Black'>Nº</td>";
        HTMLTablaInci += "               <td style='border:1px solid Black'>Cod. SICA</td>";
        HTMLTablaInci += "               <td style='border:1px solid Black'>EM</td>";
        HTMLTablaInci += "               <td style='border:1px solid Black'>Desc.</td>";
        HTMLTablaInci += "               <td style='width:15%; border:1px solid Black'>Lectura SICA previa</td>";
        HTMLTablaInci += "               <td style='width:15%; border:1px solid Black'>última Lectura Manual</td>";
        HTMLTablaInci += "               <td style='width:15%; border:1px solid Black'>Lectura SICA siguiente</td>";
        HTMLTablaInci += "               <td style='width:15%; border:1px solid Black'>Última lectura SICA</td>";
        HTMLTablaInci += "               <td style='width:15%; border:1px solid Black'>Titular/Fabricante</td>";
        HTMLTablaInci += "               <td style='border:1px solid Black'>Uds Error</td>";
        HTMLTablaInci += "           </tr>";

        HTMLTablaInciPulsos += "<h1>I2 - Indice de contador desincronizado con registrador</h1>";
        HTMLTablaInciPulsos += "       <table width=95% cellspacing=0 cellpadding=5 style='border:1px solid Black'>";
        HTMLTablaInciPulsos += "           <tr bgcolor='gray'>";
        HTMLTablaInciPulsos += "               <td style='border:1px solid Black'>Nº</td>";
        HTMLTablaInciPulsos += "               <td style='border:1px solid Black'>Cod. SICA</td>";
        HTMLTablaInciPulsos += "               <td style='border:1px solid Black'>EM</td>";
        HTMLTablaInciPulsos += "               <td style='border:1px solid Black'>Desc.</td>";
        HTMLTablaInciPulsos += "               <td style='width:15%; border:1px solid Black'>Lectura SICA previa</td>";
        HTMLTablaInciPulsos += "               <td style='width:15%; border:1px solid Black'>última Lectura Manual</td>";
        HTMLTablaInciPulsos += "               <td style='width:15%; border:1px solid Black'>Lectura SICA siguiente</td>";
        HTMLTablaInciPulsos += "               <td style='width:15%; border:1px solid Black'>Última lectura SICA</td>";
        HTMLTablaInciPulsos += "               <td style='width:15%; border:1px solid Black'>-</td>";
        HTMLTablaInciPulsos += "               <td style='border:1px solid Black'>Titular/Fabricante</td>";
        HTMLTablaInciPulsos += "               <td style='border:1px solid Black'>Uds Error</td>";
        HTMLTablaInciPulsos += "           </tr>";




        HTMLTablaNormal = "<br>" + "<h1>Registradores sin indicencias</h1>";
        HTMLTablaNormal += "       <table width=95% cellspacing=0 cellpadding=5 style='border:1px solid Black'>";
        HTMLTablaNormal += "           <tr bgcolor='gray'>";
        HTMLTablaNormal += "               <td style='border:1px solid Black'>Nº</td>";
        HTMLTablaNormal += "               <td style='border:1px solid Black'>Cod. SICA</td>";
        HTMLTablaNormal += "               <td style='border:1px solid Black'>EM</td>";
        HTMLTablaNormal += "               <td style='border:1px solid Black'>Desc.</td>";
        HTMLTablaNormal += "               <td style='width:15%; border:1px solid Black'>Lectura SICA previa</td>";
        HTMLTablaNormal += "               <td style='width:15%; border:1px solid Black'>última Lectura Manual</td>";
        HTMLTablaNormal += "               <td style='width:15%; border:1px solid Black'>Lectura SICA siguiente</td>";
        HTMLTablaNormal += "               <td style='width:15%; border:1px solid Black'>Última lectura SICA</td>";
        HTMLTablaNormal += "               <td style='width:15%; border:1px solid Black'>Titular/Fabricante</td>";
        HTMLTablaNormal += "           </tr>";

        DataTable motores = new DataTable();
        motores = recupera_motoresDT();
        for (int i = 0; i < motores.Rows.Count - 1; i++)
        {


            stylefilaroja = ""; stylefilaroja1 = ""; stylefilaroja2 = ""; stylefilaroja3 = ""; Error = "";
            incidencia = false; incidenciaanterior = false; incidenciasiguiente = false; incidenciafecha = false; incidenciapulsos = false;
            PVYCR = motores.Rows[i][0].ToString().Trim();
            EM = motores.Rows[i][1].ToString().Trim();

            //fechaUltMantenimiento = motores.Rows[i].Cells[2].Value.ToString().Trim();
            //tipoUltMantenimiento = motores.Rows[i].Cells[3].Value.ToString().Trim();
            //fabricante = motores.Rows[i].Cells[4].Value.ToString().Trim();
            fechaUltMantenimiento = "";
            tipoUltMantenimiento = "";
            fabricante = motores.Rows[i][2].ToString().Trim();
            if (fabricante == "9998") { fabricante = motores.Rows[i][14].ToString().Trim() + " / Sofrel - LS"; }
            else if (fabricante == "9997") { fabricante = motores.Rows[i][14].ToString().Trim() + " / MaserMic"; }
            else
            {
                if (motores.Rows[i][6].ToString().Trim().Contains("LS"))
                { fabricante = motores.Rows[i][14].ToString().Trim() + " / Sofrel - LS"; }
                if (motores.Rows[i][6].ToString().Trim().Contains("CELLBOX"))
                { fabricante = motores.Rows[i][14].ToString().Trim() + " / Sofrel - CellBox"; }
                if (motores.Rows[i][6].ToString().Trim().Contains("MASER"))
                { fabricante = motores.Rows[i][14].ToString().Trim() + " / MaserMic"; }
                if (motores.Rows[i][6].ToString().Trim().Contains("NIVUS"))
                { fabricante = motores.Rows[i][14].ToString().Trim() + " / Caudalimetro"; }
                if (motores.Rows[i][6].ToString().Trim().Contains("CAUD"))
                { fabricante = motores.Rows[i][14].ToString().Trim() + " / Caudalimetro"; }
            }


            if (fabricante.StartsWith("Nivus"))
            {

                DatoPVYCR = UltimoDatoPVYCRAcequia(PVYCR, EM);
                anterior = this.ultimoDatoSICAAcequiasAnterior(PVYCR, EM, DatoPVYCR[0].ToString());
                Siguiente = this.PrimerDatoSICAAcequiasSiguiente(PVYCR, EM, DatoPVYCR[0].ToString());
                UltimoSica = this.UltimoDatoSICAAcequia(PVYCR, EM);
                unidadIng = " m3/s ";
                margenError = 50.0; // No va a ocurrir
            }
            else
            {
                DatoPVYCR = UltimoDatoPVYCRMotores(PVYCR, EM);
                anterior = this.ultimoDatoSICAMotoresAnterior(PVYCR, EM, DatoPVYCR[0].ToString());
                Siguiente = this.PrimerDatoSICAMotoresSiguiente(PVYCR, EM, DatoPVYCR[0].ToString());
                UltimoSica = this.UltimoDatoSICAMotores(PVYCR, EM);
                unidadIng = " m3 ";
                margenError = 1000.0; // el -+10 es porque quieren que exista un margen con el que ocultar incidencias
            }



            try
            {//el -+10 es porque quieren que exista un margen con el que ocultar incidencias.
                System.TimeSpan tm = DateTime.Now.Subtract(DateTime.Parse(DatoPVYCR[0].ToString()));

                if (tm.Days < 60)
                {
                    if (Convert.ToDouble(anterior[1].ToString()) - margenError > Convert.ToDouble(DatoPVYCR[1].ToString()))
                    {
                        incidencia = true;
                        incidenciaanterior = false;
                        incidenciapulsos = true;
                        Error = Convert.ToString(Convert.ToDouble(anterior[1].ToString()) - Convert.ToDouble(DatoPVYCR[1].ToString())) + " Pulsos";
                    }
                }
            }
            catch
            {
            }
            try
            {
                System.TimeSpan tm = DateTime.Now.Subtract(DateTime.Parse(DatoPVYCR[0].ToString()));
                if (tm.Days < 60)
                {
                    if (Convert.ToDouble(Siguiente[1].ToString()) + margenError < Convert.ToDouble(DatoPVYCR[1].ToString()))
                    {
                        incidencia = true;
                        incidenciasiguiente = false;
                        incidenciapulsos = true;
                        Error = Convert.ToString(Convert.ToDouble(DatoPVYCR[1].ToString()) - Convert.ToDouble(Siguiente[1].ToString())) + " Pulsos";
                    }
                }
            }
            catch
            {
            }
            try
            {
                System.TimeSpan tm = DateTime.Now.Subtract(DateTime.Parse(UltimoSica[0].ToString()));
                if (tm.Days > maxDiasSinComunicacion)
                {
                    incidencia = true;
                    incidenciafecha = true;
                    incidenciapulsos = false;
                    Error = tm.Days.ToString() + " Días";
                }


            }
            catch
            {
            }
            try
            {
                if (UltimoSica[0].ToString().Trim().Contains("NO"))
                {
                    incidencia = true;
                    incidenciafecha = true;
                    incidenciapulsos = false;
                    Error = "";
                }
            }
            catch { }

            if (incidencia)
            {
                stylefilaroja = " bgcolor='salmon' ";
            }
            if (incidenciaanterior)
            {
                stylefilaroja1 = " bgcolor='red' ";
            }
            if (incidenciasiguiente)
            {
                stylefilaroja2 = " bgcolor='red' ";
            }
            if (incidenciafecha)
            {
                stylefilaroja3 = " bgcolor='red' ";
            }
            if (incidenciapulsos)
            {
                stylefilaroja = " bgcolor='#F5D0A9' ";
                stylefilaroja1 = " bgcolor='salmon' ";
            }

            if (((incidenciafecha) && !(incidenciapulsos)) && (fabricante.Contains("alimetro") == false))
            {
                SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
                //DB.GetDataDBSICA("UPDATE SICA_Puntos_Control SET enviandodatos = 'SI' where punto_control like '" + PVYCR.Trim() + "%'");
                //DB.GetDataDBSICA("UPDATE SICA_Puntos_Control SET cargandodatos = 'I1' where punto_control like '" + PVYCR.Trim() + "%'");

                equiposConInci++;
                HTMLTablaInci += "           <tr " + stylefilaroja + ">";
                HTMLTablaInci += "               <td style='border:1px solid Black'>" + Convert.ToString(equiposConInci) + "</td>";
                HTMLTablaInci += "               <td style='border:1px solid Black'>" + PVYCR + "</td>";
                HTMLTablaInci += "               <td style='border:1px solid Black'>" + EM + "</td>";
                HTMLTablaInci += "               <td style='border:1px solid Black'>" + ObtenerDescripcionPVYCR(PVYCR) + "</td>";
                HTMLTablaInci += "               <td " + stylefilaroja1 + " style='border:1px solid Black'><center>" + anterior[0].ToString() + "<br>" + anterior[1].ToString() + unidadIng + "</center></td>";
                HTMLTablaInci += "               <td style='border:1px solid Black'><center>" + DatoPVYCR[0].ToString() + "<br>" + DatoPVYCR[1].ToString() + unidadIng + "</center></td>";
                HTMLTablaInci += "               <td " + stylefilaroja2 + " style='border:1px solid Black'><center>" + Siguiente[0].ToString() + "<br>" + Siguiente[1].ToString() + unidadIng + "</center></td>";
                HTMLTablaInci += "               <td " + stylefilaroja3 + " style='border:1px solid Black'><center>" + UltimoSica[0].ToString() + "<br>" + UltimoSica[1].ToString() + unidadIng + "</center></td>";
                //HTMLTablaInci += "               <td style='border:1px solid Black'><center>" + fechaUltMantenimiento + "<br>" + tipoUltMantenimiento + "</td>";
                HTMLTablaInci += "               <td style='border:1px solid Black'><center>" + fabricante + "</center></td>";
                HTMLTablaInci += "               <td " + stylefilaroja3 + " style='border:1px solid Black'><center>" + Error + "</center></td>";
                HTMLTablaInci += "           </tr>";
            }
            else if (((incidenciapulsos) && !(incidenciafecha)) && (fabricante.Contains("alimetro") == false))
            {
                SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
                //DB.GetDataDBSICA("UPDATE SICA_Puntos_Control SET enviandodatos = 'SI' where punto_control like '" + PVYCR.Trim() + "%'");
                //DB.GetDataDBSICA("UPDATE SICA_Puntos_Control SET cargandodatos = 'I2' where punto_control like '" + PVYCR.Trim() + "%'");


                equiposConInci2++;
                HTMLTablaInciPulsos += "           <tr " + stylefilaroja + ">";
                HTMLTablaInciPulsos += "               <td style='border:1px solid Black'>" + Convert.ToString(equiposConInci2) + "</td>";
                HTMLTablaInciPulsos += "               <td style='border:1px solid Black'>" + PVYCR + "</td>";
                HTMLTablaInciPulsos += "               <td style='border:1px solid Black'>" + EM + "</td>";
                HTMLTablaInciPulsos += "               <td style='border:1px solid Black'>" + ObtenerDescripcionPVYCR(PVYCR) + "</td>";
                HTMLTablaInciPulsos += "               <td style='border:1px solid Black'><center>" + anterior[0].ToString() + "<br>" + anterior[1].ToString() + unidadIng + "</center></td>";
                HTMLTablaInciPulsos += "               <td style='border:1px solid Black'><center>" + DatoPVYCR[0].ToString() + "<br>" + DatoPVYCR[1].ToString() + unidadIng + "</center></td>";
                HTMLTablaInciPulsos += "               <td style='border:1px solid Black'><center>" + Siguiente[0].ToString() + "<br>" + Siguiente[1].ToString() + unidadIng + "</center></td>";
                HTMLTablaInciPulsos += "               <td style='border:1px solid Black'><center>" + UltimoSica[0].ToString() + "<br>" + UltimoSica[1].ToString() + unidadIng + "</center></td>";
                //HTMLTablaInciPulsos += "               <td style='border:1px solid Black'><center>" + fechaUltMantenimiento + "<br>" + tipoUltMantenimiento + "</td>";
                HTMLTablaInciPulsos += "               <td style='border:1px solid Black'><center>" + fabricante + "</center></td>";
                HTMLTablaInciPulsos += "               <td " + stylefilaroja1 + " style='border:1px solid Black'><center>" + Error + "</center></td>";
                HTMLTablaInciPulsos += "           </tr>";
            }
            else if ((fabricante.Contains("alimetro") == false))
            {
                SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
                //DB.GetDataDBSICA("UPDATE SICA_Puntos_Control SET enviandodatos = 'SI' where punto_control like '" + PVYCR.Trim() + "%'");
                //DB.GetDataDBSICA("UPDATE SICA_Puntos_Control SET cargandodatos = 'SI' where punto_control like '" + PVYCR.Trim() + "%'");
                equiposSinInci++;
                HTMLTablaNormal += "           <tr " + stylefilaroja + ">";
                HTMLTablaNormal += "               <td style='border:1px solid Black'>" + Convert.ToString(equiposSinInci) + "</td>";
                HTMLTablaNormal += "               <td style='border:1px solid Black'>" + PVYCR + "</td>";
                HTMLTablaNormal += "               <td style='border:1px solid Black'>" + EM + "</td>";
                HTMLTablaNormal += "               <td style='border:1px solid Black'>" + ObtenerDescripcionPVYCR(PVYCR) + "</td>";
                HTMLTablaNormal += "               <td " + stylefilaroja1 + " style='border:1px solid Black'><center>" + anterior[0].ToString() + "<br>" + anterior[1].ToString() + unidadIng + "</center></td>";
                HTMLTablaNormal += "               <td style='border:1px solid Black'><center>" + DatoPVYCR[0].ToString() + "<br>" + DatoPVYCR[1].ToString() + unidadIng + "</center></td>";
                HTMLTablaNormal += "               <td " + stylefilaroja2 + " style='border:1px solid Black'><center>" + Siguiente[0].ToString() + "<br>" + Siguiente[1].ToString() + unidadIng + "</center></td>";
                HTMLTablaNormal += "               <td " + stylefilaroja3 + " style='border:1px solid Black'><center>" + UltimoSica[0].ToString() + "<br>" + UltimoSica[1].ToString() + unidadIng + "</center></td>";
                // HTMLTablaNormal += "               <td style='border:1px solid Black'><center>" + fechaUltMantenimiento + "<br>" + tipoUltMantenimiento + "</td>";
                HTMLTablaNormal += "               <td style='border:1px solid Black'><center>" + fabricante + "</center></td>";
                HTMLTablaNormal += "           </tr>";
            }



        }//cierre for

        HTMLTablaInci += "       </table>";
        HTMLTablaNormal += "       </table>";
        HTMLTablaInciPulsos += "       </table>";


        HTMLBoby += HTMLTablaInciPulsos + HTMLTablaInci + HTMLTablaNormal + "   </center>";
        HTMLBoby += "</body>";
        HTML_Tabla_Informe.Text = HTMLBoby;
        //CuerpoCorreo = HTMLBoby;
        //webBrowser1.DocumentText = HTMLBoby;

    }
    
    
    /// <summary>
    /// Función para sacar de la base de datos
    /// todos los puntos de control y su elemento 
    /// de medida en los cuales guardamos datos de 
    /// telemedida
    /// </summary>
    /// <returns> Data table con la INFO</returns>
    private DataTable recupera_motoresDT()
    {
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        string sql = "";
        sql = @"SELECT distinct [Punto_control],
				 (SELECT TOP(1) [EM]
								FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t2
								Where t1.Punto_control like t2.Punto_control) as [EM],
				 (SELECT TOP(1) [Num_equipo]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t3
								Where t1.Punto_control like t3.Punto_control) as [Num_equipo],
				 (SELECT TOP(1) [Num_modem]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t4
								Where t1.Punto_control like t4.Punto_control) as [Num_modem],
				 (SELECT TOP(1) [caracteristica1]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t5
								Where t1.Punto_control like t5.Punto_control) as [caracteristica1],
				 (SELECT TOP(1) [caracteristica2]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t6
								Where t1.Punto_control like t6.Punto_control) as [caracteristica2],
				 (SELECT TOP(1) [caracteristica3]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t7
								Where t1.Punto_control like t7.Punto_control) as [caracteristica3],
				 (SELECT TOP(1) [caracteristica4]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t8
								Where t1.Punto_control like t8.Punto_control) as [caracteristica4],
				 (SELECT TOP(1) [CargandoDatos]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t9
								Where t1.Punto_control like t9.Punto_control) as [cargandodatos],
				 (SELECT TOP(1) [EnviandoDatos]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t10
								Where t1.Punto_control like t10.Punto_control) as [Enviandodatos],
				 (SELECT TOP(1) [FechaUltimaCarga]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t11
								Where t1.Punto_control like t11.Punto_control) as [FechaUltimaCarga],
				 (SELECT TOP(1) [FechaPrimeraCarga]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t12
								Where t1.Punto_control like t12.Punto_control) as [FechaPrimeraCarga],
				 (SELECT TOP(1) [InstalacionPrevista]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t13
								Where t1.Punto_control like t13.Punto_control)  as [InstalacionPrevista],
				 (SELECT TOP(1) [Instalacion]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t14
								Where t1.Punto_control like t14.Punto_control)  as [Instalacion],
				 (SELECT TOP(1) [Alegacion]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t15
								Where t1.Punto_control like t15.Punto_control)  as [Alegacion],
				 (SELECT TOP(1) [Acuse]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t16
								Where t1.Punto_control like t16.Punto_control) as [Acuse],
				 (SELECT TOP(1) [Tecnico]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t17
								Where t1.Punto_control like t17.Punto_control) as [Tecnico],
				 (SELECT TOP(1) [UTMx]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t18
								Where t1.Punto_control like t18.Punto_control) as [UTMX],
				 (SELECT TOP(1) [UTMy]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t19
								Where t1.Punto_control like t19.Punto_control) as [UTMY],
				 (SELECT TOP(1) [dtl]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t20
								Where t1.Punto_control like t20.Punto_control) as [dtl],
				 (SELECT TOP(1) [conex]
				 				FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t21
								Where t1.Punto_control like t21.Punto_control) as [conex]
      
                FROM [DBSICA].[dbo].[VW_SICA_Puntos_de_control] t1 where  cargandodatos <> 'NO' AND EM like 'v%'";

        return DB.GetDataDBSICA(sql);
    }

        /// <summary>
        /// Función para sacar de la base de datos
        /// todos los puntos de control y su elemento 
        /// de medida en los cuales guardamos datos de 
        /// telemedida
        /// </summary>
        /// <returns></returns>
    private DataTable recupera_acequias()
    {
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        return DB.GetDataSIGVECTOR("SELECT DISTINCT CodigoPVYCR, idElementoMedida FROM dbo.PVYCR_DatosAcequias WHERE (Cod_Fuente_Dato LIKE '13')");
    }
    /// <summary>
    /// Funcion para obtener de la base de datos de Telemedida
    /// la denominacion que le damos a un codigo PVYCR.
    /// </summary>
    /// <param name="PVYCR">Código PVYCR a buscar</param>
    /// <returns>Devuelve un String con la danominación del punto</returns>
    private string ObtenerDescripcionPVYCR(string PVYCR)
    {
        string denominacion = "";
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        DataSet ds = new DataSet();
        string sql = "SELECT Caracteristica1 FROM SICA_Puntos_control WHERE (punto_control LIKE '" + PVYCR + "')";
        ds= DB.GetDataDBSICA_DS(sql);
        //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionSICA, sql));
        if (ds.Tables[0].DefaultView.Count > 0)
        {
            denominacion = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        }
        else
        {
            denominacion = "NO disponible";
        }
        return denominacion;
    }
    /// <summary>
    /// Funcion para obtener de la base de datos la última lectura
    /// del PVYCR con un código y un elemento de medida concreto.
    /// </summary>
    /// <param name="PVYCR">Código PVYCR a buscar</param>
    /// <param name="EM">Elemento de medida a buscar</param>
    /// <returns>Devuelve un Array de String con la fecha/hora y con el valor</returns>
    private string[] UltimoDatoPVYCRAcequia(string PVYCR, string EM)
    {
        string[] datos = new string[2];
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        DataSet ds = new DataSet();
        string sql = "SELECT TOP 1 Fecha_Medida, Caudal_M3S FROM dbo.PVYCR_DatosAcequias WHERE (Cod_Fuente_Dato LIKE '05') AND  (CodigoPVYCR LIKE '" + PVYCR + "') AND (idElementoMedida LIKE '" + EM + "') ORDER BY Fecha_Medida DESC";
        ds = DB.GetDataSIGVECTOR_DS(sql);
        //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionCHS, sql));
        if (ds.Tables[0].DefaultView.Count > 0)
        {
            datos[0] = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            datos[1] = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        }
        else
        {
            datos[0] = "NO";
            datos[1] = "Disponible";
        }
        return datos;
    }
    /// <summary>
    /// Funcion para obtener de la base de datos la última lectura
    /// del SICA con un código y un elemento de medida concreto.
    /// </summary>
    /// <param name="PVYCR">Código PVYCR a buscar</param>
    /// <param name="EM">Elemento de medida a buscar</param>
    /// <returns>Devuelve un Array de String con la fecha/hora y con el valor</returns>
    private string[] UltimoDatoSICAAcequia(string PVYCR, string EM)
    {
        string[] datos = new string[2];
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        DataSet ds = new DataSet();
        string sql = "SELECT TOP 1 Fecha_Medida, Caudal_M3S FROM dbo.PVYCR_DatosAcequias WHERE ((Cod_Fuente_Dato LIKE '13')OR((Cod_Fuente_Dato LIKE '10'))) AND  (CodigoPVYCR LIKE '" + PVYCR + "') AND (idElementoMedida LIKE '" + EM + "') ORDER BY Fecha_Medida DESC";
        ds = DB.GetDataSIGVECTOR_DS(sql);
        //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionCHS, sql));
        if (ds.Tables[0].DefaultView.Count > 0)
        {
            datos[0] = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            datos[1] = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        }
        else
        {
            datos[0] = "NO";
            datos[1] = "Disponible";
        }
        return datos;
    }
    /// <summary>
    /// Funcion para obtener de la base de datos la última lectura
    /// del PVYCR con un código y un elemento de medida concreto.
    /// </summary>
    /// <param name="PVYCR">Código PVYCR a buscar</param>
    /// <param name="EM">Elemento de medida a buscar</param>
    /// <returns>Devuelve un Array de String con la fecha/hora y con el valor</returns>
    private string[] UltimoDatoPVYCRMotores(string PVYCR, string EM)
    {
        string[] datos = new string[2];
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        DataTable dt = new DataTable();
        string sql = "SELECT TOP 1 Fecha_Medida, LecturaContador_m3 FROM dbo.PVYCR_DatosMotores WHERE (Cod_Fuente_Dato LIKE '05') AND  (CodigoPVYCR LIKE '" + PVYCR + "') AND (idElementoMedida LIKE '" + EM + "') ORDER BY Fecha_Medida DESC";
        dt = DB.GetDataSIGVECTOR(sql);
        //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionCHS, sql));
        if (dt.Rows.Count>0)
        {
            datos[0] = dt.Rows[0][0].ToString();
            datos[1] = dt.Rows[0][1].ToString();
        }
        else
        {
            datos[0] = "NO";
            datos[1] = "Disponible";
        }
        return datos;
    }
    /// <summary>
    /// Funcion para obtener de la base de datos la última lectura
    /// del SICA con un código y un elemento de medida concreto.
    /// </summary>
    /// <param name="PVYCR">Código PVYCR a buscar</param>
    /// <param name="EM">Elemento de medida a buscar</param>
    /// <returns>Devuelve un Array de String con la fecha/hora y con el valor</returns>
    private string[] UltimoDatoSICAMotores(string PVYCR, string EM)
    {
        string[] datos = new string[2];
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        DataTable dt = new DataTable();
        string sql = "SELECT TOP 1 Fecha_Medida, LecturaContador_m3 FROM dbo.PVYCR_DatosMotores WHERE ((Cod_Fuente_Dato LIKE '13')OR((Cod_Fuente_Dato LIKE '10'))) AND  (CodigoPVYCR LIKE '" + PVYCR + "') AND (idElementoMedida LIKE '" + EM + "') ORDER BY Fecha_Medida DESC";
        dt = DB.GetDataSIGVECTOR(sql);
        //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionCHS, sql));
        if (dt.Rows.Count > 0)
        {
            datos[0] = dt.Rows[0][0].ToString();
            datos[1] = dt.Rows[0][1].ToString();
        }
        else
        {
            datos[0] = "NO";
            datos[1] = "Disponible";
        }
        return datos;
    }
    /// <summary>
    /// Funcion para obtener de la base de datos la última lectura
    /// del SICA con un código y un elemento de medida concreto 
    /// y que no supere una fecha concreta.
    /// </summary>
    /// <param name="PVYCR">Código PVYCR a buscar</param>
    /// <param name="EM">Elemento de medida a buscar</param>
    /// <param name="Fecha">Fecha límite por encima</param>
    /// <returns>Devuelve un Array de String con la fecha/hora y con el valor</returns>
    private string[] ultimoDatoSICAMotoresAnterior(string PVYCR, string EM, string Fecha)
    {
        string[] datos = new string[2];
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        if (Fecha.Equals("NO"))
        {
            datos[0] = "NO";
            datos[1] = "Disponible";
        }
        else
        {
            DataTable dt = new DataTable();
            string sql = "SELECT TOP 1 Fecha_Medida, LecturaContador_m3 FROM dbo.PVYCR_DatosMotores WHERE (CodigoPVYCR LIKE '" + PVYCR + "') AND (idElementoMedida LIKE '" + EM + "') AND ((Cod_Fuente_Dato LIKE '13')OR((Cod_Fuente_Dato LIKE '10'))) AND (Fecha_Medida < '" + Fecha + "')  ORDER BY Fecha_Medida DESC";
            dt = DB.GetDataSIGVECTOR(sql);
            //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionCHS, sql));
            if (dt.Rows.Count > 0)
            {
                datos[0] = dt.Rows[0][0].ToString();
                datos[1] = dt.Rows[0][1].ToString();
            }
            else
            {
                datos[0] = "NO";
                datos[1] = "Disponible";
            }

        }
        return datos;
    }
        /// <summary>
        /// Funcion para obtener de la base de datos la última lectura
        /// del SICA con un código y un elemento de medida concreto 
        /// y que no supere una fecha concreta.
        /// </summary>
        /// <param name="PVYCR">Código PVYCR a buscar</param>
        /// <param name="EM">Elemento de medida a buscar</param>
        /// <param name="Fecha">Fecha límite por encima</param>
        /// <returns>Devuelve un Array de String con la fecha/hora y con el valor</returns>
        private string[] ultimoDatoSICAAcequiasAnterior(string PVYCR, string EM, string Fecha)
    {
        string[] datos = new string[2];
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        if (Fecha.Equals("NO"))
        {
            datos[0] = "NO";
            datos[1] = "Disponible";
        }
        else
        {
            DataSet ds = new DataSet();
            string sql = "SELECT TOP 1 Fecha_Medida, Caudal_M3S FROM dbo.PVYCR_DatosAcequias WHERE (CodigoPVYCR LIKE '" + PVYCR + "') AND (idElementoMedida LIKE '" + EM + "') AND ((Cod_Fuente_Dato LIKE '13')OR((Cod_Fuente_Dato LIKE '10'))) AND (Fecha_Medida < '" + Fecha + "') ORDER BY Fecha_Medida DESC";
            ds = DB.GetDataSIGVECTOR_DS(sql);
            //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionCHS, sql));
            if (ds.Tables[0]!=null)
            {
                datos[0] = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                datos[1] = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }
            else
            {
                datos[0] = "NO";
                datos[1] = "Disponible";
            }
        }
        return datos;
    }
    /// <summary>
    /// Funcion para obtener de la base de datos la última lectura
    /// del SICA con un código y un elemento de medida concreto 
    /// y que sea mayor que una fecha concreta.
    /// </summary>
    /// <param name="PVYCR">Código PVYCR a buscar</param>
    /// <param name="EM">Elemento de medida a buscar</param>
    /// <param name="Fecha">Fecha límite por encima</param>
    /// <returns>Devuelve un Array de String con la fecha/hora y con el valor</returns>
    private string[] PrimerDatoSICAMotoresSiguiente(string PVYCR, string EM, string Fecha)
    {
        string[] datos = new string[2];
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        if (Fecha.Equals("NO"))
        {
            datos[0] = "NO";
            datos[1] = "Disponible";
        }
        else
        {

            DataSet ds = new DataSet();
            string sql = "SELECT TOP 1 Fecha_Medida, LecturaContador_M3 FROM dbo.PVYCR_DatosMotores WHERE (CodigoPVYCR LIKE '" + PVYCR + "') AND (idElementoMedida LIKE '" + EM + "') AND ((Cod_Fuente_Dato LIKE '13')OR((Cod_Fuente_Dato LIKE '10'))) AND (Fecha_Medida > '" + Fecha + "')  ORDER BY Fecha_Medida ASC";
            ds = DB.GetDataSIGVECTOR_DS(sql);
            //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionCHS, sql));
            if (ds.Tables[0].Rows.Count>0)
            {
                datos[0] = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                datos[1] = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }
            else
            {
                datos[0] = "NO";
                datos[1] = "Disponible";
            }
        }
        return datos;
    }
    /// <summary>
    /// Funcion para obtener de la base de datos la última lectura
    /// del SICA con un código y un elemento de medida concreto 
    /// y que sea mayor una fecha concreta.
    /// </summary>
    /// <param name="PVYCR">Código PVYCR a buscar</param>
    /// <param name="EM">Elemento de medida a buscar</param>
    /// <param name="Fecha">Fecha límite por encima</param>
    /// <returns>Devuelve un Array de String con la fecha/hora y con el valor</returns>
    private string[] PrimerDatoSICAAcequiasSiguiente(string PVYCR, string EM, string Fecha)
    {

        string[] datos = new string[2];
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        if (Fecha.Equals("NO"))
        {
            datos[0] = "NO";
            datos[1] = "Disponible";
        }
        else
        {

            DataSet ds = new DataSet();
            string sql = "SELECT TOP 1 Fecha_Medida, Caudal_M3S FROM dbo.PVYCR_DatosAcequias WHERE (CodigoPVYCR LIKE '" + PVYCR + "') AND (idElementoMedida LIKE '" + EM + "') AND ((Cod_Fuente_Dato LIKE '13')OR((Cod_Fuente_Dato LIKE '10'))) AND (Fecha_Medida > '" + Fecha + "') ORDER BY Fecha_Medida ASC";
            ds = DB.GetDataSIGVECTOR_DS(sql);
            //ds = (DataSet)conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionCHS, sql));
            if (ds.Tables[0]!=null)
            {
                datos[0] = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                datos[1] = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }
            else
            {
                datos[0] = "NO";
                datos[1] = "Disponible";
            }
        }
        return datos;
    }

    #endregion
}
