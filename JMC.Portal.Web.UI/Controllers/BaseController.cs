using Microsoft.AspNetCore.Mvc;
using PortalModel = JMC.Portal.Business.PortalModels;

namespace JMC.Portal.Web.UI.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly PortalModel.PortalContext _intranetContext;
        private readonly PortalModel.PortalContext _portalContext;
        public BaseController(PortalModel.PortalContext intranetContext, PortalModel.PortalContext portalContext)
        {
            _intranetContext = intranetContext;
            _portalContext = portalContext;
        }

        #region Employee Methods
        public bool HasRole(long applicationRoleID)
        {
            return (from ar in _intranetContext.ApplicationRoles where ar.ApplicationRoleId == applicationRoleID select ar).Count() > 0;
        }
        public PortalModel.Employee FindEmployeeByEmail(string email)
        {
            var result = (from u in _portalContext.Users
                                                      join e in _portalContext.Employees on u.UserId equals e.UserId
                                                      where u.Email == email
                                                      select e).FirstOrDefault();

            return result ?? new PortalModel.Employee();
        }

        public PortalModel.Employee FindByDomainAndSAMAccountName(string domainSAMAccountName)
        {
            string domain = "";
            string samAccountName = domainSAMAccountName;

            if (domainSAMAccountName.Contains("\\"))
            {
                domain = domainSAMAccountName.Substring(0, domainSAMAccountName.LastIndexOf("\\"));
                samAccountName = domainSAMAccountName.Substring(domainSAMAccountName.LastIndexOf("\\") + 1);
            }

            return FindByDomainAndSAMAccountName(domain, samAccountName);
        }
        public  PortalModel.Employee FindByDomainAndSAMAccountName(string domain, string samAccountName)
        {
                     var result = (from u in _portalContext.Employees
                         where u.Domain == domain.ToLower() && u.SamaccountName == samAccountName.ToLower()
                         select u).FirstOrDefault();

            return result ?? new PortalModel.Employee();
        }
        #endregion




    }
}
