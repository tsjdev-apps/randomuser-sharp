using Newtonsoft.Json;

namespace RandomUserSharp.Models
{
    public class Name
    {
        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
