using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class PriceChangeSetting
    {
        public long PriceChangeSettingId { get; set; }
        public string Name { get; set; } = null!;
        public decimal? DecimalValue { get; set; }
        public int? IntegerValue { get; set; }
        public string? StringValue { get; set; }
        public bool? BooleanValue { get; set; }
    }
}
