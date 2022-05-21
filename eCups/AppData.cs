using System;
using eCups.AppData;
using eCups.Models;

namespace eCups
{
    public static class AppDatas
    {
        // The AppData holds and manages data used by the app
        public static User CurrentUser;

        public static void Init()
        {
            CurrentUser = new User();
        }
    }
}