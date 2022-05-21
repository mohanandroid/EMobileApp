using System;
using Newtonsoft.Json;

namespace eCups.Models
{
    public class Location
    {
        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        public Location()
        {
        }
    }
}
