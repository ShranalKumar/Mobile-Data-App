using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace MobileApp.Droid.Views
{
	public class GoogleMapsView : BaseAdapter
	{
		private AdminDashboardView _context;
		private View _view;
		private MapView _mapFragment;

		public override int Count => throw new NotImplementedException();

		public GoogleMapsView(AdminDashboardView context) : base()
		{
			_context = context;
			GetView(0, null, null);
		}

		protected void findAllElements(View view)
		{
			_mapFragment = view.FindViewById<MapView>(Resource.Id.GoogleMap);
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
			_view = _context.LayoutInflater.Inflate(Resource.Layout.GoogleMapsLayout, null);

			findAllElements(_view);

			if (_mapFragment == null)
			{
				GoogleMapOptions mapOptions = new GoogleMapOptions()
					.InvokeMapType(GoogleMap.MapTypeSatellite)
					.InvokeZoomControlsEnabled(false)
					.InvokeCompassEnabled(true);
			}

			_mapFragment.GetMapAsync(null);
			return _view;
		}

		public View GetView()
		{
			return _view;
		}

	}
}