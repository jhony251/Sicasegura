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
using System.Globalization;

public partial class infoPtoCrol : System.Web.UI.Page
{
    /*conexion SqlClient.SqlConnection = New SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))*/
    SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["dsn"]);
    SqlConnection conexionVisor = new SqlConnection(ConfigurationManager.AppSettings["dsnVisor"]);
    SqlCommand comando = new SqlCommand();
    SqlDataAdapter daPuntosM = new SqlDataAdapter();
    SqlDataAdapter daPuntosA = new SqlDataAdapter();
    DataSet dstPuntos = new DataSet();
    String sentenciaSel;
    string tipo;
    //IFormatProvider formato = new CultureInfo("dd-mm-yyyy", true);
    public void Page_Load(object sender, EventArgs e)
    {
        Page.DataBind();

        //Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType(), "getD", "<script>D=document.all;</script>");
        ClientScript.RegisterStartupScript(Page.GetType(), "initD", "<script>initDXCal();</script>");
        imgCalFRDesde.Attributes.Add("onclick", "dxcal(this,document.getElementById('" + txtFechaDesde.ClientID + "'),'dd/mm/yyyy');");

        if (!IsPostBack)
        {


            /* obtendremos si el punto es de motores o de acequias*/

            tipo = obtenerTipo();

            tipo = Convert.ToString(comando.ExecuteScalar());
            if (tipo == "M")
            {
                crearDataSetMotores();
            }
            else
            {
                crearDatasetAcequias();
            }
            conexion.Close();
        }

    }
    public string obtenerTipo()
    {
        string tipoPuntos;
        comando.CommandText = "select tipoPunto from PVYCR_Puntos where CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "'";
        comando.CommandType = CommandType.Text;
        comando.Connection = conexion;
        if (conexion.State == ConnectionState.Closed)
        {
            conexion.Open();
        }
        tipoPuntos = Convert.ToString(comando.ExecuteScalar());

        return tipoPuntos;
    }

    public void crearDataSetMotores()
    {
        //primero cargamos la cabecera
        CrearDatasetMPto();

        // DATOSMOTORES, OBTENEMOS LOS DISTINTOS ELEMENTOS DE MEDIDA PARA EL PUNTO
        sentenciaSel = "SELECT distinct d.idelementoMedida " +
                        "FROM PVYCR_DatosMotores D " +
                        "WHERE d.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' ";
        daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
        daPuntosM.Fill(dstPuntos, "TablaElementosV");
        int i;

        //PARA CADA ELEMENTO DE MEDIDA OBTENEMOS EL REGISTRO QUE TENGA FECHA MAS ACTUAL
        for (i = 0; i < dstPuntos.Tables["TablaElementosV"].Rows.Count; i++)
        {
            sentenciaSel = "SELECT Top 1 d.idelementomedida idelementomedidaV, d.Fecha_medida Fecha_medidaV,d.LecturaContador_M3 " +
                           "FROM PVYCR_Puntos P, PVYCR_DatosMotores D " +
                           "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS " +
                           " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosV"].Rows[i]["idElementoMedida"] + "' " +
                         "order by d.fecha_medida desc ";



            daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
            /*Rellena Label tabla*/
            daPuntosM.Fill(dstPuntos, "TablaVolumenes");

        }

      

        //DATOS ALIMENTACION
        sentenciaSel = "SELECT distinct d.idelementoMedida " +
                        "FROM PVYCR_DatosAlimentacion D " +
                        "WHERE d.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' ";
        daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
        daPuntosM.Fill(dstPuntos, "TablaElementosE");
        for (i = 0; i < dstPuntos.Tables["TablaElementosE"].Rows.Count; i++)
        {
            sentenciaSel = "SELECT top 1 d.idElementoMedida idElementoMedidaE,d.Fecha_medida Fecha_medidaE,d.LecturaI,d.LecturaII,d.LecturaIII,d.Total_KWH,d.Total_Kvar " +
                           "FROM PVYCR_Puntos P, PVYCR_DatosAlimentacion D " +
                           "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS " +
                           " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosE"].Rows[i]["idElementoMedida"] + "' " +
                           "order by d.fecha_medida desc ";

            //dstPuntos.Tables["TablaAlimentacion"].Clear();
            daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
            daPuntosM.Fill(dstPuntos, "TablaAlimentacion");
        }
       
        //DATOS HOROMETROS
        sentenciaSel = "SELECT distinct d.idelementoMedida " +
                        "FROM PVYCR_DatosHorometros D " +
                        "WHERE d.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' ";
        daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
        daPuntosM.Fill(dstPuntos, "TablaElementosH");
        for (i = 0; i < dstPuntos.Tables["TablaElementosH"].Rows.Count; i++)
        {
            sentenciaSel = "SELECT top 1 d.idelementoMedida idelementomedidaH, d.Fecha_medida Fecha_medidaH,d.HorasIntervalo " +
                         "FROM PVYCR_Puntos P, PVYCR_DatosHorometros D " +
                         "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS " +
                         " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosH"].Rows[i]["idElementoMedida"] + "' " +
                         "order by d.fecha_medida desc ";



            daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
            /*Rellena Label tabla*/
            daPuntosM.Fill(dstPuntos, "TablaHorometros");

        }
      
        //Hacemos el repeat para obtener los datos
        rptCabecera.DataSource = dstPuntos.Tables["TablaCabecera"];
        rptCabecera.DataBind();
        rptPuntosE.DataSource = dstPuntos.Tables["TablaAlimentacion"];
        rptPuntosE.DataBind();
        rptPuntosV.DataSource = dstPuntos.Tables["TablaVolumenes"];
        rptPuntosV.DataBind();
        rptPuntosH.DataSource = dstPuntos.Tables["TablaHorometros"];
        rptPuntosH.DataBind();

    }
    public void crearDatasetAcequias()
    {
        CrearDatasetAPto();

        sentenciaSel = "SELECT distinct d.idelementoMedida " +
                        "FROM PVYCR_DatosAcequias D " +
                        "WHERE d.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' ";
        daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
        daPuntosM.Fill(dstPuntos, "TablaElementosAc");
        int i;
        for (i = 0; i < dstPuntos.Tables["TablaElementosAc"].Rows.Count; i++)
        {

            sentenciaSel = "SELECT Top 1 d.idElementomedida, d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto, " +
                "p.X_UTM,p.Y_UTM, d.Fecha_medida, C.denominacionCauce,Escala_M,Calado_M, " +
                "TipoObtencionCaudal,RegimenCurva,NumeroParada,Caudal_M3S,DUDA_CALIDAD,d.Observaciones " +
                "FROM PVYCR_Puntos P, PVYCR_DatosAcequias D, PVYCR_Cauces C " +
                "WHERE  p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +
                "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI " +
                " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosAc"].Rows[i]["idElementoMedida"] + "' " +
                "order by d.fecha_medida desc ";
            /*crearDataSets();*/
            daPuntosA.SelectCommand = new SqlCommand(sentenciaSel, conexion);
            /*Rellena Label tabla*/
            daPuntosA.Fill(dstPuntos, "TablaAcequias");
        }
        rptCabeceraA.DataSource = dstPuntos.Tables["TablaCabeceraA"];
        rptCabeceraA.DataBind();            
        rptPuntosA.DataSource = dstPuntos.Tables["TablaAcequias"];
        rptPuntosA.DataBind();
        //}
    
    }
    public void CrearDatasetMPto()
    {
        string sql;

        sql = "SELECT P.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto,p.X_UTM,p.Y_UTM, C.denominacionCauce " +
              "FROM PVYCR_Puntos P, PVYCR_Cauces C " +
              "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] +
              "' and P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI";

        /*crearDataSets();*/
        daPuntosM.SelectCommand = new SqlCommand(sql, conexion);
        /*Rellena Label tabla*/
        daPuntosM.Fill(dstPuntos, "TablaCabecera");


    }
    public void CrearDatasetAPto()
    {
        string sql;
        sql = "SELECT P.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto,p.X_UTM,p.Y_UTM, C.denominacionCauce " +
               "FROM PVYCR_Puntos P, PVYCR_Cauces C " +
               "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] +
               "' and P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI";
        /*crearDataSets();*/
        daPuntosA.SelectCommand = new SqlCommand(sql, conexion);
        /*Rellena Label tabla*/
        daPuntosA.Fill(dstPuntos, "TablaCabeceraA");
        //rptPuntosA.DataSource = dstPuntos.Tables["TablaCabeceraA"];
        //rptPuntosA.DataBind();
    }
    protected string checkVisibleV(object sender)
    {
        if (Convert.ToString(dstPuntos.Tables["TablaVolumenes"].Rows[0]["Fecha_medidaV"]) == "")
        {
            return "none";
        }
        else
        {
            return "";
        }
    }
    protected string checkVisibleH(object sender)
    {
        if (Convert.ToString(dstPuntos.Tables["TablaHorometros"].Rows[0]["Fecha_medidaH"]) == "")
        {
            return "none";
        }
        else
        {
            return "";
        }
    }
    protected string checkVisibleE(object sender)
    {
        if (Convert.ToString(dstPuntos.Tables["TablaAlimentacion"].Rows[0]["Fecha_medidaE"]) == "")
        {
            return "none";
        }
        else
        {
            return "";
        }
    }
    protected string checkVisibleM(object sender)
    {
        if ((dstPuntos.Tables["TablaHorometros"].Rows[0]["Fecha_medidaH"].ToString() == "") &&
            (dstPuntos.Tables["TablaAlimentacion"].Rows[0]["Fecha_medidaE"].ToString() == "") &&
            (dstPuntos.Tables["TablaVolumenes"].Rows[0]["Fecha_medidaV"].ToString() == ""))
        {
            return "none";
        }
        else
        {
            return "";
        }
    }
    protected string checkVisibleA(object sender)
    {
        if (Convert.ToString(dstPuntos.Tables["TablaAcequias"].Rows[0]["Fecha_medida"]) == "")
        {
            return "none";
        }
        else
        {
            return "";
        }
    }
    protected string checkSinLecturasV(object sender)
    {
        string resultado;
        if (dstPuntos.Tables["TablaVolumenes"].Rows.Count == 0)
        {
            resultado = "<tr><td style='background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:red; font-size:11px' ";
            resultado += "colspan=2><i>No tiene lecturas</i></td></tr>		";
            return resultado;
        }
        else
        {
            resultado = "";
            return resultado;
        }
    }
    protected string checkSinLecturasH(object sender)
    {
        string resultado;
        if (dstPuntos.Tables["TablaHorometros"].Rows.Count == 0) 
        {
            resultado = "<tr><td style='background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:red; font-size:11px' ";
            resultado += "colspan=2><i>No tiene lecturas</i></td></tr>		";
            return resultado;
        }
        else
        {
            resultado = "";
            return resultado;
        }
    }
    protected string checkSinLecturasE(object sender)
    {
        string resultado;
        if (dstPuntos.Tables["TablaAlimentacion"].Rows.Count == 0)
        {
            resultado = "<tr><td style='background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:red; font-size:11px' ";
            resultado += "colspan=2><i>No tiene lecturas</i></td></tr>		";
            return resultado;
        }
        else
        {
            resultado = "";
            return resultado;
        }
    }
    protected string checkSinLecturasA(object sender)
    {
        string resultado;
        if (dstPuntos.Tables["TablaAcequias"].Rows.Count == 0)
        {
            resultado = "<tr><td style='background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:red; font-size:11px' ";
            resultado += "colspan=2><i>No tiene lecturas</i></td></tr>		";
            return resultado;
        }
        else
        {
            resultado = "";
            return resultado;
        }
    }

    protected String checkNumLecturasV(DataRowView elDataItem)
    {
        String NLecturas;
        // Obtenemos el nº o cantidad de lecturas que existen para el punto de control
        comando.CommandText = "select count(*) as NumLecturas from PVYCR_Puntos P, PVYCR_DatosMotores D " +
                       "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                       " d.idElementoMedida = '" + elDataItem["idElementomedidaV"] + "' and " +
                       "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS";
        comando.CommandType = CommandType.Text;
        comando.Connection = conexion;
        if (conexion.State == ConnectionState.Closed)
        {
            conexion.Open();
        }

        NLecturas = string.Format("{0:#,##}", Convert.ToString(comando.ExecuteScalar()));
        return NLecturas;

    }
    protected String checkNumLecturasE(DataRowView elDataItem)
    {
        String NLecturas;
        // Obtenemos el nº o cantidad de lecturas que existen para el punto de control
        comando.CommandText = "select count(*) as NumLecturas from PVYCR_Puntos P, PVYCR_DatosAlimentacion D " +
                       "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                       " d.idElementoMedida = '" + elDataItem["idElementomedidaE"] + "' and " +
                       "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS";
        comando.CommandType = CommandType.Text;
        comando.Connection = conexion;
        if (conexion.State == ConnectionState.Closed)
        {
            conexion.Open();
        }

        NLecturas = string.Format("{0:#,##}", Convert.ToString(comando.ExecuteScalar()));
        return NLecturas;

    }
    protected String checkNumLecturasH(DataRowView elDataItem)
    {
        String NLecturas;
        // Obtenemos el nº o cantidad de lecturas que existen para el punto de control
        comando.CommandText = "select count(*) as NumLecturas from PVYCR_Puntos P, PVYCR_DatosHorometros D " +
                       "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                       " d.idElementoMedida = '" + elDataItem["idElementomedidaH"] + "' and " +
                       "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS";
        comando.CommandType = CommandType.Text;
        comando.Connection = conexion;
        if (conexion.State == ConnectionState.Closed)
        {
            conexion.Open();
        }

        NLecturas = string.Format("{0:#,##}", Convert.ToString(comando.ExecuteScalar()));
        return NLecturas;

    }
    protected String checkNumLecturasM()
    {   
        Int32 NLecturasM, NLecturasA, NLecturasH;
        string NLecturas;
        // Obtenemos el nº o cantidad de lecturas que existen para el punto de control Motores
        comando.CommandText = "select count(*) as NumLecturas from PVYCR_Puntos P, PVYCR_DatosMotores D " +
                       "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                       "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS";
        comando.CommandType = CommandType.Text;
        comando.Connection = conexion;
        if (conexion.State == ConnectionState.Closed)
        {
            conexion.Open();
        }

        NLecturasM = Convert.ToInt32(comando.ExecuteScalar());
        /*ALIMENTACION*/
        // Obtenemos el nº o cantidad de lecturas que existen para el punto de control
        comando.CommandText = "select count(*) as NumLecturas from PVYCR_Puntos P, PVYCR_DatosAlimentacion D " +
                       "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                       "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS";
        comando.CommandType = CommandType.Text;
        comando.Connection = conexion;
        if (conexion.State == ConnectionState.Closed)
        {
            conexion.Open();
        }

        NLecturasA = Convert.ToInt32(comando.ExecuteScalar());
        /* HOROMETROS */
        // Obtenemos el nº o cantidad de lecturas que existen para el punto de control
        comando.CommandText = "select count(*) as NumLecturas from PVYCR_Puntos P, PVYCR_DatosHorometros D " +
                       "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                       "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS";
        comando.CommandType = CommandType.Text;
        comando.Connection = conexion;
        if (conexion.State == ConnectionState.Closed)
        {
            conexion.Open();
        }
        NLecturasH = Convert.ToInt32(comando.ExecuteScalar());
        
        NLecturas = string.Format("{0:#,##}", Convert.ToString(NLecturasH + NLecturasA + NLecturasM));
        return NLecturas;

    }
    protected String checkNumLecturasQ(DataRowView elDataItem)
    {
        String NLecturas;
        // Obtenemos el nº o cantidad de lecturas que existen para el punto de control
        comando.CommandText = "select count(*) as NumLecturas from PVYCR_Puntos P, PVYCR_DatosAcequias D " +
                       "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                       " d.idElementoMedida = '" + elDataItem["idElementomedida"] + "' and " +
                       "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS";
        comando.CommandType = CommandType.Text;
        comando.Connection = conexion;
        if (conexion.State == ConnectionState.Closed)
        {
            conexion.Open();
        }

        NLecturas = string.Format("{0:#,##}", Convert.ToString(comando.ExecuteScalar()));
        return NLecturas;

    }
    protected string checkNumLecturasA()
    {
        string NLecturas;
        // Obtenemos el nº o cantidad de lecturas que existen para el punto de control
        comando.CommandText = "select count(*) as NumLecturas from PVYCR_Puntos P, PVYCR_DatosAcequias D " +
                       "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                       "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS";
        comando.CommandType = CommandType.Text;
        comando.Connection = conexion;
        if (conexion.State == ConnectionState.Closed)
        {
            conexion.Open();
        }


        NLecturas = string.Format("{0:#,##}", Convert.ToString(comando.ExecuteScalar()));

        return NLecturas;


    }
    protected string checkVisibleLecturasA()
    {
        tipo = obtenerTipo();
        if (tipo != "M")
        {
            return "";
        }
        else
        {
            return "none";
        }
    }
    protected string checkVisibleLecturasM()
    {
        tipo = obtenerTipo();
        if (tipo == "M")
        {
            return "";
        }
        else
        {
            return "none";
        }
    }
    protected string checkOcultarTabla()
    {
        string numL;
        tipo = obtenerTipo();
        if (tipo == "M")
        {
            numL = checkNumLecturasM();
            if (numL == "0")
                return "none";
            else
                return "";
        }
        else
        {
            numL = checkNumLecturasA();
            if (numL == "none")
                return "0";
            else
                return "";
        }
    }
    public void btnAceptarFecha_click(object sender, EventArgs e)
    {
        //Response.Write("Fecha: " + txtFechaDesde.Text);
        if (conexion.State == ConnectionState.Closed)
        {
            conexion.Open();
        }
        tipo = obtenerTipo();
        tipo = Convert.ToString(comando.ExecuteScalar());
        if (txtFechaDesde.Text != "")
        {
            if (tipo == "M")
            {
                crearSelectMFecha();
            }
            else
            {
                crearSelectAFecha();
            }
        }

    }
    public void crearSelectMFecha()
    {
        dstPuntos.Clear();
        //PONEMOS LA CABECERA
        CrearDatasetMPto();
        //datos motores
        string sentenciaSel;
        int i;
        
        /* Buscamos los diferentes elementos de medida para el punto */
        sentenciaSel = "SELECT distinct d.idelementomedida " +
                        "FROM PVYCR_DatosMotores D " +
                        "WHERE d.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' ";
        daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
        daPuntosM.Fill(dstPuntos, "TablaElementosV");
 
        /* PARA CADA ELEMENTO DE MEDIDA OBTENEMOS EL REGISTRO QUE TENGA FECHA MAS ACTUAL */
        for (i = 0; i < dstPuntos.Tables["TablaElementosV"].Rows.Count; i++)
        {
 
            /* para cada elemento de medida buscamos si tiene lectura para la fecha seleccionada */
            sentenciaSel = "SELECT TOP 1 d.idElementoMedida idElementoMedidaV,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto,p.X_UTM,p.Y_UTM, " +
                           "d.Fecha_medida fecha_medidaV,d.LecturaContador_M3, " +
                           "C.denominacionCauce " +
                           "FROM PVYCR_Puntos P, PVYCR_DatosMotores D, PVYCR_Cauces C " +
                           "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and Convert(nvarchar,fecha_medida,103) = '" +
                           txtFechaDesde.Text + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +//DateTime.Parse(txtFechaDesde.Text)
                           "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI " +
                         " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosV"].Rows[i]["idElementoMedida"] + "' " +
                           "ORDER BY FECHA_medida DESC ";

            daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
            /*Rellena Label tabla*/
            daPuntosM.Fill(dstPuntos, "TablaVolumenes");

        
            DataRow[] drnV = dstPuntos.Tables["TablaVolumenes"].Select("idelementomedidaV = '" + dstPuntos.Tables["TablaElementosV"].Rows[i]["idElementoMedida"]+ "' ");

            if (drnV.Length == 0)
            {
                /*Si no tiene lectura para el día seleccionado buscamos la fecha más próxima que tenga lectura*/
                //Response.Write("cuenta: " + cuenta);
                sentenciaSel = "SELECT TOP 1 d.idElementoMedida idElementoMedidaV,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto,p.X_UTM,p.Y_UTM, " +
                                  "d.Fecha_medida fecha_medidaV,d.LecturaContador_M3, " +
                                  "C.denominacionCauce " +
                                  "FROM PVYCR_Puntos P, PVYCR_DatosMotores D, PVYCR_Cauces C " +
                                  "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and fecha_medida < '" +
                                  txtFechaDesde.Text + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +//DateTime.Parse(txtFechaDesde.Text)
                                  "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI " +
                                  " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosV"].Rows[i]["idElementoMedida"] + "' " +
                                  "ORDER BY FECHA_medida DESC ";
                daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
                daPuntosM.Fill(dstPuntos, "TablaVolumenes");

                drnV = dstPuntos.Tables["TablaVolumenes"].Select("idelementomedidaV = '" + dstPuntos.Tables["TablaElementosV"].Rows[i]["idElementoMedida"] + "' ");
                if (drnV.Length == 0)
                {
                    /*si no tiene lectura anterior a la fecha seleccionada, es decir si la fecha seleccionada es anterior a la primera 
                    lectura, mostraremos la primera lectura*/

                    sentenciaSel = "SELECT TOP 1 d.idElementoMedida idElementoMedidaV,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto,p.X_UTM,p.Y_UTM, " +
                                  "d.Fecha_medida fecha_medidaV,d.LecturaContador_M3, " +
                                  "C.denominacionCauce " +
                                  "FROM PVYCR_Puntos P, PVYCR_DatosMotores D, PVYCR_Cauces C " +
                                  "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                                  "Fecha_medida is not null and " +
                                  "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +//DateTime.Parse(txtFechaDesde.Text)
                                  "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI " +
                                  " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosV"].Rows[i]["idElementoMedida"] + "' " +
                                  "ORDER BY FECHa_medida ";
                    daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
                    daPuntosM.Fill(dstPuntos, "TablaVolumenes");

                }

            }
        }

        /* DATOS ALIMENTACION*/
        /* buscamos los diferentes elementos de medida para cada punto */
        sentenciaSel = "SELECT distinct d.idelementoMedida " +
                        "FROM PVYCR_DatosAlimentacion D " +
                        "WHERE d.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' ";
        daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
        daPuntosM.Fill(dstPuntos, "TablaElementosE");
        for (i = 0; i < dstPuntos.Tables["TablaElementosE"].Rows.Count; i++)
        {
            sentenciaSel = "SELECT TOP 1 d.idElementoMedida idElementoMedidaE,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto,p.X_UTM,p.Y_UTM, " +
                           "d.Fecha_medida Fecha_medidaE,d.LecturaI,d.LecturaII,d.LecturaIII,d.Total_KWH,d.Total_Kvar, " +
                           "C.denominacionCauce " +
                           "FROM PVYCR_Puntos P, PVYCR_DatosAlimentacion D, PVYCR_Cauces C " +
                           "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and Convert(nvarchar,fecha_medida,103) = '" +
                           txtFechaDesde.Text + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +//DateTime.Parse(txtFechaDesde.Text)
                           "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI " +
                           " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosE"].Rows[i]["idElementoMedida"] + "' " +
                           "ORDER BY FECHA_medida DESC ";
            /*crearDataSets();*/
            daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
            /*Rellena Label tabla*/
            daPuntosM.Fill(dstPuntos, "TablaAlimentacion");

          
            DataRow[] drnE = dstPuntos.Tables["TablaAlimentacion"].Select("idelementomedidaE = '" + dstPuntos.Tables["TablaElementosE"].Rows[i]["idElementoMedida"] + "' ");
            if (drnE.Length == 0)
            {
                /*Si no tiene lectura para el día seleccionado buscamos la fecha más próxima que tenga lectura*/
                //Response.Write("cuenta: " + cuenta);
                sentenciaSel = "SELECT TOP 1 d.idElementoMedida idElementoMedidaE,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto,p.X_UTM,p.Y_UTM, " +
                                  "d.Fecha_medida Fecha_medidaE,d.LecturaI,d.LecturaII,d.LecturaIII,d.Total_KWH,d.Total_Kvar, " +
                                  "C.denominacionCauce " +
                                  "FROM PVYCR_Puntos P, PVYCR_DatosAlimentacion D, PVYCR_Cauces C " +
                                  "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and fecha_medida < '" +
                                  txtFechaDesde.Text + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +//DateTime.Parse(txtFechaDesde.Text)
                                  "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI " +
                                  " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosE"].Rows[i]["idElementoMedida"] + "' " +
                                  "ORDER BY FECHA_medida DESC ";
                daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
                daPuntosM.Fill(dstPuntos, "TablaAlimentacion");
                drnE = dstPuntos.Tables["TablaAlimentacion"].Select("idelementomedidaE = '" + dstPuntos.Tables["TablaElementosE"].Rows[i]["idElementoMedida"] + "' ");
                if (drnE.Length == 0)
                {
                    /*si no tiene lectura anterior a la fecha seleccionada, es decir si la fecha seleccionada es anterior a la primera 
                    lectura, mostraremos la primera lectura*/

                    sentenciaSel = "SELECT TOP 1 d.idElementoMedida idElementoMedidaE,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto,p.X_UTM,p.Y_UTM, " +
                                  "d.Fecha_medida Fecha_medidaE,LecturaI,d.LecturaII,d.LecturaIII,d.Total_KWH,d.Total_Kvar, " +
                                  "C.denominacionCauce " +
                                  "FROM PVYCR_Puntos P, PVYCR_DatosAlimentacion D, PVYCR_Cauces C " +
                                  "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                                  "Fecha_medida is not null and " +
                                  "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +//DateTime.Parse(txtFechaDesde.Text)
                                  "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI " +
                                  " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosE"].Rows[i]["idElementoMedida"] + "' " +
                                  "ORDER BY FECHa_medida ";
                    daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
                    daPuntosM.Fill(dstPuntos, "TablaAlimentacion");

                }

            }
        }
        //DATOS HOROMETROS
       sentenciaSel = "SELECT distinct d.idelementoMedida " +
                        "FROM PVYCR_DatosHorometros D " +
                        "WHERE d.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' ";
        daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
        daPuntosM.Fill(dstPuntos, "TablaElementosH");
        for (i = 0; i < dstPuntos.Tables["TablaElementosH"].Rows.Count; i++)
        {
           
            sentenciaSel = "SELECT TOP 1 d.idElementoMedida idElementoMedidaH,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto,p.X_UTM,p.Y_UTM, " +
                   "d.Fecha_medida Fecha_medidaH,d.horasintervalo, " +
                   "C.denominacionCauce " +
                   "FROM PVYCR_Puntos P, PVYCR_DatosHorometros D, PVYCR_Cauces C " +
                   "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and Convert(nvarchar,fecha_medida,103) = '" +
                   txtFechaDesde.Text + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +//DateTime.Parse(txtFechaDesde.Text)
                   "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI " +
                   " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosH"].Rows[i]["idElementoMedida"] + "' " +
                   "ORDER BY FECHA_medida DESC ";
            /*crearDataSets();*/
            daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
            /*Rellena Label tabla*/
            daPuntosM.Fill(dstPuntos, "TablaHorometros");

           
            DataRow[] drnH = dstPuntos.Tables["TablaHorometros"].Select("idelementomedidaH = '" + dstPuntos.Tables["TablaElementosH"].Rows[i]["idElementoMedida"] + "' ");

            if (drnH.Length == 0)
            {
                /*Si no tiene lectura para el día seleccionado buscamos la fecha más próxima que tenga lectura*/
                //Response.Write("cuenta: " + cuenta);
                sentenciaSel = "SELECT TOP 1 d.idElementoMedida idElementoMedidaH,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto,p.X_UTM,p.Y_UTM, " +
                                  "d.Fecha_medida Fecha_medidaH,d.horasintervalo, " +
                                  "C.denominacionCauce " +
                                  "FROM PVYCR_Puntos P, PVYCR_DatosHorometros D, PVYCR_Cauces C " +
                                  "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and fecha_medida < '" +
                                  txtFechaDesde.Text + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +//DateTime.Parse(txtFechaDesde.Text)
                                  "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI " +
                                " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosH"].Rows[i]["idElementoMedida"] + "' " +
                                  "ORDER BY FECHA_medida DESC ";
                daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
                daPuntosM.Fill(dstPuntos, "TablaHorometros");
                drnH = dstPuntos.Tables["TablaHorometros"].Select("idelementomedidaH = '" + dstPuntos.Tables["TablaElementosH"].Rows[i]["idElementoMedida"] + "' ");
                if (drnH.Length == 0)
                {
                    /*si no tiene lectura anterior a la fecha seleccionada, es decir si la fecha seleccionada es anterior a la primera 
                    lectura, mostraremos la primera lectura*/

                    sentenciaSel = "SELECT TOP 1 d.idElementoMedida idElementoMedidaH,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto,p.X_UTM,p.Y_UTM, " +
                                  "d.Fecha_medida Fecha_medidaH,d.horasintervalo, " +
                                  "C.denominacionCauce " +
                                  "FROM PVYCR_Puntos P, PVYCR_DatosHorometros D, PVYCR_Cauces C " +
                                  "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                                  "Fecha_medida is not null and " +
                                  "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +//DateTime.Parse(txtFechaDesde.Text)
                                  "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI " +
                                " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosH"].Rows[i]["idElementoMedida"] + "' " +
                                  "ORDER BY FECHa_medida ";
                    daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
                    daPuntosM.Fill(dstPuntos, "TablaHorometros");

                }

            }
        }


        //Hacemos el repeat para obtener los datos
        rptCabecera.DataSource = dstPuntos.Tables["TablaCabecera"];
        rptCabecera.DataBind();
        rptPuntosE.DataSource = dstPuntos.Tables["TablaAlimentacion"];
        rptPuntosE.DataBind();
        rptPuntosV.DataSource = dstPuntos.Tables["TablaVolumenes"];
        rptPuntosV.DataBind();
        rptPuntosH.DataSource = dstPuntos.Tables["TablaHorometros"];
        rptPuntosH.DataBind();
     
    }
    public void crearSelectAFecha()
    {
        dstPuntos.Clear();
        //PONEMOS LA CABECERA
        CrearDatasetAPto();
        //datos motores
        string sentenciaSel;
        int i;
        
        /* Buscamos los diferentes elementos de medida para el punto */
        sentenciaSel = "SELECT distinct d.idelementomedida " +
                        "FROM PVYCR_DatosAcequias D " +
                        "WHERE d.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' ";
        daPuntosM.SelectCommand = new SqlCommand(sentenciaSel, conexion);
        daPuntosM.Fill(dstPuntos, "TablaElementosAc");
 
        /* PARA CADA ELEMENTO DE MEDIDA OBTENEMOS EL REGISTRO QUE TENGA FECHA MAS ACTUAL */
        for (i = 0; i < dstPuntos.Tables["TablaElementosAc"].Rows.Count; i++)
        {
            sentenciaSel = "SELECT TOP 1 d.idelementoMedida,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto, " +
                "p.X_UTM,p.Y_UTM, d.Fecha_medida, C.denominacionCauce,Escala_M,Calado_M, " +
                "TipoObtencionCaudal,RegimenCurva,NumeroParada,Caudal_M3S,DUDA_CALIDAD,d.Observaciones " +
                "FROM PVYCR_Puntos P, PVYCR_DatosAcequias D, PVYCR_Cauces C " +
                "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and Convert(nvarchar,fecha_medida,103) = '" +
                txtFechaDesde.Text + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +
                "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI  " +
                " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosAc"].Rows[i]["idElementoMedida"] + "' " +
                "ORDER BY FECHA_medida DESC ";

            daPuntosA.SelectCommand = new SqlCommand(sentenciaSel, conexion);
            /*Rellena Label tabla*/
            daPuntosA.Fill(dstPuntos, "TablaAcequias");
            DataRow[] drnA = dstPuntos.Tables["TablaAcequias"].Select("idelementomedida = '" + dstPuntos.Tables["TablaElementosAc"].Rows[i]["idElementoMedida"] + "' ");
            if (drnA.Length == 0)
            {
                //Response.Write("cuenta: " + cuenta);
                sentenciaSel = "SELECT TOP 1 d.idelementoMedida,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto, " +
                "p.X_UTM,p.Y_UTM, d.Fecha_medida, C.denominacionCauce,Escala_M,Calado_M, " +
                "TipoObtencionCaudal,RegimenCurva,NumeroParada,Caudal_M3S,DUDA_CALIDAD,d.Observaciones " +
                "FROM PVYCR_Puntos P, PVYCR_DatosAcequias D, PVYCR_Cauces C " +
                "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and fecha_medida < '" +
                txtFechaDesde.Text + "' and p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +
                "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI  " +
                " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosAc"].Rows[i]["idElementoMedida"] + "' " +
                "ORDER BY FECHA_medida DESC ";
                daPuntosA.SelectCommand = new SqlCommand(sentenciaSel, conexion);
                daPuntosA.Fill(dstPuntos, "TablaAcequias");
                drnA = dstPuntos.Tables["TablaAcequias"].Select("idelementomedida = '" + dstPuntos.Tables["TablaElementosAc"].Rows[i]["idElementoMedida"] + "' ");
                if (drnA.Length == 0)
                {
                    sentenciaSel = "SELECT TOP 1 d.idelementoMedida,d.codigoPVYCR,p.CodigoCauce,p.DenominacionPunto, " +
                                   "p.X_UTM,p.Y_UTM, d.Fecha_medida, C.denominacionCauce,Escala_M,Calado_M, " +
                                   "TipoObtencionCaudal,RegimenCurva,NumeroParada,Caudal_M3S,DUDA_CALIDAD,d.Observaciones " +
                                   "FROM PVYCR_Puntos P, PVYCR_DatosAcequias D, PVYCR_Cauces C " +
                                   "WHERE p.CodigoPVYCR = '" + Request.QueryString["CodigoPVYCR"] + "' and " +
                                   "Fecha_medida is not null and " +
                                   "p.CodigoPVYCR = d.CodigoPVYCR COLLATE Modern_Spanish_CI_AS and " +
                                   "P.CodigoCauce = C.codigoCauce COLLATE Modern_Spanish_CI_AI  " +
                                   " and d.idelementomedida = '" + dstPuntos.Tables["TablaElementosAc"].Rows[i]["idElementoMedida"] + "' " +
                                   "ORDER BY FECHA_medida ";
                    daPuntosA.SelectCommand = new SqlCommand(sentenciaSel, conexion);
                    daPuntosA.Fill(dstPuntos, "TablaAcequias");
                }

            }

        }
        rptCabeceraA.DataSource = dstPuntos.Tables["TablaCabeceraA"];
        rptCabeceraA.DataBind();
        rptPuntosA.DataSource = dstPuntos.Tables["TablaAcequias"];
        rptPuntosA.DataBind();
    }

  protected void btnArbol_Click(object sender, EventArgs e)
  {
    string usuario;
    usuario = ObtenerUsuarioConexion();
    //Response.Redirect("http://localhost/sicasegura_1_3/SICAH/caucepuntMain.aspx?nodobusqueda=s&valor=" + Request.QueryString["CodigoPVYCR"] + ";P&usuario=" + usuario);
    Response.Redirect("https://sica.chsegura.es/sicasegura/SICAH/caucepuntMain.aspx?nodobusqueda=s&valor=" + Request.QueryString["CodigoPVYCR"] + ";P&usuario=" + usuario);
  }
  public string ObtenerUsuarioConexion()
  {
    string loginSica;
    comando.CommandText = "SELECT top 1 login FROM TSesionVisor, tusuarios where idusuario=id  and idsesion = '" + Session.SessionID + "vailavan'";
    //comando = new SqlCommand(sentenciaSel, conexion);
   comando.CommandType = CommandType.Text;
   comando.Connection = conexionVisor;
   if (conexionVisor.State == ConnectionState.Closed)
   {
     conexionVisor.Open();
   }
   loginSica = Convert.ToString(comando.ExecuteScalar());
    return loginSica;
  }
}
