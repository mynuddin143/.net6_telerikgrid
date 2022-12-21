using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("QDRFormsDocument")]
    public partial class QdrformsDocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("QDRFormsID")]
        public int QdrformsId { get; set; }
        [StringLength(255)]
        public string? DocumentName { get; set; }
    }
}
