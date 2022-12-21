using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;
using JMC.Portal.Business.WheatlandPortal;
using System.Net;
using JMC;
using JMC.Portal.Business.HSSPortalSales;
using System.Diagnostics;
using System.ServiceModel;

namespace JMC.Portal.Business
{
    public partial class SAPDelivery
    {

        private string _SoldToText;
        public string SoldToText
        {
            get
            {
                if (_SoldToText != null) return _SoldToText;
                _SoldToText = "";
                if (!this.SapsoldTo.IsNull()) _SoldToText = this.SapsoldTo.TrimmedNumber + " " + this.SapsoldTo.Name;
                return _SoldToText;
            }
        }
        private string _ShipToText;
        public string ShipToText
        {
            get
            {
                if (_ShipToText != null) return _ShipToText;
                _ShipToText = "";
                SAPShipTo st = this.FindSAPShipTo;
                if (!st.IsNull()) _ShipToText = st.Number + " " + st.Name + " (" + st.CityAndState + ")";
                return _ShipToText;
            }
        }
        private string _WebReleaseText;
        public string WebReleaseText
        {
            get
            {
                if (_WebReleaseText != null) return _WebReleaseText;
                _WebReleaseText = "";
                foreach (var w in this.WebRelease)
                {
                    _WebReleaseText += w.WebReleaseID + " ";
                }
                return _WebReleaseText;
            }
        }
        private string _WebReleasePlantText;
        public string WebReleasePlantText
        {
            get
            {
                if (_WebReleasePlantText != null) return _WebReleasePlantText;
                _WebReleasePlantText = "";
                foreach (var w in this.WebReleasePlants)
                {
                    _WebReleasePlantText += w.WebReleasePlantID + " ";
                }
                return _WebReleasePlantText;
            }
        }

        public SAPShipTo FindSAPShipTo
        {
            get
            {
                if (this is SAPSalesDelivery)
                {
                    return ((SAPSalesDelivery)this).SapshipTo;
                }
                if (this.SapdeliveryItems.IsNull()) return null;
                return (from x in this.SapdeliveryItems where x.SapsalesOrderItemID.HasValue && x.SapsalesOrderItem.SapshipToID.HasValue select x.SapsalesOrderItem.SapshipTo).FirstOrDefault();
            }
        }

        public bool IsPendingSchedule
        {
            get
            {
                return (!this.TransportationPlanningDate.HasValue || (this.TransportationPlanningDate.Value.Hour == 0 && this.TransportationPlanningDate.Value.Minute == 0));
            }
        }

        public IEnumerable<WebReleasePlant> _WebReleasePlants;
        public IEnumerable<WebRelease> _WebRelease;

        public IEnumerable<WebReleasePlant> WebReleasePlants
        {
            get
            {
                if (_WebReleasePlants.IsNull()) this.LoadWebReleases();
                return _WebReleasePlants;
            }
        }
        public IEnumerable<WebRelease> WebRelease
        {
            get
            {
                if (_WebRelease.IsNull()) this.LoadWebReleases();
                return _WebRelease;
            }
        }

        public static IQueryable<SAPSalesDelivery> GetBackloggedDeliveries(PortalEntities db, User user, long? shipToID, SAPSoldTo sapSoldTo = null,
            long plantID = 0, long? sapSalesGroupID = null)
        {
            List<SAPSoldTo> sapSoldTos = new List<SAPSoldTo>();
            List<long> sapSoldToIDs = new List<long>();
            List<long> userShipToExclusionList = user.ExcludedSAPShipToes.Select(s => s.SapshipToID).ToList();
            bool userHasExcludedShipToes = false;

            if (!userShipToExclusionList.IsNull() && userShipToExclusionList.Count > 0)
            {
                userHasExcludedShipToes = true;
            }
            if (user is Employee)
            {
                if (!sapSoldTo.IsNull())
                {
                    return (from x in db.Sapdeliveries.OfType<SAPSalesDelivery>()
                            where x.DivisionID == (long)Enums.Divisions.Atlas
                                && x.SapsoldToID.HasValue
                                && x.SapsoldToID.Value == sapSoldTo.SapshipToID
                                && (shipToID > 0 ? x.SapshipToID.Value == shipToID : userHasExcludedShipToes ? !userShipToExclusionList.Contains(x.SapshipToID.Value) : true)
                            select x);
                }
                else
                {
                    return (from x in db.Sapdeliveries.OfType<SAPSalesDelivery>()
                            where x.DivisionID == (long)Enums.Divisions.Atlas
                                && (plantID == 0 || x.PlantID == plantID)
                                && (sapSalesGroupID == null || (x.SapsoldTo != null && x.SapsoldTo.SapsalesGroup != null && x.SapsoldTo.SapsalesGroup.SapsalesGroupID == sapSalesGroupID))
                            select x);
                }
            }
            sapSoldTos = user.GetSAPSoldTos();
            if (!sapSoldTo.IsNull() && !sapSoldTos.Contains(sapSoldTo))
            {
                throw new Exception("Permission Denied. User " + user.FullName + " trying to Access a Sold To " + sapSoldTo.Number + " " + sapSoldTo.Name);
            }
            sapSoldToIDs = (from x in sapSoldTos select x.SapshipToID).ToList();
            if (sapSoldTo.IsNull())
            {
                return (from x in db.Sapdeliveries.OfType<SAPSalesDelivery>()
                        where x.DivisionID == (long)Enums.Divisions.Atlas
                            && x.SapsoldToID.HasValue
                            && sapSoldToIDs.Contains(x.SapsoldToID.Value)
                            && (plantID == 0 || x.PlantID == plantID)
                            && (sapSalesGroupID == 0 ||
                                    (x.SapsoldTo != null
                                    && x.SapsoldTo.SapsalesGroup != null
                                    && x.SapsoldTo.SapsalesGroup.SapsalesGroupID == sapSalesGroupID))
                            && (userHasExcludedShipToes ? !userShipToExclusionList.Contains(x.SapshipToID.Value) : true)
                        select x);
            }
            return (from x in db.Sapdeliveries.OfType<SAPSalesDelivery>()
                    where x.DivisionID == (long)Enums.Divisions.Atlas
                        && x.SapsoldToID.HasValue
                        && x.SapsoldToID.Value == sapSoldTo.SapshipToID
                        && (plantID == 0 || x.PlantID == plantID)
                                && (sapSalesGroupID == 0 ||
                                    (x.SapsoldTo != null
                                    && x.SapsoldTo.SapsalesGroup != null
                                    && x.SapsoldTo.SapsalesGroup.SapsalesGroupID == sapSalesGroupID))
                        && (shipToID > 0 ? x.SapshipToID.Value == shipToID : userHasExcludedShipToes ? !userShipToExclusionList.Contains(x.SapshipToID.Value) : true)
                    select x);
        }

        public static List<SAPSalesDelivery> GetDeliveriesPendingSchedule(PortalEntities db, User user, long? shipToID, SAPSoldTo sapSoldTo = null, long plantID = 0, long sapSalesGroupID = 0)
        {
            return GetBackloggedDeliveries(db, user, shipToID, sapSoldTo, plantID, sapSalesGroupID)
                .Where(x => (!x.TransportationPlanningDate.HasValue || (x.TransportationPlanningDate.Value.Hour == 0 && x.TransportationPlanningDate.Value.Minute == 0)))
                .Where(x => !x.ActualGoodsMovementDate.HasValue)
                .ToList();
        }

        public static List<SAPSalesDelivery> GetDeliveriesNotPendingSchedule(PortalEntities db, User user, long? shipToID, SAPSoldTo sapSoldTo = null, long plantID = 0, long sapSalesGroupID = 0)
        {
            return GetBackloggedDeliveries(db, user, shipToID, sapSoldTo, plantID, sapSalesGroupID)
                .Where(x => !(!x.TransportationPlanningDate.HasValue || (x.TransportationPlanningDate.Value.Hour == 0 && x.TransportationPlanningDate.Value.Minute == 0)))
                .Where(x => !x.ActualGoodsMovementDate.HasValue)
                .ToList();
        }

        private void LoadWebReleases()
        {
            List<WebRelease> wrs = new List<WebRelease>();
            List<WebReleasePlant> wrp = new List<WebReleasePlant>();
            if (!this.IsNull())
            {
                foreach (var sapDeliveryItem in this.SapdeliveryItems)
                {
                    foreach (var webReleasePlantSAPSalesOrderItem in sapDeliveryItem.WebReleasePlantSapsalesOrderItems)
                    {
                        if (!wrp.Contains(webReleasePlantSAPSalesOrderItem.WebReleasePlant))
                        {
                            wrp.Add(webReleasePlantSAPSalesOrderItem.WebReleasePlant);
                            if (!wrs.Contains(webReleasePlantSAPSalesOrderItem.WebReleasePlant.WebRelease))
                            {
                                wrs.Add(webReleasePlantSAPSalesOrderItem.WebReleasePlant.WebRelease);
                            }
                        }
                    }
                }
            }
            _WebRelease = wrs.AsEnumerable();
            _WebReleasePlants = wrp.AsEnumerable();
        }

