using System.ComponentModel.Composition;
using SimpleOAuthMail.ModuleInit;
using SimpleOAuthMail.ViewModels;

namespace SimpleOAuthMail.Views
{
    /// <summary>
    /// Interaction logic for MailViewerView.xaml
    /// </summary>
    [Export(UnityConstants.MailViewerView)]
    public partial class MailViewerView
    {
        public MailViewerView(IMailViewerViewModel mailViewerViewModel)
        {
            InitializeComponent();
            this.DataContext = mailViewerViewModel;
        }
    }
}
