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
        if ((Session== null))
        {
            Response.Redirect("http://sicasegura/inicio");
        }
        string ElTiempo = "<div id=\"c_7ff7ee2ede686afe3f4f6a24e88ac165\" class=\"normal\"><h2 style=\"color: #ffffff; margin: 0 0 3px; padding: 2px; font: bold 13px/1.2 Verdana; text-align: center;\">Tiempo Murcia</h2></div><script type=\"text/javascript\" src=\"http://www.eltiempo.es/widget/widget_loader/7ff7ee2ede686afe3f4f6a24e88ac165\"></script>";

        //string nombreusuario = Request.QueryString["usuar"].ToString();
        Session.Add("Nombre", Request.QueryString["usuar"].ToString());
        Session.Add("Fecha", DateTime.Now.ToShortTimeString());
        HTMLTablaEmbalses.HTMLTablaEmbalses HTE = new HTMLTablaEmbalses.HTMLTablaEmbalses();
        Embalses.Text = HTE.GetHTMLCodeEnlinea();
        Infousuario.Text += "<table border=\"0\" width=\"100%\"><tr><td width=\"100px\">";
        Infousuario.Text += "<img src=\"images/" + Session["Nombre"] + ".gif\" width=\"100px\">";
        Infousuario.Text += "</td><td valign=\"bottom\">";
        Infousuario.Text += "Nombre de Usuario: " + Session["Nombre"] + "<br>";
        Infousuario.Text += "Hora de inicio de sesión: " + Session["Fecha"] + "<br>";
        Infousuario.Text += "Previsión de cierre sesión: " + DateTime.Parse(Session["Fecha"].ToString()).AddHours(2).ToShortTimeString() + "<br>";
        Infousuario.Text += "<a href=\"logout.aspx\"> Cerrar la sesión activa</a>";
        Infousuario.Text += "</td><td align=\"Right\">" + ElTiempo + "</td></tr></table>";


        LinkSicaSegura.Text += "<a href=\"login.aspx?Usuar=" + Request.QueryString["usuar"].ToString() + "&app=SICA\">Acceder</a>";
        LinkEsquemasSegura.Text += "<a href=\"login.aspx?Usuar=" + Request.QueryString["usuar"].ToString() + "&app=SICA_ESQUEMAS\">Acceder</a>";
       
    }
} 
