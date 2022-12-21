using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Keyless]
    [Table("CustomerUserCustomer")]
    public partial class CustomerUserCustomer
    {
        [Column("CustomerUserID")]
        public int CustomerUserID { get; set; }
        [Column("CustomerID")]
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; } = null!;
        [ForeignKey("CustomerUserID")]
        public virtual CustomerUser CustomerUser { get; set; } = null!;
    }
}
