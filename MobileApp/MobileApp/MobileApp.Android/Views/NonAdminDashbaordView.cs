using Android.App;
using Android.Widget;
using Android.OS;
using MobileApp.Constants;
using System;
using Android.Content.PM;

namespace MobileApp.Droid.Views
{
	[Activity(Label = "MobileApp", ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/icon")]
	public class NonAdminDashbaordView : Activity
	{
        private TextView _nonAdminDataUsageUsageTitle;
        private TextView _remainingDaysNonAdmin;
        private TextView _gbRemainingNonAdmin;
		private TextView _firstAppName;
		private TextView _firstAppUsed;
		private TextView _secondAppName;
		private TextView _secondAppUsed;
		private TextView _thirdAppName;
		private TextView _thirdAppUsed;
		private Button _requestButton;
        private Button _transferButton;
        private RelativeLayout _dataRemainingFill;
        private RelativeLayout _dataRemainingPgBarLayout;
		private RelativeLayout _remainingDataBarBorder;

		private double _widthsize ;	 
		
        
        // DataRemainingFillMask ScaleX to a max of 4.1

		protected override void OnCreate(Bundle savedInstanceState)
		{

			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NonAdminDashboardLayout);

            findAllElements();
            setAllStringConstants();
			

			_dataRemainingFill = FindViewById<RelativeLayout>(Resource.Id.DataRemainingFillMask);
            _dataRemainingFill.ScaleX = (float)3.5;

            _transferButton.Click += delegate { StartActivity(typeof(TransferView)); };
            _requestButton.Click += delegate { StartActivity(typeof(RequestView)); };
			
		}


		//protected virtual void onStart()
		//{
		//	DataBarFill();
		//}

        protected void findAllElements()
        {
            _nonAdminDataUsageUsageTitle = FindViewById<TextView>(Resource.Id.NonAdminDataUsageTitle);
            _remainingDaysNonAdmin = FindViewById<TextView>(Resource.Id.RemainingDaysNonAdmin);
            _gbRemainingNonAdmin = FindViewById<TextView>(Resource.Id.DataRemainingTextInsidePgBar);
			_firstAppName = FindViewById<TextView>(Resource.Id.App1);
			_firstAppUsed = FindViewById<TextView>(Resource.Id.App1MBLeft);
			_secondAppName = FindViewById<TextView>(Resource.Id.App2);
			_secondAppUsed = FindViewById<TextView>(Resource.Id.App2MBLeft);
			_thirdAppName  = FindViewById<TextView>(Resource.Id.App3);
			_thirdAppUsed = FindViewById<TextView>(Resource.Id.App3MBLeft);
			_transferButton = FindViewById<Button>(Resource.Id.TransferButton);
            _requestButton = FindViewById<Button>(Resource.Id.RequestButton);
			_remainingDataBarBorder = FindViewById<RelativeLayout>(Resource.Id.DataRemainingPgBarLayout);			
			
			
        }

		protected void setAllStringConstants()
		{
			_nonAdminDataUsageUsageTitle.Text = string.Format(StringConstants.Localizable.UsersDataUsage, Controller._nonadminfirstname);
			_remainingDaysNonAdmin.Text = string.Format(StringConstants.Localizable.DaysRemaining, Controller._nonadmindaysRemaining);
			_gbRemainingNonAdmin.Text = string.Format(StringConstants.Localizable.GbRemaining, (Controller._nonadminallocated - Controller._nonadminused));
			_transferButton.Text = StringConstants.Localizable.TransferButton;
			_requestButton.Text = StringConstants.Localizable.RequestButton;
			_firstAppName.Text = Controller._nonadminappName[0];
			_firstAppUsed.Text = Controller._nonadminappUsage[0];
			_secondAppName.Text = Controller._nonadminappName[1];
			_secondAppUsed.Text = Controller._nonadminappUsage[1];
			_thirdAppName.Text = Controller._nonadminappName[2];
			//_thirdAppUsed.Text = Controller._nonadminappUsage[2];

			//_widthsize = _remainingDataBarBorder.Width.ToString();
			//_thirdAppUsed.Text = _widthsize;
		}

		public void DataBarFill()
		{
			RelativeLayout fr = (RelativeLayout)_remainingDataBarBorder;
			_widthsize = fr.GetX();

		}

	}
}



