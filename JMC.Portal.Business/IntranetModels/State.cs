using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("State")]
    [Index("CountryId", "Abbreviation", Name = "IX_State", IsUnique = true)]
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        [Key]
        [Column("StateID")]
        public int StateID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("CountryID")]
        public int CountryID { get; set; }
        [StringLength(3)]
        [Unicode(false)]
        public string? Abbreviation { get; set; }
        public bool Active { get; set; }

        [ForeignKey("CountryID")]
        [InverseProperty("States")]
        public virtual Country Country { get; set; } = null!;
        [InverseProperty("State")]
        public virtual ICollection<City> Cities { get; set; }
    }
}
