using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using SimpleOAuthMail.OAuthDataConnections;
using SimpleOAuthMail.OAuthDataConnections.Facebook;
using SimpleOAuthMail.OAuthDataConnections.Models;

namespace SimpleOAuthMail.Shell.Test
{
    public class MockMessageService : IMessageService
    {
        public void Connect(string emailAddress, string accessToken)
        {
        }

        public IList<ICommonMailMessage> GetInboxMailMessages(DateTime dateToDownloadTo)
        {
            string jsonString = File.ReadAllText("JSONSample.txt");
            JObject jsonObject = JObject.Parse(jsonString);
            return FacebookMessageFactory.CreateCommonMailMessages(jsonObject);
        }
    }
}
