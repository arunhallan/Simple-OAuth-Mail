using System.Windows;

namespace SimpleOAuthMail.Shell.Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MockSimpleOAuthMailBootstrapper bootstrapper = new MockSimpleOAuthMailBootstrapper();
            bootstrapper.Run();
        }
    }
}
