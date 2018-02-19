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
using MobileApp.Droid.Helpers;
using MobileApp.Constants;
using Android.Graphics;

namespace MobileApp.Droid.Views
{
    [Activity(Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PlanOverviewView : Activity
    {

        private TextView _overviewPageTitle;
        private TextView _dataPlanNameText;
        private TextView _dataPlanAmount;
        private TextView _planRemainingDataText;
        private TextView _planRemainingDataAmount;
        private TextView _selectDataPackHeading;
        private TextView _buyOneGBText;
        private TextView _buyTwoGBText;
        private TextView _outstandingAmountText;
        private TextView _outstandingAmount;
        private TextView _addonsTitle;
        private TextView _addonsAmount;
        private Button _buyOneGBPrice;
        private Button _buyTwoGBPrice;
        private ImageButton _overviewPageBackButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PlanOverviewLayout);


            findAllElements();
            setAllStringConstants();
            _overviewPageBackButton.Click += delegate { Finish(); };

            _buyOneGBPrice.Click += delegate { BuyOneGBClicked(); };
            _buyTwoGBPrice.Click += delegate { BuyTwoGBClicked(); };


            settingPriceTextColor();
        }

		private async void _buyTwoGBClick(double outStanding)
		{
			Controller._outstandingPriceValue += outStanding;
			User changedUser = await Controller.BuyAddOns(Controller._userLoggedIn, 2);
			Controller._addOns = changedUser.AddOns;
			Controller.SetGlobalValues();			
			Controller._userLoggedIn = changedUser;
			Controller._users[Controller._users.IndexOf(Controller._userLoggedIn)] = changedUser;
			_outstandingAmount.Text = string.Format(StringConstants.Localizable.BuyDataAmountInDollars, Controller._outstandingPriceValue.ToString());
			_addonsAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Controller._addOns);
			_planRemainingDataAmount.Text = string.Format(StringConstants.Localizable.DataAmount, Controller._totalRemainder);
			settingPriceTextColor();
			Toast.MakeText(this, string.Format(StringConstants.Localizable.ToastAfterBuying, StringConstants.Localizable.BuyTwoGB), ToastLength.Short).Show();
		}

