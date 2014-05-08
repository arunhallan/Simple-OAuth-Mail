using System;
using System.Configuration;
using System.Windows;
using ImapX;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SimpleOAuthMail.OAuthDataConnections;
using SimpleOAuthMail.OAuthDataConnections.Facebook;
using SimpleOAuthMail.OAuthDataConnections.Google;
using SimpleOAuthMail.OAuthDataConnections.Services;
using SimpleOAuthMail.ViewModels;
using SimpleOAuthMail.Views;

namespace SimpleOAuthMail.ModuleInit
{
    public class SimpleOAuthMailModuleInit : IModule
    {
        private readonly IUnityContainer _unityContainer;
        private readonly IRegionManager _regionManager;

        public SimpleOAuthMailModuleInit(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            _unityContainer = unityContainer;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            string googleClientId = ConfigurationManager.AppSettings.Get("GoogleClientId");
            string googleClientSecret = ConfigurationManager.AppSettings.Get("GoogleClientSecret");
            string googleImapClientAddress = ConfigurationManager.AppSettings.Get("GoogleImapClientAddress");

            string facebookClientId = ConfigurationManager.AppSettings.Get("FacebookClientId");
            string facebookClientSecret = ConfigurationManager.AppSettings.Get("FacebookClientSecret");

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
            IAuthenticationService googleAuthenticationService = new GoogleAuthenticationService(googleClientSecret, googleClientId, new HttpRequestResponseService());
            ImapClient googleImapClient = new ImapClient(googleImapClientAddress, true);
            IMessageService googleMessageService = new GoogleMessageService(googleImapClient);

            _unityContainer.RegisterInstance(UnityConstants.MailProviderGoogle, googleAuthenticationService, new ContainerControlledLifetimeManager());
            _unityContainer.RegisterInstance(UnityConstants.MailProviderGoogle, googleMessageService, new ContainerControlledLifetimeManager());

            // Facebook
            IAuthenticationService facebookAuthenticationService = new FacebookAuthenticationService(facebookClientId, facebookClientSecret, new HttpRequestResponseService());
            IMessageService facebookMessageService = new FacebookMessageService(new HttpRequestResponseService());

            _unityContainer.RegisterInstance(UnityConstants.MailProviderFacebook, facebookAuthenticationService, new ContainerControlledLifetimeManager());
            _unityContainer.RegisterInstance(UnityConstants.MailProviderFacebook, facebookMessageService, new ContainerControlledLifetimeManager());
        }
    }
}
