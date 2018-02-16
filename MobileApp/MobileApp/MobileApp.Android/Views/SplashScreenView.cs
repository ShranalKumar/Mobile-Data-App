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
    [Activity(Label = "TrustPowerMobile", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/trust", Theme = "@style/MainTheme.Splash")]

    public class SplashScreenView : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(LoginView));
        }
    }
}