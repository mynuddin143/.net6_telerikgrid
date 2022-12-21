using JMC.Portal.Business.PortalModels;
using JMC.Portal.Common.MVC.Models;
using JMC.Portal.Web.UI.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JMC.Portal.Web.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PortalContext _portalContext;
        private readonly PortalContext _intranetContext;
        HttpContext _httpcontext;
        public HomeController(PortalContext intranetContext, PortalContext portalContext) : base(intranetContext, portalContext)
        {
            //_logger = logger; 
            _portalContext = portalContext;
            _intranetContext = intranetContext;
            //_httpcontext = httpcontext;
        }

        public IActionResult Index()
        {
            ViewData["SelectedMenu"] = "Home";
            string[] jumbogauges = { "0.750", "0.875" };
            //using (HttpClient client = new HttpClient())
            //{ 

            //}
            List<OpenOrderSummaryModel> lstopenmodel = new List<OpenOrderSummaryModel>();
             OpenOrderSummaryModel openOrderSummaryModel = new OpenOrderSummaryModel(HttpContext);
            openOrderSummaryModel.sapSoldToId = 1;
            openOrderSummaryModel.sapSalesGroupId = 4;
            var user = _portalContext.Users.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name);
            lstopenmodel.Add(openOrderSummaryModel);
            List<OpenOrderItem> oderlist = new List<OpenOrderItem>();
            var proc= _portalContext.GetProcedures();
            
            var backlogItems = proc.GetBacklogAsync(8954, 1049, null, null, null, null, null).Result.ToList();
            //var backlogItems = db.GetBacklog(sapSoldTo.SAPShipToID, null, null, null, null, null, (!(this.user is Employee) ? this.user.UserID : (long?)null)).ToList();
            foreach (var backlogItem in backlogItems)
            {
                OpenOrderItem item = new OpenOrderItem();

                item.SAPSoldToID = backlogItem.SAPSoldToID ?? 0;
                item.SAPShipToID = backlogItem.SAPShipToID ?? 0;
                item.PlantID = backlogItem.PlantID ?? 0;

                item.SoldTo = backlogItem.SAPSoldToNumber.Trim().TrimStart('0').PadLeft(5, '0') + " " + backlogItem.SAPSoldToName.Trim();
                item.ShipTo = backlogItem.SAPShipToNumber.Trim().TrimStart('0').PadLeft(5, '0') + " " + backlogItem.SAPShipToName.Trim();
                item.City = backlogItem.SAPShipToIncoTerms2.Trim();

                item.Plant = backlogItem.PlantName;
                item.NotReady = backlogItem.DisplayNotReadyWeight ?? 0;
                item.Ready = backlogItem.DisplayReadyWeight ?? 0;
                item.Released = backlogItem.DisplayReleasedWeight ?? 0;
                item.BOL = backlogItem.DeliveryWeight ?? 0;
                item.Open = Convert.ToDecimal(item.BOL) + Convert.ToDecimal(item.Ready) + Convert.ToDecimal(item.NotReady) + item.Released;

                oderlist.Add(item);
            }
            // if (user == null)
            //    Employee  user1 = this.FindByDomainAndSAMAccountName(HttpContext.User.Identity.Name);
            // //user = this.FindByDomainAndSAMAccountName(HttpContext.User.Identity.Name);

            //var emp = _portalContext.Employees.ToList();
            //return View(_portalContext.Employees.AsQueryable());
            return View(oderlist.AsQueryable());
        }

        public JsonResult Customers_Read([DataSourceRequest] DataSourceRequest request)
        {
            ViewData["SelectedMenu"] = "Home";
            string[] jumbogauges = { "0.750", "0.875" };
            List<OpenOrderSummaryModel> lstopenmodel = new List<OpenOrderSummaryModel>();
            OpenOrderSummaryModel openOrderSummaryModel = new OpenOrderSummaryModel(HttpContext);
            openOrderSummaryModel.sapSoldToId = 1;
            openOrderSummaryModel.sapSalesGroupId = 4;
            var user = _portalContext.Users.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name);
            lstopenmodel.Add(openOrderSummaryModel);
            List<OpenOrderItem> oderlist = new List<OpenOrderItem>();
            var proc = _portalContext.GetProcedures();

            var backlogItems = proc.GetBacklogAsync(8954, 1049, null, null, null, null, null).Result.ToList();
            //var backlogItems = db.GetBacklog(sapSoldTo.SAPShipToID, null, null, null, null, null, (!(this.user is Employee) ? this.user.UserID : (long?)null)).ToList();
            foreach (var backlogItem in backlogItems)
            {
                OpenOrderItem item = new OpenOrderItem();

                item.SAPSoldToID = backlogItem.SAPSoldToID ?? 0;
                item.SAPShipToID = backlogItem.SAPShipToID ?? 0;
                item.PlantID = backlogItem.PlantID ?? 0;

                item.SoldTo = backlogItem.SAPSoldToNumber.Trim().TrimStart('0').PadLeft(5, '0') + " " + backlogItem.SAPSoldToName.Trim();
                item.ShipTo = backlogItem.SAPShipToNumber.Trim().TrimStart('0').PadLeft(5, '0') + " " + backlogItem.SAPShipToName.Trim();
                item.City = backlogItem.SAPShipToIncoTerms2.Trim();

                item.Plant = backlogItem.PlantName;
                item.NotReady = backlogItem.DisplayNotReadyWeight ?? 0;
                item.Ready = backlogItem.DisplayReadyWeight ?? 0;
                item.Released = backlogItem.DisplayReleasedWeight ?? 0;
                item.BOL = backlogItem.DeliveryWeight ?? 0;
                item.Open = Convert.ToDecimal(item.BOL) + Convert.ToDecimal(item.Ready) + Convert.ToDecimal(item.NotReady) + item.Released;

                oderlist.Add(item);
            }
            var dsResult = oderlist.ToDataSourceResult(request);
            //var result = Enumerable.Range(0, oderlist.Count).Select(i => new OpenOrderItem
            //{
            //    SoldTo = "Company Name " + i,
            //    ContactName = "Contact Name " + i,
            //    ContactTitle = "Contact Title " + i,
            //    Country = "Coutry " + i
            //});

            // if (user == null)
            //    Employee  user1 = this.FindByDomainAndSAMAccountName(HttpContext.User.Identity.Name);
            // //user = this.FindByDomainAndSAMAccountName(HttpContext.User.Identity.Name);

            //var emp = _portalContext.Employees.ToList();
            //return View(_portalContext.Employees.AsQueryable());
            return Json(dsResult);
           

            //var dsResult = result.ToDataSourceResult(request);
            //return Json(dsResult);
        }

        public IActionResult Privacy()
        {
            ViewData["SelectedMenu"] = "Quick Search";
            return View();
        }

        public IActionResult OpenOrders()
        {
            ViewData["SelectedMenu"] = "Open Orders";
            return View();
        }

        public IActionResult Sales()
        {
            ViewData["SelectedMenu"] = "Sales";
            return View();
        }

        public IActionResult Claims()
        {
            ViewData["SelectedMenu"] = "Claims";
            return View();
        }

        public IActionResult AllDeliveries()
        {
            ViewData["SelectedMenu"] = "AllDeliveries";
            return View();
        }

        public IActionResult Admin()
        {
            ViewData["SelectedMenu"] = "Admin";
            return View();
        }

        public IActionResult QualityManagement()
        {
            ViewData["SelectedMenu"] = "QualityManagement";
            return View();
        }

        public IActionResult TraningVideos()
        {
            ViewData["SelectedMenu"] = "TraningVideos";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}