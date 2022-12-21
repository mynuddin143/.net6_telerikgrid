using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class FiscalYear
    {
        public FiscalYear()
        {
            FiscalPeriods = new HashSet<FiscalPeriod>();
        }

        public long FiscalYearId { get; set; }
        public int TheFiscalYear { get; set; }

        public virtual ICollection<FiscalPeriod> FiscalPeriods { get; set; }
    }
}
