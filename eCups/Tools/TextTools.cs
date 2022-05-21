using System;
using System.Collections.Generic;
using System.Linq;

namespace eCups.Tools
{
    public class TextTools
    {
        public static string GetFirstNameFromInput(string fullNameText)
        {
            string firstName = "";

            
            if (fullNameText.Count(Char.IsWhiteSpace) > 0)
            {
                string[]subNames = fullNameText.Split(' ');
                firstName = subNames[0];
            }
            return firstName;
        }


        public static List<string> TextToArray(string fullText, char splitby)
        {
            List<string> infoSections = fullText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList<string>();
            return infoSections;
        }


    }
}
