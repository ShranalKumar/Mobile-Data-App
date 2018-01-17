﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DocumentDBTodo
{
	public class TodoItem
	{
		[JsonProperty(PropertyName = "id")]
		public string id { get; set; }

		[JsonProperty(PropertyName = "uID")]
		public string uid { get; set; }

		[JsonProperty(PropertyName = "Name")]
		public List<NameList> Name { get; set; }

		[JsonProperty(PropertyName = "plan")]
		public string Plan { get; set; }

		[JsonProperty(PropertyName = "Admin")]
		public string AdminStatus { get; set; }

		[JsonProperty(PropertyName = "Used")]
		public int Used { get; set; }

		[JsonProperty(PropertyName = "Allocated")]
		public int Allocated { get; set; }

		[JsonProperty(PropertyName = "PlanStartDate")]
		public string PlanStartDate { get; set; }

		[JsonProperty(PropertyName = "PlanEndDate")]
		public string PlanEndDate { get; set; }

		[JsonProperty(PropertyName = "GroupMember")]
		public List<GroupMembers> groupMembers { get; set; }
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
		[JsonProperty(PropertyName = "uID")]
		public string uid { get; set; }

		[JsonProperty(PropertyName = "Name")]
		public List<NameList> Name { get; set; }

		[JsonProperty(PropertyName = "Admin")]
		public string adminStatus { get; set; }

		[JsonProperty(PropertyName = "used")]
		public int Used { get; set; }

		[JsonProperty(PropertyName = "allocated")]
		public int Allocated { get; set; }
	}
}

