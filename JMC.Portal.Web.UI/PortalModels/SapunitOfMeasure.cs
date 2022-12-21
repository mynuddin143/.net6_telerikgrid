using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapunitOfMeasure
    {
        public SapunitOfMeasure()
        {
            SapmaterialUnitOfMeasureAlternateUnitOfMeasures = new HashSet<SapmaterialUnitOfMeasure>();
            SapmaterialUnitOfMeasureBaseUnitOfMeasures = new HashSet<SapmaterialUnitOfMeasure>();
        }

        public long SapunitOfMeasureId { get; set; }
        public string Sapcode { get; set; } = null!;

        public virtual ICollection<SapmaterialUnitOfMeasure> SapmaterialUnitOfMeasureAlternateUnitOfMeasures { get; set; }
        public virtual ICollection<SapmaterialUnitOfMeasure> SapmaterialUnitOfMeasureBaseUnitOfMeasures { get; set; }
    }
}
