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
using MobileApp.Droid.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


namespace MobileApp.Droid
{			//this whole class can be extended from CustomerTilesPage or can be combined. (Use If condition to check current page then execute either Allocation PAge or Individual Allocation Page)
	public partial class AllocationPageCustomUserTilesPage : ContentView 
	{
		public static List<LinearLayout> UserTiles;
		public AllocationPageCustomUserTilesPage ()
		{

		}

		public static void getTiles(Android.Widget.LinearLayout parent)
		{
			int userCount = Controller._uid.Count();
			UserTiles = new List<LinearLayout>();
			int pixelDensity = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;
			LinearLayout MainLinear = new LinearLayout(parent.Context);
			MainLinear.Orientation = Orientation.Vertical;
			parent.AddView(MainLinear);
			for (int i = 0; i < userCount; i++)
			{
				LinearLayout User = new LinearLayout(MainLinear.Context);
				User.Orientation = Orientation.Vertical;
				User.Id = i;

                //MainLinear.AddView(currentRow);

				LinearLayout UserDetailsTextLayout = new LinearLayout(User.Context);
				UserDetailsTextLayout.Orientation = Orientation.Horizontal;

				//MainLinear.AddView(currentRow);

				TextView UserName = new TextView(UserDetailsTextLayout.Context);
				UserName.Text = Controller._firstname[i];

				TextView Allocated = new TextView(UserDetailsTextLayout.Context);
				Allocated.Text = Controller._allocated[i].ToString();
				Allocated.Gravity = Android.Views.GravityFlags.Right;


				MainLinear.AddView(UserDetailsTextLayout);

				LinearLayout UserDataAllocationSliderLayout = new LinearLayout(User.Context);
				UserDataAllocationSliderLayout.Orientation = Orientation.Horizontal;
				//UserDataAllocationSliderLayout.Gravity = Android.Views.GravityFlags.Center;

				SeekBar userAllocationSlider = new SeekBar(UserDataAllocationSliderLayout.Context);
			



			}
		}
	}

}
