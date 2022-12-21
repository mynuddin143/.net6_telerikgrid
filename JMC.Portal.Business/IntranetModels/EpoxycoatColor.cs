using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("EpoxycoatColor")]
    [Index("Sapcode", Name = "IX_EpoxycoatColor", IsUnique = true)]
    public partial class EpoxycoatColor
    {
        public EpoxycoatColor()
        {
            Materials = new HashSet<Material>();
        }

        [Key]
        [Column("EpoxycoatColorID")]
        public int EpoxycoatColorId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;

        [InverseProperty("EpoxycoatColor")]
        public virtual ICollection<Material> Materials { get; set; }
    }
}
