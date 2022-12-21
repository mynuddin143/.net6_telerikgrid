using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("KleenkoteColor")]
    [Index("Sapcode", Name = "IX_KleenkoteColor", IsUnique = true)]
    public partial class KleenkoteColor
    {
        public KleenkoteColor()
        {
            Materials = new HashSet<Material>();
        }

        [Key]
        [Column("KleenkoteColorID")]
        public int KleenkoteColorId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;

        [InverseProperty("KleenkoteColor")]
        public virtual ICollection<Material> Materials { get; set; }
    }
}
