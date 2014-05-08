namespace SimpleOAuthMail.OAuthDataConnections.Models
{
    public class WebPageData
    {
        public WebPageData(string webPageTitle, string webPageUri)
        {
            WebPageUri = webPageUri;
            WebPageTitle = webPageTitle;
        }

        public string WebPageTitle { get; private set; }
        public string WebPageUri { get; private set; }
    }
}
