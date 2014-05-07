using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SimpleOAuthMail.OAuthDataConnections.Services
{
    public class HttpRequestResponseService : IHttpRequestResponseService
    {
        public string Post(string uri, List<string> uriPostData)
        {
            WebRequest request = WebRequest.Create(uri);

            string uriPostDataString = string.Join("&", uriPostData.ToArray());
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
            try
            {
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
            }
            catch (Exception ex)
            {
                return "Error";
            }


            return responseMessage;
        }
    }
}
