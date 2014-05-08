using SimpleOAuthMail.OAuthDataConnections.Models;
using System;

namespace SimpleOAuthMail.OAuthDataConnections
{
    public interface IAuthenticationService
    {
        Uri GetAuthenticationUri(string emailAddress);

        bool TryGetToken(WebPageData webPageData, out string token);
    }
}