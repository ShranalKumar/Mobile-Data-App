using Android.App;
using Android.Widget;
using Android.OS;

namespace MobileApp.Droid.Views
{
    [Activity(Label = "MobileApp", MainLauncher = true, Icon = "@mipmap/icon")]
    public class AdminDashboardView : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.AdminDashboardLayout);
        }
    }
}

