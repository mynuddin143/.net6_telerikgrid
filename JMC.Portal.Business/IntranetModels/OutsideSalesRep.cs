using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("OutsideSalesRep")]
    public partial class OutsideSalesRep
    {
        public OutsideSalesRep()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        [Column("OutsideSalesRepID")]
        public int OutsideSalesRepID { get; set; }
        [Column("SAPCustomerGroupID")]
        [StringLength(2)]
        [Unicode(false)]
        public string SAPCustomerGroupID { get; set; } = null!;
        [Column("SAPCustomerGroupName")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPCustomerGroupName { get; set; } = null!;
        [Column("UserID")]
        public int? UserID { get; set; }

        [ForeignKey("UserID")]
        [InverseProperty("OutsideSalesReps")]
        public virtual User? User { get; set; }
        [InverseProperty("OutsideSalesRep")]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
