using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Util;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microcharts;
using Microcharts.Droid;
using MobileApp.Constants;
using MobileApp.Droid.Helpers;
using SkiaSharp;
using Android.Graphics.Drawables;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class UsersDataUsageView : Activity
    {
        private ImageButton _backButton;
        private TextView _allocatedDataAmount;
        private TextView _allocationPageHeader;
		private TextView _usedDataAmount;
		private ChartView _chartView;
		private TextView _allocatedDataText;
        private TextView _usedDataText;
		private ImageButton _dataUsageSaveButton;
        private ImageButton _dottedMenuButton;
        private Button _removeUser;
        private Button _generateQR;
        private User _user;
		private int _uid;
		private TextView _pointsText;
		private TextView _pointsAmount;
		private Entry[] _entries;
		private TextView _graphTitle;
		private TextView _graphSubTitle;
		private TextView _userPhoneNumber;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UsersDataUsageLayout);
			_uid = Intent.GetIntExtra("tag", 0);
			_user = Controller._users[_uid];
            findAllElements();
            setAllStringConstants();

			_backButton.Click += delegate { Finish(); };
            _entries = new Entry[_user.UsageBreakdown.Count()];
			setGraph();

            if (_user.AdminStatus)
            {
                _dottedMenuButton.Visibility = ViewStates.Gone;
            }

            _dottedMenuButton.Click += ShowPopUpMenu;

            
        }

        private void ShowPopUpMenu(object sender, EventArgs e)
        {
            Context popup = new ContextThemeWrapper(this, Resource.Style.UsersPopupMenu);
            PopupMenu menu = new PopupMenu(popup, _dottedMenuButton);
            menu.Inflate(Resource.Menu.UsersDottedMenu);

            menu.MenuItemClick += (s1, arg1) =>
            {
                switch (arg1.Item.ItemId)
                {
                    case Resource.Id.RemoveUser:
                        AlertDialog.Builder memberDeleteAlert = new AlertDialog.Builder(this);
                        memberDeleteAlert.SetTitle("Remove Member");
                        memberDeleteAlert.SetMessage("Would you like to remove '" + _user.Name.FirstName + "' from your plan?");
                        memberDeleteAlert.SetPositiveButton("Yes", (deleteSender, deleteEventArgs) => { DeleteGroupMember(); });
                        memberDeleteAlert.SetNegativeButton("No", (deleteSender, deleteEventArgs) => { });
                        Dialog deleteDialog = memberDeleteAlert.Create();
                        deleteDialog.Show();
                        break;
                    case Resource.Id.GenerateQR:
                        Intent loadQRCode = new Intent(this, typeof(QRCodeView));
                        loadQRCode.PutExtra("username", _user.UID);
                        loadQRCode.PutExtra("password", "1");
                        StartActivity(loadQRCode);
                        break;
                }
            };

            menu.Show();
        }

        protected async void DeleteGroupMember()
        {
            await Controller.DeleteGroupMemeber(Controller._userLoggedIn, _user);
            Toast.MakeText(this, string.Format(StringConstants.Localizable.DeleteMemberToast, _user.Name.FirstName), ToastLength.Short).Show();
            Member toRemove = Controller._userLoggedIn.GroupMembers.Where(x => x.UID == _user.UID).FirstOrDefault();
            Controller._userLoggedIn.GroupMembers.Remove(toRemove);
            Controller._users.Remove(_user);
            Finish();
        }

        protected void findAllElements()
        {
            _allocatedDataText = FindViewById<TextView>(Resource.Id.AllocatedDataText);
            _allocatedDataAmount = FindViewById<TextView>(Resource.Id.AllocatedDataAmount);
            _allocationPageHeader = FindViewById<TextView>(Resource.Id.UserDataUsageTitle);
            _backButton = FindViewById<ImageButton>(Resource.Id.UserDataUsageBackButton);
            _usedDataText = FindViewById<TextView>(Resource.Id.RemainingDataText);
            _usedDataAmount = FindViewById<TextView>(Resource.Id.RemainingDataAmount);
			_chartView = FindViewById<ChartView>(Resource.Id.chartView);
			_pointsText = FindViewById<TextView>(Resource.Id.PointsText);
			_pointsAmount = FindViewById<TextView>(Resource.Id.PointsAmount);
			_dataUsageSaveButton = FindViewById<ImageButton>(Resource.Id.SaveButton);
			_graphTitle = FindViewById<TextView>(Resource.Id.GraphTitleText);
			_graphSubTitle = FindViewById<TextView>(Resource.Id.GraphSubTitleText);
			_userPhoneNumber = FindViewById<TextView>(Resource.Id.UserPhoneNumber);
            _dottedMenuButton = FindViewById<ImageButton>(Resource.Id.DottedButton);
            _removeUser = FindViewById<Button>(Resource.Id.RemoveUser);
            _generateQR = FindViewById<Button>(Resource.Id.GenerateQR);
        }

		protected void setAllStringConstants()
        {
            _allocatedDataText.Text = StringConstants.Localizable.AllocatedText;
            _allocatedDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Math.Round(_user.Allocated, 2));
            _allocationPageHeader.Text = String.Format(StringConstants.Localizable.UsersDataUsage, _user.Name.FirstName);
			_usedDataText.Text = StringConstants.Localizable.UsedText;
			_usedDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Math.Round(_user.Used, 2));
			_pointsText.Text = StringConstants.Localizable.PointsText;
			_pointsAmount.Text = String.Format(StringConstants.Localizable.PointsAmout, 10);
			_graphTitle.Text = StringConstants.Localizable.GraphTitle;
			_graphSubTitle.Text = StringConstants.Localizable.GraphSubTitle;
			_userPhoneNumber.Text = String.Format(StringConstants.Localizable.UserPhoneNumber, _user.UID);
        }

		protected void setGraph()
		{			
			int number = 1;
			foreach (UserUsageBreakdown breakdown in _user.UsageBreakdown)
			{
				_entries[number - 1] = new Entry(Int32.Parse(breakdown.DataUsed))
				{
					Label = breakdown.Day,
					ValueLabel = breakdown.DataUsed,
					Color = SKColor.Parse("#6191E8"),
					TextColor = SKColor.Parse("#151515")
				};
				number++;
			}

			var chart = new LineChart()
			{
				Entries = _entries,

				LineMode = LineMode.Spline,
				LineSize = 8,
				LabelTextSize = 27,
				PointMode = PointMode.None,
				BackgroundColor = SKColor.Empty
			};
			_chartView.Chart = chart;
		}
    }
}