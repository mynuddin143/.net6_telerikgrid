using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Division")]
    public partial class Division
    {
        public Division()
        {
            Employees = new HashSet<Employee>();
            Locations = new HashSet<Location>();
            PlantComputers = new HashSet<PlantComputer>();
        }

        [Key]
        [Column("DivisionID")]
        public int DivisionID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [InverseProperty("Division")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Division")]
        public virtual ICollection<Location> Locations { get; set; }
        [InverseProperty("Division")]
        public virtual ICollection<PlantComputer> PlantComputers { get; set; }
    }
}
