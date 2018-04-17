using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class librodigital_home_InfoUsuario : System.Web.UI.Page
{
    SicaSegura.SICA_LibroControl LCA;
    SicaSegura.SICA_Interfaz.SICA_LibroControl InterfazLibroControl;
    protected void Page_Load(object sender, EventArgs e)
    {
        string updatevar = "false";
        try
        {
            
            object Objeto_LC = Session["LCA"];
            object Titulo_Inscripcion = Session["USERNAME"];

            LCA = (SicaSegura.SICA_LibroControl)Objeto_LC;
            InterfazLibroControl = new SicaSegura.SICA_Interfaz.SICA_LibroControl();
            string[] campos = LCA.RecuperarInfoRaacsInscripcion().Split('#');
            try { 
                    updatevar = Request.QueryString["update"].ToString();
                }
            catch { }
            //string[] campos = LCA.RecuperarInfoRaacsInscripcion().Split('#');
            string[] titular = (new SicaSegura.SICA_Titular_Aprovechamiento()).GetListadoTitularesRaacs(LCA.ID_Inscripcion)[0].Split(new string[] { "$%" }, StringSplitOptions.None);
            string otros = "";
            if ((new SicaSegura.SICA_Titular_Aprovechamiento()).GetListadoTitularesRaacs(LCA.ID_Inscripcion).Length > 1)
            {
                otros = " (y otros)";
            }
            HTML_Título_Aprovechamiento.Text = "Inscripción: " + LCA.ID_Inscripcion.ToString() + " -- " + LCA.Agrupacion.Get_Descripcion() + " <br>Titular: " + titular[1] + " " + titular[2] + otros;
            

        }
        catch { Response.Redirect("../index.aspx"); }



        //
        //GENERACION DE LA INTERFAZ
        //
        string menu = "Cerrar sesion-borrarSesion.aspx#Información de usuario-index.aspx";
        HTML_Links_Sup_Izq.Text = InterfazLibroControl.Get_Enlaces_Sup_Izq(menu.Split('#'));
        HTML_Subcabecera_Logos.Text = InterfazLibroControl.Get_Cabecera();
        //HTML_Menu_Navegacion.Text = InterfazLibroControl.Get_Menu_Navegacion("uno");
        HTML_Pie_Logo_Corporativo.Text = InterfazLibroControl.Get_pie_logo_corporativo();
        HTML_Pie_pagina.Text = InterfazLibroControl.Get_pie_pagina();
        if (!IsPostBack)
        {
            if (updatevar == "false")
            {
                TXT_Name.ReadOnly = true;
                TXT_DNI.ReadOnly = true;
                TXT_Addres.ReadOnly = true;
                TXT_TLF.ReadOnly = true;
                TXT_Email.ReadOnly = true;
                BTN_Validar_cambios.Visible = false;

            }
            else { }
            Rellenar_informacion_contacto(LCA.ID_Inscripcion);
        }
        

    }

    private void Rellenar_informacion_contacto(int p)
    {
        SicaSegura.SICA_Titular_Aprovechamiento titular = new SicaSegura.SICA_Titular_Aprovechamiento();
        String[] datos_Contacto = titular.GetInformacionContactoLibroDigital(LCA.ID_Inscripcion).Split(new string[] { "$%" }, StringSplitOptions.None);
        if (datos_Contacto[0].Contains("error"))
        {
            LBL_MesageNoDatos.Text = "La ficha de contacto está vacía, por favor cumpliméntela";
        }
        else
        {
            TXT_Name.Text = datos_Contacto[1];           
            TXT_DNI.Text = datos_Contacto[2];
            TXT_Addres.Text = datos_Contacto[3];
            TXT_TLF.Text = datos_Contacto[4];
            TXT_Email.Text = datos_Contacto[5];
        }
        
        //throw new NotImplementedException();
    }
    protected void BTN_Validar_cambios_Click(object sender, EventArgs e)
    {
        SicaSegura.SICA_Titular_Aprovechamiento Titular = new SicaSegura.SICA_Titular_Aprovechamiento();
        int Inscripcionactiva;
        Inscripcionactiva = LCA.ID_Inscripcion;
        Titular.EliminarInformacionContactoLibroDigital(Inscripcionactiva);
        string datos = "";
        datos = TXT_Name.Text.Trim() + "$%" + TXT_DNI.Text.Trim() + "$%" + TXT_Addres.Text.Trim() + "$%" + TXT_TLF.Text.Trim() + "$%" + TXT_Email.Text.Trim();
        Titular.InsertarInformacionContactoLibroDigital(Inscripcionactiva, datos);
        Response.Redirect("infousuario.aspx");
    }
}
