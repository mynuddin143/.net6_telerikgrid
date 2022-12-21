using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("CustomerShipTo")]
    [Index("CustomerId", "ShipToId", Name = "IX_CustomerShipTo", IsUnique = true)]
    public partial class CustomerShipTo
    {
        [Key]
        [Column("CustomerShipToID")]
        public int CustomerShipToId { get; set; }
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [Column("ShipToID")]
        public int ShipToId { get; set; }
    }
}
