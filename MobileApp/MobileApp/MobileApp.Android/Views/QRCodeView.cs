using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileApp.Droid.Views
{
	[Activity(Label = "QRCodeView")]
	public class QRCodeView : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.QRCodeLayout);
			ImageView imageView = FindViewById<ImageView>(Resource.Id.QRCodeImageView);
			
			imageView.SetImageBitmap(QRAuth.GenerateQRCode());

			// Create your application here
		}
	}
}