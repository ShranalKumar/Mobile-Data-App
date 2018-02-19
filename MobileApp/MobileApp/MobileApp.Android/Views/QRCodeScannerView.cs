using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace MobileApp.Droid.Views
{
	[Activity(Label="QRScanner", Theme="@style/Theme.AppCompat.Light.NoActionBar")]
	public class QRCodeScannerView : AppCompatActivity
	{
		private SurfaceView _cameraPreview;
		//private BarcodeDetector _barcodeDetector;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.QRcodeScannerLayout);

			// Create your application here
		}

		private void OpenScanner(object sender, EventArgs e)
		{
			
		}
	}
}