using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

public partial class SICAH_Meteo_getFileWhetherLinkFormat : System.Web.UI.Page
{
    SicaSegura.SICA_DB db = new SicaSegura.SICA_DB();
    string estacion;
    DateTime currentdate;
    string headers = "                Temp	Hi	Low	Out	Dew	Wind	Wind	Wind	Hi	Hi	Wind	Heat	THW			Rain	Heat	Cool	In 	In	In 	In 	Wind	Wind	ISS 	Arc."+
    "\r\nDate	Time	Out	Temp	Temp	Hum	Pt.	Speed	Dir	Run	Speed	Dir	Chill	Index	Index	Bar  	Rain	Rate	D-D 	D-D 	Temp	Hum	Dew	Heat	Samp	Tx 	Recept	Int.\r\n";
    protected void Page_Load(object sender, EventArgs e)
    {
        estacion = "";
        if (Request.QueryString["Station"] != null)
        {
            estacion = Request.QueryString["Station"].ToString();
            currentdate = DateTime.Now;
            string SQL = "SELECT * FROM SICA_Meteo_DatosEstaciones " +
                " WHERE fecha>'01/" + currentdate.Month + "/" + currentdate.Year +"' "+
                " AND (DatePart(MINUTE,fecha)=30 OR DatePart(MINUTE,fecha)=00) " +
                " AND fecha<'01/" + currentdate.AddMonths(1).Month + "/" + currentdate.AddMonths(1).Year + "' "+
                " AND codigoestacion like '" + estacion.Trim() + "%' order by fecha asc";
            DataTable dt =  db.GetDataDBSICA(SQL);
            foreach (DataRow dr in dt.Rows)
            {
                for(int i=3; i<dr.ItemArray.Length;i++)
                {
                    if (i == 3)
                    {
                        string fecha = dr[i].ToString().Split(' ')[0];
                        string hora = dr[i].ToString().Split(' ')[1];
                        headers += fecha.Split('/')[0].ToString() + "/" + fecha.Split('/')[1].ToString() + "/" + fecha.Split('/')[2].Substring(2).ToString() + "\t";
                        if (Convert.ToInt16(hora.Split(':')[0].ToString()) > 12)
                        {
                            headers += (Convert.ToInt16(hora.Split(':')[0].ToString())-12).ToString() + ":" + hora.Split(':')[1].ToString() + " p\t";
                        }
                        else
                        {
                            headers += hora.Split(':')[0].ToString() + ":" + hora.Split(':')[1].ToString() + " a\t";
                        }
                    }
                    else { headers += dr[i].ToString() + "\t"; }
                }
                headers += "\r\n";
            }

            StringWriter oStringWriter = new StringWriter();
            oStringWriter.WriteLine("Line 1");
            Response.ContentType = "text/plain";

            Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("members-{0}.txt", string.Format("{0:ddMMyyyy}", DateTime.Today)));
            Response.Clear();

            using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
            {
                writer.Write(headers);
            }
            Response.End();

        }

        

    }
}
