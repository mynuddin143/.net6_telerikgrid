using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapcustomerText
    {
        public long SapcustomerTextId { get; set; }
        public long SapcustomerTextTypeId { get; set; }
        public long SapsalesOrganizationId { get; set; }
        public long SapshipToId { get; set; }
        public int LineNumber { get; set; }
        public string Text { get; set; } = null!;

        public virtual SapcharacteristicOption SapcustomerTextType { get; set; } = null!;
        public virtual SapsalesOrganization SapsalesOrganization { get; set; } = null!;
        public virtual SapshipTo SapshipTo { get; set; } = null!;
    }
}
