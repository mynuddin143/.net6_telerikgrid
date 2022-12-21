using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class PlantComputer
    {
        public long UserId { get; set; }
        public string ComputerName { get; set; } = null!;
        public long LocationId { get; set; }
        public long? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Location Location { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
