using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class FiscalPeriod
    {
        public long FiscalPeriodId { get; set; }
        public long FiscalYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PeriodNumber { get; set; }

        public virtual FiscalYear FiscalYear { get; set; } = null!;
    }
}
