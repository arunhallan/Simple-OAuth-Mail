using System.Collections.Generic;

namespace SimpleOAuthMail.OAuthDataConnections.Services
{
    public interface IHttpRequestResponseService
    {
        string Post(string uri, List<string> uriPostData);
    }
}
