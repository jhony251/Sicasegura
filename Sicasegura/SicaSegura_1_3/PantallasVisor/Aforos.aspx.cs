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

public partial class PantallasVisor_Aforos : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection("Server=SAP5\\SAP5SQL2005;uid=GFLUVIAL;pwd=sa9ia;database=GFLUVIAL");
    SqlCommand comando = new SqlCommand();
    SqlDataAdapter daAforos = new SqlDataAdapter();
    DataSet dstAforos = new DataSet();
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
        sql = "SELECT COD_CHS,COD_PROPIO,X_UTM_OF,Y_UTM_OF,Z_UTM_OF,NOMBRE,PARAJE,MUNICIPIO_,CAUCE,ACUIFERO_C " +
               ",COMENTARIO,RESENA,PROPIETARI,INICIO_USO,FIN_USO,CODIGO_BD_,DN_PK,DN_OID,DN_VERSION,DN_IX,DN_X " +
               ",DN_Y FROM MU_AFOROS_BASE where DN_PK=" + Request.QueryString["pk"];
        /*crearDataSets();*/
        daAforos.SelectCommand = new SqlCommand(sql, conexion);
        /*Rellena Label tabla*/
        daAforos.Fill(dstAforos, "TAforos");
        txtCod_chs.Text = dstAforos.Tables["TAforos"].Rows[0]["Cod_chs"].ToString();
        txtCod_propio.Text = dstAforos.Tables["TAforos"].Rows[0]["cod_propio"].ToString();
        txtX_UTM_OF.Text = dstAforos.Tables["TAforos"].Rows[0]["x_utm_of"].ToString();
        txtY_UTM_OF.Text = dstAforos.Tables["TAforos"].Rows[0]["y_utm_of"].ToString();        
        txtZ_UTM_OF.Text = dstAforos.Tables["TAforos"].Rows[0]["z_utm_of"].ToString();
        txtNombre.Text = dstAforos.Tables["TAforos"].Rows[0]["nombre"].ToString();
        txtparaje.Text = dstAforos.Tables["TAforos"].Rows[0]["paraje"].ToString();
        txtmunicipio.Text = dstAforos.Tables["TAforos"].Rows[0]["MUNICIPIO_"].ToString();
        txtCauce.Text = dstAforos.Tables["TAforos"].Rows[0]["CAUCE"].ToString();
        txtacuifero.Text = dstAforos.Tables["TAforos"].Rows[0]["ACUIFERO_C"].ToString();
        txtComentario.Text = dstAforos.Tables["TAforos"].Rows[0]["COMENTARIO"].ToString();
        txtresena.Text = dstAforos.Tables["TAforos"].Rows[0]["RESENA"].ToString();
        txtpropietari.Text = dstAforos.Tables["TAforos"].Rows[0]["PROPIETARI"].ToString();
        txtinicio_uso.Text = dstAforos.Tables["TAforos"].Rows[0]["INICIO_USO"].ToString();
        txtFin_uso.Text = dstAforos.Tables["TAforos"].Rows[0]["FIN_USO"].ToString();
        txtCodigo_BD.Text = dstAforos.Tables["TAforos"].Rows[0]["CODIGO_BD_"].ToString();

    }
}
