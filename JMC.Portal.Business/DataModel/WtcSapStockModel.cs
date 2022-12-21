//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace JMC.Portal.Business.DataModel
//{
//    class WtcSAPStockModel
//    {
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using System.Web.Configuration;
using System.Configuration;
using JMC.Portal.Business.WheatlandPortal;


namespace JMC.Portal.Business.DataModel {
	public class WtcSAPStockModel {


		public string Grade { get; set; }
		public decimal? Length { get; set; }
		public string MaterialGroup { get; set; }
		public string MaterialNumber { get; set; }
		public int Pieces { get; set; }
		public string MaterialDescription { get; set; }


		public decimal? Gauge { get; set; }


		public string totalPieces { get; set; }

		public decimal? Weight { get; set; }
		public decimal? TotalWeight { get; set; }
		public string sTotalWeight { get; set; }
		public string Uom { get; set; }

		public decimal TotalLength { get; set; }


		public string Name { get; set; }
		public decimal WPF { get; set; }
		public decimal TotWeight { get; set; }
		public string Plant { get; set; }
		public string PlantDisplayName { get; set; }
		public decimal? Size { get; set; }
		public decimal? Size2 { get; set; }
		public decimal? Diameter { get; set; }
		public decimal? GaugeRestrictable { get; set; }



		public decimal ConsumedInShoppingCartpcs { get; set; }

		public decimal WeightPerPiece { get; set; }
		public decimal PiecesPerBundle { get; set; }
		public int AvailablePieces { get; set; }
		public int Remainder { get; set; }
		public decimal TubeLength { get; set; }
		public List<long> excludedPlantIDs { get; set; }


		public long SAPStockID { get; set; }

        // Added on 13 Feb 2017
        // public decimal? GaugeSchedule { get; set; }
        //public decimal? Length { get; set; }
        public decimal? BundleWeight { get; set; }
        //public int? TotalPieces { get; set; }
        public string BundlesAvailableForSale { get; set; }
        public decimal? AddtoCardBox { get; set; }
        public decimal? WeightOrdered { get; set; }
        public decimal TotalBundleLength { get; set; }
        public string ItemNumbers { get; set; }
        public bool employee;
        public string GetAddToCartHtml(string selectPrefix, string stockIDPrefix = "")
        {

            StringBuilder returnValue = new StringBuilder(string.Empty);

            if (this.ConsumedInShoppingCartpcs > 0)
            {
                //returnValue.AppendLine("<img src=\"/images/darkTheme/icons/84.png\" alt=\"Already in shipping cart\" style=\"float: right;\" />");
            }

            List<Tuple<int, string>> options = new List<Tuple<int, string>>();

            options.Add(new Tuple<int, string>(0, "-"));
            if (this.Remainder != 0)
            {
                options.Add(new Tuple<int, string>(this.Remainder.ToInt(), this.Remainder.ToInt() + " pcs - " + (this.WeightPerPiece * this.Remainder).ToInt() + " lbs"));
            }
            int avb = 0;
            if (!employee)
            {
                int CustomerMaxTon = ConfigurationManager.AppSettings["CustomerMaxTon"].ToString().ToInt();

                string bunavb = BundlesAvailableForSale.Contains("+") ? BundlesAvailableForSale.Substring(0, BundlesAvailableForSale.Length - 1) : BundlesAvailableForSale;
                avb = (BundleWeight.Value == 0) ? 0 : (((bunavb.ToInt() * BundleWeight.Value) > (Convert.ToInt32(CustomerMaxTon * 2000)) ? (Convert.ToInt32(CustomerMaxTon * 2000 / BundleWeight.Value)) : (bunavb.ToInt())));// this.BundlesAvailableForSale.ToInt();//Convert.ToInt32(this.totalPieces.ToInt() / ((this.PiecesPerBundle==0)?1:this.PiecesPerBundle));
            }
            else
            {
                avb = BundlesAvailableForSale.ToInt();
            }
            for (int x = 1; x <= avb && x < 1000; x++)
            {
                options.Add(new Tuple<int, string>(x, x + " (" + (this.PiecesPerBundle * x).ToInt() + " pcs - " + (this.TotalBundleLength * x).ToInt() + " fts)"));
            }

            string selectPrefixName = selectPrefix == "sstock_" ? "stock_" : selectPrefix;

            if (this.ConsumedInShoppingCartpcs > 0)
            {
                returnValue.AppendLine("<select incart=\"" + this.ConsumedInShoppingCartpcs + "\" id=\"" + selectPrefix +
                                       this.ItemNumbers + "_qty\" name=\"" + selectPrefixName + this.ItemNumbers +
                                       "_qty\" style=\"float: left; width: 170px;\" class=\"sti StockSelectQty autoKendoDropDown\" wpp=\"" +
                                       this.BundleWeight + "\" ppb=\"" + this.PiecesPerBundle + "\" avp=\"" +
                                       this.totalPieces + "\">");
            }
            else
            {
                returnValue.AppendLine("<select incart=\"" + this.ConsumedInShoppingCartpcs + "\" id=\"" + selectPrefix +
                                       this.ItemNumbers + "_qty\" name=\"" + selectPrefixName + this.ItemNumbers +
                                       "_qty\" style=\"float: left; width: 170px; visibility: hidden;\" class=\"sti StockSelectQty nonKendo\" wpp=\"" +
                                       this.BundleWeight + "\" ppb=\"" + this.PiecesPerBundle + "\" avp=\"" +
                                       this.totalPieces + "\">");
            }

            Tuple<int, string> consumedOption = options.Where(t => t.Item1 == this.ConsumedInShoppingCartpcs).FirstOrDefault();
            if (!consumedOption.IsNull())
            {
                returnValue.AppendLine(consumedOption.ToOption(true));
            }
            foreach (Tuple<int, string> option in options.Where(t => t.Item1 != this.ConsumedInShoppingCartpcs))
            {
                returnValue.AppendLine(option.ToOption());
            }

            returnValue.AppendLine("</select>");

            returnValue.AppendLine("<input id = \"" + this.ItemNumbers + "\" type=\"hidden\" name=\"" + stockIDPrefix + "ItemNumbers\" value=\"" + this.ItemNumbers +
                                   "\" />");           

            return returnValue.ToString();
        }

