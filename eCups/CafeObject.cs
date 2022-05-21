using System;
using eCups.Models;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;

namespace eCups.DatabaseObjects
{
    public class CafeObject
    {
        [JsonProperty("id")]
        public int ID;
        [JsonProperty("coordinates")]
        public Position Coords;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("address")]
        public Address Address;
        [JsonProperty("url")]
        public string URL;
        [JsonProperty("phone")]
        public string Phone;
        [JsonProperty("description")]
        public string Description;
        [JsonProperty("qr_signature")]
        public string QRSignature; //Generate Random SHA256 Hash to use here for QR Cafe Code
    }
}
