﻿using System;
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
using MobileApp.Droid.Helpers;

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
        public static TextView _remainingDataAmount;
        private TextView _weeklyModeText;
        private Button _saveButton;
        private RadialProgressView _radialProgress;
		private ScrollView _allocationUserScrollView;
		private List<LinearLayout> _allocationUserTileList;
		private LinearLayout _userDataAllocationListItem;
        private double _unallocated;


		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AllocationPageLayout);

            findAllElements();
            setAllStringConstants();

			AllocationPageCustomUserTilesPage.getTiles(_userDataAllocationListItem);
			_allocationUserTileList = AllocationPageCustomUserTilesPage.UserTiles;

			var pixelToDp = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;

            //Radial Progress            
            //_radialProgress.LayoutParameters.Height = 100 * pixelToDp;
            //_radialProgress.LayoutParameters.Width = 100 * pixelToDp;
            //_radialProgress.LabelHidden = true;

            _backButton.SetImageResource(Resource.Drawable.ArrowBackIcon);
			_backButton.Click += delegate { StartActivity(typeof(AdminDashboardView)); };

            _saveButton.Click += delegate { saveClicked(); };
        }

        protected void findAllElements()
        {
            _backButton = FindViewById<ImageButton>(Resource.Id.AllocationBackButton);
            _allocatePageTitle = FindViewById<TextView>(Resource.Id.AllocationPageTitle);
            _currentPlanText = FindViewById<TextView>(Resource.Id.CurrentPlanText);
            _currentPlanDataAmount = FindViewById<TextView>(Resource.Id.CurrentPlanDataAmount);
            _remainingDataText = FindViewById<TextView>(Resource.Id.RemainingDataText);
            _remainingDataAmount = FindViewById<TextView>(Resource.Id.RemainingDataAmount);
            //_radialProgress = FindViewById<RadialProgressView>(Resource.Id.RadialProgressCircle);
            //_weeklyModeText = FindViewById<TextView>(Resource.Id.WeeklyModeText);
            _saveButton = FindViewById<Button>(Resource.Id.SaveButton);
			_allocationUserScrollView = FindViewById<ScrollView>(Resource.Id.ScrollableLayout);
			_userDataAllocationListItem = FindViewById<LinearLayout>(Resource.Id.UserDataAllocationList);
		}

        protected void setAllStringConstants()
        {
            _allocatePageTitle.Text = StringConstants.Localizable.AllocateData;
            _currentPlanText.Text = StringConstants.Localizable.CurrentPlan;
            _currentPlanDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Controller._planDataPool);
            _remainingDataText.Text = StringConstants.Localizable.UnAllocatedData;
            _remainingDataAmount.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(Controller._totalUnAllocated, 1));
            //_weeklyModeText.Text = StringConstants.Localizable.WeeklyMode;
            _saveButton.Text = StringConstants.Localizable.SaveButton;
        }

        private void saveClicked()
        {
            _unallocated = AllocationPageCustomUserTilesPage.unallocated;
            Math.Round(_unallocated,1);

            if (_unallocated >= 0)
            {
                //Update DB
                Toast.MakeText(this, StringConstants.Localizable.AllocationUpdate, ToastLength.Short).Show();
            }
            else
            {
                AlertDialog.Builder allocationAlert = new AlertDialog.Builder(this);
                allocationAlert.SetTitle("Allocation Alert");
                allocationAlert.SetMessage(StringConstants.Localizable.AllocationError);
                allocationAlert.SetNegativeButton("Ok!", (deleteSender, deleteEventArgs) => { });
                Dialog deleteDialog = allocationAlert.Create();
                deleteDialog.Show();
            }
        }
    }
}