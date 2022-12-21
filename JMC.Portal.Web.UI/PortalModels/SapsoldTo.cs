using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapsoldTo
    {
        public SapsoldTo()
        {
            Cmirs = new HashSet<Cmir>();
            DealsBySoldToShipTos = new HashSet<DealsBySoldToShipTo>();
            PriceChangeRequests = new HashSet<PriceChangeRequest>();
            PriceSheetNotes = new HashSet<PriceSheetNote>();
            Quotes = new HashSet<Quote>();
            RandomLengthSapsoldTos = new HashSet<RandomLengthSapsoldTo>();
            Sapdeliveries = new HashSet<Sapdelivery>();
            SapsalesOrders = new HashSet<SapsalesOrder>();
            Sapshipments = new HashSet<Sapshipment>();
            SapsoldToPlantExclusions = new HashSet<SapsoldToPlantExclusion>();
            ScrapSapsoldTos = new HashSet<ScrapSapsoldTo>();
            Users = new HashSet<User>();
            Zr04s = new HashSet<Zr04>();
        }

        public long SapshipToId { get; set; }
        public long? DefaultSapshipToId { get; set; }
        public bool CustomerSpecificPricing { get; set; }
        public long? RegionSapconditionGroupId { get; set; }
        public long? TierSapconditionGroupId { get; set; }
        public long? HomeMillSapconditionGroupId { get; set; }
        public string? PricingNotes { get; set; }
        public string? PricingProcedure { get; set; }
        public DateTime? LastBacklogRefresh { get; set; }
        public bool? RefreshingBacklog { get; set; }
        public bool Oem { get; set; }
        public short? PriceOption { get; set; }

        public virtual SapshipTo? DefaultSapshipTo { get; set; }
        public virtual SapconditionGroup? HomeMillSapconditionGroup { get; set; }
        public virtual SapconditionGroup? RegionSapconditionGroup { get; set; }
        public virtual SapshipTo SapshipTo { get; set; } = null!;
        public virtual SapconditionGroup? TierSapconditionGroup { get; set; }
        public virtual ICollection<Cmir> Cmirs { get; set; }
        public virtual ICollection<DealsBySoldToShipTo> DealsBySoldToShipTos { get; set; }
        public virtual ICollection<PriceChangeRequest> PriceChangeRequests { get; set; }
        public virtual ICollection<PriceSheetNote> PriceSheetNotes { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
        public virtual ICollection<RandomLengthSapsoldTo> RandomLengthSapsoldTos { get; set; }
        public virtual ICollection<Sapdelivery> Sapdeliveries { get; set; }
        public virtual ICollection<SapsalesOrder> SapsalesOrders { get; set; }
        public virtual ICollection<Sapshipment> Sapshipments { get; set; }
        public virtual ICollection<SapsoldToPlantExclusion> SapsoldToPlantExclusions { get; set; }
        public virtual ICollection<ScrapSapsoldTo> ScrapSapsoldTos { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Zr04> Zr04s { get; set; }
    }
}
