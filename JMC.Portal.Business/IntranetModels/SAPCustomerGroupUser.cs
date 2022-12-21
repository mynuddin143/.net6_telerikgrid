using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPCustomerGroupUser")]
    public partial class SAPCustomerGroupUser
    {
        [Key]
        [Column("SAPCustomerGroupUserID")]
        public int SAPCustomerGroupUserID { get; set; }
        [Column("SAPCustomerGroupID")]
        public int SAPCustomerGroupID { get; set; }
        [Column("UserID")]
        public int UserID { get; set; }

        [ForeignKey("SAPCustomerGroupID")]
        [InverseProperty("SAPCustomerGroupUsers")]
        public virtual SAPCustomerGroup SAPCustomerGroup { get; set; } = null!;
        [ForeignKey("UserID")]
        [InverseProperty("SAPCustomerGroupUsers")]
        public virtual User User { get; set; } = null!;
    }
}
