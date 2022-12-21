using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Keyless]
    public partial class VwEmployee
    {
        [StringLength(52)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("EmployeeID")]
        public int EmployeeId { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string UserName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; } = null!;
        public bool Active { get; set; }
        [Column("LocationID")]
        public int LocationId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? LocationName { get; set; }
        [Column("DepartmentID")]
        public int DepartmentId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? DepartmentName { get; set; }
        [Column("DivisionID")]
        public int DivisionId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? DivisionName { get; set; }
        [Column("EmployeePositionID")]
        public int? EmployeePositionId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? PositionName { get; set; }
    }
}
