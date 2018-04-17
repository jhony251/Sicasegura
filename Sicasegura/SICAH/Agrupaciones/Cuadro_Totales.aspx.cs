using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class SICAH_Agrupaciones_Cuadro_Totales : System.Web.UI.Page
{
    private int inscripcion;
    private string Descripcion;
    private string FF, FI;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (IsPostBack) { }
        else
        {
            FI  = Request.QueryString["FI"];
            FF = Request.QueryString["FF"];
            Session["FiltroFechaIni"] = FI;
            Session["FiltroFechaFin"] = FF;
            try 
            { 
                inscripcion = Convert.ToInt32(Request.QueryString["Inscripcion"]);
                if (inscripcion == 0) { inscripcion = Convert.ToInt32(TXT_Inscripcion.Text); } else { TXT_Inscripcion.Text = inscripcion.ToString(); }    
            }
            catch 
            {
                Descripcion  = Request.QueryString["Inscripcion"];
                if (Descripcion == "") { Descripcion = TXT_Inscripcion.Text; } else { TXT_Inscripcion.Text = Descripcion ; }
            }
            
            if (FI == "" || FI==null) { TXT_FI.Text = "30/09/2015 23:59:00"; FI = TXT_FI.Text; } else { TXT_FI.Text = FI; };
            if (FF == "" || FF == null) { TXT_FF.Text = DateTime.Now.ToShortDateString(); FF = TXT_FF.Text; } else { TXT_FF.Text = FF; };
            Calendar1.SelectedDate = DateTime.Parse(TXT_FI.Text);
            Calendar1.VisibleDate = DateTime.Parse(TXT_FI.Text);
            Calendar2.SelectedDate = DateTime.Parse(TXT_FF.Text);
            calculos();
            GeneraGrafico();
            if (inscripcion == 0 && Descripcion != "")
            {
                TXT_Concesion.Visible = false;
                LBL_Concesion.Visible = false;
                TXT_Concesion_temporal.Visible = false;
                LBL_Concesion_modificacion.Visible = false; 
                TXT_Concesion_total.Visible = false;
                LBL_Concesion_total.Visible = false;
                TXT_Consumido_percent.Visible = false;
                LBL_Consumido_percent.Visible = false;
                TXT_Consumido_vol.BackColor = System.Drawing.Color.White;
            }
        }
        
    }
    private int obtener_inscripcion_desde_descripcion(string descripcion)
    {

        return 0;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("cuadro_totales.aspx?FI=" + TXT_FI.Text + "&FF=" + TXT_FF.Text + "&Inscripcion=" + TXT_Inscripcion.Text);
    }
    private void calculos()
    {
        SicaSegura.SICA_Agrupacion agrupacion;
        if (inscripcion != 0) { agrupacion = new SicaSegura.SICA_Agrupacion(inscripcion); }
        else { agrupacion = new SicaSegura.SICA_Agrupacion(Descripcion); }
        TXT_Concesion.Text = agrupacion.Get_ConcesionInscrita().ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR"));
        TXT_Concesion_temporal.Text = agrupacion.Get_ConcesionTemporal().ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR"));
        TXT_Concesion_total.Text = agrupacion.Get_ConcesionModificada().ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR"));
        
        TXT_Consumido_vol.Text = agrupacion.CalculoConsumido(agrupacion.Get_NumeroInscripcion(), DateTime.Parse(TXT_FI.Text),DateTime.Parse(TXT_FF.Text)).ToString();
        TXT_Consumido_vol.Text = Convert.ToDouble(TXT_Consumido_vol.Text).ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR"));

        TXT_Consumido_percent.Text = ((Convert.ToDouble(TXT_Consumido_vol.Text) * 100) / agrupacion.Get_ConcesionModificada()).ToString();
        if (Convert.ToDouble(TXT_Consumido_percent.Text) < 100)
        {
            TXT_Consumido_percent.BackColor = System.Drawing.Color.Green;
            TXT_Consumido_vol.BackColor = System.Drawing.Color.Green;
        }
        else
        {
            TXT_Consumido_percent.BackColor = System.Drawing.Color.Red;
            TXT_Consumido_vol.BackColor = System.Drawing.Color.Red;
        }
    }
    private void GeneraGrafico()
    {
        
        SicaSegura.SICA_Agrupacion Agrupacion;
        if (inscripcion != 0) { Agrupacion = new SicaSegura.SICA_Agrupacion(inscripcion); }
        else { Agrupacion = new SicaSegura.SICA_Agrupacion(Descripcion); }
        string str,str1, str2, consumidoEnHm, acumiladoEnHm = "";
        DateTime FechaIni, FechaFin;
        
        FechaIni=DateTime.Parse(FI);
        FechaFin=DateTime.Parse(FF);
        
        str = "var Values=[";
        str1 = "var Values1=[";
        str2 = "var Values2=[";
        acumiladoEnHm = "0";
        consumidoEnHm = "0";
        while(FechaIni<FechaFin)
        {
            
            
            if (FechaIni.AddMonths(1) > FechaFin)
            {
                consumidoEnHm = Convert.ToString(Agrupacion.CalculoConsumido(Agrupacion.Get_NumeroInscripcion(), FechaIni, FechaFin) / 1000000).Replace(",", ".");
                acumiladoEnHm = Convert.ToString(Agrupacion.CalculoConsumido(Agrupacion.Get_NumeroInscripcion(), DateTime.Parse(FI), FechaFin) / 1000000).Replace(",", ".");
                str = str + "[Date.UTC(" + FechaIni.Year + ", " + Convert.ToString(Convert.ToInt16(FechaIni.Month) - 1) + ", " + FechaIni.Day + ", " + "00" + ", " + "01" + ", " + "01" + "), " + consumidoEnHm + "],";
                str1 = str1 + "[Date.UTC(" + FechaIni.Year + ", " + Convert.ToString(Convert.ToInt16(FechaIni.Month) - 1) + ", " + FechaIni.Day + ", " + "00" + ", " + "01" + ", " + "01" + "), " + acumiladoEnHm + "],"; 
                str2 = str2 + "[Date.UTC(" + FechaIni.Year + ", " + Convert.ToString(Convert.ToInt16(FechaIni.Month) - 1) + ", " + FechaIni.Day + ", " + "00" + ", " + "01" + ", " + "01" + "), " + Convert.ToString(Agrupacion.Get_ConcesionModificada() / 1000000).Replace(",", ".") + "],";
                FechaIni = FechaIni.AddMonths(1);
            }
            else
            {
                DateTime FFTemp = new DateTime();
                FFTemp = FechaIni.AddMonths(1);
                FFTemp = DateTime.Parse("01/" + FFTemp.Month.ToString() + "/" + FFTemp.Year.ToString());
                consumidoEnHm = Convert.ToString(Agrupacion.CalculoConsumido(Agrupacion.Get_NumeroInscripcion(), FechaIni, FFTemp) / 1000000).Replace(",", ".");
                acumiladoEnHm = Convert.ToString(Agrupacion.CalculoConsumido(Agrupacion.Get_NumeroInscripcion(), DateTime.Parse(FI), FFTemp) / 1000000).Replace(",", ".");
                str = str + "[Date.UTC(" + FechaIni.Year + ", " + Convert.ToString(Convert.ToInt16(FechaIni.Month) - 1) + ", " + FechaIni.Day + ", " + "00" + ", " + "01" + ", " + "01" + "), " + consumidoEnHm + "],";
                str1 = str1 + "[Date.UTC(" + FechaIni.Year + ", " + Convert.ToString(Convert.ToInt16(FechaIni.Month) - 1) + ", " + FechaIni.Day + ", " + "00" + ", " + "01" + ", " + "01" + "), " + acumiladoEnHm + "],"; 
                str2 = str2 + "[Date.UTC(" + FechaIni.Year + ", " + Convert.ToString(Convert.ToInt16(FechaIni.Month) - 1) + ", " + FechaIni.Day + ", " + "00" + ", " + "01" + ", " + "01" + "), " + Convert.ToString(Agrupacion.Get_ConcesionModificada() / 1000000).Replace(",", ".") + "],";
                FechaIni = FFTemp;
            }
        }
        
        str = str.Substring(0, str.Length - 1);
        str = str + "];";

        str1 = str1.Substring(0, str1.Length - 1);
        str1 = str1 + "];";

        str2 = str2.Substring(0, str2.Length - 1);
        str2 = str2 + "];";


        string cstext2="";
        cstext2 +="<script type=\"text/javascript\">";
        cstext2 += str + str1 + str2;
        cstext2 +="</script>";
        
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "strData", cstext2.ToString(), false);
    }

    protected void TXT_FI_Click(object sender, EventArgs e)
    {
        Calendar1.Visible = true;
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        Calendar1.Visible = false;
        TXT_FI.Text = Calendar1.SelectedDate.ToShortDateString();
    }
    protected void TXT_FF_Click(object sender, EventArgs e)
    {
        Calendar2.Visible = true;
    }
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        Calendar2.Visible = false;
        TXT_FF.Text = Calendar2.SelectedDate.ToShortDateString();
    }
}
