// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    public partial class QuickSearchCriterion
    {
        [Key]
        public long CriteriaId { get; set; }
        public long TemplateId { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? SizeX { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? SizeY { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? Diameter { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Length { get; set; }
        [StringLength(40)]
        public string GaugeRestrictable { get; set; }
        [StringLength(40)]
        public string Grade { get; set; }
        public int? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Inches { get; set; }
        public long ShiptoId { get; set; }
        public long SoldToId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RequestedDate { get; set; }
        [StringLength(25)]
        public string Shape { get; set; }

        [ForeignKey(nameof(TemplateId))]
        [InverseProperty(nameof(QuickSearchTemplate.QuickSearchCriteria))]
        public virtual QuickSearchTemplate Template { get; set; }
    }
}