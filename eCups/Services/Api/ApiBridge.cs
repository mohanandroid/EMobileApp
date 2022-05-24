using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using eCups.DatabaseObjects;
using eCups.Models;
using eCups.Models.Custom;
using eCups.Services.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;

namespace eCups.Services
{
    public class ApiBridge
    {
        static HttpClient HttpClient;

        public ApiBridge()
        {
            HttpClient = new HttpClient();
        }

        public static bool ApiConnectionAvailable()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        public async Task<bool> LogIn(string username, string password)
        {
            if (ApiConnectionAvailable())
            {
                try
                {
                    string uri = ApiRoutes.LoginUrl;

                    ResetRequestHeaders(false);

                    JObject jObject = new JObject();

                    jObject.Add("email_address", username);
                    jObject.Add("password", password);
                    jObject.Add("device_token", "mobile");
                    jObject.Add("device_type", "mobile");

                    string jsonString = CleanUpJson(jObject.ToString());

                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await HttpClient.PostAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();


                        var responseresult = JsonConvert.DeserializeObject<User>(result);
                        if (responseresult.details.auth_token != null)
                        {
                            AppSession.CurrentUser.AuthToken = responseresult.details.auth_token;
                        }
                        /*var returnedJObject = JObject.Parse(json);

                        // The below ensures app will not crash in the case of missing keys.
                        if (returnedJObject.TryGetValue("auth_token", out JToken value)) {
                            AppSession.CurrentUser.AuthToken = (string)value;
                            App.ShowAlert("Oh Dear!", "There was an issue with Authenticating your account, please contact support");
                        }*/

                        return true;
                    }
                    else
                    {
                        App.ShowAlert("Oh Dear!", "There was an issue logging you in, please double check your details and try again");
                        return false;
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            ShowConnectionAlert();
            return false;
        }

        public async Task<bool> GetUser()
        {
            if (ApiConnectionAvailable())
            {
                try
                {
                    string uri = ApiRoutes.UserDetailsUrl;

                    ResetRequestHeaders(true);

                    var result = await HttpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    var json = await result.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(json);

                    AppSession.CurrentUser = JsonConvert.DeserializeObject<User>(json);

                    Console.WriteLine($"User : {AppSession.CurrentUser}");

                    return true;
                }
                catch
                {
                    //unkown error
                }
            }

            ShowConnectionAlert();
            return false;
        }

        public async Task<bool> LogOut()
        {
            if (ApiConnectionAvailable())
            {
                try
                {
                    string uri = ApiRoutes.LogoutUrl;

                    ResetRequestHeaders(false);

                    JObject jObject = new JObject();

                    jObject.Add("token", AppSession.CurrentUser.AuthToken);

                    string jsonString = CleanUpJson(jObject.ToString());

                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = await HttpClient.PostAsync(uri, content).ConfigureAwait(false);
                    if (result.IsSuccessStatusCode)
                    {
                        AppSession.CurrentUser = new User();

                        return true;
                    }
                }
                catch
                {
                    //unkown error
                }
            }

            ShowConnectionAlert();

            return false;
        }

        public async Task<User> Register(User user)
        {
            if (ApiConnectionAvailable())
            {
                try
                {
                    string uri = ApiRoutes.RegisterUrl;

                    var jsonString = JsonConvert.SerializeObject(user);

                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await HttpClient.PostAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        var responseresult = JsonConvert.DeserializeObject<User>(result);

                        return responseresult;
                    }
                }
                catch
                {
                    //unkown error
                }
            }

            ShowConnectionAlert();

            return null;
        }

        public async Task<bool> UpdateUserDetails(User user)
        {
            if (ApiConnectionAvailable())
            {
                try
                {
                    string uri = ApiRoutes.UpdateUserDetailsUrl;

                    ResetRequestHeaders(true);

                    JObject jObject = (JObject)JsonConvert.SerializeObject(user);

                    Console.WriteLine($"Registering : {jObject}");

                    string jsonString = CleanUpJson(jObject.ToString());

                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = await HttpClient.PutAsync(uri, content).ConfigureAwait(false);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();

                        AppSession.CurrentUser = JsonConvert.DeserializeObject<User>(json);

                        return true;
                    }
                }
                catch
                {
                    //unkown error
                }
            }

            ShowConnectionAlert();

            return false;
        }

