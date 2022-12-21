using JMC.Portal.Business.PortalModels;
using JMC.Portal.Web.UI.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JMC.Portal.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly PortalContext _intranetContext;
        private readonly PortalContext _portalContext;
        const string SessionUserName = "UserName";
        const string SessionUserId = "UserId";
        const string SessionUserFullName = "UserFullName";
        public AccountController(PortalContext intranetContext, PortalContext  portalcontext)
        {
            _intranetContext = intranetContext;
            _portalContext = portalcontext;
        }
        public IActionResult LogOn()
        {
            //var users = _intranetContext.Users.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult LogOn(string userName, string passWord, string returnUrl)
        {
            SecurityProvider.LogInResults validUser = Validate(userName, passWord);

            //Hard coding as sucess for now
            switch (SecurityProvider.LogInResults.Success)
            {
                case SecurityProvider.LogInResults.ApplicationNotAuthorized:
                    ModelState.AddModelError(string.Empty, "User '" + userName + "' does not have access to this application.");
                    break;

                case SecurityProvider.LogInResults.UserNameNotFound:
                    ModelState.AddModelError(string.Empty, "User name '" + userName + "' not found.");
                    break;

                case SecurityProvider.LogInResults.AccountNotActive:
                    ModelState.AddModelError(string.Empty, "Account for user '" + userName + "' is not active.");
                    break;

                case SecurityProvider.LogInResults.IncorrectPassword:
                    ModelState.AddModelError(string.Empty, "Incorrect password for user '" + userName + "'.");
                    break;

                case SecurityProvider.LogInResults.Success:

                    
                        return RedirectToAction("Index","Home", new { ReturnUrl = returnUrl });
                   
            }

            return View();
        }


        private SecurityProvider.LogInResults Validate(string userName, string passWord)
        {
            try
            {
                LoginHistory loginHistory = new LoginHistory();

                string domain = "";
                var logInResults = JMC.Portal.Business.SecurityProvider.ValidateUser(ref domain, userName, passWord, loginHistory);
                var userList = _portalContext.Users.Where(x => x.UserName == userName).ToList();
                if (userList.Any())
                {
                    if (userList[0].Active.Value)
                    {
                        string unencodedPassword = SecurityProvider.GeneratePassword(8);
                        string PasswordSalt = SecurityProvider.GenerateSalt();
                        string Password = SecurityProvider.EncodePassword(unencodedPassword, PasswordSalt);
                        if (SecurityProvider.CheckPassword(passWord, userList[0].Password, userList[0].PasswordSalt))
                        {
                            var applicationRole = (from ar in _intranetContext.ApplicationRoles
                                                   join uar in _intranetContext.UserApplicationRoles on ar.ApplicationRoleId equals uar.ApplicationRoleId
                                                   where ar.ApplicationId == (int)Enums.Applications.Portal && uar.UserId == userList[0].UserId
                                                   select ar.Name).ToList();

                            string fullName = userList[0].LastName + ", " + userList[0].FirstName;
                            var claims = new List<Claim>();
                            claims.Add(new Claim("userID", userList[0].UserId.ToString()));
                            claims.Add(new Claim("userId", userList[0].UserName));
                            claims.Add(new Claim(ClaimTypes.NameIdentifier, userList[0].UserName));
                            claims.Add(new Claim(ClaimTypes.Name, fullName));

                            foreach (var role in applicationRole)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role));
                            }

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            HttpContext.SignInAsync(claimsPrincipal);


                            HttpContext.Session.SetInt32(SessionUserId, Convert.ToInt32(userList[0].UserId));



                            return SecurityProvider.LogInResults.Success;
                        }
                        else
                        {
                            return SecurityProvider.LogInResults.IncorrectPassword;
                        }
                    }
                    else
                    {
                        return SecurityProvider.LogInResults.AccountNotActive;
                    }
                }
                else
                {
                    return SecurityProvider.LogInResults.UserNameNotFound;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", ex.Message);
                return SecurityProvider.LogInResults.AccountNotActive;
            }
        }

    }
}
//Both relationships between 'SapsalesOrder.SapshipTo' and 'SapshipTo.SapsalesOrders' and between 'SapsalesOrder.SapsoldTo' and 'SapsoldTo' could use {'SapsoldToId'}
//    as the foreign key.To resolve this, configure the foreign key properties explicitly in 'OnModelCreating' on at least one of the relationships.
//A key cannot be configured on 'SapsoldTo' because it is a derived type. The key must be configured on the root type 'SapshipTo'.
//If you did not intend for 'SapshipTo' to be included in the model, ensure that it is not referenced 
//    by a DbSet property on your context, referenced in a configuration call to ModelBuilder, or referenced from a navigation on a type that is included in the model.
//Cannot create a relationship between 'SapsoldTo.SapsalesOrders' and 'SapsalesOrder.SapsoldTo' because a relationship already exists between 'SapshipTo.SapsalesOrders' and 'SapsalesOrder.SapshipTo'. Navigations can only 
//    participate in a single relationship. If you want to override an existing relationship call 'Ignore' on the navigation 'SapsalesOrder.SapsoldTo' first in 'OnModelCreating'.