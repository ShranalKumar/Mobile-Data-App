using Android.Views;
using Android.Widget;
using System.Linq;
using Xamarin.Forms;


namespace MobileApp.Droid
{
	public class CustomSlidingTilesView : ContentView
    {
        public static void CreateSlidingTilesView(LinearLayout parent)
        {
            int numOfTiles = Controller._groupmemeberfirstname.Count();
			//FrameLayout buttonsLayout = new FrameLayout(parent.Context);

            for (int i = 0; i < numOfTiles; i++)
            {
				ContextThemeWrapper userButtonContext = new ContextThemeWrapper(parent.Context, Resource.Style.WhiteBorderTransperentButtonStyle);
                Android.Widget.Button UserTile = new Android.Widget.Button(userButtonContext,null,0);
                UserTile.Text = Controller._groupmemeberfirstname[i];

				//buttonsLayout.AddView(UserTile);
				parent.AddView(UserTile);

			}
			//parent.AddView(buttonsLayout);
        }

    }
}