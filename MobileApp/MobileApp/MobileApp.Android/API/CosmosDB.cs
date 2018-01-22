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
		private List<TodoItem> _items;
		static TodoItemManager defaultInstance = new TodoItemManager();

		private const string _accountURL = @"https://7cd241a9-0ee0-4-231-b9ee.documents.azure.com:443/";
		private const string _accountKey = @"qd90jPFq21MhZ8i3HfJA943zmDWRqVBhTZJNOYYqKIFqvjuBau6k2CMKmqtowyz7jRkPObpYS1AO4Jvq9DFrdQ==";
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

				_items = new List<TodoItem>();
				while (query.HasMoreResults)
				{
					_items.AddRange(await query.ExecuteNextAsync<TodoItem>());
				}

				if (_items.Count ==0)
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

		public List<TodoItem> AssignAllValues()
		{
			try
			{
				if (_items[0].AdminStatus.ToUpper() == "TRUE")
				{
					_adminStatus = true;

					List<string> uid = new List<string>();
					List<string> firstname = new List<string>();
					List<int> used = new List<int>();
					List<int> allocated = new List<int>();
					List<int> remainder = new List<int>();
					List<string> AppName = new List<string>();
					List<string> AppUsage = new List<string>();
					string startDate = "";
					string endDate = "";
					string adminStatusOnDB = "";
					int planDataPool = 0;

					foreach (TodoItem item in _items)
					{

						foreach (NameList name in item.Name)
						{
							firstname.Add(name.FirstName);
						}
						uid.Add(item.uid);
						allocated.Add(item.Allocated);
						used.Add(item.Used);
						foreach (GroupMembers gm in item.groupMembers)
						{
							uid.Add(gm.uid);
							foreach (NameList name in gm.Name)
							{
								firstname.Add(name.FirstName);
							}

							allocated.Add(gm.Allocated);
							used.Add(gm.Used);
						}
					startDate = item.PlanStartDate;
					endDate = item.PlanEndDate;
					adminStatusOnDB = item.AdminStatus;
					planDataPool = int.Parse(item.Plan.Substring(0, 2));

					Controller controller = new Controller(uid, firstname, used, allocated, remainder, AppName, AppUsage, startDate, endDate, planDataPool);
					}
				}
				else
				{
					int used = 0;
					int allocated = 0;
					string firstname = "";
					string startDate = "";
					string endDate = "";
					string adminStatusOnDB = "";
					foreach (TodoItem item in _items)
					{
						foreach (NameList username in item.Name)
						{
							firstname = username.FirstName;
						}
						startDate = item.PlanStartDate;
						endDate = item.PlanEndDate;
						adminStatusOnDB = item.AdminStatus;
						used = item.Used;
						allocated = item.Allocated;
						
					}
					Controller controller = new Controller(firstname, used, allocated, startDate, endDate);
				}
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(@"ERROR {0}", e.Message);
				return null;
			}
			return _items;
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
