using Newtonsoft.Json;
using System.Collections.Generic;

namespace RandomUserSharp.Models
{
    public class RootObject
    {
        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("results")]
        public List<User> Users { get; set; }
    }
}
