using Argos.ViewModels;
using System.Windows.Controls;

namespace Argos.Views
{
    /// <summary>
    /// QueryByTimePage.xaml 的交互逻辑
    /// </summary>
    public partial class QueryByTimePage : Page
    {
        public QueryByTimePage()
        {
            InitializeComponent();
            DataContext = new QueryByTimePageViewModel();
        }
    }
}
