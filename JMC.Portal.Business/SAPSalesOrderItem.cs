using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;
using JMC.Portal.Business.HSSPortalSales;
using JMC.Portal.Business.DataModel;

namespace JMC.Portal.Business {
	public partial class SAPSalesOrderItem {
		#region Properties
		
		public string ScrapPOSDisplayName {
			get {
				return (this.SapsalesOrder is SAPScrapOrder) ? (((SAPScrapOrder)this.SapsalesOrder).Consignment ? "Consign: " + this.CustomerMaterialNumber : this.CustomerMaterialNumber) : string.Empty;
			}
		}

		public decimal WeightPerPiece {
			get {
				if (!this.GrossWeight.HasValue) return 0;
				if (this.OrderedPcs == 0) return 0;
				return (this.GrossWeight ?? 0) / OrderedPcs;
			}
		}

		public decimal WeightPerUnit {
			get {
				if (!this.GrossWeight.HasValue) return 0;
				if ((this.OrderQuantity ?? 0) == 0) return 0;
				return (this.GrossWeight ?? 0) / (this.OrderQuantity ?? 0);
			}
		}

		public int ConfirmedPieces {
			get {
				if (this.UnitIsInBundles) {
					return ((int)(this.ConfirmedQuantity ?? 0)) * this.ItemsPPB;
				}
				return ((int)(this.ConfirmedQuantity ?? 0));
			}
		}

		public bool HasCreditBlock {
			get {
				return (this.SapsalesOrder.CreditStatus ?? "").Trim().ToUpper() == "B";
			}
		}

		public bool HasDeliveryBlock {
			get {
				return (this.SapsalesOrder.DeliveryBlock ?? "").Trim() != "";
			}
		}

		public int ItemsPPB {
			get {
				return ((this.PiecesPerBundle ?? 0) == 0) ? 1 : (this.PiecesPerBundle.Value);
			}
		}
		
		public decimal OrderedPcs {
			get {
				if (this.UnitIsInBundles) {
					return (this.OrderQuantity ?? 0) * this.ItemsPPB;
				}

				return this.OrderQuantity ?? 0;
			}
		}

		public decimal OrderedLbs {
			get {
				return this.OrderedPcs * this.WeightPerPiece;
			}
		}
		
		public decimal ShipCompletePcs {
			get {
				return this.OrderedPcs.ToInt() - (this.DisplayReadyPieces.ToInt() + this.DisplayNotReadyPieces.ToInt() + this.DisplayReleasedPieces.ToInt()) - this.OnBOLPieces.ToInt();
			}
		}

		public decimal ShipCompleteLbs {
			get {
				return (this.OrderedLbs.ToDecimal() - this.OpenQuantity.ToDecimal() - this.OnBOLWeight.ToDecimal()).ToDecimal();
			}
		}

		public decimal ReadyBundles {
			get {
				return this.ReadyPieces.ToDecimal() / this.ItemsPPB;
			}
		}

		public bool UnitIsInBundles {
			get {
				return (this.SalesUnit ?? "").Trim().ToUpper() == "BUN";
			}
		}

		public decimal ReleaseablePcs {
			get {
				return (this.OrderedPcs - this.DisplayReleasedPieces - this.DeliveredPieces).ToDecimal();
			}
		}

		public decimal ReleaseableBundles {
			get {
				return ReleaseablePcs / this.ItemsPPB;
			}
		}

		public decimal ReleasedPeices {
			get {
				if (!_releasedPeices.HasValue) {
					_releasedPeices = (from x in this.WebReleasePlantSapsalesOrderItems where x.SapsalesOrderItemID == this.SapsalesOrderItemID select x.Quantity).Sum();
				}
				return _releasedPeices.Value;
			}
		}
		private decimal? _releasedPeices;

		public int ReleasedPiecesUndeliveredFromPortal {
			get {
				return this.ReleasedPeices.ToInt() - this.DeliveredInPortalPieces.ToInt();
			}
		}

		public int UndeliveredPieces {
			get {
				return this.OrderedPcs.ToInt() - this.DeliveredPieces.ToInt();
			}
		}

		public string ReadyString {
			get {
				return GetReadyString(this.DisplayReadyPieces, this.DisplayReadyWeight);
			}
		}

