using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
//using AtlasMasterData;
using JMC.Portal.Business.AtlasMasterData;
using System.Configuration;
//using WheatlandPortal;
using JMC.Portal.Business.WheatlandPortal;
using System.Net;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
//using System.Data.Objects.DataClasses;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business {
	public partial class SAPCustomerGroup {
		public string AdditionalUserString {
			get {
				return string.Join(", ", (from cgu in this.SapcustomerGroupUsers select cgu.User.FullName).ToArray<string>());
			}
		}

		public IEnumerable<SAPShipToSAPSalesOrganization> SAPSalesOrganizationSAPSoldTos {
			get { return (from stso in this.SapshipToSapsalesOrganizations where stso.SapshipTo is SAPSoldTo select stso); }
		}

		public static IEnumerable<SAPCustomerGroup> GetAll(ref PortalEntities db) {
			return GetAll(ref db, null);
		}

		public static IEnumerable<SAPCustomerGroup> GetAll(ref PortalEntities db, long? divisionID) {
			if (divisionID > 0) {
				return (from cg in db.SapcustomerGroups where cg.DivisionID == divisionID select cg);
			}

			return (from cg in db.SapcustomerGroups select cg);
		}

		public static Dictionary<string, string> GetAllDictionary(ref PortalEntities db, bool addBlankOption, long? divisionID) {
			Dictionary<string, string> dictionary = GetAll(ref db, divisionID).OrderBy(cg => cg.Name).ToDictionary(cg => cg.SapcustomerGroupID.ToString(), cg => cg.Name);

			if (addBlankOption) {
				dictionary.Add(string.Empty, string.Empty);
			}

			return dictionary;
		}
	}
}
