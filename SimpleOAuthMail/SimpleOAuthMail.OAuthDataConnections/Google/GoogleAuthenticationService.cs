using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using SimpleOAuthMail.OAuthDataConnections.Services;

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
            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("client_id", _clientId);
            nvm.Add("redirect_uri", GoogleDataConnectionConstants.RedirectUri);
            nvm.Add("scope", GoogleDataConnectionConstants.MailScopeUri);
            nvm.Add("response_type", "code");
            nvm.Add("login_hint", emailAddress);

            string uri = _httpRequestResponseService.GetFullGetUri(GoogleDataConnectionConstants.AuthenticationUri, nvm);
            return new Uri(uri);            
        }

        public bool TryGetToken(IDictionary<string, string> authenticationData, out string token)
        {
            token = string.Empty;
            string titleData = authenticationData["Title"];

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
            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("code", code);
            nvm.Add("client_id", _clientId);
            nvm.Add("client_secret", _clientSecret);
            nvm.Add("redirect_uri", GoogleDataConnectionConstants.RedirectUri);
            nvm.Add("cgrant_typeode", "authorization_code");

            string response = _httpRequestResponseService.Post(GoogleDataConnectionConstants.TokenUri, nvm);
            
            // TODO:  errors
            JObject jObject = JObject.Parse(response);

            string accessToken = jObject.SelectToken(GoogleDataConnectionConstants.AccessTokenKey).ToString();

            return accessToken;
        }
    }
}