using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class UserProfile
    {
        public long UserProfileId { get; set; }
        public long UserId { get; set; }
        public long ApplicationId { get; set; }
        public string SettingName { get; set; } = null!;

        public virtual Application Application { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
