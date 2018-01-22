using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using RadialProgress;
using MobileApp.Constants;
using Android.Content.PM;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AllocationPageView : Activity
    {
        private ImageButton _backButton;
        private TextView _allocatePageTitle;
        private TextView _currentPlanText;
        private TextView _currentPlanDataAmount;
        private TextView _remainingDataText;
        private TextView _remainingDataAmount;
        private TextView _weeklyModeText;
        private Button _saveButon;
        private RadialProgressView _radialProgress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AllocationPageLayout);
            findAllElements();
            setAllStringConstants();

            var pixelToDp = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;

            //Radial Progress            
            _radialProgress.LayoutParameters.Height = 100 * pixelToDp;
            _radialProgress.LayoutParameters.Width = 100 * pixelToDp;
            _radialProgress.LabelHidden = true;

            _backButton.SetImageResource(Resource.Drawable.ArrowBackIcon);
			_backButton.Click += delegate { StartActivity(typeof(AdminDashboardView)); };
            _allocatePageTitle.Text = "Allocate Data";
            

            
            
        }

        protected void findAllElements()
        {
            _backButton = FindViewById<ImageButton>(Resource.Id.AllocationBackButton);
            _allocatePageTitle = FindViewById<TextView>(Resource.Id.AllocationPageTitle);
            _currentPlanText = FindViewById<TextView>(Resource.Id.CurrentPlanText);
            _currentPlanDataAmount = FindViewById<TextView>(Resource.Id.CurrentPlanDataAmount);
            _remainingDataText = FindViewById<TextView>(Resource.Id.RemainingDataText);
            _remainingDataAmount = FindViewById<TextView>(Resource.Id.RemainingDataAmount);
            _radialProgress = FindViewById<RadialProgressView>(Resource.Id.RadialProgressCircle);
            _weeklyModeText = FindViewById<TextView>(Resource.Id.WeeklyModeText);
            _saveButon = FindViewById<Button>(Resource.Id.SaveButton);
        }

        protected void setAllStringConstants()
        {
            _allocatePageTitle.Text = StringConstants.Localizable.AllocateData;
            _currentPlanText.Text = StringConstants.Localizable.CurrentPlan;
            _currentPlanDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Controller._planDataPool);
            _remainingDataText.Text = StringConstants.Localizable.RemainingData;
            _remainingDataAmount.Text = string.Format(StringConstants.Localizable.DataAmount, Controller._remainder[0]);
            _weeklyModeText.Text = StringConstants.Localizable.WeeklyMode;
            _saveButon.Text = StringConstants.Localizable.SaveButton;
        }
    }
}