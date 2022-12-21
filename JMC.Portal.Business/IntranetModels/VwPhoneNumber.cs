using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Keyless]
    public partial class VwPhoneNumber
    {
        [StringLength(103)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? PhoneNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Extension { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? FaxNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string? EmployeePositionName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? DepartmentName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? LocationName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? DivisionName { get; set; }
        [Column("EmployeePositionID")]
        public int? EmployeePositionId { get; set; }
        [Column("DepartmentID")]
        public int? DepartmentId { get; set; }
        [Column("LocationID")]
        public int? LocationId { get; set; }
        [Column("DivisionID")]
        public int? DivisionId { get; set; }
        [Column("SAMAccountName")]
        [StringLength(50)]
        [Unicode(false)]
        public string? SamaccountName { get; set; }
    }
}
