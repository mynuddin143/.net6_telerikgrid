using System.Collections;
using System.DirectoryServices;
using System.Text;
using System.Configuration;
using DirectoryEntry = System.DirectoryServices.DirectoryEntry;

namespace JMC.Portal.Business.PortalModels
{
    public enum UserType
	{
		Employee,
		Customer
	};
	public partial class User
	{
		public bool IsIsr()
		{
			if (!(this is Employee)) return false;
			if (this.SapsalesGroupUsers.Any()) return true;
			return false;
		}
		public bool IsOsr()
		{
			if (!(this is Employee)) return false;
			//if (this.SapcustomerGroup.Any()) return true;
			if (this.SapcustomerGroupUsers.Any()) return true;
			return false;
		}

		public List<SapsoldTo> GetSapSoldTos()
		{
			List<SapsoldTo> sapSoldTos = new List<SapsoldTo>();//this.SAPSoldToes.ToList();
			sapSoldTos.Add(this.PrimarySapsoldTo);
			if ((this is Employee) && this.IsIsr())
			{
				var salesgroups = this.SapsalesGroupUsers.ToList();
				foreach (var s in salesgroups)
				{
					//sapSoldTos.AddRange(s.SAPSoldTos.ToList());
				}
			}
			if ((this is Employee) && this.IsOsr())
			{
				List<SapcustomerGroup> sapCustomerGroups = this.SapcustomerGroupUsers.ToList();
				//sapCustomerGroups.AddRange((from x in this.SapcustomerGroupUsers select x.SAPCustomerGroup).ToList());
				sapCustomerGroups = sapCustomerGroups.Where(x => x != null).Distinct().ToList();
				foreach (SapcustomerGroup sapCustomerGroup in sapCustomerGroups)
				{
					//sapSoldTos.AddRange(sapCustomerGroup.SAPSoldTos.ToList());
				}
			}
			sapSoldTos = sapSoldTos.Where(x => x != null).Distinct().OrderBy(x => x.PricingNotes).ToList();
			return sapSoldTos;
		}
		public List<SapshipTo> GetSapShipTos()
		{
			List<SapsoldTo> sapSoldTos = this.GetSapSoldTos();
			List<SapshipTo> sapShipTos = new List<SapshipTo>();
			foreach (SapsoldTo sapSoldTo in sapSoldTos)
			{
				//foreach (SapshipTo sapShipTo in sapSoldTo.Sapship)
				//{
				//	sapShipTos.Add(sapShipTo);
				//}
			}
			return sapShipTos;
		}

		public string FullName
		{
			get { return this.FirstName + " " + this.LastName; }
		}

		public static void RefreshFromAtlasActiveDirectory(string email)
		{
			//using (HostingEnvironment.Impersonate()) {
			DirectoryEntry de = new DirectoryEntry("LDAP://" + ConfigurationManager.AppSettings["ATCAD"], ConfigurationManager.AppSettings["ADPortalUser"], ConfigurationManager.AppSettings["ADPortalUserPassword"]);
			PerformActiveDirectoryUpdate(de, "LDAP://" + ConfigurationManager.AppSettings["ATCAD"], "ATLASTUBE", email);
			//}
		}

		public static void RefreshFromWheatlandActiveDirectory(string email)
		{
			//using (HostingEnvironment.Impersonate()) {
			DirectoryEntry de = new DirectoryEntry("LDAP://" + ConfigurationManager.AppSettings["WTCAD"], ConfigurationManager.AppSettings["ADPortalUser"], ConfigurationManager.AppSettings["ADPortalUserPassword"]);
			PerformActiveDirectoryUpdate(de, "LDAP://" + ConfigurationManager.AppSettings["WTCAD"], "JMC", email);
			//}
		}

