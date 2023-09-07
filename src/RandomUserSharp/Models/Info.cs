using Newtonsoft.Json;

namespace RandomUserSharp.Models
{
    public class Info
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("results")]
        public int Results { get; set; }

        [JsonProperty("seed")]
        public string Seed { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
