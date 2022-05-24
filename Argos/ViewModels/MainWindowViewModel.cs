using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Frame = System.Windows.Controls.Frame;

namespace Argos.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly Frame frame;
        private readonly NavigationView navigationView;
        private Stack<object?> selectHistory;
        private bool skipNavigate = false;
        private bool backEnabled;
        public bool BackEnabled
        {
            get { return backEnabled; }
            set { SetProperty(ref backEnabled, value); }
        }

        private ICommand? navigatedCommand;

        public ICommand? NavigatedCommand
        {
            get { return navigatedCommand; }
            set { SetProperty(ref navigatedCommand, value); }
        }

        private ICommand? goBackCommand;
        public ICommand? GoBackCommand
        {
            get { return goBackCommand; }
            set { SetProperty(ref goBackCommand, value); }
        }

        private ICommand? navigateCommand;
        public ICommand? NavigateCommand
        {
            get { return navigateCommand; }
            set { SetProperty(ref navigateCommand, value); }
        }

        private void UpdateBackEnabled() => BackEnabled = frame.CanGoBack;
        private void GoBack()
        {
            frame.GoBack();
            selectHistory.Pop();
            skipNavigate = true;
            navigationView.SelectedItem = selectHistory.Peek();
            skipNavigate = false;
        }

        private void Navigate(NavigationViewSelectionChangedEventArgs? args)
        {
            if(skipNavigate) return;
            string? tag = (string?)((NavigationViewItem?)args?.SelectedItem)?.Tag;
            if (tag != null)
            {
                selectHistory.Push(args?.SelectedItem);
                frame.Navigate(new Uri($"pack://application:,,,/Views/{tag}Page.xaml"), UriKind.Absolute);
            }
        }

        public MainWindowViewModel(Frame frame, NavigationView navigationView)
        {
            this.frame = frame;
            this.navigationView = navigationView;
            selectHistory = new Stack<object?>();
            NavigatedCommand = new RelayCommand(UpdateBackEnabled);
            NavigateCommand = new RelayCommand<NavigationViewSelectionChangedEventArgs>(Navigate);
            GoBackCommand = new RelayCommand(GoBack);
        }


    }
}
