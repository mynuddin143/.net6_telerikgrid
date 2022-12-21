using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using JMC.Portal.Business.AtlasMasterData;
using System.Configuration;
using JMC.Portal.Business.WheatlandPortal;
using System.Net;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
//using System.Data.Objects.DataClasses;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business {
	public partial class SAPSalesGroup {
		public IEnumerable<SAPShipToSAPSalesOrganization> SAPSalesOrganizationSAPSoldTos {
			get { return (from stso in this.SapshipToSapsalesOrganizations where stso.SapshipTo is SAPSoldTo select stso); }
		}

		public static IEnumerable<SAPSalesGroup> GetAll(ref PortalEntities db) {
			return GetAll(ref db, null);
		}

		public static IEnumerable<SAPSalesGroup> GetAll(ref PortalEntities db, long? divisionID) {
			if (divisionID > 0) {
				return (from sg in db.SapsalesGroups where sg.DivisionID == divisionID select sg);
			}

			return (from sg in db.SapsalesGroups select sg);
		}

		public static Dictionary<string, string> GetAllDictionary(ref PortalEntities db, bool addBlankOption, long? divisionID) {
			Dictionary<string, string> dictionary = GetAll(ref db, divisionID).OrderBy(sg => sg.Name).ToDictionary(cg => cg.SapsalesGroupID.ToString(), cg => cg.Name);

			if (addBlankOption) {
				dictionary.Add(string.Empty, string.Empty);
			}

			return dictionary;
		}
	}
}
