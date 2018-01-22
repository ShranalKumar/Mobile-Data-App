using Android.App;
using Android.Widget;
using Android.OS;

namespace MobileApp.Droid.Views
{
	[Activity(Label = "MobileApp", Icon = "@mipmap/icon")]
	public class NonAdminDashbaordView : Activity
	{
		Button transfer,request;

		protected override void OnCreate(Bundle savedInstanceState)
		{

			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NonAdminDashboardLayout);

			transfer = FindViewById<Button>(Resource.Id.TransferButton);
			transfer.Click += delegate { StartActivity(typeof(TransferView)); };

			request = FindViewById<Button>(Resource.Id.RequestButton);
			request.Click += delegate { StartActivity(typeof(RequestView)); };



		}

	}
}



