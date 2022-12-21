using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("EmployeePosition")]
    public partial class EmployeePosition
    {
        public EmployeePosition()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        [Column("EmployeePositionID")]
        public int EmployeePositionId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        [InverseProperty("EmployeePosition")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