		public string NotReadyString {
			get {
				return GetNotReadyString(this.DisplayNotReadyPieces, this.DisplayNotReadyWeight);
			}
		}

		public string ReleasedString {
			get {
				return GetReleasedString(this.DisplayReleasedPieces, this.DisplayReleasedWeight);
			}
		}

		public string OnBolString {
			get {
				return GetOnBolString(this.OnBOLPieces, this.OnBOLWeight);
			}
		}

		public string OrderedString {
			get {
				return GetOrderedString(this.OrderedPcs, this.WeightPerPiece);
			}
		}

		public decimal DeliverablePcs {
			get {
				return this.DisplayReleasedPieces.ToDecimal();
			}
		}

		public decimal DeliverableBundles {
			get {
				return DeliverablePcs / this.ItemsPPB;
			}
		}

		public decimal OnBOLWeight {
			get {
				return (from di in this.SapdeliveryItems where !di.Sapdelivery.ActualGoodsMovementDate.HasValue select (di.Weight ?? 0)).Sum();
			}
		}

		public decimal OnBOLPieces {
			get {
				if (this.WeightPerPiece == 0) return 0;
				return this.OnBOLWeight / this.WeightPerPiece;
			}
		}

		public decimal DeliveredWeight {
			get {
				return (from di in this.SapdeliveryItems select (di.Weight ?? 0)).Sum();
			}
		}

		public decimal DeliveredPieces {
			get {
				if (this.WeightPerPiece == 0) return 0;
				return this.DeliveredWeight / this.WeightPerPiece;
			}
		}

		public decimal DeliveredInPortalWeight {
			get {
				return (from di in this.SapdeliveryItems where di.WebReleasePlantSapsalesOrderItems.Count() > 0 select (di.Weight ?? 0)).Sum();
			}
		}

		public decimal DeliveredInPortalPieces {
			get {
				if (this.WeightPerPiece == 0) return 0;
				return this.DeliveredInPortalWeight / this.WeightPerPiece;
			}
		}

		public List<string> StorageQuantityStrings;
        public decimal netvalue;
        public decimal price;
		#endregion

		#region Methods

		#region String Formatting

		public static string GetOrderedString(decimal orderedPcs, decimal weightPerPiece) {
			return orderedPcs.ToString("#,##0.###") + " / " + (orderedPcs * weightPerPiece).ToString("#,##0.###");
		}

		public static string GetOrderedStringByWeight(decimal orderedPcs, decimal orderedWeight) {
			return orderedPcs.ToString("#,##0.###") + " / " + (orderedWeight).ToString("#,##0.###");
		}

		public static string GetOpenString(decimal openPcs, decimal openWeight) {
			return (openPcs.ToInt() == 0) ? string.Empty : openPcs.ToString("#,##0.###") + " / " + (openWeight).ToString("#,##0.###");
		}

		public static string GetNotReadyString(int? displayNotReadyPieces, decimal? displayNotReadyWeight) {
			return (displayNotReadyPieces == 0) ? string.Empty : displayNotReadyPieces.ToDecimal().ToString("#,##0.###") + " / " + displayNotReadyWeight.ToDecimal().ToString("#,##0.###");
		}

		public static string GetReadyString(int? displayReadyPieces, decimal? displayReadyWeight) {
			return (displayReadyPieces == 0) ? string.Empty : displayReadyPieces.ToDecimal().ToString("#,##0.###") + " / " + displayReadyWeight.ToDecimal().ToString("#,##0.###");
		}

		public static string GetReleasedString(int? displayReleasedPieces, decimal? displayReleasedWeight) {
			return (displayReleasedPieces == 0) ? string.Empty : displayReleasedPieces.ToDecimal().ToString("#,##0.###") + " / " + displayReleasedWeight.ToDecimal().ToString("#,##0.###");
		}

		public static string GetOnBolString(decimal onBOLPieces, decimal onBOLWeight) {
			return (onBOLPieces.ToInt() == 0) ? string.Empty : onBOLPieces.ToInt().ToString("#,##0") + " / " + onBOLWeight.ToDecimal().ToString("#,##0.#");
		}

		public static string GetShipCompleteString(decimal shipCompletePieces, decimal shipCompleteWeight) {
			return (shipCompletePieces == 0 || shipCompleteWeight == 0) ? string.Empty : shipCompletePieces.ToInt().ToString("#,##0") + " / " + shipCompleteWeight.ToDecimal().ToString("#,##0.##");
		}

