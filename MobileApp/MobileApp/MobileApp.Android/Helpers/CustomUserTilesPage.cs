using Android.Views;
using Android.Widget;
using MobileApp.Droid.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace MobileApp.Droid.Helpers
{
    public partial class CustomUserTilesPage : ContentView
    {
        public static List<LinearLayout> UserTiles;

        public static void getTiles(Android.Widget.ScrollView parent)
        {
			parent.RemoveAllViewsInLayout();
			if (UserTiles != null) { UserTiles.Clear(); }
            UserTiles = new List<LinearLayout>();
            int pixelDensity = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;
            ContextThemeWrapper mainContext = new ContextThemeWrapper(parent.Context, Resource.Style.MainLinearForUserTiles);
            LinearLayout MainLinear = new LinearLayout(mainContext);
            MainLinear.Orientation = Orientation.Vertical;
            parent.AddView(MainLinear);
            LinearLayout currentRow = null;
			int i = 0;
            foreach (User user in Controller._users)
            {
                ContextThemeWrapper rowContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserTileRowLayoutStyle);
                currentRow = new LinearLayout(rowContext);
                currentRow.Orientation = Orientation.Horizontal;

                ContextThemeWrapper userTileContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserTileLayoutStyle);
                LinearLayout currentUser = new LinearLayout(userTileContext);
                currentUser.Orientation = Orientation.Vertical;
				currentUser.Id = i;
                                
                ContextThemeWrapper userNameContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserNameCenteredText);
                TextView userName = new TextView(userNameContext);
                userName.Text = user.Name.FirstName;

                ContextThemeWrapper relativeLayoutStyle = new ContextThemeWrapper(parent.Context, Resource.Style.ProgressBorderStyle);
                FrameLayout currentUserBar = new FrameLayout(relativeLayoutStyle);
                
                ContextThemeWrapper PgBarFillContext = new ContextThemeWrapper(parent.Context, Resource.Style.ProgressBarFillStyle);
                Android.Widget.ProgressBar currentUserBarMask = new Android.Widget.ProgressBar(PgBarFillContext, null, Resource.Style.ProgressBarFillStyle);
                try
                {
                    double progress = (1 - ((double)user.Used / user.Allocated)) * 100;
                    currentUserBarMask.Progress = (int)(progress);
                }
                catch (DivideByZeroException)
                {
                    double progress = (1 - ((double)user.Used / Controller._totalRemainder)) * 100;
                    currentUserBarMask.Progress = (int)(progress);
                }                

                currentUserBar.AddView(currentUserBarMask);
                currentUser.AddView(userName);
                currentUser.AddView(currentUserBar);
                UserTiles.Add(currentUser);
                currentRow.AddView(currentUser);                
                MainLinear.RemoveView(currentRow);
                MainLinear.AddView(currentRow);
				i++;
            }
        }
    }
}