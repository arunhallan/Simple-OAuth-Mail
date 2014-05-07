using System;
using System.Collections.Generic;
using SimpleOAuthMail.OAuthDataConnections.Models;

namespace SimpleOAuthMail.OAuthDataConnections
{
    public interface IMessageService
    {
        void Connect(string emailAddress, string accessToken);
        IList<ICommonMailMessage> GetInboxMailMessages(DateTime dateToDownloadTo);
    }
}