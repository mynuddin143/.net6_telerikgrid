using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using JMC.Portal.Business.HSSPortalSales;
using JMC.Portal.Business.PortalModels;

namespace JMC.Portal.Business.DataModel {
	public class SAPSalesDeliveryModel {
		#region Properties

		public long SAPDeliveryID { get; set; }
		public string Number { get; set; }
		public decimal? Weight { get; set; }
		public string SalesOrganization { get; set; }
		public string Plant { get; set; }
        public string Invoice { get; set; }
		public string SAPSoldToNumber { get; set; }
		public string SAPSoldToTitle { get; set; }
		public string SAPShipToNumber { get; set; }
		public string SAPShipToTitle { get; set; }
		public string SAPShipToCityState { get; set; }
		public DateTime? CreationDate { get; set; }
		public string CreationDateString {
			get {
				return (this.CreationDate.HasValue) ? this.CreationDate.Value.ToShortDateString() : "";
			}
		}
		public DateTime? TransportationPlanningDate { get; set; }
		public string TransportationPlanningDateString {
			get {
                //return (this.TransportationPlanningDate.HasValue) && !(this.TransportationPlanningDate.IsNullOrMin()) ? this.TransportationPlanningDate.Value.ToString("g") : "";
                return (this.PickupAptDate.HasValue) && !(this.PickupAptDate.IsNullOrMin()) ? this.PickupAptDate.Value.ToString("g") : "";
			}
		}
		public string TransportationPlanningDateStringDate {
			get {
                //return (!this.TransportationPlanningDate.HasValue || (this.TransportationPlanningDate.Value.Hour == 0 && this.TransportationPlanningDate.Value.Minute == 0)) ? "" : this.TransportationPlanningDate.Value.ToShortDateString();
                return (this.PickupAptDate.HasValue) && !(this.PickupAptDate.IsNullOrMin()) ? this.PickupAptDate.Value.ToShortDateString(): "";
			}
		}
		public string TransportationPlanningDateStringTime {
			get {
                //return (!this.TransportationPlanningDate.HasValue || (this.TransportationPlanningDate.Value.Hour == 0 && this.TransportationPlanningDate.Value.Minute == 0)) ? "" : this.TransportationPlanningDate.Value.ToShortTimeString();
                return (this.PickupAptDate.HasValue) && !(this.PickupAptDate.IsNullOrMin()) ? this.PickupAptDate.Value.ToShortTimeString() : "";
			}
		}

		public DateTime? ActualGoodsMovementDate { get; set; }
		public string ActualGoodsMovementDateString {
			get {
				return (this.ActualGoodsMovementDate.HasValue) && !(this.ActualGoodsMovementDate.IsNullOrMin()) ? this.ActualGoodsMovementDate.Value.ToShortDateString() : "";
			}
		}
		public string SAPDeliveryName { get; set; }
		public long? WebReleasePlantID { get; set; }
		public long? WebReleaseID { get; set; }
		public string CustReleaseNumber { get; set; }
		public long? SAPSalesGroupID { get; set; }
		public bool ViewMTR { get; set; }
		public bool Print { get; set; }

        public DateTime? PickupAptDate { get; set; }
        public string PickupAptDateString
        {
            get
            {
                return (this.PickupAptDate.HasValue) && !(this.PickupAptDate.IsNullOrMin()) ? this.PickupAptDate.Value.ToShortDateString() : "";
            }
        }
        public DateTime? CheckoutDate { get; set; }
        public string CheckoutDateString
        {
            get
            {
                return (this.CheckoutDate.HasValue) && !(this.CheckoutDate.IsNullOrMin()) ? this.CheckoutDate.Value.ToShortDateString() : "";
            }
        }
        public DateTime? REQDELDate { get; set; }
        public string REQDELDateString
        {
            get
            {
                return (this.REQDELDate.HasValue) && !(this.REQDELDate.IsNullOrMin()) ? this.REQDELDate.Value.ToShortDateString() : "";
            }
        }
        public DateTime? TMSDELDate { get; set; }
        public string TMSDELDateString
        {
            get
            {
                return (this.TMSDELDate.HasValue) && !(this.TMSDELDate.IsNullOrMin()) ? this.TMSDELDate.Value.ToString("g") : "";
            }
        }
        public DateTime? DeliveredOn { get; set; }
        public string DeliveredOnString
        {
            get
            {
                return (this.DeliveredOn.HasValue) && !(this.DeliveredOn.IsNullOrMin()) ? this.DeliveredOn.Value.ToString("g") : "";
            }
        }

