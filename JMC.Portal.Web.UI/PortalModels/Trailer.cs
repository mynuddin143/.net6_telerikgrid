using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Trailer
    {
        public Trailer()
        {
            ScrapSales = new HashSet<ScrapSale>();
        }

        public long TrailerId { get; set; }
        public string Name { get; set; } = null!;
        public long? SapvendorId { get; set; }
        public decimal? Weight { get; set; }
        public bool Active { get; set; }
        public long? ScrapSapsoldToId { get; set; }
        public long? RandomLengthSapsoldToId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime? ChagedDate { get; set; }
        public decimal? PreviousWeight { get; set; }

        public virtual RandomLengthSapsoldTo? RandomLengthSapsoldTo { get; set; }
        public virtual Sapvendor? Sapvendor { get; set; }
        public virtual ScrapSapsoldTo? ScrapSapsoldTo { get; set; }
        public virtual ICollection<ScrapSale> ScrapSales { get; set; }
    }
}
