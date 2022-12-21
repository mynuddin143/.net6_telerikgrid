using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class DataView
    {
        public DataView()
        {
            DataViewColumns = new HashSet<DataViewColumn>();
            PersonDataViewTemplates = new HashSet<PersonDataViewTemplate>();
        }

        public long DataViewId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<DataViewColumn> DataViewColumns { get; set; }
        public virtual ICollection<PersonDataViewTemplate> PersonDataViewTemplates { get; set; }
    }
}
