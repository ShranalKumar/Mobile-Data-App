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
		class Controller
		{
		//private string[] _nameList;
		//private int[] _usedList;
		//private int[] _allocatedList;
		//private int[] _remainder;
		public List<string> _uid; /*{ get; set; }*/
		public List<string> _fullname;
		public List<int> _used;
		public List<int> _allocated;
		public List<int> _remainder;
		public List<string> _appName;
		public List<string> _appUsage;

			//public ViewModel(string[] nameList, int[] usedList, int[] allocatedList, int[] remainder)
			//{
			//	this._nameList = nameList;
			//	this._usedList = usedList;
			//	this._allocatedList = allocatedList;
			//	this._remainder = remainder;
			//}

			public Controller(List<string> uid, List<string> fullname, List<int> used, List<int> allocated, List<int> remainder, List<string> appName, List<string> appUsage)
			{
				this._uid = uid;
				this._fullname = fullname;
				this._used = used;
				this._allocated = allocated;
				this._remainder = remainder;
				this._appName = appName;
				this._appUsage = appUsage;
			}
		}
	}
