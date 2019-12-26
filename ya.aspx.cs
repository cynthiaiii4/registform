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
    public partial class ya : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlN = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["RegistConnectionString"]
                .ConnectionString;
            SqlConnection memberConnection = new SqlConnection(sqlN); //建立連線通道

            //搜尋字串
            SqlCommand sqlCommand = new SqlCommand("with CTE ([name],[email]) as (SELECT DISTINCT [name],[email] FROM registform group by [name],[email])\r\n\r\nSELECT count(*) from CTE", memberConnection);
            memberConnection.Open();
            SqlDataReader ereader = sqlCommand.ExecuteReader();
            ereader.Read();
            number.Text = ereader[0].ToString();
            ereader.Close();

        }
    }
}