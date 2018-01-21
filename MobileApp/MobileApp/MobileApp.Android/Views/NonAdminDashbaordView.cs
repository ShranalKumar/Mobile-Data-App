using Android.App;
using Android.Widget;
using Android.OS;

namespace MobileApp.Droid.Views
{
	[Activity(Label = "MobileApp", Icon = "@mipmap/icon")]
	public class NonAdminDashbaordView : Activity
	{

		protected override void OnCreate(Bundle savedInstanceState)
		{

			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NonAdminDashboardLayout);
            
			transfer.Click += delegate { StartActivity(typeof(TransferView)); };

		}

	}
}



