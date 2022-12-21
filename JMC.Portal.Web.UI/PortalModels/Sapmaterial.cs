using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Sapmaterial
    {
        public Sapmaterial()
        {
            Cmirs = new HashSet<Cmir>();
            QuoteMaterials = new HashSet<QuoteMaterial>();
            SapmaterialPlants = new HashSet<SapmaterialPlant>();
            SapmaterialUnitOfMeasures = new HashSet<SapmaterialUnitOfMeasure>();
            SapsalesOrderItems = new HashSet<SapsalesOrderItem>();
            Sapstocks = new HashSet<Sapstock>();
            ShoppingCartSaprollings = new HashSet<ShoppingCartSaprolling>();
        }

        public long SapmaterialId { get; set; }
        public long DivisionId { get; set; }
        public string Name { get; set; } = null!;
        public string Number { get; set; } = null!;
        public long SapmaterialGroupId { get; set; }
        public long? SapmaterialTypeId { get; set; }
        public long? SapalternateCoilIndicatorId { get; set; }
        public bool Cutting { get; set; }
        public long? SapkleenkoteColorId { get; set; }
        public long? SapepoxycoatColorId { get; set; }
        public long? SaptubeShapeId { get; set; }
        public decimal? Size { get; set; }
        public decimal? Size2 { get; set; }
        public decimal? Diameter { get; set; }
        public decimal? Length { get; set; }
        public int? LengthFeet { get; set; }
        public int? LengthInches { get; set; }
        public string LengthFractionalInches { get; set; } = null!;
        public decimal? PieceWeight { get; set; }
        public decimal? WeightPerFoot { get; set; }
        public long? SaptubeStandardId { get; set; }
        public decimal? GaugeRestrictable { get; set; }
        public int? Bundling1 { get; set; }
        public int? Bundling2 { get; set; }
        public int? BundlingRound { get; set; }
        public long? SapspecificationId { get; set; }
        public long? SapsalesInstructionId { get; set; }
        public bool Configurable { get; set; }
        public long? SapmaterialPricingGroupId { get; set; }
        public long? SappricingGroupId { get; set; }
        public long? SapproductLineId { get; set; }
        public long? SapproductGroupId { get; set; }
        public long? SapproductTypeId { get; set; }
        public long? SapproductColorFinishId { get; set; }
        public long? SapproductEndFinishId { get; set; }
        public string? ProductSize { get; set; }
        public string? ProductGauge { get; set; }
        public decimal? GrossWeight { get; set; }
        public decimal? NetWeight { get; set; }
        public string? CommissionGroup { get; set; }
        public string? VolumeRebateGroup { get; set; }
        public long? SapproductEndFinishEnergexId { get; set; }
        public long? SapmetalGradeId { get; set; }
        public string? Npsdescription { get; set; }
        public bool? BlockFromPortalRollings { get; set; }
        public string? PlanningMaterial { get; set; }
        public decimal? BundleWeight { get; set; }
        public decimal? TotalBundleLength { get; set; }
        public int? PieceperBundle { get; set; }
        public string? OldMaterialNumber { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual SapcharacteristicOption? SapalternateCoilIndicator { get; set; }
        public virtual SapcharacteristicOption? SapepoxycoatColor { get; set; }
        public virtual SapcharacteristicOption? SapkleenkoteColor { get; set; }
        public virtual SapcharacteristicOption SapmaterialGroup { get; set; } = null!;
        public virtual SapcharacteristicOption? SapmaterialPricingGroup { get; set; }
        public virtual SapcharacteristicOption? SapmaterialType { get; set; }
        public virtual SapcharacteristicOption? SapmetalGrade { get; set; }
        public virtual SapcharacteristicOption? SappricingGroup { get; set; }
        public virtual SapcharacteristicOption? SapproductColorFinish { get; set; }
        public virtual SapcharacteristicOption? SapproductEndFinish { get; set; }
        public virtual SapcharacteristicOption? SapproductEndFinishEnergex { get; set; }
        public virtual SapcharacteristicOption? SapproductGroup { get; set; }
        public virtual SapcharacteristicOption? SapproductLine { get; set; }
        public virtual SapcharacteristicOption? SapproductType { get; set; }
        public virtual SapcharacteristicOption? SapsalesInstruction { get; set; }
        public virtual SapcharacteristicOption? Sapspecification { get; set; }
        public virtual SapcharacteristicOption? SaptubeShape { get; set; }
        public virtual SapcharacteristicOption? SaptubeStandard { get; set; }
        public virtual ICollection<Cmir> Cmirs { get; set; }
        public virtual ICollection<QuoteMaterial> QuoteMaterials { get; set; }
        public virtual ICollection<SapmaterialPlant> SapmaterialPlants { get; set; }
        public virtual ICollection<SapmaterialUnitOfMeasure> SapmaterialUnitOfMeasures { get; set; }
        public virtual ICollection<SapsalesOrderItem> SapsalesOrderItems { get; set; }
        public virtual ICollection<Sapstock> Sapstocks { get; set; }
        public virtual ICollection<ShoppingCartSaprolling> ShoppingCartSaprollings { get; set; }
    }
}