		public static string GetScheduleLineDateString(DateTime? scheduleLineDate) {
			return (scheduleLineDate.HasValue) ? scheduleLineDate.Value.ToShortDateString() : "";
		}

		public static string GetRollDateString(DateTime? rollDate) {
			return (rollDate.HasValue) && !rollDate.IsNullOrMin() ? rollDate.Value.ToShortDateString() : "";
		}

		public static string GetPurchaseDateString(DateTime? purchaseDate) {
			return (purchaseDate.HasValue) ? purchaseDate.Value.ToShortDateString() : "";
		}

        public static string GetPODateString(DateTime? PODate)
        {
            return (PODate.HasValue) ? PODate.Value.ToShortDateString() : "";
        }

		#endregion

		public void SetOpenPieces() {
			this.OpenPieces = this.WeightPerPiece > 0 ? ((this.OpenQuantity.ToDecimal() < 0 ? 0 : this.OpenQuantity.ToDecimal()) / this.WeightPerPiece).ToInt() : 0;
		}

		private void SetReadyProperties() {
			if (this.OpenPieces.IsNull()) {
				this.SetOpenPieces();
			}

			this.NotReadyPieces = 0;
			this.NotReadyWeight = 0;
			this.DisplayNotReadyPieces = 0;
			this.DisplayNotReadyWeight = 0;
			this.ReadyPieces = 0;
			this.ReadyWeight = 0;
			this.DisplayReadyPieces = 0;
			this.DisplayReadyWeight = 0;

			if (!this.Backlog.ToBool()) {
				this.DisplayReleasedPieces = 0;
				this.DisplayReleasedWeight = 0;
			} else {
				if (this.DisplayReleasedPieces.IsNull()) {
					this.DisplayReleasedPieces = 0;
				}

				if (this.RequirementsType.TrimNull().ToUpper().StartsWith("P") || this.RequirementsType.TrimNull().ToUpper() == "KSVV" || (this.RequirementsType.TrimNull().ToUpper() == "Z42" && this.OpenQuantity.ToDecimal() == 0)) {
					this.NotReadyPieces = this.OrderedPcs.ToInt();
					this.DisplayNotReadyPieces = (this.OrderedPcs - this.DisplayReleasedPieces.ToInt() - this.DeliveredPieces) <= 0 ? 0 : (this.OrderedPcs - this.DisplayReleasedPieces.ToInt() - this.DeliveredPieces).ToInt();
				} else if (this.RequirementsType.TrimNull().ToUpper().StartsWith("Z4")) {
					this.NotReadyPieces = this.OpenPieces.ToInt();
					this.DisplayNotReadyPieces = (this.OpenPieces.ToInt() - this.DisplayReleasedPieces.ToInt() - this.DeliveredPieces) <= 0 ? 0 : (this.OpenPieces.ToInt() - this.DisplayReleasedPieces.ToInt() - this.DeliveredPieces).ToInt();
				}

				this.ReadyPieces = (this.OrderedPcs - this.NotReadyPieces.ToInt()) <= 0 ? 0 : (this.OrderedPcs - this.NotReadyPieces.ToInt()).ToInt();
				this.DisplayReadyPieces = (this.OrderedPcs - this.DisplayNotReadyPieces.ToInt() - this.DisplayReleasedPieces.ToInt() - this.DeliveredPieces) <= 0 ? 0 : (this.OrderedPcs - this.DisplayNotReadyPieces.ToInt() - this.DisplayReleasedPieces.ToInt() - this.DeliveredPieces).ToInt();
				this.NotReadyWeight = this.NotReadyPieces.ToInt() * this.WeightPerPiece;
				this.DisplayNotReadyWeight = this.DisplayNotReadyPieces.ToInt() * this.WeightPerPiece;
				this.ReadyWeight = this.ReadyPieces.ToInt() * this.WeightPerPiece;
				this.DisplayReadyWeight = this.DisplayReadyPieces.ToInt() * this.WeightPerPiece;
			}
		}

