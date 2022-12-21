using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class QuickSearchTemplate
    {
        public QuickSearchTemplate()
        {
            QuickSearchCriteria = new HashSet<QuickSearchCriterion>();
        }

        public long TemplateId { get; set; }
        public long PersonId { get; set; }
        public string TemplateName { get; set; } = null!;
        public long SoldToId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<QuickSearchCriterion> QuickSearchCriteria { get; set; }
    }
}
