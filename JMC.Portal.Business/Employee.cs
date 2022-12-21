using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMC.Portal.Business.PortalModels
{
	public partial class Employee
	{
        public bool HasRole(long applicationRoleId, PortalContext _portalContext)
        {
            //return (from ar in this.ApplicationRoles where ar.ApplicationRoleId == applicationRoleId select ar).Count() > 0;

            return (from u in _portalContext.Users
                    join ua in _portalContext.UserApplicationRoles on u.UserId equals ua.UserId
                    where ua.ApplicationRoleId == applicationRoleId
                    select ua).Count() > 0;
        }

        //public static Employee FindByEmail(ref PortalContext db, string email)
        //{
        //    IQueryable<Employee> result = (from u in db.Users.OfType<Employee>() where u.Email == email.ToLower() select u);
        //    return result.Any() ? result.First() : new Employee();
        //}

        public static Employee FindByDomainAndSAMAccountName(ref PortalContext db, string domainSAMAccountName)
        {
            string domain = "";
            string samAccountName = domainSAMAccountName;

            if (domainSAMAccountName.Contains("\\"))
            {
                domain = domainSAMAccountName.Substring(0, domainSAMAccountName.LastIndexOf("\\"));
                samAccountName = domainSAMAccountName.Substring(domainSAMAccountName.LastIndexOf("\\") + 1);
            }

            return FindByDomainAndSAMAccountName(ref db, domain, samAccountName);
        }

        public static Employee FindByDomainAndSAMAccountName(ref PortalContext db, string domain, string samAccountName)
        {
            try
            {
                var data = (from u in db.Employees where u.Domain == domain.ToLower() && u.SamaccountName == samAccountName.ToLower() select u);
                //IQueryable<Employee> result = (from u in db.Users.OfType<Employee>() where u.Domain == domain.ToLower() && u.SAMAccountName == samAccountName.ToLower() select u);
                IQueryable<Employee> result = (IQueryable<Employee>)(from u in db.Users
                                                                     join e in db.Employees on u.UserId equals e.UserId
                                                                     where e.Domain == domain.ToLower() && e.SamaccountName == samAccountName.ToLower() select e).ToList();
                return result.Any() ? result.First() : new Employee();
            }
            catch(Exception ex)
            {
                return new Employee();
            }
        }
    }
}
