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
    public partial class login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //如果用asp的textbox，要達到有預設文字，且點了之後要清空，要加這兩行
                username.Attributes.Add("Value", "User name...");
                username.Attributes.Add("onFocus", "this.value=''");
                //用input就不用加
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            alert.Text = "";
            string userid = username.Text;
            //string pw = password.Value;
            string pw = FormsAuthentication.HashPasswordForStoringInConfigFile(password.Value, "MD5");
            string login = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["RegistConnectionString"].ToString();
            SqlConnection loginConnection = new SqlConnection(login);
            SqlCommand loginCommand = new SqlCommand($"SELECT * FROM member WHERE UserID=@UserID and Pwd=@Pwd", loginConnection);
            loginCommand.Parameters.Add("@UserID", SqlDbType.NVarChar);
            loginCommand.Parameters["@UserID"].Value = userid;
            loginCommand.Parameters.Add("@Pwd", SqlDbType.NVarChar);
            loginCommand.Parameters["@Pwd"].Value = pw;
            SqlDataAdapter loginAdapter = new SqlDataAdapter(loginCommand);
            DataTable user = new DataTable();
            loginAdapter.Fill(user);

            if (user.Rows.Count > 0)
            {
                //session的寫法
                //Session["login"] = userid;
                //Session["RocketProfile"] = user.Rows[0]["RocketProfile"];
                //Session["Permission"] = user.Rows[0]["Permission"];
                //Session.Timeout = 60;
                //ticket的寫法
                ticket ticket = new ticket();
                ticket.UserID = user.Rows[0]["UserID"].ToString();
                ticket.Pwd = user.Rows[0]["Pwd"].ToString();
                ticket.UserName = user.Rows[0]["UserName"].ToString();
                ticket.RocketProfile = user.Rows[0]["RocketProfile"].ToString();
                ticket.Permission = user.Rows[0]["Permission"].ToString();

                string userData = JsonConvert.SerializeObject(ticket);//將物件序列化

                SetAuthenTicket(userData, username.Text);
                Response.Redirect("BKindex.aspx");
                

            }
            else
            {
                alert.Text = "登入失敗";
            }
        }

        void SetAuthenTicket(string userData, string userId)
        {
            //宣告一個驗證票,到期時間要注意跟cookie到期時間有沒有衝突
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(3), false, userData);
            //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            //建立Cookie，把票放進去
            HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authenticationcookie.Expires = DateTime.Now.AddHours(3);
            //將Cookie寫入回應
            Response.Cookies.Add(authenticationcookie);
        }
       
    }
}