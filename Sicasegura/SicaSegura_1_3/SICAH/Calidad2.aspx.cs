using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;

public partial class SICAH_Calidad2 : System.Web.UI.Page
{
    string SQL, PVYCR;
    DataTable Dconduct = new DataTable();
    DateTime FI, FF = new DateTime();
    string LectTot, LectCarg = "";
    DataTable dt = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //PVYCR = Request.QueryString["PVYCR"];
        PVYCR = "VB071P01";
        SQL = "SELECT * FROM SICA_Topkapi_calidad WHERE codigopvycr like '" + PVYCR + "%' AND DATEPART(mi,Fecha)=0 order by fecha desc";
        dt = EjecutaSQLDBSICA(SQL);
        Datos.Text = genera_modelo_de_datos(dt);
        LectTot = LectCarg = dt.Rows.Count.ToString();
        LIT_Titulo.Text = PVYCR + "- C01 -" + Obtiene_descripcion_punto(); ;
        Obtiene_estadísticas_punto();
        
        //ExportXLS("datos");
    }
    private void Obtiene_estadísticas_punto()
    {
        
        LIT_info.Text = FI.ToShortDateString() + " - " + FF.ToShortDateString();
        LIT_info0.Text = LectTot;
        LIT_info1.Text = LectCarg;
        
    }
    private string Obtiene_descripcion_punto()
    {
        string SQL = "Select DenominacionCauce from PVYCR_Cauces where codigocauce like'" + PVYCR.Substring(0,PVYCR.Length-3) + "'";
        DataSet dt = new DataSet();
        dt=EjecutaSQLSIGVECTOR(SQL);


        return dt.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); ;
    }
    private string genera_modelo_de_datos(DataTable dt)
    {
        string html = "";
        html += "<script type=\"text/javascript\" language=\"javascript\">";
        html += "var myData = [";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //['The Coca-Cola Company', 45.07, 0.26, 0.58, '9/1 12:00am'],
            html += "['" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][0].ToString().Trim() + "'," + dt.Rows[i][2].ToString() + "," + dt.Rows[i][3].ToString() + "," + dt.Rows[i][4].ToString() + "," + dt.Rows[i][5].ToString() + "," + dt.Rows[i][6].ToString() + "],";
        }
        html = html.Substring(0, html.Length - 1);
        html += "];</script>";
        FF = DateTime.Parse(dt.Rows[0][1].ToString().Trim());
        FI = DateTime.Parse(dt.Rows[dt.Rows.Count - 1][1].ToString().Trim());
        return html;
    }
    public void ExportCSV(DataTable data, string fileName)
    {
        HttpContext context = HttpContext.Current;
        context.Response.Clear();
        //   context.Response.Charset = System.Text.UTF8Encoding.UTF8.EncodingName.ToString();
        //   context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        context.Response.ContentType = "text/csv";
        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".csv");
        //rite column header names
        for (int i = 0; i < data.Columns.Count; i++)
        {
            if (i > 0)
            {
                context.Response.Write(",");
            }
            context.Response.Write(data.Columns[i].ColumnName);
        }
        context.Response.Write(Environment.NewLine);
        //Write data
        foreach (DataRow row in data.Rows)
        {
            for (int i = 0; i < data.Columns.Count; i++)
            {
                if (i > 0)
                {
                    context.Response.Write(",");
                }
                context.Response.Write(row.ItemArray[i].ToString());
            }
            context.Response.Write(Environment.NewLine);
        }
        context.Response.End();
    }
    public void ExportXLS(string filename)
    {
        HttpResponse response = HttpContext.Current.Response;

        // first let's clean up the response.object 
        response.Clear();
        response.Charset = "";

        // set the response mime type for excel 
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");

        // create a string writer 
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                // instantiate a datagrid 
                DataGrid dg = new DataGrid();
                dg.DataSource = dt;
                dg.DataBind();
                
                dg.RenderControl(htw);
                response.Write(sw.ToString());
                response.End();
            }
        }
    }
    protected DataTable EjecutaSQLDBSICA(string queryString)
    {
        string connectionString = "";
        connectionString = ConfigurationSettings.AppSettings["dsn_dbsica"].ToString();
        DataTable dtemp = new DataTable();

        System.Data.SqlClient.SqlConnection connection;
        connection = new System.Data.SqlClient.SqlConnection(connectionString);
        System.Data.SqlClient.SqlDataAdapter adapter;
        adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
        adapter.Fill(dtemp);
        return dtemp;
    }
    protected DataSet EjecutaSQLSIGVECTOR(string queryString)
    {
        DataSet dt = new DataSet();
        string connectionString = "";
        connectionString = ConfigurationSettings.AppSettings["dsn"].ToString();
        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);
        //System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=SITSQL2005;Initial Catalog=Telemedida;Persist Security Info=True;User ID=sa;Password=");
        System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
        // Fill the DataSet.
        adapter.Fill(dt);
        return dt;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ExportCSV(dt,"datos");
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        ExportXLS("datos");
    }
}
