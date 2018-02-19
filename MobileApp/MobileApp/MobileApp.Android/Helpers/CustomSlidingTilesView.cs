using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MobileApp.Droid.Helpers
{
    public partial class CustomSlidingTilesView : ContentView
    {
        
        public static List<Android.Widget.Button> _userButtons;

        public static void CreateSlidingTilesView(LinearLayout parent)
        {
            //FrameLayout buttonsLayout = new FrameLayout(parent.Context);
            int pixelDensity = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;
            if (_userButtons == null) { _userButtons = new List<Android.Widget.Button>(); }

            foreach (User user in Controller._users)
            {
                if (user != Controller._userLoggedIn)
                {
                    ContextThemeWrapper userButtonContext = new ContextThemeWrapper(parent.Context, Resource.Style.SlidingUserTileButton);
                    Android.Widget.Button UserTile = new Android.Widget.Button(userButtonContext, null, 0);
                    UserTile.Id = Int32.Parse(user.UID);
                    UserTile.Text = user.Name.FirstName;


                    parent.AddView(UserTile);
                    _userButtons.Add(UserTile);
                }				
			}
        }
    }
}