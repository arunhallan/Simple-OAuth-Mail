using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SimpleOAuthMail.OAuthDataConnections.Models;

namespace SimpleOAuthMail.OAuthDataConnections.Facebook
{
    public class FacebookMessageFactory
    {
        public static List<ICommonMailMessage> CreateCommonMailMessages(JObject message)
        {
            List<ICommonMailMessage> commonMailMessages = new List<ICommonMailMessage>();
            
            foreach (var token in message)
            {
                commonMailMessages.Add(new CommonMailMessage(new List<string>{"FacebookTo"}, "FacebookFrom", token.Value.ToString(), "SampleFacebookData"));
            }

            return commonMailMessages;
        }
    }
}
