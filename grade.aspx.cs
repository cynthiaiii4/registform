using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace RegistForm
{
    public partial class grade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Guid.NewGuid().ToString());
            //偷放個隱藏欄位並賦值
            HiddenField hiddenyo = (HiddenField)Master.FindControl("hiddenyo");
            hiddenyo.Value = "002";
            string strTicket = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            ticket UPerson = JsonConvert.DeserializeObject<ticket>(strTicket);
            //把帳號人名放在左上
            string user = UPerson.UserID;
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
                joingroup.Text = ereader["joingroup"].ToString();
                batch.Text = ereader["batch"].ToString();
                if (user == "gonsakon")
                {
                    score1.Value = ereader["gonsakon_score1"].ToString();
                    score2.Value = ereader["gonsakon_score2"].ToString();
                    score3.Value = ereader["gonsakon_score3"].ToString();
                    if (!string.IsNullOrEmpty(ereader["gonsakon_love"].ToString() ))
                    {
                        Special.Checked = true;
                    }

                    memo.Text = ereader["gonsakon_memo"].ToString();
                    followup.Text= ereader["followup"].ToString();
                }
                if (user == "justin")
                {
                    score1.Value = ereader["justin_score1"].ToString();
                    score2.Value = ereader["justin_score2"].ToString();
                    score3.Value = ereader["justin_score3"].ToString();
                    if (!string.IsNullOrEmpty(ereader["justin_love"].ToString()))
                    {
                        Special.Checked = true;
                    }

                    memo.Text = ereader["justin_memo"].ToString();
                    followup.Text = ereader["followup"].ToString();
                }
            }
        }

        protected void come_Click(object sender, EventArgs e)
        {
            string strTicket = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            ticket UPerson = JsonConvert.DeserializeObject<ticket>(strTicket);
            //把帳號人名放在左上
            string user = UPerson.UserID;
            
            string showdetail = System.Web.Configuration.WebConfigurationManager
                .ConnectionStrings["RegistConnectionString"].ToString();
            SqlConnection showConnection = new SqlConnection(showdetail);
            SqlCommand showCommand=new SqlCommand();
            if (user == "gonsakon")
            {
                showCommand =
                    new SqlCommand($" UPDATE registform SET gonsakon_score1=@gonsakon_score1,gonsakon_score2=@gonsakon_score2,gonsakon_score3=@gonsakon_score3,gonsakon_love=@gonsakon_love,gonsakon_memo=@gonsakon_memo,followup=@followup where id=@Eid", showConnection);
                showCommand.Parameters.Add("@gonsakon_score1", SqlDbType.NVarChar);
                showCommand.Parameters["@gonsakon_score1"].Value = score1.Value;
                showCommand.Parameters.Add("@gonsakon_score2", SqlDbType.NVarChar);
                showCommand.Parameters["@gonsakon_score2"].Value = score2.Value;
                showCommand.Parameters.Add("@gonsakon_score3", SqlDbType.NVarChar);
                showCommand.Parameters["@gonsakon_score3"].Value = score3.Value;
                showCommand.Parameters.Add("@gonsakon_love", SqlDbType.NVarChar);
                if (Special.Checked)
                {
                    showCommand.Parameters["@gonsakon_love"].Value = "★";
                }
                else
                {
                    showCommand.Parameters["@gonsakon_love"].Value = "";
                }
                showCommand.Parameters.Add("@gonsakon_memo", SqlDbType.NVarChar);
                showCommand.Parameters["@gonsakon_memo"].Value = memo.Text;
                showCommand.Parameters.Add("@followup", SqlDbType.NVarChar);
                showCommand.Parameters["@followup"].Value = followup.Text;
            }

            if (user == "justin")
            {
                showCommand =
                    new SqlCommand($" UPDATE registform SET justin_score1=@justin_score1,justin_score2=@justin_score2,justin_score3=@justin_score3,justin_love=@justin_love,justin_memo=@justin_memo,followup=@followup where id=@Eid", showConnection);
                showCommand.Parameters.Add("@justin_score1", SqlDbType.NVarChar);
                showCommand.Parameters["@justin_score1"].Value = score1.Value;
                showCommand.Parameters.Add("@justin_score2", SqlDbType.NVarChar);
                showCommand.Parameters["@justin_score2"].Value = score2.Value;
                showCommand.Parameters.Add("@justin_score3", SqlDbType.NVarChar);
                showCommand.Parameters["@justin_score3"].Value = score3.Value;
                showCommand.Parameters.Add("@justin_love", SqlDbType.NVarChar);
                if (Special.Checked)
                {
                    showCommand.Parameters["@justin_love"].Value = "★";
                }
                else
                {
                    showCommand.Parameters["@justin_love"].Value = "";
                }
                
                showCommand.Parameters.Add("@justin_memo", SqlDbType.NVarChar);
                showCommand.Parameters["@justin_memo"].Value = memo.Text;
                showCommand.Parameters.Add("@followup", SqlDbType.NVarChar);
                showCommand.Parameters["@followup"].Value = followup.Text;
            }

            showCommand.Parameters.Add("@Eid", SqlDbType.NVarChar);
            showCommand.Parameters["@Eid"].Value = Request.QueryString["id"];
            

            showConnection.Open();
            showCommand.ExecuteNonQuery();
            showConnection.Close();
            string page = Context.Request["page"];
            Response.Redirect($"~/Interview.aspx?page={page}");
        }

       
    }
}