using System;
using System.Collections.Generic;
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
            List<string> uriPostData = new List<string>
            {
                "client_id=" + _clientId,
                "redirect_uri=urn:ietf:wg:oauth:2.0:oob",
                "scope=https://mail.google.com/",
                "response_type=code",
                "login_hint=" + emailAddress
            };

            Uri uri = new Uri(GoogleDataConnectionConstants.AuthenticationUri + string.Join("&", uriPostData.ToArray()));
            return uri;
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
            List<string> uriPostData = new List<string>
            {
                "code=" + code,
                "client_id=" + _clientId,
                "client_secret=" + _clientSecret,
                "redirect_uri=urn:ietf:wg:oauth:2.0:oob",
                "grant_type=authorization_code"
            };

            //Posting the Data to Google
            string response = _httpRequestResponseService.Post(GoogleDataConnectionConstants.TokenUri, uriPostData);

            JObject jObject = JObject.Parse(response);

            string accessToken = jObject.SelectToken(GoogleDataConnectionConstants.AccessTokenKey).ToString();

            return accessToken;
        }
    }
}
