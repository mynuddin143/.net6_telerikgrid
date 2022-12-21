using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPBundlingOption")]
    [Index("LengthLow", "LengthHigh", "Bundling1", "Bundling2", Name = "IX_SAPBundlingOption", IsUnique = true)]
    public partial class SAPBundlingOption
    {
        [Key]
        [Column("SAPBundlingOptionID")]
        public int SAPBundlingOptionId { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal LengthLow { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal LengthHigh { get; set; }
        public int Bundling1 { get; set; }
        public int Bundling2 { get; set; }
    }
}