        public string DeliveryLocation { get; set; }
        public string LastUpdate { get; set; }
        public string TMSStatus { get; set; }
        public string DeliveryStatus { get; set; }
        public string ScheduledDelivery  // used for sort and Excel.
        {
            get
            {
                return (this.TMSDELDate.HasValue) && !(this.TMSDELDate.IsNullOrMin()) ? this.TMSDELDate.Value.ToString("g") : "";
            }
        }
        public string DeliveryTrackUrl { get; set; }
        public DateTime? ShippedOn { get; set; }
        public bool CanViewMap { get; set; }

		#endregion

		public SAPSalesDeliveryModel() { }

		public static IQueryable<SAPSalesDeliveryModel> ToModel(IQueryable<SapsalesDelivery> query) {
			return (from x in query
							select new SAPSalesDeliveryModel() {
								SAPDeliveryID = x.SapdeliveryId,
								Number = x.Number.TrimStart('0'),
								Weight = x.Weight,
								Plant = x.Plant != null ? x.Plant.City.Name + ", " + x.Plant.City.State.Abbreviation : string.Empty,
								SAPSoldToNumber = x.SapsoldTo != null ? x.SapsoldTo.TrimmedNumber : string.Empty,
								SAPSoldToTitle = x.SapsoldTo != null ? x.SapsoldTo.TrimmedNumber + " - " + x.SapsoldTo.Name : string.Empty,
								SAPShipToNumber = x.SapshipTo != null ? x.SapshipTo.TrimmedNumber : string.Empty,
								SAPShipToTitle = x.SapshipTo != null ? x.SapshipTo.TrimmedNumber + " - " + x.SapshipTo.Name : string.Empty,
								SAPShipToCityState = x.SapshipTo != null ? (x.SapshipTo.City == null || x.SapshipTo.City.State == null ? string.Empty : x.SapshipTo.City.Name + ", " + x.SapshipTo.City.State.Name) : string.Empty,
								CreationDate = x.CreationDate,
								TransportationPlanningDate = x.TransportationPlanningDate,
								SAPDeliveryName = x.Sapdelivery != null ? x.Sapdelivery.Name : string.Empty,
								WebReleasePlantID = x.WebReleasePlants.Select(wrp => (long?)wrp.WebReleasePlantID).FirstOrDefault(),
								WebReleaseID = x.WebRelease.Select(wr => (long?)wr.WebReleaseID).FirstOrDefault(),
								CustReleaseNumber = x.WebRelease.Select(wr => wr.CustReleaseNumber).FirstOrDefault(),
								SAPSalesGroupID = x.SapsoldTo != null && x.SapsoldTo.SapsalesGroup != null ? x.SapsoldTo.SapsalesGroup.SapsalesGroupID : (long?)null,
                                TMSDELDate = x.Tmsdeldate,
                                PickupAptDate = x.PickupAptDate,
                                REQDELDate = x.Reqdeldate,
                                CheckoutDate =x.CheckoutDate,
                                ActualGoodsMovementDate = x.ActualGoodsMovementDate,
                                ShippedOn = x.ActualGoodsMovementDate //same as ActualGoodsMovementDate
							}).AsQueryable();
		}
	}

	public static partial class ViewModelExtensions {
		public static IQueryable<SAPSalesDeliveryModel> ToModel(this ZstHssDelivery[] query, ZstHssDeliveryItem[] items) {
			return (from x in query
							select new SAPSalesDeliveryModel() {
								SAPDeliveryID = 0,
								Number = x.DeliveryNumber.TrimStart('0'),
								Weight = (x.TotalWeight > 0) ? x.TotalWeight : (
								 (from y in items where y != null && x.DeliveryNumber == y.DeliveryNumber select y.WeightDelivered).Sum() +
														 (from y in items where y != null && x.DeliveryNumber == y.DeliveryNumber select y.WeightSplitBatches).Sum()),
								SalesOrganization = x.SalesOrganization,
								Plant = x.Plant == null ? x.SalesOrganization : x.Plant,
								SAPSoldToNumber = x.SoldToNumber,
								SAPSoldToTitle = x.SoldToNumber,
								SAPShipToNumber = x.ShipToNumber,
								SAPShipToTitle = x.ShipToNumber,
								CreationDate = Convert.ToDateTime(x.CreationDate),
								TransportationPlanningDate = Convert.ToDateTime(x.TransportationPlanningDate),
								SAPDeliveryName = x.Vendor,
								ActualGoodsMovementDate = Convert.ToDateTime(x.ActualGoodsMovementDate),
                                ShippedOn = Convert.ToDateTime(x.ActualGoodsMovementDate), //same as ActualGoodsMovementDate
                                TMSDELDate = x.TmsDelDate.Date.Add(x.TmsDelTime.TimeOfDay),
                                REQDELDate = x.ReqDelDate.Date.Add(x.ReqDelTime.TimeOfDay),
                                PickupAptDate = x.PickupApptDate.Date.Add(x.PickupApptTime.TimeOfDay),
                                CheckoutDate = x.CheckoutDate.Date.Add(x.CheckoutTime.TimeOfDay),
							}).AsQueryable();
		}

