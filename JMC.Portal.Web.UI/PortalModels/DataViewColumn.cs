using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class DataViewColumn
    {
        public DataViewColumn()
        {
            PersonDataViewColumnTemplates = new HashSet<PersonDataViewColumnTemplate>();
        }

        public long DataViewColumnId { get; set; }
        public long DataViewId { get; set; }
        public string FieldName { get; set; } = null!;
        public string? DisplayFieldName { get; set; }
        public int Width { get; set; }
        public string Title { get; set; } = null!;
        public string? GroupFooterTemplate { get; set; }
        public string? FooterTemplate { get; set; }
        public string? GroupHeaderTemplate { get; set; }
        public string? HeaderTemplate { get; set; }
        public string? HtmlAttributeClass { get; set; }
        public string? HtmlAttributeStyle { get; set; }
        public string? SortDirection { get; set; }
        public int? GroupByOrder { get; set; }
        public string? GroupByDirection { get; set; }
        public bool Encoded { get; set; }
        public bool Sortable { get; set; }
        public bool Filterable { get; set; }
        public bool Groupable { get; set; }
        public int Order { get; set; }
        public bool Visible { get; set; }
        public bool Printable { get; set; }
        public string? Format { get; set; }
        public bool IncludeInMenu { get; set; }

        public virtual DataView DataView { get; set; } = null!;
        public virtual ICollection<PersonDataViewColumnTemplate> PersonDataViewColumnTemplates { get; set; }
    }
}
