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
using MobileApp.Droid.Helpers;
using Java.Lang;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AllocationPageView : BaseAdapter
    {
        private TextView _currentPlanText;
        private TextView _currentPlanDataAmount;
        private TextView _remainingDataText;
        public static TextView _remainingDataAmount;
        private Button _saveButon;
		private ScrollView _allocationUserScrollView;
		private List<LinearLayout> _allocationUserTileList;
		private LinearLayout _userDataAllocationListItem;
        private AdminDashboardView _context;
        private View _view;
		private User _user;

		public override int Count => throw new NotImplementedException();

        public AllocationPageView(AdminDashboardView context) : base()
        {
            _context = context;
            GetView(0, null, null);
        }

        protected void findAllElements(View view)
        {
            _currentPlanText = view.FindViewById<TextView>(Resource.Id.CurrentPlanText);
            _currentPlanDataAmount = view.FindViewById<TextView>(Resource.Id.CurrentPlanDataAmount);
            _remainingDataText = view.FindViewById<TextView>(Resource.Id.RemainingDataText);
            _remainingDataAmount = view.FindViewById<TextView>(Resource.Id.RemainingDataAmount);
            _saveButon = view.FindViewById<Button>(Resource.Id.SaveButton);
			_allocationUserScrollView = view.FindViewById<ScrollView>(Resource.Id.ScrollableLayout);
			_userDataAllocationListItem = view.FindViewById<LinearLayout>(Resource.Id.UserDataAllocationList);
		}

        protected void setAllStringConstants()
        {
            _currentPlanText.Text = StringConstants.Localizable.CurrentPlan;
            _currentPlanDataAmount.Text = System.String.Format(StringConstants.Localizable.DataAmount, Controller._planDataPool);
            _remainingDataText.Text = StringConstants.Localizable.UnAllocatedData;
            _remainingDataAmount.Text = string.Format(StringConstants.Localizable.DataAmount, System.Math.Round(Controller._totalUnAllocated, 2));
            _saveButon.Text = StringConstants.Localizable.SaveButton;
        }

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
            _view = _context.LayoutInflater.Inflate(Resource.Layout.AllocationPageLayout, null);

            findAllElements(_view);
            setAllStringConstants();

            AllocationPageCustomUserTilesPage.getTiles(_userDataAllocationListItem);
            _allocationUserTileList = AllocationPageCustomUserTilesPage.UserTiles;

            var pixelToDp = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;
			_saveButon.Click += SaveButtonClicked;
            return _view;
        }

        public View GetView()
        {
            return _view;
		}
		public async void SaveButtonClicked(object sender, EventArgs e)
		{

			//foreach (LinearLayout tile in _userTileList)
			//{
			//	tile.Click += (o, s) =>
			//	{
			//		_tileClickedOn = tile;
			//		Intent loadUserDataPage = new Intent(_context, typeof(UsersDataUsageView));
			//		string username;
			//		for (int i = 0; i < _tileClickedOn.ChildCount; i++)
			//		{
			//			if (_tileClickedOn.GetChildAt(i).GetType() == typeof(TextView))
			//			{
			//				TextView userName = (TextView)_tileClickedOn.GetChildAt(i);
			//				username = userName.Text;
			//				loadUserDataPage.PutExtra("tag", _tileClickedOn.Id);
			//				_context.StartActivity(loadUserDataPage);
			//			}
			//		}
			//	};

			double conversion;
			int userID;
			foreach (LinearLayout tile in AllocationPageCustomUserTilesPage.UserTiles)
			{
				for (int i = 0; i < tile.ChildCount; i ++)
				{
					if (tile.GetChildAt(i).GetType() == typeof(SeekBar))
					{
						SeekBar seekbar = (SeekBar)tile.GetChildAt(i);
						userID = tile.Id;
						conversion = seekbar.Progress / 100.0 * Controller._planDataPool;
						var azb = Controller._totalRemainder;
						Controller._users.Where(x => Int32.Parse(x.UID).Equals(userID))
											.ToList()
											.ForEach(x =>
											{
												x.Allocated = conversion;
											});
						//Controller._users.ForEach(y => if (y.uid == tile) 
						//{ 
						//} y.Allocated = conversion);
						//_user.Allocated = conversion;

						//Controller._users.ForEach(
					}
				}
			}

			var changedUser = await Controller.UpdateAllocation(Controller._users);
				//Controller._users[_uid] = changedUser;

		}


		//protected async void UpdateUserDataAllocation(object sender, EventArgs e)
		//{
		//	Controller._totalUnAllocated = _tempUnAllocated;
		//	Controller._users[0].Allocated = _tempUnAllocated;
		//	User changedUser = await Controller.UpdateAllocation(_user, _progressChanged);
		//Controller._users[_uid] = changedUser;
		//	Toast.MakeText(this, StringConstants.Localizable.SavedChangesMessage, ToastLength.Long).Show();
		////}

	}
}