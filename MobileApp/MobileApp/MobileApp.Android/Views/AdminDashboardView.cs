using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = true)]
    public class AdminDashboardView : Activity
    {
        private ImageButton _hamburgerIcon;
        private ImageButton _notificationButton;
        private ImageButton _accountSwitcher;

        private ImageView _mobileIcon;
        private ImageView _dataUsageBorder;
        private ImageView _dataUsageFill;

        private TextView _productName;
        private DialogTitle _dataUsage;

        private DialogTitle _user;
        private DialogTitle _daysRemaining;
        private RelativeLayout _userTilesLayout;
        private Button _louiseUserTile;
        private Button _shranalUserTile;
        private Button _soumikUserTile;
        private Button _minkyuUserTile;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.AdminDashboardLayout);

            _hamburgerIcon = FindViewById<ImageButton>(Resource.Id.MenuButton);
            _mobileIcon = FindViewById<ImageView>(Resource.Id.MobileIcon);
            _notificationButton = FindViewById<ImageButton>(Resource.Id.NotificationButton);
            _accountSwitcher = FindViewById<ImageButton>(Resource.Id.AccountSwitcher);
            _productName = FindViewById<TextView>(Resource.Id.ProductName);
            _user = FindViewById<DialogTitle>(Resource.Id.UserName);
            _daysRemaining = FindViewById<DialogTitle>(Resource.Id.DaysRemainingText);
            _louiseUserTile = FindViewById<Button>(Resource.Id.LouiseUserTile);
            _shranalUserTile = FindViewById<Button>(Resource.Id.ShranalUserTile);
            _soumikUserTile = FindViewById<Button>(Resource.Id.SoumikUserTile);
            _minkyuUserTile = FindViewById<Button>(Resource.Id.MinkyuUserTile);
            _userTilesLayout = FindViewById<RelativeLayout>(Resource.Id.UserTilesLayout);
            _dataUsageBorder = FindViewById<ImageView>(Resource.Id.DataUsageBorder);
            _dataUsageFill = FindViewById<ImageView>(Resource.Id.DataUsageFill);
            _dataUsage = FindViewById<DialogTitle>(Resource.Id.DataUsageText);

            _hamburgerIcon.SetImageResource(Resource.Drawable.Menu);
            _notificationButton.SetImageResource(Resource.Drawable.NotificationIcon);
            _accountSwitcher.SetImageResource(Resource.Drawable.ChevronDownIcon);
            _mobileIcon.SetImageResource(Resource.Drawable.MobileIcon);
            _dataUsageBorder.SetImageResource(Resource.Drawable.ProgressBarBorder);
            _dataUsageFill.SetImageResource(Resource.Drawable.ProgressBarFill);

            

            _louiseUserTile.Text = "Louise";
            _shranalUserTile.Text = "Shranal";
            _soumikUserTile.Text = "Soumik";
            _minkyuUserTile.Text = "Minkyu";
            _productName.Text = "Mobile";
            _user.Text = "mrs louise shirley wesley abcd";
            _user.SetAllCaps(true);
            _daysRemaining.Text = "XX Days Remaining";
            _dataUsage.Text = "XXGB Remaining";

            



        }
    }
}

