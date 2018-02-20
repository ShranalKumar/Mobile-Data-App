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
using ZXing.Mobile;
using Android.Support.V7.App;
using Android.Views.InputMethods;
using MobileApp.Droid.Converters;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", Label = "TrustPowerMobile", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@mipmap/trust")]
    public class LoginView : Activity
    {
        private Button _loginButtonClicked;
		private Button _qrSignInButton;
        private LinearLayout _usernameField;
		private LinearLayout _passwordField;
		private EditText _userInputID;
		private EditText _userInputPassword;
		private string _loginId;
		private string _password;
		private InputMethodManager _inputManager;

		private ProgressDialog progress;
		private ProgressDialog QRProgress;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
			MobileBarcodeScanner.Initialize(Application);

			SetContentView(Resource.Layout.LoginLayout);

            findAllElements();
            setAllStringConstants();
			SetClickActions();
            _inputManager = (InputMethodManager)GetSystemService(InputMethodService);
            _loginButtonClicked.Enabled = false;

            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Retrieving your account info...");
            progress.SetCancelable(false);

            QRProgress = new ProgressDialog(this);
            QRProgress.Indeterminate = true;
            QRProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
            QRProgress.SetMessage("Launching QR Scanner...");
            QRProgress.SetCancelable(false);

            if (!_loginButtonClicked.Enabled) { _loginButtonClicked.SetTextColor(CoreColorConverter.GetColor(ColorConstants.GreyDisabeledButtonText)); }
        }

        private async void LoginButtonIsClickedAsync(object sender, EventArgs e)
        {            
            Controller.Clear();
			_inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
			_usernameField.Visibility = ViewStates.Visible;
            _passwordField.Visibility = ViewStates.Visible;
			
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
				Finish();
			}
			else
			{
				Toast.MakeText(this, StringConstants.Localizable.LogInFailed, ToastLength.Short).Show();
			}

			progress.Hide();
		}

		private async void QRSignInButtonClickedAsync(object sender, EventArgs e)
		{
            QRProgress.Show();
			MobileBarcodeScanner.Initialize(Application);
			var scanner = new ZXing.Mobile.MobileBarcodeScanner();
			var result = await scanner.Scan();
            QRProgress.Hide();
            try
            {
                if (result.Text != null)
                {
                    var credentials = result.Text.Split();
                    _loginId = credentials[0];
                    _password = credentials[1];
                    progress.Show();
                    LoginButtonIsClickedAsync(sender, e);
                }
            }
            catch (Exception) { }            
		}

		protected void findAllElements()
        {
            _loginButtonClicked = FindViewById<Button>(Resource.Id.LogInButton);
            _usernameField = FindViewById<LinearLayout>(Resource.Id.UsernameLayout);
            _passwordField = FindViewById<LinearLayout>(Resource.Id.PasswordLayout);
            _userInputID = FindViewById<EditText>(Resource.Id.UsernameInputField);
            _userInputPassword = FindViewById<EditText>(Resource.Id.PasswordInputField);
			_qrSignInButton = FindViewById<Button>(Resource.Id.QRSignInButton);
        }

        protected void setAllStringConstants()
        {
            _userInputID.Hint = StringConstants.Localizable.UsernameHint;
            _userInputPassword.Hint = StringConstants.Localizable.PasswordHint;
            _loginButtonClicked.Text = StringConstants.Localizable.LogIn;
			_qrSignInButton.Text = StringConstants.Localizable.QRLogIn;
        }

		protected void SetClickActions() 
		{
			_loginButtonClicked.Click += (sender, e) =>
			{
				_loginId = _userInputID.Text;
				_password = _userInputPassword.Text;
				progress.Show();
				LoginButtonIsClickedAsync(sender, e);
			};

			_userInputPassword.EditorAction += (sender, e) =>
			{
				if (e.ActionId == ImeAction.Go)
				{
					_loginButtonClicked.PerformClick();
				}
			};

			_userInputID.AfterTextChanged += EnableLogin;
			_userInputPassword.AfterTextChanged += EnableLogin;
			_qrSignInButton.Click += QRSignInButtonClickedAsync;
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			_inputManager = (InputMethodManager)GetSystemService(InputMethodService);
			_inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
			return base.OnTouchEvent(e);
		}

		public void EnableLogin(object sender, EventArgs e)
		{
			if (_userInputID.Text != "" && _userInputPassword.Text != "")
			{
				_loginButtonClicked.Enabled = true;
                _loginButtonClicked.SetTextColor(CoreColorConverter.GetColor(ColorConstants.WhiteEnabeledButtonText));
            }
			else
			{
				_loginButtonClicked.Enabled = false;
                _loginButtonClicked.SetTextColor(CoreColorConverter.GetColor(ColorConstants.GreyDisabeledButtonText));
            }
        }
	}
}