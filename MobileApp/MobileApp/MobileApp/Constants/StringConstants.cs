﻿using System;
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

            //Non-Admin Dashboard Screen
            public const string UsersDataUsage = "{0} Data Usage";
            public const string DataUsageBreakdown = "{0} MB";
            public const string RequestButton = "Request";
            public const string TransferButton = "Transfer";

            //Allocation Screen
            public const string Allocate = "Allocate Data";
            public const string CurrentPlan = "Current Plan";
            public const string RemainingData = "Remaining Data";
            public const string DataAmount = "{0}GB";
            public const string WeeklyMode = "Weekly Mode";
        }
    }
}
