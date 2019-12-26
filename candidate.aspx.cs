using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistForm
{
    public partial class candidate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Guid.NewGuid().ToString());
            //偷放個隱藏欄位並賦值
            HiddenField hiddenyo = (HiddenField)Master.FindControl("hiddenyo");
            hiddenyo.Value = "002";
            if (!IsPostBack)
            {
                string showdetail = System.Web.Configuration.WebConfigurationManager
                    .ConnectionStrings["RegistConnectionString"].ToString();
                SqlConnection showConnection = new SqlConnection(showdetail);
                SqlCommand showCommand =
                    new SqlCommand($" Select * from registform where id=@Eid", showConnection);
                showCommand.Parameters.Add("@Eid", SqlDbType.NVarChar);
                showCommand.Parameters["@Eid"].Value = Request.QueryString["id"];
                showConnection.Open();
                SqlDataReader ereader = showCommand.ExecuteReader();
                ereader.Read();
                name.Text = ereader["name"].ToString();
                tel.Text = ereader["tel"].ToString();
                email.Text = ereader["email"].ToString();
                pdf.Text = ereader["pdf"].ToString();
                pdfa.HRef = "/Upload/" + ereader["pdf"].ToString();
                introduction.Text = Server.HtmlEncode(ereader["introduction"].ToString());
                why.Text = Server.HtmlEncode(ereader["why"].ToString());
                essay.Text = Server.HtmlEncode(ereader["essay"].ToString());
                joingroup.Text = ereader["why"].ToString();
                batch.Text = ereader["batch"].ToString();
            }
        }

        protected void come_Click(object sender, EventArgs e)
        {
            string showdetail = System.Web.Configuration.WebConfigurationManager
                .ConnectionStrings["RegistConnectionString"].ToString();
            SqlConnection showConnection = new SqlConnection(showdetail);
            SqlCommand showCommand =
                new SqlCommand($" UPDATE registform SET status=@status where id=@Eid", showConnection);
            showCommand.Parameters.Add("@Eid", SqlDbType.NVarChar);
            showCommand.Parameters["@Eid"].Value = Request.QueryString["id"];
            showCommand.Parameters.Add("@status", SqlDbType.NVarChar);
            showCommand.Parameters["@status"].Value = "面試";
            showConnection.Open();
            showCommand.ExecuteNonQuery();
            showConnection.Close();
            Response.Redirect("RegistrationList.aspx");
        }

        protected void byebye_Click(object sender, EventArgs e)
        {
            string showdetail = System.Web.Configuration.WebConfigurationManager
                .ConnectionStrings["RegistConnectionString"].ToString();
            SqlConnection showConnection = new SqlConnection(showdetail);
            SqlCommand showCommand =
                new SqlCommand($" UPDATE registform SET status=@status where id=@Eid", showConnection);
            showCommand.Parameters.Add("@Eid", SqlDbType.NVarChar);
            showCommand.Parameters["@Eid"].Value = Request.QueryString["id"];
            showCommand.Parameters.Add("@status", SqlDbType.NVarChar);
            showCommand.Parameters["@status"].Value = "有緣再會";

            showConnection.Open();
            showCommand.ExecuteNonQuery();
            showConnection.Close();
            Response.Redirect("RegistrationList.aspx");
        }
    }
}