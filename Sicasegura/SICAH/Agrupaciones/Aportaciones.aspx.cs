using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class SICAH_Agrupaciones_Aportaciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        bind();

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        bind();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string SQL = "";
        int idPVYCR = 0;
        GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
        Label Inscripcion = (Label)row.FindControl("lblid");
        Label PVYCR = (Label)row.FindControl("lblPVYCR");
        TextBox Aportacion = (TextBox)row.FindControl("TXT_Aportacion");
        idPVYCR = getIDPVYCR(PVYCR.Text, Inscripcion.Text);


        GridView1.EditIndex = -1;

        SQL="UPDATE SICA_SIST_PuntosAportacion SET Aportacion =" + Aportacion.Text.Replace(',','.') + " WHERE ID_Sistema =

        //conn.Open();
        //SqlCommand cmd = new SqlCommand("update  emp set marks=" + textmarks.Text + " , name='" + textname.Text + "' where rowid=" + lbl.Text + "", conn);

        //cmd.ExecuteNonQuery();
        //conn.Close();
        bind();

    }

    private int getIDInscripcion(string PVYCR_EM, string Inscripcion)
    {
        SicaSegura.SICA_DB db = new SicaSegura.SICA_DB();
        string codigoPVYCR = PVYCR_EM.Split('-')[0].Trim();
        string EM = PVYCR_EM.Split('-')[1].Trim();

        string SQL = "SELECT     SICA_SIST_Sistemas.NumInscripcion, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM, SICA_SIST_PuntoSistema.ID " +
                    "FROM         SICA_SIST_Sistemas INNER JOIN " +
                    "             SICA_SIST_PuntoSistema ON SICA_SIST_Sistemas.ID = SICA_SIST_PuntoSistema.ID " +
                    "WHERE     (SICA_SIST_PuntoSistema.CodigoPVYCR IS NOT NULL) AND (SICA_SIST_Sistemas.NumInscripcion = 4121) " +
                    "          AND (SICA_SIST_PuntoSistema.CodigoPVYCR like '" + codigoPVYCR.Trim() + "' ) AND (SICA_SIST_PuntoSistema.EM like '" + EM.Trim() + "' ) ";
        DataTable dt = db.GetDataDBSICA(SQL);
        return (int)dt.Rows[0][3];

    }


    private int getIDPVYCR(string PVYCR_EM, string Inscripcion)
    {
        SicaSegura.SICA_DB db = new SicaSegura.SICA_DB();
        string codigoPVYCR = PVYCR_EM.Split('-')[0].Trim();
        string EM = PVYCR_EM.Split('-')[1].Trim();

        string SQL = "SELECT     SICA_SIST_Sistemas.NumInscripcion, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM, SICA_SIST_PuntoSistema.ID " +
                    "FROM         SICA_SIST_Sistemas INNER JOIN " +
                    "             SICA_SIST_PuntoSistema ON SICA_SIST_Sistemas.ID = SICA_SIST_PuntoSistema.ID " +
                    "WHERE     (SICA_SIST_PuntoSistema.CodigoPVYCR IS NOT NULL) AND (SICA_SIST_Sistemas.NumInscripcion = 4121) " +
                    "          AND (SICA_SIST_PuntoSistema.CodigoPVYCR like '" + codigoPVYCR.Trim() + "' ) AND (SICA_SIST_PuntoSistema.EM like '" + EM.Trim() + "' ) ";
        DataTable dt = db.GetDataDBSICA(SQL);
        return (int)dt.Rows[0][3];

    }
    public void bind()
    {
        SicaSegura.SICA_DB db = new SicaSegura.SICA_DB();
        string SQL = "SELECT     SICA_SIST_PuntoSistema.CodigoPVYCR  + '-' + SICA_SIST_PuntoSistema.EM  as CodigoPVYCR, " +
                                 "SICA_SIST_PuntosAportacion.Aportacion as Aportacion, SICA_SIST_Sistemas.NumInscripcion as NumInscripcion " +
                    "FROM        SICA_SIST_Sistemas INNER JOIN "+
                                 "SICA_SIST_PuntoSistema ON SICA_SIST_Sistemas.ID = SICA_SIST_PuntoSistema.ID RIGHT OUTER JOIN "+
                                 "SICA_SIST_PuntosAportacion ON SICA_SIST_PuntoSistema.ID = SICA_SIST_PuntosAportacion.ID_PuntoSistema "+
                                 "WHERE     (SICA_SIST_PuntoSistema.CodigoPVYCR IS NOT NULL) AND (SICA_SIST_Sistemas.NumInscripcion = 4121) "+
                                 "ORDER BY SICA_SIST_PuntosAportacion.ID_sistema";


        DataSet ds = new DataSet();
        ds = db.GetDataDBSICA_DS(SQL);
        ds.Tables[0].TableName = "emp";
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
        Label lbldeleteID = (Label)row.FindControl("lblid");
        //conn.Open();
       // SqlCommand cmd = new SqlCommand("delete  emp where rowid=" + lbldeleteID.Text + "", conn);
        //cmd.ExecuteNonQuery();
        //conn.Close();
        bind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();

    }
}
