using System;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;

namespace eCups.Models
{
    public class DealObject
    {
        [JsonProperty("id")]
        public int ID;
        [JsonProperty("coordinates")]
        public Position Coords;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("address")]
        public Address Address;
        [JsonProperty("paragraph")]
        public string Para;
    }
}
