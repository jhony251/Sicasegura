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

public partial class aportacion : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection("Server=10.31.224.74;uid=aplisica;pwd=sica062011;database=DBSICA");
    SqlCommand comando = new SqlCommand();
    SqlDataAdapter daAportacion = new SqlDataAdapter();
    DataSet dstAportacion = new DataSet();
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
        sql = "SELECT CODIGO, X, Y, INVENT90 FROM MU_APORTACIONES_BASE WHERE Codigo= '" + Request.QueryString["Codigo"] + "'";
        /*crearDataSets();*/
        daAportacion.SelectCommand = new SqlCommand(sql, conexion);
        /*Rellena Label tabla*/
        daAportacion.Fill(dstAportacion, "TAportacion");

        txtCodigo.Text = dstAportacion.Tables["TAportacion"].Rows[0]["Codigo"].ToString();
        txtX.Text = dstAportacion.Tables["TAportacion"].Rows[0]["X"].ToString();
        txtY.Text = dstAportacion.Tables["TAportacion"].Rows[0]["Y"].ToString();
        txtInvent90.Text = dstAportacion.Tables["TAportacion"].Rows[0]["Invent90"].ToString();


    }
}
