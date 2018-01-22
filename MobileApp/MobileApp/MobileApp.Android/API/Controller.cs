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
	public class Controller
	{
		public static List<string> _uid; 
		public static List<string> _firstname;
		public static List<int> _used;
		public static List<int> _allocated;
		//public static List<int> _remainder;
		public static int[] _remainder;
		public static List<string> _appName;
		public static List<string> _appUsage;
		public static DateTime _startDate;
		public static DateTime _endDate;
		public static DateTime _currentDate;
		public static double _daysRemaining;
		public static int _allocatedlistlength;
		public static int _planDataPool;

		public static string _nonadminfirstname;
		public static int _nonadminused;
		public static int _nonadminallocated;
		public static DateTime _nonadminstartDate;
		public static DateTime _nonadminendDate;
		public static DateTime _nonadmincurrentDate;
		public static double _nonadmindaysRemaining;
		public static List<string> _nonadminappName;
		public static List<string> _nonadminappUsage;


		//public Controller(List<string> uid, List<string> firstname, List<int> used, List<int> allocated, List<int> remainder, List<string> appName, List<string> appUsage,string startDate, string endDate, int planDataPool)
		public Controller(List<string> uid, List<string> firstname, List<int> used, List<int> allocated, int[] remainder, List<string> appName, List<string> appUsage, string startDate, string endDate, int planDataPool)

		{
			_uid = uid;
			_firstname = firstname;
			_used = used;
			_allocated = allocated;
			_remainder = remainder;
			_appName = appName;
			_appUsage = appUsage;
			_startDate = Convert.ToDateTime(startDate);
			_endDate = Convert.ToDateTime(endDate);
			_planDataPool = planDataPool;

			_currentDate = DateTime.Now;
			_allocatedlistlength = _allocated.Count();


			for (int i = 0; i < _allocatedlistlength; i++)
			{
				if ((_allocated[i] - _used[i]) < 0)
				{
					_remainder[i] = (_planDataPool - _allocated[i] - _used[i]);
				}
				else
				{
					_remainder[i] = _allocated[i] - _used[i];
				}
			}

			_daysRemaining = Math.Ceiling((_endDate - _currentDate).TotalDays);
			Console.WriteLine("Controller successfully loaded and all contents are ready to go!");

		}

		public Controller (string firstname, int used, int allocated, List<string> appname, List<string> appusage, string startDate, string endDate)
		{
			_nonadminfirstname = firstname;
			_nonadminused = used;
			_nonadminallocated = allocated;
			_nonadminstartDate = Convert.ToDateTime(startDate);
			_nonadminendDate = Convert.ToDateTime(endDate);
			_nonadmincurrentDate = DateTime.Now;
			_nonadminappName = appname;
			_nonadminappUsage = appusage;

			_nonadmindaysRemaining = Math.Ceiling((_nonadminendDate - _nonadmincurrentDate).TotalDays);


		}


		public void LoadOntoScreen()
		{

		}

	}
}
