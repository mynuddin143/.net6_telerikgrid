using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Employee
    {
        public Employee()
        {
            Quotes = new HashSet<Quote>();
        }

        public long UserId { get; set; }
        public long DivisionId { get; set; }
        public long? EmployeePositionId { get; set; }
        public long? LocationId { get; set; }
        public long? DepartmentId { get; set; }
        public string? SamaccountName { get; set; }
        public string? Domain { get; set; }
        public int? ManagerId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Division Division { get; set; } = null!;
        public virtual EmployeePosition? EmployeePosition { get; set; }
        public virtual Location? Location { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
