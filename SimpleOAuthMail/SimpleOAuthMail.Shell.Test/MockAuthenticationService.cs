using System;
using SimpleOAuthMail.OAuthDataConnections;
using SimpleOAuthMail.OAuthDataConnections.Models;

namespace SimpleOAuthMail.Shell.Test
{
    public class MockAuthenticationService : IAuthenticationService
    {
        public Uri GetAuthenticationUri(string emailAddress)
        {
            return new Uri("http://127.0.0.1");
        }

        public bool TryGetToken(WebPageData webPageData, out string token)
        {
            token = "sometoken";
            return true;
        }
    }
}