		public void SetReleasedProperties(ref DBCache dbcache) {
			this.DisplayReleasedPieces = 0;
			this.DisplayReleasedWeight = 0;

			if (this.Backlog.ToBool()) {
				this.DisplayReleasedPieces = (this.ReleasedPeices == 0) ? 0 : ((this.DeliveredInPortalPieces > 0) ? ((this.ReleasedPiecesUndeliveredFromPortal > this.UndeliveredPieces) ? this.UndeliveredPieces : (this.ReleasedPiecesUndeliveredFromPortal + this.DeliveredPieces > this.OrderedPcs ? 0 : this.ReleasedPiecesUndeliveredFromPortal)) : ((this.ReleasedPeices > this.UndeliveredPieces) ? this.UndeliveredPieces : this.ReleasedPeices)).ToInt();

				if (this.DisplayReleasedPieces < 0) {
					this.DisplayReleasedPieces = 0;
				} else if (this.DisplayReleasedPieces > this.OrderedPcs.ToInt() || (!this.ReleasedPeices.IsNull() && this.ReleasedPeices.ToInt() > this.OrderedPcs.ToInt())) {
					this.DisplayReleasedPieces = this.OrderedPcs.ToInt();

					if (this.WebReleasePlantSapsalesOrderItems.Count() > 1) {
						int quantityRemaining = this.OrderedPcs.ToInt();

						foreach (WebReleasePlantSAPSalesOrderItem webReleasePlantSAPSalesOrderItem in this.WebReleasePlantSapsalesOrderItems.OrderBy(wrpsoi => wrpsoi.WebReleasePlant.WebRelease.DateTime)) {
							if (quantityRemaining > 0) {
								if (quantityRemaining > webReleasePlantSAPSalesOrderItem.Quantity) {
									quantityRemaining -= webReleasePlantSAPSalesOrderItem.Quantity.ToInt();
								} else {
									webReleasePlantSAPSalesOrderItem.Quantity = quantityRemaining;
									quantityRemaining = 0;
								}
							} else {
								webReleasePlantSAPSalesOrderItem.Quantity = 0;
							}
						}
					} else if (this.WebReleasePlantSapsalesOrderItems.Count() > 0) {
						this.WebReleasePlantSapsalesOrderItems.First().Quantity = this.OrderedPcs.ToInt();
					}
				}

				if (!dbcache.db.IsNull() && this.DisplayReleasedPieces > 0 && this.WebReleasePlantSapsalesOrderItems.Where(wrpsoi => wrpsoi.WebReleasePlant.Processed != true).Count() == 0) {
					List<WebReleasePlantSAPSalesOrderItem> WebReleasePlantSAPSalesOrderItem = this.WebReleasePlantSapsalesOrderItems.ToList();

					foreach (WebReleasePlantSAPSalesOrderItem webReleasePlantSAPSalesOrderItem in WebReleasePlantSAPSalesOrderItem) {
						List<SAPDeliveryItem> SAPDeliveryItems = webReleasePlantSAPSalesOrderItem.SapdeliveryItem.ToList();

						foreach (SAPDeliveryItem SAPDeliveryItem in SAPDeliveryItems) {
							webReleasePlantSAPSalesOrderItem.SapdeliveryItem.Remove(SAPDeliveryItem);
						}

						this.WebReleasePlantSapsalesOrderItems.Remove(webReleasePlantSAPSalesOrderItem);
						webReleasePlantSAPSalesOrderItem.WebReleasePlant.WebReleasePlantSapsalesOrderItems.Remove(webReleasePlantSAPSalesOrderItem);
						dbcache.db.WebReleasePlantSapsalesOrderItems.Remove(webReleasePlantSAPSalesOrderItem);
					}

					this.DisplayReleasedPieces = 0;
				}

				this.DisplayReleasedWeight = this.WeightPerPiece > 0 ? this.DisplayReleasedPieces.ToInt() * this.WeightPerPiece : 0;
			}

			this.SetReadyProperties();
		}


        public static List<WTC_SalesOrders> WheatLand_PopulateStorageQuantities(string V_ShipTo)
        {

           List<WTC_SalesOrders> SalesOrderList = new List<WTC_SalesOrders>();
          JMC.Portal.Business.WheatLandSales.ZWS_HSS_PORTAL_SALESClient portalSalesService = new JMC.Portal.Business.WheatLandSales.ZWS_HSS_PORTAL_SALESClient("WHEATLAND_PORTAL_SALES");
          
           portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
           portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];
           