		private static void PerformActiveDirectoryUpdate(DirectoryEntry de, string hostString, string domain, string email)
		{
			PortalContext db = new PortalContext();

			ArrayList samAccountNames = new ArrayList();

			int insertedCount = 0;
			int updatedCount = 0;
			int disabledCount = 0;
			DateTime startTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			StringBuilder emailStringBuilder = new StringBuilder();

			DirectorySearcher deSearch2 = new DirectorySearcher();
			deSearch2.SearchRoot = de;
			deSearch2.SearchScope = SearchScope.Subtree;
			deSearch2.PageSize = 500;
			//deSearch2.Filter = "(&(objectCategory=person)(objectClass=user)(mail=*)(physicalDeliveryOfficeName=*))";

			if (domain == "ATLASTUBE")
			{
				deSearch2.Filter = "(&(&(objectClass=person)(sn=*)(mail=*)(physicalDeliveryOfficeName=*)(!(objectClass=computer))(!(userAccountControl:1.2.840.113556.1.4.803:=2))))";
			}
			else if (domain == "JMC")
			{
				deSearch2.Filter = "(&(&(objectClass=person)(mail=*)(!(physicalDeliveryOfficeName=WTC Service))(physicalDeliveryOfficeName=*)(!(objectClass=computer))(!(userAccountControl:1.2.840.113556.1.4.803:=2))))";
			}

			SearchResultCollection results2 = deSearch2.FindAll();

			int count = results2.Count;

			List<Location> locations = (from l in JMC.Portal.Business.Location.GetAllActive(ref db) select l).ToList();
			List<Department> departments = (from d in db.Departments select d).ToList();
			List<Employee> employees = (from e in db.Users.OfType<Employee>() select e).ToList();
			List<EmployeePosition> employeePositions = (from ep in db.EmployeePositions select ep).ToList();

			foreach (SearchResult searchResult in results2)
			{
				string samAccountName = string.Empty;
				string mail = string.Empty;
				string sn = string.Empty;
				string givenName = string.Empty;
				string countryCode = string.Empty;
				string company = string.Empty;
				string postalCode = string.Empty;
				string streetAddress = string.Empty;
				string managerEmail = string.Empty;
				string departmentName = string.Empty;
				string title = string.Empty;
				string telephoneNumber = string.Empty;
				string physicalDeliveryOfficeName = string.Empty;

				if (!Object.ReferenceEquals(searchResult, null) && searchResult.Properties.Count > 0)
				{
					samAccountName = searchResult.Properties["samaccountname"].Count > 0 ? searchResult.Properties["samaccountname"][0].ToString().ToLower().Trim() : string.Empty;
					mail = searchResult.Properties["mail"].Count > 0 ? searchResult.Properties["mail"][0].ToString().ToLower().Trim() : string.Empty;
					sn = searchResult.Properties["sn"].Count > 0 ? searchResult.Properties["sn"][0].ToString().Trim() : string.Empty;
					givenName = searchResult.Properties["givenname"].Count > 0 ? searchResult.Properties["givenname"][0].ToString().Trim() : string.Empty;
					countryCode = searchResult.Properties["countryCode"].Count > 0 ? searchResult.Properties["countryCode"][0].ToString().Trim() : string.Empty;
					company = searchResult.Properties["company"].Count > 0 ? searchResult.Properties["company"][0].ToString().Trim() : string.Empty;
					postalCode = searchResult.Properties["postalCode"].Count > 0 ? searchResult.Properties["postalCode"][0].ToString().Trim() : string.Empty;
					streetAddress = searchResult.Properties["streetAddress"].Count > 0 ? searchResult.Properties["streetAddress"][0].ToString().Trim() : string.Empty;
					departmentName = searchResult.Properties["department"].Count > 0 ? searchResult.Properties["department"][0].ToString().Trim() : string.Empty;
					title = searchResult.Properties["title"].Count > 0 ? searchResult.Properties["title"][0].ToString().Trim() : string.Empty;
					telephoneNumber = searchResult.Properties["telephoneNumber"].Count > 0 ? searchResult.Properties["telephoneNumber"][0].ToString().Trim() : string.Empty;
					physicalDeliveryOfficeName = searchResult.Properties["physicalDeliveryOfficeName"].Count > 0 ? searchResult.Properties["physicalDeliveryOfficeName"][0].ToString().Trim() : string.Empty;

					string managerSearchString = searchResult.Properties["manager"].Count > 0 ? searchResult.Properties["manager"][0].ToString().Trim() : string.Empty;

					if (!string.IsNullOrEmpty(managerSearchString))
					{
						System.DirectoryServices.DirectoryEntry managerDE = new System.DirectoryServices.DirectoryEntry(hostString + "/" + managerSearchString, ConfigurationManager.AppSettings["ADPortalUser"], ConfigurationManager.AppSettings["ADPortalUserPassword"]);

						if (managerDE.Properties.Count > 0)
						{
							managerEmail = managerDE.Properties["mail"].Count > 0 ? managerDE.Properties["mail"][0].ToString().Trim() : string.Empty;
						}
					}

					if (!string.IsNullOrEmpty(mail) && !string.IsNullOrEmpty(samAccountName) && !string.IsNullOrEmpty(sn) && !string.IsNullOrEmpty(givenName))
					{
						Employee employee = (from e in employees where e.Domain.ToLower() == domain.ToLower() && e.SamaccountName.ToLower() == samAccountName.ToLower() select e).FirstOrDefault();
						Employee duplicateEmailEmployee = null;
						Location location = (from l in locations where (l.Name.ToLower() == physicalDeliveryOfficeName.ToLower() || l.Adname == physicalDeliveryOfficeName) select l).FirstOrDefault();
						Department department = (from d in departments where (d.Name.ToLower() == departmentName.ToLower() || d.Adname == departmentName) select d).FirstOrDefault();
						EmployeePosition employeePosition = (from ep in employeePositions where ep.Name.ToLower() == title.ToLower() select ep).FirstOrDefault();

						if (employeePosition == null && !string.IsNullOrEmpty(title))
						{
							employeePosition = new EmployeePosition();
							employeePosition.Name = title;
							employeePosition.Active = true;

							employeePositions.Add(employeePosition);
							db.EmployeePositions.Add(employeePosition);
						}

						//if (employee.IsNull()) {
						//  employee = (from e in employees where e.Domain.ToLower() == domain.ToLower() && e.SAMAccountName.ToLower() == samAccountName.ToLower() select e).FirstOrDefault();
						//}

						if (employee == null)
						{
							emailStringBuilder.Append("No Intranet Profile found for:");
							emailStringBuilder.Append(domain + "\\" + samAccountName);

							duplicateEmailEmployee = (from e in employees where e.User.Email.ToLower() == mail.ToLower() select e).FirstOrDefault();

							if (duplicateEmailEmployee == null)
							{
								employee = new Employee();

								if (domain == "ATLASTUBE")
								{
									employee.DivisionId = (long)Enums.Divisions.Atlas;
								}
								else if (domain == "JMC")
								{
									if (mail.ToLower().Contains("@wheatland.com"))
									{
										employee.DivisionId = (long)Enums.Divisions.Wheatland;
									}
									else
									{
										employee.DivisionId = (long)Enums.Divisions.JMC;
									}
								}

								string unencodedPassword = SecurityProvider.GeneratePassword(8);
								employee.User.PasswordSalt = SecurityProvider.GenerateSalt();
								employee.User.Password = SecurityProvider.EncodePassword(unencodedPassword, employee.User.PasswordSalt);
								employee.User.PasswordReset = false;

								employee.User.FirstName = givenName;
								employee.User.LastName = sn;
								//employee.User.SamaccountName = samAccountName.ToLower();
								//employee.User.Domain = domain;
								employee.User.Email = mail;

								//if (givenName.Length > 0) {
								//  employee.UserName = givenName.ToLower() + "." + sn.ToLower();
								//} else {
								//  employee.UserName = sn.ToLower();
								//}

								employees.Add(employee);
								db.Users.Add(employee.User);

								emailStringBuilder.Append("<span style=\"color:Green;\"> - Inserted.</span><br />");

								insertedCount++;
							}
							else
							{
								emailStringBuilder.Append("<span style=\"color:Red;\"> - Duplicate E-mail.</span><br />");
							}
						}
						else
						{
							emailStringBuilder.Append("Found Intranet profile for:");
							emailStringBuilder.Append(domain + "\\" + samAccountName);
							emailStringBuilder.Append("<br />");

							updatedCount++;
						}

						if (duplicateEmailEmployee == null)
						{
							if (domain == "ATLASTUBE")
							{
								employee.DivisionId = (long)Enums.Divisions.Atlas;
							}
							else if (domain == "JMC")
							{
								if (mail.ToLower().Contains("@wheatland.com"))
								{
									employee.DivisionId = (long)Enums.Divisions.Wheatland;
								}
								else
								{
									employee.DivisionId = (long)Enums.Divisions.JMC;
								}
							}

							employee.Location = location;
							employee.Department = department;
							employee.EmployeePosition = employeePosition;
							employee.User.FirstName = givenName;
							employee.User.LastName = sn;
							employee.User.PhoneNumber = telephoneNumber;
							employee.User.Extension = string.Empty;
							employee.User.Active = true;

							samAccountNames.Add(samAccountName);
						}
					}
				}
			}

			db.SaveChanges();

			emailStringBuilder.Append(insertedCount);
			emailStringBuilder.Append(" inserted.<br />");
			emailStringBuilder.Append(updatedCount);
			emailStringBuilder.Append(" updated.<br />");
			emailStringBuilder.Append(disabledCount);
			emailStringBuilder.Append(" disabled.<br />");

			endTime = DateTime.Now;

			TimeSpan runTime = endTime.Subtract(startTime);

			emailStringBuilder.Append("Run Time " + runTime.Days);
			emailStringBuilder.Append("days " + runTime.Hours);
			emailStringBuilder.Append("hours " + runTime.Minutes);
			emailStringBuilder.Append("minutes " + runTime.Seconds);
			emailStringBuilder.Append("seconds<br /><br />");

			//JMC.Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Active Directory Download Results", emailStringBuilder.ToString());
		}

		//foreach (Employee employee in Employee.ActiveList) {
		//  if ((employee.Domain == domain) && !samAccountNames.Contains(employee.SAMAccountName.ToLower())) {
		//    emailStringBuilder.Append("No active AD account found for Intranet Profile:");
		//    emailStringBuilder.Append(employee.Domain + "\\" + employee.SAMAccountName);

		//    if (employee.Email.ToLower().Contains("@jmcsteel.com") || employee.Email.ToLower().Contains("@johnmaneely.com") || employee.Email.ToLower().Contains("@atlastube.com") || employee.Email.ToLower().Contains("@wheatland.com") || employee.Email.ToLower().Contains("@seminole.com")) {
		//      emailStringBuilder.Append(", disabling.");

		//      employee.Active = false;

		//      try {
		//        employee.Save();
		//        disabledCount++;
		//        emailStringBuilder.Append("<span style=\"color:Green;\"> - Disabled.</span>");
		//      } catch (Exception ex) {
		//        emailStringBuilder.Append("<span style=\"color:Red;\"> - Error ");
		//        emailStringBuilder.Append(ex.Message);
		//        emailStringBuilder.Append("</span>");
		//      }
		//    }

		//    emailStringBuilder.Append("<br />");
		//  }
		//}
	}
}
