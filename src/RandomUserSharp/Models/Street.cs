using Newtonsoft.Json;

namespace RandomUserSharp.Models
{
    public class Street
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }
    }
}
