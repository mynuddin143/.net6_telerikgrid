using JMC.Portal.Web.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMC.Portal.Web.UI.Helpers
{
    public class MenuHelper
    {
        public async Task<IList<Menu>> GetAllMenuItems(string route, string id)          
        {

            var routeparts = new Uri(route);
            var action = "";
            var controller = "";


            if (routeparts.Segments.Count() == 1)
                {
                controller = "Home";

                action = "Index";
            }
            else {

                controller = (routeparts.Segments[1].LastIndexOf('/') > -1) ? routeparts.Segments[1].Substring(0, routeparts.Segments[1].LastIndexOf('/')) : routeparts.Segments[1];

                action = (routeparts.Segments[2].LastIndexOf('/') > -1) ? routeparts.Segments[2].Substring(0, routeparts.Segments[2].LastIndexOf('/')) : routeparts.Segments[2];

            }

            

            var menu = new List<Menu>();
           
            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = null,
                Content = "Home / Product inquiry",
                Order = 0, // i++,
                IconClass = "fa fa-fw fa-user",
                SelectedClass = (controller == "Home" && action == "Index") ? "active" : null,
                Url = "JavaScript:callHome()", //+ id// cbaId
                Controller = "Home",
                Action ="Index"
            });


            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = null,
                Content = "Quick Search",
                Order = 0, // i++,
                IconClass = "fa fa-fw fa-user",
                SelectedClass = (controller == "Home" && action == "Privacy") ? "active" : null,
                Url = "JavaScript:callHome()", //+ id// cbaId
                Controller = "Home",
                Action = "Privacy"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = null,
                Content = "Open Orders",
                Order = 0, // i++,
                IconClass = "fa fa-fw fa-user",
                SelectedClass = (controller == "Home" && action == "OpenOrders") ? "active" : null,
                Url = "JavaScript:callHome()", //+ id// cbaId
                Controller = "Home",
                Action = "OpenOrders"
            });


            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = null,
                Content = "Sales",
                Order = 0, // i++,
                IconClass = "fa fa-fw fa-user",
                SelectedClass = (controller == "Home" && action == "Sales") ? "active" : null,
                Url = "JavaScript:callHome()", //+ id// cbaId
                Controller = "Home",
                Action = "Sales"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = null,
                Content = "Claims",
                Order = 0, // i++,
                IconClass = "fa fa-fw fa-user",
                SelectedClass = (controller == "Home" && action == "Claims") ? "active" : null,
                Url = "JavaScript:callHome()", //+ id// cbaId
                Controller = "Home",
                Action = "Claims"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = null,
                Content = "All Deliveries",
                Order = 0, // i++,
                IconClass = "fa fa-fw fa-user",
                SelectedClass = (controller == "Home" && action == "AllDeliveries") ? "active" : null,
                Url = "JavaScript:callHome()", //+ id// cbaId
                Controller = "Home",
                Action = "AllDeliveries"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = null,
                Content = "Admin",
                Order = 0, // i++,
                IconClass = "fa fa-fw fa-user",
                SelectedClass = (controller == "Home" && action == "Admin") ? "active" : null,
                Url = "JavaScript:callHome()", //+ id// cbaId
                Controller = "Home",
                Action = "Admin"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = null,
                Content = "Quality Management",
                Order = 0, // i++,
                IconClass = "fa fa-fw fa-user",
                SelectedClass = (controller == "Home" && action == "QualityManagement") ? "active" : null,
                Url = "JavaScript:callHome()", //+ id// cbaId
                Controller = "Home",
                Action = "QualityManagement"
            });

            
            return  menu;
        }

        public async Task<IList<Menu>> GetSideMenuItems(string mainMenu)
        {
            if (string.IsNullOrEmpty(mainMenu))
                return null;

            var menu = new List<Menu>();

            switch (mainMenu)
            {
                case "Claims":
                    GetClaimsMenu("", menu);
                    break;
                case "Admin":
                    GetAdminMenu("",menu);
                    break;
                case "QualityManagement":
                    GetQualityManagementMenu("", menu);
                    break;

                case "Home":
                    break;

                case "Quick Search":
                    break;

                default:
                    GetPricingMenu("", menu);
                    break;
            }

            



            //menu.Add(new Menu
            //{
            //    ID = Guid.NewGuid(),
            //    ParentID = null,
            //    Content = "one",
            //    Order = 0, // i++,
            //    IconClass = "fa fa-fw fa-user",
            //    SelectedClass =  null,
            //    Url = "JavaScript:callHome()"
            //});


            //menu.Add(new Menu
            //{
            //    ID = Guid.NewGuid(),
            //    ParentID = null,
            //    Content = "two",
            //    Order = 0, // i++,
            //    IconClass = "fa fa-fw fa-user",
            //    SelectedClass = null,
            //    Url = "JavaScript:callHome()"
            //});

            //menu.Add(new Menu
            //{
            //    ID = Guid.NewGuid(),
            //    ParentID = null,
            //    Content = "Open Orders",
            //    Order = 0, // i++,
            //    IconClass = "fa fa-fw fa-user",
            //    SelectedClass =  null,
            //    Url = "JavaScript:callHome()"
            //});

            return menu;
        }


        private static void GetPricingMenu(string routetype, List<Menu> menu)
        {
            var pricingForm = Guid.NewGuid();
            menu.Add(new Menu
            {
                ID = pricingForm,
                ParentID = null,
                Content = "Pricing",
                Order = 1,
                IconClass = "fa fa-fw fa-folder-open",
                Url = "#"
            });
            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = pricingForm,
                Content = "Customer",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = pricingForm,
                Content = "Region / Tier",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });


            // ATC Customer Portal

            var atcCustomerportalForm = Guid.NewGuid();
            menu.Add(new Menu
            {
                ID = atcCustomerportalForm,
                ParentID = null,
                Content = "ATC Customer Portal",
                Order = 1,
                IconClass = "fa fa-fw fa-folder-open",
                Url = "#"
            });
            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = atcCustomerportalForm,
                Content = "Open Order Status",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = atcCustomerportalForm,
                Content = "Released",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = atcCustomerportalForm,
                Content = "Open Deliveries",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = atcCustomerportalForm,
                Content = "Users",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });


            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = atcCustomerportalForm,
                Content = "Data Sheets",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = atcCustomerportalForm,
                Content = "Backup ISR",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            // Reports

            var reportsForm = Guid.NewGuid();
            menu.Add(new Menu
            {
                ID = reportsForm,
                ParentID = null,
                Content = "Reports",
                Order = 1,
                IconClass = "fa fa-fw fa-folder-open",
                Url = "#"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "Customers",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "Price Change History",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "Backlog",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "Order Search",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "Release History",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "Delivery Search",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "Mill Status",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });


            //Freight Rates

            var freightRatesForm = Guid.NewGuid();
            menu.Add(new Menu
            {
                ID = freightRatesForm,
                ParentID = null,
                Content = "Freight Rates",
                Order = 1,
                IconClass = "fa fa-fw fa-folder-open",
                Url = "#"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = freightRatesForm,
                Content = "SAP Rates",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });
            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = freightRatesForm,
                Content = "TMS Plus",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

        }

        private static void GetClaimsMenu(string routetype, List<Menu> menu)
        {
            var claimsForm = Guid.NewGuid();
            menu.Add(new Menu
            {
                ID = claimsForm,
                ParentID = null,
                Content = "Claims",
                Order = 1,
                IconClass = "fa fa-fw fa-folder-open",
                Url = "#"
            });
            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = claimsForm,
                Content = "Dashboard",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = claimsForm,
                Content = "Submit Claim",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = claimsForm,
                Content = "Claim History",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });


        }

        private static void GetAdminMenu(string routetype, List<Menu> menu)
        {
            var configForm = Guid.NewGuid();
            menu.Add(new Menu
            {
                ID = configForm,
                ParentID = null,
                Content = "Configuration",
                Order = 1,
                IconClass = "fa fa-fw fa-folder-open",
                Url = "#"
            });
            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = configForm,
                Content = "Configuration",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });


            var securityForm = Guid.NewGuid();

            menu.Add(new Menu
            {
                ID = securityForm,
                ParentID = null,
                Content = "Security",
                Order = 1,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = securityForm,
                Content = "Employees",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            var atcCustomerForm = Guid.NewGuid();

            menu.Add(new Menu
            {
                ID = atcCustomerForm,
                ParentID = null,
                Content = "ATC Customer Portal",
                Order = 1,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = atcCustomerForm,
                Content = "Users",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });


            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = atcCustomerForm,
                Content = "Documents",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

        }


        private static void GetQualityManagementMenu(string routetype, List<Menu> menu)
        {
            var qualityMgmtForm = Guid.NewGuid();
            menu.Add(new Menu
            {
                ID = qualityMgmtForm,
                ParentID = null,
                Content = "Quality Mgmt",
                Order = 1,
                IconClass = "fa fa-fw fa-folder-open",
                Url = "#"
            });
            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = qualityMgmtForm,
                Content = "Customer Claims Requests",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = qualityMgmtForm,
                Content = "Product Complaint",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = qualityMgmtForm,
                Content = "Return Goods Authorization",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = qualityMgmtForm,
                Content = "Corrective / Preventive",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = qualityMgmtForm,
                Content = "Management of Change",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = qualityMgmtForm,
                Content = "Supplier Complaint",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = qualityMgmtForm,
                Content = "QDR Forms",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = qualityMgmtForm,
                Content = "Trials",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callCustomer()"
            });




            var reportsForm = Guid.NewGuid();

            menu.Add(new Menu
            {
                ID = reportsForm,
                ParentID = null,
                Content = "Reports",
                Order = 1,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "Product Complaint",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "Index Report",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "RGA Index Report",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "Claim Costs",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "MOC Risk Log",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = reportsForm,
                Content = "YTD Reports",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            //Configuration

            var ConfigurationForm = Guid.NewGuid();

            menu.Add(new Menu
            {
                ID = ConfigurationForm,
                ParentID = null,
                Content = "Configuration",
                Order = 1,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = ConfigurationForm,
                Content = "NonConformities",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });


            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = ConfigurationForm,
                Content = "Reason Codes",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });


            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = ConfigurationForm,
                Content = "Unit of Measure",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = ConfigurationForm,
                Content = "Scope",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = ConfigurationForm,
                Content = "Severity",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = ConfigurationForm,
                Content = "Fiscal Date Ranges",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = ConfigurationForm,
                Content = "MOC Approvers",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = ConfigurationForm,
                Content = "Trial Departments",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

            menu.Add(new Menu
            {
                ID = Guid.NewGuid(),
                ParentID = ConfigurationForm,
                Content = "Trial Users",
                Order = 0,
                IconClass = "fa fa-fw fa-pie-chart",
                SelectedClass = (routetype == "submenu") ? "is-active" : null,
                Url = "javaScript:callRegion()"
            });

        }


        public IList<MenuViewModel> GetMenu(IList<Menu> menuList, Guid? parentId)
        {
            var children = GetChildrenMenu(menuList, parentId);

            if (!children.Any())
            {
                return new List<MenuViewModel>();
            }

            var vmList = new List<MenuViewModel>();
            foreach (var item in children)
            {
                var menu = GetMenuItem(menuList, item.ID);
                var vm = new MenuViewModel();
                vm.ID = menu.ID;
                vm.Content = menu.Content;
                vm.IconClass = menu.IconClass;
                vm.Url = menu.Url;
                //vm.SelectedStyle = menu.SelectedStyle;
                vm.SelectedStyle = "nav-item";
                vm.SelectedClass = menu.SelectedClass;
                vm.OnClick = menu.OnClick;
                vm.Children = GetMenu(menuList, menu.ID);
                vmList.Add(vm);
            }
            return vmList;
        }


        private IList<Menu> GetChildrenMenu(IList<Menu> menuList, Guid? parentId = null)
        {
            return menuList.Where(x => x.ParentID == parentId).OrderBy(x => x.Order).ToList();
        }

        private Menu GetMenuItem(IList<Menu> menuList, Guid id)
        {
            return menuList.FirstOrDefault(x => x.ID == id);
        }
    }
}
