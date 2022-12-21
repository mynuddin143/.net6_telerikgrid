using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Country")]
    [Index("Name", "Abbreviation", Name = "IX_Country", IsUnique = true)]
    public partial class Country
    {
        public Country()
        {
            States = new HashSet<State>();
        }

        [Key]
        [Column("CountryID")]
        public int CountryId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(3)]
        [Unicode(false)]
        public string? Abbreviation { get; set; }
        public bool Active { get; set; }

        [InverseProperty("Country")]
        public virtual ICollection<State> States { get; set; }
    }
}
