using Argos.ViewModels;
using System.Windows.Controls;

namespace Argos.Views
{
    /// <summary>
    /// QueryByAddressPage.xaml 的交互逻辑
    /// </summary>
    public partial class QueryByAddressPage : Page
    {
        public QueryByAddressPage()
        {
            InitializeComponent();
            DataContext = new QueryByAddressPageViewModel();
        }
    }
}
