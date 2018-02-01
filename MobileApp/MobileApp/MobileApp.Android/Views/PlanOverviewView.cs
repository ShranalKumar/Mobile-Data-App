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

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PlanOverviewView : Activity
    {

        private TextView _overviewPageTitle;
        private TextView _dataPlanNameText;
        private TextView _dataPlanAmount;
        private TextView _planRemainingDataText;
        private TextView _planRemainingDataAmount;
        private TextView _planAllocatedDataText;
        private TextView _planAllocatedDataAmount;
        private TextView _planUsedDataText;
        private TextView _planUsedDataAmount;

        private ImageButton _overviewPageBackButton;
        private ImageView _dropdownListArrow;

        private FrameLayout _memberListDropDown;

        private ScrollView _memberListScrollView;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PlanOverviewLayout);

            findAllElements();
            setAllStringConstants();

            _overviewPageBackButton.Click += delegate { this.Finish(); };
            _memberListDropDown.Click += delegate { showMembersList(); };
        }

        private void findAllElements()
        {
            _overviewPageTitle = FindViewById<TextView>(Resource.Id.OverviewPageTitle);
            _dataPlanNameText = FindViewById<TextView>(Resource.Id.PlanNameTitle);
            _dataPlanAmount = FindViewById<TextView>(Resource.Id.PlaneNameAmount);
            _planRemainingDataText = FindViewById<TextView>(Resource.Id.DataRemainingTitle);
            _planRemainingDataAmount = FindViewById<TextView>(Resource.Id.DataRemainingAmount);
            _planAllocatedDataText = FindViewById<TextView>(Resource.Id.DataAllocatedTitle);
            _planAllocatedDataAmount = FindViewById<TextView>(Resource.Id.DataAllocatedAmount);
            _planUsedDataText = FindViewById<TextView>(Resource.Id.DataUsedTitle);
            _planUsedDataAmount = FindViewById<TextView>(Resource.Id.DataUsedAmount);
            _overviewPageBackButton = FindViewById<ImageButton>(Resource.Id.OverviewPageBackButton);
            _dropdownListArrow = FindViewById<ImageView>(Resource.Id.OverviewPageDownArrow);
            _memberListDropDown = FindViewById<FrameLayout>(Resource.Id.MembersListHeadingText);
            _memberListScrollView = FindViewById<ScrollView>(Resource.Id.MembersListScrollView);
    }

        private void setAllStringConstants()
        {
            _overviewPageTitle.Text = StringConstants.Localizable.OverviewTitle;
            _dataPlanNameText.Text = StringConstants.Localizable.PlanName;
            _dataPlanAmount.Text = string.Format(StringConstants.Localizable.PlanDataAmount, Controller._planDataPool);
            _planRemainingDataText.Text = StringConstants.Localizable.PlanRemaining;
            _planRemainingDataAmount.Text = string.Format(StringConstants.Localizable.PlanRemainingAmount, Controller._totalRemainder); //Need to concat amount from DB, DB under construction
            _planAllocatedDataText.Text = StringConstants.Localizable.PlanAllocated;
            _planAllocatedDataAmount.Text = string.Format(StringConstants.Localizable.PlanAllocatedAmount, Controller._totalAllocated); //Need to concat amount from DB, DB under construction
            _planUsedDataText.Text = StringConstants.Localizable.PlanUsed;
            _planUsedDataAmount.Text = string.Format(StringConstants.Localizable.PlanUsedAmount, Controller._totalUsed); //Need to concat amount from DB, DB under construction
        }

        private void showMembersList()
        {
            if (_memberListScrollView.Visibility == ViewStates.Invisible)
            {
                _dropdownListArrow.Rotation = 180;
                _memberListScrollView.Visibility = ViewStates.Visible;
            }
            if (_memberListScrollView.Visibility == ViewStates.Visible)
            {
                _dropdownListArrow.Rotation = -180;
                _memberListScrollView.Visibility = ViewStates.Invisible;
            }
        }
    }
}