using System.Collections.Specialized;
using JMC.Portal.Business.PortalModels;

namespace JMC.Portal.Business.PortalModels
{
    public class ApplicationRoleProvider
    {
        #region Static Properties

        public static int ApplicationID = 0;

        #endregion


        #region Properties

        public string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        private string _applicationName;

        #endregion


        #region Methods

        public void Initialize(string name, NameValueCollection config)
        {
            //ApplicationName = Common.GetAppSetting("ApplicationName", "Intranet");
            //ApplicationID = (int) Enum.Parse(typeof (Application.Values), ApplicationName);

            //if (new Application(ApplicationID).Active == false) {
            //  ApplicationID = (int) Application.Values.Intranet;
            //}

            Initialize(name, config);
        }

        public void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            //foreach (String username in usernames) {
            //  User user = new User(username);
            //  foreach (string rolename in roleNames) {
            //    user.AddRelatedItem<ApplicationRole>(new ApplicationRole(ApplicationID, rolename).ID);
            //  }
            //  user.Save();
            //}
        }

        public void CreateRole(string roleName)
        {
            //ApplicationRole role = new ApplicationRole();
            //role.ApplicationID = new Application(_applicationName).ID;
            //role.Name = roleName;
            //role.Save();
        }

        public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            //new ApplicationRole(ApplicationID, roleName).Delete();
            //return true;
            return true;
        }

        public string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            //return new ApplicationRole(ApplicationID, roleName).GetUsers()
            //  .FindAll(delegate(User user) { return user.UserName.Equals(usernameToMatch); })
            //  .ConvertAll<String>(delegate(User user) { return user.UserName; })
            //  .ToArray();
            return new string[] { };
        }

        public string[] GetAllRoles()
        {
            //return ApplicationRole.GetAll().ConvertAll<String>(delegate(ApplicationRole role) { return role.Name; }).ToArray();
            return new string[] { };
        }

        public string[] GetRolesForUser(string username)
        {
            PortalContext db = new PortalContext();

            Employee employee = Employee.FindByDomainAndSAMAccountName(ref db, username);

            if (employee != null && employee.UserId > 0)
            {
                IEnumerable<string> applicationRoles_P = (from ar in employee.ApplicationRoles where ar.ApplicationId == (long)Enums.Applications.Portal select ar.Name);

                IEnumerable<string> applicationRoles_WT = (from ar in employee.ApplicationRoles where ar.ApplicationId == (long)Enums.Applications.WTCPipeportal select ar.Name);

                IEnumerable<string> applicationRoles = applicationRoles_P.Concat(applicationRoles_WT);
                if (applicationRoles.Count() > 0)
                {
                    return applicationRoles.ToArray<string>();
                }
            }
            //return new User(username).ApplicationRoles.ConvertAll<String>(delegate(ApplicationRole role) { return role.Name; }).ToArray();
            return new string[] { };
        }

        public string[] GetUsersInRole(string roleName)
        {
            //List<User> users = new ApplicationRole(ApplicationID, roleName).GetUsers();
            //return users.ConvertAll<String>(delegate(User user) { return user.UserName; }).ToArray();
            return new string[] { };
        }

        public bool IsUserInRole(string username, string roleName)
        {
            //return new User(username).HasRole(new ApplicationRole(ApplicationID, roleName).ID);
            return false;
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            //foreach (String username in usernames) {
            //  User user = new User(username);
            //  foreach (string rolename in roleNames) {
            //    user.RemoveRelatedItem<ApplicationRole>(new ApplicationRole(ApplicationID, rolename).ID);
            //  }
            //  user.Save();
            //}
        }

        public bool RoleExists(string roleName)
        {
            //return new ApplicationRole(ApplicationID, roleName).HasData;
            return false;
        }

        #endregion
    }
}