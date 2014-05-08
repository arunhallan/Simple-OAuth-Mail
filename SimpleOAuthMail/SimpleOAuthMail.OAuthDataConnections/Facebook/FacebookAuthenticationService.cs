using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using SimpleOAuthMail.OAuthDataConnections.Models;
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
            NameValueCollection uriParams = new NameValueCollection
            {
                {FacebookDataConnectionConstants.ClientIdParam, _clientId},
                {FacebookDataConnectionConstants.RedirectUriParam, FacebookDataConnectionConstants.RedirectUri},
                {FacebookDataConnectionConstants.ScopeParameter, FacebookDataConnectionConstants.ScopeTypeReadMailBox}
            };

            string uri = _httpRequestResponseService.GetFullGetUri(FacebookDataConnectionConstants.AuthenticationUri, uriParams);
            return new Uri(uri);
        }

        public bool TryGetToken(WebPageData webPageData, out string token)
        {
            token = string.Empty;
            bool success = false;

            Uri uri = new Uri(webPageData.WebPageUri);
            NameValueCollection parsedUriQuery = HttpUtility.ParseQueryString(uri.Query);

            if (parsedUriQuery.AllKeys.Contains(FacebookDataConnectionConstants.CodeKey))
            {
                string code = parsedUriQuery.Get(FacebookDataConnectionConstants.CodeKey);
                token = GetToken(code);
                success = true;
            }

            return success;
        }

        private string GetToken(string code)
        {
            NameValueCollection uriParams = new NameValueCollection
            {
                {FacebookDataConnectionConstants.ClientIdParam, _clientId},
                {FacebookDataConnectionConstants.ClientSecretParam, _clientSecret},
                {FacebookDataConnectionConstants.RedirectUriParam, FacebookDataConnectionConstants.RedirectUri},
                {FacebookDataConnectionConstants.CodeParam, code}
            };

            string response = _httpRequestResponseService.Get(FacebookDataConnectionConstants.TokenUri, uriParams);

            NameValueCollection parsedquery = HttpUtility.ParseQueryString(response);
            string accesscode = parsedquery.Get(FacebookDataConnectionConstants.AccessTokenKey);
            
            return accesscode;
        }
    }
}