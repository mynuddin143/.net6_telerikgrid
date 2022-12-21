using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("NonConformanceType")]
    public partial class NonConformanceType
    {
        public NonConformanceType()
        {
            Complaints = new HashSet<Complaint>();
        }

        [Key]
        [Column("NonConformanceTypeID")]
        public int NonConformanceTypeId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        [InverseProperty("NonConformanceType")]
        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}
