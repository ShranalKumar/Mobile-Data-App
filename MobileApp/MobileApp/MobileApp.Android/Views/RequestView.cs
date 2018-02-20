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
using MobileApp.Droid.Helpers;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
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

        private double _dataAmountDouble;

        private Button _requestButtonClicked;

        private ImageButton _firstUpArrow;
        private ImageButton _secondUpArrow;
        private ImageButton _thirdUpArrow;
        private ImageButton _fourthUpArrow;
        private ImageButton _firstDownArrow;
        private ImageButton _secondDownArrow;
        private ImageButton _thirdDownArrow;
        private ImageButton _fourthDownArrow;
        private ImageButton _BackButton;
        
		private LinearLayout _userSelectionSlidingLayout;
        private List<Button> _userButtons;
        private Button _selectedUser;

        private DataAmountSelectorHelper _selectorHelper;
		private string _getDataAmount;
        private string _getDataUnit;
        private double _requestAmount;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RequestLayout);

            findAllElements();
            setAllStringConstants();
			CustomSlidingTilesView.CreateSlidingTilesView(_userSelectionSlidingLayout);
            _userButtons = CustomSlidingTilesView._userButtons;
            _selectorHelper = new DataAmountSelectorHelper();

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
            _firstUpArrow = FindViewById<ImageButton>(Resource.Id.FirstRequestUpArrow);
            _secondUpArrow = FindViewById<ImageButton>(Resource.Id.SecondRequestUpArrow);
            _thirdUpArrow = FindViewById<ImageButton>(Resource.Id.ThirdRequestUpArrow);
            _fourthUpArrow = FindViewById<ImageButton>(Resource.Id.FourthRequestUpArrow);
            _firstDownArrow = FindViewById<ImageButton>(Resource.Id.FirstRequestDownArrow);
            _secondDownArrow = FindViewById<ImageButton>(Resource.Id.SecondRequestDownArrow);
            _thirdDownArrow = FindViewById<ImageButton>(Resource.Id.ThirdRequestDownArrow);
            _fourthDownArrow = FindViewById<ImageButton>(Resource.Id.FourthRequestDownArrow);
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
            _firstUpArrow.Click += ArrowClicked;
            _secondUpArrow.Click += ArrowClicked;
            _thirdUpArrow.Click += ArrowClicked;
            _fourthUpArrow.Click += ArrowClicked;
            _firstDownArrow.Click += ArrowClicked;
            _secondDownArrow.Click += ArrowClicked;
            _thirdDownArrow.Click += ArrowClicked;
            _fourthDownArrow.Click += ArrowClicked;
            _requestButtonClicked.Click += showConfirmationPopUp;
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
                    Console.WriteLine(_selectedUser.Text);
                };
            }
        }

        private void ArrowClicked(object sender, EventArgs e)
        {
            ImageButton _arrowClicked = (ImageButton)sender;

            switch (_arrowClicked.Id)
            {
                case Resource.Id.FirstRequestUpArrow:
                    _selectorHelper.IncreaseSelector(_arrowClicked.Id, _firstNumber);
                    setGbOrMb(Int32.Parse(_firstNumber.Text));
                    break;

                case Resource.Id.SecondRequestUpArrow:
                    _selectorHelper.IncreaseSelector(_arrowClicked.Id, _secondNumber);
                    break;

                case Resource.Id.ThirdRequestUpArrow:
                    _selectorHelper.IncreaseSelector(_arrowClicked.Id, _thirdNumber);
                    break;

                case Resource.Id.FourthRequestUpArrow:
                    _selectorHelper.IncreaseSelector(_arrowClicked.Id, _fourthNumber);
                    break;

                case Resource.Id.FirstRequestDownArrow:
                    _selectorHelper.DecreaseSelector(_arrowClicked.Id, _firstNumber);
                    setGbOrMb(Int32.Parse(_firstNumber.Text));
                    break;

                case Resource.Id.SecondRequestDownArrow:
                    _selectorHelper.DecreaseSelector(_arrowClicked.Id, _secondNumber);
                    break;

                case Resource.Id.ThirdRequestDownArrow:
                    _selectorHelper.DecreaseSelector(_arrowClicked.Id, _thirdNumber);
                    break;

                case Resource.Id.FourthRequestDownArrow:
                    _selectorHelper.DecreaseSelector(_arrowClicked.Id, _fourthNumber);
                    break;
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
                _requestAmount = _dataAmountDouble;
                if (_getDataUnit == StringConstants.Localizable.MBUnit)
                {
                    _requestAmount = _dataAmountDouble / 1000.0;
                }

                if (_requestAmount != 0)
                {
                    Dialog requestDialog = DialogHelper.ConfirmRequest(this, _dataAmountDouble.ToString(), _getDataUnit, _selectedUser.Text, showSuccessMessage);
                    requestDialog.Show();
                }
                else
                {
                    Dialog zerAmountDialog = DialogHelper.ZeroAmount(this);
                    zerAmountDialog.Show();
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
            Dialog successDialog = DialogHelper.RequestSuccess(this, _dataAmountDouble.ToString(), _getDataUnit, _selectedUser.Text);
            successDialog.Show();

            _firstNumber.Text = StringConstants.Localizable.InitialAmount;
            _secondNumber.Text = StringConstants.Localizable.InitialAmount;
            _thirdNumber.Text = StringConstants.Localizable.InitialAmount;
            _fourthNumber.Text = StringConstants.Localizable.InitialAmount;
            _dataUnitsToGB.Text = StringConstants.Localizable.MBUnit;
            _decimalPointVisibility.Text = "";
        }
    }
}