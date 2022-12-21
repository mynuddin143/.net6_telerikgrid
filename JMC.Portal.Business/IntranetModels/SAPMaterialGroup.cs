using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPMaterialGroup")]
    [Index("Sapcode", Name = "IX_SAPMaterialGroup", IsUnique = true)]
    public partial class SAPMaterialGroup
    {
        public SAPMaterialGroup()
        {
            SAPMaterials = new HashSet<SAPMaterial>();
        }

        [Key]
        [Column("SAPMaterialGroupID")]
        public int SAPMaterialGroupID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPCode { get; set; } = null!;

        [InverseProperty("SAPMaterialGroup")]
        public virtual ICollection<SAPMaterial> SAPMaterials { get; set; }
    }
}
