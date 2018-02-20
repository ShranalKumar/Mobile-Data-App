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
using MobileApp.Constants;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme.Dialog", ScreenOrientation = ScreenOrientation.Portrait)]
    public class QRCodeView : Activity
	{
        private string _username;
        private string _password;
        private string _firstName;
        private ImageView _imageView;
        private TextView _qrTitleText;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.QRCodeLayout);

            _qrTitleText = FindViewById<TextView>(Resource.Id.QRTitleText);
			_imageView = FindViewById<ImageView>(Resource.Id.QRCodeImageView);

            _username = Intent.GetStringExtra("username");
            _password = Intent.GetStringExtra("password");
            _firstName = Intent.GetStringExtra("firstname");
            _qrTitleText.Text = String.Format(StringConstants.Localizable.QRTitleText, _firstName);


            _imageView.SetImageBitmap(QRAuth.GenerateQRCode(_username, _password));
		}
	}
}