using Argos.ViewModels;
using System.Windows.Controls;

namespace Argos.Views
{
    /// <summary>
    /// StatusPage.xaml 的交互逻辑
    /// </summary>
    public partial class StatusPage : Page
    {
        public StatusPage()
        {
            InitializeComponent();
            this.DataContext = new StatusPageViewModel();
        }
    }
}
