using System;
using System.Collections.Generic;
using System.IO;
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
    [Activity(Theme = "@style/MainTheme.Dialog", ScreenOrientation = ScreenOrientation.Portrait)]
    public class QRCodeView : Activity
	{
        private string _username;
        private string _password;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.QRCodeLayout);
			ImageView imageView = FindViewById<ImageView>(Resource.Id.QRCodeImageView);

            _username = Intent.GetStringExtra("username");
            _password = Intent.GetStringExtra("password");

			imageView.SetImageBitmap(QRAuth.GenerateQRCode(_username, _password));
		}
	}
}