		public enum StockTypes {
			Stock,
			Excess,
			Secondary
		};

		public StockTypes StockType { get; set; }


        //public string AddToCartHtml {
        //    get;
        //    set;
        //}

        public string AddToCartHtml
        {
            get
            {
               // switch (StockType)
                //{
                 //   case StockTypes.Stock:
                        return StockAddToCartHtml;
                   // case StockTypes.Excess:
                       // return ExcessAddToCartHtml;
                   // case StockTypes.Secondary:
                        //return SecondaryAddToCartHtml;
                   //     break;
                   // default:
                       // return StockAddToCartHtml;
               // }
            }
            set
            {
            }

        }

        public string StockAddToCartHtml
        {
            get
            {
                return GetAddToCartHtml("stock_");
            }
            set{}
        }

        //public string ExcessAddToCartHtml
        //{
        //    get
        //    {
        //        return GetAddToCartHtml("estock_", "e");
        //    }
        //    set{}
        //}

        //public string SecondaryAddToCartHtml
        //{
        //    get
        //    {
        //        return GetAddToCartHtml("sstock_");
        //    }
        //    set { }
        //}
	}

	public static partial class ViewModelExtensions {
		public static List<WtcSAPStockModel> ToModel(IQueryable<ZstHssPortalStocks> query) {
			return (from x in query
							select new WtcSAPStockModel() {

								Name = x.MaterialDescription,
								TotWeight = x.TotalWeight,
								Plant = x.Plant,
								PlantDisplayName = x.Plant,
								Pieces = x.Pieces.ToInt(),
								Diameter = x.Diameter.ToDecimal(),
								Grade = x.Grade,
								//ConsumedInShoppingCartpcs = x.ConsumedInShoppingCartPieces ?? 0,
								//WeightPerPiece = x.WeightPerPiece ?? 0,
								//PiecesPerBundle = x.PiecesPerBundle ?? 1,
								TotalLength = x.TotalLength.ToDecimal(),
								WPF = x.WeightPerFoot.ToDecimal(),

							}).ToList();
		}
	}
}
