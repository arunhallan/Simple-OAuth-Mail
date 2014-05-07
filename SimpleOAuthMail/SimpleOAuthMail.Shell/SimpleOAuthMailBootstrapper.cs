using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using SimpleOAuthMail.ModuleInit;

namespace SimpleOAuthMail.Shell
{
    public class SimpleOAuthMailBootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(SimpleOAuthMailModuleInit));
        }

        protected override DependencyObject CreateShell()
        {
            SimpleOAuthMailShell view = this.Container.TryResolve<SimpleOAuthMailShell>();
            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }
    }
}
