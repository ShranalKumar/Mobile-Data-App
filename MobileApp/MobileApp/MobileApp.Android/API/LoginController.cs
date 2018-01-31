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
	public class LoginController
	{
		TodoItemManager manager;
		private string _loginid;
		private string _password;
		public LoginController(string _loginid, string _password)
		{
			this._loginid = _loginid;
			this._password = _password;
		}

		public async System.Threading.Tasks.Task userLoginPhaseAsync()
		{
			string loginQuery = String.Format("select * from t where t.uid = '{0}'", _loginid);
			manager = TodoItemManager.DefaultManager;

			await manager.GetTodoItemsAsync(loginQuery, _loginid, _password);
		}

		public Boolean getLoginStatus() 
		{
			return manager.getLoginStatus();
		}

		public Boolean getAdminStatus()
		{
			return manager.getAdminStatus();
		}


	}
}