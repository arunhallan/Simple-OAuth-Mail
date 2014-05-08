namespace SimpleOAuthMail.OAuthDataConnections.Facebook
{
    internal class FacebookDataConnectionConstants
    {
        internal const string AccessTokenKey = "access_token";
        internal const string AuthenticationUri = "https://www.facebook.com/dialog/oauth?";
        internal const string ClientIdParam = "client_id";
        internal const string ClientSecretParam = "client_secret";
        internal const string CodeKey = "code";
        internal const string CodeParam = "code";
        internal const string EmailUri = "https://graph.facebook.com/me/inbox?";
        internal const string RedirectUri = "https://www.facebook.com/connect/login_success.html";
        internal const string RedirectUriParam = "redirect_uri";
        internal const string ScopeParameter = "scope";
        internal const string ScopeTypeReadMailBox = "read_mailbox";
        internal const string TokenUri = "https://graph.facebook.com/oauth/access_token?";
    }
}