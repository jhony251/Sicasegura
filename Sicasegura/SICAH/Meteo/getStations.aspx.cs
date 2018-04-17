using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SICAH_Meteo_getStations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SicaSegura.SICA_DB db = new SicaSegura.SICA_DB();
        DataTable dt = db.GetDataDBSICA("SELECT Distinct codigoEstacion from SICA_Meteo_DatosEstaciones");
        string respuesta ="";
        for (int i = 0; i<dt.Rows.Count ; i++)
        {
            respuesta += dt.Rows[i].ItemArray.GetValue(0).ToString().Trim();
            if (dt.Rows.Count-i>1){respuesta+="#";}
        }
        Response.Write(respuesta);
    }
}
