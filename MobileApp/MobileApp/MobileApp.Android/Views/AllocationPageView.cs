using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using RadialProgress;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme")]
    public class AllocationPageView : Activity
    {
        private ImageButton _backButton;
        private TextView _allocatePageTitle;
        private RadialProgressView _radialProgress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AllocationPageLayout);
            findAllElements();
            setAllStringConstants();

            var pixelToDp = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;

            //Radial Progress
            _radialProgress = FindViewById<RadialProgressView>(Resource.Id.RadialProgressCircle);
            _radialProgress.LayoutParameters.Height = 100 * pixelToDp;
            _radialProgress.LayoutParameters.Width = 100 * pixelToDp;
            _radialProgress.LabelHidden = true;

            _backButton.SetImageResource(Resource.Drawable.ArrowBackIcon);
            _backButton.Click += delegate { Finish(); };
            _allocatePageTitle.Text = "Allocate Data";           
        }

        protected void findAllElements()
        {
            _backButton = FindViewById<ImageButton>(Resource.Id.AllocationBackButton);
            _allocatePageTitle = FindViewById<TextView>(Resource.Id.AllocationPageTitle);
        }

        protected void setAllStringConstants()
        {

        }
    }
}