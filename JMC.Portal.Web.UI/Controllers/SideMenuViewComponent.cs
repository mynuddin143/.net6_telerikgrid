using JMC.Portal.Web.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMC.Portal.Web.UI.Controllers
{
    public class SideMenuViewComponent : ViewComponent
    {
        private readonly MenuHelper _sideMenuHelper = new MenuHelper();

        public async Task<IViewComponentResult> InvokeAsync(string mainMenu)
        {           
            var menuItems = await _sideMenuHelper.GetSideMenuItems(mainMenu); //Can pass in testId here if required.
            return View("_SideMenuPartial", _sideMenuHelper.GetMenu(menuItems, null));
        }
    }
}
