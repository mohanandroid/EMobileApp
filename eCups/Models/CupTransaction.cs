using System;
using Newtonsoft.Json;

namespace eCups.Models
{
    public class CupTransaction
    {
        [JsonProperty("qrcode")]
        public string QRCode { get; set; }

        [JsonProperty("transaction_type")]
        public string TransactionType { get; set; }

        [JsonProperty("outlet_id")]
        public string OutletID { get; set; }

        public string code { get; set; }
        public string id { get; set; }
    }
}
