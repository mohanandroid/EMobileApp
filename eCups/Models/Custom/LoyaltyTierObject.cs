using System;
using Newtonsoft.Json;

namespace eCups.Models.Custom
{
    public class LoyaltyTierObject
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("image_url")]
        public string ImgUrl { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("level_name")]
        public string LevelName { get; set; }
        [JsonProperty("reward_breakdown")]
        public string RewardBreakdown { get; set; }
    }
}
