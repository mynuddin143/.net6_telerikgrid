using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Sapdelivery
    {
        public Sapdelivery()
        {
            SapdeliveryItems = new HashSet<SapdeliveryItem>();
        }

        public long SapdeliveryId { get; set; }
        public long DivisionId { get; set; }
        public DateTime? ActualGoodsMovementDate { get; set; }
        public string? Number { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public long? PlantId { get; set; }
        public long? SapsoldToId { get; set; }
        public decimal? Weight { get; set; }
        public string? WeightUnit { get; set; }
        public string? BapireturnMessage { get; set; }
        public decimal? Distance { get; set; }
        public string? DistanceUom { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string? ShipmentNumber { get; set; }
        public decimal? AccessCharges { get; set; }
        public decimal? Fsc { get; set; }
        public string? DeliveryType { get; set; }
        public long SapshipmentId { get; set; }
        public bool Transfer { get; set; }
        public decimal? FreightPaidToCarrier { get; set; }
        public string? CarrierFreightCurrency { get; set; }
        public decimal? UsdfreightPaidToCarrier { get; set; }
        public decimal? CarrierCadexchangeRateUsed { get; set; }
        public long? SapvendorId { get; set; }
        public string? IncoTerms2 { get; set; }
        public decimal? IpsactualWeight { get; set; }
        public bool? IpsgotPaidWeight { get; set; }
        public DateTime? TransportationPlanningDate { get; set; }
        public DateTime? Reqdeldate { get; set; }
        public DateTime? Tmsdeldate { get; set; }
        public DateTime? PickupAptDate { get; set; }
        public DateTime? CheckoutDate { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual Plant? Plant { get; set; }
        public virtual Sapshipment Sapshipment { get; set; } = null!;
        public virtual SapsoldTo? SapsoldTo { get; set; }
        public virtual Sapvendor? Sapvendor { get; set; }
        public virtual SapsalesDelivery SapsalesDelivery { get; set; } = null!;
        public virtual SapscrapDelivery SapscrapDelivery { get; set; } = null!;
        public virtual ICollection<SapdeliveryItem> SapdeliveryItems { get; set; }
    }
}
