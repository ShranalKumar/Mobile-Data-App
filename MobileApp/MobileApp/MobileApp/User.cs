﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp
{
    public class User
    {
        public string UID { get; set; }
        public UserName Name { get; set; }
        public string Plan { get; set; }
        public string AdminStatus { get; set; }
        public string Used { get; set; }
        public int Allocated { get; set; }
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
        public string AdminStatus { get; set; }
        public string  Used { get; set; }
        public string Allocated { get; set; }
        public List<UserUsageBreakdown> UsageBreakdown { get; set; }
    }
}