using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Awesomium.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SimpleOAuthMail.ModuleInit;
using SimpleOAuthMail.OAuthDataConnections;
using SimpleOAuthMail.Properties;

namespace SimpleOAuthMail.ViewModels
{
    public class AuthorisationViewModel : INotifyPropertyChanged, INavigationAware
    {
        private readonly IUnityContainer _unityContainer;
        private IAuthenticationService _authenticationService;
        private readonly IRegionManager _regionManager;
        private Uri _webAddress;
        private string _emailAddress = string.Empty;
        private string _mailProvider;

        public event PropertyChangedEventHandler PropertyChanged;

        public AuthorisationViewModel(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            _unityContainer = unityContainer;
            _regionManager = regionManager;

            LoadEmailCommand = new DelegateCommand<WebControl>(DoLoadEmail, CanLoadEmail);
        }

        private bool CanLoadEmail(WebControl webControl)
        {
            return true;
        }

        private void DoLoadEmail(WebControl webControl)
        {
            IDictionary<string, string> authenticationData = new Dictionary<string, string>();
            authenticationData.Add("Title", webControl.Title);
            string accessToken;
            if (_authenticationService.TryGetToken(authenticationData, out accessToken))
            {
                LoadMailViewerView(accessToken);
            }
        }

        private void LoadAuthorisationUri()
        {
            Uri authenticationUri = _authenticationService.GetAuthenticationUri(_emailAddress);
            WebAddress = authenticationUri;
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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _mailProvider = navigationContext.Parameters[UnityConstants.NavigationMailProvider].ToString();
            _emailAddress = navigationContext.Parameters[UnityConstants.NavigationEmailAddress].ToString();
            _authenticationService = _unityContainer.Resolve<IAuthenticationService>(_mailProvider);
            LoadAuthorisationUri();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public Uri WebAddress
        {
            get { return _webAddress; }
            set
            {
                _webAddress = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadEmailCommand { get; private set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
