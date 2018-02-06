using Android.App;
using Android.Widget;
using Android.OS;
using MobileApp.Constants;
using System;
using Android.Content.PM;
using Android.Views;
using Java.Lang;
using MobileApp.Droid.Helpers;

namespace MobileApp.Droid.Views
{
	[Activity(Label = "MobileApp", ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/icon")]
	public class NonAdminDashBoardView : Activity
	{
		private TextView _nonAdminDataUsageUsageTitle;
		private TextView _remainingDaysNonAdmin;
		private TextView _gbRemainingNonAdmin;
		private Button _requestButton;
		private Button _transferButton;
		private LinearLayout _noneAdminUsageBreakdown;
		private RelativeLayout _remainingDataBarBorder;
		private ProgressBar _dataFillBar;
		

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NonAdminDashboardLayout);

			findAllElements();
			DataBarFill();
			setAllStringConstants();
			//CustomUserDataUsageView.GetUserDataUsageRows(_noneAdminUsageBreakdown, Controller._users[0]);

			_transferButton.Click += delegate { StartActivity(typeof(TransferView)); };
			_requestButton.Click += delegate { StartActivity(typeof(RequestView)); };
            
		}

		public void DataBarFill()
		{
            double _fillNumber = (1 - (double)Controller._users[0].Used / (double)Controller._users[0].Allocated) * 100;
            _dataFillBar.Progress = (int)_fillNumber;
        }

        protected void findAllElements()
		{
			_nonAdminDataUsageUsageTitle = FindViewById<TextView>(Resource.Id.NonAdminDataUsageTitle);
			_remainingDaysNonAdmin = FindViewById<TextView>(Resource.Id.RemainingDaysNonAdmin);
			_gbRemainingNonAdmin = FindViewById<TextView>(Resource.Id.DataRemainingTextInsidePgBar);
			_transferButton = FindViewById<Button>(Resource.Id.TransferButton);
			_requestButton = FindViewById<Button>(Resource.Id.RequestButton);
			_remainingDataBarBorder = FindViewById<RelativeLayout>(Resource.Id.DataRemainingPgBarLayout);
			_dataFillBar = FindViewById<ProgressBar>(Resource.Id.DataRemainingFillMask);
			_noneAdminUsageBreakdown = FindViewById<LinearLayout>(Resource.Id.NonAdminUsageBreakdown);
		}

		protected void setAllStringConstants()
		{
			_nonAdminDataUsageUsageTitle.Text = string.Format(StringConstants.Localizable.UsersDataUsage, Controller._users[0].Name.FirstName);
			_remainingDaysNonAdmin.Text = string.Format(StringConstants.Localizable.DaysRemaining, Controller._daysRemaining);
			_gbRemainingNonAdmin.Text = string.Format(StringConstants.Localizable.GbRemaining, (Controller._users[0].Allocated - Controller._users[0].Used));
			_transferButton.Text = StringConstants.Localizable.TransferButton;
			_requestButton.Text = StringConstants.Localizable.RequestButton;
		}
	}
}
