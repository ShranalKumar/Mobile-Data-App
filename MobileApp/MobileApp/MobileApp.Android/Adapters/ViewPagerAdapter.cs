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
        private List<View> _views = new List<View>();
        private AdminDashboardContentView _adminDashboardContentView;
        private AllocationPageView _allocationPageView;
        

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

        public void GetAllViews(ViewPager pager)
        {
            _allocationPageView  = new AllocationPageView(_context);
            _views.Add(_allocationPageView.GetView());
            NotifyDataSetChanged();
            _views.ForEach(x => pager.AddView(x));
        }

        public override Java.Lang.Object InstantiateItem(View container, int position)
        {            
            return _allocationPageView.GetView();
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object @object)
        {
            base.DestroyItem(container, position, @object);
        }
    }
}