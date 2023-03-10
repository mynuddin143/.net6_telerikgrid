// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("Rectangle")]
    public partial class Rectangle
    {
        [Key]
        [Column("RectangleID")]
        public int RectangleId { get; set; }
        [Column(TypeName = "decimal(9, 4)")]
        public decimal Size1 { get; set; }
        [Column(TypeName = "decimal(9, 4)")]
        public decimal Size2 { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialGroup { get; set; }
    }
}