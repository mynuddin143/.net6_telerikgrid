using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPMaterial")]
    [Index("Diameter", "GaugeRestrictable", Name = "ix_Diameter_GaugeRestrictable")]
    [Index("Size", Name = "ix_Size")]
    [Index("Size", "GaugeRestrictable", Name = "ix_Size_GaugeRestrictable")]
    [Index("Size", "Size2", Name = "ix_Size_Size2")]
    [Index("Size", "Size2", "GaugeRestrictable", Name = "ix_Size_Size2_GaugeRestrictable")]
    public partial class SAPMaterial
    {
        [Key]
        [Column("SAPMaterialID")]
        public int SAPMaterialID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(25)]
        [Unicode(false)]
        public string Number { get; set; } = null!;
        [Column("SAPMaterialGroupID")]
        public int SAPMaterialGroupID { get; set; }
        [Column("SAPAlternateCoilIndicatorID")]
        public int? SAPAlternateCoilIndicatorID { get; set; }
        public bool Cutting { get; set; }
        public bool Kleenkote { get; set; }
        [Column("SAPKleenkoteColorID")]
        public int? SAPKleenkoteColorID { get; set; }
        public bool Epoxycoat { get; set; }
        [Column("SAPEpoxycoatColorID")]
        public int? SAPEpoxycoatColorID { get; set; }
        [Column("SAPTubeShapeID")]
        public int? SAPTubeShapeID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Size { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Size2 { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? Diameter { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Length { get; set; }
        public int? LengthFeet { get; set; }
        public int? LengthInches { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string LengthFractionalInches { get; set; } = null!;
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? PieceWeight { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? WeightPerFoot { get; set; }
        [Column("SAPTubeStandardID")]
        public int? SAPTubeStandardID { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? GaugeRestrictable { get; set; }
        public int? Bundling1 { get; set; }
        public int? Bundling2 { get; set; }
        public int? BundlingRound { get; set; }
        [Column("SAPSpecificationID")]
        public int? SAPSpecificationID { get; set; }
        [Column("SAPSalesInstructionID")]
        public int? SAPSalesInstructionID { get; set; }
        public bool Configurable { get; set; }
        [Column("SAPMaterialPricingGroupID")]
        public int? SAPMaterialPricingGroupID { get; set; }
        [Column("SAPPricingGroupID")]
        public int? SAPPricingGroupID { get; set; }
        [Column("SAPMaterialTypeID")]
        public int? SAPMaterialTypeID { get; set; }

        [ForeignKey("SAPAlternateCoilIndicatorId")]
        [InverseProperty("SAPMaterials")]
        public virtual SAPAlternateCoilIndicator? SAPAlternateCoilIndicator { get; set; }
        [ForeignKey("SAPEpoxycoatColorID")]
        [InverseProperty("SAPMaterials")]
        public virtual SAPEpoxycoatColor? SAPEpoxycoatColor { get; set; }
        [ForeignKey("SAPKleenkoteColorID")]
        [InverseProperty("SAPMaterials")]
        public virtual SAPKleenkoteColor? SAPKleenkoteColor { get; set; }
        [ForeignKey("SAPMaterialGroupID")]
        [InverseProperty("SAPMaterials")]
        public virtual SAPMaterialGroup SAPMaterialGroup { get; set; } = null!;
        [ForeignKey("SAPMaterialPricingGroupId")]
        [InverseProperty("SAPMaterials")]
        public virtual SAPMaterialPricingGroup? SAPMaterialPricingGroup { get; set; }
        [ForeignKey("SAPMaterialTypeId")]
        [InverseProperty("SAPMaterials")]
        public virtual SAPMaterialType? SAPMaterialType { get; set; }
        [ForeignKey("SAPPricingGroupId")]
        [InverseProperty("SAPMaterials")]
        public virtual SAPPricingGroup? SAPPricingGroup { get; set; }
        [ForeignKey("SAPSalesInstructionID")]
        [InverseProperty("SAPMaterials")]
        public virtual SAPSalesInstruction? SAPSalesInstruction { get; set; }
        [ForeignKey("SAPSpecificationID")]
        [InverseProperty("SAPMaterials")]
        public virtual SAPSpecification? SAPSpecification { get; set; }
        [ForeignKey("SAPTubeShapeID")]
        [InverseProperty("SAPMaterials")]
        public virtual SAPTubeShape? SAPTubeShape { get; set; }
        [ForeignKey("SAPTubeStandardID")]
        [InverseProperty("SAPMaterials")]
        public virtual SAPTubeStandard? SAPTubeStandard { get; set; }
    }
}
