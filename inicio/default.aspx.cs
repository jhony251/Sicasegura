using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            try
            {
                string ValidacionSegura = Session["Trusteduser"].ToString();
                string Usuario = Session["Username"].ToString();
                if ((ValidacionSegura == "SI"))
                {
                    string ElTiempo = "";//<div id=\"c_7ff7ee2ede686afe3f4f6a24e88ac165\" class=\"normal\"><h2 style=\"color: #ffffff; margin: 0 0 3px; padding: 2px; font: bold 13px/1.2 Verdana; text-align: center;\">Tiempo Murcia</h2></div><script type=\"text/javascript\" src=\"http://www.eltiempo.es/widget/widget_loader/7ff7ee2ede686afe3f4f6a24e88ac165\"></script>";
                    Session.Add("Nombre", Usuario);
                    Session.Add("Fecha", DateTime.Now.ToShortTimeString());
                    HTMLTablaEmbalses.HTMLTablaEmbalses HTE = new HTMLTablaEmbalses.HTMLTablaEmbalses();
                    Embalses.Text = HTE.GetHTMLCodeEnlinea();
                    Infousuario.Text += "<table border=\"0\" width=\"100%\"><tr><td width=\"100px\">";
                    Infousuario.Text += "<img src=\"images/sicaadmin.gif\" width=\"100px\">";
                    Infousuario.Text += "</td><td valign=\"bottom\">";
                    Infousuario.Text += "Nombre de Usuario: " + Usuario + "<br>";
                    //Infousuario.Text += "Hora de inicio de sesión: " + Session["InicioSesion"] + "<br>";
                    //Infousuario.Text += "Previsión de cierre sesión: " + DateTime.Parse(Session["InicioSesion"].ToString()).AddHours(2).ToShortTimeString() + "<br>";
                    Infousuario.Text += "<a href=\"logout.aspx\"> Cerrar la sesión activa</a>";
                    Infousuario.Text += "</td><td align=\"Right\">" + ElTiempo + "</td></tr></table>";


                    LinkSicaSegura.Text += "<a target=\"##@··\" href=\"login.aspx?app=SICA\">Acceder</a>";
                    LinkEsquemasSegura.Text += "<a target=\"##@#··\" href=\"login.aspx?app=SICA_ESQUEMAS\">Acceder</a>";
                    LinkInformesSegura.Text += "<a target=\"##@##··\" href=\"login.aspx?app=SICA_INFORMES\">Acceder</a>";
                    LIT_LinkSCADASegura.Text += "<a target=\"##@###··\" href=\"login.aspx?app=SICA_SCADA\">Acceder</a>";
                }
            }
            catch (Exception ee)
            {
                Response.Write("Debe volver a realizar el proceso de validación");
                Response.Redirect("index.aspx");
                Response.Write(ee.Message);
            }

        
    }
} 
