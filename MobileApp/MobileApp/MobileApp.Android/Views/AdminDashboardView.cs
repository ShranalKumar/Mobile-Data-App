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

        private DialogTitle _dataUsage;
        private DialogTitle _user;
        private DialogTitle _daysRemaining;
        private DialogTitle _lousieTileName;
        private DialogTitle _shranalTileName;
        private DialogTitle _soumikTileName;
        private DialogTitle _minkyuTileName;
        
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
            _dataUsageBorder = FindViewById<ImageView>(Resource.Id.DataUsageBorder);
            _dataUsageFill = FindViewById<ImageView>(Resource.Id.DataUsageFill);
            _louiseUserTileUsageBorder = FindViewById<ImageView>(Resource.Id.LouiseUserTileUsageBorder);
            _louiseUserTileUsageBorderMask = FindViewById<ImageView>(Resource.Id.LouiseUserTileUsageBorderMask);
            _shranalUserTileUsageBorder = FindViewById<ImageView>(Resource.Id.ShranalUserTileUsageBorder);
            _shranalUserTileUsageBorderMask = FindViewById<ImageView>(Resource.Id.ShranalUserTileUsageBorderMask);
            _soumikUserTileUsageBorder = FindViewById<ImageView>(Resource.Id.SoumikUserTileUsageBorder);
            _soumikUserTileUsageBorderMask = FindViewById<ImageView>(Resource.Id.SoumikUserTileUsageBorderMask);
            _minkyuUserTileUsageBorder = FindViewById<ImageView>(Resource.Id.MinkyuUserTileUsageBorder);
            _minkyuUserTileUsageBorderMask = FindViewById<ImageView>(Resource.Id.MinkyuUserTileUsageBorderMask);

            //Text Views
            _productName = FindViewById<TextView>(Resource.Id.ProductName);

            //Dialog Titles
            _dataUsage = FindViewById<DialogTitle>(Resource.Id.DataUsageText);
            _user = FindViewById<DialogTitle>(Resource.Id.UserName);
            _daysRemaining = FindViewById<DialogTitle>(Resource.Id.DaysRemainingText);
            _lousieTileName = FindViewById<DialogTitle>(Resource.Id.LouiseTileName);
            _shranalTileName = FindViewById<DialogTitle>(Resource.Id.ShranalTileName);
            _soumikTileName = FindViewById<DialogTitle>(Resource.Id.SoumikTileName);
            _minkyuTileName = FindViewById<DialogTitle>(Resource.Id.MinkyuTileName);

            //Buttons
            _allocateButton = FindViewById<Button>(Resource.Id.AllocateButton);

            //Relative Layouts
            _userTiles = FindViewById<RelativeLayout>(Resource.Id.UserTilesLayout);

            //Image Resources
            _hamburgerIcon.SetImageResource(Resource.Drawable.Menu);
            _notificationButton.SetImageResource(Resource.Drawable.NotificationIcon);
            _accountSwitcher.SetImageResource(Resource.Drawable.ChevronDownIcon);
            _mobileIcon.SetImageResource(Resource.Drawable.MobileIcon);
            _dataUsageBorder.SetImageResource(Resource.Drawable.ProgressBarBorder);
            _dataUsageFill.SetImageResource(Resource.Drawable.ProgressBarMask);
            _louiseUserTileUsageBorder.SetImageResource(Resource.Drawable.ProgressBarBorder);
            _louiseUserTileUsageBorderMask.SetImageResource(Resource.Drawable.ProgressBarMask);
            _shranalUserTileUsageBorder.SetImageResource(Resource.Drawable.ProgressBarBorder);
            _shranalUserTileUsageBorderMask.SetImageResource(Resource.Drawable.ProgressBarMask);
            _soumikUserTileUsageBorder.SetImageResource(Resource.Drawable.ProgressBarBorder);
            _soumikUserTileUsageBorderMask.SetImageResource(Resource.Drawable.ProgressBarMask);
            _minkyuUserTileUsageBorder.SetImageResource(Resource.Drawable.ProgressBarBorder);
            _minkyuUserTileUsageBorderMask.SetImageResource(Resource.Drawable.ProgressBarMask);

            _lousieTileName.Text = "Louise";
            _shranalTileName.Text = "Shranal";
            _soumikTileName.Text = "Soumik";
            _minkyuTileName.Text = "Minkyu";
                        
            _productName.Text = "Mobile";
            _user.Text = "mrs louise shirley wesley abcd";
            _user.SetAllCaps(true);
            _daysRemaining.Text = "XX Days Remaining";
            _dataUsage.Text = "XXGB Remaining";
            _allocateButton.SetAllCaps(true);
            _allocateButton.Click += delegate { StartActivity(typeof(AllocationPageView)); };

        }
    }
}

