using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileApp.Droid
{
	class ViewModel
	{
		private string[] _nameList;
		private int[] _usedList;
		private int[] _allocatedList;
		private int[] _remainder;

		public ViewModel(string[] nameList, int[] usedList, int[] allocatedList, int[] remainder)
		{
			this._nameList = nameList;
			this._usedList = usedList;
			this._allocatedList = allocatedList;
			this._remainder = remainder;
		}


	}
}