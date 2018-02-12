using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		public static User _userLoggedIn;
		public TodoItemManager cosmosDB;
		public static double _totalRemainder;
		public static double _totalAllocated;
		public static double _totalUnAllocated;
		public static double _totalUsed;
		public static double _planDataPool;
		public static DateTime _currentDate;
		public static double _daysRemaining;
		public static double _addOns = 0;

		public Controller(List<User> user)
		{
			_users = user;
			_userLoggedIn = _users[0];
			_currentDate = DateTime.Now;

			var changeDate = user[0].PlanEndDate.Split('/');
			DateTime newEndDate = new DateTime(Int32.Parse(changeDate[2]), Int32.Parse(changeDate[0]), Int32.Parse(changeDate[1]));
			var printString = newEndDate.ToString("dd/MM/yyyy");
			_daysRemaining = Math.Ceiling((newEndDate - _currentDate).TotalDays);
			SetGlobalValues();
    //        if (_users[0].AdminStatus)
    //        {
    //            //_users[0].Allocated = _planDataPool - _totalAllocated;
				//_users[0].Allocated = 
    //        }
			
			Console.WriteLine("Controller successfully loaded and all contents are ready to go!");

		}

		public static void SetGlobalValues() 
		{
			_totalAllocated = _totalUsed = 0;
			_users.ForEach(x => _totalAllocated += x.Allocated);
			_users.ForEach(x => _totalUsed += x.Used);
			_addOns = _users[0].AddOns;
			_planDataPool = _users[0].Plan;
			_totalRemainder = _planDataPool - _totalUsed + _addOns;
			_totalUnAllocated = _planDataPool - _totalAllocated + _addOns;
		}

		public static async Task<List<User>> UpdateAllocation(List<User> users)
		{
			var changedUser = await TodoItemManager.DefaultManager.UpdateMemberAllocation(users);
			return changedUser;
		}

		public static async Task<User> AddGroupMember(User user, Member newMember)
		{
			User changedUser = await TodoItemManager.DefaultManager.CreateNewUser(user, newMember);
			return changedUser;
		}

        public static async Task<User> DeleteGroupMemeber(User user, User targetMember)
        {
            User changedUser = await TodoItemManager.DefaultManager.DeleteGroupMember(user, targetMember);
            return changedUser;
        }

		public static async Task<User> BuyAddOns(User user, double addOnAmount)
		{
			User changedUser = await TodoItemManager.DefaultManager.BuyAddOns(user, addOnAmount);
			return changedUser;
		}

        public static void Clear()
        {
            TodoItemManager.DefaultManager.ClearAll();
        }
    }
} 