using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Zg01
    {
        public long GradeXtraId { get; set; }
        public long? SapsoldtoId { get; set; }
        public long SaptubeStandardId { get; set; }
        public decimal Rate { get; set; }
        public string Currency { get; set; } = null!;
        public long Per { get; set; }
        public string Unit { get; set; } = null!;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public virtual SapcharacteristicOption SaptubeStandard { get; set; } = null!;
    }
}
