using System;
using Newtonsoft.Json;

namespace eCups.Models
{
    public class Address
    {
        [JsonProperty("address1")]
        public string AddressLine1 { get; set; }
        [JsonProperty("address2")]
        public string AddressLine2 { get; set; }
        [JsonProperty("address3")]
        public string AddressLine3 { get; set; }
        [JsonProperty("address_line_4")]
        public string AddressLine4 { get; set; }
        [JsonProperty("postcode")]
        public string AreaCode { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("county")]
        public string County { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }

        public Location Location { get; set; }

        public Address()
        {

        }

        public string FormattedAddress()
        {
            string addressLine2 = "";

            if (!string.IsNullOrEmpty(AddressLine2))
            {
                addressLine2 = "\n" + AddressLine2;
            }

            return string.Format("{0}{1}{2}{3}", AddressLine1, addressLine2, "\n"+City, "\n"+AreaCode);
        }
    }
}
