using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("CustomerServiceProblem")]
    public partial class CustomerServiceProblem
    {
        public CustomerServiceProblem()
        {
            CustomerServiceComplaints = new HashSet<CustomerServiceComplaint>();
        }

        [Key]
        [Column("CustomerServiceProblemID")]
        public int CustomerServiceProblemId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        [InverseProperty("CustomerServiceProblem")]
        public virtual ICollection<CustomerServiceComplaint> CustomerServiceComplaints { get; set; }
    }
}
