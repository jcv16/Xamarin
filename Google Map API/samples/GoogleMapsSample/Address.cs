using System;
using Newtonsoft.Json;

namespace GoogleMapsSample
{
    public class Address
    {
        public Address()
        {
        }

        [JsonProperty(PropertyName = "long_name")]
        public string LongName { get; set; }

        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "types")]
        public string Types { get; set; }
    }
}

