using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JMC.Portal.Web.UI.Helpers;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace JMC.Portal.Web.UI.Controllers
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly MenuHelper _cbaMenuHelper = new MenuHelper();

        public async Task<IViewComponentResult> InvokeAsync(int testId)
        {
            //testId is optional, and can be passed in when you call InvokeAsync
            //Parse current URL to determine which menu item to highlight:
            string baseUrl = Request.Scheme + "://" + Request.Host.Value;
            var httpRequestFeature = Request.HttpContext.Features.Get<IHttpRequestFeature>();
            var uri = httpRequestFeature.RawTarget; /*+ Request.RouteValues["controller"] + httpRequestFeature.RawTarget + Request.RouteValues["action"];*/
            var id = HttpContext.Request.Query["id"].ToString();  //Parse Query String
            var menuItems = await _cbaMenuHelper.GetAllMenuItems(baseUrl + uri, id); //Can pass in testId here if required.
            return View("_MenuPartial", menuItems);
        }
    }
}
