using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RegistForm
{
    /// <summary>
    /// sent 的摘要描述
    /// </summary>
    public class sent : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            
            string sql1 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["RegistConnectionString"]
                .ConnectionString;
            SqlConnection memberConnection = new SqlConnection(sql1);//建立連線通道
            SqlCommand sendcommand = new SqlCommand($" UPDATE registform SET sent=@sent where id=@Eid", memberConnection);
            sendcommand.Parameters.Add("@Eid", SqlDbType.NVarChar);
            sendcommand.Parameters["@Eid"].Value = context.Request["id"];
            sendcommand.Parameters.Add("@sent", SqlDbType.NVarChar);
            sendcommand.Parameters["@sent"].Value = "寄出囉!";
            memberConnection.Open();
            sendcommand.ExecuteNonQuery();
            memberConnection.Close();
            context.Response.Write("寄出囉!");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}