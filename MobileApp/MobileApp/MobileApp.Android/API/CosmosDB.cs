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
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using DocumentDBTodo;
using Microsoft.Azure.Documents;

namespace MobileApp.Droid
{
	
	public partial class TodoItemManager
	{
		private List<TodoItem> _items;
		static TodoItemManager defaultInstance = new TodoItemManager();
		private List<User> _users = new List<User>();
		private List<User> _groupMembers = new List<User>();

		private const string _accountURL = @"https://monsterdb.documents.azure.com:443/";
		private const string _accountKey = @"I1qF7OwlQ22IfBCKXxjUGP3p6prmHEyqWIqU905oNavnM9aeAcuF6EXed69sfAG6cWzZpeX6ZsTBiEG5jvveUA==";
		private const string _databaseId = @"ToDoList";
		private const string _collectionId = @"Items";
		private bool _authenticationStatus = false;
		private bool _adminStatus = false;

		private Uri collectionLink = UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId);

		private DocumentClient client;

		private TodoItemManager()
		{
			client = new DocumentClient(new System.Uri(_accountURL), _accountKey);
		}

		public static TodoItemManager DefaultManager
		{
			get
			{
				return defaultInstance;
			}
			private set
			{
				defaultInstance = value;
			}
		}		

		public async Task<List<TodoItem>> GetTodoItemsAsync(string loginQuery, string _loginId , string _password)
		{
			try
			{
				var query = client.CreateDocumentQuery<TodoItem>(collectionLink, loginQuery)
					  .AsDocumentQuery();

				//user_list = new Dictionary<string, User>();

				_items = new List<TodoItem>();
				while (query.HasMoreResults)
				{
					_items.AddRange(await query.ExecuteNextAsync<TodoItem>());
				}

				//foreach(TodoItem item in _items)
				//{
				//	User currentUser = new User();
				//	currentUser.UID = item.uid;
				//	UserName currentUserName = new UserName();
				//	foreach (NameList name in item.Name)
				//	{						
				//		currentUserName.FirstName = name.FirstName;
				//		currentUserName.LastName = name.LastName;
				//	}

				//	currentUser.Name = currentUserName;
				//	currentUser.Plan = item.Plan;
				//	currentUser.AdminStatus = item.AdminStatus;
				//	currentUser.Used = item.Used;
				//	currentUser.Allocated = item.Allocated;
				//	currentUser.PlanStartDate = item.PlanStartDate;
				//	currentUser.PlanEndDate = item.PlanEndDate;
				//	currentUser.UsageBreakdown = new List<UserUsageBreakdown>();
				//	currentUser.GroupMembers = new List<Member>();

				//	foreach (UsageBreakdownList usage in item.UsageBreakdown)
				//	{
				//		UserUsageBreakdown breakdown = new UserUsageBreakdown();
				//		breakdown.AppName = usage.App;
				//		breakdown.AppDataUsed = usage.AppUsage;
				//		currentUser.UsageBreakdown.Add(breakdown);
				//	}

				//	foreach(GroupMembers member in item.groupMembers)
				//	{
				//		Member groupMember = new Member();
				//		groupMember.UID = member.uid;
				//		UserName groupMemberName = new UserName();

				//		foreach(NameList name in member.Name)
				//		{
				//			groupMemberName.FirstName = name.FirstName;
				//			groupMemberName.LastName = name.LastName;
				//		}
				//		groupMember.Name = groupMemberName;
				//		groupMember.AdminStatus = member.adminStatus;
				//		groupMember.Used = member.Used;
				//		groupMember.Allocated = member.Allocated;
				//		groupMember.UsageBreakdown = new List<UserUsageBreakdown>();

				//		foreach(UsageBreakdownList usage in member.UsageBreakdown)
				//		{
				//			UserUsageBreakdown breakdown = new UserUsageBreakdown();
				//			breakdown.AppName = usage.App;
				//			breakdown.AppDataUsed = usage.AppUsage;
				//			groupMember.UsageBreakdown.Add(breakdown);
				//		}
				//		currentUser.GroupMembers.Add(groupMember);
				//	}
				//}

				//foreach(TodoItem tempItem in await query.ExecuteNextAsync<TodoItem>())
				//{
				//	user_list[tempItem.uid] = new User();
				//	user_list[tempItem.uid].UID = tempItem.uid;
				//	user_list[tempItem.uid].Name = new UserName();
				//	user_list[tempItem.uid].Name.FirstName = tempItem.Name[0].FirstName;
				//	user_list[tempItem.uid].Name.LastName = tempItem.Name[0].LastName;
				//	user_list[tempItem.uid].Plan = tempItem.Plan;
				//	user_list[tempItem.uid].AdminStatus = tempItem.AdminStatus;
				//	user_list[tempItem.uid].Used = tempItem.Used;
				//	user_list[tempItem.uid].Allocated = tempItem.Allocated;
				//	user_list[tempItem.uid].PlanStartDate = tempItem.PlanStartDate;
				//	user_list[tempItem.uid].PlanEndDate = tempItem.PlanEndDate;
				// have to remake structure so that the apps have the same name for key and separated into different objects, i.e.{}
				// possibly need to change to normal SQL structure if wanting to make this easier
				//	user_list[tempItem.uid].UsageBreakdown = new List<UserUsageBreakdown>
				//	{
				//		[0] = new UserUsageBreakdown(),
				//		[1] = new UserUsageBreakdown(),
				//		[2] = new UserUsageBreakdown()
				//	};
				//	user_list[tempItem.uid].UsageBreakdown[0].AppName = tempItem.UsageBreakdown[0].App1;
				//	user_list[tempItem.uid].UsageBreakdown[0].AppDataUsed = tempItem.UsageBreakdown[0].App1Usage;
				//	user_list[tempItem.uid].UsageBreakdown[1].AppName = tempItem.UsageBreakdown[0].App2;
				//	user_list[tempItem.uid].UsageBreakdown[1].AppDataUsed = tempItem.UsageBreakdown[0].App2Usage;
				//	user_list[tempItem.uid].UsageBreakdown[2].AppName = tempItem.UsageBreakdown[0].App3;
				//	user_list[tempItem.uid].UsageBreakdown[2].AppDataUsed = tempItem.UsageBreakdown[0].App3Usage;

				//}

				if (_items.Count == 0)
				{
					Console.WriteLine("Authentication failed");
				}
				else
				{
					foreach (TodoItem item in _items)
					{
						if (item.password == _password && item.uid == _loginId)
						{
							//Console.WriteLine("Authentication succeeed");
							_authenticationStatus = true;
							AssignAllValues();
						}
						else
						{
							Console.WriteLine("Authentication failed");
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(@"ERROR {0}", e.Message);
				return null;
			}
			return _items;
		}

		public void AssignAllValues()
		{
			try
			{
				foreach (TodoItem item in _items)
				{
					User currentUser = new User();
					currentUser.UID = item.uid;
					UserName currentUserName = new UserName();
					foreach (NameList name in item.Name)
					{
						currentUserName.FirstName = name.FirstName;
						currentUserName.LastName = name.LastName;
					}

					currentUser.Name = currentUserName;
					currentUser.Plan = item.Plan;
					currentUser.AdminStatus = item.AdminStatus;
					currentUser.Used = item.Used;
					currentUser.Allocated = item.Allocated;
					currentUser.PlanStartDate = item.PlanStartDate;
					currentUser.PlanEndDate = item.PlanEndDate;
					currentUser.UsageBreakdown = new List<UserUsageBreakdown>();
					currentUser.GroupMembers = new List<Member>();

					foreach (UsageBreakdownList usage in item.UsageBreakdown)
					{
						UserUsageBreakdown breakdown = new UserUsageBreakdown();
						breakdown.AppName = usage.App;
						breakdown.AppDataUsed = usage.AppUsage;
						currentUser.UsageBreakdown.Add(breakdown);
					}

					foreach (GroupMembers member in item.groupMembers)
					{						
						Member groupMember = new Member();						
						groupMember.UID = member.uid;
						UserName groupMemberName = new UserName();

						foreach (NameList name in member.Name)
						{
							groupMemberName.FirstName = name.FirstName;
							groupMemberName.LastName = name.LastName;
						}
						groupMember.Name = groupMemberName;

						User groupMemberUser = new User();
						groupMemberUser.UID = member.uid;
						groupMemberUser.Name = groupMemberName;
						
						if (currentUser.AdminStatus)
						{
							_adminStatus = true;
							groupMember.AdminStatus = member.adminStatus;
							groupMember.Used = member.Used;
							groupMember.Allocated = member.Allocated;
							groupMember.UsageBreakdown = new List<UserUsageBreakdown>();							

							foreach (UsageBreakdownList usage in member.UsageBreakdown)
							{
								UserUsageBreakdown breakdown = new UserUsageBreakdown();
								breakdown.AppName = usage.App;
								breakdown.AppDataUsed = usage.AppUsage;
								groupMember.UsageBreakdown.Add(breakdown);
							}
							currentUser.GroupMembers.Add(groupMember);
							groupMemberUser.AdminStatus = member.adminStatus;
							groupMemberUser.Used = member.Used;
							groupMemberUser.Allocated = member.Allocated;
							groupMemberUser.UsageBreakdown = groupMember.UsageBreakdown;
						}
						_groupMembers.Add(groupMemberUser);
					}
					_users.Add(currentUser);
					_groupMembers.ForEach(x => _users.Add(x));
				}
				Controller controller = new Controller(_users);
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(@"error {0}", e.Message);
			}
		}

		public async Task UpdateDocumentDB()
		{
			Document doc = client.CreateDocumentQuery<Document>(collectionLink)
							.Where(r => r.Id == "MemberDBTestUpdate")
							.AsEnumerable()
							.SingleOrDefault();


			//Update some properties on the found resource
			doc.SetPropertyValue("Plan", "20GB");

			//Now persist these changes to the database by replacing the original resource
			Document updated = await client.ReplaceDocumentAsync(doc);
		}

		public Boolean getLoginStatus()
		{
			return _authenticationStatus;
		}

		public Boolean getAdminStatus()
		{
			return _adminStatus;
		}

	}
}
