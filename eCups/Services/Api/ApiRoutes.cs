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
        public static string UserDetailsUrl = BaseUrl + "userprofile";
        public static string UpdateUserDetailsUrl = BaseUrl + "updateprofile";
        public static string QrCodeUrl = BaseUrl + "qrcode";
        public static string GetLoyaltyLevels = BaseUrl + "loyalty-levels";
        public static string CuptransactionUrl = BaseUrl + "cuptransaction";

        //Shop Related API Calls
        public static string GetCafesUrl = BaseUrl + "fetch-cafes";

        //Deal Related API Calls
        public static string GetDealsUrl = BaseUrl + "fetch-deals";

    }
}
