using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.HSSPortalSales;
using JMC.Portal.Business.PortalModels;
using JMC.Portal.Business;

namespace JMC.Portal.Business.DataModel
{
    public class SAPSalesOrderItemViewModel
    {

        public long SAPSalesOrderItemID { get; set; }
        public long SAPMaterialID { get; set; }
        public string SAPDeliveryNumber { get; set; }
        public decimal WeightPerPiece { get; set; }
        public int Position { get; set; }
        public string LineNumber { get; set; }
        public string SAPSalesOrderPONumber { get; set; }
        public string CustomerMaterialNumber { get; set; }
        public string MaterialShortDescription { get; set; }
        public string SalesUnit { get; set; }
        public decimal OrderedPcs { get; set; }
        public decimal OrderedWeight { get; set; }
        public decimal? OpenPieces { get; set; }
        public decimal? OpenWeight { get; set; }
        public decimal? NotReadyPieces { get; set; }
        public decimal? NotReadyWeight { get; set; }
        public decimal? ReadyPieces { get; set; }
        public decimal? ReadyWeight { get; set; }
        public decimal? ReleasedPieces { get; set; }
        public decimal? ReleasedWeight { get; set; }
        public decimal OnBOLPieces { get; set; }
        public decimal OnBOLWeight { get; set; }
        public decimal ShipCompletePieces { get; set; }
        public decimal ShipCompleteWeight { get; set; }
        public long LocationID { get; set; }
        public string PlantDisplayName { get; set; }
        public long? SoldToID { get; set; }
        public string SoldToName { get; set; }
        public string SoldToTitle { get; set; }
        public long? ShipToID { get; set; }
        public string ShipToName { get; set; }
        public string ShipToTitle { get; set; }
        public DateTime? ScheduleLineDate { get; set; }
        public DateTime? RollDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? PODate { get; set; }
        public long SAPSalesOrderID { get; set; }
        public string SAPSalesOrderNumber { get; set; }
        public bool HasCreditBlock { get; set; }
        public string SAPSalesOrderCreditStatus { get; set; }
        public bool HasDeliveryBlock { get; set; }
        public string SAPSalesOrderDeliveryBlockText { get; set; }
        public decimal ConsumedInShippingCartpcs { get; set; }
        public decimal ReleaseableBundles { get; set; }
        public decimal ReleaseablePcs { get; set; }
        public int ItemsPPB { get; set; }
        public String YourReference { get; set; }
        public string RejectionCode { get; set; }
        public decimal? DeliveredPrice { get; set; }
        public bool? DealIndicator { get; set; }

        public IList<string> StorageQuantityStrings { get; set; }
        public IEnumerable<BOLViewModel> BOLViewModels { get; set; }

