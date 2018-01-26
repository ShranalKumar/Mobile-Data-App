using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MobileApp.Droid.Helpers
{
    public partial class CustomSlidingTilesView : ContentView
    {
        public static void CreateSlidingTilesView(LinearLayout parent)
        {
            int numOfTiles = Controller._groupmemeberfirstname.Count();

            for (int i = 0; i < numOfTiles; i++)
            {
                Android.Widget.Button UserTile = new Android.Widget.Button(parent.Context);
                UserTile.Text = Controller._groupmemeberfirstname[i];

                parent.AddView(UserTile);
            }
        }
    }
}