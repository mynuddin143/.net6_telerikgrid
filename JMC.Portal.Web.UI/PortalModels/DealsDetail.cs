using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class DealsDetail
    {
        public DealsDetail()
        {
            DealsBySoldToShipTos = new HashSet<DealsBySoldToShipTo>();
            DealsMaterialPricingGroups = new HashSet<DealsMaterialPricingGroup>();
            DealsPlants = new HashSet<DealsPlant>();
            DealsPricingGroups = new HashSet<DealsPricingGroup>();
        }

        public string? Name { get; set; }
        public DateTime? OrderFromDate { get; set; }
        public DateTime? OrderToDate { get; set; }
        public DateTime? ShipFromDate { get; set; }
        public DateTime? ShipToDate { get; set; }
        public long? ThresholdLimit { get; set; }
        public bool? ApprovedDenied { get; set; }
        public bool? InActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public string? DealType { get; set; }
        public long DealId { get; set; }
        public long? CreatedUserId { get; set; }
        public long? ApprovedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ApprovedDeniedDate { get; set; }
        public string? Notes { get; set; }
        public decimal? TonsUsed { get; set; }
        public long? FirmFlag { get; set; }
        public long? MaxTons { get; set; }

        public virtual ICollection<DealsBySoldToShipTo> DealsBySoldToShipTos { get; set; }
        public virtual ICollection<DealsMaterialPricingGroup> DealsMaterialPricingGroups { get; set; }
        public virtual ICollection<DealsPlant> DealsPlants { get; set; }
        public virtual ICollection<DealsPricingGroup> DealsPricingGroups { get; set; }
    }
}
