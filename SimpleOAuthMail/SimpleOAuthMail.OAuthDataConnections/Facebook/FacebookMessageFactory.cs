using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SimpleOAuthMail.OAuthDataConnections.Models;

namespace SimpleOAuthMail.OAuthDataConnections.Facebook
{
    public class FacebookMessageFactory
    {
        public static List<ICommonMailMessage> CreateCommonMailMessages(JObject message)
        {
            return new List<ICommonMailMessage>();
        }
    }
}
