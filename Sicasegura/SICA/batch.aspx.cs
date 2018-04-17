using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SICA_batch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
        
        System.Data.DataTable dtpuntos = new System.Data.DataTable();
        System.Data.DataTable dtaux = new System.Data.DataTable();
        //dtpuntos = DB.GetDataDBSICA("Select Inscripcion FROM SICA_Seguimiento_Expedientes_ISM WHERE (INSCRIPCION IS NOT NULL) order by Inscripcion");
        //for (int i = 0; i < dtpuntos.Rows.Count; i++)
        //{
        //    dtaux = DB.GetDataSIGVECTOR("SELECT OtrosExpedientes FROM dbo.PVYCR_Cauces WHERE INSCRIPCION like '" + dtpuntos.Rows[i][0].ToString().Trim() + "'" );
        //    try { dtaux = DB.GetDataDBSICA("UPDATE SICA_Seguimiento_Expedientes_ISM SET EXP_ISM = '" + dtaux.Rows[0][0].ToString().Trim() + "' where Inscripcion like '" + dtpuntos.Rows[i][0].ToString().Trim() + "'"); }
        //    catch { }
        //}

        dtpuntos = DB.GetDataDBSICA("Select punto_control FROM SICA_Puntos_control order by punto_control");


        for (int i = 0; i < dtpuntos.Rows.Count; i++)
        {
            System.Data.DataTable dtinfoPunto = new System.Data.DataTable();
            dtinfoPunto = DB.GetDataSIGVECTOR("SELECT DenominacionPunto FROM PVYCR_Puntos where codigopvycr like '" + dtpuntos.Rows[i][0].ToString().Trim() + "%'");
            System.Data.DataTable dtinfoCauce = new System.Data.DataTable();
            string SQL = "SELECT Otrosexpedientes FROM Pvycr_cauces where CodigoCauce like (Select top(1) codigocauce from pvycr_puntos where codigopvycr like '" + dtpuntos.Rows[i][0].ToString().Trim() + "%' )";
            dtinfoCauce = DB.GetDataSIGVECTOR(SQL);
            System.Data.DataTable dtinfoCauce2 = new System.Data.DataTable();
            string SQL2 = "SELECT denominacioncauce FROM Pvycr_cauces where CodigoCauce like (Select codigocauce from pvycr_puntos where codigopvycr like '" + dtpuntos.Rows[i][0].ToString().Trim() + "%' )";
            dtinfoCauce2 = DB.GetDataSIGVECTOR(SQL2);
            try
            {
                DB.GetDataDBSICA("UPDATE SICA_Puntos_Control SET  Caracteristica2 = '" + dtinfoPunto.Rows[0][0].ToString().Trim() + "' where punto_control like '" + dtpuntos.Rows[i][0].ToString().Trim() + "%'");
            }
            catch { }
            try { DB.GetDataDBSICA("UPDATE SICA_Puntos_Control SET Caracteristica1 = '" + dtinfoCauce.Rows[0][0].ToString().Trim() + "' where punto_control like '" + dtpuntos.Rows[i][0].ToString().Trim() + "%'"); }
            catch (Exception ee) { }



        }

    }
}
