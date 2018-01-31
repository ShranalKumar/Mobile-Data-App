using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DocumentDBTodo
{
	public class TodoItem
	{
		[JsonProperty(PropertyName = "id")]
		public string id { get; set; }

		[JsonProperty(PropertyName = "uid")]
		public string uid { get; set; }

		[JsonProperty(PropertyName = "Password")]
		public string password { get; set; }

		[JsonProperty(PropertyName = "Name")]
		public List<NameList> Name { get; set; }

		[JsonProperty(PropertyName = "Plan")]
		public double Plan { get; set; }

		[JsonProperty(PropertyName = "Admin")]
		public bool AdminStatus { get; set; }

		[JsonProperty(PropertyName = "Used")]
		public int Used { get; set; }

		[JsonProperty(PropertyName = "Allocated")]
		public double Allocated { get; set; }

		[JsonProperty(PropertyName = "PlanStartDate")]
		public string PlanStartDate { get; set; }

		[JsonProperty(PropertyName = "PlanEndDate")]
		public string PlanEndDate { get; set; }

		[JsonProperty(PropertyName = "GroupMember")]
		public List<GroupMembers> groupMembers { get; set; }


		[JsonProperty(PropertyName = "UsageBreakdown")]
		public List<UsageBreakdownList> UsageBreakdown { get; set; }

		internal void SetPropertyValue(string v1, string v2)
		{
			throw new NotImplementedException();
		}
	}

	public class NameList
	{
		[JsonProperty(PropertyName = "FirstName")]
		public string FirstName { get; set; }

		[JsonProperty(PropertyName = "LastName")]
		public string LastName { get; set; }

	}

	public class GroupMembers
	{
		[JsonProperty(PropertyName = "uid")]
		public string uid { get; set; }

		[JsonProperty(PropertyName = "Name")]
		public List<NameList> Name { get; set; }

		[JsonProperty(PropertyName = "Admin")]
		public bool adminStatus { get; set; }

		[JsonProperty(PropertyName = "Used")]
		public int Used { get; set; }

		[JsonProperty(PropertyName = "Allocated")]
		public double Allocated { get; set; }

		[JsonProperty(PropertyName = "UsageBreakdown")]
		public List<UsageBreakdownList> UsageBreakdown { get; set; }
	}

	public class UsageBreakdownList
	{
		[JsonProperty(PropertyName = "App")]
		public string App { get; set; }

		[JsonProperty(PropertyName = "AppDataUsed")]
		public string AppUsage { get; set; }

	}

}

