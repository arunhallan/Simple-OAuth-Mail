using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;

namespace SimpleOAuthMail.Shell.Test
{
    public class MockSimpleOAuthMailBootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(MockSimpleOAuthMailModuleInit));
        }

        protected override DependencyObject CreateShell()
        {
            SimpleOAuthMailShell view = this.Container.TryResolve<SimpleOAuthMailShell>();
            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Window window = ((Window) this.Shell);
            window.Show();
        }
    }
}
