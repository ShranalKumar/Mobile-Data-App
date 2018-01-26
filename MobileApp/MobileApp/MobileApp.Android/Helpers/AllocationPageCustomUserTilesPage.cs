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


namespace MobileApp.Droid.Helpers
{			
	public partial class AllocationPageCustomUserTilesPage : ContentView 
	{
		public static List<LinearLayout> UserTiles;

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

				LinearLayout UserDetailsTextLayout = new LinearLayout(MainLinear.Context);				
				UserDetailsTextLayout.Orientation = Orientation.Horizontal;
                
				TextView UserName = new TextView(User.Context);
				UserName.Text = Controller._firstname[i];

				TextView Allocated = new TextView(UserDetailsTextLayout.Context);
				Allocated.Text = Controller._allocated[i].ToString();
				Allocated.Gravity = Android.Views.GravityFlags.Right;			
				
				LinearLayout UserDataAllocationSliderLayout = new LinearLayout(User.Context);
				UserDataAllocationSliderLayout.Orientation = Orientation.Horizontal;
				UserDataAllocationSliderLayout.SetGravity(Android.Views.GravityFlags.Center);

				SeekBar userAllocationSlider = new SeekBar(UserDataAllocationSliderLayout.Context);

				UserDetailsTextLayout.AddView(UserName);
				UserDetailsTextLayout.AddView(Allocated);
				UserDataAllocationSliderLayout.AddView(userAllocationSlider);
				User.AddView(UserDetailsTextLayout);
				User.AddView(UserDataAllocationSliderLayout);
				MainLinear.AddView(User);
			}
		}
	}

}
