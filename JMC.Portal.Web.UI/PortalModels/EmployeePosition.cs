using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class EmployeePosition
    {
        public EmployeePosition()
        {
            Employees = new HashSet<Employee>();
        }

        public long EmployeePositionId { get; set; }
        public string Name { get; set; } = null!;
        public bool? Active { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
