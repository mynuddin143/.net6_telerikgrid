using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("CustomerSalesOrg")]
    [Index("CustomerId", "SalesOrgId", Name = "IX_CustomerSalesOrg", IsUnique = true)]
    public partial class CustomerSalesOrg
    {
        [Key]
        [Column("CustomerSalesOrgID")]
        public int CustomerSalesOrgId { get; set; }
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [Column("SalesOrgID")]
        public int SalesOrgId { get; set; }
        [Column("DefaultShipToID")]
        public int DefaultShipToId { get; set; }
    }
}
