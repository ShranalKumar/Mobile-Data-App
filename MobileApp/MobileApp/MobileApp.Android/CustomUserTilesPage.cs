using Android.Views;
using Android.Widget;
using MobileApp.Droid.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace MobileApp.Droid
{
    public partial class CustomUserTilesPage : ContentView
    {
        public static List<LinearLayout> UserTiles;
        public static void getTiles(Android.Widget.ScrollView parent)
        {
            int userCount = Controller._uid.Count();
            UserTiles = new List<LinearLayout>();
            int pixelDensity = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;
            ContextThemeWrapper mainContext = new ContextThemeWrapper(parent.Context, Resource.Style.MainLinearForUserTiles);
            LinearLayout MainLinear = new LinearLayout(mainContext);
            MainLinear.Orientation = Orientation.Vertical;
            parent.AddView(MainLinear);
            LinearLayout currentRow = null;

            for (int i = 0; i < userCount; i++)
            {
                if (i % 2 == 0)
                {
                    ContextThemeWrapper rowContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserTileRowLayoutStyle);
                    currentRow = new LinearLayout(rowContext);
                    currentRow.Orientation = Orientation.Horizontal;
                }

                ContextThemeWrapper userTileContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserTileLayoutStyle);
                LinearLayout currentUser = new LinearLayout(userTileContext);
                currentUser.Orientation = Orientation.Vertical;
                                
                ContextThemeWrapper userNameContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserNameCenteredText);
                TextView userName = new TextView(userNameContext);
                userName.Text = Controller._firstname[i];

                ContextThemeWrapper relativeLayoutStyle = new ContextThemeWrapper(parent.Context, Resource.Style.ProgressBorderStyle);
                FrameLayout currentUserBar = new FrameLayout(relativeLayoutStyle);
                
                ContextThemeWrapper PgBarFillContext = new ContextThemeWrapper(parent.Context, Resource.Style.ProgressBarFillStyle);
                Android.Widget.ProgressBar currentUserBarMask = new Android.Widget.ProgressBar(PgBarFillContext, null, Resource.Style.ProgressBarFillStyle);

                currentUser.AddView(userName);
                currentUser.AddView(currentUserBar);
                UserTiles.Add(currentUser);
                currentRow.AddView(currentUser);                
                MainLinear.RemoveView(currentRow);
                MainLinear.AddView(currentRow);
            }
        }
    }
}