		public static List<SAPSalesDeliveryModel> Load(this List<SAPSalesDeliveryModel> query, ref PortalContext db) {
			foreach (SAPSalesDeliveryModel model in query) {		
				//Inter company process 07/16/2018
                Plant plant = Location.GetAllActive(ref db).OfType<Plant>().FirstOrDefault(x => x.Code == (model.Plant == "MAVE" ? "BLYT" : model.Plant == "DETR" ? "PLYM" : model.Plant));
				model.Plant = plant.IsNull() ? model.SalesOrganization : plant.DisplayName;

                SapshipTo SAPSoldTo = db.SapshipTos.OfType<SapsoldTo>().Where(x => x.Number == model.SAPSoldToNumber).FirstOrDefault();
				model.SAPSoldToTitle = SAPSoldTo.IsNull() ? model.SAPSoldToNumber : SAPSoldTo.TrimmedNumber + " " + SAPSoldTo.Name;

                SapshipTo SAPShipTo = db.SapshipTos.Where(x => x.Number == model.SAPShipToNumber).FirstOrDefault();
				model.SAPShipToTitle = SAPShipTo.IsNull() ? model.SAPShipToNumber : SAPShipTo.TrimmedNumber + " " + SAPShipTo.Name;
			}
			return query;
		}

        public static SAPSalesDeliveryModel ToAtlasStatus(this SAPSalesDeliveryModel item, TMSTrackingModel data, bool showUrl, string deliveryTrackUrl, SapshipTo SAPSoldTo)
        {                 
            var noMovement = !item.ActualGoodsMovementDate.HasValue;
            var notTendered = item.SAPDeliveryName == string.Empty;
            var eventData = data.Events.FirstOrDefault(x => x.Delivery.TrimStart('0') == item.Number.TrimStart('0'));
            if (eventData == null)
            {
				item.DeliveryStatus = (item.PickupAptDate.HasValue && noMovement) ? "Schd for Pickup" : "Out for Tender";
                return item; 
	}
            item.DeliveryStatus = eventData.ToDeliveryStatus((showUrl) ? deliveryTrackUrl : "", SAPSoldTo == null? "USD":SAPSoldTo.Currency);
            //IF miles ==0  and TMS Status/SAP Status == Shipped. we treat as delivered irrespective of Tracking Y/N.
            item.CanViewMap = true;
            if (eventData.Tracking != "Y" && !eventData.IsDelivered)
            {
                item.CanViewMap = false;
                if (notTendered && noMovement)
									item.DeliveryStatus = "Out for Tender"; //"Not Scheduled"
                else if (item.PickupAptDate.HasValue && noMovement)
                    item.DeliveryStatus = "Schd for Pickup";
                else if (item.PickupAptDate.HasValue)
                {
                    var shipId = SAPSoldTo == null ? 0 : SAPSoldTo.SapshipToID;
                    if (!showUrl)
                        item.DeliveryStatus = "Shipped";
                    else
                        item.DeliveryStatus = "Shipped <a href=\"#\" title=\"Load Status Inquiry\" onclick=\"javascript:sendISREmail('" + shipId + "','" + item.Number + "')\" class=\"googleLink\">" +
                                    "<img src=\"/images/darkTheme/icons/57.png\" class=\"Icon\" alt=\"Load Status Inquiry\" style=\"float: right;\" /></a> ";
}
            }
            item.DeliveryTrackUrl = string.Format(deliveryTrackUrl, eventData.Delivery);
            item.DeliveryLocation = eventData.ToLastKnownLocation();
            item.TMSStatus = eventData.Status;
            item.LastUpdate = eventData.ToLastKnownDateTime();
            if (item.DeliveryStatus.Contains("Delivered"))
            {
                DateTime dateValue;
                if (DateTime.TryParse(eventData.DeliveryDate, out dateValue))
                    item.DeliveredOn = dateValue;
            }
            return item;
        }
	}
}
