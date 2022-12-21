using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Rectangle
    {
        public int RectangleId { get; set; }
        public decimal Size1 { get; set; }
        public decimal Size2 { get; set; }
        public string MaterialGroup { get; set; } = null!;
    }
}
