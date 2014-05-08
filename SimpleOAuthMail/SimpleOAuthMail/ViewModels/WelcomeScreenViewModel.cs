using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using SimpleOAuthMail.ModuleInit;
using SimpleOAuthMail.Properties;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SimpleOAuthMail.ViewModels
{
    public class WelcomeScreenViewModel : INotifyPropertyChanged
    {
        private const string EmailSeperatorAt = "@";
        private const string EmailSeperatorDot = ".";
        private readonly IRegionManager _regionManager;
        private string _emailAddress = string.Empty;

        public WelcomeScreenViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SubmitMailProviderCommand = new DelegateCommand<string>(OnSubmitMailProvider, CanSubmitMailProvider);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                OnPropertyChanged();
                RaiseCanExecuteChanged();
            }
        }

        public ICommand SubmitMailProviderCommand { get; private set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanSubmitMailProvider(string mailProvider)
        {
            return IsValidEmailAddress();
        }

        private bool IsValidEmailAddress()
        {
            int atSign = EmailAddress.IndexOf(EmailSeperatorAt);
            if (atSign == -1)
                return false;

            int dot = EmailAddress.LastIndexOf(EmailSeperatorDot);
            if (dot == -1)
                return false;

            return atSign < dot;
        }

        private void LoadAuthorisationView(string mailProvider)
        {
            var parameters = new NavigationParameters
            {
                {UnityConstants.NavigationMailProvider, mailProvider},
                {UnityConstants.NavigationEmailAddress, _emailAddress}
            };

            _regionManager.RequestNavigate(UnityConstants.MainRegion, new Uri(UnityConstants.AuthorisationView + parameters, UriKind.Relative));
        }

        private void OnSubmitMailProvider(string mailProvider)
        {
            LoadAuthorisationView(mailProvider);
        }

        private void RaiseCanExecuteChanged()
        {
            DelegateCommand<string> command = SubmitMailProviderCommand as DelegateCommand<string>;
            if (command != null) command.RaiseCanExecuteChanged();
        }
    }
}