        public async Task<bool> AddNewECup(Ecup cup)
        {
            if (ApiConnectionAvailable())
            {
                try
                {
                    string uri = ApiRoutes.UpdateUserDetailsUrl;

                    ResetRequestHeaders(true);

                    JObject jObject = (JObject)JsonConvert.SerializeObject(cup);

                    Console.WriteLine($"Registering : {jObject}");

                    string jsonString = CleanUpJson(jObject.ToString());

                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = await HttpClient.PostAsync(uri, content).ConfigureAwait(false);

                    if (result.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
                catch
                {
                    //unkown error
                }
            }

            ShowConnectionAlert();

            return false;
        }

        public async Task<List<LoyaltyTierObject>> GetLevels()
        {
            if (ApiConnectionAvailable())
            {
                try
                {
                    string uri = ApiRoutes.GetLoyaltyLevels;

                    ResetRequestHeaders(true);

                    var result = await HttpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        var jObject = JObject.Parse(json);

                        List<LoyaltyTierObject> loyalties = new List<LoyaltyTierObject>();
                        JArray tiers = (JArray)jObject.SelectToken("loyalty_tiers");

                        foreach (JObject tier in tiers)
                        {
                            loyalties.Add(JsonConvert.DeserializeObject<LoyaltyTierObject>(tier.ToString()));
                        }

                        Console.WriteLine($"User : {AppSession.CurrentUser}");

                        return loyalties;
                    }
                }
                catch
                {
                    //unkown error
                }
            }

            ShowConnectionAlert();
            return null;
        }

        public async Task<List<CafeObject>> GetShops(double lat, double lon)
        {
            if (ApiConnectionAvailable())
            {
                try
                {
                    string uri = ApiRoutes.UserDetailsUrl;

                    ResetRequestHeaders(true);

                    dynamic jObject = new JObject();

                    jObject.latitude = lat;
                    jObject.longitude = lon;

                    string jsonString = CleanUpJson(jObject.ToString());

                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = await HttpClient.PostAsync(uri, content).ConfigureAwait(false);
                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        var data = JObject.Parse(json);

                        List<CafeObject> cafes = new List<CafeObject>();
                        JArray cafesArray = (JArray)data.SelectToken("cafes");

                        foreach (JObject cafe in cafesArray)
                        {
                            cafes.Add(JsonConvert.DeserializeObject<CafeObject>(cafe.ToString()));
                        }

                        Console.WriteLine($"User : {AppSession.CurrentUser}");

                        return cafes;
                    }
                }
                catch
                {
                    //unkown error
                }
            }

            ShowConnectionAlert();
            return null;
        }

        public async Task<List<DealObject>> GetDeals(double lat, double lon)
        {
            if (ApiConnectionAvailable())
            {
                try
                {
                    string uri = ApiRoutes.UserDetailsUrl;

                    ResetRequestHeaders(true);

                    dynamic jObject = new JObject();

                    jObject.latitude = lat;
                    jObject.longitude = lon;

                    string jsonString = CleanUpJson(jObject.ToString());

                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = await HttpClient.PostAsync(uri, content).ConfigureAwait(false);
                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        var data = JObject.Parse(json);

                        List<DealObject> deals = new List<DealObject>();
                        JArray dealsArray = (JArray)data.SelectToken("deals");

                        foreach (JObject deal in dealsArray)
                        {
                            deals.Add(JsonConvert.DeserializeObject<DealObject>(deal.ToString()));
                        }

                        Console.WriteLine($"User : {AppSession.CurrentUser}");

                        return deals;
                    }
                }
                catch
                {
                    //unkown error
                }
            }

            ShowConnectionAlert();
            return null;
        }

        string CleanUpJson(string dirty)
        {
            string clean = dirty;

            clean = clean.Replace("{{", "{");
            clean = clean.Replace("}}", "}");
            clean = clean.Replace("\n", "");
            clean = clean.Replace(@"\", "");
            return clean;

        }

        void ResetRequestHeaders(bool needsBearer)
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            if (needsBearer)
            {
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + AppSession.CurrentUser.AuthToken);
            }
        }

        void ShowConnectionAlert()
        {
            App.ShowAlert("No Connection!", "Please check your wifi or mobile data connection then try again");
        }
    }
}
