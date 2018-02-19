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
using Microcharts;
using Microcharts.Droid;
using MobileApp.Constants;
using MobileApp.Droid.Views;
using SkiaSharp;

namespace MobileApp.Droid.Adapters
{
    public class NonAdminContentAdapter : BaseAdapter
    {
        private NonAdminDashBoardView _context;
        private View _view;
        private TextView _remainingDaysNonAdmin;
        private TextView _gbRemainingNonAdmin;
        private Button _transferButton;
        private Button _requestButton;
        private RelativeLayout _remainingDataBarBorder;
        private ProgressBar _dataFillBar;
		private LinearLayout _linearlayoutforGraph;
		private ChartView _chartView;
		private User _user;
		private Entry[] _entries;


		public NonAdminContentAdapter(NonAdminDashBoardView context)
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
            _view = _context.LayoutInflater.Inflate(Resource.Layout.NonAdminDashboardContentLayout, null);

            findAllElements(_view);
            setAllStringConstants();
            DataBarFill();
			setGraph();

            _transferButton.Click += delegate { _context.StartActivity(typeof(TransferView)); };
            _requestButton.Click += delegate { _context.StartActivity(typeof(RequestView)); };

            return _view;
        }

        protected void findAllElements(View view)
        {
            _remainingDaysNonAdmin = view.FindViewById<TextView>(Resource.Id.RemainingDaysNonAdmin);
            _gbRemainingNonAdmin = view.FindViewById<TextView>(Resource.Id.DataRemainingTextInsidePgBar);
            _transferButton = view.FindViewById<Button>(Resource.Id.TransferButton);
            _requestButton = view.FindViewById<Button>(Resource.Id.RequestButton);
            _remainingDataBarBorder = view.FindViewById<RelativeLayout>(Resource.Id.DataRemainingPgBarLayout);
            _dataFillBar = view.FindViewById<ProgressBar>(Resource.Id.DataRemainingFillMask);
			_linearlayoutforGraph = view.FindViewById<LinearLayout>(Resource.Id.linearlayoutforGraph);
			_chartView = view.FindViewById<ChartView>(Resource.Id.chartViewforNonAdmin);
		}

        protected void setAllStringConstants()
        {
            _remainingDaysNonAdmin.Text = string.Format(StringConstants.Localizable.DaysRemaining, Controller._daysRemaining);
            _gbRemainingNonAdmin.Text = string.Format(StringConstants.Localizable.GbRemaining, Math.Round((Controller._users[0].Allocated - Controller._users[0].Used), 2));
            _transferButton.Text = StringConstants.Localizable.TransferButton;
            _requestButton.Text = StringConstants.Localizable.RequestButton;
        }

        public void DataBarFill()
        {
            double _fillNumber = (1 - (Controller._users[0].Used / Controller._users[0].Allocated)) * 100;
            _dataFillBar.Progress = (int)_fillNumber;
        }

        public View GetView()
        {
            return _view;
        }
		protected void setGraph()
		{
			_user = Controller._userLoggedIn;
			_entries = new Entry[_user.UsageBreakdown.Count];
			int number = 1;
			foreach (UserUsageBreakdown breakdown in _user.UsageBreakdown)
			{
				_entries[number - 1] = new Entry(Int32.Parse(breakdown.DataUsed))
				{
					Label = breakdown.Day,
					ValueLabel = breakdown.DataUsed,
					Color = SKColor.Parse("#ffffff"),
					TextColor = SKColor.Parse("#ffffff")
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