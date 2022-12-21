using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Employee")]
    public partial class Employee
    {
        public Employee()
        {
            InsideSalesReps = new HashSet<InsideSalesRep>();
        }

        [Key]
        [Column("EmployeeID")]
        public int EmployeeID { get; set; }
        [Column("EmployeePositionID")]
        public int? EmployeePositionID { get; set; }
        [Column("LocationID")]
        public int LocationID { get; set; }
        [Column("DepartmentID")]
        public int DepartmentID { get; set; }
        [Column("DivisionID")]
        public int DivisionID { get; set; }
        [Column("SAMAccountName")]
        [StringLength(50)]
        [Unicode(false)]
        public string? SAMAccountName { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string? Domain { get; set; }
        [Column("ManagerID")]
        public int? ManagerId { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("Employees")]
        public virtual Department Department { get; set; } = null!;
        [ForeignKey("DivisionId")]
        [InverseProperty("Employees")]
        public virtual Division Division { get; set; } = null!;
        [ForeignKey("EmployeeId")]
        [InverseProperty("Employee")]
        public virtual User EmployeeNavigation { get; set; } = null!;
        [ForeignKey("EmployeePositionId")]
        [InverseProperty("Employees")]
        public virtual EmployeePosition? EmployeePosition { get; set; }
        [ForeignKey("LocationId")]
        [InverseProperty("Employees")]
        public virtual Location Location { get; set; } = null!;
        [InverseProperty("Employee")]
        public virtual ICollection<InsideSalesRep> InsideSalesReps { get; set; }
    }
}
