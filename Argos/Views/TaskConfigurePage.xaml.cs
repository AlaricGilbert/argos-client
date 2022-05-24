using Argos.ViewModels;
using System.Windows.Controls;

namespace Argos.Views
{
    /// <summary>
    /// TaskConfigurePage.xaml 的交互逻辑
    /// </summary>
    public partial class TaskConfigurePage : Page
    {
        public TaskConfigurePage()
        {
            InitializeComponent();
            DataContext = new TaskConfigurePageViewModel();
        }
    }
}
