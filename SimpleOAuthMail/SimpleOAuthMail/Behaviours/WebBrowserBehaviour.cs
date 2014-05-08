using System.Windows;
using System.Windows.Controls;

namespace SimpleOAuthMail.Behaviours
{
    public class BrowserBehavior
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
                "BindableHtml",
                typeof(string),
                typeof(BrowserBehavior),
                new FrameworkPropertyMetadata(OnBindableHtmlChanged));

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetBindableHtml(WebBrowser webBrowser)
        {
            return (string)webBrowser.GetValue(HtmlProperty);
        }

        public static void SetBindableHtml(WebBrowser webBrowser, string value)
        {
            webBrowser.SetValue(HtmlProperty, value);
        }

        public static void OnBindableHtmlChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser webBrowser = dependencyObject as WebBrowser;
            if (webBrowser != null && e.NewValue != null)
                webBrowser.NavigateToString(e.NewValue.ToString());
        }
    }
}
