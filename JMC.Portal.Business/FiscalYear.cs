using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
//using AtlasMasterData;
//using WheatlandPortal;
using JMC.Portal.Business.AtlasMasterData;
using JMC.Portal.Business.WheatlandPortal;
using System.Net;

namespace JMC.Portal.Business {
	public partial class FiscalYear {
		public DateTime StartDate {
			get {
				return (from fp in this.FiscalPeriods orderby fp.StartDate select fp.StartDate).Min();
			}
		}

		public DateTime EndDate {
			get {
				return (from fp in this.FiscalPeriods orderby fp.EndDate select fp.EndDate).Max();
			}
		}
	}
}
