using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistForm
{
    public partial class addmember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HiddenField hiddenyo = (HiddenField)Master.FindControl("hiddenyo");
            hiddenyo.Value = "001";
        }

        protected void save_Click(object sender, EventArgs e)
        {
            string fileName = "";
            if (RocketProfile.HasFile)
            {
                if (RocketProfile.PostedFile.ContentType.IndexOf("image") == -1)
                {
                    Message.Text = "檔案型態錯誤!";
                    return;
                }
                //取得副檔名
                string Extension = RocketProfile.FileName.Split('.')[RocketProfile.FileName.Split('.').Length - 1];
                //新檔案名稱
                fileName = String.Format("{0}{1:yyyyMMddhhmmsss}.{2}", UserID.Value, DateTime.Now, Extension);
                //上傳目錄為/upload/Images/
                RocketProfile.SaveAs(Server.MapPath(String.Format("~/Profile/{0}", fileName)));

            }
            //取得勾選值
            //建立空字串儲存值
            string checkbox = ",";
            //將chkOutingType.Items裡所有的值傳到oItem儲存
            foreach (ListItem item in CheckBoxList.Items)
            {
                //判斷item是否有被選擇
                if (item.Selected == true)
                {
                    //判斷check是否已有值
                    //if (checkbox.Length > 0)
                    //{
                    //    //有就加,區分
                    //    checkbox += ",";
                    //}

                    checkbox += item.Value + ",";
                }
            }

            check.Text = "";
            if (string.IsNullOrEmpty(UserID.Value))
            {
                check.Text += "*帳號不可以為空";
            }
            if (string.IsNullOrEmpty(Pwd.Value))
            {
                check.Text += "*密碼不可以為空";
            }
            if (string.IsNullOrEmpty(UserName.Value))
            {
                check.Text += "*姓名不可以為空";
            }
            //if (!string.IsNullOrEmpty(PWmessage.Text))
            //{
            //    check.Text += "*請重新確認密碼";
            //}

            if (string.IsNullOrEmpty(check.Text))
            {
                string password = FormsAuthentication.HashPasswordForStoringInConfigFile(Pwd.Value, "MD5");

                string AddAcount = System.Web.Configuration.WebConfigurationManager
                    .ConnectionStrings["RegistConnectionString"].ToString();
                SqlConnection addConnection = new SqlConnection(AddAcount);
                SqlCommand addCommand =
                    new SqlCommand(
                        $"INSERT INTO member(UserID,Pwd,UserName,RocketProfile,Permission) VALUES(@UserID,@Pwd,@UserName,@RocketProfile,@Permission)",
                        addConnection);
                addCommand.Parameters.Add("@UserID", SqlDbType.NVarChar);
                addCommand.Parameters["@UserID"].Value = UserID.Value;
                addCommand.Parameters.Add("@Pwd", SqlDbType.NVarChar);
                addCommand.Parameters["@Pwd"].Value = password;
                addCommand.Parameters.Add("@UserName", SqlDbType.NVarChar);
                addCommand.Parameters["@UserName"].Value = UserName.Value;
                addCommand.Parameters.Add("@RocketProfile", SqlDbType.NVarChar);
                addCommand.Parameters["@RocketProfile"].Value = fileName;
                addCommand.Parameters.Add("@Permission", SqlDbType.NVarChar);
                addCommand.Parameters["@Permission"].Value = checkbox;
                addConnection.Open();
                addCommand.ExecuteNonQuery();
                addConnection.Close();
                Response.Redirect("members.aspx");
            }

        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("members.aspx");
        }
    }
}