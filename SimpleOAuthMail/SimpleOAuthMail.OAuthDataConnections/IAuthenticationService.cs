using System;
using System.Collections.Generic;

namespace SimpleOAuthMail.OAuthDataConnections
{
    public interface IAuthenticationService
    {
        Uri GetAuthenticationUri(string emailAddress);
        bool TryGetToken(IDictionary<string, string> authenticationData, out string token);
    }
}