﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System;
using MobileApp.Droid.Adapters;
using MobileApp.Droid.Helpers;
using MobileApp.Constants;
using Android.Content.PM;
using System.Collections.Generic;
using Android.Views;
using Android.Content;
using System.Linq;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AdminDashboardView : Activity
    {
        private ImageButton _hamburgerIcon;
        private ImageButton _notificationButton;
        private ImageButton _accountSwitcher;
        private ImageView _mobileIcon;
        private TextView _productName;
        private TextView _dataUsage;
        private TextView _user;
        private TextView _daysRemaining;
        private ProgressBar _dataUsageProgressBar;
        private Button _allocateButton;
        private ScrollView _userTiles;
        private List<LinearLayout> _userTileList;
        private LinearLayout _tileClickedOn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminDashboardLayout);

            findAllElements();
            setAllStringConstants();

            CustomUserTilesPage.getTiles(_userTiles);
            _userTileList = CustomUserTilesPage.UserTiles;

            foreach (LinearLayout tile in _userTileList)
            {
                tile.Click += (o,s) =>
                {
                    _tileClickedOn = tile;
                    Intent loadUserDataPage = new Intent(this, typeof(UsersDataUsageView));
                    string username;
                    for (int i = 0; i < _tileClickedOn.ChildCount; i++)
                    {
                        if (_tileClickedOn.GetChildAt(i).GetType() == typeof(TextView))
                        {
                            TextView userName = (TextView)_tileClickedOn.GetChildAt(i);
                            username = userName.Text;
                            loadUserDataPage.PutExtra("tag", _tileClickedOn.Id);
                            StartActivity(loadUserDataPage);
                        }                        
                    }
                };
            }
			//Plan data pool is temporary. Will need to be fixed.
            double progress = (double)Controller._users.Sum(x => x.Used) / 10 * 100;
            _dataUsageProgressBar.Progress = (int)progress;
            _dataUsageProgressBar.Click += delegate { StartActivity(typeof(PlanOverviewView)); };

            _allocateButton.Click += delegate { StartActivity(typeof(AllocationPageView)); };
        }

        protected void findAllElements()
        {
            _hamburgerIcon = FindViewById<ImageButton>(Resource.Id.MenuButton);
            _notificationButton = FindViewById<ImageButton>(Resource.Id.NotificationButton);
            _accountSwitcher = FindViewById<ImageButton>(Resource.Id.AccountSwitcher);
            _mobileIcon = FindViewById<ImageView>(Resource.Id.MobileIcon);
            _productName = FindViewById<TextView>(Resource.Id.ProductName);
            _dataUsage = FindViewById<TextView>(Resource.Id.DataUsageText);
            _dataUsageProgressBar = FindViewById<ProgressBar>(Resource.Id.DataProgressBar);
            _user = FindViewById<TextView>(Resource.Id.UserName);
            _daysRemaining = FindViewById<TextView>(Resource.Id.DaysRemainingText);
            _allocateButton = FindViewById<Button>(Resource.Id.AllocateButton);
            _userTiles = FindViewById<ScrollView>(Resource.Id.UserTilesLayout);
            _hamburgerIcon.SetImageResource(Resource.Drawable.Menu);
            _notificationButton.SetImageResource(Resource.Drawable.NotificationIcon);
            _accountSwitcher.SetImageResource(Resource.Drawable.ChevronDownIcon);
            _mobileIcon.SetImageResource(Resource.Drawable.MobileIcon);
            _allocateButton = FindViewById<Button>(Resource.Id.AllocateButton);
        }

        protected void setAllStringConstants()
        {
            _daysRemaining.Text = String.Format(StringConstants.Localizable.DaysRemaining, Controller._daysRemaining);
			//Need to fix remainder
            _dataUsage.Text = String.Format(StringConstants.Localizable.GbRemaining, 8);
            _allocateButton.Text = StringConstants.Localizable.AllocateData;
		}
    }
}

