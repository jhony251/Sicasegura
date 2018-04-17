using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

public partial class SICAH_Agrupaciones_InsertaSupConceson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["ID"];
        if (!estaYaExcedida(id))
        {
            try { GetDataDBSICA("Insert Into PVYCR_AprovechamientosExcedidos (id_aprovechamiento, fecha_deteccion) values (" + id + ",'" + DateTime.Now.ToShortDateString() + "')"); }
            catch (Exception ee) { }
            Response.Redirect("../agrupaciones_detalle.aspx");
        }
        else
        {
            Response.Redirect("../agrupaciones_detalle.aspx");
        }
        

    }
    private bool estaYaExcedida(string id)
    {
        string sql = "SELECT * from PVYCR_AprovechamientosExcedidos where id_aprovechamiento=" + id;
        DataTable dt = new DataTable();
        dt = GetDataDBSICA(sql);
        return (dt.Rows.Count>0);
    }
    private DataTable GetDataDBSICA(String queryString)
    {
        // Retrieve the connection string stored in the Web.config file.
        String connectionString = ConfigurationManager.ConnectionStrings["DBSICA"].ConnectionString;
        DataTable dt = new DataTable();
        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);
        //System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=SITSQL2005;Initial Catalog=Telemedida;Persist Security Info=True;User ID=sa;Password=");
        System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
        // Fill the DataSet.
        adapter.Fill(dt);
        return dt;
    }
}
