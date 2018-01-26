﻿using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Views;
using Xamarin.Forms;
using MobileApp.Constants;

namespace MobileApp.Droid.Helpers
{
	public partial class CustomUserDataUsageView : ContentView
	{
		public static void GetUserDataUsageRows(Android.Widget.ScrollView parent, string key)
		{
			LinearLayout MainLinear = new LinearLayout(parent.Context);
            MainLinear.SetPadding(0, 25, 0, 0); 
			List<string> Appnames = Controller._appName[key];
			List<int> AppUsages = Controller._appUsage[key].ConvertAll(int.Parse);
            AppUsages.Sort();
            AppUsages.Reverse();
            int indextOfUser = Controller._firstname.IndexOf(key);
			MainLinear.Orientation = Orientation.Vertical;
			parent.AddView(MainLinear);

			for (int i = 0; i < Appnames.Count; i++)
			{
                ContextThemeWrapper newAppDataUsageRowContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserDataUsageBreakdown);
				LinearLayout AppDataUsageRow = new LinearLayout(newAppDataUsageRowContext);
				AppDataUsageRow.Orientation = Orientation.Vertical;

                ContextThemeWrapper newAppTextContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserDataUsageAppTextLayout);
				FrameLayout AppTextDetails = new FrameLayout(newAppTextContext);

                ContextThemeWrapper newAppNameContext = new ContextThemeWrapper(newAppTextContext, Resource.Style.UserDataUsageAppText);
				TextView AppName = new TextView(newAppNameContext);
				AppName.Text = Appnames[i];

                ContextThemeWrapper newAppDataUsageContext = new ContextThemeWrapper(newAppTextContext, Resource.Style.UserDataUsageAppMBAmount);
				TextView AppDataUsage = new TextView(newAppDataUsageContext);
				AppDataUsage.Text = String.Format(StringConstants.Localizable.DataUsageBreakdown, AppUsages[i].ToString());

                ContextThemeWrapper newProgressBarContext = new ContextThemeWrapper(parent.Context, Resource.Style.ProgressBorderStyle);
                FrameLayout currentUserProgressBar = new FrameLayout(newProgressBarContext);

                ContextThemeWrapper newProgressBarFillContext = new ContextThemeWrapper(parent.Context, Resource.Style.ProgressBarFillStyle);
                Android.Widget.ProgressBar UserDataUsageProgressBar = new Android.Widget.ProgressBar(newProgressBarFillContext, null, Resource.Style.ProgressBarFillStyle);

                double AppUsage = Double.Parse(AppUsages[i].ToString()) / 1000;
                double progress = (AppUsage / Controller._used[indextOfUser]) * 100;
                UserDataUsageProgressBar.Progress = (int)(progress);

                currentUserProgressBar.AddView(UserDataUsageProgressBar);
                AppTextDetails.AddView(AppName);
				AppTextDetails.AddView(AppDataUsage);
				AppDataUsageRow.AddView(AppTextDetails);
				AppDataUsageRow.AddView(currentUserProgressBar);
				MainLinear.AddView(AppDataUsageRow);
			}
		}
	}
}