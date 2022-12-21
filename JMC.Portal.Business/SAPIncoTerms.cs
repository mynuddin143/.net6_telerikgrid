using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JMC.Portal.Business {
	public class SAPIncoTerms {
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		private string _name;

		public string AlphaNumericName {
			get { return Regex.Replace(this.Name, "[^A-Za-z0-9]", string.Empty); }
		}

		public SAPCondition TruckSAPPricingCondition {
			get { return _truckSAPPricingCondition; }
			set { _truckSAPPricingCondition = value; }
		}
		private SAPCondition _truckSAPPricingCondition = new SAPCondition();

		public SAPCondition RailSAPPricingCondition {
			get { return _railSAPPricingCondition; }
			set { _railSAPPricingCondition = value; }
		}
		private SAPCondition _railSAPPricingCondition = new SAPCondition();

		public decimal? TruckRequestedRate {
			get { return _truckRequestedRate; }
			set { _truckRequestedRate = value; }
		}
		private decimal? _truckRequestedRate = null;

		public string TruckRequestedRateHTML {
			get { return this.TruckRequestedRate == null ? string.Empty : string.Format("{0:c}", this.TruckRequestedRate) + "<span style=\"color: Red;\">*</span>"; }
		}

		public decimal? RailRequestedRate {
			get { return _railRequestedRate; }
			set { _railRequestedRate = value; }
		}
		private decimal? _railRequestedRate = null;

		public string RailRequestedRateHTML {
			get { return this.RailRequestedRate == null ? string.Empty : string.Format("{0:c}", this.RailRequestedRate) + "<span style=\"color: Red;\">*</span>"; }
		}
	}
}
