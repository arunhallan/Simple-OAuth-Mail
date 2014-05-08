using System;
using SimpleOAuthMail.OAuthDataConnections.Models;

namespace SimpleOAuthMail.OAuthDataConnections
{
    public interface IAuthenticationService
    {
        Uri GetAuthenticationUri(string emailAddress);
        bool TryGetToken(WebPageData webPageData, out string token);
    }
}