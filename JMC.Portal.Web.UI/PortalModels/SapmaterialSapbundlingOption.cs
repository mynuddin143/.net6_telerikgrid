using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapmaterialSapbundlingOption
    {
        public long SapmaterialId { get; set; }
        public long SapbundlingOptionId { get; set; }

        public virtual SapbundlingOption SapbundlingOption { get; set; } = null!;
        public virtual Sapmaterial Sapmaterial { get; set; } = null!;
    }
}
