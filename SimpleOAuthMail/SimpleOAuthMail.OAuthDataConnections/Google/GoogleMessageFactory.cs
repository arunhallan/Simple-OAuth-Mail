using System.Collections.Generic;
using System.Linq;
using ImapX;
using SimpleOAuthMail.OAuthDataConnections.Models;

namespace SimpleOAuthMail.OAuthDataConnections.Google
{
    public class GoogleMessageFactory
    {
        public static ICommonMailMessage CreateCommonMailMessage(Message message)
        {
            string from = message.From.Address;
            string subject = message.Subject;
            
            List<string> to = message.To.Select(n => n.Address).ToList();

            string body = string.Empty;
            if (message.Body != null)
            {
                if (message.Body.HasHtml)
                {
                    body = message.Body.Html;
                }
                else if (message.Body.HasText)
                {
                    body = message.Body.Text;
                }
            }

            return new CommonMailMessage(to, from, body, subject);
        }
    }
}
