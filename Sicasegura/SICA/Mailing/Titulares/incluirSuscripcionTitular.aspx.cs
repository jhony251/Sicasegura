using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICA_Mailing_incluirSuscripcionTitular : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["ID"].ToString();
        string tipo = Request.QueryString["tipo"].ToString();
        string Email = Request.QueryString["email"].ToString();
        string periodo = Request.QueryString["periodo"].ToString();
        string nombre = Request.QueryString["nombre"].ToString();
        string activa = Request.QueryString["activa"].ToString();
        string Observaciones = Request.QueryString["observaciones"].ToString();

        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        switch (tipo) 
        {
            case "ISM":
                {
                    string ISM = Request.QueryString["ISM"].ToString();
                    string Raacs = Request.QueryString["Raacs"].ToString();
                    string SQL = "INSERT  INTO  SICA_Mailing_Direcciones( IDRaacs, EXP_ISM,  CorreElectronicoNotificaciones, NombreDestinatario, Periodicidad, Activa, Observaciones)VALUES ('" + Raacs + "','" + ISM + "','" + Email + "','" + nombre + "','" + periodo + "','" + activa + "','" + Observaciones + "')";
                    break;
                }
            case "Raacs":
                {
                    string ISM = Request.QueryString["ISM"].ToString();
                    string Raacs = Request.QueryString["Raacs"].ToString();
                    string SQL = "INSERT  INTO  SICA_Mailing_Direcciones( IDRaacs, EXP_ISM,  CorreElectronicoNotificaciones, NombreDestinatario, Periodicidad, Activa, Observaciones)VALUES ('" + Raacs + "','" + ISM + "','" + Email + "','" + nombre + "','" + periodo + "','" + activa + "','" + Observaciones + "')";
                    break;
                }
            case "SICA":
                {
                    string ISM = Request.QueryString["ISM"].ToString();
                    string Raacs = Request.QueryString["Raacs"].ToString();
                    string SICA = Request.QueryString["SICA"].ToString();
                    string SQL = "INSERT  INTO  SICA_Mailing_Direcciones( IDRaacs, EXP_ISM, codigoSICA, CorreElectronicoNotificaciones, NombreDestinatario, Periodicidad, Activa, Observaciones)VALUES ('" + Raacs + "','" + ISM + "','" + SICA + "','" + Email + "','" + nombre + "','" + periodo + "','" + activa + "','" + Observaciones + "')";
                    break;
                }
        }
        DB.GetDataDBSICA_test("delete from SICA_Mailing_direcciones where ID =" + id.ToString()); 

    }
}
