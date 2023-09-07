using Newtonsoft.Json;
using RandomUserSharp.Utils;
using System;

namespace RandomUserSharp.Models
{
    public class PictureInfo
    {
        [JsonProperty("thumbnail")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Thumbnail { get; set; }

        [JsonProperty("medium")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Medium { get; set; }

        [JsonProperty("large")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Large { get; set; }
    }
}
