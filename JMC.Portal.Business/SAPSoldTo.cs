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
//using AtlasSAPPortal;
//using HSSPortalSales;
using JMC.Portal.Business.AtlasSAPPortal;
using JMC.Portal.Business.HSSPortalSales;
using System.Diagnostics;
using System.Web;
using System.Threading;
using IntranetEntities = JMC.Portal.Business.IntranetModel;
using Microsoft.AspNetCore.Http;

namespace JMC.Portal.Business
{
    public partial class SAPSoldTo : SAPShipTo
    {
        static HttpContext httpContext;

        public string RegionSAPConditionGroupName
        {
            get { return this.RegionSapconditionGroupID > 0 ? this.RegionSapconditionGroup.Name : string.Empty; }
        }

        public string TierSAPConditionGroupName
        {
            get { return this.TierSapconditionGroupID > 0 ? this.TierSapconditionGroup.Name : string.Empty; }
        }

        public string SAPCustomerGroupName
        {
            get { return this.SapcustomerGroupID > 0 ? this.SapcustomerGroup.Name : string.Empty; }
        }

        public string SAPSalesGroupName
        {
            get { return this.SapsalesGroupID > 0 ? this.SapsalesGroup.Name : string.Empty; }
        }

        public string RegionalManagerFullName
        {
            get { return this.SapcustomerGroupID > 0 ? (this.SapcustomerGroup.RegionalManagerUserID > 0 ? this.SapcustomerGroup.RegionalManagerUser.FullName : string.Empty) : string.Empty; }
        }

        public string NumericPhone
        {
            get { return Regex.Replace(this.Phone, @"[^0-9]", "").Trim(".".ToCharArray()); }
        }

        public bool Scrap { get { return (from s in this.ScrapSapsoldTos where s.Active select s).Count() > 0; } }
        public bool RandomLegnth { get { return (from s in this.RandomLengthSapsoldTos where s.Active select s).Count() > 0; } }

        public List<Truck> Trucks
        {
            get
            {
                if (_trucks.IsNull())
                {
                    _trucks = this.MyTrucks.Union(from ast in this.AlternateSapsoldTo from t in ast.MyTrucks select t).Distinct().ToList();
                }

                return _trucks;
            }
        }
        private List<Truck> _trucks = null;

        public List<Trailer> Trailers
        {
            get
            {
                if (_trailers.IsNull())
                {
                    _trailers = this.MyTrailers.Union(from ast in this.AlternateSapsoldTo from t in ast.MyTrailers select t).Distinct().ToList();
                }

                return _trailers;
            }
        }
        private List<Trailer> _trailers = null;

        public List<Box> Boxes
        {
            get
            {
                if (_boxes.IsNull())
                {
                    _boxes = this.MyBoxes.Union(from ast in this.AlternateSapsoldTo from t in ast.MyBoxes select t).Distinct().ToList();
                }

                return _boxes;
            }
        }
        private List<Box> _boxes = null;

        private IEnumerable<Truck> MyTrucks
        {
            get
            {
                List<Truck> trk = new List<Truck>();
                if (this.Scrap)
                {
                    foreach (ScrapSAPSoldTo scrapSAPSoldTo in this.ScrapSapsoldTos)
                    {
                        trk.AddRange(scrapSAPSoldTo.Trucks.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList());
                    }
                    //return this.ScrapSapsoldToses.FirstOrDefault().Trucks.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList();
                    return trk;
                }
                else if (this.RandomLegnth)
                {
                    foreach (RandomLengthSAPSoldTo randomLengthSAPSoldTo in this.RandomLengthSapsoldTos)
                    {
                        trk.AddRange(randomLengthSAPSoldTo.Trucks.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList());
                    }
                    //return this.RandomLengthSapsoldToses.FirstOrDefault().Trucks.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList();
                    return trk;
                }

                return null;
            }
        }

        private IEnumerable<Trailer> MyTrailers
        {
            get
            {
                List<Trailer> trl = new List<Trailer>();
                if (this.Scrap)
                {
                    foreach (ScrapSAPSoldTo scrapSAPSoldTo in this.ScrapSapsoldTos)
                    {
                        trl.AddRange(scrapSAPSoldTo.Trailers.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList());
                    }
                    //return this.ScrapSapsoldToses.FirstOrDefault().Trucks.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList();
                    return trl;
                    return this.ScrapSapsoldTos.FirstOrDefault().Trailers.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList();
                }
                else if (this.RandomLegnth)
                {
                    foreach (RandomLengthSAPSoldTo randomLengthSAPSoldTo in this.RandomLengthSapsoldTos)
                    {
                        trl.AddRange(randomLengthSAPSoldTo.Trailers.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList());
                    }
                    return trl;
                    //return this.RandomLengthSapsoldToses.FirstOrDefault().Trailers.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList();
                }

                return null;
            }
        }

        private IEnumerable<Box> MyBoxes
        {
            get
            {
                List<Box> box = new List<Box>();
                if (this.Scrap)
                {
                    foreach (ScrapSAPSoldTo scrapSAPSoldTo in this.ScrapSapsoldTos)
                    {
                        box.AddRange(scrapSAPSoldTo.Boxes.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList());
                    }
                    //return this.ScrapSapsoldToses.FirstOrDefault().Trucks.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList();
                    return box;
                    return this.ScrapSapsoldTos.FirstOrDefault().Boxes.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList();
                }
                else if (this.RandomLegnth)
                {
                    foreach (RandomLengthSAPSoldTo randomLengthSAPSoldTo in this.RandomLengthSapsoldTos)
                    {
                        box.AddRange(randomLengthSAPSoldTo.Boxes.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList());
                    }
                    return box;
                    //return this.RandomLengthSapsoldToses.FirstOrDefault().Boxes.OrderBy(t => t.Name).OrderByDescending(t => t.Active).ToList();
                }

                return null;
            }
        }

