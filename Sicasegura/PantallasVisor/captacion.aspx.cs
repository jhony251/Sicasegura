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

public partial class captacion : System.Web.UI.Page
{
    SSqlConnection conexion = new SqlConnection("Server=10.31.224.74;uid=aplisica;pwd=sica062011;database=DBSICA");
    SqlCommand comando = new SqlCommand();
    SqlDataAdapter daCaptacion = new SqlDataAdapter();
    DataSet dstCaptacion = new DataSet();
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
        sql =   " SELECT X_CAPT, Y_CAPT, HUSO_CAPT, CARTOGRAFO, ID_CAPT_1, ID_APROV, NUMERO_EXP, ANIO_EXPED, N_CAPTACIO, MUNICIPIO_, PROVINCIA_, " +
                " ESTADO_CAR, FIAB_PRECI, FIAB_ASOC_, PROCEDENCI, TIPO_CAPTA, CAUCE, TIPO_CATAS, CAPTACION, SITUACION_, ESTADO_ADM, MOTIVO_ADM, " +
                " CAUDAL_MAX, VOL_MAX_IN, DIAMETRO_C, PROFUNDIDA,  INSTALA_EL, POTENCIA, TIPO_CONTA, MOMENTO_VI, FECHA_MOME, ACUIFERO, " +
                " BW_CAPTACI, TIPO_CAP_1, CIF, NOMBRE, APELLIDO1, APELLIDO2 " +
                " FROM         MU_CAPTACIONES_BASE " +
                " WHERE DN_PK=" + Request.QueryString["pk"];
        /*crearDataSets();*/
        daCaptacion.SelectCommand = new SqlCommand(sql, conexion);
        /*Rellena Label tabla*/
        daCaptacion.Fill(dstCaptacion, "TCaptacion");

        txtapellido1.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["apellido1"].ToString();
        txtapellido2.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["apellido2"].ToString();
        txtbw.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["BW_CAPTACI"].ToString();
        txtcaptacion.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["CAPTACION"].ToString();
        txtcartografo.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["CARTOGRAFO"].ToString();
        txtcaudal.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["CAUDAL_MAX"].ToString();
        txtcif.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["CIF"].ToString();
        txtdiametro.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["DIAMETRO_C"].ToString();
        txtestado.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["ESTADO_CAR"].ToString();
        txtexpediente.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["NUMERO_EXP"].ToString() + "/" + dstCaptacion.Tables["TCaptacion"].Rows[0]["ANIO_EXPED"].ToString();
        txtfecha.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["FECHA_MOME"].ToString();
        txtfiabasoci.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["FIAB_ASOC_"].ToString();
        txtfiabprec.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["FIAB_PRECI"].ToString();
        txtinstala.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["INSTALA_EL"].ToString();
        txtmunicipio.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["MUNICIPIO_"].ToString();
        txtnombre.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["NOMBRE"].ToString();
        txtnumcaptacion.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["N_CAPTACIO"].ToString();
        txtpotencia.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["POTENCIA"].ToString();
        txtprocedencia.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["PROCEDENCI"].ToString();
        txtprofundidad.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["PROFUNDIDA"].ToString();
        txtprovincia.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["PROVINCIA_"].ToString();
        txtsituacion.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["SITUACION_"].ToString();
        txttipocap.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["TIPO_CAPTA"].ToString();
        txttipocatastro.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["TIPO_CATAS"].ToString();
        txttipocontador.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["TIPO_CONTA"].ToString();
        txtvolumen.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["VOL_MAX_IN"].ToString();
        txtX.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["X_CAPT"].ToString();
        txtY.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["Y_CAPT"].ToString();
        txthuso.Text = dstCaptacion.Tables["TCaptacion"].Rows[0]["HUSO_CAPT"].ToString();

    }
}
