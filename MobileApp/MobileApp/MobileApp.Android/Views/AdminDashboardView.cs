using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = true)]
    public class AdminDashboardView : Activity
    {
        private ImageButton _hamburgerIcon;
        private ImageButton _notificationButton;
        private ImageButton _accountSwitcher;
        private ImageView _mobileIcon;

        private TextView _productName;
        private DialogTitle _user;
        private DialogTitle _daysRemaining;

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

            _hamburgerIcon.SetImageResource(Resource.Drawable.Menu);
            _notificationButton.SetImageResource(Resource.Drawable.NotificationIcon);
            _accountSwitcher.SetImageResource(Resource.Drawable.ChevronDownIcon);
            _mobileIcon.SetImageResource(Resource.Drawable.MobileIcon);

            _productName.Text = "Mobile";
            _user.Text = "mrs louise shirley wesley abcd";
            _user.SetAllCaps(true);
            _daysRemaining.Text = "XX Days Remaining";


        }
    }
}

