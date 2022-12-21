using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using JMC.Portal.Business;
using JMC.Portal.Business.WheatlandPortal;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace JMC.Portal.Business {
	public  class WTCSAPSalesOrder:SAPSalesOrder  {
        public static void StoreSalesOrders(ref DBCache dbcache, ZstHssSalesOrder[] zstHssSalesOrders, long userID = 0)
        {
            dbcache.db.SaveChanges();
            //dbcache.PrescanAndCache(zstHssSalesOrders);
            int ii = 0;
            SAPSalesOrder SAPSalesOrder = null;
            foreach (var zstHssSalesOrder in zstHssSalesOrders)
            {
                ii++;
                SAPSalesOrder = dbcache.getSAPSalesOrderByNumberWTC(zstHssSalesOrder.SalesOrderNumber);
                Plant plant = dbcache.getPlantBySalesOrgWTC(zstHssSalesOrder.SalesOrganization);
                SAPSoldTo soldTo = dbcache.getSoldToByNumberWTC(zstHssSalesOrder.SoldToNumber);
                SAPShipTo shipTo = dbcache.getShipToByNumberWTC(zstHssSalesOrder.ShipToNumber);

                if (SAPSalesOrder.IsNull())
                {
                    SAPSalesOrder = new SAPSalesOrder();
                    SAPSalesOrder.Number = zstHssSalesOrder.SalesOrderNumber.TrimNull();
                    SAPSalesOrder.DivisionID = (long)Enums.Divisions.Wheatland;
                    dbcache.AddSAPSalesOrder(SAPSalesOrder);
                }
                if (SAPSalesOrder.Ponumber.TrimNull() != zstHssSalesOrder.PoNumber.TrimNull())
                {
                    SAPSalesOrder.Ponumber = zstHssSalesOrder.PoNumber.TrimNull();
                }
                //if (SAPSalesOrder.PODate.Value != zstHssSalesOrder.PurchaseDate)
                //{
                //    SAPSalesOrder.PODate = zstHssSalesOrder.PurchaseDate;
                //}
                //if (zstHssSalesOrder.PurchaseDate.Year > 1950 && (!SAPSalesOrder.Date.HasValue || SAPSalesOrder.Date.Value != zstHssSalesOrder.PurchaseDate))
                //{
                //    SAPSalesOrder.Date = zstHssSalesOrder.PurchaseDate;
                //}
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

                if (userID != 0)
                {
                    SAPSalesOrder.UserID = userID;
                }

                if (ii % 100 == 0)
                {
                    dbcache.db.SaveChanges();
                }
            }

            dbcache.db.SaveChanges();
            
        }

        public static SAPSalesOrder StoreSalesOrderItems(ref DBCache dbcache, ZstHssSalesOrderItemPl[] zstHssSalesOrderItems, List<SAPSalesOrderItem> alreadyBackloggedItems, bool forceBacklogTheseItems = false, bool calledFromBacklog = false)
        {
            dbcache.db.SaveChanges();

            //dbcache.PrescanAndCache(zstHssSalesOrderItems);	

            int insertedCount = 0;
            int checkedForUpdatesCount = 0;
            int rejectedCount = 0;

            //if (alreadyBackloggedItems.IsNull())
            //{
            //    alreadyBackloggedItems = new List<SAPSalesOrderItem>();
            //}
            //else
            //{
            //    forceBacklogTheseItems = true;
            //}

            List<string> rejectedMaterialNumbers = new List<string>();
            SAPSalesOrder SAPSalesOrder = null;
            int ii = 0;
            #region foreach (ZstHssSalesOrderItem zstHssSalesOrderItem in zstHssSalesOrderItems)
            foreach (ZstHssSalesOrderItemPl zstHssSalesOrderItem in zstHssSalesOrderItems)
            {
                ii++;                
                if (!zstHssSalesOrderItem.SalesOrderNumber.TrimNull().jIsEmpty())
                {
                    SAPMaterial SAPMaterial = dbcache.getSAPMaterialWTC(zstHssSalesOrderItem.MaterialNumber);
                    if (!SAPMaterial.IsNull())
                    {
                        SAPShipTo salesOrderSAPShipTo = dbcache.getShipToByNumberWTC(zstHssSalesOrderItem.SalesOrderShipToNumber);
                        Plant plant = dbcache.getPlantBySapCodeWTC(zstHssSalesOrderItem.Plant);
                        SAPSalesOrder = dbcache.getSAPSalesOrderByNumberWTC(zstHssSalesOrderItem.SalesOrderNumber);
                        SAPSoldTo SAPSoldTo = dbcache.getSoldToByNumberWTC(zstHssSalesOrderItem.SoldToNumber);
                        SAPShipTo SAPShipTo = dbcache.getShipToByNumberWTC(zstHssSalesOrderItem.ShipToNumber);
                        SAPSalesOrderItem SAPSalesOrderItem = dbcache.getSAPSalesOrderItemWTC(zstHssSalesOrderItem.SalesOrderNumber, zstHssSalesOrderItem.ItemNumber);
                       
                        if (SAPShipTo.IsNull())
                        {
                            SAPShipTo = SAPSalesOrder.SapshipTo;
                        }

                        //bool needUpdating =  NeedUpdating(SAPSalesOrder, SAPSalesOrderItem, zstHssSalesOrderItem);

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
                        if (SAPSalesOrderItem == null)
                        {
                            DateTime scheduleLineDate;
                            SAPSalesOrderItem = new SAPSalesOrderItem();                            
                            SAPSalesOrderItem.Position = Convert.ToInt32(zstHssSalesOrderItem.ItemNumber);
                            SAPSalesOrderItem.MaterialShortDescription = zstHssSalesOrderItem.MaterialShortDescription;
                            if(DateTime.TryParse(zstHssSalesOrderItem.ScheduleLineDate,out  scheduleLineDate))
                                SAPSalesOrderItem.ScheduleLineDate = scheduleLineDate ;

                            SAPSalesOrderItem.Plant = plant;
                            SAPSalesOrderItem.netvalue = zstHssSalesOrderItem.NetValue;
                            SAPSalesOrderItem.price = zstHssSalesOrderItem.Price;                                                       
                            SAPSalesOrderItem.SapsalesOrder = SAPSalesOrder;
                            dbcache.AddSAPSalesOrderItem(SAPSalesOrderItem);
                            //SAPSalesOrder.SAPSalesOrderItem.Add(SAPSalesOrderItem);
                        }
                        //else if (alreadyBackloggedItems.Contains(SAPSalesOrderItem))
                        //{
                        //    alreadyBackloggedItems.Remove(SAPSalesOrderItem);
                        //}
                        if (SAPSalesOrderItem.Sapmaterial != SAPMaterial) SAPSalesOrderItem.Sapmaterial = SAPMaterial;

                        if (zstHssSalesOrderItem.PiecesPerBundle > 0)
                        {
                            SAPSalesOrderItem.PiecesPerBundle = zstHssSalesOrderItem.PiecesPerBundle;
                        }

                        if (SAPSalesOrderItem.PiecesPerBundle.IsNull() || SAPSalesOrderItem.PiecesPerBundle == 0)
                        {
                            SAPSalesOrderItem.PiecesPerBundle = 1;
                        }

                        /*
                        if (needUpdating)
                        {
                            SAPSalesOrderItem.MaterialShortDescription = zstHssSalesOrderItem.MaterialShortDescription.TrimNull();
                            SAPSalesOrderItem.Price = zstHssSalesOrderItem.Price.ToDecimal();
                            SAPSalesOrderItem.CustomerMaterialNumber = zstHssSalesOrderItem.CustomerMaterialNumber.TrimNull();
                            SAPSalesOrderItem.Plant = plant;
                            SAPSalesOrderItem.ReadyDate = zstHssSalesOrderItem.ReadyDate.ToNullableDate();
                            SAPSalesOrderItem.RollDate = zstHssSalesOrderItem.RollDate.ToNullableDate();

                            if (zstHssSalesOrderItem.InProcessing.ToUpper() == "P")
                            {
                                SAPSalesOrderItem.RequirementsType = "P" + zstHssSalesOrderItem.RequirementsType.TrimNull();
                            }
                            else if (calledFromBacklog || SAPSalesOrderItem.RequirementsType.IsNull() || !SAPSalesOrderItem.RequirementsType.ToUpper().StartsWith("P"))
                            {
                                SAPSalesOrderItem.RequirementsType = zstHssSalesOrderItem.RequirementsType.TrimNull();
                            }

                            //if (zstHssSalesOrderItem.AvailabilityDate != DateTime.MinValue)
                            //{
                            //    SAPSalesOrderItem.MaterialStagingDate = zstHssSalesOrderItem.AvailabilityDate.ToNullableDate();
                            //}

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
                        }
                        */
                        /*
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
                        */
                        if (SAPSalesOrderItem.SapshipTo != SAPShipTo)
                        {
                            SAPSalesOrderItem.SapshipTo = SAPShipTo;

                            #region Move Items if they are in the wrong cart

                            List<long> shippingCartItemIDs = (from cartItem in SAPSalesOrderItem.ShippingCartSapsalesOrderItems select cartItem.ShippingCartSapsalesOrderItemID).ToList();
                            foreach (long shippingCartItemID in shippingCartItemIDs)
                            {
                                ShippingCartSAPSalesOrderItem shippingCartSAPSalesOrderItem = (from cartItem in SAPSalesOrderItem.ShippingCartSapsalesOrderItems
                                                                                               where cartItem.ShippingCartSapsalesOrderItemID == shippingCartItemID
                                                                                               select cartItem).FirstOrDefault();

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

            //foreach (SAPSalesOrderItem alreadyBackloggedItem in alreadyBackloggedItems)
            //{
            //    alreadyBackloggedItem.Backlog = false;
            //}
            #endregion

            dbcache.db.SaveChanges();

            return SAPSalesOrder;
        }
        public static bool NeedUpdating(SAPSalesOrder SAPSalesOrder, SAPSalesOrderItem SAPSalesOrderItem, ZstHssSalesOrderItemPl zstHssSalesOrderItem)
        {
            if (SAPSalesOrder.IsNull()) return true;
            if (SAPSalesOrderItem.IsNull()) return true;
            //if (zstHssSalesOrderItem.PurchaseOrderDate.Year > 1950)
            //{
            //    if (!SAPSalesOrder.Date.HasValue) return true;
            //    if (SAPSalesOrder.Date.Value != zstHssSalesOrderItem.PurchaseOrderDate) return true;
            //}

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
            if (
            SAPSalesOrderItem.ScheduleOrderQuantity != zstHssSalesOrderItem.ScheduleOrderQuantity.ToDecimal() ||
            SAPSalesOrderItem.ConfirmedQuantity != zstHssSalesOrderItem.ConfirmedQuantity.ToDecimal() ||
            SAPSalesOrderItem.SalesUnit.TrimNull() != zstHssSalesOrderItem.SalesUnit.TrimNull() ||
            SAPSalesOrderItem.GrossWeight != zstHssSalesOrderItem.GrossWeight.ToDecimal() ||
            SAPSalesOrderItem.OrderQuantity != zstHssSalesOrderItem.OrderQuantity.ToDecimal() ||
            SAPSalesOrderItem.ItemCategory.TrimNull() != zstHssSalesOrderItem.ItemCategory.TrimNull() ||
            SAPSalesOrderItem.DeliveryStatus.TrimNull() != zstHssSalesOrderItem.DeliveryStatus.TrimNull() ||
            SAPSalesOrderItem.Backlog != true) return true;

            //SAPSalesOrder.SAPSoldTo != sapSoldTo;
            //SAPSalesOrder.Plant = plant;
            //SAPSalesOrder.SAPShipTo = salesOrdersapShipTo;
            //SAPSalesOrderItem.Plant = plant;

            return false;
        }
        public static SAPSalesOrder CreateInWTCSAP(ref DBCache dbcache, ZstHssCreateSalesOrderIn[] inputArrayList, string email, string SAPSoldToNumber, string SAPShipToNumber, string SAPSalesOrganization, string poNumber, DateTime poDate, DateTime requestedDate, string user, bool zstkOrder, long userID, string distChannel,string comments = "")
        {

            zws_portalClient portalService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
            portalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
            portalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];
            ZfmCreateWheatlandSalesOrd salesOrder = new ZfmCreateWheatlandSalesOrd();
            salesOrder.ImSalesLine = inputArrayList;
            salesOrder.ImDivision = "00";
            salesOrder.ImDisChannel = distChannel;
            Bapisdtext test = new Bapisdtext();
            test.ItmNumber = "000"; 
            test.TextLine = comments;
            List<Bapisdtext> ht = new List<Bapisdtext>();
            ht.Add(test);
            salesOrder.ImHeadertext = ht.ToArray();
            salesOrder.ImPoDate = poDate.ToString("yyyy-MM-dd");
            salesOrder.ImRequestedDate = requestedDate.ToString("yyyy-MM-dd");
            salesOrder.ImSalesDoctype = "ZOR";
            salesOrder.ImSalesOrg = SAPSalesOrganization;
            salesOrder.ImShipToNumber = SAPShipToNumber; 
            salesOrder.ImSimInd = "";//"X"; //X for price
            salesOrder.ImSoldToNumber = SAPSoldToNumber;
            salesOrder.ImUser = user;
            salesOrder.ImPoNumber = poNumber; 
            salesOrder.ImDelBlock = "ZP";


            ZfmCreateWheatlandSalesOrdResponse createHssSalesOrderResponse = portalService.ZfmCreateWheatlandSalesOrdAsync(salesOrder);
            portalService.Close();

            SAPSalesOrder SAPSalesOrder = new SAPSalesOrder();
            if (createHssSalesOrderResponse.ExSalesOrderNumber.ToInt() > 0)
            {
                StoreSalesOrders(ref dbcache, createHssSalesOrderResponse.EtHssSalesOrders, userID);

                SAPSalesOrder = StoreSalesOrderItems(ref dbcache, createHssSalesOrderResponse.EtHssSalesOrderItems, null, true, true);

                // List<SAPStock> oldStock = new List<SAPStock>();
                //List<ZstHssCreateSalesOrderIn> stockFromSalesOrderCreate = (from x in inputArrayList where x.ConfigurationNumber != null && x.ConfigurationNumber.TrimNull() != "" select x).Distinct().ToList();
                //foreach (ZstHssCreateSalesOrderIn stockItem in stockFromSalesOrderCreate)
                //{
                //    SAPStock SAPStock = dbcache.getSAPStock(stockItem);
                //    if (!SAPStock.IsNull())
                //    {
                //        oldStock.Add(SAPStock);
                //    }
                //}

                //SAPStock.StoreStockItems(ref dbcache, createHssSalesOrderResponse.EtHssPortalStock, oldStock);
                //if (SAPSalesOrder.IsNull())
                //{
                //    SAPSalesOrder = new SAPSalesOrder();
                //    SAPSalesOrder.Number = createHssSalesOrderResponse.ExSalesOrderNumber.TrimNull();
                //}
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
            else
            {
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
               
            }

            return SAPSalesOrder;
        }

        public static Dictionary<int, string> GetSAPItemPrices(ZstHssCreateSalesOrderIn[] inputArrayList, string SAPSoldToNumber, string SAPShipToNumber, string SAPSalesOrganization, string poNumber, DateTime poDate, DateTime requestedDate, string user, bool zstkOrder, long userID, string distChannel)
        {
            Dictionary<int,string> itemPriceMap = new Dictionary<int,string>();
            zws_portalClient portalService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
            portalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
            portalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];
            ZfmCreateWheatlandSalesOrd salesOrder = new ZfmCreateWheatlandSalesOrd();
            salesOrder.ImSalesLine = inputArrayList;
            salesOrder.ImDivision = "00";
            salesOrder.ImDisChannel = distChannel;
            Bapisdtext test = new Bapisdtext();
            test.ItmNumber = "000";
            test.TextLine = "comments";
            List<Bapisdtext> ht = new List<Bapisdtext>();
            ht.Add(test);
            salesOrder.ImHeadertext = ht.ToArray();
            salesOrder.ImPoDate = poDate.ToString("yyyy-MM-dd");
            salesOrder.ImRequestedDate = requestedDate.ToString("yyyy-MM-dd");
            salesOrder.ImSalesDoctype = "ZOR";
            salesOrder.ImSalesOrg = SAPSalesOrganization;
            salesOrder.ImShipToNumber = SAPShipToNumber;
            salesOrder.ImSimInd = "X"; //X for price
            salesOrder.ImSoldToNumber = SAPSoldToNumber;
            salesOrder.ImUser = user;
            salesOrder.ImPoNumber = poNumber;
            salesOrder.ImDelBlock = "ZP";


            ZfmCreateWheatlandSalesOrdResponse createHssSalesOrderResponse = portalService.ZfmCreateWheatlandSalesOrdAsync(salesOrder);
            portalService.Close();

            foreach (ZstHssSalesOrderItemPl item in createHssSalesOrderResponse.EtHssSalesOrderItems)
            {
                string saleunit = (item.ItemCategory!="")?item.ItemCategory:"FT";
                
                string ppu = (item.PiecesPerBundle > 0) ? item.PiecesPerBundle.ToString() + saleunit : "100" + saleunit;
                string price = (item.Price > 0) ? (item.Price.ToString("#,###.##") + " /" + ppu) : "contact CSR:";
               
                string prices = price + ":" +item.NetValue.ToString();
                itemPriceMap.Add(Convert.ToInt32(item.ItemNumber), prices);
            }
            return itemPriceMap;
        }
	}
}
