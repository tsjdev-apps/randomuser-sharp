using Newtonsoft.Json;
using System;

namespace RandomUserSharp.Models
{
    public class AgeInfo
    {
        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
