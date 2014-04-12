using System;
using System.Net;

namespace DomainChecker.Core
{
    public class BlogspotChecker
    {
        public bool Check(string name)
        {
            try
            {
                var webClient = new WebClient();
                var content = webClient.DownloadString(string.Format("http://{0}.blogspot.ru", name));
                return false;
            }
            catch (WebException)
            {
                return true;
            }
            catch(Exception exception)
            {
                //todo: fix it
                throw new DomainCheckerException("Something goes wrong", exception);
            }
        }
    }
}
