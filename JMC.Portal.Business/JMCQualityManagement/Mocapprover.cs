using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("MOCApprovers")]
    public partial class Mocapprover
    {
        public Mocapprover()
        {
            Mocapprovals = new HashSet<Mocapproval>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("PlantID")]
        public int? PlantId { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Title { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }

        [InverseProperty("Approver")]
        public virtual ICollection<Mocapproval> Mocapprovals { get; set; }
    }
}
