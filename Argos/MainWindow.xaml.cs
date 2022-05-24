using Argos.ViewModels;
using System.Windows;

namespace Argos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(contentFrame, navigationView);
        }
    }
}
