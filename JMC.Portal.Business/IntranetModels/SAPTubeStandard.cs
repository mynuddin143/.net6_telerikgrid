using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPTubeStandard")]
    [Index("Sapcode", Name = "IX_SAPTubeStandard", IsUnique = true)]
    public partial class SAPTubeStandard
    {
        public SAPTubeStandard()
        {
            SAPMaterials = new HashSet<SAPMaterial>();
        }

        [Key]
        [Column("SAPTubeStandardID")]
        public int SAPTubeStandardID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPCode { get; set; } = null!;

        [InverseProperty("SAPTubeStandard")]
        public virtual ICollection<SAPMaterial> SAPMaterials { get; set; }
    }
}
