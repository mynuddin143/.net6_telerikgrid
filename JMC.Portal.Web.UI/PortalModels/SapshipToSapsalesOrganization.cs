using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapshipToSapsalesOrganization
    {
        public long SapshipToSapsalesOrganizationId { get; set; }
        public long SapshipToId { get; set; }
        public long SapsalesOrganizationId { get; set; }
        public long? SapcustomerGroupId { get; set; }
        public long? SapsalesGroupId { get; set; }
        public string? IncoTerms1 { get; set; }
        public string? IncoTerms2 { get; set; }
        public string? Currency { get; set; }
        public string? PricingProcedure { get; set; }
        public long? SapcustomerServiceRepId { get; set; }
        public long? SapregionId { get; set; }
        public long? SaptierId { get; set; }
        public long? SappaymentTermsId { get; set; }
        public bool? Active { get; set; }
        public string? OrderBlock { get; set; }

        public virtual SapcustomerGroup? SapcustomerGroup { get; set; }
        public virtual SapcustomerServiceRep? SapcustomerServiceRep { get; set; }
        public virtual SapcharacteristicOption? SappaymentTerms { get; set; }
        public virtual Sapregion? Sapregion { get; set; }
        public virtual SapsalesGroup? SapsalesGroup { get; set; }
        public virtual SapsalesOrganization SapsalesOrganization { get; set; } = null!;
        public virtual SapshipTo SapshipTo { get; set; } = null!;
        public virtual Saptier? Saptier { get; set; }
    }
}
