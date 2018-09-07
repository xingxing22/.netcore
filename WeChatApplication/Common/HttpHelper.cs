
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WebApiEnum;

namespace Common
{
    public static class HttpHelper
    {
        public static string Http(string url,string data, MyHttpMethod method)
        {
            string result = string.Empty;
            switch (method)
            {
                case MyHttpMethod.GET:
                    break;
                case MyHttpMethod.POST:
                    result = HttpPost(url, data);
                    break;
            }
            return result;
        }

        private static string HttpPost(string url,string postdata)
        {
            if (string.IsNullOrWhiteSpace(url)) return string.Empty;
            string result = string.Empty;
            //ServicePointManager.ServerCertificateValidationCallback += CheckCertificateValidate;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = MyHttpMethod.POST.ToString();
                request.ContentType = "application/x-www-form-urlencoded";
                //request.ProtocolVersion = HttpVersion.Version11;
                byte[] bytes = Encoding.UTF8.GetBytes(postdata);
                using (Stream str = request.GetRequestStream())
                {
                    str.Write(bytes, 0, bytes.Length);
                }
                using (var res = request.GetResponse())
                {
                    using (var stream = res.GetResponseStream())
                    {
                        StreamReader str = new StreamReader(stream);
                        result = str.ReadToEnd();
                        str.Dispose();
                        str.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return result;
        }

        private static bool CheckCertificateValidate(object sender,X509Certificate cret,X509Chain chain,SslPolicyErrors errors)
        {
            return true;//总是信任https协议
        }
    }
}
