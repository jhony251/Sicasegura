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
        PVYCR = Request.QueryString["PVYCR"];
        string FI, FF;
        LIT_PVYCR.Text = PVYCR;
        //PVYCR = "VB071P01";
		SQL = "SELECT COUNT(*) FROM PVYCR_Conductividad WHERE (codigopvycr like '" + PVYCR + "%' OR codigopvycr like '" + PVYCR + "') AND Cod_fuente_Dato like '13'";
		dt = EjecutaSQLSIGVECTOR(SQL);
        if (DateTime.Now.Month > 10)
        {
            FI = "01/10/" + DateTime.Now.Year.ToString();
            FF = "01/10/" + DateTime.Now.AddYears(1).Year.ToString();
        }
        else
        {
            FI = "01/10/" + DateTime.Now.AddYears(-1).Year.ToString();
            FF = "01/10/" + DateTime.Now.Year.ToString();
        }
		if(Convert.ToInt16(dt.Rows[0].ItemArray.GetValue(0))>0)
		{
            SQL = "SELECT * FROM PVYCR_Conductividad WHERE fecha_medida>'" + FI + "' AND fecha_medida<'" + FF + "' AND  (codigopvycr like '" + PVYCR + "%' OR codigopvycr like '" + PVYCR + "') AND DATEPART(mi,fecha_medida)=0 order by fecha_medida desc";
		}
		else
		{
            SQL = "SELECT * FROM PVYCR_Conductividad WHERE fecha_medida>'" + FI + "' AND fecha_medida<'" + FF + "' AND (codigopvycr like '" + PVYCR + "%' OR codigopvycr like '" + PVYCR + "') order by fecha_medida desc";
		}
        dt = EjecutaSQLSIGVECTOR(SQL);
        if (dt.Rows.Count>1)
		{
			Datos.Text = genera_modelo_de_datos(dt);
			LectTot = LectCarg = dt.Rows.Count.ToString();
			LIT_Titulo.Text = Request.QueryString["nodotexto"] + " - " + Obtiene_descripcion_punto(); ;
			Obtiene_estadísticas_punto();
		}
		else
		{
			Response.Write("No hay datos");
		}
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
        DataTable dt = new DataTable();
        dt=EjecutaSQLSIGVECTOR(SQL);


        return dt.Rows[0].ItemArray.GetValue(0).ToString(); ;
    }
    private string genera_modelo_de_datos(DataTable dt)
    {
        string html = "";
        html += "<script type=\"text/javascript\" language=\"javascript\">";
        html += "var myData = [";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //['The Coca-Cola Company', 45.07, 0.26, 0.58, '9/1 12:00am'],
            html += "['" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][0].ToString().Trim() + "'," + dt.Rows[i][3].ToString().Replace(',', '.') + "," + dt.Rows[i][8].ToString().Replace(',', '.') + "," + dt.Rows[i][6].ToString().Replace(',', '.') + "," + dt.Rows[i][7].ToString().Replace(',', '.') + "," + dt.Rows[i][4].ToString().Replace(',', '.') + "],";
        }
        html = html.Substring(0, html.Length - 1);
        html += "];</script>";
        FF = DateTime.Parse(dt.Rows[0][2].ToString().Trim());
        FI = DateTime.Parse(dt.Rows[dt.Rows.Count - 1][2].ToString().Trim());
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
    protected DataTable EjecutaSQLSIGVECTOR(string queryString)
    {
        DataTable dt = new DataTable();
        string connectionString = "";
		connectionString = ConfigurationManager.ConnectionStrings["PDASIGVECTOR"].ToString();
        //connectionString = ConfigurationSettings.AppSettings["dsn"].ToString();
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
