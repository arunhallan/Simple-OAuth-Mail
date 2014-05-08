using System;
using System.Collections.Generic;
using System.Linq;
using ImapX;
using ImapX.Authentication;
using ImapX.Enums;
using SimpleOAuthMail.OAuthDataConnections.Models;

namespace SimpleOAuthMail.OAuthDataConnections.Google
{
    public class GoogleMessageService : IDisposable, IMessageService
    {
        private readonly ImapClient _imapClient;

        public GoogleMessageService(ImapClient imapClient)
        {
            _imapClient = imapClient;
        }

        public void Connect(string emailAddress, string accessToken)
        {
            if (!_imapClient.Connect())
                throw new AccessViolationException("Failed to connect to Google Imap client");
            
            var credentials = new OAuth2Credentials(emailAddress, accessToken);
            
            if (!_imapClient.Login(credentials))
                throw new AccessViolationException("Failed to authenticate to the Google Imap client");   
        }

        public IList<ICommonMailMessage> GetInboxMailMessages(DateTime dateToDownloadFrom)
        {
            string sinceFilter = string.Format(GoogleDataConnectionConstants.SinceImapQuery, 
                                               dateToDownloadFrom.ToString(GoogleDataConnectionConstants.ImapDateFormat));

            var folder = _imapClient.Folders[GoogleDataConnectionConstants.InboxName];
            folder.Messages.Download(sinceFilter, MessageFetchMode.Basic);

            return folder.Messages.Select(GoogleMessageFactory.CreateCommonMailMessage).ToList();
        }

        public void Dispose()
        {
            _imapClient.Dispose();
        }
    }
}
