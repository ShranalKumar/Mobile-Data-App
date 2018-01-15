using System;
using Newtonsoft.Json;

namespace DocumentDBTodo
{
	public class TodoItem
	{

		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		//[JsonProperty(PropertyName = "Name")]
		//public string[] Name { get; set; }

		[JsonProperty(PropertyName = "FirstName")]
		public string FirstName { get; set; }


		[JsonProperty(PropertyName = "LastName")]
		public string LastName { get; set; }


		[JsonProperty(PropertyName = "plan")]
		public string Plan { get; set; }

		[JsonProperty(PropertyName = "PlanStartDate")]
		public string PlanStartDate { get; set; }

		[JsonProperty(PropertyName = "PlanEndDate")]
		public string PlanEndDate { get; set; }


		[JsonProperty(PropertyName = "Admin")]
		public string AdminStatus { get; set; }

		[JsonProperty(PropertyName = "Used")]
		public string Used { get; set; }

		[JsonProperty(PropertyName = "Allocated")]
		public string Allocated { get; set; }


		//[JsonProperty (PropertyName = "id")]
		//public string Id { get; set; }

		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "complete")]
		public bool Complete { get; set; }

	}
}

