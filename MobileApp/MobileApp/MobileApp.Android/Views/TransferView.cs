using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileApp.Droid.Views
{
    [Activity(Label = "TransferView", MainLauncher = true)]
    public class TransferView : Activity
    {

        private TextView _firstNumber;
        private TextView _secondNumber;
        private TextView _thirdNumber;
        private TextView _fourthNumber;
        private TextView _dataUnitsToGB;
        private TextView _decimalPointVisibility;

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
        private ImageButton _sendButtonClicked;

        private RelativeLayout _transferConfirmationPopUp;
        private RelativeLayout _transferSuccessMessage;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TransferLayout);

            _firstNumber = FindViewById<TextView>(Resource.Id.FirstNumberText);
            _secondNumber = FindViewById<TextView>(Resource.Id.SecondNumberText);
            _thirdNumber = FindViewById<TextView>(Resource.Id.ThirdNumberText);
            _fourthNumber = FindViewById<TextView>(Resource.Id.FourthNumberText);
            _dataUnitsToGB = FindViewById<TextView>(Resource.Id.DataTransferUnits);
            _decimalPointVisibility = FindViewById<TextView>(Resource.Id.DataUnitDecimalText);

            _yesToTransfer = FindViewById<Button>(Resource.Id.YesTransferButton);
            _yesToTransfer.Click += showSuccessMessage;

            _doNotTransfer = FindViewById<Button>(Resource.Id.NoDoNotTransferButton);
            _doNotTransfer.Click += showConfirmationPopUp;

            _OkSuccessfullyTransfered = FindViewById<Button>(Resource.Id.OkTransferButton);
            _OkSuccessfullyTransfered.Click += showSuccessMessage;

            _firstUpArrow = FindViewById<ImageButton>(Resource.Id.FirstUpArrow);
            _firstUpArrow.Click += increaseInt;

            _secondUpArrow = FindViewById<ImageButton>(Resource.Id.SecondUpArrow);
            _secondUpArrow.Click += increaseInt;

            _thirdUpArrow = FindViewById<ImageButton>(Resource.Id.ThirdUpArrow);
            _thirdUpArrow.Click += increaseInt;

            _fourthUpArrow = FindViewById<ImageButton>(Resource.Id.FourthUpArrow);
            _fourthUpArrow.Click += increaseInt;



            _firstDownArrow = FindViewById<ImageButton>(Resource.Id.FirstDownArrow);
            _firstDownArrow.Click += decreaseInt;

            _secondDownArrow = FindViewById<ImageButton>(Resource.Id.SecondDownArrow);
            _secondDownArrow.Click += decreaseInt;

            _thirdDownArrow = FindViewById<ImageButton>(Resource.Id.ThirdDownArrow);
            _thirdDownArrow.Click += decreaseInt;

            _fourthDownArrow = FindViewById<ImageButton>(Resource.Id.FourthDownArrow);
            _fourthDownArrow.Click += decreaseInt;

            _transferConfirmationPopUp = FindViewById<RelativeLayout>(Resource.Id.TransferPagePopUpLayout);
            _transferSuccessMessage = FindViewById<RelativeLayout>(Resource.Id.TransferPageSuccessfulPopUpLayout);

            _sendButtonClicked = FindViewById<ImageButton>(Resource.Id.SendButton);
            _sendButtonClicked.Click += showConfirmationPopUp;
        }


        private void increaseInt(object sender, EventArgs e)
        {
            int num1 = Int32.Parse(_firstNumber.Text);
            int num2 = Int32.Parse(_secondNumber.Text);
            int num3 = Int32.Parse(_thirdNumber.Text);
            int num4 = Int32.Parse(_fourthNumber.Text);

            ImageButton _upArrowClicked = (ImageButton) sender;

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
                _decimalPointVisibility.Text = " . ";
            } else
            {
                _dataUnitsToGB.Text = "MB";
                _decimalPointVisibility.Text = "";
            }
        }

        private void showConfirmationPopUp(object sender, EventArgs e)
        {
            if (_transferConfirmationPopUp.Visibility == ViewStates.Invisible) {
                _transferConfirmationPopUp.Visibility = ViewStates.Visible;
            }
            else
            {
                _transferConfirmationPopUp.Visibility = ViewStates.Invisible;
            }
        }

        private void showSuccessMessage(object sender, EventArgs e)
        {
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