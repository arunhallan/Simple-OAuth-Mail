using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SimpleOAuthMail.ModuleInit;
using SimpleOAuthMail.OAuthDataConnections;
using SimpleOAuthMail.OAuthDataConnections.Models;
using SimpleOAuthMail.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SimpleOAuthMail.ViewModels
{
    public class MailViewerViewModel : INotifyPropertyChanged, INavigationAware
    {
        private readonly IUnityContainer _unityContainer;
        private string _emailAddress;
        private bool _isLoadingMail;
        private ObservableCollection<ICommonMailMessage> _mailMessages = new ObservableCollection<ICommonMailMessage>();
        private string _mailProviderToken;
        private IMessageService _messageService;

        public MailViewerViewModel(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsLoadingMail
        {
            get { return _isLoadingMail; }
            set
            {
                _isLoadingMail = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ICommonMailMessage> MailMessages
        {
            get { return _mailMessages; }
            set
            {
                _mailMessages = value;
                OnPropertyChanged();
            }
        }

        public ICommonMailMessage SelectedMailMessage { get; set; }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void LoadMailAync()
        {
            TaskScheduler uiTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task<IList<ICommonMailMessage>> loadMailTask = Task.Factory.StartNew(() => LoadMail());
            loadMailTask.ContinueWith(loadedMailMessages => ProcessMails(loadedMailMessages.Result), uiTaskScheduler);
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string mailProvider = navigationContext.Parameters[UnityConstants.NavigationMailProvider].ToString();
            _mailProviderToken = navigationContext.Parameters[UnityConstants.NavigationMailProviderToken].ToString();
            _emailAddress = navigationContext.Parameters[UnityConstants.NavigationEmailAddress].ToString();

            _messageService = _unityContainer.Resolve<IMessageService>(mailProvider);
            _messageService.Connect(_emailAddress, _mailProviderToken);

            LoadMailAync();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private IList<ICommonMailMessage> LoadMail()
        {
            IsLoadingMail = true;
            return _messageService.GetInboxMailMessages(DateTime.Today.AddDays(-1));
        }

        private void ProcessMails(IList<ICommonMailMessage> loadedMails)
        {
            MailMessages = new ObservableCollection<ICommonMailMessage>(loadedMails);
            IsLoadingMail = false;
        }
    }
}