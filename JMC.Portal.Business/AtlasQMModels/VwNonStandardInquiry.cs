using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Keyless]
    public partial class VwNonStandardInquiry
    {
        [Column("NonStandardInquiryID")]
        public int NonStandardInquiryId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column("CustomerID")]
        public int? CustomerId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? PotentialCustomer { get; set; }
    }
}
