using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("ProductType")]
    public partial class ProductType
    {
        [Key]
        [Column("ProductTypeID")]
        public int ProductTypeID { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
    }
}
