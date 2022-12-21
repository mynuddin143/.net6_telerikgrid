using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("RandomLengthCustomer")]
    public partial class RandomLengthCustomer
    {
        [Key]
        [Column("RandomLengthCustomerID")]
        public int RandomLengthCustomerId { get; set; }
        [Column("CustomerID")]
        public int CustomerID { get; set; }
        [Column("LocationID")]
        public int LocationID { get; set; }
        public bool Active { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("RandomLengthCustomers")]
        public virtual Customer Customer { get; set; } = null!;
        [ForeignKey("LocationID")]
        [InverseProperty("RandomLengthCustomers")]
        public virtual Location Location { get; set; } = null!;
    }
}
