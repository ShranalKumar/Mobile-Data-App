using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileApp.Droid;
using MobileApp.Constants;

namespace MobileApp.Droid.Views
{
    [Activity(Label = "TrustPowerMobile", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/icon")]
    public class LoginView : Activity
    {
        private Button _loginButtonClicked;
        private LinearLayout _usernameField;
		private LinearLayout _passwordField;
		private EditText _userInputID;
		private EditText _userInputPassword;
		private string _loginId;
		private string _password;
		private TextView _text;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LoginLayout);

            findAllElements();
            setAllStringConstants();
            
            _loginButtonClicked.Click += LoginButtonIsClickedAsync;

			_text = FindViewById<TextView>(Resource.Id.textView1);

		}

		private async void LoginButtonIsClickedAsync(object sender, EventArgs e)
        {
            _usernameField.Visibility = ViewStates.Visible;
            _passwordField.Visibility = ViewStates.Visible;
			_loginId = _userInputID.Text;
			_password = _userInputPassword.Text;

			LoginController logincontroller = new LoginController(_loginId, _password);
			await logincontroller.userLoginPhaseAsync();

			if (logincontroller.getLoginStatus() && logincontroller.getAdminStatus()) 
			{
				_text.Text = "Loged In As Admin";
				Intent intent = new Intent(this, typeof(AdminDashboardView));
				StartActivity(intent);
			}
			else if (logincontroller.getLoginStatus())
			{
				_text.Text = "Loged In As Non Admin";

				Intent intent = new Intent(this, typeof(NonAdminDashbaordView));
				StartActivity(intent);
			}
			else
			{
				Console.WriteLine("Log in Failed!");
			}
		}

        protected void findAllElements()
        {
            _loginButtonClicked = FindViewById<Button>(Resource.Id.LogInButton);
            _usernameField = FindViewById<LinearLayout>(Resource.Id.UsernameLayout);
            _passwordField = FindViewById<LinearLayout>(Resource.Id.PasswordLayout);
            _userInputID = FindViewById<EditText>(Resource.Id.UsernameInputField);
            _userInputPassword = FindViewById<EditText>(Resource.Id.PasswordInputField);
        }

        protected void setAllStringConstants()
        {
            _userInputID.Hint = StringConstants.Localizable.UsernameHint;
            _userInputPassword.Hint = StringConstants.Localizable.PasswordHint;
            _loginButtonClicked.Text = StringConstants.Localizable.LogIn;
        }
	}
}