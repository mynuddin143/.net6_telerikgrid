using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.HSSPortalSales;
using JMC.Portal.Business.PortalModels;
using JMC.Portal.Business.WheatlandPortal;

namespace JMC.Portal.Business.DataModel {
	public static partial class ViewModelExtensions {

		public static string ToOption(this Tuple<int, string> tuple, bool addToCart = false) {
			StringBuilder returnValue = new StringBuilder(string.Empty);
			returnValue.AppendLine("<option " + ((addToCart) ? "class=\alreadyInCart\"" : "") + " value=\"" + tuple.Item1 + "\">");
			returnValue.AppendLine(tuple.Item2);
			returnValue.AppendLine("</option>");
			return returnValue.ToString();
		}

		//public static IQueryable<BOLViewModel> ToModel(this IEnumerable<SAPDeliveryItem> SAPDeliveryItemQuery) {
		//  return (from x in SAPDeliveryItemQuery.Select(x => x.SAPDelivery).Distinct() select new BOLViewModel() {
		//    SAPDeliveryID = x.SAPDeliveryID,
		//    Number = x.Number
		//  }).AsQueryable();
		//}

		public static IQueryable<EmployeeViewModel> ToModel(this IQueryable<Employee> employeesQuery) {
			return (from x in employeesQuery
							select new EmployeeViewModel() {
								EmployeeID = x.UserId,
								FirstName = x.FirstName,
								LastName = x.LastName,
								Fax = x.FaxNumber,
								Domain = x.Domain,
								Phone = x.PhoneNumber,
								Division = x.Division.Name,
								Department = x.Department.Name,
								EmployeePosition = x.EmployeePosition.Name
							});
		}

		public static IQueryable<UserViewModel> ToModel(this IQueryable<User> usersQuery) {
			return (from x in usersQuery
							select new UserViewModel() {
								UserID = x.UserId,
								UserName = x.UserName,
								FirstName = x.FirstName,
								LastName = x.LastName,
								Fax = x.FaxNumber,
								Phone = x.PhoneNumber,
								Primary = x.SapsoldTo.Number + " - " + x.SapsoldTo.Name
							});
		}

		public static IQueryable<SAPSoldToViewModel> ToModel(this IQueryable<SapsoldTo> query) {
			return (from x in query
							select new SAPSoldToViewModel() {
								SAPShipToID = x.SapshipToId,
								Number = x.Number,
								Name = x.Name,
								Phone = x.Phone,
								City = x.City.Name,
								StateAbbreviation = x.City.State.Abbreviation,
								CountryAbbreviation = x.City.State.Country.Abbreviation,
								Address = x.Address,
								IncoTerms2 = x.IncoTerms2
							});
		}
	}
}

