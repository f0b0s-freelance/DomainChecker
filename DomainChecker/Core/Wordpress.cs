using System;
using System.Net;

namespace DomainChecker.Core
{
    public class Wordpress
    {
        public bool Check(string name)
        {
            try
            {
                var webClient = new WebClient();
                webClient.Headers.Add("User-Agent",
                                      "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36");
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");

                var resultJson = webClient.UploadString("https://signup.wordpress.com/is-available/blog",
                                                        string.Format("q={0}&ref=signup", name));

                return !resultJson.Contains("\"status\":\"error\"");
            }
            catch (WebException)
            {
                return true;
            }
            catch (Exception exception)
            {
                //todo: fix it
                throw new DomainCheckerException("Something goes wrong", exception);
            }
        }
    }
}