        public static void RefreshAllBacklogs()
        {
            DateTime anhourago = DateTime.Now.AddHours(-1);
            DateTime startTime = DateTime.Now;
            Stopwatch clock = new Stopwatch();
            List<string> Messages = new List<string>();
            string Title = "Refresh All Backlogs";
            Messages.Add("Refresh all Backlogs started at " + DateTime.Now.ToString());
            List<string> soldToNumbers = new List<string>();
            using (PortalEntities db = new PortalEntities())
            {
                soldToNumbers = (from x in db.SapshipTos.OfType<SAPSoldTo>()
                                 where x.DivisionID == (long)Enums.Divisions.Atlas &&
                                    !(x.RefreshingBacklog ?? false) &&
                                    x.LastBacklogRefresh.HasValue && x.LastBacklogRefresh.Value < anhourago
                                 select x.Number)
                    .Distinct()
                 .ToList();
                if (soldToNumbers.Count == 0) return;
            }
            Messages.Add("Refreshing " + soldToNumbers.Count + " SoldTos");
            Messages.Add(" ");
            try
            {
                List<string> processingStack = new List<string>();
                foreach (string soldToNumber in soldToNumbers)
                {
                    processingStack.Add(soldToNumber);
                    Messages.Add("Sold to " + soldToNumber);
                    if (processingStack.Count >= 5)
                    {
                        clock.Start();
                        using (PortalEntities db = new PortalEntities())
                        {
                            DBCache dbcache = new DBCache(db);
                            SAPSoldTo.RefreshBacklog(ref dbcache, processingStack, true, true);
                            processingStack.Clear();
                        }
                        clock.Stop();
                        Messages.Add("Time " + clock.Elapsed.ToString());
                        Messages.Add(" ");
                    }
                }

                if (processingStack.Count > 0)
                {
                    clock.Start();
                    using (PortalEntities db = new PortalEntities())
                    {
                        DBCache dbcache = new DBCache(db);
                        SAPSoldTo.RefreshBacklog(ref dbcache, processingStack, true, true);
                        processingStack.Clear();
                    }
                    clock.Stop();
                    Messages.Add("Time " + clock.Elapsed.ToString());
                    Messages.Add(" ");
                }
                Messages.Add(" ");
                Messages.Add("Refresh all Backlogs completed at " + DateTime.Now.ToString());

                Messages.Add("Total Time" + DateTime.Now.Subtract(startTime).ToString());
            }
            catch (Exception ex)
            {
                Title = "Error: Refresh All Backlogs";
                Exception exc = ex;
                while (!exc.InnerException.IsNull())
                {
                    Messages.Add("ex: " + exc.Message);
                    exc = exc.InnerException;
                }
                Messages.Add("stack: " + ex.StackTrace);
            }
            finally
            {
                string body = "";
                foreach (string message in Messages)
                {
                    body += message + Environment.NewLine;
                }
                body = body.Replace(Environment.NewLine, "<br />" + Environment.NewLine);

                Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], ConfigurationManager.AppSettings["ErrorEmailAddress"], string.Empty, Title, body);
            }
        }

        public static void RefreshAllBacklogsParallel()
        {
            Thread thread = new Thread(SAPSoldTo._RefreshAllBacklogsParallel);
            thread.IsBackground = true;
            thread.Start();
        }
        private static void _RefreshAllBacklogsParallel()
        {
            List<BackgroundProcessor.ProcessingTask> tasks = new List<BackgroundProcessor.ProcessingTask>();
            List<string> Messages = new List<string>();
            DateTime startTime = DateTime.Now;

            Messages.Add("Refresh all Backlogs Parallel started at " + startTime.ToString());

            using (PortalEntities db = new PortalEntities())
            {
                List<SAPSoldTo> soldtoNumbers = (from x in db.SapshipTos.OfType<SAPSoldTo>() where x.LastBacklogRefresh.HasValue && !(x.RefreshingBacklog ?? false) select x).ToList();
                foreach (SAPSoldTo SAPSoldTo in soldtoNumbers)
                {
                    BackgroundProcessor.ProcessingTask newTask = BackgroundProcessor.Push(new BackgroundProcessor.ProcessingTask(1000, "BL", SAPSoldTo.Number));
                    tasks.Add(newTask);
                }
            }

            Messages.Add(tasks.Count + " Tasks Enqueued");

            tasks.WaitForAll();

            foreach (BackgroundProcessor.ProcessingTask task in tasks)
            {
                if (task.exception.IsNull())
                {
                    Messages.Add(task._jobtype + " " + task._arguments + task.jobStarted.ToString() + " Runtime: " + task.jobCompleted.Subtract(task.jobStarted).ToString());
                }
                else
                {
                    Messages.Add(task._jobtype + " " + task._arguments + task.jobStarted.ToString() + " Exception: " + task.exception.Message);
                }
            }
            DateTime endTime = DateTime.Now;

            Messages.Add("Runtime sum " + tasks.Sum(x => x.jobCompleted.Subtract(x.jobStarted).TotalMinutes).ToString("#,##0.00") + " minutes");
            Messages.Add("Actual Total Runtime " + endTime.Subtract(startTime).ToString());

            string body = "";
            foreach (string message in Messages)
            {
                body += message + Environment.NewLine;
            }
            body = body.Replace(Environment.NewLine, "<br />" + Environment.NewLine);

            using (PortalEntities db = new PortalEntities())
            {
                List<SAPSoldTo> soldtoNumbers = (from x in db.SapshipTos.OfType<SAPSoldTo>() where (x.RefreshingBacklog ?? false) select x).ToList();
                foreach (SAPSoldTo SAPSoldTo in soldtoNumbers)
                {
                    SAPSoldTo.RefreshingBacklog = false;
                }
                db.SaveChanges();
            }

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], ConfigurationManager.AppSettings["ErrorEmailAddress"], string.Empty, "Parallel - Backlog Refresh", body);
        }

        public static ZfmGetHssBacklogResponse RefreshBacklog(ref DBCache dbcache, List<string> soldToNumbers, bool RefreshSalesOrders = true, bool RefreshDeliveries = true, bool force = false)
        {
            ZfmGetHssBacklogResponse getHssBacklogResponse = null;
            List<SAPSalesOrderItem> backloggedSalesOrderItems = new List<SAPSalesOrderItem>();
            List<SAPDelivery> backloggedDeliveries = new List<SAPDelivery>();
            if (soldToNumbers.IsNull()) soldToNumbers = new List<string>();

            #region Build SoldTos List
            List<SAPSoldTo> forSAPSoldTos = new List<SAPSoldTo>();
            DateTime anHourAgo = DateTime.Now.AddHours(-1);

            foreach (string soldToNumber in soldToNumbers)
            {
                SAPSoldTo SAPSoldTo = dbcache.getSoldToByNumber(soldToNumber);
                if (force)
                {
                    if (!SAPSoldTo.IsNull() && !SAPSoldTo.RefreshingBacklog.ToBool())
                    {
                        SAPSoldTo.RefreshingBacklog = true;
                        forSAPSoldTos.Add(SAPSoldTo);
                    }
                }
                else
                {
                    if (!SAPSoldTo.IsNull() && !SAPSoldTo.RefreshingBacklog.ToBool() && ((!RefreshSalesOrders && RefreshDeliveries) || (!SAPSoldTo.LastBacklogRefresh.HasValue || SAPSoldTo.LastBacklogRefresh.Value < anHourAgo)))
                    {
                        if (RefreshSalesOrders && RefreshDeliveries)
                        {
                            SAPSoldTo.RefreshingBacklog = true;
                        }
                        forSAPSoldTos.Add(SAPSoldTo);
                    }
                }
            }
            dbcache.db.SaveChanges();

            if (RefreshSalesOrders)
            {
                foreach (SAPSoldTo SAPSoldTo in forSAPSoldTos)
                { //SalesOrders
                    if (RefreshSalesOrders && RefreshDeliveries)
                    {
                        SAPSoldTo.RefreshingBacklog = true;
                        List<SAPSalesOrderItem> soldToBackLoggedSalesOrderItems = (from soi in dbcache.db.SapsalesOrderItems where soi.Backlog == true && soi.SapsalesOrder.SapsoldToID == SAPSoldTo.SapshipToID select soi).ToList();
                        backloggedSalesOrderItems.AddRange(soldToBackLoggedSalesOrderItems);
                    }
                }
            }
            dbcache.AddToCache(backloggedSalesOrderItems);

            if (!RefreshSalesOrders && RefreshDeliveries && (soldToNumbers.IsNull() || !soldToNumbers.Any()))
            {
                backloggedDeliveries = (from d in dbcache.db.Sapdeliveries where !d.ActualGoodsMovementDate.HasValue select d).ToList();
            }
            else if (RefreshDeliveries)
            {
                foreach (SAPSoldTo SAPSoldTo in forSAPSoldTos)
                { //Deliveries
                    List<SAPDelivery> soldToBacklogDeliveries = (from d in dbcache.db.Sapdeliveries where !d.ActualGoodsMovementDate.HasValue && d.SapsoldToID == SAPSoldTo.SapshipToID select d).ToList();
                    backloggedDeliveries.AddRange(soldToBacklogDeliveries);
                }
            }
            dbcache.AddToCache(backloggedDeliveries);
            #endregion

            if ((forSAPSoldTos.Count > 0) || (!RefreshSalesOrders && RefreshDeliveries))
            {
                #region CALL SAP
                ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
                portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
                portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

                ZfmGetHssBacklog getHssBacklog = new ZfmGetHssBacklog();
                getHssBacklog.ItSoldToNumbers = (from x in forSAPSoldTos select x.Number).ToArray();
                getHssBacklog.ImGetDeliveries = "";
                getHssBacklog.ImGetSalesOrders = "";
                if (RefreshDeliveries)
                    getHssBacklog.ImGetDeliveries = "X";
                if (RefreshSalesOrders)
                    getHssBacklog.ImGetSalesOrders = "X";
                getHssBacklog.ItHssDeliveries = (from x in backloggedDeliveries select new ZstHssDelivery() { DeliveryNumber = x.Number }).ToArray();

                dbcache.AddMetrics("Calling SAP with the following");
                dbcache.AddMetrics("getHssBacklog.ItSoldToNumbers.Length = " + getHssBacklog.ItSoldToNumbers.Length);
                dbcache.AddMetrics("getHssBacklog.ImGetDeliveries = " + getHssBacklog.ImGetDeliveries);
                dbcache.AddMetrics("getHssBacklog.ImGetSalesOrders = " + getHssBacklog.ImGetSalesOrders);


                //getHssBacklogSummary.ImSoldToNumber = this.Number;
                Stopwatch sapTime = new Stopwatch();

                sapTime.Start();
                portalSalesService.Open();
                getHssBacklogResponse = portalSalesService.ZfmGetHssBacklogAsync(getHssBacklog);
                portalSalesService.Close();
                sapTime.Stop();
                #endregion
                dbcache.AddMetrics("Time in SAP " + sapTime.ElapsedMilliseconds);
                dbcache.AddMetrics("Sales Orders Returned " + getHssBacklogResponse.EtHssSalesOrders.Length);
                dbcache.AddMetrics("Sales OrderItems Returned " + getHssBacklogResponse.EtHssSalesOrderItems.Length);
                dbcache.AddMetrics("Deliveries Returned " + getHssBacklogResponse.EtHssDeliveries.Length);
                dbcache.AddMetrics("Delivery Items Returned " + getHssBacklogResponse.EtHssDeliveryItems.Length);

                Stopwatch storeorders = new Stopwatch();
                storeorders.Start();
                if (RefreshSalesOrders)
                {
                    SAPSalesOrder.StoreSalesOrders(ref dbcache, getHssBacklogResponse.EtHssSalesOrders, getHssBacklogResponse.EtHssSalesOrderItems);
                    SAPSalesOrder.StoreSalesOrderItems(ref dbcache, getHssBacklogResponse.EtHssSalesOrderItems, backloggedSalesOrderItems, true, true);
                    dbcache.db.SaveChanges();
                }
                storeorders.Stop();

                dbcache.AddMetrics("Total Time Processing Sales Orders " + storeorders.ElapsedMilliseconds);

                Stopwatch storedeliveries = new Stopwatch();
                storedeliveries.Start();

                if (RefreshDeliveries)
                {
                    SAPDelivery.StoreZstHssDeliveries(ref dbcache, getHssBacklogResponse.EtHssDeliveries);
                    SAPDelivery.StoreZstHssDeliveryItems(ref dbcache, getHssBacklogResponse.EtHssDeliveryItems);
                    dbcache.db.SaveChanges();
                }
                storedeliveries.Stop();
                dbcache.AddMetrics("Total Time Processing Deliveries " + storedeliveries.ElapsedMilliseconds);

                var oldDeliveryNumbers = (from x in backloggedDeliveries select x.Number.TrimNull()).Distinct().ToList();
                var newDeliveryNumbers = (from x in getHssBacklogResponse.EtHssDeliveries select x.DeliveryNumber.TrimNull()).Distinct().ToList();
                foreach (string newDeliveryNumber in newDeliveryNumbers)
                {
                    oldDeliveryNumbers.Remove(newDeliveryNumber);
                }
                foreach (string oldDeliveryNumber in oldDeliveryNumbers)
                {
                    dbcache.DestroyDeliveryByNumber(oldDeliveryNumber);
                }
                dbcache.db.SaveChanges();

                foreach (var soldTo in forSAPSoldTos)
                {
                    soldTo.LastBacklogRefresh = DateTime.Now;
                    soldTo.RefreshingBacklog = false;
                }
            }
            dbcache.db.SaveChanges();

            if (forSAPSoldTos.Count == 0 && !RefreshSalesOrders && RefreshDeliveries)
            {
                string Message = "";
                foreach (string a in dbcache.Metrics)
                {
                    Message += a + "<br />" + Environment.NewLine;
                }
                string username = string.Empty;
                try
                {
                    username = httpContext.User.Identity.Name;
                }
                catch { }
                bool html = true;
                Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], ConfigurationManager.AppSettings["ErrorEmailAddress"], string.Empty, "Backlog Stats: " + username, Message);
            }
            return getHssBacklogResponse;
        }

        public static List<SAPSoldTo> FindAllByRegion(ref PortalEntities db, long regionSAPConditionGroupID)
        {
            return (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.RegionSapconditionGroupID == regionSAPConditionGroupID select s).ToList();
        }

        public static List<SAPSoldTo> FindRegionSAPConditionGroupID(ref PortalEntities db, long soldToID)
        {
            List<SAPSoldTo> regionSAPConditionGroupID = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.SapshipToID == soldToID select s).ToList();

            return regionSAPConditionGroupID;
        }

        //public static List<SAPSoldTo> FindSOLDToInfo(ref PortalEntities db, long soldToID)
        //{
        //    List<SAPSoldTo> soldToIDInfo = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.SAPShipToID == soldToID select s).ToList();

        //    return soldToIDInfo;
        //}

        public static List<SAPSoldTo> GetMaterialPriceGroupsPrice(long? regionSAPConditionGroupID, long? tierSAPConditionGroupID, long? freightIndicatorSAPConditionGroupID, long? SAPCustomerGroupID, long? SAPSalesGroupID, string incoTerms2)
        {
            PortalEntities db = new PortalEntities();

            IQueryable<SAPSoldTo> SAPSoldTos = (from s in db.SapshipTos.OfType<SAPSoldTo>() select s);

            SAPSoldTos = (from s in SAPSoldTos
                          where s.DivisionID == (long)Enums.Divisions.Atlas
                          && (regionSAPConditionGroupID == null || s.RegionSapconditionGroupID == regionSAPConditionGroupID)
                          && (tierSAPConditionGroupID == null || s.TierSapconditionGroupID == tierSAPConditionGroupID)
                          && (SAPCustomerGroupID == null || s.SapcustomerGroupID == SAPCustomerGroupID)
                          && (SAPSalesGroupID == null || s.SapsalesGroupID == SAPSalesGroupID)
                          && (incoTerms2 == string.Empty || s.IncoTerms2.ToLower() == incoTerms2.ToLower())
                          && s.Active == true
                          select s
                                                            );

            List<SAPSoldTo> SAPSoldToList = null;

            if (!freightIndicatorSAPConditionGroupID.IsNull())
            {
                SAPSoldToList = (from s in SAPSoldTos
                                 from st in s.sapshipTos
                                 where st.FreightIndicatorSapconditionGroupID == freightIndicatorSAPConditionGroupID
                                 select s).Distinct().OrderBy(s => s.Number).ToList();
            }
            else
            {
                SAPSoldToList = SAPSoldTos.OrderBy(s => s.Number).ToList();
            }
            ////////////hide sold to for the given user in config///////////////////
            if (ConfigurationManager.AppSettings["HideSoldToForUser"] != null)
            {
                string[] hideForUsers = ConfigurationManager.AppSettings["HideSoldToForUser"].ToString().Split(';');
                string logonUser = httpContext.User.Identity.Name;//db.User.FirstOrDefault(x => x.UserName == httpContext.User.Identity.Name);                      
                Employee user = Employee.FindByDomainAndSAMAccountName(ref db, logonUser);
                foreach (string hideForUser in hideForUsers)
                {
                    string[] hideSoldToForUser = hideForUser.Split(',');
                    if ((user != null) && (logonUser == hideSoldToForUser[0]))
                    {
                        string[] hideSoldTos = hideSoldToForUser.Skip(1).ToArray();
                        var soldTos = SAPSoldToList.Where(x => hideSoldTos.Contains(x.Number)).ToList();
                        foreach (var soldTo in soldTos)
                            SAPSoldToList.Remove(soldTo);
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////////
            if (SAPSoldToList.Count() <= 600)
            {
                ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
                sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
                sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

                ZGetPricingConditions getPricingConditions = new ZGetPricingConditions();

                ArrayList pricingConditionArrayList = new ArrayList();

                foreach (SAPSoldTo SAPSoldTo in SAPSoldToList)
                {
                    SAPCharacteristicOption materialPricingGroup = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialPricingGroup && co.Sapcode == "Z1" select co).FirstOrDefault();

                    ZstGetPricingConditions zstGetPricingConditions = new ZstGetPricingConditions();
                    zstGetPricingConditions.ConditionType = SAPCondition.FindAndInsertIfMissing(ref db, "ZR00").Sapcode;
                    zstGetPricingConditions.SoldToNumber = SAPSoldTo.Number;
                    zstGetPricingConditions.MaterialPricingGroup = materialPricingGroup.IsNull() ? string.Empty : materialPricingGroup.Sapcode;
                    zstGetPricingConditions.EffectiveDate = DateTime.Today;

                    pricingConditionArrayList.Add(zstGetPricingConditions);

                    materialPricingGroup = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialPricingGroup && co.Sapcode == "Z2" select co).FirstOrDefault();

                    zstGetPricingConditions = new ZstGetPricingConditions();
                    zstGetPricingConditions.ConditionType = SAPCondition.FindAndInsertIfMissing(ref db, "ZR00").Sapcode;
                    zstGetPricingConditions.SoldToNumber = SAPSoldTo.Number;
                    zstGetPricingConditions.MaterialPricingGroup = materialPricingGroup.IsNull() ? string.Empty : materialPricingGroup.Sapcode;
                    zstGetPricingConditions.EffectiveDate = DateTime.Today;

                    pricingConditionArrayList.Add(zstGetPricingConditions);

                    materialPricingGroup = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialPricingGroup && co.Sapcode == "Z3" select co).FirstOrDefault();

                    zstGetPricingConditions = new ZstGetPricingConditions();
                    zstGetPricingConditions.ConditionType = SAPCondition.FindAndInsertIfMissing(ref db, "ZR00").Sapcode;
                    zstGetPricingConditions.SoldToNumber = SAPSoldTo.Number;
                    zstGetPricingConditions.MaterialPricingGroup = materialPricingGroup.IsNull() ? string.Empty : materialPricingGroup.Sapcode;
                    zstGetPricingConditions.EffectiveDate = DateTime.Today;

                    pricingConditionArrayList.Add(zstGetPricingConditions);

                    materialPricingGroup = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialPricingGroup && co.Sapcode == "Z4" select co).FirstOrDefault();

                    zstGetPricingConditions = new ZstGetPricingConditions();
                    zstGetPricingConditions.ConditionType = SAPCondition.FindAndInsertIfMissing(ref db, "ZR00").Sapcode;
                    zstGetPricingConditions.SoldToNumber = SAPSoldTo.Number;
                    zstGetPricingConditions.MaterialPricingGroup = materialPricingGroup.IsNull() ? string.Empty : materialPricingGroup.Sapcode;
                    zstGetPricingConditions.EffectiveDate = DateTime.Today;

                    pricingConditionArrayList.Add(zstGetPricingConditions);
                }

                getPricingConditions.PricingConditions = (ZstGetPricingConditions[])pricingConditionArrayList.ToArray(typeof(ZstGetPricingConditions));

                sapPortalService.Open();
                ZGetPricingConditionsResponse getPricingConditionsResponse = sapPortalService.ZGetPricingConditionsAsync(getPricingConditions);
                sapPortalService.Close();

                foreach (ZstGetPricingConditions zstGetPricingCondition in getPricingConditionsResponse.PricingConditions)
                {
                    if (!zstGetPricingCondition.ConditionType.jIsEmpty())
                    {
                        SAPSoldTo SAPSoldTo = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.Number == zstGetPricingCondition.SoldToNumber.Trim() select s).FirstOrDefault();

                        if (!SAPSoldTo.IsNull())
                        {
                            if (zstGetPricingCondition.MaterialPricingGroup == "Z1")
                            {
                                SAPSoldTo.CoreSAPPricingCondition = new SAPCondition();
                                SAPSoldTo.CoreSAPPricingCondition.Sapcode = zstGetPricingCondition.ConditionType;
                                SAPSoldTo.CoreSAPPricingCondition.Rate = zstGetPricingCondition.Rate;
                                SAPSoldTo.CoreSAPPricingCondition.RateUnit = zstGetPricingCondition.RateUnit;
                                SAPSoldTo.CoreSAPPricingCondition.ValidFrom = zstGetPricingCondition.ValidFrom;
                                SAPSoldTo.CoreSAPPricingCondition.ValidTo = zstGetPricingCondition.ValidTo;
                                SAPSoldTo.CoreSAPPricingCondition.PricePer = zstGetPricingCondition.PricePer.ToInt();
                                SAPSoldTo.CoreSAPPricingCondition.PricePerUnit = zstGetPricingCondition.PricePerUnit;
                            }
                            if (zstGetPricingCondition.MaterialPricingGroup == "Z2")
                            {
                                SAPSoldTo.PropSAPPricingCondition = new SAPCondition();
                                SAPSoldTo.PropSAPPricingCondition.Sapcode = zstGetPricingCondition.ConditionType;
                                SAPSoldTo.PropSAPPricingCondition.Rate = zstGetPricingCondition.Rate;
                                SAPSoldTo.PropSAPPricingCondition.RateUnit = zstGetPricingCondition.RateUnit;
                                SAPSoldTo.PropSAPPricingCondition.ValidFrom = zstGetPricingCondition.ValidFrom;
                                SAPSoldTo.PropSAPPricingCondition.ValidTo = zstGetPricingCondition.ValidTo;
                                SAPSoldTo.PropSAPPricingCondition.PricePer = zstGetPricingCondition.PricePer.ToInt();
                                SAPSoldTo.PropSAPPricingCondition.PricePerUnit = zstGetPricingCondition.PricePerUnit;
                            }
                            if (zstGetPricingCondition.MaterialPricingGroup == "Z3")
                            {
                                SAPSoldTo.JumboSAPPricingCondition = new SAPCondition();
                                SAPSoldTo.JumboSAPPricingCondition.Sapcode = zstGetPricingCondition.ConditionType;
                                SAPSoldTo.JumboSAPPricingCondition.Rate = zstGetPricingCondition.Rate;
                                SAPSoldTo.JumboSAPPricingCondition.RateUnit = zstGetPricingCondition.RateUnit;
                                SAPSoldTo.JumboSAPPricingCondition.ValidFrom = zstGetPricingCondition.ValidFrom;
                                SAPSoldTo.JumboSAPPricingCondition.ValidTo = zstGetPricingCondition.ValidTo;
                                SAPSoldTo.JumboSAPPricingCondition.PricePer = zstGetPricingCondition.PricePer.ToInt();
                                SAPSoldTo.JumboSAPPricingCondition.PricePerUnit = zstGetPricingCondition.PricePerUnit;
                            }
                            if (zstGetPricingCondition.MaterialPricingGroup == "Z4")
                            {
                                SAPSoldTo.EpoxZSAPPricingCondition = new SAPCondition();
                                SAPSoldTo.EpoxZSAPPricingCondition.Sapcode = zstGetPricingCondition.ConditionType;
                                SAPSoldTo.EpoxZSAPPricingCondition.Rate = zstGetPricingCondition.Rate;
                                SAPSoldTo.EpoxZSAPPricingCondition.RateUnit = zstGetPricingCondition.RateUnit;
                                SAPSoldTo.EpoxZSAPPricingCondition.ValidFrom = zstGetPricingCondition.ValidFrom;
                                SAPSoldTo.EpoxZSAPPricingCondition.ValidTo = zstGetPricingCondition.ValidTo;
                                SAPSoldTo.EpoxZSAPPricingCondition.PricePer = zstGetPricingCondition.PricePer.ToInt();
                                SAPSoldTo.EpoxZSAPPricingCondition.PricePerUnit = zstGetPricingCondition.PricePerUnit;
                            }

                            if (zstGetPricingCondition.MaterialPricingGroup == "Z5")
                            {
                                SAPSoldTo.LGPipeSAPPricingCondition = new SAPCondition();
                                SAPSoldTo.LGPipeSAPPricingCondition.Sapcode = zstGetPricingCondition.ConditionType;
                                SAPSoldTo.LGPipeSAPPricingCondition.Rate = zstGetPricingCondition.Rate;
                                SAPSoldTo.LGPipeSAPPricingCondition.RateUnit = zstGetPricingCondition.RateUnit;
                                SAPSoldTo.LGPipeSAPPricingCondition.ValidFrom = zstGetPricingCondition.ValidFrom;
                                SAPSoldTo.LGPipeSAPPricingCondition.ValidTo = zstGetPricingCondition.ValidTo;
                                SAPSoldTo.LGPipeSAPPricingCondition.PricePer = zstGetPricingCondition.PricePer.ToInt();
                                SAPSoldTo.LGPipeSAPPricingCondition.PricePerUnit = zstGetPricingCondition.PricePerUnit;
                            }
                        }
                    }
                }
            }

            return SAPSoldToList;
        }

        public static new void RefreshFromAtlasSAP(string email)
        {
            SAPShipTo.RefreshFromAtlasSAP(email);

            PortalEntities db = new PortalEntities();

            int insertedCount = 0;
            int checkedForUpdatesCount = 0;
            int joinsCount = 0;
            int joinsRemovedCount = 0;
            int SAPSalesGroupCount = 0;
            int SAPCustomerGroupCount = 0;
            int disabledCount = 0;
            int soldToSalesOrgJoin = 0;
            int SAPSoldTosalesorgcount = 0;

            ArrayList SAPSoldToNumbers = new ArrayList();

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            StringBuilder joinsRemovedStringBuilder = new StringBuilder();
            StringBuilder disabledStringBuilder = new StringBuilder();

            ZWS_MASTER_DATAClient masterDataService = new ZWS_MASTER_DATAClient("ATLAS_ZWS_MASTER_DATA");
            masterDataService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            masterDataService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            AtlasMasterData.ZGetAllSoldTos getAllSoldTos = new AtlasMasterData.ZGetAllSoldTos();
            getAllSoldTos.SoldTos = new AtlasMasterData.ZstSoldTo[] { new AtlasMasterData.ZstSoldTo() };
            getAllSoldTos.SoldTosShipTos = new AtlasMasterData.ZstSoldToShipTo[] { new AtlasMasterData.ZstSoldToShipTo() };
            getAllSoldTos.ConditionGroups = new AtlasMasterData.ZstConditionGroup[] { new AtlasMasterData.ZstConditionGroup() };
            getAllSoldTos.CustomerGroups = new AtlasMasterData.ZstCustomerGroup[] { new AtlasMasterData.ZstCustomerGroup() };
            getAllSoldTos.SalesGroups = new AtlasMasterData.ZstSalesGroup[] { new AtlasMasterData.ZstSalesGroup() };
            getAllSoldTos.SoldTosSalesOrgs = new AtlasMasterData.ZstSoldToSalesOrg[] { new AtlasMasterData.ZstSoldToSalesOrg() };
            getAllSoldTos.CustMaterialInfo = new AtlasMasterData.ZsdKnmtPortal[] { new AtlasMasterData.ZsdKnmtPortal() };

            masterDataService.Open();
            AtlasMasterData.ZGetAllSoldTosResponse getAllSoldTosResponse = masterDataService.ZGetAllSoldTosAsync(getAllSoldTos);
            masterDataService.Close();

            List<City> cities = (from c in db.Cities select c).ToList();
            List<State> states = (from s in db.States select s).ToList();
            List<Country> countries = (from c in db.Countries select c).ToList();
            List<SAPSoldTo> soldTos = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.DivisionID == (long)Enums.Divisions.Atlas select s).ToList();
            List<SAPShipTo> shipTos = (from s in db.SapshipTos where s.DivisionID == (long)Enums.Divisions.Atlas select s).ToList();
            List<SAPMaterial> SAPMaterials = (from s in db.Sapmaterials where s.DivisionID == (long)Enums.Divisions.Atlas select s).ToList();
            List<Cmir> CMIRs = (from s in db.Cmirs select s).ToList();
            List<SAPConditionGroup> conditionGroups = (from cg in db.SapconditionGroups where cg.DivisionID == (long)Enums.Divisions.Atlas select cg).ToList();
            List<SAPCustomerGroup> customerGroups = (from cg in db.SapcustomerGroups where cg.DivisionID == (long)Enums.Divisions.Atlas select cg).ToList();
            List<SAPSalesGroup> salesGroups = (from sg in db.SapsalesGroups where sg.DivisionID == (long)Enums.Divisions.Atlas select sg).ToList();
            List<SAPSalesOrganization> salesOrganizations = (from so in db.SapsalesOrganizations where so.DivisionID == (long)Enums.Divisions.Atlas select so).ToList();
            List<SAPShipToSAPSalesOrganization> SAPShipToSAPSalesOrganizations = (from sso in db.SapshipToSapsalesOrganizations where sso.SapsalesOrganization.DivisionID == (long)Enums.Divisions.Atlas select sso).ToList();
            List<Employee> employees = (from e in db.Users.OfType<Employee>() select e).ToList();

            List<SAPRegion> regions = (from r in db.Sapregions where r.DivisionID == (long)Enums.Divisions.Atlas select r).ToList();
            List<SAPTier> tiers = (from t in db.Saptiers where t.DivisionID == (long)Enums.Divisions.Atlas select t).ToList();
            List<SAPCharacteristicOption> paymentTerms = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.PricingGroup select co).ToList();


            foreach (AtlasMasterData.ZstConditionGroup zstConditionGroup in getAllSoldTosResponse.ConditionGroups)
            {
                SAPConditionGroup SAPConditionGroup = (from cg in conditionGroups where cg.Sapcode == zstConditionGroup.Code select cg).FirstOrDefault();

                if (SAPConditionGroup.IsNull())
                {
                    SAPConditionGroup = new SAPConditionGroup();
                    SAPConditionGroup.DivisionID = (long)Enums.Divisions.Atlas;
                    conditionGroups.Add(SAPConditionGroup);
                    db.SapconditionGroups.Add(SAPConditionGroup);
                }

                SAPConditionGroup.Sapcode = zstConditionGroup.Code;
                SAPConditionGroup.Name = zstConditionGroup.Name;
            }

            foreach (AtlasMasterData.ZstCustomerGroup zstCustomerGroup in getAllSoldTosResponse.CustomerGroups)
            {
                SAPCustomerGroup SAPCustomerGroup = (from cg in customerGroups where cg.Sapcode == zstCustomerGroup.Code select cg).FirstOrDefault();

                if (SAPCustomerGroup.IsNull())
                {
                    SAPCustomerGroup = new SAPCustomerGroup();
                    SAPCustomerGroup.DivisionID = (long)Enums.Divisions.Atlas;
                    SAPCustomerGroup.Sapcode = zstCustomerGroup.Code;

                    customerGroups.Add(SAPCustomerGroup);
                    db.SapcustomerGroups.Add(SAPCustomerGroup);
                }

                string[] nameArray = zstCustomerGroup.Name.Split(" ".ToCharArray());
                string firstName = string.Empty;
                string lastName = string.Empty;

                if (nameArray.Length >= 2)
                {
                    firstName = nameArray[0];
                    lastName = nameArray[1];

                    int slashPosition = lastName.LastIndexOf("/");

                    if (slashPosition > 0)
                    {
                        lastName = lastName.Remove(slashPosition);
                    }
                }

                Employee employee = (from e in employees where e.FirstName.ToLower() == firstName.ToLower() && e.LastName.ToLower() == lastName.ToLower() select e).FirstOrDefault();

                SAPCustomerGroup.Name = zstCustomerGroup.Name.Trim();

                if (SAPCustomerGroup.User.IsNull())
                {
                    SAPCustomerGroup.User = employee;
                }

                SAPCustomerGroupCount++;
            }

            foreach (AtlasMasterData.ZstSalesGroup zstSalesGroup in getAllSoldTosResponse.SalesGroups)
            {
                SAPSalesGroup SAPSalesGroup = (from sg in salesGroups where sg.Sapcode == zstSalesGroup.Code select sg).FirstOrDefault();

                if (SAPSalesGroup.IsNull())
                {
                    SAPSalesGroup = new SAPSalesGroup();
                    SAPSalesGroup.DivisionID = (long)Enums.Divisions.Atlas;
                    SAPSalesGroup.Sapcode = zstSalesGroup.Code;

                    salesGroups.Add(SAPSalesGroup);
                    db.SapsalesGroups.Add(SAPSalesGroup);
                }

                string[] nameArray = zstSalesGroup.Name.Split(" ".ToCharArray());
                string firstName = string.Empty;
                string lastName = string.Empty;

                if (nameArray.Length >= 2)
                {
                    firstName = nameArray[0];
                    lastName = nameArray[1];

                    int slashPosition = lastName.LastIndexOf("/");

                    if (slashPosition > 0)
                    {
                        lastName = lastName.Remove(slashPosition);
                    }
                }

                Employee employee = (from e in employees where e.FirstName.ToLower() == firstName.ToLower() && e.LastName.ToLower() == lastName.ToLower() select e).FirstOrDefault();

                SAPSalesGroup.Name = zstSalesGroup.Name.Trim();

                if (SAPSalesGroup.User.IsNull())
                {
                    SAPSalesGroup.User = employee;
                }

                SAPSalesGroupCount++;
            }

            foreach (AtlasMasterData.ZstSoldTo zstSoldTo in getAllSoldTosResponse.SoldTos)
            {
                SAPSoldToNumbers.Add(zstSoldTo.SoldToNumber.Trim());

                string countryAbbr = zstSoldTo.Country.Trim();
                string stateAbbr = zstSoldTo.State.Trim();
                string cityName = zstSoldTo.City.Trim();

                SAPSoldTo SAPSoldTo = (from s in soldTos where s.Number == zstSoldTo.SoldToNumber.Trim() select s).FirstOrDefault();

                if (!countryAbbr.jIsEmpty() && !stateAbbr.jIsEmpty() && !cityName.jIsEmpty())
                {
                    Country country = (from c in countries where c.Abbreviation.ToLower() == countryAbbr.ToLower() select c).FirstOrDefault();

                    if (country.IsNull())
                    {
                        country = new Country();
                        country.Abbreviation = countryAbbr;
                        country.Name = countryAbbr;
                        country.Active = true;
                        countries.Add(country);
                        db.Countries.Add(country);
                    }

                    State state = (from s in states where s.Abbreviation.ToLower() == stateAbbr.ToLower() && s.Country == country select s).FirstOrDefault();

                    if (state.IsNull())
                    {
                        state = new State();
                        state.Abbreviation = stateAbbr;
                        state.Name = stateAbbr;
                        state.Country = country;
                        state.Active = true;
                        states.Add(state);
                        db.States.Add(state);
                    }

                    City city = (from c in cities where c.Name.ToLower() == cityName.ToLower() && c.State == state select c).FirstOrDefault();

                    if (city.IsNull())
                    {
                        city = new City();
                        city.Name = cityName;
                        city.State = state;
                        city.Active = true;
                        cities.Add(city);
                        db.Cities.Add(city);
                    }

                    if (!city.IsNull())
                    {
                        SAPShipTo defaultSAPShipTo = (from s in shipTos where s.Number == zstSoldTo.DefaultShipTo.Trim() select s).FirstOrDefault();
                        SAPConditionGroup regionSAPConditionGroup = (from cg in conditionGroups where cg.Sapcode == zstSoldTo.Region select cg).FirstOrDefault();
                        SAPConditionGroup tierSAPConditionGroup = (from cg in conditionGroups where cg.Sapcode == zstSoldTo.Tier select cg).FirstOrDefault();
                        SAPConditionGroup homeMillSAPConditionGroup = (from cg in conditionGroups where cg.Sapcode == zstSoldTo.HomeMill select cg).FirstOrDefault();
                        SAPConditionGroup freightIndicatorSAPConditionGroup = (from cg in conditionGroups where cg.Sapcode == zstSoldTo.FreightIndicator select cg).FirstOrDefault();
                        SAPConditionGroup fuelSurchargeSAPConditionGroup = (from cg in conditionGroups where cg.Sapcode == zstSoldTo.FuelSurcharge select cg).FirstOrDefault();
                        SAPCustomerGroup SAPCustomerGroup = (from cg in customerGroups where cg.Sapcode == zstSoldTo.CustomerGroup select cg).FirstOrDefault();
                        SAPSalesGroup SAPSalesGroup = (from sg in salesGroups where sg.Sapcode == zstSoldTo.SalesGroup select sg).FirstOrDefault();

                        if (SAPSoldTo.IsNull())
                        {
                            SAPSoldTo = new SAPSoldTo();
                            SAPSoldTo.DivisionID = (long)Enums.Divisions.Atlas;
                            SAPSoldTo.Number = zstSoldTo.SoldToNumber.Trim();
                            shipTos.Add(SAPSoldTo);
                            db.SapshipTos.Add(SAPSoldTo);
                            insertedCount++;
                        }
                        else
                        {
                            checkedForUpdatesCount++;
                        }

                        SAPSoldTo.Name = zstSoldTo.Name;
                        SAPSoldTo.Address = zstSoldTo.Address;
                        SAPSoldTo.City = city;
                        SAPSoldTo.PostalCode = zstSoldTo.PostalCode;
                        SAPSoldTo.Phone = zstSoldTo.Phone;
                        SAPSoldTo.Fax = zstSoldTo.Fax;
                        SAPSoldTo.IncoTerms2 = zstSoldTo.IncoTerms2;
                        SAPSoldTo.Currency = zstSoldTo.Currency;
                        SAPSoldTo.DefaultSapshipTo = defaultSAPShipTo;
                        SAPSoldTo.CustomerSpecificPricing = zstSoldTo.CustomerSpecificPricing.ToBool();
                        SAPSoldTo.RegionSapconditionGroup = regionSAPConditionGroup;
                        SAPSoldTo.TierSapconditionGroup = tierSAPConditionGroup;
                        SAPSoldTo.HomeMillSapconditionGroup = homeMillSAPConditionGroup;
                        SAPSoldTo.FreightIndicatorSapconditionGroup = freightIndicatorSAPConditionGroup;
                        SAPSoldTo.FuelSurchargeSapconditionGroup = fuelSurchargeSAPConditionGroup;
                        SAPSoldTo.SapcustomerGroup = SAPCustomerGroup;
                        SAPSoldTo.SapsalesGroup = SAPSalesGroup;
                        SAPSoldTo.PricingProcedure = zstSoldTo.PricingProcedure;
                        //SAPSoldTo.Active = true;
                        SAPSoldTo.Active = zstSoldTo.Loevm == "X" ? false : true;
                    }
                }
            }

            List<SoldToShipTo> soldToShipTosToDelete = new List<SoldToShipTo>();

            foreach (SAPSoldTo SAPSoldTo in soldTos)
            {
                foreach (SAPShipTo SAPShipTo in SAPSoldTo.sapshipTos)
                {
                    SoldToShipTo soldToShipTo = new SoldToShipTo();
                    soldToShipTo.SAPSoldToID = SAPSoldTo.SapshipToID;
                    soldToShipTo.SAPShipToID = SAPShipTo.SapshipToID;

                    soldToShipTosToDelete.Add(soldToShipTo);
                }
            }

            foreach (AtlasMasterData.ZstSoldToShipTo zstSoldToShipTo in getAllSoldTosResponse.SoldTosShipTos)
            {
                SAPSoldTo SAPSoldTo = (from s in soldTos where s.Number == zstSoldToShipTo.SoldToNumber.Trim() select s).FirstOrDefault();
                SAPShipTo SAPShipTo = (from s in shipTos where s.Number == zstSoldToShipTo.ShipToNumber.Trim() select s).FirstOrDefault();

                if (!SAPSoldTo.IsNull() && !SAPShipTo.IsNull())
                {
                    if (!SAPSoldTo.sapshipTos.Contains(SAPShipTo))
                    {
                        SAPSoldTo.sapshipTos.Add(SAPShipTo);
                    }
                    else
                    {
                        SoldToShipTo currentSoldToShipTo = (from stst in soldToShipTosToDelete where stst.SAPSoldToID == SAPSoldTo.SapshipToID && stst.SAPShipToID == SAPShipTo.SapshipToID select stst).FirstOrDefault();

                        if (!currentSoldToShipTo.IsNull())
                        {
                            soldToShipTosToDelete.Remove(currentSoldToShipTo);
                        }
                    }
                }
            }


            foreach (SoldToShipTo soldToShipTo in soldToShipTosToDelete)
            {
                SAPSoldTo SAPSoldTo = (from s in soldTos where s.SapshipToID == soldToShipTo.SAPSoldToID select s).FirstOrDefault();

                if (!SAPSoldTo.IsNull() && SAPSoldTo.sapshipTos.Count() > 0)
                {
                    SAPShipTo SAPShipTo = (from s in SAPSoldTo.sapshipTos where s.SapshipToID == soldToShipTo.SAPShipToID select s).FirstOrDefault();

                    if (!SAPShipTo.IsNull())
                    {
                        SAPSoldTo.sapshipTos.Remove(SAPShipTo);
                        joinsRemovedCount++;
                        joinsRemovedStringBuilder.Append(SAPSoldTo.TrimmedNumber + " " + SAPSoldTo.Name + " - " + SAPShipTo.TrimmedNumber + " " + SAPShipTo.Name + "<br />");
                    }
                }
            }

            //leave this commented code 
            //foreach (SAPSoldTo SAPSoldTo in SAPSoldTo.ActiveList) {
            //  if (!SAPSoldToNumbers.Contains(SAPSoldTo.Number)) {
            //    disabledStringBuilder.Append("<span style=\"font-weight:bold;\">");
            //    disabledStringBuilder.Append(SAPSoldTo.TrimmedNumber + " " + SAPSoldTo.Name);
            //    disabledStringBuilder.Append("</span> is active on the Intranet, but not found in SAP download, disabling.<br />");

            //    SAPSoldTo.Active = false;
            //    SAPSoldTo.Save();

            //    disabledCount++;
            //  }
            //}


            db.SaveChanges();


            #region foreach (AtlasMasterData.ZstSoldToSalesOrg zstSoldToSalesOrg in getAllSoldTosResponse.SoldTosSalesOrgs)
            foreach (AtlasMasterData.ZstSoldToSalesOrg zstSoldToSalesOrg in getAllSoldTosResponse.SoldTosSalesOrgs)
            {
                SAPSoldTo SAPSoldTo = (from s in soldTos where s.Number == zstSoldToSalesOrg.SoldToNumber.Trim() select s).FirstOrDefault();
                SAPSalesOrganization SAPSalesOrganization = (from so in salesOrganizations where so.Sapcode == zstSoldToSalesOrg.SalesOrg.Trim() select so).FirstOrDefault();

                if (!SAPSoldTo.IsNull() && !SAPSalesOrganization.IsNull())
                {

                    SAPShipToSAPSalesOrganization SAPShipToSAPSalesOrganization = (from sso in SAPShipToSAPSalesOrganizations where sso.SapshipToID == SAPSoldTo.SapshipToID && sso.SapsalesOrganizationID == SAPSalesOrganization.SapsalesOrganizationID select sso).FirstOrDefault();

                    if (SAPShipToSAPSalesOrganization.IsNull())
                    {
                        SAPShipToSAPSalesOrganization = new SAPShipToSAPSalesOrganization();

                        SAPShipToSAPSalesOrganization.SapshipTo = SAPSoldTo;
                        SAPShipToSAPSalesOrganization.SapsalesOrganization = SAPSalesOrganization;

                        SAPShipToSAPSalesOrganizations.Add(SAPShipToSAPSalesOrganization);
                        db.SapshipToSapsalesOrganizations.Add(SAPShipToSAPSalesOrganization);
                    }

                    SAPShipToSAPSalesOrganization.IncoTerms1 = zstSoldToSalesOrg.IncoTerms1;
                    SAPShipToSAPSalesOrganization.IncoTerms2 = zstSoldToSalesOrg.IncoTerms2;
                    SAPShipToSAPSalesOrganization.Currency = zstSoldToSalesOrg.Currency;
                    SAPShipToSAPSalesOrganization.PricingProcedure = zstSoldToSalesOrg.PricingProcedure;
                    SAPShipToSAPSalesOrganization.SapcustomerGroup = (from cg in customerGroups where cg.Sapcode == zstSoldToSalesOrg.Osr select cg).FirstOrDefault();
                    SAPShipToSAPSalesOrganization.SapsalesGroup = (from sg in salesGroups where sg.Sapcode == zstSoldToSalesOrg.Isr select sg).FirstOrDefault();
                    //SAPShipToSAPSalesOrganization.SAPCustomerServiceRep = (from csr in customerGroups where csr.Sapcode == zstSoldToSalesOrg.Csr select csr).FirstOrDefault();
                    SAPShipToSAPSalesOrganization.Sapregion = (from r in regions where r.Sapcode == zstSoldToSalesOrg.Region select r).FirstOrDefault();
                    SAPShipToSAPSalesOrganization.Saptier = (from t in tiers where t.Sapcode == zstSoldToSalesOrg.Tier select t).FirstOrDefault();
                    SAPShipToSAPSalesOrganization.SappaymentTerms = (from pt in paymentTerms where pt.Sapcode == zstSoldToSalesOrg.PaymentTerm select pt).FirstOrDefault();
                    SAPShipToSAPSalesOrganization.Active = zstSoldToSalesOrg.Loevm == "X" ? false : true;
                    SAPShipToSAPSalesOrganization.OrderBlock = zstSoldToSalesOrg.Aufsd;
                    soldToSalesOrgJoin++;

                }

                SAPSoldTosalesorgcount++;

                if (SAPSoldTosalesorgcount % 100 == 0)
                {
                    db.SaveChanges();
                }
            }
            foreach (ZsdKnmtPortal zsdCMIR in getAllSoldTosResponse.CustMaterialInfo)
            {

                SAPSoldTo SAPSoldTo = (from s in soldTos where s.Number == zsdCMIR.Kunnr.Trim() select s).FirstOrDefault();
                SAPSalesOrganization SAPSalesOrganization = (from s in salesOrganizations where s.Name == zsdCMIR.Vkorg select s).FirstOrDefault();
                SAPMaterial SAPMaterial = SAPMaterials.Where(x => x.Number == zsdCMIR.Matnr).FirstOrDefault();

                if (!SAPSoldTo.IsNull() && !SAPMaterial.IsNull() && !SAPSalesOrganization.IsNull())
                {
                    Cmir CMIR = (from c in CMIRs
                                 where c.SapshipToID == SAPSoldTo.SapshipToID
                                                                                         && c.SapmaterialID == SAPMaterial.SapmaterialID && c.SapsalesOrganizationID == SAPSalesOrganization.SapsalesOrganizationID
                                 select c).FirstOrDefault();

                    if (CMIR.IsNull())
                    {
                        CMIR = new Cmir();
                        CMIR.SapsalesOrganizationID = SAPSalesOrganization.SapsalesOrganizationID;
                        CMIR.SapmaterialID = SAPMaterial.SapmaterialID;
                        CMIR.SapshipToID = SAPSoldTo.SapshipToID;
                        db.Cmirs.Add(CMIR);
                    }
                    CMIR.CustomerPartNumber = zsdCMIR.Kdmat;
                    CMIR.DistributionChannel = zsdCMIR.Vtweg;
                }

            }
            #endregion
            db.SaveChanges();

            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);

            RefreshQMSoldtoFromPortal(db);

            emailStringBuilder.Append(insertedCount);
            emailStringBuilder.Append(" inserted.<br />");
            emailStringBuilder.Append(checkedForUpdatesCount);
            emailStringBuilder.Append(" checked for updates.<br />");
            emailStringBuilder.Append(disabledCount);
            emailStringBuilder.Append(" disabled.<br />");
            emailStringBuilder.Append(joinsCount);
            emailStringBuilder.Append(" joins created between SAP Sold Tos and SAP Ship Tos.<br />");
            emailStringBuilder.Append(joinsRemovedCount);
            emailStringBuilder.Append(" joins removed between SAP Sold Tos and SAP Ship Tos.<br />");
            emailStringBuilder.Append(SAPCustomerGroupCount);
            emailStringBuilder.Append(" SAP Customer Groups inserted/updated.<br />");
            emailStringBuilder.Append(SAPSalesGroupCount);
            emailStringBuilder.Append(" SAP Sales Groups inserted/updated.<br />");

            emailStringBuilder.Append(SAPSoldTosalesorgcount);
            emailStringBuilder.Append(" SAP Soldto SalesOrg inserted/updated.<br />");
            emailStringBuilder.Append("Run Time " + runTime.Days);
            emailStringBuilder.Append("days " + runTime.Hours);
            emailStringBuilder.Append("hours " + runTime.Minutes);
            emailStringBuilder.Append("minutes " + runTime.Seconds);
            emailStringBuilder.Append("seconds");

            if (joinsRemovedCount > 0)
            {
                emailStringBuilder.Append("<br /><br /><br />Joins Removed:<br /><br />");
                emailStringBuilder.Append(joinsRemovedStringBuilder.ToString());
            }

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas Sold To Download Results", emailStringBuilder.ToString());
        }

        public static new void RefreshFromWheatlandSAP(string email)
        {
            SAPShipTo.RefreshFromWheatlandSAP(email);

            PortalEntities db = new PortalEntities();

            int insertedCount = 0;
            int checkedForUpdatesCount = 0;
            int joinsCount = 0;
            int joinsRemovedCount = 0;
            int SAPSalesGroupCount = 0;
            int SAPCustomerGroupCount = 0;
            int disabledCount = 0;
            int soldToSalesOrgJoin = 0;

            ArrayList SAPSoldToNumbers = new ArrayList();

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            StringBuilder joinsRemovedStringBuilder = new StringBuilder();
            StringBuilder disabledStringBuilder = new StringBuilder();

            zws_portalClient portalService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
            portalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
            portalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];

            WheatlandPortal.ZGetAllSalesTexts getAllSalesTexts = new WheatlandPortal.ZGetAllSalesTexts();

            WheatlandPortal.ZGetAllSoldTos getAllSoldTos = new WheatlandPortal.ZGetAllSoldTos();
            getAllSoldTos.SalesReps = new WheatlandPortal.ZstSalesRep[] { };
            getAllSoldTos.SoldTos = new WheatlandPortal.ZstSoldTo[] { };
            getAllSoldTos.SoldTosShipTos = new WheatlandPortal.ZstSoldToShipTo[] { };
            getAllSoldTos.SoldTosSalesOrgs = new WheatlandPortal.ZstSoldToSalesOrg[] { };
            getAllSoldTos.PaymentTerms = new WheatlandPortal.ZstPaymentTerm[] { };

            portalService.Open();
            WheatlandPortal.ZGetAllSoldTosResponse getAllSoldTosResponse = portalService.ZGetAllSoldTosAsync(getAllSoldTos);
            WheatlandPortal.ZGetAllSalesTextsResponse getAllSalesTextsResponse = portalService.ZGetAllSalesTextsAsync(getAllSalesTexts);
            portalService.Close();

            List<SAPCharacteristicOption> customerTextTypes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.CustomerTextType select co).ToList();
            List<City> cities = (from c in db.Cities select c).ToList();
            List<State> states = (from s in db.States select s).ToList();
            List<Country> countries = (from c in db.Countries select c).ToList();
            List<SAPSoldTo> soldTos = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.DivisionID == (long)Enums.Divisions.Wheatland select s).ToList();
            List<SAPShipTo> shipTos = (from s in db.SapshipTos where s.DivisionID == (long)Enums.Divisions.Wheatland select s).ToList();
            List<SAPSalesOrganization> salesOrganizations = (from so in db.SapsalesOrganizations where so.DivisionID == (long)Enums.Divisions.Wheatland select so).ToList();
            List<SAPCustomerGroup> customerGroups = (from cg in db.SapcustomerGroups where cg.DivisionID == (long)Enums.Divisions.Wheatland select cg).ToList();
            List<SAPSalesGroup> salesGroups = (from sg in db.SapsalesGroups where sg.DivisionID == (long)Enums.Divisions.Wheatland select sg).ToList();
            List<SAPCustomerServiceRep> customerServiceReps = (from csr in db.SapcustomerServiceReps where csr.DivisionID == (long)Enums.Divisions.Wheatland select csr).ToList();
            List<SAPRegion> regions = (from r in db.Sapregions where r.DivisionID == (long)Enums.Divisions.Wheatland select r).ToList();
            List<SAPTier> tiers = (from t in db.Saptiers where t.DivisionID == (long)Enums.Divisions.Wheatland select t).ToList();
            List<Employee> employees = (from e in db.Users.OfType<Employee>() select e).ToList();
            List<SAPShipToSAPSalesOrganization> SAPShipToSAPSalesOrganizations = (from sso in db.SapshipToSapsalesOrganizations where sso.SapsalesOrganization.DivisionID == (long)Enums.Divisions.Wheatland select sso).ToList();
            List<SAPCharacteristicOption> paymentTerms = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.PaymentTerms select co).ToList();

            #region if (!getAllSoldTosResponse.PaymentTerms.IsNull())
            if (!getAllSoldTosResponse.PaymentTerms.IsNull())
            {
                foreach (ZstPaymentTerm zstPaymentTerm in getAllSoldTosResponse.PaymentTerms)
                {
                    SAPCharacteristicOption paymentTerm = (from pt in paymentTerms where pt.Sapcode == zstPaymentTerm.Code select pt).FirstOrDefault();

                    if (paymentTerm.IsNull())
                    {
                        paymentTerm = new SAPCharacteristicOption();
                        paymentTerm.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.PaymentTerms;
                        paymentTerm.Sapcode = zstPaymentTerm.Code;
                        paymentTerm.Name = zstPaymentTerm.Name;

                        paymentTerms.Add(paymentTerm);
                        db.SapcharacteristicOptions.Add(paymentTerm);
                    }
                }
            }
            #endregion
            db.SaveChanges();
            #region foreach (ZstSalesRep zstSalesRep in getAllSoldTosResponse.SalesReps.Where(r => r.PartnerFunction == "ZE"))
            foreach (ZstSalesRep zstSalesRep in getAllSoldTosResponse.SalesReps.Where(r => r.PartnerFunction == "ZE"))
            {
                SAPCustomerGroup SAPCustomerGroup = (from cg in customerGroups where cg.Sapcode == zstSalesRep.SalesRepNumber select cg).FirstOrDefault();

                if (SAPCustomerGroup.IsNull())
                {
                    SAPCustomerGroup = new SAPCustomerGroup();
                    SAPCustomerGroup.DivisionID = (long)Enums.Divisions.Wheatland;
                    SAPCustomerGroup.Sapcode = zstSalesRep.SalesRepNumber;

                    customerGroups.Add(SAPCustomerGroup);
                    db.SapcustomerGroups.Add(SAPCustomerGroup);
                }

                string[] nameArray = zstSalesRep.Name.Split(" ".ToCharArray());
                string firstName = string.Empty;
                string lastName = string.Empty;

                if (nameArray.Length >= 2)
                {
                    firstName = nameArray[0];
                    lastName = nameArray[1];

                    int slashPosition = lastName.LastIndexOf("/");

                    if (slashPosition > 0)
                    {
                        lastName = lastName.Remove(slashPosition);
                    }
                }

                Employee employee = (from e in employees where e.FirstName.ToLower() == firstName.ToLower() && e.LastName.ToLower() == lastName.ToLower() select e).FirstOrDefault();

                SAPCustomerGroup.Name = zstSalesRep.Name.Trim();

                if (!employee.IsNull())
                {
                    SAPCustomerGroup.User = employee;
                }

                SAPSalesOrganization SAPSalesOrganization = (from so in salesOrganizations where so.Sapcode == zstSalesRep.SalesOrg.Trim() select so).FirstOrDefault();

                if (!SAPSalesOrganization.IsNull() && !SAPCustomerGroup.SapsalesOrganization.Contains(SAPSalesOrganization))
                {
                    SAPCustomerGroup.SapsalesOrganization.Add(SAPSalesOrganization);
                }

                SAPCustomerGroupCount++;
            }
            #endregion
            db.SaveChanges();
            #region foreach (ZstSalesRep zstSalesRep in getAllSoldTosResponse.SalesReps.Where(r => r.PartnerFunction == "ZI"))
            foreach (ZstSalesRep zstSalesRep in getAllSoldTosResponse.SalesReps.Where(r => r.PartnerFunction == "ZI"))
            {
                SAPSalesGroup SAPSalesGroup = (from sg in salesGroups where sg.Sapcode == zstSalesRep.SalesRepNumber select sg).FirstOrDefault();

                if (SAPSalesGroup.IsNull())
                {
                    SAPSalesGroup = new SAPSalesGroup();
                    SAPSalesGroup.DivisionID = (long)Enums.Divisions.Wheatland;
                    SAPSalesGroup.Sapcode = zstSalesRep.SalesRepNumber;

                    salesGroups.Add(SAPSalesGroup);
                    db.SapsalesGroups.Add(SAPSalesGroup);
                }

                string[] nameArray = zstSalesRep.Name.Split(" ".ToCharArray());
                string firstName = string.Empty;
                string lastName = string.Empty;

                if (nameArray.Length >= 2)
                {
                    firstName = nameArray[0];
                    lastName = nameArray[1];

                    int slashPosition = lastName.LastIndexOf("/");

                    if (slashPosition > 0)
                    {
                        lastName = lastName.Remove(slashPosition);
                    }
                }

                Employee employee = (from e in employees where e.FirstName.ToLower() == firstName.ToLower() && e.LastName.ToLower() == lastName.ToLower() select e).FirstOrDefault();

                SAPSalesGroup.Name = zstSalesRep.Name.Trim();

                if (!employee.IsNull())
                {
                    SAPSalesGroup.User = employee;
                }

                SAPSalesOrganization SAPSalesOrganization = (from so in salesOrganizations where so.Sapcode == zstSalesRep.SalesOrg.Trim() select so).FirstOrDefault();

                if (!SAPSalesOrganization.IsNull() && !SAPSalesGroup.SapsalesOrganization.Contains(SAPSalesOrganization))
                {
                    SAPSalesGroup.SapsalesOrganization.Add(SAPSalesOrganization);
                }

                SAPSalesGroupCount++;
            }
            #endregion
            db.SaveChanges();
            #region foreach (ZstSalesRep zstSalesRep in getAllSoldTosResponse.SalesReps.Where(r => r.PartnerFunction == "ZR"))
            foreach (ZstSalesRep zstSalesRep in getAllSoldTosResponse.SalesReps.Where(r => r.PartnerFunction == "ZR"))
            {
                SAPCustomerServiceRep SAPCustomerServiceRep = (from csr in customerServiceReps where csr.Sapcode == zstSalesRep.SalesRepNumber select csr).FirstOrDefault();

                if (SAPCustomerServiceRep.IsNull())
                {
                    SAPCustomerServiceRep = new SAPCustomerServiceRep();
                    SAPCustomerServiceRep.DivisionID = (long)Enums.Divisions.Wheatland;
                    SAPCustomerServiceRep.Sapcode = zstSalesRep.SalesRepNumber;

                    customerServiceReps.Add(SAPCustomerServiceRep);
                    db.SapcustomerServiceReps.Add(SAPCustomerServiceRep);
                }

                string[] nameArray = zstSalesRep.Name.Split(" ".ToCharArray());
                string firstName = string.Empty;
                string lastName = string.Empty;

                if (nameArray.Length >= 2)
                {
                    firstName = nameArray[0];
                    lastName = nameArray[1];

                    int slashPosition = lastName.LastIndexOf("/");

                    if (slashPosition > 0)
                    {
                        lastName = lastName.Remove(slashPosition);
                    }
                }

                Employee employee = (from e in employees where e.FirstName.ToLower() == firstName.ToLower() && e.LastName.ToLower() == lastName.ToLower() select e).FirstOrDefault();

                SAPCustomerServiceRep.Name = zstSalesRep.Name.Trim();

                if (!employee.IsNull())
                {
                    SAPCustomerServiceRep.User = employee;
                }

                SAPSalesOrganization SAPSalesOrganization = (from so in salesOrganizations where so.Sapcode == zstSalesRep.SalesOrg.Trim() select so).FirstOrDefault();

                if (!SAPSalesOrganization.IsNull() && !SAPCustomerServiceRep.SapsalesOrganization.Contains(SAPSalesOrganization))
                {
                    SAPCustomerServiceRep.SapsalesOrganization.Add(SAPSalesOrganization);
                }
            }
            #endregion
            db.SaveChanges();
            #region foreach (ZstSalesRep zstSalesRep in getAllSoldTosResponse.SalesReps.Where(r => r.PartnerFunction == "YR"))
            foreach (ZstSalesRep zstSalesRep in getAllSoldTosResponse.SalesReps.Where(r => r.PartnerFunction == "YR"))
            {
                SAPRegion SAPRegion = (from r in regions where r.Sapcode == zstSalesRep.SalesRepNumber select r).FirstOrDefault();

                if (SAPRegion.IsNull())
                {
                    SAPRegion = new SAPRegion();
                    SAPRegion.DivisionID = (long)Enums.Divisions.Wheatland;
                    SAPRegion.Sapcode = zstSalesRep.SalesRepNumber;
                    SAPRegion.Name = zstSalesRep.Name;

                    regions.Add(SAPRegion);
                    db.Sapregions.Add(SAPRegion);
                }

                SAPSalesOrganization SAPSalesOrganization = (from so in salesOrganizations where so.Sapcode == zstSalesRep.SalesOrg.Trim() select so).FirstOrDefault();

                if (!SAPSalesOrganization.IsNull() && !SAPRegion.SapsalesOrganization.Contains(SAPSalesOrganization))
                {
                    SAPRegion.SapsalesOrganization.Add(SAPSalesOrganization);
                }
            }
            #endregion
            db.SaveChanges();
            #region foreach (ZstSalesRep zstSalesRep in getAllSoldTosResponse.SalesReps.Where(r => r.PartnerFunction == "YT"))
            foreach (ZstSalesRep zstSalesRep in getAllSoldTosResponse.SalesReps.Where(r => r.PartnerFunction == "YT"))
            {
                SAPTier SAPTier = (from t in tiers where t.Sapcode == zstSalesRep.SalesRepNumber select t).FirstOrDefault();

                if (SAPTier.IsNull())
                {
                    SAPTier = new SAPTier();
                    SAPTier.DivisionID = (long)Enums.Divisions.Wheatland;
                    SAPTier.Sapcode = zstSalesRep.SalesRepNumber;
                    SAPTier.Name = zstSalesRep.Name;

                    tiers.Add(SAPTier);
                    db.Saptiers.Add(SAPTier);
                }

                SAPSalesOrganization SAPSalesOrganization = (from so in salesOrganizations where so.Sapcode == zstSalesRep.SalesOrg.Trim() select so).FirstOrDefault();

                if (!SAPSalesOrganization.IsNull() && !SAPTier.SapsalesOrganizations.Contains(SAPSalesOrganization))
                {
                    SAPTier.SapsalesOrganizations.Add(SAPSalesOrganization);
                }
            }
            #endregion
            db.SaveChanges();

            int count = 0;

            #region foreach (WheatlandPortal.ZstSoldTo zstSoldTo in getAllSoldTosResponse.SoldTos)
            foreach (WheatlandPortal.ZstSoldTo zstSoldTo in getAllSoldTosResponse.SoldTos)
            {
                SAPSoldToNumbers.Add(zstSoldTo.SoldToNumber.Trim());

                string countryAbbr = zstSoldTo.Country.Trim();
                string stateAbbr = (countryAbbr.ToLower() == "pr" && zstSoldTo.State.Trim().jIsEmpty()) ? "PR" : zstSoldTo.State.Trim();
                string cityName = zstSoldTo.City.Trim();

                SAPSoldTo SAPSoldTo = (from s in soldTos where s.Number == zstSoldTo.SoldToNumber.Trim() select s).FirstOrDefault();

                if (!countryAbbr.jIsEmpty() && !stateAbbr.jIsEmpty() && !cityName.jIsEmpty())
                {
                    Country country = (from c in countries where c.Abbreviation.ToLower() == countryAbbr.ToLower() select c).FirstOrDefault();

                    if (country.IsNull())
                    {
                        country = new Country();
                        country.Abbreviation = countryAbbr;
                        country.Name = countryAbbr;
                        country.Active = true;
                        countries.Add(country);
                        db.Countries.Add(country);
                    }

                    State state = (from s in states where s.Abbreviation.ToLower() == stateAbbr.ToLower() && s.CountryID == country.CountryID select s).FirstOrDefault();

                    if (state.IsNull())
                    {
                        state = new State();
                        state.Abbreviation = stateAbbr;
                        state.Name = stateAbbr;
                        state.Country = country;
                        state.Active = true;
                        states.Add(state);
                        db.States.Add(state);
                    }

                    City city = (from c in cities where c.Name.ToLower() == cityName.ToLower() && c.StateID == state.StateID select c).FirstOrDefault();

                    if (city.IsNull())
                    {
                        city = new City();
                        city.Name = cityName;
                        city.State = state;
                        city.Active = true;
                        cities.Add(city);
                        db.Cities.Add(city);
                    }

                    if (!city.IsNull())
                    {
                        //SAPShipTo defaultSAPShipTo = (from s in shipTos where s.Number == zstSoldTo.DefaultShipTo.Trim() select s).FirstOrDefault();
                        //SAPCustomerGroup SAPCustomerGroup = (from cg in customerGroups where cg.Sapcode == zstSoldTo.Osr select cg).FirstOrDefault();
                        //SAPSalesGroup SAPSalesGroup = (from sg in salesGroups where sg.Sapcode == zstSoldTo.Isr select sg).FirstOrDefault();

                        if (SAPSoldTo.IsNull())
                        {
                            SAPSoldTo = new SAPSoldTo();
                            SAPSoldTo.DivisionID = (long)Enums.Divisions.Wheatland;
                            SAPSoldTo.Number = zstSoldTo.SoldToNumber.Trim();
                            soldTos.Add(SAPSoldTo);
                            db.SapshipTos.Add(SAPSoldTo);
                            insertedCount++;
                        }
                        else
                        {
                            checkedForUpdatesCount++;
                        }

                        if (SAPSoldTo.Name.IsNull() || SAPSoldTo.Name.CompareTo(zstSoldTo.Name) != 0)
                            SAPSoldTo.Name = zstSoldTo.Name;
                        if (SAPSoldTo.Name2.IsNull() || SAPSoldTo.Name2.CompareTo(zstSoldTo.Name2) != 0)
                            SAPSoldTo.Name2 = zstSoldTo.Name2;
                        if (SAPSoldTo.Address.IsNull() || SAPSoldTo.Address.CompareTo(zstSoldTo.Address) != 0)
                            SAPSoldTo.Address = zstSoldTo.Address;
                        if (SAPSoldTo.City.IsNull() || SAPSoldTo.CityID != city.CityID)
                            SAPSoldTo.City = city;
                        if (SAPSoldTo.PostalCode.IsNull() || SAPSoldTo.PostalCode.CompareTo(zstSoldTo.PostalCode) != 0)
                            SAPSoldTo.PostalCode = zstSoldTo.PostalCode;
                        if (SAPSoldTo.Phone.IsNull() || SAPSoldTo.Phone.CompareTo(zstSoldTo.Phone) != 0)
                            SAPSoldTo.Phone = zstSoldTo.Phone;
                        if (SAPSoldTo.Fax.IsNull() || SAPSoldTo.Fax.CompareTo(zstSoldTo.Fax) != 0)
                            SAPSoldTo.Fax = zstSoldTo.Fax;
                        if (SAPSoldTo.IncoTerms2.IsNull() || (SAPSoldTo.IncoTerms2 ?? "").CompareTo(string.Empty) != 0)
                            SAPSoldTo.IncoTerms2 = string.Empty;
                        if (SAPSoldTo.Currency.IsNull() || (SAPSoldTo.Currency ?? "").CompareTo(string.Empty) != 0)
                            SAPSoldTo.Currency = string.Empty;
                        //SAPSoldTo.DefaultSAPShipTo = defaultsapShipTo;
                        if ((SAPSoldTo.PricingProcedure).TrimNull().CompareTo(zstSoldTo.PricingProcedure.Trim()) != 0)
                            SAPSoldTo.PricingProcedure = zstSoldTo.PricingProcedure.Trim();
                        if (!SAPSoldTo.Active)
                            SAPSoldTo.Active = true;
                    }
                }

                count++;

                if (count % 100 == 0)
                {
                    db.SaveChanges();
                }
            }
            #endregion
            db.SaveChanges();

            count = 0;

            #region foreach (WheatlandPortal.ZstSoldToSalesOrg zstSoldToSalesOrg in getAllSoldTosResponse.SoldTosSalesOrgs)
            foreach (WheatlandPortal.ZstSoldToSalesOrg zstSoldToSalesOrg in getAllSoldTosResponse.SoldTosSalesOrgs)
            {
                SAPSoldTo SAPSoldTo = (from s in soldTos where s.Number == zstSoldToSalesOrg.SoldToNumber.Trim() select s).FirstOrDefault();
                SAPSalesOrganization SAPSalesOrganization = (from so in salesOrganizations where so.Sapcode == zstSoldToSalesOrg.SalesOrg.Trim() select so).FirstOrDefault();

                if (!SAPSoldTo.IsNull() && !SAPSalesOrganization.IsNull())
                {
                    SAPShipToSAPSalesOrganization SAPShipToSAPSalesOrganization = (from sso in SAPShipToSAPSalesOrganizations where sso.SapshipToID == SAPSoldTo.SapshipToID && sso.SapsalesOrganizationID == SAPSalesOrganization.SapsalesOrganizationID select sso).FirstOrDefault();

                    if (SAPShipToSAPSalesOrganization.IsNull())
                    {
                        SAPShipToSAPSalesOrganization = new SAPShipToSAPSalesOrganization();

                        SAPShipToSAPSalesOrganization.SapshipTo = SAPSoldTo;
                        SAPShipToSAPSalesOrganization.SapsalesOrganization = SAPSalesOrganization;

                        SAPShipToSAPSalesOrganizations.Add(SAPShipToSAPSalesOrganization);
                        db.SapshipToSapsalesOrganizations.Add(SAPShipToSAPSalesOrganization);
                    }

                    SAPShipToSAPSalesOrganization.IncoTerms1 = zstSoldToSalesOrg.IncoTerms1;
                    SAPShipToSAPSalesOrganization.IncoTerms2 = zstSoldToSalesOrg.IncoTerms2;
                    SAPShipToSAPSalesOrganization.Currency = zstSoldToSalesOrg.Currency;
                    SAPShipToSAPSalesOrganization.PricingProcedure = zstSoldToSalesOrg.PricingProcedure;
                    SAPShipToSAPSalesOrganization.SapcustomerGroup = (from cg in customerGroups where cg.Sapcode == zstSoldToSalesOrg.Osr select cg).FirstOrDefault();
                    SAPShipToSAPSalesOrganization.SapsalesGroup = (from sg in salesGroups where sg.Sapcode == zstSoldToSalesOrg.Isr select sg).FirstOrDefault();
                    SAPShipToSAPSalesOrganization.SapcustomerServiceRep = (from csr in customerServiceReps where csr.Sapcode == zstSoldToSalesOrg.Csr select csr).FirstOrDefault();
                    SAPShipToSAPSalesOrganization.Sapregion = (from r in regions where r.Sapcode == zstSoldToSalesOrg.Region select r).FirstOrDefault();
                    SAPShipToSAPSalesOrganization.Saptier = (from t in tiers where t.Sapcode == zstSoldToSalesOrg.Tier select t).FirstOrDefault();
                    SAPShipToSAPSalesOrganization.SappaymentTerms = (from pt in paymentTerms where pt.Sapcode == zstSoldToSalesOrg.PaymentTerm select pt).FirstOrDefault();

                    soldToSalesOrgJoin++;
                }

                count++;

                if (count % 100 == 0)
                {
                    db.SaveChanges();
                }
            }
            #endregion
            db.SaveChanges();


            List<SoldToShipTo> soldToShipTosToDelete = new List<SoldToShipTo>();
            //List<SoldToShipTo> soldToShipTosToDelete = db.getSoldToShipTos(Enums.Divisions.Wheatland);

            foreach (SAPSoldTo SAPSoldTo in soldTos)
            {
                foreach (SAPShipTo SAPShipTo in SAPSoldTo.sapshipTos)
                {
                    SoldToShipTo soldToShipTo = new SoldToShipTo();
                    soldToShipTo.SAPSoldToID = SAPSoldTo.SapshipToID;
                    soldToShipTo.SAPShipToID = SAPShipTo.SapshipToID;

                    soldToShipTosToDelete.Add(soldToShipTo);
                }
            }

            foreach (WheatlandPortal.ZstSoldToShipTo zstSoldToShipTo in getAllSoldTosResponse.SoldTosShipTos)
            {
                SAPSoldTo SAPSoldTo = (from s in soldTos where s.Number == zstSoldToShipTo.SoldToNumber.Trim() select s).FirstOrDefault();
                SAPShipTo SAPShipTo = (from s in shipTos where s.Number == zstSoldToShipTo.ShipToNumber.Trim() select s).FirstOrDefault();

                if (!SAPSoldTo.IsNull() && !SAPShipTo.IsNull())
                {
                    if (!SAPSoldTo.sapshipTos.Contains(SAPShipTo))
                    {
                        SAPSoldTo.sapshipTos.Add(SAPShipTo);
                    }
                    else
                    {
                        SoldToShipTo currentSoldToShipTo = (from stst in soldToShipTosToDelete where stst.SAPSoldToID == SAPSoldTo.SapshipToID && stst.SAPShipToID == SAPShipTo.SapshipToID select stst).FirstOrDefault();

                        if (!currentSoldToShipTo.IsNull())
                        {
                            soldToShipTosToDelete.Remove(currentSoldToShipTo);
                        }
                    }
                }
            }

            foreach (SoldToShipTo soldToShipTo in soldToShipTosToDelete)
            {
                SAPSoldTo SAPSoldTo = (from s in soldTos where s.SapshipToID == soldToShipTo.SAPSoldToID select s).FirstOrDefault();

                if (!SAPSoldTo.IsNull() && SAPSoldTo.sapshipTos.Count() > 0)
                {
                    SAPShipTo SAPShipTo = (from s in SAPSoldTo.sapshipTos where s.SapshipToID == soldToShipTo.SAPShipToID select s).FirstOrDefault();

                    if (!SAPShipTo.IsNull())
                    {
                        SAPSoldTo.sapshipTos.Remove(SAPShipTo);
                        joinsRemovedCount++;
                        joinsRemovedStringBuilder.Append(SAPSoldTo.TrimmedNumber + " " + SAPSoldTo.Name + " - " + SAPShipTo.TrimmedNumber + " " + SAPShipTo.Name + "<br />");
                    }
                }
            }

            //List<SoldToShipTo> soldToShipTosToDelete = db.getSoldToShipTos(Enums.Divisions.Wheatland);

            //foreach (IGrouping<string, WheatlandPortal.ZstSoldToShipTo> shipTosBySoldTo in getAllSoldTosResponse.SoldTosShipTos.GroupBy(x => x.SoldToNumber)) {
            //  SAPSoldTo SAPSoldTo = (from x in soldTos where x.Number == shipTosBySoldTo.Key select x).FirstOrDefault();

            //  if (!SAPSoldTo.IsNull()) {
            //    foreach (WheatlandPortal.ZstSoldToShipTo zstSoldToShipTo in shipTosBySoldTo) {
            //      SAPShipTo SAPShipTo = (from s in shipTos where s.Number == zstSoldToShipTo.ShipToNumber.Trim() select s).FirstOrDefault();

            //      if (!SAPShipTo.IsNull()) {
            //        SoldToShipTo findit = (from x in soldToShipTosToDelete where x.SAPSoldToID == SAPSoldTo.SAPShipToID && x.SAPShipToID == SAPShipTo.SAPShipToID select x).FirstOrDefault();

            //        if (!findit.IsNull()) {
            //          soldToShipTosToDelete.Remove(findit);
            //        } else {
            //          joinsCount++;
            //          SAPSoldTo.SAPShipToes.Add(SAPShipTo);
            //        }
            //      }
            //    }
            //  }
            //}

            //foreach (IGrouping<long, SoldToShipTo> soldToGroup in soldToShipTosToDelete.GroupBy(x => x.SAPSoldToID)) {
            //  SAPSoldTo soldto = (from x in soldTos where x.SAPShipToID == soldToGroup.Key select x).FirstOrDefault();
            //  if (!soldto.IsNull()) {
            //    foreach (SoldToShipTo soldToShipTo in soldToGroup) {
            //      SAPShipTo shipTo = (from x in shipTos where x.SAPShipToID == soldToShipTo.SAPShipToID select x).FirstOrDefault();
            //      if (!shipTo.IsNull()) {
            //        soldto.SAPShipToes.Remove(shipTo);
            //        joinsRemovedCount++;
            //      }
            //    }
            //  }
            //}

            db.SaveChanges();

            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);

            emailStringBuilder.Append(insertedCount);
            emailStringBuilder.Append(" inserted.<br />");
            emailStringBuilder.Append(checkedForUpdatesCount);
            emailStringBuilder.Append(" checked for updates.<br />");
            emailStringBuilder.Append(disabledCount);
            emailStringBuilder.Append(" disabled.<br />");
            emailStringBuilder.Append(joinsCount);
            emailStringBuilder.Append(" joins created between SAP Sold Tos and SAP Ship Tos.<br />");
            emailStringBuilder.Append(joinsRemovedCount);
            emailStringBuilder.Append(" joins removed between SAP Sold Tos and SAP Ship Tos.<br />");
            emailStringBuilder.Append(SAPCustomerGroupCount);
            emailStringBuilder.Append(" SAP Customer Groups inserted/updated.<br />");
            emailStringBuilder.Append(SAPSalesGroupCount);
            emailStringBuilder.Append(" SAP Sales Groups inserted/updated.<br />");
            emailStringBuilder.Append(soldToSalesOrgJoin);
            emailStringBuilder.Append(" joins between Sold Tos and Sales Organizations.<br />");

            emailStringBuilder.Append("Run Time " + runTime.Days);
            emailStringBuilder.Append("days " + runTime.Hours);
            emailStringBuilder.Append("hours " + runTime.Minutes);
            emailStringBuilder.Append("minutes " + runTime.Seconds);
            emailStringBuilder.Append("seconds");

            if (joinsRemovedCount > 0)
            {
                emailStringBuilder.Append("<br /><br /><br />Joins Removed:<br /><br />");
                emailStringBuilder.Append(joinsRemovedStringBuilder.ToString());
            }

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Wheatland Sold To Download Results", emailStringBuilder.ToString());

            foreach (ZstSalesTextIds zstSalesTextIds in getAllSalesTextsResponse.ExSalesTextIds)
            {
                SAPCharacteristicOption customerTextType = (from ctt in customerTextTypes where ctt.Sapcode == zstSalesTextIds.Id select ctt).FirstOrDefault();

                if (customerTextType.IsNull())
                {
                    customerTextType = new SAPCharacteristicOption();
                    customerTextType.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.CustomerTextType;
                    customerTextType.Sapcode = zstSalesTextIds.Id;
                    customerTextType.Name = zstSalesTextIds.Text;

                    customerTextTypes.Add(customerTextType);
                    db.SapcharacteristicOptions.Add(customerTextType);
                }
            }

            List<SAPCustomerText> CustomerTextByShipTo = new List<SAPCustomerText>();
            foreach (IGrouping<string, ZstCustomerSalesTexts> salesTextByCustomer in getAllSalesTextsResponse.ExSalesTexts.GroupBy(x => x.Customer))
            {

                SAPShipTo SAPShipTo = (from s in shipTos where s.Number == salesTextByCustomer.Key select s).FirstOrDefault();
                CustomerTextByShipTo.Clear();

                if (!SAPShipTo.IsNull())
                {
                    CustomerTextByShipTo = (from sct in db.SapcustomerTexts where sct.SapshipToID == SAPShipTo.SapshipToID select sct).ToList();

                    foreach (ZstCustomerSalesTexts zstCustomerSalesTexts in salesTextByCustomer)
                    {
                        if (!zstCustomerSalesTexts.Text.jIsEmpty())
                        {

                            int LineNumber = zstCustomerSalesTexts.LineNumber.Trim().ToInt();
                            SAPCharacteristicOption customerTextType = (from ctt in customerTextTypes where ctt.Sapcode == zstCustomerSalesTexts.TextId select ctt).FirstOrDefault();
                            SAPSalesOrganization SAPSalesOrganization = (from so in salesOrganizations where so.Sapcode == zstCustomerSalesTexts.SalesOrg.Trim() select so).FirstOrDefault();

                            if (!customerTextType.IsNull() && !SAPShipTo.IsNull() && !SAPSalesOrganization.IsNull())
                            {
                                SAPCustomerText SAPCustomerText = (from sct in CustomerTextByShipTo
                                                                   where
                                                                                                                                    sct.SapcustomerTextTypeID == customerTextType.SapcharacteristicOptionID && sct.LineNumber == LineNumber && sct.SapshipToID == SAPShipTo.SapshipToID &&
                                                                                                                                    sct.SapsalesOrganizationID == SAPSalesOrganization.SapsalesOrganizationID
                                                                   select sct).FirstOrDefault();
                                if (SAPCustomerText.IsNull())
                                {
                                    SAPCustomerText = new SAPCustomerText();
                                    SAPCustomerText.SapcustomerTextTypeID = customerTextType.SapcharacteristicOptionID;
                                    SAPCustomerText.LineNumber = LineNumber;
                                    SAPCustomerText.SapshipToID = SAPShipTo.SapshipToID;
                                    SAPCustomerText.SapsalesOrganizationID = SAPSalesOrganization.SapsalesOrganizationID;
                                    SAPCustomerText.Text = "";

                                    CustomerTextByShipTo.Add(SAPCustomerText);
                                    db.SapcustomerTexts.Add(SAPCustomerText);
                                }

                                if (SAPCustomerText.Text.CompareTo(zstCustomerSalesTexts.Text) != 0)
                                {
                                    SAPCustomerText.Text = zstCustomerSalesTexts.Text;
                                }
                            }
                        }
                    }
                }
            }

            db.SaveChanges();
        }

        public static void RefreshQMSoldtoFromPortal(PortalEntities db)
        {
            List<SAPSoldTo> portalsoldTos = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.DivisionID == (long)Enums.Divisions.Atlas select s).ToList();
            List<SAPSalesGroup> portalsalesgroups = (from s in db.SapsalesGroups where s.DivisionID == (long)Enums.Divisions.Atlas select s).ToList();
            List<SAPCustomerGroup> portalcustomergroups = (from s in db.SapcustomerGroups where s.DivisionID == (long)Enums.Divisions.Atlas select s).ToList();
            List<Employee> employees = (from e in db.Users.OfType<Employee>() select e).ToList();

            IntranetEntities.IntranetEntities intranetdb = new IntranetEntities.IntranetEntities();
            List<IntranetEntities.Customer> customers = (from c in intranetdb.Customers select c).ToList();
            List<IntranetEntities.City> cities = (from c in intranetdb.Cities select c).ToList();
            List<IntranetEntities.State> states = (from s in intranetdb.States select s).ToList();
            List<IntranetEntities.Country> countries = (from c in intranetdb.Countries select c).ToList();
            List<IntranetEntities.OutsideSalesRep> OutsideSalesReps = (from cg in intranetdb.OutsideSalesReps select cg).ToList();
            List<IntranetEntities.InsideSalesRep> InsideSalesReps = (from cg in intranetdb.InsideSalesReps select cg).ToList();
            List<IntranetEntities.User> QMemployeesuser = (from e in intranetdb.Users select e).ToList();


            foreach (SAPCustomerGroup osr in portalcustomergroups)
            {
                IntranetEntities.OutsideSalesRep OutsideSalesRep = (from cg in intranetdb.OutsideSalesReps where cg.SAPCustomerGroupID == osr.Sapcode select cg).FirstOrDefault();

                if (OutsideSalesRep.IsNull())
                {
                    OutsideSalesRep = new IntranetEntities.OutsideSalesRep();
                    OutsideSalesRep.SAPCustomerGroupID = osr.Sapcode;
                    OutsideSalesRep.SAPCustomerGroupName = osr.Name.Trim();

                    OutsideSalesReps.Add(OutsideSalesRep);
                    intranetdb.OutsideSalesReps.Add(OutsideSalesRep);
                }

                string[] nameArray = osr.Name.Split(" ".ToCharArray());
                string firstName = string.Empty;
                string lastName = string.Empty;

                if (nameArray.Length >= 2)
                {
                    firstName = nameArray[0];
                    lastName = nameArray[1];

                    int slashPosition = lastName.LastIndexOf("/");

                    if (slashPosition > 0)
                    {
                        lastName = lastName.Remove(slashPosition);
                    }
                }
                else
                {
                    firstName = "TBD";
                    lastName = "TBD";
                }
                IntranetEntities.User Qmuser;
                Employee osruser;
                IntranetEntities.Employee QmemployeeO;

                if (osr.User.IsNull())
                {
                    Qmuser = (from e in QMemployeesuser.OfType<IntranetEntities.User>() where e.FirstName.ToLower() == firstName.ToLower() && e.LastName.ToLower() == lastName.ToLower() select e).FirstOrDefault();
                    osruser = (from u in db.Users.OfType<Employee>() where u.FirstName.ToLower() == firstName.ToLower() && u.LastName.ToLower() == lastName.ToLower() select u).FirstOrDefault();

                }
                else
                {
                    Qmuser = (from e in QMemployeesuser.OfType<IntranetEntities.User>() where e.FirstName.ToLower() == osr.User.FirstName.ToLower() && e.LastName.ToLower() == osr.User.LastName.ToLower() select e).FirstOrDefault();
                    osruser = (from u in db.Users.OfType<Employee>() where u.UserID == osr.UserID select u).FirstOrDefault();
                }


                if (Qmuser.IsNull())
                {

                    Qmuser = new IntranetEntities.User();

                    QMemployeesuser.Add(Qmuser);
                    intranetdb.Users.Add(Qmuser);

                    if (osruser.IsNull())
                    {
                        Qmuser.UserName = firstName.ToLower().Substring(0, 2) + lastName.ToLower();
                        Qmuser.FirstName = firstName.ToLower();
                        Qmuser.LastName = lastName.ToLower();
                        Qmuser.Password = firstName.ToLower() + lastName.ToLower();
                        Qmuser.PasswordSalt = firstName.ToLower() + lastName.ToLower();
                        Qmuser.Email = firstName.ToLower() + "." + lastName.ToLower() + "@zekelman.com";
                        Qmuser.PasswordReset = false;
                        Qmuser.Active = true;
                    }
                    else
                    {
                        Qmuser.UserName = osruser.SamaccountName;
                        Qmuser.FirstName = osruser.FirstName;
                        Qmuser.LastName = osruser.LastName;
                        Qmuser.Password = osruser.Password;
                        Qmuser.PasswordSalt = osruser.PasswordSalt;
                        Qmuser.Email = osruser.Email;
                        Qmuser.PasswordReset = false;
                        Qmuser.PhoneNumber = osruser.PhoneNumber;
                        Qmuser.FaxNumber = osruser.FaxNumber;
                        Qmuser.Active = true;
                    }
                }

                QmemployeeO = (from e in intranetdb.Employees where e.EmployeeID == Qmuser.UserID select e).FirstOrDefault();
                if (QmemployeeO.IsNull())
                {
                    QmemployeeO = new IntranetEntities.Employee();
                    intranetdb.Employees.Add(QmemployeeO);

                    QmemployeeO.EmployeeID = Qmuser.UserID;
                    QmemployeeO.DepartmentID = 6;
                    QmemployeeO.LocationID = 2;
                    QmemployeeO.DivisionID = 2;
                    QmemployeeO.SAMAccountName = Qmuser.UserName;
                }

                intranetdb.SaveChanges();
                // if (OutsideSalesRep.User.IsNull()) {
                OutsideSalesRep.User = Qmuser;
                // }
                OutsideSalesRep.SAPCustomerGroupName = osr.Name.Trim();
                OutsideSalesRep.SAPCustomerGroupID = osr.Sapcode.Trim();

            }
            intranetdb.SaveChanges();

            foreach (SAPSalesGroup Isr in portalsalesgroups)
            {

                IntranetEntities.InsideSalesRep InsideSalesRep = (from sg in intranetdb.InsideSalesReps where sg.SAPSalesGroupID == Isr.Sapcode select sg).FirstOrDefault();

                if (InsideSalesRep.IsNull())
                {
                    InsideSalesRep = new IntranetEntities.InsideSalesRep();
                    InsideSalesRep.SAPSalesGroupID = Isr.Sapcode;
                    InsideSalesRep.SAPSalesGroupName = Isr.Name.Trim();
                    InsideSalesReps.Add(InsideSalesRep);
                    intranetdb.InsideSalesReps.Add(InsideSalesRep);
                }

                string[] nameArray = Isr.Name.Split(" ".ToCharArray());
                string firstName = string.Empty;
                string lastName = string.Empty;

                if (nameArray.Length >= 2)
                {
                    firstName = nameArray[0];
                    lastName = nameArray[1];

                    int slashPosition = lastName.LastIndexOf("/");

                    if (slashPosition > 0)
                    {
                        lastName = lastName.Remove(slashPosition);
                    }
                }
                else
                {
                    firstName = "TBD";
                    lastName = "TBD";
                }

                IntranetEntities.User Qmuser;
                Employee Isruser;
                IntranetEntities.Employee Qmemployee;

                if (Isr.User.IsNull())
                {
                    Qmuser = (from e in QMemployeesuser.OfType<IntranetEntities.User>() where e.FirstName.ToLower() == firstName.ToLower() && e.LastName.ToLower() == lastName.ToLower() select e).FirstOrDefault();
                    Isruser = (from u in db.Users.OfType<Employee>() where u.FirstName.ToLower() == firstName.ToLower() && u.LastName.ToLower() == lastName.ToLower() select u).FirstOrDefault();

                }
                else
                {
                    Qmuser = (from e in QMemployeesuser.OfType<IntranetEntities.User>() where e.FirstName.ToLower() == Isr.User.FirstName.ToLower() && e.LastName.ToLower() == Isr.User.LastName.ToLower() select e).FirstOrDefault();
                    Isruser = (from u in db.Users.OfType<Employee>() where u.UserID == Isr.UserID select u).FirstOrDefault();
                }


                if (Qmuser.IsNull())
                {

                    Qmuser = new IntranetEntities.User();

                    QMemployeesuser.Add(Qmuser);
                    intranetdb.Users.Add(Qmuser);

                    if (Isruser.IsNull())
                    {

                        Qmuser.UserName = firstName.ToLower().Substring(0, 2) + lastName.ToLower();
                        Qmuser.FirstName = firstName.ToLower();
                        Qmuser.LastName = lastName.ToLower();
                        Qmuser.Password = firstName.ToLower() + lastName.ToLower();
                        Qmuser.PasswordSalt = firstName.ToLower() + lastName.ToLower();
                        Qmuser.Email = firstName.ToLower() + "." + lastName.ToLower() + "@zekelman.com";
                        Qmuser.PasswordReset = false;
                        Qmuser.Active = true;
                    }
                    else
                    {

                        Qmuser.UserName = Isruser.SamaccountName;
                        Qmuser.FirstName = Isruser.FirstName;
                        Qmuser.LastName = Isruser.LastName;
                        Qmuser.Password = Isruser.Password;
                        Qmuser.PasswordSalt = Isruser.PasswordSalt;
                        Qmuser.Email = Isruser.Email;
                        Qmuser.PasswordReset = false;
                        Qmuser.PhoneNumber = Isruser.PhoneNumber;
                        Qmuser.FaxNumber = Isruser.FaxNumber;
                        Qmuser.Active = true;
                    }
                }
                Qmemployee = (from e in intranetdb.Employees where e.EmployeeID == Qmuser.UserID select e).FirstOrDefault();
                if (Qmemployee.IsNull())
                {
                    Qmemployee = new IntranetEntities.Employee();
                    intranetdb.Employees.Add(Qmemployee);

                    Qmemployee.EmployeeID = Qmuser.UserID;
                    Qmemployee.DepartmentID = 6;
                    Qmemployee.LocationID = 2;
                    Qmemployee.DivisionID = 2;
                    Qmemployee.SAMAccountName = Qmuser.UserName;
                }


                intranetdb.SaveChanges();
                if (InsideSalesRep.Employee.IsNull())
                {
                    InsideSalesRep.EmployeeID = Qmuser.UserID;
                }
                InsideSalesRep.SAPSalesGroupID = Isr.Sapcode;
                InsideSalesRep.SAPSalesGroupName = Isr.Name.Trim();
            }
            intranetdb.SaveChanges();

            foreach (SAPSoldTo portalSoldTo in portalsoldTos)
            {
                //SAPSoldToNumbers.Add(zstSoldTo.SoldToNumber.Trim());

                string countryAbbr = portalSoldTo.City.State.Country.Name.Trim();
                string stateAbbr = portalSoldTo.City.State.Name.Trim();
                string cityName = portalSoldTo.City.Name.Trim();
                //int mxcustomerID = customers.OrderByDescending(c => c.CustomerID).FirstOrDefault().CustomerID;
                IntranetEntities.Customer SAPSoldTo = (from s in customers where s.SAPCustomerID == portalSoldTo.Number.Trim() select s).FirstOrDefault();

                if (!countryAbbr.jIsEmpty() && !stateAbbr.jIsEmpty() && !cityName.jIsEmpty())
                {
                    IntranetEntities.Country country = (from c in countries where c.Abbreviation.ToLower() == countryAbbr.ToLower() select c).FirstOrDefault();

                    if (country.IsNull())
                    {
                        country = new IntranetEntities.Country();
                        country.Abbreviation = countryAbbr;
                        country.Name = countryAbbr;
                        country.Active = true;
                        countries.Add(country);
                        intranetdb.Countries.Add(country);
                        intranetdb.SaveChanges();
                    }

                    IntranetEntities.State state = (from s in states where s.Abbreviation.ToLower() == stateAbbr.ToLower() && s.Country == country select s).FirstOrDefault();

                    if (state.IsNull())
                    {
                        state = new IntranetEntities.State();
                        state.Abbreviation = stateAbbr;
                        state.Name = stateAbbr;
                        state.Country = country;
                        state.Active = true;
                        states.Add(state);
                        intranetdb.States.Add(state);
                        intranetdb.SaveChanges();
                    }

                    IntranetEntities.City city = (from c in cities where c.Name.ToLower() == cityName.ToLower() && c.State == state select c).FirstOrDefault();

                    if (city.IsNull())
                    {
                        city = new IntranetEntities.City();
                        city.Name = cityName;
                        city.State = state;
                        city.Active = true;
                        cities.Add(city);
                        intranetdb.Cities.Add(city);
                        intranetdb.SaveChanges();
                    }

                    IntranetEntities.OutsideSalesRep osr;
                    IntranetEntities.InsideSalesRep isr;

                    if (!city.IsNull())
                    {
                        if (!portalSoldTo.SapcustomerGroup.IsNull())
                        {
                            osr = (from cg in intranetdb.OutsideSalesReps where cg.SAPCustomerGroupID == portalSoldTo.SapcustomerGroup.Sapcode select cg).FirstOrDefault();
                        }
                        else
                        {
                            osr = (from cg in intranetdb.OutsideSalesReps where cg.SAPCustomerGroupID == "00" select cg).FirstOrDefault();
                        }
                        if (!portalSoldTo.SapsalesGroup.IsNull())
                        {
                            isr = (from sg in intranetdb.InsideSalesReps where sg.SAPSalesGroupID == portalSoldTo.SapsalesGroup.Sapcode select sg).FirstOrDefault();
                        }
                        else
                        {
                            isr = (from sg in intranetdb.InsideSalesReps where sg.SAPSalesGroupID == "TBD" select sg).FirstOrDefault();
                        }
                        if (SAPSoldTo.IsNull())
                        {
                            SAPSoldTo = new IntranetEntities.Customer();

                            //SAPSoldTo.CustomerID = mxcustomerID + 1;
                            SAPSoldTo.SAPCustomerID = portalSoldTo.Number.Trim();
                            SAPSoldTo.Name = portalSoldTo.Name;

                            SAPSoldTo.Address = portalSoldTo.Address;
                            SAPSoldTo.City = city;
                            SAPSoldTo.PostalCode = portalSoldTo.PostalCode;
                            SAPSoldTo.PhoneNumber = portalSoldTo.Phone;
                            SAPSoldTo.FaxNumber = portalSoldTo.Fax;
                            //SAPSoldTo.Active = true;
                            SAPSoldTo.Active = portalSoldTo.Active;
                            SAPSoldTo.OutsideSalesRep = osr;
                            SAPSoldTo.InsideSalesRep = isr;
                            SAPSoldTo.AllowOrderCreation = false;

                            customers.Add(SAPSoldTo);
                            intranetdb.Customers.Add(SAPSoldTo);
                            intranetdb.SaveChanges();
                            //insertedCount++;
                        }
                        else
                        {
                            //checkedForUpdatesCount++;			
                            SAPSoldTo.Address = portalSoldTo.Address;
                            SAPSoldTo.City = city;
                            SAPSoldTo.PostalCode = portalSoldTo.PostalCode;
                            SAPSoldTo.PhoneNumber = portalSoldTo.Phone;
                            SAPSoldTo.FaxNumber = portalSoldTo.Fax;
                            //SAPSoldTo.Active = true;
                            SAPSoldTo.Active = portalSoldTo.Active;
                            SAPSoldTo.OutsideSalesRep = osr;
                            SAPSoldTo.InsideSalesRep = isr;
                            SAPSoldTo.AllowOrderCreation = false;
                            intranetdb.SaveChanges();
                        }




                    }

                }
                //intranetdb.SaveChanges();
            }

        }
    }
}
