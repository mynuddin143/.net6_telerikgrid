using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("TubeShape")]
    [Index("Sapcode", Name = "IX_TubeShape", IsUnique = true)]
    public partial class TubeShape
    {
        public TubeShape()
        {
            Materials = new HashSet<Material>();
        }

        [Key]
        [Column("TubeShapeID")]
        public int TubeShapeId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;

        [InverseProperty("TubeShape")]
        public virtual ICollection<Material> Materials { get; set; }
    }
}
