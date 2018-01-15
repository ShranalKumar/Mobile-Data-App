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
		static TodoItemManager defaultInstance = new TodoItemManager();

		private const string accountURL = @"https://9b48ec4c-0ee0-4-231-b9ee.documents.azure.com:443/";
		private const string accountKey = @"9YMu58QaaaWSbGdVXzXCmUsxiOYqZdIQatyyNplkw6xi9LOH6afai77iDPmgnjsTWszLy9b6RQCOXnLQLGdk7w==";
		private const string databaseId = @"ToDoList";
		private const string collectionId = @"Items";

		private Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

		private DocumentClient client;

		private TodoItemManager()
		{
			client = new DocumentClient(new System.Uri(accountURL), accountKey);
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

		public async Task<List<TodoItem>> GetTodoItemsAsync()
		{
			try
			{
				var AQuery = "select T.id, G.FirstName, G.LastName , G.Used, G.Allocated, G.Admin from ToDoList T join G in T.GroupMember where T.id = 'Realtest1'";
				var filterQuery = "select t.id, t.Plan , t.Admin , t.Used , t.PlanStartDate, t.Allocated, t.PlanEndDate, a.FirstName, a.LastName from ToDoList t join a in t.Name where t.id = 'Realtest1'";


				var query = client.CreateDocumentQuery<TodoItem>(collectionLink, AQuery)
					  .AsDocumentQuery();

				var result = client.CreateDocumentQuery<TodoItem>(collectionLink, filterQuery)
					.AsDocumentQuery();


				Items = new List<TodoItem>();
				while (query.HasMoreResults || result.HasMoreResults)
				{
					Items.AddRange(await query.ExecuteNextAsync<TodoItem>());
					Items.AddRange(await result.ExecuteNextAsync<TodoItem>());
				}

				int Querylength = Items.Count;


				String[] NameList = new string[Querylength];
				int[] UsedList = new int[Querylength];
				int[] AllocatedList = new int[Querylength];
				int[] Remainder = new int[Querylength];

				for (int i = 0; i <= Querylength - 1; i++)
				{
					NameList[i] = Items[i].FirstName;
					UsedList[i] = int.Parse(Items[i].Used);
					AllocatedList[i] = int.Parse(Items[i].Allocated);
					if (AllocatedList[i] - UsedList[i] < 0) //Enters this clause iff admin; because admin's allocate is less than 0
					{
						int findPlanGB = Int32.Parse(Items[i].Plan.Substring(0, 2));
						Remainder[i] = findPlanGB - AllocatedList.Sum() - UsedList[i];

					}
					else
					{
						Remainder[i] = AllocatedList[i] - UsedList[i];
					}
					ViewModel view = new ViewModel(NameList, UsedList, AllocatedList, Remainder);

				}

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
		//	{
		//		throw;
		//	}
		//}


		public async Task UpdateDocumentDB()
		{
			var AQuery = "select T.id, T.Plan, G.FirstName, G.LastName , G.Used, G.Allocated, G.Admin from ToDoList T join G in T.GroupMember where T.id = 'Zamora'";

			Document doc = client.CreateDocumentQuery<Document>(collectionLink)
							.Where(r => r.Id == "Zamora")
							.AsEnumerable()
							.SingleOrDefault();


			//Update some properties on the found resource
			doc.SetPropertyValue("Plan", "20GB");

			//Now persist these changes to the database by replacing the original resource
			Document updated = await client.ReplaceDocumentAsync(doc);
		}

	}
}
