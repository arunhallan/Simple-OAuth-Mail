namespace SimpleOAuthMail.OAuthDataConnections.Facebook
{
    internal class FacebookDataConnectionConstants
    {
        internal const string AuthenticationUri = "https://www.facebook.com/dialog/oauth?";
        internal const string TokenUri = "https://graph.facebook.com/oauth/access_token?";
        internal const string EmailUri = "https://graph.facebook.com/me/inbox?";
        internal const string RedirectUri = "https://www.facebook.com/connect/login_success.html";
        
        internal const string AccessTokenKey = "access_token";

        internal const string CodeKey = "code";
    }
}
