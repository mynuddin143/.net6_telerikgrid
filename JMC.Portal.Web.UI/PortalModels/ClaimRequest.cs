using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ClaimRequest
    {
        public DateTime? RequestDate { get; set; }
        public string? SoldTo { get; set; }
        public string? ShipTo { get; set; }
        public string? Isr { get; set; }
        public string? BillOfLading { get; set; }
        public string? Contact { get; set; }
        public string? ContactNo { get; set; }
        public int PiecesAffected { get; set; }
        public string? MigrationsOptions { get; set; }
        public string? MaterialLength { get; set; }
        public string? MaterialGauge { get; set; }
        public string? MaterialSize { get; set; }
        public string? BatchNos { get; set; }
        public string? Reason { get; set; }
    }
}
