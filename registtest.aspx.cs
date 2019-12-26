using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RegistForm
{
    public partial class registtest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }
        public class ReCAPTCHA
        {
            public bool success { get; set; }
            public double score { get; set; }
            public string action { get; set; }
            public DateTime challenge_ts { get; set; }
            public string hostname { get; set; }
        }
        public bool BotRules(ReCAPTCHA data)
        {
            if (data.success && data.score >= 0.5) return true;
            else return false;
        }

        private static string GetJsonContent(string Url)
        {
            try
            {
                string targetURI = Url;
                var request = WebRequest.Create(targetURI);
                request.ContentType = "application/json; charset=utf-8";
                string text;
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    text = sr.ReadToEnd();
                }
                return text;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        //public async Task<IActionResult> VerifyBot(string token)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        string url = string.Format(
        //            "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}",
        //            "6Lc5vHgUAAAAAKAiwQ4IU17cme7WpCBFhO4kTRT7",
        //            token,
        //            HttpContext.Connection.RemoteIpAddress.ToString());
        //        HttpResponseMessage response = await client.GetAsync(url);
        //        var data = await response.Content.ReadAsStringAsync();
        //        if (response.IsSuccessStatusCode && BotRules(JsonConvert.DeserializeObject<ReCAPTCHA>(data))) return Ok(data);
        //    }
        //    return StatusCode((int)HttpStatusCode.InternalServerError, "Internal server error");
        //}
    }

}
        
    
