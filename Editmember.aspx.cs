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
    public partial class Editmember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                //偷放個隱藏欄位並賦值
                HiddenField hiddenyo = (HiddenField)Master.FindControl("hiddenyo");
                hiddenyo.Value = "001";

                string EdiAcount = System.Web.Configuration.WebConfigurationManager
                    .ConnectionStrings["RegistConnectionString"].ToString();
                SqlConnection editConnection = new SqlConnection(EdiAcount);
                SqlCommand editCommand =
                    new SqlCommand($" Select * from member where id=@Eid", editConnection);
                editCommand.Parameters.Add("@Eid", SqlDbType.NVarChar);
                editCommand.Parameters["@Eid"].Value = Request.QueryString["id"];
                editConnection.Open();
                SqlDataReader ereader = editCommand.ExecuteReader();
                ereader.Read();
                IDLabel.Text = ereader["UserID"].ToString();
                hidden.Value = ereader["Pwd"].ToString();
                UserName.Value = ereader["UserName"].ToString();
                Image1.ImageUrl = @"/Profile/" + ereader["RocketProfile"].ToString();
                hiddenP.Value = ereader["RocketProfile"].ToString();
                string checkedBox = ereader["Permission"].ToString();
                ereader.Close();

                for (int i = 0; i < CheckBoxList.Items.Count; i++)
                {
                    if (checkedBox.IndexOf("," + CheckBoxList.Items[i].Value + ",") > -1)//把CheckBoxList裡面的值跟字串比對，以","包住確保查的值正確
                    {
                        CheckBoxList.Items[i].Selected = true;//比對成功就打勾
                        //break;
                    }
                }


            }
        }

        protected void upload_Click(object sender, EventArgs e)
        {
            string fileName = "";
            if (RocketProfile.HasFile)
            {
                if (RocketProfile.PostedFile.ContentType.IndexOf("image") == -1)
                {
                    Massage.Text = "檔案型態錯誤!";
                    return;
                }
                //取得副檔名
                string Extension = RocketProfile.FileName.Split('.')[RocketProfile.FileName.Split('.').Length - 1];
                //新檔案名稱
                fileName = String.Format("{0}{1:yyyyMMddhhmmsss}.{2}", IDLabel.Text, DateTime.Now, Extension);
                //上傳目錄為/upload/Images/
                RocketProfile.SaveAs(Server.MapPath(String.Format("~/Profile/{0}", fileName)));

            }
            else
            {
                fileName = hiddenP.Value;
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

            string Eid = Request.QueryString["id"];
            string password = FormsAuthentication.HashPasswordForStoringInConfigFile(Pwd.Value, "MD5");
            string AddAcount = System.Web.Configuration.WebConfigurationManager
                .ConnectionStrings["RegistConnectionString"].ToString();
            SqlConnection editConnection = new SqlConnection(AddAcount);
            SqlCommand editCommand =
                new SqlCommand(
                    $"UPDATE member SET Pwd=@Pwd,UserName=@UserName,RocketProfile=@RocketProfile,Permission=@Permission where id=@Eid",
                    editConnection);
            editCommand.Parameters.Add("@Eid", SqlDbType.NVarChar);
            editCommand.Parameters["@Eid"].Value = Request.QueryString["id"];
            editCommand.Parameters.Add("@Pwd", SqlDbType.NVarChar);
            if (Pwd.Value != "")
            {
                editCommand.Parameters["@Pwd"].Value = password;
            }
            else
            {
                editCommand.Parameters["@Pwd"].Value = hidden.Value;
            }

            editCommand.Parameters.Add("@UserName", SqlDbType.NVarChar);
            editCommand.Parameters["@UserName"].Value = UserName.Value;
            editCommand.Parameters.Add("@RocketProfile", SqlDbType.NVarChar);
            editCommand.Parameters["@RocketProfile"].Value = fileName;
            editCommand.Parameters.Add("@Permission", SqlDbType.NVarChar);
            editCommand.Parameters["@Permission"].Value = checkbox;
            editConnection.Open();
            editCommand.ExecuteNonQuery();
            editConnection.Close();
            Response.Redirect("members.aspx");
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("members.aspx");
        }
    }
}