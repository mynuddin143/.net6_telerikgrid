using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class PersonDataViewTemplate
    {
        public PersonDataViewTemplate()
        {
            PersonDataViewColumnTemplates = new HashSet<PersonDataViewColumnTemplate>();
        }

        public long PersonDataViewTemplateId { get; set; }
        public long PersonId { get; set; }
        public long DataViewId { get; set; }

        public virtual DataView DataView { get; set; } = null!;
        public virtual ICollection<PersonDataViewColumnTemplate> PersonDataViewColumnTemplates { get; set; }
    }
}