        public void GetSalesOrderItems(ref DBCache dbcache)
        {
            if (this.SapdeliveryItems.Any(x => !x.SapsalesOrderItemID.HasValue))
            {
                ZfmGetHssDeliveriesResponse zfmGetHssDeliveriesResponse = SAPSearch(ref dbcache, null, null, null, null, null, null, this.Number.TrimNull(), new List<SAPSoldTo>(), new List<SAPShipTo>(), new List<Plant>(), null, null, null, true, false);
                List<string> salesOrderNumbers = new List<string>();

                foreach (var sdr in zfmGetHssDeliveriesResponse.EtHssDeliveryItems)
                {
                    SAPSalesOrderItem sapSalesOrderItem = dbcache.getSAPSalesOrderItem(sdr.SalesOrderNumber, sdr.SalesOrderPosition);
                    if (sapSalesOrderItem.IsNull() && !salesOrderNumbers.Contains(sdr.SalesOrderNumber.TrimNull()))
                    {
                        salesOrderNumbers.Add(sdr.SalesOrderNumber.TrimNull());
                    }
                }
                foreach (string salesOrderNumber in salesOrderNumbers)
                {
                    ZfmGetHssSalesOrdersResponse zfmGetHssSalesOrdersResponse = SAPSalesOrder.SAPSearch(null, null, null, null, null, salesOrderNumber, true);
                    SAPSalesOrder.StoreSalesOrders(ref dbcache, zfmGetHssSalesOrdersResponse.EtHssSalesOrders, zfmGetHssSalesOrdersResponse.EtHssSalesOrderItems);
                    SAPSalesOrder.StoreSalesOrderItems(ref dbcache, zfmGetHssSalesOrdersResponse.EtHssSalesOrderItems, null, false);
                }

                SAPDelivery.StoreZstHssDeliveries(ref dbcache, zfmGetHssDeliveriesResponse.EtHssDeliveries);
                SAPDelivery.StoreZstHssDeliveryItems(ref dbcache, zfmGetHssDeliveriesResponse.EtHssDeliveryItems);
                dbcache.db.SaveChanges();
            }
        }

        public static ZstHssMaterialTestRpt[] GetMaterialTestReportFromAtlasSAP(string sapDeliveryNumber, string[] sapSolTos)
        {
            ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
            portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            ZfmGetHssMaterialTestRpt getHssMaterialTestRpt = new ZfmGetHssMaterialTestRpt();
            getHssMaterialTestRpt.ImDeliveryNumber = sapDeliveryNumber;
            getHssMaterialTestRpt.ImBatchNumber = string.Empty;
            getHssMaterialTestRpt.ItSoldToNumbers = sapSolTos;
            getHssMaterialTestRpt.ImPoNumber = string.Empty;
            getHssMaterialTestRpt.ImHeatNumber = string.Empty;

            portalSalesService.Open();
            ZfmGetHssMaterialTestRptResponse getHssMaterialTestRptResponse = portalSalesService.ZfmGetHssMaterialTestRptAsync(getHssMaterialTestRpt);
            portalSalesService.Close();

            return getHssMaterialTestRptResponse.EtHssMaterialTestRpt;
        }

        public static void RefreshFromAtlasSAP(ref DBCache dbcache, string email, DateTime startDate, DateTime endDate)
        {
            //PortalEntities db = new PortalEntities();

            int insertedCount = 0;
            int checkedForUpdatesCount = 0;
            int deletedCount = 0;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetDeliveries getDeliveries = new JMC.Portal.Business.AtlasSAPPortal.ZGetDeliveries();
            getDeliveries.ImStartDate = startDate;
            getDeliveries.ImEndDate = endDate;

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetDeliveriesResponse getDeliveriesResponse = sapPortalService.ZGetDeliveriesAsync(getDeliveries);
            sapPortalService.Close();
            foreach (JMC.Portal.Business.AtlasSAPPortal.Zdelivery zstDelivery in getDeliveriesResponse.ExtDelivery)
            {
                if (!zstDelivery.DeliveryNumber.TrimNull().jIsEmpty() && !zstDelivery.DeliveryType.TrimNull().jIsEmpty())
                {
                    bool scrap = false;
                    bool agentWarehouse = false;
                    bool express = false;
                    bool pickPost = false;
                    bool intermodal = false;
                    bool spotRate = false;

                    SAPShipment sapShipment = null;
                    SAPDelivery sapDelivery = null;
                    SAPDeliveryType sapDeliveryType = dbcache.getSAPDeliveryType(zstDelivery.DeliveryType);
                    sapDelivery = dbcache.getDeliveryByNumber(zstDelivery.DeliveryNumber);

                    SAPShipTo sapShipTo = dbcache.getShipToByNumber(zstDelivery.ShipToNumber);
                    SAPSoldTo sapSoldTo = dbcache.getSoldToByNumber(zstDelivery.SoldToNumber);
                    SAPVendor sapVendor = dbcache.getVendorByNumber(zstDelivery.Vendor);
                    Plant plant = null;
                    SAPStorageLocation storageLocation = null;

                    if (!sapSoldTo.IsNull() && !sapSoldTo.SapcustomerGroup.IsNull() && sapSoldTo.SapcustomerGroup.Sapcode == "28")
                    {
                        scrap = true;
                    }

                    string[] materialGroupSAPCodes = zstDelivery.MaterialGroups.Split(new char[] { ',' });

                    if (sapDeliveryType.IsNull())
                    {
                        sapDeliveryType = new SAPDeliveryType();

                        sapDeliveryType.Sapcode = zstDelivery.DeliveryType.TrimNull();
                        sapDeliveryType.Name = zstDelivery.DeliveryType.TrimNull();
                        dbcache.AddSAPDeliveryType(sapDeliveryType);
                    }

                    sapShipment = dbcache.getShipment(zstDelivery.ShipmentNumber, zstDelivery.DeliveryNumber);


                    if (sapShipment.IsNull())
                    {
                        sapShipment = new SAPShipment();

                        sapShipment.DivisionID = (long)Enums.Divisions.Atlas;

                        if (!zstDelivery.ShipmentNumber.Trim().jIsEmpty())
                        {
                            sapShipment.Number = zstDelivery.ShipmentNumber;
                        }
                        else
                        {
                            sapShipment.DeliveryNumber = zstDelivery.DeliveryNumber;
                        }
                        dbcache.AddShipment(sapShipment);
                    }
                    // comment this code for plymouth reorg
                    //if (zstDelivery.SalesOrg.Trim() == "ATCA" && (zstDelivery.StorageLocation == "HADT" || zstDelivery.StorageLocation == "TRPL")) {
                    //  plant = dbcache.getPlantByID((long)Enums.Plants.Plymouth);
                    //} else {

                    if (ConfigurationManager.AppSettings["InterCompany"] == "")
                    {
                        //commented line below for Plymouth regorg
                        //plant = dbcache.getPlantBySalesOrg(zstDelivery.SalesOrg);
                        plant = dbcache.getPlantBySapCode(zstDelivery.Plant == "MAVE" ? "BLYT" : zstDelivery.Plant == "DETR" ? "PLYM" : zstDelivery.Plant);
                    }
                    else
                    {
                        // Intercompany process
                        plant = dbcache.getPlantBySapCode(zstDelivery.Plant == "MAVE" ? "BLYT" : zstDelivery.Plant == "DETR" ? "PLYM" : zstDelivery.Plant);
                    }
                    //	}
                    //End
                    if (!plant.IsNull() && !zstDelivery.StorageLocation.jIsEmpty())
                    {
                        storageLocation = dbcache.getStorageLocation(plant.LocationID, zstDelivery.StorageLocation);

                        if (storageLocation.IsNull())
                        {
                            storageLocation = new SAPStorageLocation();
                            storageLocation.Plant = plant;
                            storageLocation.Sapcode = zstDelivery.StorageLocation.Trim();
                            storageLocation.Name = zstDelivery.StorageLocation.Trim();
                            storageLocation.Active = true;
                            dbcache.addStorageLocation(storageLocation);
                        }
                    }

                    if (zstDelivery.DeliveryType == "LF" && zstDelivery.IsTransDelvry == "X")
                    {
                        sapDeliveryType = dbcache.getSAPDeliveryType("NL");

                        if (sapDeliveryType.IsNull())
                        {
                            sapDeliveryType = new SAPDeliveryType();

                            sapDeliveryType.Sapcode = "NL";
                            sapDeliveryType.Name = "NL";

                            dbcache.AddSAPDeliveryType(sapDeliveryType);
                        }
                    }

                    if (sapDelivery.IsNull())
                    {
                        if (zstDelivery.IsTransDelvry == "X")
                        {
                            sapDelivery = new SAPDelivery();
                        }
                        else
                        {
                            SAPSalesDelivery sapSalesDelivery = new SAPSalesDelivery();
                            sapDelivery = sapSalesDelivery;
                        }

                        sapDelivery.DivisionID = (long)Enums.Divisions.Atlas;
                        sapDelivery.Number = zstDelivery.DeliveryNumber;

                        dbcache.AddDelivery(sapDelivery);
                        insertedCount++;
                    }
                    else
                    {
                        checkedForUpdatesCount++;
                    }

                    if (sapDelivery is SAPSalesDelivery)
                    {
                        SAPSalesDelivery sapSalesDelivery = (SAPSalesDelivery)sapDelivery;

                        sapSalesDelivery.SapstorageLocation = storageLocation;
                        sapSalesDelivery.WarehouseNumber = zstDelivery.WarehouseNumber;
                        sapSalesDelivery.SapshipTo = sapShipTo;
                        sapSalesDelivery.EqualizedFreight = zstDelivery.EqualizedFrt;
                        sapSalesDelivery.InvoiceNumbers = zstDelivery.InvoiceNumbers;
                        sapSalesDelivery.TrailorNumber = zstDelivery.TrailorNumber;
                        sapSalesDelivery.CustomerFreightCurrency = zstDelivery.CustFrtToCurr;
                        sapSalesDelivery.FreightChargedToCustomer = zstDelivery.FrtChrgToCust.ToNullableDecimal();
                        sapSalesDelivery.UsdfreightChargedToCustomer = zstDelivery.UsfrtChrgToCt.ToNullableDecimal();
                        sapSalesDelivery.CustomerCadexchangeRateUsed = zstDelivery.CustCadExchRt.ToNullableDecimal();
                        sapSalesDelivery.Length = zstDelivery.Length.ToNullableDecimal();

                        sapShipment.UsdfreightChargedToCustomer = (from d in sapShipment.Sapdeliveries.OfType<SAPSalesDelivery>() select d.UsdfreightChargedToCustomer).Sum();
                    }

                    sapShipment.ActualGoodsMovementDate = zstDelivery.ActGmoveDate.ToDate();
                    sapShipment.SapdeliveryType = sapDeliveryType;
                    sapShipment.Plant = plant;
                    sapShipment.SapsoldTo = sapSoldTo;
                    sapShipment.Distance = zstDelivery.Distance.ToNullableDecimal();
                    sapShipment.DistanceUom = zstDelivery.DistanceUom;
                    sapShipment.UsdfreightPaidToCarrier = zstDelivery.UsfrtPaidToCa.ToNullableDecimal();
                    sapShipment.Sapvendor = sapVendor;
                    sapShipment.Scrap = scrap;
                    sapShipment.AgentWarehouse = agentWarehouse;

                    sapDelivery.ActualGoodsMovementDate = zstDelivery.ActGmoveDate.ToNullableDate();
                    sapDelivery.DeliveryType = sapDeliveryType.Sapcode;
                    sapDelivery.Sapshipment = sapShipment;
                    sapDelivery.CreatedBy = zstDelivery.CreatedBy;
                    sapDelivery.CreationDate = zstDelivery.CreationDate.ToNullableDate();
                    sapDelivery.Plant = plant;
                    sapDelivery.Distance = zstDelivery.Distance.ToNullableDecimal();
                    sapDelivery.DistanceUom = zstDelivery.DistanceUom;
                    sapDelivery.EstimatedCost = zstDelivery.EstimatedCost.ToNullableDecimal();
                    sapDelivery.ShipmentNumber = zstDelivery.ShipmentNumber;
                    sapDelivery.AccessCharges = zstDelivery.AccessCharges.ToNullableDecimal();
                    sapDelivery.Fsc = zstDelivery.Fsc.ToNullableDecimal();
                    sapDelivery.SapsoldTo = sapSoldTo;
                    sapDelivery.Transfer = zstDelivery.IsTransDelvry.ToBool();
                    sapDelivery.CarrierFreightCurrency = zstDelivery.CarrFrtCurr;
                    sapDelivery.FreightPaidToCarrier = zstDelivery.FrtPaidToCarr.ToNullableDecimal();
                    sapDelivery.UsdfreightPaidToCarrier = zstDelivery.UsfrtPaidToCa.ToNullableDecimal();
                    sapDelivery.CarrierCadexchangeRateUsed = zstDelivery.CarrCadExchRt.ToNullableDecimal();
                    sapDelivery.Sapvendor = sapVendor;

                    if (zstDelivery.TotalWeight > 0)
                    {
                        sapDelivery.Weight = zstDelivery.TotalWeight;
                    }

                    sapShipment.Weight = (from d in sapShipment.Sapdeliveries select d.Weight).Sum();

                    sapDelivery.WeightUnit = zstDelivery.WeightUnit;
                    sapDelivery.IncoTerms2 = zstDelivery.Incoterms.Trim();

                    if (!sapVendor.IsNull())
                    {
                        if (sapVendor.Number == "CPU")
                        {
                            sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.CPUTruck;
                        }
                        else if (sapVendor.Number == "CPURAIL")
                        {
                            sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.CPURail;
                        }
                        else if (sapVendor.Rail)
                        {
                            sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.Rail;
                        }
                        else
                        {
                            sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.Truck;

                            if (sapVendor.Number == "PICKPOST")
                            {
                                pickPost = true;
                            }
                        }

                        if (!scrap && !agentWarehouse)
                        {
                            express = sapVendor.Express;
                        }
                    }

                    if (!scrap && !agentWarehouse && !express && !intermodal && (sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Truck || sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Rail) && sapShipment.SapdeliveryTypeID == (long)Enums.DeliveryTypes.SalesDelivery && (sapShipment.UsdfreightPaidToCarrier.IsNull() || sapShipment.UsdfreightPaidToCarrier <= 0))
                    {
                        spotRate = true;
                    }

                    sapShipment.Express = express;
                    sapShipment.Intermodal = intermodal;
                    sapShipment.SpotRate = spotRate;

                    sapShipment.Excepted = true;

                    if ((sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.CPUTruck || sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.CPURail) && (sapDelivery.Weight > 0))
                    {
                        sapShipment.Excepted = false;

                        sapShipment.Distance = 0;
                        sapDelivery.Distance = 0;
                    }
                    else if ((sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Truck || sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Rail) && (sapDelivery.Weight > 0 && sapShipment.Distance > 0 && (sapShipment.UsdfreightPaidToCarrier > 0 || pickPost)))
                    {
                        sapShipment.Excepted = false;
                    }
                    if (sapShipment.Sapdeliveries.Count > 1)
                    {
                        sapShipment.ActualGoodsMovementDate = (from d in sapShipment.Sapdeliveries select d.ActualGoodsMovementDate).Max().ToDate();

                        foreach (SAPDelivery shipmentDelivery in sapShipment.Sapdeliveries)
                        {
                            if (shipmentDelivery.ActualGoodsMovementDate.ToDate() != sapShipment.ActualGoodsMovementDate)
                            {
                                shipmentDelivery.ActualGoodsMovementDate = sapShipment.ActualGoodsMovementDate;
                            }
                        }
                    }
                }
            }

            dbcache.db.SaveChanges();

            IQueryable<SAPShipment> emptyShipments = (from s in dbcache.db.Sapshipments where s.DivisionID == (long)Enums.Divisions.Atlas && s.Sapdeliveries.Count() <= 0 select s);

            foreach (SAPShipment sapShipment in emptyShipments)
            {
                dbcache.db.Sapshipments.Remove(sapShipment);

                deletedCount++;
            }

            dbcache.db.SaveChanges();

            emailStringBuilder.Append(insertedCount);
            emailStringBuilder.Append(" inserted.<br />");
            emailStringBuilder.Append(checkedForUpdatesCount);
            emailStringBuilder.Append(" checked for updates.<br />");

            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);