        public string StorageQuantityHtml
        {
            get
            {
                if (StorageQuantityStrings != null)
                {
                    return String.Join("<br/>", StorageQuantityStrings);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public string AddToCartHtml
        {
            get
            {
                StringBuilder returnValue = new StringBuilder(string.Empty);
                var addOptions = new List<int>();

                if (this.ReleaseablePcs > 0)
                {
                    if (this.ConsumedInShippingCartpcs > 0)
                    {
                        //returnValue.AppendLine("<img src=\"/images/darkTheme/icons/85.png\" alt=\"Already in shipping cart\" style=\"float: right;\" />");

                        returnValue.AppendLine("<select name=\"" + this.SAPSalesOrderItemID + "_qty_Grid\" id=\"" + this.SAPSalesOrderItemID + "_qty\" class=\"autoKendoDropDown soi" + ((this.ReadyPieces.ToInt() > 0) ? " readysoi" : "") + ((this.NotReadyPieces.ToInt() > 0) ? " notreadysoi" : "") + "\" groupid=\"groupID\" wpp=\"" + this.WeightPerPiece.ToString("0.####") + "\" style=\"width: 115px; float: left;\" notready=\"" + this.NotReadyPieces.ToInt() + "\" ready=\"" + this.ReadyPieces.ToInt() + "\">");
                    }
                    else
                    {
                        returnValue.AppendLine("<select name=\"" + this.SAPSalesOrderItemID + "_qty_Grid\" id=\"" + this.SAPSalesOrderItemID + "_qty\" class=\"nonKendo soi" + ((this.ReadyPieces.ToInt() > 0) ? " readysoi" : "") + ((this.NotReadyPieces.ToInt() > 0) ? " notreadysoi" : "") + "\" groupid=\"groupID\" wpp=\"" + this.WeightPerPiece.ToString("0.####") + "\" style=\"width: 115px; float: left;visibility: hidden;\" notready=\"" + this.NotReadyPieces.ToInt() + "\" ready=\"" + this.ReadyPieces.ToInt() + "\">");
                    }
                    if (this.ConsumedInShippingCartpcs > 0)
                    {
                        returnValue.AppendLine("<option value=\"" + this.ConsumedInShippingCartpcs.ToInt() + "\" class=\"alreadyInCart\">");
                        addOptions.Add(this.ConsumedInShippingCartpcs.ToInt());
                        returnValue.AppendLine(((this.ConsumedInShippingCartpcs.ToInt() / this.ItemsPPB) < 1)
                               ? this.ConsumedInShippingCartpcs.ToInt() + " pcs"
                               : (this.ConsumedInShippingCartpcs / this.ItemsPPB).ToInt() + " (" + (this.ConsumedInShippingCartpcs - (this.ConsumedInShippingCartpcs % this.ItemsPPB)).ToInt() + " pcs) "
                           + ((this.ConsumedInShippingCartpcs % this.ItemsPPB).ToInt() > 0
                                ? " + " + (this.ConsumedInShippingCartpcs % this.ItemsPPB).ToInt() + " pcs"
                                : string.Empty));
                        //returnValue.AppendLine(" " + ((this.ConsumedInShippingCartpcs.ToInt() * this.ItemsPPB).ToInt() * this.WeightPerPiece).ToString("#,##0.#") + "lbs</option>");
                        returnValue.AppendLine(" " + ((this.ConsumedInShippingCartpcs.ToInt()).ToInt() * this.WeightPerPiece).ToString("#,##0.#") + "lbs</option>");
                    }
                    returnValue.AppendLine("<option value=\"0\">-</option>");
                    var extraOptions = new List<Tuple<int, string>>();
                    for (int count = 1; count <= (int)this.ReleaseableBundles && count < 1000; count++)
                    {
                        bool hasNotReady = false;
                        bool hasReady = false;
                        bool hasReleasable = false;
                        var itemCount = (count * this.ItemsPPB).ToInt();
                        if (itemCount != this.ConsumedInShippingCartpcs.ToInt())
                        {
                            returnValue.AppendLine("<option value=\"" + itemCount + "\">" + count + " (" + itemCount + "pcs) " + (itemCount * this.WeightPerPiece).ToString("#,##0.#") + "lbs</option>");
                            addOptions.Add(itemCount);
                            if (itemCount == this.NotReadyPieces.ToInt())
                            {
                                hasNotReady = true;
                            }
                            if (itemCount == this.ReadyPieces.ToInt())
                            {
                                hasReady = true;
                            }
                            if ((int)this.ReleaseablePcs > 0 && (int)this.ReleaseablePcs != this.ConsumedInShippingCartpcs && itemCount == this.ReleaseablePcs.ToInt())
                            {
                                hasReleasable = true;
                            }
                        }
                        if (!hasNotReady && this.NotReadyPieces.ToInt() > 0 && (int)this.NotReadyPieces != this.ConsumedInShippingCartpcs)
                        {
                            extraOptions.Add(this.NotReadyTuple);
                        }
                        if (!hasReady && this.ReadyPieces.ToInt() > 0 && (int)this.ReadyPieces != this.ConsumedInShippingCartpcs)
                        {
                            extraOptions.Add(this.ReadyTuple);
                        }
                        if (!hasReleasable && (int)this.ReleaseablePcs > 0 && (int)this.ReleaseablePcs != this.ConsumedInShippingCartpcs)
                        {
                            extraOptions.Add(this.ReleasableTuple);
                        }
                    }
                    if (ConsumedInShippingCartpcs > 0 && this.NotReadyPieces.ToInt() > 0 && (int)this.NotReadyPieces != this.ConsumedInShippingCartpcs)
                    {
                        extraOptions.Add(this.NotReadyTuple);
                    }
                    if (ConsumedInShippingCartpcs > 0 && this.ReadyPieces.ToInt() > 0 && (int)this.ReadyPieces != this.ConsumedInShippingCartpcs)
                    {
                        extraOptions.Add(this.ReadyTuple);
                    }
                    if ((int)this.ReleaseablePcs > 0 && (int)this.ReleaseablePcs != this.ConsumedInShippingCartpcs)
                    {
                        extraOptions.Add(this.ReleasableTuple);
                    }
                    foreach (var extraOption in extraOptions.OrderBy(x => x.Item1))
                    {
                        if (!addOptions.Contains(extraOption.Item1))
                            returnValue.AppendLine(extraOption.ToOption());
                    }
                    returnValue.AppendLine("</select>");
                    returnValue.AppendLine("<input type=\"hidden\" name=\"soids\" value=\"" + this.SAPSalesOrderItemID + "\" />");
                }
                return returnValue.ToString();
            }
        }

        private Tuple<int, string> NotReadyTuple
        {
            get
            {
                return new Tuple<int, string>(this.NotReadyPieces.ToInt(), (((int)this.NotReadyPieces / this.ItemsPPB) < 1) ? (int)this.NotReadyPieces + " pcs" : ((int)this.NotReadyPieces / this.ItemsPPB) + " (" + ((int)this.NotReadyPieces - ((int)this.NotReadyPieces % this.ItemsPPB)) + " pcs) " + (((int)this.NotReadyPieces % this.ItemsPPB) > 0 ? " + " + ((int)this.NotReadyPieces % this.ItemsPPB) + " pcs" : string.Empty) + " " + (this.NotReadyPieces.ToInt() * this.WeightPerPiece).ToString("#,##0.#") + "lbs");
            }
        }

        private string NotReadyOption
        {
            get
            {
                return this.NotReadyTuple.ToOption();
            }
        }

        private Tuple<int, string> ReadyTuple
        {
            get
            {
                return new Tuple<int, string>(this.ReadyPieces.ToInt(), (((int)this.ReadyPieces / this.ItemsPPB) < 1) ? (int)this.ReadyPieces + " pcs" : ((int)this.ReadyPieces / this.ItemsPPB) + " (" + ((int)this.ReadyPieces - ((int)this.ReadyPieces % this.ItemsPPB)) + " pcs) " + (((int)this.ReadyPieces % this.ItemsPPB) > 0 ? " + " + ((int)this.ReadyPieces % this.ItemsPPB) + " pcs" : string.Empty) + " " + (this.ReadyPieces.ToInt() * this.WeightPerPiece).ToString("#,##0.#") + "lbs");
            }
        }

        private string ReadyOption
        {
            get
            {
                return this.ReadyTuple.ToOption();
            }
        }

        private Tuple<int, string> ReleasableTuple
        {
            get
            {
                return new Tuple<int, string>(this.ReleaseablePcs.ToInt(), (((int)this.ReleaseablePcs / this.ItemsPPB) < 1) ? (int)this.ReleaseablePcs + " pcs" + " " + (this.ReleaseablePcs.ToInt() * this.WeightPerPiece).ToString("#,##0.#") + "lbs" : ((int)this.ReleaseablePcs / this.ItemsPPB) + " (" + ((int)this.ReleaseablePcs - ((int)this.ReleaseablePcs % this.ItemsPPB)) + " pcs) " + (((int)this.ReleaseablePcs % this.ItemsPPB) > 0 ? " + " + ((int)this.ReleaseablePcs % this.ItemsPPB) + " pcs" : string.Empty) + " " + (this.ReleaseablePcs.ToInt() * this.WeightPerPiece).ToString("#,##0.#") + "lbs");
            }
        }

        private string ReleasableOption
        {
            get
            {
                return this.ReleasableTuple.ToOption();
            }
        }

        public string OrderedString
        {
            get
            {
                return SAPSalesOrderItem.GetOrderedString(this.OrderedPcs, this.WeightPerPiece);
            }
        }

        public string OrderedStringByWeight
        {
            get
            {
                return SAPSalesOrderItem.GetOrderedStringByWeight(this.OrderedPcs, this.OrderedWeight);
            }
        }

        public string OpenString
        {
            get
            {
                return SAPSalesOrderItem.GetOpenString(this.OpenPieces.ToDecimal(), this.OpenWeight.ToDecimal());
            }
        }

        public string ReadyString
        {
            get
            {
                return SAPSalesOrderItem.GetReadyString(this.ReadyPieces.ToInt(), this.ReadyWeight);
            }
        }

        public string NotReadyString
        {
            get
            {
                return SAPSalesOrderItem.GetNotReadyString(this.NotReadyPieces.ToInt(), this.NotReadyWeight);
            }
        }

        public string ReleasedString
        {
            get
            {
                return SAPSalesOrderItem.GetReleasedString(this.ReleasedPieces.ToInt(), this.ReleasedWeight);
            }
        }

        public string OnBolString
        {
            get
            {
                return SAPSalesOrderItem.GetOnBolString(this.OnBOLPieces, this.OnBOLWeight);
            }
        }

        public string ShipCompleteString
        {
            get
            {
                return SAPSalesOrderItem.GetShipCompleteString(this.ShipCompletePieces, this.ShipCompleteWeight);
            }
        }

        public string ScheduleLineDateString
        {
            get
            {
                return SAPSalesOrderItem.GetScheduleLineDateString(this.ScheduleLineDate);
            }
        }

        public string RollDateString
        {
            get
            {
                return SAPSalesOrderItem.GetRollDateString(this.RollDate);
            }
        }

        public string PurchaseDateString
        {
            get
            {
                return SAPSalesOrderItem.GetPurchaseDateString(this.PurchaseDate);
            }
        }
        public string PODateString
        {
            get
            {
                return SAPSalesOrderItem.GetPODateString(this.PODate);
            }
        }

        public string BolNumbersHtml
        {
            get
            {
                StringBuilder returnValue = new StringBuilder(string.Empty);
                int count = 1;
                //foreach (BOLViewModel bol in this.BOLViewModels) {
                //  returnValue.AppendLine("<a href=\"/Delivery/Details/" + bol.SAPDeliveryID + "\">" + bol.Number + "</a>");
                //  if (count < this.BOLViewModels.Count()) {
                //    returnValue.Append("<br/>");
                //  }
                //  count++;
                //}
                if (this.SAPDeliveryNumber != null)
                {
                    var bolNumbers = this.SAPDeliveryNumber.Split(',');
                    foreach (var bolNumber in bolNumbers)
                    {
                        if (bolNumber.Split(':').Count() > 0)
                        {
                            returnValue.AppendLine("<a href= \"#\"  onclick=\"javascript:viewDataElement('Delivery','Delivery Details','" + bolNumber.Split(':')[1] + "','DetailsKendo');\"" + ">" + bolNumber.Split(':')[0] + "</a> ");
                            if (count < bolNumbers.Count())
                            {
                                returnValue.Append("<br/>");
                            }
                            count++;
                        }
                    }
                }
                return returnValue.ToString();
            }
        }
        public string BolNumbers
        {
            get
            {
                StringBuilder returnValue = new StringBuilder(string.Empty);
                int count = 1;
                //foreach (BOLViewModel bol in this.BOLViewModels) {
                //  returnValue.AppendLine("<a href=\"/Delivery/Details/" + bol.SAPDeliveryID + "\">" + bol.Number + "</a>");
                //  if (count < this.BOLViewModels.Count()) {
                //    returnValue.Append("<br/>");
                //  }
                //  count++;
                //}
                if (this.SAPDeliveryNumber != null)
                {
                    var bolNumbers = this.SAPDeliveryNumber.Split(',');
                    foreach (var bolNumber in bolNumbers)
                    {
                        if (bolNumber.Split(':').Count() > 0)
                        {
                            //returnValue.AppendLine("<a onclick='showDelivery href=\"/Delivery/Details/" + bolNumber.Split(':')[1] + "\">" + bolNumber.Split(':')[0] + "</a>");                            
                            returnValue.AppendLine(bolNumber.Split(':')[0]);
                            if (count < bolNumbers.Count())
                            {
                                returnValue.Append(",");
                            }
                            count++;
                        }
                    }
                }
                return returnValue.ToString();
            }
        }

    }

    public static partial class ViewModelExtensions
    {
        public static IQueryable<SAPSalesOrderItemViewModel> ToModel(this IQueryable<GetBacklogDetailResult> SAPSalesOrderItemQuery)
        {
            return (from x in SAPSalesOrderItemQuery
                    select new SAPSalesOrderItemViewModel()
                    {
                        SAPSalesOrderItemID = x.SAPSalesOrderItemID,
                        SAPMaterialID = x.SAPMaterialID,
                        SAPDeliveryNumber = x.SAPDeliveryNumber,
                        WeightPerPiece = x.WeightPerPiece ?? 0,
                        Position = x.Position,
                        SAPSalesOrderPONumber = x.PONumber,
                        PurchaseDate = x.PODate,
                        CustomerMaterialNumber = x.CustomerMaterialNumber,
                        MaterialShortDescription = x.MaterialShortDescription,
                        OrderedPcs = x.OrderedPieces ?? 0,
                        OrderedWeight = (x.OrderedPieces ?? 0) * (x.WeightPerPiece ?? 0),
                        NotReadyPieces = (x.DisplayNotReadyWeight ?? 0) / (x.WeightPerPiece ?? 1),
                        NotReadyWeight = x.DisplayNotReadyWeight ?? 0,
                        ReadyPieces = (x.DisplayReadyWeight ?? 0) / (x.WeightPerPiece ?? 1),
                        ReadyWeight = x.DisplayReadyWeight,
                        //ReleaseablePcs = (x.DisplayNotReadyWeight ?? 0) / (x.WeightPerPiece ?? 1) + (x.DisplayReadyWeight ?? 0) / (x.WeightPerPiece ?? 1) - (x.DisplayReleasedWeight ?? 0) / (x.WeightPerPiece ?? 1),
                        ReleaseablePcs = (x.OrderedPieces - (x.DisplayReleasedWeight ?? 0) / (x.WeightPerPiece ?? 1) - (x.DeliveryWeight ?? 0) / (x.WeightPerPiece ?? 1)).ToDecimal(),
                        //INC361607 -- issue when releasedweight/qty and readyweight/qty are same 
                        //ReleaseableBundles = ((x.DisplayNotReadyWeight ?? 0) / (x.WeightPerPiece ?? 1) + (x.DisplayReadyWeight ?? 0) / (x.WeightPerPiece ?? 1) - (x.DisplayReleasedWeight ?? 0) / (x.WeightPerPiece ?? 1)) / (((x.PiecesPerBundle ?? 0) == 0) ? 1 : (x.PiecesPerBundle.Value)),
                        ReleaseableBundles = ((x.OrderedPieces - (x.DisplayReleasedWeight ?? 0) / (x.WeightPerPiece ?? 1) - (x.DeliveryWeight ?? 0) / (x.WeightPerPiece ?? 1)).ToDecimal()) / (((x.PiecesPerBundle ?? 0) == 0) ? 1 : (x.PiecesPerBundle.Value)),
                        ReleasedPieces = (x.DisplayReleasedWeight ?? 0) / (x.WeightPerPiece ?? 1),
                        ReleasedWeight = x.DisplayReleasedWeight,
                        OnBOLPieces = (x.DeliveryWeight ?? 0) / (x.WeightPerPiece ?? 1),
                        OnBOLWeight = (x.DeliveryWeight ?? 0),
                        LocationID = x.PlantID == null ? 0 : (long)x.PlantID,
                        //PlantDisplayName = x.PlantID == 1 ? "Harrow / Plymouth" : x.PlantName,
                        PlantDisplayName = x.PlantName,
                        SoldToID = x.SAPSoldToID,
                        SoldToName = x.SAPSoldToName,
                        SoldToTitle = x.SAPSoldToNumber.TrimStart('0') + " - " + x.SAPSoldToName + " (" + x.SAPSoldToCityAndState + ")",
                        ShipToID = x.SAPShipToID,
                        ShipToName = x.SAPShipToName,
                        ShipToTitle = x.SAPShipToNumber.TrimStart('0') + " - " + x.SAPShipToName + " (" + x.SAPShipToIncoTerms2 + ")",
                        ScheduleLineDate = x.DueDate,
                        RollDate = x.RollDate,
                        SAPSalesOrderID = x.SAPSalesOrderID,
                        SAPSalesOrderNumber = x.SalesOrderNumber,
                        HasCreditBlock = (x.CreditStatus ?? "").Trim().ToUpper() == "B",
                        SAPSalesOrderCreditStatus = x.CreditStatus ?? "",
                        HasDeliveryBlock = (x.DeliveryBlock ?? "").Trim() != "",
                        SAPSalesOrderDeliveryBlockText = x.DeliveryBlock ?? "",
                        ConsumedInShippingCartpcs = (x.ConsumedInShippingCart ?? 0),
                        ItemsPPB = ((x.PiecesPerBundle ?? 0) == 0) ? 1 : (x.PiecesPerBundle.Value),
                        YourReference = x.YourReference,
                        DeliveredPrice = (x.Price ?? 0),
                        DealIndicator = x.DealIndicator

                        //BOLViewModels = (from di in x.SAPDeliveryItem.Select(sd => sd.SAPDelivery).Distinct() select new BOLViewModel() {
                        //  SAPDeliveryID = di.SAPDeliveryID,
                        //  Number = di.Number
                        //})
                    });
        }

        public static IQueryable<SAPSalesOrderItemViewModel> ToModel(this ZstHssSalesOrderItem[] SAPSalesOrderItemQuery, ZstHssSalesOrder[] salesOrders)
        {
            return (from x in SAPSalesOrderItemQuery
                    select new SAPSalesOrderItemViewModel()
                    {
                        WeightPerPiece = x.WeightPerPiece,
                        SAPSalesOrderPONumber = x.PoNumber,
                        LineNumber = x.ItemNumber,
                        PurchaseDate = salesOrders.Where(so => so.SalesOrderNumber == x.SalesOrderNumber).FirstOrDefault().PurchaseDate,
                        CustomerMaterialNumber = x.CustomerMaterialNumber,
                        MaterialShortDescription = x.MaterialShortDescription,
                        OrderedPcs = x.OrderQuantity,
                        SalesUnit = x.SalesUnit,
                        PlantDisplayName = x.Plant,
                        ShipToTitle = x.ShipToNumber == null || x.ShipToNumber == "" ? salesOrders.Where(so => so.SalesOrderNumber == x.SalesOrderNumber).FirstOrDefault().ShipToNumber.TrimStart('0') : x.ShipToNumber,
                        OrderedWeight = x.GrossWeight,
                        OpenPieces = x.OpenQuantity,
                        OpenWeight = x.OpenQuantity * x.WeightPerPiece,
                        OnBOLPieces = x.OpenQuantity,
                        OnBOLWeight = x.OpenQuantity * x.WeightPerPiece,
                        ShipCompletePieces = x.OrderQuantity,
                        ShipCompleteWeight = x.GrossWeight,
                        ScheduleLineDate = x.ScheduleLineDate,
                        RollDate = x.RollDate,
                        SAPSalesOrderNumber = x.SalesOrderNumber,
                        RejectionCode = x.LineRejected,
                        YourReference = salesOrders.Where(so => so.SalesOrderNumber == x.SalesOrderNumber).FirstOrDefault().YourReference,
                        DeliveredPrice = x.Price
                    }).AsQueryable();
        }

        public static List<SAPSalesOrderItemViewModel> Load(this List<SAPSalesOrderItemViewModel> query, ref PortalContext db)
        {
            foreach (SAPSalesOrderItemViewModel model in query)
            {
                int position = model.LineNumber.ToInt();
                SapsalesOrderItem soi = db.SapsalesOrderItems.Where(x => x.SapsalesOrder.Number == model.SAPSalesOrderNumber && x.Position == position).FirstOrDefault();
                if (soi.IsNull() && model.SAPSalesOrderItemID > 0)
                {
                    soi = db.SapsalesOrderItems.Where(x => x.SapsalesOrderItemId == model.SAPSalesOrderItemID).FirstOrDefault();
                }
                if (!soi.IsNull() && soi.SapsalesOrderItemId > 0)
                {

                    model.LineNumber = soi.Position.ToString();
                    model.CustomerMaterialNumber = soi.CustomerMaterialNumber;

                    model.ShipToID = soi.SapshipToId;
                    model.ShipToName = soi.SapshipTo.Name;
                    model.ShipToTitle = soi.SapshipTo.TrimmedNumber + " - " + soi.SapshipTo.Name + " (" + soi.SapshipTo.IncoTerms2 + ")";

                    model.MaterialShortDescription = soi.MaterialShortDescription.ToString();

                    //model.PlantDisplayName = soi.PlantID == 1 ? "Harrow / Plymouth" : soi.Plant.Name;
                    model.PlantDisplayName = soi.Plant.Name;

                    model.ScheduleLineDate = soi.ScheduleLineDate;
                    model.RollDate = soi.RollDate;
                    model.SalesUnit = soi.SalesUnit;

                    model.OrderedPcs = soi.OrderedPcs;
                    model.OrderedWeight = soi.OrderedLbs;

                    model.OpenPieces = soi.OpenPieces;
                    model.OpenWeight = soi.OpenQuantity;

                    model.OnBOLPieces = soi.OnBOLPieces;
                    model.OnBOLWeight = soi.OnBOLWeight;

                    model.ShipCompletePieces = soi.ShipCompletePcs;
                    model.ShipCompleteWeight = soi.ShipCompleteLbs;

                    model.RejectionCode = soi.RejectionCode;
                    model.YourReference = db.SapsalesOrders.Where(so => so.SapsalesOrderID == soi.SapsalesOrderID).FirstOrDefault().YourReference;
                    model.DeliveredPrice = soi.Price;
                    model.DealIndicator = soi.DealIndicator;
                }
            }
            return query;
        }
    }
}
