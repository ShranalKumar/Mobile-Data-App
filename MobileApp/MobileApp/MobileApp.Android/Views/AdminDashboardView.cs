using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System;
using MobileApp.Droid.Adapters;
using MobileApp.Constants;
using Android.Content.PM;
using System.Collections.Generic;
using Android.Views;

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
        private Button _allocateButton;
        private ScrollView _userTiles;
        private List<LinearLayout> UserTileList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminDashboardLayout);

            findAllElements();
            setAllStringConstants();

            CustomUserTilesPage.getTiles(_userTiles);

            UserTileList = CustomUserTilesPage.UserTiles;

            foreach (LinearLayout tile in UserTileList)
            {
                tile.Click += delegate { StartActivity(typeof(UsersDataUsageView)); };
            }

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
            _dataUsage.Text = String.Format(StringConstants.Localizable.GbRemaining, Controller._remainder[0]);
            _allocateButton.Text = StringConstants.Localizable.AllocateData;
		}		
	}
}

