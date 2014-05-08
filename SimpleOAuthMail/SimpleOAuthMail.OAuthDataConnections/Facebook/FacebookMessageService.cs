using Newtonsoft.Json.Linq;
using SimpleOAuthMail.OAuthDataConnections.Models;
using SimpleOAuthMail.OAuthDataConnections.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SimpleOAuthMail.OAuthDataConnections.Facebook
{
    public class FacebookMessageService : IMessageService
    {
        private readonly IHttpRequestResponseService _httpRequestResponseService;
        private string _accessToken;

        public FacebookMessageService(IHttpRequestResponseService httpRequestResponseService)
        {
            _httpRequestResponseService = httpRequestResponseService;
        }

        public void Connect(string emailAddress, string accessToken)
        {
            _accessToken = accessToken;
        }

        public IList<ICommonMailMessage> GetInboxMailMessages(DateTime dateToDownloadTo)
        {
            NameValueCollection uriData = new NameValueCollection
            {
                {FacebookDataConnectionConstants.AccessTokenKey, _accessToken}
            };

            string response = _httpRequestResponseService.Get(FacebookDataConnectionConstants.EmailUri, uriData);

            JObject jObject = JObject.Parse(response);

            var convertedMessages = FacebookMessageFactory.CreateCommonMailMessages(jObject);

            return convertedMessages;
        }
    }
}