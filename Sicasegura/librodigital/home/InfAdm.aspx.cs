using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class librodigital_home_InfAdm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            object Objeto_LC = Session["LCA"];
            object Titulo_Inscripcion = Session["USERNAME"];

            SicaSegura.SICA_LibroControl LCA = (SicaSegura.SICA_LibroControl)Objeto_LC;
            SicaSegura.SICA_Interfaz.SICA_LibroControl InterfazLibroControl = new SicaSegura.SICA_Interfaz.SICA_LibroControl();




            //
            //GENERACION DE LA INTERFAZ
            //
            string menu = "Cerrar sesion-borrarSesion.aspx#Información de usuario-index.aspx";
            HTML_Links_Sup_Izq.Text = InterfazLibroControl.Get_Enlaces_Sup_Izq(menu.Split('#'));
            HTML_Subcabecera_Logos.Text = InterfazLibroControl.Get_Cabecera();
            HTML_Menu_Navegacion.Text = InterfazLibroControl.Get_Menu_Navegacion("cuatro");
            HTML_Pie_Logo_Corporativo.Text = InterfazLibroControl.Get_pie_logo_corporativo();
            HTML_Pie_pagina.Text = InterfazLibroControl.Get_pie_pagina();
            HTML_Título_Aprovechamiento.Text = LCA.Agrupacion.Get_Descripcion();

            HTML_Link_Esqumas.Text = "<li><a href=\"esquemas.aspx\">Esquema Instalaciones</a></li>";
            HTML_Link_Fotos.Text = "<li><a  href=\"galeria.aspx\">Fotos Instalaciones</a></li>";
            HTML_Link_Infadm_pdf.Text = "<li><a href=\"download.aspx?area=Raacs&File=" + LCA.Agrupacion.Get_NumeroInscripcion() + ".pdf\">Inscripción Registro de Aguas (pdf)</a></li>";
            HTML_Link_Infadm.Text = "<li><a href=\"infadm.aspx\">Información administrativa</a></li>";
            //HTML_Link_InfContadores.Text = "<li><a  href=\"\">Información contadores</a></li>";

            Muestra_info_Raacs_formateada(LCA.RecuperarInfoRaacsInscripcion().Split('#'));
        }
        catch { Response.Redirect("../index.aspx"); }




    }
    private void Muestra_info_Raacs_formateada(string[] campos)
    {
        HTML_Num_insc.Text = campos[0].Replace('.', ' ');

        HTML_Inf_Adm_seccion.Text = campos[1].Replace('.',' ');
        HTML_Inf_Adm_tomo.Text = campos[2].Replace('.', ' ');
        HTML_Inf_Adm_hoja.Text = campos[3].Replace('.', ' ');
        HTML_Inf_Adm_Lugar.Text = campos[12].Replace('.', ' ');
        HTML_Inf_Adm_Acuifero.Text = campos[5].Replace('.', ' ');
        HTML_Inf_Adm_Term_municipal.Text = campos[13].Replace('.', ' ');
        HTML_Inf_Adm_provincia.Text = campos[14].Replace('.', ' ');
        HTML_Inf_Adm_Fec_inscripcion.Text = campos[41].Replace('.', ' ');
        HTML_Inf_adm_firmado.Text = campos[42];

        HTML_Inf_adm_superficie.Text = campos[29].Replace('.', ' ');
        HTML_Inf_adm_desnivel.Text = campos[30].Replace('.', ' ');
        HTML_Inf_adm_salto.Text = campos[31].Replace('.', ' ');
        HTML_Inf_adm_potencia.Text = campos[32].Replace('.', ' ');
        HTML_Inf_adm_Qinstantaneo.Text = campos[15].Replace('.', ' ');
        HTML_Inf_adm_Qmedio.Text = campos[16].Replace('.', ' ');

        HTML_Inf_adm_DotReg.Text = campos[23].Replace('.', ' ');
        HTML_Inf_adm_DotInd.Text = campos[24].Replace('.', ' ');
        HTML_Inf_adm_DotAbs.Text = campos[25].Replace('.', ' ');
        HTML_Inf_adm_DotGan.Text = campos[26].Replace('.', ' ');
        HTML_Inf_adm_DotHid.Text = campos[27].Replace('.', ' ');
        HTML_Inf_adm_DotOtros.Text = campos[28].Replace('.', ' ');

        HTML_Inf_adm_VolReg.Text = campos[18].Replace('.', ' ');
        HTML_Inf_adm_VolInd.Text = campos[19].Replace('.', ' ');
        HTML_Inf_adm_VolAbs.Text = campos[20].Replace('.', ' ');
        HTML_Inf_adm_VolGan.Text = campos[21].Replace('.', ' ');
        HTML_Inf_adm_VolHid.Text = campos[22].Replace('.', ' ');
        HTML_Inf_adm_VolOtros.Text = campos[17].Replace('.', ' ');

        HTML_Inf_adm_observaciones.Text += "TITULO-FECHA-AUTORIDAD: " + campos[33].Replace('.',' ') + "<br><br>";
        HTML_Inf_adm_observaciones.Text += "CONDICIONES ESPECÍFICAS: " + campos[34].Replace('.', ' ') + "<br><br>";
        HTML_Inf_adm_observaciones.Text += "PRIMERA: " + campos[35].Replace('.', ' ') + "<br><br>";
        HTML_Inf_adm_observaciones.Text += "SEGUNDA: " + campos[36].Replace('.', ' ') + "<br><br>";
        HTML_Inf_adm_observaciones.Text += "TERCERA: " + campos[37].Replace('.', ' ') + "<br><br>";
        HTML_Inf_adm_observaciones.Text += "CUARTA: " + campos[38].Replace('.', ' ') + "<br><br>";
        HTML_Inf_adm_observaciones.Text += "QUINTA: " + campos[39].Replace('.', ' ') + "<br><br>";
        HTML_Inf_adm_observaciones.Text += "SEXTA: " + campos[40].Replace('.', ' ') + "<br><br>";
    }
}
