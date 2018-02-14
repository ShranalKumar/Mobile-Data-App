using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MobileApp.Constants;
using MobileApp.Droid.Helpers;

namespace MobileApp.Droid.Views
{
    public class AdminDashboardContentView : BaseAdapter
    {
        private AdminDashboardView _context;
        private View _view;
        private ImageView _mobileIcon;
        private TextView _productName;
        private TextView _dataUsage;
        private TextView _user;
        private TextView _daysRemaining;
        private ProgressBar _dataUsageProgressBar;
        private Button _allocateButton;
        private Button _buyData;
        private static ScrollView _userTiles;
        private static List<LinearLayout> _userTileList;
        private LinearLayout _tileClickedOn;

        public AdminDashboardContentView(AdminDashboardView context)
        {
            _context = context;
            GetView(0, null, null);
        }

        public override int Count => throw new NotImplementedException();

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            throw new NotImplementedException();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            _view = _context.LayoutInflater.Inflate(Resource.Layout.AdminDashboardContentLayout, null);

            if (_userTileList != null) { _userTileList.Clear(); }
            findAllElements(_view);
            setAllStringConstants();

            CustomUserTilesPage.getTiles(_userTiles);
            _userTileList = CustomUserTilesPage.UserTiles;
            SetTileClickable();

            double progress = (1 - ((double)Controller._users.Sum(x => x.Used) / Controller._planDataPool)) * 100;
            _dataUsageProgressBar.Progress = (int)progress;

            _buyData.Click += delegate { _context.StartActivity(typeof(PlanOverviewView)); };

            return _view;
        }

        protected void findAllElements(View view)
        {
            _mobileIcon = view.FindViewById<ImageView>(Resource.Id.MobileIcon);
            _productName = view.FindViewById<TextView>(Resource.Id.ProductName);
            _dataUsage = view.FindViewById<TextView>(Resource.Id.DataUsageText);
            _dataUsageProgressBar = view.FindViewById<ProgressBar>(Resource.Id.DataProgressBar);
            _user = view.FindViewById<TextView>(Resource.Id.UserName);
            _daysRemaining = view.FindViewById<TextView>(Resource.Id.DaysRemainingText);
            _allocateButton = view.FindViewById<Button>(Resource.Id.AllocateButton);
            _userTiles = view.FindViewById<ScrollView>(Resource.Id.UserTilesLayout);
            _mobileIcon.SetImageResource(Resource.Drawable.MobileIcon);
            _allocateButton = view.FindViewById<Button>(Resource.Id.AllocateButton);
            _buyData = view.FindViewById<Button>(Resource.Id.BuyDataButton);
        }

        protected void setAllStringConstants()
        {
            _daysRemaining.Text = System.String.Format(StringConstants.Localizable.DaysRemaining, Controller._daysRemaining);
            _dataUsage.Text = System.String.Format(StringConstants.Localizable.GbRemaining, Controller._totalRemainder);
            _allocateButton.Text = StringConstants.Localizable.AllocateData;
            _buyData.Text = StringConstants.Localizable.BuyData;
        }

        public void SetTileClickable()
        {
            foreach (LinearLayout tile in _userTileList)
            {
                tile.Click += (o, s) =>
                {
                    _tileClickedOn = tile;
                    Intent loadUserDataPage = new Intent(_context, typeof(UsersDataUsageView));
                    string username;
                    for (int i = 0; i < _tileClickedOn.ChildCount; i++)
                    {
                        if (_tileClickedOn.GetChildAt(i).GetType() == typeof(TextView))
                        {
                            TextView userName = (TextView)_tileClickedOn.GetChildAt(i);
                            username = userName.Text;
                            loadUserDataPage.PutExtra("tag", _tileClickedOn.Id);
                            _context.StartActivity(loadUserDataPage);
                        }
                    }
                };
            }
        }

        public void Reload()
        {
            CustomUserTilesPage.getTiles(_userTiles);
            _userTileList = CustomUserTilesPage.UserTiles;
        }

        public View GetView()
        {
            return _view;
        }

        public Button GetAllocateButton()
        {
            return _allocateButton;
        }
    }
}