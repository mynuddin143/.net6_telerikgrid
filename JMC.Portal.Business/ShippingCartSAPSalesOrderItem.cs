using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace JMC.Portal.Business {
	public partial class ShippingCartSAPSalesOrderItem {
		public int PartBundleRemainder {
			get {
				return (int)this.CartQuantity % this.SapsalesOrderItem.ItemsPPB;
			}
		}

		public string QuantityDropDownHTML {
			get {
				return (int)((int)this.CartQuantity / this.SapsalesOrderItem.ItemsPPB) < 1 ? this.PartBundleRemainder + " pcs" : (int)((int)this.CartQuantity / this.SapsalesOrderItem.ItemsPPB) + " (" + ((int)this.CartQuantity - this.PartBundleRemainder) + " pcs)" + ((this.PartBundleRemainder > 0) ? " + " + this.PartBundleRemainder : "") + " " + ((this.CartQuantity).ToInt() * this.SapsalesOrderItem.WeightPerPiece).ToString("#,##0.#") + "lbs";
			}
		}
	}
}
