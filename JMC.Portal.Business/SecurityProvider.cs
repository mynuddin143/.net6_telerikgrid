using JMC.Portal.Business.PortalModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace JMC.Portal.Business
{
	public class SecurityProvider
	{
		#region Enums
		public static Employee loggedinUser;
		public static Employee EmpLoggedIn;
		public enum LogInResults
		{
			InvalidADCredentials = 1,
			PortalProfileNotFound,
			ApplicationNotAuthorized,
			ADNotAvailable,
			Success,
			CustomerSuccess,
			PasswordReset,
			PasswordNotMatch
		}

        #endregion


        #region Static Methods

        public static LogInResults ValidateUser(ref string domain, string username, string password, LoginHistory loginHistory)
        {
            bool authenticatedJMC = false;
            bool authenticatedWheatland = false;
            bool authenticatedAtlas = false;

            try
            {
                var bld = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: false);
                var cfg = bld.Build();
                //Setting_URL = cfg.GetSection("SettingsURL").Value;
                authenticatedJMC = SecurityProvider.Authenticate(username, password, cfg.GetSection("JMCAD").Value);// ConfigurationManager.AppSettings["JMCAD"]);

                if (authenticatedJMC)
                {
                    authenticatedAtlas = SecurityProvider.Authenticate(username, password, cfg.GetSection("ATCAD").Value);//ConfigurationManager.AppSettings["ATCAD"]);
                    authenticatedWheatland = SecurityProvider.Authenticate(username, password, cfg.GetSection("WTCAD").Value);//ConfigurationManager.AppSettings["WTCAD"]);
                }
            }
            catch (Exception ex)
            {
                return LogInResults.ADNotAvailable;
            }

            if (authenticatedJMC || authenticatedWheatland || authenticatedAtlas)
            {
                PortalContext db = new PortalContext();

                //List<Employee> employees = (from u in db.Users.OfType<Employee>() select u).ToList();

                //Employee employee = null;

                List<Employee> employees = (from u in db.Users
                                            join e in db.Employees on u.UserId equals e.UserId
                                            select u.Employee).ToList();

                string employeeDomain = String.Empty;

                if (authenticatedJMC)
                {
                    domain = "JMCSTEEL";
                    employeeDomain = domain;

                    //employee = (from u in employees where u.User.Active.Value && u.SamaccountName == username && u.Domain == employeeDomain select u).FirstOrDefault();
                    //employee = (from u in db.Employees
                    //            join e in db.Users on u.UserId equals e.UserId
                    //            where e.Active.Value && u.SamaccountName == username && u.Domain == employeeDomain
                    //            select u).FirstOrDefault();
                }

                if (employees == null && authenticatedWheatland)
                {
                    domain = "JMC";
                    employeeDomain = domain;

                    //employee = (from u in employees where u.User.Active.Value && u.SamaccountName == username && u.Domain == employeeDomain select u).FirstOrDefault();
                    //employee = (from u in db.Employees
                    //            join e in db.Users on u.UserId equals e.UserId
                    //            where e.Active.Value && u.SamaccountName == username && u.Domain == employeeDomain
                    //            select u).FirstOrDefault();
                }

                if (employees == null && authenticatedAtlas)
                {
                    domain = "ATLASTUBE";
                    employeeDomain = domain;

                    //employee = (from u in employees where u.User.Active.Value && u.SamaccountName == username && u.Domain == employeeDomain select u).FirstOrDefault();
                    //employee = (from u in db.Employees
                    //            join e in db.Users on u.UserId equals e.UserId
                    //            where e.Active.Value && u.SamaccountName == username && u.Domain == employeeDomain
                    //            select u).FirstOrDefault();
                }

                Employee employee = (from u in db.Employees
                                     join e in db.Users on u.UserId equals e.UserId
                                     where e.Active.Value && u.SamaccountName == username && u.Domain == employeeDomain
                                     select u).FirstOrDefault();

                if (employee != null && employee.UserId > 0)
                {
                    IEnumerable<ApplicationRole> applicationRoles;

                    if ((employee.DivisionId == (long)Enums.Divisions.Wheatland) && (employee.HasRole((long)Enums.ApplicationRoles.WTCPipeEcommerce, db) || (employee.HasRole((long)Enums.ApplicationRoles.WTCMTRSearch, db))))
                    {
                        applicationRoles = (from ar in employee.ApplicationRoles where ar.ApplicationId == (long)Enums.Applications.WTCPipeportal select ar);
                    }
                    else
                    {
                        applicationRoles = (from ar in employee.ApplicationRoles where ar.ApplicationId == (long)Enums.Applications.Portal select ar);
                    }
                    if (applicationRoles.Count() > 0)
                    {
                        loginHistory.LoginDate = DateTime.Now; 
                        loginHistory.UserId = employee.UserId; 
                        db.LoginHistories.Add(loginHistory);

                        db.SaveChanges(); 
                        loggedinUser = employee; 
                        EmpLoggedIn = employee; 
                        return LogInResults.Success;



                        //employee.LoginHistories.Add(loginHistory);

                        //employee.LastLoginDate = DateTime.Now;

                        //db.SaveChanges();
                        //loggedinUser = employee;
                        //EmpLoggedIn = employee;
                        //return LogInResults.Success;
                    }

                    return LogInResults.ApplicationNotAuthorized;

                }

                return LogInResults.PortalProfileNotFound;
            }
            else
            {
                return LogInResults.InvalidADCredentials;
            }
        }

        //public static LogInResults ATCCustomerLogin(ref string domain, string username, string password, long divisionID, LoginHistory loginHistory)
        //{
        //	PortalContext db = new PortalContext();
        //	User user = db.Users.Where(x => !(x is Employee)).FirstOrDefault(x => x.UserName == username);
        //	if (user.SAPSoldTo.DivisionId != divisionID)
        //	{
        //		return LogInResults.ApplicationNotAuthorized;
        //	}
        //	if (user != null)
        //	{
        //		if (CheckPassword(password, user.Password, user.PasswordSalt))
        //		{
        //			domain = "Customer";
        //			user.LastLoginDate = DateTime.Now;

        //			//LoginHistory loginHistory = new LoginHistory();
        //			//loginHistory.LoginDate = user.LastLoginDate.ToDate();
        //			user.LoginHistories.Add(loginHistory);

        //			db.SaveChanges();

        //			if (user.PasswordReset)
        //			{
        //				return LogInResults.PasswordReset;
        //			}

        //			return LogInResults.CustomerSuccess;
        //		}
        //	}

        //	return LogInResults.ApplicationNotAuthorized;
        //}

        public static bool Authenticate(string userName, string password, string domain)
		{
			//LoginHistory loginHistory = new LoginHistory();
			//var logInResults = ValidateUser(ref domain, userName, password, loginHistory);
			bool authentic = false;

			try
			{
				DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain, userName, password);
				object nativeObject = entry.NativeObject;
				authentic = true;
			}
			catch (DirectoryServicesCOMException) { }

			return authentic;
		}

		public static bool CheckPassword(string password, string encodedPassword, string salt)
		{
			password = password.Trim();
			string checkPassword = EncodePassword(password, salt);
			return encodedPassword.Equals(checkPassword);
		}

		public static string EncodePassword(string pass, string salt)
		{
			byte[] src = Encoding.Unicode.GetBytes(pass);
			byte[] buffer2 = Convert.FromBase64String(salt);
			byte[] dst = new byte[buffer2.Length + src.Length];
			byte[] inArray;
			Buffer.BlockCopy(buffer2, 0, dst, 0, buffer2.Length);
			Buffer.BlockCopy(src, 0, dst, buffer2.Length, src.Length);
			HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
			inArray = algorithm.ComputeHash(dst);
			return Convert.ToBase64String(inArray);
		}

		public static string GeneratePassword(int length)
		{
			byte[] data = new byte[length * 2];
			char[] chArray = new char[length];
			int index = 0;
			new RNGCryptoServiceProvider().GetBytes(data);
			for (int i = 0; i < length * 2; i++)
			{
				int charIndex = data[i] % 0x57;
				if (charIndex < 10)
				{
					// numbers
					chArray[index] = (char)(0x30 + charIndex);
					index++;
				}
				else if (charIndex < 0x24)
				{
					// uppercase 
					chArray[index] = (char)((0x41 + charIndex) - 10);
					index++;
				}
				else if (charIndex < 0x3e)
				{
					// lowercase
					chArray[index] = (char)((0x61 + charIndex) - 0x24);
					index++;
				}
				if (index >= length)
				{
					break;
				}
			}
			return new string(chArray);
		}

		public static string GenerateSalt()
		{
			byte[] data = new byte[0x10];
			new RNGCryptoServiceProvider().GetBytes(data);
			return Convert.ToBase64String(data);
		}

		public static LoginHistory GenerateLoginHistory(HttpRequest httpRequest)
		{
			LoginHistory loginHistory = new LoginHistory();
			loginHistory.LoginDate = DateTime.Now;
			loginHistory.UserAgent = httpRequest.Headers["UserAgent"];

			//if (httpRequest.Browser.IsMobileDevice)
			//{
			//	loginHistory.UserAgent += "; Mobile";
			//}

			//loginHistory.BrowserName = httpRequest.Browser.Browser;
			//loginHistory.BrowserVersion = httpRequest.Browser.Version;
			//loginHistory.Javascript = httpRequest.Browser.EcmaScriptVersion.Major >= 1;
			//loginHistory.Cookies = httpRequest.Browser.Cookies;
			//loginHistory.Platform = httpRequest.Browser.Platform;
			//loginHistory.IpAddress = httpRequest.UserHostAddress;
			//loginHistory.HostName = httpRequest.UserHostName;

			return loginHistory;
		}

		public static void SendCustomerAccountCreatedEmail(string username, string password, string emailAddress, long divisionID)
		{
			string subject = "";
			string body = @"
      <span>Your Web User Account has been activated for you with the following login information:</span><br />
      <br />
      <ul>
        <li>User Name: " + username + @"</li>
        <li>Password: " + password + @"</li>
      </ul>
      <br />";
			if (divisionID == (long)Enums.Divisions.Atlas)
			{
				subject = "Atlas Tube User login info";
				body = body + " <span>To log in, <a href='https://customer.atlastube.com'>click here</a> or go to <a href='https://customer.atlastube.com'>https://customer.atlastube.com</a> and enter your User Name and Password.</span>";
			}
			if (divisionID == (long)Enums.Divisions.Wheatland)
			{
				subject = "Wheatland Tube User login info";
				body = body + " <span>To log in, <a href='https://order.wheatland.com'>click here</a> or go to <a href='https://order.wheatland.com'>https://order.wheatland.com</a> and enter your User Name and Password.</span>";
			}
			//Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], ConfigurationManager.AppSettings["FromEmailAddress"], emailAddress, string.Empty, subject, body);
		}

		public static void SendIntranetAccountCreatedEmail(string username, string password, string emailAddress)
		{
			string body = @"
      <span>An Intranet Account has been activated for you with the following information:</span><br />
      <br />
      <ul>
        <li>User Name: " + username + @"</li>
        <li>Password: " + password + @"</li>
      </ul>";

			//Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], ConfigurationManager.AppSettings["FromEmailAddress"], emailAddress, string.Empty, "User Account", body);
		}
		#endregion
	}
}
