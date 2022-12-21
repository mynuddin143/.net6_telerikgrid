using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Keyless]
    public partial class VwChangeManagement
    {
        [Column("ChangeManagementID")]
        public int ChangeManagementId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? ChangeStatus { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Description { get; set; }
    }
}
