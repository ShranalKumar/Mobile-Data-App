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
		TodoItemManager manager;
		private string _loginid;
		private string _password;
		private List<string> _uid; /*{ get; set; }*/
		private List<string> _fullname;
		private List<int> _used;
		private List<int> _allocated;
		private List<int> _remainder;
		private List<string> _appName;
		private List<string> _appUsage;
		private string _startDate;
		private string _endDate;
		private DateTime _daysRemaining;

		//public ViewModel(string[] nameList, int[] usedList, int[] allocatedList, int[] remainder)
		//{
		//	this._nameList = nameList;
		//	this._usedList = usedList;
		//	this._allocatedList = allocatedList;
		//	this._remainder = remainder;
		//}

		public Controller(List<string> uid, List<string> fullname, List<int> used, List<int> allocated, List<int> remainder, List<string> appName, List<string> appUsage /*string startDate, string endDate*/)
		{
			this._uid = uid;
			this._fullname = fullname;
			this._used = used;
			this._allocated = allocated;
			this._remainder = remainder;
			this._appName = appName;
			this._appUsage = appUsage;
			//this._startDate = startDate;
			//this._endDate = endDate;

		}
	}
}
