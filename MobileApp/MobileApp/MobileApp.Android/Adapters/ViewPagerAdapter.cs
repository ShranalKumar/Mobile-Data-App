using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MobileApp.Constants;
using MobileApp.Droid.Helpers;
using MobileApp.Droid.Views;

namespace MobileApp.Droid.Adapters
{
    public class ViewPagerAdapter : PagerAdapter
    {
        private AdminDashboardView _context;
        public static List<View> _views = new List<View>();      

        public override int Count
        {
            get { return _views.Count(); }
        }

        public ViewPagerAdapter(AdminDashboardView context)
        {
            _context = context;
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object obj)
        {
            return view == obj;
        }

        public int AddView(View view)
        {
            return AddView(view, _views.Count());
        }

        public int AddView(View view, int position)
        {
            _views.Insert(position, view);
            return position;
        }

        public void removeView(View view)
        {
            _views.Remove(_views[GetItemPosition(view)]);
        }

        public override Java.Lang.Object InstantiateItem(View container, int position)
        {            
            View view = _views[position];
            container.JavaCast<ViewPager>().AddView(view);
            return view;
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object @object)
        {
            container.JavaCast<ViewPager>().RemoveView(_views[position]);
        }

        public View GetView()
        {
            return _views[0];
        }

        public int GetItemPosition(View view)
        {
            Console.WriteLine(_views.IndexOf(view));
            return _views.IndexOf(view);
        }

        public void ClearViews()
        {
            _views.Clear();
        }

        public override int GetItemPosition(Java.Lang.Object @object)
        {
            return PositionNone;
        }
    }
}