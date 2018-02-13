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
using MobileApp.Droid.Helpers;

namespace MobileApp.Droid.Views
{
    [Activity(Label = "RequestView", ScreenOrientation = ScreenOrientation.Portrait)]
    public class RequestView : Activity
    {
        private TextView _requestPageTitle;
        private TextView _requestDataFrom;
        private TextView _requestDataAmount;
        private TextView _firstNumber;
        private TextView _secondNumber;
        private TextView _thirdNumber;
        private TextView _fourthNumber;
        private TextView _dataUnitsToGB;
        private TextView _decimalPointVisibility;
        private TextView _requestDialogDisplayText;
        private TextView _successfullyRequestedMessage;

        private double _dataAmountDouble;

        private Button _requestButtonClicked;
        private Button _doNotRequest;
        private Button _yesToRequest;
        private Button _OkSuccessfullyRequested;

        private ImageButton _firstUpArrow;
        private ImageButton _secondUpArrow;
        private ImageButton _thirdUpArrow;
        private ImageButton _fourthUpArrow;
        private ImageButton _firstDownArrow;
        private ImageButton _secondDownArrow;
        private ImageButton _thirdDownArrow;
        private ImageButton _fourthDownArrow;
        private ImageButton _BackButton;

        private RelativeLayout _requestConfirmationPopUp;
        private RelativeLayout _requestSuccessMessage;
		private LinearLayout _userSelectionSlidingLayout;
        private List<Button> _userButtons;
        private Button _selectedUser;


		private string _getDataAmount;
        private string _getDataUnit;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RequestLayout);

            findAllElements();
            setAllStringConstants();
			CustomSlidingTilesView.CreateSlidingTilesView(_userSelectionSlidingLayout);
            _userButtons = CustomSlidingTilesView._userButtons;

