using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microcharts;
using Microcharts.Droid;
using MobileApp.Constants;
using MobileApp.Droid.Helpers;
using SkiaSharp;

namespace MobileApp.Droid.Views
{
    [Activity(Label = "UsersDataUsageView", ScreenOrientation = ScreenOrientation.Portrait)]
    public class UsersDataUsageView : Activity
    {
        private ImageButton _backButton;
        private SeekBar _allocationSlider;
        private TextView _allocatedDataAmount;
        private TextView _allocationPageHeader;
        private TextView _remainingDataText;
        private TextView _remainingDataAmount;
		private ChartView _chartView;
		private TextView _allocatedDataText;
        private TextView _usedDataText;
        private TextView _usedDataTextAmount;
        private TextView _dataUsageSaveButtonText;
        private ScrollView _dataUsageBreakdownlayout;
		private ImageButton _dataUsageSaveButton;
		private User _user;
		private double _tempUnAllocated;
		private double _progressChanged;
		private int _uid;
		private Microcharts.Droid.ChartView chartView;
		private TextView _pointsText;
		private TextView _pointsAmount;
		private Entry[] _entries;
		private TextView _graphTitle;
		private TextView _graphSubTitle;
		private TextView _userPhoneNumber;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UsersDataUsageLayout);
			_uid = Intent.GetIntExtra("tag", 0);
			_user = Controller._users[_uid];
            findAllElements();
            setAllStringConstants();
			//CustomUserDataUsageView.GetUserDataUsageRows(_dataUsageBreakdownlayout, _user);
			
			allocationSliderSettings();		

			_backButton.Click += delegate { Finish(); };
			_entries = new Entry[_user.UsageBreakdown.Count()];
			setGraph();

			

