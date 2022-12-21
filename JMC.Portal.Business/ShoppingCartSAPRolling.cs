using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business {
	public partial class ShoppingCartSaprolling
    {

		public int ItemsPPB {
			get {
				return (((this.Bundling1 ?? 0) == 0) ? 1 : this.Bundling1.Value) * (((this.Bundling2 ?? 0) == 0) ? 1 : this.Bundling2.Value);
			}
		}
		public decimal PieceWeight {
			get {
				return ((this.LengthFeet ?? 0) + ((decimal)(this.LengthInches ?? 0) / 12)) * (this.Sapmaterial.WeightPerFoot ?? 0);
			}
		}

        private decimal _deliveredprice=0;
        public decimal DeliveredPrice
        {
            get { return _deliveredprice; }
            set {_deliveredprice = value;}
        }

        public void RefeshDeliveredPrice(decimal deliveredPrice)
        {
            this.DeliveredPrice = deliveredPrice;
        }


        private string _partNumber = string.Empty;
        public string Partnumber
        {
            get { return _partNumber; }
            set { _partNumber = value; }
        }

        public void RefeshPartNumber(string partNumber)
        {
            this.Partnumber = partNumber;
        }

	}
}

