using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapsalesDelivery
    {
        public long SapdeliveryId { get; set; }
        public long? SapstorageLocationId { get; set; }
        public string? WarehouseNumber { get; set; }
        public long? SapshipToId { get; set; }
        public decimal? FreightChargedToCustomer { get; set; }
        public string? CustomerFreightCurrency { get; set; }
        public decimal? UsdfreightChargedToCustomer { get; set; }
        public decimal? CustomerCadexchangeRateUsed { get; set; }
        public string? TrailorNumber { get; set; }
        public decimal? Length { get; set; }
        public string? EqualizedFreight { get; set; }
        public string? InvoiceNumbers { get; set; }

        public virtual Sapdelivery Sapdelivery { get; set; } = null!;
        public virtual SapshipTo? SapshipTo { get; set; }
        public virtual SapstorageLocation? SapstorageLocation { get; set; }
    }
}
