﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System;
using Java.Util;
using MobileApp.Droid.Adapters;
using MobileApp.Droid.Helpers;
using MobileApp.Constants;
using Android.Content.PM;
using System.Collections.Generic;
using Android.Views;
using Android.Content;
using System.Linq;
using Android.Graphics.Drawables;

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
        private Button _moreDetailsButton;
        private static ScrollView _userTiles;
        private static List<LinearLayout> _userTileList;
        private LinearLayout _tileClickedOn;
        private RelativeLayout _dashboardLayout;
        private DashboardGradientTimerHelper _dashboardGradientTask;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminDashboardLayout);

			if (_userTileList != null) { _userTileList.Clear();	}

            findAllElements();
            setAllStringConstants();

            CustomUserTilesPage.getTiles(_userTiles);
            _userTileList = CustomUserTilesPage.UserTiles;
			SetTileClickable();

            double progress = (1 - ((double)Controller._users.Sum(x => x.Used) / Controller._planDataPool)) * 100;
            _dataUsageProgressBar.Progress = (int)progress;
            
            _moreDetailsButton.Click += delegate { StartActivity(typeof(PlanOverviewView)); };
            _allocateButton.Click += delegate { StartActivity(typeof(AllocationPageView)); };

            var timer = new Timer();
            _dashboardGradientTask = new DashboardGradientTimerHelper(this);
            timer.Schedule(_dashboardGradientTask, 0, NumberConstants.DashboardGradientTransition.DashboardGradientTransitionLengthInMilliseconds);


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
            _moreDetailsButton = FindViewById<Button>(Resource.Id.MoreDetailsButton);
            _dashboardLayout = FindViewById<RelativeLayout>(Resource.Id.BackgroundLayout);
        }

        protected void setAllStringConstants()
        {
            _daysRemaining.Text = String.Format(StringConstants.Localizable.DaysRemaining, Controller._daysRemaining);
            _dataUsage.Text = String.Format(StringConstants.Localizable.GbRemaining, Controller._totalRemainder);
            _allocateButton.Text = StringConstants.Localizable.AllocateData;
            _moreDetailsButton.Text = StringConstants.Localizable.MoreDetails;
        }

        public void Reload() 
		{
			CustomUserTilesPage.getTiles(_userTiles);
			_userTileList = CustomUserTilesPage.UserTiles;
			SetTileClickable();
		}

		public void SetTileClickable()
		{
			foreach (LinearLayout tile in _userTileList)
			{
				tile.Click += (o, s) =>
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
		}

        public void BackgroundGradientThread(TransitionDrawable transition)
        {
            RunOnUiThread(() =>
            {
                _dashboardLayout.Background = transition;
                transition.StartTransition(NumberConstants.DashboardGradientTransition.DashboardGradientTransitionLengthInMilliseconds);
            });
        }

        protected override void OnRestart()
		{
			base.OnRestart();
			Reload();
		}
	}
}

