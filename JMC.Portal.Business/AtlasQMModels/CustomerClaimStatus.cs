using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("CustomerClaimStatus")]
    public partial class CustomerClaimStatus
    {
        public CustomerClaimStatus()
        {
            CustomerComplaints = new HashSet<CustomerComplaint>();
        }

        [Key]
        [Column("ID")]
        public int ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Status { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<CustomerComplaint> CustomerComplaints { get; set; }
    }
}
