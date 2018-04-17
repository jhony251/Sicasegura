using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class SICAH_SMS_Usuarios : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindData();
        }
    }
    private void BindData()
    {
        string strConnString = ConfigurationManager.ConnectionStrings["DBSICA"].ConnectionString;
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            string strQuery = "SELECT * FROM SICA_SMS_AUTORIZADOS";
            SqlCommand cmd = new SqlCommand(strQuery);
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
    }

    


    protected void Add(object sender, EventArgs e)
    {
        Control control = null;
        if (GridView1.FooterRow != null)
        {
            control = GridView1.FooterRow;
        }
        else
        {
            control = GridView1.Controls[0].Controls[0];
        }

        string telefono = (control.FindControl("txt_Telefono") as TextBox).Text;
        string autorizado = (control.FindControl("txt_Usuario") as TextBox).Text;
        string puntos = (control.FindControl("txt_Puntos") as TextBox).Text;
        if (telefono != "" && autorizado != "" && puntos != "")
        {
            SicaSegura.SICA_DB DB = new SicaSegura.SICA_DB();
            DB.GetDataDBSICA("INSERT INTO [SICA_SMS_Autorizados] VALUES('" + telefono + "', '" + autorizado + "', '" + puntos + "')");

            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = GridView1.SelectedRow.RowIndex;
        if (i >= 0)
        {
            Response.Redirect("~/AddNews.aspx?Parameter=" + i);
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Control control = null;
        if (GridView1.FooterRow != null)
        {
            control = GridView1.FooterRow;
        }
        else
        {
            control = GridView1.Controls[0].Controls[0];
        }
        GridView1.EditIndex = e.NewEditIndex;
        Button btn = control.FindControl("btnadd") as Button;
        btn.Enabled = false;
        btn.Visible = false;

        //TextBox tx1 = (TextBox)(GridView1.Rows[GridView1.EditIndex].Cells[0].FindControl("TextBox1"));
        //TextBox tx2 = (TextBox)(GridView1.Rows[GridView1.EditIndex].Cells[1].FindControl("TextBox2"));
        //TextBox tx3 = (TextBox)(GridView1.Rows[GridView1.EditIndex].Cells[2].FindControl("TextBox3"));



        //tx1.Width = new Unit(100);
        //tx2.Width = new Unit(100);
        //tx3.Width = new Unit(100);


        BindData();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int id = (int)GridView1.DataKeys[e.RowIndex].Value;
        //sh.get_userid = id;
        

        TextBox tx1 = (TextBox)(GridView1.Rows[GridView1.EditIndex].Cells[0].FindControl("TextBox1"));
        TextBox tx2 = (TextBox)(GridView1.Rows[GridView1.EditIndex].Cells[1].FindControl("TextBox2"));
        TextBox tx3 = (TextBox)(GridView1.Rows[GridView1.EditIndex].Cells[2].FindControl("TextBox3"));

        GridView1.EditIndex = -1;
        SicaSegura.SICA_DB db = new SicaSegura.SICA_DB();
        db.GetDataDBSICA("update SICA_SMS_AUTORIZADOS set Autorizado='" + tx2.Text + "' ,PuntosPVYCR='" + tx3.Text + "' where telefono like '" + tx1.Text +"'");

        //sh.get_username = tx1.Text;
        //sh.get_location = tx2.Text;
        //sh.get_contactno = tx3.Text;
        // sh.update_search();

        GridView1.EditIndex = -1;
        BindData();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindData();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       // GridView1.EditIndex = e.RowIndex;
       // TextBox tx1 = (TextBox)(GridView1.Rows[e.RowIndex].Cells[0].FindControl("TextBox1"));
       // string telefono = tx1.Text;
       // SicaSegura.SICA_DB db = new SicaSegura.SICA_DB();
       // db.GetDataDBSICA("DELETE FROM SICA_SMS_AUTORIZADOS WHERE telefono like '" + GridView1.Rows[e.RowIndex].Cells[0].ToString().Trim() + "'");
       // GridView1.EditIndex = -1;
       // BindData();
    }
}
