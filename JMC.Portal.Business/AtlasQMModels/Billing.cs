using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("Billing")]
    public partial class Billing
    {
        public Billing()
        {
            CustomerServiceComplaints = new HashSet<CustomerServiceComplaint>();
        }

        [Key]
        [Column("BillingID")]
        public int BillingId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        [InverseProperty("Billing")]
        public virtual ICollection<CustomerServiceComplaint> CustomerServiceComplaints { get; set; }
    }
}
