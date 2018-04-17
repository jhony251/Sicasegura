using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using System.Data.SqlClient;

public partial class Gravedad : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection("Server=10.31.224.74;uid=aplisica;pwd=sica062011;database=DBSICA");
    SqlCommand comando = new SqlCommand();
    SqlDataAdapter daGravedad = new SqlDataAdapter();
    DataSet dstGravedad = new DataSet();
    String sentenciaSel;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.DataBind();
        if (!IsPostBack)
        {
            CrearDataset();
        }
    }

    void CrearDataset()
    {
        string sql;
        sql = "SELECT CODIGO, X, Y, INVENT90 FROM MU_GRAVEDAD_BASE WHERE Codigo= '" + Request.QueryString["Codigo"] + "'";
        /*crearDataSets();*/
        daGravedad.SelectCommand = new SqlCommand(sql, conexion);
        /*Rellena Label tabla*/
        daGravedad.Fill(dstGravedad, "TGravedad");

        txtCodigo.Text = dstGravedad.Tables["TGravedad"].Rows[0]["Codigo"].ToString();
        txtX.Text = dstGravedad.Tables["TGravedad"].Rows[0]["X"].ToString();
        txtY.Text = dstGravedad.Tables["TGravedad"].Rows[0]["Y"].ToString();
        txtInvent90.Text = dstGravedad.Tables["TGravedad"].Rows[0]["Invent90"].ToString();


    }
}
