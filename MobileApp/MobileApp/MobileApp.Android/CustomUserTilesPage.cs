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
            LinearLayout MainLinear = new LinearLayout(parent.Context);
            MainLinear.Orientation = Orientation.Vertical;
            parent.AddView(MainLinear);
            LinearLayout currentRow = null;

            for (int i = 0; i < userCount; i++)
            {
                if (i % 2 == 0)
                {
                    currentRow = new LinearLayout(MainLinear.Context);
                    currentRow.Orientation = Orientation.Horizontal;
                    currentRow.SetMinimumHeight(25 * pixelDensity);
                    currentRow.SetMinimumWidth(25 * pixelDensity);
                    currentRow.SetGravity(Android.Views.GravityFlags.Center);
                    currentRow.SetPadding(5 * pixelDensity, 5 * pixelDensity, 5 * pixelDensity, 5 * pixelDensity);
                }
                LinearLayout currentUser = new LinearLayout(currentRow.Context);
                currentUser.Orientation = Orientation.Vertical;
                currentUser.SetMinimumHeight(25 * pixelDensity);
                currentUser.SetMinimumWidth(25 * pixelDensity);
                currentUser.SetGravity(Android.Views.GravityFlags.Center);
                currentUser.SetPadding(10 * pixelDensity, 10 * pixelDensity, 10 * pixelDensity, 10 * pixelDensity);
                currentUser.Tag = i;

                TextView userName = new TextView(currentUser.Context);
                userName.Text = Controller._firstname[i];
                userName.Gravity = Android.Views.GravityFlags.Center;

                Android.Widget.RelativeLayout currentUserBar = new Android.Widget.RelativeLayout(currentUser.Context);
                currentUserBar.SetMinimumHeight(25 * pixelDensity);
                currentUserBar.SetMinimumWidth(25 * pixelDensity);
                currentUserBar.SetBackgroundResource(Resource.Drawable.ProgressBarBorder);

                Android.Widget.ProgressBar currentUserBarMask = new Android.Widget.ProgressBar(currentUserBar.Context, null, Android.Resource.Attribute.ProgressBarStyleHorizontal) {
                    Progress = 50,
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent),
                };
                currentUserBarMask.Alpha = 30f;
                //currentUserBarMask.Background = null;
                currentUserBarMask.Progress = 90;
                currentUserBarMask.ScaleY = 10;
                //currentUserBarMask.


                //Android.Widget.RelativeLayout currentUserBarMask = new Android.Widget.RelativeLayout(currentUserBar.Context);
                //currentUserBarMask.SetMinimumHeight(25 * pixelDensity);
                //currentUserBarMask.SetMinimumWidth(25 * pixelDensity);
                //currentUserBarMask.Alpha = 0.3f;
                //currentUserBarMask.SetBackgroundResource(Resource.Drawable.ProgressBarMask);

                currentUserBar.AddView(currentUserBarMask);
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