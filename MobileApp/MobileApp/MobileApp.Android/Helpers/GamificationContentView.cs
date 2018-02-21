using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MobileApp.Droid.Helpers
{

	public class GamificationContentView : ContentView
	{
		public int Index { get; set; }
		public Image NormalImage { get; set; }
		public GamificationContentView() { }
		public GamificationContentView(Image normal, Image winner, int index)
		{
			Index = index;
			NormalImage = normal;

		}
	}

}