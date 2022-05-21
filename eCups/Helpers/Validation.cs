using System;
using System.Text;
using System.Text.RegularExpressions;
using eCups.Branding;
using Xamarin.Forms;

namespace eCups.Helpers
{
    public static class Validation
    {
        public static bool IsValidEmail(string email)
        {
            Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex.IsMatch(email);
        }

        public static bool IsValidEmail(Entry entry)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(entry.Text, expression))
            {
                if (Regex.Replace(entry.Text, expression, string.Empty).Length == 0)
                {
                    entry.TextColor = Color.Black;
                    return true;
                }
                else
                {
                    entry.TextColor = Color.Red;
                    return false;
                }
            }
            else
            {
                entry.TextColor = Color.Red;
                return false;
            }
        }


        public static bool IsUsernameValid(string username)
        {
            return true;  
        }

        public static bool IsValidName(string name)
        {
            return true;
        }

        public static bool IsPasswordValid(string password)
        {
            return true;
        }

        public static bool IsPhoneNumberValid(string phonenumber)
        {
            return true;
        }


        public static bool ValidateInput(Entry entry, int maxchars, int minchars, bool exactSize)
        {
            //var entry = (Entry)sender;
            bool inputValid = false;


            // limit input length
            if (entry.Text.Length > maxchars)
            {
                string entryText = entry.Text;

                entryText = entryText.Remove(entryText.Length - 1); // remove last char

                entry.Text = entryText;
            }

            // long enough?
            if (entry.Text.Length < minchars)
            {
                entry.TextColor = Color.Red;
                inputValid = false;
            }
            else
            {
                if (entry.Text.Length < maxchars)
                {
                    if (!exactSize)
                    {
                        entry.TextColor = Color.Black;
                        inputValid = true;
                    }
                    else
                    {
                        entry.TextColor = Color.Red;
                        inputValid = false;
                    }

                }
                else
                {
                    entry.TextColor = Color.Black;
                    inputValid = true;
                }
            }


            return inputValid;
        }

        public static bool ValidateInput(object sender, int maxchars, int minchars, bool exactSize)
        {
            return ValidateInput((Entry)sender, maxchars, minchars, exactSize);
        }
    }
}
