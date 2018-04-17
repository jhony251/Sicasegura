using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SICA_AutomaticProcess_LocalizadorIncidencias : System.Web.UI.Page
{

    DataTable lineasRojas = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        DarFormatoDataTableLineasRojas();
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        
        DataTable tablaElementosMedida = new DataTable();
        DataTable tablaResultadoSQL = new DataTable();
        //DataTableReader elementosMedida = new DataTableReader();
        //DataTableReader resultadosSQL = new DataTableReader();
        double lecturaPrevia;
        double lecturaActual;
        String linea;
                // Hay que tener en cuenta el formato en que SQL nos retorna los valores por si es necesario
        // añadir estructuras de control que permitan guardar los valores de las tablas en formato útil.
        string SQL=  "SELECT DISTINCT CodigoPVYCR AS SICA, idElementoMedida AS EM " +
                            "FROM dbo.PVYCR_DatosMotores "+
                            "ORDER BY SICA, EM";
        //elementosMedida = tablaElementosMedida.CreateDataReader();
        tablaElementosMedida =  DB.GetDataSIGVECTOR(SQL);

        for (int i = 0; i<tablaElementosMedida.Rows.Count; i++)
        {
            string Codigosica = tablaElementosMedida.Rows[i][0].ToString();
            string EM = tablaElementosMedida.Rows[i][1].ToString();

            SQL = " SELECT Cod_Fuente_Dato AS CodFuente, Fecha_Medida AS Fecha, LecturaContador_M3 AS Lectura, idIncidenciaVolumetrica AS IdIncidencia " +
                            " FROM dbo.PVYCR_DatosMotores " +
                            " WHERE 	CodigoPVYCR='" + Codigosica + "' AND " +
                                    " idElementoMedida='" + EM + "' AND lecturacontador_m3 is not null AND" +
                                    " (Cod_Fuente_Dato='01' OR Cod_Fuente_Dato='05' OR Cod_Fuente_Dato='10' OR Cod_Fuente_Dato='13') AND " +
                                    " (CONVERT(CHAR(10), Fecha_Medida, 120) >= '2017-10-01') " +
                            " ORDER BY Fecha_Medida";
            tablaResultadoSQL = DB.GetDataSIGVECTOR(SQL);
            if (tablaResultadoSQL.Rows.Count > 0)
            {
                lecturaPrevia = 0; // Suponiendo que las lecturas no dan nunca valores negativos
                lecturaActual = 0;
                lecturaPrevia = Double.Parse(tablaResultadoSQL.Rows[0][2].ToString()); 
                
                for (int u = 1; u<tablaResultadoSQL.Rows.Count; u++)
                {
                   
                    lecturaActual = Double.Parse(tablaResultadoSQL.Rows[u][2].ToString());
                    if (lecturaActual < lecturaPrevia) // ¿Menor estricto?
                    {
                        DataRow dr = lineasRojas.NewRow();
                        //string[] columnas = "SICA#EM#FECHA#CFD#LECTURA#IDINCVOL".Split('#');
                        dr[0] = Codigosica;
                        dr[1] = EM;
                        dr[2] = tablaResultadoSQL.Rows[u][1].ToString();
                        dr[3] = tablaResultadoSQL.Rows[u][0].ToString();
                        dr[4] = lecturaActual;
                        dr[5] = tablaResultadoSQL.Rows[u][3].ToString();

                        // Guardar en fichero o documento excel
                        //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\ToFile\WriteLines.txt", true))
                        //{
                        //    linea = elementosMedida.GetString("SICA") + ";" +
                        //            elementoMedida.GetString("EM") + ";" +
                        //            resultadoSQL.GetString("CodFuente").ToString() + ";" + // No estoy seguro por el tipo de la columna en la BBDD
                        //            resultadoSQL.GetDateTime("Fecha").ToString() + ";" +
                        //            resultadoSQL.GetFloat("Lectura").ToString() + ";" +
                        //            resultadoSQL.GetInt32("IdIncidencia").ToString(); // No estoy seguro por el tipo de la columna en la BBDD
                        //    file.WriteLine(linea);
                        //}
                        lineasRojas.Rows.Add(dr);
                    }

                    
                    
                    lecturaPrevia = lecturaActual;
                }



            }


        }

       
        ExportarDataTableaExcel(lineasRojas);



    }

    private void DarFormatoDataTableLineasRojas()
    {
        string[] columnas = "SICA#EM#FECHA#CFD#LECTURA#IDINCVOL".Split('#');
        for (int col = 0; col < columnas.Length; col++)
        {
            lineasRojas.Columns.Add(columnas[col].ToString());
        }

    }
    public void ExportarDataTableaExcel(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            string filename = "DownloadedExcel.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            System.Web.UI.WebControls.DataGrid dgGrid = new System.Web.UI.WebControls.DataGrid();
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