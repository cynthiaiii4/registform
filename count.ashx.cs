using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RegistForm
{
    /// <summary>
    /// count 的摘要描述
    /// </summary>
    public class count : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string sqlN = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["RegistConnectionString"]
                .ConnectionString;
            SqlConnection memberConnection = new SqlConnection(sqlN); //建立連線通道

            //搜尋字串
            SqlCommand sqlCommand = new SqlCommand("with CTE ([name],[email]) as (SELECT DISTINCT [name],[email] FROM registform group by [name],[email])\r\n\r\nSELECT count(*) from CTE", memberConnection);
            memberConnection.Open();
            SqlDataReader ereader = sqlCommand.ExecuteReader();
            ereader.Read();
            string count = ereader[0].ToString();
            ereader.Close();
            context.Response.Write(count);
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