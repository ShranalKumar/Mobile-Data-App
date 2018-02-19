using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileApp.Constants;
using MobileApp.Droid.Converters;
using MobileApp.Droid.Helpers;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TransferView : Activity
    {
        private TextView _transferPageTitle;
        private TextView _sendToText;
        private TextView _dataAmountText;
        private TextView _firstNumber;
        private TextView _secondNumber;
        private TextView _thirdNumber;
        private TextView _fourthNumber;
        private TextView _dataUnitsToGB;
        private TextView _decimalPointVisibility;
        private TextView _transferDialogDisplayText;
        private TextView _successfullyTransferedMessage;

        private double _dataAmountDouble;

        private Button _sendButtonClicked;
        private List<Button> _userButtons;
        private Button _selectedUser;

        private ImageButton _firstUpArrow;
        private ImageButton _secondUpArrow;
        private ImageButton _thirdUpArrow;
        private ImageButton _fourthUpArrow;
        private ImageButton _firstDownArrow;
        private ImageButton _secondDownArrow;
        private ImageButton _thirdDownArrow;
        private ImageButton _fourthDownArrow;
        private ImageButton _BackButton;

        private TextView _dataRemainingText;
        private TextView _gbRemainingText;

        private RelativeLayout _transferConfirmationPopUp;
        private RelativeLayout _transferSuccessMessage;
        private LinearLayout _userSelectionSlidingLayout;

        private ProgressBar _progressBarFill;
        private DataAmountSelectorHelper _selectorHelper;

        private string _getDataAmount;
        private string _getDataUnit;
		private double _transferAmount;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TransferLayout);

            findAllElements();
            DataBarFill();
            setAllStringConstants();
            CustomSlidingTilesView.CreateSlidingTilesView(_userSelectionSlidingLayout);
            _userButtons = CustomSlidingTilesView._userButtons;
            _selectorHelper = new DataAmountSelectorHelper();

            SetClickable();
        }

        public void DataBarFill()
        {
            double _fillNumber = (1 - Controller._users[0].Used / Controller._users[0].Allocated) * 100;
            _progressBarFill.Progress = (int)_fillNumber;
        }

        protected void findAllElements()
        {
            _transferPageTitle = FindViewById<TextView>(Resource.Id.TransferPageTitle);
            _sendToText = FindViewById<TextView>(Resource.Id.SendDataText);
            _dataAmountText = FindViewById<TextView>(Resource.Id.DataAmountSelectorTitleText);
            _firstNumber = FindViewById<TextView>(Resource.Id.FirstNumberText);
            _secondNumber = FindViewById<TextView>(Resource.Id.SecondNumberText);
            _thirdNumber = FindViewById<TextView>(Resource.Id.ThirdNumberText);
            _fourthNumber = FindViewById<TextView>(Resource.Id.FourthNumberText);
            _dataUnitsToGB = FindViewById<TextView>(Resource.Id.DataTransferUnits);
            _decimalPointVisibility = FindViewById<TextView>(Resource.Id.DataUnitDecimalText);
            _firstUpArrow = FindViewById<ImageButton>(Resource.Id.FirstUpArrow);
            _secondUpArrow = FindViewById<ImageButton>(Resource.Id.SecondUpArrow);
            _thirdUpArrow = FindViewById<ImageButton>(Resource.Id.ThirdUpArrow);
            _fourthUpArrow = FindViewById<ImageButton>(Resource.Id.FourthUpArrow);
            _firstDownArrow = FindViewById<ImageButton>(Resource.Id.FirstDownArrow);
            _secondDownArrow = FindViewById<ImageButton>(Resource.Id.SecondDownArrow);
            _thirdDownArrow = FindViewById<ImageButton>(Resource.Id.ThirdDownArrow);
            _fourthDownArrow = FindViewById<ImageButton>(Resource.Id.FourthDownArrow);
            _dataRemainingText = FindViewById<TextView>(Resource.Id.DataRemainingTitleText);
            _gbRemainingText = FindViewById<TextView>(Resource.Id.DataRemainingTextInsidePgBar);
            _progressBarFill = FindViewById<ProgressBar>(Resource.Id.DataRemainingFillMask);
            _sendButtonClicked = FindViewById<Button>(Resource.Id.SendButton);
            _BackButton = FindViewById<ImageButton>(Resource.Id.TransferBackButton);
            _userSelectionSlidingLayout = FindViewById<LinearLayout>(Resource.Id.UserSelectionSlidingLayout);
        }

        protected void setAllStringConstants()
        {
            _transferPageTitle.Text = StringConstants.Localizable.Transfer;
            _sendToText.Text = StringConstants.Localizable.SendDataTo;
            _dataAmountText.Text = StringConstants.Localizable.SelectAmount;
            _firstNumber.Text = StringConstants.Localizable.InitialAmount;
            _secondNumber.Text = StringConstants.Localizable.InitialAmount;
            _thirdNumber.Text = StringConstants.Localizable.InitialAmount;
            _fourthNumber.Text = StringConstants.Localizable.InitialAmount;
            _dataUnitsToGB.Text = StringConstants.Localizable.MBUnit;
            _dataRemainingText.Text = StringConstants.Localizable.DataRemaining;
            _gbRemainingText.Text = string.Format(StringConstants.Localizable.GbRemaining, Math.Round((Controller._userLoggedIn.Allocated - Controller._userLoggedIn.Used), 2));
            _sendButtonClicked.Text = StringConstants.Localizable.SendButton;
            
        }

        public void SetClickable()
        {
            _firstUpArrow.Click += ArrowClicked;
            _secondUpArrow.Click += ArrowClicked;
            _thirdUpArrow.Click += ArrowClicked;
            _fourthUpArrow.Click += ArrowClicked;
            _firstDownArrow.Click += ArrowClicked;
            _secondDownArrow.Click += ArrowClicked;
            _thirdDownArrow.Click += ArrowClicked;
            _fourthDownArrow.Click += ArrowClicked;
            _sendButtonClicked.Click += showConfirmationPopUp;
            _BackButton.Click += delegate { Finish(); };

            foreach (Button user in _userButtons)
            {
                user.Click += (o, s) =>
                {
                    _userButtons.ForEach(x => x.SetBackgroundResource(Resource.Drawable.RoundedBorderButton));
                    _userButtons.ForEach(x => x.SetTextColor(Color.ParseColor("#3f3f3f")));

                    user.SetBackgroundResource(Resource.Drawable.RoundedBorderButtonClicked);
                    user.SetTextColor(Color.White);
                    _selectedUser = user;
                };
            }
        }

        private void ArrowClicked(object sender, EventArgs e)
        {
            ImageButton _arrowClicked = (ImageButton)sender;

            switch (_arrowClicked.Id)
            {
                case Resource.Id.FirstUpArrow:
                    _selectorHelper.IncreaseSelector(_arrowClicked.Id, _firstNumber);
                    setGbOrMb(Int32.Parse(_firstNumber.Text));
                    break;

                case Resource.Id.SecondUpArrow:
                    _selectorHelper.IncreaseSelector(_arrowClicked.Id, _secondNumber);
                    break;

                case Resource.Id.ThirdUpArrow:
                    _selectorHelper.IncreaseSelector(_arrowClicked.Id, _thirdNumber);
                    break;

                case Resource.Id.FourthUpArrow:
                    _selectorHelper.IncreaseSelector(_arrowClicked.Id, _fourthNumber);
                    break;

                case Resource.Id.FirstDownArrow:
                    _selectorHelper.DecreaseSelector(_arrowClicked.Id, _firstNumber);
                    setGbOrMb(Int32.Parse(_firstNumber.Text));
                    break;

                case Resource.Id.SecondDownArrow:
                    _selectorHelper.DecreaseSelector(_arrowClicked.Id, _secondNumber);
                    break;

                case Resource.Id.ThirdDownArrow:
                    _selectorHelper.DecreaseSelector(_arrowClicked.Id, _thirdNumber);
                    break;

                case Resource.Id.FourthDownArrow:
                    _selectorHelper.DecreaseSelector(_arrowClicked.Id, _fourthNumber);
                    break;
            }
            ChangeAmountColor();
        }

        private void ChangeAmountColor()
        {
            _getDataAmount = _firstNumber.Text + _decimalPointVisibility.Text + _secondNumber.Text + _thirdNumber.Text + _fourthNumber.Text;
            _dataAmountDouble = Double.Parse(_getDataAmount);
            _transferAmount = _dataAmountDouble;

            if (_getDataUnit == StringConstants.Localizable.MBUnit)
            {
                _transferAmount = _dataAmountDouble / 1000.0;
            }

            if ((Controller._userLoggedIn.Allocated - Controller._userLoggedIn.Used) < _transferAmount)
            {
                _firstNumber.SetTextColor(CoreColorConverter.GetColor(ColorConstants.RedMoreThanRemianing));
                _secondNumber.SetTextColor(CoreColorConverter.GetColor(ColorConstants.RedMoreThanRemianing));
                _thirdNumber.SetTextColor(CoreColorConverter.GetColor(ColorConstants.RedMoreThanRemianing));
                _fourthNumber.SetTextColor(CoreColorConverter.GetColor(ColorConstants.RedMoreThanRemianing));

            }
            else if (((Controller._userLoggedIn.Allocated - Controller._userLoggedIn.Used) > _transferAmount))
            {
                _firstNumber.SetTextColor(CoreColorConverter.GetColor(ColorConstants.YellowDataInLimit));
                _secondNumber.SetTextColor(CoreColorConverter.GetColor(ColorConstants.YellowDataInLimit));
                _thirdNumber.SetTextColor(CoreColorConverter.GetColor(ColorConstants.YellowDataInLimit));
                _fourthNumber.SetTextColor(CoreColorConverter.GetColor(ColorConstants.YellowDataInLimit));
            }
        }
        
        private void setGbOrMb(int valueOfFirstNumber)
        {
            if (valueOfFirstNumber > 0)
            {
                _dataUnitsToGB.Text = StringConstants.Localizable.GBUnit;
                _decimalPointVisibility.Text = StringConstants.Localizable.DecimalPoint;
            }
            else
            {
                _dataUnitsToGB.Text = StringConstants.Localizable.MBUnit;
                _decimalPointVisibility.Text = "";
            }
        }

        private void showConfirmationPopUp(object sender, EventArgs e)
        {
			_getDataAmount = _firstNumber.Text + _decimalPointVisibility.Text + _secondNumber.Text + _thirdNumber.Text + _fourthNumber.Text;
            _dataAmountDouble = Double.Parse(_getDataAmount);
            _getDataUnit = _dataUnitsToGB.Text;

			try
			{
                _transferAmount = _dataAmountDouble;
                if (_getDataUnit == StringConstants.Localizable.MBUnit)
                {
                    _transferAmount = _dataAmountDouble / 1000.0;
                }

                if ((Controller._userLoggedIn.Allocated - Controller._userLoggedIn.Used) >= _transferAmount && _transferAmount != 0)
				{
                    Dialog transferDialog = DialogHelper.ConfirmTransfer(this, _dataAmountDouble.ToString(), _getDataUnit, _selectedUser.Text, TransferDataAsync);
                    transferDialog.Show();
				}
				else if (_transferAmount == 0)
				{
                    Dialog zerAmountDialog = DialogHelper.ZeroAmount(this);
                    zerAmountDialog.Show();
                }
				else
				{
                    Dialog transferAmountDialog = DialogHelper.TransferAmount(this);
                    transferAmountDialog.Show();
				}
			}
			catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Dialog selectUserDialog = DialogHelper.SelectUser(this);
                selectUserDialog.Show();
			}		
		}

        private void showSuccessMessage(object sender, EventArgs e)
        {
            Dialog successDialog = DialogHelper.TransferSuccess(this, _dataAmountDouble.ToString(), _getDataUnit, _selectedUser.Text);
			successDialog.Show();
			
            _firstNumber.Text = StringConstants.Localizable.InitialAmount;
            _secondNumber.Text = StringConstants.Localizable.InitialAmount;
            _thirdNumber.Text = StringConstants.Localizable.InitialAmount;
            _fourthNumber.Text = StringConstants.Localizable.InitialAmount;
            _dataUnitsToGB.Text = StringConstants.Localizable.MBUnit;
            _decimalPointVisibility.Text = "";
			DataBarFill();
			_gbRemainingText.Text = string.Format(StringConstants.Localizable.GbRemaining, Math.Round((Controller._userLoggedIn.Allocated - Controller._userLoggedIn.Used), 2));

		}

		private async void TransferDataAsync(object sender, EventArgs e)
		{
			User targetuser = Controller._users.Where(x => x.Name.FirstName == _selectedUser.Text).FirstOrDefault();
			User changedUser = await Controller.TransferData(Controller._userLoggedIn, targetuser, _transferAmount);
			Controller._userLoggedIn = changedUser;
			showSuccessMessage(sender, e);
		}
    }
}