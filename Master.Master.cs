using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace RegistForm
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //先判斷帳號是否要踢出去
            #region Session寫法
            //if (Session["login"] == null)
            //{
            //    Response.Redirect("login.aspx");
            //}
            ////把帳號人名放在左上
            //username.Text = Session["login"].ToString();

            //RocketProfile.Src = "/Profile/" + Session["RocketProfile"].ToString();
            #endregion

            #region cookies寫法
            if (!HttpContext.Current.User.Identity.IsAuthenticated)//現在是否登入，如果沒有就跳回login page
            {
                Response.Redirect("login.aspx");
            }
            string strTicket = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            ticket UPerson = JsonConvert.DeserializeObject<ticket>(strTicket);
            //把帳號人名放在左上
            username.Text = UPerson.UserID;
            RocketProfile.Src = "/Profile/" + UPerson.RocketProfile;
            #endregion

            //讀取權限決定要show出那些表單
            //session寫法
            //string[] permission = Session["Permission"].ToString().Split(',');
            string[] permission = UPerson.Permission.Split(',');
            StringBuilder menu = new StringBuilder();
            string add = @"class=""javascript:;""";
            foreach (string per in permission)
            {
                switch (per)
                {
                    case "001":
                        if (hiddenyo.Value == per)//hiddenyo是隱藏欄位的值
                        {
                            add = @"class=""active""";
                        }
                        menu.Append(@"<li class=""mt"">" + @"<a " + add + @"href=""members.aspx"">" + @"<i class=""fa fa-users""></i><span>帳號管理</span></a></li>");
                        add = @"class=""javascript:;""";
                        break;
                    case "002":
                        if (hiddenyo.Value == per)
                        {
                            add = @"class=""active""";
                        }
                        menu.Append(@"<li class=""mt"">" + @"<a " + add + @"href=""RegistrationList.aspx"">" + @"<i class=""fa fa-book""></i>報名清單</span></a></li>");
                        add = @"class=""javascript:;""";
                        break;
                    case "003":
                        if (hiddenyo.Value == per)
                        {
                            add = @"class=""active""";
                        }
                        menu.Append(@"<li class=""mt"">" + @"<a " + add + @"href=""Interview.aspx"">" + @"<i class=""fa fa-desktop""></i><span>面試清單</span></a></li>");
                        add = @"class=""javascript:;""";
                        break;
                    //case "004":
                    //    if (hiddenyo.Value == per)
                    //    {
                    //        add = @"class=""active""";
                    //    }
                    //    menu.Append(@"<li class=""mt"">" + @"<a " + add + @"href=""AdmissionList.aspx"">" + @"<i class=""fa fa-dashboard""></i><span>錄取清單</span></a></li>");
                    //    add = @"class=""javascript:;""";
                    //    break;

                }

                Literal1.Text = menu.ToString();
            }
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            #region session寫法
            //Session.Clear();
            //Response.Redirect("~/login.aspx");
            #endregion

            #region cookie寫法
            //把ticket刪掉並導向login頁面
            FormsAuthentication.SignOut();
            Response.Redirect("~/login.aspx");
            #endregion

        }
    }
}