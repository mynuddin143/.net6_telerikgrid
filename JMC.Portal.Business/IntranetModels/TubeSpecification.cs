using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("TubeSpecification")]
    [Index("Sapcode", Name = "IX_TubeSpecification", IsUnique = true)]
    public partial class TubeSpecification
    {
        public TubeSpecification()
        {
            Materials = new HashSet<Material>();
        }

        [Key]
        [Column("TubeSpecificationID")]
        public int TubeSpecificationId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;

        [InverseProperty("TubeSpecification")]
        public virtual ICollection<Material> Materials { get; set; }
    }
}
