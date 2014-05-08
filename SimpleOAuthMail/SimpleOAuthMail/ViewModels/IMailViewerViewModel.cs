using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Prism.Regions;
using SimpleOAuthMail.OAuthDataConnections.Models;

namespace SimpleOAuthMail.ViewModels
{
    public interface IMailViewerViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        bool IsLoadingMail { get; set; }
        ObservableCollection<ICommonMailMessage> MailMessages { get; set; }
        ICommonMailMessage SelectedMailMessage { get; set; }
        bool IsNavigationTarget(NavigationContext navigationContext);
        void LoadMailAsync();
        void OnNavigatedFrom(NavigationContext navigationContext);
        void OnNavigatedTo(NavigationContext navigationContext);
    }
}