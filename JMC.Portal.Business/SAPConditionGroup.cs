using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JMC.Portal.Business {
	public partial class SAPConditionGroup {
		public static IEnumerable<SAPConditionGroup> GetAllRegions(ref PortalEntities db) {
			return GetAllRegions(ref db, null);
		}

		public static IEnumerable<SAPConditionGroup> GetAllRegions(ref PortalEntities db, long? divisionID) {
			if (divisionID > 0) {
				return (from cg in db.SapconditionGroups where cg.DivisionID == divisionID select cg).ToList().Where(cg => cg.Sapcode.ToInt() > 0);
			}

			return (from cg in db.SapconditionGroups select cg).ToList().Where(cg => cg.Sapcode.ToInt() > 0);
		}

		public static Dictionary<string, string> GetAllRegionsDictionary(ref PortalEntities db, bool addBlankOption, long? divisionID) {
			Dictionary<string, string> dictionary = GetAllRegions(ref db, divisionID).ToDictionary(cg => cg.SapconditionGroupID.ToString(), cg => cg.Name);

			if (addBlankOption) {
				dictionary.Add(string.Empty, string.Empty);
			}

			return dictionary;
		}

		public static IQueryable<SAPConditionGroup> GetAllTiers(ref PortalEntities db) {
			return GetAllTiers(ref db, null);
		}

		public static IQueryable<SAPConditionGroup> GetAllTiers(ref PortalEntities db, long? divisionID) {
			string[] tiersList = ConfigurationManager.AppSettings["Tiers"].Split(',');
			if (divisionID > 0) {
				return (from cg in db.SapconditionGroups where cg.DivisionID == divisionID && (tiersList.Contains(cg.Sapcode)) select cg);
			}

			return (from cg in db.SapconditionGroups where (tiersList.Contains(cg.Sapcode)) select cg);
		}

		public static Dictionary<string, string> GetAllTiersDictionary(ref PortalEntities db, bool addBlankOption, long? divisionID) {
			Dictionary<string, string> dictionary = GetAllTiers(ref db, divisionID).ToDictionary(cg => cg.SapconditionGroupID.ToString(), cg => cg.Name);

			if (addBlankOption) {
				dictionary.Add(string.Empty, string.Empty);
			}

			return dictionary;
		}

		public static IQueryable<SAPConditionGroup> GetAllFreightIndicators(ref PortalEntities db) {
			return GetAllFreightIndicators(ref db, null);
		}

		public static IQueryable<SAPConditionGroup> GetAllFreightIndicators(ref PortalEntities db, long? divisionID) {
			if (divisionID > 0) {
				return (from cg in db.SapconditionGroups where cg.DivisionID == divisionID && (cg.Sapcode == "TR" || cg.Sapcode == "RL") select cg);
			}

			return (from cg in db.SapconditionGroups where (cg.Sapcode == "TR" || cg.Sapcode == "RL") select cg);
		}

		public static Dictionary<string, string> GetAllFreightIndicatorsDictionary(ref PortalEntities db, bool addBlankOption, long? divisionID) {
			Dictionary<string, string> dictionary = GetAllFreightIndicators(ref db, divisionID).ToDictionary(cg => cg.SapconditionGroupID.ToString(), cg => cg.Name);

			if (addBlankOption) {
				dictionary.Add(string.Empty, string.Empty);
			}

			return dictionary;
		}
	}
}
