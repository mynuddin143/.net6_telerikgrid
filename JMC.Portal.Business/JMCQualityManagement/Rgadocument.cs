using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("RGADocument")]
    public partial class Rgadocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("RGAFormID")]
        public int RgaformId { get; set; }
        [StringLength(255)]
        public string? DocumentName { get; set; }
    }
}
