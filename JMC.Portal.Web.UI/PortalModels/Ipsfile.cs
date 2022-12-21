using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Ipsfile
    {
        public long IpsfileId { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
