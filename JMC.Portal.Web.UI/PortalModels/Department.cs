using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
            PlantComputers = new HashSet<PlantComputer>();
        }

        public long DepartmentId { get; set; }
        public string Name { get; set; } = null!;
        public bool? Active { get; set; }
        public string? Adname { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<PlantComputer> PlantComputers { get; set; }
    }
}
