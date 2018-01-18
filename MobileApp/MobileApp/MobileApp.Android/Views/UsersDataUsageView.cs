using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileApp.Droid.Views
{
    [Activity(Label = "UsersDataUsageView", ScreenOrientation = ScreenOrientation.Portrait)]
    public class UsersDataUsageView : Activity
    {

        private SeekBar _allocationSlider;
        private TextView _allocatedDataAmount;


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UsersDataUsageLayout);



            _allocatedDataAmount = FindViewById<TextView>(Resource.Id.AllocatedDataAmount);

            _allocationSlider = FindViewById<SeekBar>(Resource.Id.AllocationSlider);
            _allocationSlider.Progress = Int32.Parse(_allocatedDataAmount.Text);

            _allocationSlider.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
                if (e.FromUser)
                {
                    _allocatedDataAmount.Text = string.Format("{0}", e.Progress);
                }
            };


            // Create your application here
        }
    }
}