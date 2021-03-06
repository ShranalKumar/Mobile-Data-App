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
using Com.ViewPagerIndicator;
using Android.Graphics;
using System.IO;
using MobileApp.Droid.Converters;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AdminDashboardView : Activity
    {
        private ViewPager _mainViewPager;
        private ViewPagerAdapter _mainPagerAdapter;
        private RelativeLayout _dashboardLayout;
        private ImageButton _hamburgerIcon;
        private TextView _userName;
        private ImageButton _notificationButton;
        private DashboardGradientTimerHelper _dashbardGradientTask;
        private AdminDashboardContentView _adminDashboardContentInstance;
        private View _adminDashboardContentView;
        private AllocationPageView _allocationPageInstance;
        private View _allocationPageView;
        private Button _allocateButton;
        private CirclePageIndicator _circlePageIndicator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminDashboardLayout);
            findAllElements();
            
            _mainPagerAdapter = new ViewPagerAdapter(this);
            _mainViewPager.Adapter = _mainPagerAdapter;
            _circlePageIndicator.SetViewPager(_mainViewPager);
            _circlePageIndicator.SetPageColor(Color.White);
            _circlePageIndicator.SetFillColor(CoreColorConverter.GetColor(ColorConstants.CirclePageIndicatorColor));

            _adminDashboardContentInstance = new AdminDashboardContentView(this);
            _adminDashboardContentView = _adminDashboardContentInstance.GetView();
            _allocateButton = _adminDashboardContentInstance.GetAllocateButton();

            _allocationPageInstance = new AllocationPageView(this);
            _allocationPageView = _allocationPageInstance.GetView();

            _mainPagerAdapter.AddView(_adminDashboardContentView);
            _mainPagerAdapter.AddView(_allocationPageView);
            _mainPagerAdapter.NotifyDataSetChanged();

            var timer = new Timer();
            _dashbardGradientTask = new DashboardGradientTimerHelper(this);
            timer.Schedule(_dashbardGradientTask, 0, NumberConstants.DashboardGradientTransition.DashboardGradientTransitionLengthInMilliseconds);
        }
                
        protected void findAllElements()
        {
            _circlePageIndicator = FindViewById<CirclePageIndicator>(Resource.Id.circlePageIndicator);
            _mainViewPager = FindViewById<ViewPager>(Resource.Id.MainViewPager);
            _dashboardLayout = FindViewById<RelativeLayout>(Resource.Id.BackgroundLayout);
            _hamburgerIcon = FindViewById<ImageButton>(Resource.Id.MenuButton);
            _userName = FindViewById<TextView>(Resource.Id.UserName);
            _userName.Text = "MR " + (Controller._userLoggedIn.Name.FirstName + " " + Controller._userLoggedIn.Name.LastName).ToUpper();
            _notificationButton = FindViewById<ImageButton>(Resource.Id.NotificationButton);
            _hamburgerIcon.SetImageResource(Resource.Drawable.Menu);
            _notificationButton.SetImageResource(Resource.Drawable.NotificationIcon);
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            Reload();
        }

        protected override void OnResume()
        {
            base.OnResume();
            Reload();
        }

        public void Reload()
        {
            ViewPagerAdapter._views.Clear();
            _mainViewPager.RemoveAllViewsInLayout();
            _mainPagerAdapter.AddView(_adminDashboardContentInstance.GetView(0, null, null));
            _allocateButton = _adminDashboardContentInstance.GetAllocateButton();            
            _mainPagerAdapter.AddView(_allocationPageInstance.GetView(0, null, null));
			_allocateButton.Click += allocateButtonClickAction;
			_mainPagerAdapter.NotifyDataSetChanged();
        }

        public void BackgroundGradientThread(TransitionDrawable transition)
        {
            RunOnUiThread(() =>
            {
                _dashboardLayout.Background = transition;
                transition.StartTransition(NumberConstants.DashboardGradientTransition.DashboardGradientTransitionLengthInMilliseconds);
            });
        }

        public void addView(View newView)
        {
            int pageIndex = _mainPagerAdapter.AddView(newView);
            _mainViewPager.SetCurrentItem(pageIndex, true);
        }

        public void setCurrentPage(View pageToShow)
        {
            _mainViewPager.SetCurrentItem(_mainPagerAdapter.GetItemPosition(pageToShow), true);
        }

        public void allocateButtonClickAction(object sender, EventArgs e)
        {
            _mainViewPager.SetCurrentItem(2, true);
        }
    }
}