            WheatLandSales.ZfmGetHssBacklog ZfmGetHssBacklog1 = new WheatLandSales.ZfmGetHssBacklog();          
   
            ZfmGetHssBacklog1.ImGetSalesOrders = "X";
            ZfmGetHssBacklog1.ImGetDeliveries = "X";
						ZfmGetHssBacklog1.ItSoldToNumbers = new string[] { V_ShipTo };

            portalSalesService.Open();
            WheatLandSales.ZfmGetHssBacklogResponse SAPResponse = portalSalesService.ZfmGetHssBacklogAsync(ZfmGetHssBacklog1);

            var v = SAPResponse.EtHssDeliveryItems.ToString();
            portalSalesService.Close();

            foreach (WheatLandSales.ZstHssSalesOrderItem iso in SAPResponse.EtHssSalesOrderItems)            
            {
                WTC_SalesOrders SO = new WTC_SalesOrders();
                SO.PurchaseOrder = iso.PoNumber;
                SO.CustPart = iso.CustomerMaterialNumber;
                SO.Item = iso.MaterialShortDescription;
               
               
                SO.SalesOrder = iso.SalesOrderNumber.ToInt();
								if (iso.RollDate == "0000-00-00" || iso.RollDate == null) {
									SO.RollDate = "";
								} else{
									SO.RollDate = iso.RollDate;
					    	}

               // SO.Plant = iso.Plant;
              //  SO.OrderedPcsLbs = iso.OrderPieces.ToString() + "/" + iso.WeightPerPiece.ToString();
               SO.OrderedPcsLbs = iso.OrderQuantity.ToInt().ToString() + " / " + iso.GrossWeight.ToInt().ToString();
                SO.DueDate = iso.ScheduleLineDate;
                SO.PoS = iso.ItemNumber.ToInt();
                SO.Order_Quantity = iso.OrderQuantity.ToString();
                SO.CreatedDate = iso.CreatedDate;
             //   SO.PurchaseOrderDate = iso.PurchaseOrderDate;
                SO.PurchaseOrderDate = iso.PurchaseOrderDate.ToString();

                int iPlant = iso.Plant.ToInt();

                PortalEntities db = new PortalEntities();
                
                
                string[] WTCPipleLoc = ConfigurationManager.AppSettings["WTCPipeLocations"].Split();
                List<long> WTCPipleLoc_1 = new List<long>(Array.ConvertAll(ConfigurationManager.AppSettings["WTCPipeLocations"].Split(','), long.Parse));
                Plant plants = (from l in Location.GetAllActive(ref db).OfType<JMC.Portal.Business.Plant>() where l.DivisionID == (long)Enums.Divisions.Wheatland join loc in WTCPipleLoc_1 on l.LocationID equals loc select l).First();

                if (plants != null)
                    SO.Plant = plants.Name;

              //  var Plant_Name = (from l in db.Location where l.LocationID == 1234 select l).ToList();
                
                WheatLandSales.ZstHssSalesOrder soData =(from so in SAPResponse.EtHssSalesOrders where so.SalesOrderNumber == iso.SalesOrderNumber select so).First();
                
                if (soData  !=null )
                {
                SO.DeliveryBlock = soData.DeliveryBlock + " " + soData.DeliveryBlockText;
                SO.CreditBlock = soData.CreditStatus;
                }
                string BOLNumbers = "";
								string ShipmentNumbers = "";
               List<WheatLandSales.ZstHssDeliveryItem> DeliveryItems = (from d in SAPResponse.EtHssDeliveryItems where d.SalesOrderNumber == iso.SalesOrderNumber && d.SalesOrderPosition == iso.ItemNumber select d).ToList();
							 var Deliveries = (from del in DeliveryItems select del.DeliveryNumber).Distinct().ToList(); 
							  foreach(string deliivery in Deliveries){

									WheatLandSales.ZstHssDelivery del = (from d in SAPResponse.EtHssDeliveries where d.DeliveryNumber == deliivery select d).FirstOrDefault();
									if (ShipmentNumbers == "")
										ShipmentNumbers = del.ShipmentNumber;
									else
										ShipmentNumbers = del.ShipmentNumber + ", " + ShipmentNumbers;

								}
								SO.ShipmentNumbers = ShipmentNumbers;

                 decimal d_QuantityDelivered = 0;
                decimal d_WeightDelivered = 0; 
                foreach (WheatLandSales.ZstHssDeliveryItem di in DeliveryItems)
                {
                    if (BOLNumbers == "")
                        BOLNumbers = di.DeliveryNumber;
                    else
                        BOLNumbers = di.DeliveryNumber + ", " + BOLNumbers;

                 //   SO.OnBOLPcsLbs = di.QuantityDelivered + "/" + di.WeightDelivered;
                    d_QuantityDelivered += di.QuantityDelivered;
                    d_WeightDelivered += di.Weight;
                } 
                if (d_QuantityDelivered == null || d_QuantityDelivered == 0)
                    SO.OnBOLPcsLbs = null;
                else
                    SO.OnBOLPcsLbs = d_QuantityDelivered.ToInt() + " / " + d_WeightDelivered.ToInt();

                SO.BolNumbers = BOLNumbers;


                SalesOrderList.Add(SO);
            }
            // Remove where due date is less than 3 years
            int iDueDateNum = ConfigurationManager.AppSettings["WTCSalesOrdersDueDateNumber"].ToInt();
            DateTime dDueDateCond = DateTime.Today.AddYears(-iDueDateNum);


