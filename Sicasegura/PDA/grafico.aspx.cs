using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PDA_grafico : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Configura_Chart();
        CargarDT();
    }
    void Configura_Chart()
    {
        Chart1.Series.Add("Caudales (m3/s)");
        Chart1.Series.Add("Niveles (m)");
        Chart1.Series.Add("Velocidades (m/s)");
        Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
        Chart1.Series[1].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
        Chart1.Series[2].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
        
        Chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.AddHours(-12).ToOADate();
        Chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.ToOADate();
        Chart1.Series[0].MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Circle;
        Chart1.Series[1].MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Circle;
        Chart1.Series[2].MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Circle;
        Chart1.Series[0].MarkerSize = 6;
        Chart1.Series[1].MarkerSize = 6;
        Chart1.Series[2].MarkerSize = 6;
        Chart1.Series[0].IsValueShownAsLabel = true;
        Chart1.Series[1].IsValueShownAsLabel = true;
        Chart1.Series[2].IsValueShownAsLabel = true;
        Chart1.Legends[0].Docking = System.Web.UI.DataVisualization.Charting.Docking.Bottom;
        Chart1.Series[1].LabelBackColor = System.Drawing.Color.Bisque;
        Chart1.Series[0].LabelBackColor = System.Drawing.Color.CadetBlue;
        Chart1.Series[0].LabelForeColor = System.Drawing.Color.White;
        Chart1.Series[2].LabelBackColor = System.Drawing.Color.Tomato;
        System.Drawing.Font fuente = new System.Drawing.Font("Verdana",12, System.Drawing.FontStyle.Regular);
        Chart1.Series[0].Font = fuente; Chart1.Series[1].Font = fuente; Chart1.Series[2].Font = fuente;

        Chart1.Series[0].BorderWidth = 3;
        Chart1.Series[0].ShadowOffset = 3;
        Chart1.Series[1].BorderWidth = 3;
        Chart1.Series[1].ShadowOffset = 3;
        Chart1.Series[2].BorderWidth = 3;
        Chart1.Series[2].ShadowOffset = 3;

    }
    void CargarDT()
    {
        string SQL;
        DataSet ds;
        string punto = Request.QueryString.Get("codigo");
        if (punto == null)
        {
            punto = "VA002P30";
        }
        else
        {
            SQL = "SELECT CodigoPVYCR FROM PVYCR_AccesosTopkapi WHERE Denominación like '%" + punto + "%'";
            ds = GetDataSap5(SQL);
            punto = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        int i=0;
        SQL = "SELECT * FROM PVYCR_DatosAcequias_NIVUS WHERE CodigoPVYCR like '" + punto + "' AND Fecha BETWEEN '" + DateTime.Now.AddHours(-12).ToString() + "' AND '" + DateTime.Now.ToString() + "'";
        ds = GetDataSap5(SQL);
        if (ds.Tables[0].Rows.Count!=0)
        {
            while(i<ds.Tables[0].Rows.Count)
            {
                Chart1.Series["Caudales (m3/s)"].XValueType = System.Web.UI.DataVisualization.Charting.ChartValueType.Time;
                Chart1.Series["Caudales (m3/s)"].Points.AddXY(DateTime.Parse(ds.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()).ToOADate(), Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()));
                Chart1.Series["Niveles (m)"].XValueType = System.Web.UI.DataVisualization.Charting.ChartValueType.DateTime;
                Chart1.Series["Niveles (m)"].Points.AddXY(DateTime.Parse(ds.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()).ToOADate(), Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray.GetValue(3).ToString()));
                Chart1.Series["Velocidades (m/s)"].XValueType = System.Web.UI.DataVisualization.Charting.ChartValueType.DateTime;
                Chart1.Series["Velocidades (m/s)"].Points.AddXY(DateTime.Parse(ds.Tables[0].Rows[i].ItemArray.GetValue(1).ToString()).ToOADate(), Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray.GetValue(5).ToString()));
                i++;
            }
        }
    }
    DataSet GetDataSap5(String queryString)
    {
        // Retrieve the connection string stored in the Web.config file.
        //String connectionString = ConfigurationManager.ConnectionStrings["NorthWindConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Server=172.17.8.207\\sap5sql2005;  database=gfluvial; Password=sa9ia;Persist Security Info=True;User ID=gfluvial");
        //System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=SITSQL2005;Initial Catalog=Telemedida;Persist Security Info=True;User ID=sa;Password=");
        System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
        // Fill the DataSet.
        adapter.Fill(ds);
        return ds;
    }
}
