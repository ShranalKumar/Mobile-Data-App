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
		public static List<User> _users;
		//public static List<string> _uid = new List<string>();
		//public static List<string> _firstname = new List<string>();
		//public static List<int> _used = new List<int>();
		//public static List<int> _allocated = new List<int>();
		public static int _totalRemainder;
		//public static Dictionary<string, List<string>> _appName;
		//public static Dictionary<string, List<string>> _appUsage;
		//public static DateTime _startDate;
		//public static DateTime _endDate;
		public static DateTime _currentDate;
		public static double _daysRemaining;
		//public static int _allocatedlistlength;
		//public static int _planDataPool;

		//public static string _nonadminfirstname;
		//public static int _nonadminused;
		//public static int _nonadminallocated;
		//public static DateTime _nonadminstartDate;
		//public static DateTime _nonadminendDate;
		//public static DateTime _nonadmincurrentDate;
		//public static double _nonadmindaysRemaining;
		//public static List<string> _nonadminappName;
		//public static List<string> _nonadminappUsage;
		//public static List<string> _groupmemeberfirstname;


		//public Controller(List<string> uid, List<string> firstname, List<int> used, List<int> allocated, List<int> remainder, List<string> appName, List<string> appUsage,string startDate, string endDate, int planDataPool)
		public Controller(List<User> user)
		{
			_users = user;
			//_uid.Add(user.UID);
			//_firstname.Add(user.Name.FirstName);
			//_used = used;
			//_allocated = allocated;
			//_remainder = remainder;
			//_appName = appName;
			//_appUsage = appUsage;
			//_startDate = Convert.ToDateTime(startDate);
			//_endDate = Convert.ToDateTime(endDate);
			//_planDataPool = planDataPool;

			_currentDate = DateTime.Now;
			//_allocatedlistlength = _allocated.Count();

			var changeDate = user[0].PlanEndDate.Split('/');
			DateTime newEndDate = new DateTime(Int32.Parse(changeDate[2]), Int32.Parse(changeDate[0]), Int32.Parse(changeDate[1]));
			var printString = newEndDate.ToString("dd/MM/yyyy");

			//for (int i = 0; i < _allocatedlistlength; i++)
			//{
			//	if ((_allocated[i] - _used[i]) < 0)
			//	{
			//		_remainder[i] = (_planDataPool - _allocated[i] - _used[i]);
			//	}
			//	else
			//	{
			//		_remainder[i] = _allocated[i] - _used[i];
			//	}
			//}
			_daysRemaining = Math.Ceiling((newEndDate - _currentDate).TotalDays);
			Console.WriteLine("Controller successfully loaded and all contents are ready to go!");

		}
		//public Controller (string firstname, int used, int allocated, List<string> appname, List<string> appusage, string startDate, string endDate, List<string> groupmemberfirstname)
		//{
		//	_nonadminfirstname = firstname;
		//	_nonadminused = used;
		//	_nonadminallocated = allocated;
		//	_nonadminstartDate = Convert.ToDateTime(startDate);
		//	_nonadminendDate = Convert.ToDateTime(endDate);
		//	_groupmemeberfirstname = groupmemberfirstname;
		//	var changeDate = endDate.Split('/');
		//	DateTime newEndDate = new DateTime(Int32.Parse(changeDate[2]), Int32.Parse(changeDate[0]), Int32.Parse(changeDate[1]));
		//	var printString = newEndDate.ToString("dd/MM/yyyy");
		//	_nonadminappName = appname;
		//	_nonadminappUsage = appusage;
		//	_nonadmindaysRemaining = Math.Ceiling((newEndDate - DateTime.Now).TotalDays);
		//}
	}
}
