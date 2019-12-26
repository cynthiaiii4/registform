using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using System.Web.SessionState;

namespace RegistForm
{
    /// <summary>
    /// verification 的摘要描述
    /// </summary>
    public class verification : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            //驗證
            
           // bool verification = false;
            string token = context.Request["hiddenToken"];
            string tokencContent = PostJsonContent(token);
            //取得隱藏欄位token

            registform.ResponseToken responseToken = JsonConvert.DeserializeObject<registform.ResponseToken>(tokencContent);
            context.Session["verification"] = responseToken.success;
           // verification = bool.Parse(context.Session["verification"].ToString());
            context.Response.Write(context.Session["verification"]);
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private static string PostJsonContent(string token)
        {
            string key = "6Lf6pL0UAAAAAM0HGyvWdmMeD4SmWr0G1u9dRur7";
            try
            {
                WebRequest request = HttpWebRequest.Create("https://www.google.com/recaptcha/api/siteverify");
                request.Method = "POST";
                //使用 application/x-www-form-urlencoded
                request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

                //要傳送的資料內容(依字串表示)
                string postData = $"secret=6Lf6pL0UAAAAAM0HGyvWdmMeD4SmWr0G1u9dRur7&response={token}";
                //將傳送的字串轉為 byte array
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                //告訴 server content 的長度
                request.ContentLength = byteArray.Length;
                //將 byte array 寫到 request stream 中 
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(byteArray, 0, byteArray.Length);
                }
                using (var httpResponse = (HttpWebResponse)request.GetResponse())
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }

            }

            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}