using System.Collections.Generic;

namespace SimpleOAuthMail.OAuthDataConnections.Models
{
    public class CommonMailMessage : ICommonMailMessage
    {
        public CommonMailMessage(IList<string> to, string from, string body, string subject)
        {
            To = to;
            From = from;
            Body = body;
            Subject = subject;
        }

        public string Subject { get; private set; }
        public string Body { get; private set; }
        public string From { get; private set; }
        public IList<string> To { get; private set; }
    }
}
