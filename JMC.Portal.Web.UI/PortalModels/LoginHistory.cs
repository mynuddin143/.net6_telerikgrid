using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class LoginHistory
    {
        public long LoginHistoryId { get; set; }
        public long UserId { get; set; }
        public DateTime LoginDate { get; set; }
        public string? UserAgent { get; set; }
        public string? BrowserName { get; set; }
        public string? BrowserVersion { get; set; }
        public bool? Cookies { get; set; }
        public bool? Javascript { get; set; }
        public string? Platform { get; set; }
        public string? ScreenResolution { get; set; }
        public string? IpAddress { get; set; }
        public string? HostName { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
