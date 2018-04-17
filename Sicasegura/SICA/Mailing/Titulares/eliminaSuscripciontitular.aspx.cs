using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICA_Mailing_eliminaSuscripciontitular : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try 
        { 
            string id = Request.QueryString["ID"].ToString();
            SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
            DB.GetDataDBSICA_test("delete from SICA_Mailing_direcciones where ID =" + id.ToString());        
        }
        catch (Exception ee) { }


    }
}
