using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Constants
{
    public static class StringConstants
    {
        public struct Localizable
        {
            //Login Screen
            public const string UsernameHint = "Phone Number";
            public const string PasswordHint = "Password";
            public const string LogIn = "Log In";
			public const string QRLogIn = "QR Log In";
			public const string LogInFailed = "Username and password do not match. Try again";

			//Admin Dashboard Screen
			public const string DaysRemaining = "{0} Days Remaining";
            public const string GbRemaining = "{0}GB Remaining";
            public const string AllocateData = "Allocate Data";
            public const string BuyData = "Buy Data";

            //Users Data Usage Screen
            public const string AllocatedText = "Allocated";
            public const string UsedText = "Used";
            public const string UserDataUsageTitle = "{0}'s Data Usage";
            public const string PointsText = "My Points";
            public const string GraphTitle = "Data usage from last week";
            public const string GraphSubTitle = "(Values in graph are in MB)";
            public const string UserDataPageNumber = "{0}";
            public const string DeleteMemberToast = "{0} has been removed from the plan!";
            public const string QRTitleText = "Please scan QR code below to authenticate {0}";

            //Allocate Data Screen
            public const string AllocatedData = "Allocated Data";
            public const string UsedData = "Used Data";
            public const string CurrentPlan = "Current Plan";
            public const string RemainingData = "Data Remaining";
            public const string UnAllocatedData = "Reserved Data";
            public const string DataAmount = "{0}GB";
            public const string SaveButton = "Save";
            public const string AllocationUpdate = "Successfully updated users data allocation.";
            public const string AllocationError = "Please ensure value for reserved data is a positive number to proceed.";

            //Buy Data Screen
            public const string OverviewTitle = "Buy Data";
            public const string PlanName = "Data Share";
            public const string PlanDataAmount = "{0}GB / month";
            public const string PlanRemaining = "Data Remaining";
            public const string AddOnsTitle = "Add-Ons";
            public const string SelectDataPackHeading = "Select a data Pack";
            public const string AlertBeforeBuying = "Are you sure you would like to add '{0}' pack to your plan?";
            public const string AlertBeforeBuyingTitle = "Buy Data Pack";
            public const string BuyOneGB = "1GB Data Boost";
            public const string BuyTwoGB = "2GB Data Boost";
            public const string BuyDataAmountInDollars = "${0}";
            public const string OutStandingAmountText = "Off Plan Spent";
            public const string ToastAfterBuying = "Succesfully added '{0}' to your plan!";

            //Non-Admin Dashboard Screen
            public const string RequestButton = "Request";
            public const string TransferButton = "Transfer";

            //Request Screen
            public const string RequestPageTitle = "Request Data";
            public const string RequestFrom = "Reqeust Data From:";
            public const string SelectAmountRequest = "Select Amount to Request:";
            public const string RequestPopUpMessage = "Are you sure you would like to request {0}{1} from {2}?";
            public const string RequestConfirmationMessage = "OK! {0}{1} has been successfully requested from {2}.";            

            //Transfer Screen
            public const string Transfer = "Transfer Data";
            public const string SendDataTo = "Send Data To:";
            public const string SelectAmount = "Select Amount to Transfer:";
            public const string DataRemaining = "Data Remaining:";
            public const string SendButton = "Send";
            public const string TransferPopUpMessage = "Are you sure you would like to transfer {0}{1} to {2}";
            public const string TransferConfirmationMessage = "OK! {0}{1} has been successfully transfered  to {2}";
			public const string TransferAmountWarning = "Transfer amount is greater than your remaining data!\nPlease change amount to transfer.";
			public const string TransferAmountZeroWarning = "Transfer amount must be greater than 0!\nPlease change amount to transfer.";
			public const string NoUserSelectedWarning = "Please select a user to transfer data to.";
            
            //Add member Screen
			public const string AddMemberTitle = "Add New Member";
            public const string PhoneNumber = "Phone Number";
            public const string FirstName = "First Name";
            public const string LastName = "Last Name";
            public const string AdminStatus = "Give Admin Rights";

			//General
			public const string ReadQuery = "select * from t where t.uid = '{0}'";
			public const string WarningDialogTitle = "WARNING!";
			public const string SuccessDialogTitle = "Success!";
			public const string YesDialogButton = "Yes";
			public const string NoDialogButton = "No";
			public const string OkDialogButton = "OK!";
            public const string DecimalPoint = ".";
            public const string InitialAmount = "0";
            public const string MBUnit = "MB";
            public const string GBUnit = "GB";
        }
    }
}
