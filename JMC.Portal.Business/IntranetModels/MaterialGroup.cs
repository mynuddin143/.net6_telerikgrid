using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("MaterialGroup")]
    [Index("Sapcode", Name = "IX_MaterialGroup", IsUnique = true)]
    public partial class MaterialGroup
    {
        public MaterialGroup()
        {
            Materials = new HashSet<Material>();
        }

        [Key]
        [Column("MaterialGroupID")]
        public int MaterialGroupId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;

        [InverseProperty("MaterialGroup")]
        public virtual ICollection<Material> Materials { get; set; }
    }
}
