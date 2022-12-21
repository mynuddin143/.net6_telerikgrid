using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("ScrapCustomer")]
    public partial class ScrapCustomer
    {
        [Key]
        [Column("ScrapCustomerID")]
        public int ScrapCustomerID { get; set; }
        [Column("CustomerID")]
        public int CustomerID { get; set; }
        [Column("LocationID")]
        public int LocationID { get; set; }
        public bool Active { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("ScrapCustomers")]
        public virtual Customer Customer { get; set; } = null!;
        [ForeignKey("LocationID")]
        [InverseProperty("ScrapCustomers")]
        public virtual Location Location { get; set; } = null!;
    }
}
