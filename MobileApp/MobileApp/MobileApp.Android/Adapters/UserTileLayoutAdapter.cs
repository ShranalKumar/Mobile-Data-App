using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileApp.Droid.Views;

namespace MobileApp.Droid.Adapters
{
    public class UserTileLayoutAdapter : BaseAdapter
    {
        AdminDashboardView context;

        public UserTileLayoutAdapter(AdminDashboardView context)
        {
            this.context = context;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, null);
            }

            TextView UserName = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            UserName.Text = "Louise";
            UserName.TextSize = 15;
            UserName.SetTypeface(null, TypefaceStyle.Bold);
                

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return 0;
            }
        }
    }
}