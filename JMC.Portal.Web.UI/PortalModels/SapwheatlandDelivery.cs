using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapwheatlandDelivery
    {
        public int DeliveryId { get; set; }
        public string? DeliveryNumber { get; set; }
        public string? ShipToNumber { get; set; }
        public string? SoldToNumber { get; set; }
        public string? CustomerPo { get; set; }
        public string? SalesOrderNumber { get; set; }
        public string? MaterialNumber { get; set; }
        public string? ItemDesc { get; set; }
        public string? BatchNumber { get; set; }
        public string? HeatNumber { get; set; }
        public string? RunNumber { get; set; }
        public DateTime? Pgidate { get; set; }
    }
}