            SalesOrderList = (from d in SalesOrderList where  d.DueDate.ToDate() > dDueDateCond select d).ToList();


            
            return SalesOrderList;

         
        }


		public static void PopulateStorageQuantities(List<SAPSalesOrderItem> SAPSalesOrderItems) {
			string storagequantities;
      ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
			portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
			portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

			ZfmGetHssAvailableMaterial zfmGetHssAvailableMaterial = new ZfmGetHssAvailableMaterial();
			List<ZstHssSalesOrderItem> zstHssSalesOrderItems = new List<ZstHssSalesOrderItem>();

			foreach (SAPSalesOrderItem SAPSalesOrderItem in SAPSalesOrderItems) {
				SAPSalesOrderItem.StorageQuantityStrings = new List<string>();

				ZstHssSalesOrderItem zstHssSalesOrderItem = new ZstHssSalesOrderItem();
				zstHssSalesOrderItem.SalesOrderNumber = SAPSalesOrderItem.SapsalesOrder.Number.TrimNull();
				zstHssSalesOrderItem.ItemNumber = SAPSalesOrderItem.Position.ToString();
				zstHssSalesOrderItem.MaterialNumber = SAPSalesOrderItem.Sapmaterial.Number.TrimNull();
				zstHssSalesOrderItem.Plant = SAPSalesOrderItem.Plant.Code.TrimNull();
				zstHssSalesOrderItem.RequirementsType = SAPSalesOrderItem.RequirementsType.TrimNull();
				zstHssSalesOrderItems.Add(zstHssSalesOrderItem);
			}
			zfmGetHssAvailableMaterial.ItHssSalesOrderItems = zstHssSalesOrderItems.ToArray();


		
			portalSalesService.Open();
			ZfmGetHssAvailableMaterialResponse getHssSalesOrdersResponse = portalSalesService.ZfmGetHssAvailableMaterialAsync(zfmGetHssAvailableMaterial);
			portalSalesService.Close();

			foreach (ZstHssSalesOrderItem zstHssSalesOrderItem in getHssSalesOrdersResponse.EtHssSalesOrderItems) {
				storagequantities = "";
				int position = zstHssSalesOrderItem.ItemNumber.ToInt();
				SAPSalesOrderItem SAPSalesOrderItem = SAPSalesOrderItems.Where(x => x.SapsalesOrder.Number.Trim() == zstHssSalesOrderItem.SalesOrderNumber.Trim()).FirstOrDefault(x => x.Position == position);
				List<ZstHssSalesOrderByDoor> storagebyDoor = getHssSalesOrdersResponse.EtHssSalesOrderItemsDoor.Where(e => e.SalesOrderNumber == zstHssSalesOrderItem.SalesOrderNumber && e.ItemNumber == zstHssSalesOrderItem.ItemNumber && e.Plant == zstHssSalesOrderItem.Plant && e.StorageLocation == zstHssSalesOrderItem.StorageLocation).ToList();

				if (!SAPSalesOrderItem.IsNull()) {
					if (SAPSalesOrderItem.StorageQuantityStrings.IsNull()) {
						SAPSalesOrderItem.StorageQuantityStrings = new List<string>();
					}
					//SAPSalesOrderItem.StorageQuantityStrings.Add("<div id=\"Openorder\"+ zstHssSalesOrderItem.SalesOrderNumber.Trim()+\"_\" + position  +\"_\"+zstHssSalesOrderItem.Plant + \"_\" + zstHssSalesOrderItem.StorageLocation.TrimNull() + \"style=\"display: none; position: absolute; right: 60px; top: 55px; border: 1px solid; width: 300px; height: 400px; z-index: 99999999; background-color: #fff;\" loaded=\"0\"> " + zstHssSalesOrderItem.Plant + "-" + zstHssSalesOrderItem.StorageLocation.TrimNull() + " " + zstHssSalesOrderItem.OpenQuantity.ToString("#,##0.###") + " lbs</div>");
					storagequantities +=  zstHssSalesOrderItem.Plant + "-" + zstHssSalesOrderItem.StorageLocation.TrimNull() + " " + zstHssSalesOrderItem.OpenQuantity.ToString("#,##0.###") + " lbs";
					//storagequantities += "<br/>Door:" + "2" + " - " + "1000" + "lbs";
					//storagequantities += "<br/>Door:" + "3" + " - " + "2000" + "lbs";
					foreach (ZstHssSalesOrderByDoor door in storagebyDoor.OrderBy(d=>d.OpenQuantity)) {
						storagequantities += "<br />Door:" + door.DoorNumber + " - " + door.OpenQuantity.ToString("#,##0.###") + "  lbs";
					}
					//SAPSalesOrderItem.StorageQuantityStrings.Add( zstHssSalesOrderItem.Plant + "-" + zstHssSalesOrderItem.StorageLocation.TrimNull() + " " + zstHssSalesOrderItem.OpenQuantity.ToString("#,##0.###") + " lbs<br/>");
					SAPSalesOrderItem.StorageQuantityStrings.Add(storagequantities);
				}
			}
		}

#endregion
		public string GetAddToCartHtml(int consumedInShippingCartpcs, long groupID, string GridName) {
			StringBuilder returnValue = new StringBuilder(string.Empty);
			Tuple<int, string> shoppingCartCartOption = null;
			List<Tuple<int, string>> options = new List<Tuple<int, string>>();

			if (this.ReleaseablePcs > 0) {
				if (consumedInShippingCartpcs > 0) {
					shoppingCartCartOption = new Tuple<int, string>(consumedInShippingCartpcs.ToInt(), ((consumedInShippingCartpcs / this.ItemsPPB) < 1) ? consumedInShippingCartpcs + " pcs" : (consumedInShippingCartpcs / this.ItemsPPB) + " (" + (consumedInShippingCartpcs - (consumedInShippingCartpcs % this.ItemsPPB)) + " pcs) " + ((consumedInShippingCartpcs % this.ItemsPPB) > 0 ? " + " + (consumedInShippingCartpcs % this.ItemsPPB) + " pcs" : string.Empty) + " " + ((consumedInShippingCartpcs.ToInt() * this.ItemsPPB).ToInt() * this.WeightPerPiece).ToString("#,##0.#") + "lbs");
					options.Add(shoppingCartCartOption);
				}

				options.Add(new Tuple<int, string>(0, "-"));

				for (int count = 1; count <= (int)this.ReleaseableBundles && count < 1000; count++) {
					if (!options.Where(x => x.Item1 == (count * this.ItemsPPB).ToInt()).Any()) {
						options.Add(new Tuple<int, string>((count * this.ItemsPPB).ToInt(), count + " (" + (count * this.ItemsPPB) + "pcs) " + ((count.ToInt() * this.ItemsPPB).ToInt() * this.WeightPerPiece).ToString("#,##0.#") + "lbs"));
					}
				}

				if (this.DisplayNotReadyPieces.ToInt() > 0 && !options.Where( x => x.Item1 == this.DisplayNotReadyPieces.ToInt()).Any()) {
					options.Add(new Tuple<int, string>(this.DisplayNotReadyPieces.ToInt(), (((int)this.DisplayNotReadyPieces / this.ItemsPPB) < 1) ? (int)this.DisplayNotReadyPieces + " pcs" : ((int)this.DisplayNotReadyPieces / this.ItemsPPB) + " (" + ((int)this.DisplayNotReadyPieces - ((int)this.DisplayNotReadyPieces % this.ItemsPPB)) + " pcs) " + (((int)this.DisplayNotReadyPieces % this.ItemsPPB) > 0 ? " + " + ((int)this.DisplayNotReadyPieces % this.ItemsPPB) + " pcs" : string.Empty) + " " + ((this.DisplayNotReadyPieces.ToInt() * this.ItemsPPB).ToInt() * this.WeightPerPiece).ToString("#,##0.#") + "lbs"));
				}

				if (this.DisplayReadyPieces.ToInt() > 0 && !options.Where(x => x.Item1 == this.DisplayReadyPieces.ToInt()).Any()) {
					options.Add(new Tuple<int, string>(this.DisplayReadyPieces.ToInt(), (((int)this.DisplayReadyPieces / this.ItemsPPB) < 1) ? (int)this.DisplayReadyPieces + " pcs" : ((int)this.DisplayReadyPieces / this.ItemsPPB) + " (" + ((int)this.DisplayReadyPieces - ((int)this.DisplayReadyPieces % this.ItemsPPB)) + " pcs) " + (((int)this.DisplayReadyPieces % this.ItemsPPB) > 0 ? " + " + ((int)this.DisplayReadyPieces % this.ItemsPPB) + " pcs" : string.Empty) + " " + ((this.DisplayReadyPieces.ToInt() * this.ItemsPPB).ToInt() * this.WeightPerPiece).ToString("#,##0.#") + "lbs"));
				}

				if (this.ReleaseablePcs.ToInt() > 0 && !options.Where(x => x.Item1 == this.ReleaseablePcs.ToInt()).Any()) {
					options.Add(new Tuple<int, string>(this.ReleaseablePcs.ToInt(), (((int)this.ReleaseablePcs / this.ItemsPPB) < 1) ? (int)this.ReleaseablePcs + " pcs" : ((int)this.ReleaseablePcs / this.ItemsPPB) + " (" + ((int)this.ReleaseablePcs - ((int)this.ReleaseablePcs % this.ItemsPPB)) + " pcs) " + (((int)this.ReleaseablePcs % this.ItemsPPB) > 0 ? " + " + ((int)this.ReleaseablePcs % this.ItemsPPB) + " pcs" : string.Empty) + " " + ((this.ReleaseablePcs.ToInt() * this.ItemsPPB).ToInt() * this.WeightPerPiece).ToString("#,##0.#") + "lbs"));
				}

				if (consumedInShippingCartpcs > 0) {
					returnValue.AppendLine("<img src=\"/images/darkTheme/icons/85.png\" alt=\"Already in shipping cart\" style=\"float: right;\" />");
				}
				returnValue.AppendLine("<select name=\"" + this.SapsalesOrderItemID + "_qty_" + GridName + "\" id=\"" + this.SapsalesOrderItemID + "_qty_" + GridName + "\" class=\"soi" + ((this.DisplayReadyPieces.ToInt() > 0) ? " readysoi" : "") + ((this.DisplayNotReadyPieces.ToInt() > 0) ? " notreadysoi" : "") + "\" groupid=\"" + groupID + "\" wpp=\"" + this.WeightPerPiece.ToString("0.####") + "\" style=\"width: 75px; float: left;\" notready=\"" + this.DisplayNotReadyPieces.ToInt() + "\" ready=\"" + this.DisplayReadyPieces.ToInt() + "\">");
				

				if (!shoppingCartCartOption.IsNull()) {
					returnValue.AppendLine("<option class=\alreadyInCart\" value=\"" + shoppingCartCartOption.Item1 + "\">");
					returnValue.AppendLine(shoppingCartCartOption.Item2);
					returnValue.AppendLine("</option>");
				}

				foreach (Tuple<int, string> option in options.OrderBy(x => x.Item1)) {
					if (shoppingCartCartOption.IsNull() || shoppingCartCartOption.Item1 != option.Item1) {
						returnValue.AppendLine("<option value=\"" + option.Item1 + "\">");
						returnValue.AppendLine(option.Item2);
						returnValue.AppendLine("</option>");
					}
				}

				returnValue.AppendLine("</select>");
				returnValue.AppendLine("<input type=\"hidden\" name=\"soids\" value=\"" + this.SapsalesOrderItemID + "\" />");
			}
			return returnValue.ToString();
		}
	}
}