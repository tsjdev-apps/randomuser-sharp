using Newtonsoft.Json;

namespace RandomUserSharp.Models
{
    public class Timezone
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("offset")]
        public string Offset { get; set; }
    }
}
