using System;
using Newtonsoft.Json;

namespace eCups.Models.Custom
{
    public class Recipe : ICloneable
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("prep_time")]
        public string PrepTime { get; set; }

        [JsonProperty("cooking_time")]
        public string CookingTime { get; set; }

        [JsonProperty("main_image_source")]
        public string MainImageSource { get; set; }

        [JsonProperty("is_favourite")]
        public bool IsFavourite { get; set; }

        [JsonProperty("favourite_rating")]
        public int FavouriteRating { get; set; }

        [JsonProperty("star_rating")]
        public int StarRating { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public User Creator { get; set; }

        public bool IsSelected { get; set; }


        public Recipe()
        {
            Console.WriteLine("New recipe created");


        }

        public object Clone()
        {
            return (User)this.MemberwiseClone();
        }
    }
}
