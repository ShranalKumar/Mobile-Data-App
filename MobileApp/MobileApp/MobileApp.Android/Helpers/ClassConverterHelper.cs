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
using DocumentDBTodo;

namespace MobileApp.Droid.Helpers
{
    public class ClassConverterHelper
    {
        public static GroupMembers createGroupMember(Member member)
        {
            GroupMembers newGroupMember = new GroupMembers();
            newGroupMember.uid = member.UID;
            newGroupMember.Name = new List<NameList>();
            newGroupMember.Name.Add(new NameList
            {
                FirstName = member.Name.FirstName,
                LastName = member.Name.LastName
            });
            newGroupMember.AdminStatus = member.AdminStatus;
            newGroupMember.Used = member.Used;
            newGroupMember.Allocated = member.Allocated;
            newGroupMember.UsageBreakdown = new List<UsageBreakdownList>();
            member.UsageBreakdown.ForEach(x => newGroupMember.UsageBreakdown.Add(new UsageBreakdownList
            {
                App = x.AppName,
                AppUsage = x.AppDataUsed
            }));

            return newGroupMember;
        }

        public static GroupMembers createGroupMember(User user)
        {
            GroupMembers newGroupMember = new GroupMembers();
            newGroupMember.uid = user.UID;
            newGroupMember.Name = new List<NameList>();
            newGroupMember.Name.Add(new NameList
            {
                FirstName = user.Name.FirstName,
                LastName = user.Name.LastName
            });
            newGroupMember.AdminStatus = user.AdminStatus;
            newGroupMember.Used = user.Used;
            newGroupMember.Allocated = user.Allocated;
            newGroupMember.UsageBreakdown = new List<UsageBreakdownList>();
            user.UsageBreakdown.ForEach(x => newGroupMember.UsageBreakdown.Add(new UsageBreakdownList
            {
                App = x.AppName,
                AppUsage = x.AppDataUsed
            }));

            return newGroupMember;
        }

        public static Member createMember(GroupMembers groupMembers)
        {
            Member member = new Member();
            member.UID = groupMembers.uid;
            member.Name = new UserName();
            member.Name.FirstName = groupMembers.Name[0].FirstName;
            member.Name.LastName = groupMembers.Name[0].LastName;
            member.AdminStatus = groupMembers.AdminStatus;
            member.Used = groupMembers.Used;
            member.Allocated = groupMembers.Allocated;
            member.UsageBreakdown = new List<UserUsageBreakdown>();
            groupMembers.UsageBreakdown.ForEach(x => member.UsageBreakdown.Add(new UserUsageBreakdown
            {
                AppName = x.App,
                AppDataUsed = x.AppUsage
            }));

            return member;
        }

        public static Member createMember(User user)
        {
            Member member = new Member();
            member.UID = user.UID;
            member.Name.FirstName = user.Name.FirstName;
            member.Name.LastName = user.Name.LastName;
            member.AdminStatus = user.AdminStatus;
            member.Used = user.Used;
            member.Allocated = user.Allocated;
            member.UsageBreakdown = new List<UserUsageBreakdown>();
            user.UsageBreakdown.ForEach(x => member.UsageBreakdown.Add(new UserUsageBreakdown
            {
                AppName = x.AppName,
                AppDataUsed = x.AppDataUsed
            }));

            return member;
        }

        public static User createUser(Member member)
        {
            User user = new User();
            user.UID = member.UID;
            user.Name = new UserName();
            user.Name.FirstName = member.Name.FirstName;
            user.Name.LastName = member.Name.LastName;
            user.Plan = Controller._userLoggedIn.Plan;
            user.AdminStatus = member.AdminStatus;
            user.Used = member.Used;
            user.Allocated = member.Allocated;
            user.PlanStartDate = Controller._userLoggedIn.PlanStartDate;
            user.PlanEndDate = Controller._userLoggedIn.PlanEndDate;
            user.GroupMembers = new List<Member>();
            user.UsageBreakdown = new List<UserUsageBreakdown>();
            member.UsageBreakdown.ForEach(x => user.UsageBreakdown.Add(new UserUsageBreakdown
            {
                AppName = x.AppName,
                AppDataUsed = x.AppDataUsed
            }));

            return user;
        }
    }
}