using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using SimpleOAuthMail.ModuleInit;
using SimpleOAuthMail.Properties;

namespace SimpleOAuthMail.ViewModels
{
    public class WelcomeScreenViewModel : INotifyPropertyChanged
    {
        private readonly IRegionManager _regionManager;
        private string _emailAddress = string.Empty;

        private const string EmailSeperatorAt = "@";
        private const string EmailSeperatorDot = ".";

        public event PropertyChangedEventHandler PropertyChanged;

        public WelcomeScreenViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SubmitMailProviderCommand = new DelegateCommand<string>(OnSubmitMailProvider, CanSubmitMailProvider);
        }

        public ICommand SubmitMailProviderCommand { get; private set; }

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

        private void OnSubmitMailProvider(string mailProvider)
        {
            LoadAuthorisationView(mailProvider);
        }

        private bool CanSubmitMailProvider(string mailProvider)
        {
            return EmailAddress.Contains(EmailSeperatorAt) &&
                   EmailAddress.Contains(EmailSeperatorDot);
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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RaiseCanExecuteChanged()
        {
            DelegateCommand<string> command = SubmitMailProviderCommand as DelegateCommand<string>;
            if (command != null) command.RaiseCanExecuteChanged();
        }
    }
}