            emailStringBuilder.Append(startDate.ToString("MMM d yyyy") + " to " + endDate.ToString("MMM d yyyy"));
            emailStringBuilder.Append("<br />Run Time " + runTime.Days);
            emailStringBuilder.Append("days " + runTime.Hours);
            emailStringBuilder.Append("hours " + runTime.Minutes);
            emailStringBuilder.Append("minutes " + runTime.Seconds);
            emailStringBuilder.Append("seconds");

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas Delivery Download Results", emailStringBuilder.ToString());
        }

        public static void RefreshFromWheatlandSAP(string email, DateTime startDate, DateTime endDate)
        {
            PortalEntities db = new PortalEntities();

            int insertedCount = 0;
            int checkedForUpdatesCount = 0;
            int deletedCount = 0;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            zws_portalClient sapPortalService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];

            JMC.Portal.Business.WheatlandPortal.ZGetDeliveries getDeliveries = new JMC.Portal.Business.WheatlandPortal.ZGetDeliveries();
            //TODO getDeliveries.ImStartDate = startDate.ToString("yyyy-MM-dd");
            //getDeliveries.ImEndDate = endDate.ToString("yyyy-MM-dd");

            sapPortalService.Open();
            JMC.Portal.Business.WheatlandPortal.ZGetDeliveriesResponse getDeliveriesResponse = sapPortalService.ZGetDeliveriesAsync(getDeliveries);
            sapPortalService.Close();

