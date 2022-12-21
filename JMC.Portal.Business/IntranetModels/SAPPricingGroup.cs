using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPPricingGroup")]
    [Index("Sapcode", Name = "IX_SAPPricingGroup", IsUnique = true)]
    public partial class SAPPricingGroup
    {
        public SAPPricingGroup()
        {
            SAPMaterials = new HashSet<SAPMaterial>();
        }

        [Key]
        [Column("SAPPricingGroupID")]
        public int SAPPricingGroupID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;
        public bool Active { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty("SappricingGroup")]
        public virtual ICollection<SAPMaterial> SAPMaterials { get; set; }
    }
}
