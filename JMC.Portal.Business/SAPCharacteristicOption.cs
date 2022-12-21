using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business {
	public partial class SAPCharacteristicOption {
		public SAPCondition SAPCondition { get; set; }
		public SAPCondition EpoxySAPCondition { get; set; }
		public decimal? RequestedRate { get; set; }

		public string RequestedRateHTML {
			get { return this.RequestedRate == null ? string.Empty : string.Format("{0:c}", this.RequestedRate) + "<span style=\"color: Red\">*</span>"; }
		}
	}
}
