using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("AlternateCoilIndicator")]
    [Index("Sapcode", Name = "IX_AlternateCoilIndicator", IsUnique = true)]
    public partial class AlternateCoilIndicator
    {
        public AlternateCoilIndicator()
        {
            Materials = new HashSet<Material>();
        }

        [Key]
        [Column("AlternateCoilIndicatorID")]
        public int AlternateCoilIndicatorId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;

        [InverseProperty("AlternateCoilIndicator")]
        public virtual ICollection<Material> Materials { get; set; }
    }
}
