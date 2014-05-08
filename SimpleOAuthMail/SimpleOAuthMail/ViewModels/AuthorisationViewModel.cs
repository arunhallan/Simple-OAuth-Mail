using Awesomium.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SimpleOAuthMail.ModuleInit;
using SimpleOAuthMail.OAuthDataConnections;
using SimpleOAuthMail.OAuthDataConnections.Models;
using SimpleOAuthMail.Properties;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SimpleOAuthMail.ViewModels
{
    public class AuthorisationViewModel : INotifyPropertyChanged, INavigationAware, IAuthorisationViewModel
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;
        private IAuthenticationService _authenticationService;
        private string _emailAddress;
        private string _mailProvider;
        private Uri _webAddress;

        public AuthorisationViewModel(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            _unityContainer = unityContainer;
            _regionManager = regionManager;

            LoadEmailCommand = new DelegateCommand<WebControl>(DoLoadEmail, CanLoadEmail);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoadEmailCommand { get; private set; }

        public Uri WebAddress
        {
            get { return _webAddress; }
            set
            {
                _webAddress = value;
                OnPropertyChanged();
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _mailProvider = navigationContext.Parameters[UnityConstants.NavigationMailProvider].ToString();
            _emailAddress = navigationContext.Parameters[UnityConstants.NavigationEmailAddress].ToString();

            InitialiseAuthenticationService();
            LoadAuthorisationUri();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanLoadEmail(WebControl webControl)
        {
            return true;
        }

        private void DoLoadEmail(WebControl webControl)
        {
            WebPageData webPageData = new WebPageData(webControl.Title, webControl.Source.AbsoluteUri);

            string accessToken;
            if (_authenticationService.TryGetToken(webPageData, out accessToken))
            {
                LoadMailViewerView(accessToken);
            }
        }

        private void InitialiseAuthenticationService()
        {
            _authenticationService = _unityContainer.Resolve<IAuthenticationService>(_mailProvider);
        }

        private void LoadAuthorisationUri()
        {
            WebAddress = _authenticationService.GetAuthenticationUri(_emailAddress);
        }

        private void LoadMailViewerView(string accessToken)
        {
            var parameters = new NavigationParameters
            {
                {UnityConstants.NavigationMailProvider, _mailProvider},
                {UnityConstants.NavigationMailProviderToken, accessToken},
                {UnityConstants.NavigationEmailAddress, _emailAddress}
            };
            _regionManager.RequestNavigate(UnityConstants.MainRegion, new Uri(UnityConstants.MailViewerView + parameters, UriKind.Relative));
        }
    }
}