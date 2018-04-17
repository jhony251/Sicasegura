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
        SICA.CAS.CASP casp = new SICA.CAS.CASP("https://cas.chsegura.es:4443/cas/", this.Page);
        if (casp.Login())
        {
            try
            {
                string username = casp.ServiceValidate(); //or casp.Validate() for CAS1
                Response.Write(username);
                Session.Add("Trusteduser", "SI");
                Session.Add("Username", username);
                Session.Add("InicioSesion", DateTime.Now.ToString());
                Response.Redirect("default.aspx");
                //do whatever with username
            }
            catch (SICA.CAS.CASP.ValidateException ex)
            {
                //try again, something was messed up
                casp.Login(true);
                Session["Trusteduser"] = "NO";
            }

        }
        else
        {
            Response.Write("Disculpe las molestias.");
            Response.Write("<h1>El servidor de control de accesos no está disponible<br>Inténtelo de nuevo más tarde.</h1>");
            
        }
    }
}
