using System.Collections.Generic;

namespace SimpleOAuthMail.OAuthDataConnections.Models
{
    public interface ICommonMailMessage
    {
        string Subject { get; }
        string Body { get; }
        string From { get; }
        IList<string> To { get; }
    }
}