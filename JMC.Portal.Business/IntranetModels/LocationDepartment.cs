using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Keyless]
    [Table("LocationDepartment")]
    public partial class LocationDepartment
    {
        [Column("LocationID")]
        public int LocationID { get; set; }
        [Column("DepartmentID")]
        public int DepartmentID{ get; set; }

        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; } = null!;
        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; } = null!;
    }
}
