using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Plant")]
    public partial class Plant
    {
        public Plant()
        {
            Mills = new HashSet<Mill>();
        }

        [Key]
        [Column("PlantID")]
        public int PlantID { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string PlantCode { get; set; } = null!;
        [StringLength(4)]
        [Unicode(false)]
        public string SalesOrganization { get; set; } = null!;
        public bool Active { get; set; }

        [ForeignKey("PlantID")]
        [InverseProperty("Plant")]
        public virtual Location PlantNavigation { get; set; } = null!;
        [InverseProperty("Plant")]
        public virtual ICollection<Mill> Mills { get; set; }

        [ForeignKey("LocationID")]
        [InverseProperty("AdditionalPhoneNumbers")]
        public virtual Location? Location { get; set; }
    }
}
