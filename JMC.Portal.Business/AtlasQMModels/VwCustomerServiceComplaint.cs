using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Keyless]
    public partial class VwCustomerServiceComplaint
    {
        [Column("CustomerServiceComplaintID")]
        public int CustomerServiceComplaintId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CustomerName { get; set; } = null!;
        [Column("PlantID")]
        public int PlantId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PlantName { get; set; } = null!;
    }
}
