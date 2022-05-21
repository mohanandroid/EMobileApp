using System;
using Newtonsoft.Json;

namespace eCups.Models.Custom
{
    public class Ecup
    {
        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }
        [JsonProperty("registered")]
        public bool IsRegistered { get; set; }
        [JsonProperty("registered_date")]
        public DateTime RegisteredDate { get; set; }

        public Ecup(string serialNumber, DateTime registeredDate, bool isRegistered)
        {
            SerialNumber = serialNumber;
            RegisteredDate = registeredDate;
            IsRegistered = isRegistered;

        }
    }
}
