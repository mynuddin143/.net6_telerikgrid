using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("PlantComputer")]
    public partial class PlantComputer
    {
        [Key]
        [Column("PlantComputerID")]
        public int PlantComputerID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ComputerName { get; set; } = null!;
        [Column("LocationID")]
        public int? LocationID { get; set; }
        [Column("DepartmentID")]
        public int? DepartmentId { get; set; }
        [Column("DivisionID")]
        public int? DivisionID { get; set; }

        [ForeignKey("DivisionID")]
        [InverseProperty("PlantComputers")]
        public virtual Division? Division { get; set; }
        [ForeignKey("PlantComputerID")]
        [InverseProperty("PlantComputer")]
        public virtual User PlantComputerNavigation { get; set; } = null!;
    }
}
