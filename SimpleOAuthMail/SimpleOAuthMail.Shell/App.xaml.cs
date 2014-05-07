using System.Windows;

namespace SimpleOAuthMail.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SimpleOAuthMailBootstrapper bootstrapper = new SimpleOAuthMailBootstrapper();
            bootstrapper.Run();
        }
    }
}
