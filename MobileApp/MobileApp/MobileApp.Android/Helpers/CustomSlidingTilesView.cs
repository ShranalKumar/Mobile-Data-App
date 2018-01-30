using Android.Views;
using Android.Widget;
using System.Linq;
using Xamarin.Forms;

namespace MobileApp.Droid.Helpers
{
    public partial class CustomSlidingTilesView : ContentView
    {
        public static void CreateSlidingTilesView(LinearLayout parent)
        {
			//FrameLayout buttonsLayout = new FrameLayout(parent.Context);

            foreach (User user in Controller._users)
            {
				ContextThemeWrapper userButtonContext = new ContextThemeWrapper(parent.Context, Resource.Style.WhiteBorderTransperentButtonStyle);
                Android.Widget.Button UserTile = new Android.Widget.Button(userButtonContext,null,0);
                UserTile.Text = user.Name.FirstName;

				//buttonsLayout.AddView(UserTile);
				parent.AddView(UserTile);

			}
			//parent.AddView(buttonsLayout);
        }
    }
}