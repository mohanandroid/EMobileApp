using System;
using Xamarin.Forms;

namespace eCups.Components.Fields
{
    public class CLabel : Label
    {
        string text;

        public void SetPassword(bool password)
        {
            if (password)
            {
                text = Text;

                Text = "";
                foreach (char c in text)
                    Text += "*";
            }
            else
            {
                Text = text;
            }
        }
    }
}
