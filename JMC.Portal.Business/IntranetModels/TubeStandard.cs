using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("TubeStandard")]
    [Index("Sapcode", Name = "IX_TubeStandard", IsUnique = true)]
    public partial class TubeStandard
    {
        public TubeStandard()
        {
            Materials = new HashSet<Material>();
        }

        [Key]
        [Column("TubeStandardID")]
        public int TubeStandardId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;

        [InverseProperty("TubeStandard")]
        public virtual ICollection<Material> Materials { get; set; }
    }
}
