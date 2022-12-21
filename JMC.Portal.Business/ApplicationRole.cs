//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using JMC.Portal.Business;

using JMC.Portal.Business.PortalModels;

namespace JMC.Portal.Business.PortalModels
{
	public partial class ApplicationRole {
		public static IQueryable<ApplicationRole> GetAllActive(ref PortalContext db, long applicationID) {
			return (IQueryable<ApplicationRole>)db.ApplicationRoles.Where(ar => ar.ApplicationId == applicationID);
		}

		public static Dictionary<string, string> GetAllActiveDictionary(ref PortalContext db, long applicationID) {
			return ApplicationRole.GetAllActiveDictionary(ref db, applicationID, false);
		}

		public static Dictionary<string, string> GetAllActiveDictionary(ref PortalContext db, long applicationID, bool addBlankOption) {
			Dictionary<string, string> dictionary = ApplicationRole.GetAllActive(ref db, applicationID).ToDictionary(d => d.ApplicationRoleId.ToString(), d => d.Name);

			if (addBlankOption) {
				dictionary.Add(string.Empty, string.Empty);
			}

			return dictionary;
		}
	}
}
