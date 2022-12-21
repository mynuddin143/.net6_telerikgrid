using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class WebReleasePlant
    {
        public WebReleasePlant()
        {
            WebReleasePlantSapsalesOrderItems = new HashSet<WebReleasePlantSapsalesOrderItem>();
        }

        public long WebReleasePlantId { get; set; }
        public long WebReleaseId { get; set; }
        public long PlantId { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public string? ExpectedTime { get; set; }
        public string? CustomerComments { get; set; }
        public bool? Reviewed { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string? Csrcomments { get; set; }
        public bool? Processed { get; set; }
        public string? OnOrBefore { get; set; }
        public long? XferToPlant { get; set; }
        public bool? Stosuccess { get; set; }
        public string? Stoponumber { get; set; }
        public string? EtOutputMessages { get; set; }

        public virtual Plant Plant { get; set; } = null!;
        public virtual WebRelease WebRelease { get; set; } = null!;
        public virtual ICollection<WebReleasePlantSapsalesOrderItem> WebReleasePlantSapsalesOrderItems { get; set; }
    }
}
