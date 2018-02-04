using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace MobileApp.Droid.Helpers
{
    public class CustomPlanOverviewView : ContentView
    {

        public static List<TextView> MemberNamesList;

        public static void getMembers(LinearLayout parent)
        {
            MemberNamesList = new List<TextView>();

            int count = 1;
            string _firstName;
            string _lastName;

            foreach (User user in Controller._users)
            {
                ContextThemeWrapper userNameContext = new ContextThemeWrapper(parent.Context, Resource.Style.OverviewPageMemberNames);
                TextView UserName = new TextView(userNameContext);
                UserName.Id = Int32.Parse(user.UID);
                _firstName = user.Name.FirstName;
                _lastName = user.Name.LastName;
                UserName.Text = count.ToString() + ". " + _firstName + " " + _lastName;

                count++;
                
                parent.AddView(UserName);
                MemberNamesList.Add(UserName);

            }

            Android.Widget.Button addButton = new Android.Widget.Button(parent.Context);
            addButton.Text = "+ Add";
			addButton.Click += AddGroupMember;

            parent.AddView(addButton);
        }

		public static async void AddGroupMember(object sender, EventArgs e)
		{
			User changedUser = await Controller.AddGroupMember(Controller._userLoggedIn, new Member());
			Controller._users[0] = changedUser;
		}
		public static async void DeleteGroupMember(object sender, EventArgs e)
		{
			User changedUser = await Controller.Delete
		}
    }
}