using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SimpleOAuthMail.ModuleInit;
using SimpleOAuthMail.OAuthDataConnections;
using SimpleOAuthMail.OAuthDataConnections.Models;
using SimpleOAuthMail.Properties;

namespace SimpleOAuthMail.ViewModels
{
    public class MailViewerViewModel : INotifyPropertyChanged, INavigationAware
    {
        private readonly IUnityContainer _unityContainer;
        private IMessageService _messageService;
        private string _mailProviderToken;
        private string _emailAddress;
        private ObservableCollection<ICommonMailMessage> _mailMessages = new ObservableCollection<ICommonMailMessage>();
        public event PropertyChangedEventHandler PropertyChanged;

        public MailViewerViewModel(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void LoadMail()
        {
            TaskScheduler uiTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task<IList<ICommonMailMessage>> loadMailTask =
                            Task.Factory.StartNew(() => _messageService.GetInboxMailMessages(DateTime.Today.AddDays(-1)));

            loadMailTask.ContinueWith(
                            loadedMailMessages =>
                            {
                                MailMessages = new ObservableCollection<ICommonMailMessage>(loadedMailMessages.Result);
                            },
                            uiTaskScheduler) ;
        }
        
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string mailProvider = navigationContext.Parameters[UnityConstants.NavigationMailProvider].ToString();
            _mailProviderToken = navigationContext.Parameters[UnityConstants.NavigationMailProviderToken].ToString();
            _emailAddress = navigationContext.Parameters[UnityConstants.NavigationEmailAddress].ToString();

            _messageService = _unityContainer.Resolve<IMessageService>(mailProvider);
            _messageService.Connect(_emailAddress, _mailProviderToken);

            LoadMail();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
