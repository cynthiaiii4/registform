using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace RegistForm
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HiddenField hiddenyo = (HiddenField)Master.FindControl("hiddenyo");
            hiddenyo.Value = "002";
            

            if (!IsPostBack)
            {

                if (Session["timeStart"] != null)
                {
                    timeStart.Value = Session["timeStart"].ToString();
                }
                if (Session["timeEnd"] != null)
                {
                    timeEnd.Value = Session["timeEnd"].ToString();
                }
                if (Session["joingroup"] != null)
                {
                    joingroup.SelectedValue = Session["joingroup"].ToString();
                }
                if (Session["batch"] != null)
                {
                    batch.SelectedValue = Session["batch"].ToString();
                }
                if (Session["keyword"] != null)
                {
                    keyword.Value = Session["keyword"].ToString();
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
            if (!string.IsNullOrEmpty(timeStart.Value))
            {
                searchString += " and initdate>@timeStart ";
            }

            if (!string.IsNullOrEmpty(timeEnd.Value))
            {
                searchString += " and initdate<@timeEnd ";
            }

            if (joingroup.SelectedValue != "0")
            {
                searchString += " and  joingroup=@joingroup ";
            }

            if (batch.SelectedValue != "0")
            {
                searchString += " and  (batch=@batch OR batch=@batch2)";
            }

            if (!string.IsNullOrEmpty(keyword.Value))
            {
                searchString += " and ((name LIKE '%' + @keyword + '%') OR (email LIKE '%' + @keyword + '%') OR (tel LIKE '%' + @keyword + '%') OR (introduction LIKE '%' + @keyword + '%') OR (why LIKE '%' + @keyword + '%') OR (essay LIKE '%' + @keyword + '%') OR (essay LIKE '%' + @keyword + '%')) ";
                ;
            }

            commandString = commandString.Replace("/*--where begin --*/",
                string.Format("/*--where begin --*/ {0}{1}", Environment.NewLine, searchString));

            SqlCommand sqlCommand = new SqlCommand(commandString, memberConnection);
            //處理參數

            if (!string.IsNullOrEmpty(timeStart.Value))
            {
                sqlCommand.Parameters.Add("@timeStart", SqlDbType.DateTime);
                sqlCommand.Parameters["@timeStart"].Value = DateTime.Parse(timeStart.Value);
            }

            if (!string.IsNullOrEmpty(timeEnd.Value))
            {
                sqlCommand.Parameters.Add("@timeEnd", SqlDbType.DateTime);
                sqlCommand.Parameters["@timeEnd"].Value = DateTime.Parse(timeEnd.Value).AddDays(1);
            }

            if (!string.IsNullOrEmpty(joingroup.SelectedValue))
            {
                sqlCommand.Parameters.Add("@joingroup", SqlDbType.NVarChar);
                sqlCommand.Parameters["@joingroup"].Value = joingroup.SelectedValue ?? "";
            }

            sqlCommand.Parameters.Add("@batch", SqlDbType.NVarChar);
            sqlCommand.Parameters["@batch"].Value = batch.SelectedValue ?? "";
            sqlCommand.Parameters.Add("@batch2", SqlDbType.NVarChar);
            sqlCommand.Parameters["@batch2"].Value = "1,2";

            sqlCommand.Parameters.Add("@keyword", SqlDbType.NVarChar);
            sqlCommand.Parameters["@keyword"].Value = keyword.Value ?? "";

            int currentPage = Request.QueryString["Page"] == null ? 1 : Convert.ToInt32(Request.QueryString["Page"]);
            int pageSize = 25;
            sqlCommand.Parameters.Add("@start", SqlDbType.Int);
            sqlCommand.Parameters["@start"].Value = ((currentPage - 1) * pageSize) + 1;
            sqlCommand.Parameters.Add("@end", SqlDbType.Int);
            sqlCommand.Parameters["@end"].Value = currentPage * pageSize;

            //總筆數

            string countString = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Code/SQL/SearchCount.sql"));
            countString = countString.Replace("/*--where begin --*/",
                        string.Format("/*--where begin --*/ {0}{1}", Environment.NewLine, searchString));
            SqlCommand countCommand = new SqlCommand(countString, memberConnection);
            if (!string.IsNullOrEmpty(timeStart.Value))
            {
                countCommand.Parameters.Add("@timeStart", SqlDbType.DateTime);
                countCommand.Parameters["@timeStart"].Value = timeStart.Value;
            }

            if (!string.IsNullOrEmpty(timeEnd.Value))
            {
                countCommand.Parameters.Add("@timeEnd", SqlDbType.DateTime);
                countCommand.Parameters["@timeEnd"].Value = DateTime.Parse(timeEnd.Value).AddDays(1);
            }

            countCommand.Parameters.Add("@joingroup", SqlDbType.NVarChar);
            countCommand.Parameters["@joingroup"].Value = joingroup.SelectedValue ?? "";

            countCommand.Parameters.Add("@batch", SqlDbType.NVarChar);
            countCommand.Parameters["@batch"].Value = batch.SelectedValue ?? "";
            countCommand.Parameters.Add("@batch2", SqlDbType.NVarChar);
            countCommand.Parameters["@batch2"].Value = "1,2";

            countCommand.Parameters.Add("@keyword", SqlDbType.NVarChar);
            countCommand.Parameters["@keyword"].Value = keyword.Value ?? "";
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
            PageControl.targetpage = "RegistrationList.aspx";
            PageControl.showPageControls();

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string Rid = GridView1.DataKeys[e.NewEditIndex].Value.ToString();
            string page = Request["page"] ?? "1";
            Response.Redirect($"candidate.aspx?id={Rid}&page={page}");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string Rid = GridView1.DataKeys[e.RowIndex].Value.ToString();
           
            ShowData();
        }

        //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    string Rid = GridView1.DataKeys[e.RowIndex].Value.ToString();
        //    string sql1 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ORIDConnectionString"]
        //        .ConnectionString;
        //    SqlConnection oridConnection = new SqlConnection(sql1); //建立連線通道
        //    SqlCommand deletecommand = new SqlCommand($" Delete from newOrid where id={Rid}", oridConnection);
        //    //SqlDataAdapter dataAdapter = new SqlDataAdapter(deletecommand);
        //    //DataTable datatable = new DataTable();
        //    //dataAdapter.Fill(datatable);//這一行做很多事，代表，把通道打開，把資料放進dataset裡，再把通道關起來
        //    //GridView1.DataSource = datatable;
        //    //GridView1.DataBind();
        //    oridConnection.Open();
        //    deletecommand.ExecuteNonQuery();
        //    oridConnection.Close();
        //    ShowData();
        //}

        protected void search_Click(object sender, EventArgs e)
        {
            Session.Remove("timeStart");
            Session.Remove("timeEnd");
            Session.Remove("joingroup");
            Session.Remove("batch");
            Session.Remove("keyword");
            if (!string.IsNullOrEmpty(timeStart.Value))
            {
                Session["timeStart"] = Convert.ToDateTime(timeStart.Value);
            }

            if (!string.IsNullOrEmpty(timeEnd.Value))
            {
                Session["timeEnd"] = (Convert.ToDateTime(timeEnd.Value));
            }

            if (!string.IsNullOrEmpty(joingroup.SelectedValue))
            {
                Session["joingroup"] = joingroup.SelectedValue;
            }

            if (!string.IsNullOrEmpty(batch.SelectedValue))
            {
                Session["batch"] = batch.SelectedValue;
            }

            if (!string.IsNullOrEmpty(keyword.Value))
            {
                Session["keyword"] = keyword.Value;
            }

            Response.Redirect("RegistrationList.aspx");
        }

        protected void clear_Click(object sender, EventArgs e)
        {
            Session.Remove("timeStart");
            Session.Remove("timeEnd");
            Session.Remove("joingroup");
            Session.Remove("batch");
            Session.Remove("keyword");

            Response.Redirect("RegistrationList.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //只改ItemTemplate的內容
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //取得該行
                GridViewRow row = (GridViewRow)e.Row;
                //取得ID為sentEmail的HtmlInputButton
                HtmlInputButton sentEmail = (HtmlInputButton)row.FindControl("sentEmail");
                //取得ID為getEmail的Label
                Label getEmail = (Label)row.FindControl("getEmail");

                if (getEmail.Text != "")
                {
                    //如果有值就把按鈕隱藏
                    sentEmail.Visible = false;
                }
            }
        }
    }
}