			//_dataUsageSaveButton.Click += UpdateUserDataAllocation;

		}

		protected void findAllElements()
        {
            _allocatedDataText = FindViewById<TextView>(Resource.Id.AllocatedDataText);
            _allocatedDataAmount = FindViewById<TextView>(Resource.Id.AllocatedDataAmount);
            _allocationPageHeader = FindViewById<TextView>(Resource.Id.UserDataUsageTitle);
            _backButton = FindViewById<ImageButton>(Resource.Id.UserDataUsageBackButton);
            _remainingDataText = FindViewById<TextView>(Resource.Id.RemainingDataText);
            _remainingDataAmount = FindViewById<TextView>(Resource.Id.RemainingDataAmount);
            //_usedDataText = FindViewById<TextView>(Resource.Id.UsedDataText);
            //_usedDataTextAmount = FindViewById<TextView>(Resource.Id.UsedDataAmount);
            //_dataUsageSaveButtonText = FindViewById<TextView>(Resource.Id.SaveButtonText);
            //_allocationSlider = FindViewById<SeekBar>(Resource.Id.AllocationSlider);
			//_dataUsageBreakdownlayout = FindViewById<ScrollView>(Resource.Id.DataUsageBreakdownScrollView);
			_chartView = FindViewById<ChartView>(Resource.Id.chartView);
			_pointsText = FindViewById<TextView>(Resource.Id.PointsText);
			_pointsAmount = FindViewById<TextView>(Resource.Id.PointsAmount);
			_dataUsageSaveButton = FindViewById<ImageButton>(Resource.Id.SaveButton);
			_graphTitle = FindViewById<TextView>(Resource.Id.GraphTitleText);
			_graphSubTitle = FindViewById<TextView>(Resource.Id.GraphSubTitleText);
			_userPhoneNumber = FindViewById<TextView>(Resource.Id.UserPhoneNumber);

		}

		protected void setAllStringConstants()
        {
            _allocatedDataText.Text = StringConstants.Localizable.AllocatedText;
            _allocatedDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Math.Round(_user.Allocated, 1));
            _allocationPageHeader.Text = String.Format(StringConstants.Localizable.UsersDataUsage, _user.Name.FirstName);
            _remainingDataText.Text = StringConstants.Localizable.UsedText;
            _remainingDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Math.Round(Controller._users[0].Used, 1));
			_pointsText.Text = StringConstants.Localizable.PointsText;
			_pointsAmount.Text = String.Format(StringConstants.Localizable.PointsAmout, 10);
			_graphTitle.Text = StringConstants.Localizable.GraphTitle;
			_graphSubTitle.Text = StringConstants.Localizable.GraphSubTitle;
			_userPhoneNumber.Text = String.Format(StringConstants.Localizable.UserPhoneNumber, _user.UID);
            //_usedDataText.Text = StringConstants.Localizable.UsedData;
            //_usedDataTextAmount.Text = String.Format(StringConstants.Localizable.DataAmount, _user.Used);
            //_dataUsageSaveButtonText.Text = StringConstants.Localizable.SaveButton;
        }

        protected void allocationSliderSettings()
        {

			//         double _sliderPresetValue = ((double)_user.Allocated / Controller._planDataPool) * 100;
			//_allocationSlider.Progress = (int)_sliderPresetValue;
			//_allocationSlider.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
			//	if (e.FromUser)
			//	{
			//		_progressChanged = ((double)e.Progress / 100) * (Controller._totalUnAllocated + _user.Allocated);
			//		var dif = _progressChanged - _user.Allocated;
			//		_remainingDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Math.Round(Controller._totalUnAllocated - dif), 1);
			//		if (_progressChanged <= _user.Used)
			//		{
			//			_progressChanged = _user.Used;
			//			_remainingDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Math.Round(Controller._totalUnAllocated + (_user.Allocated - _user.Used), 1));
			//			_allocatedDataAmount.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(_user.Used, 1));
			//			_tempUnAllocated = Controller._totalUnAllocated + (_user.Allocated - _user.Used);
			//		}
			//		else
			//		{
			//			_remainingDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Math.Round(Controller._totalUnAllocated - dif, 1));
			//			_allocatedDataAmount.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(_progressChanged, 1));
			//			_tempUnAllocated = Controller._totalUnAllocated - dif;
			//		}
			//	}
			//};
		}

		//protected async void UpdateUserDataAllocation(object sender, EventArgs e) 
		//{
		//	Controller._totalUnAllocated = _tempUnAllocated;
		//	Controller._users[0].Allocated = _tempUnAllocated;
		//	User changedUser = await Controller.UpdateAllocation(_user, _progressChanged);
		//	Controller._users[_uid] = changedUser;
  //          Toast.MakeText(this, StringConstants.Localizable.SavedChangesMessage, ToastLength.Long).Show();
  //      }

		protected void setGraph()
		{			
			int number = 1;
			foreach (UserUsageBreakdown breakdown in _user.UsageBreakdown)
			{
				_entries[number - 1] = new Entry(Int32.Parse(breakdown.DataUsed))
				{
					Label = breakdown.Day,
					ValueLabel = breakdown.DataUsed,
					Color = SKColor.Parse("#FFFFFF"),
					TextColor = SKColor.Parse("#FFFFFF")
				};
				number++;
			}

			var chart = new LineChart()
			{
				Entries = _entries,

				LineMode = LineMode.Spline,
				LineSize = 8,
				LabelTextSize = 20,
				PointMode = PointMode.None,
				BackgroundColor = SKColor.Empty
			};
			_chartView.Chart = chart;
		}

			//var entries = new[]
			//{
			//	new Entry(200)
			//	{
			//		Label = "January",
			//		ValueLabel = "200",
			//		Color = SKColor.Parse("#FFFFFF")

			//	},
			//	new Entry(400)
			//	{
			//	Label = "February",
			//	ValueLabel = "400",
			//	Color = SKColor.Parse("#FFFFFF")
			//	},
			//	new Entry(-100)
			//	{
			//	Label = "March",
			//	ValueLabel = "-100",
			//	Color = SKColor.Parse("#FFFFFF")
			//	}




	}
}