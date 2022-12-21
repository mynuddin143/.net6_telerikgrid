using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("ProductQuantity")]
    public partial class ProductQuantity
    {
        public ProductQuantity()
        {
            CustomerServiceComplaints = new HashSet<CustomerServiceComplaint>();
        }

        [Key]
        [Column("ProductQuantityID")]
        public int ProductQuantityId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        [InverseProperty("ProductQuantity")]
        public virtual ICollection<CustomerServiceComplaint> CustomerServiceComplaints { get; set; }
    }
}
