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

					//List<string> appname = new List<string>();
					Dictionary<string, List<string>> appname = new Dictionary<string, List<string>>();

					Dictionary<string, List<string>> appusage = new Dictionary<string, List<string>>();
					string startDate = "";
					string endDate = "";
					string adminStatusOnDB = "";
					int planDataPool = 0;
					int allocatedlistlength = 0;
					int groupmembercounter = 0;

					foreach (TodoItem item in _items)
					{
                        //User user = new User();
                        //user.UID = item.uid;
                        //user.Allocated = item.Allocated;

                        foreach (NameList name in item.Name)
						{
							firstname.Add(name.FirstName);
							//user.Name = new UserName();
							//user.Name.FirstName = name.FirstName;
							//user.Name.LastName = name.LastName;  

							foreach (UsageBreakdownList app in item.UsageBreakdown)
							{
								appname[name.FirstName] = new List<string>();
								appname[name.FirstName].Add(app.App1);
								appname[name.FirstName].Add(app.App2);
								appname[name.FirstName].Add(app.App3);

								appusage[name.FirstName] = new List<string>();
								appusage[name.FirstName].Add(app.App1Usage);
								appusage[name.FirstName].Add(app.App2Usage);
								appusage[name.FirstName].Add(app.App3Usage);
							}
						}
						uid.Add(item.uid);
						allocated.Add(item.Allocated);
						used.Add(item.Used);
						foreach (GroupMembers gm in item.groupMembers)
						{
							uid.Add(gm.uid);
							groupmembercounter = item.groupMembers.Count;
							foreach (NameList name in gm.Name)
							{
								firstname.Add(name.FirstName);
								foreach (UsageBreakdownList app in gm.UsageBreakdown)
								{
									appname[name.FirstName] = new List<string>();
									appname[name.FirstName].Add(app.App1);
									appname[name.FirstName].Add(app.App2);
									appname[name.FirstName].Add(app.App3);
									
									appusage[name.FirstName] = new List<string>();
									appusage[name.FirstName].Add(app.App1Usage);
									appusage[name.FirstName].Add(app.App2Usage);
									appusage[name.FirstName].Add(app.App3Usage);

									//appusage[name.FirstName].Add(app.App1Usage);
									//appusage[name.FirstName].Add(app.App2Usage);
									//appusage[name.FirstName].Add(app.App3Usage);
									//appname.Add(gm.UsageBreakdown[i].App2);
									//appname.Add(gm.UsageBreakdown[i].App3);
									//appusage.Add(gm.UsageBreakdown[i].App1Usage);
									//	appusage.Add(gm.UsageBreakdown[i].App2Usage);
									//	appusage.Add(gm.UsageBreakdown[i].App3Usage);
								}
							}
							allocated.Add(gm.Allocated);
							used.Add(gm.Used);
						}
						startDate = item.PlanStartDate;
						endDate = item.PlanEndDate;
						adminStatusOnDB = item.AdminStatus;
						planDataPool = int.Parse(item.Plan.Substring(0, 2));
						allocatedlistlength = allocated.Count;
						int[] remainder = new int[allocatedlistlength];

						Controller controller = new Controller(uid, firstname, used, allocated, remainder, appname, appusage, startDate, endDate, planDataPool);
						//Controller controller = new Controller(uid, firstname, used, allocated, remainder, AppName, AppUsage, startDate, endDate, planDataPool);
					}
				}
				else
				{
					List<string> firstname = new List<string>(); //added
					int used = 0;
					int allocated = 0;
					//string firstname = "";
					string startDate = "";
					string endDate = "";
					string adminStatusOnDB = "";
					List<string> appname = new List<string>();
					List<string> appusage = new List<string>();
					foreach (TodoItem item in _items)
					{
						foreach (NameList name in item.Name)
						{
							//firstname = username.FirstName;
							firstname.Add(name.FirstName); //added
						}
						foreach (GroupMembers gm in item.groupMembers)
						{
							foreach (NameList name in gm.Name)
							{

								firstname.Add(name.FirstName); //added
							}
						}
						
					
						startDate = item.PlanStartDate;
						endDate = item.PlanEndDate;
						adminStatusOnDB = item.AdminStatus;
						used = item.Used;
						allocated = item.Allocated;
						foreach (UsageBreakdownList inspectelement in item.UsageBreakdown)
						{
							appname.Add(inspectelement.App1);
							appname.Add(inspectelement.App2);
							appname.Add(inspectelement.App3);
							appusage.Add(inspectelement.App1Usage);
							appusage.Add(inspectelement.App2Usage);
							appusage.Add(inspectelement.App3Usage);
						}
					}
					Controller controller = new Controller(/*firstname,*/ used, allocated, appname, appusage, startDate, endDate, firstname); //added);
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
