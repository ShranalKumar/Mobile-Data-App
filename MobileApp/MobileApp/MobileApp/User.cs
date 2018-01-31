﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp
{
    public class User
    {
        public string UID { get; set; }
        public UserName Name { get; set; }
        public double Plan { get; set; }
        public bool AdminStatus { get; set; }
        public int Used { get; set; }
        public double Allocated { get; set; }
        public string PlanStartDate { get; set; }
        public string PlanEndDate { get; set; }
        public List<UserUsageBreakdown> UsageBreakdown { get; set; }
        public List<Member> GroupMembers { get; set; }
    }

    public class UserName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserUsageBreakdown
    {
        public string AppName { get; set; }
        public string AppDataUsed { get; set; }
    }

    public class Member
    {
        public string UID { get; set; }
        public UserName Name { get; set; }
        public bool AdminStatus { get; set; }
        public int  Used { get; set; }
        public double Allocated { get; set; }
        public List<UserUsageBreakdown> UsageBreakdown { get; set; }
    }
}
