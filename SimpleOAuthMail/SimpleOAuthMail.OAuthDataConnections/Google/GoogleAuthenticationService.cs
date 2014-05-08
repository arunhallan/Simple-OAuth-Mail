using Newtonsoft.Json.Linq;
using SimpleOAuthMail.OAuthDataConnections.Models;
using SimpleOAuthMail.OAuthDataConnections.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SimpleOAuthMail.OAuthDataConnections.Google
{
    public class GoogleAuthenticationService : IAuthenticationService
    {
        
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly IHttpRequestResponseService _httpRequestResponseService;

        public GoogleAuthenticationService(string clientSecret, string clientId, IHttpRequestResponseService httpRequestResponseService)
        {
            _clientSecret = clientSecret;
            _clientId = clientId;
            _httpRequestResponseService = httpRequestResponseService;
        }

        public Uri GetAuthenticationUri(string emailAddress)
        {
            NameValueCollection uriParams = new NameValueCollection
            {
                {GoogleDataConnectionConstants.ClientIdParam, _clientId},
                {GoogleDataConnectionConstants.RedirectUriParam, GoogleDataConnectionConstants.RedirectUri},
                {GoogleDataConnectionConstants.ScopeParam, GoogleDataConnectionConstants.MailScopeUri},
                {GoogleDataConnectionConstants.ResponseTypeParam, GoogleDataConnectionConstants.ResponseTypeCode},
                {GoogleDataConnectionConstants.LoginHintParam, emailAddress}
            };

            string uri = _httpRequestResponseService.GetFullGetUri(GoogleDataConnectionConstants.AuthenticationUri, uriParams);
            return new Uri(uri);
        }

        public bool TryGetToken(WebPageData webPageData, out string token)
        {
            token = string.Empty;
            string titleData = webPageData.WebPageTitle;

            if (!titleData.Contains(GoogleDataConnectionConstants.SuccessCodePrefix))
            {
                return false;
            }

            string code = titleData.Replace(GoogleDataConnectionConstants.SuccessCodePrefix, string.Empty);
            try
            {
                token = GetToken(code);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GetToken(string code)
        {
            NameValueCollection uriParams = new NameValueCollection
            {
                {GoogleDataConnectionConstants.CodeParam, code},
                {GoogleDataConnectionConstants.ClientIdParam, _clientId},
                {GoogleDataConnectionConstants.ClientSecretParam, _clientSecret},
                {GoogleDataConnectionConstants.RedirectUriParam, GoogleDataConnectionConstants.RedirectUri},
                {GoogleDataConnectionConstants.GrantTypeParam, GoogleDataConnectionConstants.GrantTypeAuthorizationCode}
            };

            string response = _httpRequestResponseService.Post(GoogleDataConnectionConstants.TokenUri, uriParams);

            // TODO:  errors
            JObject jObject = JObject.Parse(response);

            string accessToken = jObject.SelectToken(GoogleDataConnectionConstants.AccessTokenKey).ToString();

            return accessToken;
        }
    }
}