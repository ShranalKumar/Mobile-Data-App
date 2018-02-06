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
using Android.Support.V4.View;
using Android.Graphics.Drawables;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AdminDashboardView : Activity
    {
        private ViewPager _mainViewPager;
        private ViewPagerAdapter _mainPagerAdapter;
        private RelativeLayout _dashboardLayout;
        private ImageButton _hamburgerIcon;
        private ImageButton _notificationButton;
        private ImageButton _accountSwitcher;
        private DashboardGradientTimerHelper _dashbardGradientTask;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminDashboardLayout);
            findAllElements();
            _mainPagerAdapter = new ViewPagerAdapter(this);
            _mainViewPager.Adapter = _mainPagerAdapter;
            _mainPagerAdapter.GetAllViews(_mainViewPager);

            var timer = new Timer();
            _dashbardGradientTask = new DashboardGradientTimerHelper(this);
            timer.Schedule(_dashbardGradientTask, 0, NumberConstants.DashboardGradientTransition.DashboardGradientTransitionLengthInMilliseconds);
        }
                
        protected void findAllElements()
        {
            _mainViewPager = FindViewById<ViewPager>(Resource.Id.MainViewPager);
            _dashboardLayout = FindViewById<RelativeLayout>(Resource.Id.BackgroundLayout);
            _hamburgerIcon = FindViewById<ImageButton>(Resource.Id.MenuButton);
            _notificationButton = FindViewById<ImageButton>(Resource.Id.NotificationButton);
            _accountSwitcher = FindViewById<ImageButton>(Resource.Id.AccountSwitcher);
            _hamburgerIcon.SetImageResource(Resource.Drawable.Menu);
            _notificationButton.SetImageResource(Resource.Drawable.NotificationIcon);
            _accountSwitcher.SetImageResource(Resource.Drawable.ChevronDownIcon);
        }

		//protected override void OnRestart()
		//{
		//	base.OnRestart();
		//	Reload();
		//}

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

