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
using MobileApp.Constants;
using MobileApp.Droid.Helpers;

namespace MobileApp.Droid.Views
{
    [Activity(Label = "UsersDataUsageView", ScreenOrientation = ScreenOrientation.Portrait)]
    public class UsersDataUsageView : Activity
    {
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
        private int _indexOfUser;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UsersDataUsageLayout);

            _indexOfUser = Controller._firstname.IndexOf(Intent.GetStringExtra("username"));
            findAllElements();
            setAllStringConstants();
            CustomUserDataUsageView.GetUserDataUsageRows(_dataUsageBreakdownlayout, Intent.GetStringExtra("username"));
            allocationSliderSettings();

        }

        protected void findAllElements()
        {
            _allocatedDataText = FindViewById<TextView>(Resource.Id.AllocatedDataText);
            _allocatedDataAmount = FindViewById<TextView>(Resource.Id.AllocatedDataAmount);
            _allocationPageHeader = FindViewById<TextView>(Resource.Id.UserDataUsageTitle);
            _remainingDataText = FindViewById<TextView>(Resource.Id.RemainingDataText);
            _remainingDataAmount = FindViewById<TextView>(Resource.Id.RemainingDataAmount);
            _usedDataText = FindViewById<TextView>(Resource.Id.UsedDataText);
            _usedDataTextAmount = FindViewById<TextView>(Resource.Id.UsedDataAmount);
            _dataUsageSaveButtonText = FindViewById<TextView>(Resource.Id.SaveButtonText);
            _allocationSlider = FindViewById<SeekBar>(Resource.Id.AllocationSlider);
            _dataUsageBreakdownlayout = FindViewById<ScrollView>(Resource.Id.DataUsageBreakdownScrollView);
        }

        protected void setAllStringConstants()
        {
            _allocatedDataText.Text = StringConstants.Localizable.AllocatedData;
            _allocatedDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Controller._allocated[_indexOfUser]);
            _allocationPageHeader.Text = String.Format(StringConstants.Localizable.UsersDataUsage, Controller._firstname[_indexOfUser]);
            _remainingDataText.Text = StringConstants.Localizable.RemainingData;
            _remainingDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Controller._remainder[_indexOfUser]);
            _usedDataText.Text = StringConstants.Localizable.UsedData;
            _usedDataTextAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Controller._used[_indexOfUser]);
            _dataUsageSaveButtonText.Text = StringConstants.Localizable.SaveButton;
        }

        protected void allocationSliderSettings()
        {
            double _sliderPresetValue = ((double)Controller._allocated[_indexOfUser] / Controller._planDataPool ) * 100;
            _allocationSlider.Progress = (int)_sliderPresetValue;
            _allocationSlider.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
                if (e.FromUser)
                {
                    double changed = ( (double)e.Progress / 100) * (double)Controller._planDataPool;
                    _allocatedDataAmount.Text = string.Format(StringConstants.Localizable.DataAmount, changed.ToString());
                }
            };
        }
    }
}