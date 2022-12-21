using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class PersonDataViewColumnTemplate
    {
        public long PersonDataViewColumnTemplateId { get; set; }
        public long PersonDataViewTemplateId { get; set; }
        public long DataViewColumnId { get; set; }
        public int Width { get; set; }
        public bool Visible { get; set; }
        public int Order { get; set; }
        public string? SortDirection { get; set; }
        public int? GroupByOrder { get; set; }
        public string? GroupByDirection { get; set; }
        public string? FirstFilter { get; set; }
        public string? FirstFilterOperator { get; set; }
        public string? FirstFilterValue { get; set; }
        public string? Logic { get; set; }
        public string? SecondFilter { get; set; }
        public string? SecondFilterOperator { get; set; }
        public string? SecondFilterValue { get; set; }

        public virtual DataViewColumn DataViewColumn { get; set; } = null!;
        public virtual PersonDataViewTemplate PersonDataViewTemplate { get; set; } = null!;
    }
}
