using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Keyless]
    public partial class VwMaterial
    {
        [Column("MaterialID")]
        public int MaterialId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(25)]
        [Unicode(false)]
        public string Number { get; set; } = null!;
        [Column("MaterialGroupID")]
        public int MaterialGroupId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MaterialGroupName { get; set; } = null!;
        [Column("TubeShapeID")]
        public int TubeShapeId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TubeShapeName { get; set; } = null!;
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? TubeLength { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? TubeSize { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? TubeSize2 { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? TubeDiameter { get; set; }
        [Column("TubeStandardID")]
        public int? TubeStandardId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? TubeStandardName { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? GaugeRestrictable { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? TubeWeightPerFoot { get; set; }
        [Column("MaterialCoatingID")]
        public int MaterialCoatingId { get; set; }
    }
}
