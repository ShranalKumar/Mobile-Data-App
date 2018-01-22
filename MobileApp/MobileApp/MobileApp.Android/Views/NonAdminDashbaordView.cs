using Android.App;
using Android.Widget;
using Android.OS;
using MobileApp.Constants;
using System;

namespace MobileApp.Droid.Views
{
	[Activity(Label = "MobileApp", Icon = "@mipmap/icon")]
	public class NonAdminDashbaordView : Activity
	{
        private TextView _nonAdminDataUsageUsageTitle;
        private TextView _remainingDaysNonAdmin;
        private TextView _gbRemainingNonAdmin;
        private Button _requestButton;
        private Button _transferButton;
        
        

		protected override void OnCreate(Bundle savedInstanceState)
		{

			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NonAdminDashboardLayout);

            findAllElements();
            setAllStringConstants();
            _transferButton.Click += delegate { StartActivity(typeof(TransferView)); };
            _requestButton.Click += delegate { StartActivity(typeof(RequestView)); };
		}

        protected void findAllElements()
        {
            _nonAdminDataUsageUsageTitle = FindViewById<TextView>(Resource.Id.NonAdminDataUsageTitle);
            _remainingDaysNonAdmin = FindViewById<TextView>(Resource.Id.RemainingDaysNonAdmin);
            _gbRemainingNonAdmin = FindViewById<TextView>(Resource.Id.DataRemainingTextInsidePgBar);
            _transferButton = FindViewById<Button>(Resource.Id.TransferButton);
            _requestButton = FindViewById<Button>(Resource.Id.RequestButton);
        }

        protected void setAllStringConstants()
        {
            _nonAdminDataUsageUsageTitle.Text = string.Format(StringConstants.Localizable.UsersDataUsage, "Steven's");
            _remainingDaysNonAdmin.Text = string.Format(StringConstants.Localizable.DaysRemaining, "1");
            _gbRemainingNonAdmin.Text = string.Format(StringConstants.Localizable.GbRemaining, "2");
            _transferButton.Text = StringConstants.Localizable.TransferButton;
            _requestButton.Text = StringConstants.Localizable.RequestButton;
        }

	}
}



