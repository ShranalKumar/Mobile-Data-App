using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileApp.Droid.Helpers
{
    public class DataAmountSelectorHelper
    {
        public void IncreaseSelector(int imageButtonId, TextView textView)
        {
            int num = Int32.Parse(textView.Text);

            if (num < 9) { num++; }
            else if (num == 9) { num = 0; }

            textView.Text = num.ToString();
        }

        public void DecreaseSelector(int imageButtonId, TextView textView)
        {
            int num = Int32.Parse(textView.Text);

            if (num > 0) { num--; }
            else if (num == 0) { num = 9; }

            textView.Text = num.ToString();
        }
    }
}