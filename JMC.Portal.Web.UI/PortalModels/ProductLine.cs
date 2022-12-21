using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ProductLine
    {
        public ProductLine()
        {
            SapcharacteristicOptions = new HashSet<SapcharacteristicOption>();
            Sapshipments = new HashSet<Sapshipment>();
        }

        public long ProductLineId { get; set; }
        public long DivisionId { get; set; }
        public string? Name { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual ICollection<SapcharacteristicOption> SapcharacteristicOptions { get; set; }
        public virtual ICollection<Sapshipment> Sapshipments { get; set; }
    }
}
