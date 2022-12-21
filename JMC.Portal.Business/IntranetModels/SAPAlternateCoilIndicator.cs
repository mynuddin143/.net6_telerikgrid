using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPAlternateCoilIndicator")]
    [Index("Sapcode", Name = "IX_SAPAlternateCoilIndicator", IsUnique = true)]
    public partial class SAPAlternateCoilIndicator
    {
        public SAPAlternateCoilIndicator()
        {
            SAPMaterials = new HashSet<SAPMaterial>();
        }

        [Key]
        [Column("SAPAlternateCoilIndicatorID")]
        public int SAPAlternateCoilIndicatorID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;

        [InverseProperty("SapalternateCoilIndicator")]
        public virtual ICollection<SAPMaterial> SAPMaterials { get; set; }
    }
}
