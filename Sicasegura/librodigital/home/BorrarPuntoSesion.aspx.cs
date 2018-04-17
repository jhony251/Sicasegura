using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class librodigital_home_BorrarPuntoSesion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["codigoPVYCR-EM_activo"] = "";
        Response.Redirect("lecturas.aspx");
    }
}
