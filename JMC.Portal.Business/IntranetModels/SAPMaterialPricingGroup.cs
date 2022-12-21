using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPMaterialPricingGroup")]
    [Index("Sapcode", Name = "IX_SAPMaterialPricingGroup", IsUnique = true)]
    public partial class SAPMaterialPricingGroup
    {
        public SAPMaterialPricingGroup()
        {
            SAPMaterials = new HashSet<SAPMaterial>();
        }

        [Key]
        [Column("SAPMaterialPricingGroupID")]
        public int SAPMaterialPricingGroupID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;

        [InverseProperty("SAPMaterialPricingGroup")]
        public virtual ICollection<SAPMaterial> SAPMaterials { get; set; }
    }
}
