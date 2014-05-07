using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using SimpleOAuthMail.OAuthDataConnections.Services;

namespace SimpleOAuthMail.OAuthDataConnections.Facebook
{
    public class FacebookAuthenticationService : IAuthenticationService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly IHttpRequestResponseService _httpRequestResponseService;

        public FacebookAuthenticationService(string clientId, string clientSecret, IHttpRequestResponseService httpRequestResponseService)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _httpRequestResponseService = httpRequestResponseService;
        }

        public Uri GetAuthenticationUri(string emailAddress)
        {
            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("client_id", _clientId);
            nvm.Add("redirect_uri", FacebookDataConnectionConstants.RedirectUri);
            nvm.Add("scope", "read_mailbox");

            string uri = _httpRequestResponseService.GetFullGetUri(FacebookDataConnectionConstants.AuthenticationUri, nvm);
            return new Uri(uri);
        }

        public bool TryGetToken(IDictionary<string, string> authenticationData, out string token)
        {
            try
            {
                string uriString = authenticationData["Uri"];
                Uri uri = new Uri(uriString);
                var parsedUri = HttpUtility.ParseQueryString(uri.Query);
                if (parsedUri.AllKeys.Contains(FacebookDataConnectionConstants.CodeKey))
                {
                    string code = parsedUri.Get(FacebookDataConnectionConstants.CodeKey);
                    token = GetToken(code);
                    return true;
                }
            }
            catch (Exception)
            {
                token = string.Empty;
                return false;
            }

            token = string.Empty;
            return false;
        }

        private string GetToken(string code)
        {
            NameValueCollection nvm = new NameValueCollection
            {
                {"client_id", _clientId},
                {"client_secret", _clientSecret},
                {"redirect_uri", FacebookDataConnectionConstants.RedirectUri},
                {"code", code}
            };

            string response = _httpRequestResponseService.Get(FacebookDataConnectionConstants.TokenUri, nvm);

            var parsedquery = HttpUtility.ParseQueryString(response);
            string accesscode = parsedquery.Get(FacebookDataConnectionConstants.AccessTokenKey);
            
            return accesscode;
        }
    }
}