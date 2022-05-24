using Newtonsoft.Json;

namespace Argos.Models
{
    public class Response<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("err")]
        public string Error { get; set; }   
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
