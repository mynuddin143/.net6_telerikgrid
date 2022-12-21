using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class AlternateSoldTo
    {
        public long SapshipToId { get; set; }
        public long AlternateSapsoldToId { get; set; }

        public virtual SapsoldTo AlternateSapsoldTo { get; set; } = null!;
        public virtual SapsoldTo SapshipTo { get; set; } = null!;
    }
}
