using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoogleMapsSample
{
    public class GeoCode
    {
        public GeoCode()
        {
        }

        //public Geometry Geometry { get; set; }

        [JsonProperty(PropertyName = "formatted_address")]
        public string FullAddress { get; set; }

        [JsonProperty(PropertyName = "address_components")]
        public List<string> Address { get; set; }
    }
}

