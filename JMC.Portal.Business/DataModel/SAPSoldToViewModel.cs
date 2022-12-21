using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business.DataModel {
	public class SAPSoldToViewModel {
		public long SAPShipToID { get; set; }
		public string Number { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string City { get; set; }
		public string StateAbbreviation { get; set; }
		public string CountryAbbreviation { get; set; }
		public string Address { get; set; }
		public string IncoTerms2 { get; set; }
	}
}
