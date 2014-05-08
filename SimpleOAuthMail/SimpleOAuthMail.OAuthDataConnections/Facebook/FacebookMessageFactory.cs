using System.Collections.Generic;
using System.Linq;
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
                try
                {
                    if (token.Key == "data")
                    {
                        foreach (var tokenL1 in token.Value)
                        {
                            ICommonMailMessage commonMailMessage = ExtractMessageFromJson(tokenL1);

                            if (!string.IsNullOrEmpty(commonMailMessage.Body))
                                commonMailMessages.Add(commonMailMessage);
                        }
                    }
                }
                catch 
                {
                }
            }

            return commonMailMessages;
        }

        /// <summary>
        /// Simple extraction method to extract some meaningful data. In the future, would make this more intelligent.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static ICommonMailMessage ExtractMessageFromJson(JToken token)
        {
            string body = string.Empty;

            JToken toToken = token.Value<JToken>("to");

            List<string> toItems = (from toData in toToken 
                                        from individualTo in toData 
                                            from toItem in individualTo 
                                                select toItem.Value<string>("name")).ToList();

            JToken comments = token.Value<JToken>("comments");
            if (comments != null)
            {
                body = comments.ToString();
            }
            
            
            return new CommonMailMessage(toItems, string.Empty, body, string.Empty);
        }
    }
}
