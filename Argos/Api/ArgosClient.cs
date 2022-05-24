using Argos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Argos.Api
{
    public class GetTxRecordRequest
    {
        public string Protocol { get; set; }
        public string Method { get; set; }
        public bool WithTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PageSize { get; set; }
        public string Previous { get; set; }
    }
    public static class ArgosClient
    {
        public const string BaseUri = "http://127.0.0.1:38324";
        
        public static async Task<Response<List<TxRecord>>?> GetTxRecordsByTime(GetTxRecordRequest req)
        {
            string url = $"{BaseUri}/query/time?method={req.Method}&protocol={req.Protocol}&prev={req.Previous}&ps={req.PageSize}";
            if (req.WithTime) {
                url += $"&from={new DateTimeOffset(req.StartTime).UtcTicks * 1000000}&to={new DateTimeOffset(req.EndTime).UtcTicks * 1000000}";
            }
            using var hc = new HttpClient();
            var response = await hc.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Response<List<TxRecord>>>(response);
        }

        public static async Task<Response<List<TxRecord>>?> GetTxRecordsByAddress(string ip)
        {
            string url = $"{BaseUri}/query/ip?ip={ip}";
            using var hc = new HttpClient();
            var response = await hc.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Response<List<TxRecord>>>(response);
        }

        public static async Task<Response<List<TxRecord>>?> GetTxRecordsByTx(string tx)
        {
            string url = $"{BaseUri}/query/tx?txid={tx}";
            using var hc = new HttpClient();
            var response = await hc.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Response<List<TxRecord>>>(response);
        }

        public static async Task<Response<List<SnifferTask>>?> GetTaskList()
        {
            string url = $"{BaseUri}/task/list";
            using var hc = new HttpClient();
            var response = await hc.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Response<List<SnifferTask>>>(response);
        }

        public static async Task<Response<string>> UpdateTask(string prefix, string protocol)
        {
            string url = $"{BaseUri}/task/write?prefix={prefix}&protocol={protocol}";
            using var hc = new HttpClient();
            var m = await hc.PostAsync(url, new StringContent(""));
            var s = await m.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<string>>(s);
        }

        public static async Task<Response<List<int>>> GetReportStatus()
        {
            string url = $"{BaseUri}/status/report";
            using var hc = new HttpClient();
            var response = await hc.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Response<List<int>>>(response);
        }
    }
}
