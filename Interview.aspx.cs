using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistForm
{
    public partial class Interview : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            HiddenField hiddenyo = (HiddenField)Master.FindControl("hiddenyo");
            hiddenyo.Value = "003";


            if (!IsPostBack)
            {

                if (Session["joingroup"] != null)
                {
                    joingroup.SelectedValue = Session["joingroup"].ToString();
                }
                if (Session["batch"] != null)
                {
                    batch.SelectedValue = Session["batch"].ToString();
                }

                ShowData();
            }
        }


        public void ShowData()
        {

            string sqlN = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["RegistConnectionString"]
                .ConnectionString;
            SqlConnection memberConnection = new SqlConnection(sqlN); //建立連線通道

            //搜尋字串
            string commandString = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Code/SQL/Search.sql"));
            string searchString = "";

            if (joingroup.SelectedValue != "0")
            {
                searchString += " and  joingroup=@joingroup ";
            }

            if (batch.SelectedValue != "0")
            {
                searchString += " and  (batch=@batch OR batch=@batch2) ";
            }

            searchString += " and  status=@status ";

            commandString = commandString.Replace("/*--where begin --*/",
                string.Format("/*--where begin --*/ {0}{1}", Environment.NewLine, searchString));
            commandString = commandString.Replace("ORDER BY id desc", "ORDER BY id ASC");

            SqlCommand sqlCommand = new SqlCommand(commandString, memberConnection);
            //處理參數

            if (!string.IsNullOrEmpty(joingroup.SelectedValue))
            {
                sqlCommand.Parameters.Add("@joingroup", SqlDbType.NVarChar);
                sqlCommand.Parameters["@joingroup"].Value = joingroup.SelectedValue ?? "";
            }

            sqlCommand.Parameters.Add("@batch", SqlDbType.NVarChar);
            sqlCommand.Parameters["@batch"].Value = batch.SelectedValue ?? "";
            sqlCommand.Parameters.Add("@batch2", SqlDbType.NVarChar);   
            sqlCommand.Parameters["@batch2"].Value = "1,2";

            sqlCommand.Parameters.Add("@status", SqlDbType.NVarChar);
            sqlCommand.Parameters["@status"].Value = "面試";
            



            int currentPage = Request.QueryString["Page"] == null ? 1 : Convert.ToInt32(Request.QueryString["Page"]);
            int pageSize = 20;
            sqlCommand.Parameters.Add("@start", SqlDbType.Int);
            sqlCommand.Parameters["@start"].Value = ((currentPage - 1) * pageSize) + 1;
            sqlCommand.Parameters.Add("@end", SqlDbType.Int);
            sqlCommand.Parameters["@end"].Value = currentPage * pageSize;

            //總筆數

            string countString = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Code/SQL/SearchCount.sql"));
            countString = countString.Replace("/*--where begin --*/",
                        string.Format("/*--where begin --*/ {0}{1}", Environment.NewLine, searchString));
            SqlCommand countCommand = new SqlCommand(countString, memberConnection);

            countCommand.Parameters.Add("@joingroup", SqlDbType.NVarChar);
            countCommand.Parameters["@joingroup"].Value = joingroup.SelectedValue ?? "";

            countCommand.Parameters.Add("@batch", SqlDbType.NVarChar);
            countCommand.Parameters["@batch"].Value = batch.SelectedValue ?? "";
            countCommand.Parameters.Add("@batch2", SqlDbType.NVarChar);
            countCommand.Parameters["@batch2"].Value = "1,2";

            countCommand.Parameters.Add("@status", SqlDbType.NVarChar);
            countCommand.Parameters["@status"].Value = "面試";

            countCommand.Parameters.Add("@start", SqlDbType.Int);
            countCommand.Parameters["@start"].Value = ((currentPage - 1) * pageSize) + 1;
            countCommand.Parameters.Add("@end", SqlDbType.Int);
            countCommand.Parameters["@end"].Value = currentPage * pageSize;



            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill((dataTable));
            GridView1.DataSource = dataTable;
            GridView1.DataBind();

            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(countCommand);
            DataTable datatable1 = new DataTable();
            dataAdapter1.Fill(datatable1);
            PageControl.totalitems = Convert.ToInt32(datatable1.Rows[0][0]);
            PageControl.limit = pageSize;
            PageControl.targetpage = "Interview.aspx";
            PageControl.showPageControls();

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string Rid = GridView1.DataKeys[e.NewEditIndex].Value.ToString();
            string page = Request["page"] ?? "1";
            Response.Redirect($"grade.aspx?id={Rid}&page={page}");
        }


        protected void search_Click(object sender, EventArgs e)
        {

            Session.Remove("joingroup");
            Session.Remove("batch");

            if (!string.IsNullOrEmpty(joingroup.SelectedValue))
            {
                Session["joingroup"] = joingroup.SelectedValue;
            }

            if (!string.IsNullOrEmpty(batch.SelectedValue))
            {
                Session["batch"] = batch.SelectedValue;
            }

            Response.Redirect("Interview.aspx");
        }

        protected void clear_Click(object sender, EventArgs e)
        {

            Session.Remove("joingroup");
            Session.Remove("batch");

            Response.Redirect("~/Interview.aspx");
        }
        protected void sort_Click(object sender, EventArgs e)
        {
            
            string sqlN = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["RegistConnectionString"]
                .ConnectionString;
            SqlConnection memberConnection = new SqlConnection(sqlN); //建立連線通道

            //搜尋字串
            string commandString = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Code/SQL/Search.sql"));
            string searchString = "";

            if (joingroup.SelectedValue != "0")
            {
                searchString += " and  joingroup=@joingroup ";
            }

            if (batch.SelectedValue != "0")
            {
                searchString += " and  (batch=@batch OR batch=@batch2) ";
            }

            commandString = commandString.Replace("/*--where begin --*/",
                string.Format("/*--where begin --*/ {0}{1}", Environment.NewLine, searchString));
            commandString = commandString.Replace("ORDER BY id desc", "ORDER BY (gonsakon_score1*2+gonsakon_score2*5+gonsakon_score3*2+justin_score1*2+justin_score2*5+justin_score3*2) desc");

            SqlCommand sqlCommand = new SqlCommand(commandString, memberConnection);
            int currentPage = Request.QueryString["Page"] == null ? 1 : Convert.ToInt32(Request.QueryString["Page"]);
            int pageSize = 20;
            sqlCommand.Parameters.Add("@start", SqlDbType.Int);
            sqlCommand.Parameters["@start"].Value = ((currentPage - 1) * pageSize) + 1;
            sqlCommand.Parameters.Add("@end", SqlDbType.Int);
            sqlCommand.Parameters["@end"].Value = currentPage * pageSize;
            if (!string.IsNullOrEmpty(joingroup.SelectedValue))
            {
                sqlCommand.Parameters.Add("@joingroup", SqlDbType.NVarChar);
                sqlCommand.Parameters["@joingroup"].Value = joingroup.SelectedValue ?? "";
            }

            sqlCommand.Parameters.Add("@batch", SqlDbType.NVarChar);
            sqlCommand.Parameters["@batch"].Value = batch.SelectedValue ?? "";
            sqlCommand.Parameters.Add("@batch2", SqlDbType.NVarChar);
            sqlCommand.Parameters["@batch2"].Value = "1,2";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill((dataTable));
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }

    }
}