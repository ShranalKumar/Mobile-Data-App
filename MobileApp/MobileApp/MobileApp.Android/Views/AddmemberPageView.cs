using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileApp.Constants;

namespace MobileApp.Droid.Views
{
    [Activity(Label = "TrustPowerMobile", ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/trust")]
    public class AddmemberPageView : Activity
    {

        private TextView _addMemberPageTitle;
        private EditText _phoneNumberTitle;
        private EditText _firstNametitle;
        private EditText _lastNameTitle;
        private CheckBox _adminStatusTitle;
        private Button _addButtonTitle;
        private ImageButton _addMemberBackButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddNewMemberLayout);

            findAllElements();
            setAllStringConstants();

            _addMemberBackButton.Click += delegate { Finish(); };
            _addButtonTitle.Click += AddNewMember;
        }

        protected void findAllElements()
        {
            _addMemberPageTitle = FindViewById<TextView>(Resource.Id.AddMemberPageTitle);
            _phoneNumberTitle = FindViewById<EditText>(Resource.Id.PhoneNumberInputField);
            _firstNametitle = FindViewById<EditText>(Resource.Id.FirstNameInputField);
            _lastNameTitle = FindViewById<EditText>(Resource.Id.LastNameInputField);
            _adminStatusTitle = FindViewById<CheckBox>(Resource.Id.AdminRightsCheckBox);
            _addButtonTitle = FindViewById<Button>(Resource.Id.AddButton);
            _addMemberBackButton = FindViewById<ImageButton>(Resource.Id.AddMemberBackButton);

        }

        protected void setAllStringConstants()
        {
            _addMemberPageTitle.Text = StringConstants.Localizable.AddMemberTitle;
            _phoneNumberTitle.Hint = StringConstants.Localizable.PhoneNumber;
            _firstNametitle.Hint = StringConstants.Localizable.FirstName;
            _lastNameTitle.Hint = StringConstants.Localizable.LastName;
            _adminStatusTitle.Text = StringConstants.Localizable.AdminStatus;
        }

        protected async void AddNewMember(object sender, EventArgs e)
        {
            Member newUser = new Member();
            newUser.UID = _phoneNumberTitle.Text;
            newUser.Name = new UserName();
            newUser.Name.FirstName = _firstNametitle.Text;
            newUser.Name.LastName = _lastNameTitle.Text;
            newUser.AdminStatus = _adminStatusTitle.Checked;
            newUser.Used = 0;
            newUser.Allocated = 0;
            newUser.UsageBreakdown = new List<UserUsageBreakdown>();

            User changedUser = await Controller.AddGroupMember(Controller._userLoggedIn, newUser);
            Controller._userLoggedIn = changedUser;
            Controller._users[Controller._users.IndexOf(Controller._userLoggedIn)] = changedUser;
            Toast.MakeText(this, StringConstants.Localizable.NewMemberAddedToast, ToastLength.Short).Show();
            Finish();
        }
    }
}