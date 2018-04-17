using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class cauce : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection("Server=10.31.224.74;uid=aplisica;pwd=sica062011;database=DBSICA");
    SqlCommand comando = new SqlCommand();
    SqlDataAdapter daCauce = new SqlDataAdapter();
    DataSet dstCauce = new DataSet();
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
        sql = "SELECT     CodigoCauce, DenominacionCauce, CodigoInventario90, CodigoCampo, TipoCauce, MargenDerivacion, OtrasReferencias, ParajeToma, MunicipioToma, " +
              "        ProvinciaToma, DatosAdministrador, NombreTitular, NIFTitular, DireccionTitular, MunicipioTitular, CodPostalTitular, ProvinciaTitular, TelefonoTitular, " +
              "        ReferenciaExpedienteLibroAprovechamiento, NumeroRegistroAntiguo, ReferenciaExpedientesRegistroAguas, OtrosExpedientes, Seccion, Tomo, Hoja, " +
              "       NombreContactoCampo, DireccionContactoCampo, TelefonoContactoCampo, CaudalMaximo_LSeg, VolumenMaximoAnualLegal_M3, " +
              "        VolumenMaximoAnualTeorico_M3, X_UTM_Toma, Y_UTM_Toma, CotaToma, SuperficieRealAproximada_HAS, SuperficieInscrita_HAS, " +
              "        PorcentajeTradicional, TipoCultivo, LongitudCauce_KM,  EntreOjosYContraparada, EnActivo, MedidoEnPVYCR, TitularContactado, "  +
              "        ContadorOK " +
              " FROM         TCauces_PVYCR " +
              " WHERE codigoCauce='" + Request.QueryString["codigo"] + "'";
        
        
        /*crearDataSets();*/
        daCauce.SelectCommand = new SqlCommand(sql, conexion);
        /*Rellena Label tabla*/
        daCauce.Fill(dstCauce, "TCauce");

        txtcodigocauce.Text = dstCauce.Tables["TCauce"].Rows[0]["CodigoCauce"].ToString();
        txtdenominacion.Text = dstCauce.Tables["TCauce"].Rows[0]["DenominacionCauce"].ToString();
        txtinventario.Text = dstCauce.Tables["TCauce"].Rows[0]["CodigoInventario90"].ToString();
        txtcodcampo.Text = dstCauce.Tables["TCauce"].Rows[0]["CodigoCampo"].ToString();
        txttipocauce.Text = dstCauce.Tables["TCauce"].Rows[0]["TipoCauce"].ToString();
        txtmargen.Text = dstCauce.Tables["TCauce"].Rows[0]["MargenDerivacion"].ToString();
        txtotrasref.Text = dstCauce.Tables["TCauce"].Rows[0]["OtrasReferencias"].ToString();
        txtparaje.Text = dstCauce.Tables["TCauce"].Rows[0]["ParajeToma"].ToString();
        txtmunicipio.Text = dstCauce.Tables["TCauce"].Rows[0]["MunicipioToma"].ToString();
        txtprovincia.Text = dstCauce.Tables["TCauce"].Rows[0]["ProvinciaToma"].ToString();
        txtadministrador.Text = dstCauce.Tables["TCauce"].Rows[0]["DatosAdministrador"].ToString();
        txttitular.Text = dstCauce.Tables["TCauce"].Rows[0]["NombreTitular"].ToString();
        txtnif.Text = dstCauce.Tables["TCauce"].Rows[0]["NIFTitular"].ToString();
        txtdireccion.Text = dstCauce.Tables["TCauce"].Rows[0]["DireccionTitular"].ToString();
        txtmunicipiotit.Text = dstCauce.Tables["TCauce"].Rows[0]["MunicipioTitular"].ToString();
        txtcodpostal.Text = dstCauce.Tables["TCauce"].Rows[0]["CodPostalTitular"].ToString();
        txtprovinciatit.Text = dstCauce.Tables["TCauce"].Rows[0]["ProvinciaTitular"].ToString();
        txttel.Text = dstCauce.Tables["TCauce"].Rows[0]["TelefonoTitular"].ToString();
        txtrefexpreg.Text = dstCauce.Tables["TCauce"].Rows[0]["ReferenciaExpedienteLibroAprovechamiento"].ToString();
        txtregantiguo.Text = dstCauce.Tables["TCauce"].Rows[0]["NumeroRegistroAntiguo"].ToString();
        txtrefexpreg.Text = dstCauce.Tables["TCauce"].Rows[0]["ReferenciaExpedientesRegistroAguas"].ToString();
        txtotrosexp.Text = dstCauce.Tables["TCauce"].Rows[0]["OtrosExpedientes"].ToString();
        txtseccion.Text = dstCauce.Tables["TCauce"].Rows[0]["Seccion"].ToString();
        txttomo.Text = dstCauce.Tables["TCauce"].Rows[0]["Tomo"].ToString();
        txthoja.Text = dstCauce.Tables["TCauce"].Rows[0]["Hoja"].ToString();
        txtcontacto.Text = dstCauce.Tables["TCauce"].Rows[0]["NombreContactoCampo"].ToString();

        txtdireccontacto.Text = dstCauce.Tables["TCauce"].Rows[0]["DireccionContactoCampo"].ToString();
        txttelcontacto.Text = dstCauce.Tables["TCauce"].Rows[0]["TelefonoContactoCampo"].ToString();
        txtcaudalmax.Text = dstCauce.Tables["TCauce"].Rows[0]["CaudalMaximo_LSeg"].ToString();
        txtvolmaxlegal.Text = dstCauce.Tables["TCauce"].Rows[0]["VolumenMaximoAnualLegal_M3"].ToString();
        txtvolmaxteorico.Text = dstCauce.Tables["TCauce"].Rows[0]["VolumenMaximoAnualTeorico_M3"].ToString();
        txtx.Text = dstCauce.Tables["TCauce"].Rows[0]["X_UTM_Toma"].ToString();
        txty.Text = dstCauce.Tables["TCauce"].Rows[0]["Y_UTM_Toma"].ToString();
        txtcotatoma.Text = dstCauce.Tables["TCauce"].Rows[0]["CotaToma"].ToString();
        txtsupreal.Text = dstCauce.Tables["TCauce"].Rows[0]["SuperficieRealAproximada_HAS"].ToString();
        txtsupinscrita.Text = dstCauce.Tables["TCauce"].Rows[0]["SuperficieInscrita_HAS"].ToString();
        txtporcentaje.Text = dstCauce.Tables["TCauce"].Rows[0]["PorcentajeTradicional"].ToString();
        txttipocultivo.Text = dstCauce.Tables["TCauce"].Rows[0]["TipoCultivo"].ToString();

        txtlongitud.Text = dstCauce.Tables["TCauce"].Rows[0]["LongitudCauce_KM"].ToString();
        //txtobservaciones.Text = dstCauce.Tables["TCauce"].Rows[0]["Observaciones"].ToString();
        txtentreojos.Text = dstCauce.Tables["TCauce"].Rows[0]["EntreOjosYContraparada"].ToString();
        txtenactivo.Text = dstCauce.Tables["TCauce"].Rows[0]["EnActivo"].ToString();
        txtmedido.Text = dstCauce.Tables["TCauce"].Rows[0]["MedidoEnPVYCR"].ToString();
        txttitularcontac.Text = dstCauce.Tables["TCauce"].Rows[0]["TitularContactado"].ToString();
        txtcontadorok.Text = dstCauce.Tables["TCauce"].Rows[0]["ContadorOK"].ToString();
        


    }

}