            SetClickable();
            
        }

        protected void findAllElements()
        {
            _requestPageTitle = FindViewById<TextView>(Resource.Id.RequestPageTitle);
            _requestDataFrom = FindViewById<TextView>(Resource.Id.RequestDataText);
            _requestDataAmount = FindViewById<TextView>(Resource.Id.RequestAmountSelectorTitleText);
            _firstNumber = FindViewById<TextView>(Resource.Id.FirstRequestNumberText);
            _secondNumber = FindViewById<TextView>(Resource.Id.SecondRequestNumberText);
            _thirdNumber = FindViewById<TextView>(Resource.Id.ThirdRequestNumberText);
            _fourthNumber = FindViewById<TextView>(Resource.Id.FourthRequestNumberText);
            _dataUnitsToGB = FindViewById<TextView>(Resource.Id.DataRequestUnits);
            _decimalPointVisibility = FindViewById<TextView>(Resource.Id.RequestDataUnitDecimalText);
            _requestDialogDisplayText = FindViewById<TextView>(Resource.Id.RequestDialogText);
            _successfullyRequestedMessage = FindViewById<TextView>(Resource.Id.RequestSuccessDialogText);
            _yesToRequest = FindViewById<Button>(Resource.Id.YesRequestButton);
            _doNotRequest = FindViewById<Button>(Resource.Id.NoDoNotRequestButton);
            _OkSuccessfullyRequested = FindViewById<Button>(Resource.Id.OkRequestButton);
            _firstUpArrow = FindViewById<ImageButton>(Resource.Id.FirstRequestUpArrow);
            _secondUpArrow = FindViewById<ImageButton>(Resource.Id.SecondRequestUpArrow);
            _thirdUpArrow = FindViewById<ImageButton>(Resource.Id.ThirdRequestUpArrow);
            _fourthUpArrow = FindViewById<ImageButton>(Resource.Id.FourthRequestUpArrow);
            _firstDownArrow = FindViewById<ImageButton>(Resource.Id.FirstRequestDownArrow);
            _secondDownArrow = FindViewById<ImageButton>(Resource.Id.SecondRequestDownArrow);
            _thirdDownArrow = FindViewById<ImageButton>(Resource.Id.ThirdRequestDownArrow);
            _fourthDownArrow = FindViewById<ImageButton>(Resource.Id.FourthRequestDownArrow);
            _requestConfirmationPopUp = FindViewById<RelativeLayout>(Resource.Id.RequestPagePopUpLayout);
            _requestSuccessMessage = FindViewById<RelativeLayout>(Resource.Id.RequestPageSuccessfulPopUpLayout);
            _requestButtonClicked = FindViewById<Button>(Resource.Id.RequestButton);
            _BackButton = FindViewById<ImageButton>(Resource.Id.RequestBackButton);
			_userSelectionSlidingLayout = FindViewById<LinearLayout>(Resource.Id.UserSelectionSlidingLayout);

		}

        protected void setAllStringConstants()
        {
            _requestPageTitle.Text = StringConstants.Localizable.RequestPageTitle;
            _requestDataFrom.Text = StringConstants.Localizable.RequestFrom;
            _requestDataAmount.Text = StringConstants.Localizable.SelectAmountRequest;
            _firstNumber.Text = StringConstants.Localizable.InitialAmount;
            _secondNumber.Text = StringConstants.Localizable.InitialAmount;
            _thirdNumber.Text = StringConstants.Localizable.InitialAmount;
            _fourthNumber.Text = StringConstants.Localizable.InitialAmount;
            _requestButtonClicked.Text = StringConstants.Localizable.RequestButton;
            _dataUnitsToGB.Text = StringConstants.Localizable.MBUnit;
        }

        protected void SetClickable()
        {
            _yesToRequest.Click += showSuccessMessage;
            _doNotRequest.Click += showConfirmationPopUp;
            _OkSuccessfullyRequested.Click += showSuccessMessage;
            _firstUpArrow.Click += increaseInt;
            _secondUpArrow.Click += increaseInt;
            _thirdUpArrow.Click += increaseInt;
            _fourthUpArrow.Click += increaseInt;
            _firstDownArrow.Click += decreaseInt;
            _secondDownArrow.Click += decreaseInt;
            _thirdDownArrow.Click += decreaseInt;
            _fourthDownArrow.Click += decreaseInt;
            _requestButtonClicked.Click += showConfirmationPopUp;
            _BackButton.Click += delegate { Finish(); };

            foreach (Button user in _userButtons)
            {
                user.Click += (o, s) =>
                {
                    user.Selected = true;
                    _selectedUser = user;
                };
            }
        }


        private void increaseInt(object sender, EventArgs e)
        {
            int num1 = Int32.Parse(_firstNumber.Text);
            int num2 = Int32.Parse(_secondNumber.Text);
            int num3 = Int32.Parse(_thirdNumber.Text);
            int num4 = Int32.Parse(_fourthNumber.Text);

            ImageButton _upArrowClicked = (ImageButton)sender;

            switch (_upArrowClicked.Id)
            {
                case Resource.Id.FirstRequestUpArrow:
                    if (num1 < 9) { num1++; }
                    else if (num1 == 9) { num1 = 0; }
                    _firstNumber.Text = num1.ToString();
                    setGbOrMb(num1);
                    break;

                case Resource.Id.SecondRequestUpArrow:
                    if (num2 < 9) { num2++; }
                    else if (num2 == 9) { num2 = 0; }
                    _secondNumber.Text = num2.ToString();
                    break;

                case Resource.Id.ThirdRequestUpArrow:
                    if (num3 < 9) { num3++; }
                    else if (num3 == 9) { num3 = 0; }
                    _thirdNumber.Text = num3.ToString();
                    break;

                case Resource.Id.FourthRequestUpArrow:
                    if (num4 < 9) { num4++; }
                    else if (num4 == 9) { num4 = 0; }
                    _fourthNumber.Text = num4.ToString();
                    break;
            }
        }


        private void decreaseInt(object sender, EventArgs e)
        {
            int num1 = Int32.Parse(_firstNumber.Text);
            int num2 = Int32.Parse(_secondNumber.Text);
            int num3 = Int32.Parse(_thirdNumber.Text);
            int num4 = Int32.Parse(_fourthNumber.Text);

            ImageButton _downArrowClicked = (ImageButton)sender;

            switch (_downArrowClicked.Id)
            {
                case Resource.Id.FirstRequestDownArrow:
                    num1--;
                    if (num1 < 0) { num1 = 9; }
                    _firstNumber.Text = num1.ToString();
                    setGbOrMb(num1);
                    break;

                case Resource.Id.SecondRequestDownArrow:
                    if (num2 > 0) { num2--; }
                    else if (num2 == 0) { num2 = 9; }
                    _secondNumber.Text = num2.ToString();
                    break;

                case Resource.Id.ThirdRequestDownArrow:
                    if (num3 > 0) { num3--; }
                    else if (num3 == 0) { num3 = 9; }
                    _thirdNumber.Text = num3.ToString();
                    break;

                case Resource.Id.FourthRequestDownArrow:
                    if (num4 > 0) { num4--; }
                    else if (num4 == 0) { num4 = 9; }
                    _fourthNumber.Text = num4.ToString();
                    break;
            }
        }

        private void setGbOrMb(int valueOfFirstNumber)
        {
            if (valueOfFirstNumber > 0)
            {
                _dataUnitsToGB.Text = StringConstants.Localizable.GBUnit;
                _decimalPointVisibility.Text = ".";
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
            _requestDialogDisplayText.Text = "Are you sure you would like to request " + _dataAmountDouble.ToString() + " " + _getDataUnit + " from Steven?";

            if (_requestConfirmationPopUp.Visibility == ViewStates.Invisible)
            {
                _requestConfirmationPopUp.Visibility = ViewStates.Visible;
            }
            else
            {
                _requestConfirmationPopUp.Visibility = ViewStates.Invisible;
            }
        }

        private void showSuccessMessage(object sender, EventArgs e)
        {
            _successfullyRequestedMessage.Text = "OK! " + _dataAmountDouble.ToString() + " " + _getDataUnit + " has successfully been requested from Steven!";
            _firstNumber.Text = "0";
            _secondNumber.Text = "0";
            _thirdNumber.Text = "0";
            _fourthNumber.Text = "0";
            _dataUnitsToGB.Text = "MB";
            _decimalPointVisibility.Text = "";


            if (_requestSuccessMessage.Visibility == ViewStates.Invisible)
            {
                _requestSuccessMessage.Visibility = ViewStates.Visible;
            }

            else
            {
                _requestSuccessMessage.Visibility = ViewStates.Invisible;
                _requestConfirmationPopUp.Visibility = ViewStates.Invisible;
            }
        }



    }
}