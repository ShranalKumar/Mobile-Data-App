using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Views;
using Xamarin.Forms;
using MobileApp.Constants;

namespace MobileApp.Droid
{
    public partial class CustomUserDataUsageView : ContentView
    {
        //public static List<LinearLayout> AppRows;

        public static void GetUserDataUsageRows(Android.Widget.ScrollView parent)
        {
            int AppRows = Controller._appName.Count();
            LinearLayout MainLinear = new LinearLayout(parent.Context);
            MainLinear.Orientation = Orientation.Vertical;
            parent.AddView(MainLinear);

            //get index of this user
            for (int i = 0; i < AppRows; i++)
            {
                LinearLayout AppDataUsageRow = new LinearLayout(MainLinear.Context);
                AppDataUsageRow.Orientation = Orientation.Vertical;

                LinearLayout AppTextDetails = new LinearLayout(AppDataUsageRow.Context);
                AppTextDetails.Orientation = Orientation.Horizontal;

                TextView AppName = new TextView(AppTextDetails.Context);
                AppName.Text = Controller._appName[i];
                AppName.Gravity = GravityFlags.Left;

                TextView AppDataUsage = new TextView(AppTextDetails.Context);
                AppDataUsage.Text = String.Format(StringConstants.Localizable.DataUsageBreakdown, Controller._appUsage[i]);
                AppDataUsage.Gravity = GravityFlags.Right;

                Android.Widget.RelativeLayout AppDataBar = new Android.Widget.RelativeLayout(AppDataUsageRow.Context);

                Android.Widget.ProgressBar AppProgressBar = new Android.Widget.ProgressBar(AppDataBar.Context);

                AppDataBar.AddView(AppProgressBar);
                AppTextDetails.AddView(AppName);
                AppTextDetails.AddView(AppDataUsage);
                AppDataUsageRow.AddView(AppTextDetails);
                AppDataUsageRow.AddView(AppDataBar);
                MainLinear.AddView(AppDataUsageRow);
            }
        }
    }
}