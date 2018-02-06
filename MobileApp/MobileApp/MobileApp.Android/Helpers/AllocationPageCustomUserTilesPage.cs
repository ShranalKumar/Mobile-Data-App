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
using MobileApp.Constants;
using MobileApp.Droid.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


namespace MobileApp.Droid.Helpers
{			
	public partial class AllocationPageCustomUserTilesPage : ContentView 
	{
		public static List<LinearLayout> UserTiles;
        public static double totalAllocated = 0;
        public static double progressChanged;
        public static double unallocated;


        public static void getTiles(Android.Widget.LinearLayout parent)
		{
			UserTiles = new List<LinearLayout>();
			int pixelDensity = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;
            
			foreach (User user in Controller._users)
			{
				
                ContextThemeWrapper allocationUserTileContext = new ContextThemeWrapper(parent.Context, Resource.Style.AllocationUserTileLayoutStyle);
				LinearLayout User = new LinearLayout(allocationUserTileContext);
				User.Orientation = Orientation.Vertical;
				User.Id = Int32.Parse(user.UID);

                ContextThemeWrapper TextLayoutContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserTextLayoutStyle);
                FrameLayout UserDetailsTextLayout = new FrameLayout(TextLayoutContext);

				ContextThemeWrapper userNameContext = new ContextThemeWrapper(UserDetailsTextLayout.Context, Resource.Style.UserTextLeftAlignText);
				TextView UserName = new TextView(userNameContext);
				UserName.Text = user.Name.FirstName;

                ContextThemeWrapper userAllocatedContext = new ContextThemeWrapper(UserDetailsTextLayout.Context, Resource.Style.UserTextRightAlignText);
                TextView Allocated = new TextView(userAllocatedContext);
				Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(user.Allocated, 1));

                ContextThemeWrapper userAllocatedSliderContext = new ContextThemeWrapper(UserDetailsTextLayout.Context, Resource.Style.UserAllocationSliderStyle);
                SeekBar userAllocationSlider = new SeekBar(userAllocatedSliderContext, null, Resource.Style.UserAllocationSliderStyle);
                double progress = (user.Allocated / Controller._planDataPool) * 100;
                userAllocationSlider.Progress = (int)(progress);
                userAllocationSlider.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
                {
                    if (e.FromUser)
                    {
                        double progressChanged = ((double)e.Progress / 100) * (Controller._planDataPool);
                        Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(progressChanged, 1));
                        user.Allocated = progressChanged;



                        if (progressChanged <= user.Used)
                        {
                            progressChanged = user.Used;
                            Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(progressChanged, 1));
                            user.Allocated = progressChanged;
                        }

                        totalAllocated = unallocated = 0;
                        Controller._users.ForEach(x => totalAllocated += x.Allocated);
                        unallocated = Controller._planDataPool - totalAllocated;
                        AllocationPageView._remainingDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Math.Round(unallocated, 1));
                    }

                };

                


                parent.AddView(User);                
				User.AddView(UserDetailsTextLayout);
                UserDetailsTextLayout.AddView(UserName);
                UserDetailsTextLayout.AddView(Allocated);
                User.AddView(userAllocationSlider);
                UserTiles.Add(User);
            }
        }
	}
}
