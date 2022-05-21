using System;
namespace eCups.Services.Api
{
    public class ApiRoutes
    {
        public static string BaseUrl = "https://invokedfunctions.com/ecups/api/auth/";

        //User Related API Calls
        public static string RegisterUrl = BaseUrl + "registercustomer";
        public static string LoginUrl = BaseUrl + "login";
        public static string LogoutUrl = BaseUrl + "logout";
        public static string UserDetailsUrl = BaseUrl + "user-details";
        public static string UpdateUserDetailsUrl = UserDetailsUrl + "/update";

        public static string GetLoyaltyLevels = BaseUrl + "loyalty-levels";

        //Shop Related API Calls
        public static string GetCafesUrl = BaseUrl + "fetch-cafes";

        //Deal Related API Calls
        public static string GetDealsUrl = BaseUrl + "fetch-deals";

    }
}
