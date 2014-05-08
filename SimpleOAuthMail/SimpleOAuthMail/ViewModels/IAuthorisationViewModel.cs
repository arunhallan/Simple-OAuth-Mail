using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;

namespace SimpleOAuthMail.ViewModels
{
    public interface IAuthorisationViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        ICommand LoadEmailCommand { get; }
        Uri WebAddress { get; set; }
        bool IsNavigationTarget(NavigationContext navigationContext);
        void OnNavigatedFrom(NavigationContext navigationContext);
        void OnNavigatedTo(NavigationContext navigationContext);
    }
}