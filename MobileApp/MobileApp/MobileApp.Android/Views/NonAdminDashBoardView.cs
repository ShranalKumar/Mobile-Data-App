using Android.App;
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
        private DashboardGradientTimerHelper _dashbardGradientTask;
        private ViewPager _mainViewPager;
        private CirclePageIndicator _circlePageIndicator;
        private RelativeLayout _dashboardLayout;
        private ViewPagerAdapter _mainPagerAdapter;
        private NonAdminContentAdapter _nonAdminDashboardContentInstance;
        private View _nonAdminContentView;
        private GamificationViewAdapter _gamificationContentInstance;
        private View _gamificationView;

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

            _gamificationContentInstance = new GamificationViewAdapter(this);
            _gamificationView = _gamificationContentInstance.GetView();

            _mainPagerAdapter.AddView(_nonAdminContentView);
            _mainPagerAdapter.AddView(_gamificationView);
            _mainPagerAdapter.NotifyDataSetChanged();

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
            _mainPagerAdapter.AddView(_nonAdminDashboardContentInstance.GetView(0, null, null));
            _mainPagerAdapter.AddView(_gamificationContentInstance.GetView(0, null, null));
            _mainPagerAdapter.NotifyDataSetChanged();
        }
    }
}