            List<SAPDeliveryType> deliveryTypes = (from dt in db.SapdeliveryTypes select dt).ToList();
            List<SAPShipment> shipments = (from s in db.Sapshipments where s.DivisionID == (long)Enums.Divisions.Wheatland select s).ToList();
            List<SAPDelivery> deliveries = (from d in db.Sapdeliveries where d.DivisionID == (long)Enums.Divisions.Wheatland select d).ToList();
            List<SAPShipTo> shipTos = (from s in db.SapshipTos where s.DivisionID == (long)Enums.Divisions.Wheatland select s).ToList();
            List<SAPSoldTo> soldTos = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.DivisionID == (long)Enums.Divisions.Wheatland select s).ToList();
            List<SAPVendor> vendors = (from v in db.Sapvendors where v.DivisionID == (long)Enums.Divisions.Wheatland select v).ToList();
            List<Plant> plants = (from l in Location.GetAllActive(ref db).OfType<Plant>() where l.DivisionID == (long)Enums.Divisions.Wheatland select l).ToList();
            List<SAPStorageLocation> storageLocations = (from sl in db.SapstorageLocations where sl.Plant.DivisionID == (long)Enums.Divisions.Wheatland select sl).ToList();
            List<SAPCharacteristicOption> materialGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.MaterialGroup select co).ToList();

            foreach (WheatlandPortal.Zdelivery zstDelivery in getDeliveriesResponse.ExtDelivery)
            {
                if (!zstDelivery.DeliveryNumber.Trim().jIsEmpty() && !zstDelivery.DeliveryType.Trim().jIsEmpty())
                {
                    bool scrap = false;
                    bool agentWarehouse = false;
                    bool express = false;
                    bool intermodal = false;
                    bool spotRate = false;

                    SAPShipment sapShipment = null;
                    SAPDelivery sapDelivery = null;
                    SAPDeliveryType sapDeliveryType = (from dt in deliveryTypes where dt.Sapcode == zstDelivery.DeliveryType.Trim() select dt).FirstOrDefault();
                    sapDelivery = (from d in deliveries where d.Number == zstDelivery.DeliveryNumber.Trim() select d).FirstOrDefault();

                    SAPShipTo sapShipTo = (from s in shipTos where s.Number == zstDelivery.ShipToNumber.Trim() select s).FirstOrDefault();
                    SAPSoldTo sapSoldTo = (from s in soldTos where s.Number == zstDelivery.SoldToNumber.Trim() select s).FirstOrDefault();
                    SAPVendor sapVendor = (from v in vendors where v.Number == zstDelivery.Vendor.Trim() select v).FirstOrDefault();
                    Plant plant = (from l in plants where l.Code == zstDelivery.Plant.Trim() select l).FirstOrDefault();
                    SAPStorageLocation storageLocation = null;
                    SAPCharacteristicOption materialGroup = null;

                    if (!zstDelivery.MaterialGroups.jIsEmpty())
                    {
                        string[] materialGroupStringArray = zstDelivery.MaterialGroups.Split(new char[] { ',' });

                        if (materialGroupStringArray.Length > 0)
                        {
                            materialGroup = (from co in materialGroups where co.Sapcode == materialGroupStringArray[0].Trim() select co).FirstOrDefault();
                        }
                    }

                    if (zstDelivery.DeliveryType == "ZSLF")
                    {
                        sapDeliveryType = (from dt in deliveryTypes where dt.Sapcode == "LF" select dt).FirstOrDefault();

                        if (sapDeliveryType.IsNull())
                        {
                            sapDeliveryType = new SAPDeliveryType();

                            sapDeliveryType.Sapcode = "LF";
                            sapDeliveryType.Name = "LF";

                            deliveryTypes.Add(sapDeliveryType);
                            db.SapdeliveryTypes.Add(sapDeliveryType);
                        }
                    }

                    if (zstDelivery.DeliveryType == "ZSNL")
                    {
                        sapDeliveryType = (from dt in deliveryTypes where dt.Sapcode == "NL" select dt).FirstOrDefault();

                        if (sapDeliveryType.IsNull())
                        {
                            sapDeliveryType = new SAPDeliveryType();

                            sapDeliveryType.Sapcode = "NL";
                            sapDeliveryType.Name = "NL";

                            deliveryTypes.Add(sapDeliveryType);
                            db.SapdeliveryTypes.Add(sapDeliveryType);
                        }
                    }

                    if (sapDeliveryType.IsNull())
                    {
                        sapDeliveryType = new SAPDeliveryType();

                        sapDeliveryType.Sapcode = zstDelivery.DeliveryType.Trim();
                        sapDeliveryType.Name = zstDelivery.DeliveryType.Trim();

                        deliveryTypes.Add(sapDeliveryType);
                        db.SapdeliveryTypes.Add(sapDeliveryType);
                    }

                    if (!zstDelivery.ShipmentNumber.Trim().jIsEmpty())
                    {
                        sapShipment = (from s in shipments where s.Number == zstDelivery.ShipmentNumber.Trim() select s).FirstOrDefault();
                    }
                    else
                    {
                        sapShipment = (from s in shipments where s.DeliveryNumber == zstDelivery.DeliveryNumber.Trim() select s).FirstOrDefault();
                    }

                    if (sapShipment.IsNull())
                    {
                        sapShipment = new SAPShipment();

                        sapShipment.DivisionID = (long)Enums.Divisions.Wheatland;

                        if (!zstDelivery.ShipmentNumber.Trim().jIsEmpty())
                        {
                            sapShipment.Number = zstDelivery.ShipmentNumber;
                        }
                        else
                        {
                            sapShipment.DeliveryNumber = zstDelivery.DeliveryNumber;
                        }

                        shipments.Add(sapShipment);
                        db.Sapshipments.Add(sapShipment);
                    }

                    if (!plant.IsNull() && !zstDelivery.StorageLocation.jIsEmpty())
                    {
                        storageLocation = (from sl in storageLocations where sl.Plant.LocationID == plant.LocationID && sl.Sapcode == zstDelivery.StorageLocation.Trim() select sl).FirstOrDefault();

                        if (storageLocation.IsNull())
                        {
                            storageLocation = new SAPStorageLocation();
                            storageLocation.Plant = plant;
                            storageLocation.Sapcode = zstDelivery.StorageLocation.Trim();
                            storageLocation.Name = zstDelivery.StorageLocation.Trim();
                            storageLocation.Active = true;

                            storageLocations.Add(storageLocation);
                            db.SapstorageLocations.Add(storageLocation);
                        }
                    }

                    if (sapDelivery.IsNull())
                    {
                        if (zstDelivery.IsTransDelvry == "X")
                        {
                            sapDelivery = new SAPDelivery();
                        }
                        else
                        {
                            SAPSalesDelivery sapSalesDelivery = new SAPSalesDelivery();
                            sapDelivery = sapSalesDelivery;
                        }

                        sapDelivery.DivisionID = (long)Enums.Divisions.Wheatland;
                        sapDelivery.Number = zstDelivery.DeliveryNumber;

                        deliveries.Add(sapDelivery);
                        db.Sapdeliveries.Add(sapDelivery);

                        insertedCount++;
                    }
                    else
                    {
                        checkedForUpdatesCount++;
                    }

                    if (sapDelivery is SAPSalesDelivery)
                    {
                        SAPSalesDelivery sapSalesDelivery = (SAPSalesDelivery)sapDelivery;

                        sapSalesDelivery.SapstorageLocation = storageLocation;
                        sapSalesDelivery.WarehouseNumber = zstDelivery.WarehouseNumber;
                        sapSalesDelivery.SapshipTo = sapShipTo;
                        sapSalesDelivery.EqualizedFreight = zstDelivery.EqualizedFrt;
                        sapSalesDelivery.InvoiceNumbers = zstDelivery.InvoiceNumbers;
                        sapSalesDelivery.TrailorNumber = zstDelivery.TrailorNumber;
                        sapSalesDelivery.CustomerFreightCurrency = zstDelivery.CustFrtToCurr;
                        sapSalesDelivery.FreightChargedToCustomer = zstDelivery.FrtChrgToCust.ToNullableDecimal();
                        sapSalesDelivery.UsdfreightChargedToCustomer = zstDelivery.UsfrtChrgToCt.ToNullableDecimal();
                        sapSalesDelivery.CustomerCadexchangeRateUsed = zstDelivery.CustCadExchRt.ToNullableDecimal();
                        sapSalesDelivery.Length = zstDelivery.Length.ToNullableDecimal();

                        sapShipment.UsdfreightChargedToCustomer = (from d in sapShipment.Sapdeliveries.OfType<SAPSalesDelivery>() select d.UsdfreightChargedToCustomer).Sum();
                    }

                    if (!scrap)
                    {
                        agentWarehouse = (!plant.IsNull() && plant.Code.ToInt() > 3000);
                    }

                    sapShipment.ActualGoodsMovementDate = zstDelivery.ActGmoveDate.ToDate();
                    sapShipment.SapdeliveryType = sapDeliveryType;
                    sapShipment.Plant = plant;
                    sapShipment.SapsoldTo = sapSoldTo;
                    sapShipment.Distance = zstDelivery.Distance.ToNullableDecimal();
                    sapShipment.DistanceUom = zstDelivery.DistanceUom;
                    sapShipment.EstimatedCost = zstDelivery.EstimatedCost.ToNullableDecimal();
                    sapShipment.AccessCharges = zstDelivery.AccessCharges.ToNullableDecimal();
                    sapShipment.Fsc = zstDelivery.Fsc.ToNullableDecimal();
                    sapShipment.UsdfreightPaidToCarrier = zstDelivery.UsfrtPaidToCa.ToNullableDecimal();
                    sapShipment.Sapvendor = sapVendor;
                    sapShipment.Scrap = scrap;
                    sapShipment.AgentWarehouse = agentWarehouse;

                    if (!materialGroup.IsNull())
                    {
                        sapShipment.ProductLine = materialGroup.ProductLine;
                    }

                    sapDelivery.ActualGoodsMovementDate = zstDelivery.ActGmoveDate.ToNullableDate();
                    sapDelivery.DeliveryType = sapDeliveryType.Sapcode;
                    sapDelivery.Sapshipment = sapShipment;
                    sapDelivery.CreatedBy = zstDelivery.CreatedBy;
                    sapDelivery.CreationDate = zstDelivery.CreationDate.ToNullableDate();
                    sapDelivery.Plant = plant;
                    sapDelivery.Distance = zstDelivery.Distance.ToNullableDecimal();
                    sapDelivery.DistanceUom = zstDelivery.DistanceUom;
                    sapDelivery.EstimatedCost = zstDelivery.EstimatedCost.ToNullableDecimal();
                    sapDelivery.ShipmentNumber = zstDelivery.ShipmentNumber;
                    sapDelivery.AccessCharges = zstDelivery.AccessCharges.ToNullableDecimal();
                    sapDelivery.Fsc = zstDelivery.Fsc.ToNullableDecimal();
                    sapDelivery.SapsoldTo = sapSoldTo;
                    sapDelivery.Transfer = zstDelivery.IsTransDelvry.ToBool();
                    sapDelivery.CarrierFreightCurrency = zstDelivery.CarrFrtCurr;
                    sapDelivery.FreightPaidToCarrier = zstDelivery.FrtPaidToCarr.ToNullableDecimal();
                    sapDelivery.UsdfreightPaidToCarrier = zstDelivery.UsfrtPaidToCa.ToNullableDecimal();
                    sapDelivery.CarrierCadexchangeRateUsed = zstDelivery.CarrCadExchRt.ToNullableDecimal();
                    sapDelivery.Sapvendor = sapVendor;

                    if (zstDelivery.TotalWeight > 0)
                    {
                        sapDelivery.Weight = zstDelivery.TotalWeight;
                    }

                    sapShipment.Weight = (from d in sapShipment.Sapdeliveries select d.Weight).Sum();

                    sapDelivery.WeightUnit = zstDelivery.WeightUnit;
                    sapDelivery.IncoTerms2 = zstDelivery.Incoterms.Trim();

                    if (!sapVendor.IsNull())
                    {
                        if (sapVendor.Number == "0000020144" || sapVendor.Number == "0000020006")
                        {
                            sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.CPUTruck;
                        }
                        else if (sapVendor.Rail)
                        {
                            if (sapDelivery.IncoTerms2 == "FCA" || sapDelivery.IncoTerms2 == "CIP")
                            {
                                sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.CPURail;
                            }
                            else
                            {
                                sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.Rail;
                            }
                        }
                        else
                        {
                            if (sapDelivery.IncoTerms2 == "FCA" || sapDelivery.IncoTerms2 == "CIP")
                            {
                                sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.CPUTruck;
                            }
                            else
                            {
                                sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.Truck;
                            }
                        }

                        if (!scrap && !agentWarehouse)
                        {
                            express = sapVendor.Express;

                            if (!express)
                            {
                                intermodal = sapVendor.Intermodal;
                            }
                        }
                    }
                    else
                    {
                        if (sapDelivery.IncoTerms2 == "FCA" || sapDelivery.IncoTerms2 == "CIP")
                        {
                            sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.CPUTruck;
                        }
                        else if (agentWarehouse)
                        {
                            sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.Truck;
                        }
                    }

                    if (!scrap && !agentWarehouse && !express && !intermodal && (sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Truck || sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Rail) && sapShipment.SapdeliveryTypeID == (long)Enums.DeliveryTypes.SalesDelivery && (sapShipment.UsdfreightPaidToCarrier.IsNull() || sapShipment.UsdfreightPaidToCarrier <= 0))
                    {
                        spotRate = true;
                    }

                    sapShipment.Express = express;
                    sapShipment.Intermodal = intermodal;
                    sapShipment.SpotRate = spotRate;

                    sapShipment.Excepted = true;

                    if ((sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.CPUTruck || sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.CPURail) && sapDelivery.Weight > 0)
                    {
                        sapShipment.Excepted = false;

                        sapShipment.Distance = 0;
                        sapDelivery.Distance = 0;
                    }
                    else if ((sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Truck || sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Rail) && (sapDelivery.Weight > 0 && sapShipment.Distance > 0 && sapShipment.UsdfreightPaidToCarrier > 0))
                    {
                        sapShipment.Excepted = false;
                    }

                    if (sapShipment.Sapdeliveries.Count > 1)
                    {
                        sapShipment.ActualGoodsMovementDate = (from d in sapShipment.Sapdeliveries select d.ActualGoodsMovementDate).Max().ToDate();

                        foreach (SAPDelivery shipmentDelivery in sapShipment.Sapdeliveries)
                        {
                            if (shipmentDelivery.ActualGoodsMovementDate.ToDate() != sapShipment.ActualGoodsMovementDate)
                            {
                                shipmentDelivery.ActualGoodsMovementDate = sapShipment.ActualGoodsMovementDate;
                            }
                        }
                    }
                }
            }

            db.SaveChanges();

            IQueryable<SAPShipment> emptyShipments = (from s in db.Sapshipments where s.DivisionID == (long)Enums.Divisions.Wheatland && s.Sapdeliveries.Count() <= 0 select s);

            foreach (SAPShipment sapShipment in emptyShipments)
            {
                db.Sapshipments.Remove(sapShipment);

                deletedCount++;
            }

            db.SaveChanges();

            emailStringBuilder.Append(insertedCount);
            emailStringBuilder.Append(" inserted.<br />");
            emailStringBuilder.Append(checkedForUpdatesCount);
            emailStringBuilder.Append(" checked for updates.<br />");
            emailStringBuilder.Append(deletedCount);
            emailStringBuilder.Append(" empty shipments deleted.<br />");

            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);

            emailStringBuilder.Append(startDate.ToString("MMM d yyyy") + " to " + endDate.ToString("MMM d yyyy"));
            emailStringBuilder.Append("<br />Run Time " + runTime.Days);
            emailStringBuilder.Append("days " + runTime.Hours);
            emailStringBuilder.Append("hours " + runTime.Minutes);
            emailStringBuilder.Append("minutes " + runTime.Seconds);
            emailStringBuilder.Append("seconds");

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Wheatland Delivery Download Results", emailStringBuilder.ToString());
        }
        /// <summary>
        ///  Fetches the Delivery information from SAP and updates the SqlServer table 
        /// </summary>
        /// <param name="dbcache"></param>
        /// <param name="headerText"></param>
        /// <param name="shippingInstructions"></param>
        /// <param name="pickedItemsLocation"></param>
        /// <param name="inputArrayList"></param>
        /// <param name="email"></param>
        /// <returns></returns>

        public static void RefreshDeliveriesFromWheatlandSAP(string startDate, string endDate)
        {
            using (PortalEntities db = new PortalEntities())
            {
                JMC.Portal.Business.WheatLandSales.ZWS_HSS_PORTAL_SALESClient portalSalesService = new JMC.Portal.Business.WheatLandSales.ZWS_HSS_PORTAL_SALESClient("WHEATLAND_PORTAL_SALES");
                try
                {
                    portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
                    portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];


                    WheatLandSales.ZGetMtrDeliveries wheatlandMTRDeliveries = new WheatLandSales.ZGetMtrDeliveries();

                    wheatlandMTRDeliveries.ImStartDate = startDate;
                    wheatlandMTRDeliveries.ImEndDate = endDate;

                    ////TODO: move the this logic to controller
                    //if (!startDate.HasValue && !endDate.HasValue)
                    //{
                    //    wheatlandMTRDeliveries.ImStartDate = System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    //    wheatlandMTRDeliveries.ImEndDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                    //}
                    //else
                    //{
                    //    //make sure endDate is later than start date
                    //    if (DateTime.Compare(endDate.Value, startDate.Value) > 0)
                    //    {
                    //        wheatlandMTRDeliveries.ImStartDate = startDate.Value.ToString("yyyy-MM-dd");
                    //        wheatlandMTRDeliveries.ImEndDate = endDate.Value.ToString("yyyy-MM-dd");
                    //    }
                    //}
                    //JMC.Portal.Business.WheatLandSales.ZGetMtrDeliveriesRequest wheatlandDeliveriesRequest = new WheatLandSales.ZGetMtrDeliveriesRequest(wheatlandMTRDeliveries);
                    portalSalesService.Open();
                    JMC.Portal.Business.WheatLandSales.ZGetMtrDeliveriesResponse wheatlandDeliveriesResponse = portalSalesService.ZGetMtrDeliveriesAsync(wheatlandMTRDeliveries);
                    portalSalesService.Close();

                    foreach (WheatLandSales.ZstMtrDelv zMTRDelivery in wheatlandDeliveriesResponse.ExtMtrDelivery)
                    {
                        SAPWheatlandDelivery sapWheatlandDelivery = new SAPWheatlandDelivery
                        {
                            DeliveryNumber = zMTRDelivery.DeliveryNumber,
                            BatchNumber = zMTRDelivery.Batch,
                            CustomerPo = zMTRDelivery.CustomerPo,
                            HeatNumber = zMTRDelivery.Heat,
                            ItemDesc = zMTRDelivery.SalesItemDesc,
                            MaterialNumber = zMTRDelivery.MaterialNumber,
                            Pgidate = zMTRDelivery.ActualPgiDate.ToDate(),
                            SalesOrderNumber = zMTRDelivery.SalesOrder,
                            ShipToNumber = zMTRDelivery.ShipToParty,
                            SoldToNumber = zMTRDelivery.SoldToParty,
                            RunNumber = zMTRDelivery.Runnum
                        };
                        //check if wheatland delivery already exists in database
                        var delivery = (from s in db.SapwheatlandDeliveries
                                        where (s.DeliveryNumber == sapWheatlandDelivery.DeliveryNumber && s.MaterialNumber == sapWheatlandDelivery.MaterialNumber &&
                                               s.BatchNumber == sapWheatlandDelivery.BatchNumber && s.HeatNumber == sapWheatlandDelivery.HeatNumber && s.RunNumber == sapWheatlandDelivery.RunNumber)
                                        select s).FirstOrDefault();
                        if (delivery != null)
                        {
                            db.SapwheatlandDeliveries.Remove(delivery);
                            db.SaveChanges();
                        }
                        //add the new object 
                        db.SapwheatlandDeliveries.Add(sapWheatlandDelivery);
                    }
                    db.SaveChanges();

                }
                catch (FaultException e)
                {
                    portalSalesService.Close();
                }
                catch (Exception e)
                {
                    portalSalesService.Close();
                }
                finally
                {
                    portalSalesService.Close();
                }
            }
        }
        public static void ZMailMtrDeliveries(string[] deliveryNos, string[] emailIds, string[] pgiDates)
        {
            JMC.Portal.Business.WheatLandSales.ZWS_HSS_PORTAL_SALESClient portalSalesService = new JMC.Portal.Business.WheatLandSales.ZWS_HSS_PORTAL_SALESClient("WHEATLAND_PORTAL_SALES");
            try
            {

                portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
                portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];

                WheatLandSales.ZMailMtrDeliveries mailMtrDeliveries = new WheatLandSales.ZMailMtrDeliveries();

                mailMtrDeliveries.ItDeliveryNumbers = deliveryNos;
                mailMtrDeliveries.ItEmailAddress = emailIds;
                mailMtrDeliveries.ItPgiDate = pgiDates;

                portalSalesService.Open();
                WheatLandSales.ZMailMtrDeliveriesResponse mailMtrResponse = portalSalesService.ZMailMtrDeliveriesAsync(mailMtrDeliveries);
                portalSalesService.Close();
            }
            catch (FaultException e)
            {
                portalSalesService.Close();
            }
            catch (Exception e)
            {
                portalSalesService.Close();
            }
            finally
            {
                portalSalesService.Close();
            }

        }
        public static SAPDelivery CreateInAtlasSAP(ref DBCache dbcache, string headerText, string shippingInstructions, string pickedItemsLocation, ZstHssCreateDeliveryIn[] inputArrayList, string email, string userName)
        {
            //ArrayList inputArrayList = new ArrayList();

            if (inputArrayList.Count() > 0)
            {
                ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
                portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
                portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

                List<Tline> headerTextTLines = new List<Tline>();
                List<Tline> shippingInstructionsTLines = new List<Tline>();

                if (!headerText.jIsEmpty())
                {
                    Tline tLine = new Tline();
                    tLine.Tdline = headerText.Trim().Length > 132 ? headerText.Trim().Substring(0, 132) : headerText.Trim();
                    headerTextTLines.Add(tLine);
                }

                if (!shippingInstructions.jIsEmpty())
                {
                    Tline tLine = new Tline();
                    tLine.Tdline = shippingInstructions.Trim().Length > 132 ? shippingInstructions.Trim().Substring(0, 132) : shippingInstructions.Trim();
                    shippingInstructionsTLines.Add(tLine);
                }

                ZfmCreateHssDelivery createHssDelivery = new ZfmCreateHssDelivery();
                createHssDelivery.ImText = string.Empty;
                createHssDelivery.ImPickedItemsLocation = pickedItemsLocation.TrimNull();
                createHssDelivery.ItItems = inputArrayList;
                createHssDelivery.ItDeliveryInstructions = headerTextTLines.ToArray();
                createHssDelivery.ItShippingInstructions = shippingInstructionsTLines.ToArray();
                createHssDelivery.ImUsername = userName.Trim().ToUpper().Length > 12 ? userName.Trim().ToUpper().Substring(0, 12) : userName.ToUpper().Trim(); ;

                DateTime sapStartTime = DateTime.Now;
                DateTime sapEndTime = DateTime.Now;

                portalSalesService.Open();
                ZfmCreateHssDeliveryResponse createHssDeliveryResponse = portalSalesService.ZfmCreateHssDeliveryAsync(createHssDelivery);
                portalSalesService.Close();

                SAPDelivery sapDelivery = null;

                if (createHssDeliveryResponse.ExDeliveryNumber.ToInt() > 0)
                {
                    sapDelivery = SAPDelivery.StoreZstHssDeliveries(ref dbcache, createHssDeliveryResponse.EtHssDeliveries);
                    sapDelivery = SAPDelivery.StoreZstHssDeliveryItems(ref dbcache, createHssDeliveryResponse.EtHssDeliveryItems);

                    SAPSalesOrder.StoreSalesOrders(ref dbcache, createHssDeliveryResponse.EtHssSalesOrders, createHssDeliveryResponse.EtHssSalesOrderItems);
                    SAPSalesOrder.StoreSalesOrderItems(ref dbcache, createHssDeliveryResponse.EtHssSalesOrderItems, null);

                    //if (!sapDelivery.SAPSoldTo.IsNull() && !sapDelivery.SAPSoldTo.RefreshingBacklog.ToBool()) {
                    //  dbcache.QueueUpBacklogRefresh(sapDelivery.SAPSoldTo);
                    //  //sapDelivery.SAPSoldTo.RefreshBacklog();
                    //}
                }
                if (sapDelivery.IsNull()) sapDelivery = new SAPDelivery();
                foreach (string bapisdtext in createHssDeliveryResponse.EtOutput)
                {
                    sapDelivery.BapireturnMessage += bapisdtext + "\r\n";
                }
                dbcache.db.SaveChanges();

                return sapDelivery;
            }

            return null;
        }

        public static SAPDelivery StoreZstHssDeliveries(ref DBCache dbcache, ZstHssDelivery[] zstHssDeliveries)
        {
            dbcache.db.SaveChanges();

            SAPDelivery sapDelivery = null;
            int i = 0;

            foreach (ZstHssDelivery zstHssDelivery in zstHssDeliveries)
            {
                i++;
                sapDelivery = null;
                bool scrap = false;
                bool agentWarehouse = false;
                bool express = false;
                bool pickPost = false;
                bool intermodal = false;
                bool spotRate = false;

                SAPShipment sapShipment = dbcache.getShipment(zstHssDelivery.ShipmentNumber, zstHssDelivery.DeliveryNumber);
                sapDelivery = dbcache.getDeliveryByNumber(zstHssDelivery.DeliveryNumber);
                SAPDeliveryType sapDeliveryType = dbcache.getSAPDeliveryType(zstHssDelivery.DeliveryType);

                SAPShipTo sapShipTo = dbcache.getShipToByNumber(zstHssDelivery.ShipToNumber);
                SAPSoldTo sapSoldTo = dbcache.getSoldToByNumber(zstHssDelivery.SoldToNumber);
                SAPVendor sapVendor = dbcache.getVendorByNumber(zstHssDelivery.Vendor);

                if (sapDeliveryType.IsNull())
                {
                    dbcache.getSAPDeliveryType("LF"); // (from dt in dbcache.deliveryTypes where dt.SAPCode == "LF"/*TODO EtHssDelivery.DeliveryType.Trim()*/ select dt).FirstOrDefault();
                }

                Plant plant;

                if (ConfigurationManager.AppSettings["InterCompany"] == "")
                {
                    //Commented line below for Plymouth Reorg 
                    //plant = dbcache.getPlantBySalesOrg(zstHssDelivery.SalesOrganization);
                    plant = dbcache.getPlantBySapCode(zstHssDelivery.Plant == "MAVE" ? "BLYT" : zstHssDelivery.Plant == "DETR" ? "PLYM" : zstHssDelivery.Plant);

                }
                else
                {
                    //intercompamy process implementation 06/27/2018
                    plant = dbcache.getPlantBySapCode(zstHssDelivery.Plant == "MAVE" ? "BLYT" : zstHssDelivery.Plant == "DETR" ? "PLYM" : zstHssDelivery.Plant);

                }
                // comment this code for plymouth reorg
                //if (zstHssDelivery.SalesOrganization.TrimNull() == "ATCA" && (zstHssDelivery.StorageLocation.TrimNull() == "HADT" || zstHssDelivery.StorageLocation.TrimNull() == "TRPL")) {
                //  plant = dbcache.getPlantByID((long)Enums.Plants.Plymouth);
                //}
                //End
                SAPStorageLocation storageLocation = null;

                if (sapDeliveryType.IsNull())
                {
                    sapDeliveryType = new SAPDeliveryType();
                    sapDeliveryType.Sapcode = zstHssDelivery.DeliveryType.Trim();
                    sapDeliveryType.Name = zstHssDelivery.DeliveryType.Trim();
                    dbcache.AddSAPDeliveryType(sapDeliveryType);
                }
                if (sapShipment.IsNull())
                {
                    sapShipment = new SAPShipment();
                    sapShipment.DivisionID = (long)Enums.Divisions.Atlas;
                    sapShipment.ActualGoodsMovementDate = DateTime.Now;
                    if (!zstHssDelivery.ShipmentNumber.Trim().jIsEmpty())
                    {
                        sapShipment.Number = zstHssDelivery.ShipmentNumber.Trim();
                    }
                    else
                    {
                        sapShipment.DeliveryNumber = zstHssDelivery.DeliveryNumber.Trim();
                    }
                    dbcache.AddShipment(sapShipment);
                }

                if (!plant.IsNull() && !zstHssDelivery.StorageLocation.jIsEmpty())
                {
                    storageLocation = dbcache.getStorageLocation(plant.LocationID, zstHssDelivery.StorageLocation); //(from sl in storageLocations where sl.Plant.LocationID == plant.LocationID && sl.SAPCode == zstHssDeliveryItem.StorageLocation.Trim() select sl).FirstOrDefault();

                    if (storageLocation.IsNull())
                    {
                        storageLocation = new SAPStorageLocation();
                        storageLocation.Plant = plant;
                        storageLocation.Sapcode = zstHssDelivery.StorageLocation.Trim();
                        storageLocation.Name = zstHssDelivery.StorageLocation.Trim();
                        storageLocation.Active = true;
                        dbcache.addStorageLocation(storageLocation);
                    }
                }

                if (zstHssDelivery.DeliveryType == "LF" && zstHssDelivery.IsTransferDelivery.Trim() == "X")
                {
                    sapDeliveryType = dbcache.getSAPDeliveryType("NL"); // (from dt in deliveryTypes where dt.SAPCode == "NL" select dt).FirstOrDefault();

                    if (sapDeliveryType.IsNull())
                    {
                        sapDeliveryType = new SAPDeliveryType();
                        sapDeliveryType.Sapcode = "NL";
                        sapDeliveryType.Name = "NL";
                        dbcache.AddSAPDeliveryType(sapDeliveryType);
                    }
                }

                if (sapDelivery.IsNull())
                {
                    if (zstHssDelivery.IsTransferDelivery.Trim() == "X")
                    {
                        sapDelivery = new SAPDelivery();
                    }
                    else
                    {
                        SAPSalesDelivery sapSalesDelivery = new SAPSalesDelivery();
                        sapDelivery = sapSalesDelivery;
                    }

                    sapDelivery.DivisionID = (long)Enums.Divisions.Atlas;
                    sapDelivery.Number = zstHssDelivery.DeliveryNumber;
                    dbcache.AddDelivery(sapDelivery);
                }

                if (sapDelivery is SAPSalesDelivery)
                {
                    SAPSalesDelivery sapSalesDelivery = (SAPSalesDelivery)sapDelivery;
                    sapSalesDelivery.SapstorageLocation = storageLocation;
                    sapSalesDelivery.WarehouseNumber = zstHssDelivery.WarehouseNumber;
                    sapSalesDelivery.SapshipTo = sapShipTo;
                    sapSalesDelivery.EqualizedFreight = zstHssDelivery.EqualizedFreight;
                    sapSalesDelivery.TrailorNumber = zstHssDelivery.TrailorNumber;
                    sapSalesDelivery.CustomerFreightCurrency = zstHssDelivery.CustomerFreightCurrency;
                    sapShipment.UsdfreightChargedToCustomer = (from d in sapShipment.Sapdeliveries.OfType<SAPSalesDelivery>() select d.UsdfreightChargedToCustomer).Sum();
                }

                if (zstHssDelivery.ActualGoodsMovementDate.Year > 1950)
                {
                    sapShipment.ActualGoodsMovementDate = zstHssDelivery.ActualGoodsMovementDate;
                    sapDelivery.ActualGoodsMovementDate = zstHssDelivery.ActualGoodsMovementDate;
                }
                else
                {
                    sapDelivery.ActualGoodsMovementDate = null;
                }

                sapShipment.SapdeliveryType = sapDeliveryType;
                sapShipment.Plant = plant;
                sapShipment.SapsoldTo = sapSoldTo;
                sapShipment.Sapvendor = sapVendor;
                sapShipment.Scrap = scrap;
                sapShipment.AgentWarehouse = agentWarehouse;

                sapDelivery.DeliveryType = sapDeliveryType.Sapcode;
                sapDelivery.Sapshipment = sapShipment;
                sapDelivery.CreatedBy = zstHssDelivery.CreatedBy;

                if (zstHssDelivery.CreationDate.Year > 1950)
                {
                    sapDelivery.CreationDate = zstHssDelivery.CreationDate;
                }

                sapDelivery.Plant = plant;
                sapDelivery.ShipmentNumber = zstHssDelivery.ShipmentNumber;
                sapDelivery.SapsoldTo = sapSoldTo;
                sapDelivery.Transfer = zstHssDelivery.IsTransferDelivery.ToBool();
                sapDelivery.Sapvendor = sapVendor;

                if (zstHssDelivery.TotalWeight > 0)
                {
                    sapDelivery.Weight = zstHssDelivery.TotalWeight;
                }

                sapShipment.Weight = (from d in sapShipment.Sapdeliveries select d.Weight).Sum();
                sapDelivery.WeightUnit = zstHssDelivery.WeightUnit;
                sapDelivery.IncoTerms2 = zstHssDelivery.Incoterms.Trim();

                if (!sapVendor.IsNull())
                {
                    if (sapVendor.Number == "CPU")
                    {
                        sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.CPUTruck;
                    }
                    else if (sapVendor.Number == "CPURAIL")
                    {
                        sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.CPURail;
                    }
                    else if (sapVendor.Rail)
                    {
                        sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.Rail;
                    }
                    else
                    {
                        sapShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.Truck;

                        if (sapVendor.Number == "PICKPOST")
                        {
                            pickPost = true;
                        }
                    }

                    if (!scrap && !agentWarehouse)
                    {
                        express = sapVendor.Express;
                    }
                }

                if (!scrap && !agentWarehouse && !express && !intermodal && (sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Truck || sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Rail) && sapShipment.SapdeliveryTypeID == (long)Enums.DeliveryTypes.SalesDelivery && (sapShipment.UsdfreightPaidToCarrier.IsNull() || sapShipment.UsdfreightPaidToCarrier <= 0))
                {
                    spotRate = true;
                }

                sapShipment.Express = express;
                sapShipment.Intermodal = intermodal;
                sapShipment.SpotRate = spotRate;
                sapShipment.Excepted = true;

                if ((sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.CPUTruck || sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.CPURail) && (sapDelivery.Weight > 0))
                {
                    sapShipment.Excepted = false;
                    sapShipment.Distance = 0;
                    sapDelivery.Distance = 0;
                }
                else if ((sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Truck || sapShipment.DeliveryMethodID == (long)Enums.DeliveryMethods.Rail) && (sapDelivery.Weight > 0 && sapShipment.Distance > 0 && (sapShipment.UsdfreightPaidToCarrier > 0 || pickPost)))
                {
                    sapShipment.Excepted = false;
                }

                if (sapShipment.Sapdeliveries.Count > 1)
                {
                    sapShipment.ActualGoodsMovementDate = (from d in sapShipment.Sapdeliveries select d.ActualGoodsMovementDate).Max().ToDate();
                }

                int year, month, day, hour, minute, second;
                if (zstHssDelivery.DeliveryDate.Year > 1950)
                {
                    year = zstHssDelivery.DeliveryDate.Year;
                    month = zstHssDelivery.DeliveryDate.Month;
                    day = zstHssDelivery.DeliveryDate.Day;
                    hour = zstHssDelivery.DeliveryTime.Hour;
                    minute = zstHssDelivery.DeliveryTime.Minute;
                    second = zstHssDelivery.DeliveryTime.Second;
                    sapDelivery.TransportationPlanningDate = new DateTime(year, month, day, hour, minute, second);
                }
                else
                {
                    sapDelivery.TransportationPlanningDate = null;
                }

                sapDelivery.FreightPaidToCarrier = zstHssDelivery.FreightPaidToCarrier;
                sapDelivery.UsdfreightPaidToCarrier = zstHssDelivery.UsdFreightPaidToCarrier;
                sapDelivery.CarrierFreightCurrency = zstHssDelivery.CarrierFreightCurrency.Trim();
                sapDelivery.CarrierCadexchangeRateUsed = zstHssDelivery.CarrierCadExchangeRateUsed;
                sapDelivery.Distance = zstHssDelivery.Distance.ToNullableDecimal();
                sapDelivery.DistanceUom = zstHssDelivery.DistanceUom.Trim();
                sapDelivery.Fsc = zstHssDelivery.Fsc;
                sapDelivery.AccessCharges = zstHssDelivery.AccessCharges;
                sapDelivery.EstimatedCost = zstHssDelivery.EstimatedCost;

                if (!zstHssDelivery.CheckoutDate.IsNull() && (zstHssDelivery.CheckoutDate.Date != DateTime.MinValue.Date))
                {
                    sapDelivery.CheckoutDate = zstHssDelivery.CheckoutDate.Date.Add(zstHssDelivery.CheckoutTime.TimeOfDay);

                }
                else
                {
                    sapDelivery.CheckoutDate = null;
                }
                if (!zstHssDelivery.PickupApptDate.IsNull() && (zstHssDelivery.PickupApptDate.Date != DateTime.MinValue.Date))
                {
                    sapDelivery.PickupAptDate = zstHssDelivery.PickupApptDate.Date.Add(zstHssDelivery.PickupApptTime.TimeOfDay);

                }
                else
                {
                    sapDelivery.PickupAptDate = null;
                }
                if (!zstHssDelivery.ReqDelDate.IsNull() && (zstHssDelivery.ReqDelDate.Date != DateTime.MinValue.Date))
                {
                    sapDelivery.Reqdeldate = zstHssDelivery.ReqDelDate.Date.Add(zstHssDelivery.ReqDelTime.TimeOfDay);

                }
                else
                {
                    sapDelivery.Reqdeldate = null;
                }
                if (!zstHssDelivery.TmsDelDate.IsNull() && (zstHssDelivery.TmsDelDate.Date != DateTime.MinValue.Date))
                {
                    sapDelivery.Tmsdeldate = zstHssDelivery.TmsDelDate.Date.Add(zstHssDelivery.TmsDelTime.TimeOfDay);

                }
                else
                {
                    sapDelivery.Tmsdeldate = null;
                }

                if (i % 10 == 0)
                {
                    dbcache.db.SaveChanges();
                }
            }

            var iSave = dbcache.db.SaveChanges();

            return sapDelivery;
        }
        public static DateTime convertDateAndTime(DateTime dt, DateTime dtime)
        {
            int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0;
            year = dt.Year;
            month = dt.Month;
            day = dt.Day;
            if (!dtime.IsNull())
            {
                hour = dtime.Hour;
                minute = dtime.Minute;
                second = dtime.Second;
            }
            return new DateTime(year, month, day, hour, minute, second);
        }
        public static SAPDelivery StoreZstHssDeliveryItems(ref DBCache dbcache, ZstHssDeliveryItem[] zstHssDeliveryItems)
        {
            List<SAPDelivery> checkForDeletes = new List<SAPDelivery>();

            int salesOrderItemsFound = 0;
            int salesOrderItemsNotFound = 0;
            int newDeliveryItems = 0;
            SAPDelivery sapDelivery = null;
            dbcache.db.SaveChanges();

            int i = 0;

            foreach (ZstHssDeliveryItem zstHssDeliveryItem in zstHssDeliveryItems)
            {
                i++;

                SAPShipment sapShipment = dbcache.getShipment(zstHssDeliveryItem.ShipmentNumber, zstHssDeliveryItem.DeliveryNumber);
                SAPDeliveryType sapDeliveryType = dbcache.getSAPDeliveryType("LF");// (from dt in dbcache.deliveryTypes where dt.SAPCode == "LF"/*TODO EtHssDelivery.DeliveryType.Trim()*/ select dt).FirstOrDefault();
                sapDelivery = dbcache.getDeliveryByNumber(zstHssDeliveryItem.DeliveryNumber.Trim());

                if (sapShipment.IsNull())
                {
                    sapShipment = new SAPShipment();
                    sapShipment.DivisionID = (long)Enums.Divisions.Atlas;
                    sapShipment.ActualGoodsMovementDate = DateTime.Now;

                    if (!zstHssDeliveryItem.ShipmentNumber.Trim().jIsEmpty())
                    {
                        sapShipment.Number = zstHssDeliveryItem.ShipmentNumber.Trim();
                    }
                    else
                    {
                        sapShipment.DeliveryNumber = zstHssDeliveryItem.DeliveryNumber.Trim();
                    }

                    dbcache.AddShipment(sapShipment);
                }

                if (sapDelivery.IsNull())
                {
                    SAPSalesDelivery sapSalesDelivery = new SAPSalesDelivery();
                    sapDelivery = sapSalesDelivery;
                    sapDelivery.DivisionID = (long)Enums.Divisions.Atlas;
                    sapDelivery.Number = zstHssDeliveryItem.DeliveryNumber;

                    dbcache.AddDelivery(sapDelivery);
                }

                if (!checkForDeletes.Contains(sapDelivery))
                {
                    checkForDeletes.Add(sapDelivery);
                }

                SAPSalesOrderItem sapSalesOrderItem = dbcache.getSAPSalesOrderItem(zstHssDeliveryItem.SalesOrderNumber, zstHssDeliveryItem.SalesOrderPosition);
                if (sapSalesOrderItem.IsNull())
                {
                    salesOrderItemsNotFound++;
                }
                else
                {
                    salesOrderItemsFound++;
                }

                int lineItemNumber = zstHssDeliveryItem.LineItemNumber.ToInt();
                SAPDeliveryItem sapDeliveryItem = dbcache.getDeliveryItem(sapDelivery.Number, zstHssDeliveryItem.LineItemNumber);
                if (sapDeliveryItem.IsNull())
                {
                    newDeliveryItems++;
                    sapDeliveryItem = new SAPDeliveryItem();
                    dbcache.AddSAPDeliveryItem(sapDeliveryItem);
                }

                sapDeliveryItem.Position = lineItemNumber;
                sapDeliveryItem.Sapdelivery = sapDelivery;
                sapDeliveryItem.SapsalesOrderItem = sapSalesOrderItem;
                sapDeliveryItem.Weight = zstHssDeliveryItem.Weight + zstHssDeliveryItem.WeightSplitBatches;
                sapDeliveryItem.WeightUnit = zstHssDeliveryItem.Buom;
                sapDeliveryItem.Status = zstHssDeliveryItem.Status;
                sapDeliveryItem.QuantityDelivered = zstHssDeliveryItem.QuantityDelivered + zstHssDeliveryItem.QuantitySplitBatches;

                sapDeliveryItem.SalesUnit = zstHssDeliveryItem.SalesUnit.Trim();
                sapDelivery.Weight = (from x in sapDelivery.SapdeliveryItems select x.Weight ?? 0).Sum();

                if (!sapDeliveryItem.SapsalesOrderItem.IsNull())
                {
                    sapDeliveryItem.SapsalesOrderItem.SetReleasedProperties(ref dbcache);
                }

                if (i % 10 == 0)
                {
                    dbcache.db.SaveChanges();
                }
            }

            dbcache.db.SaveChanges();

            foreach (SAPDelivery checkSAPDelivery in checkForDeletes)
            {
                List<int> positions = (from x in checkSAPDelivery.SapdeliveryItems select x.Position).ToList();
                List<int> actualints = (from x in zstHssDeliveryItems.Where(y => y.DeliveryNumber == checkSAPDelivery.Number) select x.LineItemNumber.ToInt()).ToList();
                foreach (int actualint in actualints)
                {
                    positions.Remove(actualint);
                }

                foreach (int position in positions)
                {
                    SAPDeliveryItem deleteitem = dbcache.getDeliveryItem(checkSAPDelivery.Number, position.ToString());
                    dbcache.DeleteDeliveryItem(deleteitem);
                }
            }

            dbcache.db.SaveChanges();

            foreach (SAPDelivery checkSAPDelivery in checkForDeletes)
            {
                checkSAPDelivery.Weight = (from x in checkSAPDelivery.SapdeliveryItems select x.Weight ?? 0).Sum();
            }

            dbcache.db.SaveChanges();

            return sapDelivery;
        }

        public static ZfmGetHssDeliveriesResponse SAPSearch(ref DBCache dbcache, DateTime? startDate, DateTime? endDate, DateTime? SOstartDate, DateTime? SOendDate, string PONumber, string SalesOrderNumber, string DeliveryNumber, List<SAPSoldTo> sapSoldTos, List<SAPShipTo> sapShipTos, List<Plant> sapPlants, string TrailerNum, string HeatNumber, string BatchNumber, bool GetItems = true, bool GetBatches = false)
        {
            ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
            portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            PONumber = (PONumber).TrimNull();
            SalesOrderNumber = (SalesOrderNumber).TrimNull();
            DeliveryNumber = (DeliveryNumber).TrimNull();
            TrailerNum = (TrailerNum).TrimNull();
            HeatNumber = (HeatNumber).TrimNull();
            if (DeliveryNumber.Length > 0) { DeliveryNumber = DeliveryNumber.PadLeft(10, '0'); }
            if (SalesOrderNumber.Length > 0) { SalesOrderNumber = SalesOrderNumber.PadLeft(10, '0'); }

            if (sapSoldTos.IsNull()) sapSoldTos = new List<SAPSoldTo>();
            if (sapShipTos.IsNull()) sapShipTos = new List<SAPShipTo>();
            if (sapPlants.IsNull()) sapPlants = new List<Plant>();

            #region Plymouth / Harrow Correction
            // comment this code for plymouth reorg
            //List<long> sapPlantIDS = (from x in sapPlants select x.LocationID).ToList();
            //if (sapPlantIDS.Contains((long)Enums.Plants.Plymouth) && !sapPlantIDS.Contains((long)Enums.Plants.Harrow)) { //If searching plymouth and not harrow ->add harrow
            //  sapPlants.Add(dbcache.getPlantByID((long)Enums.Plants.Harrow));
            //  sapPlantIDS.Add((long)Enums.Plants.Harrow);
            //}
            //if (!sapPlantIDS.Contains((long)Enums.Plants.Plymouth) && sapPlantIDS.Contains((long)Enums.Plants.Harrow)) { //If searching harrow and not plymouth ->add plymouth
            //  sapPlants.Add(dbcache.getPlantByID((long)Enums.Plants.Plymouth));
            //  sapPlantIDS.Add((long)Enums.Plants.Plymouth);
            //}
            //End
            #endregion

            ZfmGetHssDeliveries zfmGetHssDeliveries = new ZfmGetHssDeliveries();

            if (startDate.HasValue && endDate.HasValue)
            {
                zfmGetHssDeliveries.ImStartDate = startDate.Value;
                zfmGetHssDeliveries.ImEndDate = endDate.Value;
            }
            if (SOstartDate.HasValue && SOendDate.HasValue)
            {
                zfmGetHssDeliveries.ImSalesOrderStartDate = SOstartDate.Value;
                zfmGetHssDeliveries.ImSalesOrderEndDate = SOendDate.Value;
            }

            zfmGetHssDeliveries.ImDeliveryNumber = DeliveryNumber;
            zfmGetHssDeliveries.ImPoNumber = PONumber;
            zfmGetHssDeliveries.ImSalesOrderNumber = SalesOrderNumber;
            zfmGetHssDeliveries.ImHeat = HeatNumber;
            zfmGetHssDeliveries.ImBatch = BatchNumber;

            zfmGetHssDeliveries.ItMaterialNumbers = new List<ZstMaterialNumber>().ToArray();
            zfmGetHssDeliveries.ItPlantNumbers = (from x in sapPlants where x != null select new ZstPlantNumber() { PlantNumber = x.Code == "BLYT" ? "MAVE" : x.Code == "PLYM" ? "DETR" : x.Code }).ToArray();
            zfmGetHssDeliveries.ItShipToNumbers = (from x in sapShipTos where x != null select x.Number).ToArray();
            zfmGetHssDeliveries.ItSoldToNumbers = (from x in sapSoldTos where x != null select x.Number).ToArray();

            zfmGetHssDeliveries.ImGetItems = "";
            if (GetItems)
                zfmGetHssDeliveries.ImGetItems = "X";

            zfmGetHssDeliveries.ImBatchDetail = "";
            if (GetBatches)
                zfmGetHssDeliveries.ImBatchDetail = "X";

            portalSalesService.Open();
            ZfmGetHssDeliveriesResponse zfmGetHssDeliveriesResponse = portalSalesService.ZfmGetHssDeliveriesAsync(zfmGetHssDeliveries);
            portalSalesService.Close();

            if (!TrailerNum.jIsEmpty())
            {
                zfmGetHssDeliveriesResponse.EtHssDeliveries = (ZstHssDelivery[])zfmGetHssDeliveriesResponse.EtHssDeliveries.Where(e => e.TrailorNumber.Contains(TrailerNum)).ToArray();
            }

            return zfmGetHssDeliveriesResponse;
        }
    }
}
