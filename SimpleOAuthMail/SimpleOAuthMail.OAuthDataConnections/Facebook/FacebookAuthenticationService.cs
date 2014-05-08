using System;
using System.Collections.Generic;
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
            try
            {
                Uri uri = new Uri(webPageData.WebPageUri);
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
            NameValueCollection uriParams = new NameValueCollection
            {
                {FacebookDataConnectionConstants.ClientIdParam, _clientId},
                {FacebookDataConnectionConstants.ClientSecretParam, _clientSecret},
                {FacebookDataConnectionConstants.RedirectUriParam, FacebookDataConnectionConstants.RedirectUri},
                {FacebookDataConnectionConstants.CodeParam, code}
            };

            string response = _httpRequestResponseService.Get(FacebookDataConnectionConstants.TokenUri, uriParams);

            var parsedquery = HttpUtility.ParseQueryString(response);
            string accesscode = parsedquery.Get(FacebookDataConnectionConstants.AccessTokenKey);
            
            return accesscode;
        }
    }
}