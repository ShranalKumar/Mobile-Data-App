using Android.App;
using Android.Widget;
using Android.OS;

namespace MobileApp.Droid.Views
{
    [Activity(Label = "MobileApp", MainLauncher = true, Icon = "@mipmap/icon")]
    public class NonAdminDashbaordView : Activity
    {
        int count = 1;
		Button request, transfer;
		ImageView logo;


        protected override void OnCreate(Bundle savedInstanceState)
        {
		
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.NonAdminDashboardLayout);

			// Get our button from the layout resource,
			// and attach an event to it
			request = FindViewById<Button>(Resource.Id.RequestButton);
			transfer = FindViewById<Button>(Resource.Id.TransferButton);
			logo = FindViewById<ImageView>(Resource.Id.Logo);

		}
	}
}

