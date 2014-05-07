using System.Collections.Specialized;

namespace SimpleOAuthMail.OAuthDataConnections.Services
{
    public interface IHttpRequestResponseService
    {
        string Get(string uri, NameValueCollection uriData);
        string Post(string uri, NameValueCollection uriData);
        string GetFullGetUri(string uri, NameValueCollection uriData);
    }
}
