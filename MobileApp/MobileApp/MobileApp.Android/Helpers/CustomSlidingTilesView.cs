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

            foreach (User user in Controller._users)
            {
				ContextThemeWrapper userButtonContext = new ContextThemeWrapper(parent.Context, Resource.Style.WhiteBorderTransperentButtonStyle);
                Android.Widget.Button UserTile = new Android.Widget.Button(userButtonContext,null,0);
                UserTile.Id = Int32.Parse(user.UID);
                UserTile.Text = user.Name.FirstName;

				//buttonsLayout.AddView(UserTile);
				parent.AddView(UserTile);
                _userButtons.Add(UserTile);

			}
			//parent.AddView(buttonsLayout);
        }
    }
}