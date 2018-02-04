using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileApp.Constants;
using MobileApp.Droid.Helpers;

namespace MobileApp.Droid.Views
{
    [Activity(Label = "TransferView", ScreenOrientation = ScreenOrientation.Portrait)]
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
        private Button _doNotTransfer;
        private Button _yesToTransfer;
        private Button _OkSuccessfullyTransfered;

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

        private string _getDataAmount;
        private string _getDataUnit;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TransferLayout);

            findAllElements();
            DataBarFill();
            setAllStringConstants();
            CustomSlidingTilesView.CreateSlidingTilesView(_userSelectionSlidingLayout);
            
            _yesToTransfer.Click += showSuccessMessage;            
            _doNotTransfer.Click += showConfirmationPopUp;            
            _OkSuccessfullyTransfered.Click += showSuccessMessage;            
            _firstUpArrow.Click += increaseInt;            
            _secondUpArrow.Click += increaseInt;            
            _thirdUpArrow.Click += increaseInt;            
            _fourthUpArrow.Click += increaseInt;
            _firstDownArrow.Click += decreaseInt;            
            _secondDownArrow.Click += decreaseInt;            
            _thirdDownArrow.Click += decreaseInt;            
            _fourthDownArrow.Click += decreaseInt;            
            _sendButtonClicked.Click += showConfirmationPopUp;           
            _BackButton.Click += delegate { StartActivity(typeof(NonAdminDashBoardView)); };
        }

        public void DataBarFill()
        {
            double _fillNumber = (1 - (double)Controller._users[0].Used / (double)Controller._users[0].Allocated) * 100;
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
            _transferDialogDisplayText = FindViewById<TextView>(Resource.Id.TransferDialogText);
            _successfullyTransferedMessage = FindViewById<TextView>(Resource.Id.TransferSuccessDialogText);
            _yesToTransfer = FindViewById<Button>(Resource.Id.YesTransferButton);
            _doNotTransfer = FindViewById<Button>(Resource.Id.NoDoNotTransferButton);
            _OkSuccessfullyTransfered = FindViewById<Button>(Resource.Id.OkTransferButton);
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
            _transferConfirmationPopUp = FindViewById<RelativeLayout>(Resource.Id.TransferPagePopUpLayout);
            _transferSuccessMessage = FindViewById<RelativeLayout>(Resource.Id.TransferPageSuccessfulPopUpLayout);
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
            _gbRemainingText.Text = string.Format(StringConstants.Localizable.GbRemaining, "1");
            _sendButtonClicked.Text = StringConstants.Localizable.SendButton;
            
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
                case Resource.Id.FirstUpArrow:
                    if (num1 < 9) { num1++; }
                    else if (num1 == 9) { num1 = 0; }
                    _firstNumber.Text = num1.ToString();
                    setGbOrMb(num1);
                    break;

                case Resource.Id.SecondUpArrow:
                    if (num2 < 9) { num2++; }
                    else if (num2 == 9) { num2 = 0; }
                    _secondNumber.Text = num2.ToString();
                    break;

                case Resource.Id.ThirdUpArrow:
                    if (num3 < 9) { num3++; }
                    else if (num3 == 9) { num3 = 0; }
                    _thirdNumber.Text = num3.ToString();
                    break;

                case Resource.Id.FourthUpArrow:
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
                case Resource.Id.FirstDownArrow:
                    num1--;
                    if (num1 < 0) { num1 = 9; }
                    _firstNumber.Text = num1.ToString();
                    setGbOrMb(num1);
                    break;

                case Resource.Id.SecondDownArrow:
                    if (num2 > 0) { num2--; }
                    else if (num2 == 0) { num2 = 9; }
                    _secondNumber.Text = num2.ToString();
                    break;

                case Resource.Id.ThirdDownArrow:
                    if (num3 > 0) { num3--; }
                    else if (num3 == 0) { num3 = 9; }
                    _thirdNumber.Text = num3.ToString();
                    break;

                case Resource.Id.FourthDownArrow:
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
                _dataUnitsToGB.Text = "GB";
                _decimalPointVisibility.Text = ".";
            }
            else
            {
                _dataUnitsToGB.Text = "MB";
                _decimalPointVisibility.Text = "";
            }
        }

        private void showConfirmationPopUp(object sender, EventArgs e)
        {
            _getDataAmount = _firstNumber.Text + _decimalPointVisibility.Text + _secondNumber.Text + _thirdNumber.Text + _fourthNumber.Text;
            _dataAmountDouble = Double.Parse(_getDataAmount);
            _getDataUnit = _dataUnitsToGB.Text;
            _transferDialogDisplayText.Text = "Are you sure you would like to transfer " + _dataAmountDouble.ToString() + " " + _getDataUnit + " to Steven?";

            if (_transferConfirmationPopUp.Visibility == ViewStates.Invisible)
            {
                _transferConfirmationPopUp.Visibility = ViewStates.Visible;
            }
            else
            {
                _transferConfirmationPopUp.Visibility = ViewStates.Invisible;
            }
        }

        private void showSuccessMessage(object sender, EventArgs e)
        {
            _successfullyTransferedMessage.Text = "OK! " + _dataAmountDouble.ToString() + " " + _getDataUnit + " has successfully been transfered to Steven!";
            _firstNumber.Text = "0";
            _secondNumber.Text = "0";
            _thirdNumber.Text = "0";
            _fourthNumber.Text = "0";
            _dataUnitsToGB.Text = "MB";
            _decimalPointVisibility.Text = "";


            if (_transferSuccessMessage.Visibility == ViewStates.Invisible)
            {
                _transferSuccessMessage.Visibility = ViewStates.Visible;
            }

            else
            {
                _transferSuccessMessage.Visibility = ViewStates.Invisible;
                _transferConfirmationPopUp.Visibility = ViewStates.Invisible;
            }
        }



    }
}