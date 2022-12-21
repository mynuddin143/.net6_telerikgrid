using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business;
using JMC.Portal.Business.PortalModels;


namespace JMC.Portal.Business.DataModel
{
    public partial class PricingModel
    {
        public string Shipto { get; set; }
        public decimal CalculatedPrice { get; set; }

        public long? ShiptoID { get; set; }
        public long? SAPMaterialID { get; set; }

        public decimal ZR00Rate { get; set; }
        public string ZR00Currency { get; set; }
        public long ZR00Per { get; set; }
        public string ZR00Unit { get; set; }

        public decimal ZR01Rate { get; set; }
        public string ZR01Currency { get; set; }
        public long ZR01Per { get; set; }
        public string ZR01Unit { get; set; }

        public decimal ZR04Rate { get; set; }
        public string ZR04Currency { get; set; }
        public long ZR04Per { get; set; }
        public string ZR04Unit { get; set; }

        public decimal ZEP1Rate { get; set; }
        public string ZEP1Currency { get; set; }
        public long ZEP1Per { get; set; }
        public string ZEP1Unit { get; set; }

        public decimal ZG01Rate { get; set; }
        public string ZG01Currency { get; set; }
        public long ZG01Per { get; set; }
        public string ZG01Unit { get; set; }


        public decimal FCSRate { get; set; }
        public string FCSCurrency { get; set; }
        public long FCSPer { get; set; }
        public string FCSUnit { get; set; }

    }
    public static partial class ViewModelExtensions
    {
        //Used in RollingView and Leftside on Index.
        public static List<PricingModel> ToModel(this List<GetSAPMaterialPricingResult> queryItems)
        {
            return (from query in queryItems
                    select new PricingModel()
                    {
                        SAPMaterialID = query.SAPMaterialID,
                        ShiptoID = query.ShipTo,
                        ZR00Rate = query.ZR00Rate.GetValueOrDefault(),
                        ZR00Currency = query.ZR00Currency,
                        ZR00Per = query.ZR00Per.GetValueOrDefault(),
                        ZR00Unit = query.ZR00Unit,
                        ZR01Rate = query.ZR01Rate.GetValueOrDefault(),
                        ZR01Currency = query.ZR01Currency,
                        ZR01Per = query.ZR01Per.GetValueOrDefault(),
                        ZR01Unit = query.ZR01Unit,
                        ZR04Rate = query.ZR04Rate.GetValueOrDefault(),
                        ZR04Currency = query.ZR04Currency,
                        ZR04Per = query.ZR04Per.GetValueOrDefault(),
                        ZR04Unit = query.ZR04Unit,
                        ZEP1Rate = query.ZEP1Rate.GetValueOrDefault(),
                        ZEP1Currency = query.ZEP1Currency,
                        ZEP1Per = query.ZEP1Per.GetValueOrDefault(),
                        ZEP1Unit = query.ZEP1Unit,
                        ZG01Rate = query.ZG01Rate.GetValueOrDefault(),
                        ZG01Currency = query.ZG01Currency,
                        ZG01Per = query.ZG01Per.GetValueOrDefault(),
                        ZG01Unit = query.ZG01Unit,
                        FCSRate = query.FCSRate.GetValueOrDefault(),
                        FCSCurrency = query.FCSCurrency,
                        FCSPer = query.FCSPer.GetValueOrDefault(),
                        FCSUnit = query.FCSUnit
                    }).ToList();
        }

        public static List<PricingModel> ToPricingTable(this List<PricingModel> shipToPricingItems)
        {
            var result = from element in shipToPricingItems
                         group element by element.ShiptoID
                             into groups
                         //let topPrice = groups.Max(x=>x.ZR00Rate)
                         select groups.OrderBy(p => p.ShiptoID).First();//First(x=>x.ZR00Rate == topPrice);
            return result.ToList();
        }

        public static PricingModel UpdateShipTo(this PricingModel item, IOrderedQueryable<SapshipTo> shiptos)
        {
            if (item.ShiptoID > 0)
            {
                var shipTo = shiptos.FirstOrDefault(x => x.SapshipToID == item.ShiptoID);
                Int64 shipNumber = 0;
                var bflag = Int64.TryParse(shipTo.Number, out shipNumber);
                var displayString = string.Format("{0} {1} ({2})", shipNumber, shipTo.Name, shipTo.CityAndState);
                item.Shipto = displayString;
            }
            return item;
        }

        public static PricingModel UpdateCalculatedPrice(this PricingModel item, bool includeZG01, decimal gRate = 0)
        {
            var gradeRate = ((includeZG01) ? item.ZG01Rate : 0);
            if (gRate != 0 && includeZG01)
                gradeRate = gRate;
            var totalRate = item.ZR00Rate + item.ZR01Rate + item.ZR04Rate + gradeRate + item.ZEP1Rate + item.FCSRate;
            if (totalRate > 0 && item.ZR00Rate == 0)
                item.CalculatedPrice = -1;
            else
                item.CalculatedPrice = totalRate;
            return item;
        }
    }
}
