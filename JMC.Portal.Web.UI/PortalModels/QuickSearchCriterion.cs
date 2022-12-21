using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class QuickSearchCriterion
    {
        public long CriteriaId { get; set; }
        public long TemplateId { get; set; }
        public decimal? SizeX { get; set; }
        public decimal? SizeY { get; set; }
        public decimal? Diameter { get; set; }
        public decimal? Length { get; set; }
        public string? GaugeRestrictable { get; set; }
        public string? Grade { get; set; }
        public int? Quantity { get; set; }
        public decimal? Inches { get; set; }
        public long ShiptoId { get; set; }
        public long SoldToId { get; set; }
        public DateTime? RequestedDate { get; set; }
        public string? Shape { get; set; }

        public virtual QuickSearchTemplate Template { get; set; } = null!;
    }
}
