using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SalesInstruction")]
    [Index("Sapcode", Name = "IX_SalesInstruction", IsUnique = true)]
    public partial class SalesInstruction
    {
        public SalesInstruction()
        {
            Materials = new HashSet<Material>();
        }

        [Key]
        [Column("SalesInstructionID")]
        public int SalesInstructionId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string Sapcode { get; set; } = null!;

        [InverseProperty("SalesInstruction")]
        public virtual ICollection<Material> Materials { get; set; }
    }
}
