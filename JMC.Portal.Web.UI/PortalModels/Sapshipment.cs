using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Sapshipment
    {
        public Sapshipment()
        {
            Sapdeliveries = new HashSet<Sapdelivery>();
            ShipmentCosts = new HashSet<ShipmentCost>();
        }

        public long SapshipmentId { get; set; }
        public long DivisionId { get; set; }
        public DateTime ActualGoodsMovementDate { get; set; }
        public string? Number { get; set; }
        public long SapdeliveryTypeId { get; set; }
        public long? PlantId { get; set; }
        public long? SapsoldToId { get; set; }
        public long? SapvendorId { get; set; }
        public decimal? UsdfreightPaidToCarrier { get; set; }
        public decimal? UsdfreightChargedToCustomer { get; set; }
        public decimal? Distance { get; set; }
        public string? DistanceUom { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? AccessCharges { get; set; }
        public decimal? Fsc { get; set; }
        public string? DeliveryNumber { get; set; }
        public long? ProductLineId { get; set; }
        public long? DeliveryMethodId { get; set; }
        public bool? Excepted { get; set; }
        public bool AgentWarehouse { get; set; }
        public bool Scrap { get; set; }
        public bool Express { get; set; }
        public bool SpotRate { get; set; }
        public decimal? IpsactualWeight { get; set; }
        public decimal? IpscalculatedMiles { get; set; }
        public decimal? IpslineHaul { get; set; }
        public decimal? Ipsfsc { get; set; }
        public decimal? IpstotalCost { get; set; }
        public bool? Ipsonly { get; set; }
        public DateTime? IpspaidDate { get; set; }
        public decimal? Weight { get; set; }
        public bool Intermodal { get; set; }
        public bool? IpsgotPaidWeight { get; set; }
        public bool? IpsgotPaidMiles { get; set; }
        public decimal? IpsadditionalCharges { get; set; }
        public bool? Ipsexception { get; set; }

        public virtual DeliveryMethod? DeliveryMethod { get; set; }
        public virtual Division Division { get; set; } = null!;
        public virtual Plant? Plant { get; set; }
        public virtual ProductLine? ProductLine { get; set; }
        public virtual SapdeliveryType SapdeliveryType { get; set; } = null!;
        public virtual SapsoldTo? SapsoldTo { get; set; }
        public virtual Sapvendor? Sapvendor { get; set; }
        public virtual ICollection<Sapdelivery> Sapdeliveries { get; set; }
        public virtual ICollection<ShipmentCost> ShipmentCosts { get; set; }
    }
}
