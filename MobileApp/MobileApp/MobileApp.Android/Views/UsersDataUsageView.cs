﻿using System;
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
using MobileApp.Constants;
using MobileApp.Droid.Helpers;

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
        private TextView _allocatedDataText;
        private TextView _usedDataText;
        private TextView _usedDataTextAmount;
        private TextView _dataUsageSaveButtonText;
        private ScrollView _dataUsageBreakdownlayout;
		private ImageButton _dataUsageSaveButton;
		private User _user;
		private int _uid;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UsersDataUsageLayout);
			_uid = Intent.GetIntExtra("tag", 0);
			_user = Controller._users[_uid];
            findAllElements();
            setAllStringConstants();
            CustomUserDataUsageView.GetUserDataUsageRows(_dataUsageBreakdownlayout, _user);
            allocationSliderSettings();

            _backButton.Click += delegate { Finish(); };
			_dataUsageSaveButton.Click += UpdateUserDataAllocation;

        }

        protected void findAllElements()
        {
            _allocatedDataText = FindViewById<TextView>(Resource.Id.AllocatedDataText);
            _allocatedDataAmount = FindViewById<TextView>(Resource.Id.AllocatedDataAmount);
            _allocationPageHeader = FindViewById<TextView>(Resource.Id.UserDataUsageTitle);
            _backButton = FindViewById<ImageButton>(Resource.Id.UserDataUsageBackButton);
            _remainingDataText = FindViewById<TextView>(Resource.Id.RemainingDataText);
            _remainingDataAmount = FindViewById<TextView>(Resource.Id.RemainingDataAmount);
            _usedDataText = FindViewById<TextView>(Resource.Id.UsedDataText);
            _usedDataTextAmount = FindViewById<TextView>(Resource.Id.UsedDataAmount);
            _dataUsageSaveButtonText = FindViewById<TextView>(Resource.Id.SaveButtonText);
            _allocationSlider = FindViewById<SeekBar>(Resource.Id.AllocationSlider);
            _dataUsageBreakdownlayout = FindViewById<ScrollView>(Resource.Id.DataUsageBreakdownScrollView);
			_dataUsageSaveButton = FindViewById<ImageButton>(Resource.Id.SaveButton);
        }

        protected void setAllStringConstants()
        {
            _allocatedDataText.Text = StringConstants.Localizable.AllocatedData;
            _allocatedDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, _user.Allocated);
            _allocationPageHeader.Text = String.Format(StringConstants.Localizable.UsersDataUsage, _user.Name.FirstName);
            _remainingDataText.Text = StringConstants.Localizable.RemainingData;
            _remainingDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, 8);
            _usedDataText.Text = StringConstants.Localizable.UsedData;
            _usedDataTextAmount.Text = String.Format(StringConstants.Localizable.DataAmount, _user.Used);
            _dataUsageSaveButtonText.Text = StringConstants.Localizable.SaveButton;
        }

        protected void allocationSliderSettings()
        {
            double _sliderPresetValue = ((double)_user.Allocated / 10 ) * 100;
            _allocationSlider.Progress = (int)_sliderPresetValue;
            _allocationSlider.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
                if (e.FromUser)
                {
                    double changed = ( (double)e.Progress / 100) * (double)10;
                    _allocatedDataAmount.Text = string.Format(StringConstants.Localizable.DataAmount, changed.ToString());
                }
            };
        }

		protected async void UpdateUserDataAllocation(object sender, EventArgs e) 
		{
			var newAllocation = _allocationSlider.Progress / 100.0 * 20;
			User changedUser = await Controller.UpdateAllocation(_user, newAllocation);
			Controller._users[_uid] = changedUser;
		}
    }
}