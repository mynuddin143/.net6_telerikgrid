using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("Classification")]
    public partial class Classification
    {
        public Classification()
        {
            Complaints = new HashSet<Complaint>();
        }

        [Key]
        [Column("ClassificationID")]
        public int ClassificationId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(2000)]
        [Unicode(false)]
        public string Description { get; set; } = null!;
        public bool Active { get; set; }

        [InverseProperty("Classification")]
        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}
