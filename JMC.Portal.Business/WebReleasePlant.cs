using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace JMC.Portal.Business {
	public partial class WebReleasePlant {

		public SAPSoldTo FindSoldTo {
			get {
				SAPSoldTo SAPSoldTo = null;
				WebReleasePlantSAPSalesOrderItem webReleasePlantSAPSalesOrderItem = this.WebReleasePlantSapsalesOrderItems.FirstOrDefault();
				if (!webReleasePlantSAPSalesOrderItem.IsNull()) {
					SAPSalesOrderItem SAPSalesOrderItem = webReleasePlantSAPSalesOrderItem.SapsalesOrderItem;
					if (!SAPSalesOrderItem.IsNull()) {
						SAPSalesOrder SAPSalesOrder = SAPSalesOrderItem.SapsalesOrder;
						if (!SAPSalesOrder.IsNull()) {
							SAPSoldTo = SAPSalesOrder.SapsoldTo;
						}
					}
				}
				return SAPSoldTo;
			}
		}

		public SAPShipTo FindShipTo {
			get {
				return (from x in this.WebReleasePlantSapsalesOrderItems select x.SapsalesOrderItem.SapshipTo).FirstOrDefault();
			}
		}

		public List<SAPShipTo> FindShipTos {
			get {
				return (from x in this.WebReleasePlantSapsalesOrderItems select x.SapsalesOrderItem.SapshipTo).Distinct().ToList();
			}
		}

		public bool OKToCreateInSAP {
			get {
				if (!DepartureDate.HasValue) return false;
				if (this.FindShipTos.Count > 1) return false;

				string incoTerms2 = string.Empty;

				foreach (WebReleasePlantSAPSalesOrderItem webReleasePlantSAPSalesOrderItem in this.WebReleasePlantSapsalesOrderItems) {
					if (!(this.Reviewed ?? false)) return false;
					if (webReleasePlantSAPSalesOrderItem.SapsalesOrderItem.HasCreditBlock || webReleasePlantSAPSalesOrderItem.SapsalesOrderItem.HasDeliveryBlock) return false;

					if (incoTerms2 != string.Empty && incoTerms2 != (webReleasePlantSAPSalesOrderItem.SapsalesOrderItem.IsNull() ? string.Empty : (webReleasePlantSAPSalesOrderItem.SapsalesOrderItem.SapshipTo.IsNull() ? string.Empty : webReleasePlantSAPSalesOrderItem.SapsalesOrderItem.SapshipTo.IncoTerms2))) {
						return false;
					}

					incoTerms2 = webReleasePlantSAPSalesOrderItem.SapsalesOrderItem.IsNull() ? string.Empty : (webReleasePlantSAPSalesOrderItem.SapsalesOrderItem.SapshipTo.IsNull() ? string.Empty : webReleasePlantSAPSalesOrderItem.SapsalesOrderItem.SapshipTo.IncoTerms2);

					//if (webReleasePlantSAPSalesOrderItem.Quantity > webReleasePlantSAPSalesOrderItem.SAPSalesOrderItem.ConfirmedPieces) return false;
					//if (webReleasePlantSAPSalesOrderItem.Quantity > (webReleasePlantSAPSalesOrderItem.SAPSalesOrderItem.ReadyPieces - webReleasePlantSAPSalesOrderItem.SAPSalesOrderItem.DeliveredPieces)) return false;

					//if (!webReleasePlantSAPSalesOrderItem.SAPSalesOrderItem.MaterialStagingDate.HasValue) return false;
					//if (this.DepartureDate.Value < webReleasePlantSAPSalesOrderItem.SAPSalesOrderItem.MaterialStagingDate.Value) return false;
				}
				return true;
			}
		}
	
		public bool NotReadyForRelease {
			get {
				
				foreach (WebReleasePlantSAPSalesOrderItem webReleasePlantSAPSalesOrderItem in this.WebReleasePlantSapsalesOrderItems) {
					if (webReleasePlantSAPSalesOrderItem.SapsalesOrderItem.ReadyPieces < webReleasePlantSAPSalesOrderItem.Quantity.ToInt()) return true;
			
				}
				return false;
			}
		}

		public bool STORequired {
			get {
				if (!(this.Plant.LocationID == (long)Enums.Plants.Plymouth || this.Plant.LocationID == (long)Enums.Plants.Harrow) || (from webp in this.WebReleasePlantSapsalesOrderItems select webp.SapsalesOrderItem.Plant).Distinct().Count() == 1) return false;
				if ((from webp in this.WebReleasePlantSapsalesOrderItems select webp.SapsalesOrderItem.Plant).Distinct().Count() > 1) return true;
				//foreach (WebReleasePlantSAPSalesOrderItem webReleasePlantSAPSalesOrderItem in this.WebReleasePlantSAPSalesOrderItem) {

				//  if (webReleasePlantSAPSalesOrderItem.Quantity.ToInt() != webReleasePlantSAPSalesOrderItem.SAPSalesOrderItem.OpenQuantity.ToInt() && webReleasePlantSAPSalesOrderItem.SAPSalesOrderItem.PlantID != webReleasePlantSAPSalesOrderItem.PlantBeforeSTO) {
				//    return true;
				//  }
				//}
				return false;
			}
		}
		public bool lineSplitRequired {
			get {
				if (!(this.Plant.LocationID == (long)Enums.Plants.Plymouth || this.Plant.LocationID == (long)Enums.Plants.Harrow) || (from webp in this.WebReleasePlantSapsalesOrderItems select webp.SapsalesOrderItem.Plant).Distinct().Count() == 1) return false;

				foreach (WebReleasePlantSAPSalesOrderItem webReleasePlantSAPSalesOrderItem in this.WebReleasePlantSapsalesOrderItems.Where(w =>w.SapsalesOrderItem.PlantID != this.XferToPlant)) {

					if (webReleasePlantSAPSalesOrderItem.Quantity.ToInt() != webReleasePlantSAPSalesOrderItem.SapsalesOrderItem.OrderedPcs.ToInt()) {
						return true;
					}
				}
				return false;
			}
		}

		public List<SAPDelivery> SAPDeliveries {
			get {
				List<SAPDelivery> sapDeliveries = new List<SAPDelivery>();
				foreach (DbSet<SAPDeliveryItem> SAPDeliveryList in (from x in this.WebReleasePlantSapsalesOrderItems.ToList() select x.SapdeliveryItem).Distinct().ToList()) {
					foreach (SAPDeliveryItem sdi in SAPDeliveryList) {
						sapDeliveries.Add(sdi.Sapdelivery);
					}
				}
				return sapDeliveries.Distinct().ToList();
			}
		}

		public static WebReleasePlant GetWebReleasePlant(PortalEntities db, User user, long webReleasePlantID) {

			WebReleasePlant webReleasePlant = db.WebReleasePlants.FirstOrDefault(x => x.WebReleasePlantID == webReleasePlantID);

			if (user.IsNull() || user is Employee || webReleasePlant.IsNull() || webReleasePlant.WebRelease.UserID == user.UserID) {
				return webReleasePlant;
			}
			List<SAPSoldTo> SAPSoldTos = user.GetSAPSoldTos();
			var SAPSoldToIDs = (from x in SAPSoldTos select x.SapshipToID).ToList();

			if (webReleasePlant.WebReleasePlantSapsalesOrderItems.Any(y => y.SapsalesOrderItem.SapsalesOrder.SapsoldToID.HasValue && SAPSoldToIDs.Contains(y.SapsalesOrderItem.SapsalesOrder.SapsoldToID.Value))) {
				return webReleasePlant;
			}

			throw new Exception("Permission Denied. User " + user.FullName + " trying to Access a WebRelease " + webReleasePlant.WebReleasePlantID);
			return null;
		}

		public static List<WebReleasePlant> GetWebReleasePlants(PortalEntities db, User user, long? ShipToID, SAPSoldTo SAPSoldTo = null, long plantID = 0, long SAPSalesGroupID = 0, string status = "") {
			List<long> userExcludedSAPShipToes = user.ExcludedSAPShipToes.Select(s => s.SapshipToID).ToList();
			bool userHasExcludedShipToes = userExcludedSAPShipToes.Count() > 0 ? true : false;

            bool OKToCreateInSAP = false;
            bool NotReadyForRelease = false;
            bool STORequired = false; 
            bool NoColor = false;
            bool ALL = false;
            if (status == "Ready")
                OKToCreateInSAP = true;
            if (status == "Not Ready")
                NotReadyForRelease = true;
            if (status == "STO Required")
                STORequired = true;
            if (status == "Blocked")
                NoColor = true;
            //if (status == "ALL" || status == "")
            //    ALL = true;

            if (user is Employee) {
				if (SAPSoldTo.IsNull()) {
                   List<WebReleasePlant> webReleasePlants = db.WebReleasePlants.Where(x => !(x.Processed ?? false))
                            .Where(x => (plantID == 0 || x.PlantID == plantID) && (SAPSalesGroupID == 0 ||
                                (x.WebReleasePlantSapsalesOrderItems.Count() > 0 &&
                                x.WebReleasePlantSapsalesOrderItems.FirstOrDefault() != null &&
                                x.WebReleasePlantSapsalesOrderItems.FirstOrDefault().SapsalesOrderItem != null &&
                                x.WebReleasePlantSapsalesOrderItems.FirstOrDefault().SapsalesOrderItem.SapsalesOrder != null &&
                                x.WebReleasePlantSapsalesOrderItems.FirstOrDefault().SapsalesOrderItem.SapsalesOrder.SapsoldTo != null &&
                                x.WebReleasePlantSapsalesOrderItems.FirstOrDefault().SapsalesOrderItem.SapsalesOrder.SapsoldTo.SapsalesGroupID == SAPSalesGroupID)
                                ))
                            .ToList();

                   if (OKToCreateInSAP)
                       webReleasePlants = webReleasePlants.Where(x => (x.OKToCreateInSAP == OKToCreateInSAP && (x.NotReadyForRelease == NotReadyForRelease))).ToList();
                   if (NotReadyForRelease)
                       webReleasePlants = webReleasePlants.Where(x => (x.NotReadyForRelease == NotReadyForRelease && (x.STORequired == STORequired))).ToList();
                   if (STORequired)
                       webReleasePlants = webReleasePlants.Where(x => (x.STORequired == STORequired)).ToList();
                   if (NoColor)
                       webReleasePlants = webReleasePlants.Where(x => (x.OKToCreateInSAP == OKToCreateInSAP && x.NotReadyForRelease == NotReadyForRelease && x.STORequired == STORequired)).ToList();

					foreach (WebReleasePlant webReleasePlant in webReleasePlants.Where(x => (x.Reviewed ?? false) == true).Where(x => (x.Processed ?? false) == false)) {
						if (!webReleasePlant.WebReleasePlantSapsalesOrderItems.Any(z => !z.SapdeliveryItem.Any())) {
							webReleasePlant.Processed = true;
							db.SaveChanges();
						}
					}
                    ////////////hide sold to for the given user in config///////////////////                 
                    //if (user is Employee) 
                    //{
                    if (ConfigurationManager.AppSettings["HideSoldToForUser"] != null)
                    {
                        string[] hideForUsers = ConfigurationManager.AppSettings["HideSoldToForUser"].ToString().Split(';');
                        foreach (string hideForUser in hideForUsers)
                        {
                            string[] hideSoldToForUser = hideForUser.Split(',');                           
                            string logonUser = (user as Employee).Domain + "\\" + (user as Employee).SamaccountName;
                            if (logonUser == hideSoldToForUser[0])
                            {
                                string[] hideSoldTos = hideSoldToForUser.Skip(1).ToArray();
                                var soldTos = webReleasePlants.Where(x => hideSoldTos.Any(s => x.SoldToText.Contains(s))).ToList();
                                foreach (var soldTo in soldTos)
                                    webReleasePlants.Remove(soldTo);
                            }
                        }
                    }
                    //}
                    ////////////////////////////////////////////////////////////////////////
					return webReleasePlants;
				} else {
                    List<WebReleasePlant> webReleasePlants = db.WebReleasePlants.Where(x => !(x.Processed ?? false)).Where(x =>/* (x.WebRelease.UserID == user.UserID) ||*/ (x.WebReleasePlantSapsalesOrderItems.Any(y => y.SapsalesOrderItem.SapsalesOrder.SapsoldToID.HasValue && y.SapsalesOrderItem.SapsalesOrder.SapsoldToID.Value == SAPSoldTo.SapshipToID && (ShipToID > 0 ? y.SapsalesOrderItem.SapsalesOrder.SapshipTo.SapshipToID == ShipToID : (userHasExcludedShipToes ? !userExcludedSAPShipToes.Contains(y.SapsalesOrderItem.SapsalesOrder.SapshipTo.SapshipToID) : true)))))
                        .Where(x => (plantID == 0 || x.PlantID == plantID) && (SAPSalesGroupID == 0 ||
                            (x.WebReleasePlantSapsalesOrderItems.Count() > 0 &&
                            x.WebReleasePlantSapsalesOrderItems.FirstOrDefault() != null &&
                            x.WebReleasePlantSapsalesOrderItems.FirstOrDefault().SapsalesOrderItem != null &&
                            x.WebReleasePlantSapsalesOrderItems.FirstOrDefault().SapsalesOrderItem.SapsalesOrder != null &&
                            x.WebReleasePlantSapsalesOrderItems.FirstOrDefault().SapsalesOrderItem.SapsalesOrder.SapsoldTo != null &&
                            x.WebReleasePlantSapsalesOrderItems.FirstOrDefault().SapsalesOrderItem.SapsalesOrder.SapsoldTo.SapsalesGroupID == SAPSalesGroupID)
                            )).ToList();

                    if (OKToCreateInSAP)
                        webReleasePlants = webReleasePlants.Where(x => (x.OKToCreateInSAP == OKToCreateInSAP && (x.NotReadyForRelease == NotReadyForRelease))).ToList();
                    if (NotReadyForRelease)
                        webReleasePlants = webReleasePlants.Where(x => (x.NotReadyForRelease == NotReadyForRelease && (x.STORequired == STORequired))).ToList();
                    if (STORequired)
                        webReleasePlants = webReleasePlants.Where(x => (x.STORequired == STORequired)).ToList();
                    if (NoColor)
                        webReleasePlants = webReleasePlants.Where(x => (x.OKToCreateInSAP == OKToCreateInSAP && x.NotReadyForRelease == NotReadyForRelease && x.STORequired == STORequired)).ToList();

                    return webReleasePlants;
				}
			}
			// build list of SAPSoldTosForUser
			List<SAPSoldTo> SAPSoldTos = user.GetSAPSoldTos();
			if (!SAPSoldTo.IsNull() && !SAPSoldTos.Contains(SAPSoldTo)) {
				throw new Exception("Permission Denied. User " + user.FullName + " trying to Access a Sold To " + SAPSoldTo.Number + " " + SAPSoldTo.Name);
			}

			if (SAPSoldTo.IsNull()) {
				var SAPSoldToIDs = (from x in SAPSoldTos select x.SapshipToID).ToList();
                List<WebReleasePlant> webReleasePlants = db.WebReleasePlants.Where(x => !(x.Processed ?? false)).Where(x => /*(x.WebRelease.UserID == user.UserID) || */(x.WebReleasePlantSapsalesOrderItems.Any(y => y.SapsalesOrderItem.SapsalesOrder.SapsoldToID.HasValue && SAPSoldToIDs.Contains(y.SapsalesOrderItem.SapsalesOrder.SapsoldToID.Value) && (ShipToID > 0 ? y.SapsalesOrderItem.SapsalesOrder.SapshipTo.SapshipToID == ShipToID : (userHasExcludedShipToes ? !userExcludedSAPShipToes.Contains(y.SapsalesOrderItem.SapsalesOrder.SapshipTo.SapshipToID) : true))))).ToList();
				return webReleasePlants;
			}
			List<WebReleasePlant> webReleasePlants2 = db.WebReleasePlants.Where(x => !(x.Processed ?? false)).Where(x => /*(x.WebRelease.UserID == user.UserID) || */ (x.WebReleasePlantSapsalesOrderItems.Any(y => y.SapsalesOrderItem.SapsalesOrder.SapsoldToID.HasValue && y.SapsalesOrderItem.SapsalesOrder.SapsoldToID.Value == SAPSoldTo.SapshipToID && (ShipToID > 0 ? y.SapsalesOrderItem.SapsalesOrder.SapshipTo.SapshipToID == ShipToID : (userHasExcludedShipToes ? !userExcludedSAPShipToes.Contains(y.SapsalesOrderItem.SapsalesOrder.SapshipTo.SapshipToID) : true))))).ToList();
			return webReleasePlants2;
		}

		public static List<WebReleasePlant> GetPendingWebReleasePlants(PortalEntities db, User user, long? ShipToID, SAPSoldTo SAPSoldTo = null, long plantID = 0, long SAPSalesGroupID = 0, string status = "") {
            return WebReleasePlant.GetWebReleasePlants(db, user, ShipToID, SAPSoldTo, plantID, SAPSalesGroupID, status).Where(x => (x.Reviewed ?? false) == false).ToList();
		}

		public static List<WebReleasePlant> GetScheduledWebReleasePlants(PortalEntities db, User user, long? ShipToID, SAPSoldTo SAPSoldTo = null, long plantID = 0, long SAPSalesGroupID = 0, string status = "") {
            return WebReleasePlant.GetWebReleasePlants(db, user, ShipToID, SAPSoldTo, plantID, SAPSalesGroupID, status).Where(x => (x.Reviewed ?? false) == true).ToList();
		}

		private string _SoldToText;
		public string SoldToText {
			get {
				if (_SoldToText != null) return _SoldToText;
				SAPSoldTo st = this.FindSoldTo;
				if (!st.IsNull()) _SoldToText = st.Number + " " + st.Name;
				_SoldToText = _SoldToText ?? "";
				return _SoldToText;
			}
		}
		private string _ShipToText;
		public string ShipToText {
			get {
				if (_ShipToText != null) return _ShipToText;
				_ShipToText = _ShipToText.TrimNull();
				List<SAPShipTo> SAPShipTos = this.FindShipTos;
				if (SAPShipTos.Count != 1) {
					_ShipToText = SAPShipTos.Count + " Ship Tos";
					return _ShipToText;
				}
				SAPShipTo SAPShipTo = SAPShipTos.FirstOrDefault();
				if (!SAPShipTo.IsNull()) {
					_ShipToText += SAPShipTo.TrimmedNumber + " " + SAPShipTo.Name + " (" + SAPShipTo.CityAndState + ") ";
				}
				return _ShipToText;
			}
		}
		private string _DeliverNumberText;
		public string DeliveryNumberText {
			get {
				if (_DeliverNumberText != null) return _DeliverNumberText;
				_DeliverNumberText = "";
				foreach (SAPDelivery sd in this.SAPDeliveries) {
					_DeliverNumberText += sd.Number + " ";
				}
				return _DeliverNumberText;
			}
		}
        private decimal? _CalculatedGrossWeightHarrow;
        public decimal? CalculatedGrossWeightHarrow
        {
            get
            {
                if (_CalculatedGrossWeightHarrow != null) return _CalculatedGrossWeightHarrow;
                _CalculatedGrossWeightHarrow = 0;
                foreach (var webReleasePlantSAPSalesOrderItem in this.WebReleasePlantSapsalesOrderItems.Where(x => !x.SapdeliveryItem.Any()).Where(x => x.SapsalesOrderItem.PlantID == 4))
                {
                    var SAPSalesOrderItem = webReleasePlantSAPSalesOrderItem.SapsalesOrderItem;
                    _CalculatedGrossWeightHarrow += SAPSalesOrderItem.GrossWeight;
                }
                return _CalculatedGrossWeightHarrow;
            }
        }
        private decimal? _CalculatedGrossWeightPlaymouth;
        public decimal? CalculatedGrossWeightPlaymouth
        {
            get
            {
                if (_CalculatedGrossWeightPlaymouth != null) return _CalculatedGrossWeightPlaymouth;
                _CalculatedGrossWeightPlaymouth = 0;
                foreach (var webReleasePlantSAPSalesOrderItem in this.WebReleasePlantSapsalesOrderItems.Where(x => !x.SapdeliveryItem.Any()).Where(x => x.SapsalesOrderItem.PlantID == 1))
                {
                    var SAPSalesOrderItem = webReleasePlantSAPSalesOrderItem.SapsalesOrderItem;
                    _CalculatedGrossWeightPlaymouth += SAPSalesOrderItem.GrossWeight;
                }
                return _CalculatedGrossWeightPlaymouth;
            }
        }
	}
}
