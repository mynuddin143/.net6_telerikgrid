using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.AtlasSAPPortal;
using JMC.Portal.Business.WheatlandPortal;
using System.Configuration;

namespace JMC.Portal.Business {
	public partial class SAPScrapOrder : SAPSalesOrder {
		public int YearNoNulls { get { return this.Year.ToInt(); } }
		public int MonthNoNulls { get { return this.Month.ToInt(); } }

		public static void DownloadFromSAP(ref PortalEntities db, string poNumber, int month, int year, bool consignment, long divisionID) {
			if (month > 0 && year > 0 && !poNumber.jIsEmpty()) {
				switch (divisionID) {
					case (long)Enums.Divisions.Atlas:
						ZWS_PORTALClient atcPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
						atcPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
						atcPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

						AtlasSAPPortal.ZGetScrapSalesOrders atcGetScrapSalesOrders = new AtlasSAPPortal.ZGetScrapSalesOrders();
						atcGetScrapSalesOrders.ImPoNumber = poNumber;
						atcGetScrapSalesOrders.SalesOrders = new AtlasSAPPortal.ZstSalesOrder[] { };
						atcGetScrapSalesOrders.SalesOrderItems = new AtlasSAPPortal.ZstSalesOrderItem[] { };

						atcPortalService.Open();
						AtlasSAPPortal.ZGetScrapSalesOrdersResponse atcGetScrapSalesOrdersResponse = atcPortalService.ZGetScrapSalesOrdersAsync(atcGetScrapSalesOrders);
						atcPortalService.Close();

						// Loop through the Scrap Orders returned from SAP webservice, a collection of SAP structure ZSTSalesOrder.
						foreach (AtlasSAPPortal.ZstSalesOrder zstSalesOrder in atcGetScrapSalesOrdersResponse.SalesOrders) {
							// Check if Scrap Order exists in SAPPortal database.
							SAPScrapOrder SAPScrapOrder = (from s in db.SapsalesOrders.OfType<SAPScrapOrder>() where s.Number == zstSalesOrder.SalesOrderNumber select s).FirstOrDefault();

							if (SAPScrapOrder.IsNull()) {
								// If Scrap Order does not exist, create new SAPScrapOrder and assign properties from zstSalesOrder.
								SAPScrapOrder = new SAPScrapOrder();
								SAPScrapOrder.Number = zstSalesOrder.SalesOrderNumber;
								SAPScrapOrder.Ponumber = zstSalesOrder.PoNumber;
								SAPScrapOrder.Month = month;
								SAPScrapOrder.Year = year;
								SAPScrapOrder.Consignment = consignment;

								// Find the SAPSoldTo in the Intranet database that matches the SoldTo Number returned from SAP.
								SAPSoldTo SAPSoldTo = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.Number == zstSalesOrder.SoldToNumber select s).FirstOrDefault();
								long plantID = 0;

								//  *** THIS IS SOME FUNKY BULLSHIT THAT HAS TO DO WITH Harrow AND Plymouth BEING THE SAME COMPANY CODE IN SAP....WE SHOULD LOOK FOR A CLEANER WAY ***
								if (string.IsNullOrEmpty(zstSalesOrder.ShipToPartyCharacter)) {
									if (zstSalesOrder.SalesOrganization == "ATCA") {
										plantID = (long)Enums.Plants.Harrow;
									} else {
										Plant plant = (from l in db.Locations.OfType<Plant>() where l.SalesOrganization == zstSalesOrder.SalesOrganization select l).FirstOrDefault();
										
										if (plant.IsNull()) {
											plantID = (long)Enums.Plants.Harrow;
										} else {
											plantID = plant.LocationID;
										}
									}
								} else {
									plantID = (long)Enums.Plants.Plymouth;
								}

								// If the SAPSoldTo exists in the Intranet database and we were able to determine which Plant to link the order to.
								if (!SAPSoldTo.IsNull() && plantID > 0) {
									// Assign additional properties to the SAPScrapOrder and save it to the SAPPortal database.
									SAPScrapOrder.DivisionID = divisionID;
									SAPScrapOrder.SapsoldTo = SAPSoldTo;
									SAPScrapOrder.PlantID = plantID;

									db.SapsalesOrders.Add(SAPScrapOrder);

									if (!SAPScrapOrder.IsNull()) {
										// If the SAPScrapOrder saved successfully, loop through the Sales Order Items returned from SAP.
										// If they match the current Sales Order we are looping through, save them in the SAPPortal database linked to this SAPScrapOrder.
										foreach (AtlasSAPPortal.ZstSalesOrderItem zstSalesOrderItem in atcGetScrapSalesOrdersResponse.SalesOrderItems) {
											SAPMaterial SAPMaterial = (from s in db.Sapmaterials where s.Number == zstSalesOrderItem.MaterialNumber select s).FirstOrDefault();

											if (!SAPMaterial.IsNull() && zstSalesOrderItem.SalesOrderNumber == SAPScrapOrder.Number) {
												SAPSalesOrderItem SAPSalesOrderItem = new SAPSalesOrderItem();
												SAPSalesOrderItem.SapsalesOrder = SAPScrapOrder;
												SAPSalesOrderItem.CustomerMaterialNumber = zstSalesOrderItem.CustomerMaterialNumber;
												SAPSalesOrderItem.Sapmaterial = SAPMaterial;
												SAPSalesOrderItem.Position = zstSalesOrderItem.ItemNumber.ToInt();
												SAPSalesOrderItem.Price = zstSalesOrderItem.Price;

												SAPScrapOrder.SapsalesOrderItems.Add(SAPSalesOrderItem);
												db.SapsalesOrderItems.Add(SAPSalesOrderItem);
											}
										}
									}
								}
							}
						}

						break;

					case (long)Enums.Divisions.Wheatland:
						zws_portalClient wtcPortalService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
						wtcPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
						wtcPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];

						WheatlandPortal.ZGetScrapSalesOrders wtcGetScrapSalesOrders = new WheatlandPortal.ZGetScrapSalesOrders();
						wtcGetScrapSalesOrders.ImPoNumber = poNumber;

						wtcPortalService.Open();
						WheatlandPortal.ZGetScrapSalesOrdersResponse wtcGetScrapSalesOrdersResponse = wtcPortalService.ZGetScrapSalesOrdersAsync(wtcGetScrapSalesOrders);
						wtcPortalService.Close();

						// Loop through the Scrap Orders returned from SAP webservice, a collection of SAP structure ZSTSalesOrder.
						foreach (WheatlandPortal.ZstSalesOrder zstSalesOrder in wtcGetScrapSalesOrdersResponse.EtSalesOrders) {
							// Check if Scrap Order exists in SAPPortal database.
							SAPScrapOrder SAPScrapOrder = (from s in db.SapsalesOrders.OfType<SAPScrapOrder>() where s.Number == zstSalesOrder.SalesOrderNumber select s).FirstOrDefault();

							if (SAPScrapOrder.IsNull()) {
								// If Scrap Order does not exist, create new SAPScrapOrder and assign properties from zstSalesOrder.
								SAPScrapOrder = new SAPScrapOrder();
								SAPScrapOrder.Number = zstSalesOrder.SalesOrderNumber;
								SAPScrapOrder.Ponumber = zstSalesOrder.PoNumber;
								SAPScrapOrder.Month = month;
								SAPScrapOrder.Year = year;
								SAPScrapOrder.Consignment = consignment;

								// Find the SAPSoldTo in the Intranet database that matches the SoldTo Number returned from SAP.
								SAPSoldTo SAPSoldTo = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.Number == zstSalesOrder.SoldToNumber select s).FirstOrDefault();
								long plantID = 0;

								//  *** THIS IS SOME FUNKY BULLSHIT THAT HAS TO DO WITH Harrow AND Plymouth BEING THE SAME COMPANY CODE IN SAP....WE SHOULD LOOK FOR A CLEANER WAY ***
								if (string.IsNullOrEmpty(zstSalesOrder.ShipToPartyCharacter)) {
									Plant plant = (from l in db.Locations.OfType<Plant>() where l.Code == zstSalesOrder.Plant select l).FirstOrDefault();

									if (plant.IsNull()) {
										plantID = (long)Enums.Plants.ChicagoWheatland;
									} else {
										plantID = plant.LocationID;
									}
								}

								// If the SAPSoldTo exists in the Intranet database and we were able to determine which Plant to link the order to.
								if (!SAPSoldTo.IsNull() && plantID > 0) {
									// Assign additional properties to the SAPScrapOrder and save it to the SAPPortal database.
									SAPScrapOrder.DivisionID = divisionID;
									SAPScrapOrder.SapsoldTo = SAPSoldTo;
									SAPScrapOrder.PlantID = plantID;

									db.SapsalesOrders.Add(SAPScrapOrder);

									if (!SAPScrapOrder.IsNull()) {
										// If the SAPScrapOrder saved successfully, loop through the Sales Order Items returned from SAP.
										// If they match the current Sales Order we are looping through, save them in the SAPPortal database linked to this SAPScrapOrder.
										foreach (WheatlandPortal.ZstSalesOrderItem zstSalesOrderItem in wtcGetScrapSalesOrdersResponse.EtSalesOrderItems) {
											SAPMaterial SAPMaterial = (from s in db.Sapmaterials where s.Number == zstSalesOrderItem.MaterialNumber select s).FirstOrDefault();
											
											if (!SAPMaterial.IsNull() && zstSalesOrderItem.SalesOrderNumber == SAPScrapOrder.Number) {
												SAPSalesOrderItem SAPSalesOrderItem = new SAPSalesOrderItem();
												SAPSalesOrderItem.SapsalesOrder = SAPScrapOrder;
												SAPSalesOrderItem.CustomerMaterialNumber = zstSalesOrderItem.CustomerMaterialNumber;
												SAPSalesOrderItem.Sapmaterial = SAPMaterial;
												SAPSalesOrderItem.Position = zstSalesOrderItem.ItemNumber.ToInt();
												SAPSalesOrderItem.Price = zstSalesOrderItem.Price;

												SAPScrapOrder.SapsalesOrderItems.Add(SAPSalesOrderItem);
												db.SapsalesOrderItems.Add(SAPSalesOrderItem);
											}
										}
									}
								}
							}
						}

						break;
				}


				db.SaveChanges();
			}
		}
	}
}
