namespace SimpleOAuthMail.OAuthDataConnections.Google
{
    internal class GoogleDataConnectionConstants
    {
        internal const string AuthenticationUri = "https://accounts.google.com/o/oauth2/auth?";
        internal const string TokenUri = "https://accounts.google.com/o/oauth2/token";
        internal const string SuccessCodePrefix = "Success code=";

        internal const string AuthenticationUriParams = "client_id={0}&" +
                                                      "redirect_uri=urn:ietf:wg:oauth:2.0:oob&" +
                                                      "scope=https://mail.google.com/&" +
                                                      "response_type=code&" +
                                                      "login_hint={1}";

        internal const string AccessTokenKey = "access_token";

        internal const string InboxName = "INBOX";
    }
}
