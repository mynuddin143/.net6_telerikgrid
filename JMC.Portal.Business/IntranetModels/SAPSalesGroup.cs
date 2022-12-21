using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPSalesGroup")]
    [Index("Sapcode", Name = "IX_SAPSalesGroup", IsUnique = true)]
    public partial class SAPSalesGroup
    {
        public SAPSalesGroup()
        {
            SAPSoldTos = new HashSet<SAPSoldTo>();
        }

        [Key]
        [Column("SAPSalesGroupID")]
        public int SAPSalesGroupID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPCode { get; set; } = null!;
        [Column("UserID")]
        public int? UserId { get; set; }

        [InverseProperty("SAPSalesGroup")]
        public virtual ICollection<SAPSoldTo> SAPSoldTos { get; set; }
    }
}
