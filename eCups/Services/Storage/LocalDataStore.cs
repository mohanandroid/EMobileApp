using System;
using eCups.Models;
using Newtonsoft.Json;

namespace eCups.Services.Storage
{
    public static class LocalDataStore
    {
        public static void Init()
        {

        }

        public static void Save(string key, Object data)
        {
            Xamarin.Forms.Application.Current.Properties[key] = JsonConvert.SerializeObject(data);
        }

        public static void Save(string key, string data)
        {
            Console.WriteLine("Saved: " + key + " : " + data);
            Xamarin.Forms.Application.Current.Properties[key] = data;
        }

        public static string Load(string key)
        {
            string data = (string)Xamarin.Forms.Application.Current.Properties[key];
            Console.WriteLine("Loaded: " + key + " : " + data);
            return data;
        }

        public static void SaveAll()
        {
            Save("user", AppSession.CurrentUser);
        }

        public static User LoadUser()
        {
            try
            {
                return JsonConvert.DeserializeObject<User>(Load("user"));
            }
            catch (Exception e)
            {
                Console.WriteLine("No user saved");
            }
            return null;
        }

    }
}
