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
using MobileApp.Droid.Helpers;
using MobileApp.Constants;

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
					currentUser.AddOns = item.AddOns;
					currentUser.Outstanding = item.Outstanding;
					currentUser.PlanStartDate = item.PlanStartDate;
					currentUser.PlanEndDate = item.PlanEndDate;
					currentUser.UsageBreakdown = new List<UserUsageBreakdown>();
					currentUser.GroupMembers = new List<Member>();

					foreach (UsageBreakdownList usage in item.UsageBreakdown)
					{
						UserUsageBreakdown breakdown = new UserUsageBreakdown();
						breakdown.Day = usage.Day;
						breakdown.DataUsed = usage.DataUsed;
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
						groupMember.AdminStatus = member.AdminStatus;

						if (currentUser.AdminStatus)
						{
							_adminStatus = true;
							
							groupMember.Used = member.Used;
							groupMember.Allocated = member.Allocated;
							groupMember.UsageBreakdown = new List<UserUsageBreakdown>();							

							foreach (UsageBreakdownList usage in member.UsageBreakdown)
							{
								UserUsageBreakdown breakdown = new UserUsageBreakdown();
								breakdown.Day = usage.Day;
								breakdown.DataUsed = usage.DataUsed;
								groupMember.UsageBreakdown.Add(breakdown);
							}
							currentUser.GroupMembers.Add(groupMember);
							groupMemberUser.AdminStatus = member.AdminStatus;
							groupMemberUser.Used = member.Used;
							groupMemberUser.Allocated = member.Allocated;
							groupMemberUser.UsageBreakdown = groupMember.UsageBreakdown;
						}
						currentUser.GroupMembers.Add(groupMember);
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

		public async Task<List<User>> UpdateMemberAllocation(List<User> user)
		{
			var queryDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, string.Format(StringConstants.Localizable.ReadQuery, Controller._userLoggedIn.UID)).AsEnumerable().First();
			if (Math.Round(_users.Where(x => x.UID == queryDoc.uid).FirstOrDefault().Allocated, 2) <= queryDoc.Used) 
			{
				queryDoc.Allocated = queryDoc.Used;
				User admin = user.Where(x => x.UID == queryDoc.uid).FirstOrDefault();
				admin.Allocated = admin.Used;
			}
			else 
			{
				queryDoc.Allocated = Math.Round(_users.Where(x => x.UID == queryDoc.uid).FirstOrDefault().Allocated, 2);
				User admin = user.Where(x => x.UID == queryDoc.uid).FirstOrDefault();
				admin.Allocated = Math.Round(_users.Where(x => x.UID == queryDoc.uid).FirstOrDefault().Allocated, 2);
			}
			
			foreach (var member in queryDoc.groupMembers)
			{
				var nonAdminDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, string.Format(StringConstants.Localizable.ReadQuery, member.uid)).AsEnumerable().First();
				if (Math.Round(_users.Where(x => x.UID == member.uid).FirstOrDefault().Allocated, 2) <= member.Used)
				{
					member.Allocated = member.Used;
					User nonAdmin = user.Where(x => x.UID == member.uid).FirstOrDefault();
					nonAdmin.Allocated = nonAdmin.Used;
					nonAdminDoc.Allocated = member.Used;
				}
				else
				{
					member.Allocated = Math.Round(_users.Where(x => x.UID == member.uid).FirstOrDefault().Allocated, 2);
					User nonAdmin = user.Where(x => x.UID == member.uid).FirstOrDefault();
					nonAdmin.Allocated = Math.Round(_users.Where(x => x.UID == member.uid).FirstOrDefault().Allocated, 2);
					nonAdminDoc.Allocated = nonAdmin.Allocated;
				}
				await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, nonAdminDoc.id), nonAdminDoc);
			}
			
			await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, queryDoc.id), queryDoc);
			return user;
		}

		public async Task<User> CreateNewUser(User user, Member newMember)
		{
            if (!newMember.AdminStatus)
            {
                TodoItem queryDoc;
                try
                {
                    queryDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, string.Format(StringConstants.Localizable.ReadQuery,Controller._userLoggedIn.UID)).AsEnumerable().First();
                }
                catch (Exception)
                {
                    queryDoc = null;
                    Console.WriteLine("404 not found");
                    return user;
                }
                
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
                return user;
            }
            else
            {
                var adminDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, string.Format(StringConstants.Localizable.ReadQuery, Controller._userLoggedIn.UID)).AsEnumerable().First();
                TodoItem newAdminDoc;
                try
                {
                    newAdminDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, string.Format(StringConstants.Localizable.ReadQuery, newMember.UID)).AsEnumerable().First();
                }
                catch (Exception)
                {
                    newAdminDoc = null;
                    Console.WriteLine("404 not found");
                    return user;
                }
                
                newAdminDoc.uid = newMember.UID;
                newAdminDoc.Name = new List<NameList>();
                newAdminDoc.Name.Add(new NameList
                {
                    FirstName = newMember.Name.FirstName,
                    LastName = newMember.Name.LastName
                });                   
                newAdminDoc.Plan = Controller._userLoggedIn.Plan;
                newAdminDoc.AdminStatus = newMember.AdminStatus;
                newAdminDoc.Used = newMember.Used;
                newAdminDoc.Allocated = newMember.Allocated;
                newAdminDoc.PlanStartDate = Controller._userLoggedIn.PlanStartDate;
                newAdminDoc.PlanEndDate = Controller._userLoggedIn.PlanEndDate;
                newAdminDoc.UsageBreakdown = new List<UsageBreakdownList>();
                newMember.UsageBreakdown.ForEach(x => newAdminDoc.UsageBreakdown.Add(new UsageBreakdownList
                {
                    Day = x.Day,
                    DataUsed = x.DataUsed
                }));

                newAdminDoc.groupMembers = new List<GroupMembers>();
                newAdminDoc.groupMembers.Add(ClassConverterHelper.createGroupMember(user));
                user.GroupMembers.ForEach(x => newAdminDoc.groupMembers.Add(ClassConverterHelper.createGroupMember(x)));

                User newAdminUser = ClassConverterHelper.createUser(newMember);
                newAdminDoc.groupMembers.ForEach(x => newAdminUser.GroupMembers.Add(ClassConverterHelper.createMember(x)));
                adminDoc.groupMembers.Add(ClassConverterHelper.createGroupMember(newMember));

                await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, adminDoc.id), adminDoc);
                await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, newAdminDoc.id), newAdminDoc);
                user.GroupMembers.Add(newMember);
                Controller._users.Add(newAdminUser);
                return user;
            }            
        }

        public async Task<User> DeleteGroupMember(User user, User targetMember)
        {
            var queryDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, string.Format(StringConstants.Localizable.ReadQuery, Controller._userLoggedIn.UID)).AsEnumerable().First();
            GroupMembers groupMemberToDelete;
            Member memberToDelete;

            Console.WriteLine(targetMember.Name.FirstName);

            if (targetMember.UID != queryDoc.uid)
            {
                groupMemberToDelete = queryDoc.groupMembers.Where(x => x.uid == targetMember.UID).FirstOrDefault();
                queryDoc.groupMembers.Remove(groupMemberToDelete);
                memberToDelete = user.GroupMembers.Where(x => x.UID == targetMember.UID).FirstOrDefault();
                user.GroupMembers.Remove(memberToDelete);
            }     

            await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, queryDoc.id), queryDoc);
            return user;
        }

		public async Task<User> BuyAddOns(User user, double addonAmount, double outstanding)
		{
			var queryDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, string.Format(StringConstants.Localizable.ReadQuery, Controller._userLoggedIn.UID)).AsEnumerable().First();
			queryDoc.AddOns += addonAmount;
			queryDoc.Outstanding = outstanding;
			user.AddOns += addonAmount;
			user.Outstanding = outstanding;

			await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, queryDoc.id), queryDoc);
			return user;
		}

		public async Task<User> TransferData(User sourceUser, User targetUser, double transferAmount)
		{
			if ((sourceUser.Allocated - sourceUser.Used) > transferAmount)
			{
				string adminUser = sourceUser.GroupMembers.Where(x => x.AdminStatus == true).FirstOrDefault().UID;

				var sourceUserDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, string.Format(StringConstants.Localizable.ReadQuery, sourceUser.UID)).AsEnumerable().First();
				sourceUserDoc.Allocated -= transferAmount;

				var targetUserDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, string.Format(StringConstants.Localizable.ReadQuery, targetUser.UID)).AsEnumerable().First();
				targetUserDoc.Allocated += transferAmount;

				var adminUserDoc = client.CreateDocumentQuery<TodoItem>(collectionLink, string.Format(StringConstants.Localizable.ReadQuery, adminUser)).AsEnumerable().First();
				adminUserDoc.groupMembers.Where(x => x.uid == sourceUser.UID).FirstOrDefault().Allocated -= transferAmount;
				adminUserDoc.groupMembers.Where(x => x.uid == targetUser.UID).FirstOrDefault().Allocated += transferAmount;

				await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, sourceUserDoc.id), sourceUserDoc);
				await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, targetUserDoc.id), targetUserDoc);
				await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, adminUserDoc.id), adminUserDoc);

				sourceUser.Allocated -= transferAmount;
				sourceUser.GroupMembers.Where(x => x.UID == targetUser.UID).FirstOrDefault().Allocated += transferAmount;
				Controller._users.Where(x => x.UID == targetUser.UID).FirstOrDefault().Allocated += transferAmount;
				return sourceUser;
			}
			return sourceUser;			
		}


		public Boolean getLoginStatus()
		{
			return _authenticationStatus;
		}

		public Boolean getAdminStatus()
		{
			return _adminStatus;
		}

        public void ClearAll()
        {
            _items = null;
            _users = new List<User>();
            _groupMembers = new List<User>();
        }
    }
}
