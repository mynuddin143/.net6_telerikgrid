using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapcharacteristicType
    {
        public SapcharacteristicType()
        {
            SapcharacteristicOptions = new HashSet<SapcharacteristicOption>();
        }

        public long SapcharacteristicTypeId { get; set; }
        public long DivisionId { get; set; }
        public string Name { get; set; } = null!;
        public string Sapcode { get; set; } = null!;
        public bool Active { get; set; }
        public int? SortOrder { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual ICollection<SapcharacteristicOption> SapcharacteristicOptions { get; set; }
    }
}
