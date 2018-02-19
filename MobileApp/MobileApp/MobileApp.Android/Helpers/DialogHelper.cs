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
            transferDataAlert.SetTitle(StringConstants.Localizable.TransferDataDialogTitle);
            transferDataAlert.SetMessage(string.Format(StringConstants.Localizable.TransferPopUpMessage, amount, unit, username));
            transferDataAlert.SetPositiveButton(StringConstants.Localizable.YesDialogButton, (transferSender, transferArgs) => { callback(transferSender, transferArgs); });
            transferDataAlert.SetNegativeButton(StringConstants.Localizable.NoDialogButton, (transferSender, transferArgs) => { });
            Dialog transferDialog = transferDataAlert.Create();
            return transferDialog;
        }

        public static Dialog ZeroAmount(Context context)
        {
            AlertDialog.Builder zeroAmountAlert = new AlertDialog.Builder(context);
            zeroAmountAlert.SetTitle(StringConstants.Localizable.WarningDialogTitle);
            zeroAmountAlert.SetMessage(StringConstants.Localizable.TransferAmountZeroWarning);
            zeroAmountAlert.SetNeutralButton(StringConstants.Localizable.OkDialogButton, (transferSender, transferEventArgs) => { });
            Dialog zerAmountDialog = zeroAmountAlert.Create();
            return zerAmountDialog;
        }

        public static Dialog TransferAmount(Context context)
        {
            AlertDialog.Builder transferAmountAlert = new AlertDialog.Builder(context);
            transferAmountAlert.SetTitle(StringConstants.Localizable.WarningDialogTitle);
            transferAmountAlert.SetMessage(StringConstants.Localizable.TransferAmountWarning);
            transferAmountAlert.SetNeutralButton(StringConstants.Localizable.OkDialogButton, (transferSender, transferEventArgs) => { });
            Dialog transferAmountDialog = transferAmountAlert.Create();
            return transferAmountDialog;
        }

        public static Dialog SelectUser(Context context)
        {
            AlertDialog.Builder noUserSelectedAlert = new AlertDialog.Builder(context);
            noUserSelectedAlert.SetTitle(StringConstants.Localizable.WarningDialogTitle);
            noUserSelectedAlert.SetMessage(StringConstants.Localizable.NoUserSelectedWarning);
            noUserSelectedAlert.SetNeutralButton(StringConstants.Localizable.OkDialogButton, (transferSender, transferEventArgs) => { });
            Dialog selectUserDialog = noUserSelectedAlert.Create();
            return selectUserDialog;
        }

        public static Dialog TransferSuccess(Context context, string amount, string unit, string username)
        {
            AlertDialog.Builder successAlert = new AlertDialog.Builder(context);
            successAlert.SetTitle(StringConstants.Localizable.SuccessDialogTitle);
            successAlert.SetMessage(string.Format(StringConstants.Localizable.TransferConfirmationMessage, amount, unit, username));
            successAlert.SetNeutralButton(StringConstants.Localizable.OkDialogButton, (successSender, successEventArgs) => { });
            Dialog successDialog = successAlert.Create();
            return successDialog;
        }
    }
}