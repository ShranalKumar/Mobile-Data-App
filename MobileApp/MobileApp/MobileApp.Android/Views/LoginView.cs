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
    [Activity(Theme = "@style/MainTheme", Label = "TrustPowerMobile", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/trust")]
    public class LoginView : Activity
    {
        private Button _loginButtonClicked;
        private LinearLayout _usernameField;
		private LinearLayout _passwordField;
		private EditText _userInputID;
		private EditText _userInputPassword;
		private string _loginId;
		private string _password;

        private ProgressDialog progress;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
			

            SetContentView(Resource.Layout.LoginLayout);

            findAllElements();
            setAllStringConstants();
            
            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Retrieving your account info...");
            progress.SetCancelable(false);

            _loginButtonClicked.Click += LoginButtonIsClickedAsync;

			

		}

		private async void LoginButtonIsClickedAsync(object sender, EventArgs e)
        {
            progress.Show();
            Controller.Clear();
            _usernameField.Visibility = ViewStates.Visible;
            _passwordField.Visibility = ViewStates.Visible;
			_loginId = _userInputID.Text;
			_password = _userInputPassword.Text;

			LoginController logincontroller = new LoginController(_loginId, _password);
			await logincontroller.userLoginPhaseAsync();

			if (logincontroller.getLoginStatus() && logincontroller.getAdminStatus()) 
			{
				Intent intent = new Intent(this, typeof(AdminDashboardView));
				StartActivity(intent);
                Finish();
			}
			else if (logincontroller.getLoginStatus())
			{
				Intent intent = new Intent(this, typeof(NonAdminDashBoardView));
				StartActivity(intent);
			}
			else
			{
				Console.WriteLine("Log in Failed!");
			}

            progress.Hide();
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