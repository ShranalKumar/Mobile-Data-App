﻿using Android.App;
using Android.Widget;
using Android.OS;
using MobileApp.Constants;
using System;
using Android.Content.PM;
using Android.Views;
using Java.Lang;
using Java.Util;
using MobileApp.Droid.Helpers;
using Android.Support.V4.View;
using MobileApp.Droid.Adapters;
using Com.ViewPagerIndicator;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace MobileApp.Droid.Views
{
	[Activity(Label = "MobileApp", ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/icon")]
	public class NonAdminDashBoardView : Activity
	{
		private TextView _nonAdminDataUsageUsageTitle;
		//private TextView _remainingDaysNonAdmin;
		//private TextView _gbRemainingNonAdmin;
		//private Button _requestButton;
		//private Button _transferButton;
		//private LinearLayout _noneAdminUsageBreakdown;
		//private RelativeLayout _remainingDataBarBorder;
		//private ProgressBar _dataFillBar;
        private DashboardGradientTimerHelper _dashbardGradientTask;
        private ViewPager _mainViewPager;
        private CirclePageIndicator _circlePageIndicator;
        private RelativeLayout _dashboardLayout;
        private ViewPagerAdapter _mainPagerAdapter;
        private NonAdminContentAdapter _nonAdminDashboardContentInstance;
        private View _nonAdminContentView;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NonAdminDashboardLayout);

			findAllElements();
			setAllStringConstants();

            _mainPagerAdapter = new ViewPagerAdapter(this);
            _mainViewPager.Adapter = _mainPagerAdapter;
            _circlePageIndicator.SetViewPager(_mainViewPager);
            _circlePageIndicator.SetPageColor(Color.White);
            _circlePageIndicator.SetFillColor(new Color(255, 255, 255, 64));

            _nonAdminDashboardContentInstance = new NonAdminContentAdapter(this);
            _nonAdminContentView = _nonAdminDashboardContentInstance.GetView();

            _mainPagerAdapter.AddView(_nonAdminContentView);
            _mainPagerAdapter.NotifyDataSetChanged();
            //CustomUserDataUsageView.GetUserDataUsageRows(_nonAdminUsageBreakdown, Controller._users[0]);

            //_transferButton.Click += delegate { StartActivity(typeof(TransferView)); };
            //_requestButton.Click += delegate { StartActivity(typeof(RequestView)); };

            var timer = new Timer();
            _dashbardGradientTask = new DashboardGradientTimerHelper(this);
            timer.Schedule(_dashbardGradientTask, 0, NumberConstants.DashboardGradientTransition.DashboardGradientTransitionLengthInMilliseconds);
        }

        protected void findAllElements()
		{
			_nonAdminDataUsageUsageTitle = FindViewById<TextView>(Resource.Id.NonAdminDataUsageTitle);
            _mainViewPager = FindViewById<ViewPager>(Resource.Id.NonAdminViewPager);
            _circlePageIndicator = FindViewById<CirclePageIndicator>(Resource.Id.NonAdminPageIndicator);
            _dashboardLayout = FindViewById<RelativeLayout>(Resource.Id.NonAdminDashBoard);
		}

		protected void setAllStringConstants()
		{
			_nonAdminDataUsageUsageTitle.Text = string.Format(StringConstants.Localizable.UsersDataUsage, Controller._users[0].Name.FirstName);
		}

        public void BackgroundGradientThread(TransitionDrawable transition)
        {
            RunOnUiThread(() =>
            {
                _dashboardLayout.Background = transition;
                transition.StartTransition(NumberConstants.DashboardGradientTransition.DashboardGradientTransitionLengthInMilliseconds);
            });
        }
    }
}
