using SimpleOAuthMail.OAuthDataConnections.Models;
using System;
using System.Collections.Generic;

namespace SimpleOAuthMail.OAuthDataConnections
{
    public interface IMessageService
    {
        void Connect(string emailAddress, string accessToken);

        IList<ICommonMailMessage> GetInboxMailMessages(DateTime dateToDownloadTo);
    }
}