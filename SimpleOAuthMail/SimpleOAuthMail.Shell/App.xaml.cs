using System;
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
            #if DEBUG
                RunInDebugMode();
            #else
                RunInReleaseMode();
            #endif    
        }

        private static void RunInDebugMode()
        {
            SimpleOAuthMailBootstrapper bootstrapper = new SimpleOAuthMailBootstrapper();
            bootstrapper.Run();
        }

        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                SimpleOAuthMailBootstrapper bootstrapper = new SimpleOAuthMailBootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null) return;

            MessageBox.Show(ex.Message);
            Environment.Exit(1);
        }
    }
    
}
