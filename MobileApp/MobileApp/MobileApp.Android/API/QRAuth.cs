using System.IO;
//using Java.IO;
using ZXing;
using ZXing.QrCode;
using System;
using ZXing.Common;
using Android.Graphics;
using Xamarin.Forms;
using ZXing.Mobile;

namespace MobileApp.Droid
{
	public class QRAuth
	{
		public static Bitmap GenerateQRCode(string username, string password)
		{
			var writer = new BarcodeWriter
			{
				Format = BarcodeFormat.QR_CODE,
				Options = new ZXing.Common.EncodingOptions
				{
					Height = 800,
					Width = 600
				}
			};

			return writer.Write(username + " " + password);
		}
	}
}





