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

namespace MobileApp.Droid.Views
{
    [Activity(Label = "LoginView", MainLauncher = true, Icon ="@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginView : Activity
    {
        private Button _loginButtonClicked;

        private LinearLayout _usernameField;
        private LinearLayout _passwordField;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LoginLayout);

            _loginButtonClicked = FindViewById<Button>(Resource.Id.LogInButton);
            _loginButtonClicked.Click += loginButtonIsClicked;
            


            _usernameField = FindViewById<LinearLayout>(Resource.Id.UsernameLayout);
            _passwordField = FindViewById <LinearLayout > (Resource.Id.PasswordLayout);

            // Create your application here
        }

        private void loginButtonIsClicked(object sender, EventArgs e)
        {
            if (_usernameField.Visibility == ViewStates.Invisible && _passwordField.Visibility == ViewStates.Invisible)
            {
                _usernameField.Visibility = ViewStates.Visible;
                _passwordField.Visibility = ViewStates.Visible;
            }

            else
            {
                _loginButtonClicked.Click += delegate { StartActivity(typeof(TransferView)); };
            }

            //throw new NotImplementedException();
        }
    }
}