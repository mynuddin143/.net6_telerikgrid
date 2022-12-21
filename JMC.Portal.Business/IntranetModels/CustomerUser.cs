using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("CustomerUser")]
    public partial class CustomerUser
    {
        [Key]
        [Column("CustomerUserID")]
        public int CustomerUserID { get; set; }
        [Column("PrimaryCustomerID")]
        public int PrimaryCustomerID { get; set; }

        [ForeignKey("CustomerUserID")]
        [InverseProperty("CustomerUser")]
        public virtual User CustomerUserNavigation { get; set; } = null!;
        [ForeignKey("PrimaryCustomerID")]
        [InverseProperty("CustomerUsers")]
        public virtual Customer PrimaryCustomer { get; set; } = null!;
    }
}
