using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("InsideSalesRep")]
    public partial class InsideSalesRep
    {
        public InsideSalesRep()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        [Column("InsideSalesRepID")]
        public int InsideSalesRepID { get; set; }
        [Column("SAPSalesGroupID")]
        [StringLength(3)]
        [Unicode(false)]
        public string SAPSalesGroupID { get; set; } = null!;
        [Column("SAPSalesGroupName")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPSalesGroupName { get; set; } = null!;
        [Column("EmployeeID")]
        public int? EmployeeID { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string? AlternateEmailAddresses { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("InsideSalesReps")]
        public virtual Employee? Employee { get; set; }
        [InverseProperty("InsideSalesRep")]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
