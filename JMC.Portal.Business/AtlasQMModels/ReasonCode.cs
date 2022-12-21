using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("ReasonCode")]
    public partial class ReasonCode
    {
        public ReasonCode()
        {
            Complaints = new HashSet<Complaint>();
        }

        [Key]
        [Column("ReasonCodeID")]
        public int ReasonCodeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public bool Customer { get; set; }

        [InverseProperty("ReasonCode")]
        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}
