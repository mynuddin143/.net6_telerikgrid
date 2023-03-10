// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Keyless]
    public partial class PersonDataViewColumn
    {
        [Column("PersonDataViewColumnTemplateID")]
        public long PersonDataViewColumnTemplateId { get; set; }
        [Column("PersonDataViewTemplateID")]
        public long PersonDataViewTemplateId { get; set; }
        [Column("DataViewColumnID")]
        public long DataViewColumnId { get; set; }
        [Column("DataViewID")]
        public long DataViewId { get; set; }
        [Column("PersonID")]
        public long PersonId { get; set; }
        [Required]
        [StringLength(150)]
        public string FieldName { get; set; }
        [StringLength(50)]
        public string DisplayFieldName { get; set; }
        public int Width { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        public string GroupFooterTemplate { get; set; }
        public string FooterTemplate { get; set; }
        public string GroupHeaderTemplate { get; set; }
        public string HeaderTemplate { get; set; }
        [StringLength(250)]
        public string Format { get; set; }
        public string HtmlAttributeClass { get; set; }
        public string HtmlAttributeStyle { get; set; }
        [StringLength(4)]
        public string SortDirection { get; set; }
        public int? GroupByOrder { get; set; }
        [StringLength(4)]
        public string GroupByDirection { get; set; }
        public bool Encoded { get; set; }
        public bool Sortable { get; set; }
        public bool Filterable { get; set; }
        public bool Groupable { get; set; }
        public int Order { get; set; }
        public bool Visible { get; set; }
        public bool Printable { get; set; }
        public bool IncludeInMenu { get; set; }
        [StringLength(20)]
        public string FirstFilterOperator { get; set; }
        public string FirstFilterValue { get; set; }
        [StringLength(10)]
        public string Logic { get; set; }
        [StringLength(20)]
        public string SecondFilterOperator { get; set; }
        public string SecondFilterValue { get; set; }
    }
}