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
            public const string UsernameHint = "Username";
            public const string PasswordHint = "Password";
            public const string LogIn = "Log In";
			public const string QRLogIn = "QR Log In";
			public const string LogInFailed = "Username and password do not match. Try again";

			//Admin Dashboard Screen
			public const string DaysRemaining = "{0} Days Remaining";
            public const string GbRemaining = "{0} GB Remaining";
            public const string AllocateData = "Allocate Data";
            public const string BuyData = "Buy Data";

            //Transfer Screen
            public const string Transfer = "Transfer Data";
            public const string SendDataTo = "Send Data To:";
            public const string SelectAmount = "Select Amount to Transfer:";
            public const string DataRemaining = "Data Remaining:";
            public const string SendButton = "Send";
            public const string InitialAmount = "0";
            public const string MBUnit = "MB";
            public const string GBUnit = "GB";

            //Users Data Usage Screen
            public const string SavedChangesMessage = "Your changes have successfully been changed!";

            //Overview Screen
            public const string NewMemberAddedToast = "New group member has been successfully added!";
            public const string DeleteMemberToast = "{0} has been removed from the plan!";

            //Non-Admin Dashboard Screen
            public const string UsersDataUsage = "{0} Data Usage";
            public const string DataUsageBreakdown = "{0} MB";
            public const string RequestButton = "Request";
            public const string TransferButton = "Transfer";

            //Allocation Screen
            public const string AllocatedData = "Allocated Data";
            public const string UsedData = "Used Data";
            public const string UsedText = "Used";
			public const string AllocatedText = "Allocated";
			public const string CurrentPlan = "Current Plan";
            public const string RemainingData = "Data Remaining";
			public const string UnAllocatedData = "Reserved Data";
            public const string DataAmount = "{0} GB";
            public const string WeeklyMode = "Weekly Mode";
            public const string SaveButton = "Save";
            public const string AllocationUpdate = "Successfully updated users data allocation.";
            public const string AllocationError = "Please ensure value for reserved data is a positive number to proceed.";
			public const string PointsText = "My Points";
			public const string PointsAmout = "{0}";
			public const string GraphTitle = "Data usage from last week";
			public const string GraphSubTitle = "(Values in graph are in MB)";
			public const string UserPhoneNumber = "{0}";


			//Request Screen
			public const string RequestPageTitle = "Request Data";
            public const string RequestFrom = "Reqeust Data From:";
            public const string SelectAmountRequest = "Select Amount to Request:";

            //Plan Overview Screen
            public const string OverviewTitle = "Buy Data";
            public const string PlanName = "Data Share";
            public const string PlanDataAmount = "{0} GB / month";
            public const string PlanRemaining = "Data Remaining";
            public const string PlanAllocated = "Data Allocated";
            public const string PlanUsed = "Data Used";
            public const string PlanRemainingAmount = "{0} GB";
            public const string PlanAllocatedAmount = "{0} GB";
            public const string PlanUsedAmount = "{0} GB";
            public const string SelectDataPackHeading = "Select a data Pack";
            public const string AlertBeforeBuying = "Are you sure you would like to add '{0}' pack to your plan?";
            public const string AlertBeforeBuyingTitle = "Buy Data Pack";
            public const string BuyOneGB = "1GB Data Boost";
            public const string BuyTwoGB = "2GB Data Boost";
            public const string BuyOneGBPrice = "$ {0}";
            public const string BuyTwoGBPrice = "$ {0}";
            public const string OutStandingAmountText = "Off Plan Spent";
            public const string OutstandingAmount = "$ {0}";
			public const string ToastAfterBuying = "Succesfully added '{0}' to your plan!";
			public const string AddOnsTitle = "Add-Ons";
			public const string AddOnsAmount = "{0} GB";






			//Add member Screen
			public const string AddMemberTitle = "Add New Member";
            public const string PhoneNumber = "Phone Number";
            public const string FirstName = "First Name";
            public const string LastName = "Last Name";
            public const string AdminStatus = "Give Admin Rights";

        }
    }
}
