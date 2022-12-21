using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class DefaultTubeStandard
    {
        public long DefaultId { get; set; }
        public decimal? Size { get; set; }
        public decimal? Size2 { get; set; }
        public decimal? Diameter { get; set; }
        public long PlantId { get; set; }
        public string GradeValue { get; set; } = null!;
    }
}
