using Argos.ViewModels;
using System.Windows.Controls;
namespace Argos.Views
{
    /// <summary>
    /// QueryByTransactionPage.xaml 的交互逻辑
    /// </summary>
    public partial class QueryByTransactionPage : Page
    {
        public QueryByTransactionPage()
        {
            InitializeComponent();
            DataContext = new QueryByTransactionPageViewModel();
        }
    }
}
