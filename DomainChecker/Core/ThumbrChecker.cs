using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace DomainChecker.Core
{
    public class ThumbrChecker
    {
        readonly Regex _regex = new Regex(@"form_key"" value=""([\S]+?)""");
        
        public bool Check(string name)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://www.tumblr.com/svc/account/register");

            request.Method = "POST";
            request.Host = "www.tumblr.com";
            request.Accept = "*/*";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.UserAgent = "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";

            var data = new NameValueCollection();
            data["user[email]"] = "sergppv@gmail.com";
            data["user[password]"] = "12354678222";
            data["tumblelog[name]"] = name;
            data["user[age]"] = "";
            data["context"] = "no_referer";
            data["version"] = "STANDART";
            data["follow"] = "";
            data["form_key"] = GetFormId();
            data["seen_suggestion"] = "1";
            data["used_suggestion"] = "0";
            data["action"] = "signup_account";
            data["tracking_url"] = "/";
            data["tracking_version"] = "modal";
            var requestContent = GetRequestData(data);
            using (var stream = request.GetRequestStream())
            {
                var requestBytes = Encoding.UTF8.GetBytes(requestContent);
                stream.Write(requestBytes, 0, requestBytes.Length);
            }

            try
            {
                using (request.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException)
            {
                return false;
            }
            catch(Exception)
            {
                //todo: add error message
                throw new DomainCheckerException("");
            }
        }

        private static string GetRequestData(NameValueCollection content)
        {
            var stringBuilder = new StringBuilder();

            foreach (string key in content.Keys)
            {
                stringBuilder.AppendFormat("{0}={1}&", Uri.EscapeDataString(key), Uri.EscapeDataString(content[key]));
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }

        private string GetFormId()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://www.tumblr.com/");
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";

            string responseContent;

            using (var webResponse = request.GetResponse())
            using (var response = webResponse.GetResponseStream())
            {
                var reader = new StreamReader(response);
                responseContent = reader.ReadToEnd();
            }

            var match = _regex.Match(responseContent);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new AppDomainUnloadedException();
        }
    }
}
