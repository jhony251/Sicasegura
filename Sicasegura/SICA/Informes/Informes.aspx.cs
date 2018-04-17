using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

public partial class SICAH_Telemedidalistado_Default : System.Web.UI.Page
{
    string fi = "";
    string ff = "";

    DataTable dt2 = new DataTable();
    bool xcell = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        try { fi = Request.QueryString["FI"].ToString(); } catch { fi = "01/11/2017"; }
        try { ff = Request.QueryString["FF"].ToString(); } catch { ff = "28/02/2018"; }
        try { xcell = Boolean.Parse(Request.QueryString["XLS"].ToString()); } catch { }
        ///HAY QUE CONTROLAR EL ACCESO DE LOS USUARIOS QUE NO SON SICA
        SicaSegura.SICA_Interfaz.SICA_Sica InterfazSica = new SicaSegura.SICA_Interfaz.SICA_Sica();

        String menu = "-#-";
        HTML_Links_Sup_Izq.Text = InterfazSica.Get_Enlaces_Sup_Izq(menu.Split('#'));
        HTML_Subcabecera_Logos.Text = InterfazSica.Get_Cabecera();
        //HTML_Menu_Navegacion.Text = InterfazSica.Get_Menu_Navegacion_Completo("cuatro");
        //Cargalistado_ISM();
        HTML_Pie_Logo_Corporativo.Text = InterfazSica.Get_pie_logo_corporativo();
        HTML_Pie_pagina.Text = InterfazSica.Get_pie_pagina();
        NineRays.Reporting.Register.SetLicenseKey("4761628416024064132096");
        LIT_LINK_DOWNLOAD_EXCELL.Text = " <a href=\"Informes.aspx?XLS=true&FI=" + fi + "&FF=" + ff + "\">EXPORTAR A EXCEL</a>";
        this.Cargalistado_Informe();


    }

    private DataTable ObtenerTablaInforme()
    {
        String SQL = @"
		select 
                SUBSTRING(tbl.ISM,1,12) ISM ,  
                tbl.Codigo_SICA, 
                '' Titular, 
                
                tbl.[Fecha Primera Lectura], 
                tbl.[Fecha Ultima Lectura], 
                
                (Convert(float,tbl.[Ultima Lectura])  - CONVERT(float,tbl.[Primera Lectura])) as Total_M3 
        from
                (SELECT  
			        Distinct DM.CodigoPVYCR as Codigo_SICA, 

			        SUBSTRING(DM.CodigoPVYCR , 1,2) AS Zona,
	
			        (select CAU.OtrosExpedientes from SIGVECTOR.dbo.PVYCR_Cauces CAU  where CAU.CodigoCauce like SUBSTRING(DM.CodigoPVYCR , 1,charindex('P',DM.CodigoPVYCR)-1)) as ISM,
	
			        SUBSTRING(DM.CodigoPVYCR , 1,charindex('P',DM.CodigoPVYCR)-1) AS Cauce,		
	
			        (select count(*) 
			        from SIGVECTOR.dbo.PVYCR_DatosMotores 
			        where (fecha_medida between '{0}' AND '{1}') 
			        AND  (codigopvycr like DM.CodigoPVYCR) 
                    AND  (cod_fuente_dato like '05') 
                    AND  (lecturacontador_m3 is not null))as [Lecturas Tomadas],

			        (select TOP(1) Lecturacontador_m3 
			        from SIGVECTOR.dbo.PVYCR_DatosMotores 
			        where (codigopvycr like DM.CodigoPVYCR) 
                    AND  (cod_fuente_dato like '05') 
                    AND  (fecha_medida>'{0}')  
                    order by Fecha_Medida asc) as [Primera Lectura],


			        (select TOP(1) Fecha_Medida 
			        from SIGVECTOR.dbo.PVYCR_DatosMotores
			        where (codigopvycr like DM.CodigoPVYCR) 
                    AND  (cod_fuente_dato like '05') 
                    AND  (fecha_medida>'{0}') 
                    order by Fecha_Medida asc) as [Fecha Primera Lectura], 

			        (select TOP(1) Lecturacontador_m3 
			        from SIGVECTOR.dbo.PVYCR_DatosMotores 
			        where codigopvycr like DM.CodigoPVYCR 
                    AND (cod_fuente_dato like '05') 
                    AND (fecha_medida<'{1}')
			        AND (fecha_medida>'{0}')
                    order by Fecha_Medida desc) as [Ultima Lectura], 


			        (select TOP(1) Fecha_Medida 
			        from SIGVECTOR.dbo.PVYCR_DatosMotores 
			        where (codigopvycr like DM.CodigoPVYCR) 
                    and  (cod_fuente_dato like '05') 
                    AND  (fecha_medida<'{1}')
			        and  (fecha_medida>'{0}')
                    order by Fecha_Medida desc) as [Fecha Ultima Lectura]

		        from SIGVECTOR.dbo.PVYCR_DatosMotores DM

		        where 
                    ((DM.CodigoPVYCR not like 'va062P01'))
                    AND ((DM.CodigoPVYCR not like 'vb105p01')) 
                    AND ((DM.CodigoPVYCR like 'cm%') 
                            OR (DM.CodigoPVYCR like 'va%') 
                            OR (DM.CodigoPVYCR like 'VM%') 
                            OR (DM.CodigoPVYCR like 'VB%') 
                            OR (DM.CodigoPVYCR like 'ET047%') 
                            OR (DM.CodigoPVYCR like 'ET006%') 
                            OR (DM.CodigoPVYCR like 'S000C000701%') 
                            OR (DM.CodigoPVYCR like 'S084A142401%') 
                        )
                ) as tbl
        where tbl.[Lecturas Tomadas]>0 
        order by ISM, tbl.Codigo_SICA asc";
        SQL = string.Format(SQL, fi, ff);
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        DataTable dt = new DataTable();
        dt = DB.GetDataSIGVECTOR(SQL);
        return dt;
    }


    private void Cargalistado_Informe()
    {
        


        int i = 0;
        int u = 0;
        DataTable listaISM = ObtenerTablaInforme();
        HTML_Tabla_Informe.Text += "<table id=\"sequia\" runat=\"server\">";
        HTML_Tabla_Informe.Text += "<thead>";
        dt2.Columns.Add("ID");

        HTML_Tabla_Informe.Text += "<tr><th>ID</th>";
        for (u = 0; u < listaISM.Columns.Count; u++)
        {
            HTML_Tabla_Informe.Text += "<th>";
            HTML_Tabla_Informe.Text += listaISM.Columns[u].ColumnName.ToString();
            dt2.Columns.Add(listaISM.Columns[u].ColumnName.ToString());
            HTML_Tabla_Informe.Text += "</th>";
        }
        HTML_Tabla_Informe.Text += "<th>";
        HTML_Tabla_Informe.Text += "Ref. ASV";
        dt2.Columns.Add("ASV");
        HTML_Tabla_Informe.Text += "</th>";
        HTML_Tabla_Informe.Text += "</tr>";
        HTML_Tabla_Informe.Text += "</thead>";
        HTML_Tabla_Informe.Text += "<tbody>";
        for (i = 0; i < listaISM.Rows.Count; i++)
        {
            if (listaISM.Rows[i][1].ToString().Trim() == "VA062P01") { i++; }
            dt2.Rows.Add();
            //|| VB001-004P01
            if ((listaISM.Rows[i][1].ToString().Trim() == "VA011-012P01") 
                || (listaISM.Rows[i][1].ToString().Trim() == "VA097P01")
                || (listaISM.Rows[i][1].ToString().Trim() == "VM020P01")
                || (listaISM.Rows[i][1].ToString().Trim() == "VM041P01")
                || (listaISM.Rows[i][1].ToString().Trim() == "VB080-011P01")
                || (listaISM.Rows[i][1].ToString().Trim() == "VB001-004P01")
                || (listaISM.Rows[i][1].ToString().Trim() == "ET006-001P04"))
            {
                //inicial 3 final 6
                SicaSegura.SICA_DB SICADB = new SicaSegura.SICA_DB();
                //string SQLINICIAL, SQLFINAL = "";
                //SQLFINAL = @"SELECT SUM(M3) AS Expr1
                //             FROM  
                //                (SELECT TOP (2) LecturaContador_M3 AS M3
                //                 FROM   dbo.PVYCR_DatosMotores
                //                 WHERE  (CodigoPVYCR LIKE '" + listaISM.Rows[i][1].ToString().Trim();
                //SQLFINAL+= @"') AND (Fecha_Medida BETWEEN '{0}' AND '{1}') AND (Cod_Fuente_Dato LIKE '05')
                //                 ORDER BY Fecha_Medida DESC) AS t";
                //SQLINICIAL = @"SELECT SUM(M3) AS Expr1
                //            FROM            
                //                (SELECT TOP (2) LecturaContador_M3 AS M3
                //                 FROM dbo.PVYCR_DatosMotores
                //                 WHERE (CodigoPVYCR LIKE '" + listaISM.Rows[i][1].ToString().Trim();
                //SQLINICIAL+= @"') AND (Fecha_Medida BETWEEN '{0}' AND '{1}') AND (Cod_Fuente_Dato LIKE '05')
                //                 ORDER BY Fecha_Medida asc) AS t";
                //SQLFINAL = string.Format(SQLFINAL, fi, ff);
                //SQLINICIAL = string.Format(SQLINICIAL, fi, ff);
                ////listaISM.Rows[i][3] = SICADB.GetDataSIGVECTOR(SQLINICIAL).Rows[0][0].ToString();
                ////listaISM.Rows[i][6] = SICADB.GetDataSIGVECTOR(SQLFINAL).Rows[0][0].ToString();

                string sql = @"
                                SELECT 
                                    (Convert(float,tbl.[Ultima Lectura])  - CONVERT(float,tbl.[Primera Lectura])) as Total_M3
                                FROM
                                    (SELECT

                                        (select TOP(1) Lecturacontador_m3
                                        from SIGVECTOR.dbo.PVYCR_DatosMotores
                                        where codigopvycr like '" + listaISM.Rows[i][1].ToString().Trim();
                              sql += @"' AND idelementomedida like 'V01' 
                                        AND(cod_fuente_dato like '05')
                                        AND(fecha_medida > '{0}')
                                        order by Fecha_Medida asc) as [Primera Lectura],


                            			(select TOP(1) Lecturacontador_m3
                                        from SIGVECTOR.dbo.PVYCR_DatosMotores
                                        where codigopvycr like '" + listaISM.Rows[i][1].ToString().Trim();
                              sql += @"' AND idelementomedida like 'V01' 
                                        AND(cod_fuente_dato like '05')
                                        AND fecha_medida<'{1}'
                                        AND fecha_medida> '{0}' order by Fecha_Medida desc) as [Ultima Lectura]) as tbl";
                sql = string.Format(sql, fi, ff);
                float f1 = 0;
                try { f1 = float.Parse(SICADB.GetDataSIGVECTOR(sql).Rows[0][0].ToString()); } catch {  f1 = 0; }
                sql = @"
                                SELECT 
                                    (Convert(float,tbl.[Ultima Lectura])  - CONVERT(float,tbl.[Primera Lectura])) as Total_M3
                                FROM
                                    (SELECT

                                        (select TOP(1) Lecturacontador_m3
                                        from SIGVECTOR.dbo.PVYCR_DatosMotores
                                        where codigopvycr like '" + listaISM.Rows[i][1].ToString().Trim();
                              sql += @"' AND idelementomedida like 'V02' 
                                        AND(cod_fuente_dato like '05')
                                        AND(fecha_medida > '{0}')
                                        order by Fecha_Medida asc) as [Primera Lectura],


                            			(select TOP(1) Lecturacontador_m3
                                        from SIGVECTOR.dbo.PVYCR_DatosMotores
                                        where codigopvycr like '" + listaISM.Rows[i][1].ToString().Trim();
                              sql += @"' AND idelementomedida like 'V02' 
                                        AND(cod_fuente_dato like '05')
                                        AND fecha_medida<'{1}'
                                        AND fecha_medida> '{0}' order by Fecha_Medida desc) as [Ultima Lectura]) as tbl";
                sql = string.Format(sql, fi, ff);
                float f2 = 0;
                try { f2 = float.Parse(SICADB.GetDataSIGVECTOR(sql).Rows[0][0].ToString()); } catch { f2 = 0; }
                //listaISM.Rows[i][7] = (f1+f2).ToString().Trim();
                listaISM.Rows[i][5] = (f1 + f2).ToString().Trim();

            }

            if ((listaISM.Rows[i][1].ToString().Trim() == "VA011-011P01"))
            {
                //inicial 3 final 6
                SicaSegura.SICA_DB SICADB = new SicaSegura.SICA_DB();
                //string SQLINICIAL, SQLFINAL = "";
                //SQLFINAL = @"SELECT SUM(M3) AS Expr1
                //             FROM  
                //                (SELECT TOP (3) LecturaContador_M3 AS M3
                //                 FROM   dbo.PVYCR_DatosMotores
                //                 WHERE  (CodigoPVYCR LIKE '" + listaISM.Rows[i][1].ToString().Trim();
                //SQLFINAL += @"') AND (Fecha_Medida BETWEEN '{0}' AND '{1}') AND (Cod_Fuente_Dato LIKE '05')
                //                 ORDER BY Fecha_Medida DESC) AS t";
                //SQLINICIAL = @"SELECT SUM(M3) AS Expr1
                //            FROM            
                //                (SELECT TOP (3) LecturaContador_M3 AS M3
                //                 FROM dbo.PVYCR_DatosMotores
                //                 WHERE (CodigoPVYCR LIKE '" + listaISM.Rows[i][1].ToString().Trim();
                //SQLINICIAL += @"') AND (Fecha_Medida BETWEEN '{0}' AND '{1}') AND (Cod_Fuente_Dato LIKE '05')
                //                 ORDER BY Fecha_Medida asc) AS t";
                //SQLFINAL = string.Format(SQLFINAL, fi, ff);
                //SQLINICIAL = string.Format(SQLINICIAL, fi, ff);
                //listaISM.Rows[i][3] = SICADB.GetDataSIGVECTOR(SQLINICIAL).Rows[0][0].ToString();
                //listaISM.Rows[i][6] = SICADB.GetDataSIGVECTOR(SQLFINAL).Rows[0][0].ToString();
                string sql = @"
                                SELECT 
                                    (Convert(float,tbl.[Ultima Lectura])  - CONVERT(float,tbl.[Primera Lectura])) as Total_M3
                                FROM
                                    (SELECT

                                        (select TOP(1) Lecturacontador_m3
                                        from SIGVECTOR.dbo.PVYCR_DatosMotores
                                        where codigopvycr like '" + listaISM.Rows[i][1].ToString().Trim();
                              sql += @"' AND idelementomedida like 'V01' 
                                        AND(cod_fuente_dato like '05')
                                        AND(fecha_medida > '{0}')
                                        order by Fecha_Medida asc) as [Primera Lectura],


                            			(select TOP(1) Lecturacontador_m3
                                        from SIGVECTOR.dbo.PVYCR_DatosMotores
                                        where codigopvycr like '" + listaISM.Rows[i][1].ToString().Trim();
                              sql += @"' AND idelementomedida like 'V01' 
                                        AND(cod_fuente_dato like '05')
                                        AND fecha_medida<'{1}'
                                        AND fecha_medida> '{0}' order by Fecha_Medida desc) as [Ultima Lectura])as tbl";
                sql = string.Format(sql, fi, ff);
                float f1 = 0;
                try { f1 = float.Parse(SICADB.GetDataSIGVECTOR(sql).Rows[0][0].ToString()); } catch { }
                sql = @"
                                SELECT 
                                    (Convert(float,tbl.[Ultima Lectura])  - CONVERT(float,tbl.[Primera Lectura])) as Total_M3
                                FROM
                                    (SELECT

                                        (select TOP(1) Lecturacontador_m3
                                        from SIGVECTOR.dbo.PVYCR_DatosMotores
                                        where codigopvycr like '" + listaISM.Rows[i][1].ToString().Trim();
                              sql += @"' AND idelementomedida like 'V02' 
                                        AND(cod_fuente_dato like '05')
                                        AND(fecha_medida > '{0}')
                                        order by Fecha_Medida asc) as [Primera Lectura],


                            			(select TOP(1) Lecturacontador_m3
                                        from SIGVECTOR.dbo.PVYCR_DatosMotores
                                        where codigopvycr like '" + listaISM.Rows[i][1].ToString().Trim();
                              sql += @"' AND idelementomedida like 'V02' 
                                        AND(cod_fuente_dato like '05')
                                        AND fecha_medida<'{1}'
                                        AND fecha_medida> '{0}' order by Fecha_Medida desc) as [Ultima Lectura]) as tbl";
                sql = string.Format(sql, fi, ff);
                float f2 = 0;
                try { f2 = float.Parse(SICADB.GetDataSIGVECTOR(sql).Rows[0][0].ToString()); } catch { }
                sql = @"
                                SELECT 
                                    (Convert(float,tbl.[Ultima Lectura])  - CONVERT(float,tbl.[Primera Lectura])) as Total_M3
                                FROM
                                    (SELECT

                                        (select TOP(1) Lecturacontador_m3
                                        from SIGVECTOR.dbo.PVYCR_DatosMotores
                                        where codigopvycr like '" + listaISM.Rows[i][1].ToString().Trim();
                              sql += @"' AND idelementomedida like 'V02' 
                                        AND(cod_fuente_dato like '05')
                                        AND(fecha_medida > '{0}')
                                        order by Fecha_Medida asc) as [Primera Lectura],


                            			(select TOP(1) Lecturacontador_m3
                                        from SIGVECTOR.dbo.PVYCR_DatosMotores
                                        where codigopvycr like '" + listaISM.Rows[i][1].ToString().Trim();
                              sql += @"' AND idelementomedida like 'V02' 
                                        AND(cod_fuente_dato like '05')
                                        AND fecha_medida<'{1}'
                                        AND fecha_medida> '{0}' order by Fecha_Medida desc) as [Ultima Lectura]) as tbl";
                sql = string.Format(sql, fi, ff);
                float f3 = 0;
                try { f3 = float.Parse(SICADB.GetDataSIGVECTOR(sql).Rows[0][0].ToString()); } catch { }
                //listaISM.Rows[i][7] = (f1 + f2 + f3).ToString().Trim();
                listaISM.Rows[i][5] = (f1 + f2 + f3).ToString().Trim();


                //listaISM.Rows[i][7] = (Int64.Parse(listaISM.Rows[i][6].ToString()) - Int64.Parse(listaISM.Rows[i][3].ToString())).ToString().Trim();

            }


            //if (listaISM.Rows[i][1].ToString().Trim() == "VA097P01")
            //{
            //    //inicial 3 final 6
            //    SicaSegura.SICA_DB SICADB = new SicaSegura.SICA_DB();
            //    string SQLINICIAL, SQLFINAL = "";
            //    SQLFINAL = @"SELECT SUM(M3) AS Expr1
            //                 FROM  
            //                    (SELECT TOP (3) LecturaContador_M3 AS M3
            //                     FROM   dbo.PVYCR_DatosMotores
            //                     WHERE  (CodigoPVYCR LIKE 'VA097P01') AND (Fecha_Medida BETWEEN '01/11/2017' AND '15/12/2017') AND (Cod_Fuente_Dato LIKE '05')
            //                     ORDER BY Fecha_Medida DESC) AS t";
            //    SQLINICIAL = @"SELECT SUM(M3) AS Expr1
            //                FROM            
            //                    (SELECT TOP (3) LecturaContador_M3 AS M3
            //                     FROM dbo.PVYCR_DatosMotores
            //                     WHERE (CodigoPVYCR LIKE 'VA097P01') AND (Fecha_Medida BETWEEN '01/11/2017' AND '15/12/2017') AND (Cod_Fuente_Dato LIKE '05')
            //                     ORDER BY Fecha_Medida asc) AS t";
            //    listaISM.Rows[i][3] = SICADB.GetDataSIGVECTOR(SQLINICIAL).Rows[0][0].ToString();
            //    listaISM.Rows[i][6] = SICADB.GetDataSIGVECTOR(SQLFINAL).Rows[0][0].ToString();

            //}
            string style = "Black";
            try
            {
                //if (Int32.Parse(listaISM.Rows[i][6].ToString()) - Int32.Parse(listaISM.Rows[i][3].ToString()) < 0)
                //{ style = "RED"; }
                //else
                //{ }
                if (Int32.Parse(listaISM.Rows[i][5].ToString()) < 0)
                { style = "RED"; }
                else
                { }
            }
            catch { }

            HTML_Tabla_Informe.Text += "<tr style=\"color:" + style + ";\">";
            HTML_Tabla_Informe.Text += "<td>" + i.ToString() + "</td>";
            dt2.Rows[i][0] = i.ToString();
            for (u = 0; u < listaISM.Rows[i].Table.Columns.Count; u++)
            {
                if (u == 2)
                {
                    try
                    {
                        //SicaSegura.SICA_DB SICADB = new SicaSegura.SICA_DB();
                        //string ISM = listaISM.Rows[i][0].ToString().Split('-')[1].Split('/')[0].Trim();
                        //string ISManyo = listaISM.Rows[i][0].ToString().Split('-')[1].Split('/')[1].Trim();
                        //if (ISM.Length == 1) { ISM = "00" + ISM; } else if (ISM.Length == 2) { ISM = "0" + ISM; }
                        //string SQL = @"
                        //        SELECT  [TITULAR]
                        //          FROM  [DBSICA].[dbo].[SICA_Seguimiento_Expedientes_ISM]
                        //           where EXP_ISM like 'ISM-" + ISM + "/" + ISManyo + "'";
                        //string titular = SICADB.GetDataDBSICA(SQL).Rows[0][0].ToString();
                        //HTML_Tabla_Informe.Text += "<td>";
                        //HTML_Tabla_Informe.Text += titular;
                        //HTML_Tabla_Informe.Text += "</td>";
                        SicaSegura.SICA_DB SICADB = new SicaSegura.SICA_DB();

                        string SQL = @"SELECT dbo.PVYCR_Cauces.DenominacionCauce + '<br> ' + dbo.PVYCR_Cauces.NombreTitular AS Expr1
                                        FROM  dbo.PVYCR_Cauces INNER JOIN
                                              dbo.PVYCR_Puntos ON 
                                              dbo.PVYCR_Cauces.CodigoCauce = dbo.PVYCR_Puntos.CodigoCauce
                                              WHERE        (dbo.PVYCR_Puntos.CodigoPVYCR LIKE '" + listaISM.Rows[i][1].ToString().Trim() + "')";
                        string titular = SICADB.GetDataSIGVECTOR(SQL).Rows[0][0].ToString();
                        HTML_Tabla_Informe.Text += "<td>";
                        HTML_Tabla_Informe.Text += titular;
                        dt2.Rows[i][3] = titular;
                        HTML_Tabla_Informe.Text += "</td>";


                    }
                    catch
                    {
                        SicaSegura.SICA_DB SICADB = new SicaSegura.SICA_DB();

                        string SQL = @"SELECT dbo.PVYCR_Cauces.DenominacionCauce + '<br> ' + dbo.PVYCR_Cauces.NombreTitular AS Expr1
                                        FROM  dbo.PVYCR_Cauces INNER JOIN
                                              dbo.PVYCR_Puntos ON 
                                              dbo.PVYCR_Cauces.CodigoCauce = dbo.PVYCR_Puntos.CodigoCauce
                                              WHERE        (dbo.PVYCR_Puntos.CodigoPVYCR LIKE '" + listaISM.Rows[i][1].ToString().Trim() + "')";
                        string titular = SICADB.GetDataSIGVECTOR(SQL).Rows[0][0].ToString();
                        HTML_Tabla_Informe.Text += "<td>";
                        HTML_Tabla_Informe.Text += titular;
                        dt2.Rows[i][3] = titular;
                        HTML_Tabla_Informe.Text += "</td>";
                    }
                }
                else
                {
                    try
                    {

                        Int32 num = int.Parse(listaISM.Rows[i][u].ToString());
                        HTML_Tabla_Informe.Text += "<td>";
                        if (u == 7) { HTML_Tabla_Informe.Text += "<b>"; }
                        HTML_Tabla_Informe.Text += num.ToString("N0");
                        dt2.Rows[i][u+1] = num.ToString();
                        if (u == 7) { HTML_Tabla_Informe.Text += "</b>"; }
                        HTML_Tabla_Informe.Text += "</td>";
                    }
                    catch
                    {
                        HTML_Tabla_Informe.Text += "<td>";
                        HTML_Tabla_Informe.Text += listaISM.Rows[i][u].ToString();
                        dt2.Rows[i][u+1] = listaISM.Rows[i][u].ToString();
                        HTML_Tabla_Informe.Text += "</td>";
                    }
                }

            }
            string PuntoSICA = listaISM.Rows[i][0].ToString();

            SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();

            string SQL2 = @"SELECT dbo.PVYCR_Cauces.ReferenciaExpedientesRegistroAguas as ASV
                                        FROM  dbo.PVYCR_Cauces INNER JOIN
                                              dbo.PVYCR_Puntos ON 
                                              dbo.PVYCR_Cauces.CodigoCauce = dbo.PVYCR_Puntos.CodigoCauce
                                              WHERE        (dbo.PVYCR_Puntos.CodigoPVYCR LIKE '" + listaISM.Rows[i][1].ToString().Trim() + "')";
            string[] ASVs = DB.GetDataSIGVECTOR(SQL2).Rows[0][0].ToString().Split(',');
            string ASV = "";
            for (int ii = 0; ii < ASVs.Length; ii++)
            {
                if (ASVs[ii].ToString().Contains("ASV"))
                { ASV = ASVs[ii].ToString().Trim(); }
            }

            HTML_Tabla_Informe.Text += "<td>";
            HTML_Tabla_Informe.Text += ASV.ToString().Trim();
            dt2.Rows[i][7] = ASV.ToString().Trim();
            HTML_Tabla_Informe.Text += "</td>";


            HTML_Tabla_Informe.Text += "</tr>";
        }
        HTML_Tabla_Informe.Text += "</table>";
        HTML_Tabla_Informe.Text += "</table>";

           
        if(xcell)
        { 
            ExportToExcel(dt2);
        }
    }


    public void ExportToExcel(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            string filename = "DownloadedExcel.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();

            ////Get the HTML for the control.
            dgGrid.RenderControl(hw);
            ////Write the HTML back to the browser.
            ////Response.ContentType = application/vnd.ms-excel;
            //Response.ContentType = "application/vnd.ms-excel";
            Response.ContentType = "application/xls";

            Response.AddHeader("Content-Type", "application/xls");
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
    }


   



}
