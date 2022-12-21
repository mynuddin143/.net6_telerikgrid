using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("Delivery")]
    public partial class Delivery
    {
        public Delivery()
        {
            CustomerServiceComplaints = new HashSet<CustomerServiceComplaint>();
        }

        [Key]
        [Column("DeliveryID")]
        public int DeliveryId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        [InverseProperty("Delivery")]
        public virtual ICollection<CustomerServiceComplaint> CustomerServiceComplaints { get; set; }
    }
}
