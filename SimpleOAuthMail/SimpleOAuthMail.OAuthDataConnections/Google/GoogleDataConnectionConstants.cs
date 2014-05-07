namespace SimpleOAuthMail.OAuthDataConnections.Google
{
    internal class GoogleDataConnectionConstants
    {
        internal const string AuthenticationUri = "https://accounts.google.com/o/oauth2/auth?";
        internal const string TokenUri = "https://accounts.google.com/o/oauth2/token?";
        internal const string MailScopeUri = "https://mail.google.com/";
        internal const string RedirectUri = "urn:ietf:wg:oauth:2.0:oob";
        
        internal const string SuccessCodePrefix = "Success code=";
        internal const string AccessTokenKey = "access_token";

        internal const string InboxName = "INBOX";
    }
}
