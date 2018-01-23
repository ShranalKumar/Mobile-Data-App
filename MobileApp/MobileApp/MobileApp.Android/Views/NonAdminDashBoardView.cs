using Android.App;
using Android.Widget;
using Android.OS;
using MobileApp.Constants;
using System;
using Android.Content.PM;
using Android.Views;
using Java.Lang;

namespace MobileApp.Droid.Views
{
	[Activity(Label = "MobileApp", ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/icon")]
	public class NonAdminDashBoardView : Activity
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
		private RelativeLayout _dataFillBar;
		public double widthassumed = 3.7;
		public double trouble;
		//private NonAdminDashboardOnGlobalLayoutListener _globalLayoutListener;



		//private double _widthsize;


		// DataRemainingFillMask ScaleX to a max of 3.7

		protected override void OnCreate(Bundle savedInstanceState)
		{

			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NonAdminDashboardLayout);

			findAllElements();
			DataBarFill();
			setAllStringConstants();
			

			_transferButton.Click += delegate { StartActivity(typeof(TransferView)); };
			_requestButton.Click += delegate { StartActivity(typeof(RequestView)); };

			//_globalLayoutListener = new NonAdminDashboardOnGlobalLayoutListener(this);

		}





		public void DataBarFill()
		{
			
			trouble = (double) Controller._nonadminused / (double) Controller._nonadminallocated * widthassumed;
			_dataFillBar.ScaleX = (float)trouble;
			//_dataFillBar.ScaleX =(float)widthassumed;

		}

		protected void findAllElements()
		{
			_nonAdminDataUsageUsageTitle = FindViewById<TextView>(Resource.Id.NonAdminDataUsageTitle);
			_remainingDaysNonAdmin = FindViewById<TextView>(Resource.Id.RemainingDaysNonAdmin);
			_gbRemainingNonAdmin = FindViewById<TextView>(Resource.Id.DataRemainingTextInsidePgBar);
			_firstAppName = FindViewById<TextView>(Resource.Id.App1);
			_firstAppUsed = FindViewById<TextView>(Resource.Id.App1MBLeft);
			_secondAppName = FindViewById<TextView>(Resource.Id.App2);
			_secondAppUsed = FindViewById<TextView>(Resource.Id.App2MBLeft);
			_thirdAppName = FindViewById<TextView>(Resource.Id.App3);
			_thirdAppUsed = FindViewById<TextView>(Resource.Id.App3MBLeft);
			_transferButton = FindViewById<Button>(Resource.Id.TransferButton);
			_requestButton = FindViewById<Button>(Resource.Id.RequestButton);
			_remainingDataBarBorder = FindViewById<RelativeLayout>(Resource.Id.DataRemainingPgBarLayout);
			_dataFillBar = FindViewById<RelativeLayout>(Resource.Id.DataRemainingFillMask);


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
			_thirdAppUsed.Text = Controller._nonadminappUsage[2];

			//_widthsize = _remainingDataBarBorder.Width.ToString();
			//_thirdAppUsed.Text = _widthsize;
		}

		



	}
}











//public void OnGlobalLayout()
//{
//	_hasHadGlobalLayout = true;
//	ViewDidBecomeVisible();
//	_containerLayout.ViewTreeObserver.RemoveOnGlobalLayoutListener(_globalLayoutListener);
//}





//public void OnGlobalLayout()
//{
//	_hasHadGlobalLayout = true;
//	ViewDidBecomeVisible();
//	_containerLayout.ViewTreeObserver.RemoveOnGlobalLayoutListener(_globalLayoutListener);
//}

//private void UpdatePages()
//{
//	_mainViewPager.OffscreenPageLimit = PagedViewModels.Length;

//	_fragmentPagerAdapter = new DashboardPagerAdapter(this, SupportFragmentManager, PagedViewModels);
//	_mainViewPager.Adapter = _fragmentPagerAdapter;

//	_hasHadGlobalLayout = false;
//	_containerLayout.ViewTreeObserver.AddOnGlobalLayoutListener(_globalLayoutListener);
//}



//public void OnGlobalLayoutListener()
//{
//_remainingDataBarBorder.ViewTreeObserver.AddOnGlobalLayoutListener

//}