		private async void _buyOneGBClick(double outStanding)
		{
			Controller._outstandingPriceValue += outStanding;
			User changedUser = await Controller.BuyAddOns(Controller._userLoggedIn, 1);
			Controller._addOns = changedUser.AddOns;
			Controller.SetGlobalValues();			
			Controller._userLoggedIn = changedUser;
			Controller._users[Controller._users.IndexOf(Controller._userLoggedIn)] = changedUser;
			_outstandingAmount.Text = string.Format(StringConstants.Localizable.BuyDataAmountInDollars, Controller._outstandingPriceValue.ToString());
			_planRemainingDataAmount.Text = string.Format(StringConstants.Localizable.DataAmount, Controller._totalRemainder);
			_addonsAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Controller._addOns);
			settingPriceTextColor();
			Toast.MakeText(this, string.Format(StringConstants.Localizable.ToastAfterBuying, StringConstants.Localizable.BuyOneGB), ToastLength.Short).Show();
		}

		private void findAllElements()
        {
            _overviewPageTitle = FindViewById<TextView>(Resource.Id.OverviewPageTitle);
            _dataPlanNameText = FindViewById<TextView>(Resource.Id.PlanNameTitle);
            _dataPlanAmount = FindViewById<TextView>(Resource.Id.PlaneNameAmount);
            _planRemainingDataText = FindViewById<TextView>(Resource.Id.DataRemainingTitle);
            _planRemainingDataAmount = FindViewById<TextView>(Resource.Id.DataRemainingAmount);
            _overviewPageBackButton = FindViewById<ImageButton>(Resource.Id.OverviewPageBackButton);
            _selectDataPackHeading = FindViewById<TextView>(Resource.Id.SelectDataPackHeading);
            _buyOneGBText = FindViewById<TextView>(Resource.Id.OneGBAddOnTextView);
            _buyTwoGBText = FindViewById<TextView>(Resource.Id.TwoGBAddOnTextView);
            _buyOneGBPrice = FindViewById<Button>(Resource.Id.BuyOneGBAddOnButton);
            _buyTwoGBPrice = FindViewById<Button>(Resource.Id.BuyTwoGBAddOnButton);
            _outstandingAmountText = FindViewById<TextView>(Resource.Id.OutStandingAmountText);
            _outstandingAmount = FindViewById<TextView>(Resource.Id.OutstandingAmount);
			_addonsTitle = FindViewById<TextView>(Resource.Id.AddOnsTitle);
			_addonsAmount = FindViewById<TextView>(Resource.Id.AddOnsAmount);
		}

		private void setAllStringConstants()
        {
            _overviewPageTitle.Text = StringConstants.Localizable.OverviewTitle;
            _dataPlanNameText.Text = StringConstants.Localizable.PlanName;
            _dataPlanAmount.Text = string.Format(StringConstants.Localizable.PlanDataAmount, Controller._planDataPool);
            _planRemainingDataText.Text = StringConstants.Localizable.PlanRemaining;
            _planRemainingDataAmount.Text = string.Format(StringConstants.Localizable.DataAmount, Controller._totalRemainder);
            _selectDataPackHeading.Text = StringConstants.Localizable.SelectDataPackHeading;
            _buyOneGBText.Text = StringConstants.Localizable.BuyOneGB;
            _buyTwoGBText.Text = StringConstants.Localizable.BuyTwoGB;
            _buyOneGBPrice.Text = string.Format(StringConstants.Localizable.BuyDataAmountInDollars, "14.99");
            _buyTwoGBPrice.Text = string.Format(StringConstants.Localizable.BuyDataAmountInDollars, "19.99");
            _outstandingAmountText.Text = StringConstants.Localizable.OutStandingAmountText;
            _outstandingAmount.Text = string.Format(StringConstants.Localizable.BuyDataAmountInDollars, Controller._outstandingPriceValue.ToString());
			_addonsTitle.Text = StringConstants.Localizable.AddOnsTitle;
			_addonsAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Controller._addOns);
        }

        private void settingPriceTextColor()
        {
            if (Controller._outstandingPriceValue <= 0)
            {
                _outstandingAmount.SetTextColor(new Color(77, 155, 0));
            }

            else if (Controller._outstandingPriceValue > 0)
            {
                _outstandingAmount.SetTextColor(new Color(232, 151, 0));
            }
        }

        private void BuyOneGBClicked()
        {
            AlertDialog.Builder memberDeleteAlert = new AlertDialog.Builder(this);
            memberDeleteAlert.SetTitle(StringConstants.Localizable.AlertBeforeBuyingTitle);
            memberDeleteAlert.SetMessage(String.Format(StringConstants.Localizable.AlertBeforeBuying, StringConstants.Localizable.BuyOneGB));
            memberDeleteAlert.SetPositiveButton(StringConstants.Localizable.YesDialogButton, (deleteSender, deleteEventArgs) => { _buyOneGBClick(14.99); });
            memberDeleteAlert.SetNegativeButton(StringConstants.Localizable.NoDialogButton, (deleteSender, deleteEventArgs) => { });
            Dialog deleteDialog = memberDeleteAlert.Create();
            deleteDialog.Show();
        }

        private void BuyTwoGBClicked()
        {
            AlertDialog.Builder memberDeleteAlert = new AlertDialog.Builder(this);
            memberDeleteAlert.SetTitle(StringConstants.Localizable.AlertBeforeBuyingTitle);
            memberDeleteAlert.SetMessage(String.Format(StringConstants.Localizable.AlertBeforeBuying, StringConstants.Localizable.BuyTwoGB));
			memberDeleteAlert.SetPositiveButton(StringConstants.Localizable.YesDialogButton, (deleteSender, deleteEventArgs) => { _buyTwoGBClick(19.99); });
            memberDeleteAlert.SetNegativeButton(StringConstants.Localizable.NoDialogButton, (deleteSender, deleteEventArgs) => { });
            Dialog deleteDialog = memberDeleteAlert.Create();
            deleteDialog.Show();
        }

        protected override void OnRestart()
        {
            base.OnRestart();
        }
    }
}