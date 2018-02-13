using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileApp.Droid.Views;
using Android.Graphics.Drawables;
using Android.Support.V4.Content.Res;
using Android.Content.Res;
using MobileApp.Constants;

namespace MobileApp.Droid.Helpers
{
    public class DashboardGradientTimerHelper : TimerTask
    {
        private bool _isPaused;
        private int _gradient;
        private AdminDashboardView _adminDashboard;
        private UsersDataUsageView _usageView;

        public DashboardGradientTimerHelper(AdminDashboardView dashboard)
        {
            _gradient = 0;
            _adminDashboard = dashboard;
            _isPaused = false;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }
        public override void Run()
        {
            if (!_isPaused)
            {
                int first;
                int second;

                switch (_gradient)
                {
                    case 0:
                        first = Resource.Drawable.DashboardGradient1;
                        second = Resource.Drawable.DashboardGradient2;
                        break;
                    case 1:
                        first = Resource.Drawable.DashboardGradient2;
                        second = Resource.Drawable.DashboardGradient3;
                        break;
                    case 2:
                        first = Resource.Drawable.DashboardGradient3;
                        second = Resource.Drawable.DashboardGradient4;
                        break;
                    case 3:
                        first = Resource.Drawable.DashboardGradient4;
                        second = Resource.Drawable.DashboardGradient5;
                        break;
                    case 4:
                        first = Resource.Drawable.DashboardGradient5;
                        second = Resource.Drawable.DashboardGradient6;
                        break;
                    case 5:
                        first = Resource.Drawable.DashboardGradient6;
                        second = Resource.Drawable.DashboardGradient7;
                        break;
                    case 6:
                        first = Resource.Drawable.DashboardGradient7;
                        second = Resource.Drawable.DashboardGradient1;
                        break;
                    default:
                        first = Resource.Drawable.DashboardGradient1;
                        second = Resource.Drawable.DashboardGradient2;
                        break;
                }

                var adminTransition = new TransitionDrawable(new Drawable[]
                {
                    ResourcesCompat.GetDrawable(_adminDashboard.ApplicationContext.Resources, first, null),
                    ResourcesCompat.GetDrawable(_adminDashboard.ApplicationContext.Resources, second, null)
                });

                _adminDashboard.BackgroundGradientThread(adminTransition);

                if (_gradient == NumberConstants.DashboardGradientTransition.DashboardGradientCount - 1)
                {
                    _gradient = 0;
                }
                else
                {
                    _gradient++;
                }
            }
        }
    }
}