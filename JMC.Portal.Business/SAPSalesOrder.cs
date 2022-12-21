using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using JMC.Portal.Business.HSSPortalSales;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace JMC.Portal.Business
{
    public partial class SAPSalesOrder
    {
        public static bool isDealExist = false;
        public static bool isDealAppliedInSAP = false;

        public static void RefreshFromAtlasSAP(string email, DateTime startDate, DateTime endDate)
        {
            PortalEntities db = new PortalEntities();

            int insertedCount = 0;
            int checkedForUpdatesCount = 0;
            int rejectedCount = 0;
            int deletedCount = 0;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList rejectedMaterialNumbers = new ArrayList();

            Stopwatch timeLoadCache = new Stopwatch();
            Stopwatch timeInSAP = new Stopwatch();
            Stopwatch timeSaving = new Stopwatch();
            Stopwatch timeProcessing = new Stopwatch();


            ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
            portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            ZfmGetHssSalesOrders getHssSalesOrders = new ZfmGetHssSalesOrders();
            getHssSalesOrders.ImStartDate = startDate;
            getHssSalesOrders.ImEndDate = endDate;

            DateTime sapStartTime = DateTime.Now;
            DateTime sapEndTime = DateTime.Now;

            timeInSAP.Start();
            portalSalesService.Open();
            ZfmGetHssSalesOrdersResponse getHssSalesOrdersResponse = portalSalesService.ZfmGetHssSalesOrdersAsync(getHssSalesOrders);
            portalSalesService.Close();
            timeInSAP.Stop();

            sapEndTime = DateTime.Now;

            timeLoadCache.Start();
            var salesOrders = (from d in db.SapsalesOrders where d.DivisionID == (long)Enums.Divisions.Atlas select d); //.ToDictionary(x => x.Number, y => y);
            var shipTos = (from s in db.SapshipTos where s.DivisionID == (long)Enums.Divisions.Atlas select s).ToDictionary(x => x.Number, y => y);
            var soldTos = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.DivisionID == (long)Enums.Divisions.Atlas select s).ToDictionary(x => x.Number, y => y);
            var plants = (from l in Location.GetAllActive(ref db).OfType<Plant>() where l.DivisionID == (long)Enums.Divisions.Atlas && l.LocationID != (long)Enums.Plants.Plymouth select l).ToDictionary(x => x.SalesOrganization, y => y);
            var materials = (from m in db.Sapmaterials where m.DivisionID == (long)Enums.Divisions.Atlas select m).ToDictionary(x => x.Number, y => y);
            timeLoadCache.Stop();

            string salesOrderNumber = string.Empty;
            SAPSalesOrder SAPSalesOrder = null;

            timeProcessing.Start();
            foreach (ZstHssSalesOrderItem zstHssSalesOrderItem in getHssSalesOrdersResponse.EtHssSalesOrderItems)
            {
                if (!zstHssSalesOrderItem.SalesOrderNumber.TrimNull().jIsEmpty())
                {
                    SAPMaterial SAPMaterial = null;// (from m in materials where m.Number == zstHssSalesOrderItem.MaterialNumber.Trim() select m).FirstOrDefault();
                    materials.TryGetValue(zstHssSalesOrderItem.MaterialNumber.TrimNull(), out SAPMaterial);

                    if (!SAPMaterial.IsNull())
                    {
                        //List<SAPSalesOrderItem> SAPSalesOrderItems = null;
                        SAPSalesOrderItem SAPSalesOrderItem = null;

                        SAPShipTo salesOrderSAPShipTo = null;//  (from s in shipTos where s.Number == zstHssSalesOrderItem.SalesOrderShipToNumber.Trim() select s).FirstOrDefault();
                        shipTos.TryGetValue(zstHssSalesOrderItem.SalesOrderShipToNumber.TrimNull(), out salesOrderSAPShipTo);

                        Plant plant = null;// (from l in plants where l.SalesOrganization == zstHssSalesOrderItem.SalesOrganization.Trim() select l).FirstOrDefault();
                        plants.TryGetValue(zstHssSalesOrderItem.SalesOrganization.TrimNull(), out plant);

                        if (zstHssSalesOrderItem.SalesOrderNumber != salesOrderNumber)
                        {
                            salesOrderNumber = zstHssSalesOrderItem.SalesOrderNumber;
                            SAPSalesOrder = null;// (from s in salesOrders where s.Number == zstHssSalesOrderItem.SalesOrderNumber.Trim() select s).FirstOrDefault();
                            SAPSalesOrder = salesOrders.Where(x => x.Number == zstHssSalesOrderItem.SalesOrderNumber.Trim()).FirstOrDefault(); //.TryGetValue(zstHssSalesOrderItem.SalesOrderNumber.Trim(), out SAPSalesOrder);


                            SAPSoldTo SAPSoldTo = null;// (from s in soldTos where s.Number == zstHssSalesOrderItem.SoldToNumber.Trim() select s).FirstOrDefault();
                            soldTos.TryGetValue(zstHssSalesOrderItem.SoldToNumber.TrimNull(), out SAPSoldTo);


                            if (SAPSalesOrder.IsNull())
                            {
                                SAPSalesOrder = new SAPSalesOrder();
                                SAPSalesOrder.DivisionID = (long)Enums.Divisions.Atlas;
                                SAPSalesOrder.Number = zstHssSalesOrderItem.SalesOrderNumber;

                                //SAPSalesOrderItems = new List<SAPSalesOrderItem>();
                                //SAPSalesOrderItem = new SAPSalesOrderItem();
                                //SAPSalesOrderItem.Position = zstHssSalesOrderItem.ItemNumber.ToInt();
                                //SAPSalesOrder.SAPSalesOrderItem.Add(SAPSalesOrderItem);

                                //salesOrders.Add(SAPSalesOrder.Number, SAPSalesOrder);
                                db.SapsalesOrders.Add(SAPSalesOrder);

                                insertedCount++;
                            }
                            else
                            {
                                checkedForUpdatesCount++;
                            }
                            if (zstHssSalesOrderItem.PurchaseOrderDate.Year > 1950)
                            {
                                SAPSalesOrder.Date = zstHssSalesOrderItem.PurchaseOrderDate;
                            }
                            SAPSalesOrder.Ponumber = zstHssSalesOrderItem.PoNumber.TrimNull();
                            SAPSalesOrder.SapsoldTo = SAPSoldTo;
                            SAPSalesOrder.Plant = plant;
                            SAPSalesOrder.SapshipTo = salesOrderSAPShipTo;
                            SAPSalesOrder.DocumentType = zstHssSalesOrderItem.DocumentType.TrimNull();
                            SAPSalesOrder.DistributionChannel = zstHssSalesOrderItem.DistributionChannel.TrimNull();
                        }

                        if (!SAPSalesOrder.IsNull())
                        {
                            //SAPSalesOrderItems = SAPSalesOrder.SAPSalesOrderItem.ToList();
                            SAPSalesOrderItem = (from soi in SAPSalesOrder.SapsalesOrderItems where soi.Position == zstHssSalesOrderItem.ItemNumber.ToInt() select soi).FirstOrDefault();

                            if (SAPSalesOrderItem.IsNull())
                            {
                                SAPSalesOrderItem = new SAPSalesOrderItem();
                                SAPSalesOrderItem.Position = zstHssSalesOrderItem.ItemNumber.ToInt();
                                SAPSalesOrder.SapsalesOrderItems.Add(SAPSalesOrderItem);
                            }

                            SAPSalesOrderItem.Sapmaterial = SAPMaterial;

                            SAPSalesOrderItem.PiecesPerBundle = zstHssSalesOrderItem.PiecesPerBundle; //Get bundling from SAP
                            if ((SAPSalesOrderItem.PiecesPerBundle ?? 0) == 0)
                            {
                                SAPSalesOrderItem.PiecesPerBundle = SAPMaterial.PiecesPerBundle;
                            }

                            SAPSalesOrderItem.MaterialShortDescription = zstHssSalesOrderItem.MaterialShortDescription.TrimNull();
                            SAPSalesOrderItem.Price = zstHssSalesOrderItem.Price.ToDecimal();
                            SAPSalesOrderItem.CustomerMaterialNumber = zstHssSalesOrderItem.CustomerMaterialNumber.TrimNull();
                            SAPSalesOrderItem.Plant = plant;
                            SAPSalesOrderItem.RequirementsType = zstHssSalesOrderItem.RequirementsType.TrimNull();
                            SAPSalesOrderItem.ReadyDate = zstHssSalesOrderItem.ReadyDate.ToNullableDate();
                            SAPSalesOrderItem.MaterialStagingDate = zstHssSalesOrderItem.AvailabilityDate.ToNullableDate();
                            SAPSalesOrderItem.OpenQuantity = zstHssSalesOrderItem.OpenQuantity.ToDecimal();
                            SAPSalesOrderItem.BaseUnit = zstHssSalesOrderItem.BaseUnit.TrimNull();
                            SAPSalesOrderItem.ScheduleLineDate = zstHssSalesOrderItem.ScheduleLineDate.ToNullableDate();
                            SAPSalesOrderItem.ScheduleOrderQuantity = zstHssSalesOrderItem.ScheduleOrderQuantity.ToDecimal();
                            SAPSalesOrderItem.ConfirmedQuantity = zstHssSalesOrderItem.ConfirmedQuantity.ToDecimal();
                            SAPSalesOrderItem.SalesUnit = zstHssSalesOrderItem.SalesUnit.TrimNull();
                            SAPSalesOrderItem.GrossWeight = zstHssSalesOrderItem.GrossWeight.ToDecimal();
                            SAPSalesOrderItem.OrderQuantity = zstHssSalesOrderItem.OrderQuantity.ToDecimal();
                            SAPSalesOrderItem.ItemCategory = zstHssSalesOrderItem.ItemCategory.TrimNull();
                            SAPSalesOrderItem.DeliveryStatus = zstHssSalesOrderItem.DeliveryStatus.TrimNull();

                            SAPSalesOrderItem.SetOpenPieces();

                            if (!zstHssSalesOrderItem.ShipToNumber.jIsEmpty() && zstHssSalesOrderItem.SalesOrderShipToNumber != zstHssSalesOrderItem.ShipToNumber)
                            {
                                SAPShipTo SAPShipTo = null;//(from s in shipTos where s.Number == zstHssSalesOrderItem.ShipToNumber.Trim() select s).FirstOrDefault();
                                shipTos.TryGetValue(zstHssSalesOrderItem.ShipToNumber.TrimNull(), out SAPShipTo);
                                SAPSalesOrderItem.SapshipTo = SAPShipTo;
                            }
                        }
                    }
                    else
                    {
                        rejectedMaterialNumbers.Add(zstHssSalesOrderItem.MaterialNumber);
                        rejectedCount++;
                    }
                }
            }
            timeProcessing.Stop();

            timeSaving.Start();
            db.SaveChanges();
            timeSaving.Stop();

            emailStringBuilder.Append("Cache Load Time " + timeLoadCache.ElapsedMilliseconds + " ms<br />");
            emailStringBuilder.Append("db.SaveChanges() " + timeSaving.ElapsedMilliseconds + " ms<br />");
            emailStringBuilder.Append("Processing Time " + timeProcessing.ElapsedMilliseconds + " ms<br />");
            emailStringBuilder.Append("SAP Processing Time " + timeInSAP.ElapsedMilliseconds + " ms<br />");

            emailStringBuilder.Append(insertedCount);
            emailStringBuilder.Append(" inserted.<br />");
            emailStringBuilder.Append(checkedForUpdatesCount);
            emailStringBuilder.Append(" checked for updates.<br />");
            emailStringBuilder.Append(rejectedCount);
            emailStringBuilder.Append(" rejected.<br />");

            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);
            TimeSpan sapRunTime = sapEndTime.Subtract(sapStartTime);

            emailStringBuilder.Append(startDate.ToString("MMM d yyyy") + " to " + endDate.ToString("MMM d yyyy"));
            emailStringBuilder.Append("<br />Run Time " + runTime.Days);
            emailStringBuilder.Append("days " + runTime.Hours);
            emailStringBuilder.Append("hours " + runTime.Minutes);
            emailStringBuilder.Append("minutes " + runTime.Seconds);
            emailStringBuilder.Append("seconds<br /><br />");
            emailStringBuilder.Append("<br />SAP Run Time " + sapRunTime.Days);
            emailStringBuilder.Append("days " + sapRunTime.Hours);
            emailStringBuilder.Append("hours " + sapRunTime.Minutes);
            emailStringBuilder.Append("minutes " + sapRunTime.Seconds);
            emailStringBuilder.Append("seconds");
            emailStringBuilder.Append("<br /><br /><br />Rejected Material Numbers:<br /><br />");

            foreach (string materialNumber in rejectedMaterialNumbers)
            {
                emailStringBuilder.Append(materialNumber + "<br />");
            }

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas Sales Order Download Results", emailStringBuilder.ToString());
        }

        public static ZfmGetHssPoQueryResponse SAPSearchByPO(List<SAPSoldTo> SAPSoldTos, string PoNumber = "")
        {
            ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
            portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            if (SAPSoldTos.IsNull()) SAPSoldTos = new List<SAPSoldTo>();

            ZfmGetHssPoQuery ZfmGetHssPoQuery = new ZfmGetHssPoQuery();

            ZfmGetHssPoQuery.ImPoNumber = PoNumber;
            ZfmGetHssPoQuery.ImSoldToNumbers = (from x in SAPSoldTos where x != null select x.Number).ToArray();

            portalSalesService.Open();
            ZfmGetHssPoQueryResponse ZfmGetHssPoQueryResponse = portalSalesService.ZfmGetHssPoQueryAsync(ZfmGetHssPoQuery);
            portalSalesService.Close();

            return ZfmGetHssPoQueryResponse;
        }


        public static ZfmGetHssSalesOrdersResponse SAPSearch(DateTime? startDate, DateTime? endDate, List<SAPShipTo> SAPShipTos, List<SAPSoldTo> SAPSoldTos, List<Plant> sapPlants, string SalesOrderNumber = null, bool GetItems = true, string PoNumber = "")
        {
            ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
            portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            if (SAPShipTos.IsNull()) SAPShipTos = new List<SAPShipTo>();
            if (SAPSoldTos.IsNull()) SAPSoldTos = new List<SAPSoldTo>();
            if (sapPlants.IsNull()) sapPlants = new List<Plant>();

            ZfmGetHssSalesOrders zfmGetHssSalesOrders = new ZfmGetHssSalesOrders();
            if (startDate.HasValue && endDate.HasValue)
            {
                zfmGetHssSalesOrders.ImStartDate = startDate.Value;
                zfmGetHssSalesOrders.ImEndDate = endDate.Value;
            }
            zfmGetHssSalesOrders.ImPoNumber = PoNumber.TrimNull();
            SalesOrderNumber = SalesOrderNumber.TrimNull();
            if (SalesOrderNumber.Length > 0) SalesOrderNumber = SalesOrderNumber.PadLeft(10, '0');

            zfmGetHssSalesOrders.ImSalesOrderNumber = SalesOrderNumber;
            zfmGetHssSalesOrders.ItMaterialNumbers = new List<ZstMaterialNumber>().ToArray();
            zfmGetHssSalesOrders.ItPlantNumbers = (from x in sapPlants where x != null select new ZstPlantNumber() { PlantNumber = x.Code }).ToArray();
            zfmGetHssSalesOrders.ItShipToNumbers = (from x in SAPShipTos where x != null select x.Number).ToArray();
            zfmGetHssSalesOrders.ItSoldToNumbers = (from x in SAPSoldTos where x != null select x.Number).ToArray();
            zfmGetHssSalesOrders.ImGetItems = "";
            if (GetItems)
                zfmGetHssSalesOrders.ImGetItems = "X";

            portalSalesService.Open();
            ZfmGetHssSalesOrdersResponse zfmGetHssSalesOrdersResponse = portalSalesService.ZfmGetHssSalesOrdersAsync(zfmGetHssSalesOrders);
            portalSalesService.Close();

            return zfmGetHssSalesOrdersResponse;
        }

        public static void StoreSalesOrders(ref DBCache dbcache, ZstHssSalesOrder[] zstHssSalesOrders, ZstHssSalesOrderItem[] zstHssSalesOrderItems, long userID = 0, string originalPlant = "", long? dealID = null)
        {
            dbcache.db.SaveChanges();
            //dbcache.PrescanAndCache(zstHssSalesOrders);
            int ii = 0;
            string headerPlant;
            foreach (var zstHssSalesOrder in zstHssSalesOrders)
            {
                ii++;
                Plant plant = null;
                headerPlant = (from soi in zstHssSalesOrderItems where soi.SalesOrderNumber == zstHssSalesOrder.SalesOrderNumber select soi.Plant).FirstOrDefault();
                SAPSalesOrder SAPSalesOrder = dbcache.getSAPSalesOrderByNumber(zstHssSalesOrder.SalesOrderNumber);
                if (originalPlant == "")
                {
                    //plant = dbcache.getPlantBySalesOrg(zstHssSalesOrder.SalesOrganization);
                    plant = dbcache.getPlantBySapCode(headerPlant);
                }
                else
                {
                    plant = dbcache.getPlantBySapCode(originalPlant);
                }

                SAPSoldTo soldTo = dbcache.getSoldToByNumber(zstHssSalesOrder.SoldToNumber);
                SAPShipTo shipTo = dbcache.getShipToByNumber(zstHssSalesOrder.ShipToNumber);

                if (SAPSalesOrder.IsNull())
                {
                    SAPSalesOrder = new SAPSalesOrder();
                    SAPSalesOrder.Number = zstHssSalesOrder.SalesOrderNumber.TrimNull();
                    SAPSalesOrder.DivisionID = (long)Enums.Divisions.Atlas;
                    dbcache.AddSAPSalesOrder(SAPSalesOrder);
                }
                if (SAPSalesOrder.Ponumber.TrimNull() != zstHssSalesOrder.PoNumber.TrimNull())
                {
                    SAPSalesOrder.Ponumber = zstHssSalesOrder.PoNumber.TrimNull();
                }
                if (SAPSalesOrder.Podate != zstHssSalesOrder.PurchaseDate)
                {
                    SAPSalesOrder.Podate = zstHssSalesOrder.PurchaseDate;
                }
                if (zstHssSalesOrder.PurchaseDate.Year > 1950 && (!SAPSalesOrder.Date.HasValue || SAPSalesOrder.Date.Value != zstHssSalesOrder.PurchaseDate))
                {
                    SAPSalesOrder.Date = zstHssSalesOrder.PurchaseDate;
                }
                if (!plant.IsNull() && SAPSalesOrder.Plant != plant)
                {
                    SAPSalesOrder.Plant = plant;
                }
                if (!SAPSalesOrder.SapsoldToID.HasValue || (!soldTo.IsNull() && SAPSalesOrder.SapsoldToID.Value != soldTo.SapshipToID))
                {
                    SAPSalesOrder.SapsoldTo = soldTo;
                }
                if (!SAPSalesOrder.SapshipToID.HasValue || (!shipTo.IsNull() && SAPSalesOrder.SapshipToID.Value != shipTo.SapshipToID))
                {
                    SAPSalesOrder.SapshipTo = shipTo;
                }
                if (SAPSalesOrder.CreditStatus.TrimNull() != zstHssSalesOrder.CreditStatus.TrimNull())
                {
                    SAPSalesOrder.CreditStatus = zstHssSalesOrder.CreditStatus.TrimNull();
                }
                if (SAPSalesOrder.DeliveryBlock.TrimNull() != zstHssSalesOrder.DeliveryBlock.TrimNull())
                {
                    SAPSalesOrder.DeliveryBlock = zstHssSalesOrder.DeliveryBlock.TrimNull();
                }
                if (SAPSalesOrder.DeliveryBlockText.TrimNull() != zstHssSalesOrder.DeliveryBlockText.TrimNull())
                {
                    SAPSalesOrder.DeliveryBlockText = zstHssSalesOrder.DeliveryBlockText.TrimNull();
                }
                if (SAPSalesOrder.DocumentType.TrimNull() != zstHssSalesOrder.DocumentType.TrimNull())
                {
                    SAPSalesOrder.DocumentType = zstHssSalesOrder.DocumentType.TrimNull();
                }
                if (SAPSalesOrder.DistributionChannel.TrimNull() != zstHssSalesOrder.DistributionChannel.TrimNull())
                {
                    SAPSalesOrder.DistributionChannel = zstHssSalesOrder.DistributionChannel.TrimNull();
                }

                //update deal id in sales order check for sold to and ship to
                #region Deal discount-----

                isDealExist = GetDealsBySoldToIDDealID(soldTo.SapshipToID, shipTo.SapshipToID, plant.LocationID, (zstHssSalesOrder.PortalDeal == null || zstHssSalesOrder.PortalDeal == "") ? 0 : zstHssSalesOrder.PortalDeal.ToLong());
                if (dealID != null)
                {
                    SAPSalesOrder.DealID = dealID;
                }
                else
                {
                    if (isDealExist)
                    {
                        if (SAPSalesOrder.DealID != zstHssSalesOrder.PortalDeal.ToLong())
                        {
                            isDealAppliedInSAP = true;
                        }

                        SAPSalesOrder.DealID = zstHssSalesOrder.PortalDeal.ToLong();
                    }
                    else
                    {
                        if (SAPSalesOrder.DealID != null && zstHssSalesOrder.PortalDeal == "")
                        {
                            RemoveDealFromPortal(dbcache, SAPSalesOrder, zstHssSalesOrder.PortalDeal.ToLong());
                        }
                    }
                }

                #endregion

                if (userID != 0)
                {
                    SAPSalesOrder.UserID = userID;
                }
                SAPSalesOrder.YourReference = zstHssSalesOrder.YourReference.IsNull() ? "" : zstHssSalesOrder.YourReference;

                if (ii % 100 == 0)
                {
                    dbcache.db.SaveChanges();
                }
            }

            dbcache.db.SaveChanges();
        }

        public static SAPSalesOrder StoreSalesOrderItems(ref DBCache dbcache, ZstHssSalesOrderItem[] zstHssSalesOrderItems, List<SAPSalesOrderItem> alreadyBackloggedItems, bool forceBacklogTheseItems = false, bool calledFromBacklog = false, long? dealID = null)
        {
            dbcache.db.SaveChanges();

            //dbcache.PrescanAndCache(zstHssSalesOrderItems);	

            int insertedCount = 0;
            int checkedForUpdatesCount = 0;
            int rejectedCount = 0;

            if (alreadyBackloggedItems.IsNull())
            {
                alreadyBackloggedItems = new List<SAPSalesOrderItem>();
            }
            else
            {
                forceBacklogTheseItems = true;
            }

            List<string> rejectedMaterialNumbers = new List<string>();
            SAPSalesOrder SAPSalesOrder = null;
            int ii = 0;
            #region foreach (ZstHssSalesOrderItem zstHssSalesOrderItem in zstHssSalesOrderItems)
            foreach (ZstHssSalesOrderItem zstHssSalesOrderItem in zstHssSalesOrderItems)
            {
                ii++;
                Plant plant;
                if (!zstHssSalesOrderItem.SalesOrderNumber.TrimNull().jIsEmpty())
                {
                    SAPMaterial SAPMaterial = dbcache.getSAPMaterial(zstHssSalesOrderItem.MaterialNumber);

                    if (!SAPMaterial.IsNull())
                    {
                        SAPShipTo salesOrderSAPShipTo = dbcache.getShipToByNumber(zstHssSalesOrderItem.SalesOrderShipToNumber);

                        if (ConfigurationManager.AppSettings["InterCompany"] == "")
                        {
                            //Commented line below for Plymouth reorg stock transfer process
                            //plant = dbcache.getPlantBySalesOrg(zstHssSalesOrderItem.SalesOrganization);

                            plant = dbcache.getPlantBySapCode(zstHssSalesOrderItem.Plant);
                        }
                        else
                        {
                            //intercompamy process implementation 06/27/2018
                            plant = dbcache.getPlantBySapCode(zstHssSalesOrderItem.Plant);
                        }

                        SAPSalesOrder = dbcache.getSAPSalesOrderByNumber(zstHssSalesOrderItem.SalesOrderNumber);
                        SAPSoldTo SAPSoldTo = dbcache.getSoldToByNumber(zstHssSalesOrderItem.SoldToNumber);
                        SAPShipTo SAPShipTo = dbcache.getShipToByNumber(zstHssSalesOrderItem.ShipToNumber);
                        SAPSalesOrderItem SAPSalesOrderItem = dbcache.getSAPSalesOrderItem(zstHssSalesOrderItem.SalesOrderNumber, zstHssSalesOrderItem.ItemNumber);

                        if (SAPShipTo.IsNull())
                        {
                            SAPShipTo = SAPSalesOrder.SapshipTo;
                        }

                        bool needUpdating = NeedUpdating(SAPSalesOrder, SAPSalesOrderItem, zstHssSalesOrderItem);

                        //if (SAPSalesOrder.IsNull()) {
                        //  SAPSalesOrder = new SAPSalesOrder();
                        //  SAPSalesOrder.DivisionID = (long)Enums.Divisions.Atlas;
                        //  SAPSalesOrder.Number = zstHssSalesOrderItem.SalesOrderNumber.Trim();
                        //  if (zstHssSalesOrderItem.PurchaseOrderDate.Year > 1950) {
                        //    SAPSalesOrder.Date = zstHssSalesOrderItem.PurchaseOrderDate;
                        //  }
                        //  dbcache.AddSAPSalesOrder(SAPSalesOrder);
                        //  dbcache.db.SaveChanges();
                        //  insertedCount++;
                        //} else {
                        //  checkedForUpdatesCount++;
                        //}

                        //if (needUpdating) {
                        //  SAPSalesOrder.PONumber = zstHssSalesOrderItem.PoNumber;
                        //  SAPSalesOrder.DocumentType = zstHssSalesOrderItem.DocumentType.Trim();
                        //  SAPSalesOrder.DistributionChannel = zstHssSalesOrderItem.DistributionChannel.Trim();
                        //}
                        if (SAPSalesOrderItem.IsNull())
                        {
                            SAPSalesOrderItem = new SAPSalesOrderItem();
                            SAPSalesOrderItem.Position = zstHssSalesOrderItem.ItemNumber.ToInt();
                            SAPSalesOrderItem.SapsalesOrder = SAPSalesOrder;
                            dbcache.AddSAPSalesOrderItem(SAPSalesOrderItem);
                        }
                        else if (alreadyBackloggedItems.Contains(SAPSalesOrderItem))
                        {
                            alreadyBackloggedItems.Remove(SAPSalesOrderItem);
                        }
                        if (SAPSalesOrderItem.Sapmaterial != SAPMaterial) SAPSalesOrderItem.Sapmaterial = SAPMaterial;

                        if (zstHssSalesOrderItem.PiecesPerBundle > 0)
                        {
                            SAPSalesOrderItem.PiecesPerBundle = zstHssSalesOrderItem.PiecesPerBundle;
                        }

                        if (SAPSalesOrderItem.PiecesPerBundle.IsNull() || SAPSalesOrderItem.PiecesPerBundle == 0)
                        {
                            SAPSalesOrderItem.PiecesPerBundle = 1;
                        }

                        if (needUpdating)
                        {
                            SAPSalesOrderItem.MaterialShortDescription = zstHssSalesOrderItem.MaterialShortDescription.TrimNull();
                            SAPSalesOrderItem.Price = zstHssSalesOrderItem.Price.ToDecimal();
                            SAPSalesOrderItem.CustomerMaterialNumber = zstHssSalesOrderItem.CustomerMaterialNumber.TrimNull();
                            SAPSalesOrderItem.Plant = plant;
                            SAPSalesOrderItem.ReadyDate = zstHssSalesOrderItem.ReadyDate.ToNullableDate();
                            SAPSalesOrderItem.RollDate = zstHssSalesOrderItem.RollDate.ToNullableDate();
                            SAPSalesOrderItem.RejectionCode = zstHssSalesOrderItem.LineRejected;
                            if (zstHssSalesOrderItem.InProcessing.ToUpper() == "P")
                            {
                                SAPSalesOrderItem.RequirementsType = "P" + zstHssSalesOrderItem.RequirementsType.TrimNull();
                            }
                            else if (calledFromBacklog || SAPSalesOrderItem.RequirementsType.IsNull() || !SAPSalesOrderItem.RequirementsType.ToUpper().StartsWith("P"))
                            {
                                SAPSalesOrderItem.RequirementsType = zstHssSalesOrderItem.RequirementsType.TrimNull();
                            }

                            if (zstHssSalesOrderItem.AvailabilityDate != DateTime.MinValue)
                            {
                                SAPSalesOrderItem.MaterialStagingDate = zstHssSalesOrderItem.AvailabilityDate.ToNullableDate();
                            }

                            SAPSalesOrderItem.OpenQuantity = zstHssSalesOrderItem.OpenQuantity.ToDecimal();
                            SAPSalesOrderItem.BaseUnit = zstHssSalesOrderItem.BaseUnit.TrimNull();
                            SAPSalesOrderItem.ScheduleLineDate = zstHssSalesOrderItem.ScheduleLineDate.ToNullableDate();
                            SAPSalesOrderItem.ScheduleOrderQuantity = zstHssSalesOrderItem.ScheduleOrderQuantity.ToDecimal();
                            SAPSalesOrderItem.ConfirmedQuantity = zstHssSalesOrderItem.ConfirmedQuantity.ToDecimal();
                            SAPSalesOrderItem.SalesUnit = zstHssSalesOrderItem.SalesUnit.TrimNull();

                            var SAPSalesOrderItemGrossWeight = SAPSalesOrderItem.GrossWeight == null ? 0 : SAPSalesOrderItem.GrossWeight;
                            SAPSalesOrderItem.GrossWeight = zstHssSalesOrderItem.GrossWeight.ToDecimal();

                            SAPSalesOrderItem.OrderQuantity = zstHssSalesOrderItem.OrderQuantity.ToDecimal();
                            SAPSalesOrderItem.ItemCategory = zstHssSalesOrderItem.ItemCategory.TrimNull();
                            SAPSalesOrderItem.DeliveryStatus = zstHssSalesOrderItem.DeliveryStatus.TrimNull();
                            SAPSalesOrderItem.PolineNumber = zstHssSalesOrderItem.CustomerPoItem.TrimNull();
                            //Update deal indicator flag for deal applied
                            #region Deal discount-----

                            bool isUpdated = false;
                            //if (isDealExist)
                            isUpdated = UpdateDeals(plant.LocationID, dealID != null ? dealID : SAPSalesOrder.DealID, SAPMaterial, zstHssSalesOrderItem, SAPSalesOrderItem, SAPSalesOrderItemGrossWeight);

                            //if (deal_items != null)
                            //{
                            //    for (int j = 0; j < deal_items.Length; j++)
                            //    {
                            //        //if (!deal_items.FirstOrDefault(x => x.ItemNumber.Contains(SAPSalesOrderItem.Position.ToString())).IsNull())
                            //        //    SAPSalesOrderItem.DealIndicator = true;
                            //        //else
                            //        //    SAPSalesOrderItem.DealIndicator = false;

                            //        if (!deal_items.FirstOrDefault(x => x.ItemNumber.Contains(SAPSalesOrderItem.Position.ToString())).IsNull())
                            //            isUpdated = UpdateDeals(plant.LocationID, dealID != null ? dealID : SAPSalesOrder.DealID, SAPMaterial, zstHssSalesOrderItem, SAPSalesOrderItem);
                            //    }
                            //}
                            //else
                            //{
                            //    //SAPSalesOrderItem.DealIndicator = false;

                            //    //if (!deal_items.FirstOrDefault(x => x.ItemNumber.Contains(SAPSalesOrderItem.Position.ToString())).IsNull())
                            //    //    isUpdated = UpdateDeals(plant.LocationID, dealID != null ? dealID : SAPSalesOrder.DealID, SAPMaterial, zstHssSalesOrderItem.GrossWeight, zstHssSalesOrderItem.LineRejected);
                            //}
                            #endregion

                            SAPSalesOrderItem.SetOpenPieces();
                        }

                        if (forceBacklogTheseItems)
                        {
                            if (SAPSalesOrderItem.DeliveryStatus == "*")
                            {
                                SAPSalesOrderItem.Backlog = false;
                            }
                            else if (SAPSalesOrderItem.Backlog != forceBacklogTheseItems)
                            {
                                SAPSalesOrderItem.Backlog = forceBacklogTheseItems;
                            }
                        }

                        if (calledFromBacklog)
                        {
                            SAPSalesOrderItem.SetReleasedProperties(ref dbcache);
                        }

                        if (SAPSalesOrderItem.SapshipTo != SAPShipTo)
                        {
                            SAPSalesOrderItem.SapshipTo = SAPShipTo;

                            #region Move Items if they are in the wrong cart

                            List<long> shippingCartItemIDs = (from cartItem in SAPSalesOrderItem.ShippingCartSapsalesOrderItems select cartItem.ShippingCartSapsalesOrderItemID).ToList();
                            foreach (long shippingCartItemID in shippingCartItemIDs)
                            {
                                ShippingCartSAPSalesOrderItem shippingCartSAPSalesOrderItem = (from cartItem in SAPSalesOrderItem.ShippingCartSapsalesOrderItems where cartItem.ShippingCartSapsalesOrderItemID == shippingCartItemID select cartItem).FirstOrDefault();

                                long userID = shippingCartSAPSalesOrderItem.ShippingCart.UserID;

                                if (!shippingCartSAPSalesOrderItem.IsNull() && shippingCartSAPSalesOrderItem.ShippingCart.ShipTo != SAPShipTo.SapshipToID.ToString())
                                {
                                    decimal quantity = shippingCartSAPSalesOrderItem.CartQuantity;
                                    ShippingCart otherCart = (from cart in shippingCartSAPSalesOrderItem.ShippingCart.User.ShippingCarts
                                                              where cart.ShipTo == SAPSalesOrderItem.SapshipTo.SapshipToID.ToString()
                                                              && cart.UserID == userID
                                                              select cart).FirstOrDefault();

                                    if (otherCart.IsNull() || otherCart.ShippingCartID <= 0)
                                    {
                                        otherCart = new ShippingCart();
                                        otherCart.UserID = userID;

                                        otherCart.Plant = plant.LocationID.ToString();

                                        if (!SAPShipTo.IsNull())
                                        {
                                            otherCart.ShipTo = SAPShipTo.SapshipToID.ToString();
                                        }
                                        if (!SAPSoldTo.IsNull())
                                        {
                                            otherCart.SoldTo = SAPSoldTo.SapshipToID.ToString();
                                        }
                                        dbcache.db.ShippingCarts.Add(otherCart);
                                    }

                                    ShippingCartSAPSalesOrderItem otherCartItem = (from cartItem in otherCart.ShippingCartSapsalesOrderItems
                                                                                   where cartItem.ShippingCartSapsalesOrderItemID == shippingCartItemID
                                                                                   select cartItem).FirstOrDefault();

                                    if (otherCartItem.IsNull() || otherCartItem.ShippingCartSapsalesOrderItemID <= 0)
                                    {
                                        otherCartItem = new ShippingCartSAPSalesOrderItem();
                                        otherCartItem.SapsalesOrderItem = SAPSalesOrderItem;
                                        otherCart.ShippingCartSapsalesOrderItems.Add(otherCartItem);
                                        otherCartItem.CartQuantity = shippingCartSAPSalesOrderItem.CartQuantity;
                                    }
                                    else
                                    {
                                        otherCartItem.CartQuantity += shippingCartSAPSalesOrderItem.CartQuantity;
                                    }
                                    decimal maxQuantity = SAPSalesOrderItem.OrderQuantity.ToDecimal();

                                    if (otherCartItem.CartQuantity > maxQuantity)
                                    {
                                        otherCartItem.CartQuantity = maxQuantity;
                                    }

                                    dbcache.db.ShippingCartSapsalesOrderItems.Remove(shippingCartSAPSalesOrderItem);
                                }
                            }

                            #endregion
                        }

                    }
                    else
                    {
                        rejectedMaterialNumbers.Add(zstHssSalesOrderItem.MaterialNumber);
                        rejectedCount++;
                    }
                }
                if (ii % 100 == 0)
                {
                    dbcache.db.SaveChanges();
                }
            }

            foreach (SAPSalesOrderItem alreadyBackloggedItem in alreadyBackloggedItems)
            {
                alreadyBackloggedItem.Backlog = false;
            }
            #endregion

            dbcache.db.SaveChanges();

            return SAPSalesOrder;
        }

        public static bool NeedUpdating(SAPSalesOrder SAPSalesOrder, SAPSalesOrderItem SAPSalesOrderItem, ZstHssSalesOrderItem zstHssSalesOrderItem)
        {
            if (SAPSalesOrder.IsNull()) return true;
            if (isDealAppliedInSAP) { isDealAppliedInSAP = false; return true; }
            if (SAPSalesOrderItem.IsNull()) return true;
            if (zstHssSalesOrderItem.PurchaseOrderDate.Year > 1950)
            {
                if (!SAPSalesOrder.Date.HasValue) return true;
                if (SAPSalesOrder.Date.Value != zstHssSalesOrderItem.PurchaseOrderDate) return true;
            }

            if (SAPSalesOrder.Ponumber != zstHssSalesOrderItem.PoNumber ||
            SAPSalesOrder.DocumentType != zstHssSalesOrderItem.DocumentType.TrimNull() ||
            SAPSalesOrder.DistributionChannel != zstHssSalesOrderItem.DistributionChannel.TrimNull()
            ) return true;

            if (
                (zstHssSalesOrderItem.InProcessing.TrimNull().ToUpper() == "P" && !SAPSalesOrderItem.RequirementsType.TrimNull().Contains("P")) ||
                (zstHssSalesOrderItem.InProcessing.TrimNull().ToUpper() != "P" && SAPSalesOrderItem.RequirementsType.TrimNull().Contains("P")) ||
            SAPSalesOrderItem.MaterialShortDescription.TrimNull() != zstHssSalesOrderItem.MaterialShortDescription.TrimNull() ||
            SAPSalesOrderItem.Price != zstHssSalesOrderItem.Price.ToDecimal() ||
            SAPSalesOrderItem.CustomerMaterialNumber.TrimNull() != zstHssSalesOrderItem.CustomerMaterialNumber.TrimNull() ||
            SAPSalesOrderItem.RequirementsType.TrimNull() != zstHssSalesOrderItem.RequirementsType.TrimNull() ||
            SAPSalesOrderItem.ReadyDate != zstHssSalesOrderItem.ReadyDate.ToNullableDate()) return true;
            if (
            SAPSalesOrderItem.MaterialStagingDate != zstHssSalesOrderItem.AvailabilityDate.ToNullableDate() ||
            SAPSalesOrderItem.RollDate != zstHssSalesOrderItem.RollDate.ToNullableDate() ||
            SAPSalesOrderItem.OpenQuantity != zstHssSalesOrderItem.OpenQuantity.ToDecimal() ||
            SAPSalesOrderItem.BaseUnit != zstHssSalesOrderItem.BaseUnit.TrimNull() ||
            SAPSalesOrderItem.ScheduleLineDate != zstHssSalesOrderItem.ScheduleLineDate.ToNullableDate()) return true;
            if (!SAPSalesOrderItem.Plant.IsNull())
            {
                if (
                SAPSalesOrderItem.ScheduleOrderQuantity != zstHssSalesOrderItem.ScheduleOrderQuantity.ToDecimal() ||
                SAPSalesOrderItem.ConfirmedQuantity != zstHssSalesOrderItem.ConfirmedQuantity.ToDecimal() ||
                SAPSalesOrderItem.SalesUnit.TrimNull() != zstHssSalesOrderItem.SalesUnit.TrimNull() ||
                SAPSalesOrderItem.GrossWeight != zstHssSalesOrderItem.GrossWeight.ToDecimal() ||
                SAPSalesOrderItem.OrderQuantity != zstHssSalesOrderItem.OrderQuantity.ToDecimal() ||
                SAPSalesOrderItem.ItemCategory.TrimNull() != zstHssSalesOrderItem.ItemCategory.TrimNull() ||
                SAPSalesOrderItem.DeliveryStatus.TrimNull() != zstHssSalesOrderItem.DeliveryStatus.TrimNull() ||
                SAPSalesOrderItem.Plant.Code.TrimNull() != zstHssSalesOrderItem.Plant.TrimNull() ||
                SAPSalesOrderItem.RejectionCode.TrimNull() != zstHssSalesOrderItem.LineRejected.TrimNull() ||
                SAPSalesOrderItem.Backlog != true) return true;
            }
            //SAPSalesOrder.SAPSoldTo != sapSoldTo;
            //SAPSalesOrder.Plant = plant;
            //SAPSalesOrder.SAPShipTo = salesOrdersapShipTo;
            //SAPSalesOrderItem.Plant = plant;

            return false;
        }

        public static SAPSalesOrder CreateInAtlasSAP(ref DBCache dbcache, ZstHssCreateSalesOrderIn[] inputArrayList, string email, string SAPSoldToNumber, string SAPShipToNumber, string SAPSalesOrganization, string poNumber, DateTime poDate, DateTime requestedDate, string user, bool zstkOrder, long userID, string comment = "", string originalPlant = "", ZstHssSoConditions[] deal_items = null, DealsDetail dealInformation = null)
        {
            //ArrayList inputArrayList = new ArrayList();

            if (inputArrayList.Count() > 0)
            {
                ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
                portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
                portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];
                portalSalesService.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromMinutes(8);

                ZfmCreateHssSalesOrder createHssSalesOrder = new ZfmCreateHssSalesOrder();
                createHssSalesOrder.ImSoldToNumber = SAPSoldToNumber;
                createHssSalesOrder.ImShipToNumber = SAPShipToNumber;
                createHssSalesOrder.ImSalesOrg = SAPSalesOrganization;
                createHssSalesOrder.ImPoNumber = poNumber;
                createHssSalesOrder.ImPoDate = poDate;
                createHssSalesOrder.ImRequestedDate = requestedDate;
                createHssSalesOrder.ImUser = user;
                createHssSalesOrder.ImZstkOrder = string.Empty;
                createHssSalesOrder.ItItems = inputArrayList; //(ZstHssCreateSalesOrderIn[])inputArrayList.ToArray(typeof(ZstHssCreateSalesOrderIn));
                createHssSalesOrder.ItNotes = new Bapisdtext[] { };
                if (dealInformation != null)
                {
                    if (dealInformation.DealID != null)
                        createHssSalesOrder.ImPortalDeal = dealInformation.DealID.ToString();
                    if (deal_items.Count() > 0)
                        createHssSalesOrder.ItConditions = deal_items;
                    if (dealInformation.FirmFlag == 201)
                    {
                        createHssSalesOrder.ImOrdReason = "201";
                        createHssSalesOrder.ImPoMethod = "FIRM";
                        createHssSalesOrder.ImFirmdt = (DateTime)dealInformation.ShipToDate;
                    }
                }

                List<Bapisdtext> bapiTextList = new List<Bapisdtext>();
                comment = comment.TrimNull();
                int index = 10;
                while (comment.Length > 130)
                {
                    Bapisdtext bapiText = new Bapisdtext();
                    bapiText.TextLine = comment.Substring(0, 130);
                    comment = comment.Substring(130);
                    bapiText.TextId = index.ToString();
                    index += 10;
                    bapiTextList.Add(bapiText);
                }
                if (comment.Length > 0)
                {
                    Bapisdtext bapiText = new Bapisdtext();
                    bapiText.TextLine = comment;
                    bapiText.TextId = index.ToString();
                    bapiTextList.Add(bapiText);
                }
                createHssSalesOrder.ItNotes = bapiTextList.ToArray();

                DateTime sapStartTime = DateTime.Now;
                DateTime sapEndTime = DateTime.Now;

                portalSalesService.Open();
                ZfmCreateHssSalesOrderResponse createHssSalesOrderResponse = portalSalesService.ZfmCreateHssSalesOrderAsync(createHssSalesOrder);
                portalSalesService.Close();

                SAPSalesOrder SAPSalesOrder = new SAPSalesOrder();

                if (createHssSalesOrderResponse.ExSalesOrderNumber.ToInt() > 0)
                {
                    StoreSalesOrders(ref dbcache, createHssSalesOrderResponse.EtHssSalesOrders, createHssSalesOrderResponse.EtHssSalesOrderItems, userID, originalPlant, dealInformation != null ? dealInformation.DealID : 0);
                    SAPSalesOrder = StoreSalesOrderItems(ref dbcache, createHssSalesOrderResponse.EtHssSalesOrderItems, null, true, true, dealInformation != null ? dealInformation.DealID : 0);

                    List<SAPStock> oldStock = new List<SAPStock>();
                    List<ZstHssCreateSalesOrderIn> stockFromSalesOrderCreate = (from x in inputArrayList where x.ConfigurationNumber != null && x.ConfigurationNumber.TrimNull() != "" select x).Distinct().ToList();
                    foreach (ZstHssCreateSalesOrderIn stockItem in stockFromSalesOrderCreate)
                    {
                        SAPStock SAPStock = dbcache.getSAPStock(stockItem);
                        if (!SAPStock.IsNull())
                        {
                            oldStock.Add(SAPStock);
                        }
                    }
                    //var stockList = (from x in inputArrayList where !x.SalesOrderNumber.jIsEmpty() && !x.SalesOrderItemNumber.jIsEmpty() select new { x.SalesOrderNumber, x.SalesOrderItemNumber }).ToList();
                    //foreach (var stockPair in stockList) {
                    //  SAPStock SAPStock = dbcache.getSAPStockBySalesOrder(stockPair.SalesOrderNumber, stockPair.SalesOrderItemNumber);
                    //}
                    SAPStock.StoreStockItems(ref dbcache, createHssSalesOrderResponse.EtHssPortalStock, oldStock);
                    if (SAPSalesOrder.IsNull())
                    {
                        SAPSalesOrder = new SAPSalesOrder();
                        SAPSalesOrder.Number = createHssSalesOrderResponse.ExSalesOrderNumber.TrimNull();
                    }
                }
                foreach (string bapisdtext in createHssSalesOrderResponse.EtOutput)
                {
                    SAPSalesOrder.BapireturnMessage += bapisdtext + "\r\n";
                }
                SAPSalesOrder.BapireturnMessage = SAPSalesOrder.BapireturnMessage.TrimNull();
                if (SAPSalesOrder.BapireturnMessage.Length > 2000)
                {
                    SAPSalesOrder.BapireturnMessage = SAPSalesOrder.BapireturnMessage.Substring(0, 2000);
                }
                dbcache.db.SaveChanges();
                return SAPSalesOrder;
            }
            return null;
        }

        public static List<SAPSalesOrderItem> GetBackloggedSalesOrders(PortalEntities db, User user, SAPSoldTo SAPSoldTo = null, SAPShipTo SAPShipTo = null)
        {
            List<SAPSalesOrderItem> backloggedSalesOrderItems = new List<SAPSalesOrderItem>();
            if (SAPSoldTo.IsNull()) return backloggedSalesOrderItems;
            if (!user.IsNull() && !(user is Employee))
            {
                var allsoldtos = user.GetSAPSoldTos();
                if (!allsoldtos.Contains(SAPSoldTo))
                {
                    return backloggedSalesOrderItems;
                }
            }
            if (SAPShipTo.IsNull())
            {
                backloggedSalesOrderItems = (from soi in db.SapsalesOrderItems
                                             where soi.SapsalesOrder.DivisionID == (long)Enums.Divisions.Atlas &&
                                             (soi.Backlog ?? false) == true && soi.SapsalesOrder.SapsoldToID == SAPSoldTo.SapshipToID
                                             select soi).ToList();
            }
            else
            {
                backloggedSalesOrderItems = (from soi in db.SapsalesOrderItems
                                             where soi.SapsalesOrder.DivisionID == (long)Enums.Divisions.Atlas &&
                                                 (soi.Backlog ?? false) == true && soi.SapsalesOrder.SapshipToID == SAPShipTo.SapshipToID
                                             select soi).ToList();
            }
            return backloggedSalesOrderItems;
        }

        public static ZfmCreateHssStoPoResponse CreateSTOinAtlasSAP(string DeliveryDate, string ReceivingPlant, ZsstoCreateInput[] SOlinesArrayList)
        {

            ZfmCreateHssStoPoResponse createSTOResponse = new ZfmCreateHssStoPoResponse();
            if (SOlinesArrayList.Count() > 0)
            {
                ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
                portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
                portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];
                portalSalesService.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromMinutes(8);


                ZfmCreateHssStoPo ZfmCreateHssStoPo = new ZfmCreateHssStoPo();
                ZfmCreateHssStoPo.ImDeliveryDate = DeliveryDate.ToDate().ToString("yyyy-MM-dd");
                ZfmCreateHssStoPo.ImReceivingPlant = ReceivingPlant;
                ZfmCreateHssStoPo.ItSoData = SOlinesArrayList;

                portalSalesService.Open();
                createSTOResponse = portalSalesService.ZfmCreateHssStoPoAsync(ZfmCreateHssStoPo);
                portalSalesService.Close();

                //if (createSTOResponse.ExPurchaseOrderNumber.ToInt() > 0) {

                //  return true;
                //}
            }
            return createSTOResponse;
        }

        public static bool UpdateDeals(long? pplantid = null, long? dealID = null, SAPMaterial SAPMaterial = null, ZstHssSalesOrderItem zstHssSalesOrderItem = null, SAPSalesOrderItem SAPSalesOrderItem = null, decimal? SAPSalesOrderItemGrossWeight = 0)
        {
            PortalEntities db = new PortalEntities();

            DealsDetail dealInformation = new DealsDetail();
            DealsMaterialPricingGroup dealMaterialPricingGroupItem = null;
            DealsPricingGroup dealPricingGroupItem = null;

            dealInformation = (from x in db.DealsDetails where x.DealID == dealID && x.ApprovedDenied == true && x.InActive == false select x).FirstOrDefault();

            if (dealInformation != null)
            {
                //if (dealInformation.DealType == Enums.DealType.Override.ToString())
                //{
                //    dealMaterialPricingGroupItem = db.DealsMaterialPricingGroups.FirstOrDefault(x => x.DealID == dealID && x.ZR00 == SAPMaterial.SAPMaterialPricingGroupID);
                //}
                //if (dealInformation.DealType == Enums.DealType.Discount.ToString())
                //{
                //    dealPricingGroupItem = db.DealsPricingGroups.FirstOrDefault(x => x.DealID == dealID && x.ZR01 == SAPMaterial.SAPPricingGroupID);
                //}

                dealMaterialPricingGroupItem = db.DealsMaterialPricingGroups.FirstOrDefault(x => x.DealID == dealID && x.Zr00 == SAPMaterial.SapmaterialPricingGroupID);
                dealPricingGroupItem = db.DealsPricingGroups.FirstOrDefault(x => x.DealID == dealID && x.Zr01 == SAPMaterial.SappricingGroupID);

                #region save weight used (in tons)
                using (PortalEntities dbPortalEntities = new PortalEntities())
                {
                    dbPortalEntities.Database.OpenConnection();

                    using (var transaction = dbPortalEntities.Database.BeginTransaction())
                    {
                        try
                        {
                            //if (zstHssSalesOrderItem.LineRejected == "")
                            //{
                            if (dealMaterialPricingGroupItem != null)
                            {
                                if (dealMaterialPricingGroupItem.sapcharacteristicOption.Sapcode == "Z1") //844 - Atlas Core Price
                                {
                                    if (dealPricingGroupItem != null)
                                    {
                                        DealsDetail dealDetails = dbPortalEntities.DealsDetails.First<DealsDetail>(i => i.DealID == dealID);
                                        if (zstHssSalesOrderItem.LineRejected == "")
                                        {
                                            if (SAPSalesOrderItemGrossWeight != zstHssSalesOrderItem.GrossWeight.ToDecimal()) //SAP returned - weight calculation
                                            {
                                                dealDetails.TonsUsed = dealDetails.TonsUsed - SAPSalesOrderItemGrossWeight / 2000; //substracting sales order item gross weight
                                                //dealDetails.TonsUsed = dealDetails.TonsUsed == null ? 0 : dealDetails.TonsUsed + zstHssSalesOrderItem.GrossWeight / 2000; //adding SAP sales order item gross weight
                                                //SAPSalesOrderItem.DealIndicator = true;
                                            }
                                            //else if (SAPSalesOrderItem.GrossWeight == zstHssSalesOrderItem.GrossWeight.ToDecimal() && dealID != 0) //portal order - weight calculation
                                            //{
                                            //    dealDetails.TonsUsed = dealDetails.TonsUsed == null ? 0 : dealDetails.TonsUsed + zstHssSalesOrderItem.GrossWeight / 2000;
                                            //    SAPSalesOrderItem.DealIndicator = true;
                                            //}
                                            if (SAPSalesOrderItem.DealIndicator == false || SAPSalesOrderItem.DealIndicator.IsNull())
                                            {
                                                dealDetails.TonsUsed = dealDetails.TonsUsed == null ? 0 : dealDetails.TonsUsed + zstHssSalesOrderItem.GrossWeight / 2000; //adding SAP sales order item gross weight
                                            }
                                            SAPSalesOrderItem.DealIndicator = true;
                                        }
                                        else
                                        {
                                            if (SAPSalesOrderItem.DealIndicator == true)
                                            {
                                                dealDetails.TonsUsed = dealDetails.TonsUsed - zstHssSalesOrderItem.GrossWeight / 2000; //substracting SAP sales order item gross weight
                                                SAPSalesOrderItem.DealIndicator = false;
                                            }
                                        }

                                        dbPortalEntities.SaveChanges();
                                        transaction.Commit();

                                        transaction.Dispose();

                                        return true;
                                    }
                                }
                                else
                                {
                                    DealsDetail dealDetails = dbPortalEntities.DealsDetails.First<DealsDetail>(i => i.DealID == dealID);
                                    if (zstHssSalesOrderItem.LineRejected == "")
                                    {
                                        if (SAPSalesOrderItemGrossWeight != zstHssSalesOrderItem.GrossWeight.ToDecimal()) //SAP returned - weight calculation
                                        {
                                            dealDetails.TonsUsed = dealDetails.TonsUsed - SAPSalesOrderItemGrossWeight / 2000; //substracting sales order item gross weight
                                                                                                                               //dealDetails.TonsUsed = dealDetails.TonsUsed == null ? 0 : dealDetails.TonsUsed + zstHssSalesOrderItem.GrossWeight / 2000; //adding SAP sales order item gross weight
                                                                                                                               //SAPSalesOrderItem.DealIndicator = true;
                                        }
                                        //else if (SAPSalesOrderItem.GrossWeight == zstHssSalesOrderItem.GrossWeight.ToDecimal() && dealID != 0) //portal order - weight calculation
                                        //{
                                        //    dealDetails.TonsUsed = dealDetails.TonsUsed == null ? 0 : dealDetails.TonsUsed + zstHssSalesOrderItem.GrossWeight / 2000;
                                        //    SAPSalesOrderItem.DealIndicator = true;
                                        //}
                                        if (SAPSalesOrderItem.DealIndicator == false || SAPSalesOrderItem.DealIndicator.IsNull())
                                        {
                                            dealDetails.TonsUsed = dealDetails.TonsUsed == null ? 0 : dealDetails.TonsUsed + zstHssSalesOrderItem.GrossWeight / 2000; //adding SAP sales order item gross weight
                                        }
                                        SAPSalesOrderItem.DealIndicator = true;
                                    }
                                    else
                                    {
                                        if (SAPSalesOrderItem.DealIndicator == true)
                                        {
                                            dealDetails.TonsUsed = dealDetails.TonsUsed - zstHssSalesOrderItem.GrossWeight / 2000;
                                            SAPSalesOrderItem.DealIndicator = false;
                                        }
                                    }

                                    dbPortalEntities.SaveChanges();
                                    transaction.Commit();

                                    transaction.Dispose();

                                    return true;
                                }
                            }
                            else
                            {
                                SAPSalesOrderItem.DealIndicator = false;

                                dbPortalEntities.SaveChanges();
                                transaction.Commit();

                                transaction.Dispose();

                                return false;
                            }

                            //}
                            //else
                            //{
                            //    DealsDetail dealDetails = dbPortalEntities.DealsDetails.First<DealsDetail>(i => i.DealID == dealID);
                            //    dealDetails.TonsUsed = dealDetails.TonsUsed - zstHssSalesOrderItem.GrossWeight / 2000;

                            //    SAPSalesOrderItem.DealIndicator = false;

                            //    dbPortalEntities.SaveChanges();
                            //    transaction.Commit();

                            //    transaction.Dispose();

                            //    return true;
                            //}

                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                #endregion
            }

            return false;
        }

        public static bool GetDealsBySoldToIDDealID(long? psoldtoId = null, long? pshiptoid = null, long? pplantid = null, long? dealid = null)
        {
            PortalEntities db = new PortalEntities();

            if (!psoldtoId.HasValue) psoldtoId = 0;

            var getDealIDs = from x in db.DealsBySoldToShipTos
                             join y in db.DealsPlants on x.DealID equals y.DealID
                             where x.SapshipToID == pshiptoid && x.SapsoldToID == psoldtoId && y.PlantID == pplantid && x.DealID == dealid
                             select new { DealID = x.DealID };

            if (getDealIDs.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RemoveDealFromPortal(DBCache dbcache, SAPSalesOrder SAPSalesOrder, long? sapDealID)
        {
            foreach (SAPSalesOrderItem SAPSalesOrderItem in SAPSalesOrder.SapsalesOrderItems.Where(x => x.SapsalesOrder.Number == SAPSalesOrder.Number && x.DealIndicator == true))
            {
                DealsDetail dealDetails = dbcache.db.DealsDetails.FirstOrDefault<DealsDetail>(i => i.DealID == SAPSalesOrder.DealID);
                if (dealDetails != null)
                    dealDetails.TonsUsed = dealDetails.TonsUsed - SAPSalesOrderItem.GrossWeight / 2000;

                SAPSalesOrderItem.DealIndicator = false;
            }

            SAPSalesOrder.DealID = sapDealID == 0 ? null : sapDealID;

            dbcache.db.SaveChanges();
        }
    }
}
