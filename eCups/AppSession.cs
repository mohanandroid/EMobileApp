using System;
using System.Collections.Generic;
using eCups.AppData;
using eCups.Models;
using eCups.Models.Custom;

namespace eCups
{
    public static class AppSession
    {
        // The AppSession holds temporary data used by the app
        public static User CurrentUser;

        public static User.Details CurrentUserDetails;
        public static List<Item> TestItems;

        public static int SignUpStage;
        public static bool CupScanned;

        public static string ReturnTimeOfDay()
        {
            int hour = DateTime.Now.Hour;

            if (hour < 0 && hour < 12)
            {
                return "Morning";
            }
            else if (hour >= 12 && hour < 18)
            {
                return "Afternoon";
            }
            return "Evening";
        }


        public static void Init()
        {
            SignUpStage = 0;
            CupScanned = false;

            CurrentUser = new User();

            CurrentUserDetails = new User.Details();

            TestItems = new List<Item>();

            //TestItems = App.ApiBridge.GetItems(CurrentUser).Result;

            CurrentUser.AddPoints(135);
        }
    }
}
