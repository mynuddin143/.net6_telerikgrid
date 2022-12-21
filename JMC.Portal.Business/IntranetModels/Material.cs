using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Material")]
    public partial class Material
    {
        public Material()
        {
            MaterialBundlings = new HashSet<MaterialBundling>();
        }

        [Key]
        [Column("MaterialID")]
        public int MaterialID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(25)]
        [Unicode(false)]
        public string Number { get; set; } = null!;
        [Column("MaterialGroupID")]
        public int MaterialGroupID { get; set; }
        [Column("AlternateCoilIndicatorID")]
        public int? AlternateCoilIndicatorId { get; set; }
        public bool Cutting { get; set; }
        public bool Kleenkote { get; set; }
        [Column("KleenkoteColorID")]
        public int? KleenkoteColorID { get; set; }
        public bool Epoxycoat { get; set; }
        [Column("EpoxycoatColorID")]
        public int? EpoxycoatColorID { get; set; }
        [Column("TubeShapeID")]
        public int TubeShapeID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? TubeSize { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? TubeSize2 { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? TubeDiameter { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? TubeLength { get; set; }
        public int? TubeLengthFeet { get; set; }
        public int? TubeLengthInches { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string TubeLengthFractionalInches { get; set; } = null!;
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? TubePieceWeight { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? TubeWeightPerFoot { get; set; }
        [Column("TubeStandardID")]
        public int? TubeStandardID { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? GaugeRestrictable { get; set; }
        public int? TubeBundling1 { get; set; }
        public int? TubeBundling2 { get; set; }
        public int? TubeBundlingRound { get; set; }
        [Column("TubeSpecificationID")]
        public int? TubeSpecificationID { get; set; }
        [Column("SalesInstructionID")]
        public int? SalesInstructionID { get; set; }
        public bool Configurable { get; set; }

        [ForeignKey("AlternateCoilIndicatorID")]
        [InverseProperty("Materials")]
        public virtual AlternateCoilIndicator? AlternateCoilIndicator { get; set; }
        [ForeignKey("EpoxycoatColorID")]
        [InverseProperty("Materials")]
        public virtual EpoxycoatColor? EpoxycoatColor { get; set; }
        [ForeignKey("KleenkoteColorID")]
        [InverseProperty("Materials")]
        public virtual KleenkoteColor? KleenkoteColor { get; set; }
        [ForeignKey("MaterialGroupID")]
        [InverseProperty("Materials")]
        public virtual MaterialGroup MaterialGroup { get; set; } = null!;
        [ForeignKey("SalesInstructionID")]
        [InverseProperty("Materials")]
        public virtual SalesInstruction? SalesInstruction { get; set; }
        [ForeignKey("TubeShapeID")]
        [InverseProperty("Materials")]
        public virtual TubeShape TubeShape { get; set; } = null!;
        [ForeignKey("TubeSpecificationID")]
        [InverseProperty("Materials")]
        public virtual TubeSpecification? TubeSpecification { get; set; }
        [ForeignKey("TubeStandardId")]
        [InverseProperty("Materials")]
        public virtual TubeStandard? TubeStandard { get; set; }
        [InverseProperty("Material")]
        public virtual ICollection<MaterialBundling> MaterialBundlings { get; set; }
    }
}
