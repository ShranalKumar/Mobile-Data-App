using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System;
using MobileApp.Droid.Adapters;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme")]
    public class AdminDashboardView : Activity
    {
        private ImageButton _hamburgerIcon;
        private ImageButton _notificationButton;
        private ImageButton _accountSwitcher;

        private ImageView _mobileIcon;
        private ImageView _dataUsageBorder;
        private ImageView _dataUsageFill;
        private ImageView _louiseUserTileUsageBorder;
        private ImageView _louiseUserTileUsageBorderMask;
        private ImageView _shranalUserTileUsageBorder;
        private ImageView _shranalUserTileUsageBorderMask;
        private ImageView _soumikUserTileUsageBorder;
        private ImageView _soumikUserTileUsageBorderMask;
        private ImageView _minkyuUserTileUsageBorder;
        private ImageView _minkyuUserTileUsageBorderMask;

        private TextView _productName;

        private TextView _dataUsage;
        private TextView _user;
        private TextView _daysRemaining;
        private TextView _lousieTileName;
        private TextView _shranalTileName;
        private TextView _soumikTileName;
        private TextView _minkyuTileName;
        
        private Button _allocateButton;

        private RelativeLayout _userTiles;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AdminDashboardLayout);

            //Image Buttons
            _hamburgerIcon = FindViewById<ImageButton>(Resource.Id.MenuButton);
            _notificationButton = FindViewById<ImageButton>(Resource.Id.NotificationButton);
            _accountSwitcher = FindViewById<ImageButton>(Resource.Id.AccountSwitcher);

            //Image Views
            _mobileIcon = FindViewById<ImageView>(Resource.Id.MobileIcon);

            //Text Views
            _productName = FindViewById<TextView>(Resource.Id.ProductName);

            //Dialog Titles
            _dataUsage = FindViewById<TextView>(Resource.Id.DataUsageText);
            _user = FindViewById<TextView>(Resource.Id.UserName);
            _daysRemaining = FindViewById<TextView>(Resource.Id.DaysRemainingText);

            //Buttons
            _allocateButton = FindViewById<Button>(Resource.Id.AllocateButton);

            //Relative Layouts
            _userTiles = FindViewById<RelativeLayout>(Resource.Id.UserTilesLayout);

            //Image Resources
            _hamburgerIcon.SetImageResource(Resource.Drawable.Menu);
            _notificationButton.SetImageResource(Resource.Drawable.NotificationIcon);
            _accountSwitcher.SetImageResource(Resource.Drawable.ChevronDownIcon);
            _mobileIcon.SetImageResource(Resource.Drawable.MobileIcon);

            _allocateButton.Click += delegate { StartActivity(typeof(AllocationPageView)); };

        }
    }
}

