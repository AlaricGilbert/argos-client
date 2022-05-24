using Argos.Api;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace Argos.ViewModels
{
    public class ChartModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }

        public ChartModel(DateTime dateTime, double value)
        {
            this.DateTime = dateTime;
            this.Value = value;
        }
    }
    public class StatusPageViewModel : ObservableObject
    {
        private SeriesCollection seriesCollection; 
        public SeriesCollection SeriesCollection {
            get { return seriesCollection; } 
            set { SetProperty(ref seriesCollection, value); } 
        }

        public DateTime InitialDateTime { get; set; }
        public Func<double, string> Formatter { get; set; }

        public StatusPageViewModel()
        {
            this.SetChartModelValues();
        }

        private async void SetChartModelValues()
        {
            var dayConfig = Mappers.Xy<ChartModel>()
                               .X(dayModel => dayModel.DateTime.Ticks)
                               .Y(dayModel => dayModel.Value);


            DateTime now = DateTime.Now;

            var values = new ChartValues<ChartModel>();

            var datas = await ArgosClient.GetReportStatus();

            if (datas == null || datas.Code != 200 || datas.Data == null)
                return;

            var totol = datas.Data.Count;

            for (int i = 0; i < totol; i++)
            {
               values.Add(new ChartModel(now.AddMinutes(i - totol), datas.Data[i]));
            }

            this.SeriesCollection = new SeriesCollection(dayConfig)
            {
                new LineSeries()
                {
                    
                    Values = values,
                    Title = "后端收到的每秒报告数（Avg/1min）"
                }
            };

            this.InitialDateTime = now.AddMinutes(-totol);
            this.Formatter = value => new DateTime((long)value).ToString("yyyy-MM:dd HH:mm:ss");
        }
    }
}
