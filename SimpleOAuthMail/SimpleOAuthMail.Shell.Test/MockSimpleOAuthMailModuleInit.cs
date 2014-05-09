using System;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SimpleOAuthMail.ModuleInit;
using SimpleOAuthMail.OAuthDataConnections;
using SimpleOAuthMail.ViewModels;
using SimpleOAuthMail.Views;

namespace SimpleOAuthMail.Shell.Test
{
    public class MockSimpleOAuthMailModuleInit : IModule
    {
        private readonly IUnityContainer _unityContainer;
        private readonly IRegionManager _regionManager;

        public MockSimpleOAuthMailModuleInit(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            _unityContainer = unityContainer;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            ResourceDictionary dictionary = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/SimpleOAuthMail;component/SimpleOAuthMailResourceDictionary.xaml")
            };
            Application.Current.Resources.MergedDictionaries.Add(dictionary);

            _unityContainer.RegisterType<IWelcomeScreenViewModel, WelcomeScreenViewModel>();
            _unityContainer.RegisterType<IAuthorisationViewModel, AuthorisationViewModel>();
            _unityContainer.RegisterType<IMailViewerViewModel, MailViewerViewModel>();

            _regionManager.RegisterViewWithRegion(UnityConstants.MainRegion, () => _unityContainer.Resolve<WelcomeScreenView>());
            _regionManager.RegisterViewWithRegion(UnityConstants.MainRegion, () => _unityContainer.Resolve<AuthorisationView>());
            _regionManager.RegisterViewWithRegion(UnityConstants.MainRegion, () => _unityContainer.Resolve<MailViewerView>());

            // Google
            IAuthenticationService googleAuthenticationService = new MockAuthenticationService();
            IMessageService googleMessageService = new MockMessageService();

            _unityContainer.RegisterInstance(UnityConstants.MailProviderGoogle, googleAuthenticationService, new ContainerControlledLifetimeManager());
            _unityContainer.RegisterInstance(UnityConstants.MailProviderGoogle, googleMessageService, new ContainerControlledLifetimeManager());
        }
    }
}
