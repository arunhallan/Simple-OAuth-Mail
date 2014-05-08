using System.ComponentModel;
using System.Windows.Input;

namespace SimpleOAuthMail.ViewModels
{
    public interface IWelcomeScreenViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        string EmailAddress { get; set; }
        ICommand SubmitMailProviderCommand { get; }
    }
}