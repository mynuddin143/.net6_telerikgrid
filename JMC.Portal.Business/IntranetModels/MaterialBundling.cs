using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("MaterialBundling")]
    [Index("MaterialId", "RectangleMaterialBundlingId", "RoundMaterialBundlingId", Name = "IX_MaterialBundling", IsUnique = true)]
    public partial class MaterialBundling
    {
        [Key]
        [Column("MaterialBundlingID")]
        public int MaterialBundlingID { get; set; }
        [Column("MaterialID")]
        public int MaterialID { get; set; }
        [Column("RectangleMaterialBundlingID")]
        public int? RectangleMaterialBundlingID { get; set; }
        [Column("RoundMaterialBundlingID")]
        public int? RoundMaterialBundlingID { get; set; }

        [ForeignKey("MaterialID")]
        [InverseProperty("MaterialBundlings")]
        public virtual Material Material { get; set; } = null!;
        [ForeignKey("RectangleMaterialBundlingID")]
        [InverseProperty("MaterialBundlings")]
        public virtual RectangleMaterialBundling? RectangleMaterialBundling { get; set; }
        [ForeignKey("RoundMaterialBundlingId")]
        [InverseProperty("MaterialBundlings")]
        public virtual RoundMaterialBundling? RoundMaterialBundling { get; set; }
    }
}
