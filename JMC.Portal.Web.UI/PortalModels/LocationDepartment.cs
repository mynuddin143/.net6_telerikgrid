using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class LocationDepartment
    {
        public long LocationId { get; set; }
        public long DepartmentId { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual Location Location { get; set; } = null!;
    }
}
