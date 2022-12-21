using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class MillException
    {
        public long MillExceptionId { get; set; }
        public long MillId { get; set; }
        public decimal? MinLength { get; set; }
        public decimal? MaxLength { get; set; }
        public decimal? MinSize { get; set; }
        public decimal? MaxSize { get; set; }
        public decimal? MinGauge { get; set; }
        public decimal? MaxGauge { get; set; }
    }
}
