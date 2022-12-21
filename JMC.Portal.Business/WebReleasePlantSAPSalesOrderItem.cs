using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business {
	public partial class WebReleasePlantSAPSalesOrderItem {
		public void SetPieces() {
			this.Pieces = ((!this.SapsalesOrderItem.IsNull() && this.SapsalesOrderItem.WeightPerPiece > 0) ? this.Quantity / this.SapsalesOrderItem.WeightPerPiece : 0).ToInt();
		}
	}
}
