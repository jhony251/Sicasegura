using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public partial class EAforos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        string SQL = "SELECT   Aforos_D_Hidrolog.COD_CHS AS COD, "
                                +" "
                                +"Aforos_D_Hidrolog.NIVEL_08 as [Nivel Inst.(m)], Aforos_D_Hidrolog.CAUDAL_08 as [Caudal Inst.(m3/s)], "
                                +"Aforos_D_Hidrolog.NIVEL_MEDIO as [Nivel Med(m)], "
                                +"Aforos_D_Hidrolog.DATO_VOLUMEN as [Volumen (m3)] "
                                +"FROM    Aforos_D_Hidrolog "
                                +"WHERE (Aforos_D_Hidrolog.COD_FUENTE_DATO = '13') AND"
                                +"(Aforos_D_Hidrolog.FECHA_MEDIDA = '" + System.DateTime.Today.Date.AddDays(-1).ToString().Substring(0,10).Trim() + "')";
        
        dt=GetData(SQL);
        GridView2.DataSource=dt;
        GridView2.DataBind();
        Label1.Text = "Periodo: " + DateTime.Today.AddDays(-1).Date.AddHours(8).ToString() + " - " + DateTime.Today.Date.AddHours(8).ToString();
    }

    DataTable GetData(string queryString)
    {
        // Retrieve the connection string stored in the Web.config file.
        //String connectionString = ConfigurationManager.ConnectionStrings["NorthWindConnectionString"].ConnectionString;
        DataTable dt = new DataTable();
        string cadena = ConfigurationManager.ConnectionStrings["SIGVECTORConnectionString1"].ToString();
        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cadena);
        //System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=SITSQL2005;Initial Catalog=Telemedida;Persist Security Info=True;User ID=sa;Password=");
        System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
        // Fill the DataSet.
        adapter.Fill(dt);
        return dt;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("index.aspx");
    }
}
