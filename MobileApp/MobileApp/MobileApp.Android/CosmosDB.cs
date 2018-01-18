﻿using System;
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
		static TodoItemManager defaultInstance = new TodoItemManager();

		private const string _accountURL = @"https://7cd241a9-0ee0-4-231-b9ee.documents.azure.com:443/";
		private const string _accountKey = @"qd90jPFq21MhZ8i3HfJA943zmDWRqVBhTZJNOYYqKIFqvjuBau6k2CMKmqtowyz7jRkPObpYS1AO4Jvq9DFrdQ==";
		private const string _databaseId = @"ToDoList";
		private const string _collectionId = @"Items";

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

		public List<TodoItem> Items { get; private set; }

		public async Task<List<TodoItem>> GetTodoItemsAsync(string loginQuery, string _loginId , string _password)
		{
			try
			{
				var query = client.CreateDocumentQuery<TodoItem>(collectionLink, loginQuery)
					  .AsDocumentQuery();

				Items = new List<TodoItem>();
				while (query.HasMoreResults)
				{
					Items.AddRange(await query.ExecuteNextAsync<TodoItem>());
				}

				if (Items.Count ==0)
				{
					Console.WriteLine("Authentication failed");
				}
				else
				{
					foreach (TodoItem item in Items)
					{
						if (item.password == _password && item.uid == _loginId)
						{
							Console.WriteLine("Authentication succeeed");
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
			return Items;
		}

		public async Task<List<TodoItem>> GetTodoItemsAsync()
		{
			try
			{
				//var AQuery = "select T.id, G.FirstName, G.LastName , G.Used, G.Allocated, G.Admin from ToDoList T join G in T.GroupMember where T.id = 'MemberDBTest'";
				//var filterQuery = "select t.id, t.Plan , t.Admin , t.Used , t.Name, t.PlanStartDate, t.Allocated, t.PlanEndDate, a.FirstName, a.LastName from ToDoList t join a in t.Name where t.id = 'MemberDBTest'";
				var q = "select * from T where T.id = 'MemberDBTest'";


				var query = client.CreateDocumentQuery<TodoItem>(collectionLink, q)
					  .AsDocumentQuery();

				//var query = client.CreateDocumentQuery<TodoItem>(collectionLink, AQuery)
				//	  .AsDocumentQuery();

				//var result = client.CreateDocumentQuery<TodoItem>(collectionLink, filterQuery)
				//	.AsDocumentQuery();

				Items = new List<TodoItem>();
				while (query.HasMoreResults)
				{
					Items.AddRange(await query.ExecuteNextAsync<TodoItem>());
				}


				//Items = new List<TodoItem>();
				//while (query.HasMoreResults || result.HasMoreResults)
				//{
				//	Items.AddRange(await query.ExecuteNextAsync<TodoItem>());
				//	Items.AddRange(await result.ExecuteNextAsync<TodoItem>());
				//}

				//int Querylength = Items.Count;

				//String[] NameList = new string[Querylength];
				//int[] UsedList = new int[Querylength];
				//int[] AllocatedList = new int[Querylength];
				//int[] Remainder = new int[Querylength];

				List<string> uid = new List<string>();
				List<string> fullname = new List<string>();
				List<int> used = new List<int>();
				List<int> allocated = new List<int>();
				List<int> remainder = new List<int>();
				List<string> AppName = new List<string>();
				List<string> AppUsage = new List<string>();
				string startDate;
				string endDate;




				foreach (TodoItem item in Items)
				{
					foreach (NameList name in item.Name)
					{
						fullname.Add(name.FirstName + " " + name.LastName);
					}
					uid.Add(item.uid);
					allocated.Add(item.Allocated);
					used.Add(item.Used);
					foreach (GroupMembers gm in item.groupMembers)
					{
						uid.Add(gm.uid);
						foreach (NameList name in gm.Name)
						{
							fullname.Add(name.FirstName + " " + name.LastName);
						}

						AppName.Add(gm.UsageBreakdown[0].App1);
						AppName.Add(gm.UsageBreakdown[0].App2);
						AppName.Add(gm.UsageBreakdown[0].App3);

						AppUsage.Add(gm.UsageBreakdown[0].App1Usage);
						AppUsage.Add(gm.UsageBreakdown[0].App2Usage);
						AppUsage.Add(gm.UsageBreakdown[0].App3Usage);

						allocated.Add(gm.Allocated);
						used.Add(gm.Used);
					}
					startDate = item.PlanStartDate;
					endDate = item.PlanEndDate;
				}
				Controller controller = new Controller(uid, fullname, used, allocated, remainder, AppName, AppUsage /*startDate, endDate*/);
				

				//for (int i = 0; i <= Querylength - 1; i++)
				//{
				//	NameList[i] = Items[i].FirstName;
				//	UsedList[i] = int.Parse(Items[i].Used);
				//	AllocatedList[i] = int.Parse(Items[i].Allocated);
				//	if (AllocatedList[i] - UsedList[i] < 0) //Enters this clause iff admin; because admin's allocate is less than 0
				//	{
				//		int findPlanGB = Int32.Parse(Items[i].Plan.Substring(0, 2));
				//		Remainder[i] = findPlanGB - AllocatedList.Sum() - UsedList[i];

				//	}
				//	else
				//	{
				//		Remainder[i] = AllocatedList[i] - UsedList[i];
				//	}
				//	ViewModel view = new ViewModel(NameList, UsedList, AllocatedList, Remainder);

				//}


			}
			catch (Exception e)
			{
				Console.Error.WriteLine(@"ERROR {0}", e.Message);
				return null;
			}
			return Items;
		}

		//public async Task UpdateDocumentDB(TodoItem item)
		//{
		//	await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, item.Id), item);
		//}

		//private async Task ReplaceFamilyDocument(string databaseName, string collectionName, string familyName, Family updatedFamily)
		//{
		//	try
		//	{
		//		await this.client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, familyName), updatedFamily);
		//		this.WriteToConsoleAndPromptToContinue("Replaced Family {0}", familyName);
		//	}
		//	catch (DocumentClientException de)
		//	{C:\Users\kan02\Desktop\Trustpower\project\Trustpoer new App\MobileApp\MobileApp\MobileApp.Android\Views\NonAdminDashbaordView.cs
		//		throw;
		//	}
		//}


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

	}
}