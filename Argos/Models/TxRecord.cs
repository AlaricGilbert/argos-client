using Newtonsoft.Json;

namespace Argos.Models
{
    public class TxRecord
    {

        public string? TxId { get; set; }
        public long Timestamp { get; set; }
        public string Time { get; set; }
        [JsonProperty("source_ip")]
        public string? SourceIP { get; set; }
        public string? Sniffer { get; set; }
        public string? Protocol { get; set; }
        public string? Method { get; set; }
        public string? GeoIP { get; set; }
    }
}
