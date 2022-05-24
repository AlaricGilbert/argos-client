using Argos.Api;
using Argos.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Argos.ViewModels
{
    public class QueryByTimePageViewModel : ObservableObject
    {
        private ObservableCollection<TxRecord> records;
        public ObservableCollection<TxRecord> Records
        {
            get { return records; }
            set { SetProperty(ref records, value); }
        }


        private string protocol;
        public string Protocol
        {
            get { return protocol; }
            set { SetProperty(ref protocol, value); }
        }

        private bool withProtocol = false;
        public bool WithProtocol
        {
            get { return withProtocol;}
            set { SetProperty(ref withProtocol, value); }
        }

        private DateTime startTime;
        public DateTime StartTime
        {
            get { return startTime; }
            set { SetProperty(ref startTime, value); }
        }

        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            set { SetProperty(ref endTime, value); }
        }

        private bool withTime = false;
        public bool WithTime
        {
            get { return withTime;}
            set { SetProperty(ref withTime, value); }
        }

        private int selectedIndex = 0;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

        public string Method
        {
            get
            {
                return SelectedIndex == 0 ? "FTE" : "RCE";
            }
        }

        private ICommand queryCommand;
        public ICommand QueryCommand
        {
            get { return queryCommand; }
            set { SetProperty(ref queryCommand, value); }
        }

        private ICommand queryNextCommand;
        public ICommand QueryNextCommand
        {
            get { return queryNextCommand; }
            set { SetProperty(ref queryNextCommand, value); }
        }

        private bool canNextPage = false;
        public bool CanNextPage
        {
            get { return canNextPage; }
            set { SetProperty(ref canNextPage, value); }
        }

        private string previousTxID = "";

        public async void Query()
        {

            var resp = await ArgosClient.GetTxRecordsByTime(new GetTxRecordRequest
            {
                WithTime = withTime,
                StartTime = startTime,
                EndTime = endTime,
                PageSize = 100,
                Protocol = protocol,
                Method = Method,
            });

            Records.Clear();
            CanNextPage = false;
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
            foreach (var item in resp.Data)
            {
                item.Time = DateTimeOffset.FromUnixTimeMilliseconds(item.Timestamp / 1000000).UtcDateTime.ToString();
                Records.Add(item);
            }

            CanNextPage = resp.Data.Count == 100;
            previousTxID = resp.Data.Last().TxId;
            
        }
        public async void QueryNext()
        {

            var resp = await ArgosClient.GetTxRecordsByTime(new GetTxRecordRequest
            {
                WithTime = withTime,
                StartTime = startTime,
                EndTime = endTime,
                PageSize = 100,
                Protocol = protocol,
                Method = Method,
                Previous = previousTxID,
            });

            Records.Clear();
            CanNextPage = false;

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
            foreach (var item in resp.Data)
            {
                item.Time = DateTimeOffset.FromUnixTimeMilliseconds(item.Timestamp / 1000000).UtcDateTime.ToString();
                Records.Add(item);
            }
            CanNextPage = resp.Data.Count == 100;
            previousTxID = resp.Data.Last().TxId;
        }
        public QueryByTimePageViewModel()
        {
            QueryCommand = new RelayCommand(Query);
            QueryNextCommand = new RelayCommand(QueryNext);
            Records = new ObservableCollection<TxRecord>();
        }
    }
}
