using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapmaterialUnitOfMeasure
    {
        public long SapmaterialUnitOfMeasureId { get; set; }
        public long SapmaterialId { get; set; }
        public decimal? Numerator { get; set; }
        public decimal? Denominator { get; set; }
        public long BaseUnitOfMeasureId { get; set; }
        public long AlternateUnitOfMeasureId { get; set; }

        public virtual SapunitOfMeasure AlternateUnitOfMeasure { get; set; } = null!;
        public virtual SapunitOfMeasure BaseUnitOfMeasure { get; set; } = null!;
        public virtual Sapmaterial Sapmaterial { get; set; } = null!;
    }
}
