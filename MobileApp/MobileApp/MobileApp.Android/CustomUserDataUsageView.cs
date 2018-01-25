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
		public static void GetUserDataUsageRows(Android.Widget.ScrollView parent, string key)
		{
			LinearLayout MainLinear = new LinearLayout(parent.Context);
			List<string> Appnames = Controller._appName[key];
			List<string> AppUsages = Controller._appUsage[key];
			MainLinear.Orientation = Orientation.Vertical;
			parent.AddView(MainLinear);

			for (int i = 0; i < Appnames.Count; i++)
			{
				LinearLayout AppDataUsageRow = new LinearLayout(MainLinear.Context);
				AppDataUsageRow.Orientation = Orientation.Vertical;

				LinearLayout AppTextDetails = new LinearLayout(AppDataUsageRow.Context);
				AppTextDetails.Orientation = Orientation.Horizontal;

				TextView AppName = new TextView(AppTextDetails.Context);
				AppName.Text = Appnames[i];
				AppName.Gravity = GravityFlags.Left;

				TextView AppDataUsage = new TextView(AppTextDetails.Context);
				AppDataUsage.Text = String.Format(StringConstants.Localizable.DataUsageBreakdown, AppUsages[i]);
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