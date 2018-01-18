﻿using System;
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

namespace MobileApp.Droid.Views
{
    [Activity(Label = "LoginView", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginView : Activity
    {
        private Button _loginButtonClicked;

        private LinearLayout _usernameField;
		private LinearLayout _passwordField;
		private EditText _userInputID;
		private EditText _userInputPassword;
		private string _loginId;
		private string _password;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LoginLayout);

            _loginButtonClicked = FindViewById<Button>(Resource.Id.LogInButton);
            _loginButtonClicked.Click += loginButtonIsClicked;
            //_loginButtonClicked.Click += delegate { StartActivity(typeof(TransferView)); };


            _usernameField = FindViewById<LinearLayout>(Resource.Id.UsernameLayout);
            _passwordField = FindViewById <LinearLayout > (Resource.Id.PasswordLayout);

			_userInputID = FindViewById<EditText>(Resource.Id.UsernameInputField);
			_userInputPassword = FindViewById<EditText>(Resource.Id.PasswordInputField);
            // Create your application here
        }

        private void loginButtonIsClicked(object sender, EventArgs e)
        {
            _usernameField.Visibility = ViewStates.Visible;
            _passwordField.Visibility = ViewStates.Visible;
			_loginId = _userInputID.Text;
			_password = _userInputPassword.Text;

			LoginController logincontroller = new LoginController(_loginId, _password);


			//throw new NotImplementedException();
		}
	}
}