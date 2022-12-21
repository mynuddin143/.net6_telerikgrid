using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business {
	public partial class Box {
		public SAPSoldTo SAPSoldTo {
			get { return this.ScrapSapsoldToID > 0 ? this.ScrapSapsoldTo.SapsoldTo : this.RandomLengthSapsoldToID > 0 ? this.RandomLengthSapsoldTo.SapsoldTo : null; }
		}

		public string DescriptionHTML {
			get {
				return this.SAPSoldTo.IsNull() ? (this.sapdeliveryID > 0 ? "<span style=\"font-weight: normal;\">" + this.sapdelivery.TrimmedNumber + "</span>&nbsp;<a href=\"/Vendor/Details/" + this.sapdelivery.SapdeliveryID + "\">" + this.sapdelivery.Name + "</a>" : string.Empty) : "<span style=\"font-weight: normal;\">" + this.SAPSoldTo.TrimmedNumber + "</span>&nbsp;<a href=\"/SoldTo/Details/" + this.SAPSoldTo.SapshipToID + "\">" + this.SAPSoldTo.Name + "</a>";
			}
		}
	}
}
