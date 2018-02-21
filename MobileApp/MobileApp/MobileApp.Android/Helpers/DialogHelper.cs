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
using MobileApp.Constants;

namespace MobileApp.Droid.Helpers
{
    public class DialogHelper
    {
        public static Dialog ConfirmTransfer(Context context, string amount, string unit, string username, Action<object, EventArgs>callback)
        {
            AlertDialog.Builder transferDataAlert = new AlertDialog.Builder(context);
            transferDataAlert.SetTitle(StringConstants.Localizable.Transfer);
            transferDataAlert.SetMessage(string.Format(StringConstants.Localizable.TransferPopUpMessage, amount, unit, username));
            transferDataAlert.SetPositiveButton(StringConstants.Localizable.YesDialogButton, (sender, e) => { callback(sender, e); });
            transferDataAlert.SetNegativeButton(StringConstants.Localizable.NoDialogButton, (sender, e) => { });
            Dialog transferDialog = transferDataAlert.Create();
            return transferDialog;
        }

        public static Dialog ConfirmRequest(Context context, string amount, string unit, string username, Action<object, EventArgs> callback)
        {
            AlertDialog.Builder requestDataAlert = new AlertDialog.Builder(context);
            requestDataAlert.SetTitle(StringConstants.Localizable.Transfer);
            requestDataAlert.SetMessage(string.Format(StringConstants.Localizable.RequestPopUpMessage, amount, unit, username));
            requestDataAlert.SetPositiveButton(StringConstants.Localizable.YesDialogButton, (sender, e) => { callback(sender, e); });
            requestDataAlert.SetNegativeButton(StringConstants.Localizable.NoDialogButton, (sender, e) => { });
            Dialog requestDialog = requestDataAlert.Create();
            return requestDialog;
        }

        public static Dialog ZeroAmount(Context context)
        {
            AlertDialog.Builder zeroAmountAlert = new AlertDialog.Builder(context);
            zeroAmountAlert.SetTitle(StringConstants.Localizable.WarningDialogTitle);
            zeroAmountAlert.SetMessage(StringConstants.Localizable.TransferAmountZeroWarning);
            zeroAmountAlert.SetNeutralButton(StringConstants.Localizable.OkDialogButton, (sender, e) => { });
            Dialog zerAmountDialog = zeroAmountAlert.Create();
            return zerAmountDialog;
        }

        public static Dialog TransferAmount(Context context)
        {
            AlertDialog.Builder transferAmountAlert = new AlertDialog.Builder(context);
            transferAmountAlert.SetTitle(StringConstants.Localizable.WarningDialogTitle);
            transferAmountAlert.SetMessage(StringConstants.Localizable.TransferAmountWarning);
            transferAmountAlert.SetNeutralButton(StringConstants.Localizable.OkDialogButton, (sender, e) => { });
            Dialog transferAmountDialog = transferAmountAlert.Create();
            return transferAmountDialog;
        }

        public static Dialog SelectUser(Context context)
        {
            AlertDialog.Builder noUserSelectedAlert = new AlertDialog.Builder(context);
            noUserSelectedAlert.SetTitle(StringConstants.Localizable.WarningDialogTitle);
            noUserSelectedAlert.SetMessage(StringConstants.Localizable.NoUserSelectedWarning);
            noUserSelectedAlert.SetNeutralButton(StringConstants.Localizable.OkDialogButton, (sender, e) => { });
            Dialog selectUserDialog = noUserSelectedAlert.Create();
            return selectUserDialog;
        }

        public static Dialog TransferSuccess(Context context, string amount, string unit, string username)
        {
            AlertDialog.Builder successAlert = new AlertDialog.Builder(context);
            successAlert.SetTitle(StringConstants.Localizable.SuccessDialogTitle);
            successAlert.SetMessage(string.Format(StringConstants.Localizable.TransferConfirmationMessage, amount, unit, username));
            successAlert.SetNeutralButton(StringConstants.Localizable.OkDialogButton, (sender, e) => { });
            Dialog successDialog = successAlert.Create();
            return successDialog;
        }

        public static Dialog RequestSuccess(Context context, string amount, string unit, string username)
        {
            AlertDialog.Builder successAlert = new AlertDialog.Builder(context);
            successAlert.SetTitle(StringConstants.Localizable.SuccessDialogTitle);
            successAlert.SetMessage(string.Format(StringConstants.Localizable.RequestConfirmationMessage, amount, unit, username));
            successAlert.SetNeutralButton(StringConstants.Localizable.OkDialogButton, (sender, e) => { });
            Dialog successDialog = successAlert.Create();
            return successDialog;
        }

        public static Dialog BuyAddOn(Context context, Action<object, EventArgs, double, double, string> callback, double addOnAmount, double amount, string constant)
        {
            AlertDialog.Builder buyAddOnAlert = new AlertDialog.Builder(context);
            buyAddOnAlert.SetTitle(StringConstants.Localizable.AlertBeforeBuyingTitle);
            buyAddOnAlert.SetMessage(String.Format(StringConstants.Localizable.AlertBeforeBuying, constant));
            buyAddOnAlert.SetPositiveButton(StringConstants.Localizable.YesDialogButton, (sender, e) => { callback(sender, e, addOnAmount, amount, constant); });
            buyAddOnAlert.SetNegativeButton(StringConstants.Localizable.NoDialogButton, (sender, e) => { });
            Dialog buyAddOnDialog = buyAddOnAlert.Create();
            return buyAddOnDialog;
        }
    }
}