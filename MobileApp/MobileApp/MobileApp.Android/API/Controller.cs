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
		private List<string> _uid; /*{ get; set; }*/
		private List<string> _fullname;
		private List<int> _used;
		private List<int> _allocated;
		private List<int> _remainder;
		private List<string> _appName;
		private List<string> _appUsage;
		private DateTime _startDate;
		private DateTime _endDate;
		private DateTime _currentDate;
		private double _daysRemaining;


		public Controller(List<string> uid, List<string> fullname, List<int> used, List<int> allocated, List<int> remainder, List<string> appName, List<string> appUsage,string startDate, string endDate)
		{
			this._uid = uid;
			this._fullname = fullname;
			this._used = used;
			this._allocated = allocated;
			this._remainder = remainder;
			this._appName = appName;
			this._appUsage = appUsage;
			this._startDate = Convert.ToDateTime(startDate);
			this._endDate = Convert.ToDateTime(endDate);

			_currentDate = DateTime.Now;

			_daysRemaining = Math.Ceiling((_endDate - _currentDate).TotalDays);
			//_daysRemaining = (_startDate - _endDate).TotalDays;

			Console.WriteLine("Controller successfully loaded and all contents are ready to go!");






		}
	}
}
