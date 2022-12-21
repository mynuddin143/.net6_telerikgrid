using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using System.Web.Configuration;
using JMC.Portal.Business;
using JMC.Portal.Business.PortalModels;

namespace JMC.Portal.Business.DataModel {
	public class SAPStockModel {
		public long SAPStockID { get; set; }
		public string Name { get; set; }
		public decimal Weight { get; set; }
		public long Plant { get; set; }
		public string PlantDisplayName { get; set; }
		public decimal? Size { get; set; }
		public decimal? Size2 { get; set; }
		public decimal? Diameter { get; set; }
		public decimal? GaugeRestrictable { get; set; }

		public string Grade { get; set; }
		public decimal ConsumedInShoppingCartpcs { get; set; }

		public decimal WeightPerPiece { get; set; }
		public int PiecesPerBundle { get; set; }
		public int AvailablePieces { get; set; }
		public int Remainder { get; set; }
		public decimal TubeLength { get; set; }
		public List<long> excludedPlantIDs { get; set; }
		public bool MaxStockLevel { get; set; }
        public decimal? DeliveredPrice { get; set; }

		public int AvailableBundles {
			get {
				return AvailablePieces / PiecesPerBundle;
			}
		}

		public decimal WeightPerFoot {
			get {
				return (this.TubeLength == 0) ? 1 : (this.WeightPerPiece / this.TubeLength);
			}
		}

		public string GetAddToCartHtml(string selectPrefix, string stockIDPrefix = "") {
		
			StringBuilder returnValue = new StringBuilder(string.Empty);

			if (this.ConsumedInShoppingCartpcs > 0) {
				//returnValue.AppendLine("<img src=\"/images/darkTheme/icons/84.png\" alt=\"Already in shipping cart\" style=\"float: right;\" />");
			}

			List<Tuple<int, string>> options = new List<Tuple<int, string>>();

			options.Add(new Tuple<int, string>(0, "-"));
			if (this.Remainder != 0) {
				options.Add(new Tuple<int, string>(this.Remainder.ToInt(), this.Remainder.ToInt() + " pcs - " + (this.WeightPerPiece * this.Remainder).ToInt() + " lbs"));
			}

			int avb = this.AvailablePieces / this.PiecesPerBundle;

			for (int x = 1; x <= avb && x < 1000; x++) {
				options.Add(new Tuple<int, string>((this.PiecesPerBundle * x).ToInt(), x + " (" + (this.PiecesPerBundle * x).ToInt() + " pcs - " + (this.WeightPerPiece * this.PiecesPerBundle * x).ToInt() + " lbs)"));
			}

			string selectPrefixName = selectPrefix == "sstock_" ? "stock_" : selectPrefix;

			if (excludedPlantIDs.IsNull() || !excludedPlantIDs.Contains(this.Plant)) // TODO Sara
			{
				if (this.ConsumedInShoppingCartpcs > 0)
				{
					returnValue.AppendLine("<select incart=\"" + this.ConsumedInShoppingCartpcs + "\" id=\"" + selectPrefix +
					                       this.SAPStockID + "_qty\" name=\"" + selectPrefixName + this.SAPStockID +
					                       "_qty\" style=\"float: left; width: 170px;\" class=\"sti StockSelectQty autoKendoDropDown\" wpp=\"" +
					                       this.WeightPerPiece + "\" ppb=\"" + this.PiecesPerBundle + "\" avp=\"" +
																 this.AvailablePieces + "\" stocktype=\"" + selectPrefixName + "\" stockid=\"" + this.SAPStockID + "\">");
				}
                else
                {
                    returnValue.AppendLine("<select incart=\"" + this.ConsumedInShoppingCartpcs + "\" id=\"" + selectPrefix +
                                           this.SAPStockID + "_qty\" name=\"" + selectPrefixName + this.SAPStockID +
                                           "_qty\" style=\"float: left; width: 170px; visibility: hidden;\" class=\"sti StockSelectQty nonKendo\" wpp=\"" +
                                           this.WeightPerPiece + "\" ppb=\"" + this.PiecesPerBundle + "\" avp=\"" +
																					 this.AvailablePieces + "\" stocktype=\"" + selectPrefixName + "\" stockid=\"" + this.SAPStockID + "\">");
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
			} else {
				returnValue.AppendLine("<span style=\"color:red;\">Contact Your Sales Rep</span>");
			}
		
				returnValue.AppendLine("<input type=\"hidden\" name=\"" + stockIDPrefix + "StockIDs\" value=\"" + this.SAPStockID +"\" />");
                //returnValue.AppendLine("<input type=\"hidden\" name=\"DeliveredPrice\" value=\"" + this.DeliveredPrice + "\" stockid= \"" + this.SAPStockID + "\" />");

			return returnValue.ToString();
		}

		public enum StockTypes{
			Stock,
			Excess,
			Secondary,
			Planned
		};

		public StockTypes StockType { get; set; }

		public string AddToCartHtml{
			get{
				switch (StockType){
					case StockTypes.Stock:
						return StockAddToCartHtml;
					case StockTypes.Excess:
						return ExcessAddToCartHtml;
					case StockTypes.Secondary:
						return SecondaryAddToCartHtml;
					case StockTypes.Planned:
						return PlannedStockAddToCartHtml;
					default:
						return StockAddToCartHtml;
				}
			}
		}

		public string StockAddToCartHtml {
			get {
				return GetAddToCartHtml("stock_");
			}
		}

		public string ExcessAddToCartHtml {
			get {
				return GetAddToCartHtml("estock_", "e");
			}
		}

		public string SecondaryAddToCartHtml {
			get {
				return GetAddToCartHtml("sstock_");
			}
		}
		public string PlannedStockAddToCartHtml {
			get {
				return GetAddToCartHtml("Pstock_", "p");
			}
		}
		
	}

	public static partial class ViewModelExtensions {
		public static List<SAPStockModel> ToModel(this List<GetSAPStockResult> query, SAPStockModel.StockTypes stockType) {
			return (from x in query
							select new SAPStockModel() {
								SAPStockID = x.SAPStockID,
								Name = x.Name,
								Weight = x.Weight,
								Plant = x.PlantID,
								PlantDisplayName = x.PlantDisplayName,
								Size = x.Size,
								Size2 = x.Size2,
								Diameter = x.Diameter,
								GaugeRestrictable = x.GaugeRestrictable,
								Grade = x.Grade,
								ConsumedInShoppingCartpcs = x.ConsumedInShoppingCartPieces ?? 0,
								WeightPerPiece = x.WeightPerPiece ?? 0,
								PiecesPerBundle = x.PiecesPerBundle ?? 1,
								AvailablePieces = x.AvailablePieces,
								TubeLength = x.TubeLength,
								Remainder = x.AvailablePieces % (x.PiecesPerBundle ?? 1),
								StockType = stockType,
								MaxStockLevel = x.MaxStockLevel.ToBool(),
                                DeliveredPrice =0, 
							}).ToList();
		}
	}
}
