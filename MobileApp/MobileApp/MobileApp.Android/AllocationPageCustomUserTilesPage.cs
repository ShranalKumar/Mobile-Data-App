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
			for (int i = 0; i < userCount; i++)
			{
				
                ContextThemeWrapper allocationUserTileContext = new ContextThemeWrapper(parent.Context, Resource.Style.AllocationUserTileLayoutStyle);
				LinearLayout User = new LinearLayout(allocationUserTileContext);
				User.Orientation = Orientation.Vertical;
				User.Id = i;

                ContextThemeWrapper TextLayoutContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserTextLayoutStyle);
                FrameLayout UserDetailsTextLayout = new FrameLayout(TextLayoutContext);

				ContextThemeWrapper userNameContext = new ContextThemeWrapper(UserDetailsTextLayout.Context, Resource.Style.UserTextLeftAlignText);
				TextView UserName = new TextView(userNameContext);
				UserName.Text = Controller._firstname[i];

                ContextThemeWrapper userAllocatedContext = new ContextThemeWrapper(UserDetailsTextLayout.Context, Resource.Style.UserTextRightAlignText);
                TextView Allocated = new TextView(userAllocatedContext);
				Allocated.Text = Controller._allocated[i].ToString();

                ContextThemeWrapper userAllocatedSliderContext = new ContextThemeWrapper(UserDetailsTextLayout.Context, Resource.Style.UserAllocationSliderStyle);
                SeekBar userAllocationSlider = new SeekBar(userAllocatedSliderContext, null, Resource.Style.UserAllocationSliderStyle);

                parent.AddView(User);                
				User.AddView(UserDetailsTextLayout);
                UserDetailsTextLayout.AddView(UserName);
                UserDetailsTextLayout.AddView(Allocated);
                User.AddView(userAllocationSlider);
            }
        }
	}

}
