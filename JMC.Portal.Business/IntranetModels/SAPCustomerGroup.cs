using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPCustomerGroup")]
    [Index("Sapcode", Name = "IX_SAPCustomerGroup", IsUnique = true)]
    public partial class SAPCustomerGroup
    {
        public SAPCustomerGroup()
        {
            SAPCustomerGroupUsers = new HashSet<SAPCustomerGroupUser>();
            SAPSoldTos = new HashSet<SAPSoldTo>();
        }

        [Key]
        [Column("SAPCustomerGroupID")]
        public int SAPCustomerGroupID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPCode { get; set; } = null!;
        [Column("UserID")]
        public int? UserID { get; set; }
        [Column("RegionalManagerUserID")]
        public int? RegionalManagerUserID { get; set; }

        [ForeignKey("RegionalManagerUserID")]
        [InverseProperty("SAPCustomerGroups")]
        public virtual User? RegionalManagerUser { get; set; }
        [InverseProperty("SAPCustomerGroup")]
        public virtual ICollection<SAPCustomerGroupUser> SAPCustomerGroupUsers { get; set; }
        [InverseProperty("SAPCustomerGroup")]
        public virtual ICollection<SAPSoldTo> SAPSoldTos { get; set; }
    }
}
