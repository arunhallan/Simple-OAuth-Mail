using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SimpleOAuthMail.OAuthDataConnections.Services
{
    public class HttpRequestResponseService : IHttpRequestResponseService
    {
        public string Get(string uri, NameValueCollection uriData)
        {
            string responseMessage = string.Empty;
            string fullUri = GetFullGetUri(uri, uriData);

            WebRequest webRequest = WebRequest.Create(fullUri);
            webRequest.Credentials = CredentialCache.DefaultCredentials;
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            responseMessage = reader.ReadToEnd();
                        }
                    }
                }
            }

            return responseMessage;
        }

        public string GetFullGetUri(string uri, NameValueCollection uriData)
        {
            return uri + ToQueryString(uriData);
        }

        public string Post(string uri, NameValueCollection uriData)
        {
            WebRequest request = WebRequest.Create(uri);

            string uriPostDataString = ToQueryString(uriData);
            byte[] byteArray = Encoding.UTF8.GetBytes(uriPostDataString);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();
            }

            string responseMessage = string.Empty;

            using (WebResponse response = request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            {
                if (responseStream != null)
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        responseMessage = reader.ReadToEnd();
                    }
                }
            }

            return responseMessage;
        }

        private string ToQueryString(NameValueCollection uriData)
        {
            var array = (from key in uriData.AllKeys
                         from value in uriData.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();

            return string.Join("&", array);
        }
    }
}