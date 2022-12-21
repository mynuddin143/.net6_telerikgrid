using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPTubeShape")]
    [Index("Sapcode", Name = "IX_SAPTubeShape", IsUnique = true)]
    public partial class SAPTubeShape
    {
        public SAPTubeShape()
        {
            SAPMaterials = new HashSet<SAPMaterial>();
        }

        [Key]
        [Column("SAPTubeShapeID")]
        public int SAPTubeShapeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPCode { get; set; } = null!;

        [InverseProperty("SAPTubeShape")]
        public virtual ICollection<SAPMaterial> SAPMaterials { get; set; }
    }
}
