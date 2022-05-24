using Argos.Api;
using Argos.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Argos.ViewModels
{
    public class QueryByAddressPageViewModel : ObservableObject
    {
        private ObservableCollection<TxRecord> records;
        public ObservableCollection<TxRecord> Records
        {
            get { return records; }
            set { SetProperty(ref records, value); }
        }

        private string ipAddress = "";
        public string IpAddress
        {
            get { return ipAddress; }
            set { SetProperty(ref ipAddress, value); }
        }

        private ICommand queryCommand;
        public ICommand QueryCommand
        {
            get { return queryCommand; }
            set { SetProperty(ref queryCommand, value); }
        }

        public async void Query() 
        {
            var resp = await ArgosClient.GetTxRecordsByAddress(IpAddress);
            Records.Clear();
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
                MessageBox.Show($"查询结果为空", "提示");
                return;
            }
            foreach (var item in resp.Data)
            {
                item.Time = DateTimeOffset.FromUnixTimeMilliseconds(item.Timestamp / 1000000).UtcDateTime.ToString();
                Records.Add(item);
            }
        }
            
        public QueryByAddressPageViewModel()
        {
            Records = new ObservableCollection<TxRecord>();
            QueryCommand = new RelayCommand(Query);
        }
    }
}
