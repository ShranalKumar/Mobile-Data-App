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
using MobileApp.Droid.Views;

namespace MobileApp.Droid.Adapters
{
    public class GamificationViewAdapter : BaseAdapter
    {
        private NonAdminDashBoardView _context;
        private View _view;

        public GamificationViewAdapter(NonAdminDashBoardView context)
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
            _view = _context.LayoutInflater.Inflate(Resource.Layout.GamificationLayout, null);
            return _view;
        }
    }
}