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


public partial class usopuntual : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection("Server=10.31.224.74;uid=aplisica;pwd=sica062011;database=DBSICA");
    SqlCommand comando = new SqlCommand();
    SqlDataAdapter daUsoPuntual = new SqlDataAdapter();
    DataSet dstUsoPuntual = new DataSet();
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
        sql = " SELECT X_PUNTUAL, Y_PUNTUAL, HUSO_PUNTL, CARTOGRAFO, ID_PUNTU_1, ID_APROV, CONF_HID_1, NUMERO_EXP, ANIO_EXPED, N_PUNTUAL, " +
              " MUNICIPIO_, PROVINCIA_, ESTADO_CAR, FIAB_PRECI, FIAB_ASOC_, TIPO_USO_G, NIVEL_USO_, NIVEL_USO1, NIVEL_US_1, CAUDAL_MAX, " +
              " VOL_MAX_AN, NUMERO_HAB, NUMERO_CAB, USO_CONSUN, MOMENTO_VI, FECHA_MOME, BW_PUNTUAL, TIPO_USO_D, CIF, NOMBRE, " +
              " APELLIDO1, APELLIDO2, DN_PK, DN_OID, DN_VERSION, DN_IX, DN_X, DN_Y " +
              " FROM         MU_USOPUNTUAL_BASE " +
              " WHERE DN_PK=" +  Request.QueryString["pk"];
        /*crearDataSets();*/
        daUsoPuntual.SelectCommand = new SqlCommand(sql, conexion);
        /*Rellena Label tabla*/
        daUsoPuntual.Fill(dstUsoPuntual, "TUsoPuntual");


        txtapellido1.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["apellido1"].ToString();
        txtapellido2.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["apellido2"].ToString();
        txtbw.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["BW_PUNTUAL"].ToString();
        txtcabezas.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["NUMERO_CAB"].ToString();
        txtcartografo.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["CARTOGRAFO"].ToString();
        txtcaudal.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["CAUDAL_MAX"].ToString();
        txtch.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["CONF_HID_1"].ToString();
        txtcif.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["CIF"].ToString();
        txtestado.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["ESTADO_CAR"].ToString();
        txtexpediente.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["NUMERO_EXP"].ToString() + "/" + dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["ANIO_EXPED"].ToString();
        txtfecha.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["FECHA_MOME"].ToString();
        txtfiabasoci.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["FIAB_ASOC_"].ToString();
        txtfiabprec.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["FIAB_PRECI"].ToString();
        txthabi.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["NUMERO_HAB"].ToString();
        txtmunicipio.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["MUNICIPIO_"].ToString();
        txtnombre.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["NOMBRE"].ToString();
        txtnuso.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["NIVEL_USO_"].ToString();
        txtnuso2.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["NIVEL_USO1"].ToString();
        txtprovincia.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["PROVINCIA_"].ToString();
        txttipouso.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["TIPO_USO_G"].ToString();
        txtusocon.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["USO_CONSUN"].ToString();
        txtvolumen.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["VOL_MAX_AN"].ToString();
        txtX.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["X_PUNTUAL"].ToString();
        txtY.Text = dstUsoPuntual.Tables["TUsoPuntual"].Rows[0]["Y_PUNTUAL"].ToString();


    }
}
