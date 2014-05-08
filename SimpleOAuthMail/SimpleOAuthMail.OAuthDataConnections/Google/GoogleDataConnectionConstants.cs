namespace SimpleOAuthMail.OAuthDataConnections.Google
{
    internal class GoogleDataConnectionConstants
    {
        internal const string AccessTokenKey = "access_token";
        internal const string AuthenticationUri = "https://accounts.google.com/o/oauth2/auth?";
        internal const string ClientIdParam = "client_id";
        internal const string ClientSecretParam = "client_secret";
        internal const string CodeParam = "code";
        internal const string GrantTypeAuthorizationCode = "authorization_code";
        internal const string GrantTypeParam = "grant_type";
        internal const string ImapDateFormat = "dd-MMM-yyyy";
        internal const string InboxName = "INBOX";
        internal const string LoginHintParam = "login_hint";
        internal const string MailScopeUri = "https://mail.google.com/";
        internal const string RedirectUri = "urn:ietf:wg:oauth:2.0:oob";
        internal const string RedirectUriParam = "redirect_uri";
        internal const string ResponseTypeCode = "code";
        internal const string ResponseTypeParam = "response_type";
        internal const string ScopeParam = "scope";
        internal const string SinceImapQuery = "SINCE {0}";
        internal const string SuccessCodePrefix = "Success code=";
        internal const string TokenUri = "https://accounts.google.com/o/oauth2/token?"; 
    }
}
