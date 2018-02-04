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
using MobileApp.Droid.Views;

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
					  
				_items = new List<TodoItem>();
				while (query.HasMoreResults)
				{
					_items.AddRange(await query.ExecuteNextAsync<TodoItem>());
				}
				
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

					currentUserName.FirstName = item.Name[0].FirstName;
					currentUserName.LastName = item.Name[0].LastName;
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

						groupMemberName.FirstName = member.Name[0].FirstName;
						groupMemberName.LastName = member.Name[0].LastName;
						groupMember.Name = groupMemberName;

						User groupMemberUser = new User();
						groupMemberUser.UID = member.uid;
						groupMemberUser.Name = groupMemberName;
						
						if (currentUser.AdminStatus)
						{
							_adminStatus = true;
							groupMember.AdminStatus = member.AdminStatus;
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
							groupMemberUser.AdminStatus = member.AdminStatus;
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

		public async Task<User> UpdateMemberAllocation(User user, double allocated)
		{
			var queryDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, "select * from t where t.uid = '1004'").AsEnumerable().First();
            GroupMembers userToUpdate;
            if (user.UID != queryDoc.uid)
            {
                userToUpdate = queryDoc.groupMembers.Where(x => x.uid == user.UID).FirstOrDefault();
                userToUpdate.Allocated = allocated;
            } else
            {
                queryDoc.Allocated = allocated;
            }
            
			user.Allocated = allocated;
			await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, queryDoc.id), queryDoc);
			return user;
		}

		public async Task<User> CreateNewUser(User user, Member newMember)
		{
            TodoItem queryDoc;

            //if (!newMember.AdminStatus)
            //{
                queryDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, "select * from t where t.uid = '1004'").AsEnumerable().First();
                GroupMembers newGroupMember = new GroupMembers();
                newGroupMember.uid = newMember.UID;
                NameList newUserName = new NameList();
                newUserName.FirstName = newMember.Name.FirstName;
                newUserName.LastName = newMember.Name.LastName;
                newGroupMember.Name = new List<NameList>();
                newGroupMember.Name.Add(newUserName);
                newGroupMember.AdminStatus = newMember.AdminStatus;
                newGroupMember.Used = newMember.Used;
                newGroupMember.Allocated = newMember.Allocated;
                newGroupMember.UsageBreakdown = new List<UsageBreakdownList>();

                User newUser = new User();
                newUser.UID = newGroupMember.uid;
                newUser.Name = new UserName();
                newUser.Name.FirstName = newGroupMember.Name[0].FirstName;
                newUser.Name.LastName = newGroupMember.Name[0].LastName;
                newUser.AdminStatus = newGroupMember.AdminStatus;
                newUser.Used = newGroupMember.Used;
                newUser.Allocated = newGroupMember.Allocated;
                newUser.UsageBreakdown = new List<UserUsageBreakdown>();

                Controller._users.Add(newUser);
                queryDoc.groupMembers.Add(newGroupMember);
                await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, queryDoc.id), queryDoc);
                //return user;
            //}
            //else
            //{
            //    User newAdminUser = newMember;
            //    newAdminUser.GroupMembers = user.GroupMembers;
            //    user.GroupMembers.Add(newMember);
            //    newAdminUser.GroupMembers.Add(new Member
            //    {
            //        UID = user.UID,
            //        Name = user.Name,
            //        AdminStatus = user.AdminStatus,
            //        Used = user.Used,
            //        Allocated = user.Allocated,
            //        UsageBreakdown = user.UsageBreakdown
            //    });

            //    CreateNewDocument(newAdminUser);

            //    await client.CreateDocumentAsync(collectionLink, newAdminUser);
            //}
            
			user.GroupMembers.Add(newMember);
            return user;
        }


        public async Task<User> DeleteGroupMember(User user, User targetMember)
        {
            var queryDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, "select * from t where t.uid = '1004'").AsEnumerable().First();
            GroupMembers groupMemberToDelete;
            Member memberToDelete;

            Console.WriteLine(targetMember.Name.FirstName);

            if (targetMember.UID != queryDoc.uid)
            {
                groupMemberToDelete = queryDoc.groupMembers.Where(x => x.uid == targetMember.UID).FirstOrDefault();
                queryDoc.groupMembers.Remove(groupMemberToDelete);
                Controller._users.Remove(targetMember);
                memberToDelete = user.GroupMembers.Where(x => x.UID == targetMember.UID).FirstOrDefault();
                user.GroupMembers.Remove(memberToDelete);
            }     

            await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, queryDoc.id), queryDoc);
            return user;
        }

        public Boolean getLoginStatus()
		{
			return _authenticationStatus;
		}

		public Boolean getAdminStatus()
		{
			return _adminStatus;
		}

  //      public void CreateNewDocument(User newUser)
  //      {

  //      }
	}
}
