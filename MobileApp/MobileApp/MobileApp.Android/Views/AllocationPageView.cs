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

            //Radial Progress            
            //_radialProgress.LayoutParameters.Height = 100 * pixelToDp;
            //_radialProgress.LayoutParameters.Width = 100 * pixelToDp;
            //_radialProgress.LabelHidden = true;

            return _view;
        }

        public View GetView()
        {
            return _view;
        }
    }
}