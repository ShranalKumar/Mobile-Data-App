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

		public static Bitmap GenerateQRCode(/*string userid*/)
		{

			var writer = new BarcodeWriter
			{
				Format = BarcodeFormat.QR_CODE,
				Options = new ZXing.Common.EncodingOptions
				{
					Height = 600,
					Width = 600
				}
			};
			return writer.Write("0210752833 1");
			//var writer = new BarcodeWriter();
			//writer.Format = BarcodeFormat.QR_CODE;
			//writer.Renderer = new ZXing.Rendering.BitmapRenderer()
			//{
			//	 Background = Android.Graphics.Color.White,
			//	 Foreground = Android.Graphics.Color.Black
			//};
			//writer.Options.Height = 300;
			//writer.Options.Width = 300;
			//writer.Options.Margin = 1;

			//var bitmap = writer.Write(/*userid*/ "soumik is an idiot");
			//return bitmap;
		}


		//public static void ReadQRCode()
		//{
		//	var scanPage = new ZXingScannerPage();

		//	var reader = new BarcodeReader();
			
		//}
	}
}





