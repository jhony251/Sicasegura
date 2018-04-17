using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICA_Gestion_Visitas_Campo_index : System.Web.UI.Page
{
    
    private SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
    private String EXP_ISM = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        Session["consultando"] = "true";
        if (!IsPostBack)
        {
            SicaSegura.SICA_Interfaz.SICA_Sica InterfazSica = new SicaSegura.SICA_Interfaz.SICA_Sica();
            
            //Session["tipo"] = "";
            //Session["modo"] = "";
            //string test = (string)Session["test"];
            //HttpContext.Current.Session["test"] = "test"; 
            string modo = "";
            try { Session["modo"] = Request.QueryString["modo"].ToString(); modo = (string)Session["modo"]; } catch { modo = (string)Session["modo"]; }

            if(modo=="todas" || modo=="")
            {
                Session["ISM"] = "";
                Session["tipo"] = "";
            }

            string ISM = "";
            try { Session["ISM"] = Request.QueryString["ISM"].ToString(); ISM = (string)Session["ISM"]; } catch { ISM = (string)Session["ISM"]; }
            string tipo = "";
            try { Session["tipo"] =  Request.QueryString["tipo"].ToString(); tipo = (string)Session["tipo"]; } catch { tipo = (string)Session["tipo"]; }
            string ACTIVIDAD = "";
            try { Session["ACTIVIDAD"] = Request.QueryString["ACTIVIDAD"].ToString(); ACTIVIDAD = (string)Session["ACTIVIDAD"]; } catch { ACTIVIDAD = (string)Session["ACTIVIDAD"]; }
            String menu = "-#-";
            HTML_Links_Sup_Izq.Text = InterfazSica.Get_Enlaces_Sup_Izq(menu.Split('#'));
            HTML_Subcabecera_Logos.Text = InterfazSica.Get_Cabecera();
            //HTML_Menu_Navegacion.Text = InterfazSica.Get_Menu_Navegacion_Completo("cuatro");
            //Cargalistado_ISM();
            HTML_Pie_Logo_Corporativo.Text = InterfazSica.Get_pie_logo_corporativo();
            HTML_Pie_pagina.Text = InterfazSica.Get_pie_pagina();
            HTML_Link_visitas_pendientes.Text = "<li><a href=\"index.aspx?tipo=estado&modo=pendientes\">Pendientes</a></li>";
            HTML_Link_visitas_finalizadas.Text = "<li><a href=\"index.aspx?tipo=estado&modo=finalizadas\">Finalizadas</a></li><li><a href=\"index.aspx?tipo=estado&modo=todas\">Todas</a></li>";
            HTML_Link_Primeras_En_Finalizar_primero.Text = "<li><a href=\"index.aspx?tipo=fecha&modo=proximas\">A punto de vencer</a></li>";
            HTML_Link_Ultimas_En_Finalizar_priemro.Text = "<li><a href=\"index.aspx?tipo=fecha&modo=lejanas\">Últimas en vencer</a></li>";
            HTML_Link_Sin_fecha_asignada.Text = "<li><a href=\"index.aspx?tipo=fecha&modo=nula\">Sin fecha asignada</a></li>";

            cargar_Lista_ISM_Nuevo();
            LIT_Filtro_Activo.Text = "<h3>Visitas filtradas por " + modo + "<h3>";
            cargaDGV_Visitas();

           
        }
    }
    private void cargaDGV_Visitas()
    {
        if ((string)Session["tipo"] == "estado")
        {

            if ((string)Session["modo"] == "finalizadas") { DGV_Visitas.DataSource = DB.GetDataDBSICA("SELECT * from SICA_visitas_a_campo where fecha_visita<'" + DateTime.Now + "' order by fechaid desc"); }
            else if ((string)Session["modo"] == "pendientes") { DGV_Visitas.DataSource = DB.GetDataDBSICA("SELECT * from SICA_visitas_a_campo where fecha_visita>'" + DateTime.Now + "' order by fechaid desc"); }
            else if ((string)Session["modo"] == "todas" || (string)Session["modo"] == "") { DGV_Visitas.DataSource = DB.GetDataDBSICA("SELECT * from SICA_visitas_a_campo order by fechaid desc"); }
        }
        else if ((string)Session["tipo"] == "fecha")
        {
            if ((string)Session["modo"] == "proximas") { DGV_Visitas.DataSource = DB.GetDataDBSICA("SELECT * from SICA_visitas_a_campo  where fecha_visita is not null  and  fecha_visita > '" + DateTime.Now.ToShortDateString() + "' order by fecha_visita asc"); }
            else if ((string)Session["modo"] == "lejanas") { DGV_Visitas.DataSource = DB.GetDataDBSICA("SELECT * from SICA_visitas_a_campo where fecha_visita is not null and  fecha_visita > '" + DateTime.Now.ToShortDateString() + "' order by fecha_visita desc"); }
            else if ((string)Session["modo"] == "nula") { DGV_Visitas.DataSource = DB.GetDataDBSICA("SELECT * from SICA_visitas_a_campo where fecha_visita is null order by fechaid desc"); }
            else { DGV_Visitas.DataSource = DB.GetDataDBSICA("SELECT * from SICA_visitas_a_campo where  fecha_visita > '" + DateTime.Now.ToShortDateString() + "' order by fechaid desc"); }
        }
        else if ((string)Session["tipo"] == "ISM")
        {
            DGV_Visitas.DataSource = DB.GetDataDBSICA("SELECT * from SICA_visitas_a_campo  where EXP_ISM like '" + (string)Session["ISM"].ToString() + "' order by fecha_visita asc");
        }
        else if ((string)Session["tipo"] == "ACTIVIDAD")
        {
            string Valor = "";
            if ((string)Session["ACTIVIDAD"] == "Caracterización") { Valor = "0"; }
            if ((string)Session["ACTIVIDAD"] == "Verificación") { Valor = "1"; }
            if ((string)Session["ACTIVIDAD"] == "Valid. Telemedida") { Valor = "2"; }
            if ((string)Session["ACTIVIDAD"] == "Acta Confrontación") { Valor = "3"; }
            if ((string)Session["ACTIVIDAD"] == "Otros") { Valor = "4"; }
            DGV_Visitas.DataSource = DB.GetDataDBSICA("SELECT * from SICA_visitas_a_campo  where tipo like '" + Valor + "' order by fecha_visita asc");
        }
        else
        {
            DGV_Visitas.DataSource = DB.GetDataDBSICA("SELECT * from SICA_visitas_a_campo order by fechaid desc");
        }
        
        
        DGV_Visitas.DataBind();
        PNL_nueva_visita.Visible = false;
        PNL_Update.Visible = false;
        for (int i = 0; i < DGV_Visitas.Rows.Count; i++)
        {
            if (DGV_Visitas.Rows[i].Cells[3].Text.ToString() == "0") { DGV_Visitas.Rows[i].Cells[3].Text = "Caracterización"; }
            if (DGV_Visitas.Rows[i].Cells[3].Text.ToString() == "1") { DGV_Visitas.Rows[i].Cells[3].Text = "Verificación"; }
            if (DGV_Visitas.Rows[i].Cells[3].Text.ToString() == "2") { DGV_Visitas.Rows[i].Cells[3].Text = "Valid. Telemedida"; }
            if (DGV_Visitas.Rows[i].Cells[3].Text.ToString() == "3") { DGV_Visitas.Rows[i].Cells[3].Text = "Acta Confrontación"; }
            if (DGV_Visitas.Rows[i].Cells[3].Text.ToString() == "4") { DGV_Visitas.Rows[i].Cells[3].Text = "Otros"; }
        }

    }

    private void cargar_Lista_ISM_Nuevo()
    {
        DDL_EXP_ISM.DataSource = DB.GetDataDBSICA("SELECT distinct EXP_ISM from SICA_Seguimiento_Expedientes_ISM order by EXP_ISM");
        DDL_EXP_ISM.DataValueField = "EXP_ISM";
        DDL_EXP_ISM.DataTextField = "EXP_ISM";
        DDL_EXP_ISM.DataBind();
    }

    private string DDL_EXP_ISM_Format_value(string ISM)
    {
        if (ISM.IndexOf("ISM") >= 0) { ISM = ISM.Substring(ISM.IndexOf("ISM") + 4); }
        if (ISM.IndexOf(',') > 0) { ISM = ISM.Split(',')[0].ToString(); }


        String ISM1, ISM2 = "";
        ISM1 = ISM.Split('/')[0].ToString();
        ISM2 = ISM.Split('/')[1].ToString();

        if ((Int32.Parse(ISM1) < 100) && (Int32.Parse(ISM1) < 10)) { ISM1 = "00" + Int16.Parse(ISM1.ToString()).ToString(); }
        if ((Int32.Parse(ISM1) < 100) && (Int32.Parse(ISM1) > 10)) { ISM1 = "0" + Int16.Parse(ISM1.ToString()).ToString(); }

        return ISM1 + "/" + ISM2;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in CBO_TIPO.Items)
        {
            string fecha = "";
            if (Calendar1.SelectedDate.ToShortDateString() != "01/01/0001")
            { fecha = Calendar1.SelectedDate.ToShortDateString(); }
            if (item.Selected)
            {
                EXP_ISM= DDL_EXP_ISM_Format_value(DDL_EXP_ISM.SelectedValue.ToString());
                string   SQL = "INSERT";
                if (Calendar1.SelectedDate.Year > 2017)
                {
                    SQL += " INTO SICA_Visitas_a_campo(FechaID, EXP_ISM, Tipo, Contacto, Fecha_visita, Personal_SICA, Personal_CHS, Observaciones)";
                    SQL += " VALUES('" + DateTime.Now.ToString() + "','" + EXP_ISM + "'," + item.Value + ",'" + TXT_Contacto.Text.ToString() + "','" + fecha + "','" + TXT_PersonalUTE.Text.ToString() + "','" + TXT_PersonalCHS.Text.ToString() + "','" + TXT_Observaciones.Text.ToString() + "')";
                }
                else
                {
                    SQL += " INTO SICA_Visitas_a_campo(FechaID, EXP_ISM, Tipo, Contacto,  Personal_SICA, Personal_CHS, Observaciones)";
                    SQL += " VALUES('" + DateTime.Now.ToString() + "','" + EXP_ISM + "'," + item.Value + ",'" + TXT_Contacto.Text.ToString() + "','" + TXT_PersonalUTE.Text.ToString() + "','" + TXT_PersonalCHS.Text.ToString() + "','" + TXT_Observaciones.Text.ToString() + "')";
                }
                DB.GetDataDBSICA(SQL);
                string [] direcciones = "rosa.palomares@chsegura.es;jcarrillo@utemursiya.es;joseantonio.merono@fcc.es;jaraujo@utemursiya.es;agarcia@utemursiya.es;vmorales@utemursiya.es;raquel.rodriguez@fcc.es;alopez@utemursiya.es".Split(';');
                for (int i=0; i<direcciones.Length; i++)
                { 
                    SicaSegura.SICA_Mailing email = new SicaSegura.SICA_Mailing();
                    email.Set_asunto("[SICA-VISITAS A CAMPO]Nueva visita dada de alta o actualizada a las " + DateTime.Now.ToString());
                    email.Set_cuerpo("Visita para el expedirte ISM: <b>" + EXP_ISM + "</b><br>Visita del tipo: <b>" + item.Value + "</b><br>Para la fecha: <b>" + fecha + "</b><br><br><br> Por favor Actualiza toda la información relativa a esta visita desde la URL indicada más abajo:<br><br><br><a href=\"https://sica.chsegura.es/sicasegura/SICA/Gestion_Visitas_Campo/index.aspx\"> Acceso a la web de visitas</a> ");
                    email.Set_destinatarios(direcciones[i]);
                   //email.Set_destinatarios_cco("rosa.palomares@chsegura.es");
                    email.Set_formatoHTML();
                    email.Enviar_mail();
                }
            }
        }
        Response.Redirect("index.aspx");
    }

    protected void BTN_Cancelar_Update_Click(object sender, EventArgs e)
    {
        Session["editando"] = "false";
        PNL_Update.Visible = false;
    }

    protected void BTN_UPDATE_Click(object sender, EventArgs e)
    {
        string fechaPK = Label1.Text;
        string ISMPK = TXT_EXP_ISM_Modificacion.Text;

        String SQL = "DELETE FROM SICA_Visitas_A_campo where fechaID='" + fechaPK + "' AND EXP_ISM like '"  + ISMPK +  "'";
        DB.GetDataDBSICA(SQL);
        string fecha = "";
        if (Calendar2.SelectedDate.ToShortDateString() != "01/01/0001")
        { fecha = Calendar2.SelectedDate.ToShortDateString(); }
        EXP_ISM = DDL_EXP_ISM_Format_value(TXT_EXP_ISM_Modificacion.Text.ToString());
        SQL = "INSERT";
        SQL += " INTO SICA_Visitas_a_campo(FechaID, EXP_ISM, Tipo, Contacto, Fecha_visita, Personal_SICA, Personal_CHS, Observaciones)";
        SQL += " VALUES('" + DateTime.Now.ToString() + "','" + EXP_ISM + "'," + DDL_Tipo_Visita_Update.SelectedValue.ToString() + ",'" + TXT_Contacto_update.Text.ToString() + "','" + fecha + "','" + TXT_PersonalUTE_UPDATE.Text.ToString() + "','" + TXT_PersonalCHS_UPDATE.Text.ToString() + "','" + TXT_Observaciones_Update.Text.ToString() + "')";
        DB.GetDataDBSICA(SQL);

        Session["editando"] = "false";
        Response.Redirect("index.aspx");
    }
    
    protected void BTN_Cancelar_nuevo_Click(object sender, EventArgs e)
    {
        PNL_nueva_visita.Visible = false;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        PNL_nueva_visita.Visible = true;
        PNL_Update.Visible = false;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        PNL_nueva_visita.Visible = true;
        PNL_Update.Visible = false;
    }

    protected void DGV_Visitas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["borrando"] = "true";
    }

    protected void DGV_Visitas_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PNL_nueva_visita.Visible = false;
        PNL_Update.Visible = true;
        Session["editando"] = "true";
    }

    protected void DGV_Visitas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "filter")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(),
           "Message", "alert('" + index + "');", true);
        }
    }

    protected void DGV_Visitas_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView dgv = sender as GridView;
        if (dgv != null && dgv.SelectedRow != null)
        {
            GridViewRow row = dgv.SelectedRow;
            if (row != null)
            {
                if ((string)Session["consultando"] == "true")
                {
                    string Tipo = row.Cells[3].Text.ToString() + "  ";
                    LIT_Filtro_Tipo.Text = "<div style=\"float:right; background-image: url(\'../images/fondofiltro.png'); width:137px; height:30px;text-align: center;font: bold 12px/30px Tahoma, serif;\"><a href=\"index.aspx?tipo=ACTIVIDAD&ACTIVIDAD=" + Tipo + "\">" + Tipo + "</a></div>";
                    string ISM = row.Cells[2].Text.ToString() + "  ";
                    LIT_Filtro_ISM.Text = "<div style=\"float:right; background-image:  url(\'../images/fondofiltro.png'); width:137px; height:30px;text-align: center;font: bold 12px/30px Tahoma, serif;\"><a href=\"index.aspx?tipo=ISM&ISM=" + ISM + " \">" + ISM + "</a></div>";
                    string Fecha = row.Cells[5].Text.ToString() + "  ";
                    if (Fecha.Contains('/'))
                    {
                        LIT_Filtro_fecha.Text = "<div style=\"float:right; background-image:url(\'../images/fondofiltro.png'); width:130px; height:30px;text-align: center;font: bold 12px/30px Tahoma, serif;\"><a href=\"\">" + Fecha + "</a></div>";
                    }
                    else
                    {
                        LIT_Filtro_fecha.Text = "<div style=\"float:right; background-image:url(\'../images/fondofiltro.png'); width:0px; height:30px;text-align: center;font: bold 12px/30px Tahoma, serif;\"><a href=\"\">" + Fecha + "</a></div>";

                    }
                    Panel1.Visible = true;
                }
                if ((string)Session["editando"] == "true")
                {
                    Cargar_Datos_panel_edicion(row);
                    PNL_nueva_visita.Visible = false;
                    PNL_Update.Visible = true;
                    Session["consultando"] = "true";
                }
                if((string)Session["borrando"] == "true")
                {
                    
                    string SQL = "DELETE FROM SICA_Visitas_a_campo where fechaid='" + row.Cells[1].Text.ToString() + "'";
                    SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
                    DB.GetDataDBSICA(SQL);
                    Session["borrando"] = "false";
                    Response.Redirect("index.aspx");
                }
            }
        }
        
    }

    protected void DGV_Visitas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            // Hiding the Select Button Cell in Header Row.
            //e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Hiding the Select Button Cells showing for each Data Row. 
            //e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");

            // Attaching one onclick event for the entire row, so that it will
            // fire SelectedIndexChanged, while we click anywhere on the row.
            e.Row.Attributes["onclick"] =
              ClientScript.GetPostBackClientHyperlink(this.DGV_Visitas, "Select$" + e.Row.RowIndex);
        }
    }
    
    private void Cargar_Datos_panel_edicion(GridViewRow GVR)
    {
        string tipo = "";
        if (GVR.Cells[3].Text == "Caracterización") { tipo = "0"; }
        if (GVR.Cells[3].Text == "Verificación") { tipo = "1"; }
        if (GVR.Cells[3].Text == "Valid. Telemedida") { tipo = "2"; }
        if (GVR.Cells[3].Text == "Acta Confrontación") { tipo = "3"; }
        if (GVR.Cells[3].Text == "Otros") { tipo = "4"; }

        Label1.Text = GVR.Cells[1].Text.ToString();
        TXT_EXP_ISM_Modificacion.Text = GVR.Cells[2].Text.ToString();
        DDL_Tipo_Visita_Update.SelectedIndex = Int16.Parse(tipo);
        TXT_Contacto_update.Text = HttpUtility.HtmlDecode(GVR.Cells[4].Text.ToString());
        TXT_Observaciones_Update.Text = HttpUtility.HtmlDecode(GVR.Cells[8].Text.ToString());
        try { Calendar2.SelectedDate = DateTime.Parse(GVR.Cells[5].Text.ToString()); } catch { }
        TXT_PersonalUTE_UPDATE.Text = HttpUtility.HtmlDecode(GVR.Cells[6].Text.ToString());
        TXT_PersonalCHS_UPDATE.Text = HttpUtility.HtmlDecode(GVR.Cells[7].Text.ToString());
    }
}



