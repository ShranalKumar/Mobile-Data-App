using Android.Widget;
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
		public static void GetUserDataUsageRows(Android.Widget.ScrollView parent, User key)
		{
            var _usage = new List<UserUsageBreakdown>();
            key.UsageBreakdown.ForEach(x => _usage.Add(x));
            _usage.Sort((first, second) => string.Compare(first.DataUsed, second.DataUsed));
            _usage.Reverse();

            LinearLayout MainLinear = new LinearLayout(parent.Context);
            MainLinear.SetPadding(0, 25, 0, 0);
			MainLinear.Orientation = Orientation.Vertical;
			parent.AddView(MainLinear);

			foreach (UserUsageBreakdown breakdown in _usage)
			{
                ContextThemeWrapper newAppDataUsageRowContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserDataUsageBreakdown);
				LinearLayout AppDataUsageRow = new LinearLayout(newAppDataUsageRowContext);
				AppDataUsageRow.Orientation = Orientation.Vertical;

                ContextThemeWrapper newAppTextContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserDataUsageAppTextLayout);
				FrameLayout AppTextDetails = new FrameLayout(newAppTextContext);

                ContextThemeWrapper newAppNameContext = new ContextThemeWrapper(newAppTextContext, Resource.Style.UserDataUsageAppText);
				TextView AppName = new TextView(newAppNameContext);
				AppName.Text = breakdown.Day;

                ContextThemeWrapper newAppDataUsageContext = new ContextThemeWrapper(newAppTextContext, Resource.Style.UserDataUsageAppMBAmount);
				TextView AppDataUsage = new TextView(newAppDataUsageContext);
				AppDataUsage.Text = String.Format(StringConstants.Localizable.DataUsageBreakdown, breakdown.DataUsed);

                ContextThemeWrapper newProgressBarContext = new ContextThemeWrapper(parent.Context, Resource.Style.ProgressBorderStyle);
                FrameLayout currentUserProgressBar = new FrameLayout(newProgressBarContext);

                ContextThemeWrapper newProgressBarFillContext = new ContextThemeWrapper(parent.Context, Resource.Style.ProgressBarFillStyle);
                Android.Widget.ProgressBar UserDataUsageProgressBar = new Android.Widget.ProgressBar(newProgressBarFillContext, null, Resource.Style.ProgressBarFillStyle);

                double DataUsed = Double.Parse(breakdown.DataUsed) / 1000;
                double progress = ((DataUsed / key.Used) * 100);
                UserDataUsageProgressBar.Progress = (int)(progress);

                currentUserProgressBar.AddView(UserDataUsageProgressBar);
                AppTextDetails.AddView(AppName);
				AppTextDetails.AddView(AppDataUsage);
				AppDataUsageRow.AddView(AppTextDetails);
				AppDataUsageRow.AddView(currentUserProgressBar);
				MainLinear.AddView(AppDataUsageRow);
			}
		}

		public static void GetUserDataUsageRows(Android.Widget.LinearLayout parent, User key)
		{
            var _usage = new List<UserUsageBreakdown>();
            key.UsageBreakdown.ForEach(x => _usage.Add(x));
            _usage.Sort((first, second) => string.Compare(first.DataUsed, second.DataUsed));
            _usage.Reverse();
            
			LinearLayout MainLinear = new LinearLayout(parent.Context);
			MainLinear.SetPadding(0, 25, 0, 0);
			MainLinear.Orientation = Orientation.Vertical;
			parent.AddView(MainLinear);

            foreach (UserUsageBreakdown breakdown in _usage)
			{
                ContextThemeWrapper newAppDataUsageRowContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserDataUsageBreakdown);
				LinearLayout AppDataUsageRow = new LinearLayout(newAppDataUsageRowContext);
				AppDataUsageRow.Orientation = Orientation.Vertical;

				ContextThemeWrapper newAppTextContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserDataUsageAppTextLayout);
				FrameLayout AppTextDetails = new FrameLayout(newAppTextContext);

				ContextThemeWrapper newAppNameContext = new ContextThemeWrapper(newAppTextContext, Resource.Style.UserDataUsageAppText);
				TextView AppName = new TextView(newAppNameContext);
				AppName.Text = breakdown.Day;

				ContextThemeWrapper newAppDataUsageContext = new ContextThemeWrapper(newAppTextContext, Resource.Style.UserDataUsageAppMBAmount);
				TextView AppDataUsage = new TextView(newAppDataUsageContext);
				AppDataUsage.Text = String.Format(StringConstants.Localizable.DataUsageBreakdown, breakdown.DataUsed);

				ContextThemeWrapper newProgressBarContext = new ContextThemeWrapper(parent.Context, Resource.Style.ProgressBorderStyle);
				FrameLayout currentUserProgressBar = new FrameLayout(newProgressBarContext);

				ContextThemeWrapper newProgressBarFillContext = new ContextThemeWrapper(parent.Context, Resource.Style.ProgressBarFillStyle);
				Android.Widget.ProgressBar UserDataUsageProgressBar = new Android.Widget.ProgressBar(newProgressBarFillContext, null, Resource.Style.ProgressBarFillStyle);

				double DataUsed = Double.Parse(breakdown.DataUsed) / 1000;
				double progress = ((DataUsed / key.Used) * 100);
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