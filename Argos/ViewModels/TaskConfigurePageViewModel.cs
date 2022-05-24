using Argos.Api;
using Argos.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Argos.ViewModels
{
    public class TaskConfigurePageViewModel : ObservableObject
    {
        private ObservableCollection<SnifferTask> tasks;
        public ObservableCollection<SnifferTask> Tasks
        {
            get { return tasks; }
            set { SetProperty(ref tasks, value); }
        }

        private ObservableCollection<string> prefixes;
        public ObservableCollection<string> Prefixes
        {
            get { return prefixes; }
            set { SetProperty(ref prefixes, value); }
        }

        private string protocol;
        public string Protocol
        {
            get { return protocol; }
            set { SetProperty(ref protocol, value); }
        }
        private string prefix;
        public string Prefix
        {
            get { return prefix; }
            set { SetProperty(ref prefix, value); }
        }
        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get { return updateCommand; }
            set { SetProperty(ref updateCommand, value); }
        }
        private ICommand submitCommand;
        public ICommand SubmitCommand
        {
            get { return submitCommand; }
            set { SetProperty(ref submitCommand, value); }
        }

        public async void Submit()
        {
            await ArgosClient.UpdateTask(Prefix, Protocol);
            Update();
        }

        public async void Update()
        {
            Tasks.Clear();
            Prefixes.Clear();
            var resp = await ArgosClient.GetTaskList();
            if (resp == null)
            {
                MessageBox.Show("网络请求失败", "错误");
                return;
            }
            if (resp.Code != 200)
            {
                MessageBox.Show($"请求码不为200：{resp.Error}", "错误");
                return;
            }
            if (resp.Data.Count == 0)
            {
                MessageBox.Show($"任务查询结果为空", "提示");
                return;
            }

            resp.Data.ForEach(x => {
                Prefixes.Add(x.Prefix);
                Tasks.Add(x);
            });
        }

        public TaskConfigurePageViewModel()
        {
            Tasks = new ObservableCollection<SnifferTask>();
            Prefixes = new ObservableCollection<string>();
            Update();
            UpdateCommand = new RelayCommand(Update);
            SubmitCommand = new RelayCommand(Submit);
        }
    }
}
