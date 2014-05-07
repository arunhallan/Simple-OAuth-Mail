using SimpleOAuthMail.ViewModels;

namespace SimpleOAuthMail.Views
{
    /// <summary>
    /// Interaction logic for WelcomeScreenView.xaml
    /// </summary>
    public partial class WelcomeScreenView 
    {
        public WelcomeScreenView(WelcomeScreenViewModel welcomeScreenViewModel)
        {
            InitializeComponent();
            this.DataContext = welcomeScreenViewModel;
        }
    }
}
