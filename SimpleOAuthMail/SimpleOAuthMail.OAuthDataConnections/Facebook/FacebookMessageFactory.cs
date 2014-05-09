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

            return commonMailMessages;
        }

        /// <summary>
        /// Simple extraction method to extract some meaningful data. In the future, would make this more intelligent.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static ICommonMailMessage ExtractMessageFromJson(JToken token)
        {
            JToken toToken = token.Value<JToken>("to");

            List<string> toItems = (from toData in toToken 
                                        from individualTo in toData 
                                            from toItem in individualTo 
                                                select toItem.Value<string>("name")).ToList();

            List<string> bodyComments = new List<string>();
            JToken comments = token.Value<JToken>("comments");
            if (comments != null)
            {
                var commentProperties = (from comment in comments 
                                                from subComment in comment 
                                                    from subSubComment in subComment 
                                                        select (subSubComment));

                foreach (var commentProperty in commentProperties)
                {
                    if (!(commentProperty is JProperty))
                    {
                        string message = commentProperty.Value<string>("message");
                        JObject fromObject = commentProperty.Value<JObject>("from");
                        if (fromObject != null)
                        {
                            string messageFrom = fromObject.Value<string>("name");
                            message = string.Format("{0}:\n{1}", messageFrom, message);
                        }

                        bodyComments.Add(message);
                    }
                }

            }

            string body = string.Join("\n", bodyComments);
            return new CommonMailMessage(toItems, string.Empty, body, string.Empty);
        }
    }
}
