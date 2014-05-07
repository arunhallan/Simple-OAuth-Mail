using System.ComponentModel.Composition;
using SimpleOAuthMail.ModuleInit;
using SimpleOAuthMail.ViewModels;

namespace SimpleOAuthMail.Views
{
    /// <summary>
    /// Interaction logic for AuthorisationView.xaml
    /// </summary>
    /// 
    [Export(UnityConstants.AuthorisationView)]
    public partial class AuthorisationView
    {
        public AuthorisationView(AuthorisationViewModel authorisationViewModel)
        {
            InitializeComponent();
            this.DataContext = authorisationViewModel;
        }
    }
}
