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
            public const string LogIn = "LogIn";

            //Admin Dashboard Screen
            public const string DaysRemaining = "{0} Days Remaining";
            public const string GbRemaining = "{0}GB Remaining";
            public const string AllocateData = "Allocate Data";

            //Transfer Screen
            public const string Transfer = "Transfer Data";
            public const string SendDataTo = "Send Data To:";
            public const string SelectAmount = "Select Amount to Transfer:";
            public const string DataRemaining = "Data Remaining:";
            public const string SendButton = "Send";
            public const string InitialAmount = "0";
            public const string MBUnit = "MB";
            public const string GBUnit = "GB";


            //Non-Admin Dashboard Screen
            public const string UsersDataUsage = "{0} Data Usage";
            public const string DataUsageBreakdown = "{0} MB";
            public const string RequestButton = "Request";
            public const string TransferButton = "Transfer";

            //Allocation Screen
            public const string AllocatedData = "Allocated Data";
            public const string UsedData = "Used Data";
            public const string CurrentPlan = "Current Plan";
            public const string RemainingData = "Remaining Data";
            public const string DataAmount = "{0}GB";
            public const string WeeklyMode = "Weekly Mode";
            public const string SaveButton = "Save";

            //Request Screen
            public const string RequestPageTitle = "Request Data";
            public const string RequestFrom = "Reqeust Data From:";
            public const string SelectAmountRequest = "Select Amount to Request:";
        }
    }
}
