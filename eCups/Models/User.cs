using System;
using System.Collections.Generic;
using eCups.Models.Custom;
using Newtonsoft.Json;

namespace eCups.Models
{
    public class User : ICloneable
    {
        [JsonProperty("name")]
        public string FirstName { get; set; }

        [JsonProperty("surname")]
        public string MiddleName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("dob")]
        public string DateOfBirth { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("email")]
        public string EmailAddress { get; set; }

        [JsonProperty("phone")]
        public string MobileNumber { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("is_registered")]
        public bool IsRegistered { get; set; }

        [JsonProperty("auth_token")]
        public string AuthToken { get; set; }

        [JsonProperty("reward_points")]
        public int RewardPoints { get; set; }

        [JsonProperty("user_cups")]
        public List<Ecup> MyCups { get; set; }

        [JsonProperty("device_token")]
        public string DeviceToken { get; set; }

        [JsonProperty("device_type")]
        public string DeviceType { get; set; }

        [JsonProperty("address1")]
        public string AddressLine1 { get; set; }
        [JsonProperty("address2")]
        public string AddressLine2 { get; set; }
        [JsonProperty("address3")]
        public string AddressLine3 { get; set; }
        [JsonProperty("postcode")]
        public string AreaCode { get; set; }

        public Ecup ActiveEcup;

        public User()
        {
            RewardPoints = 0;
            Console.WriteLine("User created");
            ActiveEcup = new Ecup("0000000000", DateTime.Now, false);
            MyCups = new List<Ecup>();

            MyCups.Add(ActiveEcup);

            AddPoints(40);


        }

        public void AddPoints(int points)
        {
            RewardPoints += points;
        }

        public object Clone()
        {
            return (User)this.MemberwiseClone();
        }

        public string GetFullName()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }

        public bool error { get; set; }
        public string message { get; set; }
        public Details details { get; set; }

        public class Details
        {
            public int user_id { get; set; }
            public int client_id { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string dob { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public object address3 { get; set; }
            public string postcode { get; set; }
            public DateTime updated_at { get; set; }
            public DateTime created_at { get; set; }
            public int id { get; set; }

            public string auth_token { get; set; }
        }
    }
}
