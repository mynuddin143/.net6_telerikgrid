using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPSalesInstruction")]
    [Index("Sapcode", Name = "IX_SAPSalesInstruction", IsUnique = true)]
    public partial class SAPSalesInstruction
    {
        public SAPSalesInstruction()
        {
            SAPMaterials = new HashSet<SAPMaterial>();
        }

        [Key]
        [Column("SAPSalesInstructionID")]
        public int SAPSalesInstructionID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;

        [InverseProperty("SapsalesInstruction")]
        public virtual ICollection<SAPMaterial> SAPMaterials { get; set; }
